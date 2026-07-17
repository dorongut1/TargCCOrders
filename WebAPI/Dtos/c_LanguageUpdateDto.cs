using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LanguageUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string Code { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Name { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string NameLoc { get; set; } = string.Empty; 
        [StringLength(10)] 
        public string Culture { get; set; } = string.Empty; 
    } 
} 
