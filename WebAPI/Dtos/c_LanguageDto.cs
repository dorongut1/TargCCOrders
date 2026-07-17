using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LanguageDto 
    { 
        public long Id { get; set; } 
        public string Code { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string NameLoc { get; set; } = string.Empty; 
        public string Culture { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
