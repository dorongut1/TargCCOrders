using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class RoleDto 
    { 
        public long Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public long FkBaseRoleId { get; set; } 
        public string BaseRoleDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
