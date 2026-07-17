using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class OrderHeaderExtensions 
    { 
        public static OrderHeaderDto ToDto(this clsOrderHeader orderHeader) 
        { 
            if (orderHeader is null) return null!; 
 
            var dto = new OrderHeaderDto 
            { 
                Id = orderHeader.ID, 
                OrderNumber = orderHeader.OrderNumber, 
                FkCustomerId = orderHeader.CustomerID, 
                OrderDate = orderHeader.OrderDate, 
                TotalAmount = orderHeader.TotalAmount, 
                VatAmount = orderHeader.VATAmount, 
                TotalWithVat = orderHeader.TotalWithVAT, 
                EnmPaymentMethod = orderHeader.PaymentMethod, 
                EnmPaymentStatus = orderHeader.PaymentStatus, 
                PaymentDate = orderHeader.PaymentDate, 
                InvoiceNumber = orderHeader.InvoiceNumber, 
                EnmDeliveryMethod = orderHeader.DeliveryMethod, 
                DeliveryDate = orderHeader.DeliveryDate, 
                EnmDeliveryDay = orderHeader.DeliveryDay, 
                EnmOrderStatus = orderHeader.OrderStatus, 
                Notes = orderHeader.Notes, 
                Notes2 = orderHeader.Notes2, 
                OrderMonth = orderHeader.OrderMonth, 
                Quarter = orderHeader.Quarter
            }; 
            dto._etag = ComputeETag(orderHeader); 
            return dto; 
        } 
 
        public static string ComputeETag(clsOrderHeader entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.OrderNumber); 
            sb.Append('|').Append(entity.CustomerID); 
            sb.Append('|').Append(entity.OrderDate.Ticks); 
            sb.Append('|').Append(entity.TotalAmount); 
            sb.Append('|').Append(entity.VATAmount); 
            sb.Append('|').Append(entity.TotalWithVAT); 
            sb.Append('|').Append(entity.PaymentMethod); 
            sb.Append('|').Append(entity.PaymentStatus); 
            sb.Append('|').Append(entity.PaymentDate.Ticks); 
            sb.Append('|').Append(entity.InvoiceNumber ?? ""); 
            sb.Append('|').Append(entity.DeliveryMethod); 
            sb.Append('|').Append(entity.DeliveryDate.Ticks); 
            sb.Append('|').Append(entity.DeliveryDay); 
            sb.Append('|').Append(entity.OrderStatus); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            sb.Append('|').Append(entity.Notes2 ?? ""); 
            sb.Append('|').Append(entity.OrderMonth ?? ""); 
            sb.Append('|').Append(entity.Quarter ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsOrderHeader FromDto(this OrderHeaderUpdateDto orderHeaderDto, clsRequester requester) 
        { 
            if (orderHeaderDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsOrderHeader orderHeader = new clsOrderHeader(); 
            if (orderHeaderDto.Id > 0) 
            { 
                clsFault fault = orderHeader.GetByID(orderHeaderDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //orderHeader.ID = orderHeaderDto.Id; //not transferred on purpose ! 
            orderHeader.OrderNumber = orderHeaderDto.OrderNumber; 
            orderHeader.CustomerID = orderHeaderDto.FkCustomerId; 
            orderHeader.OrderDate = orderHeaderDto.OrderDate; 
            orderHeader.PaymentMethod = orderHeaderDto.EnmPaymentMethod; 
            orderHeader.PaymentStatus = orderHeaderDto.EnmPaymentStatus; 
            orderHeader.PaymentDate = orderHeaderDto.PaymentDate; 
            orderHeader.InvoiceNumber = orderHeaderDto.InvoiceNumber; 
            orderHeader.DeliveryMethod = orderHeaderDto.EnmDeliveryMethod; 
            orderHeader.DeliveryDate = orderHeaderDto.DeliveryDate; 
            orderHeader.DeliveryDay = orderHeaderDto.EnmDeliveryDay; 
            orderHeader.OrderStatus = orderHeaderDto.EnmOrderStatus; 
            orderHeader.Notes = orderHeaderDto.Notes; 
            orderHeader.Notes2 = orderHeaderDto.Notes2; 
 
            return orderHeader; 
        } 
 
        public static void PopulateFKDisplayNames(this OrderHeaderDto dto, clsRequester requester) 
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
        } 
    } 
} 
