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
    public partial class CustomersController : ControllerBase 
    { 
        // GET api/customers?page=0&pageSize=25&search=xyz 
        [Route("customers")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null; 
            clsCustomerCol customers = new clsCustomerCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = customers.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.CustomerCode ?? "").ToLower().Contains(searchLower) ||
                    (item.CustomerName ?? "").ToLower().Contains(searchLower) ||
                    (item.Phone ?? "").ToLower().Contains(searchLower) ||
                    (item.Email ?? "").ToLower().Contains(searchLower) ||
                    (item.Address ?? "").ToLower().Contains(searchLower) ||
                    (item.City ?? "").ToLower().Contains(searchLower) ||
                    (item.TaxId ?? "").ToLower().Contains(searchLower) ||
                    item.EnmCustomerType.ToString().ToLower().Contains(searchLower) ||
                    item.PaymentTermsDays.ToString().Contains(searchLower) ||
                    (item.Notes ?? "").ToLower().Contains(searchLower) ||
                    (item.Location ?? "").ToLower().Contains(searchLower) ||
                    (item.AccountantEmail ?? "").ToLower().Contains(searchLower) ||
                    item.EnmAccountantMethod.ToString().ToLower().Contains(searchLower) ||
                    (item.InvoiceName ?? "").ToLower().Contains(searchLower) ||
                    (item.ProfitabilityCode ?? "").ToLower().Contains(searchLower) ||
                    (item.CustomerIdentifier ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(CustomerDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/customers/{id} 
        [Route("customers/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsCustomer customer = new clsCustomer(); 
            clsFault fault = customer.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(customer.ToDto()); 
        } 
 
        // POST api/customers 
        [Route("customers")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDto> CreateCustomer(CustomerUpdateDto customerDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (customerDto.Id != 0) return BadRequest($"Received an ID of {customerDto.Id}. Expected 0 for a new record."); 
 
            clsCustomer customer; 
 
            try 
            { 
                customer = customerDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = customer.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = customer.ToDto().Id }, customer.ToDto()); 
        } 
 
        // PUT api/customers 
        [Route("customers/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<CustomerDto> UpdateCustomer(long id, CustomerUpdateDto customerDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (customerDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {customerDto.Id}"); 
 
            // Optimistic concurrency: check If-Match ETag header 
            if (Request.Headers.ContainsKey("If-Match")) 
            { 
                var clientEtag = Request.Headers["If-Match"].ToString().Trim('"'); 
                if (!string.IsNullOrEmpty(clientEtag)) 
                { 
                    try 
                    { 
                        var existing = new clsCustomer(); 
                        existing.GetByID(id, requester, false); 
                        var serverEtag = Dtos.CustomerExtensions.ComputeETag(existing); 
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
 
            clsCustomer customer; 
 
            try 
            { 
                customer = customerDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = customer.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(customer.ToDto()); 
        } 
 
        // DELETE api/customers/{id} 
        [Route("customers/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsCustomer customer = new clsCustomer(); 
            clsFault fault = customer.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = customer.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/customers/batch 
        [Route("customers/batch")] 
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
                clsCustomer customer = new clsCustomer(); 
                clsFault fault = customer.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = customer.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
        // PATCH api/customers/batch — Update specific fields on multiple records 
        [Route("customers/batch")] 
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
                    clsCustomer customer = new clsCustomer(); 
                    clsFault fault = customer.GetByID(id, requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    // Apply fields via the UpdateDto allow-list (see PatchHelper)
                    var fieldErrors = PatchHelper.ApplyFields(customer, typeof(CustomerUpdateDto), request.Fields);
                    if (fieldErrors.Count > 0) { errors.Add($"ID {id}: " + string.Join("; ", fieldErrors)); continue; }

                    fault = customer.Update(requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    updated++; 
                } 
                catch (Exception ex) { errors.Add($"ID {id}: {ex.Message}"); } 
            } 
            return Ok(new { updated, errors }); 
        } 
 
    } 
} 
