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
    public partial class ProductsController : ControllerBase 
    { 
        // GET api/products?page=0&pageSize=25&search=xyz 
        [Route("products")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null; 
            clsProductCol products = new clsProductCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = products.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.ProductCode ?? "").ToLower().Contains(searchLower) ||
                    (item.ProductName ?? "").ToLower().Contains(searchLower) ||
                    item.EnmCategory.ToString().ToLower().Contains(searchLower) ||
                    (item.UnitOfMeasure ?? "").ToLower().Contains(searchLower) ||
                    (item.Notes ?? "").ToLower().Contains(searchLower) ||
                    item.CurrentStock.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(ProductDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/products/{id} 
        [Route("products/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ProductDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsProduct product = new clsProduct(); 
            clsFault fault = product.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(product.ToDto()); 
        } 
 
        // POST api/products 
        [Route("products")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ProductDto> CreateProduct(ProductUpdateDto productDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (productDto.Id != 0) return BadRequest($"Received an ID of {productDto.Id}. Expected 0 for a new record."); 
 
            clsProduct product; 
 
            try 
            { 
                product = productDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = product.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = product.ToDto().Id }, product.ToDto()); 
        } 
 
        // PUT api/products 
        [Route("products/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<ProductDto> UpdateProduct(long id, ProductUpdateDto productDto) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState); 
 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (productDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {productDto.Id}"); 
 
            // Optimistic concurrency: check If-Match ETag header 
            if (Request.Headers.ContainsKey("If-Match")) 
            { 
                var clientEtag = Request.Headers["If-Match"].ToString().Trim('"'); 
                if (!string.IsNullOrEmpty(clientEtag)) 
                { 
                    try 
                    { 
                        var existing = new clsProduct(); 
                        existing.GetByID(id, requester, false); 
                        var serverEtag = Dtos.ProductExtensions.ComputeETag(existing); 
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
 
            clsProduct product; 
 
            try 
            { 
                product = productDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = product.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(product.ToDto()); 
        } 
 
        // DELETE api/products/{id} 
        [Route("products/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsProduct product = new clsProduct(); 
            clsFault fault = product.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = product.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/products/batch 
        [Route("products/batch")] 
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
                clsProduct product = new clsProduct(); 
                clsFault fault = product.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = product.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
        // PATCH api/products/batch — Update specific fields on multiple records 
        [Route("products/batch")] 
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
                    clsProduct product = new clsProduct(); 
                    clsFault fault = product.GetByID(id, requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    // Apply fields via the UpdateDto allow-list (see PatchHelper)
                    var fieldErrors = PatchHelper.ApplyFields(product, typeof(ProductUpdateDto), request.Fields);
                    if (fieldErrors.Count > 0) { errors.Add($"ID {id}: " + string.Join("; ", fieldErrors)); continue; }

                    fault = product.Update(requester); 
                    if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                    updated++; 
                } 
                catch (Exception ex) { errors.Add($"ID {id}: {ex.Message}"); } 
            } 
            return Ok(new { updated, errors }); 
        } 
 
    } 
} 
