using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class TableSizeDto 
    { 
        public long Id { get; set; } 
        public string TableName { get; set; } = string.Empty; 
        public int NumberOfRows { get; set; } 
        public int ReservedSizeKb { get; set; } 
        public int DataSizeKb { get; set; } 
        public int IndexSizeKb { get; set; } 
        public int UnusedSizeKb { get; set; } 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
