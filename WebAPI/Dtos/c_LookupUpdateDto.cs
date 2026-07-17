using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LookupUpdateDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmLookup EnmParentLookupType { get; set; } 
        [StringLength(50)] 
        public string ParentCode { get; set; } = string.Empty; 
        public clsEnums.enmLookup EnmLookupType { get; set; } 
        [StringLength(50)] 
        public string Code { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string Text { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Description { get; set; } = string.Empty; 
    } 
} 
