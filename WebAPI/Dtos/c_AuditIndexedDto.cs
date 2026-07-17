using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class AuditIndexedDto 
    { 
        public long Id { get; set; } 
        public long OriginalId { get; set; } 
        public string TableName { get; set; } = string.Empty; 
        public long RowId { get; set; } 
        public string Operation { get; set; } = string.Empty; 
        public DateTime OccurredAt { get; set; } 
        public string SqlCurrentUser { get; set; } = string.Empty; 
        public string FieldName { get; set; } = string.Empty; 
        public string OldValue { get; set; } = string.Empty; 
        public string NewValue { get; set; } = string.Empty; 
        public string ChangedByUser { get; set; } = string.Empty; 
        public long ActiveLoginId { get; set; } 
        public string SqlSystemUser { get; set; } = string.Empty; 
        public string SqlAppName { get; set; } = string.Empty; 
        public string SqlHostName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
