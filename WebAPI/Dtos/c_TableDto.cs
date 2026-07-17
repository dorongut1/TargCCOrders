using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class TableDto 
    { 
        public long Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string DefaultTextFields { get; set; } = string.Empty; 
        public bool UsedForIdentity { get; set; } 
        public bool IsSingleRow { get; set; } 
        public string CanAdd { get; set; } = string.Empty; 
        public string CanEdit { get; set; } = string.Empty; 
        public string CanDelete { get; set; } = string.Empty; 
        public bool AuditAdd { get; set; } 
        public bool AuditEdit { get; set; } 
        public bool AuditDelete { get; set; } 
        public bool TrackRowChangers { get; set; } 
        public bool CreateUiMenu { get; set; } 
        public bool CreateUiCollection { get; set; } 
        public bool CreateUiEntity { get; set; } 
        public int SortOrder { get; set; } 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
