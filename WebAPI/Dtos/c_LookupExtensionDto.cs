using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LookupExtensions 
    { 
        public static LookupDto ToDto(this csLookup lookup) 
        { 
            if (lookup is null) return null!; 
 
            var dto = new LookupDto 
            { 
                Id = lookup.ID, 
                EnmParentLookupType = lookup.ParentLookupType, 
                ParentCode = lookup.ParentCode, 
                EnmLookupType = lookup.LookupType, 
                Code = lookup.Code, 
                Text = lookup.TextLocalized, 
                Description = lookup.DescriptionLocalized
            }; 
            dto._etag = ComputeETag(lookup); 
            return dto; 
        } 
 
        public static string ComputeETag(csLookup entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ParentLookupType); 
            sb.Append('|').Append(entity.ParentCode ?? ""); 
            sb.Append('|').Append(entity.LookupType); 
            sb.Append('|').Append(entity.Code ?? ""); 
            sb.Append('|').Append(entity.Text ?? ""); 
            sb.Append('|').Append(entity.Description ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLookup FromDto(this LookupUpdateDto lookupDto, clsRequester requester) 
        { 
            if (lookupDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLookup lookup = new csLookup(); 
            if (lookupDto.Id > 0) 
            { 
                clsFault fault = lookup.GetByID(lookupDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //lookup.ID = lookupDto.Id; //not transferred on purpose ! 
            lookup.ParentLookupType = lookupDto.EnmParentLookupType; 
            lookup.ParentCode = lookupDto.ParentCode; 
            lookup.LookupType = lookupDto.EnmLookupType; 
            lookup.Code = lookupDto.Code; 
            lookup.Text = lookupDto.Text; 
            lookup.Description = lookupDto.Description; 
 
            return lookup; 
        } 
    } 
} 
