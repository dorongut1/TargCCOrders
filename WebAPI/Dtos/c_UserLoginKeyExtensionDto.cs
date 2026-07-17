using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class UserLoginKeyExtensions 
    { 
        public static UserLoginKeyDto ToDto(this csUserLoginKey userLoginKey) 
        { 
            if (userLoginKey is null) return null!; 
 
            var dto = new UserLoginKeyDto 
            { 
                Id = userLoginKey.ID, 
                FkUserId = userLoginKey.UserID, 
                ApplicationName = userLoginKey.ApplicationName, 
                ApplicationIdentifier = userLoginKey.ApplicationIdentifier, 
                KeyHashed = userLoginKey.KeyHashed, 
                ExternalIpAtCreation = userLoginKey.ExternalIPAtCreation, 
                CountryAtCreation = userLoginKey.CountryAtCreation, 
                LastAccessTime = userLoginKey.LastAccessTime, 
                LoggedLoginId = userLoginKey.LoggedLoginID
            }; 
            dto._etag = ComputeETag(userLoginKey); 
            return dto; 
        } 
 
        public static string ComputeETag(csUserLoginKey entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.UserID); 
            sb.Append('|').Append(entity.ApplicationName ?? ""); 
            sb.Append('|').Append(entity.ApplicationIdentifier ?? ""); 
            sb.Append('|').Append(entity.KeyHashed ?? ""); 
            sb.Append('|').Append(entity.ExternalIPAtCreation ?? ""); 
            sb.Append('|').Append(entity.CountryAtCreation ?? ""); 
            sb.Append('|').Append(entity.LastAccessTime.Ticks); 
            sb.Append('|').Append(entity.LoggedLoginID); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csUserLoginKey FromDto(this UserLoginKeyUpdateDto userLoginKeyDto, clsRequester requester) 
        { 
            if (userLoginKeyDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csUserLoginKey userLoginKey = new csUserLoginKey(); 
            if (userLoginKeyDto.Id > 0) 
            { 
                clsFault fault = userLoginKey.GetByID(userLoginKeyDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //userLoginKey.ID = userLoginKeyDto.Id; //not transferred on purpose ! 
            userLoginKey.UserID = userLoginKeyDto.FkUserId; 
            userLoginKey.ApplicationName = userLoginKeyDto.ApplicationName; 
            userLoginKey.ApplicationIdentifier = userLoginKeyDto.ApplicationIdentifier; 
            userLoginKey.KeyHashed = userLoginKeyDto.KeyHashed; 
            userLoginKey.ExternalIPAtCreation = userLoginKeyDto.ExternalIpAtCreation; 
            userLoginKey.CountryAtCreation = userLoginKeyDto.CountryAtCreation; 
            userLoginKey.LastAccessTime = userLoginKeyDto.LastAccessTime; 
            userLoginKey.LoggedLoginID = userLoginKeyDto.LoggedLoginId; 
 
            return userLoginKey; 
        } 
 
        public static void PopulateFKDisplayNames(this UserLoginKeyDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkUserId > 0) 
            { 
                try 
                { 
                    var pUser = new csUser(); 
                    var fault = pUser.GetByID(dto.FkUserId, requester); 
                    if (fault.isOK) dto.UserDisplayName = pUser.UserName; 
                } 
                catch { } 
            } 
        } 
    } 
} 
