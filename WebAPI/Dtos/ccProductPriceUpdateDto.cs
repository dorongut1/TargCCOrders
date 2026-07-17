using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductPriceUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkProductId { get; set; } 
        public clsEnums.enmCustomerType EnmCustomerType { get; set; } 
        public decimal SellingPrice { get; set; } 
        public int MinQuantity { get; set; } 
        public decimal DiscountPercent { get; set; } 
        public string Notes { get; set; } = string.Empty; 
    } 
} 
