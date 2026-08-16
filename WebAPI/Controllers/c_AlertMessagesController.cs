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
    public partial class AlertMessagesController : ControllerBase 
    { 
        // GET api/alertMessages?page=0&pageSize=25&search=xyz 
        [Route("alertMessages")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csAlertMessageCol alertMessages = new csAlertMessageCol(vIsLocalized: true, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = alertMessages.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.Number.ToString().Contains(searchLower) ||
                    (item.Description ?? "").ToLower().Contains(searchLower) ||
                    item.EnmType.ToString().ToLower().Contains(searchLower) ||
                    item.EnmSeverity.ToString().ToLower().Contains(searchLower) ||
                    (item.Message ?? "").ToLower().Contains(searchLower) ||
                    (item.Action ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(AlertMessageDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/alertMessages/{id} 
        [Route("alertMessages/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<AlertMessageDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csAlertMessage alertMessage = new csAlertMessage(vIsLocalized: true); 
            clsFault fault = alertMessage.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(alertMessage.ToDto()); 
        } 
 
        // POST api/alertMessages 
        [Route("alertMessages")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<AlertMessageDto> CreateAlertMessage(AlertMessageUpdateDto alertMessageDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (alertMessageDto.Id != 0) return BadRequest($"Received an ID of {alertMessageDto.Id}. Expected 0 for a new record."); 
 
            csAlertMessage alertMessage; 
 
            try 
            { 
                alertMessage = alertMessageDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = alertMessage.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = alertMessage.ToDto().Id }, alertMessage.ToDto()); 
        } 
 
        // PUT api/alertMessages 
        [Route("alertMessages/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<AlertMessageDto> UpdateAlertMessage(long id, AlertMessageUpdateDto alertMessageDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (alertMessageDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {alertMessageDto.Id}"); 
 
            csAlertMessage alertMessage; 
 
            try 
            { 
                alertMessage = alertMessageDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = alertMessage.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(alertMessage.ToDto()); 
        } 
 
        // DELETE api/alertMessages/{id} 
        [Route("alertMessages/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csAlertMessage alertMessage = new csAlertMessage(); 
            clsFault fault = alertMessage.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = alertMessage.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/alertMessages/batch 
        [Route("alertMessages/batch")] 
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
                csAlertMessage alertMessage = new csAlertMessage(); 
                clsFault fault = alertMessage.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = alertMessage.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
