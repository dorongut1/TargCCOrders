Public Class csIndexFragmentation
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
      Return True 
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
    [IndexName] 
    [IndexType] 
    [FragmentationPct] 
    [PageCount] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [FragmentationPct] 
    [PageCount] 
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
  Private _TableName As String
  Private _IndexName As String
  Private _IndexType As String
  Private _FragmentationPct As Decimal
  Private _PageCount As Integer
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
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [IndexName]() As String
    Get
      Return Me._IndexName
    End Get
    Set(ByVal value As String)
      If Me._IndexName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IndexName = value 
      End If 
    End Set
  End Property
  Public Property [IndexType]() As String
    Get
      Return Me._IndexType
    End Get
    Set(ByVal value As String)
      If Me._IndexType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IndexType = value 
      End If 
    End Set
  End Property
  Public Property [FragmentationPct]() As Decimal
    Get
      Return Me._FragmentationPct
    End Get
    Set(ByVal value As Decimal)
      If Me._FragmentationPct <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FragmentationPct = value 
      End If 
    End Set
  End Property
  Public Property [PageCount]() As Integer
    Get
      Return Me._PageCount
    End Get
    Set(ByVal value As Integer)
      If Me._PageCount <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PageCount = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _TableName Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _TableName <> "" Then pValue.Append("TableName='" & _TableName & "' ‡ ") 
    If _IndexName <> "" Then pValue.Append("IndexName='" & _IndexName & "' ‡ ") 
    If _IndexType <> "" Then pValue.Append("IndexType='" & _IndexType & "' ‡ ") 
    If _FragmentationPct <> 0 Then pValue.Append("FragmentationPct='" & _FragmentationPct.ToString() & "' ‡ ") 
    If _PageCount <> 0 Then pValue.Append("PageCount='" & _PageCount.ToString() & "' ‡ ") 
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
    pCSV.Append($",""{ccHelper.StringForCSV(_IndexName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_IndexType)}""") 
    pCSV.Append("," & _FragmentationPct.ToString() & "") 
    pCSV.Append("," & _PageCount.ToString() & "") 
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
  
  Public Sub New(ByVal vcsIndexFragmentation As csIndexFragmentation)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsIndexFragmentation) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vTableName As String = "" _ 
    , Optional vIndexName As String = "" _ 
    , Optional vIndexType As String = "" _ 
    , Optional vFragmentationPct As Decimal = 0 _ 
    , Optional vPageCount As Integer = 0 _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _TableName = vTableName 
    _IndexName = vIndexName 
    _IndexType = vIndexType 
    _FragmentationPct = vFragmentationPct 
    _PageCount = vPageCount 
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
    _IndexName = _IndexName.Truncate(pTruncateLength, _IsTruncated) 
    _IndexType = _IndexType.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _TableName = ccHelper.RemoveChrW0(_TableName) 
    _IndexName = ccHelper.RemoveChrW0(_IndexName) 
    _IndexType = ccHelper.RemoveChrW0(_IndexType) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the IndexFragmentation by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentation_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-IndexFragmentation-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [TableName] 
    [ID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the IndexFragmentation by the chosen parameters. This function may be a bit slower than accessing the IndexFragmentation's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentation_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.TableName 
          pFault = GetByTableName(CStr(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-IndexFragmentation-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-IndexFragmentation-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the IndexFragmentation by TableName.
  ''' </summary>
  ''' <param name="vTableName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByTableName(ByVal vTableName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("TableName={0}", vTableName)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentation_GetByTableName", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccIndexFragmentationCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccIndexFragmentationCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csIndexFragmentationCol failed: " & pResponse) 
      ' Not Implemented Yet!!  pFault = LoadMeFromDBCache(MyController.DBCache.ccIndexFragmentationCol.FindByTableName(vTableName), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationGetByTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 200).Value = (vTableName) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"IndexFragmentation not found for GetByTableName. See FunctionParameters for values", pFunctionParameters, "TRGT-IndexFragmentation-210625-0950", vRequester, vAdditionalMessageToUser:=$"IndexFragmentation not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the IndexFragmentation by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentation_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = -1 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"IndexFragmentation not found for GetByID, since its value is -1", pFunctionParameters, "TRGT-IndexFragmentation-210927-1527", vRequester, vAdditionalMessageToUser:=$"IndexFragmentation not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccIndexFragmentationCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccIndexFragmentationCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csIndexFragmentationCol failed: " & pResponse) 
      ' Not Implemented Yet!!  pFault = LoadMeFromDBCache(MyController.DBCache.ccIndexFragmentationCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"IndexFragmentation not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-IndexFragmentation-210625-0950", vRequester, vAdditionalMessageToUser:=$"IndexFragmentation not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090623-1648", vRequester) 
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
    If Not (TypeOf (vTargCCEntityToTest) Is csIndexFragmentation) Then Return False 
    Dim pIndexFragmentationToTest As csIndexFragmentation = CType(vTargCCEntityToTest, csIndexFragmentation) 
    Return isEqual(pIndexFragmentationToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vIndexFragmentationToTest As csIndexFragmentation) As Boolean
    With vIndexFragmentationToTest
      If _ID <> .ID Then Return False
      If _TableName <> .TableName Then Return False
      If _IndexName <> .IndexName Then Return False
      If _IndexType <> .IndexType Then Return False
      If _FragmentationPct <> .FragmentationPct Then Return False
      If _PageCount <> .PageCount Then Return False
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
    Dim pClone As New csIndexFragmentation(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csIndexFragmentation
    Dim pClone As New csIndexFragmentation(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
    Try : vDataRow("TableName") = _TableName : Catch ex As Exception : Return pFault.LogException(ex, "TableName", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
    Try : vDataRow("IndexName") = _IndexName : Catch ex As Exception : Return pFault.LogException(ex, "IndexName", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
    Try : vDataRow("IndexType") = _IndexType : Catch ex As Exception : Return pFault.LogException(ex, "IndexType", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
    Try : vDataRow("FragmentationPct") = _FragmentationPct : Catch ex As Exception : Return pFault.LogException(ex, "FragmentationPct", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
    Try : vDataRow("PageCount") = _PageCount : Catch ex As Exception : Return pFault.LogException(ex, "PageCount", "TRGT-IndexFragmentation-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pIndexFragmentation As csIndexFragmentation = CType(pXmlSerializer.Deserialize(pStreamReader), csIndexFragmentation) 
      AssignValues(pIndexFragmentation) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-IndexFragmentation-130515-1230", vRequester) 
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
          'IndexName 
          If _IndexName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_IndexName) 
          'IndexType 
          If _IndexType Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_IndexType) 
          'FragmentationPct 
          pBinaryWriter.Write(_FragmentationPct) 
          'PageCount 
          pBinaryWriter.Write(_PageCount) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-150307-2338", vRequester) 
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
          'IndexName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _IndexName = pReader.ReadString 
          'IndexType 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _IndexType = pReader.ReadString 
          'FragmentationPct 
          _FragmentationPct = pReader.ReadDecimal 
          'PageCount 
          _PageCount = pReader.ReadInt32 
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
      rFault.LogException(ex, "", "TRGT-IndexFragmentation-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-190720-1443", vRequester) 
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
 
      Dim pIndexFragmentation As csIndexFragmentation = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csIndexFragmentation)(vJSON, pSettings) 
      AssignValues(pIndexFragmentation) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vIndexFragmentation As csIndexFragmentation)
    With vIndexFragmentation
      _ID = .ID 
      _TableName = .TableName 
      _IndexName = .IndexName 
      _IndexType = .IndexType 
      _FragmentationPct = .FragmentationPct 
      _PageCount = .PageCount 
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
 
    If _ID = -1 Then 
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
      pLastReadVariableName = "IndexName" 
      If Not vReader.IsDBNull(2) Then _IndexName = vReader.GetString(2) 
      pLastReadVariableName = "IndexType" 
      If Not vReader.IsDBNull(3) Then _IndexType = vReader.GetString(3) 
      pLastReadVariableName = "FragmentationPct" 
      If Not vReader.IsDBNull(4) Then _FragmentationPct = vReader.GetDecimal(4)
      pLastReadVariableName = "PageCount" 
      If Not vReader.IsDBNull(5) Then _PageCount = vReader.GetInt32(5)
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedIndexFragmentation As csIndexFragmentation, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedIndexFragmentation) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = -1 
    _TableName = ""
    _IndexName = ""
    _IndexType = ""
    _FragmentationPct = 0
    _PageCount = 0
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
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
    
    RaiseEvent evtOverrideCreateEmpty() 
    
  End Sub
  
End Class 
  
Public Class csIndexFragmentationCol
  Inherits cTargCCCollection(Of csIndexFragmentation)
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
      Return True 
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
  Private _SortedDictionaryForFindByTableName As Dictionary(Of String, csIndexFragmentation) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByTableName As Boolean 
  Private Function CreateKeyForFindByTableName(ByVal vIndexFragmentation As csIndexFragmentation) As String 
    With vIndexFragmentation 
      Return .TableName
    End With 
  End Function 
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csIndexFragmentation) 
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
 
    For Each pRow As csIndexFragmentation In Me 
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
    pCSVTitle.Append(",""IndexName""") 
    pCSVTitle.Append(",""IndexType""") 
    pCSVTitle.Append(",""FragmentationPct""") 
    pCSVTitle.Append(",""PageCount""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csIndexFragmentation In Me 
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
 
  Public Overloads Sub Add(ByVal vIndexFragmentation As csIndexFragmentation) 
    SyncLock _CollectionLock 
      MyBase.Add(vIndexFragmentation) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vIndexFragmentation As csIndexFragmentation) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vIndexFragmentation) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vIndexFragmentationCol As csIndexFragmentationCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vIndexFragmentationCol) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vIndexFragmentation As csIndexFragmentation) 
    SyncLock _CollectionLock 
      MyBase.Remove(vIndexFragmentation) 
      _RecreateDictionaryForFindByTableName = True 
      _RecreateDictionaryForFindByID = True 
    End SyncLock 
  End Sub 
 
  Private Sub LoadTableNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByTableName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByTableName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByTableName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByTableName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csIndexFragmentation)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lIndexFragmentation In Me 
        Try 
          Dim pTableName As String = CreateKeyForFindByTableName(lIndexFragmentation) 
          If String.IsNullOrEmpty(pTableName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pTableName)) Then 
            pTempDictionary.Add(pTableName, lIndexFragmentation) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lIndexFragmentation.ToString, "TRGT-IndexFragmentation-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByTableName:" & ex.Message & ", IndexFragmentation:" & lIndexFragmentation.ToString() & ", TRGT-IndexFragmentation-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByTableName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByTableName = False
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
      Dim pTempDictionary As New Dictionary(Of Long, csIndexFragmentation) 
      
      For Each lIndexFragmentation In Me 
        If lIndexFragmentation.IsEmpty OrElse pTempDictionary.ContainsKey(lIndexFragmentation.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lIndexFragmentation.ID, lIndexFragmentation) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lIndexFragmentation.ToString, "TRGT-IndexFragmentation-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", IndexFragmentation:" & lIndexFragmentation.ToString() & ", TRGT-IndexFragmentation-260111-154657") 'Send it up the line 
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
 
    For Each lIndexFragmentation As csIndexFragmentation In Me 
      lIndexFragmentation.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lIndexFragmentation As csIndexFragmentation In Me 
      lIndexFragmentation.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the IndexFragmentations by the chosen parameters. This function may be a bit slower than accessing the IndexFragmentation's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-IndexFragmentation-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-IndexFragmentation-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccIndexFragmentationCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccIndexFragmentationCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csIndexFragmentationCol failed: " & pResponse) 
      Dim pIndexFragmentationsCached As csIndexFragmentationCol = MyController.DBCache.ccIndexFragmentationCol.Clone() 
      pIndexFragmentationsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pIndexFragmentationsCached.Reverse() 
      If vHowMany > 0 AndAlso pIndexFragmentationsCached.Count > vHowMany Then 
        Dim tmp As New csIndexFragmentationCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pIndexFragmentationsCached(i)) 
        Next 
        pIndexFragmentationsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pIndexFragmentationsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090624-1625", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillByBoundedTableName", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccIndexFragmentationCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccIndexFragmentationCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csIndexFragmentationCol failed: " & pResponse) 
      Dim pIndexFragmentationsCached As csIndexFragmentationCol = MyController.DBCache.ccIndexFragmentationCol.CloneByBoundedTableName(vTableNameFrom, vTableNameTo)
      pFault = LoadMeFromDBCache(pIndexFragmentationsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFillByBoundedTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "TableNameFrom" 
        pDALParameters.Add("bndTableNameFrom", ccDAL.enmSQLDataType.VarChar, 200).Value = (vTableNameFrom) 
        pLastReadVariableName = "TableNameTo" 
        pDALParameters.Add("bndTableNameTo", ccDAL.enmSQLDataType.VarChar, 200).Value = (vTableNameTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccIndexFragmentationCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccIndexFragmentationCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csIndexFragmentationCol failed: " & pResponse) 
      Dim pIndexFragmentationsCached As csIndexFragmentationCol = MyController.DBCache.ccIndexFragmentationCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pIndexFragmentationsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillByWildCardTableName", vRequester) 
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
      Return pFault.LogFreeTextFault("FillByWildCardOrderingCode has not been handled yet locally", pFunctionParameters, "TRGT-IndexFragmentation-121122-2008", vRequester) 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFillByWildCardTableName" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "wldTableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 200).Value = (pWCTableName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lIndexFragmentation As New csIndexFragmentation() 
      pFault = lIndexFragmentation.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lIndexFragmentation.IsEmpty Then Me.Add(lIndexFragmentation) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pIndexFragmentations As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pIndexFragmentations, "csIndexFragmentationCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pIndexFragmentations IsNot Nothing AndAlso Me.Count <> pIndexFragmentations.Count Then FillFromListOfITargCCEntity(pIndexFragmentations) 
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
    [TableName]
    TableNameWildcardType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pTableName, pTableNameWildcardType _
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
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TableName={2}, TableNameWildcardType={3}", vIDFrom, vIDTo, vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillOnTheFly", vRequester) 
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
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-IndexFragmentation-121122-2008", vRequester) 
      Dim pIndexFragmentationsCached As csIndexFragmentationCol = MyController.DBCache.ccIndexFragmentationCol.Clone() 
      Dim pIndexFragmentationsToUse As New csIndexFragmentationCol() 
      For Each l In pIndexFragmentationsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
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
        pIndexFragmentationsToUse.Add(l) 
      Next 
      pIndexFragmentationsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pIndexFragmentationsToUse.Reverse() 
      If vHowMany > 0 AndAlso pIndexFragmentationsToUse.Count > vHowMany Then 
        Dim tmp As New csIndexFragmentationCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pIndexFragmentationsToUse(i)) 
        Next 
        pIndexFragmentationsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pIndexFragmentationsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 200).Value = ccHelper.ObjectNullable(pWCTableName) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090303-1658", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pTableName As String = Nothing
    Dim pTableNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableName) Then pObj = vParameters(enmFillOnTheFlyParameters.TableName) : If pObj IsNot Nothing Then pTableName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.TableNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.TableNameWildcardType) : If pObj IsNot Nothing Then pTableNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pTableName, pTableNameWildcardType _
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
        , ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, TableName={2}, TableNameWildcardType={3}", vIDFrom, vIDTo, vTableName, vTableNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.viw_c_IndexFragmentationView, "csIndexFragmentationCol_FillSumOnTheFly", vRequester) 
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
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-IndexFragmentation-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_IndexFragmentationsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "TableName" 
        pDALParameters.Add("wldTableName", ccDAL.enmSQLDataType.VarChar, 200).Value = ccHelper.ObjectNullable(pWCTableName) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vIndexFragmentationArray As csIndexFragmentation())
    Me.Clear()
    
    For Each pIndexFragmentation As csIndexFragmentation In vIndexFragmentationArray
      Me.Add(pIndexFragmentation)
      _Clean.Add(pIndexFragmentation.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pIndexFragmentation As New csIndexFragmentation(pRow, vRequester) 
        Me.Add(pIndexFragmentation) 
        _Clean.Add(pIndexFragmentation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-IndexFragmentationCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-130515-1300", vRequester) 
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
      Dim pIndexFragmentations As csIndexFragmentationCol = CType(pXmlSerializer.Deserialize(pStreamReader), csIndexFragmentationCol) 
      For Each pIndexFragmentation As csIndexFragmentation In pIndexFragmentations 
        Me.Add(pIndexFragmentation) 
        _Clean.Add(pIndexFragmentation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-IndexFragmentation-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-190720-1443", vRequester) 
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
 
      Dim pIndexFragmentations As List(Of csIndexFragmentation) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csIndexFragmentation))(vJSON, pSettings) 
      For Each pIndexFragmentation As csIndexFragmentation In pIndexFragmentations 
        Me.Add(pIndexFragmentation) 
        _Clean.Add(pIndexFragmentation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-190720-2059", vRequester) 
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
          For Each lIndexFragmentation As csIndexFragmentation In Me 
            Dim pByte As Byte() = lIndexFragmentation.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-150307-2340", vRequester) 
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
            Dim pIndexFragmentation As csIndexFragmentation = New csIndexFragmentation(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pIndexFragmentation) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pIndexFragmentation.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-IndexFragmentation-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pIndexFragmentation As csIndexFragmentation In Me 
      With pIndexFragmentation 
        pFault = pIndexFragmentation.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csIndexFragmentationCol) Then Return False 
    Dim pIndexFragmentationColToTest As csIndexFragmentationCol = CType(vEntitiesToTest, csIndexFragmentationCol) 
    Return isEqual(pIndexFragmentationColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vIndexFragmentationsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vIndexFragmentationsToTest As csIndexFragmentationCol) As Boolean
    If Me.Count <> vIndexFragmentationsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vIndexFragmentationsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    If pFilledFromSumOnTheFly Then pIndexFragmentations._FilledFromSumOnTheFly = True
    
    For Each pIndexFragmentation As csIndexFragmentation In Me 
      Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone() 
      pIndexFragmentations.Add(pIndexFragmentationClone) 
      If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
    Next 
    Return pIndexFragmentations 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csIndexFragmentationCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    If pFilledFromSumOnTheFly Then pIndexFragmentations._FilledFromSumOnTheFly = True
    
    For Each pIndexFragmentation As csIndexFragmentation In Me
      Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
      pIndexFragmentations.Add(pIndexFragmentationClone)
      If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
    Next
    Return pIndexFragmentations
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by TableName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTableName(ByVal vTableNameFrom As String, ByVal vTableNameTo As String) As csIndexFragmentationCol 
    Dim pIndexFragmentations As New csIndexFragmentationCol()  
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pIndexFragmentation As csIndexFragmentation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pIndexFragmentation.TableName > vTableNameFrom AndAlso pIndexFragmentation.TableName <= vTableNameTo) Then 
        Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone() 
        pIndexFragmentations.Add(pIndexFragmentationClone) 
        If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
      End If 
    Next 
    Return pIndexFragmentations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csIndexFragmentationCol 
    Dim pIndexFragmentations As New csIndexFragmentationCol()  
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pIndexFragmentation As csIndexFragmentation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pIndexFragmentation.ID > vIDFrom AndAlso pIndexFragmentation.ID <= vIDTo) Then 
        Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone() 
        pIndexFragmentations.Add(pIndexFragmentationClone) 
        If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
      End If 
    Next 
    Return pIndexFragmentations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardTableName(ByVal vTableName As String, ByVal vTableNameWildcardType As clsEnums.enmWildCardType) As csIndexFragmentationCol 
    Dim pIndexFragmentations As New csIndexFragmentationCol 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pIndexFragmentation As csIndexFragmentation In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vTableNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pIndexFragmentation.TableName.StartsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pIndexFragmentation.TableName.EndsWith(vTableName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pIndexFragmentation.TableName.IndexOf(vTableName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vTableNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vTableName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pIndexFragmentation.TableName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone() 
      pIndexFragmentations.Add(pIndexFragmentationClone) 
    Next 
    Return pIndexFragmentations 
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
  Public Function FindByTableName(ByVal vTableName As String) As csIndexFragmentation
    If Me.Count = 0 Then Return New csIndexFragmentation 
    
    If _RecreateDictionaryForFindByTableName = True Then LoadTableNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csIndexFragmentation) = _SortedDictionaryForFindByTableName 
    
    Dim pIndexFragmentation As csIndexFragmentation = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vTableName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pIndexFragmentation) 
    If pIndexFragmentation IsNot Nothing Then Return pIndexFragmentation Else Return New csIndexFragmentation() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByID(ByVal vID As Long) As csIndexFragmentation
    If Me.Count = 0 Then Return New csIndexFragmentation 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
    
    Dim pIndexFragmentation As csIndexFragmentation = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pIndexFragmentation) 
    If pIndexFragmentation IsNot Nothing Then Return pIndexFragmentation Else Return New csIndexFragmentation() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TableName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTableName(ByVal vTableName As String) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTableName = vTableName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.TableName.ToLowerInvariant() = vTableName Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTableName with vTableName of {vTableName}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.TableName.ToLowerInvariant() = vTableName Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IndexName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIndexName(ByVal vIndexName As String) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vIndexName = vIndexName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.IndexName.ToLowerInvariant() = vIndexName Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIndexName with vIndexName of {vIndexName}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.IndexName.ToLowerInvariant() = vIndexName Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IndexType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIndexType(ByVal vIndexType As String) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vIndexType = vIndexType.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.IndexType.ToLowerInvariant() = vIndexType Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIndexType with vIndexType of {vIndexType}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.IndexType.ToLowerInvariant() = vIndexType Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FragmentationPct
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFragmentationPct(ByVal vFragmentationPct As Decimal) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.FragmentationPct = vFragmentationPct Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFragmentationPct with vFragmentationPct of {vFragmentationPct}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.FragmentationPct = vFragmentationPct Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PageCount
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPageCount(ByVal vPageCount As Integer) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.PageCount = vPageCount Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPageCount with vPageCount of {vPageCount}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.PageCount = vPageCount Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csIndexFragmentationCol
    Dim pIndexFragmentations As New csIndexFragmentationCol() 
    pIndexFragmentations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csIndexFragmentation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pIndexFragmentation As csIndexFragmentation In pTempDist.Values
        If pIndexFragmentation.Tag.ToLowerInvariant() = vTag Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csIndexFragmentationCol = Me.Clone() 
      For Each pIndexFragmentation As csIndexFragmentation In pList 
        If pIndexFragmentation.Tag.ToLowerInvariant() = vTag Then
          Dim pIndexFragmentationClone As csIndexFragmentation = pIndexFragmentation.Clone()
          pIndexFragmentations.Add(pIndexFragmentationClone)
          If Not _FilledFromSumOnTheFly Then pIndexFragmentations._Clean.Add(pIndexFragmentation.ID) 
        End If
      Next
    End If 
    
    Return pIndexFragmentations
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
    For Each pIndexFragmentation As csIndexFragmentation In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pIndexFragmentation.LoadDataRow(pRow, vRequester) 
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
    Me.Sort(New csIndexFragmentationCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
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
    Me.Sort(New csIndexFragmentationCol.CompareByTableName)
  End Sub
  Private Class CompareByTableName
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIndexName()
    Me.Sort(New csIndexFragmentationCol.CompareByIndexName)
  End Sub
  Private Class CompareByIndexName
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IndexName, y.IndexName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIndexType()
    Me.Sort(New csIndexFragmentationCol.CompareByIndexType)
  End Sub
  Private Class CompareByIndexType
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IndexType, y.IndexType, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFragmentationPct()
    Me.Sort(New csIndexFragmentationCol.CompareByFragmentationPct)
  End Sub
  Private Class CompareByFragmentationPct
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.FragmentationPct < y.FragmentationPct Then
        Return -1
      ElseIf x.FragmentationPct = y.FragmentationPct Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPageCount()
    Me.Sort(New csIndexFragmentationCol.CompareByPageCount)
  End Sub
  Private Class CompareByPageCount
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.PageCount < y.PageCount Then
        Return -1
      ElseIf x.PageCount = y.PageCount Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csIndexFragmentationCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csIndexFragmentation)
    Private Function Compare(ByVal x As csIndexFragmentation, ByVal y As csIndexFragmentation) As Integer Implements System.Collections.Generic.IComparer(Of csIndexFragmentation).Compare
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
  
    Dim pIndexFragmentation As csIndexFragmentation
  
    While vReader.Read()
      pIndexFragmentation = New csIndexFragmentation() 
      pFault = pIndexFragmentation.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pIndexFragmentation)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pIndexFragmentation.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedIndexFragmentationCol As csIndexFragmentationCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pIndexFragmentation As csIndexFragmentation 
 
      For Each pCachedIndexFragmentation As csIndexFragmentation In vCachedIndexFragmentationCol 
        pIndexFragmentation = New csIndexFragmentation(pCachedIndexFragmentation) 
        pIndexFragmentation.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pIndexFragmentation) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pIndexFragmentation.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-IndexFragmentation-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByTableName = New Dictionary(Of String, csIndexFragmentation)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByTableName = False 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csIndexFragmentation) 
    _RecreateDictionaryForFindByID = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByTableName = New Dictionary(Of String, csIndexFragmentation)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csIndexFragmentation) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = True 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
