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
    public partial class SystemDefaultsController : ControllerBase 
    { 
        // GET api/systemDefaults?page=0&pageSize=25&search=xyz 
        [Route("systemDefaults")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csSystemDefaultCol systemDefaults = new csSystemDefaultCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = systemDefaults.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.Group ?? "").ToLower().Contains(searchLower) ||
                    (item.SettingName ?? "").ToLower().Contains(searchLower) ||
                    (item.SettingValue ?? "").ToLower().Contains(searchLower) ||
                    item.EnmSystemDefaultType.ToString().ToLower().Contains(searchLower) ||
                    (item.Description ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(SystemDefaultDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/systemDefaults/{id} 
        [Route("systemDefaults/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SystemDefaultDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csSystemDefault systemDefault = new csSystemDefault(); 
            clsFault fault = systemDefault.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(systemDefault.ToDto()); 
        } 
 
        // POST api/systemDefaults 
        [Route("systemDefaults")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SystemDefaultDto> CreateSystemDefault(SystemDefaultUpdateDto systemDefaultDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (systemDefaultDto.Id != 0) return BadRequest($"Received an ID of {systemDefaultDto.Id}. Expected 0 for a new record."); 
 
            csSystemDefault systemDefault; 
 
            try 
            { 
                systemDefault = systemDefaultDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = systemDefault.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = systemDefault.ToDto().Id }, systemDefault.ToDto()); 
        } 
 
        // PUT api/systemDefaults 
        [Route("systemDefaults/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SystemDefaultDto> UpdateSystemDefault(long id, SystemDefaultUpdateDto systemDefaultDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (systemDefaultDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {systemDefaultDto.Id}"); 
 
            csSystemDefault systemDefault; 
 
            try 
            { 
                systemDefault = systemDefaultDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = systemDefault.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(systemDefault.ToDto()); 
        } 
 
        // DELETE api/systemDefaults/{id} 
        [Route("systemDefaults/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csSystemDefault systemDefault = new csSystemDefault(); 
            clsFault fault = systemDefault.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = systemDefault.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/systemDefaults/batch 
        [Route("systemDefaults/batch")] 
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
                csSystemDefault systemDefault = new csSystemDefault(); 
                clsFault fault = systemDefault.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = systemDefault.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
