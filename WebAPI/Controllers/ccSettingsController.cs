using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using System;
using System.Collections.Generic;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Business settings (VAT rate, debt thresholds, supplier email) read from
    /// c_SystemDefault (Group='Business') — seeded by DB_MIGRATION_2026-07-16.sql.
    /// Replaces hardcoded client-side constants (e.g. the 17% VAT literal).
    /// </summary>
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private static readonly string[] BusinessKeys =
        {
            "VATRatePercent",
            "DebtAmountThreshold",
            "DebtOverdueDays",
            "SupplierEmailBiobee"
        };

        // GET api/settings/business
        [HttpGet("business")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult GetBusinessSettings()
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var result = new Dictionary<string, string?>();
            foreach (var key in BusinessKeys)
            {
                try
                {
                    var setting = new csSystemDefault();
                    var fault = setting.GetByGroupAndSettingName("Business", key, requester);
                    result[key] = (fault != null && fault.isOK && setting.ID > 0) ? setting.SettingValue : null;
                }
                catch { result[key] = null; }
            }

            // Sensible fallbacks so the UI always has values
            result["VATRatePercent"] ??= "18";
            result["DebtAmountThreshold"] ??= "100";
            result["DebtOverdueDays"] ??= "10";

            return Ok(result);
        }
    }
}
