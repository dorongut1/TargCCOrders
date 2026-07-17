using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class BeehiveBuyerTrackingUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkCustomerId { get; set; } 
        public DateTime LastOrderDate { get; set; } 
        public int BeehiveQuantity { get; set; } 
        public int ReminderMonth { get; set; } 
        public string Notes { get; set; } = string.Empty; 
    } 
} 
