using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string UserName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LastName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string FirstName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string NationalIdNo { get; set; } = string.Empty; 
        [StringLength(250)] 
        public string Address { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string City { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ProvinceState { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string PostalCode { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Country { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string PhoneNumber { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string Email { get; set; } = string.Empty; 
        public clsEnums.enmUserIdentityType EnmType { get; set; } 
        public long IDinType { get; set; } 
        public bool RequiresComputerIdentification { get; set; } 
        public bool EnableSimultaneousLogins { get; set; } 
        public bool IsDisabled { get; set; } 
        public DateTime ExpiryDate { get; set; } 
        public clsEnums.enmLanguage EnmLanguage { get; set; } 
        public bool IsLockedOut { get; set; } 
        public long FkRoleId { get; set; } 
        public clsEnums.enmAuthenticationMethod EnmAuthenticationMethod { get; set; } 
        public bool RequiresFixedIp { get; set; } 
        public clsEnums.enmMessagingMode EnmMessagingMode { get; set; } 
        [StringLength(64)] 
        public string ApprovalCodeHashed { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ApprovalFunctionName { get; set; } = string.Empty; 
        public DateTimeOffset ApprovalTime { get; set; } 
        public bool PasswordNeverExpires { get; set; } 
        [StringLength(50)] 
        public string LkpSecurityQuestion1Code { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LkpSecurityQuestion2Code { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LkpSecurityQuestion3Code { get; set; } = string.Empty; 
    } 
} 
