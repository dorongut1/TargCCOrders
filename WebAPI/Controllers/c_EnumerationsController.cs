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
    public partial class EnumerationsController : ControllerBase 
    { 
        // GET api/enumerations?page=0&pageSize=25&search=xyz 
        [Route("enumerations")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            clsFault? fault = null; 
            csEnumerationCol enumerations = new csEnumerationCol(vIsLocalized: true, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = enumerations.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.EnumType ?? "").ToLower().Contains(searchLower) ||
                    (item.EnumValue ?? "").ToLower().Contains(searchLower) ||
                    (item.Text ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(EnumerationDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/enumerations/{id} 
        [Route("enumerations/{id}")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<EnumerationDto> GetByID(int id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csEnumeration enumeration = new csEnumeration(vIsLocalized: true); 
            clsFault fault = enumeration.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(enumeration.ToDto()); 
        } 
 
        // POST api/enumerations 
        [Route("enumerations")] 
        [HttpPost] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<EnumerationDto> CreateEnumeration(EnumerationUpdateDto enumerationDto) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            if (enumerationDto.Id != 0) return BadRequest($"Received an ID of {enumerationDto.Id}. Expected 0 for a new record."); 
 
            csEnumeration enumeration; 
 
            try 
            { 
                enumeration = enumerationDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = enumeration.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = enumeration.ToDto().Id }, enumeration.ToDto()); 
        } 
 
        // PUT api/enumerations 
        [Route("enumerations/{id}")] 
        [HttpPut] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<EnumerationDto> UpdateEnumeration(int id, EnumerationUpdateDto enumerationDto) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            if (enumerationDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {enumerationDto.Id}"); 
 
            csEnumeration enumeration; 
 
            try 
            { 
                enumeration = enumerationDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = enumeration.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(enumeration.ToDto()); 
        } 
 
        // DELETE api/enumerations/{id} 
        [Route("enumerations/{id}")] 
        [HttpDelete] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult DeleteByID(int id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csEnumeration enumeration = new csEnumeration(); 
            clsFault fault = enumeration.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = enumeration.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/enumerations/batch 
        [Route("enumerations/batch")] 
        [HttpDelete] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult DeleteBatch([FromBody] int[] ids) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            var errors = new System.Collections.Generic.List<string>(); 
            var deleted = 0; 
            foreach (var id in ids) 
            { 
                csEnumeration enumeration = new csEnumeration(); 
                clsFault fault = enumeration.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = enumeration.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
