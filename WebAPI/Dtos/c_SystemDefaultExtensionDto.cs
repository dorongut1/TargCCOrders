using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class SystemDefaultExtensions 
    { 
        public static SystemDefaultDto ToDto(this csSystemDefault systemDefault) 
        { 
            if (systemDefault is null) return null!; 
 
            var dto = new SystemDefaultDto 
            { 
                Id = systemDefault.ID, 
                Group = systemDefault.Group, 
                SettingName = systemDefault.SettingName, 
                SettingValue = systemDefault.SettingValue, 
                EnmSystemDefaultType = systemDefault.SystemDefaultType, 
                Description = systemDefault.Description
            }; 
            dto._etag = ComputeETag(systemDefault); 
            return dto; 
        } 
 
        public static string ComputeETag(csSystemDefault entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Group ?? ""); 
            sb.Append('|').Append(entity.SettingName ?? ""); 
            sb.Append('|').Append(entity.SettingValue ?? ""); 
            sb.Append('|').Append(entity.SystemDefaultType); 
            sb.Append('|').Append(entity.Description ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csSystemDefault FromDto(this SystemDefaultUpdateDto systemDefaultDto, clsRequester requester) 
        { 
            if (systemDefaultDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csSystemDefault systemDefault = new csSystemDefault(); 
            if (systemDefaultDto.Id > 0) 
            { 
                clsFault fault = systemDefault.GetByID(systemDefaultDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //systemDefault.ID = systemDefaultDto.Id; //not transferred on purpose ! 
            systemDefault.Group = systemDefaultDto.Group; 
            systemDefault.SettingName = systemDefaultDto.SettingName; 
            systemDefault.SystemDefaultType = systemDefaultDto.EnmSystemDefaultType; 
            systemDefault.Description = systemDefaultDto.Description; 
 
            return systemDefault; 
        } 
    } 
} 
