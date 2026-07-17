using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class UserLoginKeyDto 
    { 
        public long Id { get; set; } 
        public long FkUserId { get; set; } 
        public string ApplicationName { get; set; } = string.Empty; 
        public string ApplicationIdentifier { get; set; } = string.Empty; 
        public string KeyHashed { get; set; } = string.Empty; 
        public string ExternalIpAtCreation { get; set; } = string.Empty; 
        public string CountryAtCreation { get; set; } = string.Empty; 
        public DateTime LastAccessTime { get; set; } 
        public long LoggedLoginId { get; set; } 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
