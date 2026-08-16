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
    public partial class UsersController : ControllerBase 
    { 
        // GET api/users?page=0&pageSize=25&search=xyz 
        [Route("users")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? roleId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csUserCol users = new csUserCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = users.Select(p => p.ToDto()); 
 
            if (roleId.HasValue) allItems = allItems.Where(item => item.FkRoleId == roleId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.UserName ?? "").ToLower().Contains(searchLower) ||
                    (item.LastName ?? "").ToLower().Contains(searchLower) ||
                    (item.FirstName ?? "").ToLower().Contains(searchLower) ||
                    (item.FullName ?? "").ToLower().Contains(searchLower) ||
                    (item.NationalIdNo ?? "").ToLower().Contains(searchLower) ||
                    (item.Address ?? "").ToLower().Contains(searchLower) ||
                    (item.City ?? "").ToLower().Contains(searchLower) ||
                    (item.ProvinceState ?? "").ToLower().Contains(searchLower) ||
                    (item.PostalCode ?? "").ToLower().Contains(searchLower) ||
                    (item.Country ?? "").ToLower().Contains(searchLower) ||
                    (item.PhoneNumber ?? "").ToLower().Contains(searchLower) ||
                    (item.Email ?? "").ToLower().Contains(searchLower) ||
                    (item.PasswordHashed ?? "").ToLower().Contains(searchLower) ||
                    item.EnmType.ToString().ToLower().Contains(searchLower) ||
                    item.IDinType.ToString().Contains(searchLower) ||
                    (item.Comments ?? "").ToLower().Contains(searchLower) ||
                    (item.LastPasswords ?? "").ToLower().Contains(searchLower) ||
                    (item.Applications ?? "").ToLower().Contains(searchLower) ||
                    item.EnmLanguage.ToString().ToLower().Contains(searchLower) ||
                    item.FkRoleId.ToString().Contains(searchLower) ||
                    item.EnmAuthenticationMethod.ToString().ToLower().Contains(searchLower) ||
                    item.EnmMessagingMode.ToString().ToLower().Contains(searchLower) ||
                    (item.LoggedInIp ?? "").ToLower().Contains(searchLower) ||
                    (item.ApprovalCodeHashed ?? "").ToLower().Contains(searchLower) ||
                    (item.ApprovalFunctionName ?? "").ToLower().Contains(searchLower) ||
                    (item.LkpSecurityQuestion1Code ?? "").ToLower().Contains(searchLower) ||
                    (item.LkpSecurityQuestion2Code ?? "").ToLower().Contains(searchLower) ||
                    (item.LkpSecurityQuestion3Code ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(UserDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/users/{id} 
        [Route("users/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csUser user = new csUser(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = user.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(user.ToDto()); 
        } 
 
        // POST api/users 
        [Route("users")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserDto> CreateUser(UserUpdateDto userDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (userDto.Id != 0) return BadRequest($"Received an ID of {userDto.Id}. Expected 0 for a new record."); 
 
            csUser user; 
 
            try 
            { 
                user = userDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = user.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = user.ToDto().Id }, user.ToDto()); 
        } 
 
        // PUT api/users 
        [Route("users/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<UserDto> UpdateUser(long id, UserUpdateDto userDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (userDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {userDto.Id}"); 
 
            csUser user; 
 
            try 
            { 
                user = userDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = user.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(user.ToDto()); 
        } 
 
        // DELETE api/users/{id} 
        [Route("users/{id}")] 
        [HttpDelete] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csUser user = new csUser(); 
            clsFault fault = user.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = user.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/users/batch 
        [Route("users/batch")] 
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
                csUser user = new csUser(); 
                clsFault fault = user.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = user.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
