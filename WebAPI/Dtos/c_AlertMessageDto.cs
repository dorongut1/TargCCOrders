using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class AlertMessageDto 
    { 
        public long Id { get; set; } 
        public int Number { get; set; } 
        public string Description { get; set; } = string.Empty; 
        public clsEnums.enmFaultType EnmType { get; set; } 
        public clsEnums.enmFaultSeverity EnmSeverity { get; set; } 
        public string Message { get; set; } = string.Empty; 
        public string Action { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
