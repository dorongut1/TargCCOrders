using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class CustomerDebtUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkCustomerId { get; set; } 
        public long FkOrderHeaderId { get; set; } 
        public decimal DebtAmount { get; set; } 
        public decimal PaidAmount { get; set; } 
        public DateTime DebtDate { get; set; } 
        public DateTime DueDate { get; set; } 
        public clsEnums.enmDebtStatus EnmDebtStatus { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        [StringLength(500)] 
        public string ProductTypes { get; set; } = string.Empty; 
        public DateTime DeliveryDate { get; set; } 
    } 
} 
