using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class CustomerDebtDto 
    { 
        public long Id { get; set; } 
        public long FkCustomerId { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public decimal DebtAmount { get; set; } 
        public decimal PaidAmount { get; set; } 
        public decimal RemainingAmount { get; set; } 
        public DateTime DebtDate { get; set; } 
        public DateTime DueDate { get; set; } 
        public clsEnums.enmDebtStatus EnmDebtStatus { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public bool NeedsAttention { get; set; } 
        public string ProductTypes { get; set; } = string.Empty; 
        public DateTime DeliveryDate { get; set; } 
        public string CustomerDisplayName { get; set; } = string.Empty; 
        public string OrderHeaderDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
