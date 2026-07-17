using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedLoginUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string UserName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string UserFullName { get; set; } = string.Empty; 
        public DateTime TimeLoggedIn { get; set; } 
        [StringLength(50)] 
        public string ApplicationName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LkpUserIdentityTypeCode { get; set; } = string.Empty; 
        public int LkpUserIdentityTypeNameCode { get; set; } 
        [StringLength(250)] 
        public string Roles { get; set; } = string.Empty; 
        public DateTime TimeLoggedOut { get; set; } 
        public int LoginFaultNumber { get; set; } 
        [StringLength(100)] 
        public string EnvironmentUserName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string EnvironmentMachineName { get; set; } = string.Empty; 
        [StringLength(10)] 
        public string EnvironmentUserDomainName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string DnsGetHostName { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string AddressList { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ComputerMacAddress { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string SystemDiskVolumeSerialNo { get; set; } = string.Empty; 
        public DateTime LocalTime { get; set; } 
        public DateTime GmtTime { get; set; } 
        [StringLength(250)] 
        public string AccessingComputerDetails { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string UiCulture { get; set; } = string.Empty; 
        public long TotalPhysicalMemoryKb { get; set; } 
        public long AvailablePhysicalMemoryKb { get; set; } 
        [StringLength(250)] 
        public string ApplicationVersion { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string OriginatingIp { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        [StringLength(50)] 
        public string HostingAssembly { get; set; } = string.Empty; 
        [StringLength(10)] 
        public string OriginatingCountry { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ClientReportedIp { get; set; } = string.Empty; 
        [StringLength(10)] 
        public string ClientReportedCountry { get; set; } = string.Empty; 
        [StringLength(250)] 
        public string IpAdditionalDetails { get; set; } = string.Empty; 
    } 
} 
