Public Class csLoggedLogin
  Inherits cTargCCEntity 
 
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
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  
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
    
    CreateEmpty() 
    
    If vID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vID 
          pBinaryWriter.Write(vID) 
          ' 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csLoggedLoginGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the LoggedLogin 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
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
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLoggedLogin) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
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
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
    
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
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
  Public Function FillByApplicationName(ByVal vApplicationName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}", vApplicationName)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByApplicationName" 
      Dim pParametersToLog = $"ApplicationName: {vApplicationName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByDateLoggedIn(ByVal vDateLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DateLoggedIn={0}", vDateLoggedIn)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vDateLoggedIn 
          pBinaryWriter.Write(vDateLoggedIn.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByDateLoggedIn" 
      Dim pParametersToLog = $"DateLoggedIn: {vDateLoggedIn};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByLoginFaultNumber(ByVal vLoginFaultNumber As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginFaultNumber={0}", vLoginFaultNumber)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLoginFaultNumber 
          pBinaryWriter.Write(vLoginFaultNumber) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByLoginFaultNumber" 
      Dim pParametersToLog = $"LoginFaultNumber: {vLoginFaultNumber};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByMonthLoggedIn(ByVal vMonthLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthLoggedIn={0}", vMonthLoggedIn)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMonthLoggedIn 
          pBinaryWriter.Write(vMonthLoggedIn.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByMonthLoggedIn" 
      Dim pParametersToLog = $"MonthLoggedIn: {vMonthLoggedIn};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}", vOriginatingCountry)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOriginatingCountry 
          If vOriginatingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountry) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByOriginatingCountry" 
      Dim pParametersToLog = $"OriginatingCountry: {vOriginatingCountry};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByTimeLoggedIn(ByVal vTimeLoggedIn As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeLoggedIn={0}", vTimeLoggedIn)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vTimeLoggedIn 
          pBinaryWriter.Write(vTimeLoggedIn.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByTimeLoggedIn" 
      Dim pParametersToLog = $"TimeLoggedIn: {vTimeLoggedIn};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByUserName(ByVal vUserName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}", vUserName)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByUserName" 
      Dim pParametersToLog = $"UserName: {vUserName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName and ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserNameAndApplicationName(ByVal vUserName As String, ByVal vApplicationName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, ApplicationName={1}", vUserName, vApplicationName)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByUserNameAndApplicationName" 
      Dim pParametersToLog = $"UserNameAndApplicationName: {vUserName};{vApplicationName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationNameFrom 
          If vApplicationNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameFrom) 
          ' 
          'vApplicationNameTo 
          If vApplicationNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedApplicationName" 
      Dim pParametersToLog = $"ApplicationName: {vApplicationNameFrom};{vApplicationNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vDateLoggedInStart 
          pBinaryWriter.Write(vDateLoggedInStart.Ticks) 
          ' 
          'vDateLoggedInEnd 
          pBinaryWriter.Write(vDateLoggedInEnd.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedDateLoggedIn" 
      Dim pParametersToLog = $"DateLoggedIn: {vDateLoggedInStart};{vDateLoggedInEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLoginFaultNumberFrom 
          pBinaryWriter.Write(vLoginFaultNumberFrom) 
          ' 
          'vLoginFaultNumberTo 
          pBinaryWriter.Write(vLoginFaultNumberTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedLoginFaultNumber" 
      Dim pParametersToLog = $"LoginFaultNumber: {vLoginFaultNumberFrom};{vLoginFaultNumberTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vMonthLoggedInStart 
          pBinaryWriter.Write(vMonthLoggedInStart.Ticks) 
          ' 
          'vMonthLoggedInEnd 
          pBinaryWriter.Write(vMonthLoggedInEnd.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedMonthLoggedIn" 
      Dim pParametersToLog = $"MonthLoggedIn: {vMonthLoggedInStart};{vMonthLoggedInEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOriginatingCountryFrom 
          If vOriginatingCountryFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountryFrom) 
          ' 
          'vOriginatingCountryTo 
          If vOriginatingCountryTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountryTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedOriginatingCountry" 
      Dim pParametersToLog = $"OriginatingCountry: {vOriginatingCountryFrom};{vOriginatingCountryTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vTimeLoggedInStart 
          pBinaryWriter.Write(vTimeLoggedInStart.Ticks) 
          ' 
          'vTimeLoggedInEnd 
          pBinaryWriter.Write(vTimeLoggedInEnd.Ticks) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedTimeLoggedIn" 
      Dim pParametersToLog = $"TimeLoggedIn: {vTimeLoggedInStart};{vTimeLoggedInEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserNameFrom 
          If vUserNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameFrom) 
          ' 
          'vUserNameTo 
          If vUserNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedUserName" 
      Dim pParametersToLog = $"UserName: {vUserNameFrom};{vUserNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName and ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserNameAndApplicationName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}, ApplicationNameFrom={2}, ApplicationNameTo={3}", vUserNameFrom, vUserNameTo, vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserNameFrom 
          If vUserNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameFrom) 
          ' 
          'vUserNameTo 
          If vUserNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameTo) 
          ' 
          'vApplicationNameFrom 
          If vApplicationNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameFrom) 
          ' 
          'vApplicationNameTo 
          If vApplicationNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByBoundedUserNameAndApplicationName" 
      Dim pParametersToLog = $"UserNameAndApplicationName: {vUserNameFrom};{vUserNameTo};{vApplicationNameFrom};{vApplicationNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByWildCardApplicationName(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationNameWildcardType={1}", vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vApplicationNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByWildCardApplicationName" 
      Dim pParametersToLog = $"ApplicationName: {vApplicationName};{vApplicationNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByWildCardOriginatingCountry(ByVal vOriginatingCountry As String, ByVal vOriginatingCountryWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginatingCountry={0}, OriginatingCountryWildcardType={1}", vOriginatingCountry, vOriginatingCountryWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vOriginatingCountry 
          If vOriginatingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountry) 
          ' 
          pBinaryWriter.Write(vOriginatingCountryWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByWildCardOriginatingCountry" 
      Dim pParametersToLog = $"OriginatingCountry: {vOriginatingCountry};{vOriginatingCountryWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
  Public Function FillByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}", vUserName, vUserNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(vUserNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByWildCardUserName" 
      Dim pParametersToLog = $"UserName: {vUserName};{vUserNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName and ApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardUserNameAndApplicationName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}, ApplicationName={2}, ApplicationNameWildcardType={3}", vUserName, vUserNameWildcardType.FastToString(), vApplicationName, vApplicationNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(vUserNameWildcardType.FastToString())
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vApplicationNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByWildCardUserNameAndApplicationName" 
      Dim pParametersToLog = $"UserNameAndApplicationName: {vUserName};{vUserNameWildcardType.FastToString()};{vApplicationName};{vApplicationNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>  
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false).  
  ''' An ID can only exist once in the collection. If it's already in the collection, it will be removed from vIDs before sending to the server. 
  ''' </summary>  
  ''' <param name="vIDs"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="vDir"></param>  
  ''' <param name="vAppend"></param>  
  ''' <returns></returns>  
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    'If it's append, we have to ensure no doubles, even though we're not sending the collection to the server 
    If vAppend = True Then 
      For Each l In Me 
        If vIDs.Contains(l.ID) Then 
          vIDs.Remove(l.ID) 
        End If 
      Next 
    End If 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDs 
          pBinaryWriter.Write(vIDs.Count) 
          For Each l In vIDs 
            pBinaryWriter.Write(l) 
          Next 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin   
      If vAppend = True Then 
        Dim pLoggedLogins As New csLoggedLoginCol 
        pLoggedLogins.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pLoggedLogins) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-231207-1750", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'UserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) : pBinaryWriter.Write(vUserNameWildcardType.FastToString()) : pParametersToLog &= $"UserName={vUserName};" : pParametersToLog &= $"UserNameWildcardType={vUserNameWildcardType};"  
          'TimeLoggedIn 
          pBinaryWriter.Write(vTimeLoggedInStart.HasValue) 
          If vTimeLoggedInStart.HasValue Then pBinaryWriter.Write(vTimeLoggedInStart.Value.Ticks) : pParametersToLog &= $"TimeLoggedInStart={vTimeLoggedInStart.Value};"  
          pBinaryWriter.Write(vTimeLoggedInEnd.HasValue) 
          If vTimeLoggedInEnd.HasValue Then pBinaryWriter.Write(vTimeLoggedInEnd.Value.Ticks) : pParametersToLog &= $"TimeLoggedInEnd={vTimeLoggedInEnd.Value};"  
          'ApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) : pBinaryWriter.Write(vApplicationNameWildcardType.FastToString()) : pParametersToLog &= $"ApplicationName={vApplicationName};" : pParametersToLog &= $"ApplicationNameWildcardType={vApplicationNameWildcardType};"  
          'LoginFaultNumber 
          pBinaryWriter.Write(vLoginFaultNumberFrom.HasValue) 
          If vLoginFaultNumberFrom.HasValue Then pBinaryWriter.Write(vLoginFaultNumberFrom.Value) : pParametersToLog &= $"LoginFaultNumberFrom={vLoginFaultNumberFrom};"  
          pBinaryWriter.Write(vLoginFaultNumberTo.HasValue) 
          If vLoginFaultNumberTo.HasValue Then pBinaryWriter.Write(vLoginFaultNumberTo.Value) : pParametersToLog &= $"LoginFaultNumberTo={vLoginFaultNumberTo};"  
          'OriginatingCountry 
          If vOriginatingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountry) : pBinaryWriter.Write(vOriginatingCountryWildcardType.FastToString()) : pParametersToLog &= $"OriginatingCountry={vOriginatingCountry};" : pParametersToLog &= $"OriginatingCountryWildcardType={vOriginatingCountryWildcardType};"  
          'DateLoggedIn 
          pBinaryWriter.Write(vDateLoggedInStart.HasValue) 
          If vDateLoggedInStart.HasValue Then pBinaryWriter.Write(vDateLoggedInStart.Value.Ticks) : pParametersToLog &= $"DateLoggedInStart={vDateLoggedInStart.Value};"  
          pBinaryWriter.Write(vDateLoggedInEnd.HasValue) 
          If vDateLoggedInEnd.HasValue Then pBinaryWriter.Write(vDateLoggedInEnd.Value.Ticks) : pParametersToLog &= $"DateLoggedInEnd={vDateLoggedInEnd.Value};"  
          'MonthLoggedIn 
          pBinaryWriter.Write(vMonthLoggedInStart.HasValue) 
          If vMonthLoggedInStart.HasValue Then pBinaryWriter.Write(vMonthLoggedInStart.Value.Ticks) : pParametersToLog &= $"MonthLoggedInStart={vMonthLoggedInStart.Value};"  
          pBinaryWriter.Write(vMonthLoggedInEnd.HasValue) 
          If vMonthLoggedInEnd.HasValue Then pBinaryWriter.Write(vMonthLoggedInEnd.Value.Ticks) : pParametersToLog &= $"MonthLoggedInEnd={vMonthLoggedInEnd.Value};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'UserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) : pBinaryWriter.Write(vUserNameWildcardType.FastToString()) 
          'TimeLoggedIn 
          pBinaryWriter.Write(vTimeLoggedInStart.HasValue) 
          If vTimeLoggedInStart.HasValue Then pBinaryWriter.Write(vTimeLoggedInStart.Value.Ticks) : pParametersToLog &= $"TimeLoggedInStart={vTimeLoggedInStart};"  
          pBinaryWriter.Write(vTimeLoggedInEnd.HasValue) 
          If vTimeLoggedInEnd.HasValue Then pBinaryWriter.Write(vTimeLoggedInEnd.Value.Ticks) : pParametersToLog &= $"TimeLoggedInEnd={vTimeLoggedInEnd};"  
          'ApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) : pBinaryWriter.Write(vApplicationNameWildcardType.FastToString()) 
          'LoginFaultNumber 
          pBinaryWriter.Write(vLoginFaultNumberFrom.HasValue) 
          If vLoginFaultNumberFrom.HasValue Then pBinaryWriter.Write(vLoginFaultNumberFrom.Value) : pParametersToLog &= $"LoginFaultNumberFrom={vLoginFaultNumberFrom};"  
          pBinaryWriter.Write(vLoginFaultNumberTo.HasValue) 
          If vLoginFaultNumberTo.HasValue Then pBinaryWriter.Write(vLoginFaultNumberTo.Value) : pParametersToLog &= $"LoginFaultNumberTo={vLoginFaultNumberTo};"  
          'OriginatingCountry 
          If vOriginatingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vOriginatingCountry) : pBinaryWriter.Write(vOriginatingCountryWildcardType.FastToString()) 
          'DateLoggedIn 
          pBinaryWriter.Write(vDateLoggedInStart.HasValue) 
          If vDateLoggedInStart.HasValue Then pBinaryWriter.Write(vDateLoggedInStart.Value.Ticks) : pParametersToLog &= $"DateLoggedInStart={vDateLoggedInStart};"  
          pBinaryWriter.Write(vDateLoggedInEnd.HasValue) 
          If vDateLoggedInEnd.HasValue Then pBinaryWriter.Write(vDateLoggedInEnd.Value.Ticks) : pParametersToLog &= $"DateLoggedInEnd={vDateLoggedInEnd};"  
          'MonthLoggedIn 
          pBinaryWriter.Write(vMonthLoggedInStart.HasValue) 
          If vMonthLoggedInStart.HasValue Then pBinaryWriter.Write(vMonthLoggedInStart.Value.Ticks) : pParametersToLog &= $"MonthLoggedInStart={vMonthLoggedInStart};"  
          pBinaryWriter.Write(vMonthLoggedInEnd.HasValue) 
          If vMonthLoggedInEnd.HasValue Then pBinaryWriter.Write(vMonthLoggedInEnd.Value.Ticks) : pParametersToLog &= $"MonthLoggedInEnd={vMonthLoggedInEnd};"  
          pBinaryWriter.Write(vGroupByUserName) : pParametersToLog &= $"GroupByUserName={vGroupByUserName};"  
          pBinaryWriter.Write(vGroupByTimeLoggedIn) : pParametersToLog &= $"GroupByTimeLoggedIn={vGroupByTimeLoggedIn};"  
          pBinaryWriter.Write(vGroupByApplicationName) : pParametersToLog &= $"GroupByApplicationName={vGroupByApplicationName};"  
          pBinaryWriter.Write(vGroupByLoginFaultNumber) : pParametersToLog &= $"GroupByLoginFaultNumber={vGroupByLoginFaultNumber};"  
          pBinaryWriter.Write(vGroupByOriginatingCountry) : pParametersToLog &= $"GroupByOriginatingCountry={vGroupByOriginatingCountry};"  
          pBinaryWriter.Write(vGroupByDateLoggedIn) : pParametersToLog &= $"GroupByDateLoggedIn={vGroupByDateLoggedIn};"  
          pBinaryWriter.Write(vGroupByMonthLoggedIn) : pParametersToLog &= $"GroupByMonthLoggedIn={vGroupByMonthLoggedIn};"  
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csLoggedLoginColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the LoggedLogin  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-LoggedLogin-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
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
  
