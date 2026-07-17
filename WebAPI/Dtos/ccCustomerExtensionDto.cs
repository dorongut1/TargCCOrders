using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class CustomerExtensions 
    { 
        public static CustomerDto ToDto(this clsCustomer customer) 
        { 
            if (customer is null) return null!; 
 
            var dto = new CustomerDto 
            { 
                Id = customer.ID, 
                CustomerCode = customer.CustomerCode, 
                CustomerName = customer.CustomerName, 
                Phone = customer.Phone, 
                Email = customer.Email, 
                Address = customer.Address, 
                City = customer.City, 
                TaxId = customer.TaxID, 
                EnmCustomerType = customer.CustomerType, 
                PaymentTermsDays = customer.PaymentTermsDays, 
                Notes = customer.Notes, 
                IsActive = customer.IsActive, 
                Location = customer.Location, 
                AccountantEmail = customer.AccountantEmail, 
                EnmAccountantMethod = customer.AccountantMethod, 
                InvoiceName = customer.InvoiceName, 
                ProfitabilityCode = customer.ProfitabilityCode, 
                CustomerIdentifier = customer.CustomerIdentifier
            }; 
            dto._etag = ComputeETag(customer); 
            return dto; 
        } 
 
        public static string ComputeETag(clsCustomer entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.CustomerCode ?? ""); 
            sb.Append('|').Append(entity.CustomerName ?? ""); 
            sb.Append('|').Append(entity.Phone ?? ""); 
            sb.Append('|').Append(entity.Email ?? ""); 
            sb.Append('|').Append(entity.Address ?? ""); 
            sb.Append('|').Append(entity.City ?? ""); 
            sb.Append('|').Append(entity.TaxID ?? ""); 
            sb.Append('|').Append(entity.CustomerType); 
            sb.Append('|').Append(entity.PaymentTermsDays); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            sb.Append('|').Append(entity.IsActive); 
            sb.Append('|').Append(entity.Location ?? ""); 
            sb.Append('|').Append(entity.AccountantEmail ?? ""); 
            sb.Append('|').Append(entity.AccountantMethod); 
            sb.Append('|').Append(entity.InvoiceName ?? ""); 
            sb.Append('|').Append(entity.ProfitabilityCode ?? ""); 
            sb.Append('|').Append(entity.CustomerIdentifier ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsCustomer FromDto(this CustomerUpdateDto customerDto, clsRequester requester) 
        { 
            if (customerDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsCustomer customer = new clsCustomer(); 
            if (customerDto.Id > 0) 
            { 
                clsFault fault = customer.GetByID(customerDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //customer.ID = customerDto.Id; //not transferred on purpose ! 
            customer.CustomerCode = customerDto.CustomerCode; 
            customer.CustomerName = customerDto.CustomerName; 
            customer.Phone = customerDto.Phone; 
            customer.Email = customerDto.Email; 
            customer.Address = customerDto.Address; 
            customer.City = customerDto.City; 
            customer.TaxID = customerDto.TaxId; 
            customer.CustomerType = customerDto.EnmCustomerType; 
            customer.PaymentTermsDays = customerDto.PaymentTermsDays; 
            customer.Notes = customerDto.Notes; 
            customer.Location = customerDto.Location; 
            customer.AccountantEmail = customerDto.AccountantEmail; 
            customer.AccountantMethod = customerDto.EnmAccountantMethod; 
            customer.InvoiceName = customerDto.InvoiceName; 
            customer.ProfitabilityCode = customerDto.ProfitabilityCode; 
 
            return customer; 
        } 
    } 
} 
