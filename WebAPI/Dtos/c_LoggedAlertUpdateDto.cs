using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public class LoggedAlertUpdateDto 
    { 
        public long Id { get; set; } 
        public DateTime TimeOccurred { get; set; } 
        public int FaultNumber { get; set; } 
        [StringLength(50)] 
        public string SystemName { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string CallingApplication { get; set; } = string.Empty; 
        public long FkAffectedUserId { get; set; } 
        [StringLength(50)] 
        public string CallingApplicationVersion { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string CallingFunctionWithinApplication { get; set; } = string.Empty; 
        public string FreeText { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string FaultingAssembly { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string AssemblyEntryPoint { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string FaultingClass { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string FaultingFunction { get; set; } = string.Empty; 
        public string FaultingFunctionParameters { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string FaultIdent { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string FaultDescription { get; set; } = string.Empty; 
        [StringLength(100)] 
        public string MessageSentToUser { get; set; } = string.Empty; 
        [StringLength(200)] 
        public string ActionSentToUser { get; set; } = string.Empty; 
        public clsEnums.enmFaultType EnmFaultType { get; set; } 
        public clsEnums.enmFaultSeverity EnmFaultSeverity { get; set; } 
        public long FkLoggedLoginId { get; set; } 
        [StringLength(50)] 
        public string Thread { get; set; } = string.Empty; 
        [StringLength(50)] 
        public string LkpUserIdentityTypeCode { get; set; } = string.Empty; 
        public int LkpUserIdentityTypeNameCode { get; set; } 
    } 
} 
