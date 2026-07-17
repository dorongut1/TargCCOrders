using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class DeliveryExtensions 
    { 
        public static DeliveryDto ToDto(this clsDelivery delivery) 
        { 
            if (delivery is null) return null!; 
 
            var dto = new DeliveryDto 
            { 
                Id = delivery.ID, 
                FkOrderHeaderId = delivery.OrderHeaderID, 
                DeliveryAddress = delivery.DeliveryAddress, 
                ContactPhone = delivery.ContactPhone, 
                ContactName = delivery.ContactName, 
                EnmDeliveryMethod = delivery.DeliveryMethod, 
                OrderedDate = delivery.OrderedDate, 
                ReceivedDate = delivery.ReceivedDate, 
                ArrivalToHubDate = delivery.ArrivalToHubDate, 
                ArrivalToCustomerDate = delivery.ArrivalToCustomerDate, 
                EnmDeliveryStatus = delivery.DeliveryStatus, 
                Location = delivery.Location, 
                ProductsSummary = delivery.ProductsSummary, 
                Notes = delivery.Notes
            }; 
            dto._etag = ComputeETag(delivery); 
            return dto; 
        } 
 
        public static string ComputeETag(clsDelivery entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.OrderHeaderID); 
            sb.Append('|').Append(entity.DeliveryAddress ?? ""); 
            sb.Append('|').Append(entity.ContactPhone ?? ""); 
            sb.Append('|').Append(entity.ContactName ?? ""); 
            sb.Append('|').Append(entity.DeliveryMethod); 
            sb.Append('|').Append(entity.OrderedDate.Ticks); 
            sb.Append('|').Append(entity.ReceivedDate.Ticks); 
            sb.Append('|').Append(entity.ArrivalToHubDate.Ticks); 
            sb.Append('|').Append(entity.ArrivalToCustomerDate.Ticks); 
            sb.Append('|').Append(entity.DeliveryStatus); 
            sb.Append('|').Append(entity.Location ?? ""); 
            sb.Append('|').Append(entity.ProductsSummary ?? ""); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsDelivery FromDto(this DeliveryUpdateDto deliveryDto, clsRequester requester) 
        { 
            if (deliveryDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsDelivery delivery = new clsDelivery(); 
            if (deliveryDto.Id > 0) 
            { 
                clsFault fault = delivery.GetByID(deliveryDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //delivery.ID = deliveryDto.Id; //not transferred on purpose ! 
            delivery.OrderHeaderID = deliveryDto.FkOrderHeaderId; 
            delivery.DeliveryAddress = deliveryDto.DeliveryAddress; 
            delivery.ContactPhone = deliveryDto.ContactPhone; 
            delivery.ContactName = deliveryDto.ContactName; 
            delivery.DeliveryMethod = deliveryDto.EnmDeliveryMethod; 
            delivery.OrderedDate = deliveryDto.OrderedDate; 
            delivery.ReceivedDate = deliveryDto.ReceivedDate; 
            delivery.ArrivalToHubDate = deliveryDto.ArrivalToHubDate; 
            delivery.ArrivalToCustomerDate = deliveryDto.ArrivalToCustomerDate; 
            delivery.DeliveryStatus = deliveryDto.EnmDeliveryStatus; 
            delivery.Location = deliveryDto.Location; 
            delivery.Notes = deliveryDto.Notes; 
 
            return delivery; 
        } 
 
        public static void PopulateFKDisplayNames(this DeliveryDto dto, clsRequester requester) 
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
