using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class AuditIndexedExtensions 
    { 
        public static AuditIndexedDto ToDto(this csAuditIndexed auditIndexed) 
        { 
            if (auditIndexed is null) return null!; 
 
            var dto = new AuditIndexedDto 
            { 
                Id = auditIndexed.ID, 
                OriginalId = auditIndexed.OriginalID, 
                TableName = auditIndexed.TableName, 
                RowId = auditIndexed.RowID, 
                Operation = auditIndexed.Operation, 
                OccurredAt = auditIndexed.OccurredAt, 
                SqlCurrentUser = auditIndexed.SqlCurrentUser, 
                FieldName = auditIndexed.FieldName, 
                OldValue = auditIndexed.OldValue, 
                NewValue = auditIndexed.NewValue, 
                ChangedByUser = auditIndexed.ChangedByUser, 
                ActiveLoginId = auditIndexed.ActiveLoginID, 
                SqlSystemUser = auditIndexed.SqlSystemUser, 
                SqlAppName = auditIndexed.SqlAppName, 
                SqlHostName = auditIndexed.SqlHostName
            }; 
            dto._etag = ComputeETag(auditIndexed); 
            return dto; 
        } 
 
        public static string ComputeETag(csAuditIndexed entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.OriginalID); 
            sb.Append('|').Append(entity.TableName ?? ""); 
            sb.Append('|').Append(entity.RowID); 
            sb.Append('|').Append(entity.Operation ?? ""); 
            sb.Append('|').Append(entity.OccurredAt.Ticks); 
            sb.Append('|').Append(entity.SqlCurrentUser ?? ""); 
            sb.Append('|').Append(entity.FieldName ?? ""); 
            sb.Append('|').Append(entity.OldValue ?? ""); 
            sb.Append('|').Append(entity.NewValue ?? ""); 
            sb.Append('|').Append(entity.ChangedByUser ?? ""); 
            sb.Append('|').Append(entity.ActiveLoginID); 
            sb.Append('|').Append(entity.SqlSystemUser ?? ""); 
            sb.Append('|').Append(entity.SqlAppName ?? ""); 
            sb.Append('|').Append(entity.SqlHostName ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csAuditIndexed FromDto(this AuditIndexedUpdateDto auditIndexedDto, clsRequester requester) 
        { 
            if (auditIndexedDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csAuditIndexed auditIndexed = new csAuditIndexed(); 
            if (auditIndexedDto.Id > 0) 
            { 
                clsFault fault = auditIndexed.GetByID(auditIndexedDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //auditIndexed.ID = auditIndexedDto.Id; //not transferred on purpose ! 
            auditIndexed.OriginalID = auditIndexedDto.OriginalId; 
            auditIndexed.TableName = auditIndexedDto.TableName; 
            auditIndexed.RowID = auditIndexedDto.RowId; 
            auditIndexed.Operation = auditIndexedDto.Operation; 
            auditIndexed.OccurredAt = auditIndexedDto.OccurredAt; 
            auditIndexed.SqlCurrentUser = auditIndexedDto.SqlCurrentUser; 
            auditIndexed.FieldName = auditIndexedDto.FieldName; 
            auditIndexed.OldValue = auditIndexedDto.OldValue; 
            auditIndexed.NewValue = auditIndexedDto.NewValue; 
            auditIndexed.ChangedByUser = auditIndexedDto.ChangedByUser; 
            auditIndexed.ActiveLoginID = auditIndexedDto.ActiveLoginId; 
            auditIndexed.SqlSystemUser = auditIndexedDto.SqlSystemUser; 
            auditIndexed.SqlAppName = auditIndexedDto.SqlAppName; 
            auditIndexed.SqlHostName = auditIndexedDto.SqlHostName; 
 
            return auditIndexed; 
        } 
    } 
} 
