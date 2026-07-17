using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserDto 
    { 
        public long Id { get; set; } 
        public string UserName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string FirstName { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty; 
        public string NationalIdNo { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string City { get; set; } = string.Empty; 
        public string ProvinceState { get; set; } = string.Empty; 
        public string PostalCode { get; set; } = string.Empty; 
        public string Country { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string PasswordHashed { get; set; } = string.Empty; 
        public DateTime DatePasswordChanged { get; set; } 
        public clsEnums.enmUserIdentityType EnmType { get; set; } 
        public long IDinType { get; set; } 
        public bool RequiresComputerIdentification { get; set; } 
        public bool EnableSimultaneousLogins { get; set; } 
        public DateTime DateActivated { get; set; } 
        public bool IsDisabled { get; set; } 
        public DateTime ExpiryDate { get; set; } 
        public string Comments { get; set; } = string.Empty; 
        public string LastPasswords { get; set; } = string.Empty; 
        public string Applications { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        public bool IsLockedOut { get; set; } 
        public long FkRoleId { get; set; } 
        public clsEnums.enmAuthenticationMethod EnmAuthenticationMethod { get; set; } 
        public bool RequiresFixedIp { get; set; } 
        public clsEnums.enmMessagingMode EnmMessagingMode { get; set; } 
        public string LoggedInIp { get; set; } = string.Empty; 
        public string ApprovalCodeHashed { get; set; } = string.Empty; 
        public string ApprovalFunctionName { get; set; } = string.Empty; 
        public DateTimeOffset ApprovalTime { get; set; } 
        public DateTimeOffset LastSuccessfulLogin { get; set; } 
        public bool PasswordNeverExpires { get; set; } 
        public string LkpSecurityQuestion1Code { get; set; } = string.Empty; 
        public string LkpSecurityQuestion2Code { get; set; } = string.Empty; 
        public string LkpSecurityQuestion3Code { get; set; } = string.Empty; 
        public string RoleDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
