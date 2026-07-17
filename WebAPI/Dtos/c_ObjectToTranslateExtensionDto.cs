using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ObjectToTranslateExtensions 
    { 
        public static ObjectToTranslateDto ToDto(this csObjectToTranslate objectToTranslate) 
        { 
            if (objectToTranslate is null) return null!; 
 
            var dto = new ObjectToTranslateDto 
            { 
                Id = objectToTranslate.ID, 
                EnmObjectType = objectToTranslate.ObjectType, 
                Object = objectToTranslate.Object, 
                Item = objectToTranslate.Item
            }; 
            dto._etag = ComputeETag(objectToTranslate); 
            return dto; 
        } 
 
        public static string ComputeETag(csObjectToTranslate entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ObjectType); 
            sb.Append('|').Append(entity.Object ?? ""); 
            sb.Append('|').Append(entity.Item ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csObjectToTranslate FromDto(this ObjectToTranslateUpdateDto objectToTranslateDto, clsRequester requester) 
        { 
            if (objectToTranslateDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csObjectToTranslate objectToTranslate = new csObjectToTranslate(); 
            if (objectToTranslateDto.Id > 0) 
            { 
                clsFault fault = objectToTranslate.GetByID(objectToTranslateDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //objectToTranslate.ID = objectToTranslateDto.Id; //not transferred on purpose ! 
            objectToTranslate.ObjectType = objectToTranslateDto.EnmObjectType; 
            objectToTranslate.Object = objectToTranslateDto.Object; 
            objectToTranslate.Item = objectToTranslateDto.Item; 
 
            return objectToTranslate; 
        } 
    } 
} 
