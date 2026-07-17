using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedRequestDto 
    { 
        public long Id { get; set; } 
        public long FkLoggedLoginId { get; set; } 
        public DateTime TimeAccessed { get; set; } 
        public long FkUserId { get; set; } 
        public string CallingFunctionWithinApplication { get; set; } = string.Empty; 
        public string EntryPoint { get; set; } = string.Empty; 
        public string Process { get; set; } = string.Empty; 
        public string Thread { get; set; } = string.Empty; 
        public string LoggedLoginDisplayName { get; set; } = string.Empty; 
        public string UserDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
