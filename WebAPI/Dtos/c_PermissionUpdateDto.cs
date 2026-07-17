using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class PermissionUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkProcessId { get; set; } 
        public long FkRoleId { get; set; } 
        public bool CanDo { get; set; } 
    } 
} 
