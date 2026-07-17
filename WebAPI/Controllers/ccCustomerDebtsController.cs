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
    public partial class CustomerDebtsController : ControllerBase 
    { 
        // GET api/customerDebts?page=0&pageSize=25&search=xyz 
        [Route("customerDebts")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? customerId = null, [FromQuery] long? orderHeaderId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null; 
            clsCustomerDebtCol customerDebts = new clsCustomerDebtCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = customerDebts.Select(p => p.ToDto()); 
 
            if (customerId.HasValue) allItems = allItems.Where(item => item.FkCustomerId == customerId.Value); 
            if (orderHeaderId.HasValue) allItems = allItems.Where(item => item.FkOrderHeaderId == orderHeaderId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkCustomerId.ToString().Contains(searchLower) ||
                    item.FkOrderHeaderId.ToString().Contains(searchLower) ||
                    item.EnmDebtStatus.ToString().ToLower().Contains(searchLower) ||
                    (item.Notes ?? "").ToLower().Contains(searchLower) ||
                    (item.ProductTypes ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(CustomerDebtDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/customerDebts/{id} 
        [Route("customerDebts/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDebtDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsCustomerDebt customerDebt = new clsCustomerDebt(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = customerDebt.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            var dto = customerDebt.ToDto(); 
            dto.PopulateFKDisplayNames(requester); 
            return Ok(dto); 
        } 
 
        // POST api/customerDebts 
        [Route("customerDebts")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDebtDto> CreateCustomerDebt(CustomerDebtUpdateDto customerDebtDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (customerDebtDto.Id != 0) return BadRequest($"Received an ID of {customerDebtDto.Id}. Expected 0 for a new record."); 
 
            clsCustomerDebt customerDebt; 
 
            try 
            { 
                customerDebt = customerDebtDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = customerDebt.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = customerDebt.ToDto().Id }, customerDebt.ToDto()); 
        } 
 
        // PUT api/customerDebts 
        [Route("customerDebts/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDebtDto> UpdateCustomerDebt(long id, CustomerDebtUpdateDto customerDebtDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (customerDebtDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {customerDebtDto.Id}"); 
 
            // Optimistic concurrency: check If-Match ETag header 
            if (Request.Headers.ContainsKey("If-Match")) 
            { 
                var clientEtag = Request.Headers["If-Match"].ToString().Trim('"'); 
                if (!string.IsNullOrEmpty(clientEtag)) 
                { 
                    try 
                    { 
                        var existing = new clsCustomerDebt(); 
                        existing.GetByID(id, requester, false); 
                        var serverEtag = Dtos.CustomerDebtExtensions.ComputeETag(existing); 
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
 
            clsCustomerDebt customerDebt; 
 
            try 
            { 
                customerDebt = customerDebtDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = customerDebt.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(customerDebt.ToDto()); 
        } 
 
        // DELETE api/customerDebts/{id} 
        [Route("customerDebts/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsCustomerDebt customerDebt = new clsCustomerDebt(); 
            clsFault fault = customerDebt.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = customerDebt.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/customerDebts/batch 
        [Route("customerDebts/batch")] 
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
                clsCustomerDebt customerDebt = new clsCustomerDebt(); 
                clsFault fault = customerDebt.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = customerDebt.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
        // PATCH api/customerDebts/batch — Update specific fields on multiple records 
        [Route("customerDebts/batch")] 
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
                    clsCustomerDebt customerDebt = new clsCustomerDebt(); 
                    clsFault fault = customerDebt.GetByID(id, requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    // Apply fields via the UpdateDto allow-list (see PatchHelper)
                    var fieldErrors = PatchHelper.ApplyFields(customerDebt, typeof(CustomerDebtUpdateDto), request.Fields);
                    if (fieldErrors.Count > 0) { errors.Add($"ID {id}: " + string.Join("; ", fieldErrors)); continue; }

                    fault = customerDebt.Update(requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    updated++; 
                } 
                catch (Exception ex) { errors.Add($"ID {id}: {ex.Message}"); } 
            } 
            return Ok(new { updated, errors }); 
        } 
 
    } 
} 
