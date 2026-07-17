Public Class csAuditIndexed
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
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
    [OriginalID] 
    [TableName] 
    [RowID] 
    [Operation] 
    [OccurredAt] 
    [SqlCurrentUser] 
    [FieldName] 
    [OldValue] 
    [NewValue] 
    [ChangedByUser] 
    [ActiveLoginID] 
    [SqlSystemUser] 
    [SqlAppName] 
    [SqlHostName] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [OriginalID] 
    [RowID] 
    [ActiveLoginID] 
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
  
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _OriginalID As Long
  Private _TableName As String
  Private _RowID As Long
  Private _Operation As String
  Private _OccurredAt As Date
  Private _SqlCurrentUser As String
  Private _FieldName As String
  Private _OldValue As String
  Private _NewValue As String
  Private _ChangedByUser As String
  Private _ActiveLoginID As Long
  Private _SqlSystemUser As String
  Private _SqlAppName As String
  Private _SqlHostName As String
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
  Public Property [OriginalID]() As Long
    Get
      Return Me._OriginalID
    End Get
    Set(ByVal value As Long)
      If Me._OriginalID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OriginalID = value 
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
  Public Property [RowID]() As Long
    Get
      Return Me._RowID
    End Get
    Set(ByVal value As Long)
      If Me._RowID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RowID = value 
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
  Public Property [FieldName]() As String
    Get
      Return Me._FieldName
    End Get
    Set(ByVal value As String)
      If Me._FieldName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FieldName = value 
      End If 
    End Set
  End Property
  Public Property [OldValue]() As String
    Get
      Return Me._OldValue
    End Get
    Set(ByVal value As String)
      If Me._OldValue <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._OldValue = value 
      End If 
    End Set
  End Property
  Public Property [NewValue]() As String
    Get
      Return Me._NewValue
    End Get
    Set(ByVal value As String)
      If Me._NewValue <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._NewValue = value 
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
    If _OriginalID <> 0 Then pValue.Append("OriginalID='" & _OriginalID.ToString() & "' ‡ ") 
    If _TableName <> "" Then pValue.Append("TableName='" & _TableName & "' ‡ ") 
    If _RowID <> 0 Then pValue.Append("RowID='" & _RowID.ToString() & "' ‡ ") 
    If _Operation <> "" Then pValue.Append("Operation='" & _Operation & "' ‡ ") 
    If Not (_OccurredAt = Nothing) Then pValue.Append("OccurredAt='" & _OccurredAt.ToString("o") & "' ‡ ") 
    If _SqlCurrentUser <> "" Then pValue.Append("SqlCurrentUser='" & _SqlCurrentUser & "' ‡ ") 
    If _FieldName <> "" Then pValue.Append("FieldName='" & _FieldName & "' ‡ ") 
    If _OldValue <> "" Then pValue.Append("OldValue='" & _OldValue & "' ‡ ") 
    If _NewValue <> "" Then pValue.Append("NewValue='" & _NewValue & "' ‡ ") 
    If _ChangedByUser <> "" Then pValue.Append("ChangedByUser='" & _ChangedByUser & "' ‡ ") 
    If _ActiveLoginID <> 0 Then pValue.Append("ActiveLoginID='" & _ActiveLoginID.ToString() & "' ‡ ") 
    If _SqlSystemUser <> "" Then pValue.Append("SqlSystemUser='" & _SqlSystemUser & "' ‡ ") 
    If _SqlAppName <> "" Then pValue.Append("SqlAppName='" & _SqlAppName & "' ‡ ") 
    If _SqlHostName <> "" Then pValue.Append("SqlHostName='" & _SqlHostName & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _OriginalID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TableName)}""") 
    pCSV.Append("," & _RowID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Operation)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OccurredAt.ToShortDateString & " " & _OccurredAt.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlCurrentUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FieldName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_OldValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_NewValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ChangedByUser)}""") 
    pCSV.Append("," & _ActiveLoginID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlSystemUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlAppName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SqlHostName)}""") 
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
  
  Public Sub New(ByVal vcsAuditIndexed As csAuditIndexed)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsAuditIndexed) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vOriginalID As Long = 0 _ 
    , Optional vTableName As String = "" _ 
    , Optional vRowID As Long = 0 _ 
    , Optional vOperation As String = "" _ 
    , Optional vOccurredAt As Date = Nothing _ 
    , Optional vSqlCurrentUser As String = "" _ 
    , Optional vFieldName As String = "" _ 
    , Optional vOldValue As String = "" _ 
    , Optional vNewValue As String = "" _ 
    , Optional vChangedByUser As String = "" _ 
    , Optional vActiveLoginID As Long = 0 _ 
    , Optional vSqlSystemUser As String = "" _ 
    , Optional vSqlAppName As String = "" _ 
    , Optional vSqlHostName As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _OriginalID = vOriginalID 
    _TableName = vTableName 
    _RowID = vRowID 
    _Operation = vOperation 
    _OccurredAt = vOccurredAt 
    _SqlCurrentUser = vSqlCurrentUser 
    _FieldName = vFieldName 
    _OldValue = vOldValue 
    _NewValue = vNewValue 
    _ChangedByUser = vChangedByUser 
    _ActiveLoginID = vActiveLoginID 
    _SqlSystemUser = vSqlSystemUser 
    _SqlAppName = vSqlAppName 
    _SqlHostName = vSqlHostName 
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
    _FieldName = _FieldName.Truncate(pTruncateLength, _IsTruncated) 
    _OldValue = _OldValue.Truncate(pTruncateLength, _IsTruncated) 
    _NewValue = _NewValue.Truncate(pTruncateLength, _IsTruncated) 
    _ChangedByUser = _ChangedByUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlSystemUser = _SqlSystemUser.Truncate(pTruncateLength, _IsTruncated) 
    _SqlAppName = _SqlAppName.Truncate(pTruncateLength, _IsTruncated) 
    _SqlHostName = _SqlHostName.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _TableName = ccHelper.RemoveChrW0(_TableName) 
    _Operation = ccHelper.RemoveChrW0(_Operation) 
    _SqlCurrentUser = ccHelper.RemoveChrW0(_SqlCurrentUser) 
    _FieldName = ccHelper.RemoveChrW0(_FieldName) 
    _OldValue = ccHelper.RemoveChrW0(_OldValue) 
    _NewValue = ccHelper.RemoveChrW0(_NewValue) 
    _ChangedByUser = ccHelper.RemoveChrW0(_ChangedByUser) 
    _SqlSystemUser = ccHelper.RemoveChrW0(_SqlSystemUser) 
    _SqlAppName = ccHelper.RemoveChrW0(_SqlAppName) 
    _SqlHostName = ccHelper.RemoveChrW0(_SqlHostName) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AuditIndexed by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexed_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the AuditIndexed by the chosen parameters. This function may be a bit slower than accessing the AuditIndexed's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexed_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AuditIndexed-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the AuditIndexed by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexed_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"AuditIndexed not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-AuditIndexed-210927-1527", vRequester, vAdditionalMessageToUser:=$"AuditIndexed not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccAuditIndexedCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"AuditIndexed not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-AuditIndexed-210625-0950", vRequester, vAdditionalMessageToUser:=$"AuditIndexed not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedUpdate, "csAuditIndexed_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-AuditIndexed-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' This updates the AuditIndexed. If there are parents or children in the AuditIndexed, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedUpdate, "csAuditIndexed_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
    If _ID <> 0 Then
      Return pFault.LogFreeTextFault(56, "'AuditIndexed' is not 'Editable'", pFunctionParameters, "TRGT-AuditIndexed-190217-1704", vRequester) 
    End If
 
    'Check if we got an empty object 
    Dim pAuditIndexed As New csAuditIndexed() 
    If Me.isEqual(pAuditIndexed) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-AuditIndexed-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-AuditIndexed-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_AuditIndexedUpdate"
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
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pCachedAuditIndexed As csAuditIndexed 
      If _ID = 0 Then 
        pCachedAuditIndexed = New csAuditIndexed() 
        'get last ID 
        Dim pAuditIndexedCol As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.Clone() 
        If pAuditIndexedCol.Count = 0 Then 
          _ID = 1 
        Else 
          pAuditIndexedCol.SortByID() 
          Dim pLastID As Long = pAuditIndexedCol(pAuditIndexedCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccAuditIndexedCol.Add(pCachedAuditIndexed) 
      Else  
        pCachedAuditIndexed = MyController.DBCache.ccAuditIndexedCol.FindByID(_ID) 
      End If 
      pCachedAuditIndexed.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAuditIndexedCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "OriginalID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_OriginalID) 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_TableName) 
        pLastReadVariableName = "RowID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_RowID) 
        pLastReadVariableName = "Operation" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 10).Value = ccHelper.ObjectNullable(_Operation) 
        pLastReadVariableName = "OccurredAt" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_OccurredAt) 
        pLastReadVariableName = "SqlCurrentUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_SqlCurrentUser) 
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(_FieldName) 
        pLastReadVariableName = "OldValue" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 1000).Value = ccHelper.ObjectNullable(_OldValue) 
        pLastReadVariableName = "NewValue" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 1000).Value = ccHelper.ObjectNullable(_NewValue) 
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_ChangedByUser) 
        pLastReadVariableName = "ActiveLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ActiveLoginID) 
        pLastReadVariableName = "SqlSystemUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_SqlSystemUser) 
        pLastReadVariableName = "SqlAppName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 250).Value = ccHelper.ObjectNullable(_SqlAppName) 
        pLastReadVariableName = "SqlHostName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_SqlHostName) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

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
    Dim pFunctionParameters As String = String.Format("AuditIndexed.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexed_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_AuditIndexedDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      MyController.DBCache.ccAuditIndexedCol.Remove(MyController.DBCache.ccAuditIndexedCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAuditIndexedCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexed_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      MyController.DBCache.ccAuditIndexedCol.Remove(MyController.DBCache.ccAuditIndexedCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccAuditIndexedCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csAuditIndexed) Then Return False 
    Dim pAuditIndexedToTest As csAuditIndexed = CType(vTargCCEntityToTest, csAuditIndexed) 
    Return isEqual(pAuditIndexedToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vAuditIndexedToTest As csAuditIndexed) As Boolean
    With vAuditIndexedToTest
      If _ID <> .ID Then Return False
      If _OriginalID <> .OriginalID Then Return False
      If _TableName <> .TableName Then Return False
      If _RowID <> .RowID Then Return False
      If _Operation <> .Operation Then Return False
      If _OccurredAt <> Nothing AndAlso .OccurredAt <> Nothing Then 
        If ccHelper.ToLong(_OccurredAt.Subtract(.OccurredAt).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_OccurredAt = Nothing AndAlso .OccurredAt = Nothing) Then 
        Return False 
      End If 
      If _SqlCurrentUser <> .SqlCurrentUser Then Return False
      If _FieldName <> .FieldName Then Return False
      If _OldValue <> .OldValue Then Return False
      If _NewValue <> .NewValue Then Return False
      If _ChangedByUser <> .ChangedByUser Then Return False
      If _ActiveLoginID <> .ActiveLoginID Then Return False
      If _SqlSystemUser <> .SqlSystemUser Then Return False
      If _SqlAppName <> .SqlAppName Then Return False
      If _SqlHostName <> .SqlHostName Then Return False
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
    Dim pClone As New csAuditIndexed(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAuditIndexed
    Dim pClone As New csAuditIndexed(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OriginalID") = _OriginalID : Catch ex As Exception : Return pFault.LogException(ex, "OriginalID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("TableName") = _TableName : Catch ex As Exception : Return pFault.LogException(ex, "TableName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("RowID") = _RowID : Catch ex As Exception : Return pFault.LogException(ex, "RowID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("Operation") = _Operation : Catch ex As Exception : Return pFault.LogException(ex, "Operation", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OccurredAt") = _OccurredAt : Catch ex As Exception : Return pFault.LogException(ex, "OccurredAt", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlCurrentUser") = _SqlCurrentUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlCurrentUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("FieldName") = _FieldName : Catch ex As Exception : Return pFault.LogException(ex, "FieldName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("OldValue") = _OldValue : Catch ex As Exception : Return pFault.LogException(ex, "OldValue", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("NewValue") = _NewValue : Catch ex As Exception : Return pFault.LogException(ex, "NewValue", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("ChangedByUser") = _ChangedByUser : Catch ex As Exception : Return pFault.LogException(ex, "ChangedByUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActiveLoginID") = _ActiveLoginID : Catch ex As Exception : Return pFault.LogException(ex, "ActiveLoginID", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlSystemUser") = _SqlSystemUser : Catch ex As Exception : Return pFault.LogException(ex, "SqlSystemUser", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlAppName") = _SqlAppName : Catch ex As Exception : Return pFault.LogException(ex, "SqlAppName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
    Try : vDataRow("SqlHostName") = _SqlHostName : Catch ex As Exception : Return pFault.LogException(ex, "SqlHostName", "TRGT-AuditIndexed-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pAuditIndexed As csAuditIndexed = CType(pXmlSerializer.Deserialize(pStreamReader), csAuditIndexed) 
      AssignValues(pAuditIndexed) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-AuditIndexed-130515-1230", vRequester) 
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
          'OriginalID 
          pBinaryWriter.Write(_OriginalID) 
          'TableName 
          If _TableName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_TableName) 
          'RowID 
          pBinaryWriter.Write(_RowID) 
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
          'FieldName 
          If _FieldName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FieldName) 
          'OldValue 
          If _OldValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_OldValue) 
          'NewValue 
          If _NewValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_NewValue) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-150307-2338", vRequester) 
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
          'OriginalID 
          _OriginalID = pReader.ReadInt64 
          'TableName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _TableName = pReader.ReadString 
          'RowID 
          _RowID = pReader.ReadInt64 
          'Operation 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Operation = pReader.ReadString 
          'OccurredAt 
          _OccurredAt = New Date(pReader.ReadInt64) 
          'SqlCurrentUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SqlCurrentUser = pReader.ReadString 
          'FieldName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FieldName = pReader.ReadString 
          'OldValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _OldValue = pReader.ReadString 
          'NewValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _NewValue = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-AuditIndexed-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
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
 
      Dim pAuditIndexed As csAuditIndexed = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csAuditIndexed)(vJSON, pSettings) 
      AssignValues(pAuditIndexed) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vAuditIndexed As csAuditIndexed)
    With vAuditIndexed
      _ID = .ID 
      _OriginalID = .OriginalID 
      _TableName = .TableName 
      _RowID = .RowID 
      _Operation = .Operation 
      _OccurredAt = .OccurredAt 
      _SqlCurrentUser = .SqlCurrentUser 
      _FieldName = .FieldName 
      _OldValue = .OldValue 
      _NewValue = .NewValue 
      _ChangedByUser = .ChangedByUser 
      _ActiveLoginID = .ActiveLoginID 
      _SqlSystemUser = .SqlSystemUser 
      _SqlAppName = .SqlAppName 
      _SqlHostName = .SqlHostName 
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
      pLastReadVariableName = "OriginalID" 
      If Not vReader.IsDBNull(1) Then _OriginalID = vReader.GetInt64(1)
      pLastReadVariableName = "TableName" 
      If Not vReader.IsDBNull(2) Then _TableName = vReader.GetString(2) 
      pLastReadVariableName = "RowID" 
      If Not vReader.IsDBNull(3) Then _RowID = vReader.GetInt64(3)
      pLastReadVariableName = "Operation" 
      If Not vReader.IsDBNull(4) Then _Operation = vReader.GetString(4) 
      pLastReadVariableName = "OccurredAt" 
      If Not vReader.IsDBNull(5) Then _OccurredAt = vReader.GetDateTime(5)
      pLastReadVariableName = "SqlCurrentUser" 
      If Not vReader.IsDBNull(6) Then _SqlCurrentUser = vReader.GetString(6) 
      pLastReadVariableName = "FieldName" 
      If Not vReader.IsDBNull(7) Then _FieldName = vReader.GetString(7) 
      pLastReadVariableName = "OldValue" 
      If Not vReader.IsDBNull(8) Then _OldValue = vReader.GetString(8) 
      pLastReadVariableName = "NewValue" 
      If Not vReader.IsDBNull(9) Then _NewValue = vReader.GetString(9) 
      pLastReadVariableName = "ChangedByUser" 
      If Not vReader.IsDBNull(10) Then _ChangedByUser = vReader.GetString(10) 
      pLastReadVariableName = "ActiveLoginID" 
      If Not vReader.IsDBNull(11) Then _ActiveLoginID = vReader.GetInt64(11)
      pLastReadVariableName = "SqlSystemUser" 
      If Not vReader.IsDBNull(12) Then _SqlSystemUser = vReader.GetString(12) 
      pLastReadVariableName = "SqlAppName" 
      If Not vReader.IsDBNull(13) Then _SqlAppName = vReader.GetString(13) 
      pLastReadVariableName = "SqlHostName" 
      If Not vReader.IsDBNull(14) Then _SqlHostName = vReader.GetString(14) 
      bDateAdded = _OccurredAt 
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedAuditIndexed As csAuditIndexed, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedAuditIndexed) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _OriginalID = 0
    _TableName = ""
    _RowID = 0
    _Operation = ""
    _OccurredAt = Nothing
    _SqlCurrentUser = ""
    _FieldName = ""
    _OldValue = ""
    _NewValue = ""
    _ChangedByUser = ""
    _ActiveLoginID = 0
    _SqlSystemUser = ""
    _SqlAppName = ""
    _SqlHostName = ""
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
  
Public Class csAuditIndexedCol
  Inherits cTargCCCollection(Of csAuditIndexed)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csAuditIndexed) 
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
 
    For Each pRow As csAuditIndexed In Me 
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
    pCSVTitle.Append(",""OriginalID""") 
    pCSVTitle.Append(",""TableName""") 
    pCSVTitle.Append(",""RowID""") 
    pCSVTitle.Append(",""Operation""") 
    pCSVTitle.Append(",""OccurredAt""") 
    pCSVTitle.Append(",""SqlCurrentUser""") 
    pCSVTitle.Append(",""FieldName""") 
    pCSVTitle.Append(",""OldValue""") 
    pCSVTitle.Append(",""NewValue""") 
    pCSVTitle.Append(",""ChangedByUser""") 
    pCSVTitle.Append(",""ActiveLoginID""") 
    pCSVTitle.Append(",""SqlSystemUser""") 
    pCSVTitle.Append(",""SqlAppName""") 
    pCSVTitle.Append(",""SqlHostName""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csAuditIndexed In Me 
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
 
  Public Overloads Sub Add(ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Add(vAuditIndexed) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vAuditIndexed) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vAuditIndexedCol As csAuditIndexedCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vAuditIndexedCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vAuditIndexed As csAuditIndexed) 
    SyncLock _CollectionLock 
      MyBase.Remove(vAuditIndexed) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csAuditIndexed) 
      
      For Each lAuditIndexed In Me 
        If lAuditIndexed.IsEmpty OrElse pTempDictionary.ContainsKey(lAuditIndexed.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lAuditIndexed.ID, lAuditIndexed) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lAuditIndexed.ToString, "TRGT-AuditIndexed-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", AuditIndexed:" & lAuditIndexed.ToString() & ", TRGT-AuditIndexed-260111-154657") 'Send it up the line 
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
 
    For Each lAuditIndexed As csAuditIndexed In Me 
      lAuditIndexed.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lAuditIndexed As csAuditIndexed In Me 
      lAuditIndexed.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ActiveLoginID] 
    [ChangedByUser] 
    [FieldName] 
    [OccurredAt] 
    [OriginalID] 
    [RowID] 
    [TableName] 
    [TableNameAndRowID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the AuditIndexeds by the chosen parameters. This function may be a bit slower than accessing the AuditIndexed's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.ActiveLoginID 
          pFault = FillByActiveLoginID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ChangedByUser 
          pFault = FillByChangedByUser(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.FieldName 
          pFault = FillByFieldName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OccurredAt 
          pFault = FillByOccurredAt(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.OriginalID 
          pFault = FillByOriginalID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.RowID 
          pFault = FillByRowID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TableName 
          pFault = FillByTableName(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TableNameAndRowID 
          pFault = FillByTableNameAndRowID(CStr(vParameters(0)), ccHelper.ToLong(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-AuditIndexed-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-AuditIndexed-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.Clone() 
      pAuditIndexedsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pAuditIndexedsCached.Reverse() 
      If vHowMany > 0 AndAlso pAuditIndexedsCached.Count > vHowMany Then 
        Dim tmp As New csAuditIndexedCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pAuditIndexedsCached(i)) 
        Next 
        pAuditIndexedsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ActiveLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByActiveLoginID(ByVal vActiveLoginID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginID={0}", vActiveLoginID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByActiveLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByActiveLoginID(vActiveLoginID)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByActiveLoginID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ActiveLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByChangedByUser(ByVal vChangedByUser As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}", vChangedByUser)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByChangedByUser(vChangedByUser)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByChangedByUser" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUser) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByFieldName(ByVal vFieldName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}", vFieldName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByFieldName(vFieldName)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByFieldName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OccurredAt, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOccurredAt(ByVal vOccurredAt As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAt={0}", vOccurredAt)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByOccurredAt", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByOccurredAt(vOccurredAt)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByOccurredAt" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OccurredAt" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vOccurredAt) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginalID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByOriginalID(ByVal vOriginalID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalID={0}", vOriginalID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByOriginalID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByOriginalID(vOriginalID)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByOriginalID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OriginalID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOriginalID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByRowID(ByVal vRowID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("RowID={0}", vRowID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByRowID(vRowID)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByRowID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "RowID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vRowID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTableName(ByVal vTableName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}", vTableName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByTableName(vTableName)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableNameAndRowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTableNameAndRowID(ByVal vTableName As String, ByVal vRowID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, RowID={1}", vTableName, vRowID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByTableNameAndRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByTableNameAndRowID(vTableName, vRowID)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByTableName&RowID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableName) 
        pLastReadVariableName = "RowID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vRowID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ActiveLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedActiveLoginID(ByVal vActiveLoginIDFrom As Long, ByVal vActiveLoginIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginIDFrom={0}, ActiveLoginIDTo={1}", vActiveLoginIDFrom, vActiveLoginIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedActiveLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedActiveLoginID(vActiveLoginIDFrom, vActiveLoginIDTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedActiveLoginID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ActiveLoginIDFrom" 
        pDALParameters.Add("bndActiveLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginIDFrom) 
        pLastReadVariableName = "ActiveLoginIDTo" 
        pDALParameters.Add("bndActiveLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedChangedByUser(ByVal vChangedByUserFrom As String, ByVal vChangedByUserTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUserFrom={0}, ChangedByUserTo={1}", vChangedByUserFrom, vChangedByUserTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedChangedByUser(vChangedByUserFrom, vChangedByUserTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedChangedByUser" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ChangedByUserFrom" 
        pDALParameters.Add("bndChangedByUserFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUserFrom) 
        pLastReadVariableName = "ChangedByUserTo" 
        pDALParameters.Add("bndChangedByUserTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUserTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedFieldName(ByVal vFieldNameFrom As String, ByVal vFieldNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldNameFrom={0}, FieldNameTo={1}", vFieldNameFrom, vFieldNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedFieldName(vFieldNameFrom, vFieldNameTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedFieldName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "FieldNameFrom" 
        pDALParameters.Add("bndFieldNameFrom", ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldNameFrom) 
        pLastReadVariableName = "FieldNameTo" 
        pDALParameters.Add("bndFieldNameTo", ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OccurredAt, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOccurredAt(ByVal vOccurredAtStart As Date, ByVal vOccurredAtEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAtStart={0}, OccurredAtEnd={1}", vOccurredAtStart, vOccurredAtEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedOccurredAt", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedOccurredAt(vOccurredAtStart, vOccurredAtEnd)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedOccurredAt" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OccurredAtFrom" 
        pDALParameters.Add("bndOccurredAtFrom", ccDAL.enmSQLDataType.DateTime).Value = (vOccurredAtStart) 
        pLastReadVariableName = "OccurredAtTo" 
        pDALParameters.Add("bndOccurredAtTo", ccDAL.enmSQLDataType.DateTime).Value = (vOccurredAtEnd) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific OriginalID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedOriginalID(ByVal vOriginalIDFrom As Long, ByVal vOriginalIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalIDFrom={0}, OriginalIDTo={1}", vOriginalIDFrom, vOriginalIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedOriginalID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedOriginalID(vOriginalIDFrom, vOriginalIDTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedOriginalID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "OriginalIDFrom" 
        pDALParameters.Add("bndOriginalIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vOriginalIDFrom) 
        pLastReadVariableName = "OriginalIDTo" 
        pDALParameters.Add("bndOriginalIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vOriginalIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific RowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedRowID(ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("RowIDFrom={0}, RowIDTo={1}", vRowIDFrom, vRowIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedRowID(vRowIDFrom, vRowIDTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedRowID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}", vTableNameFrom, vTableNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedTableName(vTableNameFrom, vTableNameTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableNameFrom" 
        pDALParameters.Add("bndTableNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameFrom) 
        pLastReadVariableName = "TableNameTo" 
        pDALParameters.Add("bndTableNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific TableNameAndRowID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTableNameAndRowID(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}, RowIDFrom={2}, RowIDTo={3}", vTableNameFrom, vTableNameTo, vRowIDFrom, vRowIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByBoundedTableNameAndRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccAuditIndexedCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccAuditIndexedCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csAuditIndexedCol failed: " & pResponse) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.CloneByBoundedTableNameAndRowID(vTableNameFrom, vTableNameTo, vRowIDFrom, vRowIDTo)
      pFault = LoadMeFromDBCache(pAuditIndexedsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByBoundedTableName&RowID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableNameFrom" 
        pDALParameters.Add("bndTableNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameFrom) 
        pLastReadVariableName = "TableNameTo" 
        pDALParameters.Add("bndTableNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameTo) 
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded ChangedByUser, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardChangedByUser(ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}, ChangedByUserWildcardType={1}", vChangedByUser, vChangedByUserWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByWildCardChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ChangedByUser 
    Dim pWCChangedByUser As String = "" 
    If vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
      pWCChangedByUser = vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCChangedByUser = "%" & vChangedByUser 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCChangedByUser = "%" & vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vChangedByUser.ToCharArray 
        pWCChangedByUser &= p & "%" 
      Next 
      pWCChangedByUser = "%" & pWCChangedByUser 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-AuditIndexed-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByWildCardChangedByUser" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldChangedByUser" 
        pDALParameters.Add("wldChangedByUser", ccDAL.enmSQLDataType.NVarChar, 50).Value = (pWCChangedByUser) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded FieldName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardFieldName(ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}, FieldNameWildcardType={1}", vFieldName, vFieldNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByWildCardFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'FieldName 
    Dim pWCFieldName As String = "" 
    If vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCFieldName = vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCFieldName = "%" & vFieldName 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCFieldName = "%" & vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vFieldName.ToCharArray 
        pWCFieldName &= p & "%" 
      Next 
      pWCFieldName = "%" & pWCFieldName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-AuditIndexed-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByWildCardFieldName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldFieldName" 
        pDALParameters.Add("wldFieldName", ccDAL.enmSQLDataType.VarChar, 100).Value = (pWCFieldName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded TableName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, TableNameWildcardType={1}", vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByWildCardTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'TableName 
    Dim pWCTableName As String = "" 
    If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCTableName = vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCTableName = "%" & vTableName 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCTableName = "%" & vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vTableName.ToCharArray 
        pWCTableName &= p & "%" 
      Next 
      pWCTableName = "%" & pWCTableName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-AuditIndexed-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillByWildCardTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldTableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCTableName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lAuditIndexed As New csAuditIndexed() 
      pFault = lAuditIndexed.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lAuditIndexed.IsEmpty Then Me.Add(lAuditIndexed) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pAuditIndexeds As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pAuditIndexeds, "csAuditIndexedCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pAuditIndexeds IsNot Nothing AndAlso Me.Count <> pAuditIndexeds.Count Then FillFromListOfITargCCEntity(pAuditIndexeds) 
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
    OriginalIDFrom
    OriginalIDTo
    [TableName]
    TableNameWildcardType
    RowIDFrom
    RowIDTo
    OccurredAtStart
    OccurredAtEnd
    [FieldName]
    FieldNameWildcardType
    [ChangedByUser]
    ChangedByUserWildcardType
    ActiveLoginIDFrom
    ActiveLoginIDTo
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOriginalIDFrom As Nullable(Of Long) = Nothing
    Dim pOriginalIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pRowIDFrom As Nullable(Of Long) = Nothing
    Dim pRowIDTo As Nullable(Of Long) = Nothing
    Dim pOccurredAtStart As Nullable(Of Date) = Nothing
    Dim pOccurredAtEnd As Nullable(Of Date) = Nothing
    Dim pFieldName As String = Nothing
    Dim pFieldNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pChangedByUser As String = Nothing
    Dim pChangedByUserWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pActiveLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pActiveLoginIDTo As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDFrom) : If pObj IsNot Nothing Then pOriginalIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDTo) : If pObj IsNot Nothing Then pOriginalIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDFrom) : If pObj IsNot Nothing Then pRowIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDTo) : If pObj IsNot Nothing Then pRowIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtStart) : If pObj IsNot Nothing Then pOccurredAtStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtEnd) : If pObj IsNot Nothing Then pOccurredAtEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldName) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldName) : If pObj IsNot Nothing Then pFieldName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldNameWildcardType) : If pObj IsNot Nothing Then pFieldNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUser) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUser) : If pObj IsNot Nothing Then pChangedByUser = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUserWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUserWildcardType) : If pObj IsNot Nothing Then pChangedByUserWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDFrom) : If pObj IsNot Nothing Then pActiveLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDTo) : If pObj IsNot Nothing Then pActiveLoginIDTo = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pOriginalIDFrom, pOriginalIDTo _
        , pTableName, pTableNameWildcardType _
        , pRowIDFrom, pRowIDTo _
        , pOccurredAtStart, pOccurredAtEnd _
        , pFieldName, pFieldNameWildcardType _
        , pChangedByUser, pChangedByUserWildcardType _
        , pActiveLoginIDFrom, pActiveLoginIDTo _
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
        , ByVal vOriginalIDFrom As Nullable(Of Long), ByVal vOriginalIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRowIDFrom As Nullable(Of Long), ByVal vRowIDTo As Nullable(Of Long) _
        , ByVal vOccurredAtStart As Nullable(Of Date), ByVal vOccurredAtEnd As Nullable(Of Date) _
        , ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType _
        , ByVal vActiveLoginIDFrom As Nullable(Of Long), ByVal vActiveLoginIDTo As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OriginalIDFrom={2}, OriginalIDTo={3}, TableName={4}, TableNameWildcardType={5}, RowIDFrom={6}, RowIDTo={7}, OccurredAtStart={8}, OccurredAtEnd={9}, FieldName={10}, FieldNameWildcardType={11}, ChangedByUser={12}, ChangedByUserWildcardType={13}, ActiveLoginIDFrom={14}, ActiveLoginIDTo={15}", vIDFrom, vIDTo, vOriginalIDFrom, vOriginalIDTo, vTableName, vTableNameWildcardType.FastToString(), vRowIDFrom, vRowIDTo, vOccurredAtStart, vOccurredAtEnd, vFieldName, vFieldNameWildcardType.FastToString(), vChangedByUser, vChangedByUserWildcardType.FastToString(), vActiveLoginIDFrom, vActiveLoginIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'TableName 
    Dim pWCTableName As String = "" 
    If vTableName = Nothing Then 
      pWCTableName = vTableName
    Else 
      If vTableNameWildcardType = clsEnums.enmWildCardType.None OrElse vTableNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCTableName = vTableName
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCTableName = vTableName & "%" 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCTableName = "%" & vTableName 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCTableName = "%" & vTableName & "%" 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vTableName.ToCharArray 
          pWCTableName &= p & "%" 
        Next 
        pWCTableName = "%" & pWCTableName 
      End If 
    End If 
    'FieldName 
    Dim pWCFieldName As String = "" 
    If vFieldName = Nothing Then 
      pWCFieldName = vFieldName
    Else 
      If vFieldNameWildcardType = clsEnums.enmWildCardType.None OrElse vFieldNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCFieldName = vFieldName
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCFieldName = vFieldName & "%" 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCFieldName = "%" & vFieldName 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCFieldName = "%" & vFieldName & "%" 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vFieldName.ToCharArray 
          pWCFieldName &= p & "%" 
        Next 
        pWCFieldName = "%" & pWCFieldName 
      End If 
    End If 
    'ChangedByUser 
    Dim pWCChangedByUser As String = "" 
    If vChangedByUser = Nothing Then 
      pWCChangedByUser = vChangedByUser
    Else 
      If vChangedByUserWildcardType = clsEnums.enmWildCardType.None OrElse vChangedByUserWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCChangedByUser = vChangedByUser
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
        pWCChangedByUser = vChangedByUser & "%" 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCChangedByUser = "%" & vChangedByUser 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCChangedByUser = "%" & vChangedByUser & "%" 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vChangedByUser.ToCharArray 
          pWCChangedByUser &= p & "%" 
        Next 
        pWCChangedByUser = "%" & pWCChangedByUser 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-AuditIndexed-121122-2008", vRequester) 
      Dim pAuditIndexedsCached As csAuditIndexedCol = MyController.DBCache.ccAuditIndexedCol.Clone() 
      Dim pAuditIndexedsToUse As New csAuditIndexedCol() 
      For Each l In pAuditIndexedsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vOriginalIDFrom.HasValue Then 
          If vOriginalIDTo.HasValue Then 
            If l.OriginalID < vOriginalIDFrom OrElse l.OriginalID > vOriginalIDTo.Value Then Continue For 
          Else 
            If l.OriginalID <> vOriginalIDFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vTableName) Then 
          If vTableNameWildcardType = clsEnums.enmWildCardType.UD OrElse vTableNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.TableName.Equals(vTableName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vRowIDFrom.HasValue Then 
          If vRowIDTo.HasValue Then 
            If l.RowID < vRowIDFrom OrElse l.RowID > vRowIDTo.Value Then Continue For 
          Else 
            If l.RowID <> vRowIDFrom.Value Then Continue For 
          End If 
        End If 
        If vOccurredAtStart.HasValue Then 
          If vOccurredAtEnd.HasValue Then 
            If l.OccurredAt < vOccurredAtStart OrElse l.OccurredAt > vOccurredAtEnd.Value Then Continue For 
          Else 
            If l.OccurredAt <> vOccurredAtStart.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vFieldName) Then 
          If vFieldNameWildcardType = clsEnums.enmWildCardType.UD OrElse vFieldNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.FieldName.Equals(vFieldName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.FieldName.StartsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.FieldName.EndsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.FieldName.IndexOf(vFieldName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vChangedByUser) Then 
          If vChangedByUserWildcardType = clsEnums.enmWildCardType.UD OrElse vChangedByUserWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.ChangedByUser.Equals(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.ChangedByUser.StartsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.ChangedByUser.EndsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.ChangedByUser.IndexOf(vChangedByUser, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vActiveLoginIDFrom.HasValue Then 
          If vActiveLoginIDTo.HasValue Then 
            If l.ActiveLoginID < vActiveLoginIDFrom OrElse l.ActiveLoginID > vActiveLoginIDTo.Value Then Continue For 
          Else 
            If l.ActiveLoginID <> vActiveLoginIDFrom.Value Then Continue For 
          End If 
        End If 
        pAuditIndexedsToUse.Add(l) 
      Next 
      pAuditIndexedsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pAuditIndexedsToUse.Reverse() 
      If vHowMany > 0 AndAlso pAuditIndexedsToUse.Count > vHowMany Then 
        Dim tmp As New csAuditIndexedCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pAuditIndexedsToUse(i)) 
        Next 
        pAuditIndexedsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pAuditIndexedsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OriginalIDFrom" 
        pDALParameters.Add("bndOriginalIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOriginalIDFrom) 
        pLastReadVariableName = "OriginalIDTo" 
        pDALParameters.Add("bndOriginalIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOriginalIDTo) 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCTableName) 
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vRowIDTo) 
        pLastReadVariableName = "OccurredAtFrom" 
        pDALParameters.Add("bndOccurredAtFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOccurredAtStart) 
        pLastReadVariableName = "OccurredAtTo" 
        pDALParameters.Add("bndOccurredAtTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOccurredAtEnd) 
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add("wldFieldName", ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(pWCFieldName) 
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add("wldChangedByUser", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCChangedByUser) 
        pLastReadVariableName = "ActiveLoginIDFrom" 
        pDALParameters.Add("bndActiveLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vActiveLoginIDFrom) 
        pLastReadVariableName = "ActiveLoginIDTo" 
        pDALParameters.Add("bndActiveLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vActiveLoginIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByOriginalID
    GroupByTableName
    GroupByRowID
    GroupByOccurredAt
    GroupByFieldName
    GroupByChangedByUser
    GroupByActiveLoginID
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pOriginalIDFrom As Nullable(Of Long) = Nothing
    Dim pOriginalIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pRowIDFrom As Nullable(Of Long) = Nothing
    Dim pRowIDTo As Nullable(Of Long) = Nothing
    Dim pOccurredAtStart As Nullable(Of Date) = Nothing
    Dim pOccurredAtEnd As Nullable(Of Date) = Nothing
    Dim pFieldName As String = Nothing
    Dim pFieldNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pChangedByUser As String = Nothing
    Dim pChangedByUserWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pActiveLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pActiveLoginIDTo As Nullable(Of Long) = Nothing
    Dim pGroupByOriginalID As Boolean = False
    Dim pGroupByTableName As Boolean = False
    Dim pGroupByRowID As Boolean = False
    Dim pGroupByOccurredAt As Boolean = False
    Dim pGroupByFieldName As Boolean = False
    Dim pGroupByChangedByUser As Boolean = False
    Dim pGroupByActiveLoginID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDFrom) : If pObj IsNot Nothing Then pOriginalIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OriginalIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.OriginalIDTo) : If pObj IsNot Nothing Then pOriginalIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDFrom) : If pObj IsNot Nothing Then pRowIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RowIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.RowIDTo) : If pObj IsNot Nothing Then pRowIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtStart) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtStart) : If pObj IsNot Nothing Then pOccurredAtStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.OccurredAtEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.OccurredAtEnd) : If pObj IsNot Nothing Then pOccurredAtEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldName) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldName) : If pObj IsNot Nothing Then pFieldName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FieldNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FieldNameWildcardType) : If pObj IsNot Nothing Then pFieldNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUser) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUser) : If pObj IsNot Nothing Then pChangedByUser = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ChangedByUserWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ChangedByUserWildcardType) : If pObj IsNot Nothing Then pChangedByUserWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDFrom) : If pObj IsNot Nothing Then pActiveLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ActiveLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.ActiveLoginIDTo) : If pObj IsNot Nothing Then pActiveLoginIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOriginalID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOriginalID) : If pObj IsNot Nothing Then pGroupByOriginalID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByTableName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByTableName) : If pObj IsNot Nothing Then pGroupByTableName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByRowID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByRowID) : If pObj IsNot Nothing Then pGroupByRowID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByOccurredAt) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByOccurredAt) : If pObj IsNot Nothing Then pGroupByOccurredAt = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFieldName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFieldName) : If pObj IsNot Nothing Then pGroupByFieldName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByChangedByUser) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByChangedByUser) : If pObj IsNot Nothing Then pGroupByChangedByUser = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByActiveLoginID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByActiveLoginID) : If pObj IsNot Nothing Then pGroupByActiveLoginID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pOriginalIDFrom, pOriginalIDTo _
        , pTableName, pTableNameWildcardType _
        , pRowIDFrom, pRowIDTo _
        , pOccurredAtStart, pOccurredAtEnd _
        , pFieldName, pFieldNameWildcardType _
        , pChangedByUser, pChangedByUserWildcardType _
        , pActiveLoginIDFrom, pActiveLoginIDTo _
        , pGroupByOriginalID _
        , pGroupByTableName _
        , pGroupByRowID _
        , pGroupByOccurredAt _
        , pGroupByFieldName _
        , pGroupByChangedByUser _
        , pGroupByActiveLoginID _
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
        , ByVal vOriginalIDFrom As Nullable(Of Long), ByVal vOriginalIDTo As Nullable(Of Long) _
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRowIDFrom As Nullable(Of Long), ByVal vRowIDTo As Nullable(Of Long) _
        , ByVal vOccurredAtStart As Nullable(Of Date), ByVal vOccurredAtEnd As Nullable(Of Date) _
        , ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType _
        , ByVal vActiveLoginIDFrom As Nullable(Of Long), ByVal vActiveLoginIDTo As Nullable(Of Long) _
        , ByVal vGroupByOriginalID As Boolean _
        , ByVal vGroupByTableName As Boolean _
        , ByVal vGroupByRowID As Boolean _
        , ByVal vGroupByOccurredAt As Boolean _
        , ByVal vGroupByFieldName As Boolean _
        , ByVal vGroupByChangedByUser As Boolean _
        , ByVal vGroupByActiveLoginID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, OriginalIDFrom={2}, OriginalIDTo={3}, TableName={4}, TableNameWildcardType={5}, RowIDFrom={6}, RowIDTo={7}, OccurredAtStart={8}, OccurredAtEnd={9}, FieldName={10}, FieldNameWildcardType={11}, ChangedByUser={12}, ChangedByUserWildcardType={13}, ActiveLoginIDFrom={14}, ActiveLoginIDTo={15}, GroupByOriginalID={16}, GroupByTableName={17}, GroupByRowID={18}, GroupByOccurredAt={19}, GroupByFieldName={20}, GroupByChangedByUser={21}, GroupByActiveLoginID={22}", vIDFrom, vIDTo, vOriginalIDFrom, vOriginalIDTo, vTableName, vTableNameWildcardType.FastToString(), vRowIDFrom, vRowIDTo, vOccurredAtStart, vOccurredAtEnd, vFieldName, vFieldNameWildcardType.FastToString(), vChangedByUser, vChangedByUserWildcardType.FastToString(), vActiveLoginIDFrom, vActiveLoginIDTo, vGroupByOriginalID, vGroupByTableName, vGroupByRowID, vGroupByOccurredAt, vGroupByFieldName, vGroupByChangedByUser, vGroupByActiveLoginID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedView, "csAuditIndexedCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'TableName 
    Dim pWCTableName As String = "" 
    If vTableName = Nothing Then 
      pWCTableName = vTableName
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.None OrElse vTableNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCTableName = vTableName
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCTableName = vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCTableName = "%" & vTableName 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCTableName = "%" & vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vTableName.ToCharArray 
        pWCTableName &= p & "%" 
      Next 
      pWCTableName = "%" & pWCTableName 
    End If 
    'FieldName 
    Dim pWCFieldName As String = "" 
    If vFieldName = Nothing Then 
      pWCFieldName = vFieldName
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.None OrElse vFieldNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCFieldName = vFieldName
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCFieldName = vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCFieldName = "%" & vFieldName 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCFieldName = "%" & vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vFieldName.ToCharArray 
        pWCFieldName &= p & "%" 
      Next 
      pWCFieldName = "%" & pWCFieldName 
    End If 
    'ChangedByUser 
    Dim pWCChangedByUser As String = "" 
    If vChangedByUser = Nothing Then 
      pWCChangedByUser = vChangedByUser
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.None OrElse vChangedByUserWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCChangedByUser = vChangedByUser
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
      pWCChangedByUser = vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCChangedByUser = "%" & vChangedByUser 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCChangedByUser = "%" & vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vChangedByUser.ToCharArray 
        pWCChangedByUser &= p & "%" 
      Next 
      pWCChangedByUser = "%" & pWCChangedByUser 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-AuditIndexed-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_AuditIndexedsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "OriginalIDFrom" 
        pDALParameters.Add("bndOriginalIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOriginalIDFrom) 
        pLastReadVariableName = "OriginalIDTo" 
        pDALParameters.Add("bndOriginalIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vOriginalIDTo) 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCTableName) 
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vRowIDTo) 
        pLastReadVariableName = "OccurredAtFrom" 
        pDALParameters.Add("bndOccurredAtFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOccurredAtStart) 
        pLastReadVariableName = "OccurredAtTo" 
        pDALParameters.Add("bndOccurredAtTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vOccurredAtEnd) 
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add("wldFieldName", ccDAL.enmSQLDataType.VarChar, 100).Value = ccHelper.ObjectNullable(pWCFieldName) 
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add("wldChangedByUser", ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(pWCChangedByUser) 
        pLastReadVariableName = "ActiveLoginIDFrom" 
        pDALParameters.Add("bndActiveLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vActiveLoginIDFrom) 
        pLastReadVariableName = "ActiveLoginIDTo" 
        pDALParameters.Add("bndActiveLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vActiveLoginIDTo) 
        pLastReadVariableName = "OriginalID" 
        pDALParameters.Add("GroupByOriginalID", ccDAL.enmSQLDataType.Bit).Value = vGroupByOriginalID
        pLastReadVariableName = "TableName" 
        pDALParameters.Add("GroupByTableName", ccDAL.enmSQLDataType.Bit).Value = vGroupByTableName
        pLastReadVariableName = "RowID" 
        pDALParameters.Add("GroupByRowID", ccDAL.enmSQLDataType.Bit).Value = vGroupByRowID
        pLastReadVariableName = "OccurredAt" 
        pDALParameters.Add("GroupByOccurredAt", ccDAL.enmSQLDataType.Bit).Value = vGroupByOccurredAt
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add("GroupByFieldName", ccDAL.enmSQLDataType.Bit).Value = vGroupByFieldName
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add("GroupByChangedByUser", ccDAL.enmSQLDataType.Bit).Value = vGroupByChangedByUser
        pLastReadVariableName = "ActiveLoginID" 
        pDALParameters.Add("GroupByActiveLoginID", ccDAL.enmSQLDataType.Bit).Value = vGroupByActiveLoginID
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vAuditIndexedArray As csAuditIndexed())
    Me.Clear()
    
    For Each pAuditIndexed As csAuditIndexed In vAuditIndexedArray
      Me.Add(pAuditIndexed)
      _Clean.Add(pAuditIndexed.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pAuditIndexed As New csAuditIndexed(pRow, vRequester) 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-AuditIndexedCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-130515-1300", vRequester) 
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
      Dim pAuditIndexeds As csAuditIndexedCol = CType(pXmlSerializer.Deserialize(pStreamReader), csAuditIndexedCol) 
      For Each pAuditIndexed As csAuditIndexed In pAuditIndexeds 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-AuditIndexed-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-1443", vRequester) 
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
 
      Dim pAuditIndexeds As List(Of csAuditIndexed) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csAuditIndexed))(vJSON, pSettings) 
      For Each pAuditIndexed As csAuditIndexed In pAuditIndexeds 
        Me.Add(pAuditIndexed) 
        _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-190720-2059", vRequester) 
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
          For Each lAuditIndexed As csAuditIndexed In Me 
            Dim pByte As Byte() = lAuditIndexed.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-150307-2340", vRequester) 
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
            Dim pAuditIndexed As csAuditIndexed = New csAuditIndexed(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pAuditIndexed) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pAuditIndexed.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-AuditIndexed-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pAuditIndexed As csAuditIndexed In Me 
      With pAuditIndexed 
        pFault = pAuditIndexed.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csAuditIndexedCol) Then Return False 
    Dim pAuditIndexedColToTest As csAuditIndexedCol = CType(vEntitiesToTest, csAuditIndexedCol) 
    Return isEqual(pAuditIndexedColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vAuditIndexedsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vAuditIndexedsToTest As csAuditIndexedCol) As Boolean
    If Me.Count <> vAuditIndexedsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vAuditIndexedsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    If pFilledFromSumOnTheFly Then pAuditIndexeds._FilledFromSumOnTheFly = True
    
    For Each pAuditIndexed As csAuditIndexed In Me 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
      If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
    Next 
    Return pAuditIndexeds 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csAuditIndexedCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    If pFilledFromSumOnTheFly Then pAuditIndexeds._FilledFromSumOnTheFly = True
    
    For Each pAuditIndexed As csAuditIndexed In Me
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
      pAuditIndexeds.Add(pAuditIndexedClone)
      If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
    Next
    Return pAuditIndexeds
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ID > vIDFrom AndAlso pAuditIndexed.ID <= vIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ActiveLoginID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedActiveLoginID(ByVal vActiveLoginIDFrom As Long, ByVal vActiveLoginIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ActiveLoginID > vActiveLoginIDFrom AndAlso pAuditIndexed.ActiveLoginID <= vActiveLoginIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ChangedByUser (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedChangedByUser(ByVal vChangedByUserFrom As String, ByVal vChangedByUserTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.ChangedByUser > vChangedByUserFrom AndAlso pAuditIndexed.ChangedByUser <= vChangedByUserTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by FieldName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedFieldName(ByVal vFieldNameFrom As String, ByVal vFieldNameTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.FieldName > vFieldNameFrom AndAlso pAuditIndexed.FieldName <= vFieldNameTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OccurredAt (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOccurredAt(ByVal vOccurredAtStart As Date, ByVal vOccurredAtEnd As Date) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.OccurredAt > vOccurredAtStart AndAlso pAuditIndexed.OccurredAt <= vOccurredAtEnd) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by OriginalID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedOriginalID(ByVal vOriginalIDFrom As Long, ByVal vOriginalIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.OriginalID > vOriginalIDFrom AndAlso pAuditIndexed.OriginalID <= vOriginalIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by RowID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedRowID(ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.RowID > vRowIDFrom AndAlso pAuditIndexed.RowID <= vRowIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.TableName > vTableNameFrom AndAlso pAuditIndexed.TableName <= vTableNameTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName and RowID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableNameAndRowID(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol()  
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      If (pAuditIndexed.TableName > vTableNameFrom AndAlso pAuditIndexed.TableName <= vTableNameTo) AndAlso (pAuditIndexed.RowID > vRowIDFrom AndAlso pAuditIndexed.RowID <= vRowIDTo) Then 
        Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
        pAuditIndexeds.Add(pAuditIndexedClone) 
        If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
      End If 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardChangedByUser(ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.ChangedByUser.StartsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.ChangedByUser.EndsWith(vChangedByUser, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.ChangedByUser.IndexOf(vChangedByUser, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vChangedByUser.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.ChangedByUser.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardFieldName(ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.FieldName.StartsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.FieldName.EndsWith(vFieldName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.FieldName.IndexOf(vFieldName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vFieldName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.FieldName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableNameAndRowID(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType, ByVal vRowID As Long, ByVal vRowIDWildcardType As clsEnums.enmWildCardType) As csAuditIndexedCol 
    Dim pAuditIndexeds As New csAuditIndexedCol 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pAuditIndexed.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pAuditIndexed.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pAuditIndexed.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pAuditIndexed.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone() 
      pAuditIndexeds.Add(pAuditIndexedClone) 
    Next 
    Return pAuditIndexeds 
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
  Public Function FindByID(ByVal vID As Long) As csAuditIndexed
    If Me.Count = 0 Then Return New csAuditIndexed 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
    
    Dim pAuditIndexed As csAuditIndexed = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pAuditIndexed) 
    If pAuditIndexed IsNot Nothing Then Return pAuditIndexed Else Return New csAuditIndexed() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OriginalID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOriginalID(ByVal vOriginalID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OriginalID = vOriginalID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOriginalID with vOriginalID of {vOriginalID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OriginalID = vOriginalID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableName(ByVal vTableName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTableName = vTableName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.TableName.ToLowerInvariant() = vTableName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTableName with vTableName of {vTableName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.TableName.ToLowerInvariant() = vTableName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRowID(ByVal vRowID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRowID with vRowID of {vRowID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Operation
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOperation(ByVal vOperation As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOperation = vOperation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.Operation.ToLowerInvariant() = vOperation Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOperation with vOperation of {vOperation}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.Operation.ToLowerInvariant() = vOperation Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OccurredAt
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOccurredAt(ByVal vOccurredAt As Date) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OccurredAt = vOccurredAt Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOccurredAt with vOccurredAt of {vOccurredAt}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OccurredAt = vOccurredAt Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlCurrentUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlCurrentUser(ByVal vSqlCurrentUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlCurrentUser = vSqlCurrentUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlCurrentUser with vSqlCurrentUser of {vSqlCurrentUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlCurrentUser.ToLowerInvariant() = vSqlCurrentUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FieldName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFieldName(ByVal vFieldName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFieldName = vFieldName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.FieldName.ToLowerInvariant() = vFieldName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFieldName with vFieldName of {vFieldName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.FieldName.ToLowerInvariant() = vFieldName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined OldValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByOldValue(ByVal vOldValue As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vOldValue = vOldValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.OldValue.ToLowerInvariant() = vOldValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByOldValue with vOldValue of {vOldValue}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.OldValue.ToLowerInvariant() = vOldValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NewValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNewValue(ByVal vNewValue As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNewValue = vNewValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.NewValue.ToLowerInvariant() = vNewValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNewValue with vNewValue of {vNewValue}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.NewValue.ToLowerInvariant() = vNewValue Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ChangedByUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByChangedByUser(ByVal vChangedByUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vChangedByUser = vChangedByUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByChangedByUser with vChangedByUser of {vChangedByUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.ChangedByUser.ToLowerInvariant() = vChangedByUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActiveLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActiveLoginID(ByVal vActiveLoginID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.ActiveLoginID = vActiveLoginID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActiveLoginID with vActiveLoginID of {vActiveLoginID}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.ActiveLoginID = vActiveLoginID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlSystemUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlSystemUser(ByVal vSqlSystemUser As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlSystemUser = vSqlSystemUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlSystemUser with vSqlSystemUser of {vSqlSystemUser}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlSystemUser.ToLowerInvariant() = vSqlSystemUser Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlAppName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlAppName(ByVal vSqlAppName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlAppName = vSqlAppName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlAppName with vSqlAppName of {vSqlAppName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlAppName.ToLowerInvariant() = vSqlAppName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SqlHostName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySqlHostName(ByVal vSqlHostName As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSqlHostName = vSqlHostName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySqlHostName with vSqlHostName of {vSqlHostName}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.SqlHostName.ToLowerInvariant() = vSqlHostName Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csAuditIndexed) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In pTempDist.Values
        If pAuditIndexed.Tag.ToLowerInvariant() = vTag Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.Tag.ToLowerInvariant() = vTag Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    
    Return pAuditIndexeds
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableNameAndRowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableNameAndRowID(ByVal vTableName As String, ByVal vRowID As Long) As csAuditIndexedCol
    Dim pAuditIndexeds As New csAuditIndexedCol() 
    pAuditIndexeds._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pAuditIndexed As csAuditIndexed In _SortedDictionaryForFindByID.Values.ToList()
        If pAuditIndexed.TableName = vTableName AndAlso pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csAuditIndexedCol = Me.Clone() 
      For Each pAuditIndexed As csAuditIndexed In pList 
        If pAuditIndexed.TableName = vTableName AndAlso pAuditIndexed.RowID = vRowID Then
          Dim pAuditIndexedClone As csAuditIndexed = pAuditIndexed.Clone()
          pAuditIndexeds.Add(pAuditIndexedClone)
          If Not _FilledFromSumOnTheFly Then pAuditIndexeds._Clean.Add(pAuditIndexed.ID) 
        End If
      Next
    End If 
    Return pAuditIndexeds
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
    For Each pAuditIndexed As csAuditIndexed In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pAuditIndexed.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csAuditIndexedCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ActiveLoginID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByActiveLoginID(ByVal vActiveLoginID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginID={0}", vActiveLoginID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByActiveLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByActiveLoginID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByActiveLoginID(vActiveLoginID) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ActiveLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ChangedByUser 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByChangedByUser(ByVal vChangedByUser As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}", vChangedByUser)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByChangedByUser"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByChangedByUser(vChangedByUser) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedByUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUser) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FieldName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByFieldName(ByVal vFieldName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}", vFieldName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByFieldName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByFieldName(vFieldName) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "FieldName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OccurredAt 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOccurredAt(ByVal vOccurredAt As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAt={0}", vOccurredAt)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByOccurredAt", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByOccurredAt"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByOccurredAt(vOccurredAt) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OccurredAt" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vOccurredAt) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OriginalID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByOriginalID(ByVal vOriginalID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalID={0}", vOriginalID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByOriginalID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByOriginalID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByOriginalID(vOriginalID) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OriginalID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vOriginalID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific RowID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByRowID(ByVal vRowID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("RowID={0}", vRowID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByRowID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByRowID(vRowID) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "RowID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vRowID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TableName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTableName(ByVal vTableName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}", vTableName)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByTableName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByTableName(vTableName) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TableNameAndRowID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTableNameAndRowID(ByVal vTableName As String, ByVal vRowID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, RowID={1}", vTableName, vRowID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByTableNameAndRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByTableName&RowID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllAuditIndexeds As New csAuditIndexedCol() : pAllAuditIndexeds.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredAuditIndexeds As csAuditIndexedCol = pAllAuditIndexeds.CloneByTableNameAndRowID(vTableName, vRowID) 
      For Each l In pFilteredAuditIndexeds 
        pAllAuditIndexeds.Remove(pAllAuditIndexeds.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllAuditIndexeds, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableName) 
        pLastReadVariableName = "RowID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vRowID) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ActiveLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedActiveLoginID(ByVal vActiveLoginIDFrom As Long, ByVal vActiveLoginIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ActiveLoginIDFrom={0}, ActiveLoginIDTo={1}", vActiveLoginIDFrom, vActiveLoginIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedActiveLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedActiveLoginID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ActiveLoginIDFrom" 
        pDALParameters.Add("bndActiveLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginIDFrom) 
        pLastReadVariableName = "ActiveLoginIDTo" 
        pDALParameters.Add("bndActiveLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vActiveLoginIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ChangedByUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedChangedByUser(ByVal vChangedByUserFrom As String, ByVal vChangedByUserTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUserFrom={0}, ChangedByUserTo={1}", vChangedByUserFrom, vChangedByUserTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedChangedByUser"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedByUserFrom" 
        pDALParameters.Add("bndChangedByUserFrom", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUserFrom) 
        pLastReadVariableName = "ChangedByUserTo" 
        pDALParameters.Add("bndChangedByUserTo", ccDAL.enmSQLDataType.NVarChar, 50).Value = (vChangedByUserTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific FieldName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedFieldName(ByVal vFieldNameFrom As String, ByVal vFieldNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldNameFrom={0}, FieldNameTo={1}", vFieldNameFrom, vFieldNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedFieldName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "FieldNameFrom" 
        pDALParameters.Add("bndFieldNameFrom", ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldNameFrom) 
        pLastReadVariableName = "FieldNameTo" 
        pDALParameters.Add("bndFieldNameTo", ccDAL.enmSQLDataType.VarChar, 100).Value = (vFieldNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OccurredAt
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedOccurredAt(ByVal vOccurredAtStart As Date, ByVal vOccurredAtEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OccurredAtStart={0}, OccurredAtEnd={1}", vOccurredAtStart, vOccurredAtEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedOccurredAt", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedOccurredAt"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OccurredAtFrom" 
        pDALParameters.Add("bndOccurredAtFrom", ccDAL.enmSQLDataType.DateTime).Value = (vOccurredAtStart) 
        pLastReadVariableName = "OccurredAtTo" 
        pDALParameters.Add("bndOccurredAtTo", ccDAL.enmSQLDataType.DateTime).Value = (vOccurredAtEnd) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific OriginalID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedOriginalID(ByVal vOriginalIDFrom As Long, ByVal vOriginalIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("OriginalIDFrom={0}, OriginalIDTo={1}", vOriginalIDFrom, vOriginalIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedOriginalID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedOriginalID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "OriginalIDFrom" 
        pDALParameters.Add("bndOriginalIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vOriginalIDFrom) 
        pLastReadVariableName = "OriginalIDTo" 
        pDALParameters.Add("bndOriginalIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vOriginalIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific RowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedRowID(ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("RowIDFrom={0}, RowIDTo={1}", vRowIDFrom, vRowIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedRowID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}", vTableNameFrom, vTableNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedTableName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TableNameFrom" 
        pDALParameters.Add("bndTableNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameFrom) 
        pLastReadVariableName = "TableNameTo" 
        pDALParameters.Add("bndTableNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TableNameAndRowID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTableNameAndRowID(ByVal vTableNameFrom As String, ByVal vTableNameTo As String, ByVal vRowIDFrom As Long, ByVal vRowIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TableNameFrom={0}, TableNameTo={1}, RowIDFrom={2}, RowIDTo={3}", vTableNameFrom, vTableNameTo, vRowIDFrom, vRowIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByBoundedTableNameAndRowID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_AuditIndexedsDeleteByBoundedTableName&RowID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "TableNameFrom" 
        pDALParameters.Add("bndTableNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameFrom) 
        pLastReadVariableName = "TableNameTo" 
        pDALParameters.Add("bndTableNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vTableNameTo) 
        pLastReadVariableName = "RowIDFrom" 
        pDALParameters.Add("bndRowIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDFrom) 
        pLastReadVariableName = "RowIDTo" 
        pDALParameters.Add("bndRowIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vRowIDTo) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded ChangedByUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardChangedByUser(ByVal vChangedByUser As String, ByVal vChangedByUserWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ChangedByUser={0}, ChangedByUserWildcardType={1}", vChangedByUser, vChangedByUserWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByWildCardChangedByUser", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ChangedByUser 
    Dim pWCChangedByUser As String = "" 
    If vChangedByUserWildcardType = clsEnums.enmWildCardType.After Then 
      pWCChangedByUser = vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCChangedByUser = "%" & vChangedByUser 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCChangedByUser = "%" & vChangedByUser & "%" 
    ElseIf vChangedByUserWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vChangedByUser.ToCharArray 
        pWCChangedByUser &= p & "%" 
      Next 
      pWCChangedByUser = "%" & pWCChangedByUser 
    End If 
    
    Dim pCommandText As String = "c_AuditIndexedsDeleteByWildCardChangedByUser"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldChangedByUser" 
        pDALParameters.Add("wldChangedByUser", ccDAL.enmSQLDataType.NVarChar, 50).Value = (pWCChangedByUser) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded FieldName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardFieldName(ByVal vFieldName As String, ByVal vFieldNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("FieldName={0}, FieldNameWildcardType={1}", vFieldName, vFieldNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByWildCardFieldName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'FieldName 
    Dim pWCFieldName As String = "" 
    If vFieldNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCFieldName = vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCFieldName = "%" & vFieldName 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCFieldName = "%" & vFieldName & "%" 
    ElseIf vFieldNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vFieldName.ToCharArray 
        pWCFieldName &= p & "%" 
      Next 
      pWCFieldName = "%" & pWCFieldName 
    End If 
    
    Dim pCommandText As String = "c_AuditIndexedsDeleteByWildCardFieldName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldFieldName" 
        pDALParameters.Add("wldFieldName", ccDAL.enmSQLDataType.VarChar, 100).Value = (pWCFieldName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}, TableNameWildcardType={1}", vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_AuditIndexedDelete, "csAuditIndexedCol_DeleteByWildCardTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'TableName 
    Dim pWCTableName As String = "" 
    If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCTableName = vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCTableName = "%" & vTableName 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCTableName = "%" & vTableName & "%" 
    ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vTableName.ToCharArray 
        pWCTableName &= p & "%" 
      Next 
      pWCTableName = "%" & pWCTableName 
    End If 
    
    Dim pCommandText As String = "c_AuditIndexedsDeleteByWildCardTableName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-AuditIndexed-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldTableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCTableName) 
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-AuditIndexed-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-AuditIndexed-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-090219-1632", vRequester) 
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
    Me.Sort(New csAuditIndexedCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
  
  Public Sub SortByOriginalID()
    Me.Sort(New csAuditIndexedCol.CompareByOriginalID)
  End Sub
  Private Class CompareByOriginalID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.OriginalID < y.OriginalID Then
        Return -1
      ElseIf x.OriginalID = y.OriginalID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTableName()
    Me.Sort(New csAuditIndexedCol.CompareByTableName)
  End Sub
  Private Class CompareByTableName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRowID()
    Me.Sort(New csAuditIndexedCol.CompareByRowID)
  End Sub
  Private Class CompareByRowID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RowID < y.RowID Then
        Return -1
      ElseIf x.RowID = y.RowID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByOperation()
    Me.Sort(New csAuditIndexedCol.CompareByOperation)
  End Sub
  Private Class CompareByOperation
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Operation, y.Operation, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOccurredAt()
    Me.Sort(New csAuditIndexedCol.CompareByOccurredAt)
  End Sub
  Private Class CompareByOccurredAt
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
    Me.Sort(New csAuditIndexedCol.CompareBySqlCurrentUser)
  End Sub
  Private Class CompareBySqlCurrentUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlCurrentUser, y.SqlCurrentUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFieldName()
    Me.Sort(New csAuditIndexedCol.CompareByFieldName)
  End Sub
  Private Class CompareByFieldName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FieldName, y.FieldName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByOldValue()
    Me.Sort(New csAuditIndexedCol.CompareByOldValue)
  End Sub
  Private Class CompareByOldValue
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.OldValue, y.OldValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNewValue()
    Me.Sort(New csAuditIndexedCol.CompareByNewValue)
  End Sub
  Private Class CompareByNewValue
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.NewValue, y.NewValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByChangedByUser()
    Me.Sort(New csAuditIndexedCol.CompareByChangedByUser)
  End Sub
  Private Class CompareByChangedByUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ChangedByUser, y.ChangedByUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByActiveLoginID()
    Me.Sort(New csAuditIndexedCol.CompareByActiveLoginID)
  End Sub
  Private Class CompareByActiveLoginID
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
    Me.Sort(New csAuditIndexedCol.CompareBySqlSystemUser)
  End Sub
  Private Class CompareBySqlSystemUser
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlSystemUser, y.SqlSystemUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlAppName()
    Me.Sort(New csAuditIndexedCol.CompareBySqlAppName)
  End Sub
  Private Class CompareBySqlAppName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlAppName, y.SqlAppName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySqlHostName()
    Me.Sort(New csAuditIndexedCol.CompareBySqlHostName)
  End Sub
  Private Class CompareBySqlHostName
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SqlHostName, y.SqlHostName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csAuditIndexedCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csAuditIndexed)
    Private Function Compare(ByVal x As csAuditIndexed, ByVal y As csAuditIndexed) As Integer Implements System.Collections.Generic.IComparer(Of csAuditIndexed).Compare
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
  
    Dim pAuditIndexed As csAuditIndexed
  
    While vReader.Read()
      pAuditIndexed = New csAuditIndexed() 
      pFault = pAuditIndexed.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pAuditIndexed)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pAuditIndexed.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedAuditIndexedCol As csAuditIndexedCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pAuditIndexed As csAuditIndexed 
 
      For Each pCachedAuditIndexed As csAuditIndexed In vCachedAuditIndexedCol 
        pAuditIndexed = New csAuditIndexed(pCachedAuditIndexed) 
        pAuditIndexed.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pAuditIndexed) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pAuditIndexed.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-AuditIndexed-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAuditIndexed) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csAuditIndexed) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
