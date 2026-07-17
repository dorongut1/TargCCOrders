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
    public partial class OrderHeadersController : ControllerBase 
    { 
        // GET api/orderHeaders?page=0&pageSize=25&search=xyz 
        [Route("orderHeaders")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? customerId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null; 
            clsOrderHeaderCol orderHeaders = new clsOrderHeaderCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = orderHeaders.Select(p => p.ToDto()); 
 
            if (customerId.HasValue) allItems = allItems.Where(item => item.FkCustomerId == customerId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.OrderNumber.ToString().Contains(searchLower) ||
                    item.FkCustomerId.ToString().Contains(searchLower) ||
                    item.EnmPaymentMethod.ToString().ToLower().Contains(searchLower) ||
                    item.EnmPaymentStatus.ToString().ToLower().Contains(searchLower) ||
                    (item.InvoiceNumber ?? "").ToLower().Contains(searchLower) ||
                    item.EnmDeliveryMethod.ToString().ToLower().Contains(searchLower) ||
                    item.EnmDeliveryDay.ToString().ToLower().Contains(searchLower) ||
                    item.EnmOrderStatus.ToString().ToLower().Contains(searchLower) ||
                    (item.Notes ?? "").ToLower().Contains(searchLower) ||
                    (item.Notes2 ?? "").ToLower().Contains(searchLower) ||
                    (item.OrderMonth ?? "").ToLower().Contains(searchLower) ||
                    (item.Quarter ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(OrderHeaderDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/orderHeaders/{id} 
        [Route("orderHeaders/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<OrderHeaderDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsOrderHeader orderHeader = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = orderHeader.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            var dto = orderHeader.ToDto(); 
            dto.PopulateFKDisplayNames(requester); 
            return Ok(dto); 
        } 
 
        // POST api/orderHeaders 
        [Route("orderHeaders")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<OrderHeaderDto> CreateOrderHeader(OrderHeaderUpdateDto orderHeaderDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (orderHeaderDto.Id != 0) return BadRequest($"Received an ID of {orderHeaderDto.Id}. Expected 0 for a new record."); 
 
            clsOrderHeader orderHeader; 
 
            try 
            { 
                orderHeader = orderHeaderDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = orderHeader.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = orderHeader.ToDto().Id }, orderHeader.ToDto()); 
        } 
 
        // PUT api/orderHeaders 
        [Route("orderHeaders/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<OrderHeaderDto> UpdateOrderHeader(long id, OrderHeaderUpdateDto orderHeaderDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (orderHeaderDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {orderHeaderDto.Id}"); 
 
            // Optimistic concurrency: check If-Match ETag header 
            if (Request.Headers.ContainsKey("If-Match")) 
            { 
                var clientEtag = Request.Headers["If-Match"].ToString().Trim('"'); 
                if (!string.IsNullOrEmpty(clientEtag)) 
                { 
                    try 
                    { 
                        var existing = new clsOrderHeader(); 
                        existing.GetByID(id, requester, false); 
                        var serverEtag = Dtos.OrderHeaderExtensions.ComputeETag(existing); 
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
 
            clsOrderHeader orderHeader; 
 
            try 
            { 
                orderHeader = orderHeaderDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = orderHeader.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(orderHeader.ToDto()); 
        } 
 
        // DELETE api/orderHeaders/{id} 
        [Route("orderHeaders/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsOrderHeader orderHeader = new clsOrderHeader(); 
            clsFault fault = orderHeader.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = orderHeader.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/orderHeaders/batch 
        [Route("orderHeaders/batch")] 
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
                clsOrderHeader orderHeader = new clsOrderHeader(); 
                clsFault fault = orderHeader.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = orderHeader.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
        // PATCH api/orderHeaders/batch — Update specific fields on multiple records 
        [Route("orderHeaders/batch")] 
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
                    clsOrderHeader orderHeader = new clsOrderHeader(); 
                    clsFault fault = orderHeader.GetByID(id, requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    // Apply fields via the UpdateDto allow-list (see PatchHelper)
                    var fieldErrors = PatchHelper.ApplyFields(orderHeader, typeof(OrderHeaderUpdateDto), request.Fields);
                    if (fieldErrors.Count > 0) { errors.Add($"ID {id}: " + string.Join("; ", fieldErrors)); continue; }

                    fault = orderHeader.Update(requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    updated++; 
                } 
                catch (Exception ex) { errors.Add($"ID {id}: {ex.Message}"); } 
            } 
            return Ok(new { updated, errors }); 
        } 
 
    } 
} 
