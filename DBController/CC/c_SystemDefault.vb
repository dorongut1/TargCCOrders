Public Class csSystemDefault
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
    [SystemDefaultType] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [Group] 
    [SettingName] 
    [SettingValue] 
    [SystemDefaultType] 
    [Description] 
    [Tag] 
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
    [SettingValue] 
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
  Private _Group As String
  Private _SettingName As String
  Private _SettingValue As String
  Private _SystemDefaultType As clsEnums.enmSystemDefaultType
  Private _SystemDefaultTypeText As String 
  Private _Description As String
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
  Public Property [Group]() As String
    Get
      Return Me._Group
    End Get
    Set(ByVal value As String)
      If Me._Group <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Group = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [SettingName]() As String
    Get
      Return Me._SettingName
    End Get
    Set(ByVal value As String)
      If Me._SettingName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SettingName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public ReadOnly Property [SettingValue]() As String
    Get
      Return Me._SettingValue
    End Get
  End Property
  Public Property [SystemDefaultType]() As clsEnums.enmSystemDefaultType
    Get
      Return Me._SystemDefaultType
    End Get
    Set(ByVal value As clsEnums.enmSystemDefaultType)
      If Me._SystemDefaultType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SystemDefaultType = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [SystemDefaultTypeText]() As String
    Get
      Return Me._SystemDefaultTypeText
    End Get
    Set(ByVal value As String)
      Me._SystemDefaultTypeText = value
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _Group & "_" & _SettingName Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _Group <> "" Then pValue.Append("Group='" & _Group & "' ‡ ") 
    If _SettingName <> "" Then pValue.Append("SettingName='" & _SettingName & "' ‡ ") 
    If _SettingValue <> "" Then pValue.Append("SettingValue='" & _SettingValue & "' ‡ ") 
    If _SystemDefaultType <> clsEnums.enmSystemDefaultType.UD Then pValue.Append("SystemDefaultType='" & _SystemDefaultType.FastToString() & "' ‡ ") 
    If _SystemDefaultTypeText <> "" Then pValue.Append("SystemDefaultTypeText='" & _SystemDefaultTypeText & "' ‡ ") 
    If _Description <> "" Then pValue.Append("Description='" & _Description & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Group)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SettingName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SettingValue)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SystemDefaultType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_SystemDefaultTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Description)}""") 
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
  
  Public Sub New(ByVal vcsSystemDefault As csSystemDefault)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsSystemDefault) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vGroup As String = "" _ 
    , Optional vSettingName As String = "" _ 
    , Optional vSettingValue As String = "" _ 
    , Optional vSystemDefaultType As clsEnums.enmSystemDefaultType = clsEnums.enmSystemDefaultType.UD _ 
    , Optional vSystemDefaultTypeText As String = "" _ 
    , Optional vDescription As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _Group = vGroup 
    _SettingName = vSettingName 
    _SettingValue = vSettingValue 
    _SystemDefaultType = vSystemDefaultType 
    _SystemDefaultTypeText = vSystemDefaultTypeText 
    _Description = vDescription 
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
 
    _Group = _Group.Truncate(pTruncateLength, _IsTruncated) 
    _SettingName = _SettingName.Truncate(pTruncateLength, _IsTruncated) 
    _SettingValue = _SettingValue.Truncate(pTruncateLength, _IsTruncated) 
    _Description = _Description.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _Group = ccHelper.RemoveChrW0(_Group) 
    _SettingName = ccHelper.RemoveChrW0(_SettingName) 
    _SettingValue = ccHelper.RemoveChrW0(_SettingValue) 
    _Description = ccHelper.RemoveChrW0(_Description) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SystemDefault by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefault_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemDefault-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [GroupAndSettingName] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the SystemDefault by the chosen parameters. This function may be a bit slower than accessing the SystemDefault's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefault_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.GroupAndSettingName 
          pFault = GetByGroupAndSettingName(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SystemDefault-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemDefault-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the SystemDefault by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefault_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"SystemDefault not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-SystemDefault-210927-1527", vRequester, vAdditionalMessageToUser:=$"SystemDefault not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccSystemDefaultCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"SystemDefault not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-SystemDefault-210625-0950", vRequester, vAdditionalMessageToUser:=$"SystemDefault not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the SystemDefault by GroupAndSettingName.
  ''' </summary>
  ''' <param name="vGroup"></param>
  ''' <param name="vSettingName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByGroupAndSettingName(ByVal vGroup As String, ByVal vSettingName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}, SettingName={1}", vGroup, vSettingName)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefault_GetByGroupAndSettingName", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccSystemDefaultCol.FindByGroupAndSettingName(vGroup, vSettingName), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultGetByGroup&SettingName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Group" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroup) 
        pLastReadVariableName = "SettingName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vSettingName) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"SystemDefault not found for GetByGroupAndSettingName. See FunctionParameters for values", pFunctionParameters, "TRGT-SystemDefault-210625-0950", vRequester, vAdditionalMessageToUser:=$"SystemDefault not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, "csSystemDefault_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-SystemDefault-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, "csSystemDefault_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-SystemDefault-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the SystemDefault. If there are parents or children in the SystemDefault, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, "csSystemDefault_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pSystemDefault As New csSystemDefault() 
    If Me.isEqual(pSystemDefault) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-SystemDefault-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-SystemDefault-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_SystemDefaultUpdate"
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
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pCachedSystemDefault As csSystemDefault 
      If _ID = 0 Then 
        pCachedSystemDefault = New csSystemDefault() 
        'get last ID 
        Dim pSystemDefaultCol As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.Clone() 
        If pSystemDefaultCol.Count = 0 Then 
          _ID = 1 
        Else 
          pSystemDefaultCol.SortByID() 
          Dim pLastID As Long = pSystemDefaultCol(pSystemDefaultCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccSystemDefaultCol.Add(pCachedSystemDefault) 
      Else  
        pCachedSystemDefault = MyController.DBCache.ccSystemDefaultCol.FindByID(_ID) 
      End If 
      pCachedSystemDefault.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemDefaultCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "Group" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_Group) 
        pLastReadVariableName = "SettingName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_SettingName) 
        pLastReadVariableName = "enmSystemDefaultType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_SystemDefaultType.FastToString()) 
        pLastReadVariableName = "Description" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 500).Value = ccHelper.ObjectNullable(_Description) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090623-1809", vRequester) 
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
  
  Public Function UpdateSettingValue(ByVal vSettingValue As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("SystemDefault.ID={0}", _ID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultUpdateSettingValue, "csSystemDefault_UpdateSettingValue", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    If _ID = 0 Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", pFunctionParameters, "TRGT-SystemDefault-100113-1638", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_SystemDefaultUpdateSettingValue"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
    
    Dim pOriginalValueToTest = _SettingValue 
    _SettingValue = vSettingValue 
    RaiseEvent evtBeforeUpdate(enmUpdateType.SettingValue, pCancel) 
    If pCancel = True Then _SettingValue = pOriginalValueToTest : Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.SettingValue, pCommandText, pDALParameters, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then _SettingValue = pOriginalValueToTest : Return pFault 
    If pCancel = True Then Return pFault '_SettingValue stays at the new value in the case of cancel, unless changed in the hook 
  
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pCachedSystemDefault As csSystemDefault 
      If _ID = 0 Then 
        pCachedSystemDefault = New csSystemDefault() 
        'get last ID 
        Dim pSystemDefaultCol As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.Clone() 
        If pSystemDefaultCol.Count = 0 Then 
          _ID = 1 
        Else 
          pSystemDefaultCol.SortByID() 
          Dim pLastID As Long = pSystemDefaultCol(pSystemDefaultCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccSystemDefaultCol.Add(pCachedSystemDefault) 
      Else  
        pCachedSystemDefault = MyController.DBCache.ccSystemDefaultCol.FindByID(_ID) 
      End If 
      pCachedSystemDefault.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemDefaultCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "spt_SettingValue" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 4000).Value = ccHelper.ObjectNullable(_SettingValue) 
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
            If pID = 0 Then 
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    RaiseEvent evtAfterUpdate(enmUpdateType.SettingValue)
    RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.SettingValue, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("SystemDefault.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefault_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_SystemDefaultDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      MyController.DBCache.ccSystemDefaultCol.Remove(MyController.DBCache.ccSystemDefaultCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemDefaultCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = _ID
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefault_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      MyController.DBCache.ccSystemDefaultCol.Remove(MyController.DBCache.ccSystemDefaultCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccSystemDefaultCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = vID 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
 
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
 
        'I expected to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csSystemDefault) Then Return False 
    Dim pSystemDefaultToTest As csSystemDefault = CType(vTargCCEntityToTest, csSystemDefault) 
    Return isEqual(pSystemDefaultToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vSystemDefaultToTest As csSystemDefault) As Boolean
    With vSystemDefaultToTest
      If _ID <> .ID Then Return False
      If _Group <> .Group Then Return False
      If _SettingName <> .SettingName Then Return False
      If _SettingValue <> .SettingValue Then Return False
      If _SystemDefaultType <> .SystemDefaultType Then Return False
      If _Description <> .Description Then Return False
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
    Dim pClone As New csSystemDefault(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csSystemDefault
    Dim pClone As New csSystemDefault(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
    Try : vDataRow("Group") = _Group : Catch ex As Exception : Return pFault.LogException(ex, "Group", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
    Try : vDataRow("SettingName") = _SettingName : Catch ex As Exception : Return pFault.LogException(ex, "SettingName", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
    Try : vDataRow("SettingValue") = _SettingValue : Catch ex As Exception : Return pFault.LogException(ex, "SettingValue", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
    Try : vDataRow("SystemDefaultType") = _SystemDefaultType : Catch ex As Exception : Return pFault.LogException(ex, "SystemDefaultType", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
    Try : vDataRow("Description") = _Description : Catch ex As Exception : Return pFault.LogException(ex, "Description", "TRGT-SystemDefault-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pSystemDefault As csSystemDefault = CType(pXmlSerializer.Deserialize(pStreamReader), csSystemDefault) 
      AssignValues(pSystemDefault) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-SystemDefault-130515-1230", vRequester) 
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
          'Group 
          If _Group Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Group) 
          'SettingName 
          If _SettingName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SettingName) 
          'SettingValue 
          If _SettingValue Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SettingValue) 
          'SystemDefaultType 
          pBinaryWriter.Write(_SystemDefaultType.FastToString()) 
          'Description 
          If _Description Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Description) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-150307-2338", vRequester) 
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
          'Group 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Group = pReader.ReadString 
          'SettingName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SettingName = pReader.ReadString 
          'SettingValue 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SettingValue = pReader.ReadString 
          'SystemDefaultType 
          _SystemDefaultType = clsEnums.TranslateEnmSystemDefaultType(pReader.ReadString) 
          'Description 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Description = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-SystemDefault-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-190720-1443", vRequester) 
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
 
      Dim pSystemDefault As csSystemDefault = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csSystemDefault)(vJSON, pSettings) 
      AssignValues(pSystemDefault) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vSystemDefault As csSystemDefault)
    With vSystemDefault
      _ID = .ID 
      _Group = .Group 
      _SettingName = .SettingName 
      _SettingValue = .SettingValue 
      _SystemDefaultType = .SystemDefaultType 
      _SystemDefaultTypeText = .SystemDefaultTypeText
      _Description = .Description 
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
      'SystemDefaultType 
      pTextToGet = "SystemDefaultTypeText (Enum)" 
      _SystemDefaultTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.SystemDefaultType, _SystemDefaultType.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-SystemDefault-151124-1900", vRequester) 
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
      pLastReadVariableName = "Group" 
      If Not vReader.IsDBNull(1) Then _Group = vReader.GetString(1) 
      pLastReadVariableName = "SettingName" 
      If Not vReader.IsDBNull(2) Then _SettingName = vReader.GetString(2) 
      pLastReadVariableName = "spt_SettingValue" 
      If Not vReader.IsDBNull(3) Then _SettingValue = vReader.GetString(3) 
      pLastReadVariableName = "enmSystemDefaultType" 
      If Not vReader.IsDBNull(4) Then _SystemDefaultType = clsEnums.TranslateEnmSystemDefaultType(vReader.GetString(4))
      pLastReadVariableName = "Description" 
      If Not vReader.IsDBNull(5) Then _Description = vReader.GetString(5) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(6) Then bDateAdded = vReader.GetDateTime(6)   
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedSystemDefault As csSystemDefault, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedSystemDefault) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _Group = ""
    _SettingName = ""
    _SettingValue = ""
    _SystemDefaultType = clsEnums.enmSystemDefaultType.UD
    _SystemDefaultTypeText = ""
    _Description = ""
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
  'Additional functions for csSystemDefault 
  Public Enum enmFullSettingName 
    UD 
    AllowedGuest_GuestSystemName
    Config_AuthenticationHostPassword
    Config_AuthenticationHostRoot
    Config_CacheKeepAliveMin
    Config_CacheOn
    Config_CacheSingleLanguageOnly
    Config_ccAPICompressionMode
    Config_DownloadFileURL
    Config_InTestMode
    Config_IsAuthenticationDoneOnExternalSystem
    Config_IsForgiving
    Config_LocationIpifyURL
    Config_LocationIPRegistryKey
    Config_LocationIPRegistryURL
    Config_LocationProxyCheckKey
    Config_LocationProxyCheckURL
    Config_LogDetails
    Config_ProblemMailTo
    Config_SMSAppHash
    Config_SMSPassword
    Config_SMSSentFrom
    Config_SMSUrl
    Config_SMSUserName
    Config_SMTPDefaultEmailReplyTo
    Config_SMTPDefaultNameReplyTo
    Config_SMTPEmailFrom
    Config_SMTPEnableSSL
    Config_SMTPNameFrom
    Config_SMTPPassword
    Config_SMTPPort
    Config_SMTPServer
    Config_SMTPServerNameForMail
    Config_SMTPUserName
    Config_UploadedFilesRootFolder
    Config_UploadFilePwd
    Config_UploadFileURL
    Config_UploadFileUserName
    Config_UsersToShowEnglishAlso
    Config_XMLDataLocation
    Controller_AlertEmail
    Controller_AlertSMS
    Controller_Applications
    Controller_ccVersion
    Controller_DBControllerVersion
    Controller_DBVersion
    Controller_WSControllerVersion
    Defaults_CountryCodeForSMS
    Defaults_DefaultApplication
    Defaults_DefaultRole
    Defaults_LocalNumberIdentifierForSMS
    Maintenance_DaysToKeep
    Maintenance_RowsToKeep
    RealTime_BlockNonmasterLogin
    Security_ApplicationAuthenticationToWS
    Security_EnableTestOTP
    Security_ForceUserToChangePasswordOnInitialLogin
    Security_IncludeDefaultRoles
    Security_LogRequests
    Security_PasswordExpiryIntervalDays
    Security_RequireSecurePasswords
    Security_UserIdentificationModel
    TestMode_EnableOddMinuteFailures
    TestMode_SendRealSMS
  End Enum 
  ''' <summary> 
  ''' This gets the setting, using an enum of the Group and Setting name, in order to make it easier to call 
  ''' </summary> 
  ''' <param name="vFullSettingName"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Function GetByFullSettingName(ByVal vFullSettingName As enmFullSettingName, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("FullSettingName={0}", vFullSettingName.ToString) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefault_GetByFullSettingName", vRequester)   
    If Not pFault.isOK Then Return pFault   
 
    'Check that we got a valid FullSetting 
    Dim pFullSetting As String = vFullSettingName.ToString() 
    Dim pFullSettingArray As String() = pFullSetting.Split("_"c) 
    If pFullSettingArray.Length <> 2 Then 
      pFault = New clsFault 
      pFault.LogFreeTextFault(57, "Neither Group nor SettingName can have an underscore", pFunctionParameters, "TRGT-150123-1011", vRequester) 
    End If 
 
    Dim pGroup As String = pFullSettingArray(0) 
    If pGroup = "" Then 
      pFault = New clsFault 
      pFault.LogFreeTextFault(57, "Group cannot be blank", pFunctionParameters, "TRGT-150123-1012", vRequester) 
    End If 
    Dim pSettingName As String = pFullSettingArray(1) 
    If pSettingName = "" Then 
      pFault = New clsFault 
      pFault.LogFreeTextFault(57, "SettingName cannot be blank", pFunctionParameters, "TRGT-150123-1014", vRequester) 
    End If 
 
    pFault = GetByGroupAndSettingName(pGroup, pSettingName, vRequester, vMustExist) 
 
    Return pFault 
  End Function 
 
  Private Sub csSystemDefault_evtBeforeUpdateWithRequester(vWhichColumn As enmUpdateType, ByRef rCommandText As String, ByRef rDALParameters As ccDAL.csTargCCParameterCol, ByRef rCancel As Boolean, vRequester As clsRequester, ByRef rFault As clsFault) Handles Me.evtBeforeUpdateWithRequester 
 
    If vWhichColumn = enmUpdateType.SettingValue Then
      If _SettingValue IsNot Nothing AndAlso _SettingValue.Length > 0 Then _SettingValue = SettingValue.Trim 
   
      'check the value received 
      Select Case _SystemDefaultType 
        Case clsEnums.enmSystemDefaultType.Bit 
          If String.IsNullOrEmpty(_SettingValue) Then 
            _SettingValue = "0" 
          ElseIf ccHelper.IsNumeric(_SettingValue) Then 
            If ccHelper.ToInteger(_SettingValue) > 0 Then 
              _SettingValue = "1" 
            End If 
          Else 
            rFault = New clsFault 
            rFault = rFault.LogFreeTextFault(63, "Can only accept 0 or 1", "Received:" & _SettingValue, "TRGT-160123-1301", vRequester) 
            rCancel = True 
          End If 
        Case clsEnums.enmSystemDefaultType.Decimal 
          If String.IsNullOrEmpty(_SettingValue) Then 
            _SettingValue = "0" 
          ElseIf ccHelper.IsNumeric(_SettingValue) Then 
            _SettingValue = ccHelper.ToDecimal(_SettingValue).ToString("#,##0.00000000") 
          Else 
            rFault = New clsFault 
            rFault = rFault.LogFreeTextFault(63, "Can only accept a number, using '.' as a decimal point", "Received:" & _SettingValue, "TRGT-160123-1301", vRequester) 
            rCancel = True 
          End If 
        Case clsEnums.enmSystemDefaultType.Enum 
          If String.IsNullOrEmpty(_SettingValue) Then 
            _SettingValue = "UD" 
          ElseIf ccHelper.IsNumeric(_SettingValue) Then 
            _SettingValue = "UD" 
          End If 
        Case clsEnums.enmSystemDefaultType.Integer 
          If String.IsNullOrEmpty(_SettingValue) Then 
            _SettingValue = "0" 
          ElseIf ccHelper.IsNumeric(_SettingValue) Then 
            _SettingValue = ccHelper.ToInteger(_SettingValue).ToString() 
          Else 
            rFault = New clsFault 
            rFault = rFault.LogFreeTextFault(63, "Can only accept a whole number", "Received:" & _SettingValue, "TRGT-160123-1301", vRequester) 
            rCancel = True 
          End If 
        Case clsEnums.enmSystemDefaultType.Encrypted 
          If _SettingValue.Length > 1500 Then 
            rFault = New clsFault 
            rFault = rFault.LogFreeTextFault(63, $"The value is limited to 1500 characters. I got {_SettingValue.Length} characters", $"Received: {_SettingValue.Substring(0, 100)}.....", "TRGT-210408-1150", vRequester) 
            rCancel = True 
          Else 
            Dim pUnencrypted As String = _SettingValue 
            _SettingValue = ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, pUnencrypted) 
            If _SettingValue.Length > 4000 Then 
              rFault = New clsFault 
              rFault = rFault.LogFreeTextFault(63, $"The encrypted length is limited to 4000 characters. I got {pUnencrypted.Length} characters unencrypted, {_SettingValue.Length} characters encrypted", $"Received: {_SettingValue.Substring(0, 100)}.....", "TRGT-240130-1543", vRequester) 
              rCancel = True 
            End If 
          End If 
        Case clsEnums.enmSystemDefaultType.String 
        Case Else 
      End Select 
   
      'Check to see if we are changing the ExpiryTimeout   
      If _Group = "Security" AndAlso _SettingName = "PasswordExpiryIntervalDays" Then 
        If MyController.IsAuthenticationDoneOnExternalSystem Then 
          rFault.LogFreeTextFault(63, "Since Authentication is done on an externalSystem, this cannot be changed here!", "", "TRGT-240309-124003", vRequester) 
          Exit Sub 
        End If 
 
        'get the existing one 
        Dim pDefault As New csSystemDefault 
        rFault = pDefault.GetByFullSettingName(enmFullSettingName.Security_PasswordExpiryIntervalDays, vRequester, True) : If Not rFault.isOK Then Exit Sub 
   
        _SettingValue = _SettingValue.Trim 
   
        If pDefault.SettingValue.Trim <> _SettingValue Then 
          If _SettingValue <> "" AndAlso ccHelper.IsNumeric(_SettingValue) = False Then 
            rFault.LogFreeTextFault(63, "PasswordExpiryIntervalDays cannot be a string!", "PasswordExpiryIntervalDays=" & _SettingValue, "TRGT-160123-1026", vRequester) 
            Exit Sub 
          End If 
          Dim pPasswordExpiryIntervalDays As Integer = 0 
          If ccHelper.IsNumeric(_SettingValue) Then pPasswordExpiryIntervalDays = ccHelper.ToInteger(_SettingValue) 
          If pPasswordExpiryIntervalDays < 0 Then 
            rFault.LogFreeTextFault(63, "PasswordExpiryIntervalDays cannot be negative", "PasswordExpiryIntervalDays=" & _SettingValue, "TRGT-160123-1027", vRequester) 
            Exit Sub 
          End If 
          'Get the users    
          Dim pUsers As New csUserCol 
          rFault = pUsers.Fill(vRequester) : If Not rFault.isOK Then Exit Sub 
          'Get the roles    
          Dim pRoles As New csRoleCol 
          rFault = pRoles.Fill(vRequester) : If Not rFault.isOK Then Exit Sub 
 
          Dim pMasterRoleID As Long = 0 
          Dim pApplicationMasterRoleID As Long = 0 
          pMasterRoleID = pRoles.FindByName("Master").ID 
          pApplicationMasterRoleID = pRoles.FindByName("ApplicationMaster").ID 
          'update the expiry date   
          For Each l In pUsers 
            If pPasswordExpiryIntervalDays = 0 Then 
              l.ExpiryDate = Nothing 
            Else 
              'Only change if not a master type or Password Expires
              If l.RoleID = pMasterRoleID OrElse l.RoleID = pApplicationMasterRoleID OrElse l.PasswordNeverExpires Then 
                l.ExpiryDate = Nothing 
              Else 
                Dim pDatePasswordChanged As Date = l.DatePasswordChanged 
                If pDatePasswordChanged = DateTime.MinValue Then pDatePasswordChanged = New Date(2000, 1, 1) 
                l.ExpiryDate = pDatePasswordChanged.AddDays(pPasswordExpiryIntervalDays) 
              End If 
            End If 
            l.Tag = "SystemDefaultUpdate" 
            rFault = l.Update(vRequester) : If Not rFault.isOK AndAlso Not (rFault.Severity = clsEnums.enmFaultSeverity.Info OrElse rFault.Severity = clsEnums.enmFaultSeverity.LogOnly) Then Exit Sub 
            l.Tag = "" 
          Next 
        End If 
      End If 
    End If 
  End Sub 
 
  
End Class 
  
Public Class csSystemDefaultCol
  Inherits cTargCCCollection(Of csSystemDefault)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csSystemDefault) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByGroupAndSettingName As Dictionary(Of String, csSystemDefault) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByGroupAndSettingName As Boolean 
  Private Function CreateKeyForFindByGroupAndSettingName(ByVal vSystemDefault As csSystemDefault) As String 
    With vSystemDefault 
      Return .Group & "|" & .SettingName
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
 
    For Each pRow As csSystemDefault In Me 
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
    pCSVTitle.Append(",""Group""") 
    pCSVTitle.Append(",""SettingName""") 
    pCSVTitle.Append(",""SettingValue""") 
    pCSVTitle.Append(",""SystemDefaultType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""SystemDefaultType (Text)""") 
    pCSVTitle.Append(",""Description""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csSystemDefault In Me 
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
 
  Public Overloads Sub Add(ByVal vSystemDefault As csSystemDefault) 
    SyncLock _CollectionLock 
      MyBase.Add(vSystemDefault) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByGroupAndSettingName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vSystemDefault As csSystemDefault) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vSystemDefault) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByGroupAndSettingName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vSystemDefaultCol As csSystemDefaultCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vSystemDefaultCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByGroupAndSettingName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByGroupAndSettingName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vSystemDefault As csSystemDefault) 
    SyncLock _CollectionLock 
      MyBase.Remove(vSystemDefault) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByGroupAndSettingName = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csSystemDefault) 
      
      For Each lSystemDefault In Me 
        If lSystemDefault.IsEmpty OrElse pTempDictionary.ContainsKey(lSystemDefault.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lSystemDefault.ID, lSystemDefault) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lSystemDefault.ToString, "TRGT-SystemDefault-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", SystemDefault:" & lSystemDefault.ToString() & ", TRGT-SystemDefault-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadGroupAndSettingNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByGroupAndSettingName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByGroupAndSettingName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByGroupAndSettingName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByGroupAndSettingName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csSystemDefault)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lSystemDefault In Me 
        Try 
          Dim pGroupAndSettingName As String = CreateKeyForFindByGroupAndSettingName(lSystemDefault) 
          If String.IsNullOrEmpty(pGroupAndSettingName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pGroupAndSettingName)) Then 
            pTempDictionary.Add(pGroupAndSettingName, lSystemDefault) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lSystemDefault.ToString, "TRGT-SystemDefault-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByGroupAndSettingName:" & ex.Message & ", SystemDefault:" & lSystemDefault.ToString() & ", TRGT-SystemDefault-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByGroupAndSettingName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByGroupAndSettingName = False
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
 
    For Each lSystemDefault As csSystemDefault In Me 
      lSystemDefault.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lSystemDefault As csSystemDefault In Me 
      lSystemDefault.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [Group] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the SystemDefaults by the chosen parameters. This function may be a bit slower than accessing the SystemDefault's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.Group 
          pFault = FillByGroup(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-SystemDefault-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-SystemDefault-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.Clone() 
      pSystemDefaultsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pSystemDefaultsCached.Reverse() 
      If vHowMany > 0 AndAlso pSystemDefaultsCached.Count > vHowMany Then 
        Dim tmp As New csSystemDefaultCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pSystemDefaultsCached(i)) 
        Next 
        pSystemDefaultsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pSystemDefaultsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Group, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByGroup(ByVal vGroup As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}", vGroup)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.CloneByGroup(vGroup)
      pFault = LoadMeFromDBCache(pSystemDefaultsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByGroup" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "Group" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroup) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pSystemDefaultsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific GroupAndSettingName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedGroupAndSettingName(ByVal vGroupFrom As String, ByVal vGroupTo As String, ByVal vSettingNameFrom As String, ByVal vSettingNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("GroupFrom={0}, GroupTo={1}, SettingNameFrom={2}, SettingNameTo={3}", vGroupFrom, vGroupTo, vSettingNameFrom, vSettingNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByBoundedGroupAndSettingName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.CloneByBoundedGroupAndSettingName(vGroupFrom, vGroupTo, vSettingNameFrom, vSettingNameTo)
      pFault = LoadMeFromDBCache(pSystemDefaultsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByBoundedGroup&SettingName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "GroupFrom" 
        pDALParameters.Add("bndGroupFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupFrom) 
        pLastReadVariableName = "GroupTo" 
        pDALParameters.Add("bndGroupTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupTo) 
        pLastReadVariableName = "SettingNameFrom" 
        pDALParameters.Add("bndSettingNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSettingNameFrom) 
        pLastReadVariableName = "SettingNameTo" 
        pDALParameters.Add("bndSettingNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSettingNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Group, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedGroup(ByVal vGroupFrom As String, ByVal vGroupTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("GroupFrom={0}, GroupTo={1}", vGroupFrom, vGroupTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByBoundedGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccSystemDefaultCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccSystemDefaultCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csSystemDefaultCol failed: " & pResponse) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.CloneByBoundedGroup(vGroupFrom, vGroupTo)
      pFault = LoadMeFromDBCache(pSystemDefaultsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByBoundedGroup" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "GroupFrom" 
        pDALParameters.Add("bndGroupFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupFrom) 
        pLastReadVariableName = "GroupTo" 
        pDALParameters.Add("bndGroupTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded GroupAndSettingName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardGroupAndSettingName(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType, ByVal vSettingName As String, ByVal vSettingNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}, GroupWildcardType={1}, SettingName={2}, SettingNameWildcardType={3}", vGroup, vGroupWildcardType.FastToString(), vSettingName, vSettingNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByWildCardGroupAndSettingName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
      pWCGroup = vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCGroup = "%" & vGroup 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCGroup = "%" & vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vGroup.ToCharArray 
        pWCGroup &= p & "%" 
      Next 
      pWCGroup = "%" & pWCGroup 
    End If 
    'SettingName 
    Dim pWCSettingName As String = "" 
    If vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSettingName = vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSettingName = "%" & vSettingName 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSettingName = "%" & vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSettingName.ToCharArray 
        pWCSettingName &= p & "%" 
      Next 
      pWCSettingName = "%" & pWCSettingName 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-SystemDefault-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByWildCardGroup&SettingName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldGroup" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCGroup) 
        pLastReadVariableName = "wldSettingName" 
        pDALParameters.Add("wldSettingName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCSettingName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a WildCarded Group, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardGroup(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}, GroupWildcardType={1}", vGroup, vGroupWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByWildCardGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
      pWCGroup = vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCGroup = "%" & vGroup 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCGroup = "%" & vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vGroup.ToCharArray 
        pWCGroup &= p & "%" 
      Next 
      pWCGroup = "%" & pWCGroup 
    End If 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-SystemDefault-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillByWildCardGroup" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldGroup" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCGroup) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lSystemDefault As New csSystemDefault() 
      pFault = lSystemDefault.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lSystemDefault.IsEmpty Then Me.Add(lSystemDefault) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pSystemDefaults As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pSystemDefaults, "csSystemDefaultCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pSystemDefaults IsNot Nothing AndAlso Me.Count <> pSystemDefaults.Count Then FillFromListOfITargCCEntity(pSystemDefaults) 
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
    [Group]
    GroupWildcardType
    [SettingName]
    SettingNameWildcardType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pGroup As String = Nothing
    Dim pGroupWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pSettingName As String = Nothing
    Dim pSettingNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Group) Then pObj = vParameters(enmFillOnTheFlyParameters.Group) : If pObj IsNot Nothing Then pGroup = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.GroupWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.GroupWildcardType) : If pObj IsNot Nothing Then pGroupWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SettingName) Then pObj = vParameters(enmFillOnTheFlyParameters.SettingName) : If pObj IsNot Nothing Then pSettingName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SettingNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.SettingNameWildcardType) : If pObj IsNot Nothing Then pSettingNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pGroup, pGroupWildcardType _
        , pSettingName, pSettingNameWildcardType _
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
        , ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType _
        , ByVal vSettingName As String, ByVal vSettingNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, Group={2}, GroupWildcardType={3}, SettingName={4}, SettingNameWildcardType={5}", vIDFrom, vIDTo, vGroup, vGroupWildcardType.FastToString(), vSettingName, vSettingNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroup = Nothing Then 
      pWCGroup = vGroup
    Else 
      If vGroupWildcardType = clsEnums.enmWildCardType.None OrElse vGroupWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCGroup = vGroup
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.After Then 
        pWCGroup = vGroup & "%" 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCGroup = "%" & vGroup 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCGroup = "%" & vGroup & "%" 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vGroup.ToCharArray 
          pWCGroup &= p & "%" 
        Next 
        pWCGroup = "%" & pWCGroup 
      End If 
    End If 
    'SettingName 
    Dim pWCSettingName As String = "" 
    If vSettingName = Nothing Then 
      pWCSettingName = vSettingName
    Else 
      If vSettingNameWildcardType = clsEnums.enmWildCardType.None OrElse vSettingNameWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCSettingName = vSettingName
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
        pWCSettingName = vSettingName & "%" 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCSettingName = "%" & vSettingName 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCSettingName = "%" & vSettingName & "%" 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vSettingName.ToCharArray 
          pWCSettingName &= p & "%" 
        Next 
        pWCSettingName = "%" & pWCSettingName 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-SystemDefault-121122-2008", vRequester) 
      Dim pSystemDefaultsCached As csSystemDefaultCol = MyController.DBCache.ccSystemDefaultCol.Clone() 
      Dim pSystemDefaultsToUse As New csSystemDefaultCol() 
      For Each l In pSystemDefaultsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vGroup) Then 
          If vGroupWildcardType = clsEnums.enmWildCardType.UD OrElse vGroupWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Group.Equals(vGroup, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vGroupWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Group.StartsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Group.EndsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Group.IndexOf(vGroup, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vSettingName) Then 
          If vSettingNameWildcardType = clsEnums.enmWildCardType.UD OrElse vSettingNameWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.SettingName.Equals(vSettingName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.SettingName.StartsWith(vSettingName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.SettingName.EndsWith(vSettingName, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.SettingName.IndexOf(vSettingName, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        pSystemDefaultsToUse.Add(l) 
      Next 
      pSystemDefaultsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pSystemDefaultsToUse.Reverse() 
      If vHowMany > 0 AndAlso pSystemDefaultsToUse.Count > vHowMany Then 
        Dim tmp As New csSystemDefaultCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pSystemDefaultsToUse(i)) 
        Next 
        pSystemDefaultsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pSystemDefaultsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "Group" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCGroup) 
        pLastReadVariableName = "SettingName" 
        pDALParameters.Add("wldSettingName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCSettingName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByGroup
    GroupBySettingName
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pGroup As String = Nothing
    Dim pGroupWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pSettingName As String = Nothing
    Dim pSettingNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByGroup As Boolean = False
    Dim pGroupBySettingName As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Group) Then pObj = vParameters(enmFillOnTheFlyParameters.Group) : If pObj IsNot Nothing Then pGroup = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.GroupWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.GroupWildcardType) : If pObj IsNot Nothing Then pGroupWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SettingName) Then pObj = vParameters(enmFillOnTheFlyParameters.SettingName) : If pObj IsNot Nothing Then pSettingName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.SettingNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.SettingNameWildcardType) : If pObj IsNot Nothing Then pSettingNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByGroup) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByGroup) : If pObj IsNot Nothing Then pGroupByGroup = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupBySettingName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupBySettingName) : If pObj IsNot Nothing Then pGroupBySettingName = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pGroup, pGroupWildcardType _
        , pSettingName, pSettingNameWildcardType _
        , pGroupByGroup _
        , pGroupBySettingName _
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
        , ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType _
        , ByVal vSettingName As String, ByVal vSettingNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByGroup As Boolean _
        , ByVal vGroupBySettingName As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, Group={2}, GroupWildcardType={3}, SettingName={4}, SettingNameWildcardType={5}, GroupByGroup={6}, GroupBySettingName={7}", vIDFrom, vIDTo, vGroup, vGroupWildcardType.FastToString(), vSettingName, vSettingNameWildcardType.FastToString(), vGroupByGroup, vGroupBySettingName)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroup = Nothing Then 
      pWCGroup = vGroup
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.None OrElse vGroupWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCGroup = vGroup
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.After Then 
      pWCGroup = vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCGroup = "%" & vGroup 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCGroup = "%" & vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vGroup.ToCharArray 
        pWCGroup &= p & "%" 
      Next 
      pWCGroup = "%" & pWCGroup 
    End If 
    'SettingName 
    Dim pWCSettingName As String = "" 
    If vSettingName = Nothing Then 
      pWCSettingName = vSettingName
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.None OrElse vSettingNameWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCSettingName = vSettingName
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSettingName = vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSettingName = "%" & vSettingName 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSettingName = "%" & vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSettingName.ToCharArray 
        pWCSettingName &= p & "%" 
      Next 
      pWCSettingName = "%" & pWCSettingName 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-SystemDefault-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_SystemDefaultsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "Group" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCGroup) 
        pLastReadVariableName = "SettingName" 
        pDALParameters.Add("wldSettingName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCSettingName) 
        pLastReadVariableName = "Group" 
        pDALParameters.Add("GroupByGroup", ccDAL.enmSQLDataType.Bit).Value = vGroupByGroup
        pLastReadVariableName = "SettingName" 
        pDALParameters.Add("GroupBySettingName", ccDAL.enmSQLDataType.Bit).Value = vGroupBySettingName
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vSystemDefaultArray As csSystemDefault())
    Me.Clear()
    
    For Each pSystemDefault As csSystemDefault In vSystemDefaultArray
      Me.Add(pSystemDefault)
      _Clean.Add(pSystemDefault.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pSystemDefault As New csSystemDefault(pRow, vRequester) 
        Me.Add(pSystemDefault) 
        _Clean.Add(pSystemDefault.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-SystemDefaultCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-130515-1300", vRequester) 
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
      Dim pSystemDefaults As csSystemDefaultCol = CType(pXmlSerializer.Deserialize(pStreamReader), csSystemDefaultCol) 
      For Each pSystemDefault As csSystemDefault In pSystemDefaults 
        Me.Add(pSystemDefault) 
        _Clean.Add(pSystemDefault.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-SystemDefault-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-190720-1443", vRequester) 
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
 
      Dim pSystemDefaults As List(Of csSystemDefault) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csSystemDefault))(vJSON, pSettings) 
      For Each pSystemDefault As csSystemDefault In pSystemDefaults 
        Me.Add(pSystemDefault) 
        _Clean.Add(pSystemDefault.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-190720-2059", vRequester) 
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
          For Each lSystemDefault As csSystemDefault In Me 
            Dim pByte As Byte() = lSystemDefault.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-150307-2340", vRequester) 
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
            Dim pSystemDefault As csSystemDefault = New csSystemDefault(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pSystemDefault) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pSystemDefault.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-SystemDefault-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pSystemDefault As csSystemDefault In Me 
      With pSystemDefault 
        pFault = pSystemDefault.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csSystemDefaultCol) Then Return False 
    Dim pSystemDefaultColToTest As csSystemDefaultCol = CType(vEntitiesToTest, csSystemDefaultCol) 
    Return isEqual(pSystemDefaultColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vSystemDefaultsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vSystemDefaultsToTest As csSystemDefaultCol) As Boolean
    If Me.Count <> vSystemDefaultsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vSystemDefaultsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSystemDefaults As New csSystemDefaultCol() 
    If pFilledFromSumOnTheFly Then pSystemDefaults._FilledFromSumOnTheFly = True
    
    For Each pSystemDefault As csSystemDefault In Me 
      Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
      pSystemDefaults.Add(pSystemDefaultClone) 
      If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
    Next 
    Return pSystemDefaults 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csSystemDefaultCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pSystemDefaults As New csSystemDefaultCol() 
    If pFilledFromSumOnTheFly Then pSystemDefaults._FilledFromSumOnTheFly = True
    
    For Each pSystemDefault As csSystemDefault In Me
      Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
      pSystemDefaults.Add(pSystemDefaultClone)
      If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
    Next
    Return pSystemDefaults
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csSystemDefaultCol 
    Dim pSystemDefaults As New csSystemDefaultCol()  
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemDefault As csSystemDefault In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSystemDefault.ID > vIDFrom AndAlso pSystemDefault.ID <= vIDTo) Then 
        Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
        pSystemDefaults.Add(pSystemDefaultClone) 
        If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
      End If 
    Next 
    Return pSystemDefaults 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Group and SettingName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedGroupAndSettingName(ByVal vGroupFrom As String, ByVal vGroupTo As String, ByVal vSettingNameFrom As String, ByVal vSettingNameTo As String) As csSystemDefaultCol 
    Dim pSystemDefaults As New csSystemDefaultCol()  
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemDefault As csSystemDefault In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSystemDefault.Group > vGroupFrom AndAlso pSystemDefault.Group <= vGroupTo) AndAlso (pSystemDefault.SettingName > vSettingNameFrom AndAlso pSystemDefault.SettingName <= vSettingNameTo) Then 
        Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
        pSystemDefaults.Add(pSystemDefaultClone) 
        If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
      End If 
    Next 
    Return pSystemDefaults 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Group (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedGroup(ByVal vGroupFrom As String, ByVal vGroupTo As String) As csSystemDefaultCol 
    Dim pSystemDefaults As New csSystemDefaultCol()  
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemDefault As csSystemDefault In _SortedDictionaryForFindByID.Values.ToList() 
      If (pSystemDefault.Group > vGroupFrom AndAlso pSystemDefault.Group <= vGroupTo) Then 
        Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
        pSystemDefaults.Add(pSystemDefaultClone) 
        If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
      End If 
    Next 
    Return pSystemDefaults 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardGroupAndSettingName(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType, ByVal vSettingName As String, ByVal vSettingNameWildcardType As clsEnums.enmWildCardType) As csSystemDefaultCol 
    Dim pSystemDefaults As New csSystemDefaultCol 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemDefault As csSystemDefault In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
        If pSystemDefault.Group.StartsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
        If pSystemDefault.Group.EndsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pSystemDefault.Group.IndexOf(vGroup, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vGroup.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pSystemDefault.Group.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pSystemDefault.SettingName.StartsWith(vSettingName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pSystemDefault.SettingName.EndsWith(vSettingName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pSystemDefault.SettingName.IndexOf(vSettingName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vSettingName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pSystemDefault.SettingName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
      pSystemDefaults.Add(pSystemDefaultClone) 
    Next 
    Return pSystemDefaults 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardGroup(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType) As csSystemDefaultCol 
    Dim pSystemDefaults As New csSystemDefaultCol 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pSystemDefault As csSystemDefault In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
        If pSystemDefault.Group.StartsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
        If pSystemDefault.Group.EndsWith(vGroup, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pSystemDefault.Group.IndexOf(vGroup, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vGroup.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pSystemDefault.Group.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone() 
      pSystemDefaults.Add(pSystemDefaultClone) 
    Next 
    Return pSystemDefaults 
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
  Public Function FindByID(ByVal vID As Long) As csSystemDefault
    If Me.Count = 0 Then Return New csSystemDefault 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
    
    Dim pSystemDefault As csSystemDefault = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pSystemDefault) 
    If pSystemDefault IsNot Nothing Then Return pSystemDefault Else Return New csSystemDefault() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByGroupAndSettingName(ByVal vGroup As String, ByVal vSettingName As String) As csSystemDefault
    If Me.Count = 0 Then Return New csSystemDefault 
    
    If _RecreateDictionaryForFindByGroupAndSettingName = True Then LoadGroupAndSettingNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csSystemDefault) = _SortedDictionaryForFindByGroupAndSettingName 
    
    Dim pSystemDefault As csSystemDefault = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vGroup & "|" & vSettingName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pSystemDefault) 
    If pSystemDefault IsNot Nothing Then Return pSystemDefault Else Return New csSystemDefault() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Group
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByGroup(ByVal vGroup As String) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vGroup = vGroup.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.Group.ToLowerInvariant() = vGroup Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByGroup with vGroup of {vGroup}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.Group.ToLowerInvariant() = vGroup Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SettingName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySettingName(ByVal vSettingName As String) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSettingName = vSettingName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.SettingName.ToLowerInvariant() = vSettingName Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySettingName with vSettingName of {vSettingName}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.SettingName.ToLowerInvariant() = vSettingName Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SettingValue
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySettingValue(ByVal vSettingValue As String) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSettingValue = vSettingValue.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.SettingValue.ToLowerInvariant() = vSettingValue Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySettingValue with vSettingValue of {vSettingValue}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.SettingValue.ToLowerInvariant() = vSettingValue Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SystemDefaultType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySystemDefaultType(ByVal vSystemDefaultType As clsEnums.enmSystemDefaultType) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.SystemDefaultType = vSystemDefaultType Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySystemDefaultType with vSystemDefaultType of {vSystemDefaultType}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.SystemDefaultType = vSystemDefaultType Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDescription(ByVal vDescription As String) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDescription = vDescription.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.Description.ToLowerInvariant() = vDescription Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDescription with vDescription of {vDescription}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.Description.ToLowerInvariant() = vDescription Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csSystemDefaultCol
    Dim pSystemDefaults As New csSystemDefaultCol() 
    pSystemDefaults._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csSystemDefault) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pSystemDefault As csSystemDefault In pTempDist.Values
        If pSystemDefault.Tag.ToLowerInvariant() = vTag Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csSystemDefaultCol = Me.Clone() 
      For Each pSystemDefault As csSystemDefault In pList 
        If pSystemDefault.Tag.ToLowerInvariant() = vTag Then
          Dim pSystemDefaultClone As csSystemDefault = pSystemDefault.Clone()
          pSystemDefaults.Add(pSystemDefaultClone)
          If Not _FilledFromSumOnTheFly Then pSystemDefaults._Clean.Add(pSystemDefault.ID) 
        End If
      Next
    End If 
    
    Return pSystemDefaults
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
    For Each pSystemDefault As csSystemDefault In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pSystemDefault.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultView, "csSystemDefaultCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csSystemDefault In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csSystemDefault = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pSystemDefaultToKill As New csSystemDefault 
          pSystemDefaultToKill.ID = pCleanID 
          pSystemDefaultToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pSystemDefaultToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csSystemDefault In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-SystemDefault-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultUpdate, "csSystemDefaultCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csSystemDefault In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csSystemDefault In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csSystemDefaultCol(), vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Group 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByGroup(ByVal vGroup As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}", vGroup)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultsDeleteByGroup"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllSystemDefaults As New csSystemDefaultCol() : pAllSystemDefaults.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredSystemDefaults As csSystemDefaultCol = pAllSystemDefaults.CloneByGroup(vGroup) 
      For Each l In pFilteredSystemDefaults 
        pAllSystemDefaults.Remove(pAllSystemDefaults.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllSystemDefaults, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "Group" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroup) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemDefault-150216-2148", vRequester) 
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
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific GroupAndSettingName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedGroupAndSettingName(ByVal vGroupFrom As String, ByVal vGroupTo As String, ByVal vSettingNameFrom As String, ByVal vSettingNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("GroupFrom={0}, GroupTo={1}, SettingNameFrom={2}, SettingNameTo={3}", vGroupFrom, vGroupTo, vSettingNameFrom, vSettingNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByBoundedGroupAndSettingName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultsDeleteByBoundedGroup&SettingName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemDefault-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "GroupFrom" 
        pDALParameters.Add("bndGroupFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupFrom) 
        pLastReadVariableName = "GroupTo" 
        pDALParameters.Add("bndGroupTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupTo) 
        pLastReadVariableName = "SettingNameFrom" 
        pDALParameters.Add("bndSettingNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSettingNameFrom) 
        pLastReadVariableName = "SettingNameTo" 
        pDALParameters.Add("bndSettingNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vSettingNameTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Group
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedGroup(ByVal vGroupFrom As String, ByVal vGroupTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("GroupFrom={0}, GroupTo={1}", vGroupFrom, vGroupTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByBoundedGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_SystemDefaultsDeleteByBoundedGroup"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemDefault-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "GroupFrom" 
        pDALParameters.Add("bndGroupFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupFrom) 
        pLastReadVariableName = "GroupTo" 
        pDALParameters.Add("bndGroupTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vGroupTo) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded GroupAndSettingName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardGroupAndSettingName(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType, ByVal vSettingName As String, ByVal vSettingNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}, GroupWildcardType={1}, SettingName={2}, SettingNameWildcardType={3}", vGroup, vGroupWildcardType.FastToString(), vSettingName, vSettingNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByWildCardGroupAndSettingName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
      pWCGroup = vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCGroup = "%" & vGroup 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCGroup = "%" & vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vGroup.ToCharArray 
        pWCGroup &= p & "%" 
      Next 
      pWCGroup = "%" & pWCGroup 
    End If 
    'SettingName 
    Dim pWCSettingName As String = "" 
    If vSettingNameWildcardType = clsEnums.enmWildCardType.After Then 
      pWCSettingName = vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCSettingName = "%" & vSettingName 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCSettingName = "%" & vSettingName & "%" 
    ElseIf vSettingNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vSettingName.ToCharArray 
        pWCSettingName &= p & "%" 
      Next 
      pWCSettingName = "%" & pWCSettingName 
    End If 
    
    Dim pCommandText As String = "c_SystemDefaultsDeleteByWildCardGroup&SettingName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemDefault-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldGroup" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCGroup) 
        pLastReadVariableName = "wldSettingName" 
        pDALParameters.Add("wldSettingName", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCSettingName) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090219-1632", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded Group
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardGroup(ByVal vGroup As String, ByVal vGroupWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Group={0}, GroupWildcardType={1}", vGroup, vGroupWildcardType.FastToString())
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_SystemDefaultDelete, "csSystemDefaultCol_DeleteByWildCardGroup", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Group 
    Dim pWCGroup As String = "" 
    If vGroupWildcardType = clsEnums.enmWildCardType.After Then 
      pWCGroup = vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCGroup = "%" & vGroup 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCGroup = "%" & vGroup & "%" 
    ElseIf vGroupWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vGroup.ToCharArray 
        pWCGroup &= p & "%" 
      Next 
      pWCGroup = "%" & pWCGroup 
    End If 
    
    Dim pCommandText As String = "c_SystemDefaultsDeleteByWildCardGroup"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-SystemDefault-150216-2154", vRequester) 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "wldGroup" 
        pDALParameters.Add("wldGroup", ccDAL.enmSQLDataType.VarChar, 50).Value = (pWCGroup) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
        pLastReadVariableName = "UpdatingLoginID" 
        pDALParameters.Add("UpdatingLoginID", ccDAL.enmSQLDataType.BigInt).Value = vRequester.LoggedLoginID
        pLastReadVariableName = "" 
   
        'Execute query  
        Dim pTargCCReader As ccDAL.csTargCCReader = Nothing 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, pTargCCReader) 
   
        'I expect to get -1 back  
        If pFault.isOK = True Then 
          If pTargCCReader.HasRows = True Then 
            pTargCCReader.Read() 
            Dim pID As Integer = ccHelper.ToInteger(pTargCCReader(0)) 
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-SystemDefault-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-SystemDefault-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-090219-1632", vRequester) 
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
    Me.Sort(New csSystemDefaultCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
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
  
  Public Sub SortByGroup()
    Me.Sort(New csSystemDefaultCol.CompareByGroup)
  End Sub
  Private Class CompareByGroup
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Group, y.Group, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySettingName()
    Me.Sort(New csSystemDefaultCol.CompareBySettingName)
  End Sub
  Private Class CompareBySettingName
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SettingName, y.SettingName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySettingValue()
    Me.Sort(New csSystemDefaultCol.CompareBySettingValue)
  End Sub
  Private Class CompareBySettingValue
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SettingValue, y.SettingValue, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySystemDefaultType()
    Me.Sort(New csSystemDefaultCol.CompareBySystemDefaultType)
  End Sub
  Private Class CompareBySystemDefaultType
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.SystemDefaultType < y.SystemDefaultType Then
        Return -1
      ElseIf x.SystemDefaultType = y.SystemDefaultType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortBySystemDefaultTypeText()
    Me.Sort(New csSystemDefaultCol.CompareBySystemDefaultTypeText)
  End Sub
  Private Class CompareBySystemDefaultTypeText
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SystemDefaultTypeText, y.SystemDefaultTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDescription()
    Me.Sort(New csSystemDefaultCol.CompareByDescription)
  End Sub
  Private Class CompareByDescription
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Description, y.Description, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csSystemDefaultCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csSystemDefault)
    Private Function Compare(ByVal x As csSystemDefault, ByVal y As csSystemDefault) As Integer Implements System.Collections.Generic.IComparer(Of csSystemDefault).Compare
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
  
    Dim pSystemDefault As csSystemDefault
  
    While vReader.Read()
      pSystemDefault = New csSystemDefault() 
      pFault = pSystemDefault.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pSystemDefault)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pSystemDefault.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedSystemDefaultCol As csSystemDefaultCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pSystemDefault As csSystemDefault 
 
      For Each pCachedSystemDefault As csSystemDefault In vCachedSystemDefaultCol 
        pSystemDefault = New csSystemDefault(pCachedSystemDefault) 
        pSystemDefault.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pSystemDefault) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pSystemDefault.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-SystemDefault-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  'Additional functions for csSystemDefault 
  Public Function FindByFullSettingName(ByVal vFullSettingName As csSystemDefault.enmFullSettingName) As csSystemDefault 
 
    'Check that we got a valid FullSetting  
    Dim pFullSetting As String = vFullSettingName.ToString() 
    Dim pFullSettingArray As String() = pFullSetting.Split("_"c) 
    If pFullSettingArray.Length <> 2 Then Throw New Exception("Neither Group nor SettingName can have an underscore") 
 
    Dim pGroup As String = pFullSettingArray(0) 
    If pGroup = "" Then Throw New Exception("Group cannot be blank") 
    Dim pSettingName As String = pFullSettingArray(1) 
    If pSettingName = "" Then Throw New Exception("SettingName cannot be blank") 
 
    Return FindByGroupAndSettingName(pGroup, pSettingName) 
 
  End Function 
  
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csSystemDefault) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByGroupAndSettingName = New Dictionary(Of String, csSystemDefault)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByGroupAndSettingName = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csSystemDefault) 
    _SortedDictionaryForFindByGroupAndSettingName = New Dictionary(Of String, csSystemDefault)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
