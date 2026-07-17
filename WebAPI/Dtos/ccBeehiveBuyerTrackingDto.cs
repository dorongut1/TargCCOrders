using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class BeehiveBuyerTrackingDto 
    { 
        public long Id { get; set; } 
        public long FkCustomerId { get; set; } 
        public DateTime LastOrderDate { get; set; } 
        public int BeehiveQuantity { get; set; } 
        public int ReminderMonth { get; set; } 
        public bool IsRelevant { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public string CustomerDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
