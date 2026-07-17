using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class MailDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmMessagingMode EnmMessagingMode { get; set; } 
        public string RecipientEmail { get; set; } = string.Empty; 
        public DateTimeOffset WhenSent { get; set; } 
        public string Subject { get; set; } = string.Empty; 
        public string Body { get; set; } = string.Empty; 
        public DateTimeOffset WhenSeen { get; set; } 
        public bool WasSeen { get; set; } 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
