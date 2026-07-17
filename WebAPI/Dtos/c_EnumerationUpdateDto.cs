using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class EnumerationUpdateDto 
    { 
        public int Id { get; set; } 
        public bool IsSystem { get; set; } 
        [StringLength(50)] 
        public string EnumType { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string EnumValue { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Text { get; set; } = string.Empty; 
    } 
} 
