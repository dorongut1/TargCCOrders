using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class JobUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string LkpJobCode { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LkpJobRunnerCode { get; set; } = string.Empty; 
        [StringLength(500)] 
        public string Description { get; set; } = string.Empty; 
        [StringLength(1000)] 
        public string Instructions { get; set; } = string.Empty; 
        public clsEnums.enmJobType EnmJobType { get; set; } 
        public DateTime WhenToRun { get; set; } 
        public int CyclicCount { get; set; } 
        public bool SendNotificationOnSuccess { get; set; } 
        public bool SendAlarmOnMissed { get; set; } 
        public int TimeOutSec { get; set; } 
        public bool Active { get; set; } 
        public bool IsManaged { get; set; } 
        [StringLength(50)] 
        public string LastRunBy { get; set; } = string.Empty; 
    } 
} 
