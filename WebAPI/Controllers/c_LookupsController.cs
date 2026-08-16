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
    public partial class LookupsController : ControllerBase 
    { 
        // GET api/lookups?page=0&pageSize=25&search=xyz 
        [Route("lookups")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csLookupCol lookups = new csLookupCol(vIsLocalized: true, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = lookups.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.EnmParentLookupType.ToString().ToLower().Contains(searchLower) ||
                    (item.ParentCode ?? "").ToLower().Contains(searchLower) ||
                    item.EnmLookupType.ToString().ToLower().Contains(searchLower) ||
                    (item.Code ?? "").ToLower().Contains(searchLower) ||
                    (item.Text ?? "").ToLower().Contains(searchLower) ||
                    (item.Description ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(LookupDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/lookups/{id} 
        [Route("lookups/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<LookupDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csLookup lookup = new csLookup(vIsLocalized: true); 
            clsFault fault = lookup.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(lookup.ToDto()); 
        } 
 
        // POST api/lookups 
        [Route("lookups")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<LookupDto> CreateLookup(LookupUpdateDto lookupDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (lookupDto.Id != 0) return BadRequest($"Received an ID of {lookupDto.Id}. Expected 0 for a new record."); 
 
            csLookup lookup; 
 
            try 
            { 
                lookup = lookupDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = lookup.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = lookup.ToDto().Id }, lookup.ToDto()); 
        } 
 
        // PUT api/lookups 
        [Route("lookups/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<LookupDto> UpdateLookup(long id, LookupUpdateDto lookupDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (lookupDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {lookupDto.Id}"); 
 
            csLookup lookup; 
 
            try 
            { 
                lookup = lookupDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = lookup.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(lookup.ToDto()); 
        } 
 
        // DELETE api/lookups/{id} 
        [Route("lookups/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csLookup lookup = new csLookup(); 
            clsFault fault = lookup.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = lookup.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/lookups/batch 
        [Route("lookups/batch")] 
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
                csLookup lookup = new csLookup(); 
                clsFault fault = lookup.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = lookup.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
