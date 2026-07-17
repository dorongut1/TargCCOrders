using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class EnumerationDto 
    { 
        public int Id { get; set; } 
        public bool IsSystem { get; set; } 
        public string EnumType { get; set; } = string.Empty; 
        public string EnumValue { get; set; } = string.Empty; 
        public string Text { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
