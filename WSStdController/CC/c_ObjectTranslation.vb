Public Class csObjectTranslation
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
  Implements ITargCCEntityDeletable 
 
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
  ''' Raised after getting the row from the data store. This also occurs after an update 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterGet()
  Friend Event evtAfterGetWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  
  'Parent Properties 
  Public Enum enmParentProperty 
    UD 
    [ObjectToTranslate] 
    [Language] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [ObjectToTranslate] 
    [Instance] 
    [DefaultText] 
    [Language] 
    [Text] 
    [InstanceUniqueText] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [Instance] 
  End Enum 
  ''' <summary> 
  ''' Raised before add, just before evtBeforeUpdate 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeAdd(ByRef rCancel As Boolean) 
  Friend Event evtBeforeAddWithRequester(ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
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
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
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
  
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _ObjectToTranslateID As Long
  Private _ObjectToTranslate As csObjectToTranslate
  Private _ObjectToTranslateText As String
  Private _Instance As Long
  Private _DefaultText As String
  Private _Language As clsEnums.enmLanguage
  Private _LanguageText As String 
  Private _Text As String
  Private _InstanceUniqueText As String
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
  Public Property [ObjectToTranslateID]() As Long
    Get
      Return Me._ObjectToTranslateID
    End Get
    Set(ByVal value As Long)
      If Me._ObjectToTranslateID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ObjectToTranslateID = value 
      End If 
    End Set
  End Property
  Public Property [ObjectToTranslate]() As csObjectToTranslate
    Get
      Return Me._ObjectToTranslate
    End Get
    Set(ByVal value As csObjectToTranslate)
      Me._ObjectToTranslate = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the ObjectToTranslate object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property ObjectToTranslateText() As String
    Get
      Return Me._ObjectToTranslateText
    End Get
    Set(ByVal value As String)
      Me._ObjectToTranslateText = value
    End Set
  End Property
  Public Property [Instance]() As Long
    Get
      Return Me._Instance
    End Get
    Set(ByVal value As Long)
      If Me._Instance <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Instance = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [DefaultText]() As String
    Get
      Return Me._DefaultText
    End Get
  End Property
  Public Property [Language]() As clsEnums.enmLanguage
    Get
      Return Me._Language
    End Get
    Set(ByVal value As clsEnums.enmLanguage)
      If Me._Language <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Language = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [LanguageText]() As String
    Get
      Return Me._LanguageText
    End Get
    Set(ByVal value As String)
      Me._LanguageText = value
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
      End If 
    End Set
  End Property
  Public Property [InstanceUniqueText]() As String
    Get
      Return Me._InstanceUniqueText
    End Get
    Set(ByVal value As String)
      If Me._InstanceUniqueText <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._InstanceUniqueText = value 
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
    If _ObjectToTranslateID <> 0 Then pValue.Append("ObjectToTranslateID='" & _ObjectToTranslateID.ToString() & "' ‡ ") 
    If _ObjectToTranslateText <> "" Then pValue.Append("ObjectToTranslateText='" & _ObjectToTranslateText & "' ‡ ") 
    If _Instance <> 0 Then pValue.Append("Instance='" & _Instance.ToString() & "' ‡ ") 
    If _DefaultText <> "" Then pValue.Append("DefaultText='" & _DefaultText & "' ‡ ") 
    If _Language <> clsEnums.enmLanguage.UD Then pValue.Append("Language='" & _Language.FastToString() & "' ‡ ") 
    If _LanguageText <> "" Then pValue.Append("LanguageText='" & _LanguageText & "' ‡ ") 
    If _Text <> "" Then pValue.Append("Text='" & _Text & "' ‡ ") 
    If _InstanceUniqueText <> "" Then pValue.Append("InstanceUniqueText='" & _InstanceUniqueText & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append("," & _ObjectToTranslateID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_ObjectToTranslateText)}""") 
    pCSV.Append("," & _Instance.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DefaultText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Language.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LanguageText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Text)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_InstanceUniqueText)}""") 
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
  
  Public Sub New(ByVal vcsObjectTranslation As csObjectTranslation)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsObjectTranslation) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vObjectToTranslateID As Long = 0 _ 
    , Optional vObjectToTranslateText As String = "" _ 
    , Optional vInstance As Long = 0 _ 
    , Optional vDefaultText As String = "" _ 
    , Optional vLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.UD _ 
    , Optional vLanguageText As String = "" _ 
    , Optional vText As String = "" _ 
    , Optional vInstanceUniqueText As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _ObjectToTranslateID = vObjectToTranslateID 
    _ObjectToTranslateText = vObjectToTranslateText 
    _Instance = vInstance 
    _DefaultText = vDefaultText 
    _Language = vLanguage 
    _LanguageText = vLanguageText 
    _Text = vText 
    _InstanceUniqueText = vInstanceUniqueText 
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
 
    _DefaultText = _DefaultText.Truncate(pTruncateLength, _IsTruncated) 
    _Text = _Text.Truncate(pTruncateLength, _IsTruncated) 
    _InstanceUniqueText = _InstanceUniqueText.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ObjectTranslation by PrimaryKey (ID) 
  ''' </summary> 
  ''' <param name="vPrimaryKeyValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByPrimaryKey(ByVal vPrimaryKeyValue As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("PrimaryKeyValue={0}", vPrimaryKeyValue.ToString()) 
    Dim pFault As clsFault 
 
    Try 
      pFault = GetByID(vPrimaryKeyValue, vRequester, vMustExist) 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectTranslation-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [ObjectToTranslateIDAndInstanceAndLanguage] 
    [InstanceUniqueTextAndInstanceAndLanguage] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the ObjectTranslation by the chosen parameters. This function may be a bit slower than accessing the ObjectTranslation's GetBy... directly 
  ''' </summary> 
  ''' <param name="vParametersType"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Overrides Function GetByParameters(ByVal vParametersType As [Enum], ByVal vParameters As List(Of Object), ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = String.Format("ParametersType={0}", vParametersType.ToString()) 
    Dim pFault As clsFault 
 
    Dim pGetByOptions As enmGetByParameters = CType(vParametersType, enmGetByParameters) 
 
    Try 
      Select Case pGetByOptions 
        Case enmGetByParameters.ID 
          pFault = GetByID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.ObjectToTranslateIDAndInstanceAndLanguage 
          pFault = GetByObjectToTranslateIDAndInstanceAndLanguage(ccHelper.ToLong(vParameters(0)), ccHelper.ToLong(vParameters(1)), clsEnums.TranslateEnmLanguage(CStr(vParameters(2))), vRequester, vMustExist) 
        Case enmGetByParameters.InstanceUniqueTextAndInstanceAndLanguage 
          pFault = GetByInstanceUniqueTextAndInstanceAndLanguage(CStr(vParameters(0)), ccHelper.ToLong(vParameters(1)), clsEnums.TranslateEnmLanguage(CStr(vParameters(2))), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ObjectTranslation-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectTranslation-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the ObjectTranslation by ID. 
  ''' </summary>
  ''' <param name="vID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByID(ByVal vID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ID={0}", vID)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    If vID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
     
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vID 
          pBinaryWriter.Write(vID) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the ObjectTranslation 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the ObjectTranslation by ObjectToTranslateIDAndInstanceAndLanguage. 
  ''' </summary>
  ''' <param name="vObjectToTranslateID"></param>
  ''' <param name="vInstance"></param>
  ''' <param name="vLanguage"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectToTranslateID As Long, ByVal vInstance As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, Instance={1}, Language={2}", vObjectToTranslateID, vInstance, vLanguage)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstance 
          pBinaryWriter.Write(vInstance) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationGetByObjectToTranslateIDAndInstanceAndLanguage" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstanceAndLanguage: {vObjectToTranslateID};{vInstance};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the ObjectTranslation 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the ObjectTranslation by InstanceUniqueTextAndInstanceAndLanguage. 
  ''' </summary>
  ''' <param name="vInstanceUniqueText"></param>
  ''' <param name="vInstance"></param>
  ''' <param name="vLanguage"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueText As String, ByVal vInstance As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("InstanceUniqueText={0}, Instance={1}, Language={2}", vInstanceUniqueText, vInstance, vLanguage)
    Dim pFault As New clsFault 
    
    CreateEmpty() 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          'vInstanceUniqueText 
          If vInstanceUniqueText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueText) 
          ' 
          'vInstance 
          pBinaryWriter.Write(vInstance) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationGetByInstanceUniqueTextAndInstanceAndLanguage" 
      Dim pParametersToLog = $"InstanceUniqueTextAndInstanceAndLanguage: {vInstanceUniqueText};{vInstance};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the ObjectTranslation 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  Public Function AddUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityAddable.AddUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID <> 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ObjectTranslation-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-ObjectTranslation-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the ObjectTranslation. If there are parents or children in the ObjectTranslation, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectTranslation.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pObjectTranslation As New csObjectTranslation 
    If Me.isEqual(pObjectTranslation) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-ObjectTranslation-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-ObjectTranslation-240611-135714", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Dim pObjectAdded As Boolean = False 
    
    If _ID = 0 Then 
      pObjectAdded = True 
      RaiseEvent evtBeforeAdd(pCancel) 
      If pCancel = True Then Return pFault 
      RaiseEvent evtBeforeAddWithRequester(pCancel, vRequester, pFault) 
      If pFault.isOK = False Then Return pFault 
      If pCancel = True Then Return pFault 
    End If 
    RaiseEvent evtBeforeUpdate(enmUpdateType.Standard, pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeUpdateWithRequester(enmUpdateType.Standard, pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
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
    Dim pFunctionParameters As String = String.Format("ObjectTranslation.ID={0}", _ID)
    Dim pFault As New clsFault
    
    Dim pCancel As Boolean = False
    
    pFault.SetOK() 
    RaiseEvent evtBeforeDelete(pCancel) 
    If pCancel = True Then Return pFault 
    RaiseEvent evtBeforeDeleteWithRequester(pCancel, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    If pCancel = True Then Return pFault 
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_ID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterDelete()
    RaiseEvent evtAfterDeleteWithRequester(vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
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
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vID) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "csObjectTranslationDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csObjectTranslation) Then Return False 
    Dim pObjectTranslationToTest As csObjectTranslation = CType(vTargCCEntityToTest, csObjectTranslation) 
    Return isEqual(pObjectTranslationToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vObjectTranslationToTest As csObjectTranslation) As Boolean
    With vObjectTranslationToTest
      If _ID <> .ID Then Return False
      If _ObjectToTranslateID <> .ObjectToTranslateID Then Return False
      If _Instance <> .Instance Then Return False
      If _DefaultText <> .DefaultText Then Return False
      If _Language <> .Language Then Return False
      If _Text <> .Text Then Return False
      If _InstanceUniqueText <> .InstanceUniqueText Then Return False
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
    Dim pClone As New csObjectTranslation(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csObjectTranslation
    Dim pClone As New csObjectTranslation(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("ObjectToTranslateID") = _ObjectToTranslateID : Catch ex As Exception : Return pFault.LogException(ex, "ObjectToTranslateID", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("Instance") = _Instance : Catch ex As Exception : Return pFault.LogException(ex, "Instance", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("DefaultText") = _DefaultText : Catch ex As Exception : Return pFault.LogException(ex, "DefaultText", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("Language") = _Language : Catch ex As Exception : Return pFault.LogException(ex, "Language", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("Text") = _Text : Catch ex As Exception : Return pFault.LogException(ex, "Text", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("InstanceUniqueText") = _InstanceUniqueText : Catch ex As Exception : Return pFault.LogException(ex, "InstanceUniqueText", "TRGT-ObjectTranslation-130316-0852", vRequester) : End Try 
    Try : vDataRow("Tag") = _Tag : Catch ex As Exception : End Try 
    Try : vDataRow("DateAdded") = bDateAdded : Catch ex As Exception : Return pFault.LogException(ex, "DateAdded", "TRGT-TransactionLoad-130316-0852", vRequester) : End Try 
    bPrimaryKey = _ID
    CreateDefaultDesignation() 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pObjectTranslation As csObjectTranslation = CType(pXmlSerializer.Deserialize(pStreamReader), csObjectTranslation) 
      AssignValues(pObjectTranslation) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-ObjectTranslation-130515-1230", vRequester) 
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
          'ObjectToTranslateID 
          pBinaryWriter.Write(_ObjectToTranslateID) 
          'ObjectToTranslate 
          If _ObjectToTranslate IsNot Nothing Then 
            pObjectBytes = _ObjectToTranslate.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _ObjectToTranslateText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ObjectToTranslateText) 
          'Instance 
          pBinaryWriter.Write(_Instance) 
          'DefaultText 
          If _DefaultText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_DefaultText) 
          'Language 
          pBinaryWriter.Write(_Language.FastToString()) 
          'Text 
          If _Text Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Text) 
          'InstanceUniqueText 
          If _InstanceUniqueText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_InstanceUniqueText) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-150307-2338", vRequester) 
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
          'ObjectToTranslateID 
          _ObjectToTranslateID = pReader.ReadInt64 
          'ObjectToTranslate 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _ObjectToTranslate = New csObjectToTranslate(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ObjectToTranslateText = pReader.ReadString 
          'Instance 
          _Instance = pReader.ReadInt64 
          'DefaultText 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _DefaultText = pReader.ReadString 
          'Language 
          _Language = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'Text 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Text = pReader.ReadString 
          'InstanceUniqueText 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _InstanceUniqueText = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-ObjectTranslation-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-190720-1443", vRequester) 
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
 
      Dim pObjectTranslation As csObjectTranslation = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csObjectTranslation)(vJSON, pSettings) 
      AssignValues(pObjectTranslation) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vObjectTranslation As csObjectTranslation)
    With vObjectTranslation
      _ID = .ID 
      _ObjectToTranslateID = .ObjectToTranslateID 
      If .ObjectToTranslate IsNot Nothing Then 
        _ObjectToTranslate = .ObjectToTranslate.Clone() 
      End If 
      _ObjectToTranslateText = .ObjectToTranslateText 
      _Instance = .Instance 
      _DefaultText = .DefaultText 
      _Language = .Language 
      _LanguageText = .LanguageText
      _Text = .Text 
      _InstanceUniqueText = .InstanceUniqueText 
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
      'Language 
      pTextToGet = "LanguageText (Enum)" 
      _LanguageText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Language, _Language.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-ObjectTranslation-151124-1900", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' This loads the dependant Parent
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault 
    
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  Private Sub CreateEmpty()
    
    _ID = 0 
    _ObjectToTranslateID = 0
    _ObjectToTranslate = Nothing
    _ObjectToTranslateText = "."
    _Instance = 0
    _DefaultText = ""
    _Language = clsEnums.enmLanguage.UD
    _LanguageText = ""
    _Text = ""
    _InstanceUniqueText = ""
    _Tag = ""
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
  
Public Class csObjectTranslationCol
  Inherits cTargCCCollection(Of csObjectTranslation)
  Implements ITargCCCollectionUpdateable 
  
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
 
  Public Event evtAfterFill()
  Friend Event evtAfterFillWithRequester(ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  Private _Clean As List(Of Long) 
  
  Private _FilledFromSumOnTheFly As Boolean 
  
  Private _CollectionLock As New Object() 
  'Support for FindBys
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csObjectTranslation) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage As Dictionary(Of String, csObjectTranslation) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage As Boolean 
  Private Function CreateKeyForFindByObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectTranslation As csObjectTranslation) As String 
    With vObjectTranslation 
      Return .ObjectToTranslateID.ToString() & "|" & .Instance.ToString() & "|" & .Language.ToString()
    End With 
  End Function 
  Private _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage As Dictionary(Of String, csObjectTranslation) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage As Boolean 
  Private Function CreateKeyForFindByInstanceUniqueTextAndInstanceAndLanguage(ByVal vObjectTranslation As csObjectTranslation) As String 
    With vObjectTranslation 
      Return .InstanceUniqueText & "|" & .Instance.ToString() & "|" & .Language.ToString()
    End With 
  End Function 
   
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
 
    For Each pRow As csObjectTranslation In Me 
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
    pCSVTitle.Append(",""ObjectToTranslateID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""ObjectToTranslate (Text)""") 
    pCSVTitle.Append(",""Instance""") 
    pCSVTitle.Append(",""DefaultText""") 
    pCSVTitle.Append(",""Language" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Language (Text)""") 
    pCSVTitle.Append(",""Text""") 
    pCSVTitle.Append(",""InstanceUniqueText""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csObjectTranslation In Me 
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
 
  Public Overloads Sub Add(ByVal vObjectTranslation As csObjectTranslation) 
    SyncLock _CollectionLock 
      MyBase.Add(vObjectTranslation) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True 
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vObjectTranslation As csObjectTranslation) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vObjectTranslation) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True 
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vObjectTranslationCol As csObjectTranslationCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vObjectTranslationCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True 
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True 
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vObjectTranslation As csObjectTranslation) 
    SyncLock _CollectionLock 
      MyBase.Remove(vObjectTranslation) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True 
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csObjectTranslation) 
      
      For Each lObjectTranslation In Me 
        If lObjectTranslation.IsEmpty OrElse pTempDictionary.ContainsKey(lObjectTranslation.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lObjectTranslation.ID, lObjectTranslation) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lObjectTranslation.ToString, "TRGT-ObjectTranslation-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", ObjectTranslation:" & lObjectTranslation.ToString() & ", TRGT-ObjectTranslation-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadObjectToTranslateIDAndInstanceAndLanguages() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage' yet!
      Dim pTempDictionary As New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lObjectTranslation In Me 
        Try 
          Dim pObjectToTranslateIDAndInstanceAndLanguage As String = CreateKeyForFindByObjectToTranslateIDAndInstanceAndLanguage(lObjectTranslation) 
          If String.IsNullOrEmpty(pObjectToTranslateIDAndInstanceAndLanguage.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pObjectToTranslateIDAndInstanceAndLanguage)) Then 
            pTempDictionary.Add(pObjectToTranslateIDAndInstanceAndLanguage, lObjectTranslation) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lObjectTranslation.ToString, "TRGT-ObjectTranslation-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage:" & ex.Message & ", ObjectTranslation:" & lObjectTranslation.ToString() & ", TRGT-ObjectTranslation-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadInstanceUniqueTextAndInstanceAndLanguages() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage' yet!
      Dim pTempDictionary As New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lObjectTranslation In Me 
        Try 
          Dim pInstanceUniqueTextAndInstanceAndLanguage As String = CreateKeyForFindByInstanceUniqueTextAndInstanceAndLanguage(lObjectTranslation) 
          If String.IsNullOrEmpty(pInstanceUniqueTextAndInstanceAndLanguage.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pInstanceUniqueTextAndInstanceAndLanguage)) Then 
            pTempDictionary.Add(pInstanceUniqueTextAndInstanceAndLanguage, lObjectTranslation) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lObjectTranslation.ToString, "TRGT-ObjectTranslation-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage:" & ex.Message & ", ObjectTranslation:" & lObjectTranslation.ToString() & ", TRGT-ObjectTranslation-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = False
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
 
    For Each lObjectTranslation As csObjectTranslation In Me 
      lObjectTranslation.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [ObjectToTranslateID] 
    [ObjectToTranslateIDAndInstance] 
    [Language] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the ObjectTranslations by the chosen parameters. This function may be a bit slower than accessing the ObjectTranslation's FillBy... directly 
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
 
    Dim pGetByOptions As enmFillByParameterCombination = CType(vWhichParameterCombination, enmFillByParameterCombination) 
 
    Try 
      Select Case pGetByOptions 
        Case enmFillByParameterCombination.None_GetAll 
          pFault = Fill(vRequester) 
        Case enmFillByParameterCombination.ObjectToTranslateID 
          pFault = FillByObjectToTranslateID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.ObjectToTranslateIDAndInstance 
          pFault = FillByObjectToTranslateIDAndInstance(ccHelper.ToLong(vParameters(0)), ccHelper.ToLong(vParameters(1)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.Language 
          pFault = FillByLanguage(clsEnums.TranslateEnmLanguage(CStr(vParameters(0))), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-ObjectTranslation-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-ObjectTranslation-151223_1716", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets a collection of all the items, or a sub-collection defined by HowMany and Direction
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overrides Function Fill(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
    
    Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectToTranslateID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByObjectToTranslateID(ByVal vObjectToTranslateID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}", vObjectToTranslateID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByObjectToTranslateID" 
      Dim pParametersToLog = $"ObjectToTranslateID: {vObjectToTranslateID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectToTranslateID and Instance, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstance As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, Instance={1}", vObjectToTranslateID, vInstance)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstance 
          pBinaryWriter.Write(vInstance) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByObjectToTranslateIDAndInstance" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstance: {vObjectToTranslateID};{vInstance};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Language, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLanguage(ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Language={0}", vLanguage)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByLanguage" 
      Dim pParametersToLog = $"Language: {vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
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
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectToTranslateID and Instance, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, InstanceFrom={1}, InstanceTo={2}", vObjectToTranslateID, vInstanceFrom, vInstanceTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByBoundedObjectToTranslateIDAndInstance" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstance: {vObjectToTranslateID};{vInstanceFrom};{vInstanceTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ObjectToTranslateID and Instance and Language, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, InstanceFrom={1}, InstanceTo={2}, Language={3}", vObjectToTranslateID, vInstanceFrom, vInstanceTo, vLanguage)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByBoundedObjectToTranslateIDAndInstanceAndLanguage" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstanceAndLanguage: {vObjectToTranslateID};{vInstanceFrom};{vInstanceTo};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific InstanceUniqueText and Instance and Language, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueTextFrom As String, ByVal vInstanceUniqueTextTo As String, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("InstanceUniqueTextFrom={0}, InstanceUniqueTextTo={1}, InstanceFrom={2}, InstanceTo={3}, Language={4}", vInstanceUniqueTextFrom, vInstanceUniqueTextTo, vInstanceFrom, vInstanceTo, vLanguage)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vInstanceUniqueTextFrom 
          If vInstanceUniqueTextFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueTextFrom) 
          ' 
          'vInstanceUniqueTextTo 
          If vInstanceUniqueTextTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueTextTo) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByBoundedInstanceUniqueTextAndInstanceAndLanguage" 
      Dim pParametersToLog = $"InstanceUniqueTextAndInstanceAndLanguage: {vInstanceUniqueTextFrom};{vInstanceUniqueTextTo};{vInstanceFrom};{vInstanceTo};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>  
  ''' Gets a collection of all the items for the specified list of ID's. To append to an existing collection, set vAppend to true (default is false).  
  ''' An ID can only exist once in the collection. If it's already in the collection, it will be removed from vIDs before sending to the server. 
  ''' </summary>  
  ''' <param name="vIDs"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="vDir"></param>  
  ''' <param name="vAppend"></param>  
  ''' <returns></returns>  
  Public Function FillByListOfID(vIDs As List(Of Long), vRequester As clsRequester, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault 
    Dim pFunctionParameters As String = $"Count of IDs: {vIDs?.Count}" 
    Dim pFault As New clsFault 
 
    If vIDs Is Nothing Then Return pFault 
    If vIDs.Count = 0 Then Return pFault 
 
    'If it's append, we have to ensure no doubles, even though we're not sending the collection to the server 
    If vAppend = True Then 
      For Each l In Me 
        If vIDs.Contains(l.ID) Then 
          vIDs.Remove(l.ID) 
        End If 
      Next 
    End If 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDs 
          pBinaryWriter.Write(vIDs.Count) 
          For Each l In vIDs 
            pBinaryWriter.Write(l) 
          Next 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation   
      If vAppend = True Then 
        Dim pObjectTranslations As New csObjectTranslationCol 
        pObjectTranslations.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pObjectTranslations) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-231207-1750", vRequester) 
    End Try 
 
    pFault.SetOK() 
    RaiseEvent evtAfterFill() 
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault 
  End Function 
 
  Public Enum enmFillOnTheFlyParameters 
    UD 
    IDFrom
    IDTo
    [ObjectToTranslateID]
    InstanceFrom
    InstanceTo
    [Language]
    [InstanceUniqueText]
    InstanceUniqueTextWildcardType
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
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pObjectToTranslateID As Nullable(Of Long) = Nothing
    Dim pInstanceFrom As Nullable(Of Long) = Nothing
    Dim pInstanceTo As Nullable(Of Long) = Nothing
    Dim pLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.UD
    Dim pInstanceUniqueText As String = Nothing
    Dim pInstanceUniqueTextWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectToTranslateID) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectToTranslateID) : If pObj IsNot Nothing Then pObjectToTranslateID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceFrom) : If pObj IsNot Nothing Then pInstanceFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceTo) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceTo) : If pObj IsNot Nothing Then pInstanceTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Language) Then pObj = vParameters(enmFillOnTheFlyParameters.Language) : If pObj IsNot Nothing Then pLanguage = CType(pObj, clsEnums.enmLanguage) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceUniqueText) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceUniqueText) : If pObj IsNot Nothing Then pInstanceUniqueText = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceUniqueTextWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceUniqueTextWildcardType) : If pObj IsNot Nothing Then pInstanceUniqueTextWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pObjectToTranslateID _
        , pInstanceFrom, pInstanceTo _
        , pLanguage _
        , pInstanceUniqueText, pInstanceUniqueTextWildcardType _
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
        , ByVal vObjectToTranslateID As Nullable(Of Long) _
        , ByVal vInstanceFrom As Nullable(Of Long), ByVal vInstanceTo As Nullable(Of Long) _
        , ByVal vLanguage As clsEnums.enmLanguage _
        , ByVal vInstanceUniqueText As String, ByVal vInstanceUniqueTextWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ObjectToTranslateID={2}, InstanceFrom={3}, InstanceTo={4}, Language={5}, InstanceUniqueText={6}, InstanceUniqueTextWildcardType={7}", vIDFrom, vIDTo, vObjectToTranslateID, vInstanceFrom, vInstanceTo, vLanguage, vInstanceUniqueText, vInstanceUniqueTextWildcardType.FastToString())
    
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'ObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID.HasValue) 
          If vObjectToTranslateID.HasValue = True Then pBinaryWriter.Write(vObjectToTranslateID.Value) : pParametersToLog &= $"ObjectToTranslateID={vObjectToTranslateID};"  
          'Instance 
          pBinaryWriter.Write(vInstanceFrom.HasValue) 
          If vInstanceFrom.HasValue Then pBinaryWriter.Write(vInstanceFrom.Value) : pParametersToLog &= $"InstanceFrom={vInstanceFrom};"  
          pBinaryWriter.Write(vInstanceTo.HasValue) 
          If vInstanceTo.HasValue Then pBinaryWriter.Write(vInstanceTo.Value) : pParametersToLog &= $"InstanceTo={vInstanceTo};"  
          'Language 
          pBinaryWriter.Write(vLanguage.ToString()) : pParametersToLog &= $"Language={vLanguage};"  
          'InstanceUniqueText 
          If vInstanceUniqueText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueText) : pBinaryWriter.Write(vInstanceUniqueTextWildcardType.FastToString()) : pParametersToLog &= $"InstanceUniqueText={vInstanceUniqueText};" : pParametersToLog &= $"InstanceUniqueTextWildcardType={vInstanceUniqueTextWildcardType};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByObjectToTranslateID
    GroupByInstance
    GroupByLanguage
    GroupByInstanceUniqueText
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
 
    Dim pIDFrom As Nullable(Of Long) = Nothing
    Dim pIDTo As Nullable(Of Long) = Nothing
    Dim pObjectToTranslateID As Nullable(Of Long) = Nothing
    Dim pInstanceFrom As Nullable(Of Long) = Nothing
    Dim pInstanceTo As Nullable(Of Long) = Nothing
    Dim pLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.UD
    Dim pInstanceUniqueText As String = Nothing
    Dim pInstanceUniqueTextWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByObjectToTranslateID As Boolean = False
    Dim pGroupByInstance As Boolean = False
    Dim pGroupByLanguage As Boolean = False
    Dim pGroupByInstanceUniqueText As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ObjectToTranslateID) Then pObj = vParameters(enmFillOnTheFlyParameters.ObjectToTranslateID) : If pObj IsNot Nothing Then pObjectToTranslateID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceFrom) : If pObj IsNot Nothing Then pInstanceFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceTo) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceTo) : If pObj IsNot Nothing Then pInstanceTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Language) Then pObj = vParameters(enmFillOnTheFlyParameters.Language) : If pObj IsNot Nothing Then pLanguage = CType(pObj, clsEnums.enmLanguage) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceUniqueText) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceUniqueText) : If pObj IsNot Nothing Then pInstanceUniqueText = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.InstanceUniqueTextWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.InstanceUniqueTextWildcardType) : If pObj IsNot Nothing Then pInstanceUniqueTextWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByObjectToTranslateID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByObjectToTranslateID) : If pObj IsNot Nothing Then pGroupByObjectToTranslateID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByInstance) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByInstance) : If pObj IsNot Nothing Then pGroupByInstance = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLanguage) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLanguage) : If pObj IsNot Nothing Then pGroupByLanguage = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByInstanceUniqueText) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByInstanceUniqueText) : If pObj IsNot Nothing Then pGroupByInstanceUniqueText = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pObjectToTranslateID _
        , pInstanceFrom, pInstanceTo _
        , pLanguage _
        , pInstanceUniqueText, pInstanceUniqueTextWildcardType _
        , pGroupByObjectToTranslateID _
        , pGroupByInstance _
        , pGroupByLanguage _
        , pGroupByInstanceUniqueText _
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
        , ByVal vObjectToTranslateID As Nullable(Of Long) _
        , ByVal vInstanceFrom As Nullable(Of Long), ByVal vInstanceTo As Nullable(Of Long) _
        , ByVal vLanguage As clsEnums.enmLanguage _
        , ByVal vInstanceUniqueText As String, ByVal vInstanceUniqueTextWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByObjectToTranslateID As Boolean _
        , ByVal vGroupByInstance As Boolean _
        , ByVal vGroupByLanguage As Boolean _
        , ByVal vGroupByInstanceUniqueText As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, ObjectToTranslateID={2}, InstanceFrom={3}, InstanceTo={4}, Language={5}, InstanceUniqueText={6}, InstanceUniqueTextWildcardType={7}, GroupByObjectToTranslateID={8}, GroupByInstance={9}, GroupByLanguage={10}, GroupByInstanceUniqueText={11}", vIDFrom, vIDTo, vObjectToTranslateID, vInstanceFrom, vInstanceTo, vLanguage, vInstanceUniqueText, vInstanceUniqueTextWildcardType.FastToString(), vGroupByObjectToTranslateID, vGroupByInstance, vGroupByLanguage, vGroupByInstanceUniqueText)
    Dim pFault As New clsFault 
 
    Me.Clear() 
 
    Dim pParametersToLog = $"" 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'ID 
          pBinaryWriter.Write(vIDFrom.HasValue) 
          If vIDFrom.HasValue Then pBinaryWriter.Write(vIDFrom.Value) : pParametersToLog &= $"IDFrom={vIDFrom};"  
          pBinaryWriter.Write(vIDTo.HasValue) 
          If vIDTo.HasValue Then pBinaryWriter.Write(vIDTo.Value) : pParametersToLog &= $"IDTo={vIDTo};"  
          'ObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID.HasValue) 
          If vObjectToTranslateID.HasValue = True Then pBinaryWriter.Write(vObjectToTranslateID.Value) : pParametersToLog &= $"ObjectToTranslateID={vObjectToTranslateID};"  
          'Instance 
          pBinaryWriter.Write(vInstanceFrom.HasValue) 
          If vInstanceFrom.HasValue Then pBinaryWriter.Write(vInstanceFrom.Value) : pParametersToLog &= $"InstanceFrom={vInstanceFrom};"  
          pBinaryWriter.Write(vInstanceTo.HasValue) 
          If vInstanceTo.HasValue Then pBinaryWriter.Write(vInstanceTo.Value) : pParametersToLog &= $"InstanceTo={vInstanceTo};"  
          'Language 
          pBinaryWriter.Write(vLanguage.ToString()) : pParametersToLog &= $"Language={vLanguage};"  
          'InstanceUniqueText 
          If vInstanceUniqueText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueText) : pBinaryWriter.Write(vInstanceUniqueTextWildcardType.FastToString()) 
          pBinaryWriter.Write(vGroupByObjectToTranslateID) : pParametersToLog &= $"GroupByObjectToTranslateID={vGroupByObjectToTranslateID};"  
          pBinaryWriter.Write(vGroupByInstance) : pParametersToLog &= $"GroupByInstance={vGroupByInstance};"  
          pBinaryWriter.Write(vGroupByLanguage) : pParametersToLog &= $"GroupByLanguage={vGroupByLanguage};"  
          pBinaryWriter.Write(vGroupByInstanceUniqueText) : pParametersToLog &= $"GroupByInstanceUniqueText={vGroupByInstanceUniqueText};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslation  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vObjectTranslationArray As csObjectTranslation())
    Me.Clear()
    
    For Each pObjectTranslation As csObjectTranslation In vObjectTranslationArray
      Me.Add(pObjectTranslation)
      _Clean.Add(pObjectTranslation.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pObjectTranslation As New csObjectTranslation(pRow, vRequester, _WithParents) 
        Me.Add(pObjectTranslation) 
        _Clean.Add(pObjectTranslation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-ObjectTranslationCol-130315-2118", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Overrides Function CreateXML(ByRef rXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-130515-1300", vRequester) 
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
      Dim pObjectTranslations As csObjectTranslationCol = CType(pXmlSerializer.Deserialize(pStreamReader), csObjectTranslationCol) 
      For Each pObjectTranslation As csObjectTranslation In pObjectTranslations 
        Me.Add(pObjectTranslation) 
        _Clean.Add(pObjectTranslation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-ObjectTranslation-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-190720-1443", vRequester) 
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
 
      Dim pObjectTranslations As List(Of csObjectTranslation) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csObjectTranslation))(vJSON, pSettings) 
      For Each pObjectTranslation As csObjectTranslation In pObjectTranslations 
        Me.Add(pObjectTranslation) 
        _Clean.Add(pObjectTranslation.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-190720-2059", vRequester) 
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
          For Each lObjectTranslation As csObjectTranslation In Me 
            Dim pByte As Byte() = lObjectTranslation.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-ObjectTranslation-150307-2340", vRequester) 
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
            Dim pObjectTranslation As csObjectTranslation = New csObjectTranslation(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pObjectTranslation) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pObjectTranslation.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-ObjectTranslation-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pObjectTranslation As csObjectTranslation In Me 
      With pObjectTranslation 
        pFault = pObjectTranslation.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csObjectTranslationCol) Then Return False 
    Dim pObjectTranslationColToTest As csObjectTranslationCol = CType(vEntitiesToTest, csObjectTranslationCol) 
    Return isEqual(pObjectTranslationColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vObjectTranslationsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vObjectTranslationsToTest As csObjectTranslationCol) As Boolean
    If Me.Count <> vObjectTranslationsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vObjectTranslationsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pObjectTranslations._FilledFromSumOnTheFly = True
    
    For Each pObjectTranslation As csObjectTranslation In Me 
      Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
      pObjectTranslations.Add(pObjectTranslationClone) 
      If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
    Next 
    Return pObjectTranslations 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csObjectTranslationCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pObjectTranslations._FilledFromSumOnTheFly = True
    
    For Each pObjectTranslation As csObjectTranslation In Me
      Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
      pObjectTranslations.Add(pObjectTranslationClone)
      If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
    Next
    Return pObjectTranslations
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csObjectTranslationCol 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents)  
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectTranslation.ID > vIDFrom AndAlso pObjectTranslation.ID <= vIDTo) Then 
        Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
        pObjectTranslations.Add(pObjectTranslationClone) 
        If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
      End If 
    Next 
    Return pObjectTranslations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ObjectToTranslateID and Instance (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long) As csObjectTranslationCol 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents)  
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID) AndAlso (pObjectTranslation.Instance > vInstanceFrom AndAlso pObjectTranslation.Instance <= vInstanceTo) Then 
        Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
        pObjectTranslations.Add(pObjectTranslationClone) 
        If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
      End If 
    Next 
    Return pObjectTranslations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ObjectToTranslateID and Instance and Language (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage) As csObjectTranslationCol 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents)  
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID) AndAlso (pObjectTranslation.Instance > vInstanceFrom AndAlso pObjectTranslation.Instance <= vInstanceTo) AndAlso (pObjectTranslation.Language = vLanguage) Then 
        Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
        pObjectTranslations.Add(pObjectTranslationClone) 
        If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
      End If 
    Next 
    Return pObjectTranslations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by InstanceUniqueText and Instance and Language (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueTextFrom As String, ByVal vInstanceUniqueTextTo As String, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage) As csObjectTranslationCol 
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents)  
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList() 
      If (pObjectTranslation.InstanceUniqueText > vInstanceUniqueTextFrom AndAlso pObjectTranslation.InstanceUniqueText <= vInstanceUniqueTextTo) AndAlso (pObjectTranslation.Instance > vInstanceFrom AndAlso pObjectTranslation.Instance <= vInstanceTo) AndAlso (pObjectTranslation.Language = vLanguage) Then 
        Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
        pObjectTranslations.Add(pObjectTranslationClone) 
        If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
      End If 
    Next 
    Return pObjectTranslations 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueText As String, ByVal vInstanceUniqueTextWildcardType As clsEnums.enmWildCardType, ByVal vInstance As Long, ByVal vInstanceWildcardType As clsEnums.enmWildCardType, ByVal vLanguage As clsEnums.enmLanguage, ByVal vLanguageWildcardType As clsEnums.enmWildCardType) As csObjectTranslationCol 
    Dim pObjectTranslations As New csObjectTranslationCol 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vInstanceUniqueTextWildcardType = clsEnums.enmWildCardType.After Then 
        If pObjectTranslation.InstanceUniqueText.StartsWith(vInstanceUniqueText, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vInstanceUniqueTextWildcardType = clsEnums.enmWildCardType.Before Then 
        If pObjectTranslation.InstanceUniqueText.EndsWith(vInstanceUniqueText, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vInstanceUniqueTextWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pObjectTranslation.InstanceUniqueText.IndexOf(vInstanceUniqueText, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vInstanceUniqueTextWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vInstanceUniqueText.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pObjectTranslation.InstanceUniqueText.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone() 
      pObjectTranslations.Add(pObjectTranslationClone) 
    Next 
    Return pObjectTranslations 
  End Function 
  
  ''' <summary>
  ''' This loads the dependant parents for each of the rows and the 1 to 1 children
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadParents(ByVal vRequester As clsRequester) As clsFault
    _WithParents = clsEnums.enmLoadParent.EntireObject 
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault
    
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslationCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150314-1803", vRequester) 
    End Try 
 
    
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
  Public Function FindByID(ByVal vID As Long) As csObjectTranslation
    If Me.Count = 0 Then Return New csObjectTranslation 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
    
    Dim pObjectTranslation As csObjectTranslation = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pObjectTranslation) 
    If pObjectTranslation IsNot Nothing Then Return pObjectTranslation Else Return New csObjectTranslation() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectToTranslateID As Long, ByVal vInstance As Long, ByVal vLanguage As clsEnums.enmLanguage) As csObjectTranslation
    If Me.Count = 0 Then Return New csObjectTranslation 
    
    If _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = True Then LoadObjectToTranslateIDAndInstanceAndLanguages() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csObjectTranslation) = _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage 
    
    Dim pObjectTranslation As csObjectTranslation = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vObjectToTranslateID.ToString() & "|" & vInstance.ToString() & "|" & vLanguage.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pObjectTranslation) 
    If pObjectTranslation IsNot Nothing Then Return pObjectTranslation Else Return New csObjectTranslation() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueText As String, ByVal vInstance As Long, ByVal vLanguage As clsEnums.enmLanguage) As csObjectTranslation
    If Me.Count = 0 Then Return New csObjectTranslation 
    
    If _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = True Then LoadInstanceUniqueTextAndInstanceAndLanguages() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csObjectTranslation) = _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage 
    
    Dim pObjectTranslation As csObjectTranslation = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vInstanceUniqueText & "|" & vInstance.ToString() & "|" & vLanguage.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pObjectTranslation) 
    If pObjectTranslation IsNot Nothing Then Return pObjectTranslation Else Return New csObjectTranslation() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ObjectToTranslateID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByObjectToTranslateID(ByVal vObjectToTranslateID As Long) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByObjectToTranslateID with vObjectToTranslateID of {vObjectToTranslateID}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Instance
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByInstance(ByVal vInstance As Long) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.Instance = vInstance Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByInstance with vInstance of {vInstance}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.Instance = vInstance Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DefaultText
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDefaultText(ByVal vDefaultText As String) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDefaultText = vDefaultText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.DefaultText.ToLowerInvariant() = vDefaultText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDefaultText with vDefaultText of {vDefaultText}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.DefaultText.ToLowerInvariant() = vDefaultText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Language
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLanguage(ByVal vLanguage As clsEnums.enmLanguage) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.Language = vLanguage Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLanguage with vLanguage of {vLanguage}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.Language = vLanguage Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Text
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByText(ByVal vText As String) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vText = vText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.Text.ToLowerInvariant() = vText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByText with vText of {vText}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.Text.ToLowerInvariant() = vText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined InstanceUniqueText
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByInstanceUniqueText(ByVal vInstanceUniqueText As String) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vInstanceUniqueText = vInstanceUniqueText.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.InstanceUniqueText.ToLowerInvariant() = vInstanceUniqueText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByInstanceUniqueText with vInstanceUniqueText of {vInstanceUniqueText}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.InstanceUniqueText.ToLowerInvariant() = vInstanceUniqueText Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csObjectTranslation) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In pTempDist.Values
        If pObjectTranslation.Tag.ToLowerInvariant() = vTag Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.Tag.ToLowerInvariant() = vTag Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    
    Return pObjectTranslations
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ObjectToTranslateIDAndInstance
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstance As Long) As csObjectTranslationCol
    Dim pObjectTranslations As New csObjectTranslationCol(_WithParents) 
    pObjectTranslations._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pObjectTranslation As csObjectTranslation In _SortedDictionaryForFindByID.Values.ToList()
        If pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID AndAlso pObjectTranslation.Instance = vInstance Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csObjectTranslationCol = Me.Clone() 
      For Each pObjectTranslation As csObjectTranslation In pList 
        If pObjectTranslation.ObjectToTranslateID = vObjectToTranslateID AndAlso pObjectTranslation.Instance = vInstance Then
          Dim pObjectTranslationClone As csObjectTranslation = pObjectTranslation.Clone()
          pObjectTranslations.Add(pObjectTranslationClone)
          If Not _FilledFromSumOnTheFly Then pObjectTranslations._Clean.Add(pObjectTranslation.ID) 
        End If
      Next
    End If 
    Return pObjectTranslations
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
    For Each pObjectTranslation As csObjectTranslation In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pObjectTranslation.LoadDataRow(pRow, vRequester) 
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
 
    'Check for new rows 
    For Each p As csObjectTranslation In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As csObjectTranslation = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pObjectTranslationToKill As New csObjectTranslation 
        pObjectTranslationToKill.ID = pCleanID 
        pObjectTranslationToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pObjectTranslationToKill) 
      End If 
    Next 
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslationCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary> 
  ''' This takes an external collection and updates the found rows in the database. If a row is not found (has an ID of 0), it adds it. It will not delete any rows. Check the 'tag' of the returned collection to see if it was updated. 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function UpdateFromCollection(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault Implements ITargCCCollectionUpdateable.UpdateFromCollection 
    Dim pFunctionParameters As String = ""
 
    Dim pFault As New clsFault
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pByte As Byte() = CreateByteArray(pFault, vRequester) : If Not pFault.isOK Then Return pFault 
          pBinaryWriter.Write(pByte.Length) 
          pBinaryWriter.Write(pByte, 0, pByte.Length) 
          pBinaryWriter.Write(vReload) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csObjectTranslationColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the ObjectTranslationCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-150314-1803", vRequester) 
    End Try 
 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write("Dummy") 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectToTranslateID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByObjectToTranslateID(ByVal vObjectToTranslateID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}", vObjectToTranslateID)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByObjectToTranslateID" 
      Dim pParametersToLog = $"ObjectToTranslateID: {vObjectToTranslateID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectToTranslateIDAndInstance 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstance As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, Instance={1}", vObjectToTranslateID, vInstance)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstance 
          pBinaryWriter.Write(vInstance) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByObjectToTranslateIDAndInstance" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstance: {vObjectToTranslateID};{vInstance};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific Language 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLanguage(ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Language={0}", vLanguage)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByLanguage" 
      Dim pParametersToLog = $"Language: {vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ObjectTranslation-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vIDFrom 
          pBinaryWriter.Write(vIDFrom) 
          ' 
          'vIDTo 
          pBinaryWriter.Write(vIDTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectToTranslateIDAndInstance
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedObjectToTranslateIDAndInstance(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, InstanceFrom={1}, InstanceTo={2}", vObjectToTranslateID, vInstanceFrom, vInstanceTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByBoundedObjectToTranslateIDAndInstance" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstance: {vObjectToTranslateID};{vInstanceFrom};{vInstanceTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ObjectToTranslateIDAndInstanceAndLanguage
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedObjectToTranslateIDAndInstanceAndLanguage(ByVal vObjectToTranslateID As Long, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ObjectToTranslateID={0}, InstanceFrom={1}, InstanceTo={2}, Language={3}", vObjectToTranslateID, vInstanceFrom, vInstanceTo, vLanguage)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vObjectToTranslateID 
          pBinaryWriter.Write(vObjectToTranslateID) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByBoundedObjectToTranslateIDAndInstanceAndLanguage" 
      Dim pParametersToLog = $"ObjectToTranslateIDAndInstanceAndLanguage: {vObjectToTranslateID};{vInstanceFrom};{vInstanceTo};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific InstanceUniqueTextAndInstanceAndLanguage
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedInstanceUniqueTextAndInstanceAndLanguage(ByVal vInstanceUniqueTextFrom As String, ByVal vInstanceUniqueTextTo As String, ByVal vInstanceFrom As Long, ByVal vInstanceTo As Long, ByVal vLanguage As clsEnums.enmLanguage, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("InstanceUniqueTextFrom={0}, InstanceUniqueTextTo={1}, InstanceFrom={2}, InstanceTo={3}, Language={4}", vInstanceUniqueTextFrom, vInstanceUniqueTextTo, vInstanceFrom, vInstanceTo, vLanguage)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vInstanceUniqueTextFrom 
          If vInstanceUniqueTextFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueTextFrom) 
          ' 
          'vInstanceUniqueTextTo 
          If vInstanceUniqueTextTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vInstanceUniqueTextTo) 
          ' 
          'vInstanceFrom 
          pBinaryWriter.Write(vInstanceFrom) 
          ' 
          'vInstanceTo 
          pBinaryWriter.Write(vInstanceTo) 
          ' 
          'vLanguage 
          pBinaryWriter.Write(vLanguage.ToString()) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csObjectTranslationColDeleteByBoundedInstanceUniqueTextAndInstanceAndLanguage" 
      Dim pParametersToLog = $"InstanceUniqueTextAndInstanceAndLanguage: {vInstanceUniqueTextFrom};{vInstanceUniqueTextTo};{vInstanceFrom};{vInstanceTo};{vLanguage};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csObjectTranslationCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
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
  
  Public Sub SortByObjectToTranslateID()
    Me.Sort(New csObjectTranslationCol.CompareByObjectToTranslateID)
  End Sub
  Private Class CompareByObjectToTranslateID
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ObjectToTranslateID < y.ObjectToTranslateID Then
        Return -1
      ElseIf x.ObjectToTranslateID = y.ObjectToTranslateID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByObjectToTranslateText()
    Me.Sort(New csObjectTranslationCol.CompareByObjectToTranslateText)
  End Sub
  Private Class CompareByObjectToTranslateText
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ObjectToTranslateText, y.ObjectToTranslateText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByInstance()
    Me.Sort(New csObjectTranslationCol.CompareByInstance)
  End Sub
  Private Class CompareByInstance
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Instance < y.Instance Then
        Return -1
      ElseIf x.Instance = y.Instance Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDefaultText()
    Me.Sort(New csObjectTranslationCol.CompareByDefaultText)
  End Sub
  Private Class CompareByDefaultText
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.DefaultText, y.DefaultText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLanguage()
    Me.Sort(New csObjectTranslationCol.CompareByLanguage)
  End Sub
  Private Class CompareByLanguage
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Language < y.Language Then
        Return -1
      ElseIf x.Language = y.Language Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLanguageText()
    Me.Sort(New csObjectTranslationCol.CompareByLanguageText)
  End Sub
  Private Class CompareByLanguageText
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LanguageText, y.LanguageText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByText()
    Me.Sort(New csObjectTranslationCol.CompareByText)
  End Sub
  Private Class CompareByText
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Text, y.Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByInstanceUniqueText()
    Me.Sort(New csObjectTranslationCol.CompareByInstanceUniqueText)
  End Sub
  Private Class CompareByInstanceUniqueText
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.InstanceUniqueText, y.InstanceUniqueText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csObjectTranslationCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csObjectTranslation)
    Private Function Compare(ByVal x As csObjectTranslation, ByVal y As csObjectTranslation) As Integer Implements System.Collections.Generic.IComparer(Of csObjectTranslation).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Private Overloads Sub Clear() 
    MyBase.Clear() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csObjectTranslation) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = False 
    _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csObjectTranslation) 
    _SortedDictionaryForFindByObjectToTranslateIDAndInstanceAndLanguage = New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByInstanceUniqueTextAndInstanceAndLanguage = New Dictionary(Of String, csObjectTranslation)(StringComparer.OrdinalIgnoreCase) 
 
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
  
