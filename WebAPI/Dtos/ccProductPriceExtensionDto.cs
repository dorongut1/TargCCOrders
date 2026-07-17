using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ProductPriceExtensions 
    { 
        public static ProductPriceDto ToDto(this clsProductPrice productPrice) 
        { 
            if (productPrice is null) return null!; 
 
            var dto = new ProductPriceDto 
            { 
                Id = productPrice.ID, 
                FkProductId = productPrice.ProductID, 
                EnmCustomerType = productPrice.CustomerType, 
                SellingPrice = productPrice.SellingPrice, 
                MinQuantity = productPrice.MinQuantity, 
                DiscountPercent = productPrice.DiscountPercent, 
                Notes = productPrice.Notes
            }; 
            dto._etag = ComputeETag(productPrice); 
            return dto; 
        } 
 
        public static string ComputeETag(clsProductPrice entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ProductID); 
            sb.Append('|').Append(entity.CustomerType); 
            sb.Append('|').Append(entity.SellingPrice); 
            sb.Append('|').Append(entity.MinQuantity); 
            sb.Append('|').Append(entity.DiscountPercent); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsProductPrice FromDto(this ProductPriceUpdateDto productPriceDto, clsRequester requester) 
        { 
            if (productPriceDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsProductPrice productPrice = new clsProductPrice(); 
            if (productPriceDto.Id > 0) 
            { 
                clsFault fault = productPrice.GetByID(productPriceDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //productPrice.ID = productPriceDto.Id; //not transferred on purpose ! 
            productPrice.ProductID = productPriceDto.FkProductId; 
            productPrice.CustomerType = productPriceDto.EnmCustomerType; 
            productPrice.SellingPrice = productPriceDto.SellingPrice; 
            productPrice.MinQuantity = productPriceDto.MinQuantity; 
            productPrice.DiscountPercent = productPriceDto.DiscountPercent; 
            productPrice.Notes = productPriceDto.Notes; 
 
            return productPrice; 
        } 
 
        public static void PopulateFKDisplayNames(this ProductPriceDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
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
