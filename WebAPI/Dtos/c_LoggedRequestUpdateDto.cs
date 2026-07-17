using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedRequestUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkLoggedLoginId { get; set; } 
        public DateTime TimeAccessed { get; set; } 
        public long FkUserId { get; set; } 
        [StringLength(100)] 
        public string CallingFunctionWithinApplication { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string EntryPoint { get; set; } = string.Empty; 
        [StringLength(75)] 
        public string Process { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Thread { get; set; } = string.Empty; 
    } 
} 
