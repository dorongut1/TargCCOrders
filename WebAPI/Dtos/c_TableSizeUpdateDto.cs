using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class TableSizeUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(200)] 
        public string TableName { get; set; } = string.Empty; 
        public int NumberOfRows { get; set; } 
        public int ReservedSizeKb { get; set; } 
        public int DataSizeKb { get; set; } 
        public int IndexSizeKb { get; set; } 
        public int UnusedSizeKb { get; set; } 
    } 
} 
