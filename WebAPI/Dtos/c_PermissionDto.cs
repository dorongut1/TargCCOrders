using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class PermissionDto 
    { 
        public long Id { get; set; } 
        public long FkProcessId { get; set; } 
        public long FkRoleId { get; set; } 
        public bool CanDo { get; set; } 
        public string ProcessDisplayName { get; set; } = string.Empty; 
        public string RoleDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
