using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedJobDto 
    { 
        public long Id { get; set; } 
        public long FkJobId { get; set; } 
        public DateTime WhenStarted { get; set; } 
        public string ActivatingUser { get; set; } = string.Empty; 
        public string LastRunBy { get; set; } = string.Empty; 
        public int ExecutionTimeSec { get; set; } 
        public clsEnums.enmJobStatus EnmRunStatus { get; set; } 
        public string Remarks { get; set; } = string.Empty; 
        public long FkLoggedAlertId { get; set; } 
        public int SuccessCount { get; set; } 
        public int FailureCount { get; set; } 
        public string JobDisplayName { get; set; } = string.Empty; 
        public string LoggedAlertDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
