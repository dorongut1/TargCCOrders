using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ObjectToTranslateUpdateDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmObjectType EnmObjectType { get; set; } 
        [Required(AllowEmptyStrings = false)] 
        [StringLength(50)] 
        public string Object { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string Item { get; set; } = string.Empty; 
    } 
} 
