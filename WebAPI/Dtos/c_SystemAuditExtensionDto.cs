using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class SystemAuditExtensions 
    { 
        public static SystemAuditDto ToDto(this csSystemAudit systemAudit) 
        { 
            if (systemAudit is null) return null!; 
 
            var dto = new SystemAuditDto 
            { 
                Id = systemAudit.ID, 
                TableName = systemAudit.TableName, 
                RowId = systemAudit.RowId, 
                Operation = systemAudit.Operation, 
                OccurredAt = systemAudit.OccurredAt, 
                SqlCurrentUser = systemAudit.SqlCurrentUser, 
                ChangedByUser = systemAudit.ChangedByUser, 
                ActiveLoginId = systemAudit.ActiveLoginID, 
                SqlSystemUser = systemAudit.SqlSystemUser, 
                SqlAppName = systemAudit.SqlAppName, 
                SqlHostName = systemAudit.SqlHostName, 
                Changes = systemAudit.Changes
            }; 
            dto._etag = ComputeETag(systemAudit); 
            return dto; 
        } 
 
        public static string ComputeETag(csSystemAudit entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.TableName ?? ""); 
            sb.Append('|').Append(entity.RowId); 
            sb.Append('|').Append(entity.Operation ?? ""); 
            sb.Append('|').Append(entity.OccurredAt.Ticks); 
            sb.Append('|').Append(entity.SqlCurrentUser ?? ""); 
            sb.Append('|').Append(entity.ChangedByUser ?? ""); 
            sb.Append('|').Append(entity.ActiveLoginID); 
            sb.Append('|').Append(entity.SqlSystemUser ?? ""); 
            sb.Append('|').Append(entity.SqlAppName ?? ""); 
            sb.Append('|').Append(entity.SqlHostName ?? ""); 
            sb.Append('|').Append(entity.Changes ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csSystemAudit FromDto(this SystemAuditUpdateDto systemAuditDto, clsRequester requester) 
        { 
            if (systemAuditDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csSystemAudit systemAudit = new csSystemAudit(); 
            if (systemAuditDto.Id > 0) 
            { 
                clsFault fault = systemAudit.GetByID(systemAuditDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //systemAudit.ID = systemAuditDto.Id; //not transferred on purpose ! 
            systemAudit.TableName = systemAuditDto.TableName; 
            systemAudit.RowId = systemAuditDto.RowId; 
            systemAudit.Operation = systemAuditDto.Operation; 
            systemAudit.OccurredAt = systemAuditDto.OccurredAt; 
            systemAudit.SqlCurrentUser = systemAuditDto.SqlCurrentUser; 
            systemAudit.ChangedByUser = systemAuditDto.ChangedByUser; 
            systemAudit.ActiveLoginID = systemAuditDto.ActiveLoginId; 
            systemAudit.SqlSystemUser = systemAuditDto.SqlSystemUser; 
            systemAudit.SqlAppName = systemAuditDto.SqlAppName; 
            systemAudit.SqlHostName = systemAuditDto.SqlHostName; 
            systemAudit.Changes = systemAuditDto.Changes; 
 
            return systemAudit; 
        } 
    } 
} 
