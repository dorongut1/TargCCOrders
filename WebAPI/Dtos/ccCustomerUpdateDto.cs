using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class CustomerUpdateDto 
    { 
        public long Id { get; set; } 
        [Required(AllowEmptyStrings = false)] 
        [StringLength(50)] 
        public string CustomerCode { get; set; } = string.Empty; 
        [Required(AllowEmptyStrings = false)] 
        [StringLength(255)] 
        public string CustomerName { get; set; } = string.Empty; 
        [StringLength(20)] 
        public string Phone { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string Email { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string City { get; set; } = string.Empty; 
        [StringLength(20)] 
        public string TaxId { get; set; } = string.Empty; 
        public clsEnums.enmCustomerType EnmCustomerType { get; set; } 
        public int PaymentTermsDays { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string Location { get; set; } = string.Empty; 
        [StringLength(255)] 
        public string AccountantEmail { get; set; } = string.Empty; 
        public clsEnums.enmAccountantMethod EnmAccountantMethod { get; set; } 
        [StringLength(255)] 
        public string InvoiceName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ProfitabilityCode { get; set; } = string.Empty; 
    } 
} 
