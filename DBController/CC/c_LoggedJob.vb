Public Class csLoggedJob
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
    [Job] 
    [RunStatus] 
    [LoggedAlert] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [Job] 
    [WhenStarted] 
    [ActivatingUser] 
    [LastRunBy] 
    [ExecutionTimeSec] 
    [RunStatus] 
    [Remarks] 
    [LoggedAlert] 
    [SuccessCount] 
    [FailureCount] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [ExecutionTimeSec] 
    [SuccessCount] 
    [FailureCount] 
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
  Private _JobID As Long
  Private _Job As csJob
  Private _JobText As String
  Private _WhenStarted As Date
  Private _ActivatingUser As String
  Private _LastRunBy As String
  Private _ExecutionTimeSec As Integer
  Private _RunStatus As clsEnums.enmJobStatus
  Private _RunStatusText As String 
  Private _Remarks As String
  Private _LoggedAlertID As Long
  Private _LoggedAlert As csLoggedAlert
  Private _LoggedAlertText As String
  Private _SuccessCount As Integer
  Private _FailureCount As Integer
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
  Public Property [JobID]() As Long
    Get
      Return Me._JobID
    End Get
    Set(ByVal value As Long)
      If Me._JobID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._JobID = value 
      End If 
    End Set
  End Property
  Public Property [Job]() As csJob
    Get
      Return Me._Job
    End Get
    Set(ByVal value As csJob)
      Me._Job = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the Job object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property JobText() As String
    Get
      Return Me._JobText
    End Get
    Set(ByVal value As String)
      Me._JobText = value
    End Set
  End Property
  Public Property [WhenStarted]() As Date
    Get
      Return Me._WhenStarted
    End Get
    Set(ByVal value As Date)
      If Me._WhenStarted <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenStarted = value 
      End If 
    End Set
  End Property
  Public Property [ActivatingUser]() As String
    Get
      Return Me._ActivatingUser
    End Get
    Set(ByVal value As String)
      If Me._ActivatingUser <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ActivatingUser = value 
      End If 
    End Set
  End Property
  Public Property [LastRunBy]() As String
    Get
      Return Me._LastRunBy
    End Get
    Set(ByVal value As String)
      If Me._LastRunBy <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastRunBy = value 
      End If 
    End Set
  End Property
  Public Property [ExecutionTimeSec]() As Integer
    Get
      Return Me._ExecutionTimeSec
    End Get
    Set(ByVal value As Integer)
      If Me._ExecutionTimeSec <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ExecutionTimeSec = value 
      End If 
    End Set
  End Property
  Public Property [RunStatus]() As clsEnums.enmJobStatus
    Get
      Return Me._RunStatus
    End Get
    Set(ByVal value As clsEnums.enmJobStatus)
      If Me._RunStatus <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RunStatus = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [RunStatusText]() As String
    Get
      Return Me._RunStatusText
    End Get
    Set(ByVal value As String)
      Me._RunStatusText = value
    End Set
  End Property
  Public Property [Remarks]() As String
    Get
      Return Me._Remarks
    End Get
    Set(ByVal value As String)
      If Me._Remarks <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Remarks = value 
      End If 
    End Set
  End Property
  Public Property [LoggedAlertID]() As Long
    Get
      Return Me._LoggedAlertID
    End Get
    Set(ByVal value As Long)
      If Me._LoggedAlertID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoggedAlertID = value 
      End If 
    End Set
  End Property
  Public Property [LoggedAlert]() As csLoggedAlert
    Get
      Return Me._LoggedAlert
    End Get
    Set(ByVal value As csLoggedAlert)
      Me._LoggedAlert = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the LoggedAlert object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property LoggedAlertText() As String
    Get
      Return Me._LoggedAlertText
    End Get
    Set(ByVal value As String)
      Me._LoggedAlertText = value
    End Set
  End Property
  Public Property [SuccessCount]() As Integer
    Get
      Return Me._SuccessCount
    End Get
    Set(ByVal value As Integer)
      If Me._SuccessCount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SuccessCount = value 
      End If 
    End Set
  End Property
  Public Property [FailureCount]() As Integer
    Get
      Return Me._FailureCount
    End Get
    Set(ByVal value As Integer)
      If Me._FailureCount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FailureCount = value 
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
    If _JobID <> 0 Then pValue.Append("JobID='" & _JobID.ToString() & "' ‡ ") 
    If _JobText <> "" Then pValue.Append("JobText='" & _JobText & "' ‡ ") 
    If Not (_WhenStarted = Nothing) Then pValue.Append("WhenStarted='" & _WhenStarted.ToString("o") & "' ‡ ") 
    If _ActivatingUser <> "" Then pValue.Append("ActivatingUser='" & _ActivatingUser & "' ‡ ") 
    If _LastRunBy <> "" Then pValue.Append("LastRunBy='" & _LastRunBy & "' ‡ ") 
    If _ExecutionTimeSec <> 0 Then pValue.Append("ExecutionTimeSec='" & _ExecutionTimeSec.ToString() & "' ‡ ") 
    If _RunStatus <> clsEnums.enmJobStatus.UD Then pValue.Append("RunStatus='" & _RunStatus.FastToString() & "' ‡ ") 
    If _RunStatusText <> "" Then pValue.Append("RunStatusText='" & _RunStatusText & "' ‡ ") 
    If _Remarks <> "" Then pValue.Append("Remarks='" & _Remarks & "' ‡ ") 
    If _LoggedAlertID <> 0 Then pValue.Append("LoggedAlertID='" & _LoggedAlertID.ToString() & "' ‡ ") 
    If _LoggedAlertText <> "" Then pValue.Append("LoggedAlertText='" & _LoggedAlertText & "' ‡ ") 
    If _SuccessCount <> 0 Then pValue.Append("SuccessCount='" & _SuccessCount.ToString() & "' ‡ ") 
    If _FailureCount <> 0 Then pValue.Append("FailureCount='" & _FailureCount.ToString() & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _JobID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_JobText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenStarted.ToShortDateString & " " & _WhenStarted.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ActivatingUser)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastRunBy)}""") 
    pCSV.Append("," & _ExecutionTimeSec.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_RunStatus.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_RunStatusText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Remarks)}""") 
    pCSV.Append("," & _LoggedAlertID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LoggedAlertText)}""") 
    pCSV.Append("," & _SuccessCount.ToString() & "") 
    pCSV.Append("," & _FailureCount.ToString() & "") 
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
  
  Public Sub New(ByVal vcsLoggedJob As csLoggedJob)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsLoggedJob) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vJobID As Long = 0 _ 
    , Optional vJobText As String = "" _ 
    , Optional vWhenStarted As Date = Nothing _ 
    , Optional vActivatingUser As String = "" _ 
    , Optional vLastRunBy As String = "" _ 
    , Optional vExecutionTimeSec As Integer = 0 _ 
    , Optional vRunStatus As clsEnums.enmJobStatus = clsEnums.enmJobStatus.UD _ 
    , Optional vRunStatusText As String = "" _ 
    , Optional vRemarks As String = "" _ 
    , Optional vLoggedAlertID As Long = 0 _ 
    , Optional vLoggedAlertText As String = "" _ 
    , Optional vSuccessCount As Integer = 0 _ 
    , Optional vFailureCount As Integer = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _JobID = vJobID 
    _JobText = vJobText 
    _WhenStarted = vWhenStarted 
    _ActivatingUser = vActivatingUser 
    _LastRunBy = vLastRunBy 
    _ExecutionTimeSec = vExecutionTimeSec 
    _RunStatus = vRunStatus 
    _RunStatusText = vRunStatusText 
    _Remarks = vRemarks 
    _LoggedAlertID = vLoggedAlertID 
    _LoggedAlertText = vLoggedAlertText 
    _SuccessCount = vSuccessCount 
    _FailureCount = vFailureCount 
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
 
    _ActivatingUser = _ActivatingUser.Truncate(pTruncateLength, _IsTruncated) 
    _LastRunBy = _LastRunBy.Truncate(pTruncateLength, _IsTruncated) 
    _Remarks = _Remarks.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ActivatingUser = ccHelper.RemoveChrW0(_ActivatingUser) 
    _LastRunBy = ccHelper.RemoveChrW0(_LastRunBy) 
    _Remarks = ccHelper.RemoveChrW0(_Remarks) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedJob by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJob_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedJob-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the LoggedJob by the chosen parameters. This function may be a bit slower than accessing the LoggedJob's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJob_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedJob-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedJob-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the LoggedJob by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJob_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"LoggedJob not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-LoggedJob-210927-1527", vRequester, vAdditionalMessageToUser:=$"LoggedJob not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccLoggedJobCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"LoggedJob not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-LoggedJob-210625-0950", vRequester, vAdditionalMessageToUser:=$"LoggedJob not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobUpdate, "csLoggedJob_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedJob-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobUpdate, "csLoggedJob_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-LoggedJob-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the LoggedJob. If there are parents or children in the LoggedJob, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Friend Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobUpdate, "csLoggedJob_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pLoggedJob As New csLoggedJob(_WithParents) 
    If Me.isEqual(pLoggedJob) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-LoggedJob-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-LoggedJob-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_LoggedJobUpdate"
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
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      Dim pCachedLoggedJob As csLoggedJob 
      If _ID = 0 Then 
        pCachedLoggedJob = New csLoggedJob(_WithParents) 
        'get last ID 
        Dim pLoggedJobCol As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.Clone() 
        If pLoggedJobCol.Count = 0 Then 
          _ID = 1 
        Else 
          pLoggedJobCol.SortByID() 
          Dim pLastID As Long = pLoggedJobCol(pLoggedJobCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccLoggedJobCol.Add(pCachedLoggedJob) 
      Else  
        pCachedLoggedJob = MyController.DBCache.ccLoggedJobCol.FindByID(_ID) 
      End If 
      pCachedLoggedJob.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedJobCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_JobID, False) 
        pLastReadVariableName = "WhenStarted" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.DateTime).Value = ccHelper.DateNullable(_WhenStarted) 
        pLastReadVariableName = "ActivatingUser" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_ActivatingUser) 
        pLastReadVariableName = "LastRunBy" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_LastRunBy) 
        pLastReadVariableName = "ExecutionTimeSec" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_ExecutionTimeSec) 
        pLastReadVariableName = "enmRunStatus_JobStatus" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_RunStatus.FastToString()) 
        pLastReadVariableName = "Remarks" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar).Value = ccHelper.ObjectNullable(_Remarks) 
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(_LoggedAlertID, False) 
        pLastReadVariableName = "SuccessCount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_SuccessCount) 
        pLastReadVariableName = "FailureCount" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.Int).Value = (_FailureCount) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Parents 
      Dim pJob As csJob = _Job 
      Dim pLoggedAlert As csLoggedAlert = _LoggedAlert 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Parents 
      If _WithParents <> clsEnums.enmLoadParent.EntireObject Then 
        If Not pJob Is Nothing Then _Job = pJob 
        If Not pLoggedAlert Is Nothing Then _LoggedAlert = pLoggedAlert 
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
    Dim pFunctionParameters As String = String.Format("LoggedJob.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJob_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_LoggedJobDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedJobCol.Remove(MyController.DBCache.ccLoggedJobCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedJobCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJob_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedJobDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      MyController.DBCache.ccLoggedJobCol.Remove(MyController.DBCache.ccLoggedJobCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLoggedJobCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-231207-0843", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csLoggedJob) Then Return False 
    Dim pLoggedJobToTest As csLoggedJob = CType(vTargCCEntityToTest, csLoggedJob) 
    Return isEqual(pLoggedJobToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vLoggedJobToTest As csLoggedJob) As Boolean
    With vLoggedJobToTest
      If _ID <> .ID Then Return False
      If _JobID <> .JobID Then Return False
      If _WhenStarted <> Nothing AndAlso .WhenStarted <> Nothing Then 
        If ccHelper.ToLong(_WhenStarted.Subtract(.WhenStarted).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenStarted = Nothing AndAlso .WhenStarted = Nothing) Then 
        Return False 
      End If 
      If _ActivatingUser <> .ActivatingUser Then Return False
      If _LastRunBy <> .LastRunBy Then Return False
      If _ExecutionTimeSec <> .ExecutionTimeSec Then Return False
      If _RunStatus <> .RunStatus Then Return False
      If _Remarks <> .Remarks Then Return False
      If _LoggedAlertID <> .LoggedAlertID Then Return False
      If _SuccessCount <> .SuccessCount Then Return False
      If _FailureCount <> .FailureCount Then Return False
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
    Dim pClone As New csLoggedJob(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedJob
    Dim pClone As New csLoggedJob(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("JobID") = _JobID : Catch ex As Exception : Return pFault.LogException(ex, "JobID", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenStarted") = _WhenStarted : Catch ex As Exception : Return pFault.LogException(ex, "WhenStarted", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("ActivatingUser") = _ActivatingUser : Catch ex As Exception : Return pFault.LogException(ex, "ActivatingUser", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastRunBy") = _LastRunBy : Catch ex As Exception : Return pFault.LogException(ex, "LastRunBy", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("ExecutionTimeSec") = _ExecutionTimeSec : Catch ex As Exception : Return pFault.LogException(ex, "ExecutionTimeSec", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("RunStatus") = _RunStatus : Catch ex As Exception : Return pFault.LogException(ex, "RunStatus", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("Remarks") = _Remarks : Catch ex As Exception : Return pFault.LogException(ex, "Remarks", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoggedAlertID") = _LoggedAlertID : Catch ex As Exception : Return pFault.LogException(ex, "LoggedAlertID", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("SuccessCount") = _SuccessCount : Catch ex As Exception : Return pFault.LogException(ex, "SuccessCount", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
    Try : vDataRow("FailureCount") = _FailureCount : Catch ex As Exception : Return pFault.LogException(ex, "FailureCount", "TRGT-LoggedJob-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLoggedJob As csLoggedJob = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedJob) 
      AssignValues(pLoggedJob) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-LoggedJob-130515-1230", vRequester) 
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
          'JobID 
          pBinaryWriter.Write(_JobID) 
          'Job 
          If _Job IsNot Nothing Then 
            pObjectBytes = _Job.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _JobText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_JobText) 
          'WhenStarted 
          pBinaryWriter.Write(_WhenStarted.Ticks) 
          'ActivatingUser 
          If _ActivatingUser Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ActivatingUser) 
          'LastRunBy 
          If _LastRunBy Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastRunBy) 
          'ExecutionTimeSec 
          pBinaryWriter.Write(_ExecutionTimeSec) 
          'RunStatus 
          pBinaryWriter.Write(_RunStatus.FastToString()) 
          'Remarks 
          If _Remarks Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Remarks) 
          'LoggedAlertID 
          pBinaryWriter.Write(_LoggedAlertID) 
          'LoggedAlert 
          If _LoggedAlert IsNot Nothing Then 
            pObjectBytes = _LoggedAlert.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _LoggedAlertText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LoggedAlertText) 
          'SuccessCount 
          pBinaryWriter.Write(_SuccessCount) 
          'FailureCount 
          pBinaryWriter.Write(_FailureCount) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-150307-2338", vRequester) 
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
          'JobID 
          _JobID = pReader.ReadInt64 
          'Job 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _Job = New csJob(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _JobText = pReader.ReadString 
          'WhenStarted 
          _WhenStarted = New Date(pReader.ReadInt64) 
          'ActivatingUser 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ActivatingUser = pReader.ReadString 
          'LastRunBy 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastRunBy = pReader.ReadString 
          'ExecutionTimeSec 
          _ExecutionTimeSec = pReader.ReadInt32 
          'RunStatus 
          _RunStatus = clsEnums.TranslateEnmJobStatus(pReader.ReadString) 
          'Remarks 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Remarks = pReader.ReadString 
          'LoggedAlertID 
          _LoggedAlertID = pReader.ReadInt64 
          'LoggedAlert 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedAlert = New csLoggedAlert(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LoggedAlertText = pReader.ReadString 
          'SuccessCount 
          _SuccessCount = pReader.ReadInt32 
          'FailureCount 
          _FailureCount = pReader.ReadInt32 
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
      rFault.LogException(ex, "", "TRGT-LoggedJob-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-190720-1443", vRequester) 
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
 
      Dim pLoggedJob As csLoggedJob = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csLoggedJob)(vJSON, pSettings) 
      AssignValues(pLoggedJob) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vLoggedJob As csLoggedJob)
    With vLoggedJob
      _ID = .ID 
      _JobID = .JobID 
      If .Job IsNot Nothing Then 
        _Job = .Job.Clone() 
      End If 
      _JobText = .JobText 
      _WhenStarted = .WhenStarted 
      _ActivatingUser = .ActivatingUser 
      _LastRunBy = .LastRunBy 
      _ExecutionTimeSec = .ExecutionTimeSec 
      _RunStatus = .RunStatus 
      _RunStatusText = .RunStatusText
      _Remarks = .Remarks 
      _LoggedAlertID = .LoggedAlertID 
      If .LoggedAlert IsNot Nothing Then 
        _LoggedAlert = .LoggedAlert.Clone() 
      End If 
      _LoggedAlertText = .LoggedAlertText 
      _SuccessCount = .SuccessCount 
      _FailureCount = .FailureCount 
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
      'RunStatus 
      pTextToGet = "RunStatusText (Enum)" 
      _RunStatusText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.JobStatus, _RunStatus.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-LoggedJob-151124-1900", vRequester) 
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
    If _JobID > 0 Then
      _Job = New csJob()
      pFault = _Job.GetByID(_JobID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _JobText = _Job.DefaultDesignation 
    End If
    If _LoggedAlertID > 0 Then
      _LoggedAlert = New csLoggedAlert()
      pFault = _LoggedAlert.GetByID(_LoggedAlertID, vRequester, True)
      If pFault.isOK = False Then Return pFault
      _LoggedAlertText = _LoggedAlert.DefaultDesignation 
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
      pLastReadVariableName = "c_JobID" 
      If Not vReader.IsDBNull(1) Then _JobID = vReader.GetInt64(1)
      pLastReadVariableName = "WhenStarted" 
      If Not vReader.IsDBNull(2) Then _WhenStarted = vReader.GetDateTime(2)
      pLastReadVariableName = "ActivatingUser" 
      If Not vReader.IsDBNull(3) Then _ActivatingUser = vReader.GetString(3) 
      pLastReadVariableName = "LastRunBy" 
      If Not vReader.IsDBNull(4) Then _LastRunBy = vReader.GetString(4) 
      pLastReadVariableName = "ExecutionTimeSec" 
      If Not vReader.IsDBNull(5) Then _ExecutionTimeSec = vReader.GetInt32(5)
      pLastReadVariableName = "enmRunStatus_JobStatus" 
      If Not vReader.IsDBNull(6) Then _RunStatus = clsEnums.TranslateEnmJobStatus(vReader.GetString(6))
      pLastReadVariableName = "Remarks" 
      If Not vReader.IsDBNull(7) Then _Remarks = vReader.GetString(7) 
      pLastReadVariableName = "c_LoggedAlertID" 
      If Not vReader.IsDBNull(8) Then _LoggedAlertID = vReader.GetInt64(8)
      pLastReadVariableName = "SuccessCount" 
      If Not vReader.IsDBNull(9) Then _SuccessCount = vReader.GetInt32(9)
      pLastReadVariableName = "FailureCount" 
      If Not vReader.IsDBNull(10) Then _FailureCount = vReader.GetInt32(10)
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(11) Then bDateAdded = vReader.GetDateTime(11)   
      If _WithParents = clsEnums.enmLoadParent.TextOnly Then 
        pLastReadVariableName = "JobText" 
        If Not vReader.IsDBNull(12) Then _JobText = vReader.GetString(12) 
        pLastReadVariableName = "LoggedAlertText" 
        If Not vReader.IsDBNull(13) Then _LoggedAlertText = vReader.GetString(13) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedJob As csLoggedJob, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pWithParents As clsEnums.enmLoadParent = _WithParents 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedLoggedJob) 
      If pWithParents = clsEnums.enmLoadParent.DoNotLoad Then 
        _JobText = "."
        _LoggedAlertText = "."
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _JobID = 0
    _Job = Nothing
    _JobText = "."
    _WhenStarted = Nothing
    _ActivatingUser = ""
    _LastRunBy = ""
    _ExecutionTimeSec = 0
    _RunStatus = clsEnums.enmJobStatus.UD
    _RunStatusText = ""
    _Remarks = ""
    _LoggedAlertID = 0
    _LoggedAlert = Nothing
    _LoggedAlertText = "."
    _SuccessCount = 0
    _FailureCount = 0
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
  
Public Class csLoggedJobCol
  Inherits cTargCCCollection(Of csLoggedJob)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLoggedJob) 
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
 
    For Each pRow As csLoggedJob In Me 
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
    pCSVTitle.Append(",""JobID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Job (Text)""") 
    pCSVTitle.Append(",""WhenStarted""") 
    pCSVTitle.Append(",""ActivatingUser""") 
    pCSVTitle.Append(",""LastRunBy""") 
    pCSVTitle.Append(",""ExecutionTimeSec""") 
    pCSVTitle.Append(",""RunStatus" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""RunStatus (Text)""") 
    pCSVTitle.Append(",""Remarks""") 
    pCSVTitle.Append(",""LoggedAlertID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""LoggedAlert (Text)""") 
    pCSVTitle.Append(",""SuccessCount""") 
    pCSVTitle.Append(",""FailureCount""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csLoggedJob In Me 
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
 
  Public Overloads Sub Add(ByVal vLoggedJob As csLoggedJob) 
    SyncLock _CollectionLock 
      MyBase.Add(vLoggedJob) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vLoggedJob As csLoggedJob) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vLoggedJob) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vLoggedJobCol As csLoggedJobCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vLoggedJobCol) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vLoggedJob As csLoggedJob) 
    SyncLock _CollectionLock 
      MyBase.Remove(vLoggedJob) 
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
      Dim pTempDictionary As New Dictionary(Of Long, csLoggedJob) 
      
      For Each lLoggedJob In Me 
        If lLoggedJob.IsEmpty OrElse pTempDictionary.ContainsKey(lLoggedJob.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lLoggedJob.ID, lLoggedJob) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLoggedJob.ToString, "TRGT-LoggedJob-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", LoggedJob:" & lLoggedJob.ToString() & ", TRGT-LoggedJob-260111-154657") 'Send it up the line 
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
 
    For Each lLoggedJob As csLoggedJob In Me 
      lLoggedJob.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lLoggedJob As csLoggedJob In Me 
      lLoggedJob.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [JobID] 
    [LoggedAlertID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the LoggedJobs by the chosen parameters. This function may be a bit slower than accessing the LoggedJob's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.JobID 
          pFault = FillByJobID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.LoggedAlertID 
          pFault = FillByLoggedAlertID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-LoggedJob-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-LoggedJob-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      Dim pLoggedJobsCached As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.Clone() 
      pLoggedJobsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedJobsCached.Reverse() 
      If vHowMany > 0 AndAlso pLoggedJobsCached.Count > vHowMany Then 
        Dim tmp As New csLoggedJobCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedJobsCached(i)) 
        Next 
        pLoggedJobsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedJobsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific JobID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByJobID(ByVal vJobID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("JobID={0}", vJobID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillByJobID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      Dim pLoggedJobsCached As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.CloneByJobID(vJobID)
      pFault = LoadMeFromDBCache(pLoggedJobsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFillByJobID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vJobID, False) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LoggedAlertID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLoggedAlertID(ByVal vLoggedAlertID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LoggedAlertID={0}", vLoggedAlertID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillByLoggedAlertID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      Dim pLoggedJobsCached As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.CloneByLoggedAlertID(vLoggedAlertID)
      pFault = LoadMeFromDBCache(pLoggedJobsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFillByLoggedAlertID" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ForeignKeyLongNullable(vLoggedAlertID, False) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLoggedJobCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLoggedJobCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLoggedJobCol failed: " & pResponse) 
      Dim pLoggedJobsCached As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pLoggedJobsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lLoggedJob As New csLoggedJob() 
      pFault = lLoggedJob.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lLoggedJob.IsEmpty Then Me.Add(lLoggedJob) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pLoggedJobs As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLoggedJobs, "csLoggedJobCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLoggedJobs IsNot Nothing AndAlso Me.Count <> pLoggedJobs.Count Then FillFromListOfITargCCEntity(pLoggedJobs) 
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
    [JobID]
    [LoggedAlertID]
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pJobID As Nullable(Of Long) = Nothing
    Dim pLoggedAlertID As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobID) Then pObj = vParameters(enmFillOnTheFlyParameters.JobID) : If pObj IsNot Nothing Then pJobID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedAlertID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedAlertID) : If pObj IsNot Nothing Then pLoggedAlertID = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pJobID _
        , pLoggedAlertID _
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
        , ByVal vJobID As Nullable(Of Long) _
        , ByVal vLoggedAlertID As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, JobID={2}, LoggedAlertID={3}", vIDFrom, vIDTo, vJobID, vLoggedAlertID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedJob-121122-2008", vRequester) 
      Dim pLoggedJobsCached As csLoggedJobCol = MyController.DBCache.ccLoggedJobCol.Clone() 
      Dim pLoggedJobsToUse As New csLoggedJobCol() 
      For Each l In pLoggedJobsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vJobID.HasValue Then 
          If l.JobID <> vJobID.Value Then Continue For 
        End If 
        If vLoggedAlertID.HasValue Then 
          If l.LoggedAlertID <> vLoggedAlertID.Value Then Continue For 
        End If 
        pLoggedJobsToUse.Add(l) 
      Next 
      pLoggedJobsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLoggedJobsToUse.Reverse() 
      If vHowMany > 0 AndAlso pLoggedJobsToUse.Count > vHowMany Then 
        Dim tmp As New csLoggedJobCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLoggedJobsToUse(i)) 
        Next 
        pLoggedJobsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLoggedJobsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vJobID) 
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLoggedAlertID) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByJobID
    GroupByLoggedAlertID
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pJobID As Nullable(Of Long) = Nothing
    Dim pLoggedAlertID As Nullable(Of Long) = Nothing
    Dim pGroupByJobID As Boolean = False
    Dim pGroupByLoggedAlertID As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.JobID) Then pObj = vParameters(enmFillOnTheFlyParameters.JobID) : If pObj IsNot Nothing Then pJobID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LoggedAlertID) Then pObj = vParameters(enmFillOnTheFlyParameters.LoggedAlertID) : If pObj IsNot Nothing Then pLoggedAlertID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByJobID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByJobID) : If pObj IsNot Nothing Then pGroupByJobID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLoggedAlertID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLoggedAlertID) : If pObj IsNot Nothing Then pGroupByLoggedAlertID = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pJobID _
        , pLoggedAlertID _
        , pGroupByJobID _
        , pGroupByLoggedAlertID _
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
        , ByVal vJobID As Nullable(Of Long) _
        , ByVal vLoggedAlertID As Nullable(Of Long) _
        , ByVal vGroupByJobID As Boolean _
        , ByVal vGroupByLoggedAlertID As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, JobID={2}, LoggedAlertID={3}, GroupByJobID={4}, GroupByLoggedAlertID={5}", vIDFrom, vIDTo, vJobID, vLoggedAlertID, vGroupByJobID, vGroupByLoggedAlertID)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-LoggedJob-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LoggedJobsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vJobID) 
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vLoggedAlertID) 
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add("GroupByc_JobID", ccDAL.enmSQLDataType.Bit).Value = vGroupByJobID
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add("GroupByc_LoggedAlertID", ccDAL.enmSQLDataType.Bit).Value = vGroupByLoggedAlertID
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vLoggedJobArray As csLoggedJob())
    Me.Clear()
    
    For Each pLoggedJob As csLoggedJob In vLoggedJobArray
      Me.Add(pLoggedJob)
      _Clean.Add(pLoggedJob.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pLoggedJob As New csLoggedJob(pRow, vRequester, _WithParents) 
        Me.Add(pLoggedJob) 
        _Clean.Add(pLoggedJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-LoggedJobCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-130515-1300", vRequester) 
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
      Dim pLoggedJobs As csLoggedJobCol = CType(pXmlSerializer.Deserialize(pStreamReader), csLoggedJobCol) 
      For Each pLoggedJob As csLoggedJob In pLoggedJobs 
        Me.Add(pLoggedJob) 
        _Clean.Add(pLoggedJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-LoggedJob-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-190720-1443", vRequester) 
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
 
      Dim pLoggedJobs As List(Of csLoggedJob) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csLoggedJob))(vJSON, pSettings) 
      For Each pLoggedJob As csLoggedJob In pLoggedJobs 
        Me.Add(pLoggedJob) 
        _Clean.Add(pLoggedJob.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-190720-2059", vRequester) 
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
          For Each lLoggedJob As csLoggedJob In Me 
            Dim pByte As Byte() = lLoggedJob.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-150307-2340", vRequester) 
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
            Dim pLoggedJob As csLoggedJob = New csLoggedJob(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pLoggedJob) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pLoggedJob.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-LoggedJob-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pLoggedJob As csLoggedJob In Me 
      With pLoggedJob 
        pFault = pLoggedJob.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csLoggedJobCol) Then Return False 
    Dim pLoggedJobColToTest As csLoggedJobCol = CType(vEntitiesToTest, csLoggedJobCol) 
    Return isEqual(pLoggedJobColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vLoggedJobsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vLoggedJobsToTest As csLoggedJobCol) As Boolean
    If Me.Count <> vLoggedJobsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vLoggedJobsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedJobs._FilledFromSumOnTheFly = True
    
    For Each pLoggedJob As csLoggedJob In Me 
      Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone() 
      pLoggedJobs.Add(pLoggedJobClone) 
      If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
    Next 
    Return pLoggedJobs 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLoggedJobCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pLoggedJobs._FilledFromSumOnTheFly = True
    
    For Each pLoggedJob As csLoggedJob In Me
      Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
      pLoggedJobs.Add(pLoggedJobClone)
      If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
    Next
    Return pLoggedJobs
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csLoggedJobCol 
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents)  
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLoggedJob As csLoggedJob In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLoggedJob.ID > vIDFrom AndAlso pLoggedJob.ID <= vIDTo) Then 
        Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone() 
        pLoggedJobs.Add(pLoggedJobClone) 
        If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
      End If 
    Next 
    Return pLoggedJobs 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    For Each pLoggedJob As csLoggedJob In Me
      pFault = pLoggedJob.LoadParents(vRequester)
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
  Public Function FindByID(ByVal vID As Long) As csLoggedJob
    If Me.Count = 0 Then Return New csLoggedJob 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
    
    Dim pLoggedJob As csLoggedJob = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pLoggedJob) 
    If pLoggedJob IsNot Nothing Then Return pLoggedJob Else Return New csLoggedJob() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined JobID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByJobID(ByVal vJobID As Long) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.JobID = vJobID Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByJobID with vJobID of {vJobID}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.JobID = vJobID Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenStarted
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenStarted(ByVal vWhenStarted As Date) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.WhenStarted = vWhenStarted Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenStarted with vWhenStarted of {vWhenStarted}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.WhenStarted = vWhenStarted Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ActivatingUser
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByActivatingUser(ByVal vActivatingUser As String) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vActivatingUser = vActivatingUser.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.ActivatingUser.ToLowerInvariant() = vActivatingUser Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByActivatingUser with vActivatingUser of {vActivatingUser}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.ActivatingUser.ToLowerInvariant() = vActivatingUser Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastRunBy
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastRunBy(ByVal vLastRunBy As String) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastRunBy = vLastRunBy.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.LastRunBy.ToLowerInvariant() = vLastRunBy Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastRunBy with vLastRunBy of {vLastRunBy}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.LastRunBy.ToLowerInvariant() = vLastRunBy Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ExecutionTimeSec
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByExecutionTimeSec(ByVal vExecutionTimeSec As Integer) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.ExecutionTimeSec = vExecutionTimeSec Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByExecutionTimeSec with vExecutionTimeSec of {vExecutionTimeSec}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.ExecutionTimeSec = vExecutionTimeSec Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RunStatus
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRunStatus(ByVal vRunStatus As clsEnums.enmJobStatus) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.RunStatus = vRunStatus Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRunStatus with vRunStatus of {vRunStatus}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.RunStatus = vRunStatus Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Remarks
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRemarks(ByVal vRemarks As String) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vRemarks = vRemarks.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.Remarks.ToLowerInvariant() = vRemarks Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRemarks with vRemarks of {vRemarks}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.Remarks.ToLowerInvariant() = vRemarks Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoggedAlertID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoggedAlertID(ByVal vLoggedAlertID As Long) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.LoggedAlertID = vLoggedAlertID Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoggedAlertID with vLoggedAlertID of {vLoggedAlertID}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.LoggedAlertID = vLoggedAlertID Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SuccessCount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySuccessCount(ByVal vSuccessCount As Integer) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.SuccessCount = vSuccessCount Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySuccessCount with vSuccessCount of {vSuccessCount}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.SuccessCount = vSuccessCount Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FailureCount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFailureCount(ByVal vFailureCount As Integer) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.FailureCount = vFailureCount Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFailureCount with vFailureCount of {vFailureCount}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.FailureCount = vFailureCount Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csLoggedJobCol
    Dim pLoggedJobs As New csLoggedJobCol(_WithParents) 
    pLoggedJobs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLoggedJob) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLoggedJob As csLoggedJob In pTempDist.Values
        If pLoggedJob.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csLoggedJobCol = Me.Clone() 
      For Each pLoggedJob As csLoggedJob In pList 
        If pLoggedJob.Tag.ToLowerInvariant() = vTag Then
          Dim pLoggedJobClone As csLoggedJob = pLoggedJob.Clone()
          pLoggedJobs.Add(pLoggedJobClone)
          If Not _FilledFromSumOnTheFly Then pLoggedJobs._Clean.Add(pLoggedJob.ID) 
        End If
      Next
    End If 
    
    Return pLoggedJobs
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
    For Each pLoggedJob As csLoggedJob In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pLoggedJob.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobView, "csLoggedJobCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csLoggedJob In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csLoggedJob = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pLoggedJobToKill As New csLoggedJob 
          pLoggedJobToKill.ID = pCleanID 
          pLoggedJobToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pLoggedJobToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csLoggedJob In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-LoggedJob-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobUpdate, "csLoggedJobCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csLoggedJob In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csLoggedJob In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJobCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedJobsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csLoggedJobCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific JobID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByJobID(ByVal vJobID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("JobID={0}", vJobID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJobCol_DeleteByJobID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedJobsDeleteByJobID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedJobs As New csLoggedJobCol() : pAllLoggedJobs.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedJobs As csLoggedJobCol = pAllLoggedJobs.CloneByJobID(vJobID) 
      For Each l In pFilteredLoggedJobs 
        pAllLoggedJobs.Remove(pAllLoggedJobs.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedJobs, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "c_JobID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vJobID) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LoggedAlertID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLoggedAlertID(ByVal vLoggedAlertID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LoggedAlertID={0}", vLoggedAlertID)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJobCol_DeleteByLoggedAlertID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedJobsDeleteByLoggedAlertID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLoggedJobs As New csLoggedJobCol() : pAllLoggedJobs.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLoggedJobs As csLoggedJobCol = pAllLoggedJobs.CloneByLoggedAlertID(vLoggedAlertID) 
      For Each l In pFilteredLoggedJobs 
        pAllLoggedJobs.Remove(pAllLoggedJobs.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLoggedJobs, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "c_LoggedAlertID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (vLoggedAlertID) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LoggedJobDelete, "csLoggedJobCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LoggedJobsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-LoggedJob-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-LoggedJob-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-LoggedJob-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-090210-1341", vRequester) 
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
    Me.Sort(New csLoggedJobCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
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
  
  Public Sub SortByJobID()
    Me.Sort(New csLoggedJobCol.CompareByJobID)
  End Sub
  Private Class CompareByJobID
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.JobID < y.JobID Then
        Return -1
      ElseIf x.JobID = y.JobID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByJobText()
    Me.Sort(New csLoggedJobCol.CompareByJobText)
  End Sub
  Private Class CompareByJobText
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.JobText, y.JobText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWhenStarted()
    Me.Sort(New csLoggedJobCol.CompareByWhenStarted)
  End Sub
  Private Class CompareByWhenStarted
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenStarted < y.WhenStarted Then
        Return -1
      ElseIf x.WhenStarted = y.WhenStarted Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByActivatingUser()
    Me.Sort(New csLoggedJobCol.CompareByActivatingUser)
  End Sub
  Private Class CompareByActivatingUser
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ActivatingUser, y.ActivatingUser, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastRunBy()
    Me.Sort(New csLoggedJobCol.CompareByLastRunBy)
  End Sub
  Private Class CompareByLastRunBy
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastRunBy, y.LastRunBy, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByExecutionTimeSec()
    Me.Sort(New csLoggedJobCol.CompareByExecutionTimeSec)
  End Sub
  Private Class CompareByExecutionTimeSec
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ExecutionTimeSec < y.ExecutionTimeSec Then
        Return -1
      ElseIf x.ExecutionTimeSec = y.ExecutionTimeSec Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRunStatus()
    Me.Sort(New csLoggedJobCol.CompareByRunStatus)
  End Sub
  Private Class CompareByRunStatus
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RunStatus < y.RunStatus Then
        Return -1
      ElseIf x.RunStatus = y.RunStatus Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRunStatusText()
    Me.Sort(New csLoggedJobCol.CompareByRunStatusText)
  End Sub
  Private Class CompareByRunStatusText
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RunStatusText, y.RunStatusText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRemarks()
    Me.Sort(New csLoggedJobCol.CompareByRemarks)
  End Sub
  Private Class CompareByRemarks
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Remarks, y.Remarks, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLoggedAlertID()
    Me.Sort(New csLoggedJobCol.CompareByLoggedAlertID)
  End Sub
  Private Class CompareByLoggedAlertID
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoggedAlertID < y.LoggedAlertID Then
        Return -1
      ElseIf x.LoggedAlertID = y.LoggedAlertID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoggedAlertText()
    Me.Sort(New csLoggedJobCol.CompareByLoggedAlertText)
  End Sub
  Private Class CompareByLoggedAlertText
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LoggedAlertText, y.LoggedAlertText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySuccessCount()
    Me.Sort(New csLoggedJobCol.CompareBySuccessCount)
  End Sub
  Private Class CompareBySuccessCount
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.SuccessCount < y.SuccessCount Then
        Return -1
      ElseIf x.SuccessCount = y.SuccessCount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByFailureCount()
    Me.Sort(New csLoggedJobCol.CompareByFailureCount)
  End Sub
  Private Class CompareByFailureCount
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.FailureCount < y.FailureCount Then
        Return -1
      ElseIf x.FailureCount = y.FailureCount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csLoggedJobCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csLoggedJob)
    Private Function Compare(ByVal x As csLoggedJob, ByVal y As csLoggedJob) As Integer Implements System.Collections.Generic.IComparer(Of csLoggedJob).Compare
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
  
    Dim pLoggedJob As csLoggedJob
  
    While vReader.Read()
      pLoggedJob = New csLoggedJob(_WithParents) 
      pFault = pLoggedJob.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pLoggedJob)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedJob.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLoggedJobCol As csLoggedJobCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pLoggedJob As csLoggedJob 
 
      For Each pCachedLoggedJob As csLoggedJob In vCachedLoggedJobCol 
        pCachedLoggedJob.SetWithParents(_WithParents) 
        pLoggedJob = New csLoggedJob(pCachedLoggedJob) 
        If _WithParents = clsEnums.enmLoadParent.DoNotLoad Then 
          pLoggedJob.JobText = "." 
          pLoggedJob.LoggedAlertText = "." 
        End If 
        pLoggedJob.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pLoggedJob) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pLoggedJob.ID) 
      Next 
      If _WithParents = clsEnums.enmLoadParent.EntireObject Then 
        pFault = LoadParents(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-LoggedJob-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedJob) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLoggedJob) 
 
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
  
