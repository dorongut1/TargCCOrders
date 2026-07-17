using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class IndexFragmentationUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(200)] 
        public string TableName { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string IndexName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string IndexType { get; set; } = string.Empty; 
        public decimal FragmentationPct { get; set; } 
        public int PageCount { get; set; } 
    } 
} 
