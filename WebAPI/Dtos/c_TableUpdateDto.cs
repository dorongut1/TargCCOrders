using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class TableUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string Name { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string DefaultTextFields { get; set; } = string.Empty; 
        public bool UsedForIdentity { get; set; } 
        public bool IsSingleRow { get; set; } 
        [StringLength(1)] 
        public string CanAdd { get; set; } = string.Empty; 
        [StringLength(1)] 
        public string CanEdit { get; set; } = string.Empty; 
        [StringLength(1)] 
        public string CanDelete { get; set; } = string.Empty; 
        public bool AuditAdd { get; set; } 
        public bool AuditEdit { get; set; } 
        public bool AuditDelete { get; set; } 
        public bool TrackRowChangers { get; set; } 
        public bool CreateUiMenu { get; set; } 
        public bool CreateUiCollection { get; set; } 
        public bool CreateUiEntity { get; set; } 
        public int SortOrder { get; set; } 
    } 
} 
