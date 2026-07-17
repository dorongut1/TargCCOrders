using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LoggedRequestExtensions 
    { 
        public static LoggedRequestDto ToDto(this csLoggedRequest loggedRequest) 
        { 
            if (loggedRequest is null) return null!; 
 
            var dto = new LoggedRequestDto 
            { 
                Id = loggedRequest.ID, 
                FkLoggedLoginId = loggedRequest.LoggedLoginID, 
                TimeAccessed = loggedRequest.TimeAccessed, 
                FkUserId = loggedRequest.UserID, 
                CallingFunctionWithinApplication = loggedRequest.CallingFunctionWithinApplication, 
                EntryPoint = loggedRequest.EntryPoint, 
                Process = loggedRequest.Process, 
                Thread = loggedRequest.Thread
            }; 
            dto._etag = ComputeETag(loggedRequest); 
            return dto; 
        } 
 
        public static string ComputeETag(csLoggedRequest entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.LoggedLoginID); 
            sb.Append('|').Append(entity.TimeAccessed.Ticks); 
            sb.Append('|').Append(entity.UserID); 
            sb.Append('|').Append(entity.CallingFunctionWithinApplication ?? ""); 
            sb.Append('|').Append(entity.EntryPoint ?? ""); 
            sb.Append('|').Append(entity.Process ?? ""); 
            sb.Append('|').Append(entity.Thread ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLoggedRequest FromDto(this LoggedRequestUpdateDto loggedRequestDto, clsRequester requester) 
        { 
            if (loggedRequestDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLoggedRequest loggedRequest = new csLoggedRequest(); 
            if (loggedRequestDto.Id > 0) 
            { 
                clsFault fault = loggedRequest.GetByID(loggedRequestDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //loggedRequest.ID = loggedRequestDto.Id; //not transferred on purpose ! 
            loggedRequest.LoggedLoginID = loggedRequestDto.FkLoggedLoginId; 
            loggedRequest.TimeAccessed = loggedRequestDto.TimeAccessed; 
            loggedRequest.UserID = loggedRequestDto.FkUserId; 
            loggedRequest.CallingFunctionWithinApplication = loggedRequestDto.CallingFunctionWithinApplication; 
            loggedRequest.EntryPoint = loggedRequestDto.EntryPoint; 
            loggedRequest.Process = loggedRequestDto.Process; 
            loggedRequest.Thread = loggedRequestDto.Thread; 
 
            return loggedRequest; 
        } 
 
        public static void PopulateFKDisplayNames(this LoggedRequestDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkLoggedLoginId > 0) 
            { 
                try 
                { 
                    var pLoggedLogin = new csLoggedLogin(); 
                    var fault = pLoggedLogin.GetByID(dto.FkLoggedLoginId, requester); 
                    if (fault.isOK) dto.LoggedLoginDisplayName = pLoggedLogin.UserName; 
                } 
                catch { } 
            } 
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
