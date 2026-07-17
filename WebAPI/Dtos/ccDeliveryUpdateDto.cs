using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class DeliveryUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        [StringLength(500)] 
        public string DeliveryAddress { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ContactPhone { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string ContactName { get; set; } = string.Empty; 
        public clsEnums.enmDeliveryMethod EnmDeliveryMethod { get; set; } 
        public DateTime OrderedDate { get; set; } 
        public DateTime ReceivedDate { get; set; } 
        public DateTime ArrivalToHubDate { get; set; } 
        public DateTime ArrivalToCustomerDate { get; set; } 
        public clsEnums.enmDeliveryStatus EnmDeliveryStatus { get; set; } 
        [StringLength(500)] 
        public string Location { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
    } 
} 
