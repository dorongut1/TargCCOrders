using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class AlertMessageUpdateDto 
    { 
        public long Id { get; set; } 
        public int Number { get; set; } 
        [StringLength(100)] 
        public string Description { get; set; } = string.Empty; 
        public clsEnums.enmFaultType EnmType { get; set; } 
        public clsEnums.enmFaultSeverity EnmSeverity { get; set; } 
        [StringLength(100)] 
        public string Message { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string Action { get; set; } = string.Empty; 
    } 
} 
