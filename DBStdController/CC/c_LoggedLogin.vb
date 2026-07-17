Public Class csLoggedLogin
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
  Implements ITargCCDataReaderUser 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' To be used by the partial class to Override CreateEmpty 
  ''' </summary> 
  Private Event evtOverrideCreateEmpty() 
 
  ''' <summary> 
  ''' Raised before GetByXXX. Used to override the SP. Check rCommand to see what the SP was supposed to be 
  ''' </summary> 
  ''' <param name="rCommandText"></param> 
  ''' <param name="rDALParameters"></param> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeGetWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
 
  ''' <summary> 
  ''' Raised after getting the row from the data store. This also occurs after an update 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterGet()
  Friend Event evtAfterGetWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'Parent Properties 
  Public Enum enmParentProperty 
    UD 
    [UserIdentityType] 
    [UserIdentityTypeName] 
    [Language] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [LoggedAlert] 
    [LoggedRequest] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [UserName] 
    [UserFullName] 
    [TimeLoggedIn] 
    [ApplicationName] 
    [UserIdentityType] 
    [UserIdentityTypeName] 
    [Roles] 
    [TimeLoggedOut] 
    [LoginFaultNumber] 
    [EnvironmentUserName] 
    [EnvironmentMachineName] 
    [EnvironmentUserDomainName] 
    [DnsGetHostName] 
    [AddressList] 
    [ComputerMACAddress] 
    [SystemDiskVolumeSerialNo] 
    [LocalTime] 
    [GmtTime] 
    [AccessingComputerDetails] 
    [UICulture] 
    [TotalPhysicalMemoryKb] 
    [AvailablePhysicalMemoryKb] 
    [ApplicationVersion] 
    [OriginatingIP] 
    [Language] 
    [HostingAssembly] 
    [OriginatingCountry] 
    [DateLoggedIn] 
    [MonthLoggedIn] 
    [ClientReportedIP] 
    [ClientReportedCountry] 
    [IPAdditionalDetails] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [LoginFaultNumber] 
    [TotalPhysicalMemoryKb] 
    [AvailablePhysicalMemoryKb] 
  End Enum 
  ''' <summary> 
  ''' Raised before add, just before evtBeforeUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeAdd(ByRef rCancel As Boolean) 
  Friend Event evtBeforeAddWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after add, just before evtAfterUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterAdd()
  Friend Event evtAfterAddWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'UpdatedColumns
  Public Enum enmUpdateType 
    UD 
    [Standard] 
  End Enum 
  ''' <summary> 
  ''' Raised before updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeUpdate(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean) 
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  ''' <summary> 
  ''' Raised after updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterUpdate(ByVal vWhichColumn As enmUpdateType)
  Friend Event evtAfterUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  ''' <summary> 
  ''' Raised before deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeDelete(ByRef rCancel As Boolean) 
  Friend Event evtBeforeDeleteWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Raised after deleting the row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterDelete() 
  Friend Event evtAfterDeleteWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _UserName As String
  Private _UserFullName As String
  Private _TimeLoggedIn As Date
  Private _ApplicationName As String
  Private _UserIdentityTypeCode As String
  Private _UserIdentityTypeText As String 
  Private _UserIdentityTypeNameCode As Integer
  Private _UserIdentityTypeNameText As String 
  Private _Roles As String
  Private _TimeLoggedOut As Date
  Private _LoginFaultNumber As Integer
  Private _EnvironmentUserName As String
  Private _EnvironmentMachineName As String
  Private _EnvironmentUserDomainName As String
  Private _DnsGetHostName As String
  Private _AddressList As String
  Private _ComputerMACAddress As String
  Private _SystemDiskVolumeSerialNo As String
  Private _LocalTime As Date
  Private _GmtTime As Date
  Private _AccessingComputerDetails As String
  Private _UICulture As String
  Private _TotalPhysicalMemoryKb As Long
  Private _AvailablePhysicalMemoryKb As Long
  Private _ApplicationVersion As String
  Private _OriginatingIP As String
  Private _Language As clsEnums.enmLanguage
  Private _LanguageText As String 
  Private _HostingAssembly As String
  Private _OriginatingCountry As String
  Private _DateLoggedIn As Date
  Private _MonthLoggedIn As Date
  Private _ClientReportedIP As String
  Private _ClientReportedCountry As String
  Private _IPAdditionalDetails As String
  Private _Tag As String
  Private _LoggedAlerts As csLoggedAlertCol
  Private _LoggedRequests As csLoggedRequestCol
  
  Public Property [ID]() As Long
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Long)
      If Me._ID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ID = value 
        bPrimaryKey = _ID 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [UserName]() As String
    Get
      Return Me._UserName
    End Get
    Set(ByVal value As String)
      If Me._UserName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserName = value 
      End If 
    End Set
  End Property
  Public Property [UserFullName]() As String
    Get
      Return Me._UserFullName
    End Get
    Set(ByVal value As String)
      If Me._UserFullName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserFullName = value 
      End If 
    End Set
  End Property
  Public Property [TimeLoggedIn]() As Date
    Get
      Return Me._TimeLoggedIn
    End Get
    Set(ByVal value As Date)
      If Me._TimeLoggedIn <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TimeLoggedIn = value 
      End If 
    End Set
  End Property
  Public Property [ApplicationName]() As String
    Get
      Return Me._ApplicationName
    End Get
    Set(ByVal value As String)
      If Me._ApplicationName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ApplicationName = value 
      End If 
    End Set
  End Property
  Public Property [UserIdentityTypeCode]() As String
    Get
      Return Me._UserIdentityTypeCode
    End Get
    Set(ByVal value As String)
      If Me._UserIdentityTypeCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserIdentityTypeCode = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property UserIdentityTypeText() As String
    Get
      Return Me._UserIdentityTypeText
    End Get
    Set(ByVal value As String)
      Me._UserIdentityTypeText = value
    End Set
  End Property
  Public Property [UserIdentityTypeNameCode]() As Integer
    Get
      Return Me._UserIdentityTypeNameCode
    End Get
    Set(ByVal value As Integer)
      If Me._UserIdentityTypeNameCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserIdentityTypeNameCode = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property UserIdentityTypeNameText() As String
    Get
      Return Me._UserIdentityTypeNameText
    End Get
    Set(ByVal value As String)
      Me._UserIdentityTypeNameText = value
    End Set
  End Property
  Public Property [Roles]() As String
    Get
      Return Me._Roles
    End Get
    Set(ByVal value As String)
      If Me._Roles <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Roles = value 
      End If 
    End Set
  End Property
  Public Property [TimeLoggedOut]() As Date
    Get
      Return Me._TimeLoggedOut
    End Get
    Set(ByVal value As Date)
      If Me._TimeLoggedOut <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TimeLoggedOut = value 
      End If 
    End Set
  End Property
  Public Property [LoginFaultNumber]() As Integer
    Get
      Return Me._LoginFaultNumber
    End Get
    Set(ByVal value As Integer)
      If Me._LoginFaultNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoginFaultNumber = value 
      End If 
    End Set
  End Property
  Public Property [EnvironmentUserName]() As String
    Get
      Return Me._EnvironmentUserName
    End Get
    Set(ByVal value As String)
      If Me._EnvironmentUserName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnvironmentUserName = value 
      End If 
    End Set
  End Property
  Public Property [EnvironmentMachineName]() As String
    Get
      Return Me._EnvironmentMachineName
    End Get
    Set(ByVal value As String)
      If Me._EnvironmentMachineName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnvironmentMachineName = value 
      End If 
    End Set
  End Property
  Public Property [EnvironmentUserDomainName]() As String
    Get
      Return Me._EnvironmentUserDomainName
    End Get
    Set(ByVal value As String)
      If Me._EnvironmentUserDomainName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnvironmentUserDomainName = value 
      End If 
    End Set
  End Property
  Public Property [DnsGetHostName]() As String
    Get
      Return Me._DnsGetHostName
    End Get
    Set(ByVal value As String)
      If Me._DnsGetHostName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DnsGetHostName = value 
      End If 
    End Set
  End Property
  Public Property [AddressList]() As String
    Get
      Return Me._AddressList
    End Get
    Set(ByVal value As String)
      If Me._AddressList <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AddressList = value 
      End If 
    End Set
  End Property
  Public Property [ComputerMACAddress]() As String
    Get
      Return Me._ComputerMACAddress
    End Get
    Set(ByVal value As String)
      If Me._ComputerMACAddress <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ComputerMACAddress = value 
      End If 
    End Set
  End Property
  Public Property [SystemDiskVolumeSerialNo]() As String
    Get
      Return Me._SystemDiskVolumeSerialNo
    End Get
    Set(ByVal value As String)
      If Me._SystemDiskVolumeSerialNo <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SystemDiskVolumeSerialNo = value 
      End If 
    End Set
  End Property
  Public Property [LocalTime]() As Date
    Get
      Return Me._LocalTime
    End Get
    Set(ByVal value As Date)
      If Me._LocalTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LocalTime = value 
      End If 
    End Set
  End Property
  Public Property [GmtTime]() As Date
    Get
      Return Me._GmtTime
    End Get
    Set(ByVal value As Date)
      If Me._GmtTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._GmtTime = value 
      End If 
    End Set
  End Property
  Public Property [AccessingComputerDetails]() As String
    Get
      Return Me._AccessingComputerDetails
    End Get
    Set(ByVal value As String)
      If Me._AccessingComputerDetails <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AccessingComputerDetails = value 
      End If 
    End Set
  End Property
  Public Property [UICulture]() As String
    Get
      Return Me._UICulture
    End Get
    Set(ByVal value As String)
      If Me._UICulture <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UICulture = value 
      End If 
    End Set
  End Property
  Public Property [TotalPhysicalMemoryKb]() As Long
    Get
      Return Me._TotalPhysicalMemoryKb
    End Get
    Set(ByVal value As Long)
      If Me._TotalPhysicalMemoryKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TotalPhysicalMemoryKb = value 
      End If 
    End Set
  End Property
  Public Property [AvailablePhysicalMemoryKb]() As Long
    Get
      Return Me._AvailablePhysicalMemoryKb
    End Get
    Set(ByVal value As Long)
      If Me._AvailablePhysicalMemoryKb <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AvailablePhysicalMemoryKb = value 
      End If 
    End Set
  End Property
  Public Property [ApplicationVersion]() As String
    Get
      Return Me._ApplicationVersion
    End Get
    Set(ByVal value As String)
      If Me._ApplicationVersion <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ApplicationVersion = value 
      End If 
    End Set
  End Property
  Public Property [OriginatingIP]() As String
    Get
      Return Me._OriginatingIP
    End Get
    Set(ByVal value As String)
      If Me._OriginatingIP <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OriginatingIP = value 
      End If 
    End Set
  End Property
  Public Property [Language]() As clsEnums.enmLanguage
    Get
      Return Me._Language
    End Get
    Set(ByVal value As clsEnums.enmLanguage)
      If Me._Language <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Language = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [LanguageText]() As String
    Get
      Return Me._LanguageText
    End Get
    Set(ByVal value As String)
      Me._LanguageText = value
    End Set
  End Property
  Public Property [HostingAssembly]() As String
    Get
      Return Me._HostingAssembly
    End Get
    Set(ByVal value As String)
      If Me._HostingAssembly <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._HostingAssembly = value 
      End If 
    End Set
  End Property
  Public Property [OriginatingCountry]() As String
    Get
      Return Me._OriginatingCountry
    End Get
    Set(ByVal value As String)
      If Me._OriginatingCountry <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OriginatingCountry = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [DateLoggedIn]() As Date
    Get
      Return Me._DateLoggedIn
    End Get
  End Property
  Public ReadOnly Property [MonthLoggedIn]() As Date
    Get
      Return Me._MonthLoggedIn
    End Get
  End Property
  Public Property [ClientReportedIP]() As String
    Get
      Return Me._ClientReportedIP
    End Get
    Set(ByVal value As String)
      If Me._ClientReportedIP <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ClientReportedIP = value 
      End If 
    End Set
  End Property
  Public Property [ClientReportedCountry]() As String
    Get
      Return Me._ClientReportedCountry
    End Get
    Set(ByVal value As String)
      If Me._ClientReportedCountry <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ClientReportedCountry = value 
      End If 
    End Set
  End Property
  Public Property [IPAdditionalDetails]() As String
    Get
      Return Me._IPAdditionalDetails
    End Get
    Set(ByVal value As String)
      If Me._IPAdditionalDetails <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IPAdditionalDetails = value 
      End If 
    End Set
  End Property
  ''' <summary> 
  ''' Extra property that is not stored in the database. Setting it does not trip the status to 'Dirty' 
  ''' </summary> 
  ''' <returns></returns> 
  <Newtonsoft.Json.JsonIgnore, Xml.Serialization.XmlIgnore> 
  Public Property [Tag]() As String
    Get
      Return Me._Tag
    End Get
    Set(ByVal value As String)
      If Me._Tag <> value Then 
        Me._Tag = value 
      End If 
    End Set
  End Property
  Public Property [LoggedAlerts]() As csLoggedAlertCol
    Get
      Return Me._LoggedAlerts
    End Get
    Set(ByVal value As csLoggedAlertCol)
      Me._LoggedAlerts = value
    End Set
  End Property
  Public Property [LoggedRequests]() As csLoggedRequestCol
    Get
      Return Me._LoggedRequests
    End Get
    Set(ByVal value As csLoggedRequestCol)
      Me._LoggedRequests = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _ID.ToString() Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _UserName <> "" Then pValue.Append("UserName='" & _UserName & "' ‡ ") 
    If _UserFullName <> "" Then pValue.Append("UserFullName='" & _UserFullName & "' ‡ ") 
    If Not (_TimeLoggedIn = Nothing) Then pValue.Append("TimeLoggedIn='" & _TimeLoggedIn.ToString("o") & "' ‡ ") 
    If _ApplicationName <> "" Then pValue.Append("ApplicationName='" & _ApplicationName & "' ‡ ") 
    If _UserIdentityTypeCode <> "" Then pValue.Append("UserIdentityTypeCode='" & _UserIdentityTypeCode & "' ‡ ") 
    If _UserIdentityTypeText <> "" Then pValue.Append("UserIdentityTypeText='" & _UserIdentityTypeText & "' ‡ ") 
    If _UserIdentityTypeNameCode <> -1 Then pValue.Append("UserIdentityTypeNameCode='" & _UserIdentityTypeNameCode.ToString() & "' ‡ ") 
    If _UserIdentityTypeNameText <> "" Then pValue.Append("UserIdentityTypeNameText='" & _UserIdentityTypeNameText & "' ‡ ") 
    If _Roles <> "" Then pValue.Append("Roles='" & _Roles & "' ‡ ") 
    If Not (_TimeLoggedOut = Nothing) Then pValue.Append("TimeLoggedOut='" & _TimeLoggedOut.ToString("o") & "' ‡ ") 
    If _LoginFaultNumber <> 0 Then pValue.Append("LoginFaultNumber='" & _LoginFaultNumber.ToString() & "' ‡ ") 
    If _EnvironmentUserName <> "" Then pValue.Append("EnvironmentUserName='" & _EnvironmentUserName & "' ‡ ") 
    If _EnvironmentMachineName <> "" Then pValue.Append("EnvironmentMachineName='" & _EnvironmentMachineName & "' ‡ ") 
    If _EnvironmentUserDomainName <> "" Then pValue.Append("EnvironmentUserDomainName='" & _EnvironmentUserDomainName & "' ‡ ") 
    If _DnsGetHostName <> "" Then pValue.Append("DnsGetHostName='" & _DnsGetHostName & "' ‡ ") 
    If _AddressList <> "" Then pValue.Append("AddressList='" & _AddressList & "' ‡ ") 
    If _ComputerMACAddress <> "" Then pValue.Append("ComputerMACAddress='" & _ComputerMACAddress & "' ‡ ") 
    If _SystemDiskVolumeSerialNo <> "" Then pValue.Append("SystemDiskVolumeSerialNo='" & _SystemDiskVolumeSerialNo & "' ‡ ") 
    If Not (_LocalTime = Nothing) Then pValue.Append("LocalTime='" & _LocalTime.ToString("o") & "' ‡ ") 
    If Not (_GmtTime = Nothing) Then pValue.Append("GmtTime='" & _GmtTime.ToString("o") & "' ‡ ") 
    If _AccessingComputerDetails <> "" Then pValue.Append("AccessingComputerDetails='" & _AccessingComputerDetails & "' ‡ ") 
    If _UICulture <> "" Then pValue.Append("UICulture='" & _UICulture & "' ‡ ") 
    If _TotalPhysicalMemoryKb <> 0 Then pValue.Append("TotalPhysicalMemoryKb='" & _TotalPhysicalMemoryKb.ToString() & "' ‡ ") 
    If _AvailablePhysicalMemoryKb <> 0 Then pValue.Append("AvailablePhysicalMemoryKb='" & _AvailablePhysicalMemoryKb.ToString() & "' ‡ ") 
    If _ApplicationVersion <> "" Then pValue.Append("ApplicationVersion='" & _ApplicationVersion & "' ‡ ") 
    If _OriginatingIP <> "" Then pValue.Append("OriginatingIP='" & _OriginatingIP & "' ‡ ") 
    If _Language <> clsEnums.enmLanguage.UD Then pValue.Append("Language='" & _Language.FastToString() & "' ‡ ") 
    If _LanguageText <> "" Then pValue.Append("LanguageText='" & _LanguageText & "' ‡ ") 
    If _HostingAssembly <> "" Then pValue.Append("HostingAssembly='" & _HostingAssembly & "' ‡ ") 
    If _OriginatingCountry <> "" Then pValue.Append("OriginatingCountry='" & _OriginatingCountry & "' ‡ ") 
    If Not (_DateLoggedIn = Nothing) Then pValue.Append("DateLoggedIn='" & _DateLoggedIn.ToString("o") & "' ‡ ") 
    If Not (_MonthLoggedIn = Nothing) Then pValue.Append("MonthLoggedIn='" & _MonthLoggedIn.ToString("o") & "' ‡ ") 
    If _ClientReportedIP <> "" Then pValue.Append("ClientReportedIP='" & _ClientReportedIP & "' ‡ ") 
    If _ClientReportedCountry <> "" Then pValue.Append("ClientReportedCountry='" & _ClientReportedCountry & "' ‡ ") 
    If _IPAdditionalDetails <> "" Then pValue.Append("IPAdditionalDetails='" & _IPAdditionalDetails & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UserName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UserFullName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TimeLoggedIn.ToShortDateString & " " & _TimeLoggedIn.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ApplicationName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeCode)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeText)}""") 
    pCSV.Append("," & _UserIdentityTypeNameCode.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeNameText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Roles)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TimeLoggedOut.ToShortDateString & " " & _TimeLoggedOut.ToShortTimeString)}""") 
    pCSV.Append("," & _LoginFaultNumber.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EnvironmentUserName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EnvironmentMachineName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_EnvironmentUserDomainName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DnsGetHostName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AddressList)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ComputerMACAddress)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SystemDiskVolumeSerialNo)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LocalTime.ToShortDateString & " " & _LocalTime.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_GmtTime.ToShortDateString & " " & _GmtTime.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AccessingComputerDetails)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UICulture)}""") 
    pCSV.Append("," & _TotalPhysicalMemoryKb.ToString() & "") 
    pCSV.Append("," & _AvailablePhysicalMemoryKb.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ApplicationVersion)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OriginatingIP)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Language.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LanguageText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_HostingAssembly)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OriginatingCountry)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DateLoggedIn.ToShortDateString & " " & _DateLoggedIn.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MonthLoggedIn.ToShortDateString & " " & _MonthLoggedIn.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ClientReportedIP)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ClientReportedCountry)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_IPAdditionalDetails)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsLoggedLogin As csLoggedLogin)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsLoggedLogin) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vUserName As String = "" _ 
    , Optional vUserFullName As String = "" _ 
    , Optional vTimeLoggedIn As Date = Nothing _ 
    , Optional vApplicationName As String = "" _ 
    , Optional vUserIdentityTypeCode As String = "" _ 
    , Optional vUserIdentityTypeText As String = "" _ 
    , Optional vUserIdentityTypeNameCode As Integer = -1 _ 
    , Optional vUserIdentityTypeNameText As String = "" _ 
    , Optional vRoles As String = "" _ 
    , Optional vTimeLoggedOut As Date = Nothing _ 
    , Optional vLoginFaultNumber As Integer = 0 _ 
    , Optional vEnvironmentUserName As String = "" _ 
    , Optional vEnvironmentMachineName As String = "" _ 
    , Optional vEnvironmentUserDomainName As String = "" _ 
    , Optional vDnsGetHostName As String = "" _ 
    , Optional vAddressList As String = "" _ 
    , Optional vComputerMACAddress As String = "" _ 
    , Optional vSystemDiskVolumeSerialNo As String = "" _ 
    , Optional vLocalTime As Date = Nothing _ 
    , Optional vGmtTime As Date = Nothing _ 
    , Optional vAccessingComputerDetails As String = "" _ 
    , Optional vUICulture As String = "" _ 
    , Optional vTotalPhysicalMemoryKb As Long = 0 _ 
    , Optional vAvailablePhysicalMemoryKb As Long = 0 _ 
    , Optional vApplicationVersion As String = "" _ 
    , Optional vOriginatingIP As String = "" _ 
    , Optional vLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.UD _ 
    , Optional vLanguageText As String = "" _ 
    , Optional vHostingAssembly As String = "" _ 
    , Optional vOriginatingCountry As String = "" _ 
    , Optional vDateLoggedIn As Date = Nothing _ 
    , Optional vMonthLoggedIn As Date = Nothing _ 
    , Optional vClientReportedIP As String = "" _ 
    , Optional vClientReportedCountry As String = "" _ 
    , Optional vIPAdditionalDetails As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _UserName = vUserName 
    _UserFullName = vUserFullName 
    _TimeLoggedIn = vTimeLoggedIn 
    _ApplicationName = vApplicationName 
    _UserIdentityTypeCode = vUserIdentityTypeCode 
    _UserIdentityTypeText = vUserIdentityTypeText 
    _UserIdentityTypeNameCode = vUserIdentityTypeNameCode 
    _UserIdentityTypeNameText = vUserIdentityTypeNameText 
    _Roles = vRoles 
    _TimeLoggedOut = vTimeLoggedOut 
    _LoginFaultNumber = vLoginFaultNumber 
    _EnvironmentUserName = vEnvironmentUserName 
    _EnvironmentMachineName = vEnvironmentMachineName 
    _EnvironmentUserDomainName = vEnvironmentUserDomainName 
    _DnsGetHostName = vDnsGetHostName 
    _AddressList = vAddressList 
    _ComputerMACAddress = vComputerMACAddress 
    _SystemDiskVolumeSerialNo = vSystemDiskVolumeSerialNo 
    _LocalTime = vLocalTime 
    _GmtTime = vGmtTime 
    _AccessingComputerDetails = vAccessingComputerDetails 
    _UICulture = vUICulture 
    _TotalPhysicalMemoryKb = vTotalPhysicalMemoryKb 
    _AvailablePhysicalMemoryKb = vAvailablePhysicalMemoryKb 
    _ApplicationVersion = vApplicationVersion 
    _OriginatingIP = vOriginatingIP 
    _Language = vLanguage 
    _LanguageText = vLanguageText 
    _HostingAssembly = vHostingAssembly 
    _OriginatingCountry = vOriginatingCountry 
    _DateLoggedIn = vDateLoggedIn 
    _MonthLoggedIn = vMonthLoggedIn 
    _ClientReportedIP = vClientReportedIP 
    _ClientReportedCountry = vClientReportedCountry 
    _IPAdditionalDetails = vIPAdditionalDetails 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
 
  End Sub 
 
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New()
    CreateEmpty()
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  Private _IsTruncated As Boolean = False 
  
  ''' <summary> 
  ''' Use this before loading a DataGridView. You don't need more than X c to see what you want. 
  ''' </summary> 
  ''' <param name="pTruncateLength"></param> 
  Friend Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
    'Truncates strings, and reduces pictures to W 100 x H 50 
 
    _IsTruncated = False 
 
    _UserName = _UserName.Truncate(pTruncateLength, _IsTruncated) 
    _UserFullName = _UserFullName.Truncate(pTruncateLength, _IsTruncated) 
    _ApplicationName = _ApplicationName.Truncate(pTruncateLength, _IsTruncated) 
    _UserIdentityTypeCode = _UserIdentityTypeCode.Truncate(pTruncateLength, _IsTruncated) 
    _Roles = _Roles.Truncate(pTruncateLength, _IsTruncated) 
    _EnvironmentUserName = _EnvironmentUserName.Truncate(pTruncateLength, _IsTruncated) 
    _EnvironmentMachineName = _EnvironmentMachineName.Truncate(pTruncateLength, _IsTruncated) 
    _EnvironmentUserDomainName = _EnvironmentUserDomainName.Truncate(pTruncateLength, _IsTruncated) 
    _DnsGetHostName = _DnsGetHostName.Truncate(pTruncateLength, _IsTruncated) 
    _AddressList = _AddressList.Truncate(pTruncateLength, _IsTruncated) 
    _ComputerMACAddress = _ComputerMACAddress.Truncate(pTruncateLength, _IsTruncated) 
    _SystemDiskVolumeSerialNo = _SystemDiskVolumeSerialNo.Truncate(pTruncateLength, _IsTruncated) 
    _AccessingComputerDetails = _AccessingComputerDetails.Truncate(pTruncateLength, _IsTruncated) 
    _UICulture = _UICulture.Truncate(pTruncateLength, _IsTruncated) 
    _ApplicationVersion = _ApplicationVersion.Truncate(pTruncateLength, _IsTruncated) 
    _OriginatingIP = _OriginatingIP.Truncate(pTruncateLength, _IsTruncated) 
    _HostingAssembly = _HostingAssembly.Truncate(pTruncateLength, _IsTruncated) 
    _OriginatingCountry = _OriginatingCountry.Truncate(pTruncateLength, _IsTruncated) 
    _ClientReportedIP = _ClientReportedIP.Truncate(pTruncateLength, _IsTruncated) 
    _ClientReportedCountry = _ClientReportedCountry.Truncate(pTruncateLength, _IsTruncated) 
    _IPAdditionalDetails = _IPAdditionalDetails.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _UserName = ccHelper.RemoveChrW0(_UserName) 
    _UserFullName = ccHelper.RemoveChrW0(_UserFullName) 
    _ApplicationName = ccHelper.RemoveChrW0(_ApplicationName) 
    _UserIdentityTypeCode = ccHelper.RemoveChrW0(_UserIdentityTypeCode) 
    _Roles = ccHelper.RemoveChrW0(_Roles) 
    _EnvironmentUserName = ccHelper.RemoveChrW0(_EnvironmentUserName) 
    _EnvironmentMachineName = ccHelper.RemoveChrW0(_EnvironmentMachineName) 
    _EnvironmentUserDomainName = ccHelper.RemoveChrW0(_EnvironmentUserDomainName) 
    _DnsGetHostName = ccHelper.RemoveChrW0(_DnsGetHostName) 
    _AddressList = ccHelper.RemoveChrW0(_AddressList) 
    _ComputerMACAddress = ccHelper.RemoveChrW0(_ComputerMACAddress) 
    _SystemDiskVolumeSerialNo = ccHelper.RemoveChrW0(_SystemDiskVolumeSerialNo) 
    _AccessingComputerDetails = ccHelper.RemoveChrW0(_AccessingComputerDetails) 
    _UICulture = ccHelper.RemoveChrW0(_UICulture) 
    _ApplicationVersion = ccHelper.RemoveChrW0(_ApplicationVersion) 
    _OriginatingIP = ccHelper.RemoveChrW0(_OriginatingIP) 
    _HostingAssembly = ccHelper.RemoveChrW0(_HostingAssembly) 
    _OriginatingCountry = ccHelper.RemoveChrW0(_OriginatingCountry) 
    _ClientReportedIP = ccHelper.RemoveChrW0(_ClientReportedIP) 
    _ClientReportedCountry = ccHelper.RemoveChrW0(_ClientReportedCountry) 
    _IPAdditionalDetails = ccHelper.RemoveChrW0(_IPAdditionalDetails) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedLogin by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLogin_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedLogin-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedLogin by the chosen parameters. This function may be a bit slower than accessing the LoggedLogin's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLogin_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedLogin-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedLogin-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the LoggedLogin by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLogin_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"LoggedLogin not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-LoggedLogin-210927-1527", vRequester, vAdditionalMessageToUser:=$"LoggedLogin not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccLoggedLoginCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginGetByID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vID) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"LoggedLogin not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-LoggedLogin-210625-0950", vRequester, vAdditionalMessageToUser:=$"LoggedLogin not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginUpdate, "csLoggedLogin_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedLogin-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginUpdate, "csLoggedLogin_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedLogin-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the LoggedLogin. If there are parents or children in the LoggedLogin, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Friend Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginUpdate, "csLoggedLogin_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pLoggedLogin As New csLoggedLogin() 
    If Me.isEqual(pLoggedLogin) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-LoggedLogin-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-LoggedLogin-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_LoggedLoginUpdate"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
    
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Standard, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Standard, pCommandText, pDALParameters, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pCachedLoggedLogin As csLoggedLogin 
      If _ID = 0 Then 
        pCachedLoggedLogin = New csLoggedLogin() 
        'get last ID 
        Dim pLoggedLoginCol As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.Clone() 
        If pLoggedLoginCol.Count = 0 Then 
          _ID = 1 
        Else 
          pLoggedLoginCol.SortByID() 
          Dim pLastID As Long = pLoggedLoginCol(pLoggedLoginCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccLoggedLoginCol.Add(pCachedLoggedLogin) 
      Else  
        pCachedLoggedLogin = MyController.DBCache.ccLoggedLoginCol.FindByID(_ID) 
      End If 
      pCachedLoggedLogin.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedLoginCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_UserName) 
        pLastReadVariableName = "UserFullName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_UserFullName) 
        pLastReadVariableName = "TimeLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_TimeLoggedIn) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_ApplicationName) 
        pLastReadVariableName = "lkpUserIdentityType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.LookupNullable(_UserIdentityTypeCode) 
        pLastReadVariableName = "lkpUserIdentityTypeName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = ccHelper.LookupNullable(_UserIdentityTypeNameCode) 
        pLastReadVariableName = "Roles" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 250).Value = ccHelper.ObjectNullable(_Roles) 
        pLastReadVariableName = "TimeLoggedOut" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_TimeLoggedOut) 
        pLastReadVariableName = "LoginFaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_LoginFaultNumber) 
        pLastReadVariableName = "EnvironmentUserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_EnvironmentUserName) 
        pLastReadVariableName = "EnvironmentMachineName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_EnvironmentMachineName) 
        pLastReadVariableName = "EnvironmentUserDomainName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NChar, 10).Value = ccHelper.ObjectNullable(_EnvironmentUserDomainName) 
        pLastReadVariableName = "DnsGetHostName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_DnsGetHostName) 
        pLastReadVariableName = "AddressList" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_AddressList) 
        pLastReadVariableName = "ComputerMACAddress" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_ComputerMACAddress) 
        pLastReadVariableName = "SystemDiskVolumeSerialNo" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_SystemDiskVolumeSerialNo) 
        pLastReadVariableName = "LocalTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_LocalTime) 
        pLastReadVariableName = "GmtTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_GmtTime) 
        pLastReadVariableName = "AccessingComputerDetails" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 250).Value = ccHelper.ObjectNullable(_AccessingComputerDetails) 
        pLastReadVariableName = "UICulture" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_UICulture) 
        pLastReadVariableName = "TotalPhysicalMemoryKb" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_TotalPhysicalMemoryKb) 
        pLastReadVariableName = "AvailablePhysicalMemoryKb" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_AvailablePhysicalMemoryKb) 
        pLastReadVariableName = "ApplicationVersion" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 250).Value = ccHelper.ObjectNullable(_ApplicationVersion) 
        pLastReadVariableName = "OriginatingIP" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_OriginatingIP) 
        pLastReadVariableName = "enmLanguage" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = (_Language.FastToString()) 
        pLastReadVariableName = "HostingAssembly" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_HostingAssembly) 
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = ccHelper.ObjectNullable(_OriginatingCountry) 
        pLastReadVariableName = "ClientReportedIP" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_ClientReportedIP) 
        pLastReadVariableName = "ClientReportedCountry" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = ccHelper.ObjectNullable(_ClientReportedCountry) 
        pLastReadVariableName = "IPAdditionalDetails" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 250).Value = ccHelper.ObjectNullable(_IPAdditionalDetails) 
        pLastReadVariableName = "" 
        
        'Execute query 
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'Now get the ID 
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            pID = pTargCCReader.GetInt64(0) 
            _ID = pID 
            bPrimaryKey = pID 
            If pID = 0 Then 
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Children 
      Dim pLoggedAlerts As csLoggedAlertCol = _LoggedAlerts 
      Dim pLoggedRequests As csLoggedRequestCol = _LoggedRequests 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Children 
      If Not pLoggedAlerts Is Nothing Then _LoggedAlerts = pLoggedAlerts 
      If Not pLoggedRequests Is Nothing Then _LoggedRequests = pLoggedRequests 
      
    End If 
  
    If pObjectAdded = True Then 
      RaiseEvent evtAfterAdd() 
      RaiseEvent evtAfterAddWithRequester(vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
    End If 
    RaiseEvent evtAfterUpdate(enmUpdateType.Standard)
    RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Standard, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("LoggedLogin.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLogin_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_LoggedLoginDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedLoginCol.Remove(MyController.DBCache.ccLoggedLoginCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedLoginCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
 
        'Execute query 
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expected to get -1 back 
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090623-1813", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
          
    RaiseEvent evtAfterDelete()
    RaiseEvent evtAfterDeleteWithRequester(vRequester, pFault) : If pFault.isOK = False Then Return pFault 
          
    CreateEmpty()
          
    Return pFault
  End Function
  
  ''' <summary> 
  ''' This function enables you to delete an entity from the database without first loading it. 
  ''' </summary> 
  ''' <param name="vID"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function DeleteByID(vID As Long, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"ID: {vID}" 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLogin_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedLoginCol.Remove(MyController.DBCache.ccLoggedLoginCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedLoginCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the LoggedLogin's LoggedAlert collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedAlerts(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLogin_FillLoggedAlerts", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _LoggedAlerts = New csLoggedAlertCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedAlerts.FillByLoggedLoginID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the LoggedLogin's LoggedRequest collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedRequests(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLogin_FillLoggedRequests", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _LoggedRequests = New csLoggedRequestCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedRequests.FillByLoggedLoginID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csLoggedLogin) Then Return False 
    Dim pLoggedLoginToTest As csLoggedLogin = CType(vTargCCEntityToTest, csLoggedLogin) 
    Return isEqual(pLoggedLoginToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vLoggedLoginToTest As csLoggedLogin) As Boolean
    With vLoggedLoginToTest
      If _ID <> .ID Then Return False
      If _UserName <> .UserName Then Return False
      If _UserFullName <> .UserFullName Then Return False
      If _TimeLoggedIn <> Nothing AndAlso .TimeLoggedIn <> Nothing Then 
        If ccHelper.ToLong(_TimeLoggedIn.Subtract(.TimeLoggedIn).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_TimeLoggedIn = Nothing AndAlso .TimeLoggedIn = Nothing) Then 
        Return False 
      End If 
      If _ApplicationName <> .ApplicationName Then Return False
      If _UserIdentityTypeCode <> .UserIdentityTypeCode Then Return False
      If _UserIdentityTypeNameCode <> .UserIdentityTypeNameCode Then Return False
      If _Roles <> .Roles Then Return False
      If _TimeLoggedOut <> Nothing AndAlso .TimeLoggedOut <> Nothing Then 
        If ccHelper.ToLong(_TimeLoggedOut.Subtract(.TimeLoggedOut).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_TimeLoggedOut = Nothing AndAlso .TimeLoggedOut = Nothing) Then 
        Return False 
      End If 
      If _LoginFaultNumber <> .LoginFaultNumber Then Return False
      If _EnvironmentUserName <> .EnvironmentUserName Then Return False
      If _EnvironmentMachineName <> .EnvironmentMachineName Then Return False
      If _EnvironmentUserDomainName <> .EnvironmentUserDomainName Then Return False
      If _DnsGetHostName <> .DnsGetHostName Then Return False
      If _AddressList <> .AddressList Then Return False
      If _ComputerMACAddress <> .ComputerMACAddress Then Return False
      If _SystemDiskVolumeSerialNo <> .SystemDiskVolumeSerialNo Then Return False
      If _LocalTime <> Nothing AndAlso .LocalTime <> Nothing Then 
        If ccHelper.ToLong(_LocalTime.Subtract(.LocalTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LocalTime = Nothing AndAlso .LocalTime = Nothing) Then 
        Return False 
      End If 
      If _GmtTime <> Nothing AndAlso .GmtTime <> Nothing Then 
        If ccHelper.ToLong(_GmtTime.Subtract(.GmtTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_GmtTime = Nothing AndAlso .GmtTime = Nothing) Then 
        Return False 
      End If 
      If _AccessingComputerDetails <> .AccessingComputerDetails Then Return False
      If _UICulture <> .UICulture Then Return False
      If _TotalPhysicalMemoryKb <> .TotalPhysicalMemoryKb Then Return False
      If _AvailablePhysicalMemoryKb <> .AvailablePhysicalMemoryKb Then Return False
      If _ApplicationVersion <> .ApplicationVersion Then Return False
      If _OriginatingIP <> .OriginatingIP Then Return False
      If _Language <> .Language Then Return False
      If _HostingAssembly <> .HostingAssembly Then Return False
      If _OriginatingCountry <> .OriginatingCountry Then Return False
      If _DateLoggedIn <> Nothing AndAlso .DateLoggedIn <> Nothing Then 
        If ccHelper.ToLong(_DateLoggedIn.Subtract(.DateLoggedIn).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DateLoggedIn = Nothing AndAlso .DateLoggedIn = Nothing) Then 
        Return False 
      End If 
      If _MonthLoggedIn <> Nothing AndAlso .MonthLoggedIn <> Nothing Then 
        If ccHelper.ToLong(_MonthLoggedIn.Subtract(.MonthLoggedIn).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_MonthLoggedIn = Nothing AndAlso .MonthLoggedIn = Nothing) Then 
        Return False 
      End If 
      If _ClientReportedIP <> .ClientReportedIP Then Return False
      If _ClientReportedCountry <> .ClientReportedCountry Then Return False
      If _IPAdditionalDetails <> .IPAdditionalDetails Then Return False
      If _Tag <> .Tag Then Return False
      If bDateAdded <> .DateAdded Then Return False 
      If bccStatus <> .ccStatus Then Return False 
    End With
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are equal, IGNORING the dependants 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCEntity() As ITargCCEntity 
    Dim pClone As New csLoggedLogin(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedLogin
    Dim pClone As New csLoggedLogin(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserName") = _UserName : Catch ex As Exception : Return pFault.LogException(ex, "UserName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserFullName") = _UserFullName : Catch ex As Exception : Return pFault.LogException(ex, "UserFullName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("TimeLoggedIn") = _TimeLoggedIn : Catch ex As Exception : Return pFault.LogException(ex, "TimeLoggedIn", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApplicationName") = _ApplicationName : Catch ex As Exception : Return pFault.LogException(ex, "ApplicationName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserIdentityTypeCode") = _UserIdentityTypeCode : Catch ex As Exception : Return pFault.LogException(ex, "UserIdentityTypeCode", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserIdentityTypeNameCode") = _UserIdentityTypeNameCode : Catch ex As Exception : Return pFault.LogException(ex, "UserIdentityTypeNameCode", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("Roles") = _Roles : Catch ex As Exception : Return pFault.LogException(ex, "Roles", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("TimeLoggedOut") = _TimeLoggedOut : Catch ex As Exception : Return pFault.LogException(ex, "TimeLoggedOut", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoginFaultNumber") = _LoginFaultNumber : Catch ex As Exception : Return pFault.LogException(ex, "LoginFaultNumber", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnvironmentUserName") = _EnvironmentUserName : Catch ex As Exception : Return pFault.LogException(ex, "EnvironmentUserName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnvironmentMachineName") = _EnvironmentMachineName : Catch ex As Exception : Return pFault.LogException(ex, "EnvironmentMachineName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnvironmentUserDomainName") = _EnvironmentUserDomainName : Catch ex As Exception : Return pFault.LogException(ex, "EnvironmentUserDomainName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("DnsGetHostName") = _DnsGetHostName : Catch ex As Exception : Return pFault.LogException(ex, "DnsGetHostName", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("AddressList") = _AddressList : Catch ex As Exception : Return pFault.LogException(ex, "AddressList", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("ComputerMACAddress") = _ComputerMACAddress : Catch ex As Exception : Return pFault.LogException(ex, "ComputerMACAddress", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("SystemDiskVolumeSerialNo") = _SystemDiskVolumeSerialNo : Catch ex As Exception : Return pFault.LogException(ex, "SystemDiskVolumeSerialNo", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("LocalTime") = _LocalTime : Catch ex As Exception : Return pFault.LogException(ex, "LocalTime", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("GmtTime") = _GmtTime : Catch ex As Exception : Return pFault.LogException(ex, "GmtTime", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("AccessingComputerDetails") = _AccessingComputerDetails : Catch ex As Exception : Return pFault.LogException(ex, "AccessingComputerDetails", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("UICulture") = _UICulture : Catch ex As Exception : Return pFault.LogException(ex, "UICulture", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("TotalPhysicalMemoryKb") = _TotalPhysicalMemoryKb : Catch ex As Exception : Return pFault.LogException(ex, "TotalPhysicalMemoryKb", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("AvailablePhysicalMemoryKb") = _AvailablePhysicalMemoryKb : Catch ex As Exception : Return pFault.LogException(ex, "AvailablePhysicalMemoryKb", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApplicationVersion") = _ApplicationVersion : Catch ex As Exception : Return pFault.LogException(ex, "ApplicationVersion", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("OriginatingIP") = _OriginatingIP : Catch ex As Exception : Return pFault.LogException(ex, "OriginatingIP", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("Language") = _Language : Catch ex As Exception : Return pFault.LogException(ex, "Language", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("HostingAssembly") = _HostingAssembly : Catch ex As Exception : Return pFault.LogException(ex, "HostingAssembly", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("OriginatingCountry") = _OriginatingCountry : Catch ex As Exception : Return pFault.LogException(ex, "OriginatingCountry", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("DateLoggedIn") = _DateLoggedIn : Catch ex As Exception : Return pFault.LogException(ex, "DateLoggedIn", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("MonthLoggedIn") = _MonthLoggedIn : Catch ex As Exception : Return pFault.LogException(ex, "MonthLoggedIn", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("ClientReportedIP") = _ClientReportedIP : Catch ex As Exception : Return pFault.LogException(ex, "ClientReportedIP", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("ClientReportedCountry") = _ClientReportedCountry : Catch ex As Exception : Return pFault.LogException(ex, "ClientReportedCountry", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("IPAdditionalDetails") = _IPAdditionalDetails : Catch ex As Exception : Return pFault.LogException(ex, "IPAdditionalDetails", "TRGT-LoggedLogin-130316-0852", vRequester) : End Try 
    Try : vDataRow("Tag") = _Tag : Catch ex As Exception : End Try 
    Try : vDataRow("DateAdded") = bDateAdded : Catch ex As Exception : Return pFault.LogException(ex, "DateAdded", "TRGT-TransactionLoad-130316-0852", vRequester) : End Try 
    bPrimaryKey = _ID
    CreateDefaultDesignation() 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    If _IsCleanForXML = False Then 
      CleanEntityForXML() 
    End If 
 
    rXML = "" 
    Try 
      Dim pType As Type = Me.GetType 
      pFunctionParameters = pType.Name 
      Dim pSerializer As Xml.Serialization.XmlSerializer 
      pSerializer = New Xml.Serialization.XmlSerializer(pType) 
      Dim MyStringBuilder As New Text.StringBuilder 
      Dim pWriter As New IO.StringWriter(MyStringBuilder) 
      pSerializer.Serialize(pWriter, Me) 
      pWriter.Close() 
      pFault.SetOK() 
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLoggedLogin As csLoggedLogin = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedLogin) 
      AssignValues(pLoggedLogin) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-LoggedLogin-130515-1230", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
    
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'UserName 
          If _UserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserName) 
          'UserFullName 
          If _UserFullName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserFullName) 
          'TimeLoggedIn 
          pBinaryWriter.Write(_TimeLoggedIn.Ticks) 
          'ApplicationName 
          If _ApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApplicationName) 
          'UserIdentityTypeCode 
          If _UserIdentityTypeCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserIdentityTypeCode) 
          pBinaryWriter.Write(_UserIdentityTypeText) 
          'UserIdentityTypeNameCode 
          pBinaryWriter.Write(_UserIdentityTypeNameCode) 
          pBinaryWriter.Write(_UserIdentityTypeNameText) 
          'Roles 
          If _Roles Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Roles) 
          'TimeLoggedOut 
          pBinaryWriter.Write(_TimeLoggedOut.Ticks) 
          'LoginFaultNumber 
          pBinaryWriter.Write(_LoginFaultNumber) 
          'EnvironmentUserName 
          If _EnvironmentUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EnvironmentUserName) 
          'EnvironmentMachineName 
          If _EnvironmentMachineName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EnvironmentMachineName) 
          'EnvironmentUserDomainName 
          If _EnvironmentUserDomainName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_EnvironmentUserDomainName) 
          'DnsGetHostName 
          If _DnsGetHostName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_DnsGetHostName) 
          'AddressList 
          If _AddressList Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AddressList) 
          'ComputerMACAddress 
          If _ComputerMACAddress Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ComputerMACAddress) 
          'SystemDiskVolumeSerialNo 
          If _SystemDiskVolumeSerialNo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SystemDiskVolumeSerialNo) 
          'LocalTime 
          pBinaryWriter.Write(_LocalTime.Ticks) 
          'GmtTime 
          pBinaryWriter.Write(_GmtTime.Ticks) 
          'AccessingComputerDetails 
          If _AccessingComputerDetails Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AccessingComputerDetails) 
          'UICulture 
          If _UICulture Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UICulture) 
          'TotalPhysicalMemoryKb 
          pBinaryWriter.Write(_TotalPhysicalMemoryKb) 
          'AvailablePhysicalMemoryKb 
          pBinaryWriter.Write(_AvailablePhysicalMemoryKb) 
          'ApplicationVersion 
          If _ApplicationVersion Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApplicationVersion) 
          'OriginatingIP 
          If _OriginatingIP Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OriginatingIP) 
          'Language 
          pBinaryWriter.Write(_Language.FastToString()) 
          'HostingAssembly 
          If _HostingAssembly Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_HostingAssembly) 
          'OriginatingCountry 
          If _OriginatingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OriginatingCountry) 
          'DateLoggedIn 
          pBinaryWriter.Write(_DateLoggedIn.Ticks) 
          'MonthLoggedIn 
          pBinaryWriter.Write(_MonthLoggedIn.Ticks) 
          'ClientReportedIP 
          If _ClientReportedIP Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ClientReportedIP) 
          'ClientReportedCountry 
          If _ClientReportedCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ClientReportedCountry) 
          'IPAdditionalDetails 
          If _IPAdditionalDetails Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_IPAdditionalDetails) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'LoggedAlerts  
          If _LoggedAlerts IsNot Nothing Then 
            pObjectBytes = _LoggedAlerts.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'LoggedRequests  
          If _LoggedRequests IsNot Nothing Then 
            pObjectBytes = _LoggedRequests.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-150307-2338", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      If rFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pLength As Integer = 0 
          Dim pHasValue As Boolean = False 
          Dim pObjectBytes As Byte() = Nothing 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'UserName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserName = pReader.ReadString 
          'UserFullName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserFullName = pReader.ReadString 
          'TimeLoggedIn 
          _TimeLoggedIn = New Date(pReader.ReadInt64) 
          'ApplicationName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApplicationName = pReader.ReadString 
          'UserIdentityTypeCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserIdentityTypeCode = pReader.ReadString 
          _UserIdentityTypeText = pReader.ReadString 
          'UserIdentityTypeNameCode 
          _UserIdentityTypeNameCode = pReader.ReadInt32 
          _UserIdentityTypeNameText = pReader.ReadString 
          'Roles 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Roles = pReader.ReadString 
          'TimeLoggedOut 
          _TimeLoggedOut = New Date(pReader.ReadInt64) 
          'LoginFaultNumber 
          _LoginFaultNumber = pReader.ReadInt32 
          'EnvironmentUserName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EnvironmentUserName = pReader.ReadString 
          'EnvironmentMachineName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EnvironmentMachineName = pReader.ReadString 
          'EnvironmentUserDomainName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _EnvironmentUserDomainName = pReader.ReadString 
          'DnsGetHostName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _DnsGetHostName = pReader.ReadString 
          'AddressList 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AddressList = pReader.ReadString 
          'ComputerMACAddress 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ComputerMACAddress = pReader.ReadString 
          'SystemDiskVolumeSerialNo 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SystemDiskVolumeSerialNo = pReader.ReadString 
          'LocalTime 
          _LocalTime = New Date(pReader.ReadInt64) 
          'GmtTime 
          _GmtTime = New Date(pReader.ReadInt64) 
          'AccessingComputerDetails 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AccessingComputerDetails = pReader.ReadString 
          'UICulture 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UICulture = pReader.ReadString 
          'TotalPhysicalMemoryKb 
          _TotalPhysicalMemoryKb = pReader.ReadInt64 
          'AvailablePhysicalMemoryKb 
          _AvailablePhysicalMemoryKb = pReader.ReadInt64 
          'ApplicationVersion 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApplicationVersion = pReader.ReadString 
          'OriginatingIP 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OriginatingIP = pReader.ReadString 
          'Language 
          _Language = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'HostingAssembly 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _HostingAssembly = pReader.ReadString 
          'OriginatingCountry 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OriginatingCountry = pReader.ReadString 
          'DateLoggedIn 
          _DateLoggedIn = New Date(pReader.ReadInt64) 
          'MonthLoggedIn 
          _MonthLoggedIn = New Date(pReader.ReadInt64) 
          'ClientReportedIP 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ClientReportedIP = pReader.ReadString 
          'ClientReportedCountry 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ClientReportedCountry = pReader.ReadString 
          'IPAdditionalDetails 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _IPAdditionalDetails = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'LoggedAlerts 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedAlerts = New csLoggedAlertCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'LoggedRequests 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedRequests = New csLoggedRequestCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-LoggedLogin-150307-2339", vRequester) 
    End Try 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  ''' <summary> 
  ''' Returns JSON for public properties 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    If _IsCleanForXML = False Then 
      CleanEntityForXML() 
    End If 
 
    rJSON = "" 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      rJSON = Newtonsoft.Json.JsonConvert.SerializeObject(Me, pSettings) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' Creates object using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pLoggedLogin As csLoggedLogin = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csLoggedLogin)(vJSON, pSettings) 
      AssignValues(pLoggedLogin) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vLoggedLogin As csLoggedLogin)
    With vLoggedLogin
      _ID = .ID 
      _UserName = .UserName 
      _UserFullName = .UserFullName 
      _TimeLoggedIn = .TimeLoggedIn 
      _ApplicationName = .ApplicationName 
      _UserIdentityTypeCode = .UserIdentityTypeCode 
      _UserIdentityTypeText = .UserIdentityTypeText 
      _UserIdentityTypeNameCode = .UserIdentityTypeNameCode 
      _UserIdentityTypeNameText = .UserIdentityTypeNameText 
      _Roles = .Roles 
      _TimeLoggedOut = .TimeLoggedOut 
      _LoginFaultNumber = .LoginFaultNumber 
      _EnvironmentUserName = .EnvironmentUserName 
      _EnvironmentMachineName = .EnvironmentMachineName 
      _EnvironmentUserDomainName = .EnvironmentUserDomainName 
      _DnsGetHostName = .DnsGetHostName 
      _AddressList = .AddressList 
      _ComputerMACAddress = .ComputerMACAddress 
      _SystemDiskVolumeSerialNo = .SystemDiskVolumeSerialNo 
      _LocalTime = .LocalTime 
      _GmtTime = .GmtTime 
      _AccessingComputerDetails = .AccessingComputerDetails 
      _UICulture = .UICulture 
      _TotalPhysicalMemoryKb = .TotalPhysicalMemoryKb 
      _AvailablePhysicalMemoryKb = .AvailablePhysicalMemoryKb 
      _ApplicationVersion = .ApplicationVersion 
      _OriginatingIP = .OriginatingIP 
      _Language = .Language 
      _LanguageText = .LanguageText
      _HostingAssembly = .HostingAssembly 
      _OriginatingCountry = .OriginatingCountry 
      _DateLoggedIn = .DateLoggedIn 
      _MonthLoggedIn = .MonthLoggedIn 
      _ClientReportedIP = .ClientReportedIP 
      _ClientReportedCountry = .ClientReportedCountry 
      _IPAdditionalDetails = .IPAdditionalDetails 
      _Tag = .Tag 
      bDateAdded = .DateAdded 
      bccStatus = .ccStatus
    End With
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub
  
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      Return pFault.SetOK() 
    End If 
 
    Dim pTextToGet As String = "" 
    Try 
      'UserIdentityType 
      pTextToGet = "UserIdentityTypeText (Lookup)" 
      _UserIdentityTypeText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.UserIdentityType, _UserIdentityTypeCode, vRequester) 
      'UserIdentityTypeName 
      pTextToGet = "UserIdentityTypeNameText (Lookup)" 
      _UserIdentityTypeNameText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UserIdentityType, _UserIdentityTypeCode.ToString(), clsEnums.enmLookup.UserIdentityTypeName, _UserIdentityTypeNameCode, vRequester) 
      'Language 
      pTextToGet = "LanguageText (Enum)" 
      _LanguageText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Language, _Language.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-LoggedLogin-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
#Region "Load Entity" 
  Friend Function LoadMeFromIDataReader(vReader As IDataReader, vRequester As clsRequester) As clsFault Implements ITargCCDataReaderUser.LoadMeFromIDataReader 
    Dim pFunctionParameters As String = ""  
    Dim pFault As New clsFault
  
    Dim pLastReadVariableName As String = "" 
    Try
      pLastReadVariableName = "ID" 
      If Not vReader.IsDBNull(0) Then _ID = vReader.GetInt64(0)
      pLastReadVariableName = "UserName" 
      If Not vReader.IsDBNull(1) Then _UserName = vReader.GetString(1) 
      pLastReadVariableName = "UserFullName" 
      If Not vReader.IsDBNull(2) Then _UserFullName = vReader.GetString(2) 
      pLastReadVariableName = "TimeLoggedIn" 
      If Not vReader.IsDBNull(3) Then _TimeLoggedIn = vReader.GetDateTime(3)
      pLastReadVariableName = "ApplicationName" 
      If Not vReader.IsDBNull(4) Then _ApplicationName = vReader.GetString(4) 
      pLastReadVariableName = "lkpUserIdentityType" 
      If Not vReader.IsDBNull(5) Then _UserIdentityTypeCode = vReader.GetString(5)
      pLastReadVariableName = "lkpUserIdentityTypeName" 
      If Not vReader.IsDBNull(6) Then _UserIdentityTypeNameCode = vReader.GetInt32(6)
      pLastReadVariableName = "Roles" 
      If Not vReader.IsDBNull(7) Then _Roles = vReader.GetString(7) 
      pLastReadVariableName = "TimeLoggedOut" 
      If Not vReader.IsDBNull(8) Then _TimeLoggedOut = vReader.GetDateTime(8)
      pLastReadVariableName = "LoginFaultNumber" 
      If Not vReader.IsDBNull(9) Then _LoginFaultNumber = vReader.GetInt32(9)
      pLastReadVariableName = "EnvironmentUserName" 
      If Not vReader.IsDBNull(10) Then _EnvironmentUserName = vReader.GetString(10) 
      pLastReadVariableName = "EnvironmentMachineName" 
      If Not vReader.IsDBNull(11) Then _EnvironmentMachineName = vReader.GetString(11) 
      pLastReadVariableName = "EnvironmentUserDomainName" 
      If Not vReader.IsDBNull(12) Then _EnvironmentUserDomainName = vReader.GetString(12) 
      pLastReadVariableName = "DnsGetHostName" 
      If Not vReader.IsDBNull(13) Then _DnsGetHostName = vReader.GetString(13) 
      pLastReadVariableName = "AddressList" 
      If Not vReader.IsDBNull(14) Then _AddressList = vReader.GetString(14) 
      pLastReadVariableName = "ComputerMACAddress" 
      If Not vReader.IsDBNull(15) Then _ComputerMACAddress = vReader.GetString(15) 
      pLastReadVariableName = "SystemDiskVolumeSerialNo" 
      If Not vReader.IsDBNull(16) Then _SystemDiskVolumeSerialNo = vReader.GetString(16) 
      pLastReadVariableName = "LocalTime" 
      If Not vReader.IsDBNull(17) Then _LocalTime = vReader.GetDateTime(17)
      pLastReadVariableName = "GmtTime" 
      If Not vReader.IsDBNull(18) Then _GmtTime = vReader.GetDateTime(18)
      pLastReadVariableName = "AccessingComputerDetails" 
      If Not vReader.IsDBNull(19) Then _AccessingComputerDetails = vReader.GetString(19) 
      pLastReadVariableName = "UICulture" 
      If Not vReader.IsDBNull(20) Then _UICulture = vReader.GetString(20) 
      pLastReadVariableName = "TotalPhysicalMemoryKb" 
      If Not vReader.IsDBNull(21) Then _TotalPhysicalMemoryKb = vReader.GetInt64(21)
      pLastReadVariableName = "AvailablePhysicalMemoryKb" 
      If Not vReader.IsDBNull(22) Then _AvailablePhysicalMemoryKb = vReader.GetInt64(22)
      pLastReadVariableName = "ApplicationVersion" 
      If Not vReader.IsDBNull(23) Then _ApplicationVersion = vReader.GetString(23) 
      pLastReadVariableName = "OriginatingIP" 
      If Not vReader.IsDBNull(24) Then _OriginatingIP = vReader.GetString(24) 
      pLastReadVariableName = "enmLanguage" 
      If Not vReader.IsDBNull(25) Then _Language = clsEnums.TranslateEnmLanguage(vReader.GetString(25))
      pLastReadVariableName = "HostingAssembly" 
      If Not vReader.IsDBNull(26) Then _HostingAssembly = vReader.GetString(26) 
      pLastReadVariableName = "OriginatingCountry" 
      If Not vReader.IsDBNull(27) Then _OriginatingCountry = vReader.GetString(27) 
      pLastReadVariableName = "clc_DateLoggedIn" 
      If Not vReader.IsDBNull(28) Then _DateLoggedIn = vReader.GetDateTime(28)
      pLastReadVariableName = "clc_MonthLoggedIn" 
      If Not vReader.IsDBNull(29) Then _MonthLoggedIn = vReader.GetDateTime(29)
      pLastReadVariableName = "ClientReportedIP" 
      If Not vReader.IsDBNull(30) Then _ClientReportedIP = vReader.GetString(30) 
      pLastReadVariableName = "ClientReportedCountry" 
      If Not vReader.IsDBNull(31) Then _ClientReportedCountry = vReader.GetString(31) 
      pLastReadVariableName = "IPAdditionalDetails" 
      If Not vReader.IsDBNull(32) Then _IPAdditionalDetails = vReader.GetString(32) 
      bDateAdded = _TimeLoggedIn 
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedLogin As csLoggedLogin, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedLoggedLogin) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _UserName = ""
    _UserFullName = ""
    _TimeLoggedIn = Nothing
    _ApplicationName = ""
    _UserIdentityTypeCode = ""
    _UserIdentityTypeText = ""
    _UserIdentityTypeNameCode = -1
    _UserIdentityTypeNameText = ""
    _Roles = ""
    _TimeLoggedOut = Nothing
    _LoginFaultNumber = 0
    _EnvironmentUserName = ""
    _EnvironmentMachineName = ""
    _EnvironmentUserDomainName = ""
    _DnsGetHostName = ""
    _AddressList = ""
    _ComputerMACAddress = ""
    _SystemDiskVolumeSerialNo = ""
    _LocalTime = Nothing
    _GmtTime = Nothing
    _AccessingComputerDetails = ""
    _UICulture = ""
    _TotalPhysicalMemoryKb = 0
    _AvailablePhysicalMemoryKb = 0
    _ApplicationVersion = ""
    _OriginatingIP = ""
    _Language = clsEnums.enmLanguage.UD
    _LanguageText = ""
    _HostingAssembly = ""
    _OriginatingCountry = ""
    _DateLoggedIn = Nothing
    _MonthLoggedIn = Nothing
    _ClientReportedIP = ""
    _ClientReportedCountry = ""
    _IPAdditionalDetails = ""
    _Tag = ""
    _LoggedAlerts = Nothing
    _LoggedRequests = Nothing
    _IsCleanForXML = False 
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csLoggedLoginCol
  Inherits cTargCCCollection(Of csLoggedLogin)
  Implements ITargCCCollectionUpdateable 
  Implements ITargCCDataReaderUser 
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property CanHave0AsPrimaryKey As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' Raised before FillByXXX. Used to override the SP. Check rCommand to see what the SP was supposed to be 
  ''' </summary> 
  ''' <param name="rCommandText"></param> 
  ''' <param name="rDALParameters"></param> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeFillWithRequester(ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  ''' <summary> 
  ''' Use the tag of the collection to define what you want to do 
  ''' </summary> 
  ''' <param name="rCancel"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rFault"></param> 
  Friend Event evtBeforeUpdateWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLoggedLogin) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
  Private _IsCleanForXML As Boolean 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
 
  Private _Tag As String = "" 
  Public Property [Tag]() As String 
    Get 
      Return Me._Tag 
    End Get 
    Set(ByVal value As String) 
      Me._Tag = value 
    End Set 
  End Property 
 
  'ToString 
  Public Overrides Function ToString() As String 
    Dim pString As New Text.StringBuilder 
 
    pString.AppendLine("Instance of " & Me.GetType().Name & ". Number of rows" & Me.Count.ToString()) 
    If _Tag <> "" Then pString.AppendLine("  Tag='" & _Tag & "'") 
 
    For Each pRow As csLoggedLogin In Me 
      pString.AppendLine(pRow.ToString & Environment.NewLine) 
    Next 
 
    Return pString.ToString() 
  End Function 
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New Text.StringBuilder 
    Dim pCSVTitle As New Text.StringBuilder 
    'Get title 
    Dim pDbCode As String = "" 
    If vWithTexts Then pDbCode = " (Db Code)" 
    pCSVTitle.Append("""ID""") 
    pCSVTitle.Append(",""UserName""") 
    pCSVTitle.Append(",""UserFullName""") 
    pCSVTitle.Append(",""TimeLoggedIn""") 
    pCSVTitle.Append(",""ApplicationName""") 
    pCSVTitle.Append(",""UserIdentityTypeCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""UserIdentityType (Text)""") 
    pCSVTitle.Append(",""UserIdentityTypeNameCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""UserIdentityTypeName (Text)""") 
    pCSVTitle.Append(",""Roles""") 
    pCSVTitle.Append(",""TimeLoggedOut""") 
    pCSVTitle.Append(",""LoginFaultNumber""") 
    pCSVTitle.Append(",""EnvironmentUserName""") 
    pCSVTitle.Append(",""EnvironmentMachineName""") 
    pCSVTitle.Append(",""EnvironmentUserDomainName""") 
    pCSVTitle.Append(",""DnsGetHostName""") 
    pCSVTitle.Append(",""AddressList""") 
    pCSVTitle.Append(",""ComputerMACAddress""") 
    pCSVTitle.Append(",""SystemDiskVolumeSerialNo""") 
    pCSVTitle.Append(",""LocalTime""") 
    pCSVTitle.Append(",""GmtTime""") 
    pCSVTitle.Append(",""AccessingComputerDetails""") 
    pCSVTitle.Append(",""UICulture""") 
    pCSVTitle.Append(",""TotalPhysicalMemoryKb""") 
    pCSVTitle.Append(",""AvailablePhysicalMemoryKb""") 
    pCSVTitle.Append(",""ApplicationVersion""") 
    pCSVTitle.Append(",""OriginatingIP""") 
    pCSVTitle.Append(",""Language" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Language (Text)""") 
    pCSVTitle.Append(",""HostingAssembly""") 
    pCSVTitle.Append(",""OriginatingCountry""") 
    pCSVTitle.Append(",""DateLoggedIn""") 
    pCSVTitle.Append(",""MonthLoggedIn""") 
    pCSVTitle.Append(",""ClientReportedIP""") 
    pCSVTitle.Append(",""ClientReportedCountry""") 
    pCSVTitle.Append(",""IPAdditionalDetails""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csLoggedLogin In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    
    rFault = Fill(vRequester, vHowMany, vDir) 
  End Sub
  
  Public Sub New(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    LoadByteArray(vBytes, rFault, vRequester) 
  End Sub 
 
  Public Sub New(ByVal vBytesFromAPI As Object, ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
    MyBase.New() 
    CreateEmpty() 
    Dim pBytes As Byte() = DirectCast(vBytesFromAPI, Byte()) 
    LoadByteArray(pBytes, rFault, vRequester) 
  End Sub 
 
  Public Overloads Sub Add(ByVal vLoggedLogin As csLoggedLogin) 
    SyncLock _CollectionLock 
      MyBase.Add(vLoggedLogin) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vLoggedLogin As csLoggedLogin) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vLoggedLogin) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vLoggedLoginCol As csLoggedLoginCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vLoggedLoginCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vLoggedLogin As csLoggedLogin) 
    SyncLock _CollectionLock 
      MyBase.Remove(vLoggedLogin) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
 
  Private Sub LoadIDs() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByID Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByID Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByID = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByID' yet!
      Dim pTempDictionary As New Dictionary(Of Long, csLoggedLogin) 
      
      For Each lLoggedLogin In Me 
        If lLoggedLogin.IsEmpty OrElse pTempDictionary.ContainsKey(lLoggedLogin.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lLoggedLogin.ID, lLoggedLogin) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLoggedLogin.ToString, "TRGT-LoggedLogin-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", LoggedLogin:" & lLoggedLogin.ToString() & ", TRGT-LoggedLogin-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lLoggedLogin As csLoggedLogin In Me 
      lLoggedLogin.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lLoggedLogin As csLoggedLogin In Me 
      lLoggedLogin.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ApplicationName] 
    [DateLoggedIn] 
    [LoginFaultNumber] 
    [MonthLoggedIn] 
    [OriginatingCountry] 
    [TimeLoggedIn] 
    [UserName] 
    [UserNameAndApplicationName] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the LoggedLogins by the chosen parameters. This function may be a bit slower than accessing the LoggedLogin's FillBy... directly 
  ''' </summary> 
  ''' <param name="vWhichParameterCombination"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vHowMany"></param> 
  ''' <param name="vDir"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillByParameters(ByVal vWhichParameterCombination As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault 
    Dim pFunctionParameters As String = String.Format("WhichParameterCombination={0}", vWhichParameterCombination.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.ApplicationName 
          pFault = FillByApplicationName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.DateLoggedIn 
          pFault = FillByDateLoggedIn(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.LoginFaultNumber 
          pFault = FillByLoginFaultNumber(ccHelper.ToInteger(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.MonthLoggedIn 
          pFault = FillByMonthLoggedIn(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OriginatingCountry 
          pFault = FillByOriginatingCountry(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TimeLoggedIn 
          pFault = FillByTimeLoggedIn(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.UserName 
          pFault = FillByUserName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.UserNameAndApplicationName 
          pFault = FillByUserNameAndApplicationName(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedLogin-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedLogin-151223_1716", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection of all the items, or a sub-collection defined by HowMany and Direction
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.Clone() 
      pLoggedLoginsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedLoginsCached.Reverse() 
      If vHowMany > 0 AndAlso pLoggedLoginsCached.Count > vHowMany Then 
        Dim tmp As New csLoggedLoginCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedLoginsCached(i)) 
        Next 
        pLoggedLoginsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFill"
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Top" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString()
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByApplicationName(ByVal vApplicationName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}", vApplicationName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByApplicationName(vApplicationName)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DateLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByDateLoggedIn(ByVal vDateLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DateLoggedIn={0}", vDateLoggedIn)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByDateLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByDateLoggedIn(vDateLoggedIn)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByDateLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_DateLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(vDateLoggedIn) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoginFaultNumber, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLoginFaultNumber(ByVal vLoginFaultNumber As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginFaultNumber={0}", vLoginFaultNumber)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByLoginFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByLoginFaultNumber(vLoginFaultNumber)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByLoginFaultNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LoginFaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumber) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MonthLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMonthLoggedIn(ByVal vMonthLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthLoggedIn={0}", vMonthLoggedIn)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByMonthLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByMonthLoggedIn(vMonthLoggedIn)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByMonthLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_MonthLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(vMonthLoggedIn) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginatingCountry, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}", vOriginatingCountry)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByOriginatingCountry(vOriginatingCountry)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByOriginatingCountry" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountry) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTimeLoggedIn(ByVal vTimeLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeLoggedIn={0}", vTimeLoggedIn)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByTimeLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByTimeLoggedIn(vTimeLoggedIn)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByTimeLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vTimeLoggedIn) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserName(ByVal vUserName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}", vUserName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByUserName(vUserName)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByUserName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserNameAndApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserNameAndApplicationName(ByVal vUserName As String, ByVal vApplicationName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, ApplicationName={1}", vUserName, vApplicationName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByUserNameAndApplicationName(vUserName, vApplicationName)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByUserName&ApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserName) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedApplicationName(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationNameFrom={0}, ApplicationNameTo={1}", vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedApplicationName(vApplicationNameFrom, vApplicationNameTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DateLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedDateLoggedIn(ByVal vDateLoggedInStart As Date, ByVal vDateLoggedInEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DateLoggedInStart={0}, DateLoggedInEnd={1}", vDateLoggedInStart, vDateLoggedInEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedDateLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedDateLoggedIn(vDateLoggedInStart, vDateLoggedInEnd)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedDateLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_DateLoggedInFrom" 
        pDALParameters.Add("bndclc_DateLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = (vDateLoggedInStart) 
        pLastReadVariableName = "clc_DateLoggedInTo" 
        pDALParameters.Add("bndclc_DateLoggedInTo", ccDAL.enmSQLDataType.Date).Value = (vDateLoggedInEnd) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoginFaultNumber, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedLoginFaultNumber(ByVal vLoginFaultNumberFrom As Integer, ByVal vLoginFaultNumberTo As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginFaultNumberFrom={0}, LoginFaultNumberTo={1}", vLoginFaultNumberFrom, vLoginFaultNumberTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedLoginFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedLoginFaultNumber(vLoginFaultNumberFrom, vLoginFaultNumberTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedLoginFaultNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LoginFaultNumberFrom" 
        pDALParameters.Add("bndLoginFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumberFrom) 
        pLastReadVariableName = "LoginFaultNumberTo" 
        pDALParameters.Add("bndLoginFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumberTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MonthLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMonthLoggedIn(ByVal vMonthLoggedInStart As Date, ByVal vMonthLoggedInEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthLoggedInStart={0}, MonthLoggedInEnd={1}", vMonthLoggedInStart, vMonthLoggedInEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedMonthLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedMonthLoggedIn(vMonthLoggedInStart, vMonthLoggedInEnd)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedMonthLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_MonthLoggedInFrom" 
        pDALParameters.Add("bndclc_MonthLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = (vMonthLoggedInStart) 
        pLastReadVariableName = "clc_MonthLoggedInTo" 
        pDALParameters.Add("bndclc_MonthLoggedInTo", ccDAL.enmSQLDataType.Date).Value = (vMonthLoggedInEnd) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginatingCountry, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOriginatingCountry(ByVal vOriginatingCountryFrom As String, ByVal vOriginatingCountryTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountryFrom={0}, OriginatingCountryTo={1}", vOriginatingCountryFrom, vOriginatingCountryTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedOriginatingCountry(vOriginatingCountryFrom, vOriginatingCountryTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedOriginatingCountry" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OriginatingCountryFrom" 
        pDALParameters.Add("bndOriginatingCountryFrom", ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountryFrom) 
        pLastReadVariableName = "OriginatingCountryTo" 
        pDALParameters.Add("bndOriginatingCountryTo", ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountryTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeLoggedIn, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTimeLoggedIn(ByVal vTimeLoggedInStart As Date, ByVal vTimeLoggedInEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeLoggedInStart={0}, TimeLoggedInEnd={1}", vTimeLoggedInStart, vTimeLoggedInEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedTimeLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedTimeLoggedIn(vTimeLoggedInStart, vTimeLoggedInEnd)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedTimeLoggedIn" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeLoggedInFrom" 
        pDALParameters.Add("bndTimeLoggedInFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeLoggedInStart) 
        pLastReadVariableName = "TimeLoggedInTo" 
        pDALParameters.Add("bndTimeLoggedInTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeLoggedInEnd) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}", vUserNameFrom, vUserNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedUserName(vUserNameFrom, vUserNameTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedUserName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "UserNameFrom" 
        pDALParameters.Add("bndUserNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameFrom) 
        pLastReadVariableName = "UserNameTo" 
        pDALParameters.Add("bndUserNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserNameAndApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserNameAndApplicationName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}, ApplicationNameFrom={2}, ApplicationNameTo={3}", vUserNameFrom, vUserNameTo, vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByBoundedUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedLoginCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedLoginCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedLoginCol failed: " & pResponse) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.CloneByBoundedUserNameAndApplicationName(vUserNameFrom, vUserNameTo, vApplicationNameFrom, vApplicationNameTo)
      pFault = LoadMeFromDBCache(pLoggedLoginsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByBoundedUserName&ApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "UserNameFrom" 
        pDALParameters.Add("bndUserNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameFrom) 
        pLastReadVariableName = "UserNameTo" 
        pDALParameters.Add("bndUserNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameTo) 
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardApplicationName(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationNameWildcardType={1}", vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByWildCardApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCApplicationName = vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCApplicationName = "%" & vApplicationName 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCApplicationName = "%" & vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vApplicationName.ToCharArray 
        pWCApplicationName &= p & "%" 
      Next 
      pWCApplicationName = "%" & pWCApplicationName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByWildCardApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCApplicationName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded OriginatingCountry, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}, OriginatingCountryWildcardType={1}", vOriginatingCountry, vOriginatingCountryWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByWildCardOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'OriginatingCountry 
    Dim pWCOriginatingCountry As String = "" 
    If vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
      pWCOriginatingCountry = vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vOriginatingCountry.ToCharArray 
        pWCOriginatingCountry &= p & "%" 
      Next 
      pWCOriginatingCountry = "%" & pWCOriginatingCountry 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByWildCardOriginatingCountry" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldOriginatingCountry" 
        pDALParameters.Add("wldOriginatingCountry", ccDAL.enmSQLDataType.VarChar, 10).Value = (pWCOriginatingCountry) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded UserName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}", vUserName, vUserNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByWildCardUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCUserName = vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCUserName = "%" & vUserName 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCUserName = "%" & vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vUserName.ToCharArray 
        pWCUserName &= p & "%" 
      Next 
      pWCUserName = "%" & pWCUserName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByWildCardUserName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldUserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCUserName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded UserNameAndApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardUserNameAndApplicationName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}, ApplicationName={2}, ApplicationNameWildcardType={3}", vUserName, vUserNameWildcardType.FastToString(), vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByWildCardUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCUserName = vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCUserName = "%" & vUserName 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCUserName = "%" & vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vUserName.ToCharArray 
        pWCUserName &= p & "%" 
      Next 
      pWCUserName = "%" & pWCUserName 
    End If 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCApplicationName = vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCApplicationName = "%" & vApplicationName 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCApplicationName = "%" & vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vApplicationName.ToCharArray 
        pWCApplicationName &= p & "%" 
      Next 
      pWCApplicationName = "%" & pWCApplicationName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillByWildCardUserName&ApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldUserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCUserName) 
        pLastReadVariableName = "wldApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCApplicationName) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary> 
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false). An ID can only exist once in the collection 
  ''' </summary> 
  ''' <param name="vIDs"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vDir"></param> 
  ''' <param name="vAppend"></param> 
  ''' <returns></returns> 
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lLoggedLogin As New csLoggedLogin() 
      pFault = lLoggedLogin.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lLoggedLogin.IsEmpty Then Me.Add(lLoggedLogin) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pLoggedLogins As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedLogins, "csLoggedLoginCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedLogins IsNot Nothing AndAlso Me.Count <> pLoggedLogins.Count Then FillFromListOfITargCCEntity(pLoggedLogins) 
    End If 
 
    Me.SortByID() 
    If vDir = clsEnums.enmFillDirection.DESC Then Me.Reverse() 
 
    RaiseEvent evtAfterFill() 
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault 
  End Function 
 
  Public Enum enmFillOnTheFlyParameters 
    UD 
    IDFrom
    IDTo
    [UserName]
    UserNameWildcardType
    TimeLoggedInStart
    TimeLoggedInEnd
    [ApplicationName]
    ApplicationNameWildcardType
    LoginFaultNumberFrom
    LoginFaultNumberTo
    [OriginatingCountry]
    OriginatingCountryWildcardType
    DateLoggedInStart
    DateLoggedInEnd
    MonthLoggedInStart
    MonthLoggedInEnd
  End Enum 
  Public Enum enmListDefinition 
    UD 
    HowMany 
    Dir 
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillOnTheFly(ByVal vParameters As Dictionary(Of System.Enum, Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pUserName As String = Nothing
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pTimeLoggedInStart As Nullable(Of Date) = Nothing
    Dim pTimeLoggedInEnd As Nullable(Of Date) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLoginFaultNumberFrom As Nullable(Of Integer) = Nothing
    Dim pLoginFaultNumberTo As Nullable(Of Integer) = Nothing
    Dim pOriginatingCountry As String = Nothing
    Dim pOriginatingCountryWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pDateLoggedInStart As Nullable(Of Date) = Nothing
    Dim pDateLoggedInEnd As Nullable(Of Date) = Nothing
    Dim pMonthLoggedInStart As Nullable(Of Date) = Nothing
    Dim pMonthLoggedInEnd As Nullable(Of Date) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserName) Then pObj = vParameters(enmFillOnTheFlyParameters.UserName) : If pObj IsNot Nothing Then pUserName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.UserNameWildcardType) : If pObj IsNot Nothing Then pUserNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeLoggedInStart) : If pObj IsNot Nothing Then pTimeLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeLoggedInEnd) : If pObj IsNot Nothing Then pTimeLoggedInEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginFaultNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginFaultNumberFrom) : If pObj IsNot Nothing Then pLoginFaultNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginFaultNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginFaultNumberTo) : If pObj IsNot Nothing Then pLoginFaultNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginatingCountry) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginatingCountry) : If pObj IsNot Nothing Then pOriginatingCountry = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginatingCountryWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginatingCountryWildcardType) : If pObj IsNot Nothing Then pOriginatingCountryWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.DateLoggedInStart) : If pObj IsNot Nothing Then pDateLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.DateLoggedInEnd) : If pObj IsNot Nothing Then pDateLoggedInEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthLoggedInStart) : If pObj IsNot Nothing Then pMonthLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthLoggedInEnd) : If pObj IsNot Nothing Then pMonthLoggedInEnd = CDate(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pUserName, pUserNameWildcardType _
        , pTimeLoggedInStart, pTimeLoggedInEnd _
        , pApplicationName, pApplicationNameWildcardType _
        , pLoginFaultNumberFrom, pLoginFaultNumberTo _
        , pOriginatingCountry, pOriginatingCountryWildcardType _
        , pDateLoggedInStart, pDateLoggedInEnd _
        , pMonthLoggedInStart, pMonthLoggedInEnd _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vTimeLoggedInStart As Nullable(Of Date), ByVal vTimeLoggedInEnd As Nullable(Of Date) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLoginFaultNumberFrom As Nullable(Of Integer), ByVal vLoginFaultNumberTo As Nullable(Of Integer) _
        , ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType _
        , ByVal vDateLoggedInStart As Nullable(Of Date), ByVal vDateLoggedInEnd As Nullable(Of Date) _
        , ByVal vMonthLoggedInStart As Nullable(Of Date), ByVal vMonthLoggedInEnd As Nullable(Of Date) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserName={2}, UserNameWildcardType={3}, TimeLoggedInStart={4}, TimeLoggedInEnd={5}, ApplicationName={6}, ApplicationNameWildcardType={7}, LoginFaultNumberFrom={8}, LoginFaultNumberTo={9}, OriginatingCountry={10}, OriginatingCountryWildcardType={11}, DateLoggedInStart={12}, DateLoggedInEnd={13}, MonthLoggedInStart={14}, MonthLoggedInEnd={15}", vIDFrom, vIDTo, vUserName, vUserNameWildcardType.FastToString(), vTimeLoggedInStart, vTimeLoggedInEnd, vApplicationName, vApplicationNameWildcardType.FastToString(), vLoginFaultNumberFrom, vLoginFaultNumberTo, vOriginatingCountry, vOriginatingCountryWildcardType.FastToString(), vDateLoggedInStart, vDateLoggedInEnd, vMonthLoggedInStart, vMonthLoggedInEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserName = Nothing Then 
      pWCUserName = vUserName
    Else 
      If vUserNameWildcardType = clsEnums.enmWildCardType.None OrElse vUserNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCUserName = vUserName
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCUserName = vUserName & "%" 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCUserName = "%" & vUserName 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCUserName = "%" & vUserName & "%" 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vUserName.ToCharArray 
          pWCUserName &= p & "%" 
        Next 
        pWCUserName = "%" & pWCUserName 
      End If 
    End If 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationName = Nothing Then 
      pWCApplicationName = vApplicationName
    Else 
      If vApplicationNameWildcardType = clsEnums.enmWildCardType.None OrElse vApplicationNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCApplicationName = vApplicationName
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCApplicationName = vApplicationName & "%" 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCApplicationName = "%" & vApplicationName 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCApplicationName = "%" & vApplicationName & "%" 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vApplicationName.ToCharArray 
          pWCApplicationName &= p & "%" 
        Next 
        pWCApplicationName = "%" & pWCApplicationName 
      End If 
    End If 
    'OriginatingCountry 
    Dim pWCOriginatingCountry As String = "" 
    If vOriginatingCountry = Nothing Then 
      pWCOriginatingCountry = vOriginatingCountry
    Else 
      If vOriginatingCountryWildcardType = clsEnums.enmWildCardType.None OrElse vOriginatingCountryWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCOriginatingCountry = vOriginatingCountry
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
        pWCOriginatingCountry = vOriginatingCountry & "%" 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCOriginatingCountry = "%" & vOriginatingCountry 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCOriginatingCountry = "%" & vOriginatingCountry & "%" 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vOriginatingCountry.ToCharArray 
          pWCOriginatingCountry &= p & "%" 
        Next 
        pWCOriginatingCountry = "%" & pWCOriginatingCountry 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
      Dim pLoggedLoginsCached As csLoggedLoginCol = MyController.DBCache.ccLoggedLoginCol.Clone() 
      Dim pLoggedLoginsToUse As New csLoggedLoginCol() 
      For Each l In pLoggedLoginsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vUserName) Then 
          If vUserNameWildcardType = clsEnums.enmWildCardType.UD OrElse vUserNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.UserName.Equals(vUserName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.UserName.StartsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.UserName.EndsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.UserName.IndexOf(vUserName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vTimeLoggedInStart.HasValue Then 
          If vTimeLoggedInEnd.HasValue Then 
            If l.TimeLoggedIn < vTimeLoggedInStart OrElse l.TimeLoggedIn > vTimeLoggedInEnd.Value Then Continue For 
          Else 
            If l.TimeLoggedIn <> vTimeLoggedInStart.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vApplicationName) Then 
          If vApplicationNameWildcardType = clsEnums.enmWildCardType.UD OrElse vApplicationNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.ApplicationName.Equals(vApplicationName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.ApplicationName.StartsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.ApplicationName.EndsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.ApplicationName.IndexOf(vApplicationName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vLoginFaultNumberFrom.HasValue Then 
          If vLoginFaultNumberTo.HasValue Then 
            If l.LoginFaultNumber < vLoginFaultNumberFrom OrElse l.LoginFaultNumber > vLoginFaultNumberTo.Value Then Continue For 
          Else 
            If l.LoginFaultNumber <> vLoginFaultNumberFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vOriginatingCountry) Then 
          If vOriginatingCountryWildcardType = clsEnums.enmWildCardType.UD OrElse vOriginatingCountryWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.OriginatingCountry.Equals(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.OriginatingCountry.StartsWith(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.OriginatingCountry.EndsWith(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.OriginatingCountry.IndexOf(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vDateLoggedInStart.HasValue Then 
          If vDateLoggedInEnd.HasValue Then 
            If l.DateLoggedIn < vDateLoggedInStart OrElse l.DateLoggedIn > vDateLoggedInEnd.Value Then Continue For 
          Else 
            If l.DateLoggedIn <> vDateLoggedInStart.Value Then Continue For 
          End If 
        End If 
        If vMonthLoggedInStart.HasValue Then 
          If vMonthLoggedInEnd.HasValue Then 
            If l.MonthLoggedIn < vMonthLoggedInStart OrElse l.MonthLoggedIn > vMonthLoggedInEnd.Value Then Continue For 
          Else 
            If l.MonthLoggedIn <> vMonthLoggedInStart.Value Then Continue For 
          End If 
        End If 
        pLoggedLoginsToUse.Add(l) 
      Next 
      pLoggedLoginsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedLoginsToUse.Reverse() 
      If vHowMany > 0 AndAlso pLoggedLoginsToUse.Count > vHowMany Then 
        Dim tmp As New csLoggedLoginCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedLoginsToUse(i)) 
        Next 
        pLoggedLoginsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedLoginsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCUserName) 
        pLastReadVariableName = "TimeLoggedInFrom" 
        pDALParameters.Add("bndTimeLoggedInFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeLoggedInStart) 
        pLastReadVariableName = "TimeLoggedInTo" 
        pDALParameters.Add("bndTimeLoggedInTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeLoggedInEnd) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCApplicationName) 
        pLastReadVariableName = "LoginFaultNumberFrom" 
        pDALParameters.Add("bndLoginFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vLoginFaultNumberFrom) 
        pLastReadVariableName = "LoginFaultNumberTo" 
        pDALParameters.Add("bndLoginFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vLoginFaultNumberTo) 
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add("wldOriginatingCountry", ccDAL.enmSQLDataType.VarChar, 10).Value = ccHelper.ObjectNullable(pWCOriginatingCountry) 
        pLastReadVariableName = "clc_DateLoggedInFrom" 
        pDALParameters.Add("bndclc_DateLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateLoggedInStart) 
        pLastReadVariableName = "clc_DateLoggedInTo" 
        pDALParameters.Add("bndclc_DateLoggedInTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateLoggedInEnd) 
        pLastReadVariableName = "clc_MonthLoggedInFrom" 
        pDALParameters.Add("bndclc_MonthLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthLoggedInStart) 
        pLastReadVariableName = "clc_MonthLoggedInTo" 
        pDALParameters.Add("bndclc_MonthLoggedInTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthLoggedInEnd) 
        pLastReadVariableName = "HowMany" 
        pDALParameters.Add("Top", ccDAL.enmSQLDataType.Int).Value = vHowMany 
        pLastReadVariableName = "Dir" 
        pDALParameters.Add("Dir", ccDAL.enmSQLDataType.VarChar, 4).Value = vDir.FastToString() 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByUserName
    GroupByTimeLoggedIn
    GroupByApplicationName
    GroupByLoginFaultNumber
    GroupByOriginatingCountry
    GroupByDateLoggedIn
    GroupByMonthLoggedIn
  End Enum 
 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. Only send the fields you need. Default for GrouBy is False 
  ''' </summary> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function FillSumOnTheFly(ByVal vParameters As Dictionary(Of [Enum], Object), ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pUserName As String = Nothing
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pTimeLoggedInStart As Nullable(Of Date) = Nothing
    Dim pTimeLoggedInEnd As Nullable(Of Date) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLoginFaultNumberFrom As Nullable(Of Integer) = Nothing
    Dim pLoginFaultNumberTo As Nullable(Of Integer) = Nothing
    Dim pOriginatingCountry As String = Nothing
    Dim pOriginatingCountryWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pDateLoggedInStart As Nullable(Of Date) = Nothing
    Dim pDateLoggedInEnd As Nullable(Of Date) = Nothing
    Dim pMonthLoggedInStart As Nullable(Of Date) = Nothing
    Dim pMonthLoggedInEnd As Nullable(Of Date) = Nothing
    Dim pGroupByUserName As Boolean = False
    Dim pGroupByTimeLoggedIn As Boolean = False
    Dim pGroupByApplicationName As Boolean = False
    Dim pGroupByLoginFaultNumber As Boolean = False
    Dim pGroupByOriginatingCountry As Boolean = False
    Dim pGroupByDateLoggedIn As Boolean = False
    Dim pGroupByMonthLoggedIn As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserName) Then pObj = vParameters(enmFillOnTheFlyParameters.UserName) : If pObj IsNot Nothing Then pUserName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.UserNameWildcardType) : If pObj IsNot Nothing Then pUserNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeLoggedInStart) : If pObj IsNot Nothing Then pTimeLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeLoggedInEnd) : If pObj IsNot Nothing Then pTimeLoggedInEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginFaultNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginFaultNumberFrom) : If pObj IsNot Nothing Then pLoginFaultNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginFaultNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginFaultNumberTo) : If pObj IsNot Nothing Then pLoginFaultNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginatingCountry) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginatingCountry) : If pObj IsNot Nothing Then pOriginatingCountry = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginatingCountryWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginatingCountryWildcardType) : If pObj IsNot Nothing Then pOriginatingCountryWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.DateLoggedInStart) : If pObj IsNot Nothing Then pDateLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.DateLoggedInEnd) : If pObj IsNot Nothing Then pDateLoggedInEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthLoggedInStart) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthLoggedInStart) : If pObj IsNot Nothing Then pMonthLoggedInStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthLoggedInEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthLoggedInEnd) : If pObj IsNot Nothing Then pMonthLoggedInEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByUserName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByUserName) : If pObj IsNot Nothing Then pGroupByUserName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByTimeLoggedIn) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByTimeLoggedIn) : If pObj IsNot Nothing Then pGroupByTimeLoggedIn = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByApplicationName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByApplicationName) : If pObj IsNot Nothing Then pGroupByApplicationName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLoginFaultNumber) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLoginFaultNumber) : If pObj IsNot Nothing Then pGroupByLoginFaultNumber = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOriginatingCountry) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOriginatingCountry) : If pObj IsNot Nothing Then pGroupByOriginatingCountry = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByDateLoggedIn) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByDateLoggedIn) : If pObj IsNot Nothing Then pGroupByDateLoggedIn = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByMonthLoggedIn) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByMonthLoggedIn) : If pObj IsNot Nothing Then pGroupByMonthLoggedIn = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pUserName, pUserNameWildcardType _
        , pTimeLoggedInStart, pTimeLoggedInEnd _
        , pApplicationName, pApplicationNameWildcardType _
        , pLoginFaultNumberFrom, pLoginFaultNumberTo _
        , pOriginatingCountry, pOriginatingCountryWildcardType _
        , pDateLoggedInStart, pDateLoggedInEnd _
        , pMonthLoggedInStart, pMonthLoggedInEnd _
        , pGroupByUserName _
        , pGroupByTimeLoggedIn _
        , pGroupByApplicationName _
        , pGroupByLoginFaultNumber _
        , pGroupByOriginatingCountry _
        , pGroupByDateLoggedIn _
        , pGroupByMonthLoggedIn _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vTimeLoggedInStart As Nullable(Of Date), ByVal vTimeLoggedInEnd As Nullable(Of Date) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLoginFaultNumberFrom As Nullable(Of Integer), ByVal vLoginFaultNumberTo As Nullable(Of Integer) _
        , ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType _
        , ByVal vDateLoggedInStart As Nullable(Of Date), ByVal vDateLoggedInEnd As Nullable(Of Date) _
        , ByVal vMonthLoggedInStart As Nullable(Of Date), ByVal vMonthLoggedInEnd As Nullable(Of Date) _
        , ByVal vGroupByUserName As Boolean _
        , ByVal vGroupByTimeLoggedIn As Boolean _
        , ByVal vGroupByApplicationName As Boolean _
        , ByVal vGroupByLoginFaultNumber As Boolean _
        , ByVal vGroupByOriginatingCountry As Boolean _
        , ByVal vGroupByDateLoggedIn As Boolean _
        , ByVal vGroupByMonthLoggedIn As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserName={2}, UserNameWildcardType={3}, TimeLoggedInStart={4}, TimeLoggedInEnd={5}, ApplicationName={6}, ApplicationNameWildcardType={7}, LoginFaultNumberFrom={8}, LoginFaultNumberTo={9}, OriginatingCountry={10}, OriginatingCountryWildcardType={11}, DateLoggedInStart={12}, DateLoggedInEnd={13}, MonthLoggedInStart={14}, MonthLoggedInEnd={15}, GroupByUserName={16}, GroupByTimeLoggedIn={17}, GroupByApplicationName={18}, GroupByLoginFaultNumber={19}, GroupByOriginatingCountry={20}, GroupByDateLoggedIn={21}, GroupByMonthLoggedIn={22}", vIDFrom, vIDTo, vUserName, vUserNameWildcardType.FastToString(), vTimeLoggedInStart, vTimeLoggedInEnd, vApplicationName, vApplicationNameWildcardType.FastToString(), vLoginFaultNumberFrom, vLoginFaultNumberTo, vOriginatingCountry, vOriginatingCountryWildcardType.FastToString(), vDateLoggedInStart, vDateLoggedInEnd, vMonthLoggedInStart, vMonthLoggedInEnd, vGroupByUserName, vGroupByTimeLoggedIn, vGroupByApplicationName, vGroupByLoginFaultNumber, vGroupByOriginatingCountry, vGroupByDateLoggedIn, vGroupByMonthLoggedIn)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserName = Nothing Then 
      pWCUserName = vUserName
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.None OrElse vUserNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCUserName = vUserName
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCUserName = vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCUserName = "%" & vUserName 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCUserName = "%" & vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vUserName.ToCharArray 
        pWCUserName &= p & "%" 
      Next 
      pWCUserName = "%" & pWCUserName 
    End If 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationName = Nothing Then 
      pWCApplicationName = vApplicationName
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.None OrElse vApplicationNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCApplicationName = vApplicationName
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCApplicationName = vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCApplicationName = "%" & vApplicationName 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCApplicationName = "%" & vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vApplicationName.ToCharArray 
        pWCApplicationName &= p & "%" 
      Next 
      pWCApplicationName = "%" & pWCApplicationName 
    End If 
    'OriginatingCountry 
    Dim pWCOriginatingCountry As String = "" 
    If vOriginatingCountry = Nothing Then 
      pWCOriginatingCountry = vOriginatingCountry
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.None OrElse vOriginatingCountryWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCOriginatingCountry = vOriginatingCountry
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
      pWCOriginatingCountry = vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vOriginatingCountry.ToCharArray 
        pWCOriginatingCountry &= p & "%" 
      Next 
      pWCOriginatingCountry = "%" & pWCOriginatingCountry 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedLogin-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedLoginsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCUserName) 
        pLastReadVariableName = "TimeLoggedInFrom" 
        pDALParameters.Add("bndTimeLoggedInFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeLoggedInStart) 
        pLastReadVariableName = "TimeLoggedInTo" 
        pDALParameters.Add("bndTimeLoggedInTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeLoggedInEnd) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCApplicationName) 
        pLastReadVariableName = "LoginFaultNumberFrom" 
        pDALParameters.Add("bndLoginFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vLoginFaultNumberFrom) 
        pLastReadVariableName = "LoginFaultNumberTo" 
        pDALParameters.Add("bndLoginFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vLoginFaultNumberTo) 
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add("wldOriginatingCountry", ccDAL.enmSQLDataType.VarChar, 10).Value = ccHelper.ObjectNullable(pWCOriginatingCountry) 
        pLastReadVariableName = "clc_DateLoggedInFrom" 
        pDALParameters.Add("bndclc_DateLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateLoggedInStart) 
        pLastReadVariableName = "clc_DateLoggedInTo" 
        pDALParameters.Add("bndclc_DateLoggedInTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateLoggedInEnd) 
        pLastReadVariableName = "clc_MonthLoggedInFrom" 
        pDALParameters.Add("bndclc_MonthLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthLoggedInStart) 
        pLastReadVariableName = "clc_MonthLoggedInTo" 
        pDALParameters.Add("bndclc_MonthLoggedInTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthLoggedInEnd) 
        pLastReadVariableName = "UserName" 
        pDALParameters.Add("GroupByUserName", ccDAL.enmSQLDataType.Bit).Value = vGroupByUserName
        pLastReadVariableName = "TimeLoggedIn" 
        pDALParameters.Add("GroupByTimeLoggedIn", ccDAL.enmSQLDataType.Bit).Value = vGroupByTimeLoggedIn
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("GroupByApplicationName", ccDAL.enmSQLDataType.Bit).Value = vGroupByApplicationName
        pLastReadVariableName = "LoginFaultNumber" 
        pDALParameters.Add("GroupByLoginFaultNumber", ccDAL.enmSQLDataType.Bit).Value = vGroupByLoginFaultNumber
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add("GroupByOriginatingCountry", ccDAL.enmSQLDataType.Bit).Value = vGroupByOriginatingCountry
        pLastReadVariableName = "clc_DateLoggedIn" 
        pDALParameters.Add("GroupByclc_DateLoggedIn", ccDAL.enmSQLDataType.Bit).Value = vGroupByDateLoggedIn
        pLastReadVariableName = "clc_MonthLoggedIn" 
        pDALParameters.Add("GroupByclc_MonthLoggedIn", ccDAL.enmSQLDataType.Bit).Value = vGroupByMonthLoggedIn
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vLoggedLoginArray As csLoggedLogin())
    Me.Clear()
    
    For Each pLoggedLogin As csLoggedLogin In vLoggedLoginArray
      Me.Add(pLoggedLogin)
      _Clean.Add(pLoggedLogin.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pLoggedLogin As New csLoggedLogin(pRow, vRequester) 
        Me.Add(pLoggedLogin) 
        _Clean.Add(pLoggedLogin.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-LoggedLoginCol-130315-2118", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    If _IsCleanForXML = False Then 
      CleanCollectionForXML() 
    End If 
 
    rXML = "" 
    Try 
      Dim pType As Type = Me.GetType 
      pFunctionParameters = pType.Name 
      Dim pSerializer As Xml.Serialization.XmlSerializer 
      pSerializer = New Xml.Serialization.XmlSerializer(pType) 
      Dim MyStringBuilder As New Text.StringBuilder 
      Dim pWriter As New IO.StringWriter(MyStringBuilder) 
      pSerializer.Serialize(pWriter, Me) 
      pWriter.Close() 
      pFault.SetOK() 
 
      rXML = MyStringBuilder.ToString() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-130515-1300", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function FillFromXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLoggedLogins As csLoggedLoginCol = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedLoginCol) 
      For Each pLoggedLogin As csLoggedLogin In pLoggedLogins 
        Me.Add(pLoggedLogin) 
        _Clean.Add(pLoggedLogin.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-LoggedLogin-130515-1329", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Returns JSON for public properties in collection 
  ''' </summary> 
  ''' <param name="rJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function CreateJSON(ByRef rJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    rJSON = "" 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      rJSON = Newtonsoft.Json.JsonConvert.SerializeObject(Me, pSettings) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  ''' <summary> 
  '''   ''' Creates collection using JSON received, for public properties 
  ''' </summary> 
  ''' <param name="vJSON"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadJSON(ByVal vJSON As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Try 
      Dim pSettings As New Newtonsoft.Json.JsonSerializerSettings() 
      Dim pConverter As New Newtonsoft.Json.Converters.StringEnumConverter 
      pSettings.Converters.Add(pConverter) 
 
      Dim pDefaultContractResolver As New Newtonsoft.Json.Serialization.DefaultContractResolver() 
      'This gives the internal fields and private properties as well as well, but has been deprecated.  
      'pDefaultContractResolver.DefaultMembersSearchFlags = pDefaultContractResolver.DefaultMembersSearchFlags Or Reflction.BindingFlags.NonPublic 
      pSettings.ContractResolver = pDefaultContractResolver 
 
      Dim pLoggedLogins As List(Of csLoggedLogin) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csLoggedLogin))(vJSON, pSettings) 
      For Each pLoggedLogin As csLoggedLogin In pLoggedLogins 
        Me.Add(pLoggedLogin) 
        _Clean.Add(pLoggedLogin.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-190720-2059", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateByteArray(ByRef rFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Dim pBytes As Byte() = Nothing 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lLoggedLogin As csLoggedLogin In Me 
            Dim pByte As Byte() = lLoggedLogin.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
            pBinaryWriter.Write(pByte.Length) 
            pBinaryWriter.Write(pByte, 0, pByte.Length) 
          Next 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-150307-2340", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Public Overrides Sub LoadByteArray(ByVal vBytes As Byte(), ByRef rFault As clsFault, ByVal vRequester As clsRequester) 
 
    Me.Clear() 
    
    If rFault Is Nothing Then 
      rFault = New clsFault 
    Else 
      rFault.ClearOK() 
    End If 
 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'Tag  
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pLoggedLogin As csLoggedLogin = New csLoggedLogin(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pLoggedLogin) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pLoggedLogin.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-LoggedLogin-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pLoggedLogin As csLoggedLogin In Me 
      With pLoggedLogin 
        pFault = pLoggedLogin.LoadLookupAndEnumText(vRequester) 
        If Not pFault.isOK Then Exit For 
      End With 
    Next 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vEntitiesToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(vEntitiesToTest As ITargCCCollection) As Boolean 
    If Not (TypeOf (vEntitiesToTest) Is csLoggedLoginCol) Then Return False 
    Dim pLoggedLoginColToTest As csLoggedLoginCol = CType(vEntitiesToTest, csLoggedLoginCol) 
    Return isEqual(pLoggedLoginColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vLoggedLoginsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vLoggedLoginsToTest As csLoggedLoginCol) As Boolean
    If Me.Count <> vLoggedLoginsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vLoggedLoginsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedLogins As New csLoggedLoginCol() 
    If pFilledFromSumOnTheFly Then pLoggedLogins._FilledFromSumOnTheFly = True
    
    For Each pLoggedLogin As csLoggedLogin In Me 
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
      pLoggedLogins.Add(pLoggedLoginClone) 
      If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
    Next 
    Return pLoggedLogins 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedLoginCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedLogins As New csLoggedLoginCol() 
    If pFilledFromSumOnTheFly Then pLoggedLogins._FilledFromSumOnTheFly = True
    
    For Each pLoggedLogin As csLoggedLogin In Me
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
      pLoggedLogins.Add(pLoggedLoginClone)
      If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
    Next
    Return pLoggedLogins
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.ID > vIDFrom AndAlso pLoggedLogin.ID <= vIDTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ApplicationName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedApplicationName(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.ApplicationName > vApplicationNameFrom AndAlso pLoggedLogin.ApplicationName <= vApplicationNameTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by DateLoggedIn (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedDateLoggedIn(ByVal vDateLoggedInStart As Date, ByVal vDateLoggedInEnd As Date) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.DateLoggedIn > vDateLoggedInStart AndAlso pLoggedLogin.DateLoggedIn <= vDateLoggedInEnd) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by LoginFaultNumber (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedLoginFaultNumber(ByVal vLoginFaultNumberFrom As Integer, ByVal vLoginFaultNumberTo As Integer) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.LoginFaultNumber > vLoginFaultNumberFrom AndAlso pLoggedLogin.LoginFaultNumber <= vLoginFaultNumberTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by MonthLoggedIn (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedMonthLoggedIn(ByVal vMonthLoggedInStart As Date, ByVal vMonthLoggedInEnd As Date) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.MonthLoggedIn > vMonthLoggedInStart AndAlso pLoggedLogin.MonthLoggedIn <= vMonthLoggedInEnd) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OriginatingCountry (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOriginatingCountry(ByVal vOriginatingCountryFrom As String, ByVal vOriginatingCountryTo As String) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.OriginatingCountry > vOriginatingCountryFrom AndAlso pLoggedLogin.OriginatingCountry <= vOriginatingCountryTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TimeLoggedIn (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTimeLoggedIn(ByVal vTimeLoggedInStart As Date, ByVal vTimeLoggedInEnd As Date) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.TimeLoggedIn > vTimeLoggedInStart AndAlso pLoggedLogin.TimeLoggedIn <= vTimeLoggedInEnd) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by UserName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.UserName > vUserNameFrom AndAlso pLoggedLogin.UserName <= vUserNameTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by UserName and ApplicationName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedUserNameAndApplicationName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol()  
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedLogin.UserName > vUserNameFrom AndAlso pLoggedLogin.UserName <= vUserNameTo) AndAlso (pLoggedLogin.ApplicationName > vApplicationNameFrom AndAlso pLoggedLogin.ApplicationName <= vApplicationNameTo) Then 
        Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
        pLoggedLogins.Add(pLoggedLoginClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
      End If 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardApplicationName(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedLogin.ApplicationName.StartsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedLogin.ApplicationName.EndsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedLogin.ApplicationName.IndexOf(vApplicationName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vApplicationName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedLogin.ApplicationName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
      pLoggedLogins.Add(pLoggedLoginClone) 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedLogin.OriginatingCountry.StartsWith(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedLogin.OriginatingCountry.EndsWith(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedLogin.OriginatingCountry.IndexOf(vOriginatingCountry, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vOriginatingCountry.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedLogin.OriginatingCountry.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
      pLoggedLogins.Add(pLoggedLoginClone) 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedLogin.UserName.StartsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedLogin.UserName.EndsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedLogin.UserName.IndexOf(vUserName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vUserName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedLogin.UserName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
      pLoggedLogins.Add(pLoggedLoginClone) 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardUserNameAndApplicationName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType) As csLoggedLoginCol 
    Dim pLoggedLogins As New csLoggedLoginCol 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedLogin.UserName.StartsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedLogin.UserName.EndsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedLogin.UserName.IndexOf(vUserName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vUserName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedLogin.UserName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedLogin.ApplicationName.StartsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedLogin.ApplicationName.EndsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedLogin.ApplicationName.IndexOf(vApplicationName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vApplicationName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedLogin.ApplicationName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone() 
      pLoggedLogins.Add(pLoggedLoginClone) 
    Next 
    Return pLoggedLogins 
  End Function 
  
  ''' <summary> 
  ''' Used for Interface compliance. This returns a unique object in the collection. It searches locally, within the collection. It does not access the database  
  ''' If it doesn't find anything, it creates a new, empty object 
  ''' </summary> 
  ''' <param name="vPrimaryKey"></param> 
  ''' <returns></returns> 
  Public Overrides Function FindByPrimaryKey(vPrimaryKey As Long) As ITargCCEntity 
    Return FindByID(vPrimaryKey) 
  End Function 
 
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByID(ByVal vID As Long) As csLoggedLogin
    If Me.Count = 0 Then Return New csLoggedLogin 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
    
    Dim pLoggedLogin As csLoggedLogin = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pLoggedLogin) 
    If pLoggedLogin IsNot Nothing Then Return pLoggedLogin Else Return New csLoggedLogin() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserName(ByVal vUserName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUserName = vUserName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.UserName.ToLowerInvariant() = vUserName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserName with vUserName of {vUserName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UserName.ToLowerInvariant() = vUserName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserFullName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserFullName(ByVal vUserFullName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUserFullName = vUserFullName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.UserFullName.ToLowerInvariant() = vUserFullName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserFullName with vUserFullName of {vUserFullName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UserFullName.ToLowerInvariant() = vUserFullName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeLoggedIn(ByVal vTimeLoggedIn As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.TimeLoggedIn = vTimeLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTimeLoggedIn with vTimeLoggedIn of {vTimeLoggedIn}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.TimeLoggedIn = vTimeLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplicationName(ByVal vApplicationName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplicationName = vApplicationName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplicationName with vApplicationName of {vApplicationName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserIdentityTypeCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserIdentityTypeCode(ByVal vUserIdentityTypeCode As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUserIdentityTypeCode = vUserIdentityTypeCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.UserIdentityTypeCode.ToLowerInvariant() = vUserIdentityTypeCode Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserIdentityTypeCode with vUserIdentityTypeCode of {vUserIdentityTypeCode}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UserIdentityTypeCode.ToLowerInvariant() = vUserIdentityTypeCode Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserIdentityTypeNameCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserIdentityTypeNameCode(ByVal vUserIdentityTypeNameCode As Integer) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.UserIdentityTypeNameCode = vUserIdentityTypeNameCode Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserIdentityTypeNameCode with vUserIdentityTypeNameCode of {vUserIdentityTypeNameCode}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UserIdentityTypeNameCode = vUserIdentityTypeNameCode Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Roles
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRoles(ByVal vRoles As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vRoles = vRoles.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.Roles.ToLowerInvariant() = vRoles Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRoles with vRoles of {vRoles}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.Roles.ToLowerInvariant() = vRoles Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeLoggedOut
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeLoggedOut(ByVal vTimeLoggedOut As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.TimeLoggedOut = vTimeLoggedOut Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTimeLoggedOut with vTimeLoggedOut of {vTimeLoggedOut}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.TimeLoggedOut = vTimeLoggedOut Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoginFaultNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoginFaultNumber(ByVal vLoginFaultNumber As Integer) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.LoginFaultNumber = vLoginFaultNumber Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoginFaultNumber with vLoginFaultNumber of {vLoginFaultNumber}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.LoginFaultNumber = vLoginFaultNumber Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnvironmentUserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnvironmentUserName(ByVal vEnvironmentUserName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEnvironmentUserName = vEnvironmentUserName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.EnvironmentUserName.ToLowerInvariant() = vEnvironmentUserName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnvironmentUserName with vEnvironmentUserName of {vEnvironmentUserName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.EnvironmentUserName.ToLowerInvariant() = vEnvironmentUserName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnvironmentMachineName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnvironmentMachineName(ByVal vEnvironmentMachineName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEnvironmentMachineName = vEnvironmentMachineName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.EnvironmentMachineName.ToLowerInvariant() = vEnvironmentMachineName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnvironmentMachineName with vEnvironmentMachineName of {vEnvironmentMachineName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.EnvironmentMachineName.ToLowerInvariant() = vEnvironmentMachineName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnvironmentUserDomainName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnvironmentUserDomainName(ByVal vEnvironmentUserDomainName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEnvironmentUserDomainName = vEnvironmentUserDomainName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.EnvironmentUserDomainName.ToLowerInvariant() = vEnvironmentUserDomainName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnvironmentUserDomainName with vEnvironmentUserDomainName of {vEnvironmentUserDomainName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.EnvironmentUserDomainName.ToLowerInvariant() = vEnvironmentUserDomainName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DnsGetHostName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDnsGetHostName(ByVal vDnsGetHostName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDnsGetHostName = vDnsGetHostName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.DnsGetHostName.ToLowerInvariant() = vDnsGetHostName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDnsGetHostName with vDnsGetHostName of {vDnsGetHostName}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.DnsGetHostName.ToLowerInvariant() = vDnsGetHostName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AddressList
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAddressList(ByVal vAddressList As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAddressList = vAddressList.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.AddressList.ToLowerInvariant() = vAddressList Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAddressList with vAddressList of {vAddressList}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.AddressList.ToLowerInvariant() = vAddressList Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ComputerMACAddress
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByComputerMACAddress(ByVal vComputerMACAddress As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vComputerMACAddress = vComputerMACAddress.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.ComputerMACAddress.ToLowerInvariant() = vComputerMACAddress Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByComputerMACAddress with vComputerMACAddress of {vComputerMACAddress}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.ComputerMACAddress.ToLowerInvariant() = vComputerMACAddress Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SystemDiskVolumeSerialNo
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySystemDiskVolumeSerialNo(ByVal vSystemDiskVolumeSerialNo As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSystemDiskVolumeSerialNo = vSystemDiskVolumeSerialNo.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.SystemDiskVolumeSerialNo.ToLowerInvariant() = vSystemDiskVolumeSerialNo Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySystemDiskVolumeSerialNo with vSystemDiskVolumeSerialNo of {vSystemDiskVolumeSerialNo}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.SystemDiskVolumeSerialNo.ToLowerInvariant() = vSystemDiskVolumeSerialNo Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LocalTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLocalTime(ByVal vLocalTime As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.LocalTime = vLocalTime Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLocalTime with vLocalTime of {vLocalTime}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.LocalTime = vLocalTime Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined GmtTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByGmtTime(ByVal vGmtTime As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.GmtTime = vGmtTime Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByGmtTime with vGmtTime of {vGmtTime}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.GmtTime = vGmtTime Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AccessingComputerDetails
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAccessingComputerDetails(ByVal vAccessingComputerDetails As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAccessingComputerDetails = vAccessingComputerDetails.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.AccessingComputerDetails.ToLowerInvariant() = vAccessingComputerDetails Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAccessingComputerDetails with vAccessingComputerDetails of {vAccessingComputerDetails}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.AccessingComputerDetails.ToLowerInvariant() = vAccessingComputerDetails Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UICulture
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUICulture(ByVal vUICulture As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUICulture = vUICulture.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.UICulture.ToLowerInvariant() = vUICulture Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUICulture with vUICulture of {vUICulture}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UICulture.ToLowerInvariant() = vUICulture Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TotalPhysicalMemoryKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTotalPhysicalMemoryKb(ByVal vTotalPhysicalMemoryKb As Long) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.TotalPhysicalMemoryKb = vTotalPhysicalMemoryKb Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTotalPhysicalMemoryKb with vTotalPhysicalMemoryKb of {vTotalPhysicalMemoryKb}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.TotalPhysicalMemoryKb = vTotalPhysicalMemoryKb Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AvailablePhysicalMemoryKb
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAvailablePhysicalMemoryKb(ByVal vAvailablePhysicalMemoryKb As Long) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.AvailablePhysicalMemoryKb = vAvailablePhysicalMemoryKb Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAvailablePhysicalMemoryKb with vAvailablePhysicalMemoryKb of {vAvailablePhysicalMemoryKb}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.AvailablePhysicalMemoryKb = vAvailablePhysicalMemoryKb Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApplicationVersion
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplicationVersion(ByVal vApplicationVersion As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplicationVersion = vApplicationVersion.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.ApplicationVersion.ToLowerInvariant() = vApplicationVersion Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplicationVersion with vApplicationVersion of {vApplicationVersion}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.ApplicationVersion.ToLowerInvariant() = vApplicationVersion Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OriginatingIP
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOriginatingIP(ByVal vOriginatingIP As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOriginatingIP = vOriginatingIP.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.OriginatingIP.ToLowerInvariant() = vOriginatingIP Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOriginatingIP with vOriginatingIP of {vOriginatingIP}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.OriginatingIP.ToLowerInvariant() = vOriginatingIP Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Language
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLanguage(ByVal vLanguage As clsEnums.enmLanguage) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.Language = vLanguage Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLanguage with vLanguage of {vLanguage}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.Language = vLanguage Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined HostingAssembly
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByHostingAssembly(ByVal vHostingAssembly As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vHostingAssembly = vHostingAssembly.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.HostingAssembly.ToLowerInvariant() = vHostingAssembly Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByHostingAssembly with vHostingAssembly of {vHostingAssembly}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.HostingAssembly.ToLowerInvariant() = vHostingAssembly Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OriginatingCountry
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOriginatingCountry(ByVal vOriginatingCountry As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOriginatingCountry = vOriginatingCountry.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.OriginatingCountry.ToLowerInvariant() = vOriginatingCountry Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOriginatingCountry with vOriginatingCountry of {vOriginatingCountry}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.OriginatingCountry.ToLowerInvariant() = vOriginatingCountry Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DateLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDateLoggedIn(ByVal vDateLoggedIn As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.DateLoggedIn = vDateLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDateLoggedIn with vDateLoggedIn of {vDateLoggedIn}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.DateLoggedIn = vDateLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MonthLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMonthLoggedIn(ByVal vMonthLoggedIn As Date) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.MonthLoggedIn = vMonthLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMonthLoggedIn with vMonthLoggedIn of {vMonthLoggedIn}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.MonthLoggedIn = vMonthLoggedIn Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ClientReportedIP
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByClientReportedIP(ByVal vClientReportedIP As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vClientReportedIP = vClientReportedIP.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.ClientReportedIP.ToLowerInvariant() = vClientReportedIP Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByClientReportedIP with vClientReportedIP of {vClientReportedIP}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.ClientReportedIP.ToLowerInvariant() = vClientReportedIP Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ClientReportedCountry
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByClientReportedCountry(ByVal vClientReportedCountry As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vClientReportedCountry = vClientReportedCountry.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.ClientReportedCountry.ToLowerInvariant() = vClientReportedCountry Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByClientReportedCountry with vClientReportedCountry of {vClientReportedCountry}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.ClientReportedCountry.ToLowerInvariant() = vClientReportedCountry Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IPAdditionalDetails
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIPAdditionalDetails(ByVal vIPAdditionalDetails As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vIPAdditionalDetails = vIPAdditionalDetails.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.IPAdditionalDetails.ToLowerInvariant() = vIPAdditionalDetails Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIPAdditionalDetails with vIPAdditionalDetails of {vIPAdditionalDetails}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.IPAdditionalDetails.ToLowerInvariant() = vIPAdditionalDetails Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedLogin) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In pTempDist.Values
        If pLoggedLogin.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    
    Return pLoggedLogins
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserNameAndApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserNameAndApplicationName(ByVal vUserName As String, ByVal vApplicationName As String) As csLoggedLoginCol
    Dim pLoggedLogins As New csLoggedLoginCol() 
    pLoggedLogins._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pLoggedLogin As csLoggedLogin In _SortedDictionaryForFindByID.Values.ToList()
        If pLoggedLogin.UserName = vUserName AndAlso pLoggedLogin.ApplicationName = vApplicationName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csLoggedLoginCol = Me.Clone() 
      For Each pLoggedLogin As csLoggedLogin In pList 
        If pLoggedLogin.UserName = vUserName AndAlso pLoggedLogin.ApplicationName = vApplicationName Then
          Dim pLoggedLoginClone As csLoggedLogin = pLoggedLogin.Clone()
          pLoggedLogins.Add(pLoggedLoginClone)
          If Not _FilledFromSumOnTheFly Then pLoggedLogins._Clean.Add(pLoggedLogin.ID) 
        End If
      Next
    End If 
    Return pLoggedLogins
  End Function
  
  ''' <summary> 
  ''' Loads Me into the datatable vDataTable provided. 
  ''' </summary> 
  ''' <param name="vDataTable"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function LoadMeIntoDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    vDataTable.Rows.Clear() 
    For Each pLoggedLogin As csLoggedLogin In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pLoggedLogin.LoadDataRow(pRow, vRequester) 
      If pFault.isOK = False Then Return pFault 
      vDataTable.Rows.Add(pRow) 
    Next 
 
    Return pFault.SetOK 
  End Function 
 
  ''' <summary> 
  ''' This updates a collection that originates from the database. It will delete any rows not in the collection that were originally there (checks the 'Clean' variable) 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.Update
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginView, "csLoggedLoginCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csLoggedLogin In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csLoggedLogin = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pLoggedLoginToKill As New csLoggedLogin 
          pLoggedLoginToKill.ID = pCleanID 
          pLoggedLoginToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pLoggedLoginToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csLoggedLogin In Me 
      If pExists.ccStatus = clsEnums.enmObjectStatus.Dirty OrElse pExists.ccStatus = clsEnums.enmObjectStatus.New Then 
        pFault = pExists.Update(vRequester, vReload) : If pFault.isOK = False Then Exit For 
        _Clean.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.Deleted Then 
        Dim pPrevID As Long = pExists.ID 
        pFault = pExists.Delete(vRequester) : If pFault.isOK = False Then Exit For 
        pExists.ID = pPrevID 
        pToRemove.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.Clean Then 
        _Clean.Add(pExists.ID) 
      ElseIf pExists.ccStatus = clsEnums.enmObjectStatus.UD Then 
        'Status should not be UD  
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-LoggedLogin-130415-0942", vRequester) 
      End If 
    Next 
    
    'Now remove the deleted ones from the collection 
    For Each pIDToDelete As Long In pToRemove 
      Me.Remove(Me.FindByID(pIDToDelete)) 
    Next 
 
    Return pFault 
  End Function 
  
  ''' <summary> 
  ''' This takes an external collection and updates the found rows in the database. If a row is not found (has an ID of 0), it adds it.  
  ''' It will not delete any rows. Check the 'tag' of each item in the collection to see if it was updated.  
  ''' Use the tag of the collection itself if you want to override the function with evtBeforeUpdateWithRequester 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.UpdateFromCollection 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginUpdate, "csLoggedLoginCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csLoggedLogin In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csLoggedLogin In Me 
      p.Tag = "" 
      pFault = p.Update(vRequester, vReload) 
      If pFault.isOK = False Then 
        p.Tag = "Number: " & pFault.Number & ccHelper.NewLine & 
            "Message: " & pFault.Message & ccHelper.NewLine & 
            "Action: " & pFault.Action & ccHelper.NewLine & 
            "Description: " & pFault.Description & ccHelper.NewLine & 
            "FreeText: " & pFault.FreeText.Replace(Environment.NewLine, ccHelper.NewLine) & ccHelper.NewLine & 
            "LoggedAlertID: " & pFault.LoggedAlertID & ccHelper.NewLine 
        pFault.SetOK(vRequester) 
      Else 
        p.Tag = "OK" 
      End If 
    Next 
 
    pFault.SetOK() 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Deletes a collection of all items 
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function Delete(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csLoggedLoginCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ApplicationName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByApplicationName(ByVal vApplicationName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}", vApplicationName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByApplicationName(vApplicationName) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific DateLoggedIn 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByDateLoggedIn(ByVal vDateLoggedIn As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DateLoggedIn={0}", vDateLoggedIn)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByDateLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByDateLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByDateLoggedIn(vDateLoggedIn) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_DateLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = (vDateLoggedIn) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoginFaultNumber 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLoginFaultNumber(ByVal vLoginFaultNumber As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginFaultNumber={0}", vLoginFaultNumber)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByLoginFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByLoginFaultNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByLoginFaultNumber(vLoginFaultNumber) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "LoginFaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumber) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MonthLoggedIn 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByMonthLoggedIn(ByVal vMonthLoggedIn As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthLoggedIn={0}", vMonthLoggedIn)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByMonthLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByMonthLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByMonthLoggedIn(vMonthLoggedIn) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_MonthLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = (vMonthLoggedIn) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OriginatingCountry 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}", vOriginatingCountry)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByOriginatingCountry"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByOriginatingCountry(vOriginatingCountry) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OriginatingCountry" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountry) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeLoggedIn 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTimeLoggedIn(ByVal vTimeLoggedIn As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeLoggedIn={0}", vTimeLoggedIn)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByTimeLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByTimeLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByTimeLoggedIn(vTimeLoggedIn) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeLoggedIn" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vTimeLoggedIn) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByUserName(ByVal vUserName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}", vUserName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByUserName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByUserName(vUserName) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "UserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserNameAndApplicationName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByUserNameAndApplicationName(ByVal vUserName As String, ByVal vApplicationName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, ApplicationName={1}", vUserName, vApplicationName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByUserName&ApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedLogins As New csLoggedLoginCol() : pAllLoggedLogins.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedLogins As csLoggedLoginCol = pAllLoggedLogins.CloneByUserNameAndApplicationName(vUserName, vApplicationName) 
      For Each l In pFilteredLoggedLogins 
        pAllLoggedLogins.Remove(pAllLoggedLogins.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedLogins, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "UserName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserName) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedApplicationName(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationNameFrom={0}, ApplicationNameTo={1}", vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific DateLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedDateLoggedIn(ByVal vDateLoggedInStart As Date, ByVal vDateLoggedInEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DateLoggedInStart={0}, DateLoggedInEnd={1}", vDateLoggedInStart, vDateLoggedInEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedDateLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedDateLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_DateLoggedInFrom" 
        pDALParameters.Add("bndclc_DateLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = (vDateLoggedInStart) 
        pLastReadVariableName = "clc_DateLoggedInTo" 
        pDALParameters.Add("bndclc_DateLoggedInTo", ccDAL.enmSQLDataType.Date).Value = (vDateLoggedInEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoginFaultNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedLoginFaultNumber(ByVal vLoginFaultNumberFrom As Integer, ByVal vLoginFaultNumberTo As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginFaultNumberFrom={0}, LoginFaultNumberTo={1}", vLoginFaultNumberFrom, vLoginFaultNumberTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedLoginFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedLoginFaultNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "LoginFaultNumberFrom" 
        pDALParameters.Add("bndLoginFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumberFrom) 
        pLastReadVariableName = "LoginFaultNumberTo" 
        pDALParameters.Add("bndLoginFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = (vLoginFaultNumberTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MonthLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedMonthLoggedIn(ByVal vMonthLoggedInStart As Date, ByVal vMonthLoggedInEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthLoggedInStart={0}, MonthLoggedInEnd={1}", vMonthLoggedInStart, vMonthLoggedInEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedMonthLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedMonthLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_MonthLoggedInFrom" 
        pDALParameters.Add("bndclc_MonthLoggedInFrom", ccDAL.enmSQLDataType.Date).Value = (vMonthLoggedInStart) 
        pLastReadVariableName = "clc_MonthLoggedInTo" 
        pDALParameters.Add("bndclc_MonthLoggedInTo", ccDAL.enmSQLDataType.Date).Value = (vMonthLoggedInEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OriginatingCountry
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedOriginatingCountry(ByVal vOriginatingCountryFrom As String, ByVal vOriginatingCountryTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountryFrom={0}, OriginatingCountryTo={1}", vOriginatingCountryFrom, vOriginatingCountryTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedOriginatingCountry"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OriginatingCountryFrom" 
        pDALParameters.Add("bndOriginatingCountryFrom", ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountryFrom) 
        pLastReadVariableName = "OriginatingCountryTo" 
        pDALParameters.Add("bndOriginatingCountryTo", ccDAL.enmSQLDataType.VarChar, 10).Value = (vOriginatingCountryTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeLoggedIn
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTimeLoggedIn(ByVal vTimeLoggedInStart As Date, ByVal vTimeLoggedInEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeLoggedInStart={0}, TimeLoggedInEnd={1}", vTimeLoggedInStart, vTimeLoggedInEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedTimeLoggedIn", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedTimeLoggedIn"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeLoggedInFrom" 
        pDALParameters.Add("bndTimeLoggedInFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeLoggedInStart) 
        pLastReadVariableName = "TimeLoggedInTo" 
        pDALParameters.Add("bndTimeLoggedInTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeLoggedInEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}", vUserNameFrom, vUserNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedUserName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "UserNameFrom" 
        pDALParameters.Add("bndUserNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameFrom) 
        pLastReadVariableName = "UserNameTo" 
        pDALParameters.Add("bndUserNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserNameAndApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedUserNameAndApplicationName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}, ApplicationNameFrom={2}, ApplicationNameTo={3}", vUserNameFrom, vUserNameTo, vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByBoundedUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedLoginsDeleteByBoundedUserName&ApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "UserNameFrom" 
        pDALParameters.Add("bndUserNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameFrom) 
        pLastReadVariableName = "UserNameTo" 
        pDALParameters.Add("bndUserNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vUserNameTo) 
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded ApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardApplicationName(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationNameWildcardType={1}", vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByWildCardApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCApplicationName = vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCApplicationName = "%" & vApplicationName 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCApplicationName = "%" & vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vApplicationName.ToCharArray 
        pWCApplicationName &= p & "%" 
      Next 
      pWCApplicationName = "%" & pWCApplicationName 
    End If 
    
    Dim pCommandText As String = "c_LoggedLoginsDeleteByWildCardApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCApplicationName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded OriginatingCountry
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}, OriginatingCountryWildcardType={1}", vOriginatingCountry, vOriginatingCountryWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByWildCardOriginatingCountry", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'OriginatingCountry 
    Dim pWCOriginatingCountry As String = "" 
    If vOriginatingCountryWildcardType = clsEnums.enmWildCardType.After Then 
      pWCOriginatingCountry = vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCOriginatingCountry = "%" & vOriginatingCountry & "%" 
    ElseIf vOriginatingCountryWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vOriginatingCountry.ToCharArray 
        pWCOriginatingCountry &= p & "%" 
      Next 
      pWCOriginatingCountry = "%" & pWCOriginatingCountry 
    End If 
    
    Dim pCommandText As String = "c_LoggedLoginsDeleteByWildCardOriginatingCountry"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldOriginatingCountry" 
        pDALParameters.Add("wldOriginatingCountry", ccDAL.enmSQLDataType.VarChar, 10).Value = (pWCOriginatingCountry) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}", vUserName, vUserNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByWildCardUserName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCUserName = vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCUserName = "%" & vUserName 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCUserName = "%" & vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vUserName.ToCharArray 
        pWCUserName &= p & "%" 
      Next 
      pWCUserName = "%" & pWCUserName 
    End If 
    
    Dim pCommandText As String = "c_LoggedLoginsDeleteByWildCardUserName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldUserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCUserName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded UserNameAndApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardUserNameAndApplicationName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}, ApplicationName={2}, ApplicationNameWildcardType={3}", vUserName, vUserNameWildcardType.FastToString(), vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedLoginDelete, "csLoggedLoginCol_DeleteByWildCardUserNameAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'UserName 
    Dim pWCUserName As String = "" 
    If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCUserName = vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCUserName = "%" & vUserName 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCUserName = "%" & vUserName & "%" 
    ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vUserName.ToCharArray 
        pWCUserName &= p & "%" 
      Next 
      pWCUserName = "%" & pWCUserName 
    End If 
    'ApplicationName 
    Dim pWCApplicationName As String = "" 
    If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCApplicationName = vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCApplicationName = "%" & vApplicationName 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCApplicationName = "%" & vApplicationName & "%" 
    ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vApplicationName.ToCharArray 
        pWCApplicationName &= p & "%" 
      Next 
      pWCApplicationName = "%" & pWCApplicationName 
    End If 
    
    Dim pCommandText As String = "c_LoggedLoginsDeleteByWildCardUserName&ApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedLogin-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldUserName" 
        pDALParameters.Add("wldUserName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCUserName) 
        pLastReadVariableName = "wldApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCApplicationName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedLogin-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedLogin-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csLoggedLoginCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ID < y.ID Then
        Return -1
      ElseIf x.ID = y.ID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUserName()
    Me.Sort(New csLoggedLoginCol.CompareByUserName)
  End Sub
  Private Class CompareByUserName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserName, y.UserName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserFullName()
    Me.Sort(New csLoggedLoginCol.CompareByUserFullName)
  End Sub
  Private Class CompareByUserFullName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserFullName, y.UserFullName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTimeLoggedIn()
    Me.Sort(New csLoggedLoginCol.CompareByTimeLoggedIn)
  End Sub
  Private Class CompareByTimeLoggedIn
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TimeLoggedIn < y.TimeLoggedIn Then
        Return -1
      ElseIf x.TimeLoggedIn = y.TimeLoggedIn Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByApplicationName()
    Me.Sort(New csLoggedLoginCol.CompareByApplicationName)
  End Sub
  Private Class CompareByApplicationName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApplicationName, y.ApplicationName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeCode()
    Me.Sort(New csLoggedLoginCol.CompareByUserIdentityTypeCode)
  End Sub
  Private Class CompareByUserIdentityTypeCode
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeCode, y.UserIdentityTypeCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeText()
    Me.Sort(New csLoggedLoginCol.CompareByUserIdentityTypeText)
  End Sub
  Private Class CompareByUserIdentityTypeText
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeText, y.UserIdentityTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeNameCode()
    Me.Sort(New csLoggedLoginCol.CompareByUserIdentityTypeNameCode)
  End Sub
  Private Class CompareByUserIdentityTypeNameCode
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UserIdentityTypeNameCode < y.UserIdentityTypeNameCode Then
        Return -1
      ElseIf x.UserIdentityTypeNameCode = y.UserIdentityTypeNameCode Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeNameText()
    Me.Sort(New csLoggedLoginCol.CompareByUserIdentityTypeNameText)
  End Sub
  Private Class CompareByUserIdentityTypeNameText
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeNameText, y.UserIdentityTypeNameText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRoles()
    Me.Sort(New csLoggedLoginCol.CompareByRoles)
  End Sub
  Private Class CompareByRoles
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Roles, y.Roles, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTimeLoggedOut()
    Me.Sort(New csLoggedLoginCol.CompareByTimeLoggedOut)
  End Sub
  Private Class CompareByTimeLoggedOut
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TimeLoggedOut < y.TimeLoggedOut Then
        Return -1
      ElseIf x.TimeLoggedOut = y.TimeLoggedOut Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoginFaultNumber()
    Me.Sort(New csLoggedLoginCol.CompareByLoginFaultNumber)
  End Sub
  Private Class CompareByLoginFaultNumber
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoginFaultNumber < y.LoginFaultNumber Then
        Return -1
      ElseIf x.LoginFaultNumber = y.LoginFaultNumber Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByEnvironmentUserName()
    Me.Sort(New csLoggedLoginCol.CompareByEnvironmentUserName)
  End Sub
  Private Class CompareByEnvironmentUserName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnvironmentUserName, y.EnvironmentUserName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEnvironmentMachineName()
    Me.Sort(New csLoggedLoginCol.CompareByEnvironmentMachineName)
  End Sub
  Private Class CompareByEnvironmentMachineName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnvironmentMachineName, y.EnvironmentMachineName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEnvironmentUserDomainName()
    Me.Sort(New csLoggedLoginCol.CompareByEnvironmentUserDomainName)
  End Sub
  Private Class CompareByEnvironmentUserDomainName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnvironmentUserDomainName, y.EnvironmentUserDomainName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDnsGetHostName()
    Me.Sort(New csLoggedLoginCol.CompareByDnsGetHostName)
  End Sub
  Private Class CompareByDnsGetHostName
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DnsGetHostName, y.DnsGetHostName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAddressList()
    Me.Sort(New csLoggedLoginCol.CompareByAddressList)
  End Sub
  Private Class CompareByAddressList
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AddressList, y.AddressList, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByComputerMACAddress()
    Me.Sort(New csLoggedLoginCol.CompareByComputerMACAddress)
  End Sub
  Private Class CompareByComputerMACAddress
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ComputerMACAddress, y.ComputerMACAddress, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySystemDiskVolumeSerialNo()
    Me.Sort(New csLoggedLoginCol.CompareBySystemDiskVolumeSerialNo)
  End Sub
  Private Class CompareBySystemDiskVolumeSerialNo
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SystemDiskVolumeSerialNo, y.SystemDiskVolumeSerialNo, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLocalTime()
    Me.Sort(New csLoggedLoginCol.CompareByLocalTime)
  End Sub
  Private Class CompareByLocalTime
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LocalTime < y.LocalTime Then
        Return -1
      ElseIf x.LocalTime = y.LocalTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByGmtTime()
    Me.Sort(New csLoggedLoginCol.CompareByGmtTime)
  End Sub
  Private Class CompareByGmtTime
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.GmtTime < y.GmtTime Then
        Return -1
      ElseIf x.GmtTime = y.GmtTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByAccessingComputerDetails()
    Me.Sort(New csLoggedLoginCol.CompareByAccessingComputerDetails)
  End Sub
  Private Class CompareByAccessingComputerDetails
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AccessingComputerDetails, y.AccessingComputerDetails, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUICulture()
    Me.Sort(New csLoggedLoginCol.CompareByUICulture)
  End Sub
  Private Class CompareByUICulture
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UICulture, y.UICulture, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTotalPhysicalMemoryKb()
    Me.Sort(New csLoggedLoginCol.CompareByTotalPhysicalMemoryKb)
  End Sub
  Private Class CompareByTotalPhysicalMemoryKb
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TotalPhysicalMemoryKb < y.TotalPhysicalMemoryKb Then
        Return -1
      ElseIf x.TotalPhysicalMemoryKb = y.TotalPhysicalMemoryKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByAvailablePhysicalMemoryKb()
    Me.Sort(New csLoggedLoginCol.CompareByAvailablePhysicalMemoryKb)
  End Sub
  Private Class CompareByAvailablePhysicalMemoryKb
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.AvailablePhysicalMemoryKb < y.AvailablePhysicalMemoryKb Then
        Return -1
      ElseIf x.AvailablePhysicalMemoryKb = y.AvailablePhysicalMemoryKb Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByApplicationVersion()
    Me.Sort(New csLoggedLoginCol.CompareByApplicationVersion)
  End Sub
  Private Class CompareByApplicationVersion
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApplicationVersion, y.ApplicationVersion, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOriginatingIP()
    Me.Sort(New csLoggedLoginCol.CompareByOriginatingIP)
  End Sub
  Private Class CompareByOriginatingIP
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OriginatingIP, y.OriginatingIP, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLanguage()
    Me.Sort(New csLoggedLoginCol.CompareByLanguage)
  End Sub
  Private Class CompareByLanguage
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Language < y.Language Then
        Return -1
      ElseIf x.Language = y.Language Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLanguageText()
    Me.Sort(New csLoggedLoginCol.CompareByLanguageText)
  End Sub
  Private Class CompareByLanguageText
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LanguageText, y.LanguageText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByHostingAssembly()
    Me.Sort(New csLoggedLoginCol.CompareByHostingAssembly)
  End Sub
  Private Class CompareByHostingAssembly
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.HostingAssembly, y.HostingAssembly, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOriginatingCountry()
    Me.Sort(New csLoggedLoginCol.CompareByOriginatingCountry)
  End Sub
  Private Class CompareByOriginatingCountry
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OriginatingCountry, y.OriginatingCountry, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDateLoggedIn()
    Me.Sort(New csLoggedLoginCol.CompareByDateLoggedIn)
  End Sub
  Private Class CompareByDateLoggedIn
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DateLoggedIn < y.DateLoggedIn Then
        Return -1
      ElseIf x.DateLoggedIn = y.DateLoggedIn Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByMonthLoggedIn()
    Me.Sort(New csLoggedLoginCol.CompareByMonthLoggedIn)
  End Sub
  Private Class CompareByMonthLoggedIn
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.MonthLoggedIn < y.MonthLoggedIn Then
        Return -1
      ElseIf x.MonthLoggedIn = y.MonthLoggedIn Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByClientReportedIP()
    Me.Sort(New csLoggedLoginCol.CompareByClientReportedIP)
  End Sub
  Private Class CompareByClientReportedIP
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ClientReportedIP, y.ClientReportedIP, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByClientReportedCountry()
    Me.Sort(New csLoggedLoginCol.CompareByClientReportedCountry)
  End Sub
  Private Class CompareByClientReportedCountry
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ClientReportedCountry, y.ClientReportedCountry, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIPAdditionalDetails()
    Me.Sort(New csLoggedLoginCol.CompareByIPAdditionalDetails)
  End Sub
  Private Class CompareByIPAdditionalDetails
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IPAdditionalDetails, y.IPAdditionalDetails, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csLoggedLoginCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csLoggedLogin)
    Private Function Compare(ByVal x As csLoggedLogin, ByVal y As csLoggedLogin) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedLogin).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
#Region "Load Collection"  
  Friend Function LoadMeFromIDataReader(vReader As IDataReader, vRequester As clsRequester) As clsFault Implements ITargCCDataReaderUser.LoadMeFromIDataReader 
    Dim pFunctionParameters As String = ""  
    Dim pFault As New clsFault
  
    Dim pLoggedLogin As csLoggedLogin
  
    While vReader.Read()
      pLoggedLogin = New csLoggedLogin() 
      pFault = pLoggedLogin.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pLoggedLogin)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedLogin.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedLoginCol As csLoggedLoginCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pLoggedLogin As csLoggedLogin 
 
      For Each pCachedLoggedLogin As csLoggedLogin In vCachedLoggedLoginCol 
        pLoggedLogin = New csLoggedLogin(pCachedLoggedLogin) 
        pLoggedLogin.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pLoggedLogin) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedLogin.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedLogin-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedLogin) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedLogin) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
