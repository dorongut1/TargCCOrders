using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class OrderLineExtensions 
    { 
        public static OrderLineDto ToDto(this clsOrderLine orderLine) 
        { 
            if (orderLine is null) return null!; 
 
            var dto = new OrderLineDto 
            { 
                Id = orderLine.ID, 
                FkOrderHeaderId = orderLine.OrderHeaderID, 
                FkProductId = orderLine.ProductID, 
                Quantity = orderLine.Quantity, 
                UnitPrice = orderLine.UnitPrice, 
                DiscountPercent = orderLine.DiscountPercent, 
                UnitCost = orderLine.UnitCost, 
                LineNumber = orderLine.LineNumber, 
                LineTotal = orderLine.LineTotal, 
                TotalCost = orderLine.TotalCost, 
                Profit = orderLine.Profit
            }; 
            dto._etag = ComputeETag(orderLine); 
            return dto; 
        } 
 
        public static string ComputeETag(clsOrderLine entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.OrderHeaderID); 
            sb.Append('|').Append(entity.ProductID); 
            sb.Append('|').Append(entity.Quantity); 
            sb.Append('|').Append(entity.UnitPrice); 
            sb.Append('|').Append(entity.DiscountPercent); 
            sb.Append('|').Append(entity.UnitCost); 
            sb.Append('|').Append(entity.LineNumber); 
            sb.Append('|').Append(entity.LineTotal); 
            sb.Append('|').Append(entity.TotalCost); 
            sb.Append('|').Append(entity.Profit); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsOrderLine FromDto(this OrderLineUpdateDto orderLineDto, clsRequester requester) 
        { 
            if (orderLineDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsOrderLine orderLine = new clsOrderLine(); 
            if (orderLineDto.Id > 0) 
            { 
                clsFault fault = orderLine.GetByID(orderLineDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //orderLine.ID = orderLineDto.Id; //not transferred on purpose ! 
            orderLine.OrderHeaderID = orderLineDto.FkOrderHeaderId; 
            orderLine.ProductID = orderLineDto.FkProductId; 
            orderLine.Quantity = orderLineDto.Quantity; 
            orderLine.UnitPrice = orderLineDto.UnitPrice; 
            orderLine.DiscountPercent = orderLineDto.DiscountPercent; 
            orderLine.LineNumber = orderLineDto.LineNumber; 
 
            return orderLine; 
        } 
 
        public static void PopulateFKDisplayNames(this OrderLineDto dto, clsRequester requester) 
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
            if (dto.FkProductId > 0) 
            { 
                try 
                { 
                    var pProduct = new clsProduct(); 
                    var fault = pProduct.GetByID(dto.FkProductId, requester); 
                    if (fault.isOK) dto.ProductDisplayName = pProduct.ProductName; 
                } 
                catch { } 
            } 
        } 
    } 
} 
