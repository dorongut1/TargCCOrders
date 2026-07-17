using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductPriceHistUpdateDto 
    { 
        public long Id { get; set; } 
        public long ProductId { get; set; } 
        public clsEnums.enmCustomerType EnmCustomerType { get; set; } 
        public decimal BaseCost { get; set; } 
        public decimal SellingPrice { get; set; } 
        public int MinQuantity { get; set; } 
        public decimal DiscountPercent { get; set; } 
        public DateTime ValidFrom { get; set; } 
        public DateTime ValidTo { get; set; } 
        public DateTime ArchivedDate { get; set; } 
        [StringLength(255)] 
        public string ArchivedReason { get; set; } = string.Empty; 
        public long OriginalPriceId { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string AddFieldsHere { get; set; } = string.Empty; 
    } 
} 
