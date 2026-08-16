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
    public partial class MFAsController : ControllerBase 
    { 
        // GET api/mfas?page=0&pageSize=25&search=xyz 
        [Route("mfas")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc", [FromQuery] long? userId = null) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csMFACol mfas = new csMFACol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = mfas.Select(p => p.ToDto()); 
 
            if (userId.HasValue) allItems = allItems.Where(item => item.FkUserId == userId.Value); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    (item.CellOrEmail ?? "").ToLower().Contains(searchLower) ||
                    (item.ProtectedFunction ?? "").ToLower().Contains(searchLower) ||
                    (item.CodeHashed ?? "").ToLower().Contains(searchLower) ||
                    item.AttemptNo.ToString().Contains(searchLower) ||
                    (item.LastAccessingIp ?? "").ToLower().Contains(searchLower) ||
                    (item.LastAccessingCountry ?? "").ToLower().Contains(searchLower) ||
                    item.EnmUiLang.ToString().ToLower().Contains(searchLower) ||
                    (item.Details ?? "").ToLower().Contains(searchLower) ||
                    item.FkUserId.ToString().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(MFADto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/mfas/{id} 
        [Route("mfas/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<MFADto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csMFA mfa = new csMFA(clsEnums.enmLoadParent.DoNotLoad); 
            clsFault fault = mfa.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(mfa.ToDto()); 
        } 
 
        // POST api/mfas 
        [Route("mfas")] 
        [HttpPost] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<MFADto> CreateMFA(MFAUpdateDto mfaDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (mfaDto.Id != 0) return BadRequest($"Received an ID of {mfaDto.Id}. Expected 0 for a new record."); 
 
            csMFA mfa; 
 
            try 
            { 
                mfa = mfaDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = mfa.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return CreatedAtAction(nameof(GetByID), new { id = mfa.ToDto().Id }, mfa.ToDto()); 
        } 
 
        // PUT api/mfas 
        [Route("mfas/{id}")] 
        [HttpPut] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<MFADto> UpdateMFA(long id, MFAUpdateDto mfaDto) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            if (mfaDto.Id != id) return BadRequest($"ID received {id}, but ID in object is {mfaDto.Id}"); 
 
            csMFA mfa; 
 
            try 
            { 
                mfa = mfaDto.FromDto(requester); 
            } 
            catch (Exception e) 
            { 
                System.Diagnostics.Debug.WriteLine($"API Error: {e}"); 
                return BadRequest("Invalid data submitted."); 
            } 
 
            clsFault fault = mfa.Update(requester); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            return Ok(mfa.ToDto()); 
        } 
 
    } 
} 
