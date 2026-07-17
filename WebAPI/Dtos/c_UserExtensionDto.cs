using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class UserExtensions 
    { 
        public static UserDto ToDto(this csUser user) 
        { 
            if (user is null) return null!; 
 
            var dto = new UserDto 
            { 
                Id = user.ID, 
                UserName = user.UserName, 
                LastName = user.LastName, 
                FirstName = user.FirstName, 
                FullName = user.FullName, 
                NationalIdNo = user.NationalIDNo, 
                Address = user.Address, 
                City = user.City, 
                ProvinceState = user.ProvinceState, 
                PostalCode = user.PostalCode, 
                Country = user.Country, 
                PhoneNumber = user.PhoneNumber, 
                Email = user.Email, 
                PasswordHashed = user.PasswordHashed, 
                DatePasswordChanged = user.DatePasswordChanged, 
                EnmType = user.Type, 
                IDinType = user.IDinType, 
                RequiresComputerIdentification = user.RequiresComputerIdentification, 
                EnableSimultaneousLogins = user.EnableSimultaneousLogins, 
                DateActivated = user.DateActivated, 
                IsDisabled = user.IsDisabled, 
                ExpiryDate = user.ExpiryDate, 
                Comments = user.Comments, 
                LastPasswords = user.LastPasswords, 
                Applications = user.Applications, 
                EnmLanguage = user.Language, 
                IsLockedOut = user.IsLockedOut, 
                FkRoleId = user.RoleID, 
                EnmAuthenticationMethod = user.AuthenticationMethod, 
                RequiresFixedIp = user.RequiresFixedIP, 
                EnmMessagingMode = user.MessagingMode, 
                LoggedInIp = user.LoggedInIP, 
                ApprovalCodeHashed = user.ApprovalCodeHashed, 
                ApprovalFunctionName = user.ApprovalFunctionName, 
                ApprovalTime = user.ApprovalTime, 
                LastSuccessfulLogin = user.LastSuccessfulLogin, 
                PasswordNeverExpires = user.PasswordNeverExpires, 
                LkpSecurityQuestion1Code = user.SecurityQuestion1Code, 
                LkpSecurityQuestion2Code = user.SecurityQuestion2Code, 
                LkpSecurityQuestion3Code = user.SecurityQuestion3Code
            }; 
            dto._etag = ComputeETag(user); 
            return dto; 
        } 
 
        public static string ComputeETag(csUser entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.UserName ?? ""); 
            sb.Append('|').Append(entity.LastName ?? ""); 
            sb.Append('|').Append(entity.FirstName ?? ""); 
            sb.Append('|').Append(entity.FullName ?? ""); 
            sb.Append('|').Append(entity.NationalIDNo ?? ""); 
            sb.Append('|').Append(entity.Address ?? ""); 
            sb.Append('|').Append(entity.City ?? ""); 
            sb.Append('|').Append(entity.ProvinceState ?? ""); 
            sb.Append('|').Append(entity.PostalCode ?? ""); 
            sb.Append('|').Append(entity.Country ?? ""); 
            sb.Append('|').Append(entity.PhoneNumber ?? ""); 
            sb.Append('|').Append(entity.Email ?? ""); 
            sb.Append('|').Append(entity.PasswordHashed ?? ""); 
            sb.Append('|').Append(entity.DatePasswordChanged.Ticks); 
            sb.Append('|').Append(entity.Type); 
            sb.Append('|').Append(entity.IDinType); 
            sb.Append('|').Append(entity.RequiresComputerIdentification); 
            sb.Append('|').Append(entity.EnableSimultaneousLogins); 
            sb.Append('|').Append(entity.DateActivated.Ticks); 
            sb.Append('|').Append(entity.IsDisabled); 
            sb.Append('|').Append(entity.ExpiryDate.Ticks); 
            sb.Append('|').Append(entity.Comments ?? ""); 
            sb.Append('|').Append(entity.LastPasswords ?? ""); 
            sb.Append('|').Append(entity.Applications ?? ""); 
            sb.Append('|').Append(entity.Language); 
            sb.Append('|').Append(entity.IsLockedOut); 
            sb.Append('|').Append(entity.RoleID); 
            sb.Append('|').Append(entity.AuthenticationMethod); 
            sb.Append('|').Append(entity.RequiresFixedIP); 
            sb.Append('|').Append(entity.MessagingMode); 
            sb.Append('|').Append(entity.LoggedInIP ?? ""); 
            sb.Append('|').Append(entity.ApprovalCodeHashed ?? ""); 
            sb.Append('|').Append(entity.ApprovalFunctionName ?? ""); 
            sb.Append('|').Append(entity.ApprovalTime.Ticks); 
            sb.Append('|').Append(entity.LastSuccessfulLogin.Ticks); 
            sb.Append('|').Append(entity.PasswordNeverExpires); 
            sb.Append('|').Append(entity.SecurityQuestion1Code ?? ""); 
            sb.Append('|').Append(entity.SecurityQuestion2Code ?? ""); 
            sb.Append('|').Append(entity.SecurityQuestion3Code ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csUser FromDto(this UserUpdateDto userDto, clsRequester requester) 
        { 
            if (userDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csUser user = new csUser(); 
            if (userDto.Id > 0) 
            { 
                clsFault fault = user.GetByID(userDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //user.ID = userDto.Id; //not transferred on purpose ! 
            user.UserName = userDto.UserName; 
            user.LastName = userDto.LastName; 
            user.FirstName = userDto.FirstName; 
            user.NationalIDNo = userDto.NationalIdNo; 
            user.Address = userDto.Address; 
            user.City = userDto.City; 
            user.ProvinceState = userDto.ProvinceState; 
            user.PostalCode = userDto.PostalCode; 
            user.Country = userDto.Country; 
            user.PhoneNumber = userDto.PhoneNumber; 
            user.Email = userDto.Email; 
            user.Type = userDto.EnmType; 
            user.IDinType = userDto.IDinType; 
            user.RequiresComputerIdentification = userDto.RequiresComputerIdentification; 
            user.EnableSimultaneousLogins = userDto.EnableSimultaneousLogins; 
            user.IsDisabled = userDto.IsDisabled; 
            user.ExpiryDate = userDto.ExpiryDate; 
            user.Language = userDto.EnmLanguage; 
            user.IsLockedOut = userDto.IsLockedOut; 
            user.RoleID = userDto.FkRoleId; 
            user.AuthenticationMethod = userDto.EnmAuthenticationMethod; 
            user.RequiresFixedIP = userDto.RequiresFixedIp; 
            user.MessagingMode = userDto.EnmMessagingMode; 
            user.ApprovalCodeHashed = userDto.ApprovalCodeHashed; 
            user.ApprovalFunctionName = userDto.ApprovalFunctionName; 
            user.ApprovalTime = userDto.ApprovalTime; 
            user.PasswordNeverExpires = userDto.PasswordNeverExpires; 
            user.SecurityQuestion1Code = userDto.LkpSecurityQuestion1Code; 
            user.SecurityQuestion2Code = userDto.LkpSecurityQuestion2Code; 
            user.SecurityQuestion3Code = userDto.LkpSecurityQuestion3Code; 
 
            return user; 
        } 
 
        public static void PopulateFKDisplayNames(this UserDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkRoleId > 0) 
            { 
                try 
                { 
                    var pRole = new csRole(); 
                    var fault = pRole.GetByID(dto.FkRoleId, requester); 
                    if (fault.isOK) dto.RoleDisplayName = pRole.Name; 
                } 
                catch { } 
            } 
        } 
    } 
} 
