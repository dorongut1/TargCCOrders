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
    public partial class SupplierOrdersController : ControllerBase 
    { 
        // GET api/supplierOrders?page=0&pageSize=25&search=xyz 
        [Route("supplierOrders")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? orderHeaderId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null; 
            clsSupplierOrderCol supplierOrders = new clsSupplierOrderCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = supplierOrders.Select(p => p.ToDto()); 
 
            if (orderHeaderId.HasValue) allItems = allItems.Where(item => item.FkOrderHeaderId == orderHeaderId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkOrderHeaderId.ToString().Contains(searchLower) ||
                    (item.SupplierEmail ?? "").ToLower().Contains(searchLower) ||
                    (item.EmailSubject ?? "").ToLower().Contains(searchLower) ||
                    (item.EmailBody ?? "").ToLower().Contains(searchLower) ||
                    item.EnmEmailStatus.ToString().ToLower().Contains(searchLower) ||
                    item.EnmDeliveryMethod.ToString().ToLower().Contains(searchLower) ||
                    (item.RequestedDeliveryDay ?? "").ToLower().Contains(searchLower) ||
                    (item.Notes ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(SupplierOrderDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/supplierOrders/{id} 
        [Route("supplierOrders/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SupplierOrderDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsSupplierOrder supplierOrder = new clsSupplierOrder(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = supplierOrder.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            var dto = supplierOrder.ToDto(); 
            dto.PopulateFKDisplayNames(requester); 
            return Ok(dto); 
        } 
 
        // POST api/supplierOrders 
        [Route("supplierOrders")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SupplierOrderDto> CreateSupplierOrder(SupplierOrderUpdateDto supplierOrderDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (supplierOrderDto.Id != 0) return BadRequest($"Received an ID of {supplierOrderDto.Id}. Expected 0 for a new record."); 
 
            clsSupplierOrder supplierOrder; 
 
            try 
            { 
                supplierOrder = supplierOrderDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = supplierOrder.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = supplierOrder.ToDto().Id }, supplierOrder.ToDto()); 
        } 
 
        // PUT api/supplierOrders 
        [Route("supplierOrders/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<SupplierOrderDto> UpdateSupplierOrder(long id, SupplierOrderUpdateDto supplierOrderDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (supplierOrderDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {supplierOrderDto.Id}"); 
 
            // Optimistic concurrency: check If-Match ETag header 
            if (Request.Headers.ContainsKey("If-Match")) 
            { 
                var clientEtag = Request.Headers["If-Match"].ToString().Trim('"'); 
                if (!string.IsNullOrEmpty(clientEtag)) 
                { 
                    try 
                    { 
                        var existing = new clsSupplierOrder(); 
                        existing.GetByID(id, requester, false); 
                        var serverEtag = Dtos.SupplierOrderExtensions.ComputeETag(existing); 
                        if (clientEtag != serverEtag) 
                        { 
                            return Conflict(new { 
                                message = "This record was modified by another user since you loaded it. Please refresh and try again.", 
                                currentData = existing.ToDto() 
                            }); 
                        } 
                    } 
                    catch (Exception ex) { return Conflict(new { message = "Could not verify record version. Please refresh and try again.", detail = ex.Message }); }
                } 
            } 
 
            clsSupplierOrder supplierOrder; 
 
            try 
            { 
                supplierOrder = supplierOrderDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = supplierOrder.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(supplierOrder.ToDto()); 
        } 
 
        // DELETE api/supplierOrders/{id} 
        [Route("supplierOrders/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsSupplierOrder supplierOrder = new clsSupplierOrder(); 
            clsFault fault = supplierOrder.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = supplierOrder.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/supplierOrders/batch 
        [Route("supplierOrders/batch")] 
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
                clsSupplierOrder supplierOrder = new clsSupplierOrder(); 
                clsFault fault = supplierOrder.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = supplierOrder.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
        // PATCH api/supplierOrders/batch — Update specific fields on multiple records 
        [Route("supplierOrders/batch")] 
        [HttpPatch] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult PatchBatch([FromBody] BatchPatchRequest request) 
        { 
            if (request?.Ids == null || request.Ids.Length == 0) return BadRequest("No IDs provided."); 
            if (request.Fields == null || request.Fields.Count == 0) return BadRequest("No fields to update."); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var errors = new List<string>(); 
            var updated = 0; 
            foreach (var id in request.Ids) 
            { 
                try 
                { 
                    clsSupplierOrder supplierOrder = new clsSupplierOrder(); 
                    clsFault fault = supplierOrder.GetByID(id, requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    // Apply fields via the UpdateDto allow-list (see PatchHelper)
                    var fieldErrors = PatchHelper.ApplyFields(supplierOrder, typeof(SupplierOrderUpdateDto), request.Fields);
                    if (fieldErrors.Count > 0) { errors.Add($"ID {id}: " + string.Join("; ", fieldErrors)); continue; }

                    fault = supplierOrder.Update(requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    updated++; 
                } 
                catch (Exception ex) { errors.Add($"ID {id}: {ex.Message}"); } 
            } 
            return Ok(new { updated, errors }); 
        } 
 
    } 
} 
