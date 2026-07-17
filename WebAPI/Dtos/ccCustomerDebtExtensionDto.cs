using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class CustomerDebtExtensions 
    { 
        public static CustomerDebtDto ToDto(this clsCustomerDebt customerDebt) 
        { 
            if (customerDebt is null) return null!; 
 
            var dto = new CustomerDebtDto 
            { 
                Id = customerDebt.ID, 
                FkCustomerId = customerDebt.CustomerID, 
                FkOrderHeaderId = customerDebt.OrderHeaderID, 
                DebtAmount = customerDebt.DebtAmount, 
                PaidAmount = customerDebt.PaidAmount, 
                RemainingAmount = customerDebt.RemainingAmount, 
                DebtDate = customerDebt.DebtDate, 
                DueDate = customerDebt.DueDate, 
                EnmDebtStatus = customerDebt.DebtStatus, 
                Notes = customerDebt.Notes, 
                NeedsAttention = customerDebt.NeedsAttention, 
                ProductTypes = customerDebt.ProductTypes, 
                DeliveryDate = customerDebt.DeliveryDate
            }; 
            dto._etag = ComputeETag(customerDebt); 
            return dto; 
        } 
 
        public static string ComputeETag(clsCustomerDebt entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.CustomerID); 
            sb.Append('|').Append(entity.OrderHeaderID); 
            sb.Append('|').Append(entity.DebtAmount); 
            sb.Append('|').Append(entity.PaidAmount); 
            sb.Append('|').Append(entity.RemainingAmount); 
            sb.Append('|').Append(entity.DebtDate.Ticks); 
            sb.Append('|').Append(entity.DueDate.Ticks); 
            sb.Append('|').Append(entity.DebtStatus); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            sb.Append('|').Append(entity.NeedsAttention); 
            sb.Append('|').Append(entity.ProductTypes ?? ""); 
            sb.Append('|').Append(entity.DeliveryDate.Ticks); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsCustomerDebt FromDto(this CustomerDebtUpdateDto customerDebtDto, clsRequester requester) 
        { 
            if (customerDebtDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsCustomerDebt customerDebt = new clsCustomerDebt(); 
            if (customerDebtDto.Id > 0) 
            { 
                clsFault fault = customerDebt.GetByID(customerDebtDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //customerDebt.ID = customerDebtDto.Id; //not transferred on purpose ! 
            customerDebt.CustomerID = customerDebtDto.FkCustomerId; 
            customerDebt.OrderHeaderID = customerDebtDto.FkOrderHeaderId; 
            customerDebt.DebtAmount = customerDebtDto.DebtAmount; 
            customerDebt.PaidAmount = customerDebtDto.PaidAmount; 
            customerDebt.DebtDate = customerDebtDto.DebtDate; 
            customerDebt.DueDate = customerDebtDto.DueDate; 
            customerDebt.DebtStatus = customerDebtDto.EnmDebtStatus; 
            customerDebt.Notes = customerDebtDto.Notes; 
            customerDebt.ProductTypes = customerDebtDto.ProductTypes; 
            customerDebt.DeliveryDate = customerDebtDto.DeliveryDate; 
 
            return customerDebt; 
        } 
 
        public static void PopulateFKDisplayNames(this CustomerDebtDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkCustomerId > 0) 
            { 
                try 
                { 
                    var pCustomer = new clsCustomer(); 
                    var fault = pCustomer.GetByID(dto.FkCustomerId, requester); 
                    if (fault.isOK) dto.CustomerDisplayName = pCustomer.CustomerName; 
                } 
                catch { } 
            } 
            if (dto.FkOrderHeaderId > 0) 
            { 
                try 
                { 
                    var pOrderHeader = new clsOrderHeader(); 
                    var fault = pOrderHeader.GetByID(dto.FkOrderHeaderId, requester); 
                    if (fault.isOK) dto.OrderHeaderDisplayName = pOrderHeader.InvoiceNumber; 
                } 
                catch { } 
            } 
        } 
    } 
} 
