using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class UserPermissionExtensions 
    { 
        public static UserPermissionDto ToDto(this csUserPermission userPermission) 
        { 
            if (userPermission is null) return null!; 
 
            var dto = new UserPermissionDto 
            { 
                Id = userPermission.ID, 
                FkUserId = userPermission.UserID, 
                ApplicationName = userPermission.ApplicationName, 
                ComputerIdentifier = userPermission.ComputerIdentifier, 
                ComputerName = userPermission.ComputerName, 
                ExternalIp = userPermission.ExternalIP, 
                HasPermission = userPermission.HasPermission, 
                Comments = userPermission.Comments, 
                LastAccessTime = userPermission.LastAccessTime, 
                LoggedLoginId = userPermission.LoggedLoginID
            }; 
            dto._etag = ComputeETag(userPermission); 
            return dto; 
        } 
 
        public static string ComputeETag(csUserPermission entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.UserID); 
            sb.Append('|').Append(entity.ApplicationName ?? ""); 
            sb.Append('|').Append(entity.ComputerIdentifier ?? ""); 
            sb.Append('|').Append(entity.ComputerName ?? ""); 
            sb.Append('|').Append(entity.ExternalIP ?? ""); 
            sb.Append('|').Append(entity.HasPermission); 
            sb.Append('|').Append(entity.Comments ?? ""); 
            sb.Append('|').Append(entity.LastAccessTime.Ticks); 
            sb.Append('|').Append(entity.LoggedLoginID); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csUserPermission FromDto(this UserPermissionUpdateDto userPermissionDto, clsRequester requester) 
        { 
            if (userPermissionDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csUserPermission userPermission = new csUserPermission(); 
            if (userPermissionDto.Id > 0) 
            { 
                clsFault fault = userPermission.GetByID(userPermissionDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //userPermission.ID = userPermissionDto.Id; //not transferred on purpose ! 
            userPermission.UserID = userPermissionDto.FkUserId; 
            userPermission.ApplicationName = userPermissionDto.ApplicationName; 
            userPermission.ComputerIdentifier = userPermissionDto.ComputerIdentifier; 
            userPermission.ComputerName = userPermissionDto.ComputerName; 
            userPermission.ExternalIP = userPermissionDto.ExternalIp; 
            userPermission.HasPermission = userPermissionDto.HasPermission; 
            userPermission.Comments = userPermissionDto.Comments; 
            userPermission.LastAccessTime = userPermissionDto.LastAccessTime; 
            userPermission.LoggedLoginID = userPermissionDto.LoggedLoginId; 
 
            return userPermission; 
        } 
 
        public static void PopulateFKDisplayNames(this UserPermissionDto dto, clsRequester requester) 
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
