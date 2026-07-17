using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductUpdateDto 
    { 
        public long Id { get; set; } 
        [Required(AllowEmptyStrings = false)] 
        [StringLength(50)] 
        public string ProductCode { get; set; } = string.Empty; 
        [Required(AllowEmptyStrings = false)] 
        [StringLength(255)] 
        public string ProductName { get; set; } = string.Empty; 
        public clsEnums.enmCategory EnmCategory { get; set; } 
        [StringLength(20)] 
        public string UnitOfMeasure { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
        public decimal BaseCost { get; set; } 
    } 
} 
