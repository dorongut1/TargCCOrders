using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class AlertMessageExtensions 
    { 
        public static AlertMessageDto ToDto(this csAlertMessage alertMessage) 
        { 
            if (alertMessage is null) return null!; 
 
            var dto = new AlertMessageDto 
            { 
                Id = alertMessage.ID, 
                Number = alertMessage.Number, 
                Description = alertMessage.Description, 
                EnmType = alertMessage.Type, 
                EnmSeverity = alertMessage.Severity, 
                Message = alertMessage.MessageLocalized, 
                Action = alertMessage.ActionLocalized
            }; 
            dto._etag = ComputeETag(alertMessage); 
            return dto; 
        } 
 
        public static string ComputeETag(csAlertMessage entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Number); 
            sb.Append('|').Append(entity.Description ?? ""); 
            sb.Append('|').Append(entity.Type); 
            sb.Append('|').Append(entity.Severity); 
            sb.Append('|').Append(entity.Message ?? ""); 
            sb.Append('|').Append(entity.Action ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csAlertMessage FromDto(this AlertMessageUpdateDto alertMessageDto, clsRequester requester) 
        { 
            if (alertMessageDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csAlertMessage alertMessage = new csAlertMessage(); 
            if (alertMessageDto.Id > 0) 
            { 
                clsFault fault = alertMessage.GetByID(alertMessageDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //alertMessage.ID = alertMessageDto.Id; //not transferred on purpose ! 
            alertMessage.Number = alertMessageDto.Number; 
            alertMessage.Description = alertMessageDto.Description; 
            alertMessage.Type = alertMessageDto.EnmType; 
            alertMessage.Severity = alertMessageDto.EnmSeverity; 
            alertMessage.Message = alertMessageDto.Message; 
            alertMessage.Action = alertMessageDto.Action; 
 
            return alertMessage; 
        } 
    } 
} 
