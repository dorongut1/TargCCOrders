using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LoggedAlertExtensions 
    { 
        public static LoggedAlertDto ToDto(this csLoggedAlert loggedAlert) 
        { 
            if (loggedAlert is null) return null!; 
 
            var dto = new LoggedAlertDto 
            { 
                Id = loggedAlert.ID, 
                TimeOccurred = loggedAlert.TimeOccurred, 
                FaultNumber = loggedAlert.FaultNumber, 
                SystemName = loggedAlert.SystemName, 
                CallingApplication = loggedAlert.CallingApplication, 
                FkAffectedUserId = loggedAlert.AffectedUserID, 
                CallingApplicationVersion = loggedAlert.CallingApplicationVersion, 
                CallingFunctionWithinApplication = loggedAlert.CallingFunctionWithinApplication, 
                FreeText = loggedAlert.FreeText, 
                FaultingAssembly = loggedAlert.FaultingAssembly, 
                AssemblyEntryPoint = loggedAlert.AssemblyEntryPoint, 
                FaultingClass = loggedAlert.FaultingClass, 
                FaultingFunction = loggedAlert.FaultingFunction, 
                FaultingFunctionParameters = loggedAlert.FaultingFunctionParameters, 
                FaultIdent = loggedAlert.FaultIdent, 
                FaultDescription = loggedAlert.FaultDescription, 
                MessageSentToUser = loggedAlert.MessageSentToUser, 
                ActionSentToUser = loggedAlert.ActionSentToUser, 
                EnmFaultType = loggedAlert.FaultType, 
                EnmFaultSeverity = loggedAlert.FaultSeverity, 
                FkLoggedLoginId = loggedAlert.LoggedLoginID, 
                Thread = loggedAlert.Thread, 
                LkpUserIdentityTypeCode = loggedAlert.UserIdentityTypeCode, 
                LkpUserIdentityTypeNameCode = loggedAlert.UserIdentityTypeNameCode, 
                DateOccurred = loggedAlert.DateOccurred, 
                MonthOccurred = loggedAlert.MonthOccurred
            }; 
            dto._etag = ComputeETag(loggedAlert); 
            return dto; 
        } 
 
        public static string ComputeETag(csLoggedAlert entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.TimeOccurred.Ticks); 
            sb.Append('|').Append(entity.FaultNumber); 
            sb.Append('|').Append(entity.SystemName ?? ""); 
            sb.Append('|').Append(entity.CallingApplication ?? ""); 
            sb.Append('|').Append(entity.AffectedUserID); 
            sb.Append('|').Append(entity.CallingApplicationVersion ?? ""); 
            sb.Append('|').Append(entity.CallingFunctionWithinApplication ?? ""); 
            sb.Append('|').Append(entity.FreeText ?? ""); 
            sb.Append('|').Append(entity.FaultingAssembly ?? ""); 
            sb.Append('|').Append(entity.AssemblyEntryPoint ?? ""); 
            sb.Append('|').Append(entity.FaultingClass ?? ""); 
            sb.Append('|').Append(entity.FaultingFunction ?? ""); 
            sb.Append('|').Append(entity.FaultingFunctionParameters ?? ""); 
            sb.Append('|').Append(entity.FaultIdent ?? ""); 
            sb.Append('|').Append(entity.FaultDescription ?? ""); 
            sb.Append('|').Append(entity.MessageSentToUser ?? ""); 
            sb.Append('|').Append(entity.ActionSentToUser ?? ""); 
            sb.Append('|').Append(entity.FaultType); 
            sb.Append('|').Append(entity.FaultSeverity); 
            sb.Append('|').Append(entity.LoggedLoginID); 
            sb.Append('|').Append(entity.Thread ?? ""); 
            sb.Append('|').Append(entity.UserIdentityTypeCode ?? ""); 
            sb.Append('|').Append(entity.UserIdentityTypeNameCode); 
            sb.Append('|').Append(entity.DateOccurred.Ticks); 
            sb.Append('|').Append(entity.MonthOccurred.Ticks); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLoggedAlert FromDto(this LoggedAlertUpdateDto loggedAlertDto, clsRequester requester) 
        { 
            if (loggedAlertDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLoggedAlert loggedAlert = new csLoggedAlert(); 
            if (loggedAlertDto.Id > 0) 
            { 
                clsFault fault = loggedAlert.GetByID(loggedAlertDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //loggedAlert.ID = loggedAlertDto.Id; //not transferred on purpose ! 
            loggedAlert.TimeOccurred = loggedAlertDto.TimeOccurred; 
            loggedAlert.FaultNumber = loggedAlertDto.FaultNumber; 
            loggedAlert.SystemName = loggedAlertDto.SystemName; 
            loggedAlert.CallingApplication = loggedAlertDto.CallingApplication; 
            loggedAlert.AffectedUserID = loggedAlertDto.FkAffectedUserId; 
            loggedAlert.CallingApplicationVersion = loggedAlertDto.CallingApplicationVersion; 
            loggedAlert.CallingFunctionWithinApplication = loggedAlertDto.CallingFunctionWithinApplication; 
            loggedAlert.FreeText = loggedAlertDto.FreeText; 
            loggedAlert.FaultingAssembly = loggedAlertDto.FaultingAssembly; 
            loggedAlert.AssemblyEntryPoint = loggedAlertDto.AssemblyEntryPoint; 
            loggedAlert.FaultingClass = loggedAlertDto.FaultingClass; 
            loggedAlert.FaultingFunction = loggedAlertDto.FaultingFunction; 
            loggedAlert.FaultingFunctionParameters = loggedAlertDto.FaultingFunctionParameters; 
            loggedAlert.FaultIdent = loggedAlertDto.FaultIdent; 
            loggedAlert.FaultDescription = loggedAlertDto.FaultDescription; 
            loggedAlert.MessageSentToUser = loggedAlertDto.MessageSentToUser; 
            loggedAlert.ActionSentToUser = loggedAlertDto.ActionSentToUser; 
            loggedAlert.FaultType = loggedAlertDto.EnmFaultType; 
            loggedAlert.FaultSeverity = loggedAlertDto.EnmFaultSeverity; 
            loggedAlert.LoggedLoginID = loggedAlertDto.FkLoggedLoginId; 
            loggedAlert.Thread = loggedAlertDto.Thread; 
            loggedAlert.UserIdentityTypeCode = loggedAlertDto.LkpUserIdentityTypeCode; 
            loggedAlert.UserIdentityTypeNameCode = loggedAlertDto.LkpUserIdentityTypeNameCode; 
 
            return loggedAlert; 
        } 
 
        public static void PopulateFKDisplayNames(this LoggedAlertDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkAffectedUserId > 0) 
            { 
                try 
                { 
                    var pUser = new csUser(); 
                    var fault = pUser.GetByID(dto.FkAffectedUserId, requester); 
                    if (fault.isOK) dto.AffectedUserDisplayName = pUser.UserName; 
                } 
                catch { } 
            } 
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
        } 
    } 
} 
