using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ObjectToTranslateDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmObjectType EnmObjectType { get; set; } 
        public string Object { get; set; } = string.Empty; 
        public string Item { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
