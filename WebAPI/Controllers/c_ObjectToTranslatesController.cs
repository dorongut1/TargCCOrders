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
    public partial class ObjectToTranslatesController : ControllerBase 
    { 
        // GET api/objectToTranslates?page=0&pageSize=25&search=xyz 
        [Route("objectToTranslates")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            clsFault? fault = null; 
            csObjectToTranslateCol objectToTranslates = new csObjectToTranslateCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = objectToTranslates.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.EnmObjectType.ToString().ToLower().Contains(searchLower) ||
                    (item.Object ?? "").ToLower().Contains(searchLower) ||
                    (item.Item ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(ObjectToTranslateDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/objectToTranslates/{id} 
        [Route("objectToTranslates/{id}")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<ObjectToTranslateDto> GetByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csObjectToTranslate objectToTranslate = new csObjectToTranslate(); 
            clsFault fault = objectToTranslate.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(objectToTranslate.ToDto()); 
        } 
 
        // POST api/objectToTranslates 
        [Route("objectToTranslates")] 
        [HttpPost] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<ObjectToTranslateDto> CreateObjectToTranslate(ObjectToTranslateUpdateDto objectToTranslateDto) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            if (objectToTranslateDto.Id != 0) return BadRequest($"Received an ID of {objectToTranslateDto.Id}. Expected 0 for a new record."); 
 
            csObjectToTranslate objectToTranslate; 
 
            try 
            { 
                objectToTranslate = objectToTranslateDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = objectToTranslate.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = objectToTranslate.ToDto().Id }, objectToTranslate.ToDto()); 
        } 
 
        // PUT api/objectToTranslates 
        [Route("objectToTranslates/{id}")] 
        [HttpPut] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<ObjectToTranslateDto> UpdateObjectToTranslate(long id, ObjectToTranslateUpdateDto objectToTranslateDto) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            if (objectToTranslateDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {objectToTranslateDto.Id}"); 
 
            csObjectToTranslate objectToTranslate; 
 
            try 
            { 
                objectToTranslate = objectToTranslateDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = objectToTranslate.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(objectToTranslate.ToDto()); 
        } 
 
        // DELETE api/objectToTranslates/{id} 
        [Route("objectToTranslates/{id}")] 
        [HttpDelete] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csObjectToTranslate objectToTranslate = new csObjectToTranslate(); 
            clsFault fault = objectToTranslate.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = objectToTranslate.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/objectToTranslates/batch 
        [Route("objectToTranslates/batch")] 
        [HttpDelete] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult DeleteBatch([FromBody] long[] ids) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            var errors = new System.Collections.Generic.List<string>(); 
            var deleted = 0; 
            foreach (var id in ids) 
            { 
                csObjectToTranslate objectToTranslate = new csObjectToTranslate(); 
                clsFault fault = objectToTranslate.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = objectToTranslate.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
