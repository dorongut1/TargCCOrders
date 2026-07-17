using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductPriceHistDto 
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
        public string ArchivedReason { get; set; } = string.Empty; 
        public long OriginalPriceId { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public string AddFieldsHere { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
