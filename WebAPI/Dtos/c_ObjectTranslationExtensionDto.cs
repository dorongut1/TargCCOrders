using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ObjectTranslationExtensions 
    { 
        public static ObjectTranslationDto ToDto(this csObjectTranslation objectTranslation) 
        { 
            if (objectTranslation is null) return null!; 
 
            var dto = new ObjectTranslationDto 
            { 
                Id = objectTranslation.ID, 
                FkObjectToTranslateId = objectTranslation.ObjectToTranslateID, 
                Instance = objectTranslation.Instance, 
                DefaultText = objectTranslation.DefaultText, 
                EnmLanguage = objectTranslation.Language, 
                Text = objectTranslation.Text, 
                InstanceUniqueText = objectTranslation.InstanceUniqueText
            }; 
            dto._etag = ComputeETag(objectTranslation); 
            return dto; 
        } 
 
        public static string ComputeETag(csObjectTranslation entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ObjectToTranslateID); 
            sb.Append('|').Append(entity.Instance); 
            sb.Append('|').Append(entity.DefaultText ?? ""); 
            sb.Append('|').Append(entity.Language); 
            sb.Append('|').Append(entity.Text ?? ""); 
            sb.Append('|').Append(entity.InstanceUniqueText ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csObjectTranslation FromDto(this ObjectTranslationUpdateDto objectTranslationDto, clsRequester requester) 
        { 
            if (objectTranslationDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csObjectTranslation objectTranslation = new csObjectTranslation(); 
            if (objectTranslationDto.Id > 0) 
            { 
                clsFault fault = objectTranslation.GetByID(objectTranslationDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //objectTranslation.ID = objectTranslationDto.Id; //not transferred on purpose ! 
            objectTranslation.ObjectToTranslateID = objectTranslationDto.FkObjectToTranslateId; 
            objectTranslation.Instance = objectTranslationDto.Instance; 
            objectTranslation.Language = objectTranslationDto.EnmLanguage; 
            objectTranslation.Text = objectTranslationDto.Text; 
            objectTranslation.InstanceUniqueText = objectTranslationDto.InstanceUniqueText; 
 
            return objectTranslation; 
        } 
 
        public static void PopulateFKDisplayNames(this ObjectTranslationDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkObjectToTranslateId > 0) 
            { 
                try 
                { 
                    var pObjectToTranslate = new csObjectToTranslate(); 
                    var fault = pObjectToTranslate.GetByID(dto.FkObjectToTranslateId, requester); 
                    if (fault.isOK) dto.ObjectToTranslateDisplayName = pObjectToTranslate.Object; 
                } 
                catch { } 
            } 
        } 
    } 
} 
