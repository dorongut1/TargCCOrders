using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class UserStatusExtensions 
    { 
        public static UserStatusDto ToDto(this csUserStatus userStatus) 
        { 
            if (userStatus is null) return null!; 
 
            var dto = new UserStatusDto 
            { 
                Id = userStatus.ID, 
                FkUserId = userStatus.UserID, 
                ApplicationName = userStatus.ApplicationName, 
                LastLoggedLoginId = userStatus.LastLoggedLoginID, 
                LoginTime = userStatus.LoginTime, 
                LogoutTime = userStatus.LogoutTime
            }; 
            dto._etag = ComputeETag(userStatus); 
            return dto; 
        } 
 
        public static string ComputeETag(csUserStatus entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.UserID); 
            sb.Append('|').Append(entity.ApplicationName ?? ""); 
            sb.Append('|').Append(entity.LastLoggedLoginID); 
            sb.Append('|').Append(entity.LoginTime.Ticks); 
            sb.Append('|').Append(entity.LogoutTime.Ticks); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csUserStatus FromDto(this UserStatusUpdateDto userStatusDto, clsRequester requester) 
        { 
            if (userStatusDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csUserStatus userStatus = new csUserStatus(); 
            if (userStatusDto.Id > 0) 
            { 
                clsFault fault = userStatus.GetByID(userStatusDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //userStatus.ID = userStatusDto.Id; //not transferred on purpose ! 
            userStatus.UserID = userStatusDto.FkUserId; 
            userStatus.ApplicationName = userStatusDto.ApplicationName; 
            userStatus.LastLoggedLoginID = userStatusDto.LastLoggedLoginId; 
            userStatus.LoginTime = userStatusDto.LoginTime; 
            userStatus.LogoutTime = userStatusDto.LogoutTime; 
 
            return userStatus; 
        } 
 
        public static void PopulateFKDisplayNames(this UserStatusDto dto, clsRequester requester) 
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
