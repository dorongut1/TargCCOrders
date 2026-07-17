using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class SystemAuditUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string TableName { get; set; } = string.Empty; 
        public long RowId { get; set; } 
        [StringLength(10)] 
        public string Operation { get; set; } = string.Empty; 
        public DateTime OccurredAt { get; set; } 
        [StringLength(50)] 
        public string SqlCurrentUser { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ChangedByUser { get; set; } = string.Empty; 
        public long ActiveLoginId { get; set; } 
        [StringLength(50)] 
        public string SqlSystemUser { get; set; } = string.Empty; 
        [StringLength(250)] 
        public string SqlAppName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string SqlHostName { get; set; } = string.Empty; 
        public string Changes { get; set; } = string.Empty; 
    } 
} 
