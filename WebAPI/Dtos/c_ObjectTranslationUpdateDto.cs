using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ObjectTranslationUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkObjectToTranslateId { get; set; } 
        public long Instance { get; set; } 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        [Required(AllowEmptyStrings = false)] 
        public string Text { get; set; } = string.Empty; 
        [StringLength(500)] 
        public string InstanceUniqueText { get; set; } = string.Empty; 
    } 
} 
