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
    public partial class JobAlertRecipientsController : ControllerBase 
    { 
        // GET api/jobAlertRecipients?page=0&pageSize=25&search=xyz 
        [Route("jobAlertRecipients")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? jobId = null, [FromQuery] long? userId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csJobAlertRecipientCol jobAlertRecipients = new csJobAlertRecipientCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = jobAlertRecipients.Select(p => p.ToDto()); 
 
            if (jobId.HasValue) allItems = allItems.Where(item => item.FkJobId == jobId.Value); 
            if (userId.HasValue) allItems = allItems.Where(item => item.FkUserId == userId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkJobId.ToString().Contains(searchLower) ||
                    item.FkUserId.ToString().Contains(searchLower) ||
                    item.EnmJobAlertType.ToString().ToLower().Contains(searchLower) ||
                    (item.OverrideName ?? "").ToLower().Contains(searchLower) ||
                    (item.OverrideEmailOrPhone ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(JobAlertRecipientDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/jobAlertRecipients/{id} 
        [Route("jobAlertRecipients/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<JobAlertRecipientDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csJobAlertRecipient jobAlertRecipient = new csJobAlertRecipient(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = jobAlertRecipient.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(jobAlertRecipient.ToDto()); 
        } 
 
        // POST api/jobAlertRecipients 
        [Route("jobAlertRecipients")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<JobAlertRecipientDto> CreateJobAlertRecipient(JobAlertRecipientUpdateDto jobAlertRecipientDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (jobAlertRecipientDto.Id != 0) return BadRequest($"Received an ID of {jobAlertRecipientDto.Id}. Expected 0 for a new record."); 
 
            csJobAlertRecipient jobAlertRecipient; 
 
            try 
            { 
                jobAlertRecipient = jobAlertRecipientDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = jobAlertRecipient.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = jobAlertRecipient.ToDto().Id }, jobAlertRecipient.ToDto()); 
        } 
 
        // PUT api/jobAlertRecipients 
        [Route("jobAlertRecipients/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<JobAlertRecipientDto> UpdateJobAlertRecipient(long id, JobAlertRecipientUpdateDto jobAlertRecipientDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (jobAlertRecipientDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {jobAlertRecipientDto.Id}"); 
 
            csJobAlertRecipient jobAlertRecipient; 
 
            try 
            { 
                jobAlertRecipient = jobAlertRecipientDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = jobAlertRecipient.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(jobAlertRecipient.ToDto()); 
        } 
 
        // DELETE api/jobAlertRecipients/{id} 
        [Route("jobAlertRecipients/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csJobAlertRecipient jobAlertRecipient = new csJobAlertRecipient(); 
            clsFault fault = jobAlertRecipient.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = jobAlertRecipient.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/jobAlertRecipients/batch 
        [Route("jobAlertRecipients/batch")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteBatch([FromBody] long[] ids) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            var errors = new System.Collections.Generic.List<string>(); 
            var deleted = 0; 
            foreach (var id in ids) 
            { 
                csJobAlertRecipient jobAlertRecipient = new csJobAlertRecipient(); 
                clsFault fault = jobAlertRecipient.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = jobAlertRecipient.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
