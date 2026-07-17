Public Class csObjectToTranslate
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
    [ObjectType] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [ObjectTranslation] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [ObjectType] 
    [Object] 
    [Item] 
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
  
  
  Private _ID As Long
  Private _ObjectType As clsEnums.enmObjectType
  Private _ObjectTypeText As String 
  Private _Object As String
  Private _Item As String
  Private _Tag As String
  Private _ObjectTranslations As csObjectTranslationCol
  
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
  Public Property [ObjectType]() As clsEnums.enmObjectType
    Get
      Return Me._ObjectType
    End Get
    Set(ByVal value As clsEnums.enmObjectType)
      If Me._ObjectType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ObjectType = value 
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
  Public Property [ObjectTypeText]() As String
    Get
      Return Me._ObjectTypeText
    End Get
    Set(ByVal value As String)
      Me._ObjectTypeText = value
    End Set
  End Property
  Public Property [Object]() As String
    Get
      Return Me._Object
    End Get
    Set(ByVal value As String)
      If Me._Object <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Object = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [Item]() As String
    Get
      Return Me._Item
    End Get
    Set(ByVal value As String)
      If Me._Item <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Item = value 
        CreateDefaultDesignation() 
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
  Public Property [ObjectTranslations]() As csObjectTranslationCol
    Get
      Return Me._ObjectTranslations
    End Get
    Set(ByVal value As csObjectTranslationCol)
      Me._ObjectTranslations = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = ccHelper.CreateFriendlyTextFromHungarianNotation(_ObjectType.FastToString() & ": " & _Object & ": " & _Item) Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _ObjectType <> clsEnums.enmObjectType.UD Then pValue.Append("ObjectType='" & _ObjectType.FastToString() & "' ‡ ") 
    If _ObjectTypeText <> "" Then pValue.Append("ObjectTypeText='" & _ObjectTypeText & "' ‡ ") 
    If _Object <> "" Then pValue.Append("Object='" & _Object & "' ‡ ") 
    If _Item <> "" Then pValue.Append("Item='" & _Item & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ObjectType.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_ObjectTypeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Object)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Item)}""") 
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
  
  Public Sub New(ByVal vcsObjectToTranslate As csObjectToTranslate)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsObjectToTranslate) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vObjectType As clsEnums.enmObjectType = clsEnums.enmObjectType.UD _ 
    , Optional vObjectTypeText As String = "" _ 
    , Optional vObject As String = "" _ 
    , Optional vItem As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _ObjectType = vObjectType 
    _ObjectTypeText = vObjectTypeText 
    _Object = vObject 
    _Item = vItem 
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
 
    _Object = _Object.Truncate(pTruncateLength, _IsTruncated) 
    _Item = _Item.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  Friend Sub CleanEntityForXML() 
    'set all string to clean 
 
    _Object = ccHelper.RemoveChrW0(_Object) 
    _Item = ccHelper.RemoveChrW0(_Item) 
    _Tag = ccHelper.RemoveChrW0(_Tag) 
 
    _IsCleanForXML = True 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ObjectToTranslate by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslate_GetByPrimaryKey", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectToTranslate-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [ObjectTypeAndObjectAndItem] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ObjectToTranslate by the chosen parameters. This function may be a bit slower than accessing the ObjectToTranslate's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslate_GetByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.ObjectTypeAndObjectAndItem 
          pFault = GetByObjectTypeAndObjectAndItem(clsEnums.TranslateEnmObjectType(CStr(vParameters(0))), CStr(vParameters(1)), CStr(vParameters(2)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ObjectToTranslate-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectToTranslate-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the ObjectToTranslate by ID.
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslate_GetByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      If vMustExist Then Return pFault.LogFreeTextFault(70, $"ObjectToTranslate not found for GetByID, since its value is 0", pFunctionParameters, "TRGT-ObjectToTranslate-210927-1527", vRequester, vAdditionalMessageToUser:=$"ObjectToTranslate not found") 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccObjectToTranslateCol.FindByID(vID), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslateGetByID" 
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
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"ObjectToTranslate not found for GetByID. See FunctionParameters for values", pFunctionParameters, "TRGT-ObjectToTranslate-210625-0950", vRequester, vAdditionalMessageToUser:=$"ObjectToTranslate not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090623-1648", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
       
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the ObjectToTranslate by ObjectTypeAndObjectAndItem.
  ''' </summary>
  ''' <param name="vObjectType"></param>
  ''' <param name="vObject"></param>
  ''' <param name="vItem"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String, ByVal vItem As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, Object={1}, Item={2}", vObjectType, vObject, vItem)
    Dim pFault As New clsFault
    
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslate_GetByObjectTypeAndObjectAndItem", vRequester) 
    If Not pFault.isOK Then Return pFault 
    
    CreateEmpty() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      pFault = LoadMeFromDBCache(MyController.DBCache.ccObjectToTranslateCol.FindByObjectTypeAndObjectAndItem(vObjectType, vObject, vItem), vRequester) : If pFault.isOK = False Then Return pFault
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslateGetByObjectType&Object&Item" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType.FastToString()) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObject) 
        pLastReadVariableName = "Item" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = (vItem) 
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeGetWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) : If Not pFault.isOK Then Return pFault 
        If (vMustExist AndAlso Me.IsEmpty) Then pFault.LogFreeTextFault(70, $"ObjectToTranslate not found for GetByObjectTypeAndObjectAndItem. See FunctionParameters for values", pFunctionParameters, "TRGT-ObjectToTranslate-210625-0950", vRequester, vAdditionalMessageToUser:=$"ObjectToTranslate not found") 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090623-1648", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate, "csObjectToTranslate_AddUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ObjectToTranslate-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate, "csObjectToTranslate_EditUpdate", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ObjectToTranslate-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the ObjectToTranslate. If there are parents or children in the ObjectToTranslate, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate, "csObjectToTranslate_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    Dim pID As Long 
 
    'Check if we got an empty object 
    Dim pObjectToTranslate As New csObjectToTranslate() 
    If Me.isEqual(pObjectToTranslate) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", pFunctionParameters, "TRGT-ObjectToTranslate-100113-1638", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-ObjectToTranslate-240611-135714", vRequester) 
    End If 
 
    Dim pCommandText As String = "c_ObjectToTranslateUpdate"
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
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pCachedObjectToTranslate As csObjectToTranslate 
      If _ID = 0 Then 
        pCachedObjectToTranslate = New csObjectToTranslate() 
        'get last ID 
        Dim pObjectToTranslateCol As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.Clone() 
        If pObjectToTranslateCol.Count = 0 Then 
          _ID = 1 
        Else 
          pObjectToTranslateCol.SortByID() 
          Dim pLastID As Long = pObjectToTranslateCol(pObjectToTranslateCol.Count - 1).ID
          _ID = pLastID + 1 
        End If 
        bPrimaryKey = _ID 
        MyController.DBCache.ccObjectToTranslateCol.Add(pCachedObjectToTranslate) 
      Else  
        pCachedObjectToTranslate = MyController.DBCache.ccObjectToTranslateCol.FindByID(_ID) 
      End If 
      pCachedObjectToTranslate.AssignValues(Me) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccObjectToTranslateCol, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "ID" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.BigInt).Value = (_ID) 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (_ObjectType.FastToString()) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(_Object) 
        pLastReadVariableName = "Item" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(_Item) 
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
              pFault.LogFreeTextFault(51, "ID returned is 0!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
            End If 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090623-1809", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
  
    pID = _ID

    If vReload = True Then 
      'Keep parents and children. If they were sent to me, then the programmer expects them to be given back :-) 
      'Children 
      Dim pObjectTranslations As csObjectTranslationCol = _ObjectTranslations 
      
      pFault = Me.GetByID(pID, vRequester, True)
      If pFault.isOK = False Then Return pFault 
      
      'Now reload them 
      'Children 
      If Not pObjectTranslations Is Nothing Then _ObjectTranslations = pObjectTranslations 
      
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
    Dim pFunctionParameters As String = String.Format("ObjectToTranslate.ID={0}", _ID)
    Dim pFault As clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslate_Delete", vRequester) 
    If Not pFault.isOK Then Return pFault 
  
    Dim pCancel As Boolean = False
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
  
    Dim pCommandText As String = "c_ObjectToTranslateDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      MyController.DBCache.ccObjectToTranslateCol.Remove(MyController.DBCache.ccObjectToTranslateCol.FindByID(_ID)) 
      'Save File 
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccObjectToTranslateCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090623-1813", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslate_DeleteByID", vRequester) 
    If Not pFault.isOK Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslateDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      MyController.DBCache.ccObjectToTranslateCol.Remove(MyController.DBCache.ccObjectToTranslateCol.FindByID(vID)) 
      'Save File  
      pFault = MyController.DBCache.SaveData(MyController.DBCache.ccObjectToTranslateCol, vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-231207-0845", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-231207-0844", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-231207-0843", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the ObjectToTranslate's ObjectTranslation collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillObjectTranslations(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslate_FillObjectTranslations", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    _ObjectTranslations = New csObjectTranslationCol(clsEnums.enmLoadParent.DoNotLoad)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _ObjectTranslations.FillByObjectToTranslateID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
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
    If Not (TypeOf (vTargCCEntityToTest) Is csObjectToTranslate) Then Return False 
    Dim pObjectToTranslateToTest As csObjectToTranslate = CType(vTargCCEntityToTest, csObjectToTranslate) 
    Return isEqual(pObjectToTranslateToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vObjectToTranslateToTest As csObjectToTranslate) As Boolean
    With vObjectToTranslateToTest
      If _ID <> .ID Then Return False
      If _ObjectType <> .ObjectType Then Return False
      If _Object <> .Object Then Return False
      If _Item <> .Item Then Return False
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
    Dim pClone As New csObjectToTranslate(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csObjectToTranslate
    Dim pClone As New csObjectToTranslate(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-ObjectToTranslate-130316-0852", vRequester) : End Try 
    Try : vDataRow("ObjectType") = _ObjectType : Catch ex As Exception : Return pFault.LogException(ex, "ObjectType", "TRGT-ObjectToTranslate-130316-0852", vRequester) : End Try 
    Try : vDataRow("Object") = _Object : Catch ex As Exception : Return pFault.LogException(ex, "Object", "TRGT-ObjectToTranslate-130316-0852", vRequester) : End Try 
    Try : vDataRow("Item") = _Item : Catch ex As Exception : Return pFault.LogException(ex, "Item", "TRGT-ObjectToTranslate-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pObjectToTranslate As csObjectToTranslate = CType(pXmlSerializer.Deserialize(pStreamReader), csObjectToTranslate) 
      AssignValues(pObjectToTranslate) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-ObjectToTranslate-130515-1230", vRequester) 
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
          'ObjectType 
          pBinaryWriter.Write(_ObjectType.FastToString()) 
          'Object 
          If _Object Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Object) 
          'Item 
          If _Item Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Item) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'ObjectTranslations  
          If _ObjectTranslations IsNot Nothing Then 
            pObjectBytes = _ObjectTranslations.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-150307-2338", vRequester) 
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
          'ObjectType 
          _ObjectType = clsEnums.TranslateEnmObjectType(pReader.ReadString) 
          'Object 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Object = pReader.ReadString 
          'Item 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Item = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'ObjectTranslations 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _ObjectTranslations = New csObjectTranslationCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-ObjectToTranslate-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-190720-1443", vRequester) 
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
 
      Dim pObjectToTranslate As csObjectToTranslate = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csObjectToTranslate)(vJSON, pSettings) 
      AssignValues(pObjectToTranslate) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vObjectToTranslate As csObjectToTranslate)
    With vObjectToTranslate
      _ID = .ID 
      _ObjectType = .ObjectType 
      _ObjectTypeText = .ObjectTypeText
      _Object = .Object 
      _Item = .Item 
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
      'ObjectType 
      pTextToGet = "ObjectTypeText (Enum)" 
      _ObjectTypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.ObjectType, _ObjectType.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-ObjectToTranslate-151124-1900", vRequester) 
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
      pLastReadVariableName = "enmObjectType" 
      If Not vReader.IsDBNull(1) Then _ObjectType = clsEnums.TranslateEnmObjectType(vReader.GetString(1))
      pLastReadVariableName = "Object" 
      If Not vReader.IsDBNull(2) Then _Object = vReader.GetString(2) 
      pLastReadVariableName = "Item" 
      If Not vReader.IsDBNull(3) Then _Item = vReader.GetString(3) 
      pLastReadVariableName = "bDateAdded" 
      If Not vReader.IsDBNull(4) Then bDateAdded = vReader.GetDateTime(4)   
      _IsCleanForXML = False 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK()
      pLastReadVariableName = "" 
    Catch ex As Exception
      If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1143", vRequester)
    End Try
  
    bPrimaryKey = _ID 
    CreateDefaultDesignation() 
    Return pFault
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedObjectToTranslate As csObjectToTranslate, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    CreateEmpty()
 
    Try 
      AssignValues(vCachedObjectToTranslate) 
      bccStatus = clsEnums.enmObjectStatus.Clean 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-121122-1607", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
#End Region
 
  Private Sub CreateEmpty()
    
    _ID = 0 
    _ObjectType = clsEnums.enmObjectType.UD
    _ObjectTypeText = ""
    _Object = ""
    _Item = ""
    _Tag = ""
    _ObjectTranslations = Nothing
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
  
Public Class csObjectToTranslateCol
  Inherits cTargCCCollection(Of csObjectToTranslate)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csObjectToTranslate) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByObjectTypeAndObjectAndItem As Dictionary(Of String, csObjectToTranslate) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByObjectTypeAndObjectAndItem As Boolean 
  Private Function CreateKeyForFindByObjectTypeAndObjectAndItem(ByVal vObjectToTranslate As csObjectToTranslate) As String 
    With vObjectToTranslate 
      Return .ObjectType.ToString() & "|" & .Object & "|" & .Item
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
 
    For Each pRow As csObjectToTranslate In Me 
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
    pCSVTitle.Append(",""ObjectType" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""ObjectType (Text)""") 
    pCSVTitle.Append(",""Object""") 
    pCSVTitle.Append(",""Item""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csObjectToTranslate In Me 
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
 
  Public Overloads Sub Add(ByVal vObjectToTranslate As csObjectToTranslate) 
    SyncLock _CollectionLock 
      MyBase.Add(vObjectToTranslate) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vObjectToTranslate As csObjectToTranslate) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vObjectToTranslate) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vObjectToTranslateCol As csObjectToTranslateCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vObjectToTranslateCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vObjectToTranslate As csObjectToTranslate) 
    SyncLock _CollectionLock 
      MyBase.Remove(vObjectToTranslate) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csObjectToTranslate) 
      
      For Each lObjectToTranslate In Me 
        If lObjectToTranslate.IsEmpty OrElse pTempDictionary.ContainsKey(lObjectToTranslate.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lObjectToTranslate.ID, lObjectToTranslate) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lObjectToTranslate.ToString, "TRGT-ObjectToTranslate-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", ObjectToTranslate:" & lObjectToTranslate.ToString() & ", TRGT-ObjectToTranslate-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadObjectTypeAndObjectAndItems() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByObjectTypeAndObjectAndItem Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByObjectTypeAndObjectAndItem Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByObjectTypeAndObjectAndItem' yet!
      Dim pTempDictionary As New Dictionary(Of String, csObjectToTranslate)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lObjectToTranslate In Me 
        Try 
          Dim pObjectTypeAndObjectAndItem As String = CreateKeyForFindByObjectTypeAndObjectAndItem(lObjectToTranslate) 
          If String.IsNullOrEmpty(pObjectTypeAndObjectAndItem.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pObjectTypeAndObjectAndItem)) Then 
            pTempDictionary.Add(pObjectTypeAndObjectAndItem, lObjectToTranslate) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lObjectToTranslate.ToString, "TRGT-ObjectToTranslate-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByObjectTypeAndObjectAndItem:" & ex.Message & ", ObjectToTranslate:" & lObjectToTranslate.ToString() & ", TRGT-ObjectToTranslate-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByObjectTypeAndObjectAndItem = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = False
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
 
    For Each lObjectToTranslate As csObjectToTranslate In Me 
      lObjectToTranslate.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Sub CleanCollectionForXML() 
    'set all strings to clean 
 
    For Each lObjectToTranslate As csObjectToTranslate In Me 
      lObjectToTranslate.CleanEntityForXML() 
    Next 
 
    _IsCleanForXML = True 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ObjectType] 
    [ObjectTypeAndObject] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the ObjectToTranslates by the chosen parameters. This function may be a bit slower than accessing the ObjectToTranslate's FillBy... directly 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByParameters", vRequester) 
    If pFault.isOK = False Then Return pFault 
    
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.ObjectType 
          pFault = FillByObjectType(clsEnums.TranslateEnmObjectType(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ObjectTypeAndObject 
          pFault = FillByObjectTypeAndObject(clsEnums.TranslateEnmObjectType(CStr(vParameters(0))), CStr(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ObjectToTranslate-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectToTranslate-151223_1716", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_Fill", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Me.Clear() 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.Clone() 
      pObjectToTranslatesCached.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pObjectToTranslatesCached.Reverse() 
      If vHowMany > 0 AndAlso pObjectToTranslatesCached.Count > vHowMany Then 
        Dim tmp As New csObjectToTranslateCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pObjectToTranslatesCached(i)) 
        Next 
        pObjectToTranslatesCached = tmp 
      End If 
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFill"
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1625", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByObjectType(ByVal vObjectType As clsEnums.enmObjectType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}", vObjectType)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByObjectType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.CloneByObjectType(vObjectType)
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillByObjectType" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType.FastToString()) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1702", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectTypeAndObject, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCending or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, Object={1}", vObjectType, vObject)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByObjectTypeAndObject", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.CloneByObjectTypeAndObject(vObjectType, vObject)
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillByObjectType&Object" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType.FastToString()) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObject) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1702", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.CloneByBoundedID(vIDFrom, vIDTo)
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillByBoundedID" 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectTypeAndObject, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, ObjectFrom={1}, ObjectTo={2}", vObjectType, vObjectFrom, vObjectTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByBoundedObjectTypeAndObject", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.CloneByBoundedObjectTypeAndObject(vObjectType, vObjectFrom, vObjectTo)
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillByBoundedObjectType&Object" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType.FastToString()) 
        pLastReadVariableName = "ObjectFrom" 
        pDALParameters.Add("bndObjectFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectFrom) 
        pLastReadVariableName = "ObjectTo" 
        pDALParameters.Add("bndObjectTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-151113-1405", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectTypeAndObjectAndItem, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String, ByVal vItemFrom As String, ByVal vItemTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, ObjectFrom={1}, ObjectTo={2}, ItemFrom={3}, ItemTo={4}", vObjectType, vObjectFrom, vObjectTo, vItemFrom, vItemTo)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByBoundedObjectTypeAndObjectAndItem", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      If MyController.DBCache.ccObjectToTranslateCol.Count = 0 Then Dim pResponse As String = MyController.DBCache.LoadTableFromFileSystem(MyController.DBCache.ccObjectToTranslateCol) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Throw New Exception("Load csObjectToTranslateCol failed: " & pResponse) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.CloneByBoundedObjectTypeAndObjectAndItem(vObjectType, vObjectFrom, vObjectTo, vItemFrom, vItemTo)
      pFault = LoadMeFromDBCache(pObjectToTranslatesCached, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
        Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillByBoundedObjectType&Object&Item" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType.FastToString()) 
        pLastReadVariableName = "ObjectFrom" 
        pDALParameters.Add("bndObjectFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectFrom) 
        pLastReadVariableName = "ObjectTo" 
        pDALParameters.Add("bndObjectTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectTo) 
        pLastReadVariableName = "ItemFrom" 
        pDALParameters.Add("bndItemFrom", ccDAL.enmSQLDataType.NVarChar, 255).Value = (vItemFrom) 
        pLastReadVariableName = "ItemTo" 
        pDALParameters.Add("bndItemTo", ccDAL.enmSQLDataType.NVarChar, 255).Value = (vItemTo) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-151113-1405", vRequester) 
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillByListOfID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If vAppend = False Then Me.Clear() 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    For Each l In vIDs 
      If Not (Me.FindByID(l).IsEmpty) Then Continue For 
      Dim lObjectToTranslate As New csObjectToTranslate() 
      pFault = lObjectToTranslate.GetByID(l, vRequester) : If Not pFault.isOK Then Return pFault 
      If Not lObjectToTranslate.IsEmpty Then Me.Add(lObjectToTranslate) 
    Next 
 
    If pEnteredHere = True Then 'only check if this function was accessed directly from outside DBController 
      Dim pObjectToTranslates As Generic.List(Of ITargCCEntity) = Nothing 
      pFault = ccSecurity.GetPermissionForExternalIndentityTypeForCollection(Me, pObjectToTranslates, "csObjectToTranslateCol_Fill", vRequester) : If Not pFault.isOK Then Return pFault 
      If pObjectToTranslates IsNot Nothing AndAlso Me.Count <> pObjectToTranslates.Count Then FillFromListOfITargCCEntity(pObjectToTranslates) 
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
    [ObjectType]
    [Object]
    ObjectWildcardType
    [Item]
    ItemWildcardType
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pObjectType As clsEnums.enmObjectType = clsEnums.enmObjectType.UD
    Dim pObject As String = Nothing
    Dim pObjectWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pItem As String = Nothing
    Dim pItemWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectType) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectType) : If pObj IsNot Nothing Then pObjectType = CType(pObj, clsEnums.enmObjectType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Object) Then pObj = vParameters(enmFillOnTheFlyParameters.Object) : If pObj IsNot Nothing Then pObject = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectWildcardType) : If pObj IsNot Nothing Then pObjectWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Item) Then pObj = vParameters(enmFillOnTheFlyParameters.Item) : If pObj IsNot Nothing Then pItem = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ItemWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ItemWildcardType) : If pObj IsNot Nothing Then pItemWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pObjectType _
        , pObject, pObjectWildcardType _
        , pItem, pItemWildcardType _
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
        , ByVal vObjectType As clsEnums.enmObjectType _
        , ByVal vObject As String, ByVal vObjectWildcardType As clsEnums.enmWildCardType _
        , ByVal vItem As String, ByVal vItemWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ObjectType={2}, Object={3}, ObjectWildcardType={4}, Item={5}, ItemWildcardType={6}", vIDFrom, vIDTo, vObjectType, vObject, vObjectWildcardType.FastToString(), vItem, vItemWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Object 
    Dim pWCObject As String = "" 
    If vObject = Nothing Then 
      pWCObject = vObject
    Else 
      If vObjectWildcardType = clsEnums.enmWildCardType.None OrElse vObjectWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCObject = vObject
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.After Then 
        pWCObject = vObject & "%" 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCObject = "%" & vObject 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCObject = "%" & vObject & "%" 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vObject.ToCharArray 
          pWCObject &= p & "%" 
        Next 
        pWCObject = "%" & pWCObject 
      End If 
    End If 
    'Item 
    Dim pWCItem As String = "" 
    If vItem = Nothing Then 
      pWCItem = vItem
    Else 
      If vItemWildcardType = clsEnums.enmWildCardType.None OrElse vItemWildcardType = clsEnums.enmWildCardType.UD Then 
        pWCItem = vItem
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.After Then 
        pWCItem = vItem & "%" 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.Before Then 
        pWCItem = "%" & vItem 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        pWCItem = "%" & vItem & "%" 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        For Each p As Char In vItem.ToCharArray 
          pWCItem &= p & "%" 
        Next 
        pWCItem = "%" & pWCItem 
      End If 
    End If 
    
    Me.Clear() 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      'Return pFault.LogFreeTextFault("FillOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-ObjectToTranslate-121122-2008", vRequester) 
      Dim pObjectToTranslatesCached As csObjectToTranslateCol = MyController.DBCache.ccObjectToTranslateCol.Clone() 
      Dim pObjectToTranslatesToUse As New csObjectToTranslateCol() 
      For Each l In pObjectToTranslatesCached 
        If vIDFrom.HasValue Then 
          If vIDTo.HasValue Then 
            If l.ID < vIDFrom OrElse l.ID > vIDTo.Value Then Continue For 
          Else 
            If l.ID <> vIDFrom.Value Then Continue For 
          End If 
        End If 
        If vObjectType <> clsEnums.enmObjectType.UD Then 
          If l.ObjectType <> vObjectType Then Continue For 
        End If 
        If Not String.IsNullOrEmpty(vObject) Then 
          If vObjectWildcardType = clsEnums.enmWildCardType.UD OrElse vObjectWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Object.Equals(vObject, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vObjectWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Object.StartsWith(vObject, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vObjectWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Object.EndsWith(vObject, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Object.IndexOf(vObject, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        If Not String.IsNullOrEmpty(vItem) Then 
          If vItemWildcardType = clsEnums.enmWildCardType.UD OrElse vItemWildcardType = clsEnums.enmWildCardType.None Then 
            If Not l.Item.Equals(vItem, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vItemWildcardType = clsEnums.enmWildCardType.After Then 
            If Not l.Item.StartsWith(vItem, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vItemWildcardType = clsEnums.enmWildCardType.Before Then 
            If Not l.Item.EndsWith(vItem, StringComparison.OrdinalIgnoreCase) Then Continue For 
          ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
            If Not (l.Item.IndexOf(vItem, StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For 
          ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
            Return pFault.LogFreeTextFault("BeforeAndAfterAndBetweenEachLetter is not available for FillOnTheFly", pFunctionParameters, "TRGT-Card-210620-0809", vRequester) 
          End If 
        End If 
        pObjectToTranslatesToUse.Add(l) 
      Next 
      pObjectToTranslatesToUse.SortByID() : If vDir = clsEnums.enmFillDirection.DESC Then pObjectToTranslatesToUse.Reverse() 
      If vHowMany > 0 AndAlso pObjectToTranslatesToUse.Count > vHowMany Then 
        Dim tmp As New csObjectToTranslateCol 
        For i = 0 To vHowMany - 1 
          tmp.Add(pObjectToTranslatesToUse(i)) 
        Next 
        pObjectToTranslatesToUse = tmp 
      End If 
      pFault = LoadMeFromDBCache(pObjectToTranslatesToUse, vRequester) : If pFault.isOK = False Then Return pFault 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vObjectType.FastToString()) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add("wldObject", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCObject) 
        pLastReadVariableName = "Item" 
        pDALParameters.Add("wldItem", ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(pWCItem) 
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
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByObjectType
    GroupByObject
    GroupByItem
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
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pObjectType As clsEnums.enmObjectType = clsEnums.enmObjectType.UD
    Dim pObject As String = Nothing
    Dim pObjectWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pItem As String = Nothing
    Dim pItemWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByObjectType As Boolean = False
    Dim pGroupByObject As Boolean = False
    Dim pGroupByItem As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectType) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectType) : If pObj IsNot Nothing Then pObjectType = CType(pObj, clsEnums.enmObjectType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Object) Then pObj = vParameters(enmFillOnTheFlyParameters.Object) : If pObj IsNot Nothing Then pObject = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectWildcardType) : If pObj IsNot Nothing Then pObjectWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Item) Then pObj = vParameters(enmFillOnTheFlyParameters.Item) : If pObj IsNot Nothing Then pItem = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ItemWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ItemWildcardType) : If pObj IsNot Nothing Then pItemWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByObjectType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByObjectType) : If pObj IsNot Nothing Then pGroupByObjectType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByObject) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByObject) : If pObj IsNot Nothing Then pGroupByObject = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByItem) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByItem) : If pObj IsNot Nothing Then pGroupByItem = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pObjectType _
        , pObject, pObjectWildcardType _
        , pItem, pItemWildcardType _
        , pGroupByObjectType _
        , pGroupByObject _
        , pGroupByItem _
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
        , ByVal vObjectType As clsEnums.enmObjectType _
        , ByVal vObject As String, ByVal vObjectWildcardType As clsEnums.enmWildCardType _
        , ByVal vItem As String, ByVal vItemWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByObjectType As Boolean _
        , ByVal vGroupByObject As Boolean _
        , ByVal vGroupByItem As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ObjectType={2}, Object={3}, ObjectWildcardType={4}, Item={5}, ItemWildcardType={6}, GroupByObjectType={7}, GroupByObject={8}, GroupByItem={9}", vIDFrom, vIDTo, vObjectType, vObject, vObjectWildcardType.FastToString(), vItem, vItemWildcardType.FastToString(), vGroupByObjectType, vGroupByObject, vGroupByItem)
    Dim pFault As New clsFault
 
    Dim pEnteredHere As Boolean = String.IsNullOrEmpty(vRequester.EntryFunction) 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_FillSumOnTheFly", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Object 
    Dim pWCObject As String = "" 
    If vObject = Nothing Then 
      pWCObject = vObject
    ElseIf vObjectWildcardType = clsEnums.enmWildCardType.None OrElse vObjectWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCObject = vObject
    ElseIf vObjectWildcardType = clsEnums.enmWildCardType.After Then 
      pWCObject = vObject & "%" 
    ElseIf vObjectWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCObject = "%" & vObject 
    ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCObject = "%" & vObject & "%" 
    ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vObject.ToCharArray 
        pWCObject &= p & "%" 
      Next 
      pWCObject = "%" & pWCObject 
    End If 
    'Item 
    Dim pWCItem As String = "" 
    If vItem = Nothing Then 
      pWCItem = vItem
    ElseIf vItemWildcardType = clsEnums.enmWildCardType.None OrElse vItemWildcardType = clsEnums.enmWildCardType.UD Then 
      pWCItem = vItem
    ElseIf vItemWildcardType = clsEnums.enmWildCardType.After Then 
      pWCItem = vItem & "%" 
    ElseIf vItemWildcardType = clsEnums.enmWildCardType.Before Then 
      pWCItem = "%" & vItem 
    ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
      pWCItem = "%" & vItem & "%" 
    ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
      For Each p As Char In vItem.ToCharArray 
        pWCItem &= p & "%" 
      Next 
      pWCItem = "%" & pWCItem 
    End If 
    Me.Clear() 
    
    _FilledFromSumOnTheFly = True 
    
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("FillSumOnTheFly has not been handled yet locally", pFunctionParameters, "TRGT-ObjectToTranslate-121122-2008", vRequester) 
    Else 
      Dim pCancel As Boolean = False 
      Dim pCommandText As String = "c_ObjectToTranslatesFillSumOnTheFly" 
      Dim pDALParameters As New ccDAL.csTargCCParameterCol 
   
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters 
        pLastReadVariableName = "IDFrom" 
        pDALParameters.Add("bndIDFrom", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDFrom) 
        pLastReadVariableName = "IDTo" 
        pDALParameters.Add("bndIDTo", ccDAL.enmSQLDataType.BigInt).Value = ccHelper.ObjectNullable(vIDTo) 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(vObjectType) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add("wldObject", ccDAL.enmSQLDataType.VarChar, 50).Value = ccHelper.ObjectNullable(pWCObject) 
        pLastReadVariableName = "Item" 
        pDALParameters.Add("wldItem", ccDAL.enmSQLDataType.NVarChar, 255).Value = ccHelper.ObjectNullable(pWCItem) 
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add("GroupByenmObjectType", ccDAL.enmSQLDataType.Bit).Value = vGroupByObjectType
        pLastReadVariableName = "Object" 
        pDALParameters.Add("GroupByObject", ccDAL.enmSQLDataType.Bit).Value = vGroupByObject
        pLastReadVariableName = "Item" 
        pDALParameters.Add("GroupByItem", ccDAL.enmSQLDataType.Bit).Value = vGroupByItem
        pLastReadVariableName = "" 
   
        RaiseEvent evtBeforeFillWithRequester(pCommandText, pDALParameters, pCancel, vRequester, pFault) 
        If pFault.isOK = False Then Return pFault 
        If pCancel = True Then Return pFault 
   
        'Execute query 
        pFault = ccDAL.ExecuteQuery(pCommandText, pDALParameters, vRequester, Me) 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090303-1658", vRequester) 
      End Try 
      If Not pFault.isOK Then Return pFault 
    End If
        
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vObjectToTranslateArray As csObjectToTranslate())
    Me.Clear()
    
    For Each pObjectToTranslate As csObjectToTranslate In vObjectToTranslateArray
      Me.Add(pObjectToTranslate)
      _Clean.Add(pObjectToTranslate.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pObjectToTranslate As New csObjectToTranslate(pRow, vRequester) 
        Me.Add(pObjectToTranslate) 
        _Clean.Add(pObjectToTranslate.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-ObjectToTranslateCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-130515-1300", vRequester) 
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
      Dim pObjectToTranslates As csObjectToTranslateCol = CType(pXmlSerializer.Deserialize(pStreamReader), csObjectToTranslateCol) 
      For Each pObjectToTranslate As csObjectToTranslate In pObjectToTranslates 
        Me.Add(pObjectToTranslate) 
        _Clean.Add(pObjectToTranslate.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-ObjectToTranslate-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-190720-1443", vRequester) 
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
 
      Dim pObjectToTranslates As List(Of csObjectToTranslate) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csObjectToTranslate))(vJSON, pSettings) 
      For Each pObjectToTranslate As csObjectToTranslate In pObjectToTranslates 
        Me.Add(pObjectToTranslate) 
        _Clean.Add(pObjectToTranslate.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-190720-2059", vRequester) 
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
          For Each lObjectToTranslate As csObjectToTranslate In Me 
            Dim pByte As Byte() = lObjectToTranslate.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-150307-2340", vRequester) 
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
            Dim pObjectToTranslate As csObjectToTranslate = New csObjectToTranslate(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pObjectToTranslate) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pObjectToTranslate.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-ObjectToTranslate-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pObjectToTranslate As csObjectToTranslate In Me 
      With pObjectToTranslate 
        pFault = pObjectToTranslate.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csObjectToTranslateCol) Then Return False 
    Dim pObjectToTranslateColToTest As csObjectToTranslateCol = CType(vEntitiesToTest, csObjectToTranslateCol) 
    Return isEqual(pObjectToTranslateColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vObjectToTranslatesToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vObjectToTranslatesToTest As csObjectToTranslateCol) As Boolean
    If Me.Count <> vObjectToTranslatesToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vObjectToTranslatesToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    If pFilledFromSumOnTheFly Then pObjectToTranslates._FilledFromSumOnTheFly = True
    
    For Each pObjectToTranslate As csObjectToTranslate In Me 
      Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
      pObjectToTranslates.Add(pObjectToTranslateClone) 
      If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
    Next 
    Return pObjectToTranslates 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csObjectToTranslateCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    If pFilledFromSumOnTheFly Then pObjectToTranslates._FilledFromSumOnTheFly = True
    
    For Each pObjectToTranslate As csObjectToTranslate In Me
      Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
      pObjectToTranslates.Add(pObjectToTranslateClone)
      If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
    Next
    Return pObjectToTranslates
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csObjectToTranslateCol 
    Dim pObjectToTranslates As New csObjectToTranslateCol()  
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectToTranslate.ID > vIDFrom AndAlso pObjectToTranslate.ID <= vIDTo) Then 
        Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
        pObjectToTranslates.Add(pObjectToTranslateClone) 
        If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
      End If 
    Next 
    Return pObjectToTranslates 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ObjectType and Object (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String) As csObjectToTranslateCol 
    Dim pObjectToTranslates As New csObjectToTranslateCol()  
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectToTranslate.ObjectType = vObjectType) AndAlso (pObjectToTranslate.Object > vObjectFrom AndAlso pObjectToTranslate.Object <= vObjectTo) Then 
        Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
        pObjectToTranslates.Add(pObjectToTranslateClone) 
        If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
      End If 
    Next 
    Return pObjectToTranslates 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ObjectType and Object and Item (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String, ByVal vItemFrom As String, ByVal vItemTo As String) As csObjectToTranslateCol 
    Dim pObjectToTranslates As New csObjectToTranslateCol()  
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectToTranslate.ObjectType = vObjectType) AndAlso (pObjectToTranslate.Object > vObjectFrom AndAlso pObjectToTranslate.Object <= vObjectTo) AndAlso (pObjectToTranslate.Item > vItemFrom AndAlso pObjectToTranslate.Item <= vItemTo) Then 
        Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
        pObjectToTranslates.Add(pObjectToTranslateClone) 
        If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
      End If 
    Next 
    Return pObjectToTranslates 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectTypeWildcardType As clsEnums.enmWildCardType, ByVal vObject As String, ByVal vObjectWildcardType As clsEnums.enmWildCardType) As csObjectToTranslateCol 
    Dim pObjectToTranslates As New csObjectToTranslateCol 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vObjectWildcardType = clsEnums.enmWildCardType.After Then 
        If pObjectToTranslate.Object.StartsWith(vObject, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.Before Then 
        If pObjectToTranslate.Object.EndsWith(vObject, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pObjectToTranslate.Object.IndexOf(vObject, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vObject.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pObjectToTranslate.Object.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
      pObjectToTranslates.Add(pObjectToTranslateClone) 
    Next 
    Return pObjectToTranslates 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectTypeWildcardType As clsEnums.enmWildCardType, ByVal vObject As String, ByVal vObjectWildcardType As clsEnums.enmWildCardType, ByVal vItem As String, ByVal vItemWildcardType As clsEnums.enmWildCardType) As csObjectToTranslateCol 
    Dim pObjectToTranslates As New csObjectToTranslateCol 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vObjectWildcardType = clsEnums.enmWildCardType.After Then 
        If pObjectToTranslate.Object.StartsWith(vObject, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.Before Then 
        If pObjectToTranslate.Object.EndsWith(vObject, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pObjectToTranslate.Object.IndexOf(vObject, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vObjectWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vObject.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pObjectToTranslate.Object.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vItemWildcardType = clsEnums.enmWildCardType.After Then 
        If pObjectToTranslate.Item.StartsWith(vItem, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.Before Then 
        If pObjectToTranslate.Item.EndsWith(vItem, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pObjectToTranslate.Item.IndexOf(vItem, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vItemWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vItem.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pObjectToTranslate.Item.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone() 
      pObjectToTranslates.Add(pObjectToTranslateClone) 
    Next 
    Return pObjectToTranslates 
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
  Public Function FindByID(ByVal vID As Long) As csObjectToTranslate
    If Me.Count = 0 Then Return New csObjectToTranslate 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csObjectToTranslate) = _SortedDictionaryForFindByID 
    
    Dim pObjectToTranslate As csObjectToTranslate = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pObjectToTranslate) 
    If pObjectToTranslate IsNot Nothing Then Return pObjectToTranslate Else Return New csObjectToTranslate() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String, ByVal vItem As String) As csObjectToTranslate
    If Me.Count = 0 Then Return New csObjectToTranslate 
    
    If _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = True Then LoadObjectTypeAndObjectAndItems() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csObjectToTranslate) = _SortedDictionaryForFindByObjectTypeAndObjectAndItem 
    
    Dim pObjectToTranslate As csObjectToTranslate = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vObjectType.ToString() & "|" & vObject & "|" & vItem
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pObjectToTranslate) 
    If pObjectToTranslate IsNot Nothing Then Return pObjectToTranslate Else Return New csObjectToTranslate() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ObjectType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByObjectType(ByVal vObjectType As clsEnums.enmObjectType) As csObjectToTranslateCol
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectToTranslate) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectToTranslate As csObjectToTranslate In pTempDist.Values
        If pObjectToTranslate.ObjectType = vObjectType Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByObjectType with vObjectType of {vObjectType}", "2ndPartOfClone") 
      Dim pList As csObjectToTranslateCol = Me.Clone() 
      For Each pObjectToTranslate As csObjectToTranslate In pList 
        If pObjectToTranslate.ObjectType = vObjectType Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    End If 
    
    Return pObjectToTranslates
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByObject(ByVal vObject As String) As csObjectToTranslateCol
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectToTranslate) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vObject = vObject.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectToTranslate As csObjectToTranslate In pTempDist.Values
        If pObjectToTranslate.Object.ToLowerInvariant() = vObject Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByObject with vObject of {vObject}", "2ndPartOfClone") 
      Dim pList As csObjectToTranslateCol = Me.Clone() 
      For Each pObjectToTranslate As csObjectToTranslate In pList 
        If pObjectToTranslate.Object.ToLowerInvariant() = vObject Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    End If 
    
    Return pObjectToTranslates
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Item
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByItem(ByVal vItem As String) As csObjectToTranslateCol
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectToTranslate) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vItem = vItem.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectToTranslate As csObjectToTranslate In pTempDist.Values
        If pObjectToTranslate.Item.ToLowerInvariant() = vItem Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByItem with vItem of {vItem}", "2ndPartOfClone") 
      Dim pList As csObjectToTranslateCol = Me.Clone() 
      For Each pObjectToTranslate As csObjectToTranslate In pList 
        If pObjectToTranslate.Item.ToLowerInvariant() = vItem Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    End If 
    
    Return pObjectToTranslates
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csObjectToTranslateCol
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectToTranslate) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectToTranslate As csObjectToTranslate In pTempDist.Values
        If pObjectToTranslate.Tag.ToLowerInvariant() = vTag Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csObjectToTranslateCol = Me.Clone() 
      For Each pObjectToTranslate As csObjectToTranslate In pList 
        If pObjectToTranslate.Tag.ToLowerInvariant() = vTag Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    End If 
    
    Return pObjectToTranslates
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ObjectTypeAndObject
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String) As csObjectToTranslateCol
    Dim pObjectToTranslates As New csObjectToTranslateCol() 
    pObjectToTranslates._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pObjectToTranslate As csObjectToTranslate In _SortedDictionaryForFindByID.Values.ToList()
        If pObjectToTranslate.ObjectType = vObjectType AndAlso pObjectToTranslate.Object = vObject Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csObjectToTranslateCol = Me.Clone() 
      For Each pObjectToTranslate As csObjectToTranslate In pList 
        If pObjectToTranslate.ObjectType = vObjectType AndAlso pObjectToTranslate.Object = vObject Then
          Dim pObjectToTranslateClone As csObjectToTranslate = pObjectToTranslate.Clone()
          pObjectToTranslates.Add(pObjectToTranslateClone)
          If Not _FilledFromSumOnTheFly Then pObjectToTranslates._Clean.Add(pObjectToTranslate.ID) 
        End If
      Next
    End If 
    Return pObjectToTranslates
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
    For Each pObjectToTranslate As csObjectToTranslate In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pObjectToTranslate.LoadDataRow(pRow, vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateView, "csObjectToTranslateCol_Update", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    'Check for new rows 
    For Each p As csObjectToTranslate In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'add the rows to be deleted back to the collection, so that we can delete them via CC 
    If _Clean IsNot Nothing Then 'Since it's private, it will always be nothing when coming from a web service. (The same process was already done in WSController) 
      For Each pCleanID As Long In _Clean 
        If pCleanID = 0 Then Continue For 
        Dim pFound As csObjectToTranslate = Me.FindByID(pCleanID) 
        If pFound.ID = 0 Then 
          Dim pObjectToTranslateToKill As New csObjectToTranslate 
          pObjectToTranslateToKill.ID = pCleanID 
          pObjectToTranslateToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
          Me.Add(pObjectToTranslateToKill) 
        End If 
      Next 
    End If 
    pFault.SetOK() 
 
    _Clean = New List(Of Long) 
    Dim pToRemove As New List(Of Long) 
    For Each pExists As csObjectToTranslate In Me 
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
        Return pFault.LogFreeTextFault("Status should not be UD ", pFunctionParameters, "TRGT-ObjectToTranslate-130415-0942", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateUpdate, "csObjectToTranslateCol_UpdateFromCollection", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCancel As Boolean = False 
    RaiseEvent evtBeforeUpdateWithRequester(pCancel, vRequester, pFault) 
    If Not pFault.isOK Then Return pFault 
    
    If pCancel = True Then Return pFault 
 
    'Set the tags 
    For Each p As csObjectToTranslate In Me 
      p.Tag = "Not Yet Updated" 
    Next 
 
    'Now update them 
    For Each p As csObjectToTranslate In Me 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_Delete", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDelete" 
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      pFault = MyController.DBCache.SaveData(New csObjectToTranslateCol(), vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate--090624-1625", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByObjectType(ByVal vObjectType As clsEnums.enmObjectType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}", vObjectType)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_DeleteByObjectType", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDeleteByObjectType"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllObjectToTranslates As New csObjectToTranslateCol() : pAllObjectToTranslates.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredObjectToTranslates As csObjectToTranslateCol = pAllObjectToTranslates.CloneByObjectType(vObjectType) 
      For Each l In pFilteredObjectToTranslates 
        pAllObjectToTranslates.Remove(pAllObjectToTranslates.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllObjectToTranslates, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1702", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectTypeAndObject 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, Object={1}", vObjectType, vObject)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_DeleteByObjectTypeAndObject", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDeleteByObjectType&Object"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Dim pAllObjectToTranslates As New csObjectToTranslateCol() : pAllObjectToTranslates.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
      Dim pFilteredObjectToTranslates As csObjectToTranslateCol = pAllObjectToTranslates.CloneByObjectTypeAndObject(vObjectType, vObject) 
      For Each l In pFilteredObjectToTranslates 
        pAllObjectToTranslates.Remove(pAllObjectToTranslates.FindByID(l.ID)) 
      Next 
      pFault = MyController.DBCache.SaveData(pAllObjectToTranslates, vRequester) 
      If pFault.isOK = False Then Return pFault 
    Else 
      Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType) 
        pLastReadVariableName = "Object" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObject) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090624-1702", vRequester) 
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
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_DeleteByBoundedID", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDeleteByBoundedID"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-ObjectToTranslate-150216-2148", vRequester) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectTypeAndObject
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedObjectTypeAndObject(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, ObjectFrom={1}, ObjectTo={2}", vObjectType, vObjectFrom, vObjectTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_DeleteByBoundedObjectTypeAndObject", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDeleteByBoundedObjectType&Object"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-ObjectToTranslate-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType) 
        pLastReadVariableName = "ObjectFrom" 
        pDALParameters.Add("bndObjectFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectFrom) 
        pLastReadVariableName = "ObjectTo" 
        pDALParameters.Add("bndObjectTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090210-1341", vRequester) 
      End Try 
      If pFault.isOK = False Then Return pFault 
    End If
        
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectTypeAndObjectAndItem
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedObjectTypeAndObjectAndItem(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObjectFrom As String, ByVal vObjectTo As String, ByVal vItemFrom As String, ByVal vItemTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectType={0}, ObjectFrom={1}, ObjectTo={2}, ItemFrom={3}, ItemTo={4}", vObjectType, vObjectFrom, vObjectTo, vItemFrom, vItemTo)
    Dim pFault As New clsFault
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_ObjectToTranslateDelete, "csObjectToTranslateCol_DeleteByBoundedObjectTypeAndObjectAndItem", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pCommandText As String = "c_ObjectToTranslatesDeleteByBoundedObjectType&Object&Item"
    Dim pDALParameters As New ccDAL.csTargCCParameterCol 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.LogFreeTextFault("Function not implemented", pFunctionParameters, "TRGT-ObjectToTranslate-150216-2148", vRequester) 
    Else 
        Dim pLastReadVariableName As String = "" 
      Try 
        'set parameters  
        pLastReadVariableName = "enmObjectType" 
        pDALParameters.Add(pLastReadVariableName, ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectType) 
        pLastReadVariableName = "ObjectFrom" 
        pDALParameters.Add("bndObjectFrom", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectFrom) 
        pLastReadVariableName = "ObjectTo" 
        pDALParameters.Add("bndObjectTo", ccDAL.enmSQLDataType.VarChar, 50).Value = (vObjectTo) 
        pLastReadVariableName = "ItemFrom" 
        pDALParameters.Add("bndItemFrom", ccDAL.enmSQLDataType.NVarChar, 255).Value = (vItemFrom) 
        pLastReadVariableName = "ItemTo" 
        pDALParameters.Add("bndItemTo", ccDAL.enmSQLDataType.NVarChar, 255).Value = (vItemTo) 
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
            If pID <> -1 Then pFault.LogFreeTextFault(51, "I expected to get -1 back!", pFunctionParameters, "TRGT-ObjectToTranslate-151114-1147", vRequester) 
          Else 
            pFault.LogFreeTextFault(51, "No response returned from SQL query!", pFunctionParameters, "TRGT-ObjectToTranslate-160324-1700", vRequester) 
          End If 
        End If 
      Catch ex As Exception 
        If pLastReadVariableName <> "" Then pFunctionParameters = "Failed while reading variable: " & pLastReadVariableName & "; " & pFunctionParameters 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-090210-1341", vRequester) 
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
    Me.Sort(New csObjectToTranslateCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
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
  
  Public Sub SortByObjectType()
    Me.Sort(New csObjectToTranslateCol.CompareByObjectType)
  End Sub
  Private Class CompareByObjectType
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ObjectType < y.ObjectType Then
        Return -1
      ElseIf x.ObjectType = y.ObjectType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByObjectTypeText()
    Me.Sort(New csObjectToTranslateCol.CompareByObjectTypeText)
  End Sub
  Private Class CompareByObjectTypeText
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ObjectTypeText, y.ObjectTypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByObject()
    Me.Sort(New csObjectToTranslateCol.CompareByObject)
  End Sub
  Private Class CompareByObject
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Object, y.Object, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByItem()
    Me.Sort(New csObjectToTranslateCol.CompareByItem)
  End Sub
  Private Class CompareByItem
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Item, y.Item, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csObjectToTranslateCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csObjectToTranslate)
    Private Function Compare(ByVal x As csObjectToTranslate, ByVal y As csObjectToTranslate) As Integer Implements System.Collections.Generic.IComparer(Of csObjectToTranslate).Compare
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
  
    Dim pObjectToTranslate As csObjectToTranslate
  
    While vReader.Read()
      pObjectToTranslate = New csObjectToTranslate() 
      pFault = pObjectToTranslate.LoadMeFromIDataReader(vReader, vRequester) : If Not pFault.isOK Then Return pFault 
       
      Me.Add(pObjectToTranslate)
      If Not _FilledFromSumOnTheFly Then _Clean.Add(pObjectToTranslate.ID) 
    End While
    vRequester.Tag = "" 
  
    _IsCleanForXML = False 
     
    Return pFault
  
  End Function
  
  Private Function LoadMeFromDBCache(ByVal vCachedObjectToTranslateCol As csObjectToTranslateCol, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      Dim pObjectToTranslate As csObjectToTranslate 
 
      For Each pCachedObjectToTranslate As csObjectToTranslate In vCachedObjectToTranslateCol 
        pObjectToTranslate = New csObjectToTranslate(pCachedObjectToTranslate) 
        pObjectToTranslate.ccStatus = clsEnums.enmObjectStatus.Clean 
        Me.Add(pObjectToTranslate) 
        If Not _FilledFromSumOnTheFly Then _Clean.Add(pObjectToTranslate.ID) 
      Next 
      pFault.SetOK() 
      Catch ex As Exception 
        pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectToTranslate-121122-1513", vRequester) 
      End Try 
   
    Return pFault 
  End Function 
#End Region 
   
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csObjectToTranslate) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByObjectTypeAndObjectAndItem = New Dictionary(Of String, csObjectToTranslate)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByObjectTypeAndObjectAndItem = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _IsCleanForXML = False 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csObjectToTranslate) 
    _SortedDictionaryForFindByObjectTypeAndObjectAndItem = New Dictionary(Of String, csObjectToTranslate)(StringComparer.OrdinalIgnoreCase) 
 
    Static pWasRun As Boolean = False 
    If pWasRun = False Then 
      bHasParents = False 
      bHasLocalizedFields = False 
      bCanHave0AsPrimaryKey = False 
      pWasRun = True 
    End If 
  End Sub 
  
End Class 
  
