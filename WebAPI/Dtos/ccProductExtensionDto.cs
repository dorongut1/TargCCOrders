using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ProductExtensions 
    { 
        public static ProductDto ToDto(this clsProduct product) 
        { 
            if (product is null) return null!; 
 
            var dto = new ProductDto 
            { 
                Id = product.ID, 
                ProductCode = product.ProductCode, 
                ProductName = product.ProductName, 
                EnmCategory = product.Category, 
                UnitOfMeasure = product.UnitOfMeasure, 
                Notes = product.Notes, 
                IsActive = product.IsActive, 
                CurrentStock = product.CurrentStock, 
                BaseCost = product.BaseCost
            }; 
            dto._etag = ComputeETag(product); 
            return dto; 
        } 
 
        public static string ComputeETag(clsProduct entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ProductCode ?? ""); 
            sb.Append('|').Append(entity.ProductName ?? ""); 
            sb.Append('|').Append(entity.Category); 
            sb.Append('|').Append(entity.UnitOfMeasure ?? ""); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            sb.Append('|').Append(entity.IsActive); 
            sb.Append('|').Append(entity.CurrentStock); 
            sb.Append('|').Append(entity.BaseCost); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsProduct FromDto(this ProductUpdateDto productDto, clsRequester requester) 
        { 
            if (productDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsProduct product = new clsProduct(); 
            if (productDto.Id > 0) 
            { 
                clsFault fault = product.GetByID(productDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //product.ID = productDto.Id; //not transferred on purpose ! 
            product.ProductCode = productDto.ProductCode; 
            product.ProductName = productDto.ProductName; 
            product.Category = productDto.EnmCategory; 
            product.UnitOfMeasure = productDto.UnitOfMeasure; 
            product.Notes = productDto.Notes; 
            product.BaseCost = productDto.BaseCost; 
 
            return product; 
        } 
    } 
} 
