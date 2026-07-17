using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class MFADto 
    { 
        public long Id { get; set; } 
        public string CellOrEmail { get; set; } = string.Empty; 
        public string ProtectedFunction { get; set; } = string.Empty; 
        public string CodeHashed { get; set; } = string.Empty; 
        public int AttemptNo { get; set; } 
        public bool IsSuccessful { get; set; } 
        public string LastAccessingIp { get; set; } = string.Empty; 
        public string LastAccessingCountry { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmUiLang { get; set; } 
        public DateTimeOffset WhenCreated { get; set; } 
        public DateTimeOffset WhenAccessed { get; set; } 
        public DateTimeOffset WhenExpires { get; set; } 
        public string Details { get; set; } = string.Empty; 
        public long FkUserId { get; set; } 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
