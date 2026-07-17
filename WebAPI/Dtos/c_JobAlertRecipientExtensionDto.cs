using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class JobAlertRecipientExtensions 
    { 
        public static JobAlertRecipientDto ToDto(this csJobAlertRecipient jobAlertRecipient) 
        { 
            if (jobAlertRecipient is null) return null!; 
 
            var dto = new JobAlertRecipientDto 
            { 
                Id = jobAlertRecipient.ID, 
                FkJobId = jobAlertRecipient.JobID, 
                FkUserId = jobAlertRecipient.UserID, 
                EnmJobAlertType = jobAlertRecipient.JobAlertType, 
                OverrideName = jobAlertRecipient.OverrideName, 
                OverrideEmailOrPhone = jobAlertRecipient.OverrideEmailOrPhone
            }; 
            dto._etag = ComputeETag(jobAlertRecipient); 
            return dto; 
        } 
 
        public static string ComputeETag(csJobAlertRecipient entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.JobID); 
            sb.Append('|').Append(entity.UserID); 
            sb.Append('|').Append(entity.JobAlertType); 
            sb.Append('|').Append(entity.OverrideName ?? ""); 
            sb.Append('|').Append(entity.OverrideEmailOrPhone ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csJobAlertRecipient FromDto(this JobAlertRecipientUpdateDto jobAlertRecipientDto, clsRequester requester) 
        { 
            if (jobAlertRecipientDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csJobAlertRecipient jobAlertRecipient = new csJobAlertRecipient(); 
            if (jobAlertRecipientDto.Id > 0) 
            { 
                clsFault fault = jobAlertRecipient.GetByID(jobAlertRecipientDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //jobAlertRecipient.ID = jobAlertRecipientDto.Id; //not transferred on purpose ! 
            jobAlertRecipient.JobID = jobAlertRecipientDto.FkJobId; 
            jobAlertRecipient.UserID = jobAlertRecipientDto.FkUserId; 
            jobAlertRecipient.JobAlertType = jobAlertRecipientDto.EnmJobAlertType; 
            jobAlertRecipient.OverrideName = jobAlertRecipientDto.OverrideName; 
            jobAlertRecipient.OverrideEmailOrPhone = jobAlertRecipientDto.OverrideEmailOrPhone; 
 
            return jobAlertRecipient; 
        } 
 
        public static void PopulateFKDisplayNames(this JobAlertRecipientDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkJobId > 0) 
            { 
                try 
                { 
                    var pJob = new csJob(); 
                    var fault = pJob.GetByID(dto.FkJobId, requester); 
                    if (fault.isOK) dto.JobDisplayName = pJob.Description; 
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
