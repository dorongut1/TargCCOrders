using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class MailUpdateDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmMessagingMode EnmMessagingMode { get; set; } 
        [StringLength(50)] 
        public string RecipientEmail { get; set; } = string.Empty; 
        public DateTimeOffset WhenSent { get; set; } 
        [StringLength(50)] 
        public string Subject { get; set; } = string.Empty; 
        public string Body { get; set; } = string.Empty; 
        public DateTimeOffset WhenSeen { get; set; } 
        public bool WasSeen { get; set; } 
    } 
} 
