using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class SystemDefaultDto 
    { 
        public long Id { get; set; } 
        public string Group { get; set; } = string.Empty; 
        public string SettingName { get; set; } = string.Empty; 
        public string SettingValue { get; set; } = string.Empty; 
        public clsEnums.enmSystemDefaultType EnmSystemDefaultType { get; set; } 
        public string Description { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
