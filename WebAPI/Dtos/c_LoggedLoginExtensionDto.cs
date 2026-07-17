using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class LoggedLoginExtensions 
    { 
        public static LoggedLoginDto ToDto(this csLoggedLogin loggedLogin) 
        { 
            if (loggedLogin is null) return null!; 
 
            var dto = new LoggedLoginDto 
            { 
                Id = loggedLogin.ID, 
                UserName = loggedLogin.UserName, 
                UserFullName = loggedLogin.UserFullName, 
                TimeLoggedIn = loggedLogin.TimeLoggedIn, 
                ApplicationName = loggedLogin.ApplicationName, 
                LkpUserIdentityTypeCode = loggedLogin.UserIdentityTypeCode, 
                LkpUserIdentityTypeNameCode = loggedLogin.UserIdentityTypeNameCode, 
                Roles = loggedLogin.Roles, 
                TimeLoggedOut = loggedLogin.TimeLoggedOut, 
                LoginFaultNumber = loggedLogin.LoginFaultNumber, 
                EnvironmentUserName = loggedLogin.EnvironmentUserName, 
                EnvironmentMachineName = loggedLogin.EnvironmentMachineName, 
                EnvironmentUserDomainName = loggedLogin.EnvironmentUserDomainName, 
                DnsGetHostName = loggedLogin.DnsGetHostName, 
                AddressList = loggedLogin.AddressList, 
                ComputerMacAddress = loggedLogin.ComputerMACAddress, 
                SystemDiskVolumeSerialNo = loggedLogin.SystemDiskVolumeSerialNo, 
                LocalTime = loggedLogin.LocalTime, 
                GmtTime = loggedLogin.GmtTime, 
                AccessingComputerDetails = loggedLogin.AccessingComputerDetails, 
                UiCulture = loggedLogin.UICulture, 
                TotalPhysicalMemoryKb = loggedLogin.TotalPhysicalMemoryKb, 
                AvailablePhysicalMemoryKb = loggedLogin.AvailablePhysicalMemoryKb, 
                ApplicationVersion = loggedLogin.ApplicationVersion, 
                OriginatingIp = loggedLogin.OriginatingIP, 
                EnmLanguage = loggedLogin.Language, 
                HostingAssembly = loggedLogin.HostingAssembly, 
                OriginatingCountry = loggedLogin.OriginatingCountry, 
                DateLoggedIn = loggedLogin.DateLoggedIn, 
                MonthLoggedIn = loggedLogin.MonthLoggedIn, 
                ClientReportedIp = loggedLogin.ClientReportedIP, 
                ClientReportedCountry = loggedLogin.ClientReportedCountry, 
                IpAdditionalDetails = loggedLogin.IPAdditionalDetails
            }; 
            dto._etag = ComputeETag(loggedLogin); 
            return dto; 
        } 
 
        public static string ComputeETag(csLoggedLogin entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.UserName ?? ""); 
            sb.Append('|').Append(entity.UserFullName ?? ""); 
            sb.Append('|').Append(entity.TimeLoggedIn.Ticks); 
            sb.Append('|').Append(entity.ApplicationName ?? ""); 
            sb.Append('|').Append(entity.UserIdentityTypeCode ?? ""); 
            sb.Append('|').Append(entity.UserIdentityTypeNameCode); 
            sb.Append('|').Append(entity.Roles ?? ""); 
            sb.Append('|').Append(entity.TimeLoggedOut.Ticks); 
            sb.Append('|').Append(entity.LoginFaultNumber); 
            sb.Append('|').Append(entity.EnvironmentUserName ?? ""); 
            sb.Append('|').Append(entity.EnvironmentMachineName ?? ""); 
            sb.Append('|').Append(entity.EnvironmentUserDomainName ?? ""); 
            sb.Append('|').Append(entity.DnsGetHostName ?? ""); 
            sb.Append('|').Append(entity.AddressList ?? ""); 
            sb.Append('|').Append(entity.ComputerMACAddress ?? ""); 
            sb.Append('|').Append(entity.SystemDiskVolumeSerialNo ?? ""); 
            sb.Append('|').Append(entity.LocalTime.Ticks); 
            sb.Append('|').Append(entity.GmtTime.Ticks); 
            sb.Append('|').Append(entity.AccessingComputerDetails ?? ""); 
            sb.Append('|').Append(entity.UICulture ?? ""); 
            sb.Append('|').Append(entity.TotalPhysicalMemoryKb); 
            sb.Append('|').Append(entity.AvailablePhysicalMemoryKb); 
            sb.Append('|').Append(entity.ApplicationVersion ?? ""); 
            sb.Append('|').Append(entity.OriginatingIP ?? ""); 
            sb.Append('|').Append(entity.Language); 
            sb.Append('|').Append(entity.HostingAssembly ?? ""); 
            sb.Append('|').Append(entity.OriginatingCountry ?? ""); 
            sb.Append('|').Append(entity.DateLoggedIn.Ticks); 
            sb.Append('|').Append(entity.MonthLoggedIn.Ticks); 
            sb.Append('|').Append(entity.ClientReportedIP ?? ""); 
            sb.Append('|').Append(entity.ClientReportedCountry ?? ""); 
            sb.Append('|').Append(entity.IPAdditionalDetails ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csLoggedLogin FromDto(this LoggedLoginUpdateDto loggedLoginDto, clsRequester requester) 
        { 
            if (loggedLoginDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csLoggedLogin loggedLogin = new csLoggedLogin(); 
            if (loggedLoginDto.Id > 0) 
            { 
                clsFault fault = loggedLogin.GetByID(loggedLoginDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //loggedLogin.ID = loggedLoginDto.Id; //not transferred on purpose ! 
            loggedLogin.UserName = loggedLoginDto.UserName; 
            loggedLogin.UserFullName = loggedLoginDto.UserFullName; 
            loggedLogin.TimeLoggedIn = loggedLoginDto.TimeLoggedIn; 
            loggedLogin.ApplicationName = loggedLoginDto.ApplicationName; 
            loggedLogin.UserIdentityTypeCode = loggedLoginDto.LkpUserIdentityTypeCode; 
            loggedLogin.UserIdentityTypeNameCode = loggedLoginDto.LkpUserIdentityTypeNameCode; 
            loggedLogin.Roles = loggedLoginDto.Roles; 
            loggedLogin.TimeLoggedOut = loggedLoginDto.TimeLoggedOut; 
            loggedLogin.LoginFaultNumber = loggedLoginDto.LoginFaultNumber; 
            loggedLogin.EnvironmentUserName = loggedLoginDto.EnvironmentUserName; 
            loggedLogin.EnvironmentMachineName = loggedLoginDto.EnvironmentMachineName; 
            loggedLogin.EnvironmentUserDomainName = loggedLoginDto.EnvironmentUserDomainName; 
            loggedLogin.DnsGetHostName = loggedLoginDto.DnsGetHostName; 
            loggedLogin.AddressList = loggedLoginDto.AddressList; 
            loggedLogin.ComputerMACAddress = loggedLoginDto.ComputerMacAddress; 
            loggedLogin.SystemDiskVolumeSerialNo = loggedLoginDto.SystemDiskVolumeSerialNo; 
            loggedLogin.LocalTime = loggedLoginDto.LocalTime; 
            loggedLogin.GmtTime = loggedLoginDto.GmtTime; 
            loggedLogin.AccessingComputerDetails = loggedLoginDto.AccessingComputerDetails; 
            loggedLogin.UICulture = loggedLoginDto.UiCulture; 
            loggedLogin.TotalPhysicalMemoryKb = loggedLoginDto.TotalPhysicalMemoryKb; 
            loggedLogin.AvailablePhysicalMemoryKb = loggedLoginDto.AvailablePhysicalMemoryKb; 
            loggedLogin.ApplicationVersion = loggedLoginDto.ApplicationVersion; 
            loggedLogin.OriginatingIP = loggedLoginDto.OriginatingIp; 
            loggedLogin.Language = loggedLoginDto.EnmLanguage; 
            loggedLogin.HostingAssembly = loggedLoginDto.HostingAssembly; 
            loggedLogin.OriginatingCountry = loggedLoginDto.OriginatingCountry; 
            loggedLogin.ClientReportedIP = loggedLoginDto.ClientReportedIp; 
            loggedLogin.ClientReportedCountry = loggedLoginDto.ClientReportedCountry; 
            loggedLogin.IPAdditionalDetails = loggedLoginDto.IpAdditionalDetails; 
 
            return loggedLogin; 
        } 
    } 
} 
