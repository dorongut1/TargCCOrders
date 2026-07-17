using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class CustomerDto 
    { 
        public long Id { get; set; } 
        public string CustomerCode { get; set; } = string.Empty; 
        public string CustomerName { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string City { get; set; } = string.Empty; 
        public string TaxId { get; set; } = string.Empty; 
        public clsEnums.enmCustomerType EnmCustomerType { get; set; } 
        public int PaymentTermsDays { get; set; } 
        public string Notes { get; set; } = string.Empty; 
        public bool IsActive { get; set; } 
        public string Location { get; set; } = string.Empty; 
        public string AccountantEmail { get; set; } = string.Empty; 
        public clsEnums.enmAccountantMethod EnmAccountantMethod { get; set; } 
        public string InvoiceName { get; set; } = string.Empty; 
        public string ProfitabilityCode { get; set; } = string.Empty; 
        public string CustomerIdentifier { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
