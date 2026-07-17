using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserLoginKeyUpdateDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        [StringLength(50)] 
        public string ApplicationName { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ApplicationIdentifier { get; set; } = string.Empty; 
        [StringLength(64)] 
        public string KeyHashed { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string ExternalIpAtCreation { get; set; } = string.Empty; 
        [StringLength(2)] 
        public string CountryAtCreation { get; set; } = string.Empty; 
        public DateTime LastAccessTime { get; set; } 
        public long LoggedLoginId { get; set; } 
    } 
} 
