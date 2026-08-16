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
    public partial class ObjectTranslationsController : ControllerBase 
    { 
        // GET api/objectTranslations?page=0&pageSize=25&search=xyz 
        [Route("objectTranslations")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? objectToTranslateId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csObjectTranslationCol objectTranslations = new csObjectTranslationCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = objectTranslations.Select(p => p.ToDto()); 
 
            if (objectToTranslateId.HasValue) allItems = allItems.Where(item => item.FkObjectToTranslateId == objectToTranslateId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkObjectToTranslateId.ToString().Contains(searchLower) ||
                    item.Instance.ToString().Contains(searchLower) ||
                    (item.DefaultText ?? "").ToLower().Contains(searchLower) ||
                    item.EnmLanguage.ToString().ToLower().Contains(searchLower) ||
                    (item.Text ?? "").ToLower().Contains(searchLower) ||
                    (item.InstanceUniqueText ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(ObjectTranslationDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/objectTranslations/{id} 
        [Route("objectTranslations/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ObjectTranslationDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csObjectTranslation objectTranslation = new csObjectTranslation(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = objectTranslation.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(objectTranslation.ToDto()); 
        } 
 
        // POST api/objectTranslations 
        [Route("objectTranslations")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ObjectTranslationDto> CreateObjectTranslation(ObjectTranslationUpdateDto objectTranslationDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (objectTranslationDto.Id != 0) return BadRequest($"Received an ID of {objectTranslationDto.Id}. Expected 0 for a new record."); 
 
            csObjectTranslation objectTranslation; 
 
            try 
            { 
                objectTranslation = objectTranslationDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = objectTranslation.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = objectTranslation.ToDto().Id }, objectTranslation.ToDto()); 
        } 
 
        // PUT api/objectTranslations 
        [Route("objectTranslations/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ObjectTranslationDto> UpdateObjectTranslation(long id, ObjectTranslationUpdateDto objectTranslationDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (objectTranslationDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {objectTranslationDto.Id}"); 
 
            csObjectTranslation objectTranslation; 
 
            try 
            { 
                objectTranslation = objectTranslationDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = objectTranslation.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(objectTranslation.ToDto()); 
        } 
 
        // DELETE api/objectTranslations/{id} 
        [Route("objectTranslations/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csObjectTranslation objectTranslation = new csObjectTranslation(); 
            clsFault fault = objectTranslation.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = objectTranslation.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/objectTranslations/batch 
        [Route("objectTranslations/batch")] 
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
                csObjectTranslation objectTranslation = new csObjectTranslation(); 
                clsFault fault = objectTranslation.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = objectTranslation.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
