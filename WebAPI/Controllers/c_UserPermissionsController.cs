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
    public partial class UserPermissionsController : ControllerBase 
    { 
        // GET api/userPermissions?page=0&pageSize=25&search=xyz 
        [Route("userPermissions")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? userId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csUserPermissionCol userPermissions = new csUserPermissionCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = userPermissions.Select(p => p.ToDto()); 
 
            if (userId.HasValue) allItems = allItems.Where(item => item.FkUserId == userId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkUserId.ToString().Contains(searchLower) ||
                    (item.ApplicationName ?? "").ToLower().Contains(searchLower) ||
                    (item.ComputerIdentifier ?? "").ToLower().Contains(searchLower) ||
                    (item.ComputerName ?? "").ToLower().Contains(searchLower) ||
                    (item.ExternalIp ?? "").ToLower().Contains(searchLower) ||
                    (item.Comments ?? "").ToLower().Contains(searchLower) ||
                    item.LoggedLoginId.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(UserPermissionDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/userPermissions/{id} 
        [Route("userPermissions/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserPermissionDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csUserPermission userPermission = new csUserPermission(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = userPermission.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(userPermission.ToDto()); 
        } 
 
        // POST api/userPermissions 
        [Route("userPermissions")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserPermissionDto> CreateUserPermission(UserPermissionUpdateDto userPermissionDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (userPermissionDto.Id != 0) return BadRequest($"Received an ID of {userPermissionDto.Id}. Expected 0 for a new record."); 
 
            csUserPermission userPermission; 
 
            try 
            { 
                userPermission = userPermissionDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = userPermission.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = userPermission.ToDto().Id }, userPermission.ToDto()); 
        } 
 
        // PUT api/userPermissions 
        [Route("userPermissions/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserPermissionDto> UpdateUserPermission(long id, UserPermissionUpdateDto userPermissionDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (userPermissionDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {userPermissionDto.Id}"); 
 
            csUserPermission userPermission; 
 
            try 
            { 
                userPermission = userPermissionDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = userPermission.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(userPermission.ToDto()); 
        } 
 
        // DELETE api/userPermissions/{id} 
        [Route("userPermissions/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csUserPermission userPermission = new csUserPermission(); 
            clsFault fault = userPermission.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = userPermission.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/userPermissions/batch 
        [Route("userPermissions/batch")] 
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
                csUserPermission userPermission = new csUserPermission(); 
                clsFault fault = userPermission.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = userPermission.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
