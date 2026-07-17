using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class SupplierOrderDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public string SupplierEmail { get; set; } = string.Empty; 
        public string EmailSubject { get; set; } = string.Empty; 
        public string EmailBody { get; set; } = string.Empty; 
        public clsEnums.enmEmailStatus EnmEmailStatus { get; set; } 
        public DateTime SentDate { get; set; } 
        public decimal TotalCost { get; set; } 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime RequestedDeliveryDate { get; set; } 
        public string RequestedDeliveryDay { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
        public string OrderHeaderDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
