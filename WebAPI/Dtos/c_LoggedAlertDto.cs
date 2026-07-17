using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedAlertDto 
    { 
        public long Id { get; set; } 
        public DateTime TimeOccurred { get; set; } 
        public int FaultNumber { get; set; } 
        public string SystemName { get; set; } = string.Empty; 
        public string CallingApplication { get; set; } = string.Empty; 
        public long FkAffectedUserId { get; set; } 
        public string CallingApplicationVersion { get; set; } = string.Empty; 
        public string CallingFunctionWithinApplication { get; set; } = string.Empty; 
        public string FreeText { get; set; } = string.Empty; 
        public string FaultingAssembly { get; set; } = string.Empty; 
        public string AssemblyEntryPoint { get; set; } = string.Empty; 
        public string FaultingClass { get; set; } = string.Empty; 
        public string FaultingFunction { get; set; } = string.Empty; 
        public string FaultingFunctionParameters { get; set; } = string.Empty; 
        public string FaultIdent { get; set; } = string.Empty; 
        public string FaultDescription { get; set; } = string.Empty; 
        public string MessageSentToUser { get; set; } = string.Empty; 
        public string ActionSentToUser { get; set; } = string.Empty; 
        public clsEnums.enmFaultType EnmFaultType { get; set; } 
        public clsEnums.enmFaultSeverity EnmFaultSeverity { get; set; } 
        public long FkLoggedLoginId { get; set; } 
        public string Thread { get; set; } = string.Empty; 
        public string LkpUserIdentityTypeCode { get; set; } = string.Empty; 
        public int LkpUserIdentityTypeNameCode { get; set; } 
        public DateTime DateOccurred { get; set; } 
        public DateTime MonthOccurred { get; set; } 
        public string AffectedUserDisplayName { get; set; } = string.Empty; 
        public string LoggedLoginDisplayName { get; set; } = string.Empty; 
        /// <summary>Concurrency control: hash of record state at time of GET</summary> 
        public string _etag { get; set; } = string.Empty; 
    } 
} 
