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
    public partial class LoggedLoginsController : ControllerBase 
    { 
        // GET api/loggedLogins?page=0&pageSize=25&search=xyz 
        [Route("loggedLogins")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            clsFault? fault = null; 
            csLoggedLoginCol loggedLogins = new csLoggedLoginCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = loggedLogins.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.UserName ?? "").ToLower().Contains(searchLower) ||
                    (item.UserFullName ?? "").ToLower().Contains(searchLower) ||
                    (item.ApplicationName ?? "").ToLower().Contains(searchLower) ||
                    (item.LkpUserIdentityTypeCode ?? "").ToLower().Contains(searchLower) ||
                    item.LkpUserIdentityTypeNameCode.ToString().Contains(searchLower) ||
                    (item.Roles ?? "").ToLower().Contains(searchLower) ||
                    item.LoginFaultNumber.ToString().Contains(searchLower) ||
                    (item.EnvironmentUserName ?? "").ToLower().Contains(searchLower) ||
                    (item.EnvironmentMachineName ?? "").ToLower().Contains(searchLower) ||
                    (item.EnvironmentUserDomainName ?? "").ToLower().Contains(searchLower) ||
                    (item.DnsGetHostName ?? "").ToLower().Contains(searchLower) ||
                    (item.AddressList ?? "").ToLower().Contains(searchLower) ||
                    (item.ComputerMacAddress ?? "").ToLower().Contains(searchLower) ||
                    (item.SystemDiskVolumeSerialNo ?? "").ToLower().Contains(searchLower) ||
                    (item.AccessingComputerDetails ?? "").ToLower().Contains(searchLower) ||
                    (item.UiCulture ?? "").ToLower().Contains(searchLower) ||
                    item.TotalPhysicalMemoryKb.ToString().Contains(searchLower) ||
                    item.AvailablePhysicalMemoryKb.ToString().Contains(searchLower) ||
                    (item.ApplicationVersion ?? "").ToLower().Contains(searchLower) ||
                    (item.OriginatingIp ?? "").ToLower().Contains(searchLower) ||
                    item.EnmLanguage.ToString().ToLower().Contains(searchLower) ||
                    (item.HostingAssembly ?? "").ToLower().Contains(searchLower) ||
                    (item.OriginatingCountry ?? "").ToLower().Contains(searchLower) ||
                    (item.ClientReportedIp ?? "").ToLower().Contains(searchLower) ||
                    (item.ClientReportedCountry ?? "").ToLower().Contains(searchLower) ||
                    (item.IpAdditionalDetails ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(LoggedLoginDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/loggedLogins/{id} 
        [Route("loggedLogins/{id}")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<LoggedLoginDto> GetByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csLoggedLogin loggedLogin = new csLoggedLogin(); 
            clsFault fault = loggedLogin.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(loggedLogin.ToDto()); 
        } 
 
 
 
    } 
} 
