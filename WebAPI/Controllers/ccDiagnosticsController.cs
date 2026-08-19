using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Development-only checks that exercise the VB data layer directly.
    ///
    /// These exist because the failures worth catching in this project are
    /// round-trip failures — a value that compiles, saves without error, and is
    /// quietly different when read back. Proving that needs the real data
    /// layer, not a mock, and needs to be repeatable rather than a one-off
    /// session in a browser.
    ///
    /// Every action refuses outside Development. Production runs with
    /// ASPNETCORE_ENVIRONMENT=Production (verified: /swagger returns 404 there),
    /// so none of this is reachable on the live site.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics")]
    [AllowAnonymous]
    public class ccDiagnosticsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ccDiagnosticsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Loads an order through the VB layer and saves it straight back with
        /// no edits, reporting what the delivery method looked like at each
        /// step.
        ///
        /// A value the enum does not know is read as UD and written back as the
        /// literal "UD", so a no-op save destroys it. This is the check that
        /// demonstrates that, and the same check should pass once the layer is
        /// made transparent to unknown values.
        ///
        /// The caller reads the database before and after; this only reports
        /// what the data layer itself saw and did.
        /// </summary>
        [HttpPost("orderRoundTrip/{id}")]
        public ActionResult OrderRoundTrip(long id)
        {
            if (!_environment.IsDevelopment())
                return NotFound();

            // SecurityExempt is deliberate and confined to this file: the
            // subject under test is the data round-trip, not the permission
            // model, and requiring a login would make the check unrunnable
            // without a human.
            var requester = new clsRequester("Diagnostics", "View", true);
            requester.CallingFunctionWithinApplication = "Diagnostics";

            var order = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad);
            var fault = order.GetByID(id, requester);
            if (!fault.isOK)
                return NotFound(new { step = "load", message = fault.Message });

            var readAs = order.DeliveryMethod.ToString();

            fault = order.Update(requester);
            if (!fault.isOK)
                return BadRequest(new { step = "save", readAs, message = fault.Message });

            return Ok(new
            {
                orderId = id,
                readAs,
                note = "Compare the database column before and after this call. "
                     + "If it changed, the data layer rewrote a value it did not recognise."
            });
        }
    }
}
