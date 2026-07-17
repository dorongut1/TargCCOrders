using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LookupDto 
    { 
        public long Id { get; set; } 
        public clsEnums.enmLookup EnmParentLookupType { get; set; } 
        public string ParentCode { get; set; } = string.Empty; 
        public clsEnums.enmLookup EnmLookupType { get; set; } 
        public string Code { get; set; } = string.Empty; 
        public string Text { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
