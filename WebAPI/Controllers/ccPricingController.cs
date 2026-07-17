using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using System;
using System.Linq;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Server-side price resolution: the correct selling price for a product is
    /// determined by the CUSTOMER TYPE tier (ProductPrice is keyed by
    /// ProductID + enmCustomerType). Previously the client guessed the price
    /// with a wrong query key and could apply the wrong tier.
    /// </summary>
    [Route("api/pricing")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        // GET api/pricing/resolve?productId=5&customerId=12&quantity=3
        [HttpGet("resolve")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult Resolve([FromQuery] long productId, [FromQuery] long customerId, [FromQuery] int quantity = 1)
        {
            if (productId <= 0 || customerId <= 0) return BadRequest("productId and customerId are required.");

            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            // 1) Customer → customer type
            var customer = new clsCustomer();
            var fault = customer.GetByID(customerId, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(new { message = "Customer not found." });

            // 2) Price rows for (product, customer type)
            var prices = new clsProductPriceCol(clsEnums.enmLoadParent.DoNotLoad);
            fault = prices.FillByProductIDAndCustomerType(productId, customer.CustomerType, requester);
            if (!fault.isOK) return BadRequest(fault.Message);

            // Pick the best matching tier: highest MinQuantity that the requested
            // quantity satisfies (supports quantity-tiered pricing).
            clsProductPrice? best = null;
            foreach (clsProductPrice p in prices)
            {
                var minQ = Math.Max(1, p.MinQuantity);
                if (quantity >= minQ && (best == null || minQ > Math.Max(1, best.MinQuantity)))
                    best = p;
            }
            // Fallback: the row with the lowest MinQuantity (quantity below all tiers)
            if (best == null)
            {
                foreach (clsProductPrice p in prices)
                    if (best == null || p.MinQuantity < best.MinQuantity) best = p;
            }

            if (best == null)
                return Ok(new { found = false, customerType = customer.CustomerType.ToString() });

            return Ok(new
            {
                found = true,
                customerType = customer.CustomerType.ToString(),
                unitPrice = best.SellingPrice,
                discountPercent = best.DiscountPercent,
                minQuantity = best.MinQuantity,
                priceId = best.ID
            });
        }
    }
}
