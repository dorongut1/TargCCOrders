Public Class csUserLoginKey
  Inherits cTargCCEntity 
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
    [User] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [User] 
    [ApplicationName] 
    [ApplicationIdentifier] 
    [KeyHashed] 
    [ExternalIPAtCreation] 
    [CountryAtCreation] 
    [LastAccessTime] 
    [LoggedLoginID] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [LoggedLoginID] 
  End Enum 
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
  Private _UserID As Long
  Private _User As csUser
  Private _UserText As String
  Private _ApplicationName As String
  Private _ApplicationIdentifier As String
  Private _KeyHashed As String
  Private _ExternalIPAtCreation As String
  Private _CountryAtCreation As String
  Private _LastAccessTime As Date
  Private _LoggedLoginID As Long
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
  Public Property [ApplicationIdentifier]() As String
    Get
      Return Me._ApplicationIdentifier
    End Get
    Set(ByVal value As String)
      If Me._ApplicationIdentifier <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ApplicationIdentifier = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' If you want to hash (SHA256) the input, then prefix it with 'PleaseHash'. Otherwise, use ccHelper.Encrypt(ccHelper.enmHashType.SHA256, ValueToHash) 
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [KeyHashed]() As String
    Get
      Return Me._KeyHashed
    End Get
    Set(ByVal value As String)
      If Me._KeyHashed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
         If value.StartsWith("PleaseHash", StringComparison.OrdinalIgnoreCase) Then value = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, value.Substring(10)) 
        Me._KeyHashed = value 
      End If 
    End Set
  End Property
  Public Property [ExternalIPAtCreation]() As String
    Get
      Return Me._ExternalIPAtCreation
    End Get
    Set(ByVal value As String)
      If Me._ExternalIPAtCreation <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ExternalIPAtCreation = value 
      End If 
    End Set
  End Property
  Public Property [CountryAtCreation]() As String
    Get
      Return Me._CountryAtCreation
    End Get
    Set(ByVal value As String)
      If Me._CountryAtCreation <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._CountryAtCreation = value 
      End If 
    End Set
  End Property
  Public Property [LastAccessTime]() As Date
    Get
      Return Me._LastAccessTime
    End Get
    Set(ByVal value As Date)
      If Me._LastAccessTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastAccessTime = value 
      End If 
    End Set
  End Property
  Public Property [LoggedLoginID]() As Long
    Get
      Return Me._LoggedLoginID
    End Get
    Set(ByVal value As Long)
      If Me._LoggedLoginID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LoggedLoginID = value 
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
    If _ApplicationIdentifier <> "" Then pValue.Append("ApplicationIdentifier='" & _ApplicationIdentifier & "' ‡ ") 
    If _KeyHashed <> "" Then pValue.Append("Key='*****' ‡ ") 
    If _ExternalIPAtCreation <> "" Then pValue.Append("ExternalIPAtCreation='" & _ExternalIPAtCreation & "' ‡ ") 
    If _CountryAtCreation <> "" Then pValue.Append("CountryAtCreation='" & _CountryAtCreation & "' ‡ ") 
    If Not (_LastAccessTime = Nothing) Then pValue.Append("LastAccessTime='" & _LastAccessTime.ToString("o") & "' ‡ ") 
    If _LoggedLoginID <> 0 Then pValue.Append("LoggedLoginID='" & _LoggedLoginID.ToString() & "' ‡ ") 
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
    pCSV.Append($",""{ccHelper.StringForCSV(_ApplicationIdentifier)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ExternalIPAtCreation)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_CountryAtCreation)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastAccessTime.ToShortDateString & " " & _LastAccessTime.ToShortTimeString)}""") 
    pCSV.Append("," & _LoggedLoginID.ToString() & "") 
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
  
  Public Sub New(ByVal vcsUserLoginKey As csUserLoginKey)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsUserLoginKey) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vUserID As Long = 0 _ 
    , Optional vUserText As String = "" _ 
    , Optional vApplicationName As String = "" _ 
    , Optional vApplicationIdentifier As String = "" _ 
    , Optional vKeyHashed As String = "" _ 
    , Optional vExternalIPAtCreation As String = "" _ 
    , Optional vCountryAtCreation As String = "" _ 
    , Optional vLastAccessTime As Date = Nothing _ 
    , Optional vLoggedLoginID As Long = 0 _ 
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
    _ApplicationIdentifier = vApplicationIdentifier 
    _KeyHashed = vKeyHashed 
    _ExternalIPAtCreation = vExternalIPAtCreation 
    _CountryAtCreation = vCountryAtCreation 
    _LastAccessTime = vLastAccessTime 
    _LoggedLoginID = vLoggedLoginID 
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
    _ApplicationIdentifier = _ApplicationIdentifier.Truncate(pTruncateLength, _IsTruncated) 
    _KeyHashed = _KeyHashed.Truncate(pTruncateLength, _IsTruncated) 
    _ExternalIPAtCreation = _ExternalIPAtCreation.Truncate(pTruncateLength, _IsTruncated) 
    _CountryAtCreation = _CountryAtCreation.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the UserLoginKey by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserLoginKey-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [ApplicationNameAndApplicationIdentifier] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the UserLoginKey by the chosen parameters. This function may be a bit slower than accessing the UserLoginKey's GetBy... directly 
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
        Case enmGetByParameters.ApplicationNameAndApplicationIdentifier 
          pFault = GetByApplicationNameAndApplicationIdentifier(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-UserLoginKey-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserLoginKey-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the UserLoginKey by ID. 
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
      Dim pFunction As String = "csUserLoginKeyGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the UserLoginKey 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the UserLoginKey by ApplicationNameAndApplicationIdentifier. 
  ''' </summary>
  ''' <param name="vApplicationName"></param>
  ''' <param name="vApplicationIdentifier"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByApplicationNameAndApplicationIdentifier(ByVal vApplicationName As String, ByVal vApplicationIdentifier As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationIdentifier={1}", vApplicationName, vApplicationIdentifier)
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
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          'vApplicationIdentifier 
          If vApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifier) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserLoginKeyGetByApplicationNameAndApplicationIdentifier" 
      Dim pParametersToLog = $"ApplicationNameAndApplicationIdentifier: {vApplicationName};{vApplicationIdentifier};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the UserLoginKey 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  'Interface Edits
  Public Function EditUpdate(vRequester As clsRequester, Optional vReload As Boolean = True) As clsFault Implements ITargCCEntityEditable.EditUpdate 
    Dim pFunctionParameters As String = Me.ToString() 
    Dim pFault As New clsFault 
 
    If _ID = 0 Then 
      'Invalid EntityID received for the operation 
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-UserLoginKey-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the UserLoginKey. If there are parents or children in the UserLoginKey, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("UserLoginKey.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    If _ID = 0 Then
      Return pFault.LogFreeTextFault(56, "'UserLoginKey' is not 'Addable'", pFunctionParameters, "TRGT-UserLoginKey-190217-1702", vRequester) 
    End If
    
    'Check if we got an empty object 
    Dim pUserLoginKey As New csUserLoginKey 
    If Me.isEqual(pUserLoginKey) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-UserLoginKey-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-UserLoginKey-240611-135714", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
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
      Dim pFunction As String = "csUserLoginKeyUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.Standard)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Standard, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("UserLoginKey.ID={0}", _ID)
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
      Dim pFunction As String = "csUserLoginKeyDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150314-1803", vRequester) 
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
      Dim pFunction As String = "csUserLoginKeyDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
  ''' <summary> 
  ''' Used for Interface compliance. This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vTargCCEntityToTest"></param> 
  ''' <returns></returns> 
  Public Overrides Function isEqual(ByVal vTargCCEntityToTest As ITargCCEntity) As Boolean 
    If Not (TypeOf (vTargCCEntityToTest) Is csUserLoginKey) Then Return False 
    Dim pUserLoginKeyToTest As csUserLoginKey = CType(vTargCCEntityToTest, csUserLoginKey) 
    Return isEqual(pUserLoginKeyToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vUserLoginKeyToTest As csUserLoginKey) As Boolean
    With vUserLoginKeyToTest
      If _ID <> .ID Then Return False
      If _UserID <> .UserID Then Return False
      If _ApplicationName <> .ApplicationName Then Return False
      If _ApplicationIdentifier <> .ApplicationIdentifier Then Return False
      If _KeyHashed <> .KeyHashed Then Return False
      If _ExternalIPAtCreation <> .ExternalIPAtCreation Then Return False
      If _CountryAtCreation <> .CountryAtCreation Then Return False
      If _LastAccessTime <> Nothing AndAlso .LastAccessTime <> Nothing Then 
        If ccHelper.ToLong(_LastAccessTime.Subtract(.LastAccessTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LastAccessTime = Nothing AndAlso .LastAccessTime = Nothing) Then 
        Return False 
      End If 
      If _LoggedLoginID <> .LoggedLoginID Then Return False
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
    Dim pClone As New csUserLoginKey(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUserLoginKey
    Dim pClone As New csUserLoginKey(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserID") = _UserID : Catch ex As Exception : Return pFault.LogException(ex, "UserID", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApplicationName") = _ApplicationName : Catch ex As Exception : Return pFault.LogException(ex, "ApplicationName", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApplicationIdentifier") = _ApplicationIdentifier : Catch ex As Exception : Return pFault.LogException(ex, "ApplicationIdentifier", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("KeyHashed") = _KeyHashed : Catch ex As Exception : Return pFault.LogException(ex, "KeyHashed", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("ExternalIPAtCreation") = _ExternalIPAtCreation : Catch ex As Exception : Return pFault.LogException(ex, "ExternalIPAtCreation", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("CountryAtCreation") = _CountryAtCreation : Catch ex As Exception : Return pFault.LogException(ex, "CountryAtCreation", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastAccessTime") = _LastAccessTime : Catch ex As Exception : Return pFault.LogException(ex, "LastAccessTime", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoggedLoginID") = _LoggedLoginID : Catch ex As Exception : Return pFault.LogException(ex, "LoggedLoginID", "TRGT-UserLoginKey-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pUserLoginKey As csUserLoginKey = CType(pXmlSerializer.Deserialize(pStreamReader), csUserLoginKey) 
      AssignValues(pUserLoginKey) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-UserLoginKey-130515-1230", vRequester) 
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
          'ApplicationIdentifier 
          If _ApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApplicationIdentifier) 
          'KeyHashed 
          If _KeyHashed Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_KeyHashed) 
          'ExternalIPAtCreation 
          If _ExternalIPAtCreation Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ExternalIPAtCreation) 
          'CountryAtCreation 
          If _CountryAtCreation Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_CountryAtCreation) 
          'LastAccessTime 
          pBinaryWriter.Write(_LastAccessTime.Ticks) 
          'LoggedLoginID 
          pBinaryWriter.Write(_LoggedLoginID) 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-150307-2338", vRequester) 
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
          'ApplicationIdentifier 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApplicationIdentifier = pReader.ReadString 
          'KeyHashed 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _KeyHashed = pReader.ReadString 
          'ExternalIPAtCreation 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ExternalIPAtCreation = pReader.ReadString 
          'CountryAtCreation 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _CountryAtCreation = pReader.ReadString 
          'LastAccessTime 
          _LastAccessTime = New Date(pReader.ReadInt64) 
          'LoggedLoginID 
          _LoggedLoginID = pReader.ReadInt64 
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
      rFault.LogException(ex, "", "TRGT-UserLoginKey-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-190720-1443", vRequester) 
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
 
      Dim pUserLoginKey As csUserLoginKey = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csUserLoginKey)(vJSON, pSettings) 
      AssignValues(pUserLoginKey) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vUserLoginKey As csUserLoginKey)
    With vUserLoginKey
      _ID = .ID 
      _UserID = .UserID 
      If .User IsNot Nothing Then 
        _User = .User.Clone() 
      End If 
      _UserText = .UserText 
      _ApplicationName = .ApplicationName 
      _ApplicationIdentifier = .ApplicationIdentifier 
      _KeyHashed = .KeyHashed 
      _ExternalIPAtCreation = .ExternalIPAtCreation 
      _CountryAtCreation = .CountryAtCreation 
      _LastAccessTime = .LastAccessTime 
      _LoggedLoginID = .LoggedLoginID 
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
      Dim pFunction As String = "csUserLoginKeyLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  Private Sub CreateEmpty()
    
    _ID = 0 
    _UserID = 0
    _User = Nothing
    _UserText = "."
    _ApplicationName = ""
    _ApplicationIdentifier = ""
    _KeyHashed = ""
    _ExternalIPAtCreation = ""
    _CountryAtCreation = ""
    _LastAccessTime = Nothing
    _LoggedLoginID = 0
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
  
Public Class csUserLoginKeyCol
  Inherits cTargCCCollection(Of csUserLoginKey)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csUserLoginKey) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier As Dictionary(Of String, csUserLoginKey) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier As Boolean 
  Private Function CreateKeyForFindByApplicationNameAndApplicationIdentifier(ByVal vUserLoginKey As csUserLoginKey) As String 
    With vUserLoginKey 
      Return .ApplicationName & "|" & .ApplicationIdentifier
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
 
    For Each pRow As csUserLoginKey In Me 
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
    pCSVTitle.Append(",""ApplicationIdentifier""") 
    pCSVTitle.Append(",""KeyHashed""") 
    pCSVTitle.Append(",""ExternalIPAtCreation""") 
    pCSVTitle.Append(",""CountryAtCreation""") 
    pCSVTitle.Append(",""LastAccessTime""") 
    pCSVTitle.Append(",""LoggedLoginID""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csUserLoginKey In Me 
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
 
  Public Overloads Sub Add(ByVal vUserLoginKey As csUserLoginKey) 
    SyncLock _CollectionLock 
      MyBase.Add(vUserLoginKey) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vUserLoginKey As csUserLoginKey) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vUserLoginKey) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vUserLoginKeyCol As csUserLoginKeyCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vUserLoginKeyCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vUserLoginKey As csUserLoginKey) 
    SyncLock _CollectionLock 
      MyBase.Remove(vUserLoginKey) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csUserLoginKey) 
      
      For Each lUserLoginKey In Me 
        If lUserLoginKey.IsEmpty OrElse pTempDictionary.ContainsKey(lUserLoginKey.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lUserLoginKey.ID, lUserLoginKey) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUserLoginKey.ToString, "TRGT-UserLoginKey-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", UserLoginKey:" & lUserLoginKey.ToString() & ", TRGT-UserLoginKey-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadApplicationNameAndApplicationIdentifiers() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByApplicationNameAndApplicationIdentifier' yet!
      Dim pTempDictionary As New Dictionary(Of String, csUserLoginKey)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lUserLoginKey In Me 
        Try 
          Dim pApplicationNameAndApplicationIdentifier As String = CreateKeyForFindByApplicationNameAndApplicationIdentifier(lUserLoginKey) 
          If String.IsNullOrEmpty(pApplicationNameAndApplicationIdentifier.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pApplicationNameAndApplicationIdentifier)) Then 
            pTempDictionary.Add(pApplicationNameAndApplicationIdentifier, lUserLoginKey) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUserLoginKey.ToString, "TRGT-UserLoginKey-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier:" & ex.Message & ", UserLoginKey:" & lUserLoginKey.ToString() & ", TRGT-UserLoginKey-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = False
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
 
    For Each lUserLoginKey As csUserLoginKey In Me 
      lUserLoginKey.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [UserID] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the UserLoginKeys by the chosen parameters. This function may be a bit slower than accessing the UserLoginKey's FillBy... directly 
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
        Case enmFillByParameterCombination.UserID 
          pFault = FillByUserID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-UserLoginKey-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-UserLoginKey-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "csUserLoginKeyColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByUserID(ByVal vUserID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserID={0}", vUserID)
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
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColFillByUserID" 
      Dim pParametersToLog = $"UserID: {vUserID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      If vAppend = True Then 
        Dim pUserLoginKeys As New csUserLoginKeyCol 
        pUserLoginKeys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUserLoginKeys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csUserLoginKeyColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      If vAppend = True Then 
        Dim pUserLoginKeys As New csUserLoginKeyCol 
        pUserLoginKeys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUserLoginKeys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ApplicationName and ApplicationIdentifier, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedApplicationNameAndApplicationIdentifier(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vApplicationIdentifierFrom As String, ByVal vApplicationIdentifierTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationNameFrom={0}, ApplicationNameTo={1}, ApplicationIdentifierFrom={2}, ApplicationIdentifierTo={3}", vApplicationNameFrom, vApplicationNameTo, vApplicationIdentifierFrom, vApplicationIdentifierTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationNameFrom 
          If vApplicationNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameFrom) 
          ' 
          'vApplicationNameTo 
          If vApplicationNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameTo) 
          ' 
          'vApplicationIdentifierFrom 
          If vApplicationIdentifierFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifierFrom) 
          ' 
          'vApplicationIdentifierTo 
          If vApplicationIdentifierTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifierTo) 
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
 
      Dim pFunction As String = "csUserLoginKeyColFillByBoundedApplicationNameAndApplicationIdentifier" 
      Dim pParametersToLog = $"ApplicationNameAndApplicationIdentifier: {vApplicationNameFrom};{vApplicationNameTo};{vApplicationIdentifierFrom};{vApplicationIdentifierTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      If vAppend = True Then 
        Dim pUserLoginKeys As New csUserLoginKeyCol 
        pUserLoginKeys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUserLoginKeys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific ApplicationName and ApplicationIdentifier, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardApplicationNameAndApplicationIdentifier(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationIdentifier As String, ByVal vApplicationIdentifierWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationNameWildcardType={1}, ApplicationIdentifier={2}, ApplicationIdentifierWildcardType={3}", vApplicationName, vApplicationNameWildcardType.FastToString(), vApplicationIdentifier, vApplicationIdentifierWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vApplicationNameWildcardType.FastToString())
          'vApplicationIdentifier 
          If vApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifier) 
          ' 
          pBinaryWriter.Write(vApplicationIdentifierWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColFillByWildCardApplicationNameAndApplicationIdentifier" 
      Dim pParametersToLog = $"ApplicationNameAndApplicationIdentifier: {vApplicationName};{vApplicationNameWildcardType.FastToString()};{vApplicationIdentifier};{vApplicationIdentifierWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      If vAppend = True Then 
        Dim pUserLoginKeys As New csUserLoginKeyCol 
        pUserLoginKeys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUserLoginKeys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csUserLoginKeyColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey   
      If vAppend = True Then 
        Dim pUserLoginKeys As New csUserLoginKeyCol 
        pUserLoginKeys.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUserLoginKeys) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-231207-1750", vRequester) 
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
    [UserID]
    [ApplicationName]
    ApplicationNameWildcardType
    [ApplicationIdentifier]
    ApplicationIdentifierWildcardType
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
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pApplicationIdentifier As String = Nothing
    Dim pApplicationIdentifierWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationIdentifier) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationIdentifier) : If pObj IsNot Nothing Then pApplicationIdentifier = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationIdentifierWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationIdentifierWildcardType) : If pObj IsNot Nothing Then pApplicationIdentifierWildcardType = CType(pObj, clsEnums.enmWildCardType) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pUserID _
        , pApplicationName, pApplicationNameWildcardType _
        , pApplicationIdentifier, pApplicationIdentifierWildcardType _
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
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vApplicationIdentifier As String, ByVal vApplicationIdentifierWildcardType As clsEnums.enmWildCardType _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserID={2}, ApplicationName={3}, ApplicationNameWildcardType={4}, ApplicationIdentifier={5}, ApplicationIdentifierWildcardType={6}", vIDFrom, vIDTo, vUserID, vApplicationName, vApplicationNameWildcardType.FastToString(), vApplicationIdentifier, vApplicationIdentifierWildcardType.FastToString())
    
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
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          'ApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) : pBinaryWriter.Write(vApplicationNameWildcardType.FastToString()) : pParametersToLog &= $"ApplicationName={vApplicationName};" : pParametersToLog &= $"ApplicationNameWildcardType={vApplicationNameWildcardType};"  
          'ApplicationIdentifier 
          If vApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifier) : pBinaryWriter.Write(vApplicationIdentifierWildcardType.FastToString()) : pParametersToLog &= $"ApplicationIdentifier={vApplicationIdentifier};" : pParametersToLog &= $"ApplicationIdentifierWildcardType={vApplicationIdentifierWildcardType};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByUserID
    GroupByApplicationName
    GroupByApplicationIdentifier
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
    Dim pUserID As Nullable(Of Long) = Nothing
    Dim pApplicationName As String = Nothing
    Dim pApplicationNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pApplicationIdentifier As String = Nothing
    Dim pApplicationIdentifierWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pGroupByUserID As Boolean = False
    Dim pGroupByApplicationName As Boolean = False
    Dim pGroupByApplicationIdentifier As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserID) Then pObj = vParameters(enmFillOnTheFlyParameters.UserID) : If pObj IsNot Nothing Then pUserID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationName) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationName) : If pObj IsNot Nothing Then pApplicationName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationNameWildcardType) : If pObj IsNot Nothing Then pApplicationNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationIdentifier) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationIdentifier) : If pObj IsNot Nothing Then pApplicationIdentifier = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.ApplicationIdentifierWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.ApplicationIdentifierWildcardType) : If pObj IsNot Nothing Then pApplicationIdentifierWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByUserID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByUserID) : If pObj IsNot Nothing Then pGroupByUserID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByApplicationName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByApplicationName) : If pObj IsNot Nothing Then pGroupByApplicationName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByApplicationIdentifier) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByApplicationIdentifier) : If pObj IsNot Nothing Then pGroupByApplicationIdentifier = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pUserID _
        , pApplicationName, pApplicationNameWildcardType _
        , pApplicationIdentifier, pApplicationIdentifierWildcardType _
        , pGroupByUserID _
        , pGroupByApplicationName _
        , pGroupByApplicationIdentifier _
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
        , ByVal vUserID As Nullable(Of Long) _
        , ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vApplicationIdentifier As String, ByVal vApplicationIdentifierWildcardType As clsEnums.enmWildCardType _
        , ByVal vGroupByUserID As Boolean _
        , ByVal vGroupByApplicationName As Boolean _
        , ByVal vGroupByApplicationIdentifier As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserID={2}, ApplicationName={3}, ApplicationNameWildcardType={4}, ApplicationIdentifier={5}, ApplicationIdentifierWildcardType={6}, GroupByUserID={7}, GroupByApplicationName={8}, GroupByApplicationIdentifier={9}", vIDFrom, vIDTo, vUserID, vApplicationName, vApplicationNameWildcardType.FastToString(), vApplicationIdentifier, vApplicationIdentifierWildcardType.FastToString(), vGroupByUserID, vGroupByApplicationName, vGroupByApplicationIdentifier)
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
          'UserID 
          pBinaryWriter.Write(vUserID.HasValue) 
          If vUserID.HasValue = True Then pBinaryWriter.Write(vUserID.Value) : pParametersToLog &= $"UserID={vUserID};"  
          'ApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) : pBinaryWriter.Write(vApplicationNameWildcardType.FastToString()) 
          'ApplicationIdentifier 
          If vApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifier) : pBinaryWriter.Write(vApplicationIdentifierWildcardType.FastToString()) 
          pBinaryWriter.Write(vGroupByUserID) : pParametersToLog &= $"GroupByUserID={vGroupByUserID};"  
          pBinaryWriter.Write(vGroupByApplicationName) : pParametersToLog &= $"GroupByApplicationName={vGroupByApplicationName};"  
          pBinaryWriter.Write(vGroupByApplicationIdentifier) : pParametersToLog &= $"GroupByApplicationIdentifier={vGroupByApplicationIdentifier};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKey  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vUserLoginKeyArray As csUserLoginKey())
    Me.Clear()
    
    For Each pUserLoginKey As csUserLoginKey In vUserLoginKeyArray
      Me.Add(pUserLoginKey)
      _Clean.Add(pUserLoginKey.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pUserLoginKey As New csUserLoginKey(pRow, vRequester, _WithParents) 
        Me.Add(pUserLoginKey) 
        _Clean.Add(pUserLoginKey.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-UserLoginKeyCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-130515-1300", vRequester) 
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
      Dim pUserLoginKeys As csUserLoginKeyCol = CType(pXmlSerializer.Deserialize(pStreamReader), csUserLoginKeyCol) 
      For Each pUserLoginKey As csUserLoginKey In pUserLoginKeys 
        Me.Add(pUserLoginKey) 
        _Clean.Add(pUserLoginKey.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-UserLoginKey-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-190720-1443", vRequester) 
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
 
      Dim pUserLoginKeys As List(Of csUserLoginKey) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csUserLoginKey))(vJSON, pSettings) 
      For Each pUserLoginKey As csUserLoginKey In pUserLoginKeys 
        Me.Add(pUserLoginKey) 
        _Clean.Add(pUserLoginKey.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-190720-2059", vRequester) 
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
          For Each lUserLoginKey As csUserLoginKey In Me 
            Dim pByte As Byte() = lUserLoginKey.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-UserLoginKey-150307-2340", vRequester) 
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
            Dim pUserLoginKey As csUserLoginKey = New csUserLoginKey(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pUserLoginKey) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pUserLoginKey.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-UserLoginKey-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pUserLoginKey As csUserLoginKey In Me 
      With pUserLoginKey 
        pFault = pUserLoginKey.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csUserLoginKeyCol) Then Return False 
    Dim pUserLoginKeyColToTest As csUserLoginKeyCol = CType(vEntitiesToTest, csUserLoginKeyCol) 
    Return isEqual(pUserLoginKeyColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vUserLoginKeysToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vUserLoginKeysToTest As csUserLoginKeyCol) As Boolean
    If Me.Count <> vUserLoginKeysToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vUserLoginKeysToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUserLoginKeys._FilledFromSumOnTheFly = True
    
    For Each pUserLoginKey As csUserLoginKey In Me 
      Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone() 
      pUserLoginKeys.Add(pUserLoginKeyClone) 
      If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
    Next 
    Return pUserLoginKeys 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUserLoginKeyCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUserLoginKeys._FilledFromSumOnTheFly = True
    
    For Each pUserLoginKey As csUserLoginKey In Me
      Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
      pUserLoginKeys.Add(pUserLoginKeyClone)
      If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
    Next
    Return pUserLoginKeys
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csUserLoginKeyCol 
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents)  
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserLoginKey As csUserLoginKey In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserLoginKey.ID > vIDFrom AndAlso pUserLoginKey.ID <= vIDTo) Then 
        Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone() 
        pUserLoginKeys.Add(pUserLoginKeyClone) 
        If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
      End If 
    Next 
    Return pUserLoginKeys 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ApplicationName and ApplicationIdentifier (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedApplicationNameAndApplicationIdentifier(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vApplicationIdentifierFrom As String, ByVal vApplicationIdentifierTo As String) As csUserLoginKeyCol 
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents)  
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserLoginKey As csUserLoginKey In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUserLoginKey.ApplicationName > vApplicationNameFrom AndAlso pUserLoginKey.ApplicationName <= vApplicationNameTo) AndAlso (pUserLoginKey.ApplicationIdentifier > vApplicationIdentifierFrom AndAlso pUserLoginKey.ApplicationIdentifier <= vApplicationIdentifierTo) Then 
        Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone() 
        pUserLoginKeys.Add(pUserLoginKeyClone) 
        If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
      End If 
    Next 
    Return pUserLoginKeys 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardApplicationNameAndApplicationIdentifier(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationIdentifier As String, ByVal vApplicationIdentifierWildcardType As clsEnums.enmWildCardType) As csUserLoginKeyCol 
    Dim pUserLoginKeys As New csUserLoginKeyCol 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUserLoginKey As csUserLoginKey In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vApplicationNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pUserLoginKey.ApplicationName.StartsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUserLoginKey.ApplicationName.EndsWith(vApplicationName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUserLoginKey.ApplicationName.IndexOf(vApplicationName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vApplicationNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vApplicationName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUserLoginKey.ApplicationName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vApplicationIdentifierWildcardType = clsEnums.enmWildCardType.After Then 
        If pUserLoginKey.ApplicationIdentifier.StartsWith(vApplicationIdentifier, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationIdentifierWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUserLoginKey.ApplicationIdentifier.EndsWith(vApplicationIdentifier, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vApplicationIdentifierWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUserLoginKey.ApplicationIdentifier.IndexOf(vApplicationIdentifier, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vApplicationIdentifierWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vApplicationIdentifier.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUserLoginKey.ApplicationIdentifier.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone() 
      pUserLoginKeys.Add(pUserLoginKeyClone) 
    Next 
    Return pUserLoginKeys 
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
      Dim pFunction As String = "csUserLoginKeyColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKeyCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150314-1803", vRequester) 
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
  Public Function FindByID(ByVal vID As Long) As csUserLoginKey
    If Me.Count = 0 Then Return New csUserLoginKey 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
    
    Dim pUserLoginKey As csUserLoginKey = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pUserLoginKey) 
    If pUserLoginKey IsNot Nothing Then Return pUserLoginKey Else Return New csUserLoginKey() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByApplicationNameAndApplicationIdentifier(ByVal vApplicationName As String, ByVal vApplicationIdentifier As String) As csUserLoginKey
    If Me.Count = 0 Then Return New csUserLoginKey 
    
    If _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = True Then LoadApplicationNameAndApplicationIdentifiers() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csUserLoginKey) = _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier 
    
    Dim pUserLoginKey As csUserLoginKey = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vApplicationName & "|" & vApplicationIdentifier
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pUserLoginKey) 
    If pUserLoginKey IsNot Nothing Then Return pUserLoginKey Else Return New csUserLoginKey() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserID(ByVal vUserID As Long) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.UserID = vUserID Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserID with vUserID of {vUserID}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.UserID = vUserID Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApplicationName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplicationName(ByVal vApplicationName As String) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplicationName = vApplicationName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplicationName with vApplicationName of {vApplicationName}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.ApplicationName.ToLowerInvariant() = vApplicationName Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApplicationIdentifier
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplicationIdentifier(ByVal vApplicationIdentifier As String) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplicationIdentifier = vApplicationIdentifier.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.ApplicationIdentifier.ToLowerInvariant() = vApplicationIdentifier Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplicationIdentifier with vApplicationIdentifier of {vApplicationIdentifier}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.ApplicationIdentifier.ToLowerInvariant() = vApplicationIdentifier Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ExternalIPAtCreation
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByExternalIPAtCreation(ByVal vExternalIPAtCreation As String) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vExternalIPAtCreation = vExternalIPAtCreation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.ExternalIPAtCreation.ToLowerInvariant() = vExternalIPAtCreation Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByExternalIPAtCreation with vExternalIPAtCreation of {vExternalIPAtCreation}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.ExternalIPAtCreation.ToLowerInvariant() = vExternalIPAtCreation Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined CountryAtCreation
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCountryAtCreation(ByVal vCountryAtCreation As String) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCountryAtCreation = vCountryAtCreation.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.CountryAtCreation.ToLowerInvariant() = vCountryAtCreation Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCountryAtCreation with vCountryAtCreation of {vCountryAtCreation}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.CountryAtCreation.ToLowerInvariant() = vCountryAtCreation Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastAccessTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastAccessTime(ByVal vLastAccessTime As Date) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.LastAccessTime = vLastAccessTime Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastAccessTime with vLastAccessTime of {vLastAccessTime}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.LastAccessTime = vLastAccessTime Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoggedLoginID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoggedLoginID(ByVal vLoggedLoginID As Long) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.LoggedLoginID = vLoggedLoginID Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoggedLoginID with vLoggedLoginID of {vLoggedLoginID}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.LoggedLoginID = vLoggedLoginID Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csUserLoginKeyCol
    Dim pUserLoginKeys As New csUserLoginKeyCol(_WithParents) 
    pUserLoginKeys._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUserLoginKey) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUserLoginKey As csUserLoginKey In pTempDist.Values
        If pUserLoginKey.Tag.ToLowerInvariant() = vTag Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csUserLoginKeyCol = Me.Clone() 
      For Each pUserLoginKey As csUserLoginKey In pList 
        If pUserLoginKey.Tag.ToLowerInvariant() = vTag Then
          Dim pUserLoginKeyClone As csUserLoginKey = pUserLoginKey.Clone()
          pUserLoginKeys.Add(pUserLoginKeyClone)
          If Not _FilledFromSumOnTheFly Then pUserLoginKeys._Clean.Add(pUserLoginKey.ID) 
        End If
      Next
    End If 
    
    Return pUserLoginKeys
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
    For Each pUserLoginKey As csUserLoginKey In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pUserLoginKey.LoadDataRow(pRow, vRequester) 
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
    For Each p As csUserLoginKey In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As csUserLoginKey = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pUserLoginKeyToKill As New csUserLoginKey 
        pUserLoginKeyToKill.ID = pCleanID 
        pUserLoginKeyToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pUserLoginKeyToKill) 
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
      Dim pFunction As String = "csUserLoginKeyColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKeyCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150314-1803", vRequester) 
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
      Dim pFunction As String = "csUserLoginKeyColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserLoginKeyCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "csUserLoginKeyColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
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
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserID 
          pBinaryWriter.Write(vUserID) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColDeleteByUserID" 
      Dim pParametersToLog = $"UserID: {vUserID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-UserLoginKey-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "csUserLoginKeyColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific ApplicationNameAndApplicationIdentifier
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedApplicationNameAndApplicationIdentifier(ByVal vApplicationNameFrom As String, ByVal vApplicationNameTo As String, ByVal vApplicationIdentifierFrom As String, ByVal vApplicationIdentifierTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationNameFrom={0}, ApplicationNameTo={1}, ApplicationIdentifierFrom={2}, ApplicationIdentifierTo={3}", vApplicationNameFrom, vApplicationNameTo, vApplicationIdentifierFrom, vApplicationIdentifierTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationNameFrom 
          If vApplicationNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameFrom) 
          ' 
          'vApplicationNameTo 
          If vApplicationNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationNameTo) 
          ' 
          'vApplicationIdentifierFrom 
          If vApplicationIdentifierFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifierFrom) 
          ' 
          'vApplicationIdentifierTo 
          If vApplicationIdentifierTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifierTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColDeleteByBoundedApplicationNameAndApplicationIdentifier" 
      Dim pParametersToLog = $"ApplicationNameAndApplicationIdentifier: {vApplicationNameFrom};{vApplicationNameTo};{vApplicationIdentifierFrom};{vApplicationIdentifierTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded ApplicationNameAndApplicationIdentifier
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardApplicationNameAndApplicationIdentifier(ByVal vApplicationName As String, ByVal vApplicationNameWildcardType As clsEnums.enmWildCardType, ByVal vApplicationIdentifier As String, ByVal vApplicationIdentifierWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("ApplicationName={0}, ApplicationNameWildcardType={1}, ApplicationIdentifier={2}, ApplicationIdentifierWildcardType={3}", vApplicationName, vApplicationNameWildcardType.FastToString(), vApplicationIdentifier, vApplicationIdentifierWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vApplicationName 
          If vApplicationName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationName) 
          ' 
          pBinaryWriter.Write(vApplicationNameWildcardType.FastToString())
          'vApplicationIdentifier 
          If vApplicationIdentifier Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplicationIdentifier) 
          ' 
          pBinaryWriter.Write(vApplicationIdentifierWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserLoginKeyColDeleteByWildCardApplicationNameAndApplicationIdentifier" 
      Dim pParametersToLog = $"ApplicationNameAndApplicationIdentifier: {vApplicationName};{vApplicationNameWildcardType.FastToString()};{vApplicationIdentifier};{vApplicationIdentifierWildcardType.FastToString()};" 
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
    Me.Sort(New csUserLoginKeyCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
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
    Me.Sort(New csUserLoginKeyCol.CompareByUserID)
  End Sub
  Private Class CompareByUserID
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
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
    Me.Sort(New csUserLoginKeyCol.CompareByUserText)
  End Sub
  Private Class CompareByUserText
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserText, y.UserText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApplicationName()
    Me.Sort(New csUserLoginKeyCol.CompareByApplicationName)
  End Sub
  Private Class CompareByApplicationName
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApplicationName, y.ApplicationName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApplicationIdentifier()
    Me.Sort(New csUserLoginKeyCol.CompareByApplicationIdentifier)
  End Sub
  Private Class CompareByApplicationIdentifier
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApplicationIdentifier, y.ApplicationIdentifier, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByExternalIPAtCreation()
    Me.Sort(New csUserLoginKeyCol.CompareByExternalIPAtCreation)
  End Sub
  Private Class CompareByExternalIPAtCreation
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ExternalIPAtCreation, y.ExternalIPAtCreation, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCountryAtCreation()
    Me.Sort(New csUserLoginKeyCol.CompareByCountryAtCreation)
  End Sub
  Private Class CompareByCountryAtCreation
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.CountryAtCreation, y.CountryAtCreation, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastAccessTime()
    Me.Sort(New csUserLoginKeyCol.CompareByLastAccessTime)
  End Sub
  Private Class CompareByLastAccessTime
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LastAccessTime < y.LastAccessTime Then
        Return -1
      ElseIf x.LastAccessTime = y.LastAccessTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLoggedLoginID()
    Me.Sort(New csUserLoginKeyCol.CompareByLoggedLoginID)
  End Sub
  Private Class CompareByLoggedLoginID
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LoggedLoginID < y.LoggedLoginID Then
        Return -1
      ElseIf x.LoggedLoginID = y.LoggedLoginID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csUserLoginKeyCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csUserLoginKey)
    Private Function Compare(ByVal x As csUserLoginKey, ByVal y As csUserLoginKey) As Integer Implements System.Collections.Generic.IComparer(Of csUserLoginKey).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUserLoginKey) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier = New Dictionary(Of String, csUserLoginKey)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByApplicationNameAndApplicationIdentifier = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUserLoginKey) 
    _SortedDictionaryForFindByApplicationNameAndApplicationIdentifier = New Dictionary(Of String, csUserLoginKey)(StringComparer.OrdinalIgnoreCase) 
 
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
  
