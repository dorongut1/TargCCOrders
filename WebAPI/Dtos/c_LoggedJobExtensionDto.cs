using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LoggedJobExtensions 
    { 
        public static LoggedJobDto ToDto(this csLoggedJob loggedJob) 
        { 
            if (loggedJob is null) return null!; 
 
            var dto = new LoggedJobDto 
            { 
                Id = loggedJob.ID, 
                FkJobId = loggedJob.JobID, 
                WhenStarted = loggedJob.WhenStarted, 
                ActivatingUser = loggedJob.ActivatingUser, 
                LastRunBy = loggedJob.LastRunBy, 
                ExecutionTimeSec = loggedJob.ExecutionTimeSec, 
                EnmRunStatus = loggedJob.RunStatus, 
                Remarks = loggedJob.Remarks, 
                FkLoggedAlertId = loggedJob.LoggedAlertID, 
                SuccessCount = loggedJob.SuccessCount, 
                FailureCount = loggedJob.FailureCount
            }; 
            dto._etag = ComputeETag(loggedJob); 
            return dto; 
        } 
 
        public static string ComputeETag(csLoggedJob entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.JobID); 
            sb.Append('|').Append(entity.WhenStarted.Ticks); 
            sb.Append('|').Append(entity.ActivatingUser ?? ""); 
            sb.Append('|').Append(entity.LastRunBy ?? ""); 
            sb.Append('|').Append(entity.ExecutionTimeSec); 
            sb.Append('|').Append(entity.RunStatus); 
            sb.Append('|').Append(entity.Remarks ?? ""); 
            sb.Append('|').Append(entity.LoggedAlertID); 
            sb.Append('|').Append(entity.SuccessCount); 
            sb.Append('|').Append(entity.FailureCount); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLoggedJob FromDto(this LoggedJobUpdateDto loggedJobDto, clsRequester requester) 
        { 
            if (loggedJobDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLoggedJob loggedJob = new csLoggedJob(); 
            if (loggedJobDto.Id > 0) 
            { 
                clsFault fault = loggedJob.GetByID(loggedJobDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //loggedJob.ID = loggedJobDto.Id; //not transferred on purpose ! 
            loggedJob.JobID = loggedJobDto.FkJobId; 
            loggedJob.WhenStarted = loggedJobDto.WhenStarted; 
            loggedJob.ActivatingUser = loggedJobDto.ActivatingUser; 
            loggedJob.LastRunBy = loggedJobDto.LastRunBy; 
            loggedJob.ExecutionTimeSec = loggedJobDto.ExecutionTimeSec; 
            loggedJob.RunStatus = loggedJobDto.EnmRunStatus; 
            loggedJob.Remarks = loggedJobDto.Remarks; 
            loggedJob.LoggedAlertID = loggedJobDto.FkLoggedAlertId; 
            loggedJob.SuccessCount = loggedJobDto.SuccessCount; 
            loggedJob.FailureCount = loggedJobDto.FailureCount; 
 
            return loggedJob; 
        } 
 
        public static void PopulateFKDisplayNames(this LoggedJobDto dto, clsRequester requester) 
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
            if (dto.FkLoggedAlertId > 0) 
            { 
                try 
                { 
                    var pLoggedAlert = new csLoggedAlert(); 
                    var fault = pLoggedAlert.GetByID(dto.FkLoggedAlertId, requester); 
                    if (fault.isOK) dto.LoggedAlertDisplayName = pLoggedAlert.SystemName; 
                } 
                catch { } 
            } 
        } 
    } 
} 
