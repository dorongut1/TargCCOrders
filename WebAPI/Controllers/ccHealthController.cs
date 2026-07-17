using Microsoft.AspNetCore.Mvc; 
using TargCCOrders.DataController; 
using System; 
 
namespace TargCCOrders.WebAPI.Controllers 
{ 
    [Route("api")] 
    [ApiController] 
    public class HealthController : ControllerBase 
    { 
        // GET api/health — public, no [Authorize] 
        [Route("health")] 
        [HttpGet] 
        public ActionResult GetHealth() 
        { 
            try
            {
                // Actually test the DB connection: a tiny system-table read.
                var requester = new clsRequester("SystemDefault", "View", true);
                var setting = new csSystemDefault();
                var fault = setting.GetByGroupAndSettingName("Business", "VATRatePercent", requester);
                if (fault == null || !fault.isOK)
                    return StatusCode(503, new { status = "error", message = "Database check failed" });

                return Ok(new {
                    status = "ok",
                    db = "ok",
                    timestamp = DateTime.UtcNow,
                    version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0"
                });
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Health check failed");
                return StatusCode(503, new { status = "error", message = "Database unreachable" });
            }
        } 
    } 
} 
