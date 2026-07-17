using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserStatusDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        public string ApplicationName { get; set; } = string.Empty; 
        public long LastLoggedLoginId { get; set; } 
        public DateTime LoginTime { get; set; } 
        public DateTime LogoutTime { get; set; } 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
