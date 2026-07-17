using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class OrderHeaderDto 
    { 
        public long Id { get; set; } 
        public int OrderNumber { get; set; } 
        public long FkCustomerId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public decimal TotalAmount { get; set; } 
        public decimal VatAmount { get; set; } 
        public decimal TotalWithVat { get; set; } 
        public clsEnums.enmPaymentMethod EnmPaymentMethod { get; set; } 
        public clsEnums.enmPaymentStatus EnmPaymentStatus { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public string InvoiceNumber { get; set; } = string.Empty; 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime DeliveryDate { get; set; } 
        public clsEnums.enmDeliveryDay EnmDeliveryDay { get; set; } 
        public clsEnums.enmOrderStatus EnmOrderStatus { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public string Notes2 { get; set; } = string.Empty; 
        public string OrderMonth { get; set; } = string.Empty; 
        public string Quarter { get; set; } = string.Empty; 
        public string CustomerDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
