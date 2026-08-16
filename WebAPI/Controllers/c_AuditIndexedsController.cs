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
    public partial class AuditIndexedsController : ControllerBase 
    { 
        // GET api/auditIndexeds?page=0&pageSize=25&search=xyz 
        [Route("auditIndexeds")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult Fill([FromQuery] int page = 0, [FromQuery] int pageSize = 25, [FromQuery] string search = "", [FromQuery] string sortField = "", [FromQuery] string sortDir = "asc") 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            clsFault? fault = null; 
            csAuditIndexedCol auditIndexeds = new csAuditIndexedCol(requester, ref fault); if (!fault.isOK) return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);
 
            var allItems = auditIndexeds.Select(p => p.ToDto()); 
 
            // Apply search filter if provided 
            if (!string.IsNullOrWhiteSpace(search)) 
            { 
                var searchLower = search.ToLower(); 
                allItems = allItems.Where(item => 
                    item.Id.ToString().Contains(searchLower) ||
                    item.OriginalId.ToString().Contains(searchLower) ||
                    (item.TableName ?? "").ToLower().Contains(searchLower) ||
                    item.RowId.ToString().Contains(searchLower) ||
                    (item.Operation ?? "").ToLower().Contains(searchLower) ||
                    (item.SqlCurrentUser ?? "").ToLower().Contains(searchLower) ||
                    (item.FieldName ?? "").ToLower().Contains(searchLower) ||
                    (item.OldValue ?? "").ToLower().Contains(searchLower) ||
                    (item.NewValue ?? "").ToLower().Contains(searchLower) ||
                    (item.ChangedByUser ?? "").ToLower().Contains(searchLower) ||
                    item.ActiveLoginId.ToString().Contains(searchLower) ||
                    (item.SqlSystemUser ?? "").ToLower().Contains(searchLower) ||
                    (item.SqlAppName ?? "").ToLower().Contains(searchLower) ||
                    (item.SqlHostName ?? "").ToLower().Contains(searchLower)
                ); 
            } 
 
            var itemsList = allItems.ToList(); 
            var total = itemsList.Count; 
 
            // Server-side sorting 
            if (!string.IsNullOrEmpty(sortField)) 
            { 
                var prop = typeof(AuditIndexedDto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance); 
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
 
        // GET api/auditIndexeds/{id} 
        [Route("auditIndexeds/{id}")] 
        [HttpGet] 
        [Authorize(Policy = "AdminUI")]
        public ActionResult<AuditIndexedDto> GetByID(long id) 
        { 
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }
 
            csAuditIndexed auditIndexed = new csAuditIndexed(); 
            clsFault fault = auditIndexed.GetByID(id, requester); if (!fault.isOK) return NotFound(fault.Message); 
 
            return Ok(auditIndexed.ToDto()); 
        } 
 
 
 
    } 
} 
