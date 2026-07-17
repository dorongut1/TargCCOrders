using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class OrderLineDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public long FkProductId { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
        public decimal DiscountPercent { get; set; } 
        public decimal UnitCost { get; set; } 
        public int LineNumber { get; set; } 
        public decimal LineTotal { get; set; } 
        public decimal TotalCost { get; set; } 
        public decimal Profit { get; set; } 
        public string OrderHeaderDisplayName { get; set; } = string.Empty; 
        public string ProductDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
