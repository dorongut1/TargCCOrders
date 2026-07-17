using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class ProductDto 
    { 
        public long Id { get; set; } 
        public string ProductCode { get; set; } = string.Empty; 
        public string ProductName { get; set; } = string.Empty; 
        public clsEnums.enmCategory EnmCategory { get; set; } 
        public string UnitOfMeasure { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty; 
        public bool IsActive { get; set; } 
        public int CurrentStock { get; set; } 
        public decimal BaseCost { get; set; } 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
