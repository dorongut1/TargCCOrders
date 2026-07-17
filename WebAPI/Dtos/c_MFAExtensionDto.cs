using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class MFAExtensions 
    { 
        public static MFADto ToDto(this csMFA mfa) 
        { 
            if (mfa is null) return null!; 
 
            var dto = new MFADto 
            { 
                Id = mfa.ID, 
                CellOrEmail = mfa.CellOrEmail, 
                ProtectedFunction = mfa.ProtectedFunction, 
                CodeHashed = mfa.CodeHashed, 
                AttemptNo = mfa.AttemptNo, 
                IsSuccessful = mfa.IsSuccessful, 
                LastAccessingIp = mfa.LastAccessingIP, 
                LastAccessingCountry = mfa.LastAccessingCountry, 
                EnmUiLang = mfa.UILang, 
                WhenCreated = mfa.WhenCreated, 
                WhenAccessed = mfa.WhenAccessed, 
                WhenExpires = mfa.WhenExpires, 
                Details = mfa.Details, 
                FkUserId = mfa.UserID
            }; 
            dto._etag = ComputeETag(mfa); 
            return dto; 
        } 
 
        public static string ComputeETag(csMFA entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.CellOrEmail ?? ""); 
            sb.Append('|').Append(entity.ProtectedFunction ?? ""); 
            sb.Append('|').Append(entity.CodeHashed ?? ""); 
            sb.Append('|').Append(entity.AttemptNo); 
            sb.Append('|').Append(entity.IsSuccessful); 
            sb.Append('|').Append(entity.LastAccessingIP ?? ""); 
            sb.Append('|').Append(entity.LastAccessingCountry ?? ""); 
            sb.Append('|').Append(entity.UILang); 
            sb.Append('|').Append(entity.WhenCreated.Ticks); 
            sb.Append('|').Append(entity.WhenAccessed.Ticks); 
            sb.Append('|').Append(entity.WhenExpires.Ticks); 
            sb.Append('|').Append(entity.Details ?? ""); 
            sb.Append('|').Append(entity.UserID); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csMFA FromDto(this MFAUpdateDto mfaDto, clsRequester requester) 
        { 
            if (mfaDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csMFA mfa = new csMFA(); 
            if (mfaDto.Id > 0) 
            { 
                clsFault fault = mfa.GetByID(mfaDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //mfa.ID = mfaDto.Id; //not transferred on purpose ! 
            mfa.CellOrEmail = mfaDto.CellOrEmail; 
            mfa.ProtectedFunction = mfaDto.ProtectedFunction; 
            mfa.CodeHashed = mfaDto.CodeHashed; 
            mfa.AttemptNo = mfaDto.AttemptNo; 
            mfa.IsSuccessful = mfaDto.IsSuccessful; 
            mfa.LastAccessingIP = mfaDto.LastAccessingIp; 
            mfa.LastAccessingCountry = mfaDto.LastAccessingCountry; 
            mfa.UILang = mfaDto.EnmUiLang; 
            mfa.WhenCreated = mfaDto.WhenCreated; 
            mfa.WhenAccessed = mfaDto.WhenAccessed; 
            mfa.WhenExpires = mfaDto.WhenExpires; 
            mfa.Details = mfaDto.Details; 
            mfa.UserID = mfaDto.FkUserId; 
 
            return mfa; 
        } 
 
        public static void PopulateFKDisplayNames(this MFADto dto, clsRequester requester) 
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
