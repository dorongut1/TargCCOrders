Public Class csUserStatus
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
    [User] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [User] 
    [ApplicationName] 
    [LastLoggedLoginID] 
    [LoginTime] 
    [LogoutTime] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [LastLoggedLoginID] 
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
  
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _UserID As Long
  Private _User As csUser
  Private _UserText As String
  Private _ApplicationName As String
  Private _LastLoggedLoginID As Long
  Private _LoginTime As Date
  Private _LogoutTime As Date
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
  Public Property [UserID]() As Long
    Get
      Return Me._UserID
    End Get
    Set(ByVal value As Long)
      If Me._UserID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserID = value 
      End If 
    End Set
  End Property
  Public Property [User]() As csUser
    Get
      Return Me._User
    End Get
    Set(ByVal value As csUser)
      Me._User = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the User object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property UserText() As String
    Get
      Return Me._UserText
    End Get
    Set(ByVal value As String)
      Me._UserText = value
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
  Public Property [LastLoggedLoginID]() As Long
    Get
      Return Me._LastLoggedLoginID
    End Get
    Set(ByVal value As Long)
      If Me._LastLoggedLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastLoggedLoginID = value 
      End If 
    End Set
  End Property
  Public Property [LoginTime]() As Date
    Get
      Return Me._LoginTime
    End Get
    Set(ByVal value As Date)
      If Me._LoginTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoginTime = value 
      End If 
    End Set
  End Property
  Public Property [LogoutTime]() As Date
    Get
      Return Me._LogoutTime
    End Get
    Set(ByVal value As Date)
      If Me._LogoutTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LogoutTime = value 
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
    If _UserID <> 0 Then pValue.Append("UserID='" & _UserID.ToString() & "' ‡ ") 
    If _UserText <> "" Then pValue.Append("UserText='" & _UserText & "' ‡ ") 
    If _ApplicationName <> "" Then pValue.Append("ApplicationName='" & _ApplicationName & "' ‡ ") 
    If _LastLoggedLoginID <> 0 Then pValue.Append("LastLoggedLoginID='" & _LastLoggedLoginID.ToString() & "' ‡ ") 
    If Not (_LoginTime = Nothing) Then pValue.Append("LoginTime='" & _LoginTime.ToString("o") & "' ‡ ") 
    If Not (_LogoutTime = Nothing) Then pValue.Append("LogoutTime='" & _LogoutTime.ToString("o") & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _UserID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ApplicationName)}""") 
    pCSV.Append("," & _LastLoggedLoginID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LoginTime.ToShortDateString & " " & _LoginTime.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LogoutTime.ToShortDateString & " " & _LogoutTime.ToShortTimeString)}""") 
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
  
  Public Sub New(ByVal vcsUserStatus As csUserStatus)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsUserStatus) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vUserID As Long = 0 _ 
    , Optional vUserText As String = "" _ 
    , Optional vApplicationName As String = "" _ 
    , Optional vLastLoggedLoginID As Long = 0 _ 
    , Optional vLoginTime As Date = Nothing _ 
    , Optional vLogoutTime As Date = Nothing _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _UserID = vUserID 
    _UserText = vUserText 
    _ApplicationName = vApplicationName 
    _LastLoggedLoginID = vLastLoggedLoginID 
    _LoginTime = vLoginTime 
    _LogoutTime = vLogoutTime 
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
 
    _ApplicationName = _ApplicationName.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ApplicationName = ccHelper.RemoveChrW0(_ApplicationName) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the UserStatus by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatus_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserStatus-151224_0844", vRequester) 
    End Try 
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "csUserStatus_GetByPrimaryKey", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [LastLoggedLoginID] 
    [UserIDAndApplicationName] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the UserStatus by the chosen parameters. This function may be a bit slower than accessing the UserStatus's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatus_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.LastLoggedLoginID 
          pFault = GetByLastLoggedLoginID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.UserIDAndApplicationName 
          pFault = GetByUserIDAndApplicationName(ccHelper.ToLong(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-UserStatus-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserStatus-151223_1716", vRequester)  
    End Try  
 
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "csUserStatus_GetByParameters", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the UserStatus by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatus_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"UserStatus not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-UserStatus-210927-1527", vRequester, vAdditionalMessageToUser:=$"UserStatus not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccUserStatusCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatusGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"UserStatus not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-UserStatus-210625-0950", vRequester, vAdditionalMessageToUser:=$"UserStatus not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "csUserStatus_GetByID", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the UserStatus by LastLoggedLoginID.
  ''' </summary>
  ''' <param name="vLastLoggedLoginID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByLastLoggedLoginID(ByVal vLastLoggedLoginID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastLoggedLoginID={0}", vLastLoggedLoginID)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatus_GetByLastLoggedLoginID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccUserStatusCol.FindByLastLoggedLoginID(vLastLoggedLoginID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatusGetByLastLoggedLoginID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LastLoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vLastLoggedLoginID) 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"UserStatus not found for GetByLastLoggedLoginID. See FunctionParameters for values", pFunctionParameters, "TRGT-UserStatus-210625-0950", vRequester, vAdditionalMessageToUser:=$"UserStatus not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "csUserStatus_GetByLastLoggedLoginID", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the UserStatus by UserIDAndApplicationName.
  ''' </summary>
  ''' <param name="vUserID"></param>
  ''' <param name="vApplicationName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByUserIDAndApplicationName(ByVal vUserID As Long, ByVal vApplicationName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}, ApplicationName={1}", vUserID, vApplicationName)
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatus_GetByUserIDAndApplicationName", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccUserStatusCol.FindByUserIDAndApplicationName(vUserID, vApplicationName), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatusGetByUserID&ApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vUserID, False) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationName) 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"UserStatus not found for GetByUserIDAndApplicationName. See FunctionParameters for values", pFunctionParameters, "TRGT-UserStatus-210625-0950", vRequester, vAdditionalMessageToUser:=$"UserStatus not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    'only check if this function was accessed directly from outside DBController 
    If pEnteredHere = True Then pFault = ccSecurity.GetPermissionForExternalIndentityTypeForEntity(Me, "csUserStatus_GetByUserIDAndApplicationName", vRequester) : If Not pFault.isOK Then CreateEmpty() : Return pFault 
    
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusUpdate, "csUserStatus_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-UserStatus-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusUpdate, "csUserStatus_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-UserStatus-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the UserStatus. If there are parents or children in the UserStatus, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusUpdate, "csUserStatus_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pUserStatus As New csUserStatus(_WithParents) 
    If Me.isEqual(pUserStatus) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-UserStatus-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-UserStatus-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_UserStatusUpdate"
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
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pCachedUserStatus As csUserStatus 
      If _ID = 0 Then 
        pCachedUserStatus = New csUserStatus(_WithParents) 
        'get last ID 
        Dim pUserStatusCol As csUserStatusCol = MyController.DBCache.ccUserStatusCol.Clone() 
        If pUserStatusCol.Count = 0 Then 
          _ID = 1 
        Else 
          pUserStatusCol.SortByID() 
          Dim pLastID As Long = pUserStatusCol(pUserStatusCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccUserStatusCol.Add(pCachedUserStatus) 
      Else  
        pCachedUserStatus = MyController.DBCache.ccUserStatusCol.FindByID(_ID) 
      End If 
      pCachedUserStatus.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccUserStatusCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_UserID, False) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_ApplicationName) 
        pLastReadVariableName = "LastLoggedLoginID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_LastLoggedLoginID) 
        pLastReadVariableName = "LoginTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_LoginTime) 
        pLastReadVariableName = "LogoutTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_LogoutTime) 
        pLastReadVariableName = "ChangedBy" 
        pDALParameters.Add("ChangedBy", ccDAL.enmSQLDataType.NVarChar, 50).Value = vRequester.UserName
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pUser As csUser = _User 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pUser Is Nothing Then _User = pUser 
      End If 
      
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
    Dim pFunctionParameters As String = String.Format("UserStatus.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatus_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_UserStatusDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      MyController.DBCache.ccUserStatusCol.Remove(MyController.DBCache.ccUserStatusCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccUserStatusCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatus_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatusDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      MyController.DBCache.ccUserStatusCol.Remove(MyController.DBCache.ccUserStatusCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccUserStatusCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csUserStatus) Then Return False 
    Dim pUserStatusToTest As csUserStatus = CType(vTargCCEntityToTest, csUserStatus) 
    Return isEqual(pUserStatusToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vUserStatusToTest As csUserStatus) As Boolean
    With vUserStatusToTest
      If _ID <> .ID Then Return False
      If _UserID <> .UserID Then Return False
      If _ApplicationName <> .ApplicationName Then Return False
      If _LastLoggedLoginID <> .LastLoggedLoginID Then Return False
      If _LoginTime <> Nothing AndAlso .LoginTime <> Nothing Then 
        If ccHelper.ToLong(_LoginTime.Subtract(.LoginTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LoginTime = Nothing AndAlso .LoginTime = Nothing) Then 
        Return False 
      End If 
      If _LogoutTime <> Nothing AndAlso .LogoutTime <> Nothing Then 
        If ccHelper.ToLong(_LogoutTime.Subtract(.LogoutTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LogoutTime = Nothing AndAlso .LogoutTime = Nothing) Then 
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
    Dim pClone As New csUserStatus(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUserStatus
    Dim pClone As New csUserStatus(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserID") = _UserID : Catch ex As Exception : Return pFault.LogException(ex, "UserID", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApplicationName") = _ApplicationName : Catch ex As Exception : Return pFault.LogException(ex, "ApplicationName", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastLoggedLoginID") = _LastLoggedLoginID : Catch ex As Exception : Return pFault.LogException(ex, "LastLoggedLoginID", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoginTime") = _LoginTime : Catch ex As Exception : Return pFault.LogException(ex, "LoginTime", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
    Try : vDataRow("LogoutTime") = _LogoutTime : Catch ex As Exception : Return pFault.LogException(ex, "LogoutTime", "TRGT-UserStatus-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pUserStatus As csUserStatus = CType(pXmlSerializer.Deserialize(pStreamReader), csUserStatus) 
      AssignValues(pUserStatus) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-UserStatus-130515-1230", vRequester) 
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
          'UserID 
          pBinaryWriter.Write(_UserID) 
          'User 
          If _User IsNot Nothing Then 
            pObjectBytes = _User.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _UserText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserText) 
          'ApplicationName 
          If _ApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApplicationName) 
          'LastLoggedLoginID 
          pBinaryWriter.Write(_LastLoggedLoginID) 
          'LoginTime 
          pBinaryWriter.Write(_LoginTime.Ticks) 
          'LogoutTime 
          pBinaryWriter.Write(_LogoutTime.Ticks) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-150307-2338", vRequester) 
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
          'UserID 
          _UserID = pReader.ReadInt64 
          'User 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _User = New csUser(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserText = pReader.ReadString 
          'ApplicationName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApplicationName = pReader.ReadString 
          'LastLoggedLoginID 
          _LastLoggedLoginID = pReader.ReadInt64 
          'LoginTime 
          _LoginTime = New Date(pReader.ReadInt64) 
          'LogoutTime 
          _LogoutTime = New Date(pReader.ReadInt64) 
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
      rFault.LogException(ex, "", "TRGT-UserStatus-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-190720-1443", vRequester) 
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
 
      Dim pUserStatus As csUserStatus = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csUserStatus)(vJSON, pSettings) 
      AssignValues(pUserStatus) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vUserStatus As csUserStatus)
    With vUserStatus
      _ID = .ID 
      _UserID = .UserID 
      If .User IsNot Nothing Then 
        _User = .User.Clone() 
      End If 
      _UserText = .UserText 
      _ApplicationName = .ApplicationName 
      _LastLoggedLoginID = .LastLoggedLoginID 
      _LoginTime = .LoginTime 
      _LogoutTime = .LogoutTime 
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
 
    'There are no enums or lookups. This function was added to this object for interface compatibility 
    Return pFault.SetOK() 
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
    If _UserID > 0 Then
      _User = New csUser()
      pFault = _User.GetByID(_UserID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _UserText = _User.DefaultDesignation 
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
      pLastReadVariableName = "c_UserID" 
      If Not vReader.IsDBNull(1) Then _UserID = vReader.GetInt64(1)
      pLastReadVariableName = "ApplicationName" 
      If Not vReader.IsDBNull(2) Then _ApplicationName = vReader.GetString(2) 
      pLastReadVariableName = "LastLoggedLoginID" 
      If Not vReader.IsDBNull(3) Then _LastLoggedLoginID = vReader.GetInt64(3)
      pLastReadVariableName = "LoginTime" 
      If Not vReader.IsDBNull(4) Then _LoginTime = vReader.GetDateTime(4)
      pLastReadVariableName = "LogoutTime" 
      If Not vReader.IsDBNull(5) Then _LogoutTime = vReader.GetDateTime(5)
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(6) Then bDateAdded = vReader.GetDateTime(6)   
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "UserText" 
        If Not vReader.IsDBNull(7) Then _UserText = vReader.GetString(7) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedUserStatus As csUserStatus, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedUserStatus) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _UserText = "."
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _UserID = 0
    _User = Nothing
    _UserText = "."
    _ApplicationName = ""
    _LastLoggedLoginID = 0
    _LoginTime = Nothing
    _LogoutTime = Nothing
    _Tag = ""
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
  
End Class 
  
Public Class csUserStatusCol
  Inherits cTargCCCollection(Of csUserStatus)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csUserStatus) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByLastLoggedLoginID As Dictionary(Of String, csUserStatus) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByLastLoggedLoginID As Boolean 
  Private Function CreateKeyForFindByLastLoggedLoginID(ByVal vUserStatus As csUserStatus) As String 
    With vUserStatus 
      Return .LastLoggedLoginID.ToString()
    End With 
  End Function 
  Private _SortedDictionaryForFindByUserIDAndApplicationName As Dictionary(Of String, csUserStatus) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByUserIDAndApplicationName As Boolean 
  Private Function CreateKeyForFindByUserIDAndApplicationName(ByVal vUserStatus As csUserStatus) As String 
    With vUserStatus 
      Return .UserID.ToString() & "|" & .ApplicationName
    End With 
  End Function 
   
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
 
    For Each pRow As csUserStatus In Me 
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
    pCSVTitle.Append(",""UserID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""User (Text)""") 
    pCSVTitle.Append(",""ApplicationName""") 
    pCSVTitle.Append(",""LastLoggedLoginID""") 
    pCSVTitle.Append(",""LoginTime""") 
    pCSVTitle.Append(",""LogoutTime""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csUserStatus In Me 
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
 
  Public Overloads Sub Add(ByVal vUserStatus As csUserStatus) 
    SyncLock _CollectionLock 
      MyBase.Add(vUserStatus) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByLastLoggedLoginID = True 
      _RecreateDictionaryForFindByUserIDAndApplicationName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vUserStatus As csUserStatus) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vUserStatus) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByLastLoggedLoginID = True 
      _RecreateDictionaryForFindByUserIDAndApplicationName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vUserStatusCol As csUserStatusCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vUserStatusCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByLastLoggedLoginID = True 
      _RecreateDictionaryForFindByUserIDAndApplicationName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByLastLoggedLoginID = True 
      _RecreateDictionaryForFindByUserIDAndApplicationName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vUserStatus As csUserStatus) 
    SyncLock _CollectionLock 
      MyBase.Remove(vUserStatus) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByLastLoggedLoginID = True 
      _RecreateDictionaryForFindByUserIDAndApplicationName = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csUserStatus) 
      
      For Each lUserStatus In Me 
        If lUserStatus.IsEmpty OrElse pTempDictionary.ContainsKey(lUserStatus.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lUserStatus.ID, lUserStatus) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUserStatus.ToString, "TRGT-UserStatus-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", UserStatus:" & lUserStatus.ToString() & ", TRGT-UserStatus-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadLastLoggedLoginIDs() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByLastLoggedLoginID Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByLastLoggedLoginID Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByLastLoggedLoginID = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByLastLoggedLoginID' yet!
      Dim pTempDictionary As New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lUserStatus In Me 
        Try 
          Dim pLastLoggedLoginID As String = CreateKeyForFindByLastLoggedLoginID(lUserStatus) 
          If String.IsNullOrEmpty(pLastLoggedLoginID.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pLastLoggedLoginID)) Then 
            pTempDictionary.Add(pLastLoggedLoginID, lUserStatus) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUserStatus.ToString, "TRGT-UserStatus-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByLastLoggedLoginID:" & ex.Message & ", UserStatus:" & lUserStatus.ToString() & ", TRGT-UserStatus-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByLastLoggedLoginID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByLastLoggedLoginID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadUserIDAndApplicationNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByUserIDAndApplicationName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByUserIDAndApplicationName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByUserIDAndApplicationName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByUserIDAndApplicationName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lUserStatus In Me 
        Try 
          Dim pUserIDAndApplicationName As String = CreateKeyForFindByUserIDAndApplicationName(lUserStatus) 
          If String.IsNullOrEmpty(pUserIDAndApplicationName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pUserIDAndApplicationName)) Then 
            pTempDictionary.Add(pUserIDAndApplicationName, lUserStatus) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUserStatus.ToString, "TRGT-UserStatus-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByUserIDAndApplicationName:" & ex.Message & ", UserStatus:" & lUserStatus.ToString() & ", TRGT-UserStatus-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByUserIDAndApplicationName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByUserIDAndApplicationName = False
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
 
    For Each lUserStatus As csUserStatus In Me 
      lUserStatus.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lUserStatus As csUserStatus In Me 
      lUserStatus.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [LoginTime] 
    [UserID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the UserStatuss by the chosen parameters. This function may be a bit slower than accessing the UserStatus's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.LoginTime 
          pFault = FillByLoginTime(CDate(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.UserID 
          pFault = FillByUserID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-UserStatus-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserStatus-151223_1716", vRequester) 
    End Try 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByParameters", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.Clone() 
      pUserStatussCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pUserStatussCached.Reverse() 
      If vHowMany > 0 AndAlso pUserStatussCached.Count > vHowMany Then 
        Dim tmp As New csUserStatusCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pUserStatussCached(i)) 
        Next 
        pUserStatussCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoginTime, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLoginTime(ByVal vLoginTime As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginTime={0}", vLoginTime)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByLoginTime", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByLoginTime(vLoginTime)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByLoginTime" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LoginTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(vLoginTime) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByLoginTime", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserID(ByVal vUserID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}", vUserID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByUserID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByUserID(vUserID)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByUserID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vUserID, False) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByUserID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByBoundedID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastLoggedLoginID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedLastLoggedLoginID(ByVal vLastLoggedLoginIDFrom As Long, ByVal vLastLoggedLoginIDTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastLoggedLoginIDFrom={0}, LastLoggedLoginIDTo={1}", vLastLoggedLoginIDFrom, vLastLoggedLoginIDTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByBoundedLastLoggedLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByBoundedLastLoggedLoginID(vLastLoggedLoginIDFrom, vLastLoggedLoginIDTo)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByBoundedLastLoggedLoginID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LastLoggedLoginIDFrom" 
        pDALParameters.Add("bndLastLoggedLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vLastLoggedLoginIDFrom) 
        pLastReadVariableName = "LastLoggedLoginIDTo" 
        pDALParameters.Add("bndLastLoggedLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vLastLoggedLoginIDTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByBoundedLastLoggedLoginID", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoginTime, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedLoginTime(ByVal vLoginTimeStart As Date, ByVal vLoginTimeEnd As Date, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginTimeStart={0}, LoginTimeEnd={1}", vLoginTimeStart, vLoginTimeEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByBoundedLoginTime", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByBoundedLoginTime(vLoginTimeStart, vLoginTimeEnd)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByBoundedLoginTime" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "LoginTimeFrom" 
        pDALParameters.Add("bndLoginTimeFrom", ccDAL.enmSQLDataType.DateTime).Value = (vLoginTimeStart) 
        pLastReadVariableName = "LoginTimeTo" 
        pDALParameters.Add("bndLoginTimeTo", ccDAL.enmSQLDataType.DateTime).Value = (vLoginTimeEnd) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByBoundedLoginTime", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserIDAndApplicationName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserIDAndApplicationName(ByVal vUserID As Long, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}, ApplicationNameFrom={1}, ApplicationNameTo={2}", vUserID, vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByBoundedUserIDAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccUserStatusCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccUserStatusCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csUserStatusCol failed: " & pResponse) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.CloneByBoundedUserIDAndApplicationName(vUserID, vApplicationNameFrom, vApplicationNameTo)
      pFault = LoadMeFromDBCache(pUserStatussCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillByBoundedUserID&ApplicationName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vUserID) 
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillByBoundedUserIDAndApplicationName", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lUserStatus As New csUserStatus() 
      pFault = lUserStatus.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lUserStatus.IsEmpty Then Me.Add(lUserStatus) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
    [UserID]
    [ApplicationName]
    ApplicationNameWildcardType
    LastLoggedLoginIDFrom
    LastLoggedLoginIDTo
    LoginTimeStart
    LoginTimeEnd
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLastLoggedLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pLastLoggedLoginIDTo As Nullable(Of Long) = Nothing
    Dim pLoginTimeStart As Nullable(Of Date) = Nothing
    Dim pLoginTimeEnd As Nullable(Of Date) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastLoggedLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.LastLoggedLoginIDFrom) : If pObj IsNot Nothing Then pLastLoggedLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastLoggedLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.LastLoggedLoginIDTo) : If pObj IsNot Nothing Then pLastLoggedLoginIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginTimeStart) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginTimeStart) : If pObj IsNot Nothing Then pLoginTimeStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginTimeEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginTimeEnd) : If pObj IsNot Nothing Then pLoginTimeEnd = CDate(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pUserID _
        , pApplicationName, pApplicationNameWildcardType _
        , pLastLoggedLoginIDFrom, pLastLoggedLoginIDTo _
        , pLoginTimeStart, pLoginTimeEnd _
        , vRequester, pHowMany, pDir) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLastLoggedLoginIDFrom As Nullable(Of Long), ByVal vLastLoggedLoginIDTo As Nullable(Of Long) _
        , ByVal vLoginTimeStart As Nullable(Of Date), ByVal vLoginTimeEnd As Nullable(Of Date) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserID={2}, ApplicationName={3}, ApplicationNameWildcardType={4}, LastLoggedLoginIDFrom={5}, LastLoggedLoginIDTo={6}, LoginTimeStart={7}, LoginTimeEnd={8}", vIDFrom, vIDTo, vUserID, vApplicationName, vApplicationNameWildcardType.FastToString(), vLastLoggedLoginIDFrom, vLastLoggedLoginIDTo, vLoginTimeStart, vLoginTimeEnd)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-UserStatus-121122-2008", vRequester) 
      Dim pUserStatussCached As csUserStatusCol = MyController.DBCache.ccUserStatusCol.Clone() 
      Dim pUserStatussToUse As New csUserStatusCol() 
      For Each l In pUserStatussCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vUserID.HasValue Then 
          If l.UserID <> vUserID.Value Then Continue For 
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
        If vLastLoggedLoginIDFrom.HasValue Then 
          If vLastLoggedLoginIDTo.HasValue Then 
            If l.LastLoggedLoginID < vLastLoggedLoginIDFrom OrElse l.LastLoggedLoginID > vLastLoggedLoginIDTo.Value Then Continue For 
          Else 
            If l.LastLoggedLoginID <> vLastLoggedLoginIDFrom.Value Then Continue For 
          End If 
        End If 
        If vLoginTimeStart.HasValue Then 
          If vLoginTimeEnd.HasValue Then 
            If l.LoginTime < vLoginTimeStart OrElse l.LoginTime > vLoginTimeEnd.Value Then Continue For 
          Else 
            If l.LoginTime <> vLoginTimeStart.Value Then Continue For 
          End If 
        End If 
        pUserStatussToUse.Add(l) 
      Next 
      pUserStatussToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pUserStatussToUse.Reverse() 
      If vHowMany > 0 AndAlso pUserStatussToUse.Count > vHowMany Then 
        Dim tmp As New csUserStatusCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pUserStatussToUse(i)) 
        Next 
        pUserStatussToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pUserStatussToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vUserID) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCApplicationName) 
        pLastReadVariableName = "LastLoggedLoginIDFrom" 
        pDALParameters.Add("bndLastLoggedLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLastLoggedLoginIDFrom) 
        pLastReadVariableName = "LastLoggedLoginIDTo" 
        pDALParameters.Add("bndLastLoggedLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLastLoggedLoginIDTo) 
        pLastReadVariableName = "LoginTimeFrom" 
        pDALParameters.Add("bndLoginTimeFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vLoginTimeStart) 
        pLastReadVariableName = "LoginTimeTo" 
        pDALParameters.Add("bndLoginTimeTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vLoginTimeEnd) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByUserID
    GroupByApplicationName
    GroupByLoginTime
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLastLoggedLoginIDFrom As Nullable(Of Long) = Nothing
    Dim pLastLoggedLoginIDTo As Nullable(Of Long) = Nothing
    Dim pLoginTimeStart As Nullable(Of Date) = Nothing
    Dim pLoginTimeEnd As Nullable(Of Date) = Nothing
    Dim pGroupByUserID As Boolean = False
    Dim pGroupByApplicationName As Boolean = False
    Dim pGroupByLoginTime As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastLoggedLoginIDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.LastLoggedLoginIDFrom) : If pObj IsNot Nothing Then pLastLoggedLoginIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastLoggedLoginIDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.LastLoggedLoginIDTo) : If pObj IsNot Nothing Then pLastLoggedLoginIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginTimeStart) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginTimeStart) : If pObj IsNot Nothing Then pLoginTimeStart = CDate(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoginTimeEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.LoginTimeEnd) : If pObj IsNot Nothing Then pLoginTimeEnd = CDate(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByUserID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByUserID) : If pObj IsNot Nothing Then pGroupByUserID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByApplicationName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByApplicationName) : If pObj IsNot Nothing Then pGroupByApplicationName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLoginTime) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLoginTime) : If pObj IsNot Nothing Then pGroupByLoginTime = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pUserID _
        , pApplicationName, pApplicationNameWildcardType _
        , pLastLoggedLoginIDFrom, pLastLoggedLoginIDTo _
        , pLoginTimeStart, pLoginTimeEnd _
        , pGroupByUserID _
        , pGroupByApplicationName _
        , pGroupByLoginTime _
        , vRequester) : If pFault.isOK = False Then Return pFault 
    
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
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
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLastLoggedLoginIDFrom As Nullable(Of Long), ByVal vLastLoggedLoginIDTo As Nullable(Of Long) _
        , ByVal vLoginTimeStart As Nullable(Of Date), ByVal vLoginTimeEnd As Nullable(Of Date) _
        , ByVal vGroupByUserID As Boolean _
        , ByVal vGroupByApplicationName As Boolean _
        , ByVal vGroupByLoginTime As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserID={2}, ApplicationName={3}, ApplicationNameWildcardType={4}, LastLoggedLoginIDFrom={5}, LastLoggedLoginIDTo={6}, LoginTimeStart={7}, LoginTimeEnd={8}, GroupByUserID={9}, GroupByApplicationName={10}, GroupByLoginTime={11}", vIDFrom, vIDTo, vUserID, vApplicationName, vApplicationNameWildcardType.FastToString(), vLastLoggedLoginIDFrom, vLastLoggedLoginIDTo, vLoginTimeStart, vLoginTimeEnd, vGroupByUserID, vGroupByApplicationName, vGroupByLoginTime)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
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
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-UserStatus-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_UserStatussFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vUserID) 
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("wldApplicationName", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCApplicationName) 
        pLastReadVariableName = "LastLoggedLoginIDFrom" 
        pDALParameters.Add("bndLastLoggedLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLastLoggedLoginIDFrom) 
        pLastReadVariableName = "LastLoggedLoginIDTo" 
        pDALParameters.Add("bndLastLoggedLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLastLoggedLoginIDTo) 
        pLastReadVariableName = "LoginTimeFrom" 
        pDALParameters.Add("bndLoginTimeFrom", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vLoginTimeStart) 
        pLastReadVariableName = "LoginTimeTo" 
        pDALParameters.Add("bndLoginTimeTo", ccDAL.enmSQLDataType.DateTime).Value = ccHelper.ObjectNullable(vLoginTimeEnd) 
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add("GroupByc_UserID", ccDAL.enmSQLDataType.Bit).Value = vGroupByUserID
        pLastReadVariableName = "ApplicationName" 
        pDALParameters.Add("GroupByApplicationName", ccDAL.enmSQLDataType.Bit).Value = vGroupByApplicationName
        pLastReadVariableName = "LoginTime" 
        pDALParameters.Add("GroupByLoginTime", ccDAL.enmSQLDataType.Bit).Value = vGroupByLoginTime
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController
      Dim pUserStatuss As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pUserStatuss, "csUserStatusCol_FillSumOnTheFly", vRequester) : If Not pFault.isOK Then Return pFault 
      If pUserStatuss IsNot Nothing AndAlso Me.Count <> pUserStatuss.Count Then FillFromListOfITargCCEntity(pUserStatuss) 
    End If 
 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vUserStatusArray As csUserStatus())
    Me.Clear()
    
    For Each pUserStatus As csUserStatus In vUserStatusArray
      Me.Add(pUserStatus)
      _Clean.Add(pUserStatus.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pUserStatus As New csUserStatus(pRow, vRequester, _WithParents) 
        Me.Add(pUserStatus) 
        _Clean.Add(pUserStatus.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-UserStatusCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-130515-1300", vRequester) 
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
      Dim pUserStatuss As csUserStatusCol = CType(pXmlSerializer.Deserialize(pStreamReader), csUserStatusCol) 
      For Each pUserStatus As csUserStatus In pUserStatuss 
        Me.Add(pUserStatus) 
        _Clean.Add(pUserStatus.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-UserStatus-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-190720-1443", vRequester) 
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
 
      Dim pUserStatuss As List(Of csUserStatus) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csUserStatus))(vJSON, pSettings) 
      For Each pUserStatus As csUserStatus In pUserStatuss 
        Me.Add(pUserStatus) 
        _Clean.Add(pUserStatus.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-190720-2059", vRequester) 
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
          For Each lUserStatus As csUserStatus In Me 
            Dim pByte As Byte() = lUserStatus.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-150307-2340", vRequester) 
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
            Dim pUserStatus As csUserStatus = New csUserStatus(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pUserStatus) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pUserStatus.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-UserStatus-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pUserStatus As csUserStatus In Me 
      With pUserStatus 
        pFault = pUserStatus.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csUserStatusCol) Then Return False 
    Dim pUserStatusColToTest As csUserStatusCol = CType(vEntitiesToTest, csUserStatusCol) 
    Return isEqual(pUserStatusColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vUserStatussToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vUserStatussToTest As csUserStatusCol) As Boolean
    If Me.Count <> vUserStatussToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vUserStatussToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUserStatuss._FilledFromSumOnTheFly = True
    
    For Each pUserStatus As csUserStatus In Me 
      Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
      pUserStatuss.Add(pUserStatusClone) 
      If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
    Next 
    Return pUserStatuss 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUserStatusCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUserStatuss._FilledFromSumOnTheFly = True
    
    For Each pUserStatus As csUserStatus In Me
      Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
      pUserStatuss.Add(pUserStatusClone)
      If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
    Next
    Return pUserStatuss
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csUserStatusCol 
    Dim pUserStatuss As New csUserStatusCol(_WithParents)  
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserStatus As csUserStatus In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserStatus.ID > vIDFrom AndAlso pUserStatus.ID <= vIDTo) Then 
        Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
        pUserStatuss.Add(pUserStatusClone) 
        If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
      End If 
    Next 
    Return pUserStatuss 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by LastLoggedLoginID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedLastLoggedLoginID(ByVal vLastLoggedLoginIDFrom As Long, ByVal vLastLoggedLoginIDTo As Long) As csUserStatusCol 
    Dim pUserStatuss As New csUserStatusCol(_WithParents)  
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserStatus As csUserStatus In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserStatus.LastLoggedLoginID > vLastLoggedLoginIDFrom AndAlso pUserStatus.LastLoggedLoginID <= vLastLoggedLoginIDTo) Then 
        Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
        pUserStatuss.Add(pUserStatusClone) 
        If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
      End If 
    Next 
    Return pUserStatuss 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by LoginTime (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedLoginTime(ByVal vLoginTimeStart As Date, ByVal vLoginTimeEnd As Date) As csUserStatusCol 
    Dim pUserStatuss As New csUserStatusCol(_WithParents)  
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserStatus As csUserStatus In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserStatus.LoginTime > vLoginTimeStart AndAlso pUserStatus.LoginTime <= vLoginTimeEnd) Then 
        Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
        pUserStatuss.Add(pUserStatusClone) 
        If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
      End If 
    Next 
    Return pUserStatuss 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by UserID and ApplicationName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedUserIDAndApplicationName(ByVal vUserID As Long, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String) As csUserStatusCol 
    Dim pUserStatuss As New csUserStatusCol(_WithParents)  
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserStatus As csUserStatus In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserStatus.UserID = vUserID) AndAlso (pUserStatus.ApplicationName > vApplicationNameFrom AndAlso pUserStatus.ApplicationName <= vApplicationNameTo) Then 
        Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
        pUserStatuss.Add(pUserStatusClone) 
        If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
      End If 
    Next 
    Return pUserStatuss 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardUserIDAndApplicationName(ByVal vUserID As Long, ByVal vUserIDWildcardType As clsEnums.enmWildCardType, ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType) As csUserStatusCol 
    Dim pUserStatuss As New csUserStatusCol 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserStatus As csUserStatus In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pUserStatus.ApplicationName.StartsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUserStatus.ApplicationName.EndsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUserStatus.ApplicationName.IndexOf(vApplicationName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vApplicationName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUserStatus.ApplicationName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pUserStatusClone As csUserStatus = pUserStatus.Clone() 
      pUserStatuss.Add(pUserStatusClone) 
    Next 
    Return pUserStatuss 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pUserStatus As csUserStatus In Me
      pFault = pUserStatus.LoadParents(vRequester)
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
  Public Function FindByID(ByVal vID As Long) As csUserStatus
    If Me.Count = 0 Then Return New csUserStatus 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
    
    Dim pUserStatus As csUserStatus = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pUserStatus) 
    If pUserStatus IsNot Nothing Then Return pUserStatus Else Return New csUserStatus() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByLastLoggedLoginID(ByVal vLastLoggedLoginID As Long) As csUserStatus
    If Me.Count = 0 Then Return New csUserStatus 
    
    If _RecreateDictionaryForFindByLastLoggedLoginID = True Then LoadLastLoggedLoginIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csUserStatus) = _SortedDictionaryForFindByLastLoggedLoginID 
    
    Dim pUserStatus As csUserStatus = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vLastLoggedLoginID.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pUserStatus) 
    If pUserStatus IsNot Nothing Then Return pUserStatus Else Return New csUserStatus() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByUserIDAndApplicationName(ByVal vUserID As Long, ByVal vApplicationName As String) As csUserStatus
    If Me.Count = 0 Then Return New csUserStatus 
    
    If _RecreateDictionaryForFindByUserIDAndApplicationName = True Then LoadUserIDAndApplicationNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csUserStatus) = _SortedDictionaryForFindByUserIDAndApplicationName 
    
    Dim pUserStatus As csUserStatus = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vUserID.ToString() & "|" & vApplicationName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pUserStatus) 
    If pUserStatus IsNot Nothing Then Return pUserStatus Else Return New csUserStatus() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserID(ByVal vUserID As Long) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.UserID = vUserID Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserID with vUserID of {vUserID}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.UserID = vUserID Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplicationName(ByVal vApplicationName As String) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplicationName = vApplicationName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplicationName with vApplicationName of {vApplicationName}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastLoggedLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastLoggedLoginID(ByVal vLastLoggedLoginID As Long) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.LastLoggedLoginID = vLastLoggedLoginID Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastLoggedLoginID with vLastLoggedLoginID of {vLastLoggedLoginID}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.LastLoggedLoginID = vLastLoggedLoginID Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoginTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoginTime(ByVal vLoginTime As Date) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.LoginTime = vLoginTime Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoginTime with vLoginTime of {vLoginTime}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.LoginTime = vLoginTime Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LogoutTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLogoutTime(ByVal vLogoutTime As Date) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.LogoutTime = vLogoutTime Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLogoutTime with vLogoutTime of {vLogoutTime}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.LogoutTime = vLogoutTime Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csUserStatusCol
    Dim pUserStatuss As New csUserStatusCol(_WithParents) 
    pUserStatuss._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserStatus) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserStatus As csUserStatus In pTempDist.Values
        If pUserStatus.Tag.ToLowerInvariant() = vTag Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csUserStatusCol = Me.Clone() 
      For Each pUserStatus As csUserStatus In pList 
        If pUserStatus.Tag.ToLowerInvariant() = vTag Then
          Dim pUserStatusClone As csUserStatus = pUserStatus.Clone()
          pUserStatuss.Add(pUserStatusClone)
          If Not _FilledFromSumOnTheFly Then pUserStatuss._Clean.Add(pUserStatus.ID) 
        End If
      Next
    End If 
    
    Return pUserStatuss
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
    For Each pUserStatus As csUserStatus In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pUserStatus.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusView, "csUserStatusCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csUserStatus In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csUserStatus = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pUserStatusToKill As New csUserStatus 
          pUserStatusToKill.ID = pCleanID 
          pUserStatusToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pUserStatusToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csUserStatus In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-UserStatus-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusUpdate, "csUserStatusCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csUserStatus In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csUserStatus In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csUserStatusCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoginTime 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLoginTime(ByVal vLoginTime As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginTime={0}", vLoginTime)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByLoginTime", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByLoginTime"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllUserStatuss As New csUserStatusCol() : pAllUserStatuss.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredUserStatuss As csUserStatusCol = pAllUserStatuss.CloneByLoginTime(vLoginTime) 
      For Each l In pFilteredUserStatuss 
        pAllUserStatuss.Remove(pAllUserStatuss.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllUserStatuss, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "LoginTime" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = (vLoginTime) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByUserID(ByVal vUserID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}", vUserID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByUserID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByUserID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllUserStatuss As New csUserStatusCol() : pAllUserStatuss.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredUserStatuss As csUserStatusCol = pAllUserStatuss.CloneByUserID(vUserID) 
      For Each l In pFilteredUserStatuss 
        pAllUserStatuss.Remove(pAllUserStatuss.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllUserStatuss, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vUserID) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-UserStatus-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LastLoggedLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedLastLoggedLoginID(ByVal vLastLoggedLoginIDFrom As Long, ByVal vLastLoggedLoginIDTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastLoggedLoginIDFrom={0}, LastLoggedLoginIDTo={1}", vLastLoggedLoginIDFrom, vLastLoggedLoginIDTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByBoundedLastLoggedLoginID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByBoundedLastLoggedLoginID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-UserStatus-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "LastLoggedLoginIDFrom" 
        pDALParameters.Add("bndLastLoggedLoginIDFrom", ccDAL.enmSQLDataType.BigInt).Value = (vLastLoggedLoginIDFrom) 
        pLastReadVariableName = "LastLoggedLoginIDTo" 
        pDALParameters.Add("bndLastLoggedLoginIDTo", ccDAL.enmSQLDataType.BigInt).Value = (vLastLoggedLoginIDTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoginTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedLoginTime(ByVal vLoginTimeStart As Date, ByVal vLoginTimeEnd As Date, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoginTimeStart={0}, LoginTimeEnd={1}", vLoginTimeStart, vLoginTimeEnd)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByBoundedLoginTime", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByBoundedLoginTime"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-UserStatus-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "LoginTimeFrom" 
        pDALParameters.Add("bndLoginTimeFrom", ccDAL.enmSQLDataType.DateTime).Value = (vLoginTimeStart) 
        pLastReadVariableName = "LoginTimeTo" 
        pDALParameters.Add("bndLoginTimeTo", ccDAL.enmSQLDataType.DateTime).Value = (vLoginTimeEnd) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserIDAndApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedUserIDAndApplicationName(ByVal vUserID As Long, ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}, ApplicationNameFrom={1}, ApplicationNameTo={2}", vUserID, vApplicationNameFrom, vApplicationNameTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserStatusDelete, "csUserStatusCol_DeleteByBoundedUserIDAndApplicationName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_UserStatussDeleteByBoundedUserID&ApplicationName"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-UserStatus-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "c_UserID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vUserID) 
        pLastReadVariableName = "ApplicationNameFrom" 
        pDALParameters.Add("bndApplicationNameFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameFrom) 
        pLastReadVariableName = "ApplicationNameTo" 
        pDALParameters.Add("bndApplicationNameTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vApplicationNameTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-UserStatus-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-UserStatus-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-090210-1341", vRequester) 
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
    Me.Sort(New csUserStatusCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
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
  
  Public Sub SortByUserID()
    Me.Sort(New csUserStatusCol.CompareByUserID)
  End Sub
  Private Class CompareByUserID
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UserID < y.UserID Then
        Return -1
      ElseIf x.UserID = y.UserID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUserText()
    Me.Sort(New csUserStatusCol.CompareByUserText)
  End Sub
  Private Class CompareByUserText
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserText, y.UserText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApplicationName()
    Me.Sort(New csUserStatusCol.CompareByApplicationName)
  End Sub
  Private Class CompareByApplicationName
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApplicationName, y.ApplicationName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastLoggedLoginID()
    Me.Sort(New csUserStatusCol.CompareByLastLoggedLoginID)
  End Sub
  Private Class CompareByLastLoggedLoginID
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LastLoggedLoginID < y.LastLoggedLoginID Then
        Return -1
      ElseIf x.LastLoggedLoginID = y.LastLoggedLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoginTime()
    Me.Sort(New csUserStatusCol.CompareByLoginTime)
  End Sub
  Private Class CompareByLoginTime
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoginTime < y.LoginTime Then
        Return -1
      ElseIf x.LoginTime = y.LoginTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLogoutTime()
    Me.Sort(New csUserStatusCol.CompareByLogoutTime)
  End Sub
  Private Class CompareByLogoutTime
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LogoutTime < y.LogoutTime Then
        Return -1
      ElseIf x.LogoutTime = y.LogoutTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csUserStatusCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csUserStatus)
    Private Function Compare(ByVal x As csUserStatus, ByVal y As csUserStatus) As Integer Implements System.Collections.Generic.IComparer(Of csUserStatus).Compare
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
  
    Dim pUserStatus As csUserStatus
  
    While vReader.Read()
      pUserStatus = New csUserStatus(_WithParents) 
      pFault = pUserStatus.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pUserStatus)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pUserStatus.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedUserStatusCol As csUserStatusCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pUserStatus As csUserStatus 
 
      For Each pCachedUserStatus As csUserStatus In vCachedUserStatusCol 
        pCachedUserStatus.SetWithParents(_WithParents) 
        pUserStatus = New csUserStatus(pCachedUserStatus) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pUserStatus.UserText = "." 
        End If 
        pUserStatus.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pUserStatus) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pUserStatus.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-UserStatus-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUserStatus) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByLastLoggedLoginID = New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByLastLoggedLoginID = False 
    _SortedDictionaryForFindByUserIDAndApplicationName = New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByUserIDAndApplicationName = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUserStatus) 
    _SortedDictionaryForFindByLastLoggedLoginID = New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByUserIDAndApplicationName = New Dictionary(Of String, csUserStatus)(StringComparer.OrdinalIgnoreCase) 
 
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
  
