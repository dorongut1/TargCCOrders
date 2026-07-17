using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedLoginDto 
    { 
        public long Id { get; set; } 
        public string UserName { get; set; } = string.Empty; 
        public string UserFullName { get; set; } = string.Empty; 
        public DateTime TimeLoggedIn { get; set; } 
        public string ApplicationName { get; set; } = string.Empty; 
        public string LkpUserIdentityTypeCode { get; set; } = string.Empty; 
        public int LkpUserIdentityTypeNameCode { get; set; } 
        public string Roles { get; set; } = string.Empty; 
        public DateTime TimeLoggedOut { get; set; } 
        public int LoginFaultNumber { get; set; } 
        public string EnvironmentUserName { get; set; } = string.Empty; 
        public string EnvironmentMachineName { get; set; } = string.Empty; 
        public string EnvironmentUserDomainName { get; set; } = string.Empty; 
        public string DnsGetHostName { get; set; } = string.Empty; 
        public string AddressList { get; set; } = string.Empty; 
        public string ComputerMacAddress { get; set; } = string.Empty; 
        public string SystemDiskVolumeSerialNo { get; set; } = string.Empty; 
        public DateTime LocalTime { get; set; } 
        public DateTime GmtTime { get; set; } 
        public string AccessingComputerDetails { get; set; } = string.Empty; 
        public string UiCulture { get; set; } = string.Empty; 
        public long TotalPhysicalMemoryKb { get; set; } 
        public long AvailablePhysicalMemoryKb { get; set; } 
        public string ApplicationVersion { get; set; } = string.Empty; 
        public string OriginatingIp { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        public string HostingAssembly { get; set; } = string.Empty; 
        public string OriginatingCountry { get; set; } = string.Empty; 
        public DateTime DateLoggedIn { get; set; } 
        public DateTime MonthLoggedIn { get; set; } 
        public string ClientReportedIp { get; set; } = string.Empty; 
        public string ClientReportedCountry { get; set; } = string.Empty; 
        public string IpAdditionalDetails { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
