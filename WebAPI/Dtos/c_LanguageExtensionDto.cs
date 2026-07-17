using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LanguageExtensions 
    { 
        public static LanguageDto ToDto(this csLanguage language) 
        { 
            if (language is null) return null!; 
 
            var dto = new LanguageDto 
            { 
                Id = language.ID, 
                Code = language.Code, 
                Name = language.Name, 
                NameLoc = language.NameLoc, 
                Culture = language.Culture
            }; 
            dto._etag = ComputeETag(language); 
            return dto; 
        } 
 
        public static string ComputeETag(csLanguage entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Code ?? ""); 
            sb.Append('|').Append(entity.Name ?? ""); 
            sb.Append('|').Append(entity.NameLoc ?? ""); 
            sb.Append('|').Append(entity.Culture ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLanguage FromDto(this LanguageUpdateDto languageDto, clsRequester requester) 
        { 
            if (languageDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLanguage language = new csLanguage(); 
            if (languageDto.Id > 0) 
            { 
                clsFault fault = language.GetByID(languageDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //language.ID = languageDto.Id; //not transferred on purpose ! 
            language.Code = languageDto.Code; 
            language.Name = languageDto.Name; 
            language.NameLoc = languageDto.NameLoc; 
            language.Culture = languageDto.Culture; 
 
            return language; 
        } 
    } 
} 
