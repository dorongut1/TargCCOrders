Imports System.Runtime.CompilerServices

Public Class clsEnums
  
  Public Enum enmEnum
    UD
    [AccountantMethod]
    [ApplicationAuthenticationToWS]
    [AuthenticationMethod]
    [Category]
    [ccAPICompressionMode]
    [ComboListKeyType]
    [CustomerType]
    [DebtStatus]
    [DeliveryDay]
    [DeliveryMethod]
    [DeliveryStatus]
    [EmailStatus]
    [FaultSeverity]
    [FaultType]
    [FillDirection]
    [Importance]
    [JobAlertType]
    [JobStatus]
    [JobType]
    [Language]
    [LoadParent]
    [Lookup]
    [MessagingMode]
    [ObjectStatus]
    [ObjectType]
    [OrderStatus]
    [PaymentMethod]
    [PaymentStatus]
    [Process]
    [SystemDefaultType]
    [UserIdentificationModel]
    [UserIdentityType]
    [WildCardType]
  End Enum
  
  Public Enum enmAccountantMethod
    UD
    [Email]
    [Mail]
  End Enum
  Public Enum enmApplicationAuthenticationToWS
    UD
    [ActiveUserCredentials]
    [ApplicationCredentials]
    [None]
    [SpecificUserCredentials]
  End Enum
  Public Enum enmAuthenticationMethod
    UD
    [NamePassword]
    [OneTimePassword]
    [SingleVenue2FA]
    [TwoFactorAuthentication]
  End Enum
  Public Enum enmCategory
    UD
    [Beehives]
    [Biolife]
    [BiologicalPest]
    [BioPest]
    [Biotime]
    [Butano]
    [Canrise]
    [Delivery]
    [Equipment]
    [General]
    [Preparations]
    [Shmoolik]
    [Stock]
    [Traps]
  End Enum
  Public Enum enmccAPICompressionMode
    UD
    [DeflateTargCC]
    [GzipTargCC]
    [IIS]
    [None]
  End Enum
  Public Enum enmComboListKeyType
    UD
    [Enum]
    [Integer]
    [Long]
    [Object]
    [String]
  End Enum
  Public Enum enmCustomerType
    UD
    [Farm]
    [Farmer]
    [Hydro]
    [Private]
    [Retail]
  End Enum
  Public Enum enmDebtStatus
    UD
    [Cancelled]
    [Open]
    [Paid]
    [PartiallyPaid]
    [WrittenOff]
  End Enum
  Public Enum enmDeliveryDay
    UD
    [Friday]
    [Monday]
    [Saturday]
    [Sunday]
    [Thursday]
    [Tuesday]
    [Wednesday]
  End Enum
  Public Enum enmDeliveryMethod
    UD
    [Biobee]
    [BioTour]
    [GreenArt]
    [Gvulot]
    [LiorCarmiel]
    [Netzach]
    [NoDelivery]
    [Other]
    [Paran]
    [Ptzael]
    [SelfPickup]
    [Shmoolik]
    [Tzofar]
    [WarehouseKQ]
    ' Appended rather than inserted alphabetically. TargCC used to emit these
    ' in order, but it no longer runs, and appending leaves every existing
    ' member's number untouched. Display order comes from the parameters
    ' screen, so alphabetical order here buys nothing.
    ' Added 18.8.2026 from the live workbook's settings sheet, which the
    ' business maintains; without them an imported order carrying one of these
    ' is read as UD and destroyed on the first save.
    [Elkana]
    [YDM]
    [BeerTuvia]
    [BGabriel]
  End Enum
  Public Enum enmDeliveryStatus
    UD
    [AtHub]
    [Cancelled]
    [Delivered]
    [Failed]
    [InTransit]
    [Ordered]
    [Pending]
    [Received]
  End Enum
  Public Enum enmEmailStatus
    UD
    [Draft]
    [Failed]
    [Sent]
  End Enum
  Public Enum enmFaultSeverity
    UD
    [Alert]
    [Email]
    [Info]
    [LogOnly]
    [SMS]
  End Enum
  Public Enum enmFaultType
    UD
    [Business]
    [Security]
    [System]
  End Enum
  Public Enum enmFillDirection
    UD
    [ASC]
    [DESC]
  End Enum
  Public Enum enmImportance
    UD
    [High]
    [Low]
    [Medium]
  End Enum
  Public Enum enmJobAlertType
    UD
    [email]
    [Pager]
    [SMS]
  End Enum
  Public Enum enmJobStatus
    UD
    [Failure]
    [InProcess]
    [Missed]
    [Success]
    [Warning]
  End Enum
  Public Enum enmJobType
    UD
    [Annually]
    [CyclicDay]
    [CyclicHour]
    [CyclicMin]
    [CyclicSec]
    [Daily]
    [Monthly]
    [OneOff]
    [Weekly]
  End Enum
  Public Enum enmLanguage
    UD
    [af]
    [am]
    [ar]
    [bg]
    [cr]
    [cs]
    [cy]
    [da]
    [de]
    [el]
    [en]
    [eo]
    [es]
    [et]
    [fa]
    [fi]
    [fr]
    [ga]
    [gd]
    [he]
    [hr]
    [hu]
    [hy]
    [id]
    [it]
    [iu]
    [ja]
    [ko]
    [nl]
    [no]
    [pl]
    [pt]
    [ro]
    [ru]
    [sq]
    [sv]
    [tr]
    [uk]
    [vi]
    [yi]
    [zh]
    [zu]
  End Enum
  Public Enum enmLoadParent
    UD
    [DoNotLoad]
    [EntireObject]
    [TextOnly]
  End Enum
  Public Enum enmLookup
    UD
    [Generic]
    [Job]
    [JobRunner]
    [SecurityQuestion]
    [UserIdentityType]
    [UserIdentityTypeName]
  End Enum
  Public Enum enmMessagingMode
    UD
    [Email]
    [SMS]
  End Enum
  Public Enum enmObjectStatus
    UD
    [Clean]
    [Deleted]
    [Dirty]
    [New]
  End Enum
  Public Enum enmObjectType
    UD
    [System]
    [TableData]
    [TableFieldName]
    [UI]
  End Enum
  Public Enum enmOrderStatus
    UD
    [Cancelled]
    [Completed]
    [InProgress]
    [New]
    [Processing]
    [Shipped]
  End Enum
  Public Enum enmPaymentMethod
    UD
    [BitPaybox]
    [Cash]
    [Check]
    [Credit]
    [CreditCard]
    [Transfer]
    [WebPayment]
  End Enum
  Public Enum enmPaymentStatus
    UD
    [Paid]
    [PartiallyPaid]
    [Pending]
    [Unpaid]
  End Enum
  Public Enum enmProcess
    UD
    [prc_BackupDatabase]
    [prc_CreateDefaultPermissionsForNewRolesAndTables]
    [prc_DeleteOldLogs]
    [prc_DoSample]
    [prc_DoTasks]
    [prc_EjectAllUsers]
    [prc_EjectNonMaster]
    [prc_GetNextJobForRunner]
    [prc_GetNextManagedJobForRunner]
    [prc_GetSpecificUnmanagedJobForRunner]
    [prc_HandleObjectToTranslate]
    [prc_LogInAnonymously]
    [prc_LogInByNamePwd]
    [prc_LogInByNetworkCredentials]
    [prc_LogOut]
    [prc_MarkJobAsComplete]
    [prc_MoveAudits]
    [prc_RequestDatabaseBackup]
    [prc_RequestIndexReorganization]
    [prc_ResetDefaultPermissions]
    [prc_ScanJobs]
    [prc_SendMail]
    [prc_SetDefaultPermissionsForRole]
    [prc_SetJobToNow]
    [prc_SysAdmin]
    [prc_WriteDatabaseToXML]
    [tbl_BeehiveBuyerTrackingDelete]
    [tbl_BeehiveBuyerTrackingUpdate]
    [tbl_BeehiveBuyerTrackingView]
    [tbl_c_AlertMessageDelete]
    [tbl_c_AlertMessageUpdate]
    [tbl_c_AlertMessageView]
    [tbl_c_AuditIndexedDelete]
    [tbl_c_AuditIndexedUpdate]
    [tbl_c_AuditIndexedView]
    [tbl_c_EnumerationDelete]
    [tbl_c_EnumerationUpdate]
    [tbl_c_EnumerationView]
    [tbl_c_JobAlertRecipientDelete]
    [tbl_c_JobAlertRecipientUpdate]
    [tbl_c_JobAlertRecipientView]
    [tbl_c_JobDelete]
    [tbl_c_JobUpdate]
    [tbl_c_JobUpdateSetToNow]
    [tbl_c_JobView]
    [tbl_c_LanguageDelete]
    [tbl_c_LanguageUpdate]
    [tbl_c_LanguageView]
    [tbl_c_LoggedAlertDelete]
    [tbl_c_LoggedAlertUpdate]
    [tbl_c_LoggedAlertView]
    [tbl_c_LoggedJobDelete]
    [tbl_c_LoggedJobUpdate]
    [tbl_c_LoggedJobView]
    [tbl_c_LoggedLoginDelete]
    [tbl_c_LoggedLoginUpdate]
    [tbl_c_LoggedLoginView]
    [tbl_c_LoggedRequestDelete]
    [tbl_c_LoggedRequestUpdate]
    [tbl_c_LoggedRequestView]
    [tbl_c_LookupDelete]
    [tbl_c_LookupUpdate]
    [tbl_c_LookupView]
    [tbl_c_MailDelete]
    [tbl_c_MailUpdate]
    [tbl_c_MailView]
    [tbl_c_MFADelete]
    [tbl_c_MFAUpdate]
    [tbl_c_MFAView]
    [tbl_c_ObjectToTranslateDelete]
    [tbl_c_ObjectToTranslateUpdate]
    [tbl_c_ObjectToTranslateView]
    [tbl_c_ObjectTranslationDelete]
    [tbl_c_ObjectTranslationUpdate]
    [tbl_c_ObjectTranslationView]
    [tbl_c_PermissionDelete]
    [tbl_c_PermissionUpdate]
    [tbl_c_PermissionView]
    [tbl_c_ProcessDelete]
    [tbl_c_ProcessUpdate]
    [tbl_c_ProcessView]
    [tbl_c_RoleDelete]
    [tbl_c_RoleUpdate]
    [tbl_c_RoleView]
    [tbl_c_SystemAuditDelete]
    [tbl_c_SystemAuditUpdate]
    [tbl_c_SystemAuditView]
    [tbl_c_SystemDefaultDelete]
    [tbl_c_SystemDefaultUpdate]
    [tbl_c_SystemDefaultUpdateSettingValue]
    [tbl_c_SystemDefaultView]
    [tbl_c_TableDelete]
    [tbl_c_TableUpdate]
    [tbl_c_TableView]
    [tbl_c_UserDelete]
    [tbl_c_UserLoginKeyDelete]
    [tbl_c_UserLoginKeyUpdate]
    [tbl_c_UserLoginKeyView]
    [tbl_c_UserPermissionDelete]
    [tbl_c_UserPermissionUpdate]
    [tbl_c_UserPermissionView]
    [tbl_c_UserStatusDelete]
    [tbl_c_UserStatusUpdate]
    [tbl_c_UserStatusView]
    [tbl_c_UserUpdate]
    [tbl_c_UserUpdateApplications]
    [tbl_c_UserUpdateApproval]
    [tbl_c_UserUpdateComments]
    [tbl_c_UserUpdateLastSuccessfulLogin]
    [tbl_c_UserUpdateLoggedInIP]
    [tbl_c_UserUpdatePasswordHashed]
    [tbl_c_UserUpdatePIN]
    [tbl_c_UserUpdateSecurityQuestion1Response]
    [tbl_c_UserUpdateSecurityQuestion2Response]
    [tbl_c_UserUpdateSecurityQuestion3Response]
    [tbl_c_UserView]
    [tbl_CustomerDebtDelete]
    [tbl_CustomerDebtUpdate]
    [tbl_CustomerDebtView]
    [tbl_CustomerDelete]
    [tbl_CustomerUpdate]
    [tbl_CustomerView]
    [tbl_DeliveryDelete]
    [tbl_DeliveryUpdate]
    [tbl_DeliveryView]
    [tbl_OrderHeaderDelete]
    [tbl_OrderHeaderUpdate]
    [tbl_OrderHeaderView]
    [tbl_OrderLineDelete]
    [tbl_OrderLineUpdate]
    [tbl_OrderLineView]
    [tbl_ProductDelete]
    [tbl_ProductPriceDelete]
    [tbl_ProductPriceHistDelete]
    [tbl_ProductPriceHistUpdate]
    [tbl_ProductPriceHistView]
    [tbl_ProductPriceUpdate]
    [tbl_ProductPriceView]
    [tbl_ProductUpdate]
    [tbl_ProductView]
    [tbl_SupplierOrderDelete]
    [tbl_SupplierOrderUpdate]
    [tbl_SupplierOrderView]
    [viw_c_IndexFragmentationDelete]
    [viw_c_IndexFragmentationUpdate]
    [viw_c_IndexFragmentationView]
    [viw_c_TableSizeDelete]
    [viw_c_TableSizeUpdate]
    [viw_c_TableSizeView]
    [viw_vw_OrderLineCalcDataDelete]
    [viw_vw_OrderLineCalcDataUpdate]
    [viw_vw_OrderLineCalcDataView]
    [viw_vw_OrderLineCalcDelete]
    [viw_vw_OrderLineCalcUpdate]
    [viw_vw_OrderLineCalcView]
    [viw_vw_OrderLineFullDataDelete]
    [viw_vw_OrderLineFullDataUpdate]
    [viw_vw_OrderLineFullDataView]
    [viw_vw_OrderLineFullDelete]
    [viw_vw_OrderLineFullUpdate]
    [viw_vw_OrderLineFullView]
    [viw_vwOrderLineCalcDelete]
    [viw_vwOrderLineCalcUpdate]
    [viw_vwOrderLineCalcView]
    [viw_vwOrderLineFullDelete]
    [viw_vwOrderLineFullUpdate]
    [viw_vwOrderLineFullView]
  End Enum
  Public Enum enmSystemDefaultType
    UD
    [Bit]
    [Decimal]
    [Encrypted]
    [Enum]
    [Integer]
    [String]
  End Enum
  Public Enum enmUserIdentificationModel
    UD
    [ByApplicationUser]
    [ByDomainGroup]
    [ByDomainUser]
  End Enum
  Public Enum enmUserIdentityType
    UD
    [c_User]
    [Customer]
    [Global]
  End Enum
  Public Enum enmWildCardType
    UD
    [After]
    [Before]
    [BeforeAndAfter]
    [BeforeAndAfterAndBetweenEachLetter]
    [None]
  End Enum

  'ComboList
  Public Enum enmComboListType 
    UD 
    [ccBeehiveBuyerTrackingDefaultByID] 
    [ccBeehiveBuyerTrackingForCustomerDefaultByID] 
    [ccCustomerDefaultByID] 
    [ccCustomerDebtDefaultByID] 
    [ccCustomerDebtForCustomerDefaultByID] 
    [ccDeliveryDefaultByID] 
    [ccOrderHeaderDefaultByID] 
    [ccOrderHeaderForCustomerDefaultByID] 
    [ccOrderLineDefaultByID] 
    [ccProductDefaultByID] 
    [ccProductPriceDefaultByID] 
    [ccProductPriceHistDefaultByID] 
    [ccSupplierOrderDefaultByID] 
    [c_AlertMessageDefaultByID] 
    [c_EnumerationDefaultByID] 
    [c_IndexFragmentationDefaultByID] 
    [c_JobDefaultByID] 
    [c_LanguageDefaultByID] 
    [c_LoggedAlertDefaultByID] 
    [c_LoggedAlertForAffectedUserDefaultByID] 
    [c_LoggedLoginDefaultByID] 
    [c_LookupDefaultByID] 
    [c_MFADefaultByID] 
    [c_MFAForUserDefaultByID] 
    [c_ObjectToTranslateDefaultByID] 
    [c_ProcessDefaultByID] 
    [c_RoleDefaultByID] 
    [c_SystemDefaultDefaultByID] 
    [c_TableDefaultByID] 
    [c_TableSizeDefaultByID] 
    [c_UserDefaultByID] 
    [c_UserDefaultNoMasterByID] 
    [c_RoleNonBaseDefaultByID] 
    [c_RoleWithBaseDefaultByID] 
    [c_RoleWithBaseAndMasterDefaultByID] 
    [c_RoleWithBaseNoSysAdminDefaultByID] 
    [SampleComboQueryOne] 
    [SampleComboQueryTwo] 
    [ccTestComboListFillManual] 
  End Enum 
  
  Public Shared Function TranslateEnmEnum(ByVal vString As String) As enmEnum 
    Dim pEnum As enmEnum 
    
    If vString Is Nothing Then Return enmEnum.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant() 
    Select Case pStrg 
      Case "accountantmethod" 
        pEnum = enmEnum.AccountantMethod 
      Case "applicationauthenticationtows" 
        pEnum = enmEnum.ApplicationAuthenticationToWS 
      Case "authenticationmethod" 
        pEnum = enmEnum.AuthenticationMethod 
      Case "category" 
        pEnum = enmEnum.Category 
      Case "ccapicompressionmode" 
        pEnum = enmEnum.ccAPICompressionMode 
      Case "combolistkeytype" 
        pEnum = enmEnum.ComboListKeyType 
      Case "customertype" 
        pEnum = enmEnum.CustomerType 
      Case "debtstatus" 
        pEnum = enmEnum.DebtStatus 
      Case "deliveryday" 
        pEnum = enmEnum.DeliveryDay 
      Case "deliverymethod" 
        pEnum = enmEnum.DeliveryMethod 
      Case "deliverystatus" 
        pEnum = enmEnum.DeliveryStatus 
      Case "emailstatus" 
        pEnum = enmEnum.EmailStatus 
      Case "faultseverity" 
        pEnum = enmEnum.FaultSeverity 
      Case "faulttype" 
        pEnum = enmEnum.FaultType 
      Case "filldirection" 
        pEnum = enmEnum.FillDirection 
      Case "importance" 
        pEnum = enmEnum.Importance 
      Case "jobalerttype" 
        pEnum = enmEnum.JobAlertType 
      Case "jobstatus" 
        pEnum = enmEnum.JobStatus 
      Case "jobtype" 
        pEnum = enmEnum.JobType 
      Case "language" 
        pEnum = enmEnum.Language 
      Case "loadparent" 
        pEnum = enmEnum.LoadParent 
      Case "lookup" 
        pEnum = enmEnum.Lookup 
      Case "messagingmode" 
        pEnum = enmEnum.MessagingMode 
      Case "objectstatus" 
        pEnum = enmEnum.ObjectStatus 
      Case "objecttype" 
        pEnum = enmEnum.ObjectType 
      Case "orderstatus" 
        pEnum = enmEnum.OrderStatus 
      Case "paymentmethod" 
        pEnum = enmEnum.PaymentMethod 
      Case "paymentstatus" 
        pEnum = enmEnum.PaymentStatus 
      Case "process" 
        pEnum = enmEnum.Process 
      Case "systemdefaulttype" 
        pEnum = enmEnum.SystemDefaultType 
      Case "useridentificationmodel" 
        pEnum = enmEnum.UserIdentificationModel 
      Case "useridentitytype" 
        pEnum = enmEnum.UserIdentityType 
      Case "wildcardtype" 
        pEnum = enmEnum.WildCardType 
      Case Else 
        pEnum = enmEnum.UD 
    End Select 
    
    Return pEnum 
  End Function 
  
  Public Shared Function TranslateToEnum(ByVal vEnumTypeName As String, ByVal vEnumValue As String) As [Enum] 
    Dim pEnum As [Enum] 
    
    If String.IsNullOrWhiteSpace(vEnumTypeName) OrElse String.IsNullOrWhiteSpace(vEnumValue) Then Return enmEnum.UD 
    vEnumTypeName = vEnumTypeName.Trim 
    vEnumValue = vEnumValue.Trim 
 
    If vEnumTypeName.IndexOf("+") >= 0 Then vEnumTypeName = vEnumTypeName.Split("+"c)(1) 
    If vEnumTypeName.StartsWith("enm", StringComparison.OrdinalIgnoreCase) Then vEnumTypeName = vEnumTypeName.Substring(3) 
    
    Dim pStrg As String = vEnumTypeName.ToLowerInvariant() 
    Select Case pStrg 
      Case "accountantmethod" 
        pEnum = TranslateEnmAccountantMethod(vEnumValue) 
      Case "applicationauthenticationtows" 
        pEnum = TranslateEnmApplicationAuthenticationToWS(vEnumValue) 
      Case "authenticationmethod" 
        pEnum = TranslateEnmAuthenticationMethod(vEnumValue) 
      Case "category" 
        pEnum = TranslateEnmCategory(vEnumValue) 
      Case "ccapicompressionmode" 
        pEnum = TranslateEnmccAPICompressionMode(vEnumValue) 
      Case "combolistkeytype" 
        pEnum = TranslateEnmComboListKeyType(vEnumValue) 
      Case "customertype" 
        pEnum = TranslateEnmCustomerType(vEnumValue) 
      Case "debtstatus" 
        pEnum = TranslateEnmDebtStatus(vEnumValue) 
      Case "deliveryday" 
        pEnum = TranslateEnmDeliveryDay(vEnumValue) 
      Case "deliverymethod" 
        pEnum = TranslateEnmDeliveryMethod(vEnumValue) 
      Case "deliverystatus" 
        pEnum = TranslateEnmDeliveryStatus(vEnumValue) 
      Case "emailstatus" 
        pEnum = TranslateEnmEmailStatus(vEnumValue) 
      Case "faultseverity" 
        pEnum = TranslateEnmFaultSeverity(vEnumValue) 
      Case "faulttype" 
        pEnum = TranslateEnmFaultType(vEnumValue) 
      Case "filldirection" 
        pEnum = TranslateEnmFillDirection(vEnumValue) 
      Case "importance" 
        pEnum = TranslateEnmImportance(vEnumValue) 
      Case "jobalerttype" 
        pEnum = TranslateEnmJobAlertType(vEnumValue) 
      Case "jobstatus" 
        pEnum = TranslateEnmJobStatus(vEnumValue) 
      Case "jobtype" 
        pEnum = TranslateEnmJobType(vEnumValue) 
      Case "language" 
        pEnum = TranslateEnmLanguage(vEnumValue) 
      Case "loadparent" 
        pEnum = TranslateEnmLoadParent(vEnumValue) 
      Case "lookup" 
        pEnum = TranslateEnmLookup(vEnumValue) 
      Case "messagingmode" 
        pEnum = TranslateEnmMessagingMode(vEnumValue) 
      Case "objectstatus" 
        pEnum = TranslateEnmObjectStatus(vEnumValue) 
      Case "objecttype" 
        pEnum = TranslateEnmObjectType(vEnumValue) 
      Case "orderstatus" 
        pEnum = TranslateEnmOrderStatus(vEnumValue) 
      Case "paymentmethod" 
        pEnum = TranslateEnmPaymentMethod(vEnumValue) 
      Case "paymentstatus" 
        pEnum = TranslateEnmPaymentStatus(vEnumValue) 
      Case "process" 
        pEnum = TranslateEnmProcess(vEnumValue) 
      Case "systemdefaulttype" 
        pEnum = TranslateEnmSystemDefaultType(vEnumValue) 
      Case "useridentificationmodel" 
        pEnum = TranslateEnmUserIdentificationModel(vEnumValue) 
      Case "useridentitytype" 
        pEnum = TranslateEnmUserIdentityType(vEnumValue) 
      Case "wildcardtype" 
        pEnum = TranslateEnmWildCardType(vEnumValue) 
      Case Else 
        pEnum = enmEnum.UD 
    End Select 
    
    Return pEnum 
  End Function 
  
  Public Shared Function TranslateEnmAccountantMethod(ByVal vString As String) As enmAccountantMethod
    Dim pAccountantMethod As enmAccountantMethod
    
    If vString Is Nothing Then Return enmAccountantMethod.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "email"
        pAccountantMethod = enmAccountantMethod.Email
      Case "mail"
        pAccountantMethod = enmAccountantMethod.Mail
      Case Else
        pAccountantMethod = enmAccountantMethod.UD
    End Select
    
    Return pAccountantMethod
  End Function
  Public Shared Function TranslateEnmApplicationAuthenticationToWS(ByVal vString As String) As enmApplicationAuthenticationToWS
    Dim pApplicationAuthenticationToWS As enmApplicationAuthenticationToWS
    
    If vString Is Nothing Then Return enmApplicationAuthenticationToWS.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "activeusercredentials"
        pApplicationAuthenticationToWS = enmApplicationAuthenticationToWS.ActiveUserCredentials
      Case "applicationcredentials"
        pApplicationAuthenticationToWS = enmApplicationAuthenticationToWS.ApplicationCredentials
      Case "none"
        pApplicationAuthenticationToWS = enmApplicationAuthenticationToWS.None
      Case "specificusercredentials"
        pApplicationAuthenticationToWS = enmApplicationAuthenticationToWS.SpecificUserCredentials
      Case Else
        pApplicationAuthenticationToWS = enmApplicationAuthenticationToWS.UD
    End Select
    
    Return pApplicationAuthenticationToWS
  End Function
  Public Shared Function TranslateEnmAuthenticationMethod(ByVal vString As String) As enmAuthenticationMethod
    Dim pAuthenticationMethod As enmAuthenticationMethod
    
    If vString Is Nothing Then Return enmAuthenticationMethod.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "namepassword"
        pAuthenticationMethod = enmAuthenticationMethod.NamePassword
      Case "onetimepassword"
        pAuthenticationMethod = enmAuthenticationMethod.OneTimePassword
      Case "singlevenue2fa"
        pAuthenticationMethod = enmAuthenticationMethod.SingleVenue2FA
      Case "twofactorauthentication"
        pAuthenticationMethod = enmAuthenticationMethod.TwoFactorAuthentication
      Case Else
        pAuthenticationMethod = enmAuthenticationMethod.UD
    End Select
    
    Return pAuthenticationMethod
  End Function
  Public Shared Function TranslateEnmCategory(ByVal vString As String) As enmCategory
    Dim pCategory As enmCategory
    
    If vString Is Nothing Then Return enmCategory.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "beehives"
        pCategory = enmCategory.Beehives
      Case "biolife"
        pCategory = enmCategory.Biolife
      Case "biologicalpest"
        pCategory = enmCategory.BiologicalPest
      Case "biopest"
        pCategory = enmCategory.BioPest
      Case "biotime"
        pCategory = enmCategory.Biotime
      Case "butano"
        pCategory = enmCategory.Butano
      Case "canrise"
        pCategory = enmCategory.Canrise
      Case "delivery"
        pCategory = enmCategory.Delivery
      Case "equipment"
        pCategory = enmCategory.Equipment
      Case "general"
        pCategory = enmCategory.General
      Case "preparations"
        pCategory = enmCategory.Preparations
      Case "shmoolik"
        pCategory = enmCategory.Shmoolik
      Case "stock"
        pCategory = enmCategory.Stock
      Case "traps"
        pCategory = enmCategory.Traps
      Case Else
        pCategory = enmCategory.UD
    End Select
    
    Return pCategory
  End Function
  Public Shared Function TranslateEnmccAPICompressionMode(ByVal vString As String) As enmccAPICompressionMode
    Dim pccAPICompressionMode As enmccAPICompressionMode
    
    If vString Is Nothing Then Return enmccAPICompressionMode.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "deflatetargcc"
        pccAPICompressionMode = enmccAPICompressionMode.DeflateTargCC
      Case "gziptargcc"
        pccAPICompressionMode = enmccAPICompressionMode.GzipTargCC
      Case "iis"
        pccAPICompressionMode = enmccAPICompressionMode.IIS
      Case "none"
        pccAPICompressionMode = enmccAPICompressionMode.None
      Case Else
        pccAPICompressionMode = enmccAPICompressionMode.UD
    End Select
    
    Return pccAPICompressionMode
  End Function
  Public Shared Function TranslateEnmComboListKeyType(ByVal vString As String) As enmComboListKeyType
    Dim pComboListKeyType As enmComboListKeyType
    
    If vString Is Nothing Then Return enmComboListKeyType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "enum"
        pComboListKeyType = enmComboListKeyType.Enum
      Case "integer"
        pComboListKeyType = enmComboListKeyType.Integer
      Case "long"
        pComboListKeyType = enmComboListKeyType.Long
      Case "object"
        pComboListKeyType = enmComboListKeyType.Object
      Case "string"
        pComboListKeyType = enmComboListKeyType.String
      Case Else
        pComboListKeyType = enmComboListKeyType.UD
    End Select
    
    Return pComboListKeyType
  End Function
  Public Shared Function TranslateEnmCustomerType(ByVal vString As String) As enmCustomerType
    Dim pCustomerType As enmCustomerType
    
    If vString Is Nothing Then Return enmCustomerType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "farm"
        pCustomerType = enmCustomerType.Farm
      Case "farmer"
        pCustomerType = enmCustomerType.Farmer
      Case "hydro"
        pCustomerType = enmCustomerType.Hydro
      Case "private"
        pCustomerType = enmCustomerType.Private
      Case "retail"
        pCustomerType = enmCustomerType.Retail
      Case Else
        pCustomerType = enmCustomerType.UD
    End Select
    
    Return pCustomerType
  End Function
  Public Shared Function TranslateEnmDebtStatus(ByVal vString As String) As enmDebtStatus
    Dim pDebtStatus As enmDebtStatus
    
    If vString Is Nothing Then Return enmDebtStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "cancelled"
        pDebtStatus = enmDebtStatus.Cancelled
      Case "open"
        pDebtStatus = enmDebtStatus.Open
      Case "paid"
        pDebtStatus = enmDebtStatus.Paid
      Case "partiallypaid"
        pDebtStatus = enmDebtStatus.PartiallyPaid
      Case "writtenoff"
        pDebtStatus = enmDebtStatus.WrittenOff
      Case Else
        pDebtStatus = enmDebtStatus.UD
    End Select
    
    Return pDebtStatus
  End Function
  Public Shared Function TranslateEnmDeliveryDay(ByVal vString As String) As enmDeliveryDay
    Dim pDeliveryDay As enmDeliveryDay
    
    If vString Is Nothing Then Return enmDeliveryDay.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "friday"
        pDeliveryDay = enmDeliveryDay.Friday
      Case "monday"
        pDeliveryDay = enmDeliveryDay.Monday
      Case "saturday"
        pDeliveryDay = enmDeliveryDay.Saturday
      Case "sunday"
        pDeliveryDay = enmDeliveryDay.Sunday
      Case "thursday"
        pDeliveryDay = enmDeliveryDay.Thursday
      Case "tuesday"
        pDeliveryDay = enmDeliveryDay.Tuesday
      Case "wednesday"
        pDeliveryDay = enmDeliveryDay.Wednesday
      Case Else
        pDeliveryDay = enmDeliveryDay.UD
    End Select
    
    Return pDeliveryDay
  End Function
  Public Shared Function TranslateEnmDeliveryMethod(ByVal vString As String) As enmDeliveryMethod
    Dim pDeliveryMethod As enmDeliveryMethod
    
    If vString Is Nothing Then Return enmDeliveryMethod.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "biobee"
        pDeliveryMethod = enmDeliveryMethod.Biobee
      Case "biotour"
        pDeliveryMethod = enmDeliveryMethod.BioTour
      Case "greenart"
        pDeliveryMethod = enmDeliveryMethod.GreenArt
      Case "gvulot"
        pDeliveryMethod = enmDeliveryMethod.Gvulot
      Case "liorcarmiel"
        pDeliveryMethod = enmDeliveryMethod.LiorCarmiel
      Case "netzach"
        pDeliveryMethod = enmDeliveryMethod.Netzach
      Case "nodelivery"
        pDeliveryMethod = enmDeliveryMethod.NoDelivery
      Case "other"
        pDeliveryMethod = enmDeliveryMethod.Other
      Case "paran"
        pDeliveryMethod = enmDeliveryMethod.Paran
      Case "ptzael"
        pDeliveryMethod = enmDeliveryMethod.Ptzael
      Case "selfpickup"
        pDeliveryMethod = enmDeliveryMethod.SelfPickup
      Case "shmoolik"
        pDeliveryMethod = enmDeliveryMethod.Shmoolik
      Case "tzofar"
        pDeliveryMethod = enmDeliveryMethod.Tzofar
      Case "warehousekq"
        pDeliveryMethod = enmDeliveryMethod.WarehouseKQ
      Case "elkana"
        pDeliveryMethod = enmDeliveryMethod.Elkana
      Case "ydm"
        pDeliveryMethod = enmDeliveryMethod.YDM
      Case "beertuvia"
        pDeliveryMethod = enmDeliveryMethod.BeerTuvia
      Case "bgabriel"
        pDeliveryMethod = enmDeliveryMethod.BGabriel
      Case Else
        pDeliveryMethod = enmDeliveryMethod.UD
    End Select
    
    Return pDeliveryMethod
  End Function
  Public Shared Function TranslateEnmDeliveryStatus(ByVal vString As String) As enmDeliveryStatus
    Dim pDeliveryStatus As enmDeliveryStatus
    
    If vString Is Nothing Then Return enmDeliveryStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "athub"
        pDeliveryStatus = enmDeliveryStatus.AtHub
      Case "cancelled"
        pDeliveryStatus = enmDeliveryStatus.Cancelled
      Case "delivered"
        pDeliveryStatus = enmDeliveryStatus.Delivered
      Case "failed"
        pDeliveryStatus = enmDeliveryStatus.Failed
      Case "intransit"
        pDeliveryStatus = enmDeliveryStatus.InTransit
      Case "ordered"
        pDeliveryStatus = enmDeliveryStatus.Ordered
      Case "pending"
        pDeliveryStatus = enmDeliveryStatus.Pending
      Case "received"
        pDeliveryStatus = enmDeliveryStatus.Received
      Case Else
        pDeliveryStatus = enmDeliveryStatus.UD
    End Select
    
    Return pDeliveryStatus
  End Function
  Public Shared Function TranslateEnmEmailStatus(ByVal vString As String) As enmEmailStatus
    Dim pEmailStatus As enmEmailStatus
    
    If vString Is Nothing Then Return enmEmailStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "draft"
        pEmailStatus = enmEmailStatus.Draft
      Case "failed"
        pEmailStatus = enmEmailStatus.Failed
      Case "sent"
        pEmailStatus = enmEmailStatus.Sent
      Case Else
        pEmailStatus = enmEmailStatus.UD
    End Select
    
    Return pEmailStatus
  End Function
  Public Shared Function TranslateEnmFaultSeverity(ByVal vString As String) As enmFaultSeverity
    Dim pFaultSeverity As enmFaultSeverity
    
    If vString Is Nothing Then Return enmFaultSeverity.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "alert"
        pFaultSeverity = enmFaultSeverity.Alert
      Case "email"
        pFaultSeverity = enmFaultSeverity.Email
      Case "info"
        pFaultSeverity = enmFaultSeverity.Info
      Case "logonly"
        pFaultSeverity = enmFaultSeverity.LogOnly
      Case "sms"
        pFaultSeverity = enmFaultSeverity.SMS
      Case Else
        pFaultSeverity = enmFaultSeverity.UD
    End Select
    
    Return pFaultSeverity
  End Function
  Public Shared Function TranslateEnmFaultType(ByVal vString As String) As enmFaultType
    Dim pFaultType As enmFaultType
    
    If vString Is Nothing Then Return enmFaultType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "business"
        pFaultType = enmFaultType.Business
      Case "security"
        pFaultType = enmFaultType.Security
      Case "system"
        pFaultType = enmFaultType.System
      Case Else
        pFaultType = enmFaultType.UD
    End Select
    
    Return pFaultType
  End Function
  Public Shared Function TranslateEnmFillDirection(ByVal vString As String) As enmFillDirection
    Dim pFillDirection As enmFillDirection
    
    If vString Is Nothing Then Return enmFillDirection.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "asc"
        pFillDirection = enmFillDirection.ASC
      Case "desc"
        pFillDirection = enmFillDirection.DESC
      Case Else
        pFillDirection = enmFillDirection.UD
    End Select
    
    Return pFillDirection
  End Function
  Public Shared Function TranslateEnmImportance(ByVal vString As String) As enmImportance
    Dim pImportance As enmImportance
    
    If vString Is Nothing Then Return enmImportance.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "high"
        pImportance = enmImportance.High
      Case "low"
        pImportance = enmImportance.Low
      Case "medium"
        pImportance = enmImportance.Medium
      Case Else
        pImportance = enmImportance.UD
    End Select
    
    Return pImportance
  End Function
  Public Shared Function TranslateEnmJobAlertType(ByVal vString As String) As enmJobAlertType
    Dim pJobAlertType As enmJobAlertType
    
    If vString Is Nothing Then Return enmJobAlertType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "email"
        pJobAlertType = enmJobAlertType.email
      Case "pager"
        pJobAlertType = enmJobAlertType.Pager
      Case "sms"
        pJobAlertType = enmJobAlertType.SMS
      Case Else
        pJobAlertType = enmJobAlertType.UD
    End Select
    
    Return pJobAlertType
  End Function
  Public Shared Function TranslateEnmJobStatus(ByVal vString As String) As enmJobStatus
    Dim pJobStatus As enmJobStatus
    
    If vString Is Nothing Then Return enmJobStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "failure"
        pJobStatus = enmJobStatus.Failure
      Case "inprocess"
        pJobStatus = enmJobStatus.InProcess
      Case "missed"
        pJobStatus = enmJobStatus.Missed
      Case "success"
        pJobStatus = enmJobStatus.Success
      Case "warning"
        pJobStatus = enmJobStatus.Warning
      Case Else
        pJobStatus = enmJobStatus.UD
    End Select
    
    Return pJobStatus
  End Function
  Public Shared Function TranslateEnmJobType(ByVal vString As String) As enmJobType
    Dim pJobType As enmJobType
    
    If vString Is Nothing Then Return enmJobType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "annually"
        pJobType = enmJobType.Annually
      Case "cyclicday"
        pJobType = enmJobType.CyclicDay
      Case "cyclichour"
        pJobType = enmJobType.CyclicHour
      Case "cyclicmin"
        pJobType = enmJobType.CyclicMin
      Case "cyclicsec"
        pJobType = enmJobType.CyclicSec
      Case "daily"
        pJobType = enmJobType.Daily
      Case "monthly"
        pJobType = enmJobType.Monthly
      Case "oneoff"
        pJobType = enmJobType.OneOff
      Case "weekly"
        pJobType = enmJobType.Weekly
      Case Else
        pJobType = enmJobType.UD
    End Select
    
    Return pJobType
  End Function
  Public Shared Function TranslateEnmLanguage(ByVal vString As String) As enmLanguage
    Dim pLanguage As enmLanguage
    
    If vString Is Nothing Then Return enmLanguage.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "af"
        pLanguage = enmLanguage.af
      Case "am"
        pLanguage = enmLanguage.am
      Case "ar"
        pLanguage = enmLanguage.ar
      Case "bg"
        pLanguage = enmLanguage.bg
      Case "cr"
        pLanguage = enmLanguage.cr
      Case "cs"
        pLanguage = enmLanguage.cs
      Case "cy"
        pLanguage = enmLanguage.cy
      Case "da"
        pLanguage = enmLanguage.da
      Case "de"
        pLanguage = enmLanguage.de
      Case "el"
        pLanguage = enmLanguage.el
      Case "en"
        pLanguage = enmLanguage.en
      Case "eo"
        pLanguage = enmLanguage.eo
      Case "es"
        pLanguage = enmLanguage.es
      Case "et"
        pLanguage = enmLanguage.et
      Case "fa"
        pLanguage = enmLanguage.fa
      Case "fi"
        pLanguage = enmLanguage.fi
      Case "fr"
        pLanguage = enmLanguage.fr
      Case "ga"
        pLanguage = enmLanguage.ga
      Case "gd"
        pLanguage = enmLanguage.gd
      Case "he"
        pLanguage = enmLanguage.he
      Case "hr"
        pLanguage = enmLanguage.hr
      Case "hu"
        pLanguage = enmLanguage.hu
      Case "hy"
        pLanguage = enmLanguage.hy
      Case "id"
        pLanguage = enmLanguage.id
      Case "it"
        pLanguage = enmLanguage.it
      Case "iu"
        pLanguage = enmLanguage.iu
      Case "ja"
        pLanguage = enmLanguage.ja
      Case "ko"
        pLanguage = enmLanguage.ko
      Case "nl"
        pLanguage = enmLanguage.nl
      Case "no"
        pLanguage = enmLanguage.no
      Case "pl"
        pLanguage = enmLanguage.pl
      Case "pt"
        pLanguage = enmLanguage.pt
      Case "ro"
        pLanguage = enmLanguage.ro
      Case "ru"
        pLanguage = enmLanguage.ru
      Case "sq"
        pLanguage = enmLanguage.sq
      Case "sv"
        pLanguage = enmLanguage.sv
      Case "tr"
        pLanguage = enmLanguage.tr
      Case "uk"
        pLanguage = enmLanguage.uk
      Case "vi"
        pLanguage = enmLanguage.vi
      Case "yi"
        pLanguage = enmLanguage.yi
      Case "zh"
        pLanguage = enmLanguage.zh
      Case "zu"
        pLanguage = enmLanguage.zu
      Case Else
        pLanguage = enmLanguage.UD
    End Select
    
    Return pLanguage
  End Function
  Public Shared Function TranslateEnmLoadParent(ByVal vString As String) As enmLoadParent
    Dim pLoadParent As enmLoadParent
    
    If vString Is Nothing Then Return enmLoadParent.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "donotload"
        pLoadParent = enmLoadParent.DoNotLoad
      Case "entireobject"
        pLoadParent = enmLoadParent.EntireObject
      Case "textonly"
        pLoadParent = enmLoadParent.TextOnly
      Case Else
        pLoadParent = enmLoadParent.UD
    End Select
    
    Return pLoadParent
  End Function
  Public Shared Function TranslateEnmLookup(ByVal vString As String) As enmLookup
    Dim pLookup As enmLookup
    
    If vString Is Nothing Then Return enmLookup.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "generic"
        pLookup = enmLookup.Generic
      Case "job"
        pLookup = enmLookup.Job
      Case "jobrunner"
        pLookup = enmLookup.JobRunner
      Case "securityquestion"
        pLookup = enmLookup.SecurityQuestion
      Case "useridentitytype"
        pLookup = enmLookup.UserIdentityType
      Case "useridentitytypename"
        pLookup = enmLookup.UserIdentityTypeName
      Case Else
        pLookup = enmLookup.UD
    End Select
    
    Return pLookup
  End Function
  Public Shared Function TranslateEnmMessagingMode(ByVal vString As String) As enmMessagingMode
    Dim pMessagingMode As enmMessagingMode
    
    If vString Is Nothing Then Return enmMessagingMode.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "email"
        pMessagingMode = enmMessagingMode.Email
      Case "sms"
        pMessagingMode = enmMessagingMode.SMS
      Case Else
        pMessagingMode = enmMessagingMode.UD
    End Select
    
    Return pMessagingMode
  End Function
  Public Shared Function TranslateEnmObjectStatus(ByVal vString As String) As enmObjectStatus
    Dim pObjectStatus As enmObjectStatus
    
    If vString Is Nothing Then Return enmObjectStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "clean"
        pObjectStatus = enmObjectStatus.Clean
      Case "deleted"
        pObjectStatus = enmObjectStatus.Deleted
      Case "dirty"
        pObjectStatus = enmObjectStatus.Dirty
      Case "new"
        pObjectStatus = enmObjectStatus.New
      Case Else
        pObjectStatus = enmObjectStatus.UD
    End Select
    
    Return pObjectStatus
  End Function
  Public Shared Function TranslateEnmObjectType(ByVal vString As String) As enmObjectType
    Dim pObjectType As enmObjectType
    
    If vString Is Nothing Then Return enmObjectType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "system"
        pObjectType = enmObjectType.System
      Case "tabledata"
        pObjectType = enmObjectType.TableData
      Case "tablefieldname"
        pObjectType = enmObjectType.TableFieldName
      Case "ui"
        pObjectType = enmObjectType.UI
      Case Else
        pObjectType = enmObjectType.UD
    End Select
    
    Return pObjectType
  End Function
  Public Shared Function TranslateEnmOrderStatus(ByVal vString As String) As enmOrderStatus
    Dim pOrderStatus As enmOrderStatus
    
    If vString Is Nothing Then Return enmOrderStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "cancelled"
        pOrderStatus = enmOrderStatus.Cancelled
      Case "completed"
        pOrderStatus = enmOrderStatus.Completed
      Case "inprogress"
        pOrderStatus = enmOrderStatus.InProgress
      Case "new"
        pOrderStatus = enmOrderStatus.New
      Case "processing"
        pOrderStatus = enmOrderStatus.Processing
      Case "shipped"
        pOrderStatus = enmOrderStatus.Shipped
      Case Else
        pOrderStatus = enmOrderStatus.UD
    End Select
    
    Return pOrderStatus
  End Function
  Public Shared Function TranslateEnmPaymentMethod(ByVal vString As String) As enmPaymentMethod
    Dim pPaymentMethod As enmPaymentMethod
    
    If vString Is Nothing Then Return enmPaymentMethod.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "bitpaybox"
        pPaymentMethod = enmPaymentMethod.BitPaybox
      Case "cash"
        pPaymentMethod = enmPaymentMethod.Cash
      Case "check"
        pPaymentMethod = enmPaymentMethod.Check
      Case "credit"
        pPaymentMethod = enmPaymentMethod.Credit
      Case "creditcard"
        pPaymentMethod = enmPaymentMethod.CreditCard
      Case "transfer"
        pPaymentMethod = enmPaymentMethod.Transfer
      Case "webpayment"
        pPaymentMethod = enmPaymentMethod.WebPayment
      Case Else
        pPaymentMethod = enmPaymentMethod.UD
    End Select
    
    Return pPaymentMethod
  End Function
  Public Shared Function TranslateEnmPaymentStatus(ByVal vString As String) As enmPaymentStatus
    Dim pPaymentStatus As enmPaymentStatus
    
    If vString Is Nothing Then Return enmPaymentStatus.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "paid"
        pPaymentStatus = enmPaymentStatus.Paid
      Case "partiallypaid"
        pPaymentStatus = enmPaymentStatus.PartiallyPaid
      Case "pending"
        pPaymentStatus = enmPaymentStatus.Pending
      Case "unpaid"
        pPaymentStatus = enmPaymentStatus.Unpaid
      Case Else
        pPaymentStatus = enmPaymentStatus.UD
    End Select
    
    Return pPaymentStatus
  End Function
  Public Shared Function TranslateEnmProcess(ByVal vString As String) As enmProcess
    Dim pProcess As enmProcess
    
    If vString Is Nothing Then Return enmProcess.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "prc_backupdatabase"
        pProcess = enmProcess.prc_BackupDatabase
      Case "prc_createdefaultpermissionsfornewrolesandtables"
        pProcess = enmProcess.prc_CreateDefaultPermissionsForNewRolesAndTables
      Case "prc_deleteoldlogs"
        pProcess = enmProcess.prc_DeleteOldLogs
      Case "prc_dosample"
        pProcess = enmProcess.prc_DoSample
      Case "prc_dotasks"
        pProcess = enmProcess.prc_DoTasks
      Case "prc_ejectallusers"
        pProcess = enmProcess.prc_EjectAllUsers
      Case "prc_ejectnonmaster"
        pProcess = enmProcess.prc_EjectNonMaster
      Case "prc_getnextjobforrunner"
        pProcess = enmProcess.prc_GetNextJobForRunner
      Case "prc_getnextmanagedjobforrunner"
        pProcess = enmProcess.prc_GetNextManagedJobForRunner
      Case "prc_getspecificunmanagedjobforrunner"
        pProcess = enmProcess.prc_GetSpecificUnmanagedJobForRunner
      Case "prc_handleobjecttotranslate"
        pProcess = enmProcess.prc_HandleObjectToTranslate
      Case "prc_loginanonymously"
        pProcess = enmProcess.prc_LogInAnonymously
      Case "prc_loginbynamepwd"
        pProcess = enmProcess.prc_LogInByNamePwd
      Case "prc_loginbynetworkcredentials"
        pProcess = enmProcess.prc_LogInByNetworkCredentials
      Case "prc_logout"
        pProcess = enmProcess.prc_LogOut
      Case "prc_markjobascomplete"
        pProcess = enmProcess.prc_MarkJobAsComplete
      Case "prc_moveaudits"
        pProcess = enmProcess.prc_MoveAudits
      Case "prc_requestdatabasebackup"
        pProcess = enmProcess.prc_RequestDatabaseBackup
      Case "prc_requestindexreorganization"
        pProcess = enmProcess.prc_RequestIndexReorganization
      Case "prc_resetdefaultpermissions"
        pProcess = enmProcess.prc_ResetDefaultPermissions
      Case "prc_scanjobs"
        pProcess = enmProcess.prc_ScanJobs
      Case "prc_sendmail"
        pProcess = enmProcess.prc_SendMail
      Case "prc_setdefaultpermissionsforrole"
        pProcess = enmProcess.prc_SetDefaultPermissionsForRole
      Case "prc_setjobtonow"
        pProcess = enmProcess.prc_SetJobToNow
      Case "prc_sysadmin"
        pProcess = enmProcess.prc_SysAdmin
      Case "prc_writedatabasetoxml"
        pProcess = enmProcess.prc_WriteDatabaseToXML
      Case "tbl_beehivebuyertrackingdelete"
        pProcess = enmProcess.tbl_BeehiveBuyerTrackingDelete
      Case "tbl_beehivebuyertrackingupdate"
        pProcess = enmProcess.tbl_BeehiveBuyerTrackingUpdate
      Case "tbl_beehivebuyertrackingview"
        pProcess = enmProcess.tbl_BeehiveBuyerTrackingView
      Case "tbl_c_alertmessagedelete"
        pProcess = enmProcess.tbl_c_AlertMessageDelete
      Case "tbl_c_alertmessageupdate"
        pProcess = enmProcess.tbl_c_AlertMessageUpdate
      Case "tbl_c_alertmessageview"
        pProcess = enmProcess.tbl_c_AlertMessageView
      Case "tbl_c_auditindexeddelete"
        pProcess = enmProcess.tbl_c_AuditIndexedDelete
      Case "tbl_c_auditindexedupdate"
        pProcess = enmProcess.tbl_c_AuditIndexedUpdate
      Case "tbl_c_auditindexedview"
        pProcess = enmProcess.tbl_c_AuditIndexedView
      Case "tbl_c_enumerationdelete"
        pProcess = enmProcess.tbl_c_EnumerationDelete
      Case "tbl_c_enumerationupdate"
        pProcess = enmProcess.tbl_c_EnumerationUpdate
      Case "tbl_c_enumerationview"
        pProcess = enmProcess.tbl_c_EnumerationView
      Case "tbl_c_jobalertrecipientdelete"
        pProcess = enmProcess.tbl_c_JobAlertRecipientDelete
      Case "tbl_c_jobalertrecipientupdate"
        pProcess = enmProcess.tbl_c_JobAlertRecipientUpdate
      Case "tbl_c_jobalertrecipientview"
        pProcess = enmProcess.tbl_c_JobAlertRecipientView
      Case "tbl_c_jobdelete"
        pProcess = enmProcess.tbl_c_JobDelete
      Case "tbl_c_jobupdate"
        pProcess = enmProcess.tbl_c_JobUpdate
      Case "tbl_c_jobupdatesettonow"
        pProcess = enmProcess.tbl_c_JobUpdateSetToNow
      Case "tbl_c_jobview"
        pProcess = enmProcess.tbl_c_JobView
      Case "tbl_c_languagedelete"
        pProcess = enmProcess.tbl_c_LanguageDelete
      Case "tbl_c_languageupdate"
        pProcess = enmProcess.tbl_c_LanguageUpdate
      Case "tbl_c_languageview"
        pProcess = enmProcess.tbl_c_LanguageView
      Case "tbl_c_loggedalertdelete"
        pProcess = enmProcess.tbl_c_LoggedAlertDelete
      Case "tbl_c_loggedalertupdate"
        pProcess = enmProcess.tbl_c_LoggedAlertUpdate
      Case "tbl_c_loggedalertview"
        pProcess = enmProcess.tbl_c_LoggedAlertView
      Case "tbl_c_loggedjobdelete"
        pProcess = enmProcess.tbl_c_LoggedJobDelete
      Case "tbl_c_loggedjobupdate"
        pProcess = enmProcess.tbl_c_LoggedJobUpdate
      Case "tbl_c_loggedjobview"
        pProcess = enmProcess.tbl_c_LoggedJobView
      Case "tbl_c_loggedlogindelete"
        pProcess = enmProcess.tbl_c_LoggedLoginDelete
      Case "tbl_c_loggedloginupdate"
        pProcess = enmProcess.tbl_c_LoggedLoginUpdate
      Case "tbl_c_loggedloginview"
        pProcess = enmProcess.tbl_c_LoggedLoginView
      Case "tbl_c_loggedrequestdelete"
        pProcess = enmProcess.tbl_c_LoggedRequestDelete
      Case "tbl_c_loggedrequestupdate"
        pProcess = enmProcess.tbl_c_LoggedRequestUpdate
      Case "tbl_c_loggedrequestview"
        pProcess = enmProcess.tbl_c_LoggedRequestView
      Case "tbl_c_lookupdelete"
        pProcess = enmProcess.tbl_c_LookupDelete
      Case "tbl_c_lookupupdate"
        pProcess = enmProcess.tbl_c_LookupUpdate
      Case "tbl_c_lookupview"
        pProcess = enmProcess.tbl_c_LookupView
      Case "tbl_c_maildelete"
        pProcess = enmProcess.tbl_c_MailDelete
      Case "tbl_c_mailupdate"
        pProcess = enmProcess.tbl_c_MailUpdate
      Case "tbl_c_mailview"
        pProcess = enmProcess.tbl_c_MailView
      Case "tbl_c_mfadelete"
        pProcess = enmProcess.tbl_c_MFADelete
      Case "tbl_c_mfaupdate"
        pProcess = enmProcess.tbl_c_MFAUpdate
      Case "tbl_c_mfaview"
        pProcess = enmProcess.tbl_c_MFAView
      Case "tbl_c_objecttotranslatedelete"
        pProcess = enmProcess.tbl_c_ObjectToTranslateDelete
      Case "tbl_c_objecttotranslateupdate"
        pProcess = enmProcess.tbl_c_ObjectToTranslateUpdate
      Case "tbl_c_objecttotranslateview"
        pProcess = enmProcess.tbl_c_ObjectToTranslateView
      Case "tbl_c_objecttranslationdelete"
        pProcess = enmProcess.tbl_c_ObjectTranslationDelete
      Case "tbl_c_objecttranslationupdate"
        pProcess = enmProcess.tbl_c_ObjectTranslationUpdate
      Case "tbl_c_objecttranslationview"
        pProcess = enmProcess.tbl_c_ObjectTranslationView
      Case "tbl_c_permissiondelete"
        pProcess = enmProcess.tbl_c_PermissionDelete
      Case "tbl_c_permissionupdate"
        pProcess = enmProcess.tbl_c_PermissionUpdate
      Case "tbl_c_permissionview"
        pProcess = enmProcess.tbl_c_PermissionView
      Case "tbl_c_processdelete"
        pProcess = enmProcess.tbl_c_ProcessDelete
      Case "tbl_c_processupdate"
        pProcess = enmProcess.tbl_c_ProcessUpdate
      Case "tbl_c_processview"
        pProcess = enmProcess.tbl_c_ProcessView
      Case "tbl_c_roledelete"
        pProcess = enmProcess.tbl_c_RoleDelete
      Case "tbl_c_roleupdate"
        pProcess = enmProcess.tbl_c_RoleUpdate
      Case "tbl_c_roleview"
        pProcess = enmProcess.tbl_c_RoleView
      Case "tbl_c_systemauditdelete"
        pProcess = enmProcess.tbl_c_SystemAuditDelete
      Case "tbl_c_systemauditupdate"
        pProcess = enmProcess.tbl_c_SystemAuditUpdate
      Case "tbl_c_systemauditview"
        pProcess = enmProcess.tbl_c_SystemAuditView
      Case "tbl_c_systemdefaultdelete"
        pProcess = enmProcess.tbl_c_SystemDefaultDelete
      Case "tbl_c_systemdefaultupdate"
        pProcess = enmProcess.tbl_c_SystemDefaultUpdate
      Case "tbl_c_systemdefaultupdatesettingvalue"
        pProcess = enmProcess.tbl_c_SystemDefaultUpdateSettingValue
      Case "tbl_c_systemdefaultview"
        pProcess = enmProcess.tbl_c_SystemDefaultView
      Case "tbl_c_tabledelete"
        pProcess = enmProcess.tbl_c_TableDelete
      Case "tbl_c_tableupdate"
        pProcess = enmProcess.tbl_c_TableUpdate
      Case "tbl_c_tableview"
        pProcess = enmProcess.tbl_c_TableView
      Case "tbl_c_userdelete"
        pProcess = enmProcess.tbl_c_UserDelete
      Case "tbl_c_userloginkeydelete"
        pProcess = enmProcess.tbl_c_UserLoginKeyDelete
      Case "tbl_c_userloginkeyupdate"
        pProcess = enmProcess.tbl_c_UserLoginKeyUpdate
      Case "tbl_c_userloginkeyview"
        pProcess = enmProcess.tbl_c_UserLoginKeyView
      Case "tbl_c_userpermissiondelete"
        pProcess = enmProcess.tbl_c_UserPermissionDelete
      Case "tbl_c_userpermissionupdate"
        pProcess = enmProcess.tbl_c_UserPermissionUpdate
      Case "tbl_c_userpermissionview"
        pProcess = enmProcess.tbl_c_UserPermissionView
      Case "tbl_c_userstatusdelete"
        pProcess = enmProcess.tbl_c_UserStatusDelete
      Case "tbl_c_userstatusupdate"
        pProcess = enmProcess.tbl_c_UserStatusUpdate
      Case "tbl_c_userstatusview"
        pProcess = enmProcess.tbl_c_UserStatusView
      Case "tbl_c_userupdate"
        pProcess = enmProcess.tbl_c_UserUpdate
      Case "tbl_c_userupdateapplications"
        pProcess = enmProcess.tbl_c_UserUpdateApplications
      Case "tbl_c_userupdateapproval"
        pProcess = enmProcess.tbl_c_UserUpdateApproval
      Case "tbl_c_userupdatecomments"
        pProcess = enmProcess.tbl_c_UserUpdateComments
      Case "tbl_c_userupdatelastsuccessfullogin"
        pProcess = enmProcess.tbl_c_UserUpdateLastSuccessfulLogin
      Case "tbl_c_userupdateloggedinip"
        pProcess = enmProcess.tbl_c_UserUpdateLoggedInIP
      Case "tbl_c_userupdatepasswordhashed"
        pProcess = enmProcess.tbl_c_UserUpdatePasswordHashed
      Case "tbl_c_userupdatepin"
        pProcess = enmProcess.tbl_c_UserUpdatePIN
      Case "tbl_c_userupdatesecurityquestion1response"
        pProcess = enmProcess.tbl_c_UserUpdateSecurityQuestion1Response
      Case "tbl_c_userupdatesecurityquestion2response"
        pProcess = enmProcess.tbl_c_UserUpdateSecurityQuestion2Response
      Case "tbl_c_userupdatesecurityquestion3response"
        pProcess = enmProcess.tbl_c_UserUpdateSecurityQuestion3Response
      Case "tbl_c_userview"
        pProcess = enmProcess.tbl_c_UserView
      Case "tbl_customerdebtdelete"
        pProcess = enmProcess.tbl_CustomerDebtDelete
      Case "tbl_customerdebtupdate"
        pProcess = enmProcess.tbl_CustomerDebtUpdate
      Case "tbl_customerdebtview"
        pProcess = enmProcess.tbl_CustomerDebtView
      Case "tbl_customerdelete"
        pProcess = enmProcess.tbl_CustomerDelete
      Case "tbl_customerupdate"
        pProcess = enmProcess.tbl_CustomerUpdate
      Case "tbl_customerview"
        pProcess = enmProcess.tbl_CustomerView
      Case "tbl_deliverydelete"
        pProcess = enmProcess.tbl_DeliveryDelete
      Case "tbl_deliveryupdate"
        pProcess = enmProcess.tbl_DeliveryUpdate
      Case "tbl_deliveryview"
        pProcess = enmProcess.tbl_DeliveryView
      Case "tbl_orderheaderdelete"
        pProcess = enmProcess.tbl_OrderHeaderDelete
      Case "tbl_orderheaderupdate"
        pProcess = enmProcess.tbl_OrderHeaderUpdate
      Case "tbl_orderheaderview"
        pProcess = enmProcess.tbl_OrderHeaderView
      Case "tbl_orderlinedelete"
        pProcess = enmProcess.tbl_OrderLineDelete
      Case "tbl_orderlineupdate"
        pProcess = enmProcess.tbl_OrderLineUpdate
      Case "tbl_orderlineview"
        pProcess = enmProcess.tbl_OrderLineView
      Case "tbl_productdelete"
        pProcess = enmProcess.tbl_ProductDelete
      Case "tbl_productpricedelete"
        pProcess = enmProcess.tbl_ProductPriceDelete
      Case "tbl_productpricehistdelete"
        pProcess = enmProcess.tbl_ProductPriceHistDelete
      Case "tbl_productpricehistupdate"
        pProcess = enmProcess.tbl_ProductPriceHistUpdate
      Case "tbl_productpricehistview"
        pProcess = enmProcess.tbl_ProductPriceHistView
      Case "tbl_productpriceupdate"
        pProcess = enmProcess.tbl_ProductPriceUpdate
      Case "tbl_productpriceview"
        pProcess = enmProcess.tbl_ProductPriceView
      Case "tbl_productupdate"
        pProcess = enmProcess.tbl_ProductUpdate
      Case "tbl_productview"
        pProcess = enmProcess.tbl_ProductView
      Case "tbl_supplierorderdelete"
        pProcess = enmProcess.tbl_SupplierOrderDelete
      Case "tbl_supplierorderupdate"
        pProcess = enmProcess.tbl_SupplierOrderUpdate
      Case "tbl_supplierorderview"
        pProcess = enmProcess.tbl_SupplierOrderView
      Case "viw_c_indexfragmentationdelete"
        pProcess = enmProcess.viw_c_IndexFragmentationDelete
      Case "viw_c_indexfragmentationupdate"
        pProcess = enmProcess.viw_c_IndexFragmentationUpdate
      Case "viw_c_indexfragmentationview"
        pProcess = enmProcess.viw_c_IndexFragmentationView
      Case "viw_c_tablesizedelete"
        pProcess = enmProcess.viw_c_TableSizeDelete
      Case "viw_c_tablesizeupdate"
        pProcess = enmProcess.viw_c_TableSizeUpdate
      Case "viw_c_tablesizeview"
        pProcess = enmProcess.viw_c_TableSizeView
      Case "viw_vw_orderlinecalcdatadelete"
        pProcess = enmProcess.viw_vw_OrderLineCalcDataDelete
      Case "viw_vw_orderlinecalcdataupdate"
        pProcess = enmProcess.viw_vw_OrderLineCalcDataUpdate
      Case "viw_vw_orderlinecalcdataview"
        pProcess = enmProcess.viw_vw_OrderLineCalcDataView
      Case "viw_vw_orderlinecalcdelete"
        pProcess = enmProcess.viw_vw_OrderLineCalcDelete
      Case "viw_vw_orderlinecalcupdate"
        pProcess = enmProcess.viw_vw_OrderLineCalcUpdate
      Case "viw_vw_orderlinecalcview"
        pProcess = enmProcess.viw_vw_OrderLineCalcView
      Case "viw_vw_orderlinefulldatadelete"
        pProcess = enmProcess.viw_vw_OrderLineFullDataDelete
      Case "viw_vw_orderlinefulldataupdate"
        pProcess = enmProcess.viw_vw_OrderLineFullDataUpdate
      Case "viw_vw_orderlinefulldataview"
        pProcess = enmProcess.viw_vw_OrderLineFullDataView
      Case "viw_vw_orderlinefulldelete"
        pProcess = enmProcess.viw_vw_OrderLineFullDelete
      Case "viw_vw_orderlinefullupdate"
        pProcess = enmProcess.viw_vw_OrderLineFullUpdate
      Case "viw_vw_orderlinefullview"
        pProcess = enmProcess.viw_vw_OrderLineFullView
      Case "viw_vworderlinecalcdelete"
        pProcess = enmProcess.viw_vwOrderLineCalcDelete
      Case "viw_vworderlinecalcupdate"
        pProcess = enmProcess.viw_vwOrderLineCalcUpdate
      Case "viw_vworderlinecalcview"
        pProcess = enmProcess.viw_vwOrderLineCalcView
      Case "viw_vworderlinefulldelete"
        pProcess = enmProcess.viw_vwOrderLineFullDelete
      Case "viw_vworderlinefullupdate"
        pProcess = enmProcess.viw_vwOrderLineFullUpdate
      Case "viw_vworderlinefullview"
        pProcess = enmProcess.viw_vwOrderLineFullView
      Case Else
        pProcess = enmProcess.UD
    End Select
    
    Return pProcess
  End Function
  Public Shared Function TranslateEnmSystemDefaultType(ByVal vString As String) As enmSystemDefaultType
    Dim pSystemDefaultType As enmSystemDefaultType
    
    If vString Is Nothing Then Return enmSystemDefaultType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "bit"
        pSystemDefaultType = enmSystemDefaultType.Bit
      Case "decimal"
        pSystemDefaultType = enmSystemDefaultType.Decimal
      Case "encrypted"
        pSystemDefaultType = enmSystemDefaultType.Encrypted
      Case "enum"
        pSystemDefaultType = enmSystemDefaultType.Enum
      Case "integer"
        pSystemDefaultType = enmSystemDefaultType.Integer
      Case "string"
        pSystemDefaultType = enmSystemDefaultType.String
      Case Else
        pSystemDefaultType = enmSystemDefaultType.UD
    End Select
    
    Return pSystemDefaultType
  End Function
  Public Shared Function TranslateEnmUserIdentificationModel(ByVal vString As String) As enmUserIdentificationModel
    Dim pUserIdentificationModel As enmUserIdentificationModel
    
    If vString Is Nothing Then Return enmUserIdentificationModel.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "byapplicationuser"
        pUserIdentificationModel = enmUserIdentificationModel.ByApplicationUser
      Case "bydomaingroup"
        pUserIdentificationModel = enmUserIdentificationModel.ByDomainGroup
      Case "bydomainuser"
        pUserIdentificationModel = enmUserIdentificationModel.ByDomainUser
      Case Else
        pUserIdentificationModel = enmUserIdentificationModel.UD
    End Select
    
    Return pUserIdentificationModel
  End Function
  Public Shared Function TranslateEnmUserIdentityType(ByVal vString As String) As enmUserIdentityType
    Dim pUserIdentityType As enmUserIdentityType
    
    If vString Is Nothing Then Return enmUserIdentityType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "c_user"
        pUserIdentityType = enmUserIdentityType.c_User
      Case "customer"
        pUserIdentityType = enmUserIdentityType.Customer
      Case "global"
        pUserIdentityType = enmUserIdentityType.Global
      Case Else
        pUserIdentityType = enmUserIdentityType.UD
    End Select
    
    Return pUserIdentityType
  End Function
  Public Shared Function TranslateEnmWildCardType(ByVal vString As String) As enmWildCardType
    Dim pWildCardType As enmWildCardType
    
    If vString Is Nothing Then Return enmWildCardType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant()
    Select Case pStrg
      Case "after"
        pWildCardType = enmWildCardType.After
      Case "before"
        pWildCardType = enmWildCardType.Before
      Case "beforeandafter"
        pWildCardType = enmWildCardType.BeforeAndAfter
      Case "beforeandafterandbetweeneachletter"
        pWildCardType = enmWildCardType.BeforeAndAfterAndBetweenEachLetter
      Case "none"
        pWildCardType = enmWildCardType.None
      Case Else
        pWildCardType = enmWildCardType.UD
    End Select
    
    Return pWildCardType
  End Function
  
  'ComboList 
  Public Shared Function TranslateEnmComboListType(ByVal vString As String) As enmComboListType 
    Dim pComboListType As enmComboListType 
    
    If vString Is Nothing Then Return enmComboListType.UD 
    
    Dim pStrg As String = vString.ToLowerInvariant() 
    Select Case pStrg 
      Case "ccbeehivebuyertrackingdefaultbyid" 
        pComboListType = enmComboListType.ccBeehiveBuyerTrackingDefaultByID 
      Case "ccbeehivebuyertrackingforcustomerdefaultbyid" 
        pComboListType = enmComboListType.ccBeehiveBuyerTrackingForCustomerDefaultByID 
      Case "cccustomerdefaultbyid" 
        pComboListType = enmComboListType.ccCustomerDefaultByID 
      Case "cccustomerdebtdefaultbyid" 
        pComboListType = enmComboListType.ccCustomerDebtDefaultByID 
      Case "cccustomerdebtforcustomerdefaultbyid" 
        pComboListType = enmComboListType.ccCustomerDebtForCustomerDefaultByID 
      Case "ccdeliverydefaultbyid" 
        pComboListType = enmComboListType.ccDeliveryDefaultByID 
      Case "ccorderheaderdefaultbyid" 
        pComboListType = enmComboListType.ccOrderHeaderDefaultByID 
      Case "ccorderheaderforcustomerdefaultbyid" 
        pComboListType = enmComboListType.ccOrderHeaderForCustomerDefaultByID 
      Case "ccorderlinedefaultbyid" 
        pComboListType = enmComboListType.ccOrderLineDefaultByID 
      Case "ccproductdefaultbyid" 
        pComboListType = enmComboListType.ccProductDefaultByID 
      Case "ccproductpricedefaultbyid" 
        pComboListType = enmComboListType.ccProductPriceDefaultByID 
      Case "ccproductpricehistdefaultbyid" 
        pComboListType = enmComboListType.ccProductPriceHistDefaultByID 
      Case "ccsupplierorderdefaultbyid" 
        pComboListType = enmComboListType.ccSupplierOrderDefaultByID 
      Case "c_alertmessagedefaultbyid" 
        pComboListType = enmComboListType.c_AlertMessageDefaultByID 
      Case "c_enumerationdefaultbyid" 
        pComboListType = enmComboListType.c_EnumerationDefaultByID 
      Case "c_indexfragmentationdefaultbyid" 
        pComboListType = enmComboListType.c_IndexFragmentationDefaultByID 
      Case "c_jobdefaultbyid" 
        pComboListType = enmComboListType.c_JobDefaultByID 
      Case "c_languagedefaultbyid" 
        pComboListType = enmComboListType.c_LanguageDefaultByID 
      Case "c_loggedalertdefaultbyid" 
        pComboListType = enmComboListType.c_LoggedAlertDefaultByID 
      Case "c_loggedalertforaffecteduserdefaultbyid" 
        pComboListType = enmComboListType.c_LoggedAlertForAffectedUserDefaultByID 
      Case "c_loggedlogindefaultbyid" 
        pComboListType = enmComboListType.c_LoggedLoginDefaultByID 
      Case "c_lookupdefaultbyid" 
        pComboListType = enmComboListType.c_LookupDefaultByID 
      Case "c_mfadefaultbyid" 
        pComboListType = enmComboListType.c_MFADefaultByID 
      Case "c_mfaforuserdefaultbyid" 
        pComboListType = enmComboListType.c_MFAForUserDefaultByID 
      Case "c_objecttotranslatedefaultbyid" 
        pComboListType = enmComboListType.c_ObjectToTranslateDefaultByID 
      Case "c_processdefaultbyid" 
        pComboListType = enmComboListType.c_ProcessDefaultByID 
      Case "c_roledefaultbyid" 
        pComboListType = enmComboListType.c_RoleDefaultByID 
      Case "c_systemdefaultdefaultbyid" 
        pComboListType = enmComboListType.c_SystemDefaultDefaultByID 
      Case "c_tabledefaultbyid" 
        pComboListType = enmComboListType.c_TableDefaultByID 
      Case "c_tablesizedefaultbyid" 
        pComboListType = enmComboListType.c_TableSizeDefaultByID 
      Case "c_userdefaultbyid" 
        pComboListType = enmComboListType.c_UserDefaultByID 
      Case "c_userdefaultnomasterbyid" 
        pComboListType = enmComboListType.c_UserDefaultNoMasterByID 
      Case "c_rolenonbasedefaultbyid" 
        pComboListType = enmComboListType.c_RoleNonBaseDefaultByID 
      Case "c_rolewithbasedefaultbyid" 
        pComboListType = enmComboListType.c_RoleWithBaseDefaultByID 
      Case "c_rolewithbaseandmasterdefaultbyid" 
        pComboListType = enmComboListType.c_RoleWithBaseAndMasterDefaultByID 
      Case "c_rolewithbasenosysadmindefaultbyid" 
        pComboListType = enmComboListType.c_RoleWithBaseNoSysAdminDefaultByID 
      Case "samplecomboqueryone" 
        pComboListType = enmComboListType.SampleComboQueryOne 
      Case "samplecomboquerytwo" 
        pComboListType = enmComboListType.SampleComboQueryTwo 
      Case "cctestcombolistfillmanual" 
        pComboListType = enmComboListType.ccTestComboListFillManual 
      Case Else 
        pComboListType = enmComboListType.UD 
    End Select 
    
    Return pComboListType 
  End Function 
  
  Public Shared Function LoadLocalizedText(ByVal vEnum As enmEnum, ByVal vValue As String, ByRef rLocalizedText As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters = String.Format("Enum = {0}, Value={1}, UILang = {2}", vEnum.ToString, vValue, vRequester.UILang.ToString) 
    Dim pFault As New clsFault 
 
    Try 
      rLocalizedText = ccHelper.GetLocalizedEnum(vEnum, vValue, vRequester) 
      If rLocalizedText = "" Then rLocalizedText = vValue 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-151123-1734", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
End Class
 
Public Module clsEnumsExtensions 
  <Extension> 
  Public Function FastToString(ByVal vEnum As clsEnums.enmEnum) As String 
    Select Case vEnum 
      Case clsEnums.enmEnum.AccountantMethod : Return "AccountantMethod" 
      Case clsEnums.enmEnum.ApplicationAuthenticationToWS : Return "ApplicationAuthenticationToWS" 
      Case clsEnums.enmEnum.AuthenticationMethod : Return "AuthenticationMethod" 
      Case clsEnums.enmEnum.Category : Return "Category" 
      Case clsEnums.enmEnum.ccAPICompressionMode : Return "ccAPICompressionMode" 
      Case clsEnums.enmEnum.ComboListKeyType : Return "ComboListKeyType" 
      Case clsEnums.enmEnum.CustomerType : Return "CustomerType" 
      Case clsEnums.enmEnum.DebtStatus : Return "DebtStatus" 
      Case clsEnums.enmEnum.DeliveryDay : Return "DeliveryDay" 
      Case clsEnums.enmEnum.DeliveryMethod : Return "DeliveryMethod" 
      Case clsEnums.enmEnum.DeliveryStatus : Return "DeliveryStatus" 
      Case clsEnums.enmEnum.EmailStatus : Return "EmailStatus" 
      Case clsEnums.enmEnum.FaultSeverity : Return "FaultSeverity" 
      Case clsEnums.enmEnum.FaultType : Return "FaultType" 
      Case clsEnums.enmEnum.FillDirection : Return "FillDirection" 
      Case clsEnums.enmEnum.Importance : Return "Importance" 
      Case clsEnums.enmEnum.JobAlertType : Return "JobAlertType" 
      Case clsEnums.enmEnum.JobStatus : Return "JobStatus" 
      Case clsEnums.enmEnum.JobType : Return "JobType" 
      Case clsEnums.enmEnum.Language : Return "Language" 
      Case clsEnums.enmEnum.LoadParent : Return "LoadParent" 
      Case clsEnums.enmEnum.Lookup : Return "Lookup" 
      Case clsEnums.enmEnum.MessagingMode : Return "MessagingMode" 
      Case clsEnums.enmEnum.ObjectStatus : Return "ObjectStatus" 
      Case clsEnums.enmEnum.ObjectType : Return "ObjectType" 
      Case clsEnums.enmEnum.OrderStatus : Return "OrderStatus" 
      Case clsEnums.enmEnum.PaymentMethod : Return "PaymentMethod" 
      Case clsEnums.enmEnum.PaymentStatus : Return "PaymentStatus" 
      Case clsEnums.enmEnum.Process : Return "Process" 
      Case clsEnums.enmEnum.SystemDefaultType : Return "SystemDefaultType" 
      Case clsEnums.enmEnum.UserIdentificationModel : Return "UserIdentificationModel" 
      Case clsEnums.enmEnum.UserIdentityType : Return "UserIdentityType" 
      Case clsEnums.enmEnum.WildCardType : Return "WildCardType" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 
 
  <Extension> 
  Public Function FastToString(ByVal vAccountantMethod As clsEnums.enmAccountantMethod) As String 
    Select Case vAccountantMethod 
      Case clsEnums.enmAccountantMethod.Email : Return "Email" 
      Case clsEnums.enmAccountantMethod.Mail : Return "Mail" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS) As String 
    Select Case vApplicationAuthenticationToWS 
      Case clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials : Return "ActiveUserCredentials" 
      Case clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials : Return "ApplicationCredentials" 
      Case clsEnums.enmApplicationAuthenticationToWS.None : Return "None" 
      Case clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials : Return "SpecificUserCredentials" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vAuthenticationMethod As clsEnums.enmAuthenticationMethod) As String 
    Select Case vAuthenticationMethod 
      Case clsEnums.enmAuthenticationMethod.NamePassword : Return "NamePassword" 
      Case clsEnums.enmAuthenticationMethod.OneTimePassword : Return "OneTimePassword" 
      Case clsEnums.enmAuthenticationMethod.SingleVenue2FA : Return "SingleVenue2FA" 
      Case clsEnums.enmAuthenticationMethod.TwoFactorAuthentication : Return "TwoFactorAuthentication" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vCategory As clsEnums.enmCategory) As String 
    Select Case vCategory 
      Case clsEnums.enmCategory.Beehives : Return "Beehives" 
      Case clsEnums.enmCategory.Biolife : Return "Biolife" 
      Case clsEnums.enmCategory.BiologicalPest : Return "BiologicalPest" 
      Case clsEnums.enmCategory.BioPest : Return "BioPest" 
      Case clsEnums.enmCategory.Biotime : Return "Biotime" 
      Case clsEnums.enmCategory.Butano : Return "Butano" 
      Case clsEnums.enmCategory.Canrise : Return "Canrise" 
      Case clsEnums.enmCategory.Delivery : Return "Delivery" 
      Case clsEnums.enmCategory.Equipment : Return "Equipment" 
      Case clsEnums.enmCategory.General : Return "General" 
      Case clsEnums.enmCategory.Preparations : Return "Preparations" 
      Case clsEnums.enmCategory.Shmoolik : Return "Shmoolik" 
      Case clsEnums.enmCategory.Stock : Return "Stock" 
      Case clsEnums.enmCategory.Traps : Return "Traps" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vccAPICompressionMode As clsEnums.enmccAPICompressionMode) As String 
    Select Case vccAPICompressionMode 
      Case clsEnums.enmccAPICompressionMode.DeflateTargCC : Return "DeflateTargCC" 
      Case clsEnums.enmccAPICompressionMode.GzipTargCC : Return "GzipTargCC" 
      Case clsEnums.enmccAPICompressionMode.IIS : Return "IIS" 
      Case clsEnums.enmccAPICompressionMode.None : Return "None" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vComboListKeyType As clsEnums.enmComboListKeyType) As String 
    Select Case vComboListKeyType 
      Case clsEnums.enmComboListKeyType.Enum : Return "Enum" 
      Case clsEnums.enmComboListKeyType.Integer : Return "Integer" 
      Case clsEnums.enmComboListKeyType.Long : Return "Long" 
      Case clsEnums.enmComboListKeyType.Object : Return "Object" 
      Case clsEnums.enmComboListKeyType.String : Return "String" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vCustomerType As clsEnums.enmCustomerType) As String 
    Select Case vCustomerType 
      Case clsEnums.enmCustomerType.Farm : Return "Farm" 
      Case clsEnums.enmCustomerType.Farmer : Return "Farmer" 
      Case clsEnums.enmCustomerType.Hydro : Return "Hydro" 
      Case clsEnums.enmCustomerType.Private : Return "Private" 
      Case clsEnums.enmCustomerType.Retail : Return "Retail" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vDebtStatus As clsEnums.enmDebtStatus) As String 
    Select Case vDebtStatus 
      Case clsEnums.enmDebtStatus.Cancelled : Return "Cancelled" 
      Case clsEnums.enmDebtStatus.Open : Return "Open" 
      Case clsEnums.enmDebtStatus.Paid : Return "Paid" 
      Case clsEnums.enmDebtStatus.PartiallyPaid : Return "PartiallyPaid" 
      Case clsEnums.enmDebtStatus.WrittenOff : Return "WrittenOff" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vDeliveryDay As clsEnums.enmDeliveryDay) As String 
    Select Case vDeliveryDay 
      Case clsEnums.enmDeliveryDay.Friday : Return "Friday" 
      Case clsEnums.enmDeliveryDay.Monday : Return "Monday" 
      Case clsEnums.enmDeliveryDay.Saturday : Return "Saturday" 
      Case clsEnums.enmDeliveryDay.Sunday : Return "Sunday" 
      Case clsEnums.enmDeliveryDay.Thursday : Return "Thursday" 
      Case clsEnums.enmDeliveryDay.Tuesday : Return "Tuesday" 
      Case clsEnums.enmDeliveryDay.Wednesday : Return "Wednesday" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vDeliveryMethod As clsEnums.enmDeliveryMethod) As String 
    Select Case vDeliveryMethod 
      Case clsEnums.enmDeliveryMethod.Biobee : Return "Biobee" 
      Case clsEnums.enmDeliveryMethod.BioTour : Return "BioTour" 
      Case clsEnums.enmDeliveryMethod.GreenArt : Return "GreenArt" 
      Case clsEnums.enmDeliveryMethod.Gvulot : Return "Gvulot" 
      Case clsEnums.enmDeliveryMethod.LiorCarmiel : Return "LiorCarmiel" 
      Case clsEnums.enmDeliveryMethod.Netzach : Return "Netzach" 
      Case clsEnums.enmDeliveryMethod.NoDelivery : Return "NoDelivery" 
      Case clsEnums.enmDeliveryMethod.Other : Return "Other" 
      Case clsEnums.enmDeliveryMethod.Paran : Return "Paran" 
      Case clsEnums.enmDeliveryMethod.Ptzael : Return "Ptzael" 
      Case clsEnums.enmDeliveryMethod.SelfPickup : Return "SelfPickup" 
      Case clsEnums.enmDeliveryMethod.Shmoolik : Return "Shmoolik" 
      Case clsEnums.enmDeliveryMethod.Tzofar : Return "Tzofar" 
      Case clsEnums.enmDeliveryMethod.WarehouseKQ : Return "WarehouseKQ" 
      Case clsEnums.enmDeliveryMethod.Elkana : Return "Elkana" 
      Case clsEnums.enmDeliveryMethod.YDM : Return "YDM" 
      Case clsEnums.enmDeliveryMethod.BeerTuvia : Return "BeerTuvia" 
      Case clsEnums.enmDeliveryMethod.BGabriel : Return "BGabriel" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vDeliveryStatus As clsEnums.enmDeliveryStatus) As String 
    Select Case vDeliveryStatus 
      Case clsEnums.enmDeliveryStatus.AtHub : Return "AtHub" 
      Case clsEnums.enmDeliveryStatus.Cancelled : Return "Cancelled" 
      Case clsEnums.enmDeliveryStatus.Delivered : Return "Delivered" 
      Case clsEnums.enmDeliveryStatus.Failed : Return "Failed" 
      Case clsEnums.enmDeliveryStatus.InTransit : Return "InTransit" 
      Case clsEnums.enmDeliveryStatus.Ordered : Return "Ordered" 
      Case clsEnums.enmDeliveryStatus.Pending : Return "Pending" 
      Case clsEnums.enmDeliveryStatus.Received : Return "Received" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vEmailStatus As clsEnums.enmEmailStatus) As String 
    Select Case vEmailStatus 
      Case clsEnums.enmEmailStatus.Draft : Return "Draft" 
      Case clsEnums.enmEmailStatus.Failed : Return "Failed" 
      Case clsEnums.enmEmailStatus.Sent : Return "Sent" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vFaultSeverity As clsEnums.enmFaultSeverity) As String 
    Select Case vFaultSeverity 
      Case clsEnums.enmFaultSeverity.Alert : Return "Alert" 
      Case clsEnums.enmFaultSeverity.Email : Return "Email" 
      Case clsEnums.enmFaultSeverity.Info : Return "Info" 
      Case clsEnums.enmFaultSeverity.LogOnly : Return "LogOnly" 
      Case clsEnums.enmFaultSeverity.SMS : Return "SMS" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vFaultType As clsEnums.enmFaultType) As String 
    Select Case vFaultType 
      Case clsEnums.enmFaultType.Business : Return "Business" 
      Case clsEnums.enmFaultType.Security : Return "Security" 
      Case clsEnums.enmFaultType.System : Return "System" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vFillDirection As clsEnums.enmFillDirection) As String 
    Select Case vFillDirection 
      Case clsEnums.enmFillDirection.ASC : Return "ASC" 
      Case clsEnums.enmFillDirection.DESC : Return "DESC" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vImportance As clsEnums.enmImportance) As String 
    Select Case vImportance 
      Case clsEnums.enmImportance.High : Return "High" 
      Case clsEnums.enmImportance.Low : Return "Low" 
      Case clsEnums.enmImportance.Medium : Return "Medium" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vJobAlertType As clsEnums.enmJobAlertType) As String 
    Select Case vJobAlertType 
      Case clsEnums.enmJobAlertType.email : Return "email" 
      Case clsEnums.enmJobAlertType.Pager : Return "Pager" 
      Case clsEnums.enmJobAlertType.SMS : Return "SMS" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vJobStatus As clsEnums.enmJobStatus) As String 
    Select Case vJobStatus 
      Case clsEnums.enmJobStatus.Failure : Return "Failure" 
      Case clsEnums.enmJobStatus.InProcess : Return "InProcess" 
      Case clsEnums.enmJobStatus.Missed : Return "Missed" 
      Case clsEnums.enmJobStatus.Success : Return "Success" 
      Case clsEnums.enmJobStatus.Warning : Return "Warning" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vJobType As clsEnums.enmJobType) As String 
    Select Case vJobType 
      Case clsEnums.enmJobType.Annually : Return "Annually" 
      Case clsEnums.enmJobType.CyclicDay : Return "CyclicDay" 
      Case clsEnums.enmJobType.CyclicHour : Return "CyclicHour" 
      Case clsEnums.enmJobType.CyclicMin : Return "CyclicMin" 
      Case clsEnums.enmJobType.CyclicSec : Return "CyclicSec" 
      Case clsEnums.enmJobType.Daily : Return "Daily" 
      Case clsEnums.enmJobType.Monthly : Return "Monthly" 
      Case clsEnums.enmJobType.OneOff : Return "OneOff" 
      Case clsEnums.enmJobType.Weekly : Return "Weekly" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vLanguage As clsEnums.enmLanguage) As String 
    Select Case vLanguage 
      Case clsEnums.enmLanguage.af : Return "af" 
      Case clsEnums.enmLanguage.am : Return "am" 
      Case clsEnums.enmLanguage.ar : Return "ar" 
      Case clsEnums.enmLanguage.bg : Return "bg" 
      Case clsEnums.enmLanguage.cr : Return "cr" 
      Case clsEnums.enmLanguage.cs : Return "cs" 
      Case clsEnums.enmLanguage.cy : Return "cy" 
      Case clsEnums.enmLanguage.da : Return "da" 
      Case clsEnums.enmLanguage.de : Return "de" 
      Case clsEnums.enmLanguage.el : Return "el" 
      Case clsEnums.enmLanguage.en : Return "en" 
      Case clsEnums.enmLanguage.eo : Return "eo" 
      Case clsEnums.enmLanguage.es : Return "es" 
      Case clsEnums.enmLanguage.et : Return "et" 
      Case clsEnums.enmLanguage.fa : Return "fa" 
      Case clsEnums.enmLanguage.fi : Return "fi" 
      Case clsEnums.enmLanguage.fr : Return "fr" 
      Case clsEnums.enmLanguage.ga : Return "ga" 
      Case clsEnums.enmLanguage.gd : Return "gd" 
      Case clsEnums.enmLanguage.he : Return "he" 
      Case clsEnums.enmLanguage.hr : Return "hr" 
      Case clsEnums.enmLanguage.hu : Return "hu" 
      Case clsEnums.enmLanguage.hy : Return "hy" 
      Case clsEnums.enmLanguage.id : Return "id" 
      Case clsEnums.enmLanguage.it : Return "it" 
      Case clsEnums.enmLanguage.iu : Return "iu" 
      Case clsEnums.enmLanguage.ja : Return "ja" 
      Case clsEnums.enmLanguage.ko : Return "ko" 
      Case clsEnums.enmLanguage.nl : Return "nl" 
      Case clsEnums.enmLanguage.no : Return "no" 
      Case clsEnums.enmLanguage.pl : Return "pl" 
      Case clsEnums.enmLanguage.pt : Return "pt" 
      Case clsEnums.enmLanguage.ro : Return "ro" 
      Case clsEnums.enmLanguage.ru : Return "ru" 
      Case clsEnums.enmLanguage.sq : Return "sq" 
      Case clsEnums.enmLanguage.sv : Return "sv" 
      Case clsEnums.enmLanguage.tr : Return "tr" 
      Case clsEnums.enmLanguage.uk : Return "uk" 
      Case clsEnums.enmLanguage.vi : Return "vi" 
      Case clsEnums.enmLanguage.yi : Return "yi" 
      Case clsEnums.enmLanguage.zh : Return "zh" 
      Case clsEnums.enmLanguage.zu : Return "zu" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vLoadParent As clsEnums.enmLoadParent) As String 
    Select Case vLoadParent 
      Case clsEnums.enmLoadParent.DoNotLoad : Return "DoNotLoad" 
      Case clsEnums.enmLoadParent.EntireObject : Return "EntireObject" 
      Case clsEnums.enmLoadParent.TextOnly : Return "TextOnly" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vLookup As clsEnums.enmLookup) As String 
    Select Case vLookup 
      Case clsEnums.enmLookup.Generic : Return "Generic" 
      Case clsEnums.enmLookup.Job : Return "Job" 
      Case clsEnums.enmLookup.JobRunner : Return "JobRunner" 
      Case clsEnums.enmLookup.SecurityQuestion : Return "SecurityQuestion" 
      Case clsEnums.enmLookup.UserIdentityType : Return "UserIdentityType" 
      Case clsEnums.enmLookup.UserIdentityTypeName : Return "UserIdentityTypeName" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vMessagingMode As clsEnums.enmMessagingMode) As String 
    Select Case vMessagingMode 
      Case clsEnums.enmMessagingMode.Email : Return "Email" 
      Case clsEnums.enmMessagingMode.SMS : Return "SMS" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vObjectStatus As clsEnums.enmObjectStatus) As String 
    Select Case vObjectStatus 
      Case clsEnums.enmObjectStatus.Clean : Return "Clean" 
      Case clsEnums.enmObjectStatus.Deleted : Return "Deleted" 
      Case clsEnums.enmObjectStatus.Dirty : Return "Dirty" 
      Case clsEnums.enmObjectStatus.New : Return "New" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vObjectType As clsEnums.enmObjectType) As String 
    Select Case vObjectType 
      Case clsEnums.enmObjectType.System : Return "System" 
      Case clsEnums.enmObjectType.TableData : Return "TableData" 
      Case clsEnums.enmObjectType.TableFieldName : Return "TableFieldName" 
      Case clsEnums.enmObjectType.UI : Return "UI" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vOrderStatus As clsEnums.enmOrderStatus) As String 
    Select Case vOrderStatus 
      Case clsEnums.enmOrderStatus.Cancelled : Return "Cancelled" 
      Case clsEnums.enmOrderStatus.Completed : Return "Completed" 
      Case clsEnums.enmOrderStatus.InProgress : Return "InProgress" 
      Case clsEnums.enmOrderStatus.New : Return "New" 
      Case clsEnums.enmOrderStatus.Processing : Return "Processing" 
      Case clsEnums.enmOrderStatus.Shipped : Return "Shipped" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vPaymentMethod As clsEnums.enmPaymentMethod) As String 
    Select Case vPaymentMethod 
      Case clsEnums.enmPaymentMethod.BitPaybox : Return "BitPaybox" 
      Case clsEnums.enmPaymentMethod.Cash : Return "Cash" 
      Case clsEnums.enmPaymentMethod.Check : Return "Check" 
      Case clsEnums.enmPaymentMethod.Credit : Return "Credit" 
      Case clsEnums.enmPaymentMethod.CreditCard : Return "CreditCard" 
      Case clsEnums.enmPaymentMethod.Transfer : Return "Transfer" 
      Case clsEnums.enmPaymentMethod.WebPayment : Return "WebPayment" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vPaymentStatus As clsEnums.enmPaymentStatus) As String 
    Select Case vPaymentStatus 
      Case clsEnums.enmPaymentStatus.Paid : Return "Paid" 
      Case clsEnums.enmPaymentStatus.PartiallyPaid : Return "PartiallyPaid" 
      Case clsEnums.enmPaymentStatus.Pending : Return "Pending" 
      Case clsEnums.enmPaymentStatus.Unpaid : Return "Unpaid" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vProcess As clsEnums.enmProcess) As String 
    Select Case vProcess 
      Case clsEnums.enmProcess.prc_BackupDatabase : Return "prc_BackupDatabase" 
      Case clsEnums.enmProcess.prc_CreateDefaultPermissionsForNewRolesAndTables : Return "prc_CreateDefaultPermissionsForNewRolesAndTables" 
      Case clsEnums.enmProcess.prc_DeleteOldLogs : Return "prc_DeleteOldLogs" 
      Case clsEnums.enmProcess.prc_DoSample : Return "prc_DoSample" 
      Case clsEnums.enmProcess.prc_DoTasks : Return "prc_DoTasks" 
      Case clsEnums.enmProcess.prc_EjectAllUsers : Return "prc_EjectAllUsers" 
      Case clsEnums.enmProcess.prc_EjectNonMaster : Return "prc_EjectNonMaster" 
      Case clsEnums.enmProcess.prc_GetNextJobForRunner : Return "prc_GetNextJobForRunner" 
      Case clsEnums.enmProcess.prc_GetNextManagedJobForRunner : Return "prc_GetNextManagedJobForRunner" 
      Case clsEnums.enmProcess.prc_GetSpecificUnmanagedJobForRunner : Return "prc_GetSpecificUnmanagedJobForRunner" 
      Case clsEnums.enmProcess.prc_HandleObjectToTranslate : Return "prc_HandleObjectToTranslate" 
      Case clsEnums.enmProcess.prc_LogInAnonymously : Return "prc_LogInAnonymously" 
      Case clsEnums.enmProcess.prc_LogInByNamePwd : Return "prc_LogInByNamePwd" 
      Case clsEnums.enmProcess.prc_LogInByNetworkCredentials : Return "prc_LogInByNetworkCredentials" 
      Case clsEnums.enmProcess.prc_LogOut : Return "prc_LogOut" 
      Case clsEnums.enmProcess.prc_MarkJobAsComplete : Return "prc_MarkJobAsComplete" 
      Case clsEnums.enmProcess.prc_MoveAudits : Return "prc_MoveAudits" 
      Case clsEnums.enmProcess.prc_RequestDatabaseBackup : Return "prc_RequestDatabaseBackup" 
      Case clsEnums.enmProcess.prc_RequestIndexReorganization : Return "prc_RequestIndexReorganization" 
      Case clsEnums.enmProcess.prc_ResetDefaultPermissions : Return "prc_ResetDefaultPermissions" 
      Case clsEnums.enmProcess.prc_ScanJobs : Return "prc_ScanJobs" 
      Case clsEnums.enmProcess.prc_SendMail : Return "prc_SendMail" 
      Case clsEnums.enmProcess.prc_SetDefaultPermissionsForRole : Return "prc_SetDefaultPermissionsForRole" 
      Case clsEnums.enmProcess.prc_SetJobToNow : Return "prc_SetJobToNow" 
      Case clsEnums.enmProcess.prc_SysAdmin : Return "prc_SysAdmin" 
      Case clsEnums.enmProcess.prc_WriteDatabaseToXML : Return "prc_WriteDatabaseToXML" 
      Case clsEnums.enmProcess.tbl_BeehiveBuyerTrackingDelete : Return "tbl_BeehiveBuyerTrackingDelete" 
      Case clsEnums.enmProcess.tbl_BeehiveBuyerTrackingUpdate : Return "tbl_BeehiveBuyerTrackingUpdate" 
      Case clsEnums.enmProcess.tbl_BeehiveBuyerTrackingView : Return "tbl_BeehiveBuyerTrackingView" 
      Case clsEnums.enmProcess.tbl_c_AlertMessageDelete : Return "tbl_c_AlertMessageDelete" 
      Case clsEnums.enmProcess.tbl_c_AlertMessageUpdate : Return "tbl_c_AlertMessageUpdate" 
      Case clsEnums.enmProcess.tbl_c_AlertMessageView : Return "tbl_c_AlertMessageView" 
      Case clsEnums.enmProcess.tbl_c_AuditIndexedDelete : Return "tbl_c_AuditIndexedDelete" 
      Case clsEnums.enmProcess.tbl_c_AuditIndexedUpdate : Return "tbl_c_AuditIndexedUpdate" 
      Case clsEnums.enmProcess.tbl_c_AuditIndexedView : Return "tbl_c_AuditIndexedView" 
      Case clsEnums.enmProcess.tbl_c_EnumerationDelete : Return "tbl_c_EnumerationDelete" 
      Case clsEnums.enmProcess.tbl_c_EnumerationUpdate : Return "tbl_c_EnumerationUpdate" 
      Case clsEnums.enmProcess.tbl_c_EnumerationView : Return "tbl_c_EnumerationView" 
      Case clsEnums.enmProcess.tbl_c_JobAlertRecipientDelete : Return "tbl_c_JobAlertRecipientDelete" 
      Case clsEnums.enmProcess.tbl_c_JobAlertRecipientUpdate : Return "tbl_c_JobAlertRecipientUpdate" 
      Case clsEnums.enmProcess.tbl_c_JobAlertRecipientView : Return "tbl_c_JobAlertRecipientView" 
      Case clsEnums.enmProcess.tbl_c_JobDelete : Return "tbl_c_JobDelete" 
      Case clsEnums.enmProcess.tbl_c_JobUpdate : Return "tbl_c_JobUpdate" 
      Case clsEnums.enmProcess.tbl_c_JobUpdateSetToNow : Return "tbl_c_JobUpdateSetToNow" 
      Case clsEnums.enmProcess.tbl_c_JobView : Return "tbl_c_JobView" 
      Case clsEnums.enmProcess.tbl_c_LanguageDelete : Return "tbl_c_LanguageDelete" 
      Case clsEnums.enmProcess.tbl_c_LanguageUpdate : Return "tbl_c_LanguageUpdate" 
      Case clsEnums.enmProcess.tbl_c_LanguageView : Return "tbl_c_LanguageView" 
      Case clsEnums.enmProcess.tbl_c_LoggedAlertDelete : Return "tbl_c_LoggedAlertDelete" 
      Case clsEnums.enmProcess.tbl_c_LoggedAlertUpdate : Return "tbl_c_LoggedAlertUpdate" 
      Case clsEnums.enmProcess.tbl_c_LoggedAlertView : Return "tbl_c_LoggedAlertView" 
      Case clsEnums.enmProcess.tbl_c_LoggedJobDelete : Return "tbl_c_LoggedJobDelete" 
      Case clsEnums.enmProcess.tbl_c_LoggedJobUpdate : Return "tbl_c_LoggedJobUpdate" 
      Case clsEnums.enmProcess.tbl_c_LoggedJobView : Return "tbl_c_LoggedJobView" 
      Case clsEnums.enmProcess.tbl_c_LoggedLoginDelete : Return "tbl_c_LoggedLoginDelete" 
      Case clsEnums.enmProcess.tbl_c_LoggedLoginUpdate : Return "tbl_c_LoggedLoginUpdate" 
      Case clsEnums.enmProcess.tbl_c_LoggedLoginView : Return "tbl_c_LoggedLoginView" 
      Case clsEnums.enmProcess.tbl_c_LoggedRequestDelete : Return "tbl_c_LoggedRequestDelete" 
      Case clsEnums.enmProcess.tbl_c_LoggedRequestUpdate : Return "tbl_c_LoggedRequestUpdate" 
      Case clsEnums.enmProcess.tbl_c_LoggedRequestView : Return "tbl_c_LoggedRequestView" 
      Case clsEnums.enmProcess.tbl_c_LookupDelete : Return "tbl_c_LookupDelete" 
      Case clsEnums.enmProcess.tbl_c_LookupUpdate : Return "tbl_c_LookupUpdate" 
      Case clsEnums.enmProcess.tbl_c_LookupView : Return "tbl_c_LookupView" 
      Case clsEnums.enmProcess.tbl_c_MailDelete : Return "tbl_c_MailDelete" 
      Case clsEnums.enmProcess.tbl_c_MailUpdate : Return "tbl_c_MailUpdate" 
      Case clsEnums.enmProcess.tbl_c_MailView : Return "tbl_c_MailView" 
      Case clsEnums.enmProcess.tbl_c_MFADelete : Return "tbl_c_MFADelete" 
      Case clsEnums.enmProcess.tbl_c_MFAUpdate : Return "tbl_c_MFAUpdate" 
      Case clsEnums.enmProcess.tbl_c_MFAView : Return "tbl_c_MFAView" 
      Case clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete : Return "tbl_c_ObjectToTranslateDelete" 
      Case clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate : Return "tbl_c_ObjectToTranslateUpdate" 
      Case clsEnums.enmProcess.tbl_c_ObjectToTranslateView : Return "tbl_c_ObjectToTranslateView" 
      Case clsEnums.enmProcess.tbl_c_ObjectTranslationDelete : Return "tbl_c_ObjectTranslationDelete" 
      Case clsEnums.enmProcess.tbl_c_ObjectTranslationUpdate : Return "tbl_c_ObjectTranslationUpdate" 
      Case clsEnums.enmProcess.tbl_c_ObjectTranslationView : Return "tbl_c_ObjectTranslationView" 
      Case clsEnums.enmProcess.tbl_c_PermissionDelete : Return "tbl_c_PermissionDelete" 
      Case clsEnums.enmProcess.tbl_c_PermissionUpdate : Return "tbl_c_PermissionUpdate" 
      Case clsEnums.enmProcess.tbl_c_PermissionView : Return "tbl_c_PermissionView" 
      Case clsEnums.enmProcess.tbl_c_ProcessDelete : Return "tbl_c_ProcessDelete" 
      Case clsEnums.enmProcess.tbl_c_ProcessUpdate : Return "tbl_c_ProcessUpdate" 
      Case clsEnums.enmProcess.tbl_c_ProcessView : Return "tbl_c_ProcessView" 
      Case clsEnums.enmProcess.tbl_c_RoleDelete : Return "tbl_c_RoleDelete" 
      Case clsEnums.enmProcess.tbl_c_RoleUpdate : Return "tbl_c_RoleUpdate" 
      Case clsEnums.enmProcess.tbl_c_RoleView : Return "tbl_c_RoleView" 
      Case clsEnums.enmProcess.tbl_c_SystemAuditDelete : Return "tbl_c_SystemAuditDelete" 
      Case clsEnums.enmProcess.tbl_c_SystemAuditUpdate : Return "tbl_c_SystemAuditUpdate" 
      Case clsEnums.enmProcess.tbl_c_SystemAuditView : Return "tbl_c_SystemAuditView" 
      Case clsEnums.enmProcess.tbl_c_SystemDefaultDelete : Return "tbl_c_SystemDefaultDelete" 
      Case clsEnums.enmProcess.tbl_c_SystemDefaultUpdate : Return "tbl_c_SystemDefaultUpdate" 
      Case clsEnums.enmProcess.tbl_c_SystemDefaultUpdateSettingValue : Return "tbl_c_SystemDefaultUpdateSettingValue" 
      Case clsEnums.enmProcess.tbl_c_SystemDefaultView : Return "tbl_c_SystemDefaultView" 
      Case clsEnums.enmProcess.tbl_c_TableDelete : Return "tbl_c_TableDelete" 
      Case clsEnums.enmProcess.tbl_c_TableUpdate : Return "tbl_c_TableUpdate" 
      Case clsEnums.enmProcess.tbl_c_TableView : Return "tbl_c_TableView" 
      Case clsEnums.enmProcess.tbl_c_UserDelete : Return "tbl_c_UserDelete" 
      Case clsEnums.enmProcess.tbl_c_UserLoginKeyDelete : Return "tbl_c_UserLoginKeyDelete" 
      Case clsEnums.enmProcess.tbl_c_UserLoginKeyUpdate : Return "tbl_c_UserLoginKeyUpdate" 
      Case clsEnums.enmProcess.tbl_c_UserLoginKeyView : Return "tbl_c_UserLoginKeyView" 
      Case clsEnums.enmProcess.tbl_c_UserPermissionDelete : Return "tbl_c_UserPermissionDelete" 
      Case clsEnums.enmProcess.tbl_c_UserPermissionUpdate : Return "tbl_c_UserPermissionUpdate" 
      Case clsEnums.enmProcess.tbl_c_UserPermissionView : Return "tbl_c_UserPermissionView" 
      Case clsEnums.enmProcess.tbl_c_UserStatusDelete : Return "tbl_c_UserStatusDelete" 
      Case clsEnums.enmProcess.tbl_c_UserStatusUpdate : Return "tbl_c_UserStatusUpdate" 
      Case clsEnums.enmProcess.tbl_c_UserStatusView : Return "tbl_c_UserStatusView" 
      Case clsEnums.enmProcess.tbl_c_UserUpdate : Return "tbl_c_UserUpdate" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateApplications : Return "tbl_c_UserUpdateApplications" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateApproval : Return "tbl_c_UserUpdateApproval" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateComments : Return "tbl_c_UserUpdateComments" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateLastSuccessfulLogin : Return "tbl_c_UserUpdateLastSuccessfulLogin" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateLoggedInIP : Return "tbl_c_UserUpdateLoggedInIP" 
      Case clsEnums.enmProcess.tbl_c_UserUpdatePasswordHashed : Return "tbl_c_UserUpdatePasswordHashed" 
      Case clsEnums.enmProcess.tbl_c_UserUpdatePIN : Return "tbl_c_UserUpdatePIN" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateSecurityQuestion1Response : Return "tbl_c_UserUpdateSecurityQuestion1Response" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateSecurityQuestion2Response : Return "tbl_c_UserUpdateSecurityQuestion2Response" 
      Case clsEnums.enmProcess.tbl_c_UserUpdateSecurityQuestion3Response : Return "tbl_c_UserUpdateSecurityQuestion3Response" 
      Case clsEnums.enmProcess.tbl_c_UserView : Return "tbl_c_UserView" 
      Case clsEnums.enmProcess.tbl_CustomerDebtDelete : Return "tbl_CustomerDebtDelete" 
      Case clsEnums.enmProcess.tbl_CustomerDebtUpdate : Return "tbl_CustomerDebtUpdate" 
      Case clsEnums.enmProcess.tbl_CustomerDebtView : Return "tbl_CustomerDebtView" 
      Case clsEnums.enmProcess.tbl_CustomerDelete : Return "tbl_CustomerDelete" 
      Case clsEnums.enmProcess.tbl_CustomerUpdate : Return "tbl_CustomerUpdate" 
      Case clsEnums.enmProcess.tbl_CustomerView : Return "tbl_CustomerView" 
      Case clsEnums.enmProcess.tbl_DeliveryDelete : Return "tbl_DeliveryDelete" 
      Case clsEnums.enmProcess.tbl_DeliveryUpdate : Return "tbl_DeliveryUpdate" 
      Case clsEnums.enmProcess.tbl_DeliveryView : Return "tbl_DeliveryView" 
      Case clsEnums.enmProcess.tbl_OrderHeaderDelete : Return "tbl_OrderHeaderDelete" 
      Case clsEnums.enmProcess.tbl_OrderHeaderUpdate : Return "tbl_OrderHeaderUpdate" 
      Case clsEnums.enmProcess.tbl_OrderHeaderView : Return "tbl_OrderHeaderView" 
      Case clsEnums.enmProcess.tbl_OrderLineDelete : Return "tbl_OrderLineDelete" 
      Case clsEnums.enmProcess.tbl_OrderLineUpdate : Return "tbl_OrderLineUpdate" 
      Case clsEnums.enmProcess.tbl_OrderLineView : Return "tbl_OrderLineView" 
      Case clsEnums.enmProcess.tbl_ProductDelete : Return "tbl_ProductDelete" 
      Case clsEnums.enmProcess.tbl_ProductPriceDelete : Return "tbl_ProductPriceDelete" 
      Case clsEnums.enmProcess.tbl_ProductPriceHistDelete : Return "tbl_ProductPriceHistDelete" 
      Case clsEnums.enmProcess.tbl_ProductPriceHistUpdate : Return "tbl_ProductPriceHistUpdate" 
      Case clsEnums.enmProcess.tbl_ProductPriceHistView : Return "tbl_ProductPriceHistView" 
      Case clsEnums.enmProcess.tbl_ProductPriceUpdate : Return "tbl_ProductPriceUpdate" 
      Case clsEnums.enmProcess.tbl_ProductPriceView : Return "tbl_ProductPriceView" 
      Case clsEnums.enmProcess.tbl_ProductUpdate : Return "tbl_ProductUpdate" 
      Case clsEnums.enmProcess.tbl_ProductView : Return "tbl_ProductView" 
      Case clsEnums.enmProcess.tbl_SupplierOrderDelete : Return "tbl_SupplierOrderDelete" 
      Case clsEnums.enmProcess.tbl_SupplierOrderUpdate : Return "tbl_SupplierOrderUpdate" 
      Case clsEnums.enmProcess.tbl_SupplierOrderView : Return "tbl_SupplierOrderView" 
      Case clsEnums.enmProcess.viw_c_IndexFragmentationDelete : Return "viw_c_IndexFragmentationDelete" 
      Case clsEnums.enmProcess.viw_c_IndexFragmentationUpdate : Return "viw_c_IndexFragmentationUpdate" 
      Case clsEnums.enmProcess.viw_c_IndexFragmentationView : Return "viw_c_IndexFragmentationView" 
      Case clsEnums.enmProcess.viw_c_TableSizeDelete : Return "viw_c_TableSizeDelete" 
      Case clsEnums.enmProcess.viw_c_TableSizeUpdate : Return "viw_c_TableSizeUpdate" 
      Case clsEnums.enmProcess.viw_c_TableSizeView : Return "viw_c_TableSizeView" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcDataDelete : Return "viw_vw_OrderLineCalcDataDelete" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcDataUpdate : Return "viw_vw_OrderLineCalcDataUpdate" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcDataView : Return "viw_vw_OrderLineCalcDataView" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcDelete : Return "viw_vw_OrderLineCalcDelete" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcUpdate : Return "viw_vw_OrderLineCalcUpdate" 
      Case clsEnums.enmProcess.viw_vw_OrderLineCalcView : Return "viw_vw_OrderLineCalcView" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullDataDelete : Return "viw_vw_OrderLineFullDataDelete" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullDataUpdate : Return "viw_vw_OrderLineFullDataUpdate" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullDataView : Return "viw_vw_OrderLineFullDataView" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullDelete : Return "viw_vw_OrderLineFullDelete" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullUpdate : Return "viw_vw_OrderLineFullUpdate" 
      Case clsEnums.enmProcess.viw_vw_OrderLineFullView : Return "viw_vw_OrderLineFullView" 
      Case clsEnums.enmProcess.viw_vwOrderLineCalcDelete : Return "viw_vwOrderLineCalcDelete" 
      Case clsEnums.enmProcess.viw_vwOrderLineCalcUpdate : Return "viw_vwOrderLineCalcUpdate" 
      Case clsEnums.enmProcess.viw_vwOrderLineCalcView : Return "viw_vwOrderLineCalcView" 
      Case clsEnums.enmProcess.viw_vwOrderLineFullDelete : Return "viw_vwOrderLineFullDelete" 
      Case clsEnums.enmProcess.viw_vwOrderLineFullUpdate : Return "viw_vwOrderLineFullUpdate" 
      Case clsEnums.enmProcess.viw_vwOrderLineFullView : Return "viw_vwOrderLineFullView" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vSystemDefaultType As clsEnums.enmSystemDefaultType) As String 
    Select Case vSystemDefaultType 
      Case clsEnums.enmSystemDefaultType.Bit : Return "Bit" 
      Case clsEnums.enmSystemDefaultType.Decimal : Return "Decimal" 
      Case clsEnums.enmSystemDefaultType.Encrypted : Return "Encrypted" 
      Case clsEnums.enmSystemDefaultType.Enum : Return "Enum" 
      Case clsEnums.enmSystemDefaultType.Integer : Return "Integer" 
      Case clsEnums.enmSystemDefaultType.String : Return "String" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vUserIdentificationModel As clsEnums.enmUserIdentificationModel) As String 
    Select Case vUserIdentificationModel 
      Case clsEnums.enmUserIdentificationModel.ByApplicationUser : Return "ByApplicationUser" 
      Case clsEnums.enmUserIdentificationModel.ByDomainGroup : Return "ByDomainGroup" 
      Case clsEnums.enmUserIdentificationModel.ByDomainUser : Return "ByDomainUser" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vUserIdentityType As clsEnums.enmUserIdentityType) As String 
    Select Case vUserIdentityType 
      Case clsEnums.enmUserIdentityType.c_User : Return "c_User" 
      Case clsEnums.enmUserIdentityType.Customer : Return "Customer" 
      Case clsEnums.enmUserIdentityType.Global : Return "Global" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  <Extension> 
  Public Function FastToString(ByVal vWildCardType As clsEnums.enmWildCardType) As String 
    Select Case vWildCardType 
      Case clsEnums.enmWildCardType.After : Return "After" 
      Case clsEnums.enmWildCardType.Before : Return "Before" 
      Case clsEnums.enmWildCardType.BeforeAndAfter : Return "BeforeAndAfter" 
      Case clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter : Return "BeforeAndAfterAndBetweenEachLetter" 
      Case clsEnums.enmWildCardType.None : Return "None" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 

  'ComboList
  <Extension> 
  Public Function FastToString(ByVal vComboListType As clsEnums.enmComboListType) As String 
    Select Case vComboListType 
      Case clsEnums.enmComboListType.ccBeehiveBuyerTrackingDefaultByID : Return "ccBeehiveBuyerTrackingDefaultByID" 
      Case clsEnums.enmComboListType.ccBeehiveBuyerTrackingForCustomerDefaultByID : Return "ccBeehiveBuyerTrackingForCustomerDefaultByID" 
      Case clsEnums.enmComboListType.ccCustomerDefaultByID : Return "ccCustomerDefaultByID" 
      Case clsEnums.enmComboListType.ccCustomerDebtDefaultByID : Return "ccCustomerDebtDefaultByID" 
      Case clsEnums.enmComboListType.ccCustomerDebtForCustomerDefaultByID : Return "ccCustomerDebtForCustomerDefaultByID" 
      Case clsEnums.enmComboListType.ccDeliveryDefaultByID : Return "ccDeliveryDefaultByID" 
      Case clsEnums.enmComboListType.ccOrderHeaderDefaultByID : Return "ccOrderHeaderDefaultByID" 
      Case clsEnums.enmComboListType.ccOrderHeaderForCustomerDefaultByID : Return "ccOrderHeaderForCustomerDefaultByID" 
      Case clsEnums.enmComboListType.ccOrderLineDefaultByID : Return "ccOrderLineDefaultByID" 
      Case clsEnums.enmComboListType.ccProductDefaultByID : Return "ccProductDefaultByID" 
      Case clsEnums.enmComboListType.ccProductPriceDefaultByID : Return "ccProductPriceDefaultByID" 
      Case clsEnums.enmComboListType.ccProductPriceHistDefaultByID : Return "ccProductPriceHistDefaultByID" 
      Case clsEnums.enmComboListType.ccSupplierOrderDefaultByID : Return "ccSupplierOrderDefaultByID" 
      Case clsEnums.enmComboListType.c_AlertMessageDefaultByID : Return "c_AlertMessageDefaultByID" 
      Case clsEnums.enmComboListType.c_EnumerationDefaultByID : Return "c_EnumerationDefaultByID" 
      Case clsEnums.enmComboListType.c_IndexFragmentationDefaultByID : Return "c_IndexFragmentationDefaultByID" 
      Case clsEnums.enmComboListType.c_JobDefaultByID : Return "c_JobDefaultByID" 
      Case clsEnums.enmComboListType.c_LanguageDefaultByID : Return "c_LanguageDefaultByID" 
      Case clsEnums.enmComboListType.c_LoggedAlertDefaultByID : Return "c_LoggedAlertDefaultByID" 
      Case clsEnums.enmComboListType.c_LoggedAlertForAffectedUserDefaultByID : Return "c_LoggedAlertForAffectedUserDefaultByID" 
      Case clsEnums.enmComboListType.c_LoggedLoginDefaultByID : Return "c_LoggedLoginDefaultByID" 
      Case clsEnums.enmComboListType.c_LookupDefaultByID : Return "c_LookupDefaultByID" 
      Case clsEnums.enmComboListType.c_MFADefaultByID : Return "c_MFADefaultByID" 
      Case clsEnums.enmComboListType.c_MFAForUserDefaultByID : Return "c_MFAForUserDefaultByID" 
      Case clsEnums.enmComboListType.c_ObjectToTranslateDefaultByID : Return "c_ObjectToTranslateDefaultByID" 
      Case clsEnums.enmComboListType.c_ProcessDefaultByID : Return "c_ProcessDefaultByID" 
      Case clsEnums.enmComboListType.c_RoleDefaultByID : Return "c_RoleDefaultByID" 
      Case clsEnums.enmComboListType.c_SystemDefaultDefaultByID : Return "c_SystemDefaultDefaultByID" 
      Case clsEnums.enmComboListType.c_TableDefaultByID : Return "c_TableDefaultByID" 
      Case clsEnums.enmComboListType.c_TableSizeDefaultByID : Return "c_TableSizeDefaultByID" 
      Case clsEnums.enmComboListType.c_UserDefaultByID : Return "c_UserDefaultByID" 
      Case clsEnums.enmComboListType.c_UserDefaultNoMasterByID : Return "c_UserDefaultNoMasterByID" 
      Case clsEnums.enmComboListType.c_RoleNonBaseDefaultByID : Return "c_RoleNonBaseDefaultByID" 
      Case clsEnums.enmComboListType.c_RoleWithBaseDefaultByID : Return "c_RoleWithBaseDefaultByID" 
      Case clsEnums.enmComboListType.c_RoleWithBaseAndMasterDefaultByID : Return "c_RoleWithBaseAndMasterDefaultByID" 
      Case clsEnums.enmComboListType.c_RoleWithBaseNoSysAdminDefaultByID : Return "c_RoleWithBaseNoSysAdminDefaultByID" 
      Case clsEnums.enmComboListType.SampleComboQueryOne : Return "SampleComboQueryOne" 
      Case clsEnums.enmComboListType.SampleComboQueryTwo : Return "SampleComboQueryTwo" 
      Case clsEnums.enmComboListType.ccTestComboListFillManual : Return "ccTestComboListFillManual" 
      Case Else 
        Return "UD" 
    End Select 
  End Function 
  
End Module 
 
