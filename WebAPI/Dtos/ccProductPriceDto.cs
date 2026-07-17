using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductPriceDto 
    { 
        public long Id { get; set; } 
        public long FkProductId { get; set; } 
        public clsEnums.enmCustomerType EnmCustomerType { get; set; } 
        public decimal SellingPrice { get; set; } 
        public int MinQuantity { get; set; } 
        public decimal DiscountPercent { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public string ProductDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
