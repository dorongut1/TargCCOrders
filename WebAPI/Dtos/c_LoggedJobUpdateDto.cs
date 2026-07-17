using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedJobUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkJobId { get; set; } 
        public DateTime WhenStarted { get; set; } 
        [StringLength(50)] 
        public string ActivatingUser { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LastRunBy { get; set; } = string.Empty; 
        public int ExecutionTimeSec { get; set; } 
        public clsEnums.enmJobStatus EnmRunStatus { get; set; } 
        public string Remarks { get; set; } = string.Empty; 
        public long FkLoggedAlertId { get; set; } 
        public int SuccessCount { get; set; } 
        public int FailureCount { get; set; } 
    } 
} 
