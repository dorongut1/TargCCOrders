using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class JobAlertRecipientDto 
    { 
        public long Id { get; set; } 
        public long FkJobId { get; set; } 
        public long FkUserId { get; set; } 
        public clsEnums.enmJobAlertType EnmJobAlertType { get; set; } 
        public string OverrideName { get; set; } = string.Empty; 
        public string OverrideEmailOrPhone { get; set; } = string.Empty; 
        public string JobDisplayName { get; set; } = string.Empty; 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
