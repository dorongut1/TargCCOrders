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
    public partial class MailsController : ControllerBase 
    { 
        // GET api/mails?page=0&pageSize=25&search=xyz 
        [Route("mails")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csMailCol mails = new csMailCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = mails.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.EnmMessagingMode.ToString().ToLower().Contains(searchLower) ||
                    (item.RecipientEmail ?? "").ToLower().Contains(searchLower) ||
                    (item.Subject ?? "").ToLower().Contains(searchLower) ||
                    (item.Body ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(MailDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/mails/{id} 
        [Route("mails/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<MailDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csMail mail = new csMail(); 
            clsFault fault = mail.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(mail.ToDto()); 
        } 
 
 
        // PUT api/mails 
        [Route("mails/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<MailDto> UpdateMail(long id, MailUpdateDto mailDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (mailDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {mailDto.Id}"); 
 
            csMail mail; 
 
            try 
            { 
                mail = mailDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = mail.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(mail.ToDto()); 
        } 
 
        // DELETE api/mails/{id} 
        [Route("mails/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csMail mail = new csMail(); 
            clsFault fault = mail.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = mail.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/mails/batch 
        [Route("mails/batch")] 
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
                csMail mail = new csMail(); 
                clsFault fault = mail.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = mail.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
