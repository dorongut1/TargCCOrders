Public Class csLoggedAlert
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
  Implements ITargCCDataReaderUser 
 
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return True 
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
    [AffectedUser] 
    [FaultType] 
    [FaultSeverity] 
    [LoggedLogin] 
    [UserIdentityType] 
    [UserIdentityTypeName] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [LoggedJob] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [TimeOccurred] 
    [FaultNumber] 
    [SystemName] 
    [CallingApplication] 
    [AffectedUser] 
    [CallingApplicationVersion] 
    [CallingFunctionWithinApplication] 
    [FreeText] 
    [FaultingAssembly] 
    [AssemblyEntryPoint] 
    [FaultingClass] 
    [FaultingFunction] 
    [FaultingFunctionParameters] 
    [FaultIdent] 
    [FaultDescription] 
    [MessageSentToUser] 
    [ActionSentToUser] 
    [FaultType] 
    [FaultSeverity] 
    [LoggedLogin] 
    [Thread] 
    [UserIdentityType] 
    [UserIdentityTypeName] 
    [DateOccurred] 
    [MonthOccurred] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [FaultNumber] 
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
  
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _TimeOccurred As Date
  Private _FaultNumber As Integer
  Private _SystemName As String
  Private _CallingApplication As String
  Private _AffectedUserID As Long
  Private _AffectedUser As csUser
  Private _AffectedUserText As String
  Private _CallingApplicationVersion As String
  Private _CallingFunctionWithinApplication As String
  Private _FreeText As String
  Private _FaultingAssembly As String
  Private _AssemblyEntryPoint As String
  Private _FaultingClass As String
  Private _FaultingFunction As String
  Private _FaultingFunctionParameters As String
  Private _FaultIdent As String
  Private _FaultDescription As String
  Private _MessageSentToUser As String
  Private _ActionSentToUser As String
  Private _FaultType As clsEnums.enmFaultType
  Private _FaultTypeText As String 
  Private _FaultSeverity As clsEnums.enmFaultSeverity
  Private _FaultSeverityText As String 
  Private _LoggedLoginID As Long
  Private _LoggedLogin As csLoggedLogin
  Private _LoggedLoginText As String
  Private _Thread As String
  Private _UserIdentityTypeCode As String
  Private _UserIdentityTypeText As String 
  Private _UserIdentityTypeNameCode As Integer
  Private _UserIdentityTypeNameText As String 
  Private _DateOccurred As Date
  Private _MonthOccurred As Date
  Private _Tag As String
  Private _LoggedJobs As csLoggedJobCol
  
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
  Public Property [TimeOccurred]() As Date
    Get
      Return Me._TimeOccurred
    End Get
    Set(ByVal value As Date)
      If Me._TimeOccurred <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TimeOccurred = value 
      End If 
    End Set
  End Property
  Public Property [FaultNumber]() As Integer
    Get
      Return Me._FaultNumber
    End Get
    Set(ByVal value As Integer)
      If Me._FaultNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultNumber = value 
      End If 
    End Set
  End Property
  Public Property [SystemName]() As String
    Get
      Return Me._SystemName
    End Get
    Set(ByVal value As String)
      If Me._SystemName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SystemName = value 
      End If 
    End Set
  End Property
  Public Property [CallingApplication]() As String
    Get
      Return Me._CallingApplication
    End Get
    Set(ByVal value As String)
      If Me._CallingApplication <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CallingApplication = value 
      End If 
    End Set
  End Property
  Public Property [AffectedUserID]() As Long
    Get
      Return Me._AffectedUserID
    End Get
    Set(ByVal value As Long)
      If Me._AffectedUserID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AffectedUserID = value 
      End If 
    End Set
  End Property
  Public Property [AffectedUser]() As csUser
    Get
      Return Me._AffectedUser
    End Get
    Set(ByVal value As csUser)
      Me._AffectedUser = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the User object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property AffectedUserText() As String
    Get
      Return Me._AffectedUserText
    End Get
    Set(ByVal value As String)
      Me._AffectedUserText = value
    End Set
  End Property
  Public Property [CallingApplicationVersion]() As String
    Get
      Return Me._CallingApplicationVersion
    End Get
    Set(ByVal value As String)
      If Me._CallingApplicationVersion <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CallingApplicationVersion = value 
      End If 
    End Set
  End Property
  Public Property [CallingFunctionWithinApplication]() As String
    Get
      Return Me._CallingFunctionWithinApplication
    End Get
    Set(ByVal value As String)
      If Me._CallingFunctionWithinApplication <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CallingFunctionWithinApplication = value 
      End If 
    End Set
  End Property
  Public Property [FreeText]() As String
    Get
      Return Me._FreeText
    End Get
    Set(ByVal value As String)
      If Me._FreeText <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FreeText = value 
      End If 
    End Set
  End Property
  Public Property [FaultingAssembly]() As String
    Get
      Return Me._FaultingAssembly
    End Get
    Set(ByVal value As String)
      If Me._FaultingAssembly <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultingAssembly = value 
      End If 
    End Set
  End Property
  Public Property [AssemblyEntryPoint]() As String
    Get
      Return Me._AssemblyEntryPoint
    End Get
    Set(ByVal value As String)
      If Me._AssemblyEntryPoint <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AssemblyEntryPoint = value 
      End If 
    End Set
  End Property
  Public Property [FaultingClass]() As String
    Get
      Return Me._FaultingClass
    End Get
    Set(ByVal value As String)
      If Me._FaultingClass <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultingClass = value 
      End If 
    End Set
  End Property
  Public Property [FaultingFunction]() As String
    Get
      Return Me._FaultingFunction
    End Get
    Set(ByVal value As String)
      If Me._FaultingFunction <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultingFunction = value 
      End If 
    End Set
  End Property
  Public Property [FaultingFunctionParameters]() As String
    Get
      Return Me._FaultingFunctionParameters
    End Get
    Set(ByVal value As String)
      If Me._FaultingFunctionParameters <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultingFunctionParameters = value 
      End If 
    End Set
  End Property
  Public Property [FaultIdent]() As String
    Get
      Return Me._FaultIdent
    End Get
    Set(ByVal value As String)
      If Me._FaultIdent <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultIdent = value 
      End If 
    End Set
  End Property
  Public Property [FaultDescription]() As String
    Get
      Return Me._FaultDescription
    End Get
    Set(ByVal value As String)
      If Me._FaultDescription <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultDescription = value 
      End If 
    End Set
  End Property
  Public Property [MessageSentToUser]() As String
    Get
      Return Me._MessageSentToUser
    End Get
    Set(ByVal value As String)
      If Me._MessageSentToUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._MessageSentToUser = value 
      End If 
    End Set
  End Property
  Public Property [ActionSentToUser]() As String
    Get
      Return Me._ActionSentToUser
    End Get
    Set(ByVal value As String)
      If Me._ActionSentToUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ActionSentToUser = value 
      End If 
    End Set
  End Property
  Public Property [FaultType]() As clsEnums.enmFaultType
    Get
      Return Me._FaultType
    End Get
    Set(ByVal value As clsEnums.enmFaultType)
      If Me._FaultType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultType = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [FaultTypeText]() As String
    Get
      Return Me._FaultTypeText
    End Get
    Set(ByVal value As String)
      Me._FaultTypeText = value
    End Set
  End Property
  Public Property [FaultSeverity]() As clsEnums.enmFaultSeverity
    Get
      Return Me._FaultSeverity
    End Get
    Set(ByVal value As clsEnums.enmFaultSeverity)
      If Me._FaultSeverity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FaultSeverity = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [FaultSeverityText]() As String
    Get
      Return Me._FaultSeverityText
    End Get
    Set(ByVal value As String)
      Me._FaultSeverityText = value
    End Set
  End Property
  Public Property [LoggedLoginID]() As Long
    Get
      Return Me._LoggedLoginID
    End Get
    Set(ByVal value As Long)
      If Me._LoggedLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoggedLoginID = value 
      End If 
    End Set
  End Property
  Public Property [LoggedLogin]() As csLoggedLogin
    Get
      Return Me._LoggedLogin
    End Get
    Set(ByVal value As csLoggedLogin)
      Me._LoggedLogin = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the LoggedLogin object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property LoggedLoginText() As String
    Get
      Return Me._LoggedLoginText
    End Get
    Set(ByVal value As String)
      Me._LoggedLoginText = value
    End Set
  End Property
  Public Property [Thread]() As String
    Get
      Return Me._Thread
    End Get
    Set(ByVal value As String)
      If Me._Thread <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Thread = value 
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
  Public ReadOnly Property [DateOccurred]() As Date
    Get
      Return Me._DateOccurred
    End Get
  End Property
  Public ReadOnly Property [MonthOccurred]() As Date
    Get
      Return Me._MonthOccurred
    End Get
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
  Public Property [LoggedJobs]() As csLoggedJobCol
    Get
      Return Me._LoggedJobs
    End Get
    Set(ByVal value As csLoggedJobCol)
      Me._LoggedJobs = value
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
    If Not (_TimeOccurred = Nothing) Then pValue.Append("TimeOccurred='" & _TimeOccurred.ToString("o") & "' ‡ ") 
    If _FaultNumber <> 0 Then pValue.Append("FaultNumber='" & _FaultNumber.ToString() & "' ‡ ") 
    If _SystemName <> "" Then pValue.Append("SystemName='" & _SystemName & "' ‡ ") 
    If _CallingApplication <> "" Then pValue.Append("CallingApplication='" & _CallingApplication & "' ‡ ") 
    If _AffectedUserID <> 0 Then pValue.Append("AffectedUserID='" & _AffectedUserID.ToString() & "' ‡ ") 
    If _AffectedUserText <> "" Then pValue.Append("AffectedUserText='" & _AffectedUserText & "' ‡ ") 
    If _CallingApplicationVersion <> "" Then pValue.Append("CallingApplicationVersion='" & _CallingApplicationVersion & "' ‡ ") 
    If _CallingFunctionWithinApplication <> "" Then pValue.Append("CallingFunctionWithinApplication='" & _CallingFunctionWithinApplication & "' ‡ ") 
    If _FreeText <> "" Then pValue.Append("FreeText='" & _FreeText & "' ‡ ") 
    If _FaultingAssembly <> "" Then pValue.Append("FaultingAssembly='" & _FaultingAssembly & "' ‡ ") 
    If _AssemblyEntryPoint <> "" Then pValue.Append("AssemblyEntryPoint='" & _AssemblyEntryPoint & "' ‡ ") 
    If _FaultingClass <> "" Then pValue.Append("FaultingClass='" & _FaultingClass & "' ‡ ") 
    If _FaultingFunction <> "" Then pValue.Append("FaultingFunction='" & _FaultingFunction & "' ‡ ") 
    If _FaultingFunctionParameters <> "" Then pValue.Append("FaultingFunctionParameters='" & _FaultingFunctionParameters & "' ‡ ") 
    If _FaultIdent <> "" Then pValue.Append("FaultIdent='" & _FaultIdent & "' ‡ ") 
    If _FaultDescription <> "" Then pValue.Append("FaultDescription='" & _FaultDescription & "' ‡ ") 
    If _MessageSentToUser <> "" Then pValue.Append("MessageSentToUser='" & _MessageSentToUser & "' ‡ ") 
    If _ActionSentToUser <> "" Then pValue.Append("ActionSentToUser='" & _ActionSentToUser & "' ‡ ") 
    If _FaultType <> clsEnums.enmFaultType.UD Then pValue.Append("FaultType='" & _FaultType.FastToString() & "' ‡ ") 
    If _FaultTypeText <> "" Then pValue.Append("FaultTypeText='" & _FaultTypeText & "' ‡ ") 
    If _FaultSeverity <> clsEnums.enmFaultSeverity.UD Then pValue.Append("FaultSeverity='" & _FaultSeverity.FastToString() & "' ‡ ") 
    If _FaultSeverityText <> "" Then pValue.Append("FaultSeverityText='" & _FaultSeverityText & "' ‡ ") 
    If _LoggedLoginID <> 0 Then pValue.Append("LoggedLoginID='" & _LoggedLoginID.ToString() & "' ‡ ") 
    If _LoggedLoginText <> "" Then pValue.Append("LoggedLoginText='" & _LoggedLoginText & "' ‡ ") 
    If _Thread <> "" Then pValue.Append("Thread='" & _Thread & "' ‡ ") 
    If _UserIdentityTypeCode <> "" Then pValue.Append("UserIdentityTypeCode='" & _UserIdentityTypeCode & "' ‡ ") 
    If _UserIdentityTypeText <> "" Then pValue.Append("UserIdentityTypeText='" & _UserIdentityTypeText & "' ‡ ") 
    If _UserIdentityTypeNameCode <> -1 Then pValue.Append("UserIdentityTypeNameCode='" & _UserIdentityTypeNameCode.ToString() & "' ‡ ") 
    If _UserIdentityTypeNameText <> "" Then pValue.Append("UserIdentityTypeNameText='" & _UserIdentityTypeNameText & "' ‡ ") 
    If Not (_DateOccurred = Nothing) Then pValue.Append("DateOccurred='" & _DateOccurred.ToString("o") & "' ‡ ") 
    If Not (_MonthOccurred = Nothing) Then pValue.Append("MonthOccurred='" & _MonthOccurred.ToString("o") & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TimeOccurred.ToShortDateString & " " & _TimeOccurred.ToShortTimeString)}""") 
    pCSV.Append("," & _FaultNumber.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SystemName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CallingApplication)}""") 
    pCSV.Append("," & _AffectedUserID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_AffectedUserText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CallingApplicationVersion)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CallingFunctionWithinApplication)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FreeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultingAssembly)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AssemblyEntryPoint)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultingClass)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultingFunction)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultingFunctionParameters)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultIdent)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultDescription)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MessageSentToUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ActionSentToUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_FaultTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FaultSeverity.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_FaultSeverityText)}""") 
    pCSV.Append("," & _LoggedLoginID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LoggedLoginText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Thread)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeCode)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeText)}""") 
    pCSV.Append("," & _UserIdentityTypeNameCode.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserIdentityTypeNameText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DateOccurred.ToShortDateString & " " & _DateOccurred.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MonthOccurred.ToShortDateString & " " & _MonthOccurred.ToShortTimeString)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
    _WithParents = clsEnums.enmLoadParent.DoNotLoad 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent) 
    MyBase.New()
    CreateEmpty()
    _WithParents = vWithParents 
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vWithParents As clsEnums.enmLoadParent, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    _WithParents = vWithParents 
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsLoggedAlert As csLoggedAlert)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsLoggedAlert) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vTimeOccurred As Date = Nothing _ 
    , Optional vFaultNumber As Integer = 0 _ 
    , Optional vSystemName As String = "" _ 
    , Optional vCallingApplication As String = "" _ 
    , Optional vAffectedUserID As Long = 0 _ 
    , Optional vAffectedUserText As String = "" _ 
    , Optional vCallingApplicationVersion As String = "" _ 
    , Optional vCallingFunctionWithinApplication As String = "" _ 
    , Optional vFreeText As String = "" _ 
    , Optional vFaultingAssembly As String = "" _ 
    , Optional vAssemblyEntryPoint As String = "" _ 
    , Optional vFaultingClass As String = "" _ 
    , Optional vFaultingFunction As String = "" _ 
    , Optional vFaultingFunctionParameters As String = "" _ 
    , Optional vFaultIdent As String = "" _ 
    , Optional vFaultDescription As String = "" _ 
    , Optional vMessageSentToUser As String = "" _ 
    , Optional vActionSentToUser As String = "" _ 
    , Optional vFaultType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD _ 
    , Optional vFaultTypeText As String = "" _ 
    , Optional vFaultSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD _ 
    , Optional vFaultSeverityText As String = "" _ 
    , Optional vLoggedLoginID As Long = 0 _ 
    , Optional vLoggedLoginText As String = "" _ 
    , Optional vThread As String = "" _ 
    , Optional vUserIdentityTypeCode As String = "" _ 
    , Optional vUserIdentityTypeText As String = "" _ 
    , Optional vUserIdentityTypeNameCode As Integer = -1 _ 
    , Optional vUserIdentityTypeNameText As String = "" _ 
    , Optional vDateOccurred As Date = Nothing _ 
    , Optional vMonthOccurred As Date = Nothing _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _TimeOccurred = vTimeOccurred 
    _FaultNumber = vFaultNumber 
    _SystemName = vSystemName 
    _CallingApplication = vCallingApplication 
    _AffectedUserID = vAffectedUserID 
    _AffectedUserText = vAffectedUserText 
    _CallingApplicationVersion = vCallingApplicationVersion 
    _CallingFunctionWithinApplication = vCallingFunctionWithinApplication 
    _FreeText = vFreeText 
    _FaultingAssembly = vFaultingAssembly 
    _AssemblyEntryPoint = vAssemblyEntryPoint 
    _FaultingClass = vFaultingClass 
    _FaultingFunction = vFaultingFunction 
    _FaultingFunctionParameters = vFaultingFunctionParameters 
    _FaultIdent = vFaultIdent 
    _FaultDescription = vFaultDescription 
    _MessageSentToUser = vMessageSentToUser 
    _ActionSentToUser = vActionSentToUser 
    _FaultType = vFaultType 
    _FaultTypeText = vFaultTypeText 
    _FaultSeverity = vFaultSeverity 
    _FaultSeverityText = vFaultSeverityText 
    _LoggedLoginID = vLoggedLoginID 
    _LoggedLoginText = vLoggedLoginText 
    _Thread = vThread 
    _UserIdentityTypeCode = vUserIdentityTypeCode 
    _UserIdentityTypeText = vUserIdentityTypeText 
    _UserIdentityTypeNameCode = vUserIdentityTypeNameCode 
    _UserIdentityTypeNameText = vUserIdentityTypeNameText 
    _DateOccurred = vDateOccurred 
    _MonthOccurred = vMonthOccurred 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester, Optional ByVal vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
    _WithParents = vWithParents 
 
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
    _WithParents = vWithParents 
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
 
    _SystemName = _SystemName.Truncate(pTruncateLength, _IsTruncated) 
    _CallingApplication = _CallingApplication.Truncate(pTruncateLength, _IsTruncated) 
    _CallingApplicationVersion = _CallingApplicationVersion.Truncate(pTruncateLength, _IsTruncated) 
    _CallingFunctionWithinApplication = _CallingFunctionWithinApplication.Truncate(pTruncateLength, _IsTruncated) 
    _FreeText = _FreeText.Truncate(pTruncateLength, _IsTruncated) 
    _FaultingAssembly = _FaultingAssembly.Truncate(pTruncateLength, _IsTruncated) 
    _AssemblyEntryPoint = _AssemblyEntryPoint.Truncate(pTruncateLength, _IsTruncated) 
    _FaultingClass = _FaultingClass.Truncate(pTruncateLength, _IsTruncated) 
    _FaultingFunction = _FaultingFunction.Truncate(pTruncateLength, _IsTruncated) 
    _FaultingFunctionParameters = _FaultingFunctionParameters.Truncate(pTruncateLength, _IsTruncated) 
    _FaultIdent = _FaultIdent.Truncate(pTruncateLength, _IsTruncated) 
    _FaultDescription = _FaultDescription.Truncate(pTruncateLength, _IsTruncated) 
    _MessageSentToUser = _MessageSentToUser.Truncate(pTruncateLength, _IsTruncated) 
    _ActionSentToUser = _ActionSentToUser.Truncate(pTruncateLength, _IsTruncated) 
    _Thread = _Thread.Truncate(pTruncateLength, _IsTruncated) 
    _UserIdentityTypeCode = _UserIdentityTypeCode.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _SystemName = ccHelper.RemoveChrW0(_SystemName) 
    _CallingApplication = ccHelper.RemoveChrW0(_CallingApplication) 
    _CallingApplicationVersion = ccHelper.RemoveChrW0(_CallingApplicationVersion) 
    _CallingFunctionWithinApplication = ccHelper.RemoveChrW0(_CallingFunctionWithinApplication) 
    _FreeText = ccHelper.RemoveChrW0(_FreeText) 
    _FaultingAssembly = ccHelper.RemoveChrW0(_FaultingAssembly) 
    _AssemblyEntryPoint = ccHelper.RemoveChrW0(_AssemblyEntryPoint) 
    _FaultingClass = ccHelper.RemoveChrW0(_FaultingClass) 
    _FaultingFunction = ccHelper.RemoveChrW0(_FaultingFunction) 
    _FaultingFunctionParameters = ccHelper.RemoveChrW0(_FaultingFunctionParameters) 
    _FaultIdent = ccHelper.RemoveChrW0(_FaultIdent) 
    _FaultDescription = ccHelper.RemoveChrW0(_FaultDescription) 
    _MessageSentToUser = ccHelper.RemoveChrW0(_MessageSentToUser) 
    _ActionSentToUser = ccHelper.RemoveChrW0(_ActionSentToUser) 
    _Thread = ccHelper.RemoveChrW0(_Thread) 
    _UserIdentityTypeCode = ccHelper.RemoveChrW0(_UserIdentityTypeCode) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedAlert by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlert_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedAlert-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedAlert by the chosen parameters. This function may be a bit slower than accessing the LoggedAlert's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlert_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedAlert-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedAlert-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the LoggedAlert by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlert_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"LoggedAlert not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-LoggedAlert-210927-1527", vRequester, vAdditionalMessageToUser:=$"LoggedAlert not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccLoggedAlertCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertGetByID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vID) 
        pLastReadVariableName = "WithParents" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"LoggedAlert not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-LoggedAlert-210625-0950", vRequester, vAdditionalMessageToUser:=$"LoggedAlert not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertUpdate, "csLoggedAlert_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedAlert-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertUpdate, "csLoggedAlert_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedAlert-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the LoggedAlert. If there are parents or children in the LoggedAlert, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Friend Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertUpdate, "csLoggedAlert_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pLoggedAlert As New csLoggedAlert(_WithParents) 
    If Me.isEqual(pLoggedAlert) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-LoggedAlert-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-LoggedAlert-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_LoggedAlertUpdate"
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
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pCachedLoggedAlert As csLoggedAlert 
      If _ID = 0 Then 
        pCachedLoggedAlert = New csLoggedAlert(_WithParents) 
        'get last ID 
        Dim pLoggedAlertCol As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.Clone() 
        If pLoggedAlertCol.Count = 0 Then 
          _ID = 1 
        Else 
          pLoggedAlertCol.SortByID() 
          Dim pLastID As Long = pLoggedAlertCol(pLoggedAlertCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccLoggedAlertCol.Add(pCachedLoggedAlert) 
      Else  
        pCachedLoggedAlert = MyController.DBCache.ccLoggedAlertCol.FindByID(_ID) 
      End If 
      pCachedLoggedAlert.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedAlertCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_TimeOccurred) 
        pLastReadVariableName = "FaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_FaultNumber) 
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_SystemName) 
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_CallingApplication) 
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_AffectedUserID, False) 
        pLastReadVariableName = "CallingApplicationVersion" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_CallingApplicationVersion) 
        pLastReadVariableName = "CallingFunctionWithinApplication" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_CallingFunctionWithinApplication) 
        pLastReadVariableName = "FreeText" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_FreeText) 
        pLastReadVariableName = "FaultingAssembly" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_FaultingAssembly) 
        pLastReadVariableName = "AssemblyEntryPoint" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_AssemblyEntryPoint) 
        pLastReadVariableName = "FaultingClass" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_FaultingClass) 
        pLastReadVariableName = "FaultingFunction" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_FaultingFunction) 
        pLastReadVariableName = "FaultingFunctionParameters" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_FaultingFunctionParameters) 
        pLastReadVariableName = "FaultIdent" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_FaultIdent) 
        pLastReadVariableName = "FaultDescription" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_FaultDescription) 
        pLastReadVariableName = "MessageSentToUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_MessageSentToUser) 
        pLastReadVariableName = "ActionSentToUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 200).Value = ccHelper.ObjectNullable(_ActionSentToUser) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_FaultType.FastToString()) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_FaultSeverity.FastToString()) 
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_LoggedLoginID, False) 
        pLastReadVariableName = "Thread" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_Thread) 
        pLastReadVariableName = "lkpUserIdentityType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.LookupNullable(_UserIdentityTypeCode) 
        pLastReadVariableName = "lkpUserIdentityTypeName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = ccHelper.LookupNullable(_UserIdentityTypeNameCode) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pAffectedUser As csUser = _AffectedUser 
      Dim pLoggedLogin As csLoggedLogin = _LoggedLogin 
      
      'Children 
      Dim pLoggedJobs As csLoggedJobCol = _LoggedJobs 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pAffectedUser Is Nothing Then _AffectedUser = pAffectedUser 
        If Not pLoggedLogin Is Nothing Then _LoggedLogin = pLoggedLogin 
      End If 
      
      'Children 
      If Not pLoggedJobs Is Nothing Then _LoggedJobs = pLoggedJobs 
      
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
    Dim pFunctionParameters As String = String.Format("LoggedAlert.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlert_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_LoggedAlertDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedAlertCol.Remove(MyController.DBCache.ccLoggedAlertCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedAlertCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlert_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedAlertCol.Remove(MyController.DBCache.ccLoggedAlertCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedAlertCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the LoggedAlert's LoggedJob collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedJobs(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlert_FillLoggedJobs", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _LoggedJobs = New csLoggedJobCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedJobs.FillByLoggedAlertID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
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
    If Not (TypeOf (vTargCCEntityToTest) Is csLoggedAlert) Then Return False 
    Dim pLoggedAlertToTest As csLoggedAlert = CType(vTargCCEntityToTest, csLoggedAlert) 
    Return isEqual(pLoggedAlertToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vLoggedAlertToTest As csLoggedAlert) As Boolean
    With vLoggedAlertToTest
      If _ID <> .ID Then Return False
      If _TimeOccurred <> Nothing AndAlso .TimeOccurred <> Nothing Then 
        If ccHelper.ToLong(_TimeOccurred.Subtract(.TimeOccurred).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_TimeOccurred = Nothing AndAlso .TimeOccurred = Nothing) Then 
        Return False 
      End If 
      If _FaultNumber <> .FaultNumber Then Return False
      If _SystemName <> .SystemName Then Return False
      If _CallingApplication <> .CallingApplication Then Return False
      If _AffectedUserID <> .AffectedUserID Then Return False
      If _CallingApplicationVersion <> .CallingApplicationVersion Then Return False
      If _CallingFunctionWithinApplication <> .CallingFunctionWithinApplication Then Return False
      If _FreeText <> .FreeText Then Return False
      If _FaultingAssembly <> .FaultingAssembly Then Return False
      If _AssemblyEntryPoint <> .AssemblyEntryPoint Then Return False
      If _FaultingClass <> .FaultingClass Then Return False
      If _FaultingFunction <> .FaultingFunction Then Return False
      If _FaultingFunctionParameters <> .FaultingFunctionParameters Then Return False
      If _FaultIdent <> .FaultIdent Then Return False
      If _FaultDescription <> .FaultDescription Then Return False
      If _MessageSentToUser <> .MessageSentToUser Then Return False
      If _ActionSentToUser <> .ActionSentToUser Then Return False
      If _FaultType <> .FaultType Then Return False
      If _FaultSeverity <> .FaultSeverity Then Return False
      If _LoggedLoginID <> .LoggedLoginID Then Return False
      If _Thread <> .Thread Then Return False
      If _UserIdentityTypeCode <> .UserIdentityTypeCode Then Return False
      If _UserIdentityTypeNameCode <> .UserIdentityTypeNameCode Then Return False
      If _DateOccurred <> Nothing AndAlso .DateOccurred <> Nothing Then 
        If ccHelper.ToLong(_DateOccurred.Subtract(.DateOccurred).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DateOccurred = Nothing AndAlso .DateOccurred = Nothing) Then 
        Return False 
      End If 
      If _MonthOccurred <> Nothing AndAlso .MonthOccurred <> Nothing Then 
        If ccHelper.ToLong(_MonthOccurred.Subtract(.MonthOccurred).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_MonthOccurred = Nothing AndAlso .MonthOccurred = Nothing) Then 
        Return False 
      End If 
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
    Dim pClone As New csLoggedAlert(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedAlert
    Dim pClone As New csLoggedAlert(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("TimeOccurred") = _TimeOccurred : Catch ex As Exception : Return pFault.LogException(ex, "TimeOccurred", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultNumber") = _FaultNumber : Catch ex As Exception : Return pFault.LogException(ex, "FaultNumber", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("SystemName") = _SystemName : Catch ex As Exception : Return pFault.LogException(ex, "SystemName", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("CallingApplication") = _CallingApplication : Catch ex As Exception : Return pFault.LogException(ex, "CallingApplication", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("AffectedUserID") = _AffectedUserID : Catch ex As Exception : Return pFault.LogException(ex, "AffectedUserID", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("CallingApplicationVersion") = _CallingApplicationVersion : Catch ex As Exception : Return pFault.LogException(ex, "CallingApplicationVersion", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("CallingFunctionWithinApplication") = _CallingFunctionWithinApplication : Catch ex As Exception : Return pFault.LogException(ex, "CallingFunctionWithinApplication", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FreeText") = _FreeText : Catch ex As Exception : Return pFault.LogException(ex, "FreeText", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultingAssembly") = _FaultingAssembly : Catch ex As Exception : Return pFault.LogException(ex, "FaultingAssembly", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("AssemblyEntryPoint") = _AssemblyEntryPoint : Catch ex As Exception : Return pFault.LogException(ex, "AssemblyEntryPoint", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultingClass") = _FaultingClass : Catch ex As Exception : Return pFault.LogException(ex, "FaultingClass", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultingFunction") = _FaultingFunction : Catch ex As Exception : Return pFault.LogException(ex, "FaultingFunction", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultingFunctionParameters") = _FaultingFunctionParameters : Catch ex As Exception : Return pFault.LogException(ex, "FaultingFunctionParameters", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultIdent") = _FaultIdent : Catch ex As Exception : Return pFault.LogException(ex, "FaultIdent", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultDescription") = _FaultDescription : Catch ex As Exception : Return pFault.LogException(ex, "FaultDescription", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("MessageSentToUser") = _MessageSentToUser : Catch ex As Exception : Return pFault.LogException(ex, "MessageSentToUser", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActionSentToUser") = _ActionSentToUser : Catch ex As Exception : Return pFault.LogException(ex, "ActionSentToUser", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultType") = _FaultType : Catch ex As Exception : Return pFault.LogException(ex, "FaultType", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("FaultSeverity") = _FaultSeverity : Catch ex As Exception : Return pFault.LogException(ex, "FaultSeverity", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoggedLoginID") = _LoggedLoginID : Catch ex As Exception : Return pFault.LogException(ex, "LoggedLoginID", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("Thread") = _Thread : Catch ex As Exception : Return pFault.LogException(ex, "Thread", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserIdentityTypeCode") = _UserIdentityTypeCode : Catch ex As Exception : Return pFault.LogException(ex, "UserIdentityTypeCode", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserIdentityTypeNameCode") = _UserIdentityTypeNameCode : Catch ex As Exception : Return pFault.LogException(ex, "UserIdentityTypeNameCode", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("DateOccurred") = _DateOccurred : Catch ex As Exception : Return pFault.LogException(ex, "DateOccurred", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
    Try : vDataRow("MonthOccurred") = _MonthOccurred : Catch ex As Exception : Return pFault.LogException(ex, "MonthOccurred", "TRGT-LoggedAlert-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLoggedAlert As csLoggedAlert = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedAlert) 
      AssignValues(pLoggedAlert) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-LoggedAlert-130515-1230", vRequester) 
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
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'TimeOccurred 
          pBinaryWriter.Write(_TimeOccurred.Ticks) 
          'FaultNumber 
          pBinaryWriter.Write(_FaultNumber) 
          'SystemName 
          If _SystemName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SystemName) 
          'CallingApplication 
          If _CallingApplication Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CallingApplication) 
          'AffectedUserID 
          pBinaryWriter.Write(_AffectedUserID) 
          'AffectedUser 
          If _AffectedUser IsNot Nothing Then 
            pObjectBytes = _AffectedUser.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _AffectedUserText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AffectedUserText) 
          'CallingApplicationVersion 
          If _CallingApplicationVersion Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CallingApplicationVersion) 
          'CallingFunctionWithinApplication 
          If _CallingFunctionWithinApplication Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CallingFunctionWithinApplication) 
          'FreeText 
          If _FreeText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FreeText) 
          'FaultingAssembly 
          If _FaultingAssembly Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultingAssembly) 
          'AssemblyEntryPoint 
          If _AssemblyEntryPoint Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_AssemblyEntryPoint) 
          'FaultingClass 
          If _FaultingClass Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultingClass) 
          'FaultingFunction 
          If _FaultingFunction Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultingFunction) 
          'FaultingFunctionParameters 
          If _FaultingFunctionParameters Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultingFunctionParameters) 
          'FaultIdent 
          If _FaultIdent Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultIdent) 
          'FaultDescription 
          If _FaultDescription Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FaultDescription) 
          'MessageSentToUser 
          If _MessageSentToUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_MessageSentToUser) 
          'ActionSentToUser 
          If _ActionSentToUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ActionSentToUser) 
          'FaultType 
          pBinaryWriter.Write(_FaultType.FastToString()) 
          'FaultSeverity 
          pBinaryWriter.Write(_FaultSeverity.FastToString()) 
          'LoggedLoginID 
          pBinaryWriter.Write(_LoggedLoginID) 
          'LoggedLogin 
          If _LoggedLogin IsNot Nothing Then 
            pObjectBytes = _LoggedLogin.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _LoggedLoginText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LoggedLoginText) 
          'Thread 
          If _Thread Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Thread) 
          'UserIdentityTypeCode 
          If _UserIdentityTypeCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserIdentityTypeCode) 
          pBinaryWriter.Write(_UserIdentityTypeText) 
          'UserIdentityTypeNameCode 
          pBinaryWriter.Write(_UserIdentityTypeNameCode) 
          pBinaryWriter.Write(_UserIdentityTypeNameText) 
          'DateOccurred 
          pBinaryWriter.Write(_DateOccurred.Ticks) 
          'MonthOccurred 
          pBinaryWriter.Write(_MonthOccurred.Ticks) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'LoggedJobs  
          If _LoggedJobs IsNot Nothing Then 
            pObjectBytes = _LoggedJobs.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-150307-2338", vRequester) 
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
          _WithParents = clsEnums.TranslateEnmLoadParent(pReader.ReadString) 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'TimeOccurred 
          _TimeOccurred = New Date(pReader.ReadInt64) 
          'FaultNumber 
          _FaultNumber = pReader.ReadInt32 
          'SystemName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SystemName = pReader.ReadString 
          'CallingApplication 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CallingApplication = pReader.ReadString 
          'AffectedUserID 
          _AffectedUserID = pReader.ReadInt64 
          'AffectedUser 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _AffectedUser = New csUser(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AffectedUserText = pReader.ReadString 
          'CallingApplicationVersion 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CallingApplicationVersion = pReader.ReadString 
          'CallingFunctionWithinApplication 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CallingFunctionWithinApplication = pReader.ReadString 
          'FreeText 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FreeText = pReader.ReadString 
          'FaultingAssembly 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultingAssembly = pReader.ReadString 
          'AssemblyEntryPoint 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _AssemblyEntryPoint = pReader.ReadString 
          'FaultingClass 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultingClass = pReader.ReadString 
          'FaultingFunction 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultingFunction = pReader.ReadString 
          'FaultingFunctionParameters 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultingFunctionParameters = pReader.ReadString 
          'FaultIdent 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultIdent = pReader.ReadString 
          'FaultDescription 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FaultDescription = pReader.ReadString 
          'MessageSentToUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _MessageSentToUser = pReader.ReadString 
          'ActionSentToUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ActionSentToUser = pReader.ReadString 
          'FaultType 
          _FaultType = clsEnums.TranslateEnmFaultType(pReader.ReadString) 
          'FaultSeverity 
          _FaultSeverity = clsEnums.TranslateEnmFaultSeverity(pReader.ReadString) 
          'LoggedLoginID 
          _LoggedLoginID = pReader.ReadInt64 
          'LoggedLogin 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedLogin = New csLoggedLogin(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LoggedLoginText = pReader.ReadString 
          'Thread 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Thread = pReader.ReadString 
          'UserIdentityTypeCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserIdentityTypeCode = pReader.ReadString 
          _UserIdentityTypeText = pReader.ReadString 
          'UserIdentityTypeNameCode 
          _UserIdentityTypeNameCode = pReader.ReadInt32 
          _UserIdentityTypeNameText = pReader.ReadString 
          'DateOccurred 
          _DateOccurred = New Date(pReader.ReadInt64) 
          'MonthOccurred 
          _MonthOccurred = New Date(pReader.ReadInt64) 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'LoggedJobs 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedJobs = New csLoggedJobCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-LoggedAlert-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-190720-1443", vRequester) 
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
 
      Dim pLoggedAlert As csLoggedAlert = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csLoggedAlert)(vJSON, pSettings) 
      AssignValues(pLoggedAlert) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vLoggedAlert As csLoggedAlert)
    With vLoggedAlert
      _ID = .ID 
      _TimeOccurred = .TimeOccurred 
      _FaultNumber = .FaultNumber 
      _SystemName = .SystemName 
      _CallingApplication = .CallingApplication 
      _AffectedUserID = .AffectedUserID 
      If .AffectedUser IsNot Nothing Then 
        _AffectedUser = .AffectedUser.Clone() 
      End If 
      _AffectedUserText = .AffectedUserText 
      _CallingApplicationVersion = .CallingApplicationVersion 
      _CallingFunctionWithinApplication = .CallingFunctionWithinApplication 
      _FreeText = .FreeText 
      _FaultingAssembly = .FaultingAssembly 
      _AssemblyEntryPoint = .AssemblyEntryPoint 
      _FaultingClass = .FaultingClass 
      _FaultingFunction = .FaultingFunction 
      _FaultingFunctionParameters = .FaultingFunctionParameters 
      _FaultIdent = .FaultIdent 
      _FaultDescription = .FaultDescription 
      _MessageSentToUser = .MessageSentToUser 
      _ActionSentToUser = .ActionSentToUser 
      _FaultType = .FaultType 
      _FaultTypeText = .FaultTypeText
      _FaultSeverity = .FaultSeverity 
      _FaultSeverityText = .FaultSeverityText
      _LoggedLoginID = .LoggedLoginID 
      If .LoggedLogin IsNot Nothing Then 
        _LoggedLogin = .LoggedLogin.Clone() 
      End If 
      _LoggedLoginText = .LoggedLoginText 
      _Thread = .Thread 
      _UserIdentityTypeCode = .UserIdentityTypeCode 
      _UserIdentityTypeText = .UserIdentityTypeText 
      _UserIdentityTypeNameCode = .UserIdentityTypeNameCode 
      _UserIdentityTypeNameText = .UserIdentityTypeNameText 
      _DateOccurred = .DateOccurred 
      _MonthOccurred = .MonthOccurred 
      _Tag = .Tag 
      _WithParents = .WithParents 
      _WithParents = .WithParents 
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
      'FaultType 
      pTextToGet = "FaultTypeText (Enum)" 
      _FaultTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.FaultType, _FaultType.FastToString(), vRequester) 
      'FaultSeverity 
      pTextToGet = "FaultSeverityText (Enum)" 
      _FaultSeverityText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.FaultSeverity, _FaultSeverity.FastToString(), vRequester) 
      'UserIdentityType 
      pTextToGet = "UserIdentityTypeText (Lookup)" 
      _UserIdentityTypeText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.UserIdentityType, _UserIdentityTypeCode, vRequester) 
      'UserIdentityTypeName 
      pTextToGet = "UserIdentityTypeNameText (Lookup)" 
      _UserIdentityTypeNameText = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UserIdentityType, _UserIdentityTypeCode.ToString(), clsEnums.enmLookup.UserIdentityTypeName, _UserIdentityTypeNameCode, vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-LoggedAlert-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' This loads the dependant Parents
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault
    
    'Foreign Parent
    If _AffectedUserID > 0 Then
      _AffectedUser = New csUser()
      pFault = _AffectedUser.GetByID(_AffectedUserID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _AffectedUserText = _AffectedUser.DefaultDesignation 
    End If
    If _LoggedLoginID > 0 Then
      _LoggedLogin = New csLoggedLogin()
      pFault = _LoggedLogin.GetByID(_LoggedLoginID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _LoggedLoginText = _LoggedLogin.DefaultDesignation 
    End If
    _WithParents = clsEnums.enmLoadParent.EntireObject 
    
    pFault.SetOK()
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
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
      pLastReadVariableName = "TimeOccurred" 
      If Not vReader.IsDBNull(1) Then _TimeOccurred = vReader.GetDateTime(1)
      pLastReadVariableName = "FaultNumber" 
      If Not vReader.IsDBNull(2) Then _FaultNumber = vReader.GetInt32(2)
      pLastReadVariableName = "SystemName" 
      If Not vReader.IsDBNull(3) Then _SystemName = vReader.GetString(3) 
      pLastReadVariableName = "CallingApplication" 
      If Not vReader.IsDBNull(4) Then _CallingApplication = vReader.GetString(4) 
      pLastReadVariableName = "AffectedUserID" 
      If Not vReader.IsDBNull(5) Then _AffectedUserID = vReader.GetInt64(5)
      pLastReadVariableName = "CallingApplicationVersion" 
      If Not vReader.IsDBNull(6) Then _CallingApplicationVersion = vReader.GetString(6) 
      pLastReadVariableName = "CallingFunctionWithinApplication" 
      If Not vReader.IsDBNull(7) Then _CallingFunctionWithinApplication = vReader.GetString(7) 
      pLastReadVariableName = "FreeText" 
      If Not vReader.IsDBNull(8) Then _FreeText = vReader.GetString(8) 
      pLastReadVariableName = "FaultingAssembly" 
      If Not vReader.IsDBNull(9) Then _FaultingAssembly = vReader.GetString(9) 
      pLastReadVariableName = "AssemblyEntryPoint" 
      If Not vReader.IsDBNull(10) Then _AssemblyEntryPoint = vReader.GetString(10) 
      pLastReadVariableName = "FaultingClass" 
      If Not vReader.IsDBNull(11) Then _FaultingClass = vReader.GetString(11) 
      pLastReadVariableName = "FaultingFunction" 
      If Not vReader.IsDBNull(12) Then _FaultingFunction = vReader.GetString(12) 
      pLastReadVariableName = "FaultingFunctionParameters" 
      If Not vReader.IsDBNull(13) Then _FaultingFunctionParameters = vReader.GetString(13) 
      pLastReadVariableName = "FaultIdent" 
      If Not vReader.IsDBNull(14) Then _FaultIdent = vReader.GetString(14) 
      pLastReadVariableName = "FaultDescription" 
      If Not vReader.IsDBNull(15) Then _FaultDescription = vReader.GetString(15) 
      pLastReadVariableName = "MessageSentToUser" 
      If Not vReader.IsDBNull(16) Then _MessageSentToUser = vReader.GetString(16) 
      pLastReadVariableName = "ActionSentToUser" 
      If Not vReader.IsDBNull(17) Then _ActionSentToUser = vReader.GetString(17) 
      pLastReadVariableName = "enmFaultType_FaultType" 
      If Not vReader.IsDBNull(18) Then _FaultType = clsEnums.TranslateEnmFaultType(vReader.GetString(18))
      pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
      If Not vReader.IsDBNull(19) Then _FaultSeverity = clsEnums.TranslateEnmFaultSeverity(vReader.GetString(19))
      pLastReadVariableName = "c_LoggedLoginID" 
      If Not vReader.IsDBNull(20) Then _LoggedLoginID = vReader.GetInt64(20)
      pLastReadVariableName = "Thread" 
      If Not vReader.IsDBNull(21) Then _Thread = vReader.GetString(21) 
      pLastReadVariableName = "lkpUserIdentityType" 
      If Not vReader.IsDBNull(22) Then _UserIdentityTypeCode = vReader.GetString(22)
      pLastReadVariableName = "lkpUserIdentityTypeName" 
      If Not vReader.IsDBNull(23) Then _UserIdentityTypeNameCode = vReader.GetInt32(23)
      pLastReadVariableName = "clc_DateOccurred" 
      If Not vReader.IsDBNull(24) Then _DateOccurred = vReader.GetDateTime(24)
      pLastReadVariableName = "clc_MonthOccurred" 
      If Not vReader.IsDBNull(25) Then _MonthOccurred = vReader.GetDateTime(25)
      bDateAdded = _TimeOccurred 
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "AffectedUserText" 
        If Not vReader.IsDBNull(26) Then _AffectedUserText = vReader.GetString(26) 
        pLastReadVariableName = "LoggedLoginText" 
        If Not vReader.IsDBNull(27) Then _LoggedLoginText = vReader.GetString(27) 
      ElseIf _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        'vReader.Close() ' in case we are in a transaction - can't open 2 readers 
        pFault = LoadParents(vRequester) : If pFault.isOK = False Then Return pFault 
      End If
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedAlert As csLoggedAlert, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedLoggedAlert) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _AffectedUserText = "."
        _LoggedLoginText = "."
        _WithParents = clsEnums.enmLoadParent.DoNotLoad 
      ElseIf pWithParents = clsEnums.enmLoadParent.TextOnly Then 
        'cache is loaded with TextOnly 
        _WithParents = clsEnums.enmLoadParent.TextOnly 
      ElseIf pWithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) : If pFault.isOK = False Then Return pFault 
      End If 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _TimeOccurred = Nothing
    _FaultNumber = 0
    _SystemName = ""
    _CallingApplication = ""
    _AffectedUserID = 0
    _AffectedUser = Nothing
    _AffectedUserText = "."
    _CallingApplicationVersion = ""
    _CallingFunctionWithinApplication = ""
    _FreeText = ""
    _FaultingAssembly = ""
    _AssemblyEntryPoint = ""
    _FaultingClass = ""
    _FaultingFunction = ""
    _FaultingFunctionParameters = ""
    _FaultIdent = ""
    _FaultDescription = ""
    _MessageSentToUser = ""
    _ActionSentToUser = ""
    _FaultType = clsEnums.enmFaultType.UD
    _FaultTypeText = ""
    _FaultSeverity = clsEnums.enmFaultSeverity.UD
    _FaultSeverityText = ""
    _LoggedLoginID = 0
    _LoggedLogin = Nothing
    _LoggedLoginText = "."
    _Thread = ""
    _UserIdentityTypeCode = ""
    _UserIdentityTypeText = ""
    _UserIdentityTypeNameCode = -1
    _UserIdentityTypeNameText = ""
    _DateOccurred = Nothing
    _MonthOccurred = Nothing
    _Tag = ""
    _LoggedJobs = Nothing
    _IsCleanForXML = False 
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _WithParents = clsEnums.enmLoadParent.UD 
      bHasParents = True 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
 
  Private Sub csLoggedAlert_evtBeforeUpdateWithRequester(vWhichColumn As enmUpdateType, ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, vRequester As clsRequester, ByRef rFault As clsFault) Handles Me.evtBeforeUpdateWithRequester 
    If vWhichColumn = enmUpdateType.Standard Then 
      If _AssemblyEntryPoint.Length > 100 Then _AssemblyEntryPoint = _AssemblyEntryPoint.Substring(0, 100) 
    '  Dim pSize As Integer = 6000 
    '  If _FreeText.Length > pSize Then 
    '    Dim pName As String = DateTime.Now.ToString("HHmmssffff") 
    '    Tools.LogToTextFile.WriteMessage(_FreeText, $"CC_LoggedAlert_{pName}_Freetext") 
    '    _FreeText = $"See text file '...CC_LoggedAlert_{pName}_Freetext...' on server for full text{Environment.NewLine}{_FreeText.Substring(0, pSize - 150)}.... " 
    '  End If 
    '  If _FaultingFunctionParameters.Length > pSize Then 
    '    Dim pName As String = DateTime.Now.ToString("HHmmssffff") 
    '    Tools.LogToTextFile.WriteMessage(_FaultingFunctionParameters, $"CC_LoggedAlert_{pName}_FaultingFunctionParameters") 
    '    _FaultingFunctionParameters = $"See text file '...CC_LoggedAlert_{pName}_FaultingFunctionParameters...' on server for full text{Environment.NewLine}{_FaultingFunctionParameters.Substring(0, pSize - 150)}.... " 
    '  End If 
    End If 
  End Sub 
  
End Class 
  
Public Class csLoggedAlertCol
  Inherits cTargCCCollection(Of csLoggedAlert)
  Implements ITargCCCollectionUpdateable 
  Implements ITargCCDataReaderUser 
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return True 
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLoggedAlert) 
  Private _RecreateDictionaryForFindByID As Boolean 
   
  Private _IsCleanForXML As Boolean 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
 
  Private _WithParents As clsEnums.enmLoadParent
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
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
 
    For Each pRow As csLoggedAlert In Me 
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
    pCSVTitle.Append(",""TimeOccurred""") 
    pCSVTitle.Append(",""FaultNumber""") 
    pCSVTitle.Append(",""SystemName""") 
    pCSVTitle.Append(",""CallingApplication""") 
    pCSVTitle.Append(",""AffectedUserID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""AffectedUser (Text)""") 
    pCSVTitle.Append(",""CallingApplicationVersion""") 
    pCSVTitle.Append(",""CallingFunctionWithinApplication""") 
    pCSVTitle.Append(",""FreeText""") 
    pCSVTitle.Append(",""FaultingAssembly""") 
    pCSVTitle.Append(",""AssemblyEntryPoint""") 
    pCSVTitle.Append(",""FaultingClass""") 
    pCSVTitle.Append(",""FaultingFunction""") 
    pCSVTitle.Append(",""FaultingFunctionParameters""") 
    pCSVTitle.Append(",""FaultIdent""") 
    pCSVTitle.Append(",""FaultDescription""") 
    pCSVTitle.Append(",""MessageSentToUser""") 
    pCSVTitle.Append(",""ActionSentToUser""") 
    pCSVTitle.Append(",""FaultType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""FaultType (Text)""") 
    pCSVTitle.Append(",""FaultSeverity" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""FaultSeverity (Text)""") 
    pCSVTitle.Append(",""LoggedLoginID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""LoggedLogin (Text)""") 
    pCSVTitle.Append(",""Thread""") 
    pCSVTitle.Append(",""UserIdentityTypeCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""UserIdentityType (Text)""") 
    pCSVTitle.Append(",""UserIdentityTypeNameCode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""UserIdentityTypeName (Text)""") 
    pCSVTitle.Append(",""DateOccurred""") 
    pCSVTitle.Append(",""MonthOccurred""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csLoggedAlert In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent) 
    MyBase.New()
    CreateEmpty() 
    _WithParents = vWithParents 
  End Sub
  
  Public Sub New(ByVal vWithParents As clsEnums.enmLoadParent, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    _WithParents = vWithParents 
    
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
 
  Public Overloads Sub Add(ByVal vLoggedAlert As csLoggedAlert) 
    SyncLock _CollectionLock 
      MyBase.Add(vLoggedAlert) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vLoggedAlert As csLoggedAlert) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vLoggedAlert) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vLoggedAlertCol As csLoggedAlertCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vLoggedAlertCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vLoggedAlert As csLoggedAlert) 
    SyncLock _CollectionLock 
      MyBase.Remove(vLoggedAlert) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csLoggedAlert) 
      
      For Each lLoggedAlert In Me 
        If lLoggedAlert.IsEmpty OrElse pTempDictionary.ContainsKey(lLoggedAlert.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lLoggedAlert.ID, lLoggedAlert) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLoggedAlert.ToString, "TRGT-LoggedAlert-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", LoggedAlert:" & lLoggedAlert.ToString() & ", TRGT-LoggedAlert-260111-154657") 'Send it up the line 
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
    _WithParents = vWithParents 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    Throw New Exception("Entity is not localizable") 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lLoggedAlert As csLoggedAlert In Me 
      lLoggedAlert.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lLoggedAlert As csLoggedAlert In Me 
      lLoggedAlert.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [CallingApplication] 
    [DateOccurred] 
    [FaultNumber] 
    [FaultSeverity] 
    [FaultType] 
    [FaultTypeAndFaultSeverity] 
    [LoggedLoginID] 
    [MonthOccurred] 
    [SystemName] 
    [TimeOccurred] 
    [TimeOccurredAndFaultTypeAndFaultSeverity] 
    [AffectedUserID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the LoggedAlerts by the chosen parameters. This function may be a bit slower than accessing the LoggedAlert's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.CallingApplication 
          pFault = FillByCallingApplication(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.DateOccurred 
          pFault = FillByDateOccurred(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FaultNumber 
          pFault = FillByFaultNumber(ccHelper.ToInteger(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FaultSeverity 
          pFault = FillByFaultSeverity(clsEnums.TranslateEnmFaultSeverity(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FaultType 
          pFault = FillByFaultType(clsEnums.TranslateEnmFaultType(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FaultTypeAndFaultSeverity 
          pFault = FillByFaultTypeAndFaultSeverity(clsEnums.TranslateEnmFaultType(CStr(vParameters(0))), clsEnums.TranslateEnmFaultSeverity(CStr(vParameters(1))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.LoggedLoginID 
          pFault = FillByLoggedLoginID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.MonthOccurred 
          pFault = FillByMonthOccurred(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.SystemName 
          pFault = FillBySystemName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TimeOccurred 
          pFault = FillByTimeOccurred(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TimeOccurredAndFaultTypeAndFaultSeverity 
          pFault = FillByTimeOccurredAndFaultTypeAndFaultSeverity(CDate(vParameters(0)), clsEnums.TranslateEnmFaultType(CStr(vParameters(1))), clsEnums.TranslateEnmFaultSeverity(CStr(vParameters(2))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.AffectedUserID 
          pFault = FillByAffectedUserID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedAlert-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedAlert-151223_1716", vRequester) 
    End Try 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByParameters", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.Clone() 
      pLoggedAlertsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedAlertsCached.Reverse() 
      If vHowMany > 0 AndAlso pLoggedAlertsCached.Count > vHowMany Then 
        Dim tmp As New csLoggedAlertCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedAlertsCached(i)) 
        Next 
        pLoggedAlertsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFill"
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CallingApplication, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByCallingApplication(ByVal vCallingApplication As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplication={0}", vCallingApplication)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByCallingApplication(vCallingApplication)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByCallingApplication" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplication) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByCallingApplication", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DateOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByDateOccurred(ByVal vDateOccurred As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DateOccurred={0}", vDateOccurred)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByDateOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByDateOccurred(vDateOccurred)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByDateOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_DateOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(vDateOccurred) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByDateOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FaultNumber, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFaultNumber(ByVal vFaultNumber As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultNumber={0}", vFaultNumber)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByFaultNumber(vFaultNumber)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByFaultNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "FaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vFaultNumber) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByFaultNumber", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FaultSeverity, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFaultSeverity(ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultSeverity={0}", vFaultSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByFaultSeverity(vFaultSeverity)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByFaultSeverity" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByFaultSeverity", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FaultType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFaultType(ByVal vFaultType As clsEnums.enmFaultType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultType={0}", vFaultType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByFaultType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByFaultType(vFaultType)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByFaultType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByFaultType", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FaultTypeAndFaultSeverity, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFaultTypeAndFaultSeverity(ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultType={0}, FaultSeverity={1}", vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByFaultTypeAndFaultSeverity(vFaultType, vFaultSeverity)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByFaultType&FaultSeverity" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType.FastToString()) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByFaultTypeAndFaultSeverity", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoggedLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLoggedLoginID(ByVal vLoggedLoginID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoggedLoginID={0}", vLoggedLoginID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByLoggedLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByLoggedLoginID(vLoggedLoginID)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByLoggedLoginID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vLoggedLoginID, False) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByLoggedLoginID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MonthOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByMonthOccurred(ByVal vMonthOccurred As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthOccurred={0}", vMonthOccurred)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByMonthOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByMonthOccurred(vMonthOccurred)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByMonthOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_MonthOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = ccHelper.DateNullable(vMonthOccurred) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByMonthOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific SystemName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillBySystemName(ByVal vSystemName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemName={0}", vSystemName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillBySystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneBySystemName(vSystemName)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillBySystemName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemName) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillBySystemName", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTimeOccurred(ByVal vTimeOccurred As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurred={0}", vTimeOccurred)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByTimeOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByTimeOccurred(vTimeOccurred)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByTimeOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vTimeOccurred) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByTimeOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeOccurredAndFaultTypeAndFaultSeverity, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurred As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurred={0}, FaultType={1}, FaultSeverity={2}", vTimeOccurred, vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByTimeOccurredAndFaultTypeAndFaultSeverity(vTimeOccurred, vFaultType, vFaultSeverity)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByTimeOccurred&FaultType&FaultSeverity" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vTimeOccurred) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType.FastToString()) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific AffectedUserID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByAffectedUserID(ByVal vAffectedUserID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("AffectedUserID={0}", vAffectedUserID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByAffectedUserID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByAffectedUserID(vAffectedUserID)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByAffectedUserID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vAffectedUserID, False) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByAffectedUserID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific CallingApplication, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedCallingApplication(ByVal vCallingApplicationFrom As String, ByVal vCallingApplicationTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplicationFrom={0}, CallingApplicationTo={1}", vCallingApplicationFrom, vCallingApplicationTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedCallingApplication(vCallingApplicationFrom, vCallingApplicationTo)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedCallingApplication" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "CallingApplicationFrom" 
        pDALParameters.Add("bndCallingApplicationFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplicationFrom) 
        pLastReadVariableName = "CallingApplicationTo" 
        pDALParameters.Add("bndCallingApplicationTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplicationTo) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedCallingApplication", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific DateOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedDateOccurred(ByVal vDateOccurredStart As Date, ByVal vDateOccurredEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DateOccurredStart={0}, DateOccurredEnd={1}", vDateOccurredStart, vDateOccurredEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedDateOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedDateOccurred(vDateOccurredStart, vDateOccurredEnd)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedDateOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_DateOccurredFrom" 
        pDALParameters.Add("bndclc_DateOccurredFrom", ccDAL.enmSQLDataType.Date).Value = (vDateOccurredStart) 
        pLastReadVariableName = "clc_DateOccurredTo" 
        pDALParameters.Add("bndclc_DateOccurredTo", ccDAL.enmSQLDataType.Date).Value = (vDateOccurredEnd) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedDateOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FaultNumber, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedFaultNumber(ByVal vFaultNumberFrom As Integer, ByVal vFaultNumberTo As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultNumberFrom={0}, FaultNumberTo={1}", vFaultNumberFrom, vFaultNumberTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedFaultNumber(vFaultNumberFrom, vFaultNumberTo)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedFaultNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "FaultNumberFrom" 
        pDALParameters.Add("bndFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vFaultNumberFrom) 
        pLastReadVariableName = "FaultNumberTo" 
        pDALParameters.Add("bndFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = (vFaultNumberTo) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedFaultNumber", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific MonthOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedMonthOccurred(ByVal vMonthOccurredStart As Date, ByVal vMonthOccurredEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthOccurredStart={0}, MonthOccurredEnd={1}", vMonthOccurredStart, vMonthOccurredEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedMonthOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedMonthOccurred(vMonthOccurredStart, vMonthOccurredEnd)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedMonthOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "clc_MonthOccurredFrom" 
        pDALParameters.Add("bndclc_MonthOccurredFrom", ccDAL.enmSQLDataType.Date).Value = (vMonthOccurredStart) 
        pLastReadVariableName = "clc_MonthOccurredTo" 
        pDALParameters.Add("bndclc_MonthOccurredTo", ccDAL.enmSQLDataType.Date).Value = (vMonthOccurredEnd) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedMonthOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific SystemName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedSystemName(ByVal vSystemNameFrom As String, ByVal vSystemNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemNameFrom={0}, SystemNameTo={1}", vSystemNameFrom, vSystemNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedSystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedSystemName(vSystemNameFrom, vSystemNameTo)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedSystemName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "SystemNameFrom" 
        pDALParameters.Add("bndSystemNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemNameFrom) 
        pLastReadVariableName = "SystemNameTo" 
        pDALParameters.Add("bndSystemNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemNameTo) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedSystemName", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeOccurred, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTimeOccurred(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurredStart={0}, TimeOccurredEnd={1}", vTimeOccurredStart, vTimeOccurredEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedTimeOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedTimeOccurred(vTimeOccurredStart, vTimeOccurredEnd)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedTimeOccurred" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredEnd) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedTimeOccurred", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TimeOccurredAndFaultTypeAndFaultSeverity, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurredStart={0}, TimeOccurredEnd={1}, FaultType={2}, FaultSeverity={3}", vTimeOccurredStart, vTimeOccurredEnd, vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByBoundedTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedAlertCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedAlertCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedAlertCol failed: " & pResponse) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.CloneByBoundedTimeOccurredAndFaultTypeAndFaultSeverity(vTimeOccurredStart, vTimeOccurredEnd, vFaultType, vFaultSeverity)
      pFault = LoadMeFromDBCache(pLoggedAlertsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByBoundedTimeOccurred&FaultType&FaultSeverity" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredEnd) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType.FastToString()) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity.FastToString()) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByBoundedTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded CallingApplication, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardCallingApplication(ByVal vCallingApplication As String, ByVal vCallingApplicationWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplication={0}, CallingApplicationWildcardType={1}", vCallingApplication, vCallingApplicationWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByWildCardCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CallingApplication 
    Dim pWCCallingApplication As String = "" 
    If vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCallingApplication = vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCallingApplication = "%" & vCallingApplication 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCallingApplication = "%" & vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCallingApplication.ToCharArray 
        pWCCallingApplication &= p & "%" 
      Next 
      pWCCallingApplication = "%" & pWCCallingApplication 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedAlert-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByWildCardCallingApplication" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldCallingApplication" 
        pDALParameters.Add("wldCallingApplication", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCCallingApplication) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByWildCardCallingApplication", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded SystemName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardSystemName(ByVal vSystemName As String, ByVal vSystemNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemName={0}, SystemNameWildcardType={1}", vSystemName, vSystemNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByWildCardSystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'SystemName 
    Dim pWCSystemName As String = "" 
    If vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSystemName = vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSystemName = "%" & vSystemName 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSystemName = "%" & vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSystemName.ToCharArray 
        pWCSystemName &= p & "%" 
      Next 
      pWCSystemName = "%" & pWCSystemName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-LoggedAlert-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillByWildCardSystemName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldSystemName" 
        pDALParameters.Add("wldSystemName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCSystemName) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillByWildCardSystemName", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lLoggedAlert As New csLoggedAlert() 
      pFault = lLoggedAlert.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lLoggedAlert.IsEmpty Then Me.Add(lLoggedAlert) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
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
    TimeOccurredStart
    TimeOccurredEnd
    FaultNumberFrom
    FaultNumberTo
    [SystemName]
    SystemNameWildcardType
    [CallingApplication]
    CallingApplicationWildcardType
    [AffectedUserID]
    [FaultType]
    [FaultSeverity]
    [LoggedLoginID]
    DateOccurredStart
    DateOccurredEnd
    MonthOccurredStart
    MonthOccurredEnd
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pTimeOccurredStart As Nullable(Of Date) = Nothing
    Dim pTimeOccurredEnd As Nullable(Of Date) = Nothing
    Dim pFaultNumberFrom As Nullable(Of Integer) = Nothing
    Dim pFaultNumberTo As Nullable(Of Integer) = Nothing
    Dim pSystemName As String = Nothing
    Dim pSystemNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCallingApplication As String = Nothing
    Dim pCallingApplicationWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pAffectedUserID As Nullable(Of Long) = Nothing
    Dim pFaultType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD
    Dim pFaultSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing
    Dim pDateOccurredStart As Nullable(Of Date) = Nothing
    Dim pDateOccurredEnd As Nullable(Of Date) = Nothing
    Dim pMonthOccurredStart As Nullable(Of Date) = Nothing
    Dim pMonthOccurredEnd As Nullable(Of Date) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeOccurredStart) : If pObj IsNot Nothing Then pTimeOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeOccurredEnd) : If pObj IsNot Nothing Then pTimeOccurredEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultNumberFrom) : If pObj IsNot Nothing Then pFaultNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultNumberTo) : If pObj IsNot Nothing Then pFaultNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SystemName) Then pObj = vParameters(enmFillOnTheFlyParameters.SystemName) : If pObj IsNot Nothing Then pSystemName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SystemNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.SystemNameWildcardType) : If pObj IsNot Nothing Then pSystemNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CallingApplication) Then pObj = vParameters(enmFillOnTheFlyParameters.CallingApplication) : If pObj IsNot Nothing Then pCallingApplication = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CallingApplicationWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CallingApplicationWildcardType) : If pObj IsNot Nothing Then pCallingApplicationWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.AffectedUserID) Then pObj = vParameters(enmFillOnTheFlyParameters.AffectedUserID) : If pObj IsNot Nothing Then pAffectedUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultType) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultType) : If pObj IsNot Nothing Then pFaultType = CType(pObj, clsEnums.enmFaultType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultSeverity) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultSeverity) : If pObj IsNot Nothing Then pFaultSeverity = CType(pObj, clsEnums.enmFaultSeverity) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedLoginID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedLoginID) : If pObj IsNot Nothing Then pLoggedLoginID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.DateOccurredStart) : If pObj IsNot Nothing Then pDateOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.DateOccurredEnd) : If pObj IsNot Nothing Then pDateOccurredEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthOccurredStart) : If pObj IsNot Nothing Then pMonthOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthOccurredEnd) : If pObj IsNot Nothing Then pMonthOccurredEnd = CDate(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pTimeOccurredStart, pTimeOccurredEnd _
        , pFaultNumberFrom, pFaultNumberTo _
        , pSystemName, pSystemNameWildcardType _
        , pCallingApplication, pCallingApplicationWildcardType _
        , pAffectedUserID _
        , pFaultType _
        , pFaultSeverity _
        , pLoggedLoginID _
        , pDateOccurredStart, pDateOccurredEnd _
        , pMonthOccurredStart, pMonthOccurredEnd _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
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
        , ByVal vTimeOccurredStart As Nullable(Of Date), ByVal vTimeOccurredEnd As Nullable(Of Date) _
        , ByVal vFaultNumberFrom As Nullable(Of Integer), ByVal vFaultNumberTo As Nullable(Of Integer) _
        , ByVal vSystemName As String, ByVal vSystemNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vCallingApplication As String, ByVal vCallingApplicationWildcardType As clsEnums.enmWildCardType _
        , ByVal vAffectedUserID As Nullable(Of Long) _
        , ByVal vFaultType As clsEnums.enmFaultType _
        , ByVal vFaultSeverity As clsEnums.enmFaultSeverity _
        , ByVal vLoggedLoginID As Nullable(Of Long) _
        , ByVal vDateOccurredStart As Nullable(Of Date), ByVal vDateOccurredEnd As Nullable(Of Date) _
        , ByVal vMonthOccurredStart As Nullable(Of Date), ByVal vMonthOccurredEnd As Nullable(Of Date) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TimeOccurredStart={2}, TimeOccurredEnd={3}, FaultNumberFrom={4}, FaultNumberTo={5}, SystemName={6}, SystemNameWildcardType={7}, CallingApplication={8}, CallingApplicationWildcardType={9}, AffectedUserID={10}, FaultType={11}, FaultSeverity={12}, LoggedLoginID={13}, DateOccurredStart={14}, DateOccurredEnd={15}, MonthOccurredStart={16}, MonthOccurredEnd={17}", vIDFrom, vIDTo, vTimeOccurredStart, vTimeOccurredEnd, vFaultNumberFrom, vFaultNumberTo, vSystemName, vSystemNameWildcardType.FastToString(), vCallingApplication, vCallingApplicationWildcardType.FastToString(), vAffectedUserID, vFaultType, vFaultSeverity, vLoggedLoginID, vDateOccurredStart, vDateOccurredEnd, vMonthOccurredStart, vMonthOccurredEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'SystemName 
    Dim pWCSystemName As String = "" 
    If vSystemName = Nothing Then 
      pWCSystemName = vSystemName
    Else 
      If vSystemNameWildcardType = clsEnums.enmWildCardType.None OrElse vSystemNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCSystemName = vSystemName
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCSystemName = vSystemName & "%" 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCSystemName = "%" & vSystemName 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCSystemName = "%" & vSystemName & "%" 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vSystemName.ToCharArray 
          pWCSystemName &= p & "%" 
        Next 
        pWCSystemName = "%" & pWCSystemName 
      End If 
    End If 
    'CallingApplication 
    Dim pWCCallingApplication As String = "" 
    If vCallingApplication = Nothing Then 
      pWCCallingApplication = vCallingApplication
    Else 
      If vCallingApplicationWildcardType = clsEnums.enmWildCardType.None OrElse vCallingApplicationWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCCallingApplication = vCallingApplication
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
        pWCCallingApplication = vCallingApplication & "%" 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCCallingApplication = "%" & vCallingApplication 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCCallingApplication = "%" & vCallingApplication & "%" 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vCallingApplication.ToCharArray 
          pWCCallingApplication &= p & "%" 
        Next 
        pWCCallingApplication = "%" & pWCCallingApplication 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedAlert-121122-2008", vRequester) 
      Dim pLoggedAlertsCached As csLoggedAlertCol = MyController.DBCache.ccLoggedAlertCol.Clone() 
      Dim pLoggedAlertsToUse As New csLoggedAlertCol() 
      For Each l In pLoggedAlertsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vTimeOccurredStart.HasValue Then 
          If vTimeOccurredEnd.HasValue Then 
            If l.TimeOccurred < vTimeOccurredStart OrElse l.TimeOccurred > vTimeOccurredEnd.Value Then Continue For 
          Else 
            If l.TimeOccurred <> vTimeOccurredStart.Value Then Continue For 
          End If 
        End If 
        If vFaultNumberFrom.HasValue Then 
          If vFaultNumberTo.HasValue Then 
            If l.FaultNumber < vFaultNumberFrom OrElse l.FaultNumber > vFaultNumberTo.Value Then Continue For 
          Else 
            If l.FaultNumber <> vFaultNumberFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vSystemName) Then 
          If vSystemNameWildcardType = clsEnums.enmWildCardType.UD OrElse vSystemNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.SystemName.Equals(vSystemName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.SystemName.StartsWith(vSystemName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.SystemName.EndsWith(vSystemName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.SystemName.IndexOf(vSystemName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vCallingApplication) Then 
          If vCallingApplicationWildcardType = clsEnums.enmWildCardType.UD OrElse vCallingApplicationWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.CallingApplication.Equals(vCallingApplication, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.CallingApplication.StartsWith(vCallingApplication, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.CallingApplication.EndsWith(vCallingApplication, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.CallingApplication.IndexOf(vCallingApplication, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vAffectedUserID.HasValue Then 
          If l.AffectedUserID <> vAffectedUserID.Value Then Continue For 
        End If 
        If vFaultType <> clsEnums.enmFaultType.UD Then 
          If l.FaultType <> vFaultType Then Continue For 
        End If 
        If vFaultSeverity <> clsEnums.enmFaultSeverity.UD Then 
          If l.FaultSeverity <> vFaultSeverity Then Continue For 
        End If 
        If vLoggedLoginID.HasValue Then 
          If l.LoggedLoginID <> vLoggedLoginID.Value Then Continue For 
        End If 
        If vDateOccurredStart.HasValue Then 
          If vDateOccurredEnd.HasValue Then 
            If l.DateOccurred < vDateOccurredStart OrElse l.DateOccurred > vDateOccurredEnd.Value Then Continue For 
          Else 
            If l.DateOccurred <> vDateOccurredStart.Value Then Continue For 
          End If 
        End If 
        If vMonthOccurredStart.HasValue Then 
          If vMonthOccurredEnd.HasValue Then 
            If l.MonthOccurred < vMonthOccurredStart OrElse l.MonthOccurred > vMonthOccurredEnd.Value Then Continue For 
          Else 
            If l.MonthOccurred <> vMonthOccurredStart.Value Then Continue For 
          End If 
        End If 
        pLoggedAlertsToUse.Add(l) 
      Next 
      pLoggedAlertsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedAlertsToUse.Reverse() 
      If vHowMany > 0 AndAlso pLoggedAlertsToUse.Count > vHowMany Then 
        Dim tmp As New csLoggedAlertCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedAlertsToUse(i)) 
        Next 
        pLoggedAlertsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedAlertsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeOccurredEnd) 
        pLastReadVariableName = "FaultNumberFrom" 
        pDALParameters.Add("bndFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vFaultNumberFrom) 
        pLastReadVariableName = "FaultNumberTo" 
        pDALParameters.Add("bndFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vFaultNumberTo) 
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add("wldSystemName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCSystemName) 
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add("wldCallingApplication", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCCallingApplication) 
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vAffectedUserID) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vFaultType.FastToString()) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vFaultSeverity.FastToString()) 
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLoggedLoginID) 
        pLastReadVariableName = "clc_DateOccurredFrom" 
        pDALParameters.Add("bndclc_DateOccurredFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateOccurredStart) 
        pLastReadVariableName = "clc_DateOccurredTo" 
        pDALParameters.Add("bndclc_DateOccurredTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateOccurredEnd) 
        pLastReadVariableName = "clc_MonthOccurredFrom" 
        pDALParameters.Add("bndclc_MonthOccurredFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthOccurredStart) 
        pLastReadVariableName = "clc_MonthOccurredTo" 
        pDALParameters.Add("bndclc_MonthOccurredTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthOccurredEnd) 
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByTimeOccurred
    GroupByFaultNumber
    GroupBySystemName
    GroupByCallingApplication
    GroupByAffectedUserID
    GroupByFaultType
    GroupByFaultSeverity
    GroupByLoggedLoginID
    GroupByDateOccurred
    GroupByMonthOccurred
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pTimeOccurredStart As Nullable(Of Date) = Nothing
    Dim pTimeOccurredEnd As Nullable(Of Date) = Nothing
    Dim pFaultNumberFrom As Nullable(Of Integer) = Nothing
    Dim pFaultNumberTo As Nullable(Of Integer) = Nothing
    Dim pSystemName As String = Nothing
    Dim pSystemNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCallingApplication As String = Nothing
    Dim pCallingApplicationWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pAffectedUserID As Nullable(Of Long) = Nothing
    Dim pFaultType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD
    Dim pFaultSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD
    Dim pLoggedLoginID As Nullable(Of Long) = Nothing
    Dim pDateOccurredStart As Nullable(Of Date) = Nothing
    Dim pDateOccurredEnd As Nullable(Of Date) = Nothing
    Dim pMonthOccurredStart As Nullable(Of Date) = Nothing
    Dim pMonthOccurredEnd As Nullable(Of Date) = Nothing
    Dim pGroupByTimeOccurred As Boolean = False
    Dim pGroupByFaultNumber As Boolean = False
    Dim pGroupBySystemName As Boolean = False
    Dim pGroupByCallingApplication As Boolean = False
    Dim pGroupByAffectedUserID As Boolean = False
    Dim pGroupByFaultType As Boolean = False
    Dim pGroupByFaultSeverity As Boolean = False
    Dim pGroupByLoggedLoginID As Boolean = False
    Dim pGroupByDateOccurred As Boolean = False
    Dim pGroupByMonthOccurred As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeOccurredStart) : If pObj IsNot Nothing Then pTimeOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TimeOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.TimeOccurredEnd) : If pObj IsNot Nothing Then pTimeOccurredEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultNumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultNumberFrom) : If pObj IsNot Nothing Then pFaultNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultNumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultNumberTo) : If pObj IsNot Nothing Then pFaultNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SystemName) Then pObj = vParameters(enmFillOnTheFlyParameters.SystemName) : If pObj IsNot Nothing Then pSystemName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SystemNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.SystemNameWildcardType) : If pObj IsNot Nothing Then pSystemNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CallingApplication) Then pObj = vParameters(enmFillOnTheFlyParameters.CallingApplication) : If pObj IsNot Nothing Then pCallingApplication = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CallingApplicationWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CallingApplicationWildcardType) : If pObj IsNot Nothing Then pCallingApplicationWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.AffectedUserID) Then pObj = vParameters(enmFillOnTheFlyParameters.AffectedUserID) : If pObj IsNot Nothing Then pAffectedUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultType) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultType) : If pObj IsNot Nothing Then pFaultType = CType(pObj, clsEnums.enmFaultType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FaultSeverity) Then pObj = vParameters(enmFillOnTheFlyParameters.FaultSeverity) : If pObj IsNot Nothing Then pFaultSeverity = CType(pObj, clsEnums.enmFaultSeverity) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedLoginID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedLoginID) : If pObj IsNot Nothing Then pLoggedLoginID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.DateOccurredStart) : If pObj IsNot Nothing Then pDateOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DateOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.DateOccurredEnd) : If pObj IsNot Nothing Then pDateOccurredEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthOccurredStart) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthOccurredStart) : If pObj IsNot Nothing Then pMonthOccurredStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.MonthOccurredEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.MonthOccurredEnd) : If pObj IsNot Nothing Then pMonthOccurredEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByTimeOccurred) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByTimeOccurred) : If pObj IsNot Nothing Then pGroupByTimeOccurred = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFaultNumber) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFaultNumber) : If pObj IsNot Nothing Then pGroupByFaultNumber = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupBySystemName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupBySystemName) : If pObj IsNot Nothing Then pGroupBySystemName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCallingApplication) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCallingApplication) : If pObj IsNot Nothing Then pGroupByCallingApplication = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByAffectedUserID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByAffectedUserID) : If pObj IsNot Nothing Then pGroupByAffectedUserID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFaultType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFaultType) : If pObj IsNot Nothing Then pGroupByFaultType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFaultSeverity) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFaultSeverity) : If pObj IsNot Nothing Then pGroupByFaultSeverity = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLoggedLoginID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLoggedLoginID) : If pObj IsNot Nothing Then pGroupByLoggedLoginID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByDateOccurred) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByDateOccurred) : If pObj IsNot Nothing Then pGroupByDateOccurred = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByMonthOccurred) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByMonthOccurred) : If pObj IsNot Nothing Then pGroupByMonthOccurred = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pTimeOccurredStart, pTimeOccurredEnd _
        , pFaultNumberFrom, pFaultNumberTo _
        , pSystemName, pSystemNameWildcardType _
        , pCallingApplication, pCallingApplicationWildcardType _
        , pAffectedUserID _
        , pFaultType _
        , pFaultSeverity _
        , pLoggedLoginID _
        , pDateOccurredStart, pDateOccurredEnd _
        , pMonthOccurredStart, pMonthOccurredEnd _
        , pGroupByTimeOccurred _
        , pGroupByFaultNumber _
        , pGroupBySystemName _
        , pGroupByCallingApplication _
        , pGroupByAffectedUserID _
        , pGroupByFaultType _
        , pGroupByFaultSeverity _
        , pGroupByLoggedLoginID _
        , pGroupByDateOccurred _
        , pGroupByMonthOccurred _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a grouped collection on the fly for all indexed fields. For 'any', send 'Nothing' (no quotes)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function FillSumOnTheFly( _
          ByVal vIDFrom As Nullable(Of Long), ByVal vIDTo As Nullable(Of Long) _
        , ByVal vTimeOccurredStart As Nullable(Of Date), ByVal vTimeOccurredEnd As Nullable(Of Date) _
        , ByVal vFaultNumberFrom As Nullable(Of Integer), ByVal vFaultNumberTo As Nullable(Of Integer) _
        , ByVal vSystemName As String, ByVal vSystemNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vCallingApplication As String, ByVal vCallingApplicationWildcardType As clsEnums.enmWildCardType _
        , ByVal vAffectedUserID As Nullable(Of Long) _
        , ByVal vFaultType As clsEnums.enmFaultType _
        , ByVal vFaultSeverity As clsEnums.enmFaultSeverity _
        , ByVal vLoggedLoginID As Nullable(Of Long) _
        , ByVal vDateOccurredStart As Nullable(Of Date), ByVal vDateOccurredEnd As Nullable(Of Date) _
        , ByVal vMonthOccurredStart As Nullable(Of Date), ByVal vMonthOccurredEnd As Nullable(Of Date) _
        , ByVal vGroupByTimeOccurred As Boolean _
        , ByVal vGroupByFaultNumber As Boolean _
        , ByVal vGroupBySystemName As Boolean _
        , ByVal vGroupByCallingApplication As Boolean _
        , ByVal vGroupByAffectedUserID As Boolean _
        , ByVal vGroupByFaultType As Boolean _
        , ByVal vGroupByFaultSeverity As Boolean _
        , ByVal vGroupByLoggedLoginID As Boolean _
        , ByVal vGroupByDateOccurred As Boolean _
        , ByVal vGroupByMonthOccurred As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TimeOccurredStart={2}, TimeOccurredEnd={3}, FaultNumberFrom={4}, FaultNumberTo={5}, SystemName={6}, SystemNameWildcardType={7}, CallingApplication={8}, CallingApplicationWildcardType={9}, AffectedUserID={10}, FaultType={11}, FaultSeverity={12}, LoggedLoginID={13}, DateOccurredStart={14}, DateOccurredEnd={15}, MonthOccurredStart={16}, MonthOccurredEnd={17}, GroupByTimeOccurred={18}, GroupByFaultNumber={19}, GroupBySystemName={20}, GroupByCallingApplication={21}, GroupByAffectedUserID={22}, GroupByFaultType={23}, GroupByFaultSeverity={24}, GroupByLoggedLoginID={25}, GroupByDateOccurred={26}, GroupByMonthOccurred={27}", vIDFrom, vIDTo, vTimeOccurredStart, vTimeOccurredEnd, vFaultNumberFrom, vFaultNumberTo, vSystemName, vSystemNameWildcardType.FastToString(), vCallingApplication, vCallingApplicationWildcardType.FastToString(), vAffectedUserID, vFaultType, vFaultSeverity, vLoggedLoginID, vDateOccurredStart, vDateOccurredEnd, vMonthOccurredStart, vMonthOccurredEnd, vGroupByTimeOccurred, vGroupByFaultNumber, vGroupBySystemName, vGroupByCallingApplication, vGroupByAffectedUserID, vGroupByFaultType, vGroupByFaultSeverity, vGroupByLoggedLoginID, vGroupByDateOccurred, vGroupByMonthOccurred)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'SystemName 
    Dim pWCSystemName As String = "" 
    If vSystemName = Nothing Then 
      pWCSystemName = vSystemName
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.None OrElse vSystemNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCSystemName = vSystemName
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSystemName = vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSystemName = "%" & vSystemName 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSystemName = "%" & vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSystemName.ToCharArray 
        pWCSystemName &= p & "%" 
      Next 
      pWCSystemName = "%" & pWCSystemName 
    End If 
    'CallingApplication 
    Dim pWCCallingApplication As String = "" 
    If vCallingApplication = Nothing Then 
      pWCCallingApplication = vCallingApplication
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.None OrElse vCallingApplicationWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCCallingApplication = vCallingApplication
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCallingApplication = vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCallingApplication = "%" & vCallingApplication 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCallingApplication = "%" & vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCallingApplication.ToCharArray 
        pWCCallingApplication &= p & "%" 
      Next 
      pWCCallingApplication = "%" & pWCCallingApplication 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedAlert-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedAlertsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vTimeOccurredEnd) 
        pLastReadVariableName = "FaultNumberFrom" 
        pDALParameters.Add("bndFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vFaultNumberFrom) 
        pLastReadVariableName = "FaultNumberTo" 
        pDALParameters.Add("bndFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vFaultNumberTo) 
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add("wldSystemName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCSystemName) 
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add("wldCallingApplication", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCCallingApplication) 
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vAffectedUserID) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vFaultType) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vFaultSeverity) 
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLoggedLoginID) 
        pLastReadVariableName = "clc_DateOccurredFrom" 
        pDALParameters.Add("bndclc_DateOccurredFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateOccurredStart) 
        pLastReadVariableName = "clc_DateOccurredTo" 
        pDALParameters.Add("bndclc_DateOccurredTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vDateOccurredEnd) 
        pLastReadVariableName = "clc_MonthOccurredFrom" 
        pDALParameters.Add("bndclc_MonthOccurredFrom", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthOccurredStart) 
        pLastReadVariableName = "clc_MonthOccurredTo" 
        pDALParameters.Add("bndclc_MonthOccurredTo", ccDAL.enmSQLDataType.Date).Value = ccHelper.ObjectNullable(vMonthOccurredEnd) 
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add("GroupByTimeOccurred", ccDAL.enmSQLDataType.Bit).Value = vGroupByTimeOccurred
        pLastReadVariableName = "FaultNumber" 
        pDALParameters.Add("GroupByFaultNumber", ccDAL.enmSQLDataType.Bit).Value = vGroupByFaultNumber
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add("GroupBySystemName", ccDAL.enmSQLDataType.Bit).Value = vGroupBySystemName
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add("GroupByCallingApplication", ccDAL.enmSQLDataType.Bit).Value = vGroupByCallingApplication
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add("GroupByAffectedUserID", ccDAL.enmSQLDataType.Bit).Value = vGroupByAffectedUserID
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add("GroupByenmFaultType_FaultType", ccDAL.enmSQLDataType.Bit).Value = vGroupByFaultType
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add("GroupByenmFaultSeverity_FaultSeverity", ccDAL.enmSQLDataType.Bit).Value = vGroupByFaultSeverity
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add("GroupByc_LoggedLoginID", ccDAL.enmSQLDataType.Bit).Value = vGroupByLoggedLoginID
        pLastReadVariableName = "clc_DateOccurred" 
        pDALParameters.Add("GroupByclc_DateOccurred", ccDAL.enmSQLDataType.Bit).Value = vGroupByDateOccurred
        pLastReadVariableName = "clc_MonthOccurred" 
        pDALParameters.Add("GroupByclc_MonthOccurred", ccDAL.enmSQLDataType.Bit).Value = vGroupByMonthOccurred
        pLastReadVariableName = "WithParentText" 
        If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = True 
        Else 
          pDALParameters.Add("WithParentText", ccDAL.enmSQLDataType.Bit).Value = False 
        End If 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pLoggedAlerts As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedAlerts, "csLoggedAlertCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedAlerts IsNot Nothing AndAlso Me.Count <> pLoggedAlerts.Count Then FillFromListOfITargCCEntity(pLoggedAlerts) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vLoggedAlertArray As csLoggedAlert())
    Me.Clear()
    
    For Each pLoggedAlert As csLoggedAlert In vLoggedAlertArray
      Me.Add(pLoggedAlert)
      _Clean.Add(pLoggedAlert.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pLoggedAlert As New csLoggedAlert(pRow, vRequester, _WithParents) 
        Me.Add(pLoggedAlert) 
        _Clean.Add(pLoggedAlert.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-LoggedAlertCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-130515-1300", vRequester) 
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
      Dim pLoggedAlerts As csLoggedAlertCol = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedAlertCol) 
      For Each pLoggedAlert As csLoggedAlert In pLoggedAlerts 
        Me.Add(pLoggedAlert) 
        _Clean.Add(pLoggedAlert.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-LoggedAlert-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-190720-1443", vRequester) 
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
 
      Dim pLoggedAlerts As List(Of csLoggedAlert) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csLoggedAlert))(vJSON, pSettings) 
      For Each pLoggedAlert As csLoggedAlert In pLoggedAlerts 
        Me.Add(pLoggedAlert) 
        _Clean.Add(pLoggedAlert.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-190720-2059", vRequester) 
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
          'WithParents 
          pBinaryWriter.Write(_WithParents.ToString()) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lLoggedAlert As csLoggedAlert In Me 
            Dim pByte As Byte() = lLoggedAlert.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-150307-2340", vRequester) 
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
          'WithParents 
          _WithParents = clsEnums.TranslateEnmLoadParent(pReader.ReadString) 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pLoggedAlert As csLoggedAlert = New csLoggedAlert(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pLoggedAlert) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pLoggedAlert.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-LoggedAlert-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pLoggedAlert As csLoggedAlert In Me 
      With pLoggedAlert 
        pFault = pLoggedAlert.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csLoggedAlertCol) Then Return False 
    Dim pLoggedAlertColToTest As csLoggedAlertCol = CType(vEntitiesToTest, csLoggedAlertCol) 
    Return isEqual(pLoggedAlertColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vLoggedAlertsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vLoggedAlertsToTest As csLoggedAlertCol) As Boolean
    If Me.Count <> vLoggedAlertsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vLoggedAlertsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedAlerts._FilledFromSumOnTheFly = True
    
    For Each pLoggedAlert As csLoggedAlert In Me 
      Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
      pLoggedAlerts.Add(pLoggedAlertClone) 
      If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
    Next 
    Return pLoggedAlerts 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedAlertCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedAlerts._FilledFromSumOnTheFly = True
    
    For Each pLoggedAlert As csLoggedAlert In Me
      Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
      pLoggedAlerts.Add(pLoggedAlertClone)
      If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
    Next
    Return pLoggedAlerts
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.ID > vIDFrom AndAlso pLoggedAlert.ID <= vIDTo) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by CallingApplication (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedCallingApplication(ByVal vCallingApplicationFrom As String, ByVal vCallingApplicationTo As String) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.CallingApplication > vCallingApplicationFrom AndAlso pLoggedAlert.CallingApplication <= vCallingApplicationTo) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by DateOccurred (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedDateOccurred(ByVal vDateOccurredStart As Date, ByVal vDateOccurredEnd As Date) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.DateOccurred > vDateOccurredStart AndAlso pLoggedAlert.DateOccurred <= vDateOccurredEnd) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by FaultNumber (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedFaultNumber(ByVal vFaultNumberFrom As Integer, ByVal vFaultNumberTo As Integer) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.FaultNumber > vFaultNumberFrom AndAlso pLoggedAlert.FaultNumber <= vFaultNumberTo) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by MonthOccurred (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedMonthOccurred(ByVal vMonthOccurredStart As Date, ByVal vMonthOccurredEnd As Date) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.MonthOccurred > vMonthOccurredStart AndAlso pLoggedAlert.MonthOccurred <= vMonthOccurredEnd) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by SystemName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedSystemName(ByVal vSystemNameFrom As String, ByVal vSystemNameTo As String) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.SystemName > vSystemNameFrom AndAlso pLoggedAlert.SystemName <= vSystemNameTo) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TimeOccurred (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTimeOccurred(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.TimeOccurred > vTimeOccurredStart AndAlso pLoggedAlert.TimeOccurred <= vTimeOccurredEnd) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TimeOccurred and FaultType and FaultSeverity (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents)  
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedAlert.TimeOccurred > vTimeOccurredStart AndAlso pLoggedAlert.TimeOccurred <= vTimeOccurredEnd) AndAlso (pLoggedAlert.FaultType = vFaultType) AndAlso (pLoggedAlert.FaultSeverity = vFaultSeverity) Then 
        Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
        pLoggedAlerts.Add(pLoggedAlertClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
      End If 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardCallingApplication(ByVal vCallingApplication As String, ByVal vCallingApplicationWildcardType As clsEnums.enmWildCardType) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedAlert.CallingApplication.StartsWith(vCallingApplication, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedAlert.CallingApplication.EndsWith(vCallingApplication, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedAlert.CallingApplication.IndexOf(vCallingApplication, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vCallingApplication.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedAlert.CallingApplication.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
      pLoggedAlerts.Add(pLoggedAlertClone) 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardSystemName(ByVal vSystemName As String, ByVal vSystemNameWildcardType As clsEnums.enmWildCardType) As csLoggedAlertCol 
    Dim pLoggedAlerts As New csLoggedAlertCol 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pLoggedAlert.SystemName.StartsWith(vSystemName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLoggedAlert.SystemName.EndsWith(vSystemName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLoggedAlert.SystemName.IndexOf(vSystemName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vSystemName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLoggedAlert.SystemName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone() 
      pLoggedAlerts.Add(pLoggedAlertClone) 
    Next 
    Return pLoggedAlerts 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pLoggedAlert As csLoggedAlert In Me
      pFault = pLoggedAlert.LoadParents(vRequester)
      If pFault.isOK = False Then Return pFault
    Next
    _WithParents = clsEnums.enmLoadParent.EntireObject 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
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
  Public Function FindByID(ByVal vID As Long) As csLoggedAlert
    If Me.Count = 0 Then Return New csLoggedAlert 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
    
    Dim pLoggedAlert As csLoggedAlert = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pLoggedAlert) 
    If pLoggedAlert IsNot Nothing Then Return pLoggedAlert Else Return New csLoggedAlert() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeOccurred(ByVal vTimeOccurred As Date) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.TimeOccurred = vTimeOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTimeOccurred with vTimeOccurred of {vTimeOccurred}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.TimeOccurred = vTimeOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultNumber(ByVal vFaultNumber As Integer) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultNumber = vFaultNumber Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultNumber with vFaultNumber of {vFaultNumber}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultNumber = vFaultNumber Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SystemName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySystemName(ByVal vSystemName As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSystemName = vSystemName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.SystemName.ToLowerInvariant() = vSystemName Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySystemName with vSystemName of {vSystemName}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.SystemName.ToLowerInvariant() = vSystemName Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CallingApplication
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCallingApplication(ByVal vCallingApplication As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCallingApplication = vCallingApplication.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.CallingApplication.ToLowerInvariant() = vCallingApplication Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCallingApplication with vCallingApplication of {vCallingApplication}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.CallingApplication.ToLowerInvariant() = vCallingApplication Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AffectedUserID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAffectedUserID(ByVal vAffectedUserID As Long) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.AffectedUserID = vAffectedUserID Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAffectedUserID with vAffectedUserID of {vAffectedUserID}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.AffectedUserID = vAffectedUserID Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CallingApplicationVersion
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCallingApplicationVersion(ByVal vCallingApplicationVersion As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCallingApplicationVersion = vCallingApplicationVersion.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.CallingApplicationVersion.ToLowerInvariant() = vCallingApplicationVersion Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCallingApplicationVersion with vCallingApplicationVersion of {vCallingApplicationVersion}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.CallingApplicationVersion.ToLowerInvariant() = vCallingApplicationVersion Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CallingFunctionWithinApplication
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCallingFunctionWithinApplication(ByVal vCallingFunctionWithinApplication As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCallingFunctionWithinApplication = vCallingFunctionWithinApplication.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.CallingFunctionWithinApplication.ToLowerInvariant() = vCallingFunctionWithinApplication Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCallingFunctionWithinApplication with vCallingFunctionWithinApplication of {vCallingFunctionWithinApplication}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.CallingFunctionWithinApplication.ToLowerInvariant() = vCallingFunctionWithinApplication Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FreeText
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFreeText(ByVal vFreeText As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFreeText = vFreeText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FreeText.ToLowerInvariant() = vFreeText Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFreeText with vFreeText of {vFreeText}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FreeText.ToLowerInvariant() = vFreeText Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultingAssembly
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultingAssembly(ByVal vFaultingAssembly As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultingAssembly = vFaultingAssembly.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultingAssembly.ToLowerInvariant() = vFaultingAssembly Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultingAssembly with vFaultingAssembly of {vFaultingAssembly}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultingAssembly.ToLowerInvariant() = vFaultingAssembly Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AssemblyEntryPoint
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAssemblyEntryPoint(ByVal vAssemblyEntryPoint As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAssemblyEntryPoint = vAssemblyEntryPoint.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.AssemblyEntryPoint.ToLowerInvariant() = vAssemblyEntryPoint Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAssemblyEntryPoint with vAssemblyEntryPoint of {vAssemblyEntryPoint}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.AssemblyEntryPoint.ToLowerInvariant() = vAssemblyEntryPoint Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultingClass
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultingClass(ByVal vFaultingClass As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultingClass = vFaultingClass.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultingClass.ToLowerInvariant() = vFaultingClass Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultingClass with vFaultingClass of {vFaultingClass}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultingClass.ToLowerInvariant() = vFaultingClass Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultingFunction
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultingFunction(ByVal vFaultingFunction As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultingFunction = vFaultingFunction.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultingFunction.ToLowerInvariant() = vFaultingFunction Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultingFunction with vFaultingFunction of {vFaultingFunction}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultingFunction.ToLowerInvariant() = vFaultingFunction Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultingFunctionParameters
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultingFunctionParameters(ByVal vFaultingFunctionParameters As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultingFunctionParameters = vFaultingFunctionParameters.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultingFunctionParameters.ToLowerInvariant() = vFaultingFunctionParameters Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultingFunctionParameters with vFaultingFunctionParameters of {vFaultingFunctionParameters}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultingFunctionParameters.ToLowerInvariant() = vFaultingFunctionParameters Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultIdent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultIdent(ByVal vFaultIdent As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultIdent = vFaultIdent.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultIdent.ToLowerInvariant() = vFaultIdent Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultIdent with vFaultIdent of {vFaultIdent}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultIdent.ToLowerInvariant() = vFaultIdent Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultDescription
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultDescription(ByVal vFaultDescription As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFaultDescription = vFaultDescription.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultDescription.ToLowerInvariant() = vFaultDescription Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultDescription with vFaultDescription of {vFaultDescription}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultDescription.ToLowerInvariant() = vFaultDescription Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MessageSentToUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessageSentToUser(ByVal vMessageSentToUser As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vMessageSentToUser = vMessageSentToUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.MessageSentToUser.ToLowerInvariant() = vMessageSentToUser Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMessageSentToUser with vMessageSentToUser of {vMessageSentToUser}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.MessageSentToUser.ToLowerInvariant() = vMessageSentToUser Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActionSentToUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActionSentToUser(ByVal vActionSentToUser As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vActionSentToUser = vActionSentToUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.ActionSentToUser.ToLowerInvariant() = vActionSentToUser Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActionSentToUser with vActionSentToUser of {vActionSentToUser}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.ActionSentToUser.ToLowerInvariant() = vActionSentToUser Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultType(ByVal vFaultType As clsEnums.enmFaultType) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultType = vFaultType Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultType with vFaultType of {vFaultType}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultType = vFaultType Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultSeverity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultSeverity(ByVal vFaultSeverity As clsEnums.enmFaultSeverity) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFaultSeverity with vFaultSeverity of {vFaultSeverity}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoggedLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoggedLoginID(ByVal vLoggedLoginID As Long) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.LoggedLoginID = vLoggedLoginID Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoggedLoginID with vLoggedLoginID of {vLoggedLoginID}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.LoggedLoginID = vLoggedLoginID Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Thread
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByThread(ByVal vThread As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vThread = vThread.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.Thread.ToLowerInvariant() = vThread Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByThread with vThread of {vThread}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.Thread.ToLowerInvariant() = vThread Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserIdentityTypeCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserIdentityTypeCode(ByVal vUserIdentityTypeCode As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUserIdentityTypeCode = vUserIdentityTypeCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.UserIdentityTypeCode.ToLowerInvariant() = vUserIdentityTypeCode Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserIdentityTypeCode with vUserIdentityTypeCode of {vUserIdentityTypeCode}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.UserIdentityTypeCode.ToLowerInvariant() = vUserIdentityTypeCode Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserIdentityTypeNameCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserIdentityTypeNameCode(ByVal vUserIdentityTypeNameCode As Integer) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.UserIdentityTypeNameCode = vUserIdentityTypeNameCode Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserIdentityTypeNameCode with vUserIdentityTypeNameCode of {vUserIdentityTypeNameCode}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.UserIdentityTypeNameCode = vUserIdentityTypeNameCode Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DateOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDateOccurred(ByVal vDateOccurred As Date) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.DateOccurred = vDateOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDateOccurred with vDateOccurred of {vDateOccurred}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.DateOccurred = vDateOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MonthOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMonthOccurred(ByVal vMonthOccurred As Date) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.MonthOccurred = vMonthOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMonthOccurred with vMonthOccurred of {vMonthOccurred}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.MonthOccurred = vMonthOccurred Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedAlert) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In pTempDist.Values
        If pLoggedAlert.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FaultTypeAndFaultSeverity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFaultTypeAndFaultSeverity(ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList()
        If pLoggedAlert.FaultType = vFaultType AndAlso pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.FaultType = vFaultType AndAlso pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    Return pLoggedAlerts
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TimeOccurredAndFaultTypeAndFaultSeverity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurred As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity) As csLoggedAlertCol
    Dim pLoggedAlerts As New csLoggedAlertCol(_WithParents) 
    pLoggedAlerts._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pLoggedAlert As csLoggedAlert In _SortedDictionaryForFindByID.Values.ToList()
        If pLoggedAlert.TimeOccurred = vTimeOccurred AndAlso pLoggedAlert.FaultType = vFaultType AndAlso pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csLoggedAlertCol = Me.Clone() 
      For Each pLoggedAlert As csLoggedAlert In pList 
        If pLoggedAlert.TimeOccurred = vTimeOccurred AndAlso pLoggedAlert.FaultType = vFaultType AndAlso pLoggedAlert.FaultSeverity = vFaultSeverity Then
          Dim pLoggedAlertClone As csLoggedAlert = pLoggedAlert.Clone()
          pLoggedAlerts.Add(pLoggedAlertClone)
          If Not _FilledFromSumOnTheFly Then pLoggedAlerts._Clean.Add(pLoggedAlert.ID) 
        End If
      Next
    End If 
    Return pLoggedAlerts
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
    For Each pLoggedAlert As csLoggedAlert In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pLoggedAlert.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertView, "csLoggedAlertCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csLoggedAlert In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csLoggedAlert = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pLoggedAlertToKill As New csLoggedAlert 
          pLoggedAlertToKill.ID = pCleanID 
          pLoggedAlertToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pLoggedAlertToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csLoggedAlert In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-LoggedAlert-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertUpdate, "csLoggedAlertCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csLoggedAlert In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csLoggedAlert In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csLoggedAlertCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific CallingApplication 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByCallingApplication(ByVal vCallingApplication As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplication={0}", vCallingApplication)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByCallingApplication"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByCallingApplication(vCallingApplication) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "CallingApplication" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplication) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific DateOccurred 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByDateOccurred(ByVal vDateOccurred As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DateOccurred={0}", vDateOccurred)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByDateOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByDateOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByDateOccurred(vDateOccurred) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_DateOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = (vDateOccurred) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FaultNumber 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByFaultNumber(ByVal vFaultNumber As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultNumber={0}", vFaultNumber)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByFaultNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByFaultNumber(vFaultNumber) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "FaultNumber" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vFaultNumber) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FaultSeverity 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByFaultSeverity(ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultSeverity={0}", vFaultSeverity)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByFaultSeverity"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByFaultSeverity(vFaultSeverity) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FaultType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByFaultType(ByVal vFaultType As clsEnums.enmFaultType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultType={0}", vFaultType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByFaultType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByFaultType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByFaultType(vFaultType) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FaultTypeAndFaultSeverity 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByFaultTypeAndFaultSeverity(ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultType={0}, FaultSeverity={1}", vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByFaultType&FaultSeverity"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByFaultTypeAndFaultSeverity(vFaultType, vFaultSeverity) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoggedLoginID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLoggedLoginID(ByVal vLoggedLoginID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoggedLoginID={0}", vLoggedLoginID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByLoggedLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByLoggedLoginID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByLoggedLoginID(vLoggedLoginID) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "c_LoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vLoggedLoginID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MonthOccurred 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByMonthOccurred(ByVal vMonthOccurred As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthOccurred={0}", vMonthOccurred)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByMonthOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByMonthOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByMonthOccurred(vMonthOccurred) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_MonthOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Date).Value = (vMonthOccurred) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific SystemName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteBySystemName(ByVal vSystemName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemName={0}", vSystemName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteBySystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteBySystemName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneBySystemName(vSystemName) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "SystemName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeOccurred 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTimeOccurred(ByVal vTimeOccurred As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurred={0}", vTimeOccurred)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByTimeOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByTimeOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByTimeOccurred(vTimeOccurred) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurred) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeOccurredAndFaultTypeAndFaultSeverity 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurred As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurred={0}, FaultType={1}, FaultSeverity={2}", vTimeOccurred, vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByTimeOccurred&FaultType&FaultSeverity"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByTimeOccurredAndFaultTypeAndFaultSeverity(vTimeOccurred, vFaultType, vFaultSeverity) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeOccurred" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurred) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific AffectedUserID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByAffectedUserID(ByVal vAffectedUserID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("AffectedUserID={0}", vAffectedUserID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByAffectedUserID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByAffectedUserID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedAlerts As New csLoggedAlertCol() : pAllLoggedAlerts.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedAlerts As csLoggedAlertCol = pAllLoggedAlerts.CloneByAffectedUserID(vAffectedUserID) 
      For Each l In pFilteredLoggedAlerts 
        pAllLoggedAlerts.Remove(pAllLoggedAlerts.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedAlerts, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "AffectedUserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vAffectedUserID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific CallingApplication
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedCallingApplication(ByVal vCallingApplicationFrom As String, ByVal vCallingApplicationTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplicationFrom={0}, CallingApplicationTo={1}", vCallingApplicationFrom, vCallingApplicationTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedCallingApplication"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "CallingApplicationFrom" 
        pDALParameters.Add("bndCallingApplicationFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplicationFrom) 
        pLastReadVariableName = "CallingApplicationTo" 
        pDALParameters.Add("bndCallingApplicationTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCallingApplicationTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific DateOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedDateOccurred(ByVal vDateOccurredStart As Date, ByVal vDateOccurredEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DateOccurredStart={0}, DateOccurredEnd={1}", vDateOccurredStart, vDateOccurredEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedDateOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedDateOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_DateOccurredFrom" 
        pDALParameters.Add("bndclc_DateOccurredFrom", ccDAL.enmSQLDataType.Date).Value = (vDateOccurredStart) 
        pLastReadVariableName = "clc_DateOccurredTo" 
        pDALParameters.Add("bndclc_DateOccurredTo", ccDAL.enmSQLDataType.Date).Value = (vDateOccurredEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FaultNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedFaultNumber(ByVal vFaultNumberFrom As Integer, ByVal vFaultNumberTo As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FaultNumberFrom={0}, FaultNumberTo={1}", vFaultNumberFrom, vFaultNumberTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedFaultNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedFaultNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "FaultNumberFrom" 
        pDALParameters.Add("bndFaultNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vFaultNumberFrom) 
        pLastReadVariableName = "FaultNumberTo" 
        pDALParameters.Add("bndFaultNumberTo", ccDAL.enmSQLDataType.Int).Value = (vFaultNumberTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific MonthOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedMonthOccurred(ByVal vMonthOccurredStart As Date, ByVal vMonthOccurredEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("MonthOccurredStart={0}, MonthOccurredEnd={1}", vMonthOccurredStart, vMonthOccurredEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedMonthOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedMonthOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "clc_MonthOccurredFrom" 
        pDALParameters.Add("bndclc_MonthOccurredFrom", ccDAL.enmSQLDataType.Date).Value = (vMonthOccurredStart) 
        pLastReadVariableName = "clc_MonthOccurredTo" 
        pDALParameters.Add("bndclc_MonthOccurredTo", ccDAL.enmSQLDataType.Date).Value = (vMonthOccurredEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific SystemName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedSystemName(ByVal vSystemNameFrom As String, ByVal vSystemNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemNameFrom={0}, SystemNameTo={1}", vSystemNameFrom, vSystemNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedSystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedSystemName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "SystemNameFrom" 
        pDALParameters.Add("bndSystemNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemNameFrom) 
        pLastReadVariableName = "SystemNameTo" 
        pDALParameters.Add("bndSystemNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSystemNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeOccurred
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTimeOccurred(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurredStart={0}, TimeOccurredEnd={1}", vTimeOccurredStart, vTimeOccurredEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedTimeOccurred", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedTimeOccurred"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TimeOccurredAndFaultTypeAndFaultSeverity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTimeOccurredAndFaultTypeAndFaultSeverity(ByVal vTimeOccurredStart As Date, ByVal vTimeOccurredEnd As Date, ByVal vFaultType As clsEnums.enmFaultType, ByVal vFaultSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TimeOccurredStart={0}, TimeOccurredEnd={1}, FaultType={2}, FaultSeverity={3}", vTimeOccurredStart, vTimeOccurredEnd, vFaultType, vFaultSeverity)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByBoundedTimeOccurredAndFaultTypeAndFaultSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedAlertsDeleteByBoundedTimeOccurred&FaultType&FaultSeverity"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TimeOccurredFrom" 
        pDALParameters.Add("bndTimeOccurredFrom", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredStart) 
        pLastReadVariableName = "TimeOccurredTo" 
        pDALParameters.Add("bndTimeOccurredTo", ccDAL.enmSQLDataType.DateTime).Value = (vTimeOccurredEnd) 
        pLastReadVariableName = "enmFaultType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultType) 
        pLastReadVariableName = "enmFaultSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vFaultSeverity) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded CallingApplication
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardCallingApplication(ByVal vCallingApplication As String, ByVal vCallingApplicationWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CallingApplication={0}, CallingApplicationWildcardType={1}", vCallingApplication, vCallingApplicationWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByWildCardCallingApplication", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'CallingApplication 
    Dim pWCCallingApplication As String = "" 
    If vCallingApplicationWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCallingApplication = vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCallingApplication = "%" & vCallingApplication 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCallingApplication = "%" & vCallingApplication & "%" 
    ElseIf vCallingApplicationWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCallingApplication.ToCharArray 
        pWCCallingApplication &= p & "%" 
      Next 
      pWCCallingApplication = "%" & pWCCallingApplication 
    End If 
    
    Dim pCommandText As String = "c_LoggedAlertsDeleteByWildCardCallingApplication"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldCallingApplication" 
        pDALParameters.Add("wldCallingApplication", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCCallingApplication) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded SystemName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardSystemName(ByVal vSystemName As String, ByVal vSystemNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemName={0}, SystemNameWildcardType={1}", vSystemName, vSystemNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedAlertDelete, "csLoggedAlertCol_DeleteByWildCardSystemName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'SystemName 
    Dim pWCSystemName As String = "" 
    If vSystemNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSystemName = vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSystemName = "%" & vSystemName 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSystemName = "%" & vSystemName & "%" 
    ElseIf vSystemNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSystemName.ToCharArray 
        pWCSystemName &= p & "%" 
      Next 
      pWCSystemName = "%" & pWCSystemName 
    End If 
    
    Dim pCommandText As String = "c_LoggedAlertsDeleteByWildCardSystemName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedAlert-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldSystemName" 
        pDALParameters.Add("wldSystemName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCSystemName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedAlert-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedAlert-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-090219-1632", vRequester) 
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
    Me.Sort(New csLoggedAlertCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
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
  
  Public Sub SortByTimeOccurred()
    Me.Sort(New csLoggedAlertCol.CompareByTimeOccurred)
  End Sub
  Private Class CompareByTimeOccurred
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.TimeOccurred < y.TimeOccurred Then
        Return -1
      ElseIf x.TimeOccurred = y.TimeOccurred Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByFaultNumber()
    Me.Sort(New csLoggedAlertCol.CompareByFaultNumber)
  End Sub
  Private Class CompareByFaultNumber
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.FaultNumber < y.FaultNumber Then
        Return -1
      ElseIf x.FaultNumber = y.FaultNumber Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySystemName()
    Me.Sort(New csLoggedAlertCol.CompareBySystemName)
  End Sub
  Private Class CompareBySystemName
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SystemName, y.SystemName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCallingApplication()
    Me.Sort(New csLoggedAlertCol.CompareByCallingApplication)
  End Sub
  Private Class CompareByCallingApplication
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CallingApplication, y.CallingApplication, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAffectedUserID()
    Me.Sort(New csLoggedAlertCol.CompareByAffectedUserID)
  End Sub
  Private Class CompareByAffectedUserID
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.AffectedUserID < y.AffectedUserID Then
        Return -1
      ElseIf x.AffectedUserID = y.AffectedUserID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByAffectedUserText()
    Me.Sort(New csLoggedAlertCol.CompareByAffectedUserText)
  End Sub
  Private Class CompareByAffectedUserText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AffectedUserText, y.AffectedUserText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCallingApplicationVersion()
    Me.Sort(New csLoggedAlertCol.CompareByCallingApplicationVersion)
  End Sub
  Private Class CompareByCallingApplicationVersion
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CallingApplicationVersion, y.CallingApplicationVersion, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCallingFunctionWithinApplication()
    Me.Sort(New csLoggedAlertCol.CompareByCallingFunctionWithinApplication)
  End Sub
  Private Class CompareByCallingFunctionWithinApplication
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CallingFunctionWithinApplication, y.CallingFunctionWithinApplication, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFreeText()
    Me.Sort(New csLoggedAlertCol.CompareByFreeText)
  End Sub
  Private Class CompareByFreeText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FreeText, y.FreeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultingAssembly()
    Me.Sort(New csLoggedAlertCol.CompareByFaultingAssembly)
  End Sub
  Private Class CompareByFaultingAssembly
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultingAssembly, y.FaultingAssembly, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAssemblyEntryPoint()
    Me.Sort(New csLoggedAlertCol.CompareByAssemblyEntryPoint)
  End Sub
  Private Class CompareByAssemblyEntryPoint
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AssemblyEntryPoint, y.AssemblyEntryPoint, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultingClass()
    Me.Sort(New csLoggedAlertCol.CompareByFaultingClass)
  End Sub
  Private Class CompareByFaultingClass
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultingClass, y.FaultingClass, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultingFunction()
    Me.Sort(New csLoggedAlertCol.CompareByFaultingFunction)
  End Sub
  Private Class CompareByFaultingFunction
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultingFunction, y.FaultingFunction, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultingFunctionParameters()
    Me.Sort(New csLoggedAlertCol.CompareByFaultingFunctionParameters)
  End Sub
  Private Class CompareByFaultingFunctionParameters
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultingFunctionParameters, y.FaultingFunctionParameters, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultIdent()
    Me.Sort(New csLoggedAlertCol.CompareByFaultIdent)
  End Sub
  Private Class CompareByFaultIdent
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultIdent, y.FaultIdent, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultDescription()
    Me.Sort(New csLoggedAlertCol.CompareByFaultDescription)
  End Sub
  Private Class CompareByFaultDescription
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultDescription, y.FaultDescription, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByMessageSentToUser()
    Me.Sort(New csLoggedAlertCol.CompareByMessageSentToUser)
  End Sub
  Private Class CompareByMessageSentToUser
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.MessageSentToUser, y.MessageSentToUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActionSentToUser()
    Me.Sort(New csLoggedAlertCol.CompareByActionSentToUser)
  End Sub
  Private Class CompareByActionSentToUser
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ActionSentToUser, y.ActionSentToUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultType()
    Me.Sort(New csLoggedAlertCol.CompareByFaultType)
  End Sub
  Private Class CompareByFaultType
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.FaultType < y.FaultType Then
        Return -1
      ElseIf x.FaultType = y.FaultType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByFaultTypeText()
    Me.Sort(New csLoggedAlertCol.CompareByFaultTypeText)
  End Sub
  Private Class CompareByFaultTypeText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultTypeText, y.FaultTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFaultSeverity()
    Me.Sort(New csLoggedAlertCol.CompareByFaultSeverity)
  End Sub
  Private Class CompareByFaultSeverity
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.FaultSeverity < y.FaultSeverity Then
        Return -1
      ElseIf x.FaultSeverity = y.FaultSeverity Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByFaultSeverityText()
    Me.Sort(New csLoggedAlertCol.CompareByFaultSeverityText)
  End Sub
  Private Class CompareByFaultSeverityText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FaultSeverityText, y.FaultSeverityText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLoggedLoginID()
    Me.Sort(New csLoggedAlertCol.CompareByLoggedLoginID)
  End Sub
  Private Class CompareByLoggedLoginID
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoggedLoginID < y.LoggedLoginID Then
        Return -1
      ElseIf x.LoggedLoginID = y.LoggedLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoggedLoginText()
    Me.Sort(New csLoggedAlertCol.CompareByLoggedLoginText)
  End Sub
  Private Class CompareByLoggedLoginText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LoggedLoginText, y.LoggedLoginText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByThread()
    Me.Sort(New csLoggedAlertCol.CompareByThread)
  End Sub
  Private Class CompareByThread
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Thread, y.Thread, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeCode()
    Me.Sort(New csLoggedAlertCol.CompareByUserIdentityTypeCode)
  End Sub
  Private Class CompareByUserIdentityTypeCode
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeCode, y.UserIdentityTypeCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeText()
    Me.Sort(New csLoggedAlertCol.CompareByUserIdentityTypeText)
  End Sub
  Private Class CompareByUserIdentityTypeText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeText, y.UserIdentityTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserIdentityTypeNameCode()
    Me.Sort(New csLoggedAlertCol.CompareByUserIdentityTypeNameCode)
  End Sub
  Private Class CompareByUserIdentityTypeNameCode
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
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
    Me.Sort(New csLoggedAlertCol.CompareByUserIdentityTypeNameText)
  End Sub
  Private Class CompareByUserIdentityTypeNameText
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserIdentityTypeNameText, y.UserIdentityTypeNameText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDateOccurred()
    Me.Sort(New csLoggedAlertCol.CompareByDateOccurred)
  End Sub
  Private Class CompareByDateOccurred
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DateOccurred < y.DateOccurred Then
        Return -1
      ElseIf x.DateOccurred = y.DateOccurred Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByMonthOccurred()
    Me.Sort(New csLoggedAlertCol.CompareByMonthOccurred)
  End Sub
  Private Class CompareByMonthOccurred
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.MonthOccurred < y.MonthOccurred Then
        Return -1
      ElseIf x.MonthOccurred = y.MonthOccurred Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csLoggedAlertCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csLoggedAlert)
    Private Function Compare(ByVal x As csLoggedAlert, ByVal y As csLoggedAlert) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedAlert).Compare
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
  
    Dim pLoggedAlert As csLoggedAlert
  
    While vReader.Read()
      pLoggedAlert = New csLoggedAlert(_WithParents) 
      pFault = pLoggedAlert.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pLoggedAlert)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedAlert.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedAlertCol As csLoggedAlertCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pLoggedAlert As csLoggedAlert 
 
      For Each pCachedLoggedAlert As csLoggedAlert In vCachedLoggedAlertCol 
        pCachedLoggedAlert.SetWithParents(_WithParents) 
        pLoggedAlert = New csLoggedAlert(pCachedLoggedAlert) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pLoggedAlert.AffectedUserText = "." 
          pLoggedAlert.LoggedLoginText = "." 
        End If 
        pLoggedAlert.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pLoggedAlert) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedAlert.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedAlert-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedAlert) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedAlert) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _WithParents = clsEnums.enmLoadParent.UD 
      bHasParents = True 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
