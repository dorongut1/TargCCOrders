using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class OrderHeaderUpdateDto 
    { 
        public long Id { get; set; } 
        public int OrderNumber { get; set; } 
        public long FkCustomerId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public clsEnums.enmPaymentMethod EnmPaymentMethod { get; set; } 
        public clsEnums.enmPaymentStatus EnmPaymentStatus { get; set; } 
        public DateTime PaymentDate { get; set; } 
        [StringLength(50)] 
        public string InvoiceNumber { get; set; } = string.Empty; 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime DeliveryDate { get; set; } 
        public clsEnums.enmDeliveryDay EnmDeliveryDay { get; set; } 
        public clsEnums.enmOrderStatus EnmOrderStatus { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public string Notes2 { get; set; } = string.Empty; 
    } 
} 
