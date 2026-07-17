using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class JobAlertRecipientUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkJobId { get; set; } 
        public long FkUserId { get; set; } 
        public clsEnums.enmJobAlertType EnmJobAlertType { get; set; } 
        [StringLength(50)] 
        public string OverrideName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string OverrideEmailOrPhone { get; set; } = string.Empty; 
    } 
} 
