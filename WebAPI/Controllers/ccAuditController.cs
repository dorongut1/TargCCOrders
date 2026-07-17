using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Authorization; 
using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
 
namespace TargCCOrders.WebAPI.Controllers 
{ 
    [Route("api/audit")] 
    [ApiController] 
    [Authorize(Policy = "AdminUI")] 
    public class AuditController : ControllerBase 
    { 
        // GET api/audit/{tableName}/{rowId} — Get change history for a specific record 
        [HttpGet("{tableName}/{rowId}")] 
        public ActionResult GetAuditHistory(string tableName, long rowId, [FromQuery] int maxRecords = 200)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            try
            {
                var auditCol = new csAuditIndexedCol(); 
                var fault = auditCol.FillByTableNameAndRowID(tableName, rowId, requester, maxRecords, clsEnums.enmFillDirection.DESC); 
                if (!fault.isOK) return BadRequest(fault.Message); 
 
                var items = new List<object>(); 
                foreach (csAuditIndexed audit in auditCol) 
                { 
                    items.Add(new { 
                        id = audit.ID, 
                        fieldName = audit.FieldName, 
                        oldValue = audit.OldValue, 
                        newValue = audit.NewValue, 
                        changedByUser = audit.ChangedByUser, 
                        occurredAt = audit.OccurredAt, 
                        operation = audit.Operation 
                    }); 
                } 
                return Ok(new { items, total = items.Count }); 
            } 
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Audit history failed for {TableName}/{RowId}", tableName, rowId);
                return StatusCode(500, new { message = "Failed to load audit history." });
            }
        } 
    } 
} 
