using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using TargCCOrders.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Composite order endpoints — save an order header together with its lines
    /// in one call, with automatic order numbering and line reconciliation.
    /// This is the endpoint the React OrderCompositeForm should use instead of
    /// orchestrating N separate header/line calls (which was non-atomic).
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrdersCompositeController : ControllerBase
    {
        public class CompositeOrderRequest
        {
            public OrderHeaderUpdateDto Header { get; set; } = new();
            public List<OrderLineUpdateDto> Lines { get; set; } = new();
            /// <summary>IDs of existing lines the user removed (PUT only).</summary>
            public List<long> DeletedLineIds { get; set; } = new();
        }

        // GET api/orders/nextNumber — next free order number
        [HttpGet("nextNumber")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult GetNextOrderNumber()
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var next = ComputeNextOrderNumber(requester, out var fault);
            if (next == null) return BadRequest(fault);
            return Ok(new { nextOrderNumber = next.Value });
        }

        // GET api/orders/composite/{id} — header + lines + display names in one call
        [HttpGet("composite/{id}")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult GetComposite(long id)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var header = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad);
            var fault = header.GetByID(id, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(fault.Message);

            fault = header.FillOrderLines(requester);
            if (!fault.isOK) return BadRequest(fault.Message);

            var headerDto = header.ToDto();
            headerDto.PopulateFKDisplayNames(requester);

            var lines = new List<object>();
            foreach (clsOrderLine line in header.OrderLines)
            {
                var dto = line.ToDto();
                dto.PopulateFKDisplayNames(requester);
                lines.Add(dto);
            }

            return Ok(new { header = headerDto, lines });
        }

        // POST api/orders/composite — create header + lines together
        [HttpPost("composite")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult CreateComposite([FromBody] CompositeOrderRequest request)
        {
            if (request?.Header == null) return BadRequest("Missing order header.");
            if (request.Header.Id != 0) return BadRequest("Expected header ID 0 for a new order.");

            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            // Auto order number when the client sends 0
            if (request.Header.OrderNumber <= 0)
            {
                var next = ComputeNextOrderNumber(requester, out var numFault);
                if (next == null) return BadRequest(numFault);
                request.Header.OrderNumber = next.Value;
            }

            clsOrderHeader header;
            try { header = request.Header.FromDto(requester); }
            catch (Exception ex)
            {
                Serilog.Log.Warning(ex, "Composite create: invalid header data");
                return BadRequest("Invalid order header data.");
            }

            // Retry a couple of times on order-number collisions (concurrent entry)
            clsFault fault = header.Update(requester);
            for (int attempt = 0; !fault.isOK && attempt < 2 && LooksLikeDuplicateNumber(fault); attempt++)
            {
                var next = ComputeNextOrderNumber(requester, out _);
                if (next == null) break;
                header.OrderNumber = next.Value;
                fault = header.Update(requester);
            }
            if (!fault.isOK)
                return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);

            var headerId = header.ID;
            var lineErrors = new List<string>();
            var savedLines = 0;

            foreach (var lineDto in request.Lines ?? new List<OrderLineUpdateDto>())
            {
                try
                {
                    lineDto.Id = 0;
                    lineDto.FkOrderHeaderId = headerId;
                    if (lineDto.LineNumber <= 0) lineDto.LineNumber = savedLines + 1;
                    var line = lineDto.FromDto(requester);
                    var lineFault = line.Update(requester);
                    if (!lineFault.isOK) { lineErrors.Add($"שורה {lineDto.LineNumber}: {lineFault.Message}"); continue; }
                    savedLines++;
                }
                catch (Exception ex)
                {
                    lineErrors.Add($"שורה {lineDto.LineNumber}: {ex.Message}");
                }
            }

            // Compensation: if NO line saved but lines were requested — remove the header
            if (savedLines == 0 && (request.Lines?.Count ?? 0) > 0)
            {
                try { header.Delete(requester); } catch { /* best effort */ }
                return UnprocessableEntity(new { message = "אף שורת הזמנה לא נשמרה — ההזמנה בוטלה.", errors = lineErrors });
            }

            // Reload for fresh totals (computed by the DB trigger)
            header = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad);
            header.GetByID(headerId, requester);

            var dto = header.ToDto();
            if (lineErrors.Count > 0)
                return StatusCode(207, new { header = dto, savedLines, errors = lineErrors });
            return CreatedAtAction(nameof(GetComposite), new { id = headerId }, new { header = dto, savedLines, errors = lineErrors });
        }

        // PUT api/orders/composite/{id} — update header + reconcile lines
        [HttpPut("composite/{id}")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult UpdateComposite(long id, [FromBody] CompositeOrderRequest request)
        {
            if (request?.Header == null) return BadRequest("Missing order header.");
            if (request.Header.Id != id) return BadRequest($"ID mismatch: route {id}, body {request.Header.Id}.");

            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsOrderHeader header;
            try { header = request.Header.FromDto(requester); }
            catch (Exception ex)
            {
                Serilog.Log.Warning(ex, "Composite update: invalid header data");
                return BadRequest("Invalid order header data.");
            }

            var fault = header.Update(requester);
            if (!fault.isOK)
                return BadRequest(!string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message);

            var errors = new List<string>();

            // 1) Delete removed lines
            foreach (var lineId in request.DeletedLineIds ?? new List<long>())
            {
                var line = new clsOrderLine();
                var f = line.GetByID(lineId, requester);
                if (!f.isOK) { errors.Add($"מחיקת שורה {lineId}: {f.Message}"); continue; }
                if (line.OrderHeaderID != id) { errors.Add($"שורה {lineId} אינה שייכת להזמנה זו."); continue; }
                f = line.Delete(requester);
                if (!f.isOK) errors.Add($"מחיקת שורה {lineId}: {f.Message}");
            }

            // 2) Create / update lines
            foreach (var lineDto in request.Lines ?? new List<OrderLineUpdateDto>())
            {
                try
                {
                    if (lineDto.Id < 0) lineDto.Id = 0;   // client temp IDs
                    lineDto.FkOrderHeaderId = id;
                    var line = lineDto.FromDto(requester);
                    var f = line.Update(requester);
                    if (!f.isOK) errors.Add($"שורה {lineDto.LineNumber}: {f.Message}");
                }
                catch (Exception ex)
                {
                    errors.Add($"שורה {lineDto.LineNumber}: {ex.Message}");
                }
            }

            // Reload for fresh totals
            header = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad);
            header.GetByID(id, requester);

            var dto = header.ToDto();
            if (errors.Count > 0) return StatusCode(207, new { header = dto, errors });
            return Ok(new { header = dto, errors });
        }

        // ─────────────────────────────────────────────────────────────

        private static int? ComputeNextOrderNumber(clsRequester requester, out string? error)
        {
            error = null;
            clsFault? fault = null;
            var col = new clsOrderHeaderCol(clsEnums.enmLoadParent.DoNotLoad);
            fault = col.FillByBoundedOrderNumber(0, int.MaxValue, requester, 1, clsEnums.enmFillDirection.DESC);
            if (fault == null || !fault.isOK)
            {
                error = fault?.Message ?? "Failed to compute next order number.";
                return null;
            }
            var max = 0;
            foreach (clsOrderHeader h in col) { max = Math.Max(max, h.OrderNumber); break; }
            return max + 1;
        }

        private static bool LooksLikeDuplicateNumber(clsFault fault)
        {
            var text = (fault.Message ?? "") + " " + (fault.FreeText ?? "");
            return text.Contains("UQ_OrderHeader_OrderNumber", StringComparison.OrdinalIgnoreCase)
                || text.Contains("duplicate", StringComparison.OrdinalIgnoreCase);
        }
    }
}
