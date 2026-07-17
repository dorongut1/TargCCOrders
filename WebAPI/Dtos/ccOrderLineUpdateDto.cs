using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class OrderLineUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public long FkProductId { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
        public decimal DiscountPercent { get; set; } 
        public int LineNumber { get; set; } 
    } 
} 
