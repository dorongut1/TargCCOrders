using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class DeliveryDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public string DeliveryAddress { get; set; } = string.Empty; 
        public string ContactPhone { get; set; } = string.Empty; 
        public string ContactName { get; set; } = string.Empty; 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime OrderedDate { get; set; } 
        public DateTime ReceivedDate { get; set; } 
        public DateTime ArrivalToHubDate { get; set; } 
        public DateTime ArrivalToCustomerDate { get; set; } 
        public clsEnums.enmDeliveryStatus EnmDeliveryStatus { get; set; } 
        public string Location { get; set; } = string.Empty; 
        public string ProductsSummary { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
        public string OrderHeaderDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
