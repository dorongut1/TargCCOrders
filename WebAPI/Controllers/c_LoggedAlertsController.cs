using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc; 
using TargCCOrders.DataController; 
using TargCCOrders.WebAPI.Dtos; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
 
namespace TargCCOrders.WebAPI.Controllers 
{ 
    [Route("api")] 
    [ApiController] 
    public partial class LoggedAlertsController : ControllerBase 
    { 
        // GET api/loggedAlerts?page=0&pageSize=25&search=xyz 
        [Route("loggedAlerts")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? affectedUserId = null, [FromQuery] long? loggedLoginId = null) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            clsFault? fault = null; 
            csLoggedAlertCol loggedAlerts = new csLoggedAlertCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = loggedAlerts.Select(p => p.ToDto()); 
 
            if (affectedUserId.HasValue) allItems = allItems.Where(item => item.FkAffectedUserId == affectedUserId.Value); 
            if (loggedLoginId.HasValue) allItems = allItems.Where(item => item.FkLoggedLoginId == loggedLoginId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FaultNumber.ToString().Contains(searchLower) ||
                    (item.SystemName ?? "").ToLower().Contains(searchLower) ||
                    (item.CallingApplication ?? "").ToLower().Contains(searchLower) ||
                    item.FkAffectedUserId.ToString().Contains(searchLower) ||
                    (item.CallingApplicationVersion ?? "").ToLower().Contains(searchLower) ||
                    (item.CallingFunctionWithinApplication ?? "").ToLower().Contains(searchLower) ||
                    (item.FreeText ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultingAssembly ?? "").ToLower().Contains(searchLower) ||
                    (item.AssemblyEntryPoint ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultingClass ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultingFunction ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultingFunctionParameters ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultIdent ?? "").ToLower().Contains(searchLower) ||
                    (item.FaultDescription ?? "").ToLower().Contains(searchLower) ||
                    (item.MessageSentToUser ?? "").ToLower().Contains(searchLower) ||
                    (item.ActionSentToUser ?? "").ToLower().Contains(searchLower) ||
                    item.EnmFaultType.ToString().ToLower().Contains(searchLower) ||
                    item.EnmFaultSeverity.ToString().ToLower().Contains(searchLower) ||
                    item.FkLoggedLoginId.ToString().Contains(searchLower) ||
                    (item.Thread ?? "").ToLower().Contains(searchLower) ||
                    (item.LkpUserIdentityTypeCode ?? "").ToLower().Contains(searchLower) ||
                    item.LkpUserIdentityTypeNameCode.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(LoggedAlertDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
                if (prop != null) 
                { 
                    itemsList = sortDir?.ToLower() == "desc" 
                        ? itemsList.OrderByDescending(x => prop.GetValue(x)).ToList() 
                        : itemsList.OrderBy(x => prop.GetValue(x)).ToList(); 
                } 
            } 
 
            var pagedItems = itemsList.Skip(page * pageSize).Take(pageSize); 
 
            return Ok(new { items = pagedItems, total }); 
        } 
 
        // GET api/loggedAlerts/{id} 
        [Route("loggedAlerts/{id}")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<LoggedAlertDto> GetByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csLoggedAlert loggedAlert = new csLoggedAlert(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = loggedAlert.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(loggedAlert.ToDto()); 
        } 
 
 
 
    } 
} 
