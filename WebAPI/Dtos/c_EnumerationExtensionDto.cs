using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class EnumerationExtensions 
    { 
        public static EnumerationDto ToDto(this csEnumeration enumeration) 
        { 
            if (enumeration is null) return null!; 
 
            var dto = new EnumerationDto 
            { 
                Id = enumeration.ID, 
                IsSystem = enumeration.IsSystem, 
                EnumType = enumeration.EnumType, 
                EnumValue = enumeration.EnumValue, 
                Text = enumeration.TextLocalized
            }; 
            dto._etag = ComputeETag(enumeration); 
            return dto; 
        } 
 
        public static string ComputeETag(csEnumeration entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.IsSystem); 
            sb.Append('|').Append(entity.EnumType ?? ""); 
            sb.Append('|').Append(entity.EnumValue ?? ""); 
            sb.Append('|').Append(entity.Text ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csEnumeration FromDto(this EnumerationUpdateDto enumerationDto, clsRequester requester) 
        { 
            if (enumerationDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csEnumeration enumeration = new csEnumeration(); 
            if (enumerationDto.Id > 0) 
            { 
                clsFault fault = enumeration.GetByID(enumerationDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //enumeration.ID = enumerationDto.Id; //not transferred on purpose ! 
            enumeration.IsSystem = enumerationDto.IsSystem; 
            enumeration.EnumType = enumerationDto.EnumType; 
            enumeration.EnumValue = enumerationDto.EnumValue; 
            enumeration.Text = enumerationDto.Text; 
 
            return enumeration; 
        } 
    } 
} 
