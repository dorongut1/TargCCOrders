Public Class csUser
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
    [Type] 
    [Language] 
    [Role] 
    [AuthenticationMethod] 
    [MessagingMode] 
    [SecurityQuestion1] 
    [SecurityQuestion2] 
    [SecurityQuestion3] 
  End Enum 
  'Child Properties 
  Public Enum enmChildProperty 
    UD 
    [JobAlertRecipient] 
    [LoggedAlertsForAffectedUser] 
    [LoggedRequest] 
    [MFA] 
    [UserLoginKey] 
    [UserPermission] 
    [UserStatus] 
  End Enum 
  ''Intrinsic Properties 
  Public Enum enmProperty 
    UD 
    [ID] 
    [UserName] 
    [LastName] 
    [FirstName] 
    [FullName] 
    [NationalIDNo] 
    [Address] 
    [City] 
    [ProvinceState] 
    [PostalCode] 
    [Country] 
    [PhoneNumber] 
    [Email] 
    [PasswordHashed] 
    [DatePasswordChanged] 
    [Type] 
    [IDinType] 
    [RequiresComputerIdentification] 
    [EnableSimultaneousLogins] 
    [DateActivated] 
    [IsDisabled] 
    [ExpiryDate] 
    [Comments] 
    [LastPasswords] 
    [Applications] 
    [Language] 
    [IsLockedOut] 
    [Role] 
    [AuthenticationMethod] 
    [RequiresFixedIP] 
    [MessagingMode] 
    [LoggedInIP] 
    [ApprovalCodeHashed] 
    [ApprovalFunctionName] 
    [ApprovalTime] 
    [LastSuccessfulLogin] 
    [PasswordNeverExpires] 
    [SecurityQuestion1] 
    [SecurityQuestion1Response] 
    [SecurityQuestion2] 
    [SecurityQuestion2Response] 
    [SecurityQuestion3] 
    [SecurityQuestion3Response] 
    [PIN] 
    [Tag] 
  End Enum 
  Public Enum enmSummarizeableProperty 
    UD 
    [IDinType] 
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
    [PasswordHashed] 
    [Comments] 
    [Applications] 
    [LoggedInIP] 
    [LastSuccessfulLogin] 
    [SecurityQuestion1Response] 
    [SecurityQuestion2Response] 
    [SecurityQuestion3Response] 
    [PIN] 
    [ccUpdateApprovalShared] 
  End Enum 
  ''' <summary> 
  ''' Raised before updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtBeforeUpdate(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean) 
  Friend Event evtBeforeUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  Friend Shared Event evtBeforeSharedUpdateWithRequester(ByVal vUpdateType As enmUpdateType, ByRef rCancel As Boolean, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
  ''' <summary> 
  ''' Raised after updating or adding a row 
  ''' </summary> 
  ''' <remarks></remarks> 
  Public Event evtAfterUpdate(ByVal vWhichColumn As enmUpdateType)
  Friend Event evtAfterUpdateWithRequester(ByVal vWhichColumn As enmUpdateType, ByVal vRequester As clsRequester, ByRef rFault As clsFault)
  Friend Shared Event evtAfterSharedUpdateWithRequester(ByVal vUpdateType As enmUpdateType, ByVal vID As Long, ByVal vRequester As clsRequester, ByRef rFault As clsFault) 
  
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
  Private _WithParents As clsEnums.enmLoadParent
  <Newtonsoft.Json.JsonIgnore>
  Public ReadOnly Property WithParents() As clsEnums.enmLoadParent
    Get
      Return Me._WithParents
    End Get
  End Property
  
  Private _ID As Long
  Private _UserName As String
  Private _LastName As String
  Private _FirstName As String
  Private _FullName As String
  Private _NationalIDNo As String
  Private _Address As String
  Private _City As String
  Private _ProvinceState As String
  Private _PostalCode As String
  Private _Country As String
  Private _PhoneNumber As String
  Private _Email As String
  Private _PasswordHashed As String
  Private _DatePasswordChanged As Date
  Private _Type As clsEnums.enmUserIdentityType
  Private _TypeText As String 
  Private _IDinType As Long
  Private _RequiresComputerIdentification As Boolean
  Private _EnableSimultaneousLogins As Boolean
  Private _DateActivated As Date
  Private _IsDisabled As Boolean
  Private _ExpiryDate As Date
  Private _Comments As String
  Private _LastPasswords As String
  Private _Applications As String
  Private _Language As clsEnums.enmLanguage
  Private _LanguageText As String 
  Private _IsLockedOut As Boolean
  Private _RoleID As Long
  Private _Role As csRole
  Private _RoleText As String
  Private _AuthenticationMethod As clsEnums.enmAuthenticationMethod
  Private _AuthenticationMethodText As String 
  Private _RequiresFixedIP As Boolean
  Private _MessagingMode As clsEnums.enmMessagingMode
  Private _MessagingModeText As String 
  Private _LoggedInIP As String
  Private _ApprovalCodeHashed As String
  Private _ApprovalFunctionName As String
  Private _ApprovalTime As DateTimeOffset
  Private _LastSuccessfulLogin As DateTimeOffset
  Private _PasswordNeverExpires As Boolean
  Private _SecurityQuestion1Code As String
  Private _SecurityQuestion1Text As String 
  Private _SecurityQuestion1Response As String
  Private _SecurityQuestion2Code As String
  Private _SecurityQuestion2Text As String 
  Private _SecurityQuestion2Response As String
  Private _SecurityQuestion3Code As String
  Private _SecurityQuestion3Text As String 
  Private _SecurityQuestion3Response As String
  Private _PIN As String
  Private _Tag As String
  Private _JobAlertRecipients As csJobAlertRecipientCol
  Private _LoggedAlertsForAffectedUsers As csLoggedAlertCol
  Private _LoggedRequests As csLoggedRequestCol
  Private _MFA As csMFA
  Private _UserLoginKeys As csUserLoginKeyCol
  Private _UserPermissions As csUserPermissionCol
  Private _UserStatuss As csUserStatusCol
  
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
  Public Property [UserName]() As String
    Get
      Return Me._UserName
    End Get
    Set(ByVal value As String)
      If Me._UserName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._UserName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [LastName]() As String
    Get
      Return Me._LastName
    End Get
    Set(ByVal value As String)
      If Me._LastName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._LastName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public Property [FirstName]() As String
    Get
      Return Me._FirstName
    End Get
    Set(ByVal value As String)
      If Me._FirstName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._FirstName = value 
        CreateDefaultDesignation() 
      End If 
    End Set
  End Property
  Public ReadOnly Property [FullName]() As String
    Get
      Return Me._FullName
    End Get
  End Property
  Public Property [NationalIDNo]() As String
    Get
      Return Me._NationalIDNo
    End Get
    Set(ByVal value As String)
      If Me._NationalIDNo <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._NationalIDNo = value 
      End If 
    End Set
  End Property
  Public Property [Address]() As String
    Get
      Return Me._Address
    End Get
    Set(ByVal value As String)
      If Me._Address <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Address = value 
      End If 
    End Set
  End Property
  Public Property [City]() As String
    Get
      Return Me._City
    End Get
    Set(ByVal value As String)
      If Me._City <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._City = value 
      End If 
    End Set
  End Property
  Public Property [ProvinceState]() As String
    Get
      Return Me._ProvinceState
    End Get
    Set(ByVal value As String)
      If Me._ProvinceState <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ProvinceState = value 
      End If 
    End Set
  End Property
  Public Property [PostalCode]() As String
    Get
      Return Me._PostalCode
    End Get
    Set(ByVal value As String)
      If Me._PostalCode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PostalCode = value 
      End If 
    End Set
  End Property
  Public Property [Country]() As String
    Get
      Return Me._Country
    End Get
    Set(ByVal value As String)
      If Me._Country <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Country = value 
      End If 
    End Set
  End Property
  Public Property [PhoneNumber]() As String
    Get
      Return Me._PhoneNumber
    End Get
    Set(ByVal value As String)
      If Me._PhoneNumber <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PhoneNumber = value 
      End If 
    End Set
  End Property
  Public Property [Email]() As String
    Get
      Return Me._Email
    End Get
    Set(ByVal value As String)
      If Me._Email <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Email = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' If you want to hash (SHA256) the input, then prefix it with 'PleaseHash'. Otherwise, use ccHelper.Encrypt(ccHelper.enmHashType.SHA256, ValueToHash) 
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property [PasswordHashed]() As String
    Get
      Return Me._PasswordHashed
    End Get
  End Property
  Public ReadOnly Property [DatePasswordChanged]() As Date
    Get
      Return Me._DatePasswordChanged
    End Get
  End Property
  Public Property [Type]() As clsEnums.enmUserIdentityType
    Get
      Return Me._Type
    End Get
    Set(ByVal value As clsEnums.enmUserIdentityType)
      If Me._Type <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._Type = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [TypeText]() As String
    Get
      Return Me._TypeText
    End Get
    Set(ByVal value As String)
      Me._TypeText = value
    End Set
  End Property
  Public Property [IDinType]() As Long
    Get
      Return Me._IDinType
    End Get
    Set(ByVal value As Long)
      If Me._IDinType <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IDinType = value 
      End If 
    End Set
  End Property
  Public Property [RequiresComputerIdentification]() As Boolean
    Get
      Return Me._RequiresComputerIdentification
    End Get
    Set(ByVal value As Boolean)
      If Me._RequiresComputerIdentification <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RequiresComputerIdentification = value 
      End If 
    End Set
  End Property
  Public Property [EnableSimultaneousLogins]() As Boolean
    Get
      Return Me._EnableSimultaneousLogins
    End Get
    Set(ByVal value As Boolean)
      If Me._EnableSimultaneousLogins <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._EnableSimultaneousLogins = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [DateActivated]() As Date
    Get
      Return Me._DateActivated
    End Get
  End Property
  Public Property [IsDisabled]() As Boolean
    Get
      Return Me._IsDisabled
    End Get
    Set(ByVal value As Boolean)
      If Me._IsDisabled <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsDisabled = value 
      End If 
    End Set
  End Property
  Public Property [ExpiryDate]() As Date
    Get
      Return Me._ExpiryDate
    End Get
    Set(ByVal value As Date)
      If Me._ExpiryDate <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ExpiryDate = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [Comments]() As String
    Get
      Return Me._Comments
    End Get
  End Property
  Public ReadOnly Property [LastPasswords]() As String
    Get
      Return Me._LastPasswords
    End Get
  End Property
  Public ReadOnly Property [Applications]() As String
    Get
      Return Me._Applications
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
  Public Property [IsLockedOut]() As Boolean
    Get
      Return Me._IsLockedOut
    End Get
    Set(ByVal value As Boolean)
      If Me._IsLockedOut <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._IsLockedOut = value 
      End If 
    End Set
  End Property
  Public Property [RoleID]() As Long
    Get
      Return Me._RoleID
    End Get
    Set(ByVal value As Long)
      If Me._RoleID <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RoleID = value 
      End If 
    End Set
  End Property
  Public Property [Role]() As csRole
    Get
      Return Me._Role
    End Get
    Set(ByVal value As csRole)
      Me._Role = value
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text from the Role object.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property RoleText() As String
    Get
      Return Me._RoleText
    End Get
    Set(ByVal value As String)
      Me._RoleText = value
    End Set
  End Property
  Public Property [AuthenticationMethod]() As clsEnums.enmAuthenticationMethod
    Get
      Return Me._AuthenticationMethod
    End Get
    Set(ByVal value As clsEnums.enmAuthenticationMethod)
      If Me._AuthenticationMethod <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._AuthenticationMethod = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [AuthenticationMethodText]() As String
    Get
      Return Me._AuthenticationMethodText
    End Get
    Set(ByVal value As String)
      Me._AuthenticationMethodText = value
    End Set
  End Property
  Public Property [RequiresFixedIP]() As Boolean
    Get
      Return Me._RequiresFixedIP
    End Get
    Set(ByVal value As Boolean)
      If Me._RequiresFixedIP <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._RequiresFixedIP = value 
      End If 
    End Set
  End Property
  Public Property [MessagingMode]() As clsEnums.enmMessagingMode
    Get
      Return Me._MessagingMode
    End Get
    Set(ByVal value As clsEnums.enmMessagingMode)
      If Me._MessagingMode <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._MessagingMode = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [MessagingModeText]() As String
    Get
      Return Me._MessagingModeText
    End Get
    Set(ByVal value As String)
      Me._MessagingModeText = value
    End Set
  End Property
  Public ReadOnly Property [LoggedInIP]() As String
    Get
      Return Me._LoggedInIP
    End Get
  End Property
  ''' <summary>
  ''' If you want to hash (SHA256) the input, then prefix it with 'PleaseHash'. Otherwise, use ccHelper.Encrypt(ccHelper.enmHashType.SHA256, ValueToHash) 
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property [ApprovalCodeHashed]() As String
    Get
      Return Me._ApprovalCodeHashed
    End Get
    Set(ByVal value As String)
      If Me._ApprovalCodeHashed <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
         If value.StartsWith("PleaseHash", StringComparison.OrdinalIgnoreCase) Then value = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, value.Substring(10)) 
        Me._ApprovalCodeHashed = value 
      End If 
    End Set
  End Property
  Public Property [ApprovalFunctionName]() As String
    Get
      Return Me._ApprovalFunctionName
    End Get
    Set(ByVal value As String)
      If Me._ApprovalFunctionName <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ApprovalFunctionName = value 
      End If 
    End Set
  End Property
  Public Property [ApprovalTime]() As DateTimeOffset
    Get
      Return Me._ApprovalTime
    End Get
    Set(ByVal value As DateTimeOffset)
      If Me._ApprovalTime <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._ApprovalTime = value 
      End If 
    End Set
  End Property
  Public ReadOnly Property [LastSuccessfulLogin]() As DateTimeOffset
    Get
      Return Me._LastSuccessfulLogin
    End Get
  End Property
  Public Property [PasswordNeverExpires]() As Boolean
    Get
      Return Me._PasswordNeverExpires
    End Get
    Set(ByVal value As Boolean)
      If Me._PasswordNeverExpires <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._PasswordNeverExpires = value 
      End If 
    End Set
  End Property
  Public Property [SecurityQuestion1Code]() As String
    Get
      Return Me._SecurityQuestion1Code
    End Get
    Set(ByVal value As String)
      If Me._SecurityQuestion1Code <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SecurityQuestion1Code = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property SecurityQuestion1Text() As String
    Get
      Return Me._SecurityQuestion1Text
    End Get
    Set(ByVal value As String)
      Me._SecurityQuestion1Text = value
    End Set
  End Property
  Public ReadOnly Property [SecurityQuestion1Response](ByVal vDecrypt As Boolean) As String
    Get
      If vDecrypt = True Then 
        If _SecurityQuestion1Response.Length < 9 OrElse Not _SecurityQuestion1Response.StartsWith("********") Then 
          If String.IsNullOrEmpty(_SecurityQuestion1Response) Then 
            Return "" 
          Else 
            Return "!!Invalid SecurityQuestion1Response!!" 
          End If 
        End If 
        Return ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, _SecurityQuestion1Response.Substring(8)) 
      Else 
        Return Me._SecurityQuestion1Response 
      End If 
    End Get
  End Property
  Public Property [SecurityQuestion2Code]() As String
    Get
      Return Me._SecurityQuestion2Code
    End Get
    Set(ByVal value As String)
      If Me._SecurityQuestion2Code <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SecurityQuestion2Code = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property SecurityQuestion2Text() As String
    Get
      Return Me._SecurityQuestion2Text
    End Get
    Set(ByVal value As String)
      Me._SecurityQuestion2Text = value
    End Set
  End Property
  Public ReadOnly Property [SecurityQuestion2Response](ByVal vDecrypt As Boolean) As String
    Get
      If vDecrypt = True Then 
        If _SecurityQuestion2Response.Length < 9 OrElse Not _SecurityQuestion2Response.StartsWith("********") Then 
          If String.IsNullOrEmpty(_SecurityQuestion2Response) Then 
            Return "" 
          Else 
            Return "!!Invalid SecurityQuestion2Response!!" 
          End If 
        End If 
        Return ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, _SecurityQuestion2Response.Substring(8)) 
      Else 
        Return Me._SecurityQuestion2Response 
      End If 
    End Get
  End Property
  Public Property [SecurityQuestion3Code]() As String
    Get
      Return Me._SecurityQuestion3Code
    End Get
    Set(ByVal value As String)
      If Me._SecurityQuestion3Code <> value Then 
        bccStatus = clsEnums.enmObjectStatus.Dirty 
        Me._SecurityQuestion3Code = value 
      End If 
    End Set
  End Property
  ''' <summary>
  ''' This is a dummy field, used for sorting purposes. Fill it with text by running LoadLookupAndEnumText
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property SecurityQuestion3Text() As String
    Get
      Return Me._SecurityQuestion3Text
    End Get
    Set(ByVal value As String)
      Me._SecurityQuestion3Text = value
    End Set
  End Property
  Public ReadOnly Property [SecurityQuestion3Response](ByVal vDecrypt As Boolean) As String
    Get
      If vDecrypt = True Then 
        If _SecurityQuestion3Response.Length < 9 OrElse Not _SecurityQuestion3Response.StartsWith("********") Then 
          If String.IsNullOrEmpty(_SecurityQuestion3Response) Then 
            Return "" 
          Else 
            Return "!!Invalid SecurityQuestion3Response!!" 
          End If 
        End If 
        Return ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, _SecurityQuestion3Response.Substring(8)) 
      Else 
        Return Me._SecurityQuestion3Response 
      End If 
    End Get
  End Property
  Public ReadOnly Property [PIN](ByVal vDecrypt As Boolean) As String
    Get
      If vDecrypt = True Then 
        If _PIN.Length < 9 OrElse Not _PIN.StartsWith("********") Then 
          If String.IsNullOrEmpty(_PIN) Then 
            Return "" 
          Else 
            Return "!!Invalid PIN!!" 
          End If 
        End If 
        Return ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, _PIN.Substring(8)) 
      Else 
        Return Me._PIN 
      End If 
    End Get
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
  Public Property [JobAlertRecipients]() As csJobAlertRecipientCol
    Get
      Return Me._JobAlertRecipients
    End Get
    Set(ByVal value As csJobAlertRecipientCol)
      Me._JobAlertRecipients = value
    End Set
  End Property
  Public Property [LoggedAlertsForAffectedUsers]() As csLoggedAlertCol
    Get
      Return Me._LoggedAlertsForAffectedUsers
    End Get
    Set(ByVal value As csLoggedAlertCol)
      Me._LoggedAlertsForAffectedUsers = value
    End Set
  End Property
  Public Property [LoggedRequests]() As csLoggedRequestCol
    Get
      Return Me._LoggedRequests
    End Get
    Set(ByVal value As csLoggedRequestCol)
      Me._LoggedRequests = value
    End Set
  End Property
  Public Property [MFA]() As csMFA
    Get
      Return Me._MFA
    End Get
    Set(ByVal value As csMFA)
      Me._MFA = value
    End Set
  End Property
  Public Property [UserLoginKeys]() As csUserLoginKeyCol
    Get
      Return Me._UserLoginKeys
    End Get
    Set(ByVal value As csUserLoginKeyCol)
      Me._UserLoginKeys = value
    End Set
  End Property
  Public Property [UserPermissions]() As csUserPermissionCol
    Get
      Return Me._UserPermissions
    End Get
    Set(ByVal value As csUserPermissionCol)
      Me._UserPermissions = value
    End Set
  End Property
  Public Property [UserStatuss]() As csUserStatusCol
    Get
      Return Me._UserStatuss
    End Get
    Set(ByVal value As csUserStatusCol)
      Me._UserStatuss = value
    End Set
  End Property
  
  Private Sub CreateDefaultDesignation() 
    Dim pOverridenValue As String = Nothing 
    RaiseEvent evtOverrideDefaultDesignation(pOverridenValue) 
    If pOverridenValue = Nothing Then bDefaultDesignation = _FirstName & " " & _LastName & " (" & _UserName & ")" Else bDefaultDesignation = pOverridenValue 
  End Sub 
 
  'ToString  
  Public Overrides Function ToString() As String 
    Dim pValue As New System.Text.StringBuilder 
    If Not Me.IsEmpty Then pValue.Append("ID='" & _ID.ToString() & "' ‡ ") 
    If _UserName <> "" Then pValue.Append("UserName='" & _UserName & "' ‡ ") 
    If _LastName <> "" Then pValue.Append("LastName='" & _LastName & "' ‡ ") 
    If _FirstName <> "" Then pValue.Append("FirstName='" & _FirstName & "' ‡ ") 
    If _FullName <> "" Then pValue.Append("FullName='" & _FullName & "' ‡ ") 
    If _NationalIDNo <> "" Then pValue.Append("NationalIDNo='" & _NationalIDNo & "' ‡ ") 
    If _Address <> "" Then pValue.Append("Address='" & _Address & "' ‡ ") 
    If _City <> "" Then pValue.Append("City='" & _City & "' ‡ ") 
    If _ProvinceState <> "" Then pValue.Append("ProvinceState='" & _ProvinceState & "' ‡ ") 
    If _PostalCode <> "" Then pValue.Append("PostalCode='" & _PostalCode & "' ‡ ") 
    If _Country <> "" Then pValue.Append("Country='" & _Country & "' ‡ ") 
    If _PhoneNumber <> "" Then pValue.Append("PhoneNumber='" & _PhoneNumber & "' ‡ ") 
    If _Email <> "" Then pValue.Append("Email='" & _Email & "' ‡ ") 
    If _PasswordHashed <> "" Then pValue.Append("Password='*****' ‡ ") 
    If Not (_DatePasswordChanged = Nothing) Then pValue.Append("DatePasswordChanged='" & _DatePasswordChanged.ToString("o") & "' ‡ ") 
    If _Type <> clsEnums.enmUserIdentityType.UD Then pValue.Append("Type='" & _Type.FastToString() & "' ‡ ") 
    If _TypeText <> "" Then pValue.Append("TypeText='" & _TypeText & "' ‡ ") 
    If _IDinType <> 0 Then pValue.Append("IDinType='" & _IDinType.ToString() & "' ‡ ") 
    pValue.Append("RequiresComputerIdentification='" & _RequiresComputerIdentification.ToString() & "' ‡ ") 
    pValue.Append("EnableSimultaneousLogins='" & _EnableSimultaneousLogins.ToString() & "' ‡ ") 
    If Not (_DateActivated = Nothing) Then pValue.Append("DateActivated='" & _DateActivated.ToString("o") & "' ‡ ") 
    pValue.Append("IsDisabled='" & _IsDisabled.ToString() & "' ‡ ") 
    If Not (_ExpiryDate = Nothing) Then pValue.Append("ExpiryDate='" & _ExpiryDate.ToString("o") & "' ‡ ") 
    If _Comments <> "" Then pValue.Append("Comments='" & _Comments & "' ‡ ") 
    If _LastPasswords <> "" Then pValue.Append("LastPasswords='" & _LastPasswords & "' ‡ ") 
    If _Applications <> "" Then pValue.Append("Applications='" & _Applications & "' ‡ ") 
    If _Language <> clsEnums.enmLanguage.UD Then pValue.Append("Language='" & _Language.FastToString() & "' ‡ ") 
    If _LanguageText <> "" Then pValue.Append("LanguageText='" & _LanguageText & "' ‡ ") 
    pValue.Append("IsLockedOut='" & _IsLockedOut.ToString() & "' ‡ ") 
    If _RoleID <> 0 Then pValue.Append("RoleID='" & _RoleID.ToString() & "' ‡ ") 
    If _RoleText <> "" Then pValue.Append("RoleText='" & _RoleText & "' ‡ ") 
    If _AuthenticationMethod <> clsEnums.enmAuthenticationMethod.UD Then pValue.Append("AuthenticationMethod='" & _AuthenticationMethod.FastToString() & "' ‡ ") 
    If _AuthenticationMethodText <> "" Then pValue.Append("AuthenticationMethodText='" & _AuthenticationMethodText & "' ‡ ") 
    pValue.Append("RequiresFixedIP='" & _RequiresFixedIP.ToString() & "' ‡ ") 
    If _MessagingMode <> clsEnums.enmMessagingMode.UD Then pValue.Append("MessagingMode='" & _MessagingMode.FastToString() & "' ‡ ") 
    If _MessagingModeText <> "" Then pValue.Append("MessagingModeText='" & _MessagingModeText & "' ‡ ") 
    If _LoggedInIP <> "" Then pValue.Append("LoggedInIP='" & _LoggedInIP & "' ‡ ") 
    If _ApprovalCodeHashed <> "" Then pValue.Append("ApprovalCode='*****' ‡ ") 
    If _ApprovalFunctionName <> "" Then pValue.Append("ApprovalFunctionName='" & _ApprovalFunctionName & "' ‡ ") 
    If Not (_ApprovalTime = Nothing) Then pValue.Append("ApprovalTime='" & _ApprovalTime.ToString("o") & "' ‡ ") 
    If Not (_LastSuccessfulLogin = Nothing) Then pValue.Append("LastSuccessfulLogin='" & _LastSuccessfulLogin.ToString("o") & "' ‡ ") 
    pValue.Append("PasswordNeverExpires='" & _PasswordNeverExpires.ToString() & "' ‡ ") 
    If _SecurityQuestion1Code <> "" Then pValue.Append("SecurityQuestion1Code='" & _SecurityQuestion1Code & "' ‡ ") 
    If _SecurityQuestion1Text <> "" Then pValue.Append("SecurityQuestion1Text='" & _SecurityQuestion1Text & "' ‡ ") 
    If _SecurityQuestion1Response <> "" Then pValue.Append("SecurityQuestion1Response='*****' ‡ ") 
    If _SecurityQuestion2Code <> "" Then pValue.Append("SecurityQuestion2Code='" & _SecurityQuestion2Code & "' ‡ ") 
    If _SecurityQuestion2Text <> "" Then pValue.Append("SecurityQuestion2Text='" & _SecurityQuestion2Text & "' ‡ ") 
    If _SecurityQuestion2Response <> "" Then pValue.Append("SecurityQuestion2Response='*****' ‡ ") 
    If _SecurityQuestion3Code <> "" Then pValue.Append("SecurityQuestion3Code='" & _SecurityQuestion3Code & "' ‡ ") 
    If _SecurityQuestion3Text <> "" Then pValue.Append("SecurityQuestion3Text='" & _SecurityQuestion3Text & "' ‡ ") 
    If _SecurityQuestion3Response <> "" Then pValue.Append("SecurityQuestion3Response='*****' ‡ ") 
    If _PIN <> "" Then pValue.Append("PIN='*****' ‡ ") 
    If _Tag <> "" Then pValue.Append("Tag='" & _Tag & "' ‡ ") 
    If Not (bDateAdded = Nothing) Then pValue.Append("DateAdded='" & bDateAdded.ToString("o") & "' ‡ ") 
    
    Return pValue.ToString() 
  End Function 
  
  'ToCSV 
  Public Overrides Function ToCSV(Optional ByVal vWithTexts As Boolean = False) As String 
    Dim pCSV As New System.Text.StringBuilder 
 
      'http://superuser.com/questions/542927/increase-size-limit-of-data-import-from-csv-into-excel  (There is a weird bug in Excel.) 
 
    pCSV.Append("" & _ID.ToString() & "") 
    pCSV.Append($",""{ccHelper.StringForCSV(_UserName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FirstName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_FullName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_NationalIDNo)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Address)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_City)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ProvinceState)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_PostalCode)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Country)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_PhoneNumber)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Email)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DatePasswordChanged.ToShortDateString & " " & _DatePasswordChanged.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Type.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_TypeText)}""") 
    pCSV.Append("," & _IDinType.ToString() & "") 
    pCSV.Append(",""" & _RequiresComputerIdentification.ToString() & """") 
    pCSV.Append(",""" & _EnableSimultaneousLogins.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_DateActivated.ToShortDateString & " " & _DateActivated.ToShortTimeString)}""") 
    pCSV.Append(",""" & _IsDisabled.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ExpiryDate.ToShortDateString & " " & _ExpiryDate.ToShortTimeString)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Comments)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastPasswords)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Applications)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_Language.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_LanguageText)}""") 
    pCSV.Append(",""" & _IsLockedOut.ToString() & """") 
    pCSV.Append("," & _RoleID.ToString() & "") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_RoleText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_AuthenticationMethod.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_AuthenticationMethodText)}""") 
    pCSV.Append(",""" & _RequiresFixedIP.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_MessagingMode.FastToString())}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_MessagingModeText)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LoggedInIP)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ApprovalFunctionName)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_ApprovalTime.DateTime.ToShortDateString & " " & _ApprovalTime.DateTime.ToShortTimeString & " " & _ApprovalTime.Offset.TotalMinutes)}""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_LastSuccessfulLogin.DateTime.ToShortDateString & " " & _LastSuccessfulLogin.DateTime.ToShortTimeString & " " & _LastSuccessfulLogin.Offset.TotalMinutes)}""") 
    pCSV.Append(",""" & _PasswordNeverExpires.ToString() & """") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion1Code)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion1Text)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion2Code)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion2Text)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion3Code)}""") 
    If vWithTexts Then pCSV.Append($",""{ccHelper.StringForCSV(_SecurityQuestion3Text)}""") 
    pCSV.Append(",""*****""") 
    pCSV.Append(",""*****""") 
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
  
  Public Sub New(ByVal vcsUser As csUser)
    MyBase.New()
    CreateEmpty()
    AssignValues(vcsUser) 
  End Sub
  
  Public Sub New( 
      vID As Long _ 
    , Optional vUserName As String = "" _ 
    , Optional vLastName As String = "" _ 
    , Optional vFirstName As String = "" _ 
    , Optional vFullName As String = "" _ 
    , Optional vNationalIDNo As String = "" _ 
    , Optional vAddress As String = "" _ 
    , Optional vCity As String = "" _ 
    , Optional vProvinceState As String = "" _ 
    , Optional vPostalCode As String = "" _ 
    , Optional vCountry As String = "" _ 
    , Optional vPhoneNumber As String = "" _ 
    , Optional vEmail As String = "" _ 
    , Optional vPasswordHashed As String = "" _ 
    , Optional vDatePasswordChanged As Date = Nothing _ 
    , Optional vType As clsEnums.enmUserIdentityType = clsEnums.enmUserIdentityType.UD _ 
    , Optional vTypeText As String = "" _ 
    , Optional vIDinType As Long = 0 _ 
    , Optional vRequiresComputerIdentification As Boolean = False _ 
    , Optional vEnableSimultaneousLogins As Boolean = False _ 
    , Optional vDateActivated As Date = Nothing _ 
    , Optional vIsDisabled As Boolean = False _ 
    , Optional vExpiryDate As Date = Nothing _ 
    , Optional vComments As String = "" _ 
    , Optional vLastPasswords As String = "" _ 
    , Optional vApplications As String = "" _ 
    , Optional vLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.en _ 
    , Optional vLanguageText As String = "" _ 
    , Optional vIsLockedOut As Boolean = False _ 
    , Optional vRoleID As Long = 0 _ 
    , Optional vRoleText As String = "" _ 
    , Optional vAuthenticationMethod As clsEnums.enmAuthenticationMethod = clsEnums.enmAuthenticationMethod.UD _ 
    , Optional vAuthenticationMethodText As String = "" _ 
    , Optional vRequiresFixedIP As Boolean = False _ 
    , Optional vMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD _ 
    , Optional vMessagingModeText As String = "" _ 
    , Optional vLoggedInIP As String = "" _ 
    , Optional vApprovalCodeHashed As String = "" _ 
    , Optional vApprovalFunctionName As String = "" _ 
    , Optional vApprovalTime As DateTimeOffset = Nothing _ 
    , Optional vLastSuccessfulLogin As DateTimeOffset = Nothing _ 
    , Optional vPasswordNeverExpires As Boolean = False _ 
    , Optional vSecurityQuestion1Code As String = "" _ 
    , Optional vSecurityQuestion1Text As String = "" _ 
    , Optional vSecurityQuestion1Response As String = "" _ 
    , Optional vSecurityQuestion2Code As String = "" _ 
    , Optional vSecurityQuestion2Text As String = "" _ 
    , Optional vSecurityQuestion2Response As String = "" _ 
    , Optional vSecurityQuestion3Code As String = "" _ 
    , Optional vSecurityQuestion3Text As String = "" _ 
    , Optional vSecurityQuestion3Response As String = "" _ 
    , Optional vPIN As String = "" _ 
    , Optional vTag As String = "" _ 
    , Optional vDateAdded As DateTime = Nothing _ 
    , Optional vWithParents As clsEnums.enmLoadParent = clsEnums.enmLoadParent.DoNotLoad _ 
) 
    MyBase.New()
    CreateEmpty()
 
    _ID = vID 
    _UserName = vUserName 
    _LastName = vLastName 
    _FirstName = vFirstName 
    _FullName = vFullName 
    _NationalIDNo = vNationalIDNo 
    _Address = vAddress 
    _City = vCity 
    _ProvinceState = vProvinceState 
    _PostalCode = vPostalCode 
    _Country = vCountry 
    _PhoneNumber = vPhoneNumber 
    _Email = vEmail 
    _PasswordHashed = vPasswordHashed 
    _DatePasswordChanged = vDatePasswordChanged 
    _Type = vType 
    _TypeText = vTypeText 
    _IDinType = vIDinType 
    _RequiresComputerIdentification = vRequiresComputerIdentification 
    _EnableSimultaneousLogins = vEnableSimultaneousLogins 
    _DateActivated = vDateActivated 
    _IsDisabled = vIsDisabled 
    _ExpiryDate = vExpiryDate 
    _Comments = vComments 
    _LastPasswords = vLastPasswords 
    _Applications = vApplications 
    _Language = vLanguage 
    _LanguageText = vLanguageText 
    _IsLockedOut = vIsLockedOut 
    _RoleID = vRoleID 
    _RoleText = vRoleText 
    _AuthenticationMethod = vAuthenticationMethod 
    _AuthenticationMethodText = vAuthenticationMethodText 
    _RequiresFixedIP = vRequiresFixedIP 
    _MessagingMode = vMessagingMode 
    _MessagingModeText = vMessagingModeText 
    _LoggedInIP = vLoggedInIP 
    _ApprovalCodeHashed = vApprovalCodeHashed 
    _ApprovalFunctionName = vApprovalFunctionName 
    _ApprovalTime = vApprovalTime 
    _LastSuccessfulLogin = vLastSuccessfulLogin 
    _PasswordNeverExpires = vPasswordNeverExpires 
    _SecurityQuestion1Code = vSecurityQuestion1Code 
    _SecurityQuestion1Text = vSecurityQuestion1Text 
    _SecurityQuestion1Response = vSecurityQuestion1Response 
    _SecurityQuestion2Code = vSecurityQuestion2Code 
    _SecurityQuestion2Text = vSecurityQuestion2Text 
    _SecurityQuestion2Response = vSecurityQuestion2Response 
    _SecurityQuestion3Code = vSecurityQuestion3Code 
    _SecurityQuestion3Text = vSecurityQuestion3Text 
    _SecurityQuestion3Response = vSecurityQuestion3Response 
    _PIN = vPIN 
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
 
    _UserName = _UserName.Truncate(pTruncateLength, _IsTruncated) 
    _LastName = _LastName.Truncate(pTruncateLength, _IsTruncated) 
    _FirstName = _FirstName.Truncate(pTruncateLength, _IsTruncated) 
    _FullName = _FullName.Truncate(pTruncateLength, _IsTruncated) 
    _NationalIDNo = _NationalIDNo.Truncate(pTruncateLength, _IsTruncated) 
    _Address = _Address.Truncate(pTruncateLength, _IsTruncated) 
    _City = _City.Truncate(pTruncateLength, _IsTruncated) 
    _ProvinceState = _ProvinceState.Truncate(pTruncateLength, _IsTruncated) 
    _PostalCode = _PostalCode.Truncate(pTruncateLength, _IsTruncated) 
    _Country = _Country.Truncate(pTruncateLength, _IsTruncated) 
    _PhoneNumber = _PhoneNumber.Truncate(pTruncateLength, _IsTruncated) 
    _Email = _Email.Truncate(pTruncateLength, _IsTruncated) 
    _PasswordHashed = _PasswordHashed.Truncate(pTruncateLength, _IsTruncated) 
    _Comments = _Comments.Truncate(pTruncateLength, _IsTruncated) 
    _LastPasswords = _LastPasswords.Truncate(pTruncateLength, _IsTruncated) 
    _Applications = _Applications.Truncate(pTruncateLength, _IsTruncated) 
    _LoggedInIP = _LoggedInIP.Truncate(pTruncateLength, _IsTruncated) 
    _ApprovalCodeHashed = _ApprovalCodeHashed.Truncate(pTruncateLength, _IsTruncated) 
    _ApprovalFunctionName = _ApprovalFunctionName.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion1Code = _SecurityQuestion1Code.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion1Response = _SecurityQuestion1Response.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion2Code = _SecurityQuestion2Code.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion2Response = _SecurityQuestion2Response.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion3Code = _SecurityQuestion3Code.Truncate(pTruncateLength, _IsTruncated) 
    _SecurityQuestion3Response = _SecurityQuestion3Response.Truncate(pTruncateLength, _IsTruncated) 
    _PIN = _PIN.Truncate(pTruncateLength, _IsTruncated) 
 
  End Sub 
 
  'Inherited Gets 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the User by PrimaryKey (ID) 
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
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-User-151224_0844", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Enum enmGetByParameters 
    [UD] 
    [ID] 
    [UserName] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCEntities have this function. It gets the User by the chosen parameters. This function may be a bit slower than accessing the User's GetBy... directly 
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
        Case enmGetByParameters.UserName 
          pFault = GetByUserName(CStr(vParameters(0)), vRequester, vMustExist) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-User-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault  
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-User-151223_1716", vRequester)  
    End Try  
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Gets the User by ID. 
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
      Dim pFunction As String = "csUserGetByID" 
      Dim pParametersToLog = $"ID: {vID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the User 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150308-1015", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault)
     
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets the User by UserName. 
  ''' </summary>
  ''' <param name="vUserName"></param>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetByUserName(ByVal vUserName As String, ByVal vRequester As clsRequester, Optional vMustExist As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}", vUserName)
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
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Write(vMustExist) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserGetByUserName" 
      Dim pParametersToLog = $"UserName: {vUserName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      'Use the response to build the User 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150308-1015", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-User-151227-1738", vRequester) 
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
      Return pFault.LogFreeTextFault(58, "Received and ID of " & _ID, pFunctionParameters, "TRGT-User-151227-1738", vRequester) 
    Else 
      pFault = Update(vRequester, vReload) : If Not pFault.isOK Then Return pFault 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This updates the User. If there are parents or children in the User, they are NOT updated.  
  ''' Children and Parents remain in the object, even if vReload is true. However, if WithParents = clsEnums.enmLoadParent.EntireObject, the parents are requeried from the database 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vReload"></param> 
  ''' <returns></returns> 
  Public Function Update(ByVal vRequester As clsRequester, Optional ByVal vReload As Boolean = True) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(53, "Object is 'New' - contains no data ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    If _IsTruncated Then 
      Return pFault.LogFreeTextFault(57, "Object is Truncated", pFunctionParameters, "TRGT-User-240611-135714", vRequester) 
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
      Dim pFunction As String = "csUserUpdate" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 
        Dim pID As Long = BitConverter.ToInt64(pResponse, 0) 
        _ID = pID 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
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
  
  Public Function UpdatePasswordHashed(ByVal vPassword As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vPassword 
          If vPassword Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vPassword) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdatePasswordHashed" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _PasswordHashed = vPassword 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.PasswordHashed)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.PasswordHashed, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateComments(ByVal vComments As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vComments 
          If vComments Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vComments) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateComments" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _Comments = vComments 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.Comments)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Comments, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateApplications(ByVal vApplications As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vApplications 
          If vApplications Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vApplications) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateApplications" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _Applications = vApplications 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.Applications)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.Applications, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateLoggedInIP(ByVal vLoggedInIP As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vLoggedInIP 
          If vLoggedInIP Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLoggedInIP) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateLoggedInIP" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _LoggedInIP = vLoggedInIP 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.LoggedInIP)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.LoggedInIP, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateLastSuccessfulLogin(ByVal vLastSuccessfulLogin As DateTimeOffset, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vLastSuccessfulLogin 
          pBinaryWriter.Write(vLastSuccessfulLogin.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLogin.Offset.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateLastSuccessfulLogin" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _LastSuccessfulLogin = vLastSuccessfulLogin 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.LastSuccessfulLogin)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.LastSuccessfulLogin, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateSecurityQuestion1Response(ByVal vSecurityQuestion1Response As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vSecurityQuestion1Response 
          If vSecurityQuestion1Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vSecurityQuestion1Response) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateSecurityQuestion1Response" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _SecurityQuestion1Response = "********" & ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, vSecurityQuestion1Response, "SecurityQuestion1Response", 0) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.SecurityQuestion1Response)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.SecurityQuestion1Response, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateSecurityQuestion2Response(ByVal vSecurityQuestion2Response As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vSecurityQuestion2Response 
          If vSecurityQuestion2Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vSecurityQuestion2Response) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateSecurityQuestion2Response" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _SecurityQuestion2Response = "********" & ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, vSecurityQuestion2Response, "SecurityQuestion2Response", 0) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.SecurityQuestion2Response)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.SecurityQuestion2Response, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdateSecurityQuestion3Response(ByVal vSecurityQuestion3Response As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vSecurityQuestion3Response 
          If vSecurityQuestion3Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vSecurityQuestion3Response) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdateSecurityQuestion3Response" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _SecurityQuestion3Response = "********" & ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, vSecurityQuestion3Response, "SecurityQuestion3Response", 0) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.SecurityQuestion3Response)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.SecurityQuestion3Response, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  Public Function UpdatePIN(ByVal vPIN As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
    Dim pFault As New clsFault 
    
    
    'Check if we got an empty object 
    Dim pUser As New csUser 
    If Me.isEqual(pUser) Then 
      Return pFault.LogFreeTextFault(58, "ID = 0. There's no item to update ", "", "TRGT-User-100113-1813", vRequester) 
    End If 
 
    Dim pCancel As Boolean = False 
    pFault.SetOK() 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pHasValue As Boolean = False 
          pBinaryWriter.Write(_ID) 
          'vPIN 
          If vPIN Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vPIN) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request 
      Dim pFunction As String = "csUserUpdatePIN" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      _PIN = "********" & ccHelper.Encrypt(ccHelper.enmEncryptionMethod.AES, vPIN, "PIN", 0) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
    End Try 
 
    RaiseEvent evtAfterGet()
      RaiseEvent evtAfterUpdate(enmUpdateType.PIN)
      RaiseEvent evtAfterUpdateWithRequester(enmUpdateType.PIN, vRequester, pFault) 
    If pFault.isOK = False Then Return pFault 
    
    Return pFault
  End Function
  
  'Interface Delete 
  Public Function Delete(ByVal vRequester As clsRequester) As clsFault Implements ITargCCEntityDeletable.Delete 
    Dim pFunctionParameters As String = String.Format("User.ID={0}", _ID)
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
      Dim pFunction As String = "csUserDelete" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value 
      CreateEmpty() 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
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
      Dim pFunction As String = "csUserDeleteByID" 
      Dim pParametersToLog = $"ID: {vID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
    Catch ex As Exception 
      Return New clsFault().LogException(73, ex, pFunctionParameters, "TRGT-User-231207-1707", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  ''' <summary>
  ''' Fills the User's JobAlertRecipient collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillJobAlertRecipients(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _JobAlertRecipients = New csJobAlertRecipientCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _JobAlertRecipients.FillByUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the User's LoggedAlert collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedAlertsForAffectedUsers(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _LoggedAlertsForAffectedUsers = New csLoggedAlertCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedAlertsForAffectedUsers.FillByAffectedUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the User's LoggedRequest collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillLoggedRequests(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _LoggedRequests = New csLoggedRequestCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _LoggedRequests.FillByUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Gets the User's MFA (1 to 1 relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function GetMFA(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault
    
    'one  to one children
    _MFA = New csMFA(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _MFA.GetByUserID(_ID, vRequester, vMustExist:=False)
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the User's UserLoginKey collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillUserLoginKeys(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _UserLoginKeys = New csUserLoginKeyCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _UserLoginKeys.FillByUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the User's UserPermission collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillUserPermissions(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _UserPermissions = New csUserPermissionCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _UserPermissions.FillByUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  ''' <summary>
  ''' Fills the User's UserStatus collection (1 to Many relationship)
  ''' </summary>
  ''' <param name="vRequester"></param>
  ''' <param name="vHowMany"></param>
  ''' <param name="vDir"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillUserStatuss(ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFault As New clsFault
    
    _UserStatuss = New csUserStatusCol(_WithParents)
 
    If _ID = 0 Then 
      pFault.SetOK() 
      RaiseEvent evtAfterGet() 
      RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
      Return pFault 
    End If 
 
    pFault = _UserStatuss.FillByUserID(_ID, vRequester, vHowMany, vDir) : If pFault.isOK = False Then Return pFault
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
    If Not (TypeOf (vTargCCEntityToTest) Is csUser) Then Return False 
    Dim pUserToTest As csUser = CType(vTargCCEntityToTest, csUser) 
    Return isEqual(pUserToTest) 
  End Function 
 
  ''' <summary>
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overloads Function isEqual(ByVal vUserToTest As csUser) As Boolean
    With vUserToTest
      If _ID <> .ID Then Return False
      If _UserName <> .UserName Then Return False
      If _LastName <> .LastName Then Return False
      If _FirstName <> .FirstName Then Return False
      If _FullName <> .FullName Then Return False
      If _NationalIDNo <> .NationalIDNo Then Return False
      If _Address <> .Address Then Return False
      If _City <> .City Then Return False
      If _ProvinceState <> .ProvinceState Then Return False
      If _PostalCode <> .PostalCode Then Return False
      If _Country <> .Country Then Return False
      If _PhoneNumber <> .PhoneNumber Then Return False
      If _Email <> .Email Then Return False
      If _PasswordHashed <> .PasswordHashed Then Return False
      If _DatePasswordChanged <> Nothing AndAlso .DatePasswordChanged <> Nothing Then 
        If ccHelper.ToLong(_DatePasswordChanged.Subtract(.DatePasswordChanged).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DatePasswordChanged = Nothing AndAlso .DatePasswordChanged = Nothing) Then 
        Return False 
      End If 
      If _Type <> .Type Then Return False
      If _IDinType <> .IDinType Then Return False
      If _RequiresComputerIdentification <> .RequiresComputerIdentification Then Return False
      If _EnableSimultaneousLogins <> .EnableSimultaneousLogins Then Return False
      If _DateActivated <> Nothing AndAlso .DateActivated <> Nothing Then 
        If ccHelper.ToLong(_DateActivated.Subtract(.DateActivated).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_DateActivated = Nothing AndAlso .DateActivated = Nothing) Then 
        Return False 
      End If 
      If _IsDisabled <> .IsDisabled Then Return False
      If _ExpiryDate <> Nothing AndAlso .ExpiryDate <> Nothing Then 
        If ccHelper.ToLong(_ExpiryDate.Subtract(.ExpiryDate).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ExpiryDate = Nothing AndAlso .ExpiryDate = Nothing) Then 
        Return False 
      End If 
      If _Comments <> .Comments Then Return False
      If _LastPasswords <> .LastPasswords Then Return False
      If _Applications <> .Applications Then Return False
      If _Language <> .Language Then Return False
      If _IsLockedOut <> .IsLockedOut Then Return False
      If _RoleID <> .RoleID Then Return False
      If _AuthenticationMethod <> .AuthenticationMethod Then Return False
      If _RequiresFixedIP <> .RequiresFixedIP Then Return False
      If _MessagingMode <> .MessagingMode Then Return False
      If _LoggedInIP <> .LoggedInIP Then Return False
      If _ApprovalCodeHashed <> .ApprovalCodeHashed Then Return False
      If _ApprovalFunctionName <> .ApprovalFunctionName Then Return False
      If _ApprovalTime <> Nothing AndAlso .ApprovalTime <> Nothing Then 
        If ccHelper.ToLong(_ApprovalTime.Subtract(.ApprovalTime).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_ApprovalTime = Nothing AndAlso .ApprovalTime = Nothing) Then 
        Return False 
      End If 
      If _LastSuccessfulLogin <> Nothing AndAlso .LastSuccessfulLogin <> Nothing Then 
        If ccHelper.ToLong(_LastSuccessfulLogin.Subtract(.LastSuccessfulLogin).TotalSeconds) <> 0 Then Return False 
      ElseIf Not (_LastSuccessfulLogin = Nothing AndAlso .LastSuccessfulLogin = Nothing) Then 
        Return False 
      End If 
      If _PasswordNeverExpires <> .PasswordNeverExpires Then Return False
      If _SecurityQuestion1Code <> .SecurityQuestion1Code Then Return False
      If _SecurityQuestion1Response <> .SecurityQuestion1Response(False) Then Return False
      If _SecurityQuestion2Code <> .SecurityQuestion2Code Then Return False
      If _SecurityQuestion2Response <> .SecurityQuestion2Response(False) Then Return False
      If _SecurityQuestion3Code <> .SecurityQuestion3Code Then Return False
      If _SecurityQuestion3Response <> .SecurityQuestion3Response(False) Then Return False
      If _PIN <> .PIN(False) Then Return False
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
    Dim pClone As New csUser(Me) 
    Return pClone 
  End Function 
 
  ''' <summary>
  ''' This clones the object, returning an exact replica including dependants
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUser
    Dim pClone As New csUser(Me)
    Return pClone
  End Function
  
  Friend Overrides Function LoadDataRow(ByVal vDataRow As DataRow, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'In case we didn't find all the fields ('All fields required except ID and Tag, which are CC fields) 
    Try : vDataRow("ID") = _ID : Catch ex As Exception : Return pFault.LogException(ex, "ID", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("UserName") = _UserName : Catch ex As Exception : Return pFault.LogException(ex, "UserName", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastName") = _LastName : Catch ex As Exception : Return pFault.LogException(ex, "LastName", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("FirstName") = _FirstName : Catch ex As Exception : Return pFault.LogException(ex, "FirstName", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("FullName") = _FullName : Catch ex As Exception : Return pFault.LogException(ex, "FullName", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("NationalIDNo") = _NationalIDNo : Catch ex As Exception : Return pFault.LogException(ex, "NationalIDNo", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Address") = _Address : Catch ex As Exception : Return pFault.LogException(ex, "Address", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("City") = _City : Catch ex As Exception : Return pFault.LogException(ex, "City", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("ProvinceState") = _ProvinceState : Catch ex As Exception : Return pFault.LogException(ex, "ProvinceState", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("PostalCode") = _PostalCode : Catch ex As Exception : Return pFault.LogException(ex, "PostalCode", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Country") = _Country : Catch ex As Exception : Return pFault.LogException(ex, "Country", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("PhoneNumber") = _PhoneNumber : Catch ex As Exception : Return pFault.LogException(ex, "PhoneNumber", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Email") = _Email : Catch ex As Exception : Return pFault.LogException(ex, "Email", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("PasswordHashed") = _PasswordHashed : Catch ex As Exception : Return pFault.LogException(ex, "PasswordHashed", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("DatePasswordChanged") = _DatePasswordChanged : Catch ex As Exception : Return pFault.LogException(ex, "DatePasswordChanged", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Type") = _Type : Catch ex As Exception : Return pFault.LogException(ex, "Type", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("IDinType") = _IDinType : Catch ex As Exception : Return pFault.LogException(ex, "IDinType", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("RequiresComputerIdentification") = _RequiresComputerIdentification : Catch ex As Exception : Return pFault.LogException(ex, "RequiresComputerIdentification", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("EnableSimultaneousLogins") = _EnableSimultaneousLogins : Catch ex As Exception : Return pFault.LogException(ex, "EnableSimultaneousLogins", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("DateActivated") = _DateActivated : Catch ex As Exception : Return pFault.LogException(ex, "DateActivated", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsDisabled") = _IsDisabled : Catch ex As Exception : Return pFault.LogException(ex, "IsDisabled", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("ExpiryDate") = _ExpiryDate : Catch ex As Exception : Return pFault.LogException(ex, "ExpiryDate", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Comments") = _Comments : Catch ex As Exception : Return pFault.LogException(ex, "Comments", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastPasswords") = _LastPasswords : Catch ex As Exception : Return pFault.LogException(ex, "LastPasswords", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Applications") = _Applications : Catch ex As Exception : Return pFault.LogException(ex, "Applications", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("Language") = _Language : Catch ex As Exception : Return pFault.LogException(ex, "Language", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("IsLockedOut") = _IsLockedOut : Catch ex As Exception : Return pFault.LogException(ex, "IsLockedOut", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("RoleID") = _RoleID : Catch ex As Exception : Return pFault.LogException(ex, "RoleID", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("AuthenticationMethod") = _AuthenticationMethod : Catch ex As Exception : Return pFault.LogException(ex, "AuthenticationMethod", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("RequiresFixedIP") = _RequiresFixedIP : Catch ex As Exception : Return pFault.LogException(ex, "RequiresFixedIP", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("MessagingMode") = _MessagingMode : Catch ex As Exception : Return pFault.LogException(ex, "MessagingMode", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("LoggedInIP") = _LoggedInIP : Catch ex As Exception : Return pFault.LogException(ex, "LoggedInIP", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApprovalCodeHashed") = _ApprovalCodeHashed : Catch ex As Exception : Return pFault.LogException(ex, "ApprovalCodeHashed", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApprovalFunctionName") = _ApprovalFunctionName : Catch ex As Exception : Return pFault.LogException(ex, "ApprovalFunctionName", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("ApprovalTime") = _ApprovalTime : Catch ex As Exception : Return pFault.LogException(ex, "ApprovalTime", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("LastSuccessfulLogin") = _LastSuccessfulLogin : Catch ex As Exception : Return pFault.LogException(ex, "LastSuccessfulLogin", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("PasswordNeverExpires") = _PasswordNeverExpires : Catch ex As Exception : Return pFault.LogException(ex, "PasswordNeverExpires", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion1Code") = _SecurityQuestion1Code : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion1Code", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion1Response") = _SecurityQuestion1Response : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion1Response", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion2Code") = _SecurityQuestion2Code : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion2Code", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion2Response") = _SecurityQuestion2Response : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion2Response", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion3Code") = _SecurityQuestion3Code : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion3Code", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("SecurityQuestion3Response") = _SecurityQuestion3Response : Catch ex As Exception : Return pFault.LogException(ex, "SecurityQuestion3Response", "TRGT-User-130316-0852", vRequester) : End Try 
    Try : vDataRow("PIN") = _PIN : Catch ex As Exception : Return pFault.LogException(ex, "PIN", "TRGT-User-130316-0852", vRequester) : End Try 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-130515-1236", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  Public Overrides Function LoadXML(ByVal vXML As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Dim pType As Type = Me.GetType 
    Try 
      Dim pXmlSerializer As New System.Xml.Serialization.XmlSerializer(pType) 
      Dim pStreamReader As New IO.StringReader(vXML) 
      Dim pUser As csUser = CType(pXmlSerializer.Deserialize(pStreamReader), csUser) 
      AssignValues(pUser) 
      pFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      pFault.LogException(ex, vXML, "TRGT-User-130515-1230", vRequester) 
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
          'UserName 
          If _UserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_UserName) 
          'LastName 
          If _LastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastName) 
          'FirstName 
          If _FirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FirstName) 
          'FullName 
          If _FullName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_FullName) 
          'NationalIDNo 
          If _NationalIDNo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_NationalIDNo) 
          'Address 
          If _Address Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Address) 
          'City 
          If _City Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_City) 
          'ProvinceState 
          If _ProvinceState Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ProvinceState) 
          'PostalCode 
          If _PostalCode Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_PostalCode) 
          'Country 
          If _Country Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Country) 
          'PhoneNumber 
          If _PhoneNumber Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_PhoneNumber) 
          'Email 
          If _Email Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Email) 
          'PasswordHashed 
          If _PasswordHashed Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_PasswordHashed) 
          'DatePasswordChanged 
          pBinaryWriter.Write(_DatePasswordChanged.Ticks) 
          'Type 
          pBinaryWriter.Write(_Type.FastToString()) 
          'IDinType 
          pBinaryWriter.Write(_IDinType) 
          'RequiresComputerIdentification 
          pBinaryWriter.Write(_RequiresComputerIdentification) 
          'EnableSimultaneousLogins 
          pBinaryWriter.Write(_EnableSimultaneousLogins) 
          'DateActivated 
          pBinaryWriter.Write(_DateActivated.Ticks) 
          'IsDisabled 
          pBinaryWriter.Write(_IsDisabled) 
          'ExpiryDate 
          pBinaryWriter.Write(_ExpiryDate.Ticks) 
          'Comments 
          If _Comments Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Comments) 
          'LastPasswords 
          If _LastPasswords Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LastPasswords) 
          'Applications 
          If _Applications Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Applications) 
          'Language 
          pBinaryWriter.Write(_Language.FastToString()) 
          'IsLockedOut 
          pBinaryWriter.Write(_IsLockedOut) 
          'RoleID 
          pBinaryWriter.Write(_RoleID) 
          'Role 
          If _Role IsNot Nothing Then 
            pObjectBytes = _Role.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          If _RoleText Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_RoleText) 
          'AuthenticationMethod 
          pBinaryWriter.Write(_AuthenticationMethod.FastToString()) 
          'RequiresFixedIP 
          pBinaryWriter.Write(_RequiresFixedIP) 
          'MessagingMode 
          pBinaryWriter.Write(_MessagingMode.FastToString()) 
          'LoggedInIP 
          If _LoggedInIP Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_LoggedInIP) 
          'ApprovalCodeHashed 
          If _ApprovalCodeHashed Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApprovalCodeHashed) 
          'ApprovalFunctionName 
          If _ApprovalFunctionName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_ApprovalFunctionName) 
          'ApprovalTime 
          pBinaryWriter.Write(_ApprovalTime.DateTime.Ticks) 
          pBinaryWriter.Write(_ApprovalTime.Offset.Ticks) 
          'LastSuccessfulLogin 
          pBinaryWriter.Write(_LastSuccessfulLogin.DateTime.Ticks) 
          pBinaryWriter.Write(_LastSuccessfulLogin.Offset.Ticks) 
          'PasswordNeverExpires 
          pBinaryWriter.Write(_PasswordNeverExpires) 
          'SecurityQuestion1Code 
          If _SecurityQuestion1Code Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion1Code) 
          pBinaryWriter.Write(_SecurityQuestion1Text) 
          'SecurityQuestion1Response 
          If _SecurityQuestion1Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion1Response) 
          'SecurityQuestion2Code 
          If _SecurityQuestion2Code Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion2Code) 
          pBinaryWriter.Write(_SecurityQuestion2Text) 
          'SecurityQuestion2Response 
          If _SecurityQuestion2Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion2Response) 
          'SecurityQuestion3Code 
          If _SecurityQuestion3Code Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion3Code) 
          pBinaryWriter.Write(_SecurityQuestion3Text) 
          'SecurityQuestion3Response 
          If _SecurityQuestion3Response Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_SecurityQuestion3Response) 
          'PIN 
          If _PIN Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_PIN) 
          'Tag 
          If _Tag Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(_Tag) 
          'DateAdded 
          pBinaryWriter.Write(bDateAdded.Ticks) 
          'JobAlertRecipients  
          If _JobAlertRecipients IsNot Nothing Then 
            pObjectBytes = _JobAlertRecipients.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'LoggedAlertsForAffectedUsers  
          If _LoggedAlertsForAffectedUsers IsNot Nothing Then 
            pObjectBytes = _LoggedAlertsForAffectedUsers.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'LoggedRequests  
          If _LoggedRequests IsNot Nothing Then 
            pObjectBytes = _LoggedRequests.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'MFA 
          If _MFA IsNot Nothing Then 
            pObjectBytes = _MFA.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength)  
          If pLength > 0 Then 
            pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          End If 
          'UserLoginKeys  
          If _UserLoginKeys IsNot Nothing Then 
            pObjectBytes = _UserLoginKeys.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'UserPermissions  
          If _UserPermissions IsNot Nothing Then 
            pObjectBytes = _UserPermissions.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
            pLength = pObjectBytes.Length 
          Else 
            pLength = 0 
          End If 
          pBinaryWriter.Write(pLength) 
          If pLength > 0 Then pBinaryWriter.Write(pObjectBytes, 0, pLength) 
          'UserStatuss  
          If _UserStatuss IsNot Nothing Then 
            pObjectBytes = _UserStatuss.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Exit Try 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-User-150307-2338", vRequester) 
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
          'UserName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _UserName = pReader.ReadString 
          'LastName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastName = pReader.ReadString 
          'FirstName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FirstName = pReader.ReadString 
          'FullName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _FullName = pReader.ReadString 
          'NationalIDNo 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _NationalIDNo = pReader.ReadString 
          'Address 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Address = pReader.ReadString 
          'City 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _City = pReader.ReadString 
          'ProvinceState 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ProvinceState = pReader.ReadString 
          'PostalCode 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _PostalCode = pReader.ReadString 
          'Country 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Country = pReader.ReadString 
          'PhoneNumber 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _PhoneNumber = pReader.ReadString 
          'Email 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Email = pReader.ReadString 
          'PasswordHashed 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _PasswordHashed = pReader.ReadString 
          'DatePasswordChanged 
          _DatePasswordChanged = New Date(pReader.ReadInt64) 
          'Type 
          _Type = clsEnums.TranslateEnmUserIdentityType(pReader.ReadString) 
          'IDinType 
          _IDinType = pReader.ReadInt64 
          'RequiresComputerIdentification 
          _RequiresComputerIdentification = pReader.ReadBoolean 
          'EnableSimultaneousLogins 
          _EnableSimultaneousLogins = pReader.ReadBoolean 
          'DateActivated 
          _DateActivated = New Date(pReader.ReadInt64) 
          'IsDisabled 
          _IsDisabled = pReader.ReadBoolean 
          'ExpiryDate 
          _ExpiryDate = New Date(pReader.ReadInt64) 
          'Comments 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Comments = pReader.ReadString 
          'LastPasswords 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LastPasswords = pReader.ReadString 
          'Applications 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Applications = pReader.ReadString 
          'Language 
          _Language = clsEnums.TranslateEnmLanguage(pReader.ReadString) 
          'IsLockedOut 
          _IsLockedOut = pReader.ReadBoolean 
          'RoleID 
          _RoleID = pReader.ReadInt64 
          'Role 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _Role = New csRole(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _RoleText = pReader.ReadString 
          'AuthenticationMethod 
          _AuthenticationMethod = clsEnums.TranslateEnmAuthenticationMethod(pReader.ReadString) 
          'RequiresFixedIP 
          _RequiresFixedIP = pReader.ReadBoolean 
          'MessagingMode 
          _MessagingMode = clsEnums.TranslateEnmMessagingMode(pReader.ReadString) 
          'LoggedInIP 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _LoggedInIP = pReader.ReadString 
          'ApprovalCodeHashed 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApprovalCodeHashed = pReader.ReadString 
          'ApprovalFunctionName 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _ApprovalFunctionName = pReader.ReadString 
          'ApprovalTime 
          _ApprovalTime = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'LastSuccessfulLogin 
          _LastSuccessfulLogin = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64)) 
          'PasswordNeverExpires 
          _PasswordNeverExpires = pReader.ReadBoolean 
          'SecurityQuestion1Code 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion1Code = pReader.ReadString 
          _SecurityQuestion1Text = pReader.ReadString 
          'SecurityQuestion1Response 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion1Response = pReader.ReadString 
          'SecurityQuestion2Code 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion2Code = pReader.ReadString 
          _SecurityQuestion2Text = pReader.ReadString 
          'SecurityQuestion2Response 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion2Response = pReader.ReadString 
          'SecurityQuestion3Code 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion3Code = pReader.ReadString 
          _SecurityQuestion3Text = pReader.ReadString 
          'SecurityQuestion3Response 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _SecurityQuestion3Response = pReader.ReadString 
          'PIN 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _PIN = pReader.ReadString 
          'Tag 
          pHasValue = pReader.ReadBoolean 
          If pHasValue = True Then _Tag = pReader.ReadString 
          'bDateAdded 
          bDateAdded = New DateTime(pReader.ReadInt64) 
          'JobAlertRecipients 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _JobAlertRecipients = New csJobAlertRecipientCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'LoggedAlertsForAffectedUsers 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedAlertsForAffectedUsers = New csLoggedAlertCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'LoggedRequests 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _LoggedRequests = New csLoggedRequestCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'MFA 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _MFA = New csMFA(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'UserLoginKeys 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _UserLoginKeys = New csUserLoginKeyCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'UserPermissions 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _UserPermissions = New csUserPermissionCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          'UserStatuss 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pObjectBytes = pReader.ReadBytes(pLength) 
            _UserStatuss = New csUserStatusCol(pObjectBytes, rFault, vRequester) : If Not rFault.isOK Then Exit Try 
          End If 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      rFault.LogException(ex, "", "TRGT-User-150307-2339", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-190720-1443", vRequester) 
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
 
      Dim pUser As csUser = Newtonsoft.Json.JsonConvert.DeserializeObject(Of csUser)(vJSON, pSettings) 
      AssignValues(pUser) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-190720-1443", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
  
  ''' <summary>
  ''' This assigns the values of the item to the object, with parent or children, if any
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AssignValues(ByVal vUser As csUser)
    With vUser
      _ID = .ID 
      _UserName = .UserName 
      _LastName = .LastName 
      _FirstName = .FirstName 
      _FullName = .FullName 
      _NationalIDNo = .NationalIDNo 
      _Address = .Address 
      _City = .City 
      _ProvinceState = .ProvinceState 
      _PostalCode = .PostalCode 
      _Country = .Country 
      _PhoneNumber = .PhoneNumber 
      _Email = .Email 
      _PasswordHashed = .PasswordHashed 
      _DatePasswordChanged = .DatePasswordChanged 
      _Type = .Type 
      _TypeText = .TypeText
      _IDinType = .IDinType 
      _RequiresComputerIdentification = .RequiresComputerIdentification 
      _EnableSimultaneousLogins = .EnableSimultaneousLogins 
      _DateActivated = .DateActivated 
      _IsDisabled = .IsDisabled 
      _ExpiryDate = .ExpiryDate 
      _Comments = .Comments 
      _LastPasswords = .LastPasswords 
      _Applications = .Applications 
      _Language = .Language 
      _LanguageText = .LanguageText
      _IsLockedOut = .IsLockedOut 
      _RoleID = .RoleID 
      If .Role IsNot Nothing Then 
        _Role = .Role.Clone() 
      End If 
      _RoleText = .RoleText 
      _AuthenticationMethod = .AuthenticationMethod 
      _AuthenticationMethodText = .AuthenticationMethodText
      _RequiresFixedIP = .RequiresFixedIP 
      _MessagingMode = .MessagingMode 
      _MessagingModeText = .MessagingModeText
      _LoggedInIP = .LoggedInIP 
      _ApprovalCodeHashed = .ApprovalCodeHashed 
      _ApprovalFunctionName = .ApprovalFunctionName 
      _ApprovalTime = .ApprovalTime 
      _LastSuccessfulLogin = .LastSuccessfulLogin 
      _PasswordNeverExpires = .PasswordNeverExpires 
      _SecurityQuestion1Code = .SecurityQuestion1Code 
      _SecurityQuestion1Text = .SecurityQuestion1Text 
      _SecurityQuestion1Response = .SecurityQuestion1Response(False) 
      _SecurityQuestion2Code = .SecurityQuestion2Code 
      _SecurityQuestion2Text = .SecurityQuestion2Text 
      _SecurityQuestion2Response = .SecurityQuestion2Response(False) 
      _SecurityQuestion3Code = .SecurityQuestion3Code 
      _SecurityQuestion3Text = .SecurityQuestion3Text 
      _SecurityQuestion3Response = .SecurityQuestion3Response(False) 
      _PIN = .PIN(False) 
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
      'Type 
      pTextToGet = "TypeText (Enum)" 
      _TypeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.UserIdentityType, _Type.FastToString(), vRequester) 
      'Language 
      pTextToGet = "LanguageText (Enum)" 
      _LanguageText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.Language, _Language.FastToString(), vRequester) 
      'AuthenticationMethod 
      pTextToGet = "AuthenticationMethodText (Enum)" 
      _AuthenticationMethodText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.AuthenticationMethod, _AuthenticationMethod.FastToString(), vRequester) 
      'MessagingMode 
      pTextToGet = "MessagingModeText (Enum)" 
      _MessagingModeText = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.MessagingMode, _MessagingMode.FastToString(), vRequester) 
      'SecurityQuestion1 
      pTextToGet = "SecurityQuestion1Text (Lookup)" 
      _SecurityQuestion1Text = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.SecurityQuestion, _SecurityQuestion1Code, vRequester) 
      'SecurityQuestion2 
      pTextToGet = "SecurityQuestion2Text (Lookup)" 
      _SecurityQuestion2Text = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.SecurityQuestion, _SecurityQuestion2Code, vRequester) 
      'SecurityQuestion3 
      pTextToGet = "SecurityQuestion3Text (Lookup)" 
      _SecurityQuestion3Text = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.SecurityQuestion, _SecurityQuestion3Code, vRequester) 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, "Failed getting: " & pTextToGet, "TRGT-User-151124-1900", vRequester) 
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
      Dim pFunction As String = "csUserLoadParents" 
      Dim pParametersToLog = $"ID: {_ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK() 
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function
  
  ''' <summary>
  ''' This loads the dependant 1 to 1 children
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function LoadDependantOneToOneChildren(ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = ""
    Dim pFault As New clsFault 

    If _ID = 0 Then 
      _MFA = New csMFA
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
      Dim pFunction As String = "csUserLoadDependantOneToOneChildren" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1107", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterGet()
    RaiseEvent evtAfterGetWithRequester(vRequester, pFault) 
    Return pFault
  End Function

  Private Sub CreateEmpty()
    
    _ID = 0 
    _UserName = ""
    _LastName = ""
    _FirstName = ""
    _FullName = ""
    _NationalIDNo = ""
    _Address = ""
    _City = ""
    _ProvinceState = ""
    _PostalCode = ""
    _Country = ""
    _PhoneNumber = ""
    _Email = ""
    _PasswordHashed = ""
    _DatePasswordChanged = Nothing
    _Type = clsEnums.enmUserIdentityType.UD
    _TypeText = ""
    _IDinType = 0
    _RequiresComputerIdentification = False
    _EnableSimultaneousLogins = False
    _DateActivated = Nothing
    _IsDisabled = False
    _ExpiryDate = Nothing
    _Comments = ""
    _LastPasswords = ""
    _Applications = ""
    'Default Value set by SQL Server Database (below): en
    _Language = clsEnums.enmLanguage.en
    _LanguageText = ""
    _IsLockedOut = False
    _RoleID = 0
    _Role = Nothing
    _RoleText = "."
    'Default Value set by SQL Server Database (below): UD
    _AuthenticationMethod = clsEnums.enmAuthenticationMethod.UD
    _AuthenticationMethodText = ""
    _RequiresFixedIP = False
    _MessagingMode = clsEnums.enmMessagingMode.UD
    _MessagingModeText = ""
    _LoggedInIP = ""
    _ApprovalCodeHashed = ""
    _ApprovalFunctionName = ""
    _ApprovalTime = Nothing
    _LastSuccessfulLogin = Nothing
    _PasswordNeverExpires = False
    _SecurityQuestion1Code = ""
    _SecurityQuestion1Text = ""
    _SecurityQuestion1Response = ""
    _SecurityQuestion2Code = ""
    _SecurityQuestion2Text = ""
    _SecurityQuestion2Response = ""
    _SecurityQuestion3Code = ""
    _SecurityQuestion3Text = ""
    _SecurityQuestion3Response = ""
    _PIN = ""
    _Tag = ""
    _JobAlertRecipients = Nothing
    _LoggedAlertsForAffectedUsers = Nothing
    _LoggedRequests = Nothing
    _MFA = Nothing
    _UserLoginKeys = Nothing
    _UserPermissions = Nothing
    _UserStatuss = Nothing
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
 
  Public Function CheckPassword(ByVal vPasswordToCheck As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Dim pPasswordToCheckHashed As String 
    pPasswordToCheckHashed = NETEncryption.clsHash.Hash(vPasswordToCheck, NETEncryption.clsHash.HashName.SHA256) 
 
    If pPasswordToCheckHashed <> _PasswordHashed Then 
      pFault.LogFreeTextFault(92, "Bad password received", pFunctionParameters, "TRGT-09090217-1138", vRequester) 
    Else 
      pFault.SetOK() 
    End If 
 
    'Check Expiry 
    If _ExpiryDate <> Nothing AndAlso DateTime.Now > _ExpiryDate Then 
      pFault.LogFreeTextFault(121, "Password expired on " & DateTime.Now.ToString("dd-MMM-yyyy", New System.Globalization.CultureInfo("en-US")), pFunctionParameters, "TRGT-141019-1544", vRequester) 
    Else 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  Public Function ChangePassword(ByVal vNewPassword As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_ID) 
          pBinaryWriter.Write(vNewPassword) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "csUserChangePassword" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value  
      _PasswordHashed = vNewPassword 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1330", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Function ChangePIN(ByVal vNewPIN As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(_ID) 
          pBinaryWriter.Write(vNewPIN) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "csUserChangePIN" 
      Dim pParametersToLog = $"ID: {ID}" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Assign the value   
      _PIN = vNewPIN 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150411-1330", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  
End Class 
  
Public Class csUserCol
  Inherits cTargCCCollection(Of csUser)
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
  Private _SortedDictionaryForFindByID As Dictionary(Of Long, csUser) 
  Private _RecreateDictionaryForFindByID As Boolean 
  Private _SortedDictionaryForFindByUserName As Dictionary(Of String, csUser) 'bigger, but safer, in case ID = 0 
  Private _RecreateDictionaryForFindByUserName As Boolean 
  Private Function CreateKeyForFindByUserName(ByVal vUser As csUser) As String 
    With vUser 
      Return .UserName
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
 
    For Each pRow As csUser In Me 
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
    pCSVTitle.Append(",""UserName""") 
    pCSVTitle.Append(",""LastName""") 
    pCSVTitle.Append(",""FirstName""") 
    pCSVTitle.Append(",""FullName""") 
    pCSVTitle.Append(",""NationalIDNo""") 
    pCSVTitle.Append(",""Address""") 
    pCSVTitle.Append(",""City""") 
    pCSVTitle.Append(",""ProvinceState""") 
    pCSVTitle.Append(",""PostalCode""") 
    pCSVTitle.Append(",""Country""") 
    pCSVTitle.Append(",""PhoneNumber""") 
    pCSVTitle.Append(",""Email""") 
    pCSVTitle.Append(",""PasswordHashed""") 
    pCSVTitle.Append(",""DatePasswordChanged""") 
    pCSVTitle.Append(",""Type" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Type (Text)""") 
    pCSVTitle.Append(",""IDinType""") 
    pCSVTitle.Append(",""RequiresComputerIdentification""") 
    pCSVTitle.Append(",""EnableSimultaneousLogins""") 
    pCSVTitle.Append(",""DateActivated""") 
    pCSVTitle.Append(",""IsDisabled""") 
    pCSVTitle.Append(",""ExpiryDate""") 
    pCSVTitle.Append(",""Comments""") 
    pCSVTitle.Append(",""LastPasswords""") 
    pCSVTitle.Append(",""Applications""") 
    pCSVTitle.Append(",""Language" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Language (Text)""") 
    pCSVTitle.Append(",""IsLockedOut""") 
    pCSVTitle.Append(",""RoleID" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""Role (Text)""") 
    pCSVTitle.Append(",""AuthenticationMethod" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""AuthenticationMethod (Text)""") 
    pCSVTitle.Append(",""RequiresFixedIP""") 
    pCSVTitle.Append(",""MessagingMode" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""MessagingMode (Text)""") 
    pCSVTitle.Append(",""LoggedInIP""") 
    pCSVTitle.Append(",""ApprovalCodeHashed""") 
    pCSVTitle.Append(",""ApprovalFunctionName""") 
    pCSVTitle.Append(",""ApprovalTime""") 
    pCSVTitle.Append(",""LastSuccessfulLogin""") 
    pCSVTitle.Append(",""PasswordNeverExpires""") 
    pCSVTitle.Append(",""SecurityQuestion1Code" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""SecurityQuestion1 (Text)""") 
    pCSVTitle.Append(",""SecurityQuestion1Response""") 
    pCSVTitle.Append(",""SecurityQuestion2Code" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""SecurityQuestion2 (Text)""") 
    pCSVTitle.Append(",""SecurityQuestion2Response""") 
    pCSVTitle.Append(",""SecurityQuestion3Code" & pDbCode & """") 
    If vWithTexts Then pCSVTitle.Append(",""SecurityQuestion3 (Text)""") 
    pCSVTitle.Append(",""SecurityQuestion3Response""") 
    pCSVTitle.Append(",""PIN""") 
    If Not vWithTexts Then 
      pCSVTitle.Append(",""Tag""") 
    End If 
    'pCSVTitle.Append(",""DateAdded""") 
     
    pCSV.AppendLine(pCSVTitle.ToString()) 
 
    For Each pRow As csUser In Me 
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
 
  Public Overloads Sub Add(ByVal vUser As csUser) 
    SyncLock _CollectionLock 
      MyBase.Add(vUser) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Insert(ByVal vIndex As Integer, ByVal vUser As csUser) 
    SyncLock _CollectionLock 
      MyBase.Insert(vIndex, vUser) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub AddRange(ByVal vUserCol As csUserCol) 
    SyncLock _CollectionLock 
      MyBase.AddRange(vUserCol) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub RemoveAt(ByVal vIndex As Integer) 
    SyncLock _CollectionLock 
      MyBase.RemoveAt(vIndex) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserName = True 
    End SyncLock 
  End Sub 
  Public Overloads Sub Remove(ByVal vUser As csUser) 
    SyncLock _CollectionLock 
      MyBase.Remove(vUser) 
      _RecreateDictionaryForFindByID = True 
      _RecreateDictionaryForFindByUserName = True 
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
      Dim pTempDictionary As New Dictionary(Of Long, csUser) 
      
      For Each lUser In Me 
        If lUser.IsEmpty OrElse pTempDictionary.ContainsKey(lUser.ID) Then 
          'Not Unique or no ID 
          Continue For 
        End If 
        Try 
          pTempDictionary.Add(lUser.ID, lUser) 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUser.ToString, "TRGT-User-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByID:" & ex.Message & ", User:" & lUser.ToString() & ", TRGT-User-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByID = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByID = False
    End SyncLock 
  End Sub 
 
  Private Sub LoadUserNames() 
    ' 1. First Check (Optimization): prevents locking if we don't need to 
    If Not _RecreateDictionaryForFindByUserName Then Return 
    SyncLock _CollectionLock 
      ' 2. Second Check (Safety): ensures no one else fixed it while we waited for the lock 
      If Not _RecreateDictionaryForFindByUserName Then Return 
      
      If _FilledFromSumOnTheFly Then _RecreateDictionaryForFindByUserName = False : Return 'Not logical 
      
      ' 3. Create a TEMPORARY dictionary first. 
      ' This is the 'Atomic' part. We build it safely in a local variable. 
      ' Do not touch the shared variable '_SortedDictionaryForFindByUserName' yet!
      Dim pTempDictionary As New Dictionary(Of String, csUser)(StringComparer.OrdinalIgnoreCase) 
      
      For Each lUser In Me 
        Try 
          Dim pUserName As String = CreateKeyForFindByUserName(lUser) 
          If String.IsNullOrEmpty(pUserName.Replace("|", "")) Then Continue For 
          If Not (pTempDictionary.ContainsKey(pUserName)) Then 
            pTempDictionary.Add(pUserName, lUser) 
          Else 'Keep only the 1st one    
            Continue For 
          End If 
        Catch ex As Exception 
          Dim pFault As New clsFault 
          pFault.LogException(ex, lUser.ToString, "TRGT-User-260111-154655", Nothing) 'Log it 
          Throw New Exception("Failed pTempDictionary for _SortedDictionaryForFindByUserName:" & ex.Message & ", User:" & lUser.ToString() & ", TRGT-User-260111-154657") 'Send it up the line 
        End Try 
      Next 
      
      ' 4. ATOMIC SWAP: The dictionary is 100% ready.
      ' We swap the reference instantly. Readers will never see a half-filled list.
      _SortedDictionaryForFindByUserName = pTempDictionary
      
      ' 5. Mark as done INSIDE the lock.
      _RecreateDictionaryForFindByUserName = False
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
 
    For Each lUser As csUser In Me 
      lUser.TruncateStrings(pTruncateLength) 
    Next 
 
  End Sub 
 
  Public Enum enmFillByParameterCombination 
    [UD] 
    [None_GetAll] 
    [City] 
    [LastNameAndFirstName] 
    [LastSuccessfulLogin] 
    [RoleID] 
    [TypeAndIDinType] 
  End Enum 
  ''' <summary> 
  ''' This satisfies an interface requirement. All TargCCCollections have this function. It gets the Users by the chosen parameters. This function may be a bit slower than accessing the User's FillBy... directly 
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
        Case enmFillByParameterCombination.City 
          pFault = FillByCity(CStr(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.LastNameAndFirstName 
          pFault = FillByLastNameAndFirstName(CStr(vParameters(0)), CStr(vParameters(1)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.LastSuccessfulLogin 
          pFault = FillByLastSuccessfulLogin(CType(vParameters(0), DateTimeOffset), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.RoleID 
          pFault = FillByRoleID(ccHelper.ToLong(vParameters(0)), vRequester, vHowMany, vDir) 
        Case enmFillByParameterCombination.TypeAndIDinType 
          pFault = FillByTypeAndIDinType(clsEnums.TranslateEnmUserIdentityType(CStr(vParameters(0))), ccHelper.ToLong(vParameters(1)), vRequester, vHowMany, vDir) 
        Case Else 
          pFault = New clsFault 
          pFault.LogFreeTextFault(56, "", pFunctionParameters, "TRGT-User-151223_1715", vRequester) 
      End Select 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.LogException(57, ex, pFunctionParameters, "TRGT-User-151223_1716", vRequester) 
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
 
      Dim pFunction As String = "csUserColFill" 
      Dim pParametersToLog = $"Parameters: None" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User 
      LoadByteArray(pResponse, pFault, vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150308-1015", vRequester) 
    End Try 
 
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific City, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByCity(ByVal vCity As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("City={0}", vCity)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCity 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) 
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
 
      Dim pFunction As String = "csUserColFillByCity" 
      Dim pParametersToLog = $"City: {vCity};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastName and FirstName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLastNameAndFirstName(ByVal vLastName As String, ByVal vFirstName As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastName={0}, FirstName={1}", vLastName, vFirstName)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) 
          ' 
          'vFirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) 
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
 
      Dim pFunction As String = "csUserColFillByLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastName};{vFirstName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastSuccessfulLogin, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByLastSuccessfulLogin(ByVal vLastSuccessfulLogin As DateTimeOffset, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastSuccessfulLogin={0}", vLastSuccessfulLogin)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastSuccessfulLogin 
          pBinaryWriter.Write(vLastSuccessfulLogin.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLogin.Offset.Ticks) 
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
 
      Dim pFunction As String = "csUserColFillByLastSuccessfulLogin" 
      Dim pParametersToLog = $"LastSuccessfulLogin: {vLastSuccessfulLogin};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific RoleID, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByRoleID(ByVal vRoleID As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("RoleID={0}", vRoleID)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vRoleID 
          pBinaryWriter.Write(vRoleID) 
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
 
      Dim pFunction As String = "csUserColFillByRoleID" 
      Dim pParametersToLog = $"RoleID: {vRoleID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Type and IDinType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinType As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, IDinType={1}", vType, vIDinType)
    Dim pFault As New clsFault 
    
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vType 
          pBinaryWriter.Write(vType.ToString()) 
          ' 
          'vIDinType 
          pBinaryWriter.Write(vIDinType) 
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
 
      Dim pFunction As String = "csUserColFillByTypeAndIDinType" 
      Dim pParametersToLog = $"TypeAndIDinType: {vType};{vIDinType};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific City, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedCity(ByVal vCityFrom As String, ByVal vCityTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("CityFrom={0}, CityTo={1}", vCityFrom, vCityTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCityFrom 
          If vCityFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCityFrom) 
          ' 
          'vCityTo 
          If vCityTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCityTo) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedCity" 
      Dim pParametersToLog = $"City: {vCityFrom};{vCityTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastName and FirstName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedLastNameAndFirstName(ByVal vLastNameFrom As String, ByVal vLastNameTo As String, ByVal vFirstNameFrom As String, ByVal vFirstNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastNameFrom={0}, LastNameTo={1}, FirstNameFrom={2}, FirstNameTo={3}", vLastNameFrom, vLastNameTo, vFirstNameFrom, vFirstNameTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastNameFrom 
          If vLastNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastNameFrom) 
          ' 
          'vLastNameTo 
          If vLastNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastNameTo) 
          ' 
          'vFirstNameFrom 
          If vFirstNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstNameFrom) 
          ' 
          'vFirstNameTo 
          If vFirstNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstNameTo) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastNameFrom};{vLastNameTo};{vFirstNameFrom};{vFirstNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastSuccessfulLogin, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedLastSuccessfulLogin(ByVal vLastSuccessfulLoginStart As DateTimeOffset, ByVal vLastSuccessfulLoginEnd As DateTimeOffset, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastSuccessfulLoginStart={0}, LastSuccessfulLoginEnd={1}", vLastSuccessfulLoginStart, vLastSuccessfulLoginEnd)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastSuccessfulLoginStart 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.Offset.Ticks) 
          ' 
          'vLastSuccessfulLoginEnd 
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.Offset.Ticks) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedLastSuccessfulLogin" 
      Dim pParametersToLog = $"LastSuccessfulLogin: {vLastSuccessfulLoginStart};{vLastSuccessfulLoginEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}", vUserNameFrom, vUserNameTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserNameFrom 
          If vUserNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameFrom) 
          ' 
          'vUserNameTo 
          If vUserNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameTo) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedUserName" 
      Dim pParametersToLog = $"UserName: {vUserNameFrom};{vUserNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific Type and IDinType, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByBoundedTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinTypeFrom As Long, ByVal vIDinTypeTo As Long, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, IDinTypeFrom={1}, IDinTypeTo={2}", vType, vIDinTypeFrom, vIDinTypeTo)
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vType 
          pBinaryWriter.Write(vType.ToString()) 
          ' 
          'vIDinTypeFrom 
          pBinaryWriter.Write(vIDinTypeFrom) 
          ' 
          'vIDinTypeTo 
          pBinaryWriter.Write(vIDinTypeTo) 
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
 
      Dim pFunction As String = "csUserColFillByBoundedTypeAndIDinType" 
      Dim pParametersToLog = $"TypeAndIDinType: {vType};{vIDinTypeFrom};{vIDinTypeTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific City, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardCity(ByVal vCity As String, ByVal vCityWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("City={0}, CityWildcardType={1}", vCity, vCityWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCity 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) 
          ' 
          pBinaryWriter.Write(vCityWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColFillByWildCardCity" 
      Dim pParametersToLog = $"City: {vCity};{vCityWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific LastName and FirstName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardLastNameAndFirstName(ByVal vLastName As String, ByVal vLastNameWildcardType As clsEnums.enmWildCardType, ByVal vFirstName As String, ByVal vFirstNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("LastName={0}, LastNameWildcardType={1}, FirstName={2}, FirstNameWildcardType={3}", vLastName, vLastNameWildcardType.FastToString(), vFirstName, vFirstNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) 
          ' 
          pBinaryWriter.Write(vLastNameWildcardType.FastToString())
          'vFirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) 
          ' 
          pBinaryWriter.Write(vFirstNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColFillByWildCardLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastName};{vLastNameWildcardType.FastToString()};{vFirstName};{vFirstNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Gets a collection of all the items for a specific UserName, or a sub-collection defined by HowMany and Direction. To append to an existing collection, set vAppend to true (default is false)
  ''' </summary>
  ''' <param name="vHowMany">How Many (sorted by ID) - 0 for all</param>
  ''' <param name="vDir">ASCendined or DESCending</param>
  ''' <param name="vAppend">True of False</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FillByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC, Optional vAppend As Boolean = False) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}", vUserName, vUserNameWildcardType.FastToString())
    Dim pFault As New clsFault 
 
    If vAppend = False Then Me.Clear() 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(vUserNameWildcardType.FastToString())
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(False) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColFillByWildCardUserName" 
      Dim pParametersToLog = $"UserName: {vUserName};{vUserNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
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
 
      Dim pFunction As String = "csUserColFillByListOfID" 
      Dim pParametersToLog = $"" 
      For Each l In vIDs 
        pParametersToLog &= $"{l};" 
      Next 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User   
      If vAppend = True Then 
        Dim pUsers As New csUserCol 
        pUsers.LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
        Me.AddRange(pUsers) 
      Else 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-231207-1750", vRequester) 
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
    [UserName]
    UserNameWildcardType
    [LastName]
    LastNameWildcardType
    [FirstName]
    FirstNameWildcardType
    [City]
    CityWildcardType
    [Type]
    IDinTypeFrom
    IDinTypeTo
    [RoleID]
    LastSuccessfulLoginStart
    LastSuccessfulLoginEnd
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
    Dim pUserName As String = Nothing
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLastName As String = Nothing
    Dim pLastNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pFirstName As String = Nothing
    Dim pFirstNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCity As String = Nothing
    Dim pCityWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pType As clsEnums.enmUserIdentityType = clsEnums.enmUserIdentityType.UD
    Dim pIDinTypeFrom As Nullable(Of Long) = Nothing
    Dim pIDinTypeTo As Nullable(Of Long) = Nothing
    Dim pRoleID As Nullable(Of Long) = Nothing
    Dim pLastSuccessfulLoginStart As Nullable(Of DateTimeOffset) = Nothing
    Dim pLastSuccessfulLoginEnd As Nullable(Of DateTimeOffset) = Nothing
 
    Dim pHowMany As Integer = 0 
    Dim pDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC 
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserName) Then pObj = vParameters(enmFillOnTheFlyParameters.UserName) : If pObj IsNot Nothing Then pUserName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.UserNameWildcardType) : If pObj IsNot Nothing Then pUserNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastName) Then pObj = vParameters(enmFillOnTheFlyParameters.LastName) : If pObj IsNot Nothing Then pLastName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.LastNameWildcardType) : If pObj IsNot Nothing Then pLastNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FirstName) Then pObj = vParameters(enmFillOnTheFlyParameters.FirstName) : If pObj IsNot Nothing Then pFirstName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FirstNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FirstNameWildcardType) : If pObj IsNot Nothing Then pFirstNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.City) Then pObj = vParameters(enmFillOnTheFlyParameters.City) : If pObj IsNot Nothing Then pCity = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CityWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CityWildcardType) : If pObj IsNot Nothing Then pCityWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Type) Then pObj = vParameters(enmFillOnTheFlyParameters.Type) : If pObj IsNot Nothing Then pType = CType(pObj, clsEnums.enmUserIdentityType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDinTypeFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDinTypeFrom) : If pObj IsNot Nothing Then pIDinTypeFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDinTypeTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDinTypeTo) : If pObj IsNot Nothing Then pIDinTypeTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RoleID) Then pObj = vParameters(enmFillOnTheFlyParameters.RoleID) : If pObj IsNot Nothing Then pRoleID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastSuccessfulLoginStart) Then pObj = vParameters(enmFillOnTheFlyParameters.LastSuccessfulLoginStart) : If pObj IsNot Nothing Then pLastSuccessfulLoginStart = CType(pObj, DateTimeOffset) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) : If pObj IsNot Nothing Then pLastSuccessfulLoginEnd = CType(pObj, DateTimeOffset) 
     
    If vParameters.ContainsKey(enmListDefinition.HowMany) Then pObj = vParameters(enmListDefinition.HowMany) : If pObj IsNot Nothing Then pHowMany = ccHelper.ToInteger(pObj) 
    If vParameters.ContainsKey(enmListDefinition.Dir) Then pObj = vParameters(enmListDefinition.Dir) : If pObj IsNot Nothing Then pDir = CType(pObj, clsEnums.enmFillDirection) 
 
    pFault = FillOnTheFly(
          pIDFrom, pIDTo _
        , pUserName, pUserNameWildcardType _
        , pLastName, pLastNameWildcardType _
        , pFirstName, pFirstNameWildcardType _
        , pCity, pCityWildcardType _
        , pType _
        , pIDinTypeFrom, pIDinTypeTo _
        , pRoleID _
        , pLastSuccessfulLoginStart, pLastSuccessfulLoginEnd _
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
        , ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLastName As String, ByVal vLastNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vFirstName As String, ByVal vFirstNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vCity As String, ByVal vCityWildcardType As clsEnums.enmWildCardType _
        , ByVal vType As clsEnums.enmUserIdentityType _
        , ByVal vIDinTypeFrom As Nullable(Of Long), ByVal vIDinTypeTo As Nullable(Of Long) _
        , ByVal vRoleID As Nullable(Of Long) _
        , ByVal vLastSuccessfulLoginStart As Nullable(Of DateTimeOffset), ByVal vLastSuccessfulLoginEnd As Nullable(Of DateTimeOffset) _
        , ByVal vRequester As clsRequester, Optional ByVal vHowMany As Integer = 0, Optional ByVal vDir As clsEnums.enmFillDirection = clsEnums.enmFillDirection.ASC) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserName={2}, UserNameWildcardType={3}, LastName={4}, LastNameWildcardType={5}, FirstName={6}, FirstNameWildcardType={7}, City={8}, CityWildcardType={9}, Type={10}, IDinTypeFrom={11}, IDinTypeTo={12}, RoleID={13}, LastSuccessfulLoginStart={14}, LastSuccessfulLoginEnd={15}", vIDFrom, vIDTo, vUserName, vUserNameWildcardType.FastToString(), vLastName, vLastNameWildcardType.FastToString(), vFirstName, vFirstNameWildcardType.FastToString(), vCity, vCityWildcardType.FastToString(), vType, vIDinTypeFrom, vIDinTypeTo, vRoleID, vLastSuccessfulLoginStart, vLastSuccessfulLoginEnd)
    
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
          'UserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) : pBinaryWriter.Write(vUserNameWildcardType.FastToString()) : pParametersToLog &= $"UserName={vUserName};" : pParametersToLog &= $"UserNameWildcardType={vUserNameWildcardType};"  
          'LastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) : pBinaryWriter.Write(vLastNameWildcardType.FastToString()) : pParametersToLog &= $"LastName={vLastName};" : pParametersToLog &= $"LastNameWildcardType={vLastNameWildcardType};"  
          'FirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) : pBinaryWriter.Write(vFirstNameWildcardType.FastToString()) : pParametersToLog &= $"FirstName={vFirstName};" : pParametersToLog &= $"FirstNameWildcardType={vFirstNameWildcardType};"  
          'City 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) : pBinaryWriter.Write(vCityWildcardType.FastToString()) : pParametersToLog &= $"City={vCity};" : pParametersToLog &= $"CityWildcardType={vCityWildcardType};"  
          'Type 
          pBinaryWriter.Write(vType.ToString()) : pParametersToLog &= $"Type={vType};"  
          'IDinType 
          pBinaryWriter.Write(vIDinTypeFrom.HasValue) 
          If vIDinTypeFrom.HasValue Then pBinaryWriter.Write(vIDinTypeFrom.Value) : pParametersToLog &= $"IDinTypeFrom={vIDinTypeFrom};"  
          pBinaryWriter.Write(vIDinTypeTo.HasValue) 
          If vIDinTypeTo.HasValue Then pBinaryWriter.Write(vIDinTypeTo.Value) : pParametersToLog &= $"IDinTypeTo={vIDinTypeTo};"  
          'RoleID 
          pBinaryWriter.Write(vRoleID.HasValue) 
          If vRoleID.HasValue = True Then pBinaryWriter.Write(vRoleID.Value) : pParametersToLog &= $"RoleID={vRoleID};"  
          'LastSuccessfulLogin 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.HasValue) 
          If vLastSuccessfulLoginStart.HasValue Then pBinaryWriter.Write(vLastSuccessfulLoginStart.Value.DateTime.Ticks) : pBinaryWriter.Write(vLastSuccessfulLoginStart.Value.Offset.Ticks) : pParametersToLog &= $"LastSuccessfulLoginStart={vLastSuccessfulLoginStart.Value};"  
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.HasValue) 
          If vLastSuccessfulLoginEnd.HasValue Then pBinaryWriter.Write(vLastSuccessfulLoginEnd.Value.DateTime.Ticks) : pBinaryWriter.Write(vLastSuccessfulLoginEnd.Value.Offset.Ticks) : pParametersToLog &= $"LastSuccessfulLoginEnd={vLastSuccessfulLoginEnd.Value};"  
          pBinaryWriter.Write(vHowMany) 
          pBinaryWriter.Write(vDir.FastToString()) 
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColFillOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    pFault.SetOK()
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Enum enmFillSumOnTheFlyParameters 
    UD 
    GroupByLastName
    GroupByFirstName
    GroupByCity
    GroupByType
    GroupByIDinType
    GroupByRoleID
    GroupByLastSuccessfulLogin
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
    Dim pUserName As String = Nothing
    Dim pUserNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pLastName As String = Nothing
    Dim pLastNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pFirstName As String = Nothing
    Dim pFirstNameWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pCity As String = Nothing
    Dim pCityWildcardType As clsEnums.enmWildCardType = clsEnums.enmWildCardType.UD
    Dim pType As clsEnums.enmUserIdentityType = clsEnums.enmUserIdentityType.UD
    Dim pIDinTypeFrom As Nullable(Of Long) = Nothing
    Dim pIDinTypeTo As Nullable(Of Long) = Nothing
    Dim pRoleID As Nullable(Of Long) = Nothing
    Dim pLastSuccessfulLoginStart As Nullable(Of DateTimeOffset) = Nothing
    Dim pLastSuccessfulLoginEnd As Nullable(Of DateTimeOffset) = Nothing
    Dim pGroupByLastName As Boolean = False
    Dim pGroupByFirstName As Boolean = False
    Dim pGroupByCity As Boolean = False
    Dim pGroupByType As Boolean = False
    Dim pGroupByIDinType As Boolean = False
    Dim pGroupByRoleID As Boolean = False
    Dim pGroupByLastSuccessfulLogin As Boolean = False
 
    Dim pObj As Object 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDFrom) : If pObj IsNot Nothing Then pIDFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDTo) : If pObj IsNot Nothing Then pIDTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserName) Then pObj = vParameters(enmFillOnTheFlyParameters.UserName) : If pObj IsNot Nothing Then pUserName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.UserNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.UserNameWildcardType) : If pObj IsNot Nothing Then pUserNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastName) Then pObj = vParameters(enmFillOnTheFlyParameters.LastName) : If pObj IsNot Nothing Then pLastName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.LastNameWildcardType) : If pObj IsNot Nothing Then pLastNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FirstName) Then pObj = vParameters(enmFillOnTheFlyParameters.FirstName) : If pObj IsNot Nothing Then pFirstName = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.FirstNameWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.FirstNameWildcardType) : If pObj IsNot Nothing Then pFirstNameWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.City) Then pObj = vParameters(enmFillOnTheFlyParameters.City) : If pObj IsNot Nothing Then pCity = CStr(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.CityWildcardType) Then pObj = vParameters(enmFillOnTheFlyParameters.CityWildcardType) : If pObj IsNot Nothing Then pCityWildcardType = CType(pObj, clsEnums.enmWildCardType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.Type) Then pObj = vParameters(enmFillOnTheFlyParameters.Type) : If pObj IsNot Nothing Then pType = CType(pObj, clsEnums.enmUserIdentityType) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDinTypeFrom) Then pObj = vParameters(enmFillOnTheFlyParameters.IDinTypeFrom) : If pObj IsNot Nothing Then pIDinTypeFrom = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.IDinTypeTo) Then pObj = vParameters(enmFillOnTheFlyParameters.IDinTypeTo) : If pObj IsNot Nothing Then pIDinTypeTo = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.RoleID) Then pObj = vParameters(enmFillOnTheFlyParameters.RoleID) : If pObj IsNot Nothing Then pRoleID = ccHelper.ToLong(pObj) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastSuccessfulLoginStart) Then pObj = vParameters(enmFillOnTheFlyParameters.LastSuccessfulLoginStart) : If pObj IsNot Nothing Then pLastSuccessfulLoginStart = CType(pObj, DateTimeOffset) 
    If vParameters.ContainsKey(enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) Then pObj = vParameters(enmFillOnTheFlyParameters.LastSuccessfulLoginEnd) : If pObj IsNot Nothing Then pLastSuccessfulLoginEnd = CType(pObj, DateTimeOffset) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLastName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLastName) : If pObj IsNot Nothing Then pGroupByLastName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByFirstName) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByFirstName) : If pObj IsNot Nothing Then pGroupByFirstName = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByCity) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByCity) : If pObj IsNot Nothing Then pGroupByCity = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByType) : If pObj IsNot Nothing Then pGroupByType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByIDinType) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByIDinType) : If pObj IsNot Nothing Then pGroupByIDinType = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByRoleID) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByRoleID) : If pObj IsNot Nothing Then pGroupByRoleID = CBool(pObj) 
    If vParameters.ContainsKey(enmFillSumOnTheFlyParameters.GroupByLastSuccessfulLogin) Then pObj = vParameters(enmFillSumOnTheFlyParameters.GroupByLastSuccessfulLogin) : If pObj IsNot Nothing Then pGroupByLastSuccessfulLogin = CBool(pObj) 
     
    pFault = FillSumOnTheFly(
          pIDFrom, pIDTo _
        , pUserName, pUserNameWildcardType _
        , pLastName, pLastNameWildcardType _
        , pFirstName, pFirstNameWildcardType _
        , pCity, pCityWildcardType _
        , pType _
        , pIDinTypeFrom, pIDinTypeTo _
        , pRoleID _
        , pLastSuccessfulLoginStart, pLastSuccessfulLoginEnd _
        , pGroupByLastName _
        , pGroupByFirstName _
        , pGroupByCity _
        , pGroupByType _
        , pGroupByIDinType _
        , pGroupByRoleID _
        , pGroupByLastSuccessfulLogin _
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
        , ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vLastName As String, ByVal vLastNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vFirstName As String, ByVal vFirstNameWildcardType As clsEnums.enmWildCardType _
        , ByVal vCity As String, ByVal vCityWildcardType As clsEnums.enmWildCardType _
        , ByVal vType As clsEnums.enmUserIdentityType _
        , ByVal vIDinTypeFrom As Nullable(Of Long), ByVal vIDinTypeTo As Nullable(Of Long) _
        , ByVal vRoleID As Nullable(Of Long) _
        , ByVal vLastSuccessfulLoginStart As Nullable(Of DateTimeOffset), ByVal vLastSuccessfulLoginEnd As Nullable(Of DateTimeOffset) _
        , ByVal vGroupByLastName As Boolean _
        , ByVal vGroupByFirstName As Boolean _
        , ByVal vGroupByCity As Boolean _
        , ByVal vGroupByType As Boolean _
        , ByVal vGroupByIDinType As Boolean _
        , ByVal vGroupByRoleID As Boolean _
        , ByVal vGroupByLastSuccessfulLogin As Boolean _
        , ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("IDFrom={0}, IDTo={1}, UserName={2}, UserNameWildcardType={3}, LastName={4}, LastNameWildcardType={5}, FirstName={6}, FirstNameWildcardType={7}, City={8}, CityWildcardType={9}, Type={10}, IDinTypeFrom={11}, IDinTypeTo={12}, RoleID={13}, LastSuccessfulLoginStart={14}, LastSuccessfulLoginEnd={15}, GroupByLastName={16}, GroupByFirstName={17}, GroupByCity={18}, GroupByType={19}, GroupByIDinType={20}, GroupByRoleID={21}, GroupByLastSuccessfulLogin={22}", vIDFrom, vIDTo, vUserName, vUserNameWildcardType.FastToString(), vLastName, vLastNameWildcardType.FastToString(), vFirstName, vFirstNameWildcardType.FastToString(), vCity, vCityWildcardType.FastToString(), vType, vIDinTypeFrom, vIDinTypeTo, vRoleID, vLastSuccessfulLoginStart, vLastSuccessfulLoginEnd, vGroupByLastName, vGroupByFirstName, vGroupByCity, vGroupByType, vGroupByIDinType, vGroupByRoleID, vGroupByLastSuccessfulLogin)
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
          'UserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) : pBinaryWriter.Write(vUserNameWildcardType.FastToString()) 
          'LastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) : pBinaryWriter.Write(vLastNameWildcardType.FastToString()) 
          'FirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) : pBinaryWriter.Write(vFirstNameWildcardType.FastToString()) 
          'City 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) : pBinaryWriter.Write(vCityWildcardType.FastToString()) 
          'Type 
          pBinaryWriter.Write(vType.ToString()) : pParametersToLog &= $"Type={vType};"  
          'IDinType 
          pBinaryWriter.Write(vIDinTypeFrom.HasValue) 
          If vIDinTypeFrom.HasValue Then pBinaryWriter.Write(vIDinTypeFrom.Value) : pParametersToLog &= $"IDinTypeFrom={vIDinTypeFrom};"  
          pBinaryWriter.Write(vIDinTypeTo.HasValue) 
          If vIDinTypeTo.HasValue Then pBinaryWriter.Write(vIDinTypeTo.Value) : pParametersToLog &= $"IDinTypeTo={vIDinTypeTo};"  
          'RoleID 
          pBinaryWriter.Write(vRoleID.HasValue) 
          If vRoleID.HasValue = True Then pBinaryWriter.Write(vRoleID.Value) : pParametersToLog &= $"RoleID={vRoleID};"  
          'LastSuccessfulLogin 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.HasValue) 
          If vLastSuccessfulLoginStart.HasValue Then pBinaryWriter.Write(vLastSuccessfulLoginStart.Value.DateTime.Ticks) : pBinaryWriter.Write(vLastSuccessfulLoginStart.Value.Offset.Ticks) : pParametersToLog &= $"LastSuccessfulLoginStart={vLastSuccessfulLoginStart};"  
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.HasValue) 
          If vLastSuccessfulLoginEnd.HasValue Then pBinaryWriter.Write(vLastSuccessfulLoginEnd.Value.DateTime.Ticks) : pBinaryWriter.Write(vLastSuccessfulLoginEnd.Value.Offset.Ticks) : pParametersToLog &= $"LastSuccessfulLoginEnd={vLastSuccessfulLoginEnd};"  
          pBinaryWriter.Write(vGroupByLastName) : pParametersToLog &= $"GroupByLastName={vGroupByLastName};"  
          pBinaryWriter.Write(vGroupByFirstName) : pParametersToLog &= $"GroupByFirstName={vGroupByFirstName};"  
          pBinaryWriter.Write(vGroupByCity) : pParametersToLog &= $"GroupByCity={vGroupByCity};"  
          pBinaryWriter.Write(vGroupByType) : pParametersToLog &= $"GroupByType={vGroupByType};"  
          pBinaryWriter.Write(vGroupByIDinType) : pParametersToLog &= $"GroupByIDinType={vGroupByIDinType};"  
          pBinaryWriter.Write(vGroupByRoleID) : pParametersToLog &= $"GroupByRoleID={vGroupByRoleID};"  
          pBinaryWriter.Write(vGroupByLastSuccessfulLogin) : pParametersToLog &= $"GroupByLastSuccessfulLogin={vGroupByLastSuccessfulLogin};"  
          pBinaryWriter.Write(_WithParents.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColFillSumOnTheFly" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the User  
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150407-2142", vRequester) 
    End Try 
    
    _FilledFromSumOnTheFly = True 
    
    pFault.SetOK() 
    RaiseEvent evtAfterFill()
    RaiseEvent evtAfterFillWithRequester(vRequester, pFault) 
    Return pFault
  End Function
        
  Public Overloads Sub FillFromArray(ByVal vUserArray As csUser())
    Me.Clear()
    
    For Each pUser As csUser In vUserArray
      Me.Add(pUser)
      _Clean.Add(pUser.ID) 
    Next
  End Sub
  
  Public Overrides Function FillFromDataTable(ByVal vDataTable As DataTable, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    Me.Clear() 
    
    Try 
      For Each pRow As DataRow In vDataTable.Rows 
        Dim pUser As New csUser(pRow, vRequester, _WithParents) 
        Me.Add(pUser) 
        _Clean.Add(pUser.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      Return pFault.LogException(ex, "", "TRGT-UserCol-130315-2118", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-130515-1300", vRequester) 
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
      Dim pUsers As csUserCol = CType(pXmlSerializer.Deserialize(pStreamReader), csUserCol) 
      For Each pUser As csUser In pUsers 
        Me.Add(pUser) 
        _Clean.Add(pUser.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, vXML, "TRGT-User-130515-1329", vRequester) 
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
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-190720-1443", vRequester) 
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
 
      Dim pUsers As List(Of csUser) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of csUser))(vJSON, pSettings) 
      For Each pUser As csUser In pUsers 
        Me.Add(pUser) 
        _Clean.Add(pUser.ID) 
      Next 
      pFault.SetOK() 
    Catch ex As Exception 
      pFault.LogException(ex, pFunctionParameters, "TRGT-User-190720-2059", vRequester) 
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
          For Each lUser As csUser In Me 
            Dim pByte As Byte() = lUser.CreateByteArray(rFault, vRequester) : If Not rFault.isOK Then Return Nothing 
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
      rFault.LogException(ex, pFunctionParameters, "TRGT-User-150307-2340", vRequester) 
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
            Dim pUser As csUser = New csUser(pReader.ReadBytes(pLength), rFault, vRequester) 
            Me.Add(pUser) : If Not rFault.isOK Then Exit Sub 
            _Clean.Add(pUser.ID) 
          Next 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      rFault.SetOK() 
    Catch ex As Exception 
      rFault.LogException(ex, "", "TRGT-User-150307-2341", vRequester) 
    End Try 
 
  End Sub 
 
  Public Overrides Function LoadLookupAndEnumText(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    If Me.Count = 0 Then Return pFault.SetOK() 
 
    For Each pUser As csUser In Me 
      With pUser 
        pFault = pUser.LoadLookupAndEnumText(vRequester) 
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
    If Not (TypeOf (vEntitiesToTest) Is csUserCol) Then Return False 
    Dim pUserColToTest As csUserCol = CType(vEntitiesToTest, csUserCol) 
    Return isEqual(pUserColToTest) 
  End Function 
 
 
  ''' <summary> 
  ''' This checks if the objects are the same (not if the share the same address), IGNORING the dependants 
  ''' </summary> 
  ''' <param name="vUsersToTest"></param> 
  ''' <returns></returns> 
  Public Overloads Function isEqual(ByVal vUsersToTest As csUserCol) As Boolean
    If Me.Count <> vUsersToTest.Count Then Return False
    For i As Integer = 0 To Me.Count - 1 
      If Me(i).isEqual(vUsersToTest(i)) = False Then Return False
    Next
    Return True
  End Function
  
  ''' <summary> 
  ''' Used for Interface compliance. This clones the collection, returning an exact replica (collection of clones) 
  ''' </summary> 
  ''' <returns></returns> 
  Public Overrides Function CloneTargCCCollection() As ITargCCCollection 
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUsers As New csUserCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUsers._FilledFromSumOnTheFly = True
    
    For Each pUser As csUser In Me 
      Dim pUserClone As csUser = pUser.Clone() 
      pUsers.Add(pUserClone) 
      If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
    Next 
    Return pUsers 
  End Function 
 
  ''' <summary>
  ''' This clones the collection, returning an exact replica (collection of clones)
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function Clone() As csUserCol
    Dim pFilledFromSumOnTheFly = _FilledFromSumOnTheFly 
    Dim pUsers As New csUserCol(_WithParents) 
    If pFilledFromSumOnTheFly Then pUsers._FilledFromSumOnTheFly = True
    
    For Each pUser As csUser In Me
      Dim pUserClone As csUser = pUser.Clone()
      pUsers.Add(pUserClone)
      If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
    Next
    Return pUsers
  End Function
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by ID (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedID(ByVal vIDFrom As Long, ByVal vIDTo As Long) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.ID > vIDFrom AndAlso pUser.ID <= vIDTo) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by City (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedCity(ByVal vCityFrom As String, ByVal vCityTo As String) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.City > vCityFrom AndAlso pUser.City <= vCityTo) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by LastName and FirstName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedLastNameAndFirstName(ByVal vLastNameFrom As String, ByVal vLastNameTo As String, ByVal vFirstNameFrom As String, ByVal vFirstNameTo As String) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.LastName > vLastNameFrom AndAlso pUser.LastName <= vLastNameTo) AndAlso (pUser.FirstName > vFirstNameFrom AndAlso pUser.FirstName <= vFirstNameTo) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by LastSuccessfulLogin (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedLastSuccessfulLogin(ByVal vLastSuccessfulLoginStart As DateTimeOffset, ByVal vLastSuccessfulLoginEnd As DateTimeOffset) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.LastSuccessfulLogin > vLastSuccessfulLoginStart AndAlso pUser.LastSuccessfulLogin <= vLastSuccessfulLoginEnd) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by UserName (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.UserName > vUserNameFrom AndAlso pUser.UserName <= vUserNameTo) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects bounded by Type and IDinType (From - To) 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByBoundedTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinTypeFrom As Long, ByVal vIDinTypeTo As Long) As csUserCol 
    Dim pUsers As New csUserCol(_WithParents)  
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      If (pUser.Type = vType) AndAlso (pUser.IDinType > vIDinTypeFrom AndAlso pUser.IDinType <= vIDinTypeTo) Then 
        Dim pUserClone As csUser = pUser.Clone() 
        pUsers.Add(pUserClone) 
        If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
      End If 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardCity(ByVal vCity As String, ByVal vCityWildcardType As clsEnums.enmWildCardType) As csUserCol 
    Dim pUsers As New csUserCol 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vCityWildcardType = clsEnums.enmWildCardType.After Then 
        If pUser.City.StartsWith(vCity, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCityWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUser.City.EndsWith(vCity, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vCityWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUser.City.IndexOf(vCity, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vCityWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vCity.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUser.City.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pUserClone As csUser = pUser.Clone() 
      pUsers.Add(pUserClone) 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardLastNameAndFirstName(ByVal vLastName As String, ByVal vLastNameWildcardType As clsEnums.enmWildCardType, ByVal vFirstName As String, ByVal vFirstNameWildcardType As clsEnums.enmWildCardType) As csUserCol 
    Dim pUsers As New csUserCol 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vLastNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pUser.LastName.StartsWith(vLastName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vLastNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUser.LastName.EndsWith(vLastName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vLastNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUser.LastName.IndexOf(vLastName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vLastNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vLastName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUser.LastName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      If vFirstNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pUser.FirstName.StartsWith(vFirstName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFirstNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUser.FirstName.EndsWith(vFirstName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vFirstNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUser.FirstName.IndexOf(vFirstName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vFirstNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vFirstName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUser.FirstName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pUserClone As csUser = pUser.Clone() 
      pUsers.Add(pUserClone) 
    Next 
    Return pUsers 
  End Function 
  
  ''' <summary> 
  ''' This returns a clone of the collection, including only those objects that fulfill the wildcard 
  ''' </summary> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function CloneByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType) As csUserCol 
    Dim pUsers As New csUserCol 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList() 
      Dim pAdd As Boolean = False 
 
      If vUserNameWildcardType = clsEnums.enmWildCardType.After Then 
        If pUser.UserName.StartsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.Before Then 
        If pUser.UserName.EndsWith(vUserName, StringComparison.OrdinalIgnoreCase) Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfter Then 
        If pUser.UserName.IndexOf(vUserName, StringComparison.OrdinalIgnoreCase) > 0 Then pAdd = True 
      ElseIf vUserNameWildcardType = clsEnums.enmWildCardType.BeforeAndAfterAndBetweenEachLetter Then 
        Dim pLastIndex As Integer = 0 
        pAdd = True 
        For Each p As Char In vUserName.ToLowerInvariant().ToCharArray 
          Dim pNewIndex As Integer = pUser.UserName.ToLowerInvariant().IndexOf(p, pLastIndex) 
          If pNewIndex = -1 Then 
            pAdd = False 
            Exit For 
          Else 
            pLastIndex = pNewIndex 
          End If 
        Next 
      End If 
      If pAdd = False Then Continue For 
 
      Dim pUserClone As csUser = pUser.Clone() 
      pUsers.Add(pUserClone) 
    Next 
    Return pUsers 
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
      Dim pFunction As String = "csUserColLoadParents" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserCol 
      LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
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
  Public Function FindByID(ByVal vID As Long) As csUser
    If Me.Count = 0 Then Return New csUser 
    
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
    
    Dim pUser As csUser = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(vID, pUser) 
    If pUser IsNot Nothing Then Return pUser Else Return New csUser() 
  End Function
  
  ''' <summary>
  ''' This returns a unique object in the collection. It searches locally, within the collection. It does not access the database 
  ''' If it doesn't find anything, it creates a new, empty object
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FindByUserName(ByVal vUserName As String) As csUser
    If Me.Count = 0 Then Return New csUser 
    
    If _RecreateDictionaryForFindByUserName = True Then LoadUserNames() 
    
    ' Capture the current reference to a local variable.
    ' This ensures that even if LoadIDs replaces the dictionary halfway through 
    ' this function, we are still looking at the valid (older) snapshot.
    Dim pLocalDict As Dictionary(Of String, csUser) = _SortedDictionaryForFindByUserName 
    
    Dim pUser As csUser = Nothing 
    
    ' Add a safety check in case pLocalDict is nothing (though Load{pIndex.Name}s should prevent this)
    Dim pValueToSearchFor As String = vUserName
    If pLocalDict IsNot Nothing Then pLocalDict.TryGetValue(pValueToSearchFor, pUser) 
    If pUser IsNot Nothing Then Return pUser Else Return New csUser() 
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByUserName(ByVal vUserName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vUserName = vUserName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.UserName.ToLowerInvariant() = vUserName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByUserName with vUserName of {vUserName}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.UserName.ToLowerInvariant() = vUserName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastName(ByVal vLastName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastName = vLastName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.LastName.ToLowerInvariant() = vLastName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastName with vLastName of {vLastName}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.LastName.ToLowerInvariant() = vLastName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FirstName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFirstName(ByVal vFirstName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFirstName = vFirstName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.FirstName.ToLowerInvariant() = vFirstName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFirstName with vFirstName of {vFirstName}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.FirstName.ToLowerInvariant() = vFirstName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined FullName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByFullName(ByVal vFullName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vFullName = vFullName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.FullName.ToLowerInvariant() = vFullName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByFullName with vFullName of {vFullName}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.FullName.ToLowerInvariant() = vFullName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined NationalIDNo
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByNationalIDNo(ByVal vNationalIDNo As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vNationalIDNo = vNationalIDNo.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.NationalIDNo.ToLowerInvariant() = vNationalIDNo Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByNationalIDNo with vNationalIDNo of {vNationalIDNo}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.NationalIDNo.ToLowerInvariant() = vNationalIDNo Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Address
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAddress(ByVal vAddress As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vAddress = vAddress.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Address.ToLowerInvariant() = vAddress Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAddress with vAddress of {vAddress}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Address.ToLowerInvariant() = vAddress Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined City
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCity(ByVal vCity As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCity = vCity.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.City.ToLowerInvariant() = vCity Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCity with vCity of {vCity}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.City.ToLowerInvariant() = vCity Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ProvinceState
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByProvinceState(ByVal vProvinceState As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vProvinceState = vProvinceState.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.ProvinceState.ToLowerInvariant() = vProvinceState Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByProvinceState with vProvinceState of {vProvinceState}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.ProvinceState.ToLowerInvariant() = vProvinceState Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PostalCode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPostalCode(ByVal vPostalCode As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vPostalCode = vPostalCode.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.PostalCode.ToLowerInvariant() = vPostalCode Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPostalCode with vPostalCode of {vPostalCode}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.PostalCode.ToLowerInvariant() = vPostalCode Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Country
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByCountry(ByVal vCountry As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vCountry = vCountry.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Country.ToLowerInvariant() = vCountry Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByCountry with vCountry of {vCountry}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Country.ToLowerInvariant() = vCountry Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PhoneNumber
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPhoneNumber(ByVal vPhoneNumber As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vPhoneNumber = vPhoneNumber.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.PhoneNumber.ToLowerInvariant() = vPhoneNumber Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPhoneNumber with vPhoneNumber of {vPhoneNumber}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.PhoneNumber.ToLowerInvariant() = vPhoneNumber Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Email
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEmail(ByVal vEmail As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vEmail = vEmail.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Email.ToLowerInvariant() = vEmail Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEmail with vEmail of {vEmail}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Email.ToLowerInvariant() = vEmail Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DatePasswordChanged
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDatePasswordChanged(ByVal vDatePasswordChanged As Date) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.DatePasswordChanged = vDatePasswordChanged Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDatePasswordChanged with vDatePasswordChanged of {vDatePasswordChanged}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.DatePasswordChanged = vDatePasswordChanged Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Type
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByType(ByVal vType As clsEnums.enmUserIdentityType) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Type = vType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByType with vType of {vType}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Type = vType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IDinType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIDinType(ByVal vIDinType As Long) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.IDinType = vIDinType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIDinType with vIDinType of {vIDinType}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.IDinType = vIDinType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RequiresComputerIdentification
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRequiresComputerIdentification(ByVal vRequiresComputerIdentification As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.RequiresComputerIdentification = vRequiresComputerIdentification Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRequiresComputerIdentification with vRequiresComputerIdentification of {vRequiresComputerIdentification}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.RequiresComputerIdentification = vRequiresComputerIdentification Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined EnableSimultaneousLogins
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByEnableSimultaneousLogins(ByVal vEnableSimultaneousLogins As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.EnableSimultaneousLogins = vEnableSimultaneousLogins Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByEnableSimultaneousLogins with vEnableSimultaneousLogins of {vEnableSimultaneousLogins}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.EnableSimultaneousLogins = vEnableSimultaneousLogins Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined DateActivated
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByDateActivated(ByVal vDateActivated As Date) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.DateActivated = vDateActivated Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByDateActivated with vDateActivated of {vDateActivated}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.DateActivated = vDateActivated Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsDisabled
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsDisabled(ByVal vIsDisabled As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.IsDisabled = vIsDisabled Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsDisabled with vIsDisabled of {vIsDisabled}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.IsDisabled = vIsDisabled Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ExpiryDate
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByExpiryDate(ByVal vExpiryDate As Date) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.ExpiryDate = vExpiryDate Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByExpiryDate with vExpiryDate of {vExpiryDate}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.ExpiryDate = vExpiryDate Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Comments
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByComments(ByVal vComments As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vComments = vComments.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Comments.ToLowerInvariant() = vComments Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByComments with vComments of {vComments}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Comments.ToLowerInvariant() = vComments Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastPasswords
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastPasswords(ByVal vLastPasswords As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLastPasswords = vLastPasswords.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.LastPasswords.ToLowerInvariant() = vLastPasswords Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastPasswords with vLastPasswords of {vLastPasswords}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.LastPasswords.ToLowerInvariant() = vLastPasswords Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Applications
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApplications(ByVal vApplications As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApplications = vApplications.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Applications.ToLowerInvariant() = vApplications Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApplications with vApplications of {vApplications}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Applications.ToLowerInvariant() = vApplications Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Language
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLanguage(ByVal vLanguage As clsEnums.enmLanguage) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Language = vLanguage Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLanguage with vLanguage of {vLanguage}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Language = vLanguage Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined IsLockedOut
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByIsLockedOut(ByVal vIsLockedOut As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.IsLockedOut = vIsLockedOut Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByIsLockedOut with vIsLockedOut of {vIsLockedOut}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.IsLockedOut = vIsLockedOut Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RoleID
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRoleID(ByVal vRoleID As Long) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.RoleID = vRoleID Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRoleID with vRoleID of {vRoleID}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.RoleID = vRoleID Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined AuthenticationMethod
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByAuthenticationMethod(ByVal vAuthenticationMethod As clsEnums.enmAuthenticationMethod) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.AuthenticationMethod = vAuthenticationMethod Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByAuthenticationMethod with vAuthenticationMethod of {vAuthenticationMethod}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.AuthenticationMethod = vAuthenticationMethod Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined RequiresFixedIP
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByRequiresFixedIP(ByVal vRequiresFixedIP As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.RequiresFixedIP = vRequiresFixedIP Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByRequiresFixedIP with vRequiresFixedIP of {vRequiresFixedIP}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.RequiresFixedIP = vRequiresFixedIP Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined MessagingMode
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByMessagingMode(ByVal vMessagingMode As clsEnums.enmMessagingMode) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.MessagingMode = vMessagingMode Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByMessagingMode with vMessagingMode of {vMessagingMode}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.MessagingMode = vMessagingMode Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LoggedInIP
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLoggedInIP(ByVal vLoggedInIP As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vLoggedInIP = vLoggedInIP.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.LoggedInIP.ToLowerInvariant() = vLoggedInIP Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLoggedInIP with vLoggedInIP of {vLoggedInIP}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.LoggedInIP.ToLowerInvariant() = vLoggedInIP Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApprovalFunctionName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApprovalFunctionName(ByVal vApprovalFunctionName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vApprovalFunctionName = vApprovalFunctionName.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.ApprovalFunctionName.ToLowerInvariant() = vApprovalFunctionName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApprovalFunctionName with vApprovalFunctionName of {vApprovalFunctionName}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.ApprovalFunctionName.ToLowerInvariant() = vApprovalFunctionName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined ApprovalTime
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByApprovalTime(ByVal vApprovalTime As DateTimeOffset) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.ApprovalTime = vApprovalTime Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByApprovalTime with vApprovalTime of {vApprovalTime}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.ApprovalTime = vApprovalTime Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastSuccessfulLogin
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastSuccessfulLogin(ByVal vLastSuccessfulLogin As DateTimeOffset) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.LastSuccessfulLogin = vLastSuccessfulLogin Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByLastSuccessfulLogin with vLastSuccessfulLogin of {vLastSuccessfulLogin}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.LastSuccessfulLogin = vLastSuccessfulLogin Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined PasswordNeverExpires
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByPasswordNeverExpires(ByVal vPasswordNeverExpires As Boolean) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.PasswordNeverExpires = vPasswordNeverExpires Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByPasswordNeverExpires with vPasswordNeverExpires of {vPasswordNeverExpires}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.PasswordNeverExpires = vPasswordNeverExpires Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SecurityQuestion1Code
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySecurityQuestion1Code(ByVal vSecurityQuestion1Code As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSecurityQuestion1Code = vSecurityQuestion1Code.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.SecurityQuestion1Code.ToLowerInvariant() = vSecurityQuestion1Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySecurityQuestion1Code with vSecurityQuestion1Code of {vSecurityQuestion1Code}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.SecurityQuestion1Code.ToLowerInvariant() = vSecurityQuestion1Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SecurityQuestion2Code
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySecurityQuestion2Code(ByVal vSecurityQuestion2Code As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSecurityQuestion2Code = vSecurityQuestion2Code.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.SecurityQuestion2Code.ToLowerInvariant() = vSecurityQuestion2Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySecurityQuestion2Code with vSecurityQuestion2Code of {vSecurityQuestion2Code}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.SecurityQuestion2Code.ToLowerInvariant() = vSecurityQuestion2Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined SecurityQuestion3Code
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneBySecurityQuestion3Code(ByVal vSecurityQuestion3Code As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vSecurityQuestion3Code = vSecurityQuestion3Code.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.SecurityQuestion3Code.ToLowerInvariant() = vSecurityQuestion3Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneBySecurityQuestion3Code with vSecurityQuestion3Code of {vSecurityQuestion3Code}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.SecurityQuestion3Code.ToLowerInvariant() = vSecurityQuestion3Code Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined Tag
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTag(ByVal vTag As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    ' 1. Trigger the rebuild if needed 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    ' 2. CAPTURE THE SNAPSHOT (Crucial for Thread Safety) 
    ' We grab the reference now. Even if another thread swaps the dictionary  
    ' a millisecond later, 'pLocalDict' points to the valid object we just grabbed. 
    Dim pTempDist As Dictionary(Of Long, csUser) = _SortedDictionaryForFindByID 
 
    ' 3. READ FROM THE SNAPSHOT (Lock-Free Speed) 
    vTag = vTag.ToLowerInvariant() 
    If pTempDist IsNot Nothing AndAlso pTempDist.Count > 0 Then 
      'This is faster 
      For Each pUser As csUser In pTempDist.Values
        If pUser.Tag.ToLowerInvariant() = vTag Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      If MyController.LogDetails Then Tools.LogToTextFile.WriteMessage($"In 2nd part of clone for CloneByTag with vTag of {vTag}", "2ndPartOfClone") 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Tag.ToLowerInvariant() = vTag Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined LastNameAndFirstName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByLastNameAndFirstName(ByVal vLastName As String, ByVal vFirstName As String) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList()
        If pUser.LastName = vLastName AndAlso pUser.FirstName = vFirstName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.LastName = vLastName AndAlso pUser.FirstName = vFirstName Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    Return pUsers
  End Function
  
  ''' <summary>
  ''' This returns a clone of the collection, including only those object with the defined TypeAndIDinType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CloneByTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinType As Long) As csUserCol
    Dim pUsers As New csUserCol(_WithParents) 
    pUsers._FilledFromSumOnTheFly = _FilledFromSumOnTheFly 
 
    'Use the dictionary to improve thread safety 
    If _RecreateDictionaryForFindByID = True Then LoadIDs() 
 
    If Not _SortedDictionaryForFindByID.Count = 0 Then 
      'This is faster 
      For Each pUser As csUser In _SortedDictionaryForFindByID.Values.ToList()
        If pUser.Type = vType AndAlso pUser.IDinType = vIDinType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    Else 
      'We have no ID, so we can't use it 
      Dim pList As csUserCol = Me.Clone() 
      For Each pUser As csUser In pList 
        If pUser.Type = vType AndAlso pUser.IDinType = vIDinType Then
          Dim pUserClone As csUser = pUser.Clone()
          pUsers.Add(pUserClone)
          If Not _FilledFromSumOnTheFly Then pUsers._Clean.Add(pUser.ID) 
        End If
      Next
    End If 
    Return pUsers
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
    For Each pUser As csUser In Me 
      Dim pRow As DataRow = vDataTable.NewRow() 
      pFault = pUser.LoadDataRow(pRow, vRequester) 
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
    For Each p As csUser In Me 
      If p.ID = 0 Then p.ccStatus = clsEnums.enmObjectStatus.New 
    Next 
 
    'assign rows  to be deleted. 
    For Each pCleanID As Long In _Clean 
      If pCleanID = 0 Then Continue For 
      Dim pFound As csUser = Me.FindByID(pCleanID) 
      If pFound.ID = 0 Then 
        Dim pUserToKill As New csUser 
        pUserToKill.ID = pCleanID 
        pUserToKill.ccStatus = clsEnums.enmObjectStatus.Deleted 
        Me.Add(pUserToKill) 
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
      Dim pFunction As String = "csUserColUpdate" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
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
      Dim pFunction As String = "csUserColUpdateFromCollection" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      'Use the response to build the UserCol 
      If vReload = True Then 
        LoadByteArray(pResponse, pFault, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-150314-1803", vRequester) 
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
 
      Dim pFunction As String = "csUserColDelete" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, "Parameters: None", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific City 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByCity(ByVal vCity As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("City={0}", vCity)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCity 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByCity" 
      Dim pParametersToLog = $"City: {vCity};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LastNameAndFirstName 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLastNameAndFirstName(ByVal vLastName As String, ByVal vFirstName As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastName={0}, FirstName={1}", vLastName, vFirstName)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) 
          ' 
          'vFirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastName};{vFirstName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LastSuccessfulLogin 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByLastSuccessfulLogin(ByVal vLastSuccessfulLogin As DateTimeOffset, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastSuccessfulLogin={0}", vLastSuccessfulLogin)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastSuccessfulLogin 
          pBinaryWriter.Write(vLastSuccessfulLogin.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLogin.Offset.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByLastSuccessfulLogin" 
      Dim pParametersToLog = $"LastSuccessfulLogin: {vLastSuccessfulLogin};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific RoleID 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByRoleID(ByVal vRoleID As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("RoleID={0}", vRoleID)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vRoleID 
          pBinaryWriter.Write(vRoleID) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByRoleID" 
      Dim pParametersToLog = $"RoleID: {vRoleID};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TypeAndIDinType 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinType As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, IDinType={1}", vType, vIDinType)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vType 
          pBinaryWriter.Write(vType.ToString()) 
          ' 
          'vIDinType 
          pBinaryWriter.Write(vIDinType) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByTypeAndIDinType" 
      Dim pParametersToLog = $"TypeAndIDinType: {vType};{vIDinType};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-User-200709-0852-", vRequester) 
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
 
      Dim pFunction As String = "csUserColDeleteByBoundedID" 
      Dim pParametersToLog = $"ID: {vIDFrom};{vIDTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific City
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedCity(ByVal vCityFrom As String, ByVal vCityTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("CityFrom={0}, CityTo={1}", vCityFrom, vCityTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCityFrom 
          If vCityFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCityFrom) 
          ' 
          'vCityTo 
          If vCityTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCityTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByBoundedCity" 
      Dim pParametersToLog = $"City: {vCityFrom};{vCityTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LastNameAndFirstName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedLastNameAndFirstName(ByVal vLastNameFrom As String, ByVal vLastNameTo As String, ByVal vFirstNameFrom As String, ByVal vFirstNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastNameFrom={0}, LastNameTo={1}, FirstNameFrom={2}, FirstNameTo={3}", vLastNameFrom, vLastNameTo, vFirstNameFrom, vFirstNameTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastNameFrom 
          If vLastNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastNameFrom) 
          ' 
          'vLastNameTo 
          If vLastNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastNameTo) 
          ' 
          'vFirstNameFrom 
          If vFirstNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstNameFrom) 
          ' 
          'vFirstNameTo 
          If vFirstNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstNameTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByBoundedLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastNameFrom};{vLastNameTo};{vFirstNameFrom};{vFirstNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific LastSuccessfulLogin
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedLastSuccessfulLogin(ByVal vLastSuccessfulLoginStart As DateTimeOffset, ByVal vLastSuccessfulLoginEnd As DateTimeOffset, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastSuccessfulLoginStart={0}, LastSuccessfulLoginEnd={1}", vLastSuccessfulLoginStart, vLastSuccessfulLoginEnd)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastSuccessfulLoginStart 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLoginStart.Offset.Ticks) 
          ' 
          'vLastSuccessfulLoginEnd 
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.DateTime.Ticks) 
          pBinaryWriter.Write(vLastSuccessfulLoginEnd.Offset.Ticks) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByBoundedLastSuccessfulLogin" 
      Dim pParametersToLog = $"LastSuccessfulLogin: {vLastSuccessfulLoginStart};{vLastSuccessfulLoginEnd};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedUserName(ByVal vUserNameFrom As String, ByVal vUserNameTo As String, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserNameFrom={0}, UserNameTo={1}", vUserNameFrom, vUserNameTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserNameFrom 
          If vUserNameFrom Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameFrom) 
          ' 
          'vUserNameTo 
          If vUserNameTo Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserNameTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByBoundedUserName" 
      Dim pParametersToLog = $"UserName: {vUserNameFrom};{vUserNameTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a specific TypeAndIDinType
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByBoundedTypeAndIDinType(ByVal vType As clsEnums.enmUserIdentityType, ByVal vIDinTypeFrom As Long, ByVal vIDinTypeTo As Long, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("Type={0}, IDinTypeFrom={1}, IDinTypeTo={2}", vType, vIDinTypeFrom, vIDinTypeTo)
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vType 
          pBinaryWriter.Write(vType.ToString()) 
          ' 
          'vIDinTypeFrom 
          pBinaryWriter.Write(vIDinTypeFrom) 
          ' 
          'vIDinTypeTo 
          pBinaryWriter.Write(vIDinTypeTo) 
          ' 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByBoundedTypeAndIDinType" 
      Dim pParametersToLog = $"TypeAndIDinType: {vType};{vIDinTypeFrom};{vIDinTypeTo};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded City
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardCity(ByVal vCity As String, ByVal vCityWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("City={0}, CityWildcardType={1}", vCity, vCityWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vCity 
          If vCity Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vCity) 
          ' 
          pBinaryWriter.Write(vCityWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByWildCardCity" 
      Dim pParametersToLog = $"City: {vCity};{vCityWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded LastNameAndFirstName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardLastNameAndFirstName(ByVal vLastName As String, ByVal vLastNameWildcardType As clsEnums.enmWildCardType, ByVal vFirstName As String, ByVal vFirstNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("LastName={0}, LastNameWildcardType={1}, FirstName={2}, FirstNameWildcardType={3}", vLastName, vLastNameWildcardType.FastToString(), vFirstName, vFirstNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vLastName 
          If vLastName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vLastName) 
          ' 
          pBinaryWriter.Write(vLastNameWildcardType.FastToString())
          'vFirstName 
          If vFirstName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vFirstName) 
          ' 
          pBinaryWriter.Write(vFirstNameWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByWildCardLastNameAndFirstName" 
      Dim pParametersToLog = $"LastNameAndFirstName: {vLastName};{vLastNameWildcardType.FastToString()};{vFirstName};{vFirstNameWildcardType.FastToString()};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-Mail-200709-0852-", vRequester) 
    End Try 
 
    pFault.SetOK() 
    Return pFault 
  End Function
        
  ''' <summary>
  ''' Deletes a collection of all the items for a WildCarded UserName
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DeleteByWildCardUserName(ByVal vUserName As String, ByVal vUserNameWildcardType As clsEnums.enmWildCardType, ByVal vRequester As clsRequester) As clsFault
    Dim pFunctionParameters As String = String.Format("UserName={0}, UserNameWildcardType={1}", vUserName, vUserNameWildcardType.FastToString())
    Dim pFault As New clsFault
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pHasValue As Boolean = False 
          'vUserName 
          If vUserName Is Nothing Then pHasValue = False Else pHasValue = True 
          pBinaryWriter.Write(pHasValue) 
          If pHasValue = True Then pBinaryWriter.Write(vUserName) 
          ' 
          pBinaryWriter.Write(vUserNameWildcardType.FastToString())
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      Dim pFunction As String = "csUserColDeleteByWildCardUserName" 
      Dim pParametersToLog = $"UserName: {vUserName};{vUserNameWildcardType.FastToString()};" 
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
    Me.Sort(New csUserCol.CompareByID)
  End Sub
  Private Class CompareByID
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
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
  
  Public Sub SortByUserName()
    Me.Sort(New csUserCol.CompareByUserName)
  End Sub
  Private Class CompareByUserName
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.UserName, y.UserName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastName()
    Me.Sort(New csUserCol.CompareByLastName)
  End Sub
  Private Class CompareByLastName
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastName, y.LastName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFirstName()
    Me.Sort(New csUserCol.CompareByFirstName)
  End Sub
  Private Class CompareByFirstName
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FirstName, y.FirstName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByFullName()
    Me.Sort(New csUserCol.CompareByFullName)
  End Sub
  Private Class CompareByFullName
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.FullName, y.FullName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByNationalIDNo()
    Me.Sort(New csUserCol.CompareByNationalIDNo)
  End Sub
  Private Class CompareByNationalIDNo
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.NationalIDNo, y.NationalIDNo, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAddress()
    Me.Sort(New csUserCol.CompareByAddress)
  End Sub
  Private Class CompareByAddress
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Address, y.Address, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCity()
    Me.Sort(New csUserCol.CompareByCity)
  End Sub
  Private Class CompareByCity
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.City, y.City, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByProvinceState()
    Me.Sort(New csUserCol.CompareByProvinceState)
  End Sub
  Private Class CompareByProvinceState
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ProvinceState, y.ProvinceState, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPostalCode()
    Me.Sort(New csUserCol.CompareByPostalCode)
  End Sub
  Private Class CompareByPostalCode
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.PostalCode, y.PostalCode, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByCountry()
    Me.Sort(New csUserCol.CompareByCountry)
  End Sub
  Private Class CompareByCountry
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Country, y.Country, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByPhoneNumber()
    Me.Sort(New csUserCol.CompareByPhoneNumber)
  End Sub
  Private Class CompareByPhoneNumber
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.PhoneNumber, y.PhoneNumber, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEmail()
    Me.Sort(New csUserCol.CompareByEmail)
  End Sub
  Private Class CompareByEmail
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Email, y.Email, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDatePasswordChanged()
    Me.Sort(New csUserCol.CompareByDatePasswordChanged)
  End Sub
  Private Class CompareByDatePasswordChanged
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DatePasswordChanged < y.DatePasswordChanged Then
        Return -1
      ElseIf x.DatePasswordChanged = y.DatePasswordChanged Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByType()
    Me.Sort(New csUserCol.CompareByType)
  End Sub
  Private Class CompareByType
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.Type < y.Type Then
        Return -1
      ElseIf x.Type = y.Type Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByTypeText()
    Me.Sort(New csUserCol.CompareByTypeText)
  End Sub
  Private Class CompareByTypeText
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.TypeText, y.TypeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIDinType()
    Me.Sort(New csUserCol.CompareByIDinType)
  End Sub
  Private Class CompareByIDinType
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.IDinType < y.IDinType Then
        Return -1
      ElseIf x.IDinType = y.IDinType Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRequiresComputerIdentification()
    Me.Sort(New csUserCol.CompareByRequiresComputerIdentification)
  End Sub
  Private Class CompareByRequiresComputerIdentification
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RequiresComputerIdentification.ToString, y.RequiresComputerIdentification.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByEnableSimultaneousLogins()
    Me.Sort(New csUserCol.CompareByEnableSimultaneousLogins)
  End Sub
  Private Class CompareByEnableSimultaneousLogins
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.EnableSimultaneousLogins.ToString, y.EnableSimultaneousLogins.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByDateActivated()
    Me.Sort(New csUserCol.CompareByDateActivated)
  End Sub
  Private Class CompareByDateActivated
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.DateActivated < y.DateActivated Then
        Return -1
      ElseIf x.DateActivated = y.DateActivated Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByIsDisabled()
    Me.Sort(New csUserCol.CompareByIsDisabled)
  End Sub
  Private Class CompareByIsDisabled
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsDisabled.ToString, y.IsDisabled.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByExpiryDate()
    Me.Sort(New csUserCol.CompareByExpiryDate)
  End Sub
  Private Class CompareByExpiryDate
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ExpiryDate < y.ExpiryDate Then
        Return -1
      ElseIf x.ExpiryDate = y.ExpiryDate Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByComments()
    Me.Sort(New csUserCol.CompareByComments)
  End Sub
  Private Class CompareByComments
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Comments, y.Comments, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLastPasswords()
    Me.Sort(New csUserCol.CompareByLastPasswords)
  End Sub
  Private Class CompareByLastPasswords
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LastPasswords, y.LastPasswords, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApplications()
    Me.Sort(New csUserCol.CompareByApplications)
  End Sub
  Private Class CompareByApplications
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.Applications, y.Applications, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLanguage()
    Me.Sort(New csUserCol.CompareByLanguage)
  End Sub
  Private Class CompareByLanguage
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
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
    Me.Sort(New csUserCol.CompareByLanguageText)
  End Sub
  Private Class CompareByLanguageText
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LanguageText, y.LanguageText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByIsLockedOut()
    Me.Sort(New csUserCol.CompareByIsLockedOut)
  End Sub
  Private Class CompareByIsLockedOut
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.IsLockedOut.ToString, y.IsLockedOut.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRoleID()
    Me.Sort(New csUserCol.CompareByRoleID)
  End Sub
  Private Class CompareByRoleID
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.RoleID < y.RoleID Then
        Return -1
      ElseIf x.RoleID = y.RoleID Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByRoleText()
    Me.Sort(New csUserCol.CompareByRoleText)
  End Sub
  Private Class CompareByRoleText
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RoleText, y.RoleText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByAuthenticationMethod()
    Me.Sort(New csUserCol.CompareByAuthenticationMethod)
  End Sub
  Private Class CompareByAuthenticationMethod
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.AuthenticationMethod < y.AuthenticationMethod Then
        Return -1
      ElseIf x.AuthenticationMethod = y.AuthenticationMethod Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByAuthenticationMethodText()
    Me.Sort(New csUserCol.CompareByAuthenticationMethodText)
  End Sub
  Private Class CompareByAuthenticationMethodText
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.AuthenticationMethodText, y.AuthenticationMethodText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByRequiresFixedIP()
    Me.Sort(New csUserCol.CompareByRequiresFixedIP)
  End Sub
  Private Class CompareByRequiresFixedIP
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.RequiresFixedIP.ToString, y.RequiresFixedIP.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByMessagingMode()
    Me.Sort(New csUserCol.CompareByMessagingMode)
  End Sub
  Private Class CompareByMessagingMode
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.MessagingMode < y.MessagingMode Then
        Return -1
      ElseIf x.MessagingMode = y.MessagingMode Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByMessagingModeText()
    Me.Sort(New csUserCol.CompareByMessagingModeText)
  End Sub
  Private Class CompareByMessagingModeText
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.MessagingModeText, y.MessagingModeText, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByLoggedInIP()
    Me.Sort(New csUserCol.CompareByLoggedInIP)
  End Sub
  Private Class CompareByLoggedInIP
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.LoggedInIP, y.LoggedInIP, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApprovalFunctionName()
    Me.Sort(New csUserCol.CompareByApprovalFunctionName)
  End Sub
  Private Class CompareByApprovalFunctionName
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.ApprovalFunctionName, y.ApprovalFunctionName, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByApprovalTime()
    Me.Sort(New csUserCol.CompareByApprovalTime)
  End Sub
  Private Class CompareByApprovalTime
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.ApprovalTime < y.ApprovalTime Then
        Return -1
      ElseIf x.ApprovalTime = y.ApprovalTime Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByLastSuccessfulLogin()
    Me.Sort(New csUserCol.CompareByLastSuccessfulLogin)
  End Sub
  Private Class CompareByLastSuccessfulLogin
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      If x.LastSuccessfulLogin < y.LastSuccessfulLogin Then
        Return -1
      ElseIf x.LastSuccessfulLogin = y.LastSuccessfulLogin Then
        Return 0
      Else
        Return 1
      End If
    End Function
  End Class
  
  Public Sub SortByPasswordNeverExpires()
    Me.Sort(New csUserCol.CompareByPasswordNeverExpires)
  End Sub
  Private Class CompareByPasswordNeverExpires
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.PasswordNeverExpires.ToString, y.PasswordNeverExpires.ToString, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion1Code()
    Me.Sort(New csUserCol.CompareBySecurityQuestion1Code)
  End Sub
  Private Class CompareBySecurityQuestion1Code
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion1Code, y.SecurityQuestion1Code, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion1Text()
    Me.Sort(New csUserCol.CompareBySecurityQuestion1Text)
  End Sub
  Private Class CompareBySecurityQuestion1Text
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion1Text, y.SecurityQuestion1Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion2Code()
    Me.Sort(New csUserCol.CompareBySecurityQuestion2Code)
  End Sub
  Private Class CompareBySecurityQuestion2Code
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion2Code, y.SecurityQuestion2Code, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion2Text()
    Me.Sort(New csUserCol.CompareBySecurityQuestion2Text)
  End Sub
  Private Class CompareBySecurityQuestion2Text
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion2Text, y.SecurityQuestion2Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion3Code()
    Me.Sort(New csUserCol.CompareBySecurityQuestion3Code)
  End Sub
  Private Class CompareBySecurityQuestion3Code
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion3Code, y.SecurityQuestion3Code, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortBySecurityQuestion3Text()
    Me.Sort(New csUserCol.CompareBySecurityQuestion3Text)
  End Sub
  Private Class CompareBySecurityQuestion3Text
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
      If x Is Nothing AndAlso y Is Nothing Then Return 0
      If x Is Nothing And Not y Is Nothing Then Return 1
      If Not x Is Nothing And y Is Nothing Then Return -1
      Return String.Compare(x.SecurityQuestion3Text, y.SecurityQuestion3Text, StringComparison.OrdinalIgnoreCase)
    End Function
  End Class
  
  Public Sub SortByTag()
    Me.Sort(New csUserCol.CompareByTag)
  End Sub
  Private Class CompareByTag
    Implements IComparer(Of csUser)
    Private Function Compare(ByVal x As csUser, ByVal y As csUser) As Integer Implements System.Collections.Generic.IComparer(Of csUser).Compare
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
    
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUser) 
    _RecreateDictionaryForFindByID = False 
    _SortedDictionaryForFindByUserName = New Dictionary(Of String, csUser)(StringComparer.OrdinalIgnoreCase) 
    _RecreateDictionaryForFindByUserName = False 
  End Sub 
  
  Private Sub CreateEmpty() 
    _Tag = "" 
    _Clean = New List(Of Long) 
    _FilledFromSumOnTheFly = False 
 
    _SortedDictionaryForFindByID = New Dictionary(Of Long, csUser) 
    _SortedDictionaryForFindByUserName = New Dictionary(Of String, csUser)(StringComparer.OrdinalIgnoreCase) 
 
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
  
