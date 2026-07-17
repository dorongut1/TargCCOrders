using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class SupplierOrderExtensions 
    { 
        public static SupplierOrderDto ToDto(this clsSupplierOrder supplierOrder) 
        { 
            if (supplierOrder is null) return null!; 
 
            var dto = new SupplierOrderDto 
            { 
                Id = supplierOrder.ID, 
                FkOrderHeaderId = supplierOrder.OrderHeaderID, 
                SupplierEmail = supplierOrder.SupplierEmail, 
                EmailSubject = supplierOrder.EmailSubject, 
                EmailBody = supplierOrder.EmailBody, 
                EnmEmailStatus = supplierOrder.EmailStatus, 
                SentDate = supplierOrder.SentDate, 
                TotalCost = supplierOrder.TotalCost, 
                EnmDeliveryMethod = supplierOrder.DeliveryMethod, 
                RequestedDeliveryDate = supplierOrder.RequestedDeliveryDate, 
                RequestedDeliveryDay = supplierOrder.RequestedDeliveryDay, 
                Notes = supplierOrder.Notes
            }; 
            dto._etag = ComputeETag(supplierOrder); 
            return dto; 
        } 
 
        public static string ComputeETag(clsSupplierOrder entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.OrderHeaderID); 
            sb.Append('|').Append(entity.SupplierEmail ?? ""); 
            sb.Append('|').Append(entity.EmailSubject ?? ""); 
            sb.Append('|').Append(entity.EmailBody ?? ""); 
            sb.Append('|').Append(entity.EmailStatus); 
            sb.Append('|').Append(entity.SentDate.Ticks); 
            sb.Append('|').Append(entity.TotalCost); 
            sb.Append('|').Append(entity.DeliveryMethod); 
            sb.Append('|').Append(entity.RequestedDeliveryDate.Ticks); 
            sb.Append('|').Append(entity.RequestedDeliveryDay ?? ""); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsSupplierOrder FromDto(this SupplierOrderUpdateDto supplierOrderDto, clsRequester requester) 
        { 
            if (supplierOrderDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsSupplierOrder supplierOrder = new clsSupplierOrder(); 
            if (supplierOrderDto.Id > 0) 
            { 
                clsFault fault = supplierOrder.GetByID(supplierOrderDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //supplierOrder.ID = supplierOrderDto.Id; //not transferred on purpose ! 
            supplierOrder.OrderHeaderID = supplierOrderDto.FkOrderHeaderId; 
            supplierOrder.SupplierEmail = supplierOrderDto.SupplierEmail; 
            supplierOrder.EmailSubject = supplierOrderDto.EmailSubject; 
            supplierOrder.EmailBody = supplierOrderDto.EmailBody; 
            supplierOrder.EmailStatus = supplierOrderDto.EnmEmailStatus; 
            supplierOrder.SentDate = supplierOrderDto.SentDate; 
            supplierOrder.DeliveryMethod = supplierOrderDto.EnmDeliveryMethod; 
            supplierOrder.RequestedDeliveryDate = supplierOrderDto.RequestedDeliveryDate; 
            supplierOrder.RequestedDeliveryDay = supplierOrderDto.RequestedDeliveryDay; 
            supplierOrder.Notes = supplierOrderDto.Notes; 
 
            return supplierOrder; 
        } 
 
        public static void PopulateFKDisplayNames(this SupplierOrderDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
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
