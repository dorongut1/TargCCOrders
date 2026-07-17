using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Safe field application for batch PATCH.
    /// Only fields that exist on the entity's UpdateDto (the client-writable
    /// allow-list) may be set, and conversion failures are reported instead of
    /// being silently swallowed. This replaces the previous unrestricted
    /// reflection over the VB entity, which allowed clients to set
    /// server-only business fields (e.g. UnitCost) and hid all errors.
    /// </summary>
    public static class PatchHelper
    {
        public static List<string> ApplyFields(object entity, Type updateDtoType, Dictionary<string, object?> fields)
        {
            var errors = new List<string>();

            // Allow-list: properties the UpdateDto exposes (minus identity/etag)
            var allowed = updateDtoType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .Where(n => !string.Equals(n, "Id", StringComparison.OrdinalIgnoreCase)
                         && !n.StartsWith("_", StringComparison.Ordinal))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var kvp in fields)
            {
                var fieldName = kvp.Key;

                // Map DTO FK naming (FkCustomerId) to entity naming (CustomerID)
                var entityName = fieldName;
                if (entityName.StartsWith("fk", StringComparison.OrdinalIgnoreCase) && entityName.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
                    entityName = entityName.Substring(2, entityName.Length - 4) + "ID";

                if (!allowed.Contains(fieldName))
                {
                    errors.Add($"Field '{fieldName}' is not updatable.");
                    continue;
                }

                var prop = entity.GetType().GetProperty(entityName,
                               BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        ?? entity.GetType().GetProperty(fieldName,
                               BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop == null || !prop.CanWrite)
                {
                    errors.Add($"Field '{fieldName}' is not updatable.");
                    continue;
                }

                try
                {
                    prop.SetValue(entity, ConvertValue(kvp.Value, prop.PropertyType));
                }
                catch (Exception ex)
                {
                    errors.Add($"Field '{fieldName}': {ex.Message}");
                }
            }

            return errors;
        }

        private static object? ConvertValue(object? value, Type targetType)
        {
            var underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (value == null)
            {
                if (targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null)
                    throw new InvalidOperationException("Null is not allowed.");
                return null;
            }

            var s = value.ToString() ?? "";

            if (underlying.IsEnum)
            {
                if (int.TryParse(s, out var num)) return Enum.ToObject(underlying, num);
                return Enum.Parse(underlying, s, ignoreCase: true);
            }
            if (underlying == typeof(DateTime)) return DateTime.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
            if (underlying == typeof(bool)) return s == "1" || string.Equals(s, "true", StringComparison.OrdinalIgnoreCase);

            return Convert.ChangeType(s, underlying, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
