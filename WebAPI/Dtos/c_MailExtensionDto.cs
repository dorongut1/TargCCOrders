using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class MailExtensions 
    { 
        public static MailDto ToDto(this csMail mail) 
        { 
            if (mail is null) return null!; 
 
            var dto = new MailDto 
            { 
                Id = mail.ID, 
                EnmMessagingMode = mail.MessagingMode, 
                RecipientEmail = mail.RecipientEmail, 
                WhenSent = mail.WhenSent, 
                Subject = mail.Subject, 
                Body = mail.Body, 
                WhenSeen = mail.WhenSeen, 
                WasSeen = mail.WasSeen
            }; 
            dto._etag = ComputeETag(mail); 
            return dto; 
        } 
 
        public static string ComputeETag(csMail entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.MessagingMode); 
            sb.Append('|').Append(entity.RecipientEmail ?? ""); 
            sb.Append('|').Append(entity.WhenSent.Ticks); 
            sb.Append('|').Append(entity.Subject ?? ""); 
            sb.Append('|').Append(entity.Body ?? ""); 
            sb.Append('|').Append(entity.WhenSeen.Ticks); 
            sb.Append('|').Append(entity.WasSeen); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csMail FromDto(this MailUpdateDto mailDto, clsRequester requester) 
        { 
            if (mailDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csMail mail = new csMail(); 
            if (mailDto.Id > 0) 
            { 
                clsFault fault = mail.GetByID(mailDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //mail.ID = mailDto.Id; //not transferred on purpose ! 
            mail.MessagingMode = mailDto.EnmMessagingMode; 
            mail.RecipientEmail = mailDto.RecipientEmail; 
            mail.WhenSent = mailDto.WhenSent; 
            mail.Subject = mailDto.Subject; 
            mail.Body = mailDto.Body; 
            mail.WhenSeen = mailDto.WhenSeen; 
            mail.WasSeen = mailDto.WasSeen; 
 
            return mail; 
        } 
    } 
} 
