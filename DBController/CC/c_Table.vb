Public Class csTable
  Inherits cTargCCEntity 
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
    [Name] 
    [DefaultTextFields] 
    [UsedForIdentity] 
    [IsSingleRow] 
    [CanAdd] 
    [CanEdit] 
    [CanDelete] 
    [AuditAdd] 
    [AuditEdit] 
    [AuditDelete] 
    [TrackRowChangers] 
    [CreateUIMenu] 
    [CreateUICollection] 
    [CreateUIEntity] 
    [SortOrder] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [SortOrder] 
  End Enum 
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  
  Private _IsCleanForXML As Boolean 
  <Newtonsoft.Json.JsonIgnore> 
  Public ReadOnly Property IsCleanForXML As Boolean 
    Get 
      Return _IsCleanForXML 
    End Get 
  End Property 
  
  
  Private _ID As Long
  Private _Name As String
  Private _DefaultTextFields As String
  Private _UsedForIdentity As Boolean
  Private _IsSingleRow As Boolean
  Private _CanAdd As String
  Private _CanEdit As String
  Private _CanDelete As String
  Private _AuditAdd As Boolean
  Private _AuditEdit As Boolean
  Private _AuditDelete As Boolean
  Private _TrackRowChangers As Boolean
  Private _CreateUIMenu As Boolean
  Private _CreateUICollection As Boolean
  Private _CreateUIEntity As Boolean
  Private _SortOrder As Integer
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
  Public Property [Name]() As String
    Get
      Return Me._Name
    End Get
    Set(ByVal value As String)
      If Me._Name <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Name = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [DefaultTextFields]() As String
    Get
      Return Me._DefaultTextFields
    End Get
    Set(ByVal value As String)
      If Me._DefaultTextFields <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DefaultTextFields = value 
      End If 
    End Set
  End Property
  Public Property [UsedForIdentity]() As Boolean
    Get
      Return Me._UsedForIdentity
    End Get
    Set(ByVal value As Boolean)
      If Me._UsedForIdentity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UsedForIdentity = value 
      End If 
    End Set
  End Property
  Public Property [IsSingleRow]() As Boolean
    Get
      Return Me._IsSingleRow
    End Get
    Set(ByVal value As Boolean)
      If Me._IsSingleRow <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsSingleRow = value 
      End If 
    End Set
  End Property
  Public Property [CanAdd]() As String
    Get
      Return Me._CanAdd
    End Get
    Set(ByVal value As String)
      If Me._CanAdd <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CanAdd = value 
      End If 
    End Set
  End Property
  Public Property [CanEdit]() As String
    Get
      Return Me._CanEdit
    End Get
    Set(ByVal value As String)
      If Me._CanEdit <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CanEdit = value 
      End If 
    End Set
  End Property
  Public Property [CanDelete]() As String
    Get
      Return Me._CanDelete
    End Get
    Set(ByVal value As String)
      If Me._CanDelete <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CanDelete = value 
      End If 
    End Set
  End Property
  Public Property [AuditAdd]() As Boolean
    Get
      Return Me._AuditAdd
    End Get
    Set(ByVal value As Boolean)
      If Me._AuditAdd <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AuditAdd = value 
      End If 
    End Set
  End Property
  Public Property [AuditEdit]() As Boolean
    Get
      Return Me._AuditEdit
    End Get
    Set(ByVal value As Boolean)
      If Me._AuditEdit <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AuditEdit = value 
      End If 
    End Set
  End Property
  Public Property [AuditDelete]() As Boolean
    Get
      Return Me._AuditDelete
    End Get
    Set(ByVal value As Boolean)
      If Me._AuditDelete <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AuditDelete = value 
      End If 
    End Set
  End Property
  Public Property [TrackRowChangers]() As Boolean
    Get
      Return Me._TrackRowChangers
    End Get
    Set(ByVal value As Boolean)
      If Me._TrackRowChangers <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TrackRowChangers = value 
      End If 
    End Set
  End Property
  Public Property [CreateUIMenu]() As Boolean
    Get
      Return Me._CreateUIMenu
    End Get
    Set(ByVal value As Boolean)
      If Me._CreateUIMenu <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CreateUIMenu = value 
      End If 
    End Set
  End Property
  Public Property [CreateUICollection]() As Boolean
    Get
      Return Me._CreateUICollection
    End Get
    Set(ByVal value As Boolean)
      If Me._CreateUICollection <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CreateUICollection = value 
      End If 
    End Set
  End Property
  Public Property [CreateUIEntity]() As Boolean
    Get
      Return Me._CreateUIEntity
    End Get
    Set(ByVal value As Boolean)
      If Me._CreateUIEntity <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CreateUIEntity = value 
      End If 
    End Set
  End Property
  Public Property [SortOrder]() As Integer
    Get
      Return Me._SortOrder
    End Get
    Set(ByVal value As Integer)
      If Me._SortOrder <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SortOrder = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _Name Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _Name <> "" Then pValue.Append("Name='" & _Name & "' ‡ ") 
    If _DefaultTextFields <> "" Then pValue.Append("DefaultTextFields='" & _DefaultTextFields & "' ‡ ") 
    pValue.Append("UsedForIdentity='" & _UsedForIdentity.ToString() & "' ‡ ") 
    pValue.Append("IsSingleRow='" & _IsSingleRow.ToString() & "' ‡ ") 
    If _CanAdd <> "" Then pValue.Append("CanAdd='" & _CanAdd & "' ‡ ") 
    If _CanEdit <> "" Then pValue.Append("CanEdit='" & _CanEdit & "' ‡ ") 
    If _CanDelete <> "" Then pValue.Append("CanDelete='" & _CanDelete & "' ‡ ") 
    pValue.Append("AuditAdd='" & _AuditAdd.ToString() & "' ‡ ") 
    pValue.Append("AuditEdit='" & _AuditEdit.ToString() & "' ‡ ") 
    pValue.Append("AuditDelete='" & _AuditDelete.ToString() & "' ‡ ") 
    pValue.Append("TrackRowChangers='" & _TrackRowChangers.ToString() & "' ‡ ") 
    pValue.Append("CreateUIMenu='" & _CreateUIMenu.ToString() & "' ‡ ") 
    pValue.Append("CreateUICollection='" & _CreateUICollection.ToString() & "' ‡ ") 
    pValue.Append("CreateUIEntity='" & _CreateUIEntity.ToString() & "' ‡ ") 
    If _SortOrder <> 0 Then pValue.Append("SortOrder='" & _SortOrder.ToString() & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Name)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DefaultTextFields)}""") 
    pCSV.Append(",""" & _UsedForIdentity.ToString() & """") 
    pCSV.Append(",""" & _IsSingleRow.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CanAdd)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CanEdit)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CanDelete)}""") 
    pCSV.Append(",""" & _AuditAdd.ToString() & """") 
    pCSV.Append(",""" & _AuditEdit.ToString() & """") 
    pCSV.Append(",""" & _AuditDelete.ToString() & """") 
    pCSV.Append(",""" & _TrackRowChangers.ToString() & """") 
    pCSV.Append(",""" & _CreateUIMenu.ToString() & """") 
    pCSV.Append(",""" & _CreateUICollection.ToString() & """") 
    pCSV.Append(",""" & _CreateUIEntity.ToString() & """") 
    pCSV.Append("," & _SortOrder.ToString() & "") 
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
  
  Public Sub New(ByVal vcsTable As csTable)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsTable) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vName As String = "" _ 
    , Optional vDefaultTextFields As String = "" _ 
    , Optional vUsedForIdentity As Boolean = False _ 
    , Optional vIsSingleRow As Boolean = False _ 
    , Optional vCanAdd As String = "" _ 
    , Optional vCanEdit As String = "" _ 
    , Optional vCanDelete As String = "" _ 
    , Optional vAuditAdd As Boolean = False _ 
    , Optional vAuditEdit As Boolean = False _ 
    , Optional vAuditDelete As Boolean = False _ 
    , Optional vTrackRowChangers As Boolean = False _ 
    , Optional vCreateUIMenu As Boolean = False _ 
    , Optional vCreateUICollection As Boolean = False _ 
    , Optional vCreateUIEntity As Boolean = False _ 
    , Optional vSortOrder As Integer = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _Name = vName 
    _DefaultTextFields = vDefaultTextFields 
    _UsedForIdentity = vUsedForIdentity 
    _IsSingleRow = vIsSingleRow 
    _CanAdd = vCanAdd 
    _CanEdit = vCanEdit 
    _CanDelete = vCanDelete 
    _AuditAdd = vAuditAdd 
    _AuditEdit = vAuditEdit 
    _AuditDelete = vAuditDelete 
    _TrackRowChangers = vTrackRowChangers 
    _CreateUIMenu = vCreateUIMenu 
    _CreateUICollection = vCreateUICollection 
    _CreateUIEntity = vCreateUIEntity 
    _SortOrder = vSortOrder 
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
 
    _Name = _Name.Truncate(pTruncateLength, _IsTruncated) 
    _DefaultTextFields = _DefaultTextFields.Truncate(pTruncateLength, _IsTruncated) 
    _CanAdd = _CanAdd.Truncate(pTruncateLength, _IsTruncated) 
    _CanEdit = _CanEdit.Truncate(pTruncateLength, _IsTruncated) 
    _CanDelete = _CanDelete.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _Name = ccHelper.RemoveChrW0(_Name) 
    _DefaultTextFields = ccHelper.RemoveChrW0(_DefaultTextFields) 
    _CanAdd = ccHelper.RemoveChrW0(_CanAdd) 
    _CanEdit = ccHelper.RemoveChrW0(_CanEdit) 
    _CanDelete = ccHelper.RemoveChrW0(_CanDelete) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Table by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTable_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Table-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [Name] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Table by the chosen parameters. This function may be a bit slower than accessing the Table's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTable_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.Name 
          pFault = GetByName(CStr(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Table-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Table-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Table by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTable_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"Table not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-Table-210927-1527", vRequester, vAdditionalMessageToUser:=$"Table not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccTableCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccTableCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csTableCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccTableCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TableGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Table not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-Table-210625-0950", vRequester, vAdditionalMessageToUser:=$"Table not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the Table by Name.
  ''' </summary>
  ''' <param name="vName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByName(ByVal vName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Name={0}", vName)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTable_GetByName", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccTableCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccTableCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csTableCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccTableCol.FindByName(vName), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TableGetByName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Name" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vName) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Table not found for GetByName. See FunctionParameters for values", pFunctionParameters, "TRGT-Table-210625-0950", vRequester, vAdditionalMessageToUser:=$"Table not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csTable) Then Return False 
    Dim pTableToTest As csTable = CType(vTargCCEntityToTest, csTable) 
    Return isEqual(pTableToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vTableToTest As csTable) As Boolean
    With vTableToTest
      If _ID <> .ID Then Return False
      If _Name <> .Name Then Return False
      If _DefaultTextFields <> .DefaultTextFields Then Return False
      If _UsedForIdentity <> .UsedForIdentity Then Return False
      If _IsSingleRow <> .IsSingleRow Then Return False
      If _CanAdd <> .CanAdd Then Return False
      If _CanEdit <> .CanEdit Then Return False
      If _CanDelete <> .CanDelete Then Return False
      If _AuditAdd <> .AuditAdd Then Return False
      If _AuditEdit <> .AuditEdit Then Return False
      If _AuditDelete <> .AuditDelete Then Return False
      If _TrackRowChangers <> .TrackRowChangers Then Return False
      If _CreateUIMenu <> .CreateUIMenu Then Return False
      If _CreateUICollection <> .CreateUICollection Then Return False
      If _CreateUIEntity <> .CreateUIEntity Then Return False
      If _SortOrder <> .SortOrder Then Return False
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
    Dim pClone As New csTable(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csTable
    Dim pClone As New csTable(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("Name") = _Name : Catch ex As Exception : Return pFault.LogException(ex, "Name", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("DefaultTextFields") = _DefaultTextFields : Catch ex As Exception : Return pFault.LogException(ex, "DefaultTextFields", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("UsedForIdentity") = _UsedForIdentity : Catch ex As Exception : Return pFault.LogException(ex, "UsedForIdentity", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsSingleRow") = _IsSingleRow : Catch ex As Exception : Return pFault.LogException(ex, "IsSingleRow", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CanAdd") = _CanAdd : Catch ex As Exception : Return pFault.LogException(ex, "CanAdd", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CanEdit") = _CanEdit : Catch ex As Exception : Return pFault.LogException(ex, "CanEdit", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CanDelete") = _CanDelete : Catch ex As Exception : Return pFault.LogException(ex, "CanDelete", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("AuditAdd") = _AuditAdd : Catch ex As Exception : Return pFault.LogException(ex, "AuditAdd", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("AuditEdit") = _AuditEdit : Catch ex As Exception : Return pFault.LogException(ex, "AuditEdit", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("AuditDelete") = _AuditDelete : Catch ex As Exception : Return pFault.LogException(ex, "AuditDelete", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("TrackRowChangers") = _TrackRowChangers : Catch ex As Exception : Return pFault.LogException(ex, "TrackRowChangers", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CreateUIMenu") = _CreateUIMenu : Catch ex As Exception : Return pFault.LogException(ex, "CreateUIMenu", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CreateUICollection") = _CreateUICollection : Catch ex As Exception : Return pFault.LogException(ex, "CreateUICollection", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("CreateUIEntity") = _CreateUIEntity : Catch ex As Exception : Return pFault.LogException(ex, "CreateUIEntity", "TRGT-Table-130316-0852", vRequester) : End Try 
    Try : vDataRow("SortOrder") = _SortOrder : Catch ex As Exception : Return pFault.LogException(ex, "SortOrder", "TRGT-Table-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pTable As csTable = CType(pXmlSerializer.Deserialize(pStreamReader), csTable) 
      AssignValues(pTable) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Table-130515-1230", vRequester) 
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
          'Name 
          If _Name Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Name) 
          'DefaultTextFields 
          If _DefaultTextFields Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_DefaultTextFields) 
          'UsedForIdentity 
          pBinaryWriter.Write(_UsedForIdentity) 
          'IsSingleRow 
          pBinaryWriter.Write(_IsSingleRow) 
          'CanAdd 
          If _CanAdd Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CanAdd) 
          'CanEdit 
          If _CanEdit Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CanEdit) 
          'CanDelete 
          If _CanDelete Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CanDelete) 
          'AuditAdd 
          pBinaryWriter.Write(_AuditAdd) 
          'AuditEdit 
          pBinaryWriter.Write(_AuditEdit) 
          'AuditDelete 
          pBinaryWriter.Write(_AuditDelete) 
          'TrackRowChangers 
          pBinaryWriter.Write(_TrackRowChangers) 
          'CreateUIMenu 
          pBinaryWriter.Write(_CreateUIMenu) 
          'CreateUICollection 
          pBinaryWriter.Write(_CreateUICollection) 
          'CreateUIEntity 
          pBinaryWriter.Write(_CreateUIEntity) 
          'SortOrder 
          pBinaryWriter.Write(_SortOrder) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Table-150307-2338", vRequester) 
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
          'Name 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Name = pReader.ReadString 
          'DefaultTextFields 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _DefaultTextFields = pReader.ReadString 
          'UsedForIdentity 
          _UsedForIdentity = pReader.ReadBoolean 
          'IsSingleRow 
          _IsSingleRow = pReader.ReadBoolean 
          'CanAdd 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CanAdd = pReader.ReadString 
          'CanEdit 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CanEdit = pReader.ReadString 
          'CanDelete 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CanDelete = pReader.ReadString 
          'AuditAdd 
          _AuditAdd = pReader.ReadBoolean 
          'AuditEdit 
          _AuditEdit = pReader.ReadBoolean 
          'AuditDelete 
          _AuditDelete = pReader.ReadBoolean 
          'TrackRowChangers 
          _TrackRowChangers = pReader.ReadBoolean 
          'CreateUIMenu 
          _CreateUIMenu = pReader.ReadBoolean 
          'CreateUICollection 
          _CreateUICollection = pReader.ReadBoolean 
          'CreateUIEntity 
          _CreateUIEntity = pReader.ReadBoolean 
          'SortOrder 
          _SortOrder = pReader.ReadInt32 
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
      rFault.LogException(ex, "", "TRGT-Table-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-190720-1443", vRequester) 
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
 
      Dim pTable As csTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csTable)(vJSON, pSettings) 
      AssignValues(pTable) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vTable As csTable)
    With vTable
      _ID = .ID 
      _Name = .Name 
      _DefaultTextFields = .DefaultTextFields 
      _UsedForIdentity = .UsedForIdentity 
      _IsSingleRow = .IsSingleRow 
      _CanAdd = .CanAdd 
      _CanEdit = .CanEdit 
      _CanDelete = .CanDelete 
      _AuditAdd = .AuditAdd 
      _AuditEdit = .AuditEdit 
      _AuditDelete = .AuditDelete 
      _TrackRowChangers = .TrackRowChangers 
      _CreateUIMenu = .CreateUIMenu 
      _CreateUICollection = .CreateUICollection 
      _CreateUIEntity = .CreateUIEntity 
      _SortOrder = .SortOrder 
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
      pLastReadVariableName = "Name" 
      If Not vReader.IsDBNull(1) Then _Name = vReader.GetString(1) 
      pLastReadVariableName = "DefaultTextFields" 
      If Not vReader.IsDBNull(2) Then _DefaultTextFields = vReader.GetString(2) 
      pLastReadVariableName = "UsedForIdentity" 
      If Not vReader.IsDBNull(3) Then _UsedForIdentity = vReader.GetBoolean(3)
      pLastReadVariableName = "IsSingleRow" 
      If Not vReader.IsDBNull(4) Then _IsSingleRow = vReader.GetBoolean(4)
      pLastReadVariableName = "CanAdd" 
      If Not vReader.IsDBNull(5) Then _CanAdd = vReader.GetString(5) 
      pLastReadVariableName = "CanEdit" 
      If Not vReader.IsDBNull(6) Then _CanEdit = vReader.GetString(6) 
      pLastReadVariableName = "CanDelete" 
      If Not vReader.IsDBNull(7) Then _CanDelete = vReader.GetString(7) 
      pLastReadVariableName = "AuditAdd" 
      If Not vReader.IsDBNull(8) Then _AuditAdd = vReader.GetBoolean(8)
      pLastReadVariableName = "AuditEdit" 
      If Not vReader.IsDBNull(9) Then _AuditEdit = vReader.GetBoolean(9)
      pLastReadVariableName = "AuditDelete" 
      If Not vReader.IsDBNull(10) Then _AuditDelete = vReader.GetBoolean(10)
      pLastReadVariableName = "TrackRowChangers" 
      If Not vReader.IsDBNull(11) Then _TrackRowChangers = vReader.GetBoolean(11)
      pLastReadVariableName = "CreateUIMenu" 
      If Not vReader.IsDBNull(12) Then _CreateUIMenu = vReader.GetBoolean(12)
      pLastReadVariableName = "CreateUICollection" 
      If Not vReader.IsDBNull(13) Then _CreateUICollection = vReader.GetBoolean(13)
      pLastReadVariableName = "CreateUIEntity" 
      If Not vReader.IsDBNull(14) Then _CreateUIEntity = vReader.GetBoolean(14)
      pLastReadVariableName = "SortOrder" 
      If Not vReader.IsDBNull(15) Then _SortOrder = vReader.GetInt32(15)
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedTable As csTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedTable) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _Name = ""
    _DefaultTextFields = ""
    _UsedForIdentity = False
    _IsSingleRow = False
    _CanAdd = ""
    _CanEdit = ""
    _CanDelete = ""
    _AuditAdd = False
    _AuditEdit = False
    _AuditDelete = False
    _TrackRowChangers = False
    _CreateUIMenu = False
    _CreateUICollection = False
    _CreateUIEntity = False
    _SortOrder = 0
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
  
Public Class csTableCol
  Inherits cTargCCCollection(Of csTable)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csTable) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByName As Dictionary(Of String, csTable) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByName As Boolean 
  Private Function CreateKeyForFindByName(ByVal vTable As csTable) As String 
    With vTable 
      Return .Name
    End With 
  End Function 
   
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
 
    For Each pRow As csTable In Me 
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
    pCSVTitle.Append(",""Name""") 
    pCSVTitle.Append(",""DefaultTextFields""") 
    pCSVTitle.Append(",""UsedForIdentity""") 
    pCSVTitle.Append(",""IsSingleRow""") 
    pCSVTitle.Append(",""CanAdd""") 
    pCSVTitle.Append(",""CanEdit""") 
    pCSVTitle.Append(",""CanDelete""") 
    pCSVTitle.Append(",""AuditAdd""") 
    pCSVTitle.Append(",""AuditEdit""") 
    pCSVTitle.Append(",""AuditDelete""") 
    pCSVTitle.Append(",""TrackRowChangers""") 
    pCSVTitle.Append(",""CreateUIMenu""") 
    pCSVTitle.Append(",""CreateUICollection""") 
    pCSVTitle.Append(",""CreateUIEntity""") 
    pCSVTitle.Append(",""SortOrder""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csTable In Me 
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
 
  Public Overloads Sub Add(ByVal vTable As csTable) 
    SyncLock _CollectionLock 
      MyBase.Add(vTable) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vTable As csTable) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vTable) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vTableCol As csTableCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vTableCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vTable As csTable) 
    SyncLock _CollectionLock 
      MyBase.Remove(vTable) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByName = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csTable) 
      
      For Each lTable In Me 
        If lTable.IsEmpty OrElse pTempDictionary.ContainsKey(lTable.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lTable.ID, lTable) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lTable.ToString, "TRGT-Table-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Table:" & lTable.ToString() & ", TRGT-Table-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csTable)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lTable In Me 
        Try 
          Dim pName As String = CreateKeyForFindByName(lTable) 
          If String.IsNullOrEmpty(pName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pName)) Then 
            pTempDictionary.Add(pName, lTable) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lTable.ToString, "TRGT-Table-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByName:" & ex.Message & ", Table:" & lTable.ToString() & ", TRGT-Table-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByName = False
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
 
    For Each lTable As csTable In Me 
      lTable.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lTable As csTable In Me 
      lTable.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Tables by the chosen parameters. This function may be a bit slower than accessing the Table's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Table-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Table-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccTableCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccTableCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csTableCol failed: " & pResponse) 
      Dim pTablesCached As csTableCol = MyController.DBCache.ccTableCol.Clone() 
      pTablesCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pTablesCached.Reverse() 
      If vHowMany > 0 AndAlso pTablesCached.Count > vHowMany Then 
        Dim tmp As New csTableCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pTablesCached(i)) 
        Next 
        pTablesCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pTablesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccTableCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccTableCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csTableCol failed: " & pResponse) 
      Dim pTablesCached As csTableCol = MyController.DBCache.ccTableCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pTablesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Name, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedName(ByVal vNameFrom As String, ByVal vNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("NameFrom={0}, NameTo={1}", vNameFrom, vNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillByBoundedName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccTableCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccTableCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csTableCol failed: " & pResponse) 
      Dim pTablesCached As csTableCol = MyController.DBCache.ccTableCol.CloneByBoundedName(vNameFrom, vNameTo)
      pFault = LoadMeFromDBCache(pTablesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFillByBoundedName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "NameFrom" 
        pDALParameters.Add("bndNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vNameFrom) 
        pLastReadVariableName = "NameTo" 
        pDALParameters.Add("bndNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded Name, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardName(ByVal vName As String, ByVal vNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Name={0}, NameWildcardType={1}", vName, vNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillByWildCardName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Name 
    Dim pWCName As String = "" 
    If vNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCName = vName & "%" 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCName = "%" & vName 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCName = "%" & vName & "%" 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vName.ToCharArray 
        pWCName &= p & "%" 
      Next 
      pWCName = "%" & pWCName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-Table-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFillByWildCardName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldName" 
        pDALParameters.Add("wldName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lTable As New csTable() 
      pFault = lTable.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lTable.IsEmpty Then Me.Add(lTable) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pTables As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pTables, "csTableCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pTables IsNot Nothing AndAlso Me.Count <> pTables.Count Then FillFromListOfITargCCEntity(pTables) 
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
    [Name]
    NameWildcardType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pName As String = Nothing
    Dim pNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Name) Then pObj = vParameters(enmFillOnTheFlyParameters.Name) : If pObj IsNot Nothing Then pName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.NameWildcardType) : If pObj IsNot Nothing Then pNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pName, pNameWildcardType _
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
        , ByVal vName As String, ByVal vNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, Name={2}, NameWildcardType={3}", vIDFrom, vIDTo, vName, vNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Name 
    Dim pWCName As String = "" 
    If vName = Nothing Then 
      pWCName = vName
    Else 
      If vNameWildcardType = clsEnums.enmWildCardType.None OrElse vNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCName = vName
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCName = vName & "%" 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCName = "%" & vName 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCName = "%" & vName & "%" 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vName.ToCharArray 
          pWCName &= p & "%" 
        Next 
        pWCName = "%" & pWCName 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Table-121122-2008", vRequester) 
      Dim pTablesCached As csTableCol = MyController.DBCache.ccTableCol.Clone() 
      Dim pTablesToUse As New csTableCol() 
      For Each l In pTablesCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vName) Then 
          If vNameWildcardType = clsEnums.enmWildCardType.UD OrElse vNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Name.Equals(vName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Name.StartsWith(vName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Name.EndsWith(vName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Name.IndexOf(vName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        pTablesToUse.Add(l) 
      Next 
      pTablesToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pTablesToUse.Reverse() 
      If vHowMany > 0 AndAlso pTablesToUse.Count > vHowMany Then 
        Dim tmp As New csTableCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pTablesToUse(i)) 
        Next 
        pTablesToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pTablesToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "Name" 
        pDALParameters.Add("wldName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090303-1658", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pName As String = Nothing
    Dim pNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Name) Then pObj = vParameters(enmFillOnTheFlyParameters.Name) : If pObj IsNot Nothing Then pName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.NameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.NameWildcardType) : If pObj IsNot Nothing Then pNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pName, pNameWildcardType _
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
        , ByVal vName As String, ByVal vNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, Name={2}, NameWildcardType={3}", vIDFrom, vIDTo, vName, vNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_TableView, "csTableCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Name 
    Dim pWCName As String = "" 
    If vName = Nothing Then 
      pWCName = vName
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.None OrElse vNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCName = vName
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCName = vName & "%" 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCName = "%" & vName 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCName = "%" & vName & "%" 
    ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vName.ToCharArray 
        pWCName &= p & "%" 
      Next 
      pWCName = "%" & pWCName 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Table-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_TablesFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "Name" 
        pDALParameters.Add("wldName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCName) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vTableArray As csTable())
    Me.Clear()
    
    For Each pTable As csTable In vTableArray
      Me.Add(pTable)
      _Clean.Add(pTable.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pTable As New csTable(pRow, vRequester) 
        Me.Add(pTable) 
        _Clean.Add(pTable.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-TableCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-130515-1300", vRequester) 
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
      Dim pTables As csTableCol = CType(pXmlSerializer.Deserialize(pStreamReader), csTableCol) 
      For Each pTable As csTable In pTables 
        Me.Add(pTable) 
        _Clean.Add(pTable.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Table-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-190720-1443", vRequester) 
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
 
      Dim pTables As List(Of csTable) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csTable))(vJSON, pSettings) 
      For Each pTable As csTable In pTables 
        Me.Add(pTable) 
        _Clean.Add(pTable.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Table-190720-2059", vRequester) 
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
          For Each lTable As csTable In Me 
            Dim pByte As Byte() = lTable.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Table-150307-2340", vRequester) 
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
            Dim pTable As csTable = New csTable(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pTable) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pTable.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Table-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pTable As csTable In Me 
      With pTable 
        pFault = pTable.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csTableCol) Then Return False 
    Dim pTableColToTest As csTableCol = CType(vEntitiesToTest, csTableCol) 
    Return isEqual(pTableColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTablesToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vTablesToTest As csTableCol) As Boolean
    If Me.Count <> vTablesToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vTablesToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pTables As New csTableCol() 
    If pFilledFromSumOnTheFly Then pTables._FilledFromSumOnTheFly = True
    
    For Each pTable As csTable In Me 
      Dim pTableClone As csTable = pTable.Clone() 
      pTables.Add(pTableClone) 
      If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
    Next 
    Return pTables 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csTableCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pTables As New csTableCol() 
    If pFilledFromSumOnTheFly Then pTables._FilledFromSumOnTheFly = True
    
    For Each pTable As csTable In Me
      Dim pTableClone As csTable = pTable.Clone()
      pTables.Add(pTableClone)
      If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
    Next
    Return pTables
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csTableCol 
    Dim pTables As New csTableCol()  
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTable As csTable In _SortedDictionaryForFindByID.Values.ToList() 
      If (pTable.ID > vIDFrom AndAlso pTable.ID <= vIDTo) Then 
        Dim pTableClone As csTable = pTable.Clone() 
        pTables.Add(pTableClone) 
        If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
      End If 
    Next 
    Return pTables 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Name (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedName(ByVal vNameFrom As String, ByVal vNameTo As String) As csTableCol 
    Dim pTables As New csTableCol()  
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTable As csTable In _SortedDictionaryForFindByID.Values.ToList() 
      If (pTable.Name > vNameFrom AndAlso pTable.Name <= vNameTo) Then 
        Dim pTableClone As csTable = pTable.Clone() 
        pTables.Add(pTableClone) 
        If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
      End If 
    Next 
    Return pTables 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardName(ByVal vName As String, ByVal vNameWildcardType As clsEnums.enmWildCardType) As csTableCol 
    Dim pTables As New csTableCol 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pTable As csTable In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pTable.Name.StartsWith(vName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pTable.Name.EndsWith(vName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pTable.Name.IndexOf(vName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pTable.Name.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pTableClone As csTable = pTable.Clone() 
      pTables.Add(pTableClone) 
    Next 
    Return pTables 
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
  Public Function FindByID(ByVal vID As Long) As csTable
    If Me.Count = 0 Then Return New csTable 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
    
    Dim pTable As csTable = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pTable) 
    If pTable IsNot Nothing Then Return pTable Else Return New csTable() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByName(ByVal vName As String) As csTable
    If Me.Count = 0 Then Return New csTable 
    
    If _RecreateDictionaryForFindByName = True Then LoadNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csTable) = _SortedDictionaryForFindByName 
    
    Dim pTable As csTable = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pTable) 
    If pTable IsNot Nothing Then Return pTable Else Return New csTable() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Name
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByName(ByVal vName As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vName = vName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.Name.ToLowerInvariant() = vName Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByName with vName of {vName}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.Name.ToLowerInvariant() = vName Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DefaultTextFields
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDefaultTextFields(ByVal vDefaultTextFields As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDefaultTextFields = vDefaultTextFields.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.DefaultTextFields.ToLowerInvariant() = vDefaultTextFields Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDefaultTextFields with vDefaultTextFields of {vDefaultTextFields}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.DefaultTextFields.ToLowerInvariant() = vDefaultTextFields Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UsedForIdentity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUsedForIdentity(ByVal vUsedForIdentity As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.UsedForIdentity = vUsedForIdentity Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUsedForIdentity with vUsedForIdentity of {vUsedForIdentity}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.UsedForIdentity = vUsedForIdentity Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsSingleRow
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsSingleRow(ByVal vIsSingleRow As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.IsSingleRow = vIsSingleRow Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsSingleRow with vIsSingleRow of {vIsSingleRow}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.IsSingleRow = vIsSingleRow Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CanAdd
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCanAdd(ByVal vCanAdd As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCanAdd = vCanAdd.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CanAdd.ToLowerInvariant() = vCanAdd Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCanAdd with vCanAdd of {vCanAdd}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CanAdd.ToLowerInvariant() = vCanAdd Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CanEdit
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCanEdit(ByVal vCanEdit As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCanEdit = vCanEdit.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CanEdit.ToLowerInvariant() = vCanEdit Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCanEdit with vCanEdit of {vCanEdit}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CanEdit.ToLowerInvariant() = vCanEdit Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CanDelete
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCanDelete(ByVal vCanDelete As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCanDelete = vCanDelete.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CanDelete.ToLowerInvariant() = vCanDelete Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCanDelete with vCanDelete of {vCanDelete}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CanDelete.ToLowerInvariant() = vCanDelete Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AuditAdd
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAuditAdd(ByVal vAuditAdd As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.AuditAdd = vAuditAdd Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAuditAdd with vAuditAdd of {vAuditAdd}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.AuditAdd = vAuditAdd Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AuditEdit
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAuditEdit(ByVal vAuditEdit As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.AuditEdit = vAuditEdit Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAuditEdit with vAuditEdit of {vAuditEdit}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.AuditEdit = vAuditEdit Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AuditDelete
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAuditDelete(ByVal vAuditDelete As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.AuditDelete = vAuditDelete Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAuditDelete with vAuditDelete of {vAuditDelete}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.AuditDelete = vAuditDelete Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TrackRowChangers
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTrackRowChangers(ByVal vTrackRowChangers As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.TrackRowChangers = vTrackRowChangers Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTrackRowChangers with vTrackRowChangers of {vTrackRowChangers}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.TrackRowChangers = vTrackRowChangers Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CreateUIMenu
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCreateUIMenu(ByVal vCreateUIMenu As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CreateUIMenu = vCreateUIMenu Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCreateUIMenu with vCreateUIMenu of {vCreateUIMenu}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CreateUIMenu = vCreateUIMenu Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CreateUICollection
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCreateUICollection(ByVal vCreateUICollection As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CreateUICollection = vCreateUICollection Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCreateUICollection with vCreateUICollection of {vCreateUICollection}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CreateUICollection = vCreateUICollection Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CreateUIEntity
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCreateUIEntity(ByVal vCreateUIEntity As Boolean) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.CreateUIEntity = vCreateUIEntity Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCreateUIEntity with vCreateUIEntity of {vCreateUIEntity}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.CreateUIEntity = vCreateUIEntity Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SortOrder
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySortOrder(ByVal vSortOrder As Integer) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.SortOrder = vSortOrder Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySortOrder with vSortOrder of {vSortOrder}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.SortOrder = vSortOrder Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csTableCol
    Dim pTables As New csTableCol() 
    pTables._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csTable) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pTable As csTable In pTempDist.Values
        If pTable.Tag.ToLowerInvariant() = vTag Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csTableCol = Me.Clone() 
      For Each pTable As csTable In pList 
        If pTable.Tag.ToLowerInvariant() = vTag Then
          Dim pTableClone As csTable = pTable.Clone()
          pTables.Add(pTableClone)
          If Not _FilledFromSumOnTheFly Then pTables._Clean.Add(pTable.ID) 
        End If
      Next
    End If 
    
    Return pTables
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
    For Each pTable As csTable In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pTable.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New csTableCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
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
  
  Public Sub SortByName()
    Me.Sort(New csTableCol.CompareByName)
  End Sub
  Private Class CompareByName
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDefaultTextFields()
    Me.Sort(New csTableCol.CompareByDefaultTextFields)
  End Sub
  Private Class CompareByDefaultTextFields
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DefaultTextFields, y.DefaultTextFields, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUsedForIdentity()
    Me.Sort(New csTableCol.CompareByUsedForIdentity)
  End Sub
  Private Class CompareByUsedForIdentity
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UsedForIdentity.ToString, y.UsedForIdentity.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIsSingleRow()
    Me.Sort(New csTableCol.CompareByIsSingleRow)
  End Sub
  Private Class CompareByIsSingleRow
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsSingleRow.ToString, y.IsSingleRow.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCanAdd()
    Me.Sort(New csTableCol.CompareByCanAdd)
  End Sub
  Private Class CompareByCanAdd
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CanAdd, y.CanAdd, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCanEdit()
    Me.Sort(New csTableCol.CompareByCanEdit)
  End Sub
  Private Class CompareByCanEdit
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CanEdit, y.CanEdit, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCanDelete()
    Me.Sort(New csTableCol.CompareByCanDelete)
  End Sub
  Private Class CompareByCanDelete
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CanDelete, y.CanDelete, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAuditAdd()
    Me.Sort(New csTableCol.CompareByAuditAdd)
  End Sub
  Private Class CompareByAuditAdd
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AuditAdd.ToString, y.AuditAdd.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAuditEdit()
    Me.Sort(New csTableCol.CompareByAuditEdit)
  End Sub
  Private Class CompareByAuditEdit
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AuditEdit.ToString, y.AuditEdit.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAuditDelete()
    Me.Sort(New csTableCol.CompareByAuditDelete)
  End Sub
  Private Class CompareByAuditDelete
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AuditDelete.ToString, y.AuditDelete.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTrackRowChangers()
    Me.Sort(New csTableCol.CompareByTrackRowChangers)
  End Sub
  Private Class CompareByTrackRowChangers
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TrackRowChangers.ToString, y.TrackRowChangers.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCreateUIMenu()
    Me.Sort(New csTableCol.CompareByCreateUIMenu)
  End Sub
  Private Class CompareByCreateUIMenu
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CreateUIMenu.ToString, y.CreateUIMenu.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCreateUICollection()
    Me.Sort(New csTableCol.CompareByCreateUICollection)
  End Sub
  Private Class CompareByCreateUICollection
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CreateUICollection.ToString, y.CreateUICollection.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCreateUIEntity()
    Me.Sort(New csTableCol.CompareByCreateUIEntity)
  End Sub
  Private Class CompareByCreateUIEntity
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CreateUIEntity.ToString, y.CreateUIEntity.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySortOrder()
    Me.Sort(New csTableCol.CompareBySortOrder)
  End Sub
  Private Class CompareBySortOrder
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.SortOrder < y.SortOrder Then
        Return -1
      ElseIf x.SortOrder = y.SortOrder Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csTableCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csTable)
    Private Function Compare(ByVal x As csTable, ByVal y As csTable) As Integer Implements System.Collections.Generic.IComparer(Of csTable).Compare
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
  
    Dim pTable As csTable
  
    While vReader.Read()
      pTable = New csTable() 
      pFault = pTable.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pTable)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pTable.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedTableCol As csTableCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pTable As csTable 
 
      For Each pCachedTable As csTable In vCachedTableCol 
        pTable = New csTable(pCachedTable) 
        pTable.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pTable) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pTable.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Table-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csTable) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByName = New Dictionary(Of String, csTable)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByName = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csTable) 
    _SortedDictionaryForFindByName = New Dictionary(Of String, csTable)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
