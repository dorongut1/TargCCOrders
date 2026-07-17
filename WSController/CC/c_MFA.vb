Public Class csMFA
  Inherits cTargCCEntity 
  Implements ITargCCEntityAddable 
  Implements ITargCCEntityEditable 
 
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
    [UILang] 
    [User] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [CellOrEmail] 
    [ProtectedFunction] 
    [CodeHashed] 
    [AttemptNo] 
    [IsSuccessful] 
    [LastAccessingIP] 
    [LastAccessingCountry] 
    [UILang] 
    [WhenCreated] 
    [WhenAccessed] 
    [WhenExpires] 
    [Details] 
    [User] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [AttemptNo] 
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
  
  Private Event evtOverrideDefaultDesignation(ByRef rOverridenValue As String) 
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _CellOrEmail As String
  Private _ProtectedFunction As String
  Private _CodeHashed As String
  Private _AttemptNo As Integer
  Private _IsSuccessful As Boolean
  Private _LastAccessingIP As String
  Private _LastAccessingCountry As String
  Private _UILang As clsEnums.enmLanguage
  Private _UILangText As String 
  Private _WhenCreated As DateTimeOffset
  Private _WhenAccessed As DateTimeOffset
  Private _WhenExpires As DateTimeOffset
  Private _Details As String
  Private _UserID As Long
  Private _User As csUser
  Private _UserText As String
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
  Public Property [CellOrEmail]() As String
    Get
      Return Me._CellOrEmail
    End Get
    Set(ByVal value As String)
      If Me._CellOrEmail <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CellOrEmail = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [ProtectedFunction]() As String
    Get
      Return Me._ProtectedFunction
    End Get
    Set(ByVal value As String)
      If Me._ProtectedFunction <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProtectedFunction = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' If you want to hash (SHA256) the input, then prefix it with 'PleaseHash'. Otherwise, use ccHelper.Encrypt(ccHelper.enmHashType.SHA256, ValueToHash) 
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [CodeHashed]() As String
    Get
      Return Me._CodeHashed
    End Get
    Set(ByVal value As String)
      If Me._CodeHashed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
         If value.StartsWith("PleaseHash", StringComparison.OrdinalIgnoreCase) Then value = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, value.Substring(10)) 
        Me._CodeHashed = value 
      End If 
    End Set
  End Property
  Public Property [AttemptNo]() As Integer
    Get
      Return Me._AttemptNo
    End Get
    Set(ByVal value As Integer)
      If Me._AttemptNo <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AttemptNo = value 
      End If 
    End Set
  End Property
  Public Property [IsSuccessful]() As Boolean
    Get
      Return Me._IsSuccessful
    End Get
    Set(ByVal value As Boolean)
      If Me._IsSuccessful <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsSuccessful = value 
      End If 
    End Set
  End Property
  Public Property [LastAccessingIP]() As String
    Get
      Return Me._LastAccessingIP
    End Get
    Set(ByVal value As String)
      If Me._LastAccessingIP <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastAccessingIP = value 
      End If 
    End Set
  End Property
  Public Property [LastAccessingCountry]() As String
    Get
      Return Me._LastAccessingCountry
    End Get
    Set(ByVal value As String)
      If Me._LastAccessingCountry <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastAccessingCountry = value 
      End If 
    End Set
  End Property
  Public Property [UILang]() As clsEnums.enmLanguage
    Get
      Return Me._UILang
    End Get
    Set(ByVal value As clsEnums.enmLanguage)
      If Me._UILang <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UILang = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [UILangText]() As String
    Get
      Return Me._UILangText
    End Get
    Set(ByVal value As String)
      Me._UILangText = value
    End Set
  End Property
  Public Property [WhenCreated]() As DateTimeOffset
    Get
      Return Me._WhenCreated
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._WhenCreated <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenCreated = value 
      End If 
    End Set
  End Property
  Public Property [WhenAccessed]() As DateTimeOffset
    Get
      Return Me._WhenAccessed
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._WhenAccessed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenAccessed = value 
      End If 
    End Set
  End Property
  Public Property [WhenExpires]() As DateTimeOffset
    Get
      Return Me._WhenExpires
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._WhenExpires <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._WhenExpires = value 
      End If 
    End Set
  End Property
  Public Property [Details]() As String
    Get
      Return Me._Details
    End Get
    Set(ByVal value As String)
      If Me._Details <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Details = value 
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
    If pOverridenValue = Nothing Then bDefaultDesignation = _CellOrEmail Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _CellOrEmail <> "" Then pValue.Append("CellOrEmail='" & _CellOrEmail & "' ‡ ") 
    If _ProtectedFunction <> "" Then pValue.Append("ProtectedFunction='" & _ProtectedFunction & "' ‡ ") 
    If _CodeHashed <> "" Then pValue.Append("Code='*****' ‡ ") 
    If _AttemptNo <> 0 Then pValue.Append("AttemptNo='" & _AttemptNo.ToString() & "' ‡ ") 
    pValue.Append("IsSuccessful='" & _IsSuccessful.ToString() & "' ‡ ") 
    If _LastAccessingIP <> "" Then pValue.Append("LastAccessingIP='" & _LastAccessingIP & "' ‡ ") 
    If _LastAccessingCountry <> "" Then pValue.Append("LastAccessingCountry='" & _LastAccessingCountry & "' ‡ ") 
    If _UILang <> clsEnums.enmLanguage.UD Then pValue.Append("UILang='" & _UILang.FastToString() & "' ‡ ") 
    If _UILangText <> "" Then pValue.Append("UILangText='" & _UILangText & "' ‡ ") 
    If Not (_WhenCreated = Nothing) Then pValue.Append("WhenCreated='" & _WhenCreated.ToString("o") & "' ‡ ") 
    If Not (_WhenAccessed = Nothing) Then pValue.Append("WhenAccessed='" & _WhenAccessed.ToString("o") & "' ‡ ") 
    If Not (_WhenExpires = Nothing) Then pValue.Append("WhenExpires='" & _WhenExpires.ToString("o") & "' ‡ ") 
    If _Details <> "" Then pValue.Append("Details='" & _Details & "' ‡ ") 
    If _UserID <> 0 Then pValue.Append("UserID='" & _UserID.ToString() & "' ‡ ") 
    If _UserText <> "" Then pValue.Append("UserText='" & _UserText & "' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CellOrEmail)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProtectedFunction)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append("," & _AttemptNo.ToString() & "") 
    pCSV.Append(",""" & _IsSuccessful.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastAccessingIP)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastAccessingCountry)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UILang.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UILangText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenCreated.DateTime.ToShortDateString & " " & _WhenCreated.DateTime.ToShortTimeString & " " & _WhenCreated.Offset.TotalMinutes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenAccessed.DateTime.ToShortDateString & " " & _WhenAccessed.DateTime.ToShortTimeString & " " & _WhenAccessed.Offset.TotalMinutes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_WhenExpires.DateTime.ToShortDateString & " " & _WhenExpires.DateTime.ToShortTimeString & " " & _WhenExpires.Offset.TotalMinutes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Details)}""") 
    pCSV.Append("," & _UserID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_UserText)}""") 
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
  
  Public Sub New(ByVal vcsMFA As csMFA)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsMFA) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vCellOrEmail As String = "" _ 
    , Optional vProtectedFunction As String = "" _ 
    , Optional vCodeHashed As String = "" _ 
    , Optional vAttemptNo As Integer = 0 _ 
    , Optional vIsSuccessful As Boolean = False _ 
    , Optional vLastAccessingIP As String = "" _ 
    , Optional vLastAccessingCountry As String = "" _ 
    , Optional vUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD _ 
    , Optional vUILangText As String = "" _ 
    , Optional vWhenCreated As DateTimeOffset = Nothing _ 
    , Optional vWhenAccessed As DateTimeOffset = Nothing _ 
    , Optional vWhenExpires As DateTimeOffset = Nothing _ 
    , Optional vDetails As String = "" _ 
    , Optional vUserID As Long = 0 _ 
    , Optional vUserText As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _CellOrEmail = vCellOrEmail 
    _ProtectedFunction = vProtectedFunction 
    _CodeHashed = vCodeHashed 
    _AttemptNo = vAttemptNo 
    _IsSuccessful = vIsSuccessful 
    _LastAccessingIP = vLastAccessingIP 
    _LastAccessingCountry = vLastAccessingCountry 
    _UILang = vUILang 
    _UILangText = vUILangText 
    _WhenCreated = vWhenCreated 
    _WhenAccessed = vWhenAccessed 
    _WhenExpires = vWhenExpires 
    _Details = vDetails 
    _UserID = vUserID 
    _UserText = vUserText 
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
 
    _CellOrEmail = _CellOrEmail.Truncate(pTruncateLength, _IsTruncated) 
    _ProtectedFunction = _ProtectedFunction.Truncate(pTruncateLength, _IsTruncated) 
    _CodeHashed = _CodeHashed.Truncate(pTruncateLength, _IsTruncated) 
    _LastAccessingIP = _LastAccessingIP.Truncate(pTruncateLength, _IsTruncated) 
    _LastAccessingCountry = _LastAccessingCountry.Truncate(pTruncateLength, _IsTruncated) 
    _Details = _Details.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the MFA by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-MFA-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [UserID] 
    [UserIDAndCellOrEmail] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the MFA by the chosen parameters. This function may be a bit slower than accessing the MFA's GetBy... directly 
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
        Case enmGetByParameters.UserID 
          pFault = GetByUserID(ccHelper.ToLong(vParameters(0)), vRequester, vMustExist) 
        Case enmGetByParameters.UserIDAndCellOrEmail 
          pFault = GetByUserIDAndCellOrEmail(ccHelper.ToLong(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-MFA-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-MFA-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the MFA by ID. 
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
      Dim pFunction As String = "csMFAGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the MFA 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the MFA by UserID. 
  ''' </summary>
  ''' <param name="vUserID"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByUserID(ByVal vUserID As Long, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}", vUserID)
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
          'vUserID 
          pBinaryWriter.Write(vUserID) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csMFAGetByUserID" 
      Dim pParametersToLog = $"UserID: {vUserID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the MFA 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the MFA by UserIDAndCellOrEmail. 
  ''' </summary>
  ''' <param name="vUserID"></param>
  ''' <param name="vCellOrEmail"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByUserIDAndCellOrEmail(ByVal vUserID As Long, ByVal vCellOrEmail As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}, CellOrEmail={1}", vUserID, vCellOrEmail)
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
          'vUserID 
          pBinaryWriter.Write(vUserID) 
          ' 
          'vCellOrEmail 
          If vCellOrEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCellOrEmail) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csMFAGetByUserIDAndCellOrEmail" 
      Dim pParametersToLog = $"UserIDAndCellOrEmail: {vUserID};{vCellOrEmail};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the MFA 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150308-1015", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-MFA-151227-1738", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-MFA-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the MFA. If there are parents or children in the MFA, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("MFA.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pMFA As New csMFA 
    If Me.isEqual(pMFA) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-MFA-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-MFA-240611-135714", vRequester) 
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
      Dim pFunction As String = "csMFAUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150314-1803", vRequester) 
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
  
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csMFA) Then Return False 
    Dim pMFAToTest As csMFA = CType(vTargCCEntityToTest, csMFA) 
    Return isEqual(pMFAToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vMFAToTest As csMFA) As Boolean
    With vMFAToTest
      If _ID <> .ID Then Return False
      If _CellOrEmail <> .CellOrEmail Then Return False
      If _ProtectedFunction <> .ProtectedFunction Then Return False
      If _CodeHashed <> .CodeHashed Then Return False
      If _AttemptNo <> .AttemptNo Then Return False
      If _IsSuccessful <> .IsSuccessful Then Return False
      If _LastAccessingIP <> .LastAccessingIP Then Return False
      If _LastAccessingCountry <> .LastAccessingCountry Then Return False
      If _UILang <> .UILang Then Return False
      If _WhenCreated <> Nothing AndAlso .WhenCreated <> Nothing Then 
        If ccHelper.ToLong(_WhenCreated.Subtract(.WhenCreated).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenCreated = Nothing AndAlso .WhenCreated = Nothing) Then 
        Return False 
      End If 
      If _WhenAccessed <> Nothing AndAlso .WhenAccessed <> Nothing Then 
        If ccHelper.ToLong(_WhenAccessed.Subtract(.WhenAccessed).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenAccessed = Nothing AndAlso .WhenAccessed = Nothing) Then 
        Return False 
      End If 
      If _WhenExpires <> Nothing AndAlso .WhenExpires <> Nothing Then 
        If ccHelper.ToLong(_WhenExpires.Subtract(.WhenExpires).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_WhenExpires = Nothing AndAlso .WhenExpires = Nothing) Then 
        Return False 
      End If 
      If _Details <> .Details Then Return False
      If _UserID <> .UserID Then Return False
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
    Dim pClone As New csMFA(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csMFA
    Dim pClone As New csMFA(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("CellOrEmail") = _CellOrEmail : Catch ex As Exception : Return pFault.LogException(ex, "CellOrEmail", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProtectedFunction") = _ProtectedFunction : Catch ex As Exception : Return pFault.LogException(ex, "ProtectedFunction", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("CodeHashed") = _CodeHashed : Catch ex As Exception : Return pFault.LogException(ex, "CodeHashed", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("AttemptNo") = _AttemptNo : Catch ex As Exception : Return pFault.LogException(ex, "AttemptNo", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsSuccessful") = _IsSuccessful : Catch ex As Exception : Return pFault.LogException(ex, "IsSuccessful", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastAccessingIP") = _LastAccessingIP : Catch ex As Exception : Return pFault.LogException(ex, "LastAccessingIP", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastAccessingCountry") = _LastAccessingCountry : Catch ex As Exception : Return pFault.LogException(ex, "LastAccessingCountry", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("UILang") = _UILang : Catch ex As Exception : Return pFault.LogException(ex, "UILang", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenCreated") = _WhenCreated : Catch ex As Exception : Return pFault.LogException(ex, "WhenCreated", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenAccessed") = _WhenAccessed : Catch ex As Exception : Return pFault.LogException(ex, "WhenAccessed", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("WhenExpires") = _WhenExpires : Catch ex As Exception : Return pFault.LogException(ex, "WhenExpires", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("Details") = _Details : Catch ex As Exception : Return pFault.LogException(ex, "Details", "TRGT-MFA-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserID") = _UserID : Catch ex As Exception : Return pFault.LogException(ex, "UserID", "TRGT-MFA-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pMFA As csMFA = CType(pXmlSerializer.Deserialize(pStreamReader), csMFA) 
      AssignValues(pMFA) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-MFA-130515-1230", vRequester) 
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
          'CellOrEmail 
          If _CellOrEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CellOrEmail) 
          'ProtectedFunction 
          If _ProtectedFunction Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProtectedFunction) 
          'CodeHashed 
          If _CodeHashed Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CodeHashed) 
          'AttemptNo 
          pBinaryWriter.Write(_AttemptNo) 
          'IsSuccessful 
          pBinaryWriter.Write(_IsSuccessful) 
          'LastAccessingIP 
          If _LastAccessingIP Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastAccessingIP) 
          'LastAccessingCountry 
          If _LastAccessingCountry Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastAccessingCountry) 
          'UILang 
          pBinaryWriter.Write(_UILang.FastToString()) 
          'WhenCreated 
          pBinaryWriter.Write(_WhenCreated.DateTime.Ticks) 
          pBinaryWriter.Write(_WhenCreated.Offset.Ticks) 
          'WhenAccessed 
          pBinaryWriter.Write(_WhenAccessed.DateTime.Ticks) 
          pBinaryWriter.Write(_WhenAccessed.Offset.Ticks) 
          'WhenExpires 
          pBinaryWriter.Write(_WhenExpires.DateTime.Ticks) 
          pBinaryWriter.Write(_WhenExpires.Offset.Ticks) 
          'Details 
          If _Details Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Details) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-MFA-150307-2338", vRequester) 
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
          'CellOrEmail 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CellOrEmail = pReader.ReadString 
          'ProtectedFunction 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProtectedFunction = pReader.ReadString 
          'CodeHashed 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CodeHashed = pReader.ReadString 
          'AttemptNo 
          _AttemptNo = pReader.ReadInt32 
          'IsSuccessful 
          _IsSuccessful = pReader.ReadBoolean 
          'LastAccessingIP 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastAccessingIP = pReader.ReadString 
          'LastAccessingCountry 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastAccessingCountry = pReader.ReadString 
          'UILang 
          _UILang = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'WhenCreated 
          _WhenCreated = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'WhenAccessed 
          _WhenAccessed = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'WhenExpires 
          _WhenExpires = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'Details 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Details = pReader.ReadString 
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
      rFault.LogException(ex, "", "TRGT-MFA-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-190720-1443", vRequester) 
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
 
      Dim pMFA As csMFA = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csMFA)(vJSON, pSettings) 
      AssignValues(pMFA) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vMFA As csMFA)
    With vMFA
      _ID = .ID 
      _CellOrEmail = .CellOrEmail 
      _ProtectedFunction = .ProtectedFunction 
      _CodeHashed = .CodeHashed 
      _AttemptNo = .AttemptNo 
      _IsSuccessful = .IsSuccessful 
      _LastAccessingIP = .LastAccessingIP 
      _LastAccessingCountry = .LastAccessingCountry 
      _UILang = .UILang 
      _UILangText = .UILangText
      _WhenCreated = .WhenCreated 
      _WhenAccessed = .WhenAccessed 
      _WhenExpires = .WhenExpires 
      _Details = .Details 
      _UserID = .UserID 
      If .User IsNot Nothing Then 
        _User = .User.Clone() 
      End If 
      _UserText = .UserText 
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
      'UILang 
      pTextToGet = "UILangText (Enum)" 
      _UILangText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Language, _UILang.FastToString(), vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-MFA-151124-1900", vRequester) 
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
      Dim pFunction As String = "csMFALoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  Private Sub CreateEmpty()
    
    _ID = 0 
    _CellOrEmail = ""
    _ProtectedFunction = ""
    _CodeHashed = ""
    _AttemptNo = 0
    _IsSuccessful = False
    _LastAccessingIP = ""
    _LastAccessingCountry = ""
    _UILang = clsEnums.enmLanguage.UD
    _UILangText = ""
    _WhenCreated = Nothing
    _WhenAccessed = Nothing
    _WhenExpires = Nothing
    _Details = ""
    _UserID = 0
    _User = Nothing
    _UserText = "."
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
 
  ''' <summary>  
  ''' The code is valid for a default of 5 minutes and 3 attempts. <br/> 
  ''' The SMSAppHash can be either 0 (none), 1 or 2. They are set in the SystemDefinition Screen <br/> 
  ''' If the UserID is provided, then the CellOrEmail is ignored if provided<br/>  
  ''' Details is text that is returned when calling CheckMFA<br/>  
  ''' rMessageMethod returns how the message is sent <br/> 
  ''' </summary>  
  ''' <param name="vUserID"></param>  
  ''' <param name="vCellOrEmail"></param>  
  ''' <param name="vProtectedFunction"></param>  
  ''' <param name="vDetails"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="rMessageMethod"></param>  
  ''' <param name="vNumDigits"></param>  
  ''' <param name="vUILang"></param>  
  ''' <returns></returns>  
  Public Shared Function SetMFA(vUserID As Long, vCellOrEmail As String, vProtectedFunction As String, vDetails As String, vRequester As clsRequester, ByRef rMessageMethod As clsEnums.enmMessagingMode, Optional vNumDigits As Integer = 6, Optional vUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As clsFault 
    Dim pFunctionParameters As String = $"CellOrEmail: {vCellOrEmail}, ProtectedFunction: {vProtectedFunction}, UILang: {vUILang.FastToString()}" 
    Dim pFault As clsFault 
 
    rMessageMethod = clsEnums.enmMessagingMode.UD 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vUserID) 
          pBinaryWriter.Write(vCellOrEmail) 
          pBinaryWriter.Write(vProtectedFunction) 
          pBinaryWriter.Write(vDetails) 
          pBinaryWriter.Write(vNumDigits) 
          pBinaryWriter.Write(vUILang.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "csMFASetMFA" 
      Dim pParametersToLog = $"UserID: {vUserID}; CellOrEmail: {vCellOrEmail}; ProtectedFunction: {vProtectedFunction}; ProtectedFunction: {vProtectedFunction}; " 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value   
      rMessageMethod = clsEnums.TranslateEnmMessagingMode(System.Text.Encoding.ASCII.GetString(pResponse)) 
    Catch ex As Exception 
      pFault = New clsFault() 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1330", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function CheckMFA(vUserID As Long, vCellOrEmail As String, vProtectedFunction As String, vCode As String, vAccessingIP As String, vAccessingCountry As String, vRequester As clsRequester, ByRef rDetails As String) As clsFault 
    Dim pFunctionParameters As String = $"CellOrEmail: {vCellOrEmail}, ProtectedFunction: {vProtectedFunction}, Code: {vCode}, AccessingIP: {vAccessingIP}, AccessingCountry: {vAccessingCountry}" 
    Dim pFault As clsFault 
 
    rDetails = "" 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vUserID) 
          pBinaryWriter.Write(vCellOrEmail) 
          pBinaryWriter.Write(vProtectedFunction) 
          pBinaryWriter.Write(vCode) 
          pBinaryWriter.Write(vAccessingIP) 
          pBinaryWriter.Write(vAccessingCountry) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "csMFACheckMFA" 
      Dim pParametersToLog = $"UserID: {vUserID}; CellOrEmail: {vCellOrEmail}; ProtectedFunction: {vProtectedFunction}; ProtectedFunction: {vProtectedFunction}; " 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value   
      rDetails = System.Text.Encoding.ASCII.GetString(pResponse) 
    Catch ex As Exception 
      pFault = New clsFault() 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1330", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
End Class 
  
Public Class csMFACol
  Inherits cTargCCCollection(Of csMFA)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csMFA) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByUserID As Dictionary(Of String, csMFA) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByUserID As Boolean 
  Private Function CreateKeyForFindByUserID(ByVal vMFA As csMFA) As String 
    With vMFA 
      Return .UserID.ToString()
    End With 
  End Function 
  Private _SortedDictionaryForFindByUserIDAndCellOrEmail As Dictionary(Of String, csMFA) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByUserIDAndCellOrEmail As Boolean 
  Private Function CreateKeyForFindByUserIDAndCellOrEmail(ByVal vMFA As csMFA) As String 
    With vMFA 
      Return .UserID.ToString() & "|" & .CellOrEmail
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
 
    For Each pRow As csMFA In Me 
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
    pCSVTitle.Append(",""CellOrEmail""") 
    pCSVTitle.Append(",""ProtectedFunction""") 
    pCSVTitle.Append(",""CodeHashed""") 
    pCSVTitle.Append(",""AttemptNo""") 
    pCSVTitle.Append(",""IsSuccessful""") 
    pCSVTitle.Append(",""LastAccessingIP""") 
    pCSVTitle.Append(",""LastAccessingCountry""") 
    pCSVTitle.Append(",""UILang" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""UILang (Text)""") 
    pCSVTitle.Append(",""WhenCreated""") 
    pCSVTitle.Append(",""WhenAccessed""") 
    pCSVTitle.Append(",""WhenExpires""") 
    pCSVTitle.Append(",""Details""") 
    pCSVTitle.Append(",""UserID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""User (Text)""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csMFA In Me 
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
 
  Public Overloads Sub Add(ByVal vMFA As csMFA) 
    SyncLock _CollectionLock 
      MyBase.Add(vMFA) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserID = True 
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vMFA As csMFA) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vMFA) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserID = True 
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vMFACol As csMFACol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vMFACol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserID = True 
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserID = True 
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vMFA As csMFA) 
    SyncLock _CollectionLock 
      MyBase.Remove(vMFA) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserID = True 
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csMFA) 
      
      For Each lMFA In Me 
        If lMFA.IsEmpty OrElse pTempDictionary.ContainsKey(lMFA.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lMFA.ID, lMFA) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lMFA.ToString, "TRGT-MFA-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", MFA:" & lMFA.ToString() & ", TRGT-MFA-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadUserIDs() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByUserID Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByUserID Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByUserID = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByUserID' yet!
      Dim pTempDictionary As New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lMFA In Me 
        Try 
          Dim pUserID As String = CreateKeyForFindByUserID(lMFA) 
          If String.IsNullOrEmpty(pUserID.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pUserID)) Then 
            pTempDictionary.Add(pUserID, lMFA) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lMFA.ToString, "TRGT-MFA-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByUserID:" & ex.Message & ", MFA:" & lMFA.ToString() & ", TRGT-MFA-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByUserID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByUserID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadUserIDAndCellOrEmails() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByUserIDAndCellOrEmail Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByUserIDAndCellOrEmail Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByUserIDAndCellOrEmail = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByUserIDAndCellOrEmail' yet!
      Dim pTempDictionary As New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lMFA In Me 
        Try 
          Dim pUserIDAndCellOrEmail As String = CreateKeyForFindByUserIDAndCellOrEmail(lMFA) 
          If String.IsNullOrEmpty(pUserIDAndCellOrEmail.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pUserIDAndCellOrEmail)) Then 
            pTempDictionary.Add(pUserIDAndCellOrEmail, lMFA) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lMFA.ToString, "TRGT-MFA-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByUserIDAndCellOrEmail:" & ex.Message & ", MFA:" & lMFA.ToString() & ", TRGT-MFA-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByUserIDAndCellOrEmail = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByUserIDAndCellOrEmail = False
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
 
    For Each lMFA As csMFA In Me 
      lMFA.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the MFAs by the chosen parameters. This function may be a bit slower than accessing the MFA's FillBy... directly 
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
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-MFA-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-MFA-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "csMFAColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150308-1015", vRequester) 
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
 
      Dim pFunction As String = "csMFAColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA  
      If vAppend = True Then 
        Dim pMFAs As New csMFACol 
        pMFAs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMFAs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserID and CellOrEmail, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserIDAndCellOrEmail(ByVal vUserID As Long, ByVal vCellOrEmailFrom As String, ByVal vCellOrEmailTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}, CellOrEmailFrom={1}, CellOrEmailTo={2}", vUserID, vCellOrEmailFrom, vCellOrEmailTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserID 
          pBinaryWriter.Write(vUserID) 
          ' 
          'vCellOrEmailFrom 
          If vCellOrEmailFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCellOrEmailFrom) 
          ' 
          'vCellOrEmailTo 
          If vCellOrEmailTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCellOrEmailTo) 
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
 
      Dim pFunction As String = "csMFAColFillByBoundedUserIDAndCellOrEmail" 
      Dim pParametersToLog = $"UserIDAndCellOrEmail: {vUserID};{vCellOrEmailFrom};{vCellOrEmailTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA  
      If vAppend = True Then 
        Dim pMFAs As New csMFACol 
        pMFAs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMFAs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csMFAColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA   
      If vAppend = True Then 
        Dim pMFAs As New csMFACol 
        pMFAs.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pMFAs) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-231207-1750", vRequester) 
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
    [CellOrEmail]
    CellOrEmailWildcardType
    [UserID]
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
    Dim pCellOrEmail As String = Nothing
    Dim pCellOrEmailWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pUserID As Nullable(Of Long) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CellOrEmail) Then pObj = vParameters(enmFillOnTheFlyParameters.CellOrEmail) : If pObj IsNot Nothing Then pCellOrEmail = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CellOrEmailWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CellOrEmailWildcardType) : If pObj IsNot Nothing Then pCellOrEmailWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pCellOrEmail, pCellOrEmailWildcardType _
        , pUserID _
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
        , ByVal vCellOrEmail As String, ByVal vCellOrEmailWildcardType As clsEnums.enmWildCardType _
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CellOrEmail={2}, CellOrEmailWildcardType={3}, UserID={4}", vIDFrom, vIDTo, vCellOrEmail, vCellOrEmailWildcardType.FastToString(), vUserID)
    
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
          'CellOrEmail 
          If vCellOrEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCellOrEmail) : pBinaryWriter.Write(vCellOrEmailWildcardType.FastToString()) : pParametersToLog &= $"CellOrEmail={vCellOrEmail};" : pParametersToLog &= $"CellOrEmailWildcardType={vCellOrEmailWildcardType};"  
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMFAColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByCellOrEmail
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
    Dim pCellOrEmail As String = Nothing
    Dim pCellOrEmailWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pGroupByCellOrEmail As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CellOrEmail) Then pObj = vParameters(enmFillOnTheFlyParameters.CellOrEmail) : If pObj IsNot Nothing Then pCellOrEmail = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CellOrEmailWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CellOrEmailWildcardType) : If pObj IsNot Nothing Then pCellOrEmailWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCellOrEmail) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCellOrEmail) : If pObj IsNot Nothing Then pGroupByCellOrEmail = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pCellOrEmail, pCellOrEmailWildcardType _
        , pUserID _
        , pGroupByCellOrEmail _
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
        , ByVal vCellOrEmail As String, ByVal vCellOrEmailWildcardType As clsEnums.enmWildCardType _
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vGroupByCellOrEmail As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, CellOrEmail={2}, CellOrEmailWildcardType={3}, UserID={4}, GroupByCellOrEmail={5}", vIDFrom, vIDTo, vCellOrEmail, vCellOrEmailWildcardType.FastToString(), vUserID, vGroupByCellOrEmail)
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
          'CellOrEmail 
          If vCellOrEmail Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCellOrEmail) : pBinaryWriter.Write(vCellOrEmailWildcardType.FastToString()) 
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          pBinaryWriter.Write(vGroupByCellOrEmail) : pParametersToLog &= $"GroupByCellOrEmail={vGroupByCellOrEmail};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csMFAColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFA  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vMFAArray As csMFA())
    Me.Clear()
    
    For Each pMFA As csMFA In vMFAArray
      Me.Add(pMFA)
      _Clean.Add(pMFA.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pMFA As New csMFA(pRow, vRequester, _WithParents) 
        Me.Add(pMFA) 
        _Clean.Add(pMFA.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-MFACol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-130515-1300", vRequester) 
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
      Dim pMFAs As csMFACol = CType(pXmlSerializer.Deserialize(pStreamReader), csMFACol) 
      For Each pMFA As csMFA In pMFAs 
        Me.Add(pMFA) 
        _Clean.Add(pMFA.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-MFA-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-190720-1443", vRequester) 
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
 
      Dim pMFAs As List(Of csMFA) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csMFA))(vJSON, pSettings) 
      For Each pMFA As csMFA In pMFAs 
        Me.Add(pMFA) 
        _Clean.Add(pMFA.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-MFA-190720-2059", vRequester) 
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
          For Each lMFA As csMFA In Me 
            Dim pByte As Byte() = lMFA.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-MFA-150307-2340", vRequester) 
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
            Dim pMFA As csMFA = New csMFA(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pMFA) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pMFA.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-MFA-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pMFA As csMFA In Me 
      With pMFA 
        pFault = pMFA.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csMFACol) Then Return False 
    Dim pMFAColToTest As csMFACol = CType(vEntitiesToTest, csMFACol) 
    Return isEqual(pMFAColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vMFAsToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vMFAsToTest As csMFACol) As Boolean
    If Me.Count <> vMFAsToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vMFAsToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pMFAs As New csMFACol(_WithParents) 
    If pFilledFromSumOnTheFly Then pMFAs._FilledFromSumOnTheFly = True
    
    For Each pMFA As csMFA In Me 
      Dim pMFAClone As csMFA = pMFA.Clone() 
      pMFAs.Add(pMFAClone) 
      If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
    Next 
    Return pMFAs 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csMFACol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pMFAs As New csMFACol(_WithParents) 
    If pFilledFromSumOnTheFly Then pMFAs._FilledFromSumOnTheFly = True
    
    For Each pMFA As csMFA In Me
      Dim pMFAClone As csMFA = pMFA.Clone()
      pMFAs.Add(pMFAClone)
      If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
    Next
    Return pMFAs
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csMFACol 
    Dim pMFAs As New csMFACol(_WithParents)  
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMFA As csMFA In _SortedDictionaryForFindByID.Values.ToList() 
      If (pMFA.ID > vIDFrom AndAlso pMFA.ID <= vIDTo) Then 
        Dim pMFAClone As csMFA = pMFA.Clone() 
        pMFAs.Add(pMFAClone) 
        If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
      End If 
    Next 
    Return pMFAs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by UserID and CellOrEmail (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedUserIDAndCellOrEmail(ByVal vUserID As Long, ByVal vCellOrEmailFrom As String, ByVal vCellOrEmailTo As String) As csMFACol 
    Dim pMFAs As New csMFACol(_WithParents)  
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMFA As csMFA In _SortedDictionaryForFindByID.Values.ToList() 
      If (pMFA.UserID = vUserID) AndAlso (pMFA.CellOrEmail > vCellOrEmailFrom AndAlso pMFA.CellOrEmail <= vCellOrEmailTo) Then 
        Dim pMFAClone As csMFA = pMFA.Clone() 
        pMFAs.Add(pMFAClone) 
        If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
      End If 
    Next 
    Return pMFAs 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardUserIDAndCellOrEmail(ByVal vUserID As Long, ByVal vUserIDWildcardType As clsEnums.enmWildCardType, ByVal vCellOrEmail As String, ByVal vCellOrEmailWildcardType As clsEnums.enmWildCardType) As csMFACol 
    Dim pMFAs As New csMFACol 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pMFA As csMFA In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vCellOrEmailWildcardType = clsEnums.enmWildCardType.After Then 
        If pMFA.CellOrEmail.StartsWith(vCellOrEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCellOrEmailWildcardType = clsEnums.enmWildCardType.Before Then 
        If pMFA.CellOrEmail.EndsWith(vCellOrEmail, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCellOrEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pMFA.CellOrEmail.IndexOf(vCellOrEmail, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vCellOrEmailWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vCellOrEmail.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pMFA.CellOrEmail.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pMFAClone As csMFA = pMFA.Clone() 
      pMFAs.Add(pMFAClone) 
    Next 
    Return pMFAs 
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
      Dim pFunction As String = "csMFAColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFACol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150314-1803", vRequester) 
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
  Public Function FindByID(ByVal vID As Long) As csMFA
    If Me.Count = 0 Then Return New csMFA 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
    
    Dim pMFA As csMFA = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pMFA) 
    If pMFA IsNot Nothing Then Return pMFA Else Return New csMFA() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByUserID(ByVal vUserID As Long) As csMFA
    If Me.Count = 0 Then Return New csMFA 
    
    If _RecreateDictionaryForFindByUserID = True Then LoadUserIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csMFA) = _SortedDictionaryForFindByUserID 
    
    Dim pMFA As csMFA = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vUserID.ToString()
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pMFA) 
    If pMFA IsNot Nothing Then Return pMFA Else Return New csMFA() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByUserIDAndCellOrEmail(ByVal vUserID As Long, ByVal vCellOrEmail As String) As csMFA
    If Me.Count = 0 Then Return New csMFA 
    
    If _RecreateDictionaryForFindByUserIDAndCellOrEmail = True Then LoadUserIDAndCellOrEmails() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csMFA) = _SortedDictionaryForFindByUserIDAndCellOrEmail 
    
    Dim pMFA As csMFA = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vUserID.ToString() & "|" & vCellOrEmail
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pMFA) 
    If pMFA IsNot Nothing Then Return pMFA Else Return New csMFA() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CellOrEmail
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCellOrEmail(ByVal vCellOrEmail As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCellOrEmail = vCellOrEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.CellOrEmail.ToLowerInvariant() = vCellOrEmail Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCellOrEmail with vCellOrEmail of {vCellOrEmail}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.CellOrEmail.ToLowerInvariant() = vCellOrEmail Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProtectedFunction
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProtectedFunction(ByVal vProtectedFunction As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProtectedFunction = vProtectedFunction.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.ProtectedFunction.ToLowerInvariant() = vProtectedFunction Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProtectedFunction with vProtectedFunction of {vProtectedFunction}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.ProtectedFunction.ToLowerInvariant() = vProtectedFunction Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AttemptNo
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAttemptNo(ByVal vAttemptNo As Integer) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.AttemptNo = vAttemptNo Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAttemptNo with vAttemptNo of {vAttemptNo}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.AttemptNo = vAttemptNo Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsSuccessful
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsSuccessful(ByVal vIsSuccessful As Boolean) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.IsSuccessful = vIsSuccessful Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsSuccessful with vIsSuccessful of {vIsSuccessful}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.IsSuccessful = vIsSuccessful Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastAccessingIP
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastAccessingIP(ByVal vLastAccessingIP As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastAccessingIP = vLastAccessingIP.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.LastAccessingIP.ToLowerInvariant() = vLastAccessingIP Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastAccessingIP with vLastAccessingIP of {vLastAccessingIP}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.LastAccessingIP.ToLowerInvariant() = vLastAccessingIP Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastAccessingCountry
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastAccessingCountry(ByVal vLastAccessingCountry As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastAccessingCountry = vLastAccessingCountry.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.LastAccessingCountry.ToLowerInvariant() = vLastAccessingCountry Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastAccessingCountry with vLastAccessingCountry of {vLastAccessingCountry}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.LastAccessingCountry.ToLowerInvariant() = vLastAccessingCountry Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UILang
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUILang(ByVal vUILang As clsEnums.enmLanguage) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.UILang = vUILang Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUILang with vUILang of {vUILang}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.UILang = vUILang Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenCreated
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenCreated(ByVal vWhenCreated As DateTimeOffset) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.WhenCreated = vWhenCreated Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenCreated with vWhenCreated of {vWhenCreated}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.WhenCreated = vWhenCreated Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenAccessed
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenAccessed(ByVal vWhenAccessed As DateTimeOffset) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.WhenAccessed = vWhenAccessed Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenAccessed with vWhenAccessed of {vWhenAccessed}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.WhenAccessed = vWhenAccessed Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined WhenExpires
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByWhenExpires(ByVal vWhenExpires As DateTimeOffset) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.WhenExpires = vWhenExpires Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByWhenExpires with vWhenExpires of {vWhenExpires}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.WhenExpires = vWhenExpires Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Details
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDetails(ByVal vDetails As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vDetails = vDetails.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.Details.ToLowerInvariant() = vDetails Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDetails with vDetails of {vDetails}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.Details.ToLowerInvariant() = vDetails Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserID(ByVal vUserID As Long) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.UserID = vUserID Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserID with vUserID of {vUserID}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.UserID = vUserID Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csMFACol
    Dim pMFAs As New csMFACol(_WithParents) 
    pMFAs._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csMFA) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pMFA As csMFA In pTempDist.Values
        If pMFA.Tag.ToLowerInvariant() = vTag Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csMFACol = Me.Clone() 
      For Each pMFA As csMFA In pList 
        If pMFA.Tag.ToLowerInvariant() = vTag Then
          Dim pMFAClone As csMFA = pMFA.Clone()
          pMFAs.Add(pMFAClone)
          If Not _FilledFromSumOnTheFly Then pMFAs._Clean.Add(pMFA.ID) 
        End If
      Next
    End If 
    
    Return pMFAs
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
    For Each pMFA As csMFA In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pMFA.LoadDataRow(pRow, vRequester) 
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
    For Each p As csMFA In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
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
      Dim pFunction As String = "csMFAColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFACol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150314-1803", vRequester) 
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
      Dim pFunction As String = "csMFAColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the MFACol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-MFA-150314-1803", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
   
  ''' <summary> 
  ''' Used for Interface compliance. Sorts the Entity by the Primary Key (usually ID) 
  ''' </summary> 
  Public Overrides Sub SortByPrimaryKey() 
    SortByID() 
  End Sub 
 
  Public Sub SortByID()
    Me.Sort(New csMFACol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
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
  
  Public Sub SortByCellOrEmail()
    Me.Sort(New csMFACol.CompareByCellOrEmail)
  End Sub
  Private Class CompareByCellOrEmail
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CellOrEmail, y.CellOrEmail, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProtectedFunction()
    Me.Sort(New csMFACol.CompareByProtectedFunction)
  End Sub
  Private Class CompareByProtectedFunction
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProtectedFunction, y.ProtectedFunction, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAttemptNo()
    Me.Sort(New csMFACol.CompareByAttemptNo)
  End Sub
  Private Class CompareByAttemptNo
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.AttemptNo < y.AttemptNo Then
        Return -1
      ElseIf x.AttemptNo = y.AttemptNo Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByIsSuccessful()
    Me.Sort(New csMFACol.CompareByIsSuccessful)
  End Sub
  Private Class CompareByIsSuccessful
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsSuccessful.ToString, y.IsSuccessful.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastAccessingIP()
    Me.Sort(New csMFACol.CompareByLastAccessingIP)
  End Sub
  Private Class CompareByLastAccessingIP
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastAccessingIP, y.LastAccessingIP, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastAccessingCountry()
    Me.Sort(New csMFACol.CompareByLastAccessingCountry)
  End Sub
  Private Class CompareByLastAccessingCountry
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastAccessingCountry, y.LastAccessingCountry, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUILang()
    Me.Sort(New csMFACol.CompareByUILang)
  End Sub
  Private Class CompareByUILang
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.UILang < y.UILang Then
        Return -1
      ElseIf x.UILang = y.UILang Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByUILangText()
    Me.Sort(New csMFACol.CompareByUILangText)
  End Sub
  Private Class CompareByUILangText
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UILangText, y.UILangText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByWhenCreated()
    Me.Sort(New csMFACol.CompareByWhenCreated)
  End Sub
  Private Class CompareByWhenCreated
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenCreated < y.WhenCreated Then
        Return -1
      ElseIf x.WhenCreated = y.WhenCreated Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByWhenAccessed()
    Me.Sort(New csMFACol.CompareByWhenAccessed)
  End Sub
  Private Class CompareByWhenAccessed
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenAccessed < y.WhenAccessed Then
        Return -1
      ElseIf x.WhenAccessed = y.WhenAccessed Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByWhenExpires()
    Me.Sort(New csMFACol.CompareByWhenExpires)
  End Sub
  Private Class CompareByWhenExpires
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.WhenExpires < y.WhenExpires Then
        Return -1
      ElseIf x.WhenExpires = y.WhenExpires Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByDetails()
    Me.Sort(New csMFACol.CompareByDetails)
  End Sub
  Private Class CompareByDetails
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Details, y.Details, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByUserID()
    Me.Sort(New csMFACol.CompareByUserID)
  End Sub
  Private Class CompareByUserID
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
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
    Me.Sort(New csMFACol.CompareByUserText)
  End Sub
  Private Class CompareByUserText
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserText, y.UserText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csMFACol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csMFA)
    Private Function Compare(ByVal x As csMFA, ByVal y As csMFA) As Integer Implements System.Collections.Generic.IComparer(Of csMFA).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csMFA) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByUserID = New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByUserID = False 
    _SortedDictionaryForFindByUserIDAndCellOrEmail = New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByUserIDAndCellOrEmail = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csMFA) 
    _SortedDictionaryForFindByUserID = New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
    _SortedDictionaryForFindByUserIDAndCellOrEmail = New Dictionary(Of String, csMFA)(StringComparer.OrdinalIgnoreCase) 
 
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
  
