'Created by TargCC Version 4.0.6.3
<Assembly: CLSCompliant(True)>
<Assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TargCCOrders.WSControllerBL")>
 
Public Class clsRequester 
 
  Private _UserID As Long 
  Private _UserName As String 
  Private _UserEnableSimultaneousLogins As Boolean 
  Private _LoggedLoginID As Long 
 
  Private _UserFullName As String 
  Private _UserPIN As String 
  Private _Roles As String 
  Private _UserIdentityType As clsEnums.enmUserIdentityType 
  Private _UserIdentityInstanceID As Long 
 
  Private _UILang As clsEnums.enmLanguage 
  Private _CallingApplication As String 
  Private _CallingApplicationVersion As String 
  Private _CallingFunctionWithinApplication As String 
  Private _EntryFunction As String 
  Private _SpecificUserCredentials As System.Net.NetworkCredential 
 
  Public ReadOnly Property UserID() As Long 
    Get 
      Return _UserID 
    End Get 
  End Property 
  Public ReadOnly Property UserName() As String 
    Get 
      Return _UserName 
    End Get 
  End Property 
  Public ReadOnly Property UserEnableSimultaneousLogins As Boolean 
    Get 
      Return _UserEnableSimultaneousLogins 
    End Get 
  End Property 
  Public ReadOnly Property LoggedLoginID() As Long 
    Get 
      Return _LoggedLoginID 
    End Get 
  End Property 
 
  Public ReadOnly Property UserFullName() As String 
    Get 
      Return _UserFullName 
    End Get 
  End Property 
  Public ReadOnly Property UserPIN() As String 
    Get 
      Return _UserPIN 
    End Get 
  End Property 
  Public ReadOnly Property Roles() As String 
    Get 
      Return _Roles 
    End Get 
  End Property 
  Public ReadOnly Property UserIdentityType() As clsEnums.enmUserIdentityType 
    Get 
      Return _UserIdentityType 
    End Get 
  End Property 
  Public ReadOnly Property UserIdentityInstanceID() As Long 
    Get 
      Return _UserIdentityInstanceID 
    End Get 
  End Property 
 
 
  Public ReadOnly Property UILang() As clsEnums.enmLanguage 
    Get 
      Return _UILang 
    End Get 
  End Property 
  Public ReadOnly Property CallingApplication() As String 
    Get 
      Return _CallingApplication 
    End Get 
  End Property 
  Public ReadOnly Property CallingApplicationVersion() As String 
    Get 
      Return _CallingApplicationVersion 
    End Get 
  End Property 
  Public Property CallingFunctionWithinApplication() As String 
    Get 
      Return _CallingFunctionWithinApplication 
    End Get 
    Set(ByVal value As String) 
      _CallingFunctionWithinApplication = value 
    End Set 
  End Property 
  Friend ReadOnly Property Credential() As System.Net.NetworkCredential 
    Get 
      Dim pCredential As System.Net.NetworkCredential = Nothing 
 
      If MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials Then 
        pCredential = New System.Net.NetworkCredential(MyController.ApplicationName, ccHelper.GetSecureString(MyController.ApplicationPwd)) 
      ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials Then 
        pCredential = System.Net.CredentialCache.DefaultNetworkCredentials 
      ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials Then 
        pCredential = _SpecificUserCredentials 
      End If 
      Return pCredential 
    End Get 
  End Property 
 
  Private _Tag As Object 
  ''' <summary> 
  ''' This property is not sent outside the assembly. It's used to simplify injection of any information between functions in the WSController. 
  ''' This property is not transferred when cloning the Requester object 
  ''' </summary> 
  ''' <returns></returns> 
  Friend Property Tag() As Object 
    Get 
      Return _Tag 
    End Get 
    Set(ByVal value As Object) 
      _Tag = value 
    End Set 
  End Property 
 
  Public Sub New() 
    CreateEmpty() 
  End Sub 
 
  ''' <summary> 
  ''' Default Roles are SysAdmin, Administrator, UserManager, User 
  ''' </summary> 
  ''' <param name="vRoleName"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Function IsInRole(ByVal vRoleName As String) As Boolean 
    If Not (String.IsNullOrEmpty(vRoleName)) AndAlso _Roles.IndexOf("#" & vRoleName & "~", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      Return True 
    Else 
      Return False 
    End If 
  End Function 
 
  ''' <summary> 
  ''' Gets list of roles  
  ''' </summary> 
  ''' <returns></returns> 
  Public Function GetRoleList() As List(Of String) 
    Dim pRoleList As New List(Of String) 
 
    Dim pRoles = _Roles.Split("#"c) 
 
    For Each l In pRoles 
      If l.IndexOf("~") < 0 Then Continue For 
      Dim pRoleText = l.Split("~"c)(0) 
      pRoleList.Add(pRoleText) 
    Next 
 
    Return pRoleList 
  End Function 
 
  Friend Sub SetSpecificUserCredentials(ByVal vUserName As String, ByVal vPassword As String) 
    _SpecificUserCredentials = New System.Net.NetworkCredential(vUserName, ccHelper.GetSecureString(vPassword)) 
  End Sub 
 
  Public Function CreateTicket() As String 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
 
    Dim pTicket As New Text.StringBuilder 
 
    pTicket.Append(_UserID.ToString() & "|") 
    pTicket.Append(_UserName & "|") 
    If _UserEnableSimultaneousLogins = True Then 
      pTicket.Append("1" & "|") 
    Else 
      pTicket.Append("0" & "|") 
    End If 
    pTicket.Append(_LoggedLoginID.ToString() & "|") 
    pTicket.Append(_UserFullName & "|") 
    pTicket.Append(_UserPIN & "|") 
    pTicket.Append(_Roles & "|") 
    pTicket.Append(_UserIdentityType.FastToString() & "|") 
    pTicket.Append(_UserIdentityInstanceID.ToString() & "|") 
    pTicket.Append(_UILang.FastToString() & "|") 
    pTicket.Append(_CallingApplication & "|") 
    pTicket.Append(_CallingApplicationVersion & "|") 
    pTicket.Append(_CallingFunctionWithinApplication & "|") 
 
    Return ccHelper.Encrypt(ccHelper.enmEncryptionMethod.TripleDES, pTicket.ToString(), "Ticket") 
  End Function  
  
  ''' <summary> 
  ''' This keeps the same address, for use with ByVals   
  ''' </summary> 
  ''' <param name="vTicket"></param> 
  Friend Sub LoadTicket(ByVal vTicket As String) 'CreateFromTicket   
    Dim pFault As New clsFault  
  
    CreateEmpty()  
  
    If vTicket Is Nothing OrElse String.IsNullOrEmpty(vTicket) Then 
      Dim pMessage As String = "No ticket received" 
      pFault.LogFreeTextFault(86, pMessage, "", "TRGT-190810-2021", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2021") 
    End If 
 
    Dim pTicket As String = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.TripleDES, vTicket) 
    If pTicket = "!! Decryption Error !!" Then 
      Dim pMessage As String = "Invalid ticket" 
      pFault.LogFreeTextFault(86, pMessage, "", "TRGT-190810-2022", Nothing) 
      Throw New Exception(pMessage & ", TRGT-190810-2022") 
    End If 
 
    Dim pTickets As String() = pTicket.Split("|"c) 
 
    _UserID = ccHelper.ToLong(pTickets(0)) 
    _UserName = pTickets(1) 
    If pTickets(2) = "1" Then 
      _UserEnableSimultaneousLogins = True 
    Else 
      _UserEnableSimultaneousLogins = False 
    End If 
    _LoggedLoginID = ccHelper.ToLong(pTickets(3)) 
    _UserFullName = pTickets(4) 
    _UserPIN = pTickets(5) 
    _Roles = pTickets(6) 
    _UserIdentityType = clsEnums.TranslateEnmUserIdentityType(pTickets(7)) 
    _UserIdentityInstanceID = ccHelper.ToLong(pTickets(8)) 
    _UILang = clsEnums.TranslateEnmLanguage(pTickets(9)) 
    _CallingApplication = pTickets(10) 
    _CallingApplicationVersion = pTickets(11) 
    _CallingFunctionWithinApplication = pTickets(12) 
 
  End Sub 
 
  ''' <summary> 
  ''' Use this when there is no limit on the size of the ticket. It does not require accessing the database 
  ''' </summary> 
  ''' <param name="vTicket"></param> 
  Public Sub New(ByVal vTicket As String) 'CreateFromTicket  
    LoadTicket(vTicket)  
  End Sub 
 
  ''' <summary> 
  ''' Use this when the text must be as short as possible. It requires confirmation by accessing the database 
  ''' </summary> 
  ''' <param name="vLoginIDEncrypted"></param> 
  ''' <param name="vUserNameEncrypted"></param> 
  Public Sub New(vLoginIDEncrypted As String, vUserNameEncrypted As String) 
    Dim pFault As New clsFault 
 
    CreateEmpty() 
 
    Dim pWorkingRequester As New clsRequester("LoggedLogin", "View", True) 
 
    If vLoginIDEncrypted Is Nothing OrElse vUserNameEncrypted Is Nothing OrElse String.IsNullOrEmpty(vLoginIDEncrypted) OrElse String.IsNullOrEmpty(vUserNameEncrypted) Then 
      Dim pMessage As String = "No LoginID or UserName received" 
      pFault.LogFreeTextFault(86, pMessage, "", "TRGT-230519-1140", Nothing) 
      Throw New Exception(pFault.ShortStringForUser) 
    End If 
 
    Dim pLoggedLoginID As Long 
    Dim pUserName As String 
 
    Dim pStrg As String 
    pStrg = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.TripleDES, vLoginIDEncrypted) 
    If pStrg = "!! Decryption Error !!" Then 
      Dim pMessage As String = "Invalid LoginID" 
      pFault.LogFreeTextFault(86, pMessage, "", "TRGT-230519-1142", Nothing) 
      Throw New Exception(pFault.ShortStringForUser) 
    End If 
    If Not ccHelper.IsNumeric(pStrg) Then 
      Dim pMessage As String = "Invalid LoginID" 
      pFault.LogFreeTextFault(86, pMessage, "", "TRGT-230519-1146", Nothing) 
      Throw New Exception(pFault.ShortStringForUser) 
    End If 
    pLoggedLoginID = ccHelper.ToLong(pStrg) 
    Dim pFunctionParameters As String = $"LoggedLoginID: {pLoggedLoginID}" 
 
    pStrg = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.TripleDES, vUserNameEncrypted) 
    If pStrg = "!! Decryption Error !!" Then 
      Dim pMessage As String = "Invalid User Name" 
      pFault.LogFreeTextFault(86, pMessage, pFunctionParameters, "TRGT-230519-1147", Nothing) 
      Throw New Exception(pFault.ShortStringForUser) 
    End If 
    pUserName = pStrg 
    pFunctionParameters = $"LoggedLoginID: {pLoggedLoginID}, UserName: {pUserName}" 
 
    'get the Logged Login   
    Dim pLoggedLogin As New csLoggedLogin 
    pFault = pLoggedLogin.GetByID(pLoggedLoginID, pWorkingRequester, vMustExist:=True) 
    If pFault.isOK = False Then Throw New Exception(pFault.ShortStringForUser) 
 
    If pLoggedLogin.TimeLoggedOut <> DateTime.MinValue Then 
      pFault.LogFreeTextFault(86, $"TimeLoggedOut <> DateTime.MinValue {pLoggedLogin.TimeLoggedOut:s}", pFunctionParameters, "TRGT-230518-1145", pWorkingRequester) 
      Throw New Exception("pLoggedLoginID already closed ") 
    End If 
 
    If pLoggedLogin.TimeLoggedIn.Date <> DateTime.Now.Date Then 
      pFault.LogFreeTextFault(86, $"TimeLoggedIn.Date <> DateTime.Now.Date {pLoggedLogin.TimeLoggedIn:s}", pFunctionParameters, "TRGT-230518-1147", pWorkingRequester) 
      Throw New Exception("pLoggedLoginID Expired (> MinValue)") 
    End If 
 
    If Not pUserName.Equals(pLoggedLogin.UserName) Then 
      pFault.LogFreeTextFault(86, $"The user name for this Logged Login is {pLoggedLogin.UserName}", pFunctionParameters, "TRGT-230518-1134", pWorkingRequester) 
      Throw New Exception(pFault.ShortStringForUser) 
    End If 
 
    Dim pUser As New csUser 
    pFault = pUser.GetByUserName(pUserName, pWorkingRequester, vMustExist:=True) 
    If pFault.isOK = False Then Throw New Exception(pFault.ShortStringForUser) 
 
    _UserEnableSimultaneousLogins = pUser.EnableSimultaneousLogins 
 
    'Load the Requester   
    _LoggedLoginID = pLoggedLoginID 
    _UserName = pUserName 
    _UserFullName = pLoggedLogin.UserFullName 
    _UserPIN = pUser.PIN(vDecrypt:=True) 
    _UserIdentityInstanceID = pLoggedLogin.UserIdentityTypeNameCode 
    _UserIdentityType = clsEnums.TranslateEnmUserIdentityType(pLoggedLogin.UserIdentityTypeCode) 
    _Roles = pLoggedLogin.Roles 
    _CallingApplication = pLoggedLogin.ApplicationName 
    _UILang = pLoggedLogin.Language 
    _CallingApplicationVersion = pLoggedLogin.ApplicationVersion 
    _UserID = pUser.ID 
 
    If _UserEnableSimultaneousLogins = False Then 'check that there are no other for this Application that are open 
      Dim pLoggedLogins As New csLoggedLoginCol() 
      pFault = pLoggedLogins.FillByUserNameAndApplicationName(pUserName, _CallingApplication, pWorkingRequester, 15, clsEnums.enmFillDirection.DESC) 
      If pFault.isOK = False Then Throw New Exception(pFault.ShortStringForUser) 
 
      pLoggedLogins.SortByID() 
      pLoggedLogins.Reverse() 
 
      For Each l In pLoggedLogins 
        If l.ID = pLoggedLoginID Then Exit For 
        Throw New Exception($"Tried to use a login {pLoggedLoginID} after another one has already been assigned {l.ID}.") 
      Next 
 
    End If 
 
  End Sub 
 
  ''' <summary>  
  ''' Used for SecurityExemptTables. OperationType is View or Update. vInternal is a dummy variable to differentiate from another function with 2 string signature 
  ''' </summary>  
  ''' <param name="vSecurityExemptTable"></param>  
  ''' <param name="vOperationType"></param>  
  ''' <param name="vInternal"></param>  
  Public Sub New(ByVal vSecurityExemptTable As String, ByVal vOperationType As String, vInternal As Boolean) 
    Dim pFunctionParameters As String = "" 
 
    Dim pFault As New clsFault 
    CreateEmpty() 
 
    _UserID = 0 
    _UserName = "SecurityExempt" 
    _UserEnableSimultaneousLogins = True 
    _LoggedLoginID = 0 
    _UserFullName = "SecurityExempt No Requester" 
    _UserPIN = "" 
    _Roles = "#User~" 
    _UserIdentityType = clsEnums.enmUserIdentityType.Global 
    _UserIdentityInstanceID = 0 
    _UILang = clsEnums.enmLanguage.en 
    _CallingApplication = System.Reflection.Assembly.GetEntryAssembly?.GetName.Name 
    _CallingApplicationVersion = "n/a" 
    _CallingFunctionWithinApplication = "n/a" 
    
  End Sub 
 
  Friend Sub CreateEmpty() 
    _UserID = 0 
    _UserName = "" 
    _UserEnableSimultaneousLogins = False 
    _LoggedLoginID = 0 
 
    _UserFullName = "" 
    _UserPIN = "" 
    _Roles = "" 
    _UserIdentityType = clsEnums.enmUserIdentityType.UD 
    _UserIdentityInstanceID = 0 
 
    _UILang = clsEnums.enmLanguage.en 
    _CallingApplication = "" 
    _CallingApplicationVersion = "" 
    _CallingFunctionWithinApplication = "" 
  End Sub 
End Class 
