using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Real dashboard aggregates. Replaces the hardcoded mock numbers that were
    /// previously rendered client-side.
    /// Volumes here (~7K orders) make in-memory aggregation acceptable; when the
    /// data grows, push these into SQL views.
    /// </summary>
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        // GET api/dashboard/summary
        [HttpGet("summary")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult GetSummary()
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            clsFault? fault = null;

            // Orders
            var orders = new clsOrderHeaderCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault);
            if (fault == null || !fault.isOK) return BadRequest(fault?.Message ?? "Failed to load orders");

            var now = DateTime.Now;
            var monthStart = new DateTime(now.Year, now.Month, 1);
            var yearAgo = monthStart.AddMonths(-11);

            decimal monthRevenue = 0, yearRevenue = 0;
            int monthOrders = 0, openOrders = 0, unpaidOrders = 0;
            var monthly = new SortedDictionary<string, (decimal revenue, int count)>(StringComparer.Ordinal);
            var byStatus = new Dictionary<string, int>();

            foreach (clsOrderHeader o in orders)
            {
                var status = o.OrderStatus.ToString();
                byStatus[status] = byStatus.TryGetValue(status, out var c) ? c + 1 : 1;

                if (o.OrderStatus != clsEnums.enmOrderStatus.Completed &&
                    o.OrderStatus != clsEnums.enmOrderStatus.Cancelled)
                    openOrders++;

                if (o.PaymentStatus == clsEnums.enmPaymentStatus.Unpaid ||
                    o.PaymentStatus == clsEnums.enmPaymentStatus.Pending ||
                    o.PaymentStatus == clsEnums.enmPaymentStatus.PartiallyPaid)
                    unpaidOrders++;

                if (o.OrderDate >= monthStart)
                {
                    monthOrders++;
                    monthRevenue += o.TotalWithVAT;
                }

                if (o.OrderDate >= yearAgo)
                {
                    yearRevenue += o.TotalWithVAT;
                    var key = o.OrderDate.ToString("yyyy-MM");
                    monthly[key] = monthly.TryGetValue(key, out var m)
                        ? (m.revenue + o.TotalWithVAT, m.count + 1)
                        : (o.TotalWithVAT, 1);
                }
            }

            // Debts
            fault = null;
            var debts = new clsCustomerDebtCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault);
            decimal openDebtTotal = 0;
            int needsAttention = 0, openDebts = 0;
            if (fault != null && fault.isOK)
            {
                foreach (clsCustomerDebt d in debts)
                {
                    if (d.DebtStatus == clsEnums.enmDebtStatus.Paid ||
                        d.DebtStatus == clsEnums.enmDebtStatus.Cancelled ||
                        d.DebtStatus == clsEnums.enmDebtStatus.WrittenOff) continue;
                    openDebts++;
                    openDebtTotal += d.RemainingAmount;
                    if (d.NeedsAttention) needsAttention++;
                }
            }

            // Deliveries
            fault = null;
            var deliveries = new clsDeliveryCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault);
            int pendingDeliveries = 0;
            if (fault != null && fault.isOK)
            {
                foreach (clsDelivery d in deliveries)
                {
                    if (d.DeliveryStatus == clsEnums.enmDeliveryStatus.Pending ||
                        d.DeliveryStatus == clsEnums.enmDeliveryStatus.Ordered ||
                        d.DeliveryStatus == clsEnums.enmDeliveryStatus.InTransit ||
                        d.DeliveryStatus == clsEnums.enmDeliveryStatus.AtHub)
                        pendingDeliveries++;
                }
            }

            // Fill empty months so the chart is continuous
            var monthlySeries = new List<object>();
            for (var m = yearAgo; m <= monthStart; m = m.AddMonths(1))
            {
                var key = m.ToString("yyyy-MM");
                monthly.TryGetValue(key, out var v);
                monthlySeries.Add(new { month = key, revenue = Math.Round(v.revenue, 2), orders = v.count });
            }

            return Ok(new
            {
                monthRevenue = Math.Round(monthRevenue, 2),
                yearRevenue = Math.Round(yearRevenue, 2),
                monthOrders,
                openOrders,
                unpaidOrders,
                openDebts,
                openDebtTotal = Math.Round(openDebtTotal, 2),
                debtsNeedingAttention = needsAttention,
                pendingDeliveries,
                monthlySeries,
                ordersByStatus = byStatus.Select(kv => new { status = kv.Key, count = kv.Value })
            });
        }
    }
}
