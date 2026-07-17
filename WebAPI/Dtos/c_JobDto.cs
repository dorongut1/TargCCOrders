using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class JobDto 
    { 
        public long Id { get; set; } 
        public string LkpJobCode { get; set; } = string.Empty; 
        public string LkpJobRunnerCode { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public string Instructions { get; set; } = string.Empty; 
        public clsEnums.enmJobType EnmJobType { get; set; } 
        public DateTime WhenToRun { get; set; } 
        public int CyclicCount { get; set; } 
        public bool SendNotificationOnSuccess { get; set; } 
        public bool SendAlarmOnMissed { get; set; } 
        public int TimeOutSec { get; set; } 
        public bool Active { get; set; } 
        public string ActivatingUser { get; set; } = string.Empty; 
        public DateTime NextRunTime { get; set; } 
        public DateTime LastRunTime { get; set; } 
        public clsEnums.enmJobStatus EnmJobStatus { get; set; } 
        public bool WarningMailSent { get; set; } 
        public bool IsManaged { get; set; } 
        public string LastRunBy { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
