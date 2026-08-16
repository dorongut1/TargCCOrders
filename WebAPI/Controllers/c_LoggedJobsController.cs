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
    public partial class LoggedJobsController : ControllerBase 
    { 
        // GET api/loggedJobs?page=0&pageSize=25&search=xyz 
        [Route("loggedJobs")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? jobId = null, [FromQuery] long? loggedAlertId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csLoggedJobCol loggedJobs = new csLoggedJobCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = loggedJobs.Select(p => p.ToDto()); 
 
            if (jobId.HasValue) allItems = allItems.Where(item => item.FkJobId == jobId.Value); 
            if (loggedAlertId.HasValue) allItems = allItems.Where(item => item.FkLoggedAlertId == loggedAlertId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkJobId.ToString().Contains(searchLower) ||
                    (item.ActivatingUser ?? "").ToLower().Contains(searchLower) ||
                    (item.LastRunBy ?? "").ToLower().Contains(searchLower) ||
                    item.ExecutionTimeSec.ToString().Contains(searchLower) ||
                    item.EnmRunStatus.ToString().ToLower().Contains(searchLower) ||
                    (item.Remarks ?? "").ToLower().Contains(searchLower) ||
                    item.FkLoggedAlertId.ToString().Contains(searchLower) ||
                    item.SuccessCount.ToString().Contains(searchLower) ||
                    item.FailureCount.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(LoggedJobDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/loggedJobs/{id} 
        [Route("loggedJobs/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<LoggedJobDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csLoggedJob loggedJob = new csLoggedJob(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = loggedJob.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(loggedJob.ToDto()); 
        } 
 
 
 
    } 
} 
