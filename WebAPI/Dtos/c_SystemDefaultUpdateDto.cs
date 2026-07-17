using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class SystemDefaultUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string Group { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string SettingName { get; set; } = string.Empty; 
        public clsEnums.enmSystemDefaultType EnmSystemDefaultType { get; set; } 
        [StringLength(500)] 
        public string Description { get; set; } = string.Empty; 
    } 
} 
