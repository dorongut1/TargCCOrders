using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class JobExtensions 
    { 
        public static JobDto ToDto(this csJob job) 
        { 
            if (job is null) return null!; 
 
            var dto = new JobDto 
            { 
                Id = job.ID, 
                LkpJobCode = job.JobCode, 
                LkpJobRunnerCode = job.JobRunnerCode, 
                Description = job.Description, 
                Instructions = job.Instructions, 
                EnmJobType = job.JobType, 
                WhenToRun = job.WhenToRun, 
                CyclicCount = job.CyclicCount, 
                SendNotificationOnSuccess = job.SendNotificationOnSuccess, 
                SendAlarmOnMissed = job.SendAlarmOnMissed, 
                TimeOutSec = job.TimeOutSec, 
                Active = job.Active, 
                ActivatingUser = job.ActivatingUser, 
                NextRunTime = job.NextRunTime, 
                LastRunTime = job.LastRunTime, 
                EnmJobStatus = job.JobStatus, 
                WarningMailSent = job.WarningMailSent, 
                IsManaged = job.IsManaged, 
                LastRunBy = job.LastRunBy
            }; 
            dto._etag = ComputeETag(job); 
            return dto; 
        } 
 
        public static string ComputeETag(csJob entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.JobCode ?? ""); 
            sb.Append('|').Append(entity.JobRunnerCode ?? ""); 
            sb.Append('|').Append(entity.Description ?? ""); 
            sb.Append('|').Append(entity.Instructions ?? ""); 
            sb.Append('|').Append(entity.JobType); 
            sb.Append('|').Append(entity.WhenToRun.Ticks); 
            sb.Append('|').Append(entity.CyclicCount); 
            sb.Append('|').Append(entity.SendNotificationOnSuccess); 
            sb.Append('|').Append(entity.SendAlarmOnMissed); 
            sb.Append('|').Append(entity.TimeOutSec); 
            sb.Append('|').Append(entity.Active); 
            sb.Append('|').Append(entity.ActivatingUser ?? ""); 
            sb.Append('|').Append(entity.NextRunTime.Ticks); 
            sb.Append('|').Append(entity.LastRunTime.Ticks); 
            sb.Append('|').Append(entity.JobStatus); 
            sb.Append('|').Append(entity.WarningMailSent); 
            sb.Append('|').Append(entity.IsManaged); 
            sb.Append('|').Append(entity.LastRunBy ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csJob FromDto(this JobUpdateDto jobDto, clsRequester requester) 
        { 
            if (jobDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csJob job = new csJob(); 
            if (jobDto.Id > 0) 
            { 
                clsFault fault = job.GetByID(jobDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //job.ID = jobDto.Id; //not transferred on purpose ! 
            job.JobCode = jobDto.LkpJobCode; 
            job.JobRunnerCode = jobDto.LkpJobRunnerCode; 
            job.Description = jobDto.Description; 
            job.Instructions = jobDto.Instructions; 
            job.JobType = jobDto.EnmJobType; 
            job.WhenToRun = jobDto.WhenToRun; 
            job.CyclicCount = jobDto.CyclicCount; 
            job.SendNotificationOnSuccess = jobDto.SendNotificationOnSuccess; 
            job.SendAlarmOnMissed = jobDto.SendAlarmOnMissed; 
            job.TimeOutSec = jobDto.TimeOutSec; 
            job.Active = jobDto.Active; 
            job.IsManaged = jobDto.IsManaged; 
            job.LastRunBy = jobDto.LastRunBy; 
 
            return job; 
        } 
    } 
} 
