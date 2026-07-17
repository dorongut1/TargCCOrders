Public Class csAlertMessage
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
      Return True 
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
    [Type] 
    [Severity] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [Number] 
    [Description] 
    [Type] 
    [Severity] 
    [Message] 
    [MessageLocalized] 
    [Action] 
    [ActionLocalized] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [Number] 
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
  
  Private _IsLocalized As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsLocalized() As Boolean 
    Get
      Return _IsLocalized
    End Get
  End Property
  Private _LocalizedLanguage As clsEnums.enmLanguage 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property LocalizedLanguage() As clsEnums.enmLanguage 
    Get 
      Return _LocalizedLanguage 
    End Get 
  End Property 
  
  Private _ID As Long
  Private _Number As Integer
  Private _Description As String
  Private _Type As clsEnums.enmFaultType
  Private _TypeText As String 
  Private _Severity As clsEnums.enmFaultSeverity
  Private _SeverityText As String 
  Private _Message As String
  Private _MessageLocalized As String 
  Private _Action As String
  Private _ActionLocalized As String 
  Private _Tag As String
  
  Public Property [ID]() As Long
    Get
      Return Me._ID
    End Get
    Set(ByVal value As Long)
      If Me._ID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ID = value 
        bPrimaryKey = _ID 
      End If 
    End Set
  End Property
  Public Property [Number]() As Integer
    Get
      Return Me._Number
    End Get
    Set(ByVal value As Integer)
      If Me._Number <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Number = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [Description]() As String
    Get
      Return Me._Description
    End Get
    Set(ByVal value As String)
      If Me._Description <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Description = value 
      End If 
    End Set
  End Property
  Public Property [Type]() As clsEnums.enmFaultType
    Get
      Return Me._Type
    End Get
    Set(ByVal value As clsEnums.enmFaultType)
      If Me._Type <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Type = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [TypeText]() As String
    Get
      Return Me._TypeText
    End Get
    Set(ByVal value As String)
      Me._TypeText = value
    End Set
  End Property
  Public Property [Severity]() As clsEnums.enmFaultSeverity
    Get
      Return Me._Severity
    End Get
    Set(ByVal value As clsEnums.enmFaultSeverity)
      If Me._Severity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Severity = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [SeverityText]() As String
    Get
      Return Me._SeverityText
    End Get
    Set(ByVal value As String)
      Me._SeverityText = value
    End Set
  End Property
  Public Property [Message]() As String
    Get
      Return Me._Message
    End Get
    Set(ByVal value As String)
      If Me._Message <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Message = value 
      End If 
    End Set
  End Property
  Public Property [MessageLocalized]() As String
    Get
      Return Me._MessageLocalized
    End Get
    Set(ByVal value As String)
      If Me._MessageLocalized <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._MessageLocalized = value 
      End If 
    End Set
  End Property
  Public Property [Action]() As String
    Get
      Return Me._Action
    End Get
    Set(ByVal value As String)
      If Me._Action <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Action = value 
      End If 
    End Set
  End Property
  Public Property [ActionLocalized]() As String
    Get
      Return Me._ActionLocalized
    End Get
    Set(ByVal value As String)
      If Me._ActionLocalized <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ActionLocalized = value 
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
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _Number.ToString() Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _Number <> 0 Then pValue.Append("Number='" & _Number.ToString() & "' ‡ ") 
    If _Description <> "" Then pValue.Append("Description='" & _Description & "' ‡ ") 
    If _Type <> clsEnums.enmFaultType.UD Then pValue.Append("Type='" & _Type.FastToString() & "' ‡ ") 
    If _TypeText <> "" Then pValue.Append("TypeText='" & _TypeText & "' ‡ ") 
    If _Severity <> clsEnums.enmFaultSeverity.UD Then pValue.Append("Severity='" & _Severity.FastToString() & "' ‡ ") 
    If _SeverityText <> "" Then pValue.Append("SeverityText='" & _SeverityText & "' ‡ ") 
    If _Message <> "" Then pValue.Append("Message='" & _Message & "' ‡ ") 
    If _MessageLocalized <> "" Then pValue.Append("MessageLocalized='" & _MessageLocalized & "' ‡ ") 
    If _Action <> "" Then pValue.Append("Action='" & _Action & "' ‡ ") 
    If _ActionLocalized <> "" Then pValue.Append("ActionLocalized='" & _ActionLocalized & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _Number.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Description)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Type.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_TypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Severity.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_SeverityText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Message)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MessageLocalized)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Action)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ActionLocalized)}""") 
    If Not vWithTexts Then 
        pCSV.Append($",""{ccHelper.StringForCSV(_Tag)}""") 
    End If 
    'pCSV.Append($",""{bDateAdded:yyyyMMddTHH:mm:ss.ffff}"" ") 
    
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty()
    _IsLocalized = False 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean) 
    MyBase.New()
    CreateEmpty()
    _IsLocalized = vIsLocalized 
  End Sub
  
  Public Sub New(ByVal vPrimaryKeyValue As Long, ByVal vIsLocalized As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional vMustExist As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    _IsLocalized = vIsLocalized 
    
    rFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
  End Sub
  
  Public Sub New(ByVal vcsAlertMessage As csAlertMessage)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsAlertMessage) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vNumber As Integer = 0 _ 
    , Optional vDescription As String = "" _ 
    , Optional vType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD _ 
    , Optional vTypeText As String = "" _ 
    , Optional vSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD _ 
    , Optional vSeverityText As String = "" _ 
    , Optional vMessage As String = "" _ 
    , Optional vMessageLocalized As String = "" _ 
    , Optional vAction As String = "" _ 
    , Optional vActionLocalized As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vIsLocalized As Boolean = False _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _Number = vNumber 
    _Description = vDescription 
    _Type = vType 
    _TypeText = vTypeText 
    _Severity = vSeverity 
    _SeverityText = vSeverityText 
    _Message = vMessage 
    _MessageLocalized = vMessageLocalized 
    _Action = vAction 
    _ActionLocalized = vActionLocalized 
    _Tag = vTag 
    bDateAdded = vDateAdded 
    _IsLocalized = vIsLocalized 
    bccStatus = clsEnums.enmObjectStatus.Dirty 
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
  End Sub 
 
  Friend Sub New(ByVal vRow As DataRow, ByVal vRequester As clsRequester, Optional ByVal vIsLocalized As Boolean = False) 
    MyBase.New()
    CreateEmpty()
    Dim pFault As New clsFault 
 
    pFault = LoadDataRow(vRow, vRequester) 
    If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
 
    _IsLocalized = vIsLocalized 
 
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
    _IsLocalized = vIsLocalized 
  End Sub 
  ''' <summary> 
  ''' The default language is that if the Requester object. You can override this here 
  ''' </summary> 
  ''' <param name="vLanguage"></param> 
  Public Sub OverrideDefaultLanguage(ByVal vLanguage As clsEnums.enmLanguage) 
    If _IsLocalized = True Then 
      _LocalizedLanguage = vLanguage 
    End If 
    If vLanguage <> clsEnums.enmLanguage.UD AndAlso _IsLocalized = False Then 
      Throw New Exception("You can't set a language unless you 1st localize the instance") 
    End If 
  End Sub 
 
  Private _IsTruncated As Boolean = False 
  
  ''' <summary> 
  ''' Use this before loading a DataGridView. You don't need more than X c to see what you want. 
  ''' </summary> 
  ''' <param name="pTruncateLength"></param> 
  Friend Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
    'Truncates strings, and reduces pictures to W 100 x H 50 
 
    _IsTruncated = False 
 
    _Description = _Description.Truncate(pTruncateLength, _IsTruncated) 
    _Message = _Message.Truncate(pTruncateLength, _IsTruncated) 
    _Action = _Action.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _Description = ccHelper.RemoveChrW0(_Description) 
    _Message = ccHelper.RemoveChrW0(_Message) 
    _Action = ccHelper.RemoveChrW0(_Action) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AlertMessage by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessage_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AlertMessage-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [Number] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AlertMessage by the chosen parameters. This function may be a bit slower than accessing the AlertMessage's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessage_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.Number 
          pFault = GetByNumber(ccHelper.ToInteger(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AlertMessage-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AlertMessage-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the AlertMessage by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessage_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"AlertMessage not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-AlertMessage-210927-1527", vRequester, vAdditionalMessageToUser:=$"AlertMessage not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccAlertMessageCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessageGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"AlertMessage not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-AlertMessage-210625-0950", vRequester, vAdditionalMessageToUser:=$"AlertMessage not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the AlertMessage by Number.
  ''' </summary>
  ''' <param name="vNumber"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByNumber(ByVal vNumber As Integer, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Number={0}", vNumber)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessage_GetByNumber", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccAlertMessageCol.FindByNumber(vNumber), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessageGetByNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Number" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (vNumber) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"AlertMessage not found for GetByNumber. See FunctionParameters for values", pFunctionParameters, "TRGT-AlertMessage-210625-0950", vRequester, vAdditionalMessageToUser:=$"AlertMessage not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, "csAlertMessage_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-AlertMessage-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, "csAlertMessage_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-AlertMessage-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the AlertMessage. If there are parents or children in the AlertMessage, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, "csAlertMessage_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pAlertMessage As New csAlertMessage(_IsLocalized) 
    If Me.isEqual(pAlertMessage) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-AlertMessage-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-AlertMessage-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_AlertMessageUpdate"
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
  
    If _IsLocalized = True Then 
      'get the original one 
      Dim pAlertMessageOrig As New csAlertMessage(vIsLocalized:=True) 
      If _ID > 0 Then 
        pFault = pAlertMessageOrig.GetByID(_ID, vRequester, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
      End If 
      Dim pDoTranslate As Boolean = False 
      If Not (String.IsNullOrEmpty(_MessageLocalized)) Then 
        pDoTranslate = True 
        If _MessageLocalized = _Message Then pDoTranslate = False 
        If _MessageLocalized = pAlertMessageOrig.Message Then pDoTranslate = False 
        If pDoTranslate = False Then _MessageLocalized = "" 
      End If 
      If Not (String.IsNullOrEmpty(_ActionLocalized)) Then 
        pDoTranslate = True 
        If _ActionLocalized = _Action Then pDoTranslate = False 
        If _ActionLocalized = pAlertMessageOrig.Action Then pDoTranslate = False 
        If pDoTranslate = False Then _ActionLocalized = "" 
      End If 
    End If 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pCachedAlertMessage As csAlertMessage 
      If _ID = 0 Then 
        pCachedAlertMessage = New csAlertMessage(_IsLocalized) 
        'get last ID 
        Dim pAlertMessageCol As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.Clone() 
        If pAlertMessageCol.Count = 0 Then 
          _ID = 1 
        Else 
          pAlertMessageCol.SortByID() 
          Dim pLastID As Long = pAlertMessageCol(pAlertMessageCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccAlertMessageCol.Add(pCachedAlertMessage) 
      Else  
        pCachedAlertMessage = MyController.DBCache.ccAlertMessageCol.FindByID(_ID) 
      End If 
      pCachedAlertMessage.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAlertMessageCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "Number" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_Number) 
        pLastReadVariableName = "Description" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Description) 
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_Type.FastToString()) 
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_Severity.FastToString()) 
        pLastReadVariableName = "locMessage" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Message) 
        pLastReadVariableName = "locAction" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Action) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If _IsLocalized = True Then 
      'Now save the Localized version 
      'Message 
      pFault = ccHelper.UpdateTranslation(clsEnums.enmObjectType.TableData, "c_AlertMessage", "Message", pID, _MessageLocalized, _LocalizedLanguage, vRequester) 
      If pFault.isOK = False Then Return pFault 
      'Action 
      pFault = ccHelper.UpdateTranslation(clsEnums.enmObjectType.TableData, "c_AlertMessage", "Action", pID, _ActionLocalized, _LocalizedLanguage, vRequester) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If vReload = True Then 
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
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
    Dim pFunctionParameters As String = String.Format("AlertMessage.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessage_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_AlertMessageDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      MyController.DBCache.ccAlertMessageCol.Remove(MyController.DBCache.ccAlertMessageCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAlertMessageCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090623-1813", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
          
    'Now delete translations 
    pFault = ccHelper.DeleteTranslationsForTableDataRow(clsEnums.enmObjectType.TableData, "AlertMessage", _ID, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessage_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessageDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      MyController.DBCache.ccAlertMessageCol.Remove(MyController.DBCache.ccAlertMessageCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAlertMessageCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    'Now delete translations 
    pFault = ccHelper.DeleteTranslationsForTableDataRow(clsEnums.enmObjectType.TableData, "AlertMessage", vID, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csAlertMessage) Then Return False 
    Dim pAlertMessageToTest As csAlertMessage = CType(vTargCCEntityToTest, csAlertMessage) 
    Return isEqual(pAlertMessageToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vAlertMessageToTest As csAlertMessage) As Boolean
    With vAlertMessageToTest
      If _ID <> .ID Then Return False
      If _Number <> .Number Then Return False
      If _Description <> .Description Then Return False
      If _Type <> .Type Then Return False
      If _Severity <> .Severity Then Return False
      If _Message <> .Message Then Return False
      If _MessageLocalized <> .MessageLocalized Then Return False
      If _Action <> .Action Then Return False
      If _ActionLocalized <> .ActionLocalized Then Return False
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
    Dim pClone As New csAlertMessage(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAlertMessage
    Dim pClone As New csAlertMessage(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Number") = _Number : Catch ex As Exception : Return pFault.LogException(ex, "Number", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Description") = _Description : Catch ex As Exception : Return pFault.LogException(ex, "Description", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Type") = _Type : Catch ex As Exception : Return pFault.LogException(ex, "Type", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Severity") = _Severity : Catch ex As Exception : Return pFault.LogException(ex, "Severity", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Message") = _Message : Catch ex As Exception : Return pFault.LogException(ex, "Message", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
    Try : vDataRow("Action") = _Action : Catch ex As Exception : Return pFault.LogException(ex, "Action", "TRGT-AlertMessage-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pAlertMessage As csAlertMessage = CType(pXmlSerializer.Deserialize(pStreamReader), csAlertMessage) 
      AssignValues(pAlertMessage) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-AlertMessage-130515-1230", vRequester) 
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
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          pBinaryWriter.Write(bccStatus.FastToString()) 
          'ID 
          pBinaryWriter.Write(_ID) 
          'Number 
          pBinaryWriter.Write(_Number) 
          'Description 
          If _Description Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Description) 
          'Type 
          pBinaryWriter.Write(_Type.FastToString()) 
          'Severity 
          pBinaryWriter.Write(_Severity.FastToString()) 
          'Message 
          If _Message Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Message) 
          pBinaryWriter.Write(_MessageLocalized) 
          'Action 
          If _Action Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Action) 
          pBinaryWriter.Write(_ActionLocalized) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-150307-2338", vRequester) 
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
          _IsLocalized = pReader.ReadBoolean 
          _LocalizedLanguage = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          bccStatus = clsEnums.TranslateEnmObjectStatus(pReader.ReadString) 
          'ID 
          _ID = pReader.ReadInt64 
          'Number 
          _Number = pReader.ReadInt32 
          'Description 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Description = pReader.ReadString 
          'Type 
          _Type = clsEnums.TranslateEnmFaultType(pReader.ReadString) 
          'Severity 
          _Severity = clsEnums.TranslateEnmFaultSeverity(pReader.ReadString) 
          'Message 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Message = pReader.ReadString 
          'Localizable 
          _MessageLocalized = pReader.ReadString 
          'Action 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Action = pReader.ReadString 
          'Localizable 
          _ActionLocalized = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-AlertMessage-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-190720-1443", vRequester) 
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
 
      Dim pAlertMessage As csAlertMessage = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csAlertMessage)(vJSON, pSettings) 
      AssignValues(pAlertMessage) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vAlertMessage As csAlertMessage)
    With vAlertMessage
      _ID = .ID 
      _Number = .Number 
      _Description = .Description 
      _Type = .Type 
      _TypeText = .TypeText
      _Severity = .Severity 
      _SeverityText = .SeverityText
      _Message = .Message 
      _MessageLocalized = .MessageLocalized
      _Action = .Action 
      _ActionLocalized = .ActionLocalized
      _Tag = .Tag 
      _IsLocalized = .IsLocalized 
      _LocalizedLanguage = .LocalizedLanguage 
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
      'Type 
      pTextToGet = "TypeText (Enum)" 
      _TypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.FaultType, _Type.FastToString(), vRequester) 
      'Severity 
      pTextToGet = "SeverityText (Enum)" 
      _SeverityText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.FaultSeverity, _Severity.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-AlertMessage-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Friend Function LoadTranslations(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters = String.Format("Item ID = {0}, UILang = {1}", _ID.ToString, vRequester.UILang.ToString) 
    Dim pFault As New clsFault 
 
    Try 
      _MessageLocalized = ccHelper.GetLocalizedTableData("c_AlertMessage", "Message", _ID, vRequester, _LocalizedLanguage) 
      If _MessageLocalized = "" Then _MessageLocalized = _Message 
      _ActionLocalized = ccHelper.GetLocalizedTableData("c_AlertMessage", "Action", _ID, vRequester, _LocalizedLanguage) 
      If _ActionLocalized = "" Then _ActionLocalized = _Action 
      _IsLocalized = True 
      If _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
        _LocalizedLanguage = vRequester.UILang 
      End If 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-130216-0956", vRequester) 
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
      pLastReadVariableName = "Number" 
      If Not vReader.IsDBNull(1) Then _Number = vReader.GetInt32(1)
      pLastReadVariableName = "Description" 
      If Not vReader.IsDBNull(2) Then _Description = vReader.GetString(2) 
      pLastReadVariableName = "enmType_FaultType" 
      If Not vReader.IsDBNull(3) Then _Type = clsEnums.TranslateEnmFaultType(vReader.GetString(3))
      pLastReadVariableName = "enmSeverity_FaultSeverity" 
      If Not vReader.IsDBNull(4) Then _Severity = clsEnums.TranslateEnmFaultSeverity(vReader.GetString(4))
      pLastReadVariableName = "locMessage" 
      If Not vReader.IsDBNull(5) Then _Message = vReader.GetString(5) 
      pLastReadVariableName = "locAction" 
      If Not vReader.IsDBNull(6) Then _Action = vReader.GetString(6) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(7) Then bDateAdded = vReader.GetDateTime(7)   
      If _IsLocalized = True Then 
        pFault = LoadTranslations(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedAlertMessage As csAlertMessage, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIsLocalized As Boolean = _IsLocalized 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedAlertMessage) 
      If pIsLocalized = True Then 
        pFault = LoadTranslations(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _Number = 0
    _Description = ""
    _Type = clsEnums.enmFaultType.UD
    _TypeText = ""
    _Severity = clsEnums.enmFaultSeverity.UD
    _SeverityText = ""
    _Message = ""
    _MessageLocalized = ""
    _Action = ""
    _ActionLocalized = ""
    _Tag = ""
    _IsCleanForXML = False 
    bccStatus = clsEnums.enmObjectStatus.New 
    bPrimaryKey = _ID 
    bDateAdded = Nothing 
    bDefaultDesignation = "" 
     
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _IsLocalized = False 
      _LocalizedLanguage = clsEnums.enmLanguage.UD 
      bHasParents = False 
      bHasLocalizedFields = True 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csAlertMessageCol
  Inherits cTargCCCollection(Of csAlertMessage)
  Implements ITargCCCollectionUpdateable 
  Implements ITargCCDataReaderUser 
  
  Public Overloads Shared ReadOnly Property HasParents As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Overloads Shared ReadOnly Property HasLocalizedFields As Boolean 
    Get 
      Return True 
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csAlertMessage) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByNumber As Dictionary(Of String, csAlertMessage) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByNumber As Boolean 
  Private Function CreateKeyForFindByNumber(ByVal vAlertMessage As csAlertMessage) As String 
    With vAlertMessage 
      Return .Number.ToString()
    End With 
  End Function 
   
  Private _IsCleanForXML As Boolean 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
 
  Private _IsLocalized As Boolean 
  Public ReadOnly Property IsLocalized() As Boolean
    Get
      Return _IsLocalized
    End Get
  End Property
  Private _LocalizedLanguage As clsEnums.enmLanguage 
  Public ReadOnly Property LocalizedLanguage() As clsEnums.enmLanguage 
    Get 
      Return _LocalizedLanguage 
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
 
    For Each pRow As csAlertMessage In Me 
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
    pCSVTitle.Append(",""Number""") 
    pCSVTitle.Append(",""Description""") 
    pCSVTitle.Append(",""Type" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Type (Text)""") 
    pCSVTitle.Append(",""Severity" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Severity (Text)""") 
    pCSVTitle.Append(",""Message""") 
    pCSVTitle.Append(",""MessageLocalized""") 
    pCSVTitle.Append(",""Action""") 
    pCSVTitle.Append(",""ActionLocalized""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csAlertMessage In Me 
      pCSV.AppendLine(pRow.ToCSV(vWithTexts)) 
    Next 
 
    Return pCSV.ToString() 
  End Function 
  
  Public Sub New()
    MyBase.New()
    CreateEmpty() 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean) 
    MyBase.New()
    CreateEmpty() 
    _IsLocalized = vIsLocalized 
  End Sub
  
  Public Sub New(ByVal vIsLocalized As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) 
    MyBase.New()
    CreateEmpty() 
    _IsLocalized = vIsLocalized 
    
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
 
  Public Overloads Sub Add(ByVal vAlertMessage As csAlertMessage) 
    SyncLock _CollectionLock 
      MyBase.Add(vAlertMessage) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vAlertMessage As csAlertMessage) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vAlertMessage) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vAlertMessageCol As csAlertMessageCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vAlertMessageCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByNumber = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vAlertMessage As csAlertMessage) 
    SyncLock _CollectionLock 
      MyBase.Remove(vAlertMessage) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByNumber = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csAlertMessage) 
      
      For Each lAlertMessage In Me 
        If lAlertMessage.IsEmpty OrElse pTempDictionary.ContainsKey(lAlertMessage.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lAlertMessage.ID, lAlertMessage) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lAlertMessage.ToString, "TRGT-AlertMessage-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", AlertMessage:" & lAlertMessage.ToString() & ", TRGT-AlertMessage-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadNumbers() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByNumber Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByNumber Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByNumber = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByNumber' yet!
      Dim pTempDictionary As New Dictionary(Of String, csAlertMessage)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lAlertMessage In Me 
        Try 
          Dim pNumber As String = CreateKeyForFindByNumber(lAlertMessage) 
          If String.IsNullOrEmpty(pNumber.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pNumber)) Then 
            pTempDictionary.Add(pNumber, lAlertMessage) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lAlertMessage.ToString, "TRGT-AlertMessage-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByNumber:" & ex.Message & ", AlertMessage:" & lAlertMessage.ToString() & ", TRGT-AlertMessage-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByNumber = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByNumber = False
    End SyncLock 
  End Sub 
 
  Public Overrides Sub SetWithParents(ByVal vWithParents As clsEnums.enmLoadParent) 
    Throw New Exception("Entity has no parents") 
  End Sub 
  Public Overrides Sub SetLocalizable(ByVal vIsLocalized As Boolean) 
    _IsLocalized = vIsLocalized 
  End Sub 
  ''' <summary> 
  ''' The default language is that if the Requester object. You can override this here 
  ''' </summary> 
  ''' <param name="vLanguage"></param> 
  Public Sub OverrideDefaultLanguage(ByVal vLanguage As clsEnums.enmLanguage) 
    If _IsLocalized = True Then 
      _LocalizedLanguage = vLanguage 
    End If 
    If vLanguage <> clsEnums.enmLanguage.UD AndAlso _IsLocalized = False Then 
      Throw New Exception("You can't set a language unless you 1st localize the instance") 
    End If 
  End Sub 
 
  ''' <summary>  
  ''' Use this before loading a DataGridView. You don't need more than pTruncateLength characters to see what you want.  
  ''' </summary>  
  ''' <param name="pTruncateLength"></param>  
  Public Sub TruncateStrings(Optional pTruncateLength As Integer = 50) 
 
    For Each lAlertMessage As csAlertMessage In Me 
      lAlertMessage.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lAlertMessage As csAlertMessage In Me 
      lAlertMessage.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [Description] 
    [TypeAndSeverity] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the AlertMessages by the chosen parameters. This function may be a bit slower than accessing the AlertMessage's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.Description 
          pFault = FillByDescription(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TypeAndSeverity 
          pFault = FillByTypeAndSeverity(clsEnums.TranslateEnmFaultType(CStr(vParameters(0))), clsEnums.TranslateEnmFaultSeverity(CStr(vParameters(1))), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AlertMessage-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AlertMessage-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.Clone() 
      pAlertMessagesCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pAlertMessagesCached.Reverse() 
      If vHowMany > 0 AndAlso pAlertMessagesCached.Count > vHowMany Then 
        Dim tmp As New csAlertMessageCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pAlertMessagesCached(i)) 
        Next 
        pAlertMessagesCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Description, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByDescription(ByVal vDescription As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Description={0}", vDescription)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.CloneByDescription(vDescription)
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByDescription" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Description" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescription) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TypeAndSeverity, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTypeAndSeverity(ByVal vType As clsEnums.enmFaultType, ByVal vSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, Severity={1}", vType, vSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByTypeAndSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.CloneByTypeAndSeverity(vType, vSeverity)
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByType&Severity" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vType.FastToString()) 
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vSeverity.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Description, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedDescription(ByVal vDescriptionFrom As String, ByVal vDescriptionTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("DescriptionFrom={0}, DescriptionTo={1}", vDescriptionFrom, vDescriptionTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByBoundedDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.CloneByBoundedDescription(vDescriptionFrom, vDescriptionTo)
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByBoundedDescription" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "DescriptionFrom" 
        pDALParameters.Add("bndDescriptionFrom", ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescriptionFrom) 
        pLastReadVariableName = "DescriptionTo" 
        pDALParameters.Add("bndDescriptionTo", ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescriptionTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Number, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedNumber(ByVal vNumberFrom As Integer, ByVal vNumberTo As Integer, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("NumberFrom={0}, NumberTo={1}", vNumberFrom, vNumberTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByBoundedNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAlertMessageCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAlertMessageCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAlertMessageCol failed: " & pResponse) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.CloneByBoundedNumber(vNumberFrom, vNumberTo)
      pFault = LoadMeFromDBCache(pAlertMessagesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByBoundedNumber" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "NumberFrom" 
        pDALParameters.Add("bndNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vNumberFrom) 
        pLastReadVariableName = "NumberTo" 
        pDALParameters.Add("bndNumberTo", ccDAL.enmSQLDataType.Int).Value = (vNumberTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded Description, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardDescription(ByVal vDescription As String, ByVal vDescriptionWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Description={0}, DescriptionWildcardType={1}", vDescription, vDescriptionWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByWildCardDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Description 
    Dim pWCDescription As String = "" 
    If vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
      pWCDescription = vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCDescription = "%" & vDescription 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCDescription = "%" & vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vDescription.ToCharArray 
        pWCDescription &= p & "%" 
      Next 
      pWCDescription = "%" & pWCDescription 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-AlertMessage-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillByWildCardDescription" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldDescription" 
        pDALParameters.Add("wldDescription", ccDAL.enmSQLDataType.NVarChar, 100).Value = (pWCDescription) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lAlertMessage As New csAlertMessage() 
      pFault = lAlertMessage.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lAlertMessage.IsEmpty Then Me.Add(lAlertMessage) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pAlertMessages As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pAlertMessages, "csAlertMessageCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pAlertMessages IsNot Nothing AndAlso Me.Count <> pAlertMessages.Count Then FillFromListOfITargCCEntity(pAlertMessages) 
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
    NumberFrom
    NumberTo
    [Description]
    DescriptionWildcardType
    [Type]
    [Severity]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pNumberFrom As Nullable(Of Integer) = Nothing
    Dim pNumberTo As Nullable(Of Integer) = Nothing
    Dim pDescription As String = Nothing
    Dim pDescriptionWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD
    Dim pSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.NumberFrom) : If pObj IsNot Nothing Then pNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.NumberTo) : If pObj IsNot Nothing Then pNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Description) Then pObj = vParameters(enmFillOnTheFlyParameters.Description) : If pObj IsNot Nothing Then pDescription = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DescriptionWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.DescriptionWildcardType) : If pObj IsNot Nothing Then pDescriptionWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Type) Then pObj = vParameters(enmFillOnTheFlyParameters.Type) : If pObj IsNot Nothing Then pType = CType(pObj, clsEnums.enmFaultType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Severity) Then pObj = vParameters(enmFillOnTheFlyParameters.Severity) : If pObj IsNot Nothing Then pSeverity = CType(pObj, clsEnums.enmFaultSeverity) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pNumberFrom, pNumberTo _
        , pDescription, pDescriptionWildcardType _
        , pType _
        , pSeverity _
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
        , ByVal vNumberFrom As Nullable(Of Integer), ByVal vNumberTo As Nullable(Of Integer) _
        , ByVal vDescription As String, ByVal vDescriptionWildcardType As clsEnums.enmWildCardType _
        , ByVal vType As clsEnums.enmFaultType _
        , ByVal vSeverity As clsEnums.enmFaultSeverity _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, NumberFrom={2}, NumberTo={3}, Description={4}, DescriptionWildcardType={5}, Type={6}, Severity={7}", vIDFrom, vIDTo, vNumberFrom, vNumberTo, vDescription, vDescriptionWildcardType.FastToString(), vType, vSeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Description 
    Dim pWCDescription As String = "" 
    If vDescription = Nothing Then 
      pWCDescription = vDescription
    Else 
      If vDescriptionWildcardType = clsEnums.enmWildCardType.None OrElse vDescriptionWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCDescription = vDescription
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
        pWCDescription = vDescription & "%" 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCDescription = "%" & vDescription 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCDescription = "%" & vDescription & "%" 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vDescription.ToCharArray 
          pWCDescription &= p & "%" 
        Next 
        pWCDescription = "%" & pWCDescription 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-AlertMessage-121122-2008", vRequester) 
      Dim pAlertMessagesCached As csAlertMessageCol = MyController.DBCache.ccAlertMessageCol.Clone() 
      Dim pAlertMessagesToUse As New csAlertMessageCol() 
      For Each l In pAlertMessagesCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vNumberFrom.HasValue Then 
          If vNumberTo.HasValue Then 
            If l.Number < vNumberFrom OrElse l.Number > vNumberTo.Value Then Continue For 
          Else 
            If l.Number <> vNumberFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vDescription) Then 
          If vDescriptionWildcardType = clsEnums.enmWildCardType.UD OrElse vDescriptionWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Description.Equals(vDescription, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Description.StartsWith(vDescription, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Description.EndsWith(vDescription, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Description.IndexOf(vDescription, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vType <> clsEnums.enmFaultType.UD Then 
          If l.Type <> vType Then Continue For 
        End If 
        If vSeverity <> clsEnums.enmFaultSeverity.UD Then 
          If l.Severity <> vSeverity Then Continue For 
        End If 
        pAlertMessagesToUse.Add(l) 
      Next 
      pAlertMessagesToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pAlertMessagesToUse.Reverse() 
      If vHowMany > 0 AndAlso pAlertMessagesToUse.Count > vHowMany Then 
        Dim tmp As New csAlertMessageCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pAlertMessagesToUse(i)) 
        Next 
        pAlertMessagesToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pAlertMessagesToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "NumberFrom" 
        pDALParameters.Add("bndNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vNumberFrom) 
        pLastReadVariableName = "NumberTo" 
        pDALParameters.Add("bndNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vNumberTo) 
        pLastReadVariableName = "Description" 
        pDALParameters.Add("wldDescription", ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(pWCDescription) 
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vType.FastToString()) 
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vSeverity.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByDescription
    GroupByType
    GroupBySeverity
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pNumberFrom As Nullable(Of Integer) = Nothing
    Dim pNumberTo As Nullable(Of Integer) = Nothing
    Dim pDescription As String = Nothing
    Dim pDescriptionWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pType As clsEnums.enmFaultType = clsEnums.enmFaultType.UD
    Dim pSeverity As clsEnums.enmFaultSeverity = clsEnums.enmFaultSeverity.UD
    Dim pGroupByDescription As Boolean = False
    Dim pGroupByType As Boolean = False
    Dim pGroupBySeverity As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NumberFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.NumberFrom) : If pObj IsNot Nothing Then pNumberFrom = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NumberTo) Then pObj = vParameters(enmFillOnTheFlyParameters.NumberTo) : If pObj IsNot Nothing Then pNumberTo = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Description) Then pObj = vParameters(enmFillOnTheFlyParameters.Description) : If pObj IsNot Nothing Then pDescription = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.DescriptionWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.DescriptionWildcardType) : If pObj IsNot Nothing Then pDescriptionWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Type) Then pObj = vParameters(enmFillOnTheFlyParameters.Type) : If pObj IsNot Nothing Then pType = CType(pObj, clsEnums.enmFaultType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Severity) Then pObj = vParameters(enmFillOnTheFlyParameters.Severity) : If pObj IsNot Nothing Then pSeverity = CType(pObj, clsEnums.enmFaultSeverity) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByDescription) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByDescription) : If pObj IsNot Nothing Then pGroupByDescription = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByType) : If pObj IsNot Nothing Then pGroupByType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupBySeverity) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupBySeverity) : If pObj IsNot Nothing Then pGroupBySeverity = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pNumberFrom, pNumberTo _
        , pDescription, pDescriptionWildcardType _
        , pType _
        , pSeverity _
        , pGroupByDescription _
        , pGroupByType _
        , pGroupBySeverity _
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
        , ByVal vNumberFrom As Nullable(Of Integer), ByVal vNumberTo As Nullable(Of Integer) _
        , ByVal vDescription As String, ByVal vDescriptionWildcardType As clsEnums.enmWildCardType _
        , ByVal vType As clsEnums.enmFaultType _
        , ByVal vSeverity As clsEnums.enmFaultSeverity _
        , ByVal vGroupByDescription As Boolean _
        , ByVal vGroupByType As Boolean _
        , ByVal vGroupBySeverity As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, NumberFrom={2}, NumberTo={3}, Description={4}, DescriptionWildcardType={5}, Type={6}, Severity={7}, GroupByDescription={8}, GroupByType={9}, GroupBySeverity={10}", vIDFrom, vIDTo, vNumberFrom, vNumberTo, vDescription, vDescriptionWildcardType.FastToString(), vType, vSeverity, vGroupByDescription, vGroupByType, vGroupBySeverity)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Description 
    Dim pWCDescription As String = "" 
    If vDescription = Nothing Then 
      pWCDescription = vDescription
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.None OrElse vDescriptionWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCDescription = vDescription
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
      pWCDescription = vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCDescription = "%" & vDescription 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCDescription = "%" & vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vDescription.ToCharArray 
        pWCDescription &= p & "%" 
      Next 
      pWCDescription = "%" & pWCDescription 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-AlertMessage-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AlertMessagesFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "NumberFrom" 
        pDALParameters.Add("bndNumberFrom", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vNumberFrom) 
        pLastReadVariableName = "NumberTo" 
        pDALParameters.Add("bndNumberTo", ccDAL.enmSQLDataType.Int).Value = ccHelper.ObjectNullable(vNumberTo) 
        pLastReadVariableName = "Description" 
        pDALParameters.Add("wldDescription", ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(pWCDescription) 
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vType) 
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vSeverity) 
        pLastReadVariableName = "Description" 
        pDALParameters.Add("GroupByDescription", ccDAL.enmSQLDataType.Bit).Value = vGroupByDescription
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add("GroupByenmType_FaultType", ccDAL.enmSQLDataType.Bit).Value = vGroupByType
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add("GroupByenmSeverity_FaultSeverity", ccDAL.enmSQLDataType.Bit).Value = vGroupBySeverity
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vAlertMessageArray As csAlertMessage())
    Me.Clear()
    
    For Each pAlertMessage As csAlertMessage In vAlertMessageArray
      Me.Add(pAlertMessage)
      _Clean.Add(pAlertMessage.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pAlertMessage As New csAlertMessage(pRow, vRequester, _IsLocalized) 
        Me.Add(pAlertMessage) 
        _Clean.Add(pAlertMessage.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-AlertMessageCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-130515-1300", vRequester) 
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
      Dim pAlertMessages As csAlertMessageCol = CType(pXmlSerializer.Deserialize(pStreamReader), csAlertMessageCol) 
      For Each pAlertMessage As csAlertMessage In pAlertMessages 
        Me.Add(pAlertMessage) 
        _Clean.Add(pAlertMessage.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-AlertMessage-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-190720-1443", vRequester) 
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
 
      Dim pAlertMessages As List(Of csAlertMessage) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csAlertMessage))(vJSON, pSettings) 
      For Each pAlertMessage As csAlertMessage In pAlertMessages 
        Me.Add(pAlertMessage) 
        _Clean.Add(pAlertMessage.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-190720-2059", vRequester) 
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
          'IsLocalized 
          pBinaryWriter.Write(_IsLocalized) 
          pBinaryWriter.Write(_LocalizedLanguage.FastToString()) 
          'Items 
          pBinaryWriter.Write(Me.Count) 
          For Each lAlertMessage As csAlertMessage In Me 
            Dim pByte As Byte() = lAlertMessage.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-150307-2340", vRequester) 
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
          'IsLocalized 
          _IsLocalized = pReader.ReadBoolean 
          _LocalizedLanguage = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'Items 
          Dim pCount As Integer = pReader.ReadInt32 
          For iCntr As Integer = 0 To pCount - 1 
            Dim pLength As Integer = pReader.ReadInt32 
            Dim pAlertMessage As csAlertMessage = New csAlertMessage(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pAlertMessage) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pAlertMessage.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-AlertMessage-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pAlertMessage As csAlertMessage In Me 
      With pAlertMessage 
        pFault = pAlertMessage.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csAlertMessageCol) Then Return False 
    Dim pAlertMessageColToTest As csAlertMessageCol = CType(vEntitiesToTest, csAlertMessageCol) 
    Return isEqual(pAlertMessageColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vAlertMessagesToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vAlertMessagesToTest As csAlertMessageCol) As Boolean
    If Me.Count <> vAlertMessagesToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vAlertMessagesToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pAlertMessages._FilledFromSumOnTheFly = True
    
    For Each pAlertMessage As csAlertMessage In Me 
      Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone() 
      pAlertMessages.Add(pAlertMessageClone) 
      If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAlertMessageCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pAlertMessages._FilledFromSumOnTheFly = True
    
    For Each pAlertMessage As csAlertMessage In Me
      Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
      pAlertMessages.Add(pAlertMessageClone)
      If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
    Next
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csAlertMessageCol 
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized)  
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAlertMessage As csAlertMessage In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAlertMessage.ID > vIDFrom AndAlso pAlertMessage.ID <= vIDTo) Then 
        Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone() 
        pAlertMessages.Add(pAlertMessageClone) 
        If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Description (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedDescription(ByVal vDescriptionFrom As String, ByVal vDescriptionTo As String) As csAlertMessageCol 
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized)  
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAlertMessage As csAlertMessage In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAlertMessage.Description > vDescriptionFrom AndAlso pAlertMessage.Description <= vDescriptionTo) Then 
        Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone() 
        pAlertMessages.Add(pAlertMessageClone) 
        If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Number (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedNumber(ByVal vNumberFrom As Integer, ByVal vNumberTo As Integer) As csAlertMessageCol 
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized)  
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAlertMessage As csAlertMessage In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAlertMessage.Number > vNumberFrom AndAlso pAlertMessage.Number <= vNumberTo) Then 
        Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone() 
        pAlertMessages.Add(pAlertMessageClone) 
        If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardDescription(ByVal vDescription As String, ByVal vDescriptionWildcardType As clsEnums.enmWildCardType) As csAlertMessageCol 
    Dim pAlertMessages As New csAlertMessageCol 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAlertMessage As csAlertMessage In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
        If pAlertMessage.Description.StartsWith(vDescription, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAlertMessage.Description.EndsWith(vDescription, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAlertMessage.Description.IndexOf(vDescription, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vDescription.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAlertMessage.Description.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone() 
      pAlertMessages.Add(pAlertMessageClone) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pAlertMessages 
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
  Public Function FindByID(ByVal vID As Long) As csAlertMessage
    If Me.Count = 0 Then Return New csAlertMessage 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
    
    Dim pAlertMessage As csAlertMessage = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pAlertMessage) 
    If pAlertMessage IsNot Nothing Then Return pAlertMessage Else Return New csAlertMessage() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByNumber(ByVal vNumber As Integer) As csAlertMessage
    If Me.Count = 0 Then Return New csAlertMessage 
    
    If _RecreateDictionaryForFindByNumber = True Then LoadNumbers() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csAlertMessage) = _SortedDictionaryForFindByNumber 
    
    Dim pAlertMessage As csAlertMessage = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vNumber.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pAlertMessage) 
    If pAlertMessage IsNot Nothing Then Return pAlertMessage Else Return New csAlertMessage() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Number
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNumber(ByVal vNumber As Integer) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Number = vNumber Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNumber with vNumber of {vNumber}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Number = vNumber Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDescription(ByVal vDescription As String) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDescription = vDescription.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Description.ToLowerInvariant() = vDescription Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDescription with vDescription of {vDescription}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Description.ToLowerInvariant() = vDescription Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Type
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByType(ByVal vType As clsEnums.enmFaultType) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Type = vType Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByType with vType of {vType}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Type = vType Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Severity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySeverity(ByVal vSeverity As clsEnums.enmFaultSeverity) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Severity = vSeverity Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySeverity with vSeverity of {vSeverity}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Severity = vSeverity Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Message
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessage(ByVal vMessage As String) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vMessage = vMessage.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Message.ToLowerInvariant() = vMessage Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMessage with vMessage of {vMessage}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Message.ToLowerInvariant() = vMessage Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Action
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAction(ByVal vAction As String) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAction = vAction.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Action.ToLowerInvariant() = vAction Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAction with vAction of {vAction}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Action.ToLowerInvariant() = vAction Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAlertMessage) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In pTempDist.Values
        If pAlertMessage.Tag.ToLowerInvariant() = vTag Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Tag.ToLowerInvariant() = vTag Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessages.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pAlertMessages
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TypeAndSeverity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTypeAndSeverity(ByVal vType As clsEnums.enmFaultType, ByVal vSeverity As clsEnums.enmFaultSeverity) As csAlertMessageCol
    Dim pAlertMessages As New csAlertMessageCol(_IsLocalized) 
    pAlertMessages._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pAlertMessage As csAlertMessage In _SortedDictionaryForFindByID.Values.ToList()
        If pAlertMessage.Type = vType AndAlso pAlertMessage.Severity = vSeverity Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csAlertMessageCol = Me.Clone() 
      For Each pAlertMessage As csAlertMessage In pList 
        If pAlertMessage.Type = vType AndAlso pAlertMessage.Severity = vSeverity Then
          Dim pAlertMessageClone As csAlertMessage = pAlertMessage.Clone()
          pAlertMessages.Add(pAlertMessageClone)
          If Not _FilledFromSumOnTheFly Then pAlertMessages._Clean.Add(pAlertMessage.ID) 
        End If
      Next
    End If 
    Return pAlertMessages
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
    For Each pAlertMessage As csAlertMessage In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pAlertMessage.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageView, "csAlertMessageCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csAlertMessage In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csAlertMessage = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pAlertMessageToKill As New csAlertMessage 
          pAlertMessageToKill.ID = pCleanID 
          pAlertMessageToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pAlertMessageToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csAlertMessage In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-AlertMessage-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageUpdate, "csAlertMessageCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csAlertMessage In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csAlertMessage In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csAlertMessageCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Description 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByDescription(ByVal vDescription As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Description={0}", vDescription)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDeleteByDescription"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAlertMessages As New csAlertMessageCol() : pAllAlertMessages.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAlertMessages As csAlertMessageCol = pAllAlertMessages.CloneByDescription(vDescription) 
      For Each l In pFilteredAlertMessages 
        pAllAlertMessages.Remove(pAllAlertMessages.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAlertMessages, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "Description" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescription) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TypeAndSeverity 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTypeAndSeverity(ByVal vType As clsEnums.enmFaultType, ByVal vSeverity As clsEnums.enmFaultSeverity, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, Severity={1}", vType, vSeverity)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByTypeAndSeverity", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDeleteByType&Severity"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAlertMessages As New csAlertMessageCol() : pAllAlertMessages.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAlertMessages As csAlertMessageCol = pAllAlertMessages.CloneByTypeAndSeverity(vType, vSeverity) 
      For Each l In pFilteredAlertMessages 
        pAllAlertMessages.Remove(pAllAlertMessages.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAlertMessages, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmType_FaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vType) 
        pLastReadVariableName = "enmSeverity_FaultSeverity" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vSeverity) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AlertMessage-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vIDTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedDescription(ByVal vDescriptionFrom As String, ByVal vDescriptionTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("DescriptionFrom={0}, DescriptionTo={1}", vDescriptionFrom, vDescriptionTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByBoundedDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDeleteByBoundedDescription"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AlertMessage-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "DescriptionFrom" 
        pDALParameters.Add("bndDescriptionFrom", ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescriptionFrom) 
        pLastReadVariableName = "DescriptionTo" 
        pDALParameters.Add("bndDescriptionTo", ccDAL.enmSQLDataType.NVarChar, 100).Value = (vDescriptionTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Number
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedNumber(ByVal vNumberFrom As Integer, ByVal vNumberTo As Integer, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("NumberFrom={0}, NumberTo={1}", vNumberFrom, vNumberTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByBoundedNumber", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AlertMessagesDeleteByBoundedNumber"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AlertMessage-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "NumberFrom" 
        pDALParameters.Add("bndNumberFrom", ccDAL.enmSQLDataType.Int).Value = (vNumberFrom) 
        pLastReadVariableName = "NumberTo" 
        pDALParameters.Add("bndNumberTo", ccDAL.enmSQLDataType.Int).Value = (vNumberTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardDescription(ByVal vDescription As String, ByVal vDescriptionWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Description={0}, DescriptionWildcardType={1}", vDescription, vDescriptionWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AlertMessageDelete, "csAlertMessageCol_DeleteByWildCardDescription", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Description 
    Dim pWCDescription As String = "" 
    If vDescriptionWildcardType = clsEnums.enmWildCardType.After Then 
      pWCDescription = vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCDescription = "%" & vDescription 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCDescription = "%" & vDescription & "%" 
    ElseIf vDescriptionWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vDescription.ToCharArray 
        pWCDescription &= p & "%" 
      Next 
      pWCDescription = "%" & pWCDescription 
    End If 
    
    Dim pCommandText As String = "c_AlertMessagesDeleteByWildCardDescription"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AlertMessage-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldDescription" 
        pDALParameters.Add("wldDescription", ccDAL.enmSQLDataType.NVarChar, 100).Value = (pWCDescription) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AlertMessage-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AlertMessage-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-090219-1632", vRequester) 
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
    Me.Sort(New csAlertMessageCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
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
  
  Public Sub SortByNumber()
    Me.Sort(New csAlertMessageCol.CompareByNumber)
  End Sub
  Private Class CompareByNumber
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Number < y.Number Then
        Return -1
      ElseIf x.Number = y.Number Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDescription()
    Me.Sort(New csAlertMessageCol.CompareByDescription)
  End Sub
  Private Class CompareByDescription
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Description, y.Description, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByType()
    Me.Sort(New csAlertMessageCol.CompareByType)
  End Sub
  Private Class CompareByType
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Type < y.Type Then
        Return -1
      ElseIf x.Type = y.Type Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTypeText()
    Me.Sort(New csAlertMessageCol.CompareByTypeText)
  End Sub
  Private Class CompareByTypeText
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TypeText, y.TypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySeverity()
    Me.Sort(New csAlertMessageCol.CompareBySeverity)
  End Sub
  Private Class CompareBySeverity
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Severity < y.Severity Then
        Return -1
      ElseIf x.Severity = y.Severity Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySeverityText()
    Me.Sort(New csAlertMessageCol.CompareBySeverityText)
  End Sub
  Private Class CompareBySeverityText
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SeverityText, y.SeverityText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByMessage()
    Me.Sort(New csAlertMessageCol.CompareByMessage)
  End Sub
  Private Class CompareByMessage
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Message, y.Message, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByMessageLocalized()
    Me.Sort(New csAlertMessageCol.CompareByMessageLocalized)
  End Sub
  Private Class CompareByMessageLocalized
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.MessageLocalized, y.MessageLocalized, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAction()
    Me.Sort(New csAlertMessageCol.CompareByAction)
  End Sub
  Private Class CompareByAction
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Action, y.Action, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActionLocalized()
    Me.Sort(New csAlertMessageCol.CompareByActionLocalized)
  End Sub
  Private Class CompareByActionLocalized
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ActionLocalized, y.ActionLocalized, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csAlertMessageCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csAlertMessage)
    Private Function Compare(ByVal x As csAlertMessage, ByVal y As csAlertMessage) As Integer Implements System.Collections.Generic.IComparer(Of csAlertMessage).Compare
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
  
    Dim pAlertMessage As csAlertMessage
  
    While vReader.Read()
      pAlertMessage = New csAlertMessage(_IsLocalized) 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pAlertMessage.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
      pFault = pAlertMessage.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pAlertMessage)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pAlertMessage.ID) 
    End While
    If _IsLocalized = True AndAlso _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
      _LocalizedLanguage = vRequester.UILang 
    End If 
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedAlertMessageCol As csAlertMessageCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pAlertMessage As csAlertMessage 
 
      For Each pCachedAlertMessage As csAlertMessage In vCachedAlertMessageCol 
        pCachedAlertMessage.SetLocalizable(_IsLocalized) 
        pAlertMessage = New csAlertMessage(pCachedAlertMessage) 
        If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
          pAlertMessage.OverrideDefaultLanguage(_LocalizedLanguage) 
        End If 
        pFault = pAlertMessage.LoadTranslations(vRequester) 
        If pFault.isOK = False Then Return pFault 
        pAlertMessage.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pAlertMessage) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pAlertMessage.ID) 
      Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
      _LocalizedLanguage = vRequester.UILang 
    End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AlertMessage-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAlertMessage) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByNumber = New Dictionary(Of String, csAlertMessage)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByNumber = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAlertMessage) 
    _SortedDictionaryForFindByNumber = New Dictionary(Of String, csAlertMessage)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      _IsLocalized = False 
      _LocalizedLanguage = clsEnums.enmLanguage.UD 
      bHasParents = False 
      bHasLocalizedFields = True 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
