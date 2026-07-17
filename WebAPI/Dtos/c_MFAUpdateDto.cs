using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class MFAUpdateDto 
    { 
        public long Id { get; set; } 
        [StringLength(50)] 
        public string CellOrEmail { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string ProtectedFunction { get; set; } = string.Empty; 
        [StringLength(64)] 
        public string CodeHashed { get; set; } = string.Empty; 
        public int AttemptNo { get; set; } 
        public bool IsSuccessful { get; set; } 
        [StringLength(50)] 
        public string LastAccessingIp { get; set; } = string.Empty; 
        [StringLength(5)] 
        public string LastAccessingCountry { get; set; } = string.Empty; 
        public clsEnums.enmLanguage EnmUiLang { get; set; } 
        public DateTimeOffset WhenCreated { get; set; } 
        public DateTimeOffset WhenAccessed { get; set; } 
        public DateTimeOffset WhenExpires { get; set; } 
        [StringLength(500)] 
        public string Details { get; set; } = string.Empty; 
        public long FkUserId { get; set; } 
    } 
} 
