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
    public partial class UserLoginKeysController : ControllerBase 
    { 
        // GET api/userLoginKeys?page=0&pageSize=25&search=xyz 
        [Route("userLoginKeys")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? userId = null) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            clsFault? fault = null; 
            csUserLoginKeyCol userLoginKeys = new csUserLoginKeyCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = userLoginKeys.Select(p => p.ToDto()); 
 
            if (userId.HasValue) allItems = allItems.Where(item => item.FkUserId == userId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.FkUserId.ToString().Contains(searchLower) ||
                    (item.ApplicationName ?? "").ToLower().Contains(searchLower) ||
                    (item.ApplicationIdentifier ?? "").ToLower().Contains(searchLower) ||
                    (item.KeyHashed ?? "").ToLower().Contains(searchLower) ||
                    (item.ExternalIpAtCreation ?? "").ToLower().Contains(searchLower) ||
                    (item.CountryAtCreation ?? "").ToLower().Contains(searchLower) ||
                    item.LoggedLoginId.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(UserLoginKeyDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/userLoginKeys/{id} 
        [Route("userLoginKeys/{id}")] 
        [HttpGet] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<UserLoginKeyDto> GetByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csUserLoginKey userLoginKey = new csUserLoginKey(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = userLoginKey.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(userLoginKey.ToDto()); 
        } 
 
 
        // PUT api/userLoginKeys 
        [Route("userLoginKeys/{id}")] 
        [HttpPut] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult<UserLoginKeyDto> UpdateUserLoginKey(long id, UserLoginKeyUpdateDto userLoginKeyDto) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            if (userLoginKeyDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {userLoginKeyDto.Id}"); 
 
            csUserLoginKey userLoginKey; 
 
            try 
            { 
                userLoginKey = userLoginKeyDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = userLoginKey.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(userLoginKey.ToDto()); 
        } 
 
        // DELETE api/userLoginKeys/{id} 
        [Route("userLoginKeys/{id}")] 
        [HttpDelete] 
        //[Authorize] //TODO: enable when auth is configured
        public ActionResult DeleteByID(long id) 
        { 
            clsRequester requester = new clsRequester("*", "View", true); //TODO: replace with JWT/ticket authentication 
            requester.CallingFunctionWithinApplication = "WebAPI"; 
 
            csUserLoginKey userLoginKey = new csUserLoginKey(); 
            clsFault fault = userLoginKey.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
            fault = userLoginKey.Delete(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return NoContent(); 
        } 
 
        // DELETE api/userLoginKeys/batch 
        [Route("userLoginKeys/batch")] 
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
                csUserLoginKey userLoginKey = new csUserLoginKey(); 
                clsFault fault = userLoginKey.GetByID(id, requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                fault = userLoginKey.Delete(requester); 
                if (!fault.isOK) { errors.Add($"ID {id}: {fault.Message}"); continue; } 
                deleted++; 
            } 
 
            if (errors.Count > 0) return Ok(new { deleted, errors }); 
            return Ok(new { deleted, errors = new string[0] }); 
        } 
 
    } 
} 
