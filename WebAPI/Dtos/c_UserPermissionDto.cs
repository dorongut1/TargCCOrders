using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserPermissionDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        public string ApplicationName { get; set; } = string.Empty; 
        public string ComputerIdentifier { get; set; } = string.Empty; 
        public string ComputerName { get; set; } = string.Empty; 
        public string ExternalIp { get; set; } = string.Empty; 
        public bool HasPermission { get; set; } 
        public string Comments { get; set; } = string.Empty; 
        public DateTime LastAccessTime { get; set; } 
        public long LoggedLoginId { get; set; } 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
