using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class IndexFragmentationDto 
    { 
        public long Id { get; set; } 
        public string TableName { get; set; } = string.Empty; 
        public string IndexName { get; set; } = string.Empty; 
        public string IndexType { get; set; } = string.Empty; 
        public decimal FragmentationPct { get; set; } 
        public int PageCount { get; set; } 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
