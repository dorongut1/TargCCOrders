using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ObjectTranslationDto 
    { 
        public long Id { get; set; } 
        public long FkObjectToTranslateId { get; set; } 
        public long Instance { get; set; } 
        public string DefaultText { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        public string Text { get; set; } = string.Empty; 
        public string InstanceUniqueText { get; set; } = string.Empty; 
        public string ObjectToTranslateDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
