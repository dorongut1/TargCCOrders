using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ProductPriceHistExtensions 
    { 
        public static ProductPriceHistDto ToDto(this clsProductPriceHist productPriceHist) 
        { 
            if (productPriceHist is null) return null!; 
 
            var dto = new ProductPriceHistDto 
            { 
                Id = productPriceHist.ID, 
                ProductId = productPriceHist.ProductID, 
                EnmCustomerType = productPriceHist.CustomerType, 
                BaseCost = productPriceHist.BaseCost, 
                SellingPrice = productPriceHist.SellingPrice, 
                MinQuantity = productPriceHist.MinQuantity, 
                DiscountPercent = productPriceHist.DiscountPercent, 
                ValidFrom = productPriceHist.ValidFrom, 
                ValidTo = productPriceHist.ValidTo, 
                ArchivedDate = productPriceHist.ArchivedDate, 
                ArchivedReason = productPriceHist.ArchivedReason, 
                OriginalPriceId = productPriceHist.OriginalPriceID, 
                Notes = productPriceHist.Notes, 
                AddFieldsHere = productPriceHist.AddFieldsHere
            }; 
            dto._etag = ComputeETag(productPriceHist); 
            return dto; 
        } 
 
        public static string ComputeETag(clsProductPriceHist entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ProductID); 
            sb.Append('|').Append(entity.CustomerType); 
            sb.Append('|').Append(entity.BaseCost); 
            sb.Append('|').Append(entity.SellingPrice); 
            sb.Append('|').Append(entity.MinQuantity); 
            sb.Append('|').Append(entity.DiscountPercent); 
            sb.Append('|').Append(entity.ValidFrom.Ticks); 
            sb.Append('|').Append(entity.ValidTo.Ticks); 
            sb.Append('|').Append(entity.ArchivedDate.Ticks); 
            sb.Append('|').Append(entity.ArchivedReason ?? ""); 
            sb.Append('|').Append(entity.OriginalPriceID); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            sb.Append('|').Append(entity.AddFieldsHere ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsProductPriceHist FromDto(this ProductPriceHistUpdateDto productPriceHistDto, clsRequester requester) 
        { 
            if (productPriceHistDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsProductPriceHist productPriceHist = new clsProductPriceHist(); 
            if (productPriceHistDto.Id > 0) 
            { 
                clsFault fault = productPriceHist.GetByID(productPriceHistDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //productPriceHist.ID = productPriceHistDto.Id; //not transferred on purpose ! 
            productPriceHist.ProductID = productPriceHistDto.ProductId; 
            productPriceHist.CustomerType = productPriceHistDto.EnmCustomerType; 
            productPriceHist.BaseCost = productPriceHistDto.BaseCost; 
            productPriceHist.SellingPrice = productPriceHistDto.SellingPrice; 
            productPriceHist.MinQuantity = productPriceHistDto.MinQuantity; 
            productPriceHist.DiscountPercent = productPriceHistDto.DiscountPercent; 
            productPriceHist.ValidFrom = productPriceHistDto.ValidFrom; 
            productPriceHist.ValidTo = productPriceHistDto.ValidTo; 
            productPriceHist.ArchivedDate = productPriceHistDto.ArchivedDate; 
            productPriceHist.ArchivedReason = productPriceHistDto.ArchivedReason; 
            productPriceHist.OriginalPriceID = productPriceHistDto.OriginalPriceId; 
            productPriceHist.Notes = productPriceHistDto.Notes; 
            productPriceHist.AddFieldsHere = productPriceHistDto.AddFieldsHere; 
 
            return productPriceHist; 
        } 
    } 
} 
