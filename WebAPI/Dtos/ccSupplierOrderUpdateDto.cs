using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class SupplierOrderUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        [StringLength(255)] 
        public string SupplierEmail { get; set; } = string.Empty; 
        [StringLength(500)] 
        public string EmailSubject { get; set; } = string.Empty; 
        public string EmailBody { get; set; } = string.Empty; 
        public clsEnums.enmEmailStatus EnmEmailStatus { get; set; } 
        public DateTime SentDate { get; set; } 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime RequestedDeliveryDate { get; set; } 
        [StringLength(10)] 
        public string RequestedDeliveryDay { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
    } 
} 
