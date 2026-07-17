Public Class csLookup
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
    [ParentLookupType] 
    [LookupType] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [ParentLookupType] 
    [ParentCode] 
    [LookupType] 
    [Code] 
    [Text] 
    [TextLocalized] 
    [Description] 
    [DescriptionLocalized] 
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
  Private _ParentLookupType As clsEnums.enmLookup
  Private _ParentLookupTypeText As String 
  Private _ParentCode As String
  Private _LookupType As clsEnums.enmLookup
  Private _LookupTypeText As String 
  Private _Code As String
  Private _Text As String
  Private _TextLocalized As String 
  Private _Description As String
  Private _DescriptionLocalized As String 
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
  Public Property [ParentLookupType]() As clsEnums.enmLookup
    Get
      Return Me._ParentLookupType
    End Get
    Set(ByVal value As clsEnums.enmLookup)
      If Me._ParentLookupType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ParentLookupType = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [ParentLookupTypeText]() As String
    Get
      Return Me._ParentLookupTypeText
    End Get
    Set(ByVal value As String)
      Me._ParentLookupTypeText = value
    End Set
  End Property
  Public Property [ParentCode]() As String
    Get
      Return Me._ParentCode
    End Get
    Set(ByVal value As String)
      If Me._ParentCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ParentCode = value 
      End If 
    End Set
  End Property
  Public Property [LookupType]() As clsEnums.enmLookup
    Get
      Return Me._LookupType
    End Get
    Set(ByVal value As clsEnums.enmLookup)
      If Me._LookupType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LookupType = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [LookupTypeText]() As String
    Get
      Return Me._LookupTypeText
    End Get
    Set(ByVal value As String)
      Me._LookupTypeText = value
    End Set
  End Property
  Public Property [Code]() As String
    Get
      Return Me._Code
    End Get
    Set(ByVal value As String)
      If Me._Code <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Code = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [Text]() As String
    Get
      Return Me._Text
    End Get
    Set(ByVal value As String)
      If Me._Text <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Text = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [TextLocalized]() As String
    Get
      Return Me._TextLocalized
    End Get
    Set(ByVal value As String)
      If Me._TextLocalized <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._TextLocalized = value 
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
  Public Property [DescriptionLocalized]() As String
    Get
      Return Me._DescriptionLocalized
    End Get
    Set(ByVal value As String)
      If Me._DescriptionLocalized <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._DescriptionLocalized = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = ccHelper.CreateFriendlyTextFromHungarianNotation(_LookupType.FastToString() & " --> " & _Code & " (" & _Text & ")") Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _ParentLookupType <> clsEnums.enmLookup.UD Then pValue.Append("ParentLookupType='" & _ParentLookupType.FastToString() & "' ‡ ") 
    If _ParentLookupTypeText <> "" Then pValue.Append("ParentLookupTypeText='" & _ParentLookupTypeText & "' ‡ ") 
    If _ParentCode <> "" Then pValue.Append("ParentCode='" & _ParentCode & "' ‡ ") 
    If _LookupType <> clsEnums.enmLookup.UD Then pValue.Append("LookupType='" & _LookupType.FastToString() & "' ‡ ") 
    If _LookupTypeText <> "" Then pValue.Append("LookupTypeText='" & _LookupTypeText & "' ‡ ") 
    If _Code <> "" Then pValue.Append("Code='" & _Code & "' ‡ ") 
    If _Text <> "" Then pValue.Append("Text='" & _Text & "' ‡ ") 
    If _TextLocalized <> "" Then pValue.Append("TextLocalized='" & _TextLocalized & "' ‡ ") 
    If _Description <> "" Then pValue.Append("Description='" & _Description & "' ‡ ") 
    If _DescriptionLocalized <> "" Then pValue.Append("DescriptionLocalized='" & _DescriptionLocalized & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ParentLookupType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_ParentLookupTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ParentCode)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LookupType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LookupTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Code)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Text)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_TextLocalized)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Description)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DescriptionLocalized)}""") 
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
  
  Public Sub New(ByVal vcsLookup As csLookup)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsLookup) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vParentLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD _ 
    , Optional vParentLookupTypeText As String = "" _ 
    , Optional vParentCode As String = "" _ 
    , Optional vLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD _ 
    , Optional vLookupTypeText As String = "" _ 
    , Optional vCode As String = "" _ 
    , Optional vText As String = "" _ 
    , Optional vTextLocalized As String = "" _ 
    , Optional vDescription As String = "" _ 
    , Optional vDescriptionLocalized As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vIsLocalized As Boolean = False _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _ParentLookupType = vParentLookupType 
    _ParentLookupTypeText = vParentLookupTypeText 
    _ParentCode = vParentCode 
    _LookupType = vLookupType 
    _LookupTypeText = vLookupTypeText 
    _Code = vCode 
    _Text = vText 
    _TextLocalized = vTextLocalized 
    _Description = vDescription 
    _DescriptionLocalized = vDescriptionLocalized 
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
 
    _ParentCode = _ParentCode.Truncate(pTruncateLength, _IsTruncated) 
    _Code = _Code.Truncate(pTruncateLength, _IsTruncated) 
    _Text = _Text.Truncate(pTruncateLength, _IsTruncated) 
    _Description = _Description.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _ParentCode = ccHelper.RemoveChrW0(_ParentCode) 
    _Code = ccHelper.RemoveChrW0(_Code) 
    _Text = ccHelper.RemoveChrW0(_Text) 
    _Description = ccHelper.RemoveChrW0(_Description) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Lookup by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookup_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Lookup-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [ParentLookupTypeAndParentCodeAndLookupTypeAndCode] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the Lookup by the chosen parameters. This function may be a bit slower than accessing the Lookup's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookup_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.ParentLookupTypeAndParentCodeAndLookupTypeAndCode 
          pFault = GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode(clsEnums.TranslateEnmLookup(CStr(vParameters(0))), CStr(vParameters(1)), clsEnums.TranslateEnmLookup(CStr(vParameters(2))), CStr(vParameters(3)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Lookup-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Lookup-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the Lookup by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookup_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"Lookup not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-Lookup-210927-1527", vRequester, vAdditionalMessageToUser:=$"Lookup not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccLookupCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Lookup not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-Lookup-210625-0950", vRequester, vAdditionalMessageToUser:=$"Lookup not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the Lookup by ParentLookupTypeAndParentCodeAndLookupTypeAndCode.
  ''' </summary>
  ''' <param name="vParentLookupType"></param>
  ''' <param name="vParentCode"></param>
  ''' <param name="vLookupType"></param>
  ''' <param name="vCode"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vCode As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCode={1}, LookupType={2}, Code={3}", vParentLookupType, vParentCode, vLookupType, vCode)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookup_GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccLookupCol.FindByParentLookupTypeAndParentCodeAndLookupTypeAndCode(vParentLookupType, vParentCode, vLookupType, vCode), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupGetByParentLookupType&ParentCode&LookupType&Code" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
        pLastReadVariableName = "Code" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vCode) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"Lookup not found for GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode. See FunctionParameters for values", pFunctionParameters, "TRGT-Lookup-210625-0950", vRequester, vAdditionalMessageToUser:=$"Lookup not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupUpdate, "csLookup_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Lookup-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupUpdate, "csLookup_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-Lookup-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the Lookup. If there are parents or children in the Lookup, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupUpdate, "csLookup_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pLookup As New csLookup(_IsLocalized) 
    If Me.isEqual(pLookup) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-Lookup-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-Lookup-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_LookupUpdate"
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
      Dim pLookupOrig As New csLookup(vIsLocalized:=True) 
      If _ID > 0 Then 
        pFault = pLookupOrig.GetByID(_ID, vRequester, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
      End If 
      Dim pDoTranslate As Boolean = False 
      If _Text = _Code Then 
        _Text = "" 
      End If 
      If Not (String.IsNullOrEmpty(_TextLocalized)) Then 
        pDoTranslate = True 
        If _TextLocalized = _Text Then pDoTranslate = False 
        If _TextLocalized = pLookupOrig.Text Then pDoTranslate = False 
        If pDoTranslate = False Then _TextLocalized = "" 
      End If 
      If _Text = _Code Then 
        _Text = "" 
      End If 
      If Not (String.IsNullOrEmpty(_DescriptionLocalized)) Then 
        pDoTranslate = True 
        If _DescriptionLocalized = _Description Then pDoTranslate = False 
        If _DescriptionLocalized = pLookupOrig.Description Then pDoTranslate = False 
        If pDoTranslate = False Then _DescriptionLocalized = "" 
      End If 
    End If 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pCachedLookup As csLookup 
      If _ID = 0 Then 
        pCachedLookup = New csLookup(_IsLocalized) 
        'get last ID 
        Dim pLookupCol As csLookupCol = MyController.DBCache.ccLookupCol.Clone() 
        If pLookupCol.Count = 0 Then 
          _ID = 1 
        Else 
          pLookupCol.SortByID() 
          Dim pLastID As Long = pLookupCol(pLookupCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccLookupCol.Add(pCachedLookup) 
      Else  
        pCachedLookup = MyController.DBCache.ccLookupCol.FindByID(_ID) 
      End If 
      pCachedLookup.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLookupCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_ParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_ParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_LookupType.FastToString()) 
        pLastReadVariableName = "Code" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_Code) 
        pLastReadVariableName = "locText" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 100).Value = ccHelper.ObjectNullable(_Text) 
        pLastReadVariableName = "locDescription" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 50).Value = ccHelper.ObjectNullable(_Description) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If _IsLocalized = True Then 
      'Now save the Localized version 
      'Text 
      pFault = ccHelper.UpdateTranslation(clsEnums.enmObjectType.TableData, "c_Lookup", "Text", pID, _TextLocalized, _LocalizedLanguage, vRequester) 
      If pFault.isOK = False Then Return pFault 
      'Description 
      pFault = ccHelper.UpdateTranslation(clsEnums.enmObjectType.TableData, "c_Lookup", "Description", pID, _DescriptionLocalized, _LocalizedLanguage, vRequester) 
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
    Dim pFunctionParameters As String = String.Format("Lookup.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookup_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_LookupDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      MyController.DBCache.ccLookupCol.Remove(MyController.DBCache.ccLookupCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLookupCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090623-1813", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
          
    'Now delete translations 
    pFault = ccHelper.DeleteTranslationsForTableDataRow(clsEnums.enmObjectType.TableData, "Lookup", _ID, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookup_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_LookupDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      MyController.DBCache.ccLookupCol.Remove(MyController.DBCache.ccLookupCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccLookupCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    'Now delete translations 
    pFault = ccHelper.DeleteTranslationsForTableDataRow(clsEnums.enmObjectType.TableData, "Lookup", vID, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csLookup) Then Return False 
    Dim pLookupToTest As csLookup = CType(vTargCCEntityToTest, csLookup) 
    Return isEqual(pLookupToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vLookupToTest As csLookup) As Boolean
    With vLookupToTest
      If _ID <> .ID Then Return False
      If _ParentLookupType <> .ParentLookupType Then Return False
      If _ParentCode <> .ParentCode Then Return False
      If _LookupType <> .LookupType Then Return False
      If _Code <> .Code Then Return False
      If _Text <> .Text Then Return False
      If _TextLocalized <> .TextLocalized Then Return False
      If _Description <> .Description Then Return False
      If _DescriptionLocalized <> .DescriptionLocalized Then Return False
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
    Dim pClone As New csLookup(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLookup
    Dim pClone As New csLookup(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("ParentLookupType") = _ParentLookupType : Catch ex As Exception : Return pFault.LogException(ex, "ParentLookupType", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("ParentCode") = _ParentCode : Catch ex As Exception : Return pFault.LogException(ex, "ParentCode", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("LookupType") = _LookupType : Catch ex As Exception : Return pFault.LogException(ex, "LookupType", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("Code") = _Code : Catch ex As Exception : Return pFault.LogException(ex, "Code", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("Text") = _Text : Catch ex As Exception : Return pFault.LogException(ex, "Text", "TRGT-Lookup-130316-0852", vRequester) : End Try 
    Try : vDataRow("Description") = _Description : Catch ex As Exception : Return pFault.LogException(ex, "Description", "TRGT-Lookup-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pLookup As csLookup = CType(pXmlSerializer.Deserialize(pStreamReader), csLookup) 
      AssignValues(pLookup) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-Lookup-130515-1230", vRequester) 
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
          'ParentLookupType 
          pBinaryWriter.Write(_ParentLookupType.FastToString()) 
          'ParentCode 
          If _ParentCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ParentCode) 
          'LookupType 
          pBinaryWriter.Write(_LookupType.FastToString()) 
          'Code 
          If _Code Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Code) 
          'Text 
          If _Text Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Text) 
          pBinaryWriter.Write(_TextLocalized) 
          'Description 
          If _Description Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Description) 
          pBinaryWriter.Write(_DescriptionLocalized) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-150307-2338", vRequester) 
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
          'ParentLookupType 
          _ParentLookupType = clsEnums.TranslateEnmLookup(pReader.ReadString) 
          'ParentCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ParentCode = pReader.ReadString 
          'LookupType 
          _LookupType = clsEnums.TranslateEnmLookup(pReader.ReadString) 
          'Code 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Code = pReader.ReadString 
          'Text 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Text = pReader.ReadString 
          'Localizable 
          _TextLocalized = pReader.ReadString 
          'Description 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Description = pReader.ReadString 
          'Localizable 
          _DescriptionLocalized = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-Lookup-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-190720-1443", vRequester) 
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
 
      Dim pLookup As csLookup = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csLookup)(vJSON, pSettings) 
      AssignValues(pLookup) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vLookup As csLookup)
    With vLookup
      _ID = .ID 
      _ParentLookupType = .ParentLookupType 
      _ParentLookupTypeText = .ParentLookupTypeText
      _ParentCode = .ParentCode 
      _LookupType = .LookupType 
      _LookupTypeText = .LookupTypeText
      _Code = .Code 
      _Text = .Text 
      _TextLocalized = .TextLocalized
      _Description = .Description 
      _DescriptionLocalized = .DescriptionLocalized
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
      'ParentLookupType 
      pTextToGet = "ParentLookupTypeText (Enum)" 
      _ParentLookupTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Lookup, _ParentLookupType.FastToString(), vRequester) 
      'LookupType 
      pTextToGet = "LookupTypeText (Enum)" 
      _LookupTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Lookup, _LookupType.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-Lookup-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Friend Function LoadTranslations(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters = String.Format("Item ID = {0}, UILang = {1}", _ID.ToString, vRequester.UILang.ToString) 
    Dim pFault As New clsFault 
 
    Try 
      _TextLocalized = ccHelper.GetLocalizedTableData("c_Lookup", "Text", _ID, vRequester, _LocalizedLanguage) 
      If _Text = "" Then _Text = _Code 
      If _TextLocalized = "" Then _TextLocalized = _Text 
      _DescriptionLocalized = ccHelper.GetLocalizedTableData("c_Lookup", "Description", _ID, vRequester, _LocalizedLanguage) 
      If _Text = "" Then _Text = _Code 
      If _DescriptionLocalized = "" Then _DescriptionLocalized = _Description 
      _IsLocalized = True 
      If _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
        _LocalizedLanguage = vRequester.UILang 
      End If 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-130216-0956", vRequester) 
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
      pLastReadVariableName = "enmParentLookupType_Lookup" 
      If Not vReader.IsDBNull(1) Then _ParentLookupType = clsEnums.TranslateEnmLookup(vReader.GetString(1))
      pLastReadVariableName = "ParentCode" 
      If Not vReader.IsDBNull(2) Then _ParentCode = vReader.GetString(2) 
      pLastReadVariableName = "enmLookupType_Lookup" 
      If Not vReader.IsDBNull(3) Then _LookupType = clsEnums.TranslateEnmLookup(vReader.GetString(3))
      pLastReadVariableName = "Code" 
      If Not vReader.IsDBNull(4) Then _Code = vReader.GetString(4) 
      pLastReadVariableName = "locText" 
      If Not vReader.IsDBNull(5) Then _Text = vReader.GetString(5) 
      pLastReadVariableName = "locDescription" 
      If Not vReader.IsDBNull(6) Then _Description = vReader.GetString(6) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLookup As csLookup, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pIsLocalized As Boolean = _IsLocalized 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedLookup) 
      If pIsLocalized = True Then 
        pFault = LoadTranslations(vRequester) 
        If pFault.isOK = False Then Return pFault 
      End If 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _ParentLookupType = clsEnums.enmLookup.UD
    _ParentLookupTypeText = ""
    _ParentCode = ""
    _LookupType = clsEnums.enmLookup.UD
    _LookupTypeText = ""
    _Code = ""
    _Text = ""
    _TextLocalized = ""
    _Description = ""
    _DescriptionLocalized = ""
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
  
Public Class csLookupCol
  Inherits cTargCCCollection(Of csLookup)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csLookup) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode As Dictionary(Of String, csLookup) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode As Boolean 
  Private Function CreateKeyForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vLookup As csLookup) As String 
    With vLookup 
      Return .ParentLookupType.ToString() & "|" & .ParentCode & "|" & .LookupType.ToString() & "|" & .Code
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
 
    For Each pRow As csLookup In Me 
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
    pCSVTitle.Append(",""ParentLookupType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""ParentLookupType (Text)""") 
    pCSVTitle.Append(",""ParentCode""") 
    pCSVTitle.Append(",""LookupType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""LookupType (Text)""") 
    pCSVTitle.Append(",""Code""") 
    pCSVTitle.Append(",""Text""") 
    pCSVTitle.Append(",""TextLocalized""") 
    pCSVTitle.Append(",""Description""") 
    pCSVTitle.Append(",""DescriptionLocalized""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csLookup In Me 
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
 
  Public Overloads Sub Add(ByVal vLookup As csLookup) 
    SyncLock _CollectionLock 
      MyBase.Add(vLookup) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vLookup As csLookup) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vLookup) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vLookupCol As csLookupCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vLookupCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vLookup As csLookup) 
    SyncLock _CollectionLock 
      MyBase.Remove(vLookup) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csLookup) 
      
      For Each lLookup In Me 
        If lLookup.IsEmpty OrElse pTempDictionary.ContainsKey(lLookup.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lLookup.ID, lLookup) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLookup.ToString, "TRGT-Lookup-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", Lookup:" & lLookup.ToString() & ", TRGT-Lookup-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadParentLookupTypeAndParentCodeAndLookupTypeAndCodes() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode' yet!
      Dim pTempDictionary As New Dictionary(Of String, csLookup)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lLookup In Me 
        Try 
          Dim pParentLookupTypeAndParentCodeAndLookupTypeAndCode As String = CreateKeyForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode(lLookup) 
          If String.IsNullOrEmpty(pParentLookupTypeAndParentCodeAndLookupTypeAndCode.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pParentLookupTypeAndParentCodeAndLookupTypeAndCode)) Then 
            pTempDictionary.Add(pParentLookupTypeAndParentCodeAndLookupTypeAndCode, lLookup) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lLookup.ToString, "TRGT-Lookup-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode:" & ex.Message & ", Lookup:" & lLookup.ToString() & ", TRGT-Lookup-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = False
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
 
    For Each lLookup As csLookup In Me 
      lLookup.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lLookup As csLookup In Me 
      lLookup.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [LookupType] 
    [ParentLookupTypeAndLookupType] 
    [ParentLookupTypeAndParentCodeAndLookupType] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Lookups by the chosen parameters. This function may be a bit slower than accessing the Lookup's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.LookupType 
          pFault = FillByLookupType(clsEnums.TranslateEnmLookup(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ParentLookupTypeAndLookupType 
          pFault = FillByParentLookupTypeAndLookupType(clsEnums.TranslateEnmLookup(CStr(vParameters(0))), clsEnums.TranslateEnmLookup(CStr(vParameters(1))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ParentLookupTypeAndParentCodeAndLookupType 
          pFault = FillByParentLookupTypeAndParentCodeAndLookupType(clsEnums.TranslateEnmLookup(CStr(vParameters(0))), CStr(vParameters(1)), clsEnums.TranslateEnmLookup(CStr(vParameters(2))), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-Lookup-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-Lookup-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.Clone() 
      pLookupsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLookupsCached.Reverse() 
      If vHowMany > 0 AndAlso pLookupsCached.Count > vHowMany Then 
        Dim tmp As New csLookupCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLookupsCached(i)) 
        Next 
        pLookupsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary> 
  ''' Gets a collection of all the items EXCEPT 'UserIdentityTypeName', or a sub-collection defined by HowMany and Direction 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param> 
  ''' <param name="vDir">ASCendined or DESCending</param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function FillForLookupCache(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillForLookupCache", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.Clone() 
      pLookupsCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLookupsCached.Reverse() 
      If vHowMany > 0 AndAlso pLookupsCached.Count > vHowMany Then 
        Dim tmp As New csLookupCol 
        For i = 0 To vHowMany - 1 
          If pLookupsCached(i).LookupType = clsEnums.enmLookup.UserIdentityTypeName Then Continue For 
          tmp.Add(pLookupsCached(i)) 
        Next 
        pLookupsCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillForLookupCache" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "" 
 
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
 
        'Execute query  
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-220517-1109", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If 
 
    RaiseEvent evtAfterFill() 
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection of all the items for a specific LookupType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLookupType(ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LookupType={0}", vLookupType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByLookupType(vLookupType)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByLookupType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ParentLookupTypeAndLookupType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByParentLookupTypeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, LookupType={1}", vParentLookupType, vLookupType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByParentLookupTypeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByParentLookupTypeAndLookupType(vParentLookupType, vLookupType)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByParentLookupType&LookupType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType.FastToString()) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCode={1}, LookupType={2}", vParentLookupType, vParentCode, vLookupType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByParentLookupTypeAndParentCodeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByParentLookupTypeAndParentCodeAndLookupType(vParentLookupType, vParentCode, vLookupType)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByParentLookupType&ParentCode&LookupType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCodeFrom={1}, ParentCodeTo={2}, LookupType={3}", vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByBoundedParentLookupTypeAndParentCodeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByBoundedParentLookupTypeAndParentCodeAndLookupType(vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByBoundedParentLookupType&ParentCode&LookupType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCodeFrom" 
        pDALParameters.Add("bndParentCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeFrom) 
        pLastReadVariableName = "ParentCodeTo" 
        pDALParameters.Add("bndParentCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeTo) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupTypeAndCode, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vCodeFrom As String, ByVal vCodeTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCodeFrom={1}, ParentCodeTo={2}, LookupType={3}, CodeFrom={4}, CodeTo={5}", vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType, vCodeFrom, vCodeTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccLookupCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccLookupCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csLookupCol failed: " & pResponse) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.CloneByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode(vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType, vCodeFrom, vCodeTo)
      pFault = LoadMeFromDBCache(pLookupsCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillByBoundedParentLookupType&ParentCode&LookupType&Code" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCodeFrom" 
        pDALParameters.Add("bndParentCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeFrom) 
        pLastReadVariableName = "ParentCodeTo" 
        pDALParameters.Add("bndParentCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeTo) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType.FastToString()) 
        pLastReadVariableName = "CodeFrom" 
        pDALParameters.Add("bndCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCodeFrom) 
        pLastReadVariableName = "CodeTo" 
        pDALParameters.Add("bndCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCodeTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lLookup As New csLookup() 
      pFault = lLookup.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lLookup.IsEmpty Then Me.Add(lLookup) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pLookups As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pLookups, "csLookupCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pLookups IsNot Nothing AndAlso Me.Count <> pLookups.Count Then FillFromListOfITargCCEntity(pLookups) 
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
    [ParentLookupType]
    [ParentCode]
    ParentCodeWildcardType
    [LookupType]
    [Code]
    CodeWildcardType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pParentLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD
    Dim pParentCode As String = Nothing
    Dim pParentCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD
    Dim pCode As String = Nothing
    Dim pCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentLookupType) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentLookupType) : If pObj IsNot Nothing Then pParentLookupType = CType(pObj, clsEnums.enmLookup) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentCode) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentCode) : If pObj IsNot Nothing Then pParentCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentCodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentCodeWildcardType) : If pObj IsNot Nothing Then pParentCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LookupType) Then pObj = vParameters(enmFillOnTheFlyParameters.LookupType) : If pObj IsNot Nothing Then pLookupType = CType(pObj, clsEnums.enmLookup) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Code) Then pObj = vParameters(enmFillOnTheFlyParameters.Code) : If pObj IsNot Nothing Then pCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CodeWildcardType) : If pObj IsNot Nothing Then pCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pParentLookupType _
        , pParentCode, pParentCodeWildcardType _
        , pLookupType _
        , pCode, pCodeWildcardType _
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
        , ByVal vParentLookupType As clsEnums.enmLookup _
        , ByVal vParentCode As String, ByVal vParentCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vLookupType As clsEnums.enmLookup _
        , ByVal vCode As String, ByVal vCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ParentLookupType={2}, ParentCode={3}, ParentCodeWildcardType={4}, LookupType={5}, Code={6}, CodeWildcardType={7}", vIDFrom, vIDTo, vParentLookupType, vParentCode, vParentCodeWildcardType.FastToString(), vLookupType, vCode, vCodeWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ParentCode 
    Dim pWCParentCode As String = "" 
    If vParentCode = Nothing Then 
      pWCParentCode = vParentCode
    Else 
      If vParentCodeWildcardType = clsEnums.enmWildCardType.None OrElse vParentCodeWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCParentCode = vParentCode
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.After Then 
        pWCParentCode = vParentCode & "%" 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCParentCode = "%" & vParentCode 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCParentCode = "%" & vParentCode & "%" 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vParentCode.ToCharArray 
          pWCParentCode &= p & "%" 
        Next 
        pWCParentCode = "%" & pWCParentCode 
      End If 
    End If 
    'Code 
    Dim pWCCode As String = "" 
    If vCode = Nothing Then 
      pWCCode = vCode
    Else 
      If vCodeWildcardType = clsEnums.enmWildCardType.None OrElse vCodeWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCCode = vCode
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.After Then 
        pWCCode = vCode & "%" 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCCode = "%" & vCode 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCCode = "%" & vCode & "%" 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vCode.ToCharArray 
          pWCCode &= p & "%" 
        Next 
        pWCCode = "%" & pWCCode 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Lookup-121122-2008", vRequester) 
      Dim pLookupsCached As csLookupCol = MyController.DBCache.ccLookupCol.Clone() 
      Dim pLookupsToUse As New csLookupCol() 
      For Each l In pLookupsCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vParentLookupType <> clsEnums.enmLookup.UD Then 
          If l.ParentLookupType <> vParentLookupType Then Continue For 
        End If 
        If Not String.IsNullOrEmpty(vParentCode) Then 
          If vParentCodeWildcardType = clsEnums.enmWildCardType.UD OrElse vParentCodeWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.ParentCode.Equals(vParentCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.ParentCode.StartsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.ParentCode.EndsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.ParentCode.IndexOf(vParentCode, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If vLookupType <> clsEnums.enmLookup.UD Then 
          If l.LookupType <> vLookupType Then Continue For 
        End If 
        If Not String.IsNullOrEmpty(vCode) Then 
          If vCodeWildcardType = clsEnums.enmWildCardType.UD OrElse vCodeWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Code.Equals(vCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCodeWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Code.StartsWith(vCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCodeWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Code.EndsWith(vCode, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Code.IndexOf(vCode, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        pLookupsToUse.Add(l) 
      Next 
      pLookupsToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pLookupsToUse.Reverse() 
      If vHowMany > 0 AndAlso pLookupsToUse.Count > vHowMany Then 
        Dim tmp As New csLookupCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pLookupsToUse(i)) 
        Next 
        pLookupsToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pLookupsToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vParentLookupType.FastToString()) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add("wldParentCode", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vLookupType.FastToString()) 
        pLastReadVariableName = "Code" 
        pDALParameters.Add("wldCode", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCCode) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByParentLookupType
    GroupByParentCode
    GroupByLookupType
    GroupByCode
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pParentLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD
    Dim pParentCode As String = Nothing
    Dim pParentCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLookupType As clsEnums.enmLookup = clsEnums.enmLookup.UD
    Dim pCode As String = Nothing
    Dim pCodeWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByParentLookupType As Boolean = False
    Dim pGroupByParentCode As Boolean = False
    Dim pGroupByLookupType As Boolean = False
    Dim pGroupByCode As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentLookupType) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentLookupType) : If pObj IsNot Nothing Then pParentLookupType = CType(pObj, clsEnums.enmLookup) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentCode) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentCode) : If pObj IsNot Nothing Then pParentCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ParentCodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ParentCodeWildcardType) : If pObj IsNot Nothing Then pParentCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LookupType) Then pObj = vParameters(enmFillOnTheFlyParameters.LookupType) : If pObj IsNot Nothing Then pLookupType = CType(pObj, clsEnums.enmLookup) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Code) Then pObj = vParameters(enmFillOnTheFlyParameters.Code) : If pObj IsNot Nothing Then pCode = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CodeWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CodeWildcardType) : If pObj IsNot Nothing Then pCodeWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByParentLookupType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByParentLookupType) : If pObj IsNot Nothing Then pGroupByParentLookupType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByParentCode) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByParentCode) : If pObj IsNot Nothing Then pGroupByParentCode = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLookupType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLookupType) : If pObj IsNot Nothing Then pGroupByLookupType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCode) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCode) : If pObj IsNot Nothing Then pGroupByCode = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pParentLookupType _
        , pParentCode, pParentCodeWildcardType _
        , pLookupType _
        , pCode, pCodeWildcardType _
        , pGroupByParentLookupType _
        , pGroupByParentCode _
        , pGroupByLookupType _
        , pGroupByCode _
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
        , ByVal vParentLookupType As clsEnums.enmLookup _
        , ByVal vParentCode As String, ByVal vParentCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vLookupType As clsEnums.enmLookup _
        , ByVal vCode As String, ByVal vCodeWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByParentLookupType As Boolean _
        , ByVal vGroupByParentCode As Boolean _
        , ByVal vGroupByLookupType As Boolean _
        , ByVal vGroupByCode As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ParentLookupType={2}, ParentCode={3}, ParentCodeWildcardType={4}, LookupType={5}, Code={6}, CodeWildcardType={7}, GroupByParentLookupType={8}, GroupByParentCode={9}, GroupByLookupType={10}, GroupByCode={11}", vIDFrom, vIDTo, vParentLookupType, vParentCode, vParentCodeWildcardType.FastToString(), vLookupType, vCode, vCodeWildcardType.FastToString(), vGroupByParentLookupType, vGroupByParentCode, vGroupByLookupType, vGroupByCode)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'ParentCode 
    Dim pWCParentCode As String = "" 
    If vParentCode = Nothing Then 
      pWCParentCode = vParentCode
    ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.None OrElse vParentCodeWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCParentCode = vParentCode
    ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.After Then 
      pWCParentCode = vParentCode & "%" 
    ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCParentCode = "%" & vParentCode 
    ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCParentCode = "%" & vParentCode & "%" 
    ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vParentCode.ToCharArray 
        pWCParentCode &= p & "%" 
      Next 
      pWCParentCode = "%" & pWCParentCode 
    End If 
    'Code 
    Dim pWCCode As String = "" 
    If vCode = Nothing Then 
      pWCCode = vCode
    ElseIf vCodeWildcardType = clsEnums.enmWildCardType.None OrElse vCodeWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCCode = vCode
    ElseIf vCodeWildcardType = clsEnums.enmWildCardType.After Then 
      pWCCode = vCode & "%" 
    ElseIf vCodeWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCCode = "%" & vCode 
    ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCCode = "%" & vCode & "%" 
    ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vCode.ToCharArray 
        pWCCode &= p & "%" 
      Next 
      pWCCode = "%" & pWCCode 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-Lookup-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_LookupsFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vParentLookupType) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add("wldParentCode", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vLookupType) 
        pLastReadVariableName = "Code" 
        pDALParameters.Add("wldCode", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCCode) 
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add("GroupByenmParentLookupType_Lookup", ccDAL.enmSQLDataType.Bit).Value = vGroupByParentLookupType
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add("GroupByParentCode", ccDAL.enmSQLDataType.Bit).Value = vGroupByParentCode
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add("GroupByenmLookupType_Lookup", ccDAL.enmSQLDataType.Bit).Value = vGroupByLookupType
        pLastReadVariableName = "Code" 
        pDALParameters.Add("GroupByCode", ccDAL.enmSQLDataType.Bit).Value = vGroupByCode
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vLookupArray As csLookup())
    Me.Clear()
    
    For Each pLookup As csLookup In vLookupArray
      Me.Add(pLookup)
      _Clean.Add(pLookup.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pLookup As New csLookup(pRow, vRequester, _IsLocalized) 
        Me.Add(pLookup) 
        _Clean.Add(pLookup.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-LookupCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-130515-1300", vRequester) 
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
      Dim pLookups As csLookupCol = CType(pXmlSerializer.Deserialize(pStreamReader), csLookupCol) 
      For Each pLookup As csLookup In pLookups 
        Me.Add(pLookup) 
        _Clean.Add(pLookup.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-Lookup-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-190720-1443", vRequester) 
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
 
      Dim pLookups As List(Of csLookup) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csLookup))(vJSON, pSettings) 
      For Each pLookup As csLookup In pLookups 
        Me.Add(pLookup) 
        _Clean.Add(pLookup.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-190720-2059", vRequester) 
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
          For Each lLookup As csLookup In Me 
            Dim pByte As Byte() = lLookup.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-150307-2340", vRequester) 
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
            Dim pLookup As csLookup = New csLookup(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pLookup) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pLookup.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-Lookup-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pLookup As csLookup In Me 
      With pLookup 
        pFault = pLookup.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csLookupCol) Then Return False 
    Dim pLookupColToTest As csLookupCol = CType(vEntitiesToTest, csLookupCol) 
    Return isEqual(pLookupColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vLookupsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vLookupsToTest As csLookupCol) As Boolean
    If Me.Count <> vLookupsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vLookupsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLookups As New csLookupCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pLookups._FilledFromSumOnTheFly = True
    
    For Each pLookup As csLookup In Me 
      Dim pLookupClone As csLookup = pLookup.Clone() 
      pLookups.Add(pLookupClone) 
      If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csLookupCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pLookups As New csLookupCol(_IsLocalized) 
    If pFilledFromSumOnTheFly Then pLookups._FilledFromSumOnTheFly = True
    
    For Each pLookup As csLookup In Me
      Dim pLookupClone As csLookup = pLookup.Clone()
      pLookups.Add(pLookupClone)
      If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
    Next
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csLookupCol 
    Dim pLookups As New csLookupCol(_IsLocalized)  
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLookup.ID > vIDFrom AndAlso pLookup.ID <= vIDTo) Then 
        Dim pLookupClone As csLookup = pLookup.Clone() 
        pLookups.Add(pLookupClone) 
        If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ParentLookupType and ParentCode and LookupType (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup) As csLookupCol 
    Dim pLookups As New csLookupCol(_IsLocalized)  
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLookup.ParentLookupType = vParentLookupType) AndAlso (pLookup.ParentCode > vParentCodeFrom AndAlso pLookup.ParentCode <= vParentCodeTo) AndAlso (pLookup.LookupType = vLookupType) Then 
        Dim pLookupClone As csLookup = pLookup.Clone() 
        pLookups.Add(pLookupClone) 
        If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ParentLookupType and ParentCode and LookupType and Code (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vCodeFrom As String, ByVal vCodeTo As String) As csLookupCol 
    Dim pLookups As New csLookupCol(_IsLocalized)  
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList() 
      If (pLookup.ParentLookupType = vParentLookupType) AndAlso (pLookup.ParentCode > vParentCodeFrom AndAlso pLookup.ParentCode <= vParentCodeTo) AndAlso (pLookup.LookupType = vLookupType) AndAlso (pLookup.Code > vCodeFrom AndAlso pLookup.Code <= vCodeTo) Then 
        Dim pLookupClone As csLookup = pLookup.Clone() 
        pLookups.Add(pLookupClone) 
        If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
      End If 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentLookupTypeWildcardType As clsEnums.enmWildCardType, ByVal vParentCode As String, ByVal vParentCodeWildcardType As clsEnums.enmWildCardType, ByVal vLookupType As clsEnums.enmLookup, ByVal vLookupTypeWildcardType As clsEnums.enmWildCardType) As csLookupCol 
    Dim pLookups As New csLookupCol 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vParentCodeWildcardType = clsEnums.enmWildCardType.After Then 
        If pLookup.ParentCode.StartsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLookup.ParentCode.EndsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLookup.ParentCode.IndexOf(vParentCode, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vParentCode.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLookup.ParentCode.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLookupClone As csLookup = pLookup.Clone() 
      pLookups.Add(pLookupClone) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentLookupTypeWildcardType As clsEnums.enmWildCardType, ByVal vParentCode As String, ByVal vParentCodeWildcardType As clsEnums.enmWildCardType, ByVal vLookupType As clsEnums.enmLookup, ByVal vLookupTypeWildcardType As clsEnums.enmWildCardType, ByVal vCode As String, ByVal vCodeWildcardType As clsEnums.enmWildCardType) As csLookupCol 
    Dim pLookups As New csLookupCol 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vParentCodeWildcardType = clsEnums.enmWildCardType.After Then 
        If pLookup.ParentCode.StartsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLookup.ParentCode.EndsWith(vParentCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLookup.ParentCode.IndexOf(vParentCode, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vParentCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vParentCode.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLookup.ParentCode.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vCodeWildcardType = clsEnums.enmWildCardType.After Then 
        If pLookup.Code.StartsWith(vCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.Before Then 
        If pLookup.Code.EndsWith(vCode, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pLookup.Code.IndexOf(vCode, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vCodeWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vCode.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pLookup.Code.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pLookupClone As csLookup = pLookup.Clone() 
      pLookups.Add(pLookupClone) 
    Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    Return pLookups 
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
  Public Function FindByID(ByVal vID As Long) As csLookup
    If Me.Count = 0 Then Return New csLookup 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
    
    Dim pLookup As csLookup = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pLookup) 
    If pLookup IsNot Nothing Then Return pLookup Else Return New csLookup() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vCode As String) As csLookup
    If Me.Count = 0 Then Return New csLookup 
    
    If _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = True Then LoadParentLookupTypeAndParentCodeAndLookupTypeAndCodes() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csLookup) = _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode 
    
    Dim pLookup As csLookup = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vParentLookupType.ToString() & "|" & vParentCode & "|" & vLookupType.ToString() & "|" & vCode
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pLookup) 
    If pLookup IsNot Nothing Then Return pLookup Else Return New csLookup() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ParentLookupType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByParentLookupType(ByVal vParentLookupType As clsEnums.enmLookup) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.ParentLookupType = vParentLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByParentLookupType with vParentLookupType of {vParentLookupType}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.ParentLookupType = vParentLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ParentCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByParentCode(ByVal vParentCode As String) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vParentCode = vParentCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.ParentCode.ToLowerInvariant() = vParentCode Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByParentCode with vParentCode of {vParentCode}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.ParentCode.ToLowerInvariant() = vParentCode Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LookupType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLookupType(ByVal vLookupType As clsEnums.enmLookup) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLookupType with vLookupType of {vLookupType}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Code
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCode(ByVal vCode As String) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCode = vCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.Code.ToLowerInvariant() = vCode Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCode with vCode of {vCode}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.Code.ToLowerInvariant() = vCode Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Text
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByText(ByVal vText As String) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vText = vText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.Text.ToLowerInvariant() = vText Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByText with vText of {vText}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.Text.ToLowerInvariant() = vText Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Description
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDescription(ByVal vDescription As String) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDescription = vDescription.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.Description.ToLowerInvariant() = vDescription Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDescription with vDescription of {vDescription}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.Description.ToLowerInvariant() = vDescription Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csLookup) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pLookup As csLookup In pTempDist.Values
        If pLookup.Tag.ToLowerInvariant() = vTag Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.Tag.ToLowerInvariant() = vTag Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookups.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
    
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ParentLookupTypeAndLookupType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByParentLookupTypeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vLookupType As clsEnums.enmLookup) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList()
        If pLookup.ParentLookupType = vParentLookupType AndAlso pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.ParentLookupType = vParentLookupType AndAlso pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    Return pLookups
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ParentLookupTypeAndParentCodeAndLookupType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup) As csLookupCol
    Dim pLookups As New csLookupCol(_IsLocalized) 
    pLookups._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pLookup As csLookup In _SortedDictionaryForFindByID.Values.ToList()
        If pLookup.ParentLookupType = vParentLookupType AndAlso pLookup.ParentCode = vParentCode AndAlso pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csLookupCol = Me.Clone() 
      For Each pLookup As csLookup In pList 
        If pLookup.ParentLookupType = vParentLookupType AndAlso pLookup.ParentCode = vParentCode AndAlso pLookup.LookupType = vLookupType Then
          Dim pLookupClone As csLookup = pLookup.Clone()
          pLookups.Add(pLookupClone)
          If Not _FilledFromSumOnTheFly Then pLookups._Clean.Add(pLookup.ID) 
        End If
      Next
    End If 
    Return pLookups
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
    For Each pLookup As csLookup In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pLookup.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupView, "csLookupCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csLookup In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csLookup = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pLookupToKill As New csLookup 
          pLookupToKill.ID = pCleanID 
          pLookupToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pLookupToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csLookup In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-Lookup-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupUpdate, "csLookupCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csLookup In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csLookup In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csLookupCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LookupType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLookupType(ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LookupType={0}", vLookupType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByLookupType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLookups As New csLookupCol() : pAllLookups.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLookups As csLookupCol = pAllLookups.CloneByLookupType(vLookupType) 
      For Each l In pFilteredLookups 
        pAllLookups.Remove(pAllLookups.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLookups, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ParentLookupTypeAndLookupType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByParentLookupTypeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, LookupType={1}", vParentLookupType, vLookupType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByParentLookupTypeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByParentLookupType&LookupType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLookups As New csLookupCol() : pAllLookups.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLookups As csLookupCol = pAllLookups.CloneByParentLookupTypeAndLookupType(vParentLookupType, vLookupType) 
      For Each l In pFilteredLookups 
        pAllLookups.Remove(pAllLookups.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLookups, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCode={1}, LookupType={2}", vParentLookupType, vParentCode, vLookupType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByParentLookupTypeAndParentCodeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByParentLookupType&ParentCode&LookupType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllLookups As New csLookupCol() : pAllLookups.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredLookups As csLookupCol = pAllLookups.CloneByParentLookupTypeAndParentCodeAndLookupType(vParentLookupType, vParentCode, vLookupType) 
      For Each l In pFilteredLookups 
        pAllLookups.Remove(pAllLookups.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllLookups, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType) 
        pLastReadVariableName = "ParentCode" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCode) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Lookup-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedParentLookupTypeAndParentCodeAndLookupType(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCodeFrom={1}, ParentCodeTo={2}, LookupType={3}", vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByBoundedParentLookupTypeAndParentCodeAndLookupType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByBoundedParentLookupType&ParentCode&LookupType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Lookup-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType) 
        pLastReadVariableName = "ParentCodeFrom" 
        pDALParameters.Add("bndParentCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeFrom) 
        pLastReadVariableName = "ParentCodeTo" 
        pDALParameters.Add("bndParentCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeTo) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ParentLookupTypeAndParentCodeAndLookupTypeAndCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCodeFrom As String, ByVal vParentCodeTo As String, ByVal vLookupType As clsEnums.enmLookup, ByVal vCodeFrom As String, ByVal vCodeTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ParentLookupType={0}, ParentCodeFrom={1}, ParentCodeTo={2}, LookupType={3}, CodeFrom={4}, CodeTo={5}", vParentLookupType, vParentCodeFrom, vParentCodeTo, vLookupType, vCodeFrom, vCodeTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_LookupDelete, "csLookupCol_DeleteByBoundedParentLookupTypeAndParentCodeAndLookupTypeAndCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_LookupsDeleteByBoundedParentLookupType&ParentCode&LookupType&Code"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-Lookup-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmParentLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentLookupType) 
        pLastReadVariableName = "ParentCodeFrom" 
        pDALParameters.Add("bndParentCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeFrom) 
        pLastReadVariableName = "ParentCodeTo" 
        pDALParameters.Add("bndParentCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vParentCodeTo) 
        pLastReadVariableName = "enmLookupType_Lookup" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vLookupType) 
        pLastReadVariableName = "CodeFrom" 
        pDALParameters.Add("bndCodeFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCodeFrom) 
        pLastReadVariableName = "CodeTo" 
        pDALParameters.Add("bndCodeTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vCodeTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-Lookup-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-Lookup-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-090210-1341", vRequester) 
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
    Me.Sort(New csLookupCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
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
  
  Public Sub SortByParentLookupType()
    Me.Sort(New csLookupCol.CompareByParentLookupType)
  End Sub
  Private Class CompareByParentLookupType
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ParentLookupType < y.ParentLookupType Then
        Return -1
      ElseIf x.ParentLookupType = y.ParentLookupType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByParentLookupTypeText()
    Me.Sort(New csLookupCol.CompareByParentLookupTypeText)
  End Sub
  Private Class CompareByParentLookupTypeText
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ParentLookupTypeText, y.ParentLookupTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByParentCode()
    Me.Sort(New csLookupCol.CompareByParentCode)
  End Sub
  Private Class CompareByParentCode
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ParentCode, y.ParentCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLookupType()
    Me.Sort(New csLookupCol.CompareByLookupType)
  End Sub
  Private Class CompareByLookupType
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LookupType < y.LookupType Then
        Return -1
      ElseIf x.LookupType = y.LookupType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLookupTypeText()
    Me.Sort(New csLookupCol.CompareByLookupTypeText)
  End Sub
  Private Class CompareByLookupTypeText
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LookupTypeText, y.LookupTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCode()
    Me.Sort(New csLookupCol.CompareByCode)
  End Sub
  Private Class CompareByCode
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Code, y.Code, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByText()
    Me.Sort(New csLookupCol.CompareByText)
  End Sub
  Private Class CompareByText
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Text, y.Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTextLocalized()
    Me.Sort(New csLookupCol.CompareByTextLocalized)
  End Sub
  Private Class CompareByTextLocalized
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TextLocalized, y.TextLocalized, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDescription()
    Me.Sort(New csLookupCol.CompareByDescription)
  End Sub
  Private Class CompareByDescription
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Description, y.Description, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDescriptionLocalized()
    Me.Sort(New csLookupCol.CompareByDescriptionLocalized)
  End Sub
  Private Class CompareByDescriptionLocalized
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DescriptionLocalized, y.DescriptionLocalized, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csLookupCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csLookup)
    Private Function Compare(ByVal x As csLookup, ByVal y As csLookup) As Integer Implements System.Collections.Generic.IComparer(Of csLookup).Compare
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
  
    Dim pLookup As csLookup
  
    While vReader.Read()
      pLookup = New csLookup(_IsLocalized) 
    If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
      pLookup.OverrideDefaultLanguage(_LocalizedLanguage) 
    End If 
      pFault = pLookup.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pLookup)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pLookup.ID) 
    End While
    If _IsLocalized = True AndAlso _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
      _LocalizedLanguage = vRequester.UILang 
    End If 
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedLookupCol As csLookupCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pLookup As csLookup 
 
      For Each pCachedLookup As csLookup In vCachedLookupCol 
        pCachedLookup.SetLocalizable(_IsLocalized) 
        pLookup = New csLookup(pCachedLookup) 
        If _IsLocalized = True AndAlso _LocalizedLanguage <> clsEnums.enmLanguage.UD Then 
          pLookup.OverrideDefaultLanguage(_LocalizedLanguage) 
        End If 
        pFault = pLookup.LoadTranslations(vRequester) 
        If pFault.isOK = False Then Return pFault 
        pLookup.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pLookup) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pLookup.ID) 
      Next 
    If _IsLocalized = True AndAlso _LocalizedLanguage = clsEnums.enmLanguage.UD Then 
      _LocalizedLanguage = vRequester.UILang 
    End If 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-Lookup-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLookup) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = New Dictionary(Of String, csLookup)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csLookup) 
    _SortedDictionaryForFindByParentLookupTypeAndParentCodeAndLookupTypeAndCode = New Dictionary(Of String, csLookup)(StringComparer.OrdinalIgnoreCase) 
 
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
  
