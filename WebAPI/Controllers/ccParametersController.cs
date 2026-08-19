using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Lets the business manage how the system's lists are presented: the
    /// Hebrew label, the order values appear in, whether a value is still
    /// offered on new records, and per-type flags such as IsDelivery.
    ///
    /// What it does not do is add a value. The set of values comes from the
    /// compiled enum -- /api/enums builds its list with Enum.GetValues -- so a
    /// new one is a code change in csSptEnums.vb. That was measured rather
    /// than assumed; see SPIKE_1.1_ENUM_EXTENSIBILITY_2026-08-18.md.
    ///
    /// Reads and writes two tables directly rather than through the VB layer:
    /// c_Enumeration for the label, EnumMetadata for the rest. EnumMetadata is
    /// not a TargCC table on purpose, so it stays clear of positional column
    /// reads.
    /// </summary>
    [ApiController]
    [Route("api/parameters")]
    [Authorize(Policy = "AdminUI")]
    public class ccParametersController : ControllerBase
    {
        /// <summary>Types the business is meant to manage. Deliberately a
        /// short list: c_Enumeration also holds framework types such as
        /// FaultSeverity and JobStatus, which mean nothing to a user and
        /// would be dangerous to reorder.</summary>
        private static readonly (string Type, string Label, bool ShowDeliveryFlag)[] ManagedTypes =
        {
            ("DeliveryMethod",  "צורות משלוח",   true),
            ("PaymentMethod",   "אמצעי תשלום",   false),
            ("Category",        "קטגוריות מוצר", false),
            ("CustomerType",    "סיווגי לקוח",   false),
            ("DeliveryDay",     "ימי משלוח",     false),
        };

        public class ParameterValueDto
        {
            public string EnumType { get; set; } = "";
            public string EnumValue { get; set; } = "";
            public string Label { get; set; } = "";
            public bool IsActive { get; set; }
            public bool IsDelivery { get; set; }
            public int SortOrder { get; set; }
            /// <summary>How many live records use this value. The screen shows
            /// it so nobody hides a value that half the orders depend on
            /// without knowing.</summary>
            public int UsageCount { get; set; }
        }

        public class ParameterTypeDto
        {
            public string EnumType { get; set; } = "";
            public string Label { get; set; } = "";
            public bool ShowDeliveryFlag { get; set; }
            public List<ParameterValueDto> Values { get; set; } = new();
        }

        public class UpdateParameterRequest
        {
            public string Label { get; set; } = "";
            public bool IsActive { get; set; }
            public bool IsDelivery { get; set; }
            public int SortOrder { get; set; }
        }

        /// <summary>
        /// Builds the connection the same way Program.cs does, from the
        /// TargCCOrders.Controller app setting, so there is one definition of
        /// where the database is.
        /// </summary>
        private static SqlConnection OpenConnection()
        {
            var setting = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.Controller"];
            if (string.IsNullOrWhiteSpace(setting))
                throw new InvalidOperationException("Missing appSetting 'TargCCOrders.Controller'.");

            var parts = setting.Split('~');
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = parts.ElementAtOrDefault(0) ?? "",
                InitialCatalog = parts.ElementAtOrDefault(1) ?? "",
                ConnectTimeout = 10,
                TrustServerCertificate = true
            };

            var user = parts.ElementAtOrDefault(3);
            if (!string.IsNullOrWhiteSpace(user))
            {
                builder.UserID = user;
                builder.Password = parts.ElementAtOrDefault(4) ?? "";
            }
            else
            {
                builder.IntegratedSecurity = true;
            }

            var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>Column holding this enum type on the table that uses it,
        /// for the usage count. Types absent here simply report 0.</summary>
        private static readonly Dictionary<string, (string Table, string Column)> UsageSources =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["DeliveryMethod"] = ("OrderHeader", "enmDeliveryMethod"),
                ["PaymentMethod"]  = ("OrderHeader", "enmPaymentMethod"),
                ["DeliveryDay"]    = ("OrderHeader", "enmDeliveryDay"),
                ["Category"]       = ("Product", "enmCategory"),
                ["CustomerType"]   = ("Customer", "enmCustomerType"),
            };

        [HttpGet]
        public ActionResult<IEnumerable<ParameterTypeDto>> GetAll()
        {
            try { RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var result = new List<ParameterTypeDto>();

            using var connection = OpenConnection();

            foreach (var (type, label, showDeliveryFlag) in ManagedTypes)
            {
                var dto = new ParameterTypeDto
                {
                    EnumType = type,
                    Label = label,
                    ShowDeliveryFlag = showDeliveryFlag
                };

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT e.EnumValue,
                               ISNULL(CAST(e.locText AS NVARCHAR(200)), N'') AS Label,
                               ISNULL(m.IsActive, 1)   AS IsActive,
                               ISNULL(m.IsDelivery, 0) AS IsDelivery,
                               ISNULL(m.SortOrder, 0)  AS SortOrder
                        FROM c_Enumeration e
                        LEFT JOIN dbo.EnumMetadata m
                               ON m.EnumType = e.EnumType AND m.EnumValue = e.EnumValue
                        WHERE e.EnumType = @type AND e.DeletedOn IS NULL
                        ORDER BY ISNULL(m.SortOrder, 0), e.EnumValue;";
                    command.Parameters.AddWithValue("@type", type);

                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dto.Values.Add(new ParameterValueDto
                        {
                            EnumType = type,
                            EnumValue = reader.GetString(0),
                            Label = reader.GetString(1),
                            IsActive = reader.GetBoolean(2),
                            IsDelivery = reader.GetBoolean(3),
                            SortOrder = reader.GetInt32(4)
                        });
                    }
                }

                ApplyUsageCounts(connection, type, dto.Values);
                result.Add(dto);
            }

            return Ok(result);
        }

        /// <summary>
        /// Counts live records per value in one grouped query rather than one
        /// query per value. The table and column names come from a fixed
        /// dictionary above, never from the request, so the interpolation
        /// below cannot carry user input.
        /// </summary>
        private static void ApplyUsageCounts(SqlConnection connection, string type, List<ParameterValueDto> values)
        {
            if (!UsageSources.TryGetValue(type, out var source)) return;

            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    $"SELECT [{source.Column}] AS V, COUNT(*) AS N " +
                    $"FROM [{source.Table}] " +
                    $"WHERE DeletedOn IS NULL AND [{source.Column}] IS NOT NULL " +
                    $"GROUP BY [{source.Column}];";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                    counts[reader.GetString(0)] = reader.GetInt32(1);
            }

            foreach (var value in values)
                value.UsageCount = counts.TryGetValue(value.EnumValue, out var n) ? n : 0;
        }

        [HttpPut("{enumType}/{enumValue}")]
        public ActionResult Update(string enumType, string enumValue, [FromBody] UpdateParameterRequest request)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!ManagedTypes.Any(t => string.Equals(t.Type, enumType, StringComparison.OrdinalIgnoreCase)))
                return BadRequest(new { message = $"הרשימה '{enumType}' אינה ניתנת לעריכה." });

            if (string.IsNullOrWhiteSpace(request?.Label))
                return BadRequest(new { message = "התווית היא שדה חובה." });

            using var connection = OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE c_Enumeration
                    SET locText = @label, ChangedBy = @user, ChangedOn = GETDATE()
                    WHERE EnumType = @type AND EnumValue = @value AND DeletedOn IS NULL;";
                command.Parameters.AddWithValue("@label", request.Label.Trim());
                command.Parameters.AddWithValue("@user", requester.UserName ?? "");
                command.Parameters.AddWithValue("@type", enumType);
                command.Parameters.AddWithValue("@value", enumValue);
                if (command.ExecuteNonQuery() == 0)
                    return NotFound(new { message = "הערך לא נמצא." });
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    MERGE dbo.EnumMetadata AS target
                    USING (SELECT @type AS EnumType, @value AS EnumValue) AS source
                       ON target.EnumType = source.EnumType AND target.EnumValue = source.EnumValue
                    WHEN MATCHED THEN UPDATE SET
                        IsActive = @isActive, IsDelivery = @isDelivery, SortOrder = @sortOrder,
                        ChangedBy = @user, ChangedOn = GETDATE()
                    WHEN NOT MATCHED THEN INSERT
                        (EnumType, EnumValue, IsActive, IsDelivery, SortOrder, ChangedBy, ChangedOn)
                        VALUES (@type, @value, @isActive, @isDelivery, @sortOrder, @user, GETDATE());";
                command.Parameters.AddWithValue("@type", enumType);
                command.Parameters.AddWithValue("@value", enumValue);
                command.Parameters.AddWithValue("@isActive", request.IsActive);
                command.Parameters.AddWithValue("@isDelivery", request.IsDelivery);
                command.Parameters.AddWithValue("@sortOrder", request.SortOrder);
                command.Parameters.AddWithValue("@user", requester.UserName ?? "");
                command.ExecuteNonQuery();
            }

            return Ok(new { message = $"'{request.Label.Trim()}' עודכן." });
        }
    }
}
