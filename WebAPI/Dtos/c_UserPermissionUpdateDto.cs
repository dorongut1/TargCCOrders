using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserPermissionUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        [StringLength(50)] 
        public string ApplicationName { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ComputerIdentifier { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ComputerName { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ExternalIp { get; set; } = string.Empty; 
        public bool HasPermission { get; set; } 
        [StringLength(200)] 
        public string Comments { get; set; } = string.Empty; 
        public DateTime LastAccessTime { get; set; } 
        public long LoggedLoginId { get; set; } 
    } 
} 
