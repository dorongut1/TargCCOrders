using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProcessUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(100)] 
        public string Name { get; set; } = string.Empty; 
        public DateTime DateChecked { get; set; } 
    } 
} 
