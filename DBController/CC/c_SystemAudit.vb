Public Class csSystemAudit
  Inherits cTargCCEntity 
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
  
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [TableName] 
    [RowId] 
    [Operation] 
    [OccurredAt] 
    [SqlCurrentUser] 
    [ChangedByUser] 
    [ActiveLoginID] 
    [SqlSystemUser] 
    [SqlAppName] 
    [SqlHostName] 
    [Changes] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [RowId] 
    [ActiveLoginID] 
  End Enum 
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
  
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _TableName As String
  Private _RowId As Long
  Private _Operation As String
  Private _OccurredAt As Date
  Private _SqlCurrentUser As String
  Private _ChangedByUser As String
  Private _ActiveLoginID As Long
  Private _SqlSystemUser As String
  Private _SqlAppName As String
  Private _SqlHostName As String
  Private _Changes As String
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
  Public Property [TableName]() As String
    Get
      Return Me._TableName
    End Get
    Set(ByVal value As String)
      If Me._TableName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TableName = value 
      End If 
    End Set
  End Property
  Public Property [RowId]() As Long
    Get
      Return Me._RowId
    End Get
    Set(ByVal value As Long)
      If Me._RowId <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RowId = value 
      End If 
    End Set
  End Property
  Public Property [Operation]() As String
    Get
      Return Me._Operation
    End Get
    Set(ByVal value As String)
      If Me._Operation <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Operation = value 
      End If 
    End Set
  End Property
  Public Property [OccurredAt]() As Date
    Get
      Return Me._OccurredAt
    End Get
    Set(ByVal value As Date)
      If Me._OccurredAt <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OccurredAt = value 
      End If 
    End Set
  End Property
  Public Property [SqlCurrentUser]() As String
    Get
      Return Me._SqlCurrentUser
    End Get
    Set(ByVal value As String)
      If Me._SqlCurrentUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlCurrentUser = value 
      End If 
    End Set
  End Property
  Public Property [ChangedByUser]() As String
    Get
      Return Me._ChangedByUser
    End Get
    Set(ByVal value As String)
      If Me._ChangedByUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ChangedByUser = value 
      End If 
    End Set
  End Property
  Public Property [ActiveLoginID]() As Long
    Get
      Return Me._ActiveLoginID
    End Get
    Set(ByVal value As Long)
      If Me._ActiveLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ActiveLoginID = value 
      End If 
    End Set
  End Property
  Public Property [SqlSystemUser]() As String
    Get
      Return Me._SqlSystemUser
    End Get
    Set(ByVal value As String)
      If Me._SqlSystemUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlSystemUser = value 
      End If 
    End Set
  End Property
  Public Property [SqlAppName]() As String
    Get
      Return Me._SqlAppName
    End Get
    Set(ByVal value As String)
      If Me._SqlAppName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlAppName = value 
      End If 
    End Set
  End Property
  Public Property [SqlHostName]() As String
    Get
      Return Me._SqlHostName
    End Get
    Set(ByVal value As String)
      If Me._SqlHostName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SqlHostName = value 
      End If 
    End Set
  End Property
  Public Property [Changes]() As String
    Get
      Return Me._Changes
    End Get
    Set(ByVal value As String)
      If Me._Changes <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Changes = value 
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
    bDefaultDesignation = "" 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _TableName <> "" Then pValue.Append("TableName='" & _TableName & "' ‡ ") 
    If _RowId <> 0 Then pValue.Append("RowId='" & _RowId.ToString() & "' ‡ ") 
    If _Operation <> "" Then pValue.Append("Operation='" & _Operation & "' ‡ ") 
    If Not (_OccurredAt = Nothing) Then pValue.Append("OccurredAt='" & _OccurredAt.ToString("o") & "' ‡ ") 
    If _SqlCurrentUser <> "" Then pValue.Append("SqlCurrentUser='" & _SqlCurrentUser & "' ‡ ") 
    If _ChangedByUser <> "" Then pValue.Append("ChangedByUser='" & _ChangedByUser & "' ‡ ") 
    If _ActiveLoginID <> 0 Then pValue.Append("ActiveLoginID='" & _ActiveLoginID.ToString() & "' ‡ ") 
    If _SqlSystemUser <> "" Then pValue.Append("SqlSystemUser='" & _SqlSystemUser & "' ‡ ") 
    If _SqlAppName <> "" Then pValue.Append("SqlAppName='" & _SqlAppName & "' ‡ ") 
    If _SqlHostName <> "" Then pValue.Append("SqlHostName='" & _SqlHostName & "' ‡ ") 
    If _Changes <> "" Then pValue.Append("Changes='" & _Changes & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TableName)}""") 
    pCSV.Append("," & _RowId.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Operation)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OccurredAt.ToShortDateString & " " & _OccurredAt.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlCurrentUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ChangedByUser)}""") 
    pCSV.Append("," & _ActiveLoginID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlSystemUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlAppName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlHostName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Changes)}""") 
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
  
  Public Sub New(ByVal vcsSystemAudit As csSystemAudit)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsSystemAudit) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vTableName As String = "" _ 
    , Optional vRowId As Long = 0 _ 
    , Optional vOperation As String = "" _ 
    , Optional vOccurredAt As Date = Nothing _ 
    , Optional vSqlCurrentUser As String = "" _ 
    , Optional vChangedByUser As String = "" _ 
    , Optional vActiveLoginID As Long = 0 _ 
    , Optional vSqlSystemUser As String = "" _ 
    , Optional vSqlAppName As String = "" _ 
    , Optional vSqlHostName As String = "" _ 
    , Optional vChanges As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _TableName = vTableName 
    _RowId = vRowId 
    _Operation = vOperation 
    _OccurredAt = vOccurredAt 
    _SqlCurrentUser = vSqlCurrentUser 
    _ChangedByUser = vChangedByUser 
    _ActiveLoginID = vActiveLoginID 
    _SqlSystemUser = vSqlSystemUser 
    _SqlAppName = vSqlAppName 
    _SqlHostName = vSqlHostName 
    _Changes = vChanges 
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
 
    _TableName = _TableName.Truncate(pTruncateLength, _IsTruncated) 
    _Operation = _Operation.Truncate(pTruncateLength, _IsTruncated) 
    _SqlCurrentUser = _SqlCurrentUser.Truncate(pTruncateLength, _IsTruncated) 
    _ChangedByUser = _ChangedByUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlSystemUser = _SqlSystemUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlAppName = _SqlAppName.Truncate(pTruncateLength, _IsTruncated) 
    _SqlHostName = _SqlHostName.Truncate(pTruncateLength, _IsTruncated) 
    _Changes = _Changes.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _TableName = ccHelper.RemoveChrW0(_TableName) 
    _Operation = ccHelper.RemoveChrW0(_Operation) 
    _SqlCurrentUser = ccHelper.RemoveChrW0(_SqlCurrentUser) 
    _ChangedByUser = ccHelper.RemoveChrW0(_ChangedByUser) 
    _SqlSystemUser = ccHelper.RemoveChrW0(_SqlSystemUser) 
    _SqlAppName = ccHelper.RemoveChrW0(_SqlAppName) 
    _SqlHostName = ccHelper.RemoveChrW0(_SqlHostName) 
    _Changes = ccHelper.RemoveChrW0(_Changes) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SystemAudit by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAudit_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemAudit-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SystemAudit by the chosen parameters. This function may be a bit slower than accessing the SystemAudit's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAudit_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SystemAudit-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemAudit-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the SystemAudit by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAudit_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"SystemAudit not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-SystemAudit-210927-1527", vRequester, vAdditionalMessageToUser:=$"SystemAudit not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemAuditCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemAuditCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemAuditCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccSystemAuditCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemAuditGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"SystemAudit not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-SystemAudit-210625-0950", vRequester, vAdditionalMessageToUser:=$"SystemAudit not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("SystemAudit.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditDelete, "csSystemAudit_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_SystemAuditDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemAuditCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemAuditCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemAuditCol failed: " & pResponse) 
      MyController.DBCache.ccSystemAuditCol.Remove(MyController.DBCache.ccSystemAuditCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemAuditCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemAudit-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemAudit-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditDelete, "csSystemAudit_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_SystemAuditDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemAuditCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemAuditCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemAuditCol failed: " & pResponse) 
      MyController.DBCache.ccSystemAuditCol.Remove(MyController.DBCache.ccSystemAuditCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemAuditCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemAudit-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemAudit-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csSystemAudit) Then Return False 
    Dim pSystemAuditToTest As csSystemAudit = CType(vTargCCEntityToTest, csSystemAudit) 
    Return isEqual(pSystemAuditToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vSystemAuditToTest As csSystemAudit) As Boolean
    With vSystemAuditToTest
      If _ID <> .ID Then Return False
      If _TableName <> .TableName Then Return False
      If _RowId <> .RowId Then Return False
      If _Operation <> .Operation Then Return False
      If _OccurredAt <> Nothing AndAlso .OccurredAt <> Nothing Then 
        If ccHelper.ToLong(_OccurredAt.Subtract(.OccurredAt).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_OccurredAt = Nothing AndAlso .OccurredAt = Nothing) Then 
        Return False 
      End If 
      If _SqlCurrentUser <> .SqlCurrentUser Then Return False
      If _ChangedByUser <> .ChangedByUser Then Return False
      If _ActiveLoginID <> .ActiveLoginID Then Return False
      If _SqlSystemUser <> .SqlSystemUser Then Return False
      If _SqlAppName <> .SqlAppName Then Return False
      If _SqlHostName <> .SqlHostName Then Return False
      If _Changes <> .Changes Then Return False
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
    Dim pClone As New csSystemAudit(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csSystemAudit
    Dim pClone As New csSystemAudit(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("TableName") = _TableName : Catch ex As Exception : Return pFault.LogException(ex, "TableName", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("RowId") = _RowId : Catch ex As Exception : Return pFault.LogException(ex, "RowId", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("Operation") = _Operation : Catch ex As Exception : Return pFault.LogException(ex, "Operation", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("OccurredAt") = _OccurredAt : Catch ex As Exception : Return pFault.LogException(ex, "OccurredAt", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlCurrentUser") = _SqlCurrentUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlCurrentUser", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("ChangedByUser") = _ChangedByUser : Catch ex As Exception : Return pFault.LogException(ex, "ChangedByUser", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActiveLoginID") = _ActiveLoginID : Catch ex As Exception : Return pFault.LogException(ex, "ActiveLoginID", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlSystemUser") = _SqlSystemUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlSystemUser", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlAppName") = _SqlAppName : Catch ex As Exception : Return pFault.LogException(ex, "SqlAppName", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlHostName") = _SqlHostName : Catch ex As Exception : Return pFault.LogException(ex, "SqlHostName", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
    Try : vDataRow("Changes") = _Changes : Catch ex As Exception : Return pFault.LogException(ex, "Changes", "TRGT-SystemAudit-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pSystemAudit As csSystemAudit = CType(pXmlSerializer.Deserialize(pStreamReader), csSystemAudit) 
      AssignValues(pSystemAudit) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-SystemAudit-130515-1230", vRequester) 
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
          'TableName 
          If _TableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_TableName) 
          'RowId 
          pBinaryWriter.Write(_RowId) 
          'Operation 
          If _Operation Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Operation) 
          'OccurredAt 
          pBinaryWriter.Write(_OccurredAt.Ticks) 
          'SqlCurrentUser 
          If _SqlCurrentUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlCurrentUser) 
          'ChangedByUser 
          If _ChangedByUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ChangedByUser) 
          'ActiveLoginID 
          pBinaryWriter.Write(_ActiveLoginID) 
          'SqlSystemUser 
          If _SqlSystemUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlSystemUser) 
          'SqlAppName 
          If _SqlAppName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlAppName) 
          'SqlHostName 
          If _SqlHostName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SqlHostName) 
          'Changes 
          If _Changes Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Changes) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-150307-2338", vRequester) 
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
          'TableName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _TableName = pReader.ReadString 
          'RowId 
          _RowId = pReader.ReadInt64 
          'Operation 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Operation = pReader.ReadString 
          'OccurredAt 
          _OccurredAt = New Date(pReader.ReadInt64) 
          'SqlCurrentUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlCurrentUser = pReader.ReadString 
          'ChangedByUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ChangedByUser = pReader.ReadString 
          'ActiveLoginID 
          _ActiveLoginID = pReader.ReadInt64 
          'SqlSystemUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlSystemUser = pReader.ReadString 
          'SqlAppName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlAppName = pReader.ReadString 
          'SqlHostName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlHostName = pReader.ReadString 
          'Changes 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Changes = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-SystemAudit-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-190720-1443", vRequester) 
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
 
      Dim pSystemAudit As csSystemAudit = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csSystemAudit)(vJSON, pSettings) 
      AssignValues(pSystemAudit) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vSystemAudit As csSystemAudit)
    With vSystemAudit
      _ID = .ID 
      _TableName = .TableName 
      _RowId = .RowId 
      _Operation = .Operation 
      _OccurredAt = .OccurredAt 
      _SqlCurrentUser = .SqlCurrentUser 
      _ChangedByUser = .ChangedByUser 
      _ActiveLoginID = .ActiveLoginID 
      _SqlSystemUser = .SqlSystemUser 
      _SqlAppName = .SqlAppName 
      _SqlHostName = .SqlHostName 
      _Changes = .Changes 
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
 
    'There are no enums or lookups. This function was added to this object for interface compatibility 
    Return pFault.SetOK() 
  End Function 
 
#Region "Load Entity" 
  Friend Function LoadMeFromIDataReader(vReader As IDataReader, vRequester As clsRequester) As clsFault Implements ITargCCDataReaderUser.LoadMeFromIDataReader 
    Dim pFunctionParameters As String = ""  
    Dim pFault As New clsFault
  
    Dim pLastReadVariableName As String = "" 
    Try
      pLastReadVariableName = "ID" 
      If Not vReader.IsDBNull(0) Then _ID = vReader.GetInt64(0)
      pLastReadVariableName = "TableName" 
      If Not vReader.IsDBNull(1) Then _TableName = vReader.GetString(1) 
      pLastReadVariableName = "RowId" 
      If Not vReader.IsDBNull(2) Then _RowId = vReader.GetInt64(2)
      pLastReadVariableName = "Operation" 
      If Not vReader.IsDBNull(3) Then _Operation = vReader.GetString(3) 
      pLastReadVariableName = "OccurredAt" 
      If Not vReader.IsDBNull(4) Then _OccurredAt = vReader.GetDateTime(4)
      pLastReadVariableName = "SqlCurrentUser" 
      If Not vReader.IsDBNull(5) Then _SqlCurrentUser = vReader.GetString(5) 
      pLastReadVariableName = "ChangedByUser" 
      If Not vReader.IsDBNull(6) Then _ChangedByUser = vReader.GetString(6) 
      pLastReadVariableName = "ActiveLoginID" 
      If Not vReader.IsDBNull(7) Then _ActiveLoginID = vReader.GetInt64(7)
      pLastReadVariableName = "SqlSystemUser" 
      If Not vReader.IsDBNull(8) Then _SqlSystemUser = vReader.GetString(8) 
      pLastReadVariableName = "SqlAppName" 
      If Not vReader.IsDBNull(9) Then _SqlAppName = vReader.GetString(9) 
      pLastReadVariableName = "SqlHostName" 
      If Not vReader.IsDBNull(10) Then _SqlHostName = vReader.GetString(10) 
      pLastReadVariableName = "Changes" 
      If Not vReader.IsDBNull(11) Then _Changes = vReader.GetString(11) 
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedSystemAudit As csSystemAudit, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedSystemAudit) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _TableName = ""
    _RowId = 0
    _Operation = ""
    _OccurredAt = Nothing
    _SqlCurrentUser = ""
    _ChangedByUser = ""
    _ActiveLoginID = 0
    _SqlSystemUser = ""
    _SqlAppName = ""
    _SqlHostName = ""
    _Changes = ""
    _Tag = ""
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
  
Public Class csSystemAuditCol
  Inherits cTargCCCollection(Of csSystemAudit)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csSystemAudit) 
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
 
    For Each pRow As csSystemAudit In Me 
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
    pCSVTitle.Append(",""TableName""") 
    pCSVTitle.Append(",""RowId""") 
    pCSVTitle.Append(",""Operation""") 
    pCSVTitle.Append(",""OccurredAt""") 
    pCSVTitle.Append(",""SqlCurrentUser""") 
    pCSVTitle.Append(",""ChangedByUser""") 
    pCSVTitle.Append(",""ActiveLoginID""") 
    pCSVTitle.Append(",""SqlSystemUser""") 
    pCSVTitle.Append(",""SqlAppName""") 
    pCSVTitle.Append(",""SqlHostName""") 
    pCSVTitle.Append(",""Changes""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csSystemAudit In Me 
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
 
  Public Overloads Sub Add(ByVal vSystemAudit As csSystemAudit) 
    SyncLock _CollectionLock 
      MyBase.Add(vSystemAudit) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vSystemAudit As csSystemAudit) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vSystemAudit) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vSystemAuditCol As csSystemAuditCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vSystemAuditCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vSystemAudit As csSystemAudit) 
    SyncLock _CollectionLock 
      MyBase.Remove(vSystemAudit) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csSystemAudit) 
      
      For Each lSystemAudit In Me 
        If lSystemAudit.IsEmpty OrElse pTempDictionary.ContainsKey(lSystemAudit.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lSystemAudit.ID, lSystemAudit) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lSystemAudit.ToString, "TRGT-SystemAudit-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", SystemAudit:" & lSystemAudit.ToString() & ", TRGT-SystemAudit-260111-154657") 'Send it up the line 
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
 
    For Each lSystemAudit As csSystemAudit In Me 
      lSystemAudit.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lSystemAudit As csSystemAudit In Me 
      lSystemAudit.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the SystemAudits by the chosen parameters. This function may be a bit slower than accessing the SystemAudit's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SystemAudit-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemAudit-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemAuditCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemAuditCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemAuditCol failed: " & pResponse) 
      Dim pSystemAuditsCached As csSystemAuditCol = MyController.DBCache.ccSystemAuditCol.Clone() 
      pSystemAuditsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pSystemAuditsCached.Reverse() 
      If vHowMany > 0 AndAlso pSystemAuditsCached.Count > vHowMany Then 
        Dim tmp As New csSystemAuditCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pSystemAuditsCached(i)) 
        Next 
        pSystemAuditsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pSystemAuditsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemAuditsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemAuditCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemAuditCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemAuditCol failed: " & pResponse) 
      Dim pSystemAuditsCached As csSystemAuditCol = MyController.DBCache.ccSystemAuditCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pSystemAuditsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemAuditsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lSystemAudit As New csSystemAudit() 
      pFault = lSystemAudit.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lSystemAudit.IsEmpty Then Me.Add(lSystemAudit) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pSystemAudits As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pSystemAudits, "csSystemAuditCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pSystemAudits IsNot Nothing AndAlso Me.Count <> pSystemAudits.Count Then FillFromListOfITargCCEntity(pSystemAudits) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
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
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-SystemAudit-121122-2008", vRequester) 
      Dim pSystemAuditsCached As csSystemAuditCol = MyController.DBCache.ccSystemAuditCol.Clone() 
      Dim pSystemAuditsToUse As New csSystemAuditCol() 
      For Each l In pSystemAuditsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        pSystemAuditsToUse.Add(l) 
      Next 
      pSystemAuditsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pSystemAuditsToUse.Reverse() 
      If vHowMany > 0 AndAlso pSystemAuditsToUse.Count > vHowMany Then 
        Dim tmp As New csSystemAuditCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pSystemAuditsToUse(i)) 
        Next 
        pSystemAuditsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pSystemAuditsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemAuditsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
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
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}", vIDFrom, vIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditView, "csSystemAuditCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-SystemAudit-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemAuditsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vSystemAuditArray As csSystemAudit())
    Me.Clear()
    
    For Each pSystemAudit As csSystemAudit In vSystemAuditArray
      Me.Add(pSystemAudit)
      _Clean.Add(pSystemAudit.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pSystemAudit As New csSystemAudit(pRow, vRequester) 
        Me.Add(pSystemAudit) 
        _Clean.Add(pSystemAudit.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-SystemAuditCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-130515-1300", vRequester) 
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
      Dim pSystemAudits As csSystemAuditCol = CType(pXmlSerializer.Deserialize(pStreamReader), csSystemAuditCol) 
      For Each pSystemAudit As csSystemAudit In pSystemAudits 
        Me.Add(pSystemAudit) 
        _Clean.Add(pSystemAudit.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-SystemAudit-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-190720-1443", vRequester) 
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
 
      Dim pSystemAudits As List(Of csSystemAudit) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csSystemAudit))(vJSON, pSettings) 
      For Each pSystemAudit As csSystemAudit In pSystemAudits 
        Me.Add(pSystemAudit) 
        _Clean.Add(pSystemAudit.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-190720-2059", vRequester) 
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
          For Each lSystemAudit As csSystemAudit In Me 
            Dim pByte As Byte() = lSystemAudit.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-150307-2340", vRequester) 
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
            Dim pSystemAudit As csSystemAudit = New csSystemAudit(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pSystemAudit) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pSystemAudit.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-SystemAudit-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pSystemAudit As csSystemAudit In Me 
      With pSystemAudit 
        pFault = pSystemAudit.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csSystemAuditCol) Then Return False 
    Dim pSystemAuditColToTest As csSystemAuditCol = CType(vEntitiesToTest, csSystemAuditCol) 
    Return isEqual(pSystemAuditColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vSystemAuditsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vSystemAuditsToTest As csSystemAuditCol) As Boolean
    If Me.Count <> vSystemAuditsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vSystemAuditsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSystemAudits As New csSystemAuditCol() 
    If pFilledFromSumOnTheFly Then pSystemAudits._FilledFromSumOnTheFly = True
    
    For Each pSystemAudit As csSystemAudit In Me 
      Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone() 
      pSystemAudits.Add(pSystemAuditClone) 
      If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
    Next 
    Return pSystemAudits 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csSystemAuditCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSystemAudits As New csSystemAuditCol() 
    If pFilledFromSumOnTheFly Then pSystemAudits._FilledFromSumOnTheFly = True
    
    For Each pSystemAudit As csSystemAudit In Me
      Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
      pSystemAudits.Add(pSystemAuditClone)
      If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
    Next
    Return pSystemAudits
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csSystemAuditCol 
    Dim pSystemAudits As New csSystemAuditCol()  
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemAudit As csSystemAudit In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSystemAudit.ID > vIDFrom AndAlso pSystemAudit.ID <= vIDTo) Then 
        Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone() 
        pSystemAudits.Add(pSystemAuditClone) 
        If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
      End If 
    Next 
    Return pSystemAudits 
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
  Public Function FindByID(ByVal vID As Long) As csSystemAudit
    If Me.Count = 0 Then Return New csSystemAudit 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
    
    Dim pSystemAudit As csSystemAudit = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pSystemAudit) 
    If pSystemAudit IsNot Nothing Then Return pSystemAudit Else Return New csSystemAudit() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableName(ByVal vTableName As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTableName = vTableName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.TableName.ToLowerInvariant() = vTableName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTableName with vTableName of {vTableName}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.TableName.ToLowerInvariant() = vTableName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RowId
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRowId(ByVal vRowId As Long) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.RowId = vRowId Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRowId with vRowId of {vRowId}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.RowId = vRowId Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Operation
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOperation(ByVal vOperation As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOperation = vOperation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.Operation.ToLowerInvariant() = vOperation Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOperation with vOperation of {vOperation}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.Operation.ToLowerInvariant() = vOperation Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OccurredAt
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOccurredAt(ByVal vOccurredAt As Date) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.OccurredAt = vOccurredAt Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOccurredAt with vOccurredAt of {vOccurredAt}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.OccurredAt = vOccurredAt Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlCurrentUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlCurrentUser(ByVal vSqlCurrentUser As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlCurrentUser = vSqlCurrentUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlCurrentUser with vSqlCurrentUser of {vSqlCurrentUser}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ChangedByUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByChangedByUser(ByVal vChangedByUser As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vChangedByUser = vChangedByUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByChangedByUser with vChangedByUser of {vChangedByUser}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActiveLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActiveLoginID(ByVal vActiveLoginID As Long) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.ActiveLoginID = vActiveLoginID Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActiveLoginID with vActiveLoginID of {vActiveLoginID}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.ActiveLoginID = vActiveLoginID Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlSystemUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlSystemUser(ByVal vSqlSystemUser As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlSystemUser = vSqlSystemUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlSystemUser with vSqlSystemUser of {vSqlSystemUser}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlAppName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlAppName(ByVal vSqlAppName As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlAppName = vSqlAppName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlAppName with vSqlAppName of {vSqlAppName}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlHostName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlHostName(ByVal vSqlHostName As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlHostName = vSqlHostName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlHostName with vSqlHostName of {vSqlHostName}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Changes
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByChanges(ByVal vChanges As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vChanges = vChanges.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.Changes.ToLowerInvariant() = vChanges Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByChanges with vChanges of {vChanges}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.Changes.ToLowerInvariant() = vChanges Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csSystemAuditCol
    Dim pSystemAudits As New csSystemAuditCol() 
    pSystemAudits._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemAudit) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemAudit As csSystemAudit In pTempDist.Values
        If pSystemAudit.Tag.ToLowerInvariant() = vTag Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csSystemAuditCol = Me.Clone() 
      For Each pSystemAudit As csSystemAudit In pList 
        If pSystemAudit.Tag.ToLowerInvariant() = vTag Then
          Dim pSystemAuditClone As csSystemAudit = pSystemAudit.Clone()
          pSystemAudits.Add(pSystemAuditClone)
          If Not _FilledFromSumOnTheFly Then pSystemAudits._Clean.Add(pSystemAudit.ID) 
        End If
      Next
    End If 
    
    Return pSystemAudits
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
    For Each pSystemAudit As csSystemAudit In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pSystemAudit.LoadDataRow(pRow, vRequester) 
      If pFault.isOK = False Then Return pFault 
      vDataTable.Rows.Add(pRow) 
    Next 
 
    Return pFault.SetOK 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditDelete, "csSystemAuditCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemAuditsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csSystemAuditCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemAudit-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemAudit-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit--090624-1625", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemAuditDelete, "csSystemAuditCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemAuditsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemAudit-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemAudit-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemAudit-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-090210-1341", vRequester) 
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
    Me.Sort(New csSystemAuditCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
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
  
  Public Sub SortByTableName()
    Me.Sort(New csSystemAuditCol.CompareByTableName)
  End Sub
  Private Class CompareByTableName
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRowId()
    Me.Sort(New csSystemAuditCol.CompareByRowId)
  End Sub
  Private Class CompareByRowId
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RowId < y.RowId Then
        Return -1
      ElseIf x.RowId = y.RowId Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByOperation()
    Me.Sort(New csSystemAuditCol.CompareByOperation)
  End Sub
  Private Class CompareByOperation
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Operation, y.Operation, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOccurredAt()
    Me.Sort(New csSystemAuditCol.CompareByOccurredAt)
  End Sub
  Private Class CompareByOccurredAt
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OccurredAt < y.OccurredAt Then
        Return -1
      ElseIf x.OccurredAt = y.OccurredAt Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySqlCurrentUser()
    Me.Sort(New csSystemAuditCol.CompareBySqlCurrentUser)
  End Sub
  Private Class CompareBySqlCurrentUser
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlCurrentUser, y.SqlCurrentUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByChangedByUser()
    Me.Sort(New csSystemAuditCol.CompareByChangedByUser)
  End Sub
  Private Class CompareByChangedByUser
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ChangedByUser, y.ChangedByUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActiveLoginID()
    Me.Sort(New csSystemAuditCol.CompareByActiveLoginID)
  End Sub
  Private Class CompareByActiveLoginID
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ActiveLoginID < y.ActiveLoginID Then
        Return -1
      ElseIf x.ActiveLoginID = y.ActiveLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySqlSystemUser()
    Me.Sort(New csSystemAuditCol.CompareBySqlSystemUser)
  End Sub
  Private Class CompareBySqlSystemUser
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlSystemUser, y.SqlSystemUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlAppName()
    Me.Sort(New csSystemAuditCol.CompareBySqlAppName)
  End Sub
  Private Class CompareBySqlAppName
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlAppName, y.SqlAppName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlHostName()
    Me.Sort(New csSystemAuditCol.CompareBySqlHostName)
  End Sub
  Private Class CompareBySqlHostName
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlHostName, y.SqlHostName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByChanges()
    Me.Sort(New csSystemAuditCol.CompareByChanges)
  End Sub
  Private Class CompareByChanges
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Changes, y.Changes, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csSystemAuditCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csSystemAudit)
    Private Function Compare(ByVal x As csSystemAudit, ByVal y As csSystemAudit) As Integer Implements System.Collections.Generic.IComparer(Of csSystemAudit).Compare
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
  
    Dim pSystemAudit As csSystemAudit
  
    While vReader.Read()
      pSystemAudit = New csSystemAudit() 
      pFault = pSystemAudit.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pSystemAudit)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pSystemAudit.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedSystemAuditCol As csSystemAuditCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pSystemAudit As csSystemAudit 
 
      For Each pCachedSystemAudit As csSystemAudit In vCachedSystemAuditCol 
        pSystemAudit = New csSystemAudit(pCachedSystemAudit) 
        pSystemAudit.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pSystemAudit) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pSystemAudit.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemAudit-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csSystemAudit) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csSystemAudit) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
