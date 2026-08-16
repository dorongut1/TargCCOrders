using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TargCCOrders.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Policy = "AdminUI")]
    public class EnumsController : ControllerBase
    {
        /// <summary>
        /// Dropdown values for every enm* enum.
        ///
        /// Two things this endpoint has to get right:
        ///
        /// 1. THE LABEL. The .NET member name is English ("Cancelled", "New").
        ///    The Hebrew text lives in c_Enumeration.locText, keyed by
        ///    (EnumType, EnumValue) where EnumValue is the English member name.
        ///    Labels are taken from there and fall back to the member name.
        ///
        /// 2. THE NUMBER IS NOT STABLE. TargCC emits members alphabetically, so
        ///    adding one value renumbers everything after it — which is exactly
        ///    how the order form ended up defaulting to Cancelled(1) while
        ///    meaning New. 'name' is therefore returned alongside 'value', and
        ///    clients should match on name.
        /// </summary>
        [Route("enums")]
        [HttpGet]
        public ActionResult GetAllEnums()
        {
            var translations = LoadTranslations();
            var result = new List<object>();

            var nestedTypes = typeof(clsEnums).GetNestedTypes()
                .Where(t => t.IsEnum && t.Name.StartsWith("enm"));

            foreach (var enumType in nestedTypes)
            {
                var enumName = enumType.Name.Substring(3); // strip "enm"

                var values = Enum.GetValues(enumType)
                    .Cast<object>()
                    .Select(v =>
                    {
                        var memberName = v.ToString() ?? "";
                        var key = enumName + "|" + memberName;
                        return new
                        {
                            value = (int)Convert.ChangeType(v, typeof(int)),
                            name = memberName,
                            label = translations.TryGetValue(key, out var heb) && !string.IsNullOrWhiteSpace(heb)
                                    ? heb
                                    : memberName
                        };
                    })
                    // "UD" is TargCC's internal Undefined placeholder — never a
                    // real choice, and offering it invites saving a junk value.
                    .Where(x => !string.Equals(x.name, "UD", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                result.Add(new { enumType = enumName, values });
            }

            return Ok(result);
        }

        /// <summary>
        /// "{EnumType}|{EnumValue}" -> Hebrew locText, read straight from
        /// c_Enumeration. Read via the collection rather than raw SQL so it goes
        /// through TargCC's own permission and audit path. A failure here is not
        /// fatal: the caller degrades to English member names.
        /// </summary>
        private Dictionary<string, string> LoadTranslations()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var requester = RequesterFactory.FromUser(User);
                clsFault? fault = null;
                // c_Enumeration is a localized system table: its collection takes
                // an IsLocalized flag, not a LoadParent. Passing true makes
                // TextLocalized resolve for the requester's language, with Text
                // (the raw locText column) as the fallback.
                var rows = new csEnumerationCol(true, requester, ref fault);
                if (fault == null || !fault.isOK) return map;

                foreach (csEnumeration row in rows)
                {
                    if (string.IsNullOrWhiteSpace(row.EnumType) || string.IsNullOrWhiteSpace(row.EnumValue))
                        continue;
                    var text = !string.IsNullOrWhiteSpace(row.TextLocalized) ? row.TextLocalized : row.Text;
                    map[row.EnumType.Trim() + "|" + row.EnumValue.Trim()] = text ?? "";
                }
            }
            catch
            {
                // Fall through to English labels.
            }
            return map;
        }
    }
}
