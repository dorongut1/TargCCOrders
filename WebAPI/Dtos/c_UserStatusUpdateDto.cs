using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserStatusUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        [StringLength(50)] 
        public string ApplicationName { get; set; } = string.Empty; 
        public long LastLoggedLoginId { get; set; } 
        public DateTime LoginTime { get; set; } 
        public DateTime LogoutTime { get; set; } 
    } 
} 
