Imports System.Security.Cryptography 
Imports System.Threading 
 
Public Class ccSecurity 
  
  Private Shared _SecurityExemptViewTables As List(Of String) 
  Private Shared _SecurityExemptUpdateTables As List(Of String) 
 
  Private Shared Event evtAfterGetPermissionForExternal(ByVal vRequester As clsRequester, ByRef rCancel As Boolean, ByRef rFault As clsFault) 
  Private Shared Event evtAfterLogin(ByVal vRequester As clsRequester, ByRef rLoginFaultNumber As Integer, ByRef rFault As clsFault) 
 
  Private Shared Event evtLoadSecurityExemptions()
 
  Private Shared _IPCache As New Dictionary(Of String, String) 
 
  Public Shared ReadOnly Property RequiresLoginScreen() As Boolean 
    Get 
      If MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
          MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None Then 
        If MyController.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser Then 
          Return True 
        Else 
          Return False 
        End If 
      ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials Then 
        Return False 
      ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials Then 
        Return True 
      Else 
        Throw New Exception("Invalid Authentication model") 
      End If 
    End Get 
  End Property 
 
  Public Shared ReadOnly Property UserIdentificationModel() As clsEnums.enmUserIdentificationModel 
    Get 
      'If using WinF opposite DBController, can not log on with application credentials 
      Return clsEnums.enmUserIdentificationModel.ByApplicationUser 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' This assumes application in ApplicationCredentials mode or None mode, but user identity required.    
  ''' AccessingEntity must be provided is provided if DBController is used by TargCCOrders.WS.   
  ''' The Password is passed encrypted only if DBController is in a Web Service. It must not be 64 characters in length.  
  ''' The user's Language will be used as the UILanguage, unless overridden 
  ''' </summary> 
  ''' <param name="vUserName"></param> 
  ''' <param name="vPassword"></param> 
  ''' <param name="rRequester"></param> 
  ''' <param name="vOverrideUILang"></param> 
  ''' <param name="vSendMessageFor2FA"></param> 
  ''' <param name="vSendMessageOnPasswordExpiry"></param> 
  ''' <param name="vAccessingEntity"></param> 
  ''' <param name="vNewPassword"></param> 
  ''' <param name="vGuestSystem"></param> 
  ''' <returns></returns> 
  Public Shared Function LogInByNamePwd(ByVal vUserName As String, ByVal vPassword As String, ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vSendMessageFor2FA As Boolean = True, Optional ByVal vSendMessageOnPasswordExpiry As Boolean = True, Optional ByVal vAccessingEntity As csAccessingEntity = Nothing, Optional ByVal vNewPassword As String = "", Optional ByVal vGuestSystem As String = "") As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    'If vPassword = "" Then 
    '  rRequester = New clsRequester() 
    '  Return pFault.LogFreeTextFault(92, $"No password received for user {vUserName}", $"AccessingEntit: {vAccessingEntity.ToStringCC()}", "TRGT-251129-192350", rRequester) 
    'End If 
 
    vUserName = vUserName.Trim 
 
    If vAccessingEntity Is Nothing Then 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity={2}", vUserName, "***", "Nothing") 
    Else 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
    End If 
 
    Dim pEntryPoint As String = "ccSecurity_LogInByNamePwd" 
 
    'Load MyController 
    Try 
      Dim pDummy As String = MyController.ServerName 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.SetAlertMessage("Cannot create connection string" & Environment.NewLine & ex.Message, "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS) 
      'pFault.LogException(5, ex, pFunctionParameters, "TRGT-190401-1646", rRequester) 
      Return pFault 
    End Try 
 
    'In case of problems, to help figure it out, uncomment the lines below. 
    'Tools.LogToTextFile.WriteMessage($"DBController ccSecurity LogInByNamePwd", "AssemblyNames") 
    'Try : Tools.LogToTextFile.WriteMessage($"  01. My.Application.Info.AssemblyName: {My.Application.Info.AssemblyName}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  02. ccHelper.DoesAssemblyExist(pTestAssemblyName): {ccHelper.DoesAssemblyExist("Microsoft.VisualStudio.QualityTools.UnitTestFramework")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  03. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  04. ccHelper.DoesAssemblyEndWith('.WS'): {ccHelper.DoesAssemblyEndWith(".WS")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  05. ccHelper.DoesAssemblyEndWith('.WSDev'): {ccHelper.DoesAssemblyEndWith(".WSDev")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  06. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  07. System.Reflection.Assembly.GetEntryAssembly.GetName.Name: {System.Reflection.Assembly.GetEntryAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  08. System.Reflection.Assembly.GetExecutingAssembly().Location: {System.Reflection.Assembly.GetExecutingAssembly().Location}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  09. IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location): {IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  10. Environment.CurrentDirectory: {Environment.CurrentDirectory}", "AssemblyNames") : Catch ex As Exception : End Try 
 
    Dim pInWeb As Boolean = False 
    Dim pInWS As Boolean = False 
    Dim pInComObject As Boolean = False 
    'For Framework 
    If ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) OrElse 
       ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBStdController", StringComparison.OrdinalIgnoreCase) Then 'When used in a desktop application, then My.Application.Info refers to the host assembly  
      Dim pTestAssemblyName As String = "Microsoft.VisualStudio.QualityTools.UnitTestFramework" 
      If ccHelper.DoesAssemblyExist(pTestAssemblyName) = False Then 
        pInWeb = True 
        If System.Reflection.Assembly.GetCallingAssembly.GetName.Name.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WS") OrElse ccHelper.DoesAssemblyEndWith(".WSDev") OrElse ccHelper.DoesAssemblyEndWith(".WSStg") Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WebAPI") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIDev") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIStg") Then 
          pInWS = True 
        ElseIf System.Reflection.Assembly.GetCallingAssembly.GetName.Name.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) Then 
          'the must be a com object 
          pInWeb = False 
          pInWS = False 
          pInComObject = True 
        Else 
          'I assume I'm hosted by a Web App, but not *the* web service 
        End If 
      End If 
    Else 
      'For .Net Core 
      Dim pName As String = System.Reflection.Assembly.GetCallingAssembly.GetName.Name 
      If pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WebAPI", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      ElseIf pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      Else 'Test for Core 
        Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
        If IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
          pInWeb = True 
        ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
          'One of these:   
          'NLog.Web.AspNetCore   
          'Microsoft.AspNetCore.Server.IIS   
          'Microsoft.AspNetCore.WebUtilities   
          pInWeb = True 
        End If 
      End If 
    End If 
 
    Dim pUpdateFault As clsFault 
    rRequester = New clsRequester 
 
    Dim pBlankPassword As String = NETEncryption.clsHash.Hash("", NETEncryption.clsHash.HashName.SHA256) 
 
    If vPassword.Length <> 64 Then 
      vPassword = NETEncryption.clsHash.Hash(vPassword, NETEncryption.clsHash.HashName.SHA256) 
    Else 
      'allow the hashed password to pass only if it's hosted by WS 
      If pInWS = False Then 
        'Throw New Exception("Invalid password type for this host") 
        Return pFault.LogFreeTextFault("Invalid password type for this host", pFunctionParameters, "TRGT-160129-1537", rRequester) 
      End If 
    End If 
 
    If pInWeb = False Then 
      'I load the PCDetails 
        Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
        vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
      If pInComObject = True Then 
        vAccessingEntity.ApplicationName &= " via Com Object" 
      End If 
    Else 
      'expected AccessingEntity  
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    Dim pLoggedLogin As New csLoggedLogin 
 
    LogonInitiateData(vAccessingEntity, pLoggedLogin) 
 
    If vUserName.Trim.Length = 0 Then 
      pLoggedLogin.LoginFaultNumber = 87 'User name not provided 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
    pLoggedLogin.UserName = vUserName 
    pLoggedLogin.HostingAssembly = (New StackFrame(1)).GetMethod().DeclaringType.Namespace() 
    If pInWeb = False Then 
      pLoggedLogin.ClientReportedIP = vAccessingEntity.ClientReportedIP 
      pLoggedLogin.ClientReportedCountry = vAccessingEntity.ClientReportedCtry 
      pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails 
    End If 
 
    rRequester.LoadValuesInLogin(vUserName, 0, 0, "", "", clsEnums.enmUserIdentityType.UD, 0, vOverrideUILang, "", vAccessingEntity.ApplicationName, vAccessingEntity.ApplicationVersion) 
 
    rRequester.SetEntryFunction(pEntryPoint) 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, rRequester) 
    If pFault.isOK = False Then 
      pLoggedLogin.LoginFaultNumber = pFault.Number 
    End If 
 
    If pLoggedLogin.LoginFaultNumber > 0 Then 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'get the appropriate defaults 
    Dim pSystemDefaults As New csSystemDefaultCol 
    pFault = pSystemDefaults.Fill(rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
 
    Dim pSystemDefaultApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS = clsEnums.TranslateEnmApplicationAuthenticationToWS(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_ApplicationAuthenticationToWS).SettingValue) 
    Dim pSystemPasswordExpiryIntervalDays As Integer = ccHelper.ToInteger(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_PasswordExpiryIntervalDays).SettingValue) 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel).SettingValue) 
    Dim pSystemDefaultBlockNonmasterLogin As Boolean = CBool(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin).SettingValue) 
 
    'Check if appropriate method is being used  
    If Not ((pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
           pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
           pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Check if the password is OK 
    Dim pUser = New csUser 
    pFault = pUser.GetByUserName(vUserName, rRequester, False) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    If pUser.ID = 0 Then 
      pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Override text language to user, if UD was sent 
    If rRequester.UILang = clsEnums.enmLanguage.UD Then 
      vOverrideUILang = pUser.Language 
    End If 
    pLoggedLogin.Language = vOverrideUILang 
 
    'Refresh, for logging in case of error 
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pUser.FullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    If pUser.AuthenticationMethod <> clsEnums.enmAuthenticationMethod.OneTimePassword AndAlso vPassword.Equals(pBlankPassword, StringComparison.OrdinalIgnoreCase) Then 
      pLoggedLogin.LoginFaultNumber = 88 'Password not provided 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
 
    rRequester.SetUserEnableSimultaneousLogins(pUser.EnableSimultaneousLogins) 
 
    'reassign to ensure capitals are "pretty" 
    pLoggedLogin.UserName = pUser.UserName 
    pLoggedLogin.UserFullName = pUser.FirstName & " " & pUser.LastName 
 
    'Assign the identity  
    pLoggedLogin.UserIdentityTypeCode = pUser.Type.FastToString() 
    pLoggedLogin.UserIdentityTypeNameCode = CType(pUser.IDinType, Integer) 
 
    'reload for full details  
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'Check if disabled
    If pUser.IsDisabled = True Then 
      pLoggedLogin.LoginFaultNumber = 81 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
    'Check if locked out - Exempt AllowLoginBy2ndFactorOnly when not sending password
    If pUser.IsLockedOut = True AndAlso Not (pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.OneTimePassword AndAlso vPassword.Equals(pBlankPassword, StringComparison.OrdinalIgnoreCase)) Then 
      pLoggedLogin.LoginFaultNumber = 119 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Find the user's role
    Dim pRole As New csRole(pUser.RoleID, clsEnums.enmLoadParent.TextOnly, rRequester, pFault, True) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    Dim pRoleName As String = pRole.Name 
 
    'Now check who does the authentication  
    Dim pSystemDefaultAuthenticationHostRoot As String = "" 
    Dim pIsAuthenticationDoneOnExternalSystem As Boolean = False 
    If Not (pRoleName = "Master" OrElse pRoleName = "ApplicationMaster") Then 
      'if user is master, then password is checked locally automatically. Otherwise..... 
      pSystemDefaultAuthenticationHostRoot = pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Config_AuthenticationHostRoot).SettingValue 
      Dim pStrg As String = pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Config_IsAuthenticationDoneOnExternalSystem).SettingValue 
      pIsAuthenticationDoneOnExternalSystem = ccHelper.ToBoolean(pStrg) 
    End If 
 
    Dim pHostLoggedLoginID As Long = 0 
    Dim pHostFaultNo As Integer = 0 
    Dim pHostLoggedAlertID As Long = 0 
 
    Dim pIsPwdInvalid As Boolean = False 
    If Not String.IsNullOrEmpty(pSystemDefaultAuthenticationHostRoot) AndAlso pIsAuthenticationDoneOnExternalSystem Then 
      'Check Password at host 
      pFault = GetAuthenticationFromHost(vUserName, vPassword, vNewPassword, pSystemDefaultAuthenticationHostRoot, rRequester, pHostLoggedLoginID, pHostFaultNo, pHostLoggedAlertID) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      If pHostFaultNo > 0 AndAlso pHostFaultNo <> 92 Then 
        pLoggedLogin.LoginFaultNumber = pHostFaultNo 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, vFreeText:=$"Message sent by Host server. Message ID: {pHostLoggedAlertID}") 
      End If 
      If pHostFaultNo = 92 Then pIsPwdInvalid = True 
    Else 
      'Check Password locally 
      Dim pPasswordToTest As String = NETEncryption.clsHash.Hash(pUser.ID.ToString() & vPassword, NETEncryption.clsHash.HashName.SHA256) 
      If Not (pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.OneTimePassword AndAlso vPassword.Equals(pBlankPassword, StringComparison.OrdinalIgnoreCase) OrElse 
              pPasswordToTest.Equals(pUser.PasswordHashed, StringComparison.OrdinalIgnoreCase) OrElse 
              vPassword.Equals(pUser.PasswordHashed, StringComparison.OrdinalIgnoreCase)) Then 'Invalid password      
        pIsPwdInvalid = True 
      End If 
    End If 
 
    'Check Password  
    If pIsPwdInvalid = True Then 'Invalid password      
      'vPassword.Equals .... is used for the old format, where the ID is not used as the SALT 
      pLoggedLogin.LoginFaultNumber = 92 'Invalid User Password  
      'Get the StackFrame for later 
      Dim pStackFrame As String = (New StackFrame).GetMethod().Name 
      'Check how many times wrong password since last OK 
      'Get ID of last OK 
      Dim pLoggedLoginsForTest As New csLoggedLoginCol 
      pFault = pLoggedLoginsForTest.FillByUserName(vUserName, rRequester, 4, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      'check if we have 4 already with 92 
      Dim pRefusalCount As Integer = 0 
      For Each l In pLoggedLoginsForTest 
        If l.LoginFaultNumber = -1 Then Exit For 
        If l.LoginFaultNumber = 92 Then pRefusalCount += 1 
      Next 
      If pRefusalCount = 4 Then 
        'disable the user 
        pUser.IsLockedOut = True 
        pFault = pUser.Update(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
        pFault = pUser.UpdateComments(DateTime.Now.ToString("yyyyMMddTHHmm") & ": User temporarily locked out. Invalid password 5 times." & Environment.NewLine & pUser.Comments, rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      End If 
      Return UpdateWithFault(pStackFrame, pLoggedLogin, rRequester) 
    End If 
 
    'Create the roles 
    Dim pRoles As String = "#" 
    pRoles &= pRole.Name & "~" & pRole.ID.ToString().Trim & "#" & pRole.BaseRoleText & "~" & pRole.BaseRoleID.ToString().Trim & "#" 
    pLoggedLogin.Roles = pRoles 
 
    'Now check the computer 
    pLoggedLogin.LoginFaultNumber = -1 
 
    pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
 
    'reload for full details  
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 pUser.PIN(vDecrypt:=True), 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'check application permissions  
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      Dim pApps As String = "#" & pUser.Applications.Replace(ChrW(13), "").Replace(ChrW(10), "#") & "#" 
      Dim pAppName As String = "" 
      If vAccessingEntity.ApplicationName.EndsWith("dev", StringComparison.OrdinalIgnoreCase) OrElse vAccessingEntity.ApplicationName.EndsWith("stg", StringComparison.OrdinalIgnoreCase) Then 
        pAppName = vAccessingEntity.ApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3) 
      Else 
        pAppName = vAccessingEntity.ApplicationName 
      End If 
      If (pAppName.EndsWith(".DBController") OrElse pAppName.EndsWith(".DBStdController")) AndAlso Not String.IsNullOrEmpty(vGuestSystem) Then 
        pAppName = vGuestSystem 
        pLoggedLogin.ApplicationName = pAppName 
        pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
      End If 
      If pApps.IndexOf("#" & (pAppName).ToString() & "#", StringComparison.OrdinalIgnoreCase) < 0 Then 
        If pAppName.EndsWith("CC:ChangePassword", StringComparison.OrdinalIgnoreCase) AndAlso rRequester.CallingFunctionWithinApplication = "Initial Login" AndAlso rRequester.EntryFunction = "ccSecurity_LogInByNamePwd" Then 
          'allow it - since it's a change password from the web  
        Else 
          pLoggedLogin.LoginFaultNumber = 99 
          Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
        End If 
      End If 
    End If 
 
    If Math.Abs(DateTime.Now.ToUniversalTime.Subtract(vAccessingEntity.GmtTime).TotalMinutes) > 5 Then 
      pLoggedLogin.LoginFaultNumber = 106 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, $"Server time {DateTime.Now.ToUniversalTime:dd-MMM-yyyy HH:mm} UTC{Environment.NewLine}Client time {vAccessingEntity.GmtTime:dd-MMM-yyyy HH:mm} UTC") 
    End If 
 
    'Now check computer identification 
    If (pUser.RequiresComputerIdentification = True OrElse pUser.RequiresFixedIP = True) AndAlso String.IsNullOrEmpty(vNewPassword) Then 
      'Note that user owuld normally change password from another machine... 
      pFault = CheckHardwarePermission((New StackFrame).GetMethod().Name, pUser, vAccessingEntity, pLoggedLogin, rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    End If 
 
    'Check Expiry   
    'if not master  
    If String.IsNullOrEmpty(pSystemDefaultAuthenticationHostRoot) Then ' only if we are responsible for the password check. 
      If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse 
              pRoles.IndexOf("#ApplicationMaster~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse 
               (pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.OneTimePassword AndAlso vPassword.Equals(pBlankPassword, StringComparison.OrdinalIgnoreCase))) Then 
        If vNewPassword = "" Then 
          If pSystemPasswordExpiryIntervalDays > 0 AndAlso pUser.ExpiryDate <> Nothing AndAlso (DateTime.Now > pUser.ExpiryDate) Then 
            Dim pFreeText As String = "" 
            If pUser.DatePasswordChanged.Subtract(pUser.ExpiryDate).TotalMinutes < 1 Then 'ie, it was set to expire at the time of change!!!! 
              'ForceUserToChangePasswordOnInitialLogin 
              pLoggedLogin.LoginFaultNumber = 122 
            Else 
              pLoggedLogin.LoginFaultNumber = 121 
            End If 
            If pLoggedLogin.LoginFaultNumber > 0 Then pFreeText = "Change your password via the application" 
            If vSendMessageOnPasswordExpiry Then 
              'send message   
              If pLoggedLogin.LoginFaultNumber = 122 Then 
                pFault = pUser.SendPasswordChangeMessage(vIsExpired:=False, rRequester) 
              Else 
                pFault = pUser.SendPasswordChangeMessage(vIsExpired:=True, rRequester) 
              End If 
              If Not pFault.isOK Then 
                pFault.SetOK(rRequester) 
              End If 
              If pLoggedLogin.LoginFaultNumber > 0 Then 
                If pUser.MessagingMode = clsEnums.enmMessagingMode.SMS Then 
                  pFreeText = "You will receive an SMS with instructions" 
                Else 
                  pFreeText = "Please check your mail for instructions" 
                End If 
              End If 
            End If 
            Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, pFreeText) 
          End If 
        End If 
      End If 
    End If 
    'Check that we are not in maintenance   
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse 
            pRoles.IndexOf("#ApplicationMaster~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      If pSystemDefaultBlockNonmasterLogin = True Then 
        pLoggedLogin.LoginFaultNumber = 109 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    'now check anything else 
    Dim pLoginFaultNumber As Integer = -1 
    RaiseEvent evtAfterLogin(rRequester, pLoginFaultNumber, pFault) 
    If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
    If pLoginFaultNumber <> -1 Then 
      pLoggedLogin.LoginFaultNumber = pLoginFaultNumber 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Now write it to the SysUserStatus table 
    Dim pSysUserStatus As New csUserStatus() 
    pUpdateFault = pSysUserStatus.GetByUserIDAndApplicationName(pUser.ID, pLoggedLogin.ApplicationName, rRequester, False) 
    If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    If pSysUserStatus.ID = 0 Then 
      With pSysUserStatus 
        .UserID = pUser.ID 
        .ApplicationName = pLoggedLogin.ApplicationName 
      End With 
    End If 
    With pSysUserStatus 
      .UserID = pUser.ID 
      .LastLoggedLoginID = pLoggedLogin.ID 
      .LoginTime = pLoggedLogin.TimeLoggedIn 
      .LogoutTime = Nothing 
      .ApplicationName = pLoggedLogin.ApplicationName 
      pUpdateFault = .Update(rRequester, False) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    End With 
 
    'add it to the collection 
    AddUserToCacheSafe(pSysUserStatus) 
    
    'Change password 
    If vNewPassword <> "" Then 
      pFault = pUser.ChangePassword(vNewPassword, rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    End If 
 
    'Now check 2 factor authentication 
    If pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.UD Then 
      Return New clsFault(95, $"Authentication Method is not defined for {pUser.UserName}", pFunctionParameters, "TRGT-201010-0938", rRequester) 
      rRequester.RemoveLoginID() 
      Return pFault 
    End If 
 
    Dim pRequire2ndFactor As Boolean = False 
    If pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.TwoFactorAuthentication Then 
      pRequire2ndFactor = True 
    ElseIf pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.SingleVenue2FA Then 
      Dim pPrevIP As String = "" 
      Dim pPrevLoggedInDate As DateTime = DateTime.MinValue 
      pRequire2ndFactor = True 
      If Not String.IsNullOrEmpty(pUser.LoggedInIP) Then 
        Try 
          pPrevIP = pUser.LoggedInIP.Split(";"c)(0).Trim() & "; " & pUser.LoggedInIP.Split(";"c)(1).Trim() 
          Dim pSuccess As Boolean = DateTime.TryParseExact(pUser.LoggedInIP.Split(";"c)(2).Trim, "yyyyMMddTHHmmss", System.Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.AssumeLocal, pPrevLoggedInDate) 
          If pSuccess = True Then  
            If pPrevLoggedInDate.Date.Equals(DateTime.Now.Date) Then 
              'CheckIP  
              Dim pIP = pLoggedLogin.OriginatingIP & "; " & pLoggedLogin.AddressList.Split(","c)(0).Trim() 
              If pIP = pPrevIP Then 
                pRequire2ndFactor = False 
              End If 
            End If 
          End If 
        Catch ex As Exception 
        End Try 
      End If 
    ElseIf pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.OneTimePassword Then 
      If vPassword.Equals(pBlankPassword, StringComparison.OrdinalIgnoreCase) Then 
        pRequire2ndFactor = True 
      End If 
    End If 
 
    If pRequire2ndFactor = True Then 
      If Not vSendMessageFor2FA Then 
        'Get the CellOrEmail 
        Dim pCellOrEmail As String = "" 
        If pUser.MessagingMode = clsEnums.enmMessagingMode.SMS Then 
          pCellOrEmail = pUser.PhoneNumber 
        ElseIf pUser.MessagingMode = clsEnums.enmMessagingMode.Email Then 
          pCellOrEmail = pUser.Email 
        End If 
        'send the mfa 
        Dim pMessageMethod As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD 'for future use 
        pFault = csMFA.SetMFA(pUser.ID, pCellOrEmail, "LoginByNamePwd", "", rRequester, pMessageMethod, vUILang:=pUser.Language) : If Not pFault.isOK Then Return pFault 
 
        'now set the requester loginID to negative  
        rRequester.KillLoginID() 
      ElseIf String.IsNullOrEmpty(vNewPassword) Then 
        'Send the SMS Message 
        Dim pFunctionName As String = $"Login2305171915#login to {rRequester.CallingApplication}" 
        pFault = ccSecurity.RequireApproval(pFunctionName, enmApprovalMethod.ApproveViaWebLink, rRequester) : If Not pFault.isOK Then Return pFault 
        pFault = ccSecurity.CheckApproval(pFunctionName, rRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    End If 
 
    pFault = pUser.UpdateLastSuccessfulLogin(DateTimeOffset.Now, rRequester) : If Not pFault.isOK Then Return pFault 
 
    'Check ComboListFillManual 
    Dim pTest As New clsComboList() 'if there's an error below, find sample code and instructions at the end of SP 'c__ComboListFillAuto' 
    pFault = pTest.Fill(clsEnums.enmComboListType.ccTestComboListFillManual, rRequester) 
    If Not pFault.isOK Then 
      pFault.AddToUserMessage("Check the ComboListFillManual Stored Procedure") 
      Return pFault 
    End If 
 
    If pInWeb = True Then 
      'get country and details if we don't already have it  
      If String.IsNullOrEmpty(pLoggedLogin.IPAdditionalDetails) Then 
        'Calculate from Originating IP  
        If (String.IsNullOrEmpty(vAccessingEntity.WSReportedCountry) OrElse vAccessingEntity.WSReportedCountry.Equals("UD", StringComparison.OrdinalIgnoreCase)) Then 
          Dim pDetails As String = "" 
 
          If _IPCache.ContainsKey(vAccessingEntity.WSReportedIP) Then  
            pDetails = _IPCache(vAccessingEntity.WSReportedIP) 
            Tools.LogToTextFile.WriteMessage($"LogInByNamePwd: LoadDetailsForWSReportedIP: Got Cached details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
          Else 
            pFault = vAccessingEntity.LoadDetailsForWSReportedIP(rRequester) : If Not pFault.isOK Then Return pFault 
            Tools.LogToTextFile.WriteMessage($"LogInByNamePwd: LoadDetailsForWSReportedIP: Loaded details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
            _IPCache(vAccessingEntity.WSReportedIP) = pDetails  
          End If  
 
          If pDetails.IndexOf(",") > 0 Then 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails.Replace(",", ", ") 
          Else 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = "" 
          End If 
          pFault = pLoggedLogin.Update(rRequester) : If Not pFault.isOK Then Return pFault 
        End If 
      End If  
    End If 
 
    rRequester.CallingFunctionWithinApplication = "" 
    rRequester.SetEntryFunction("") 
    
    Return pFault 
    'We are now logged in 
  End Function 
  Private Shared Function GetAuthenticationFromHost(vName As String, vPassword As String, vNewPassword As String, vAuthenticationHostRoot As String, vRequester As clsRequester, ByRef rHostLoggedLoginID As Long, ByRef rHostFaultNo As Integer, ByRef rHostLoggedAlertID As Long) As clsFault 
    Dim pFunctionParameters As String = $"Name: {vName}, Password: {New String("*"c, vPassword.Length)}, AuthenticationHostRoot: {vAuthenticationHostRoot}" 
 
    Dim pFault As New clsFault() 
 
    rHostLoggedLoginID = 0 
    rHostFaultNo = 0 
    rHostLoggedAlertID = 0 
 
    Dim pDestinationURL As String = vAuthenticationHostRoot 
    If Not pDestinationURL.EndsWith("/") Then pDestinationURL &= "/" 
    pDestinationURL = $"{pDestinationURL}CC/ccAPI.aspx" 
 
    Dim pName As String = vName.Trim() 
    Dim pPassword As String = vPassword.Trim() 
    Dim pNewPassword As String = vNewPassword.Trim() 
 
    If pPassword.IndexOf(" ") >= 0 Then 
      Return pFault.LogFreeTextFault(92, "The password contains a space", pFunctionParameters, "TRGT-240307-164358", vRequester) 
    End If 
    If pNewPassword.IndexOf(" ") >= 0 Then 
      Return pFault.LogFreeTextFault(92, "The new password contains a space", pFunctionParameters, "TRGT-240308-110215", vRequester) 
    End If 
 
    'use space to delimit 
    Dim pTextToSend As String = "" 
    If String.IsNullOrEmpty(pNewPassword) Then 
      pTextToSend = $"{vName.Trim} {vPassword.Trim}" 
    Else 
      pTextToSend = $"{vName.Trim} {vPassword.Trim} {vNewPassword.Trim}" 
    End If 
 
    pTextToSend = NETEncryption.clsTripleDES.EncryptData(pTextToSend, "TargCCOrders") 
 
    ' Create a new WebClient instance.    
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("Task", "ExternalAuthentication") 
    pWebClient.QueryString.Add("Guest", "TargCCOrders") 
 
    pWebClient.Credentials = vRequester.Credential 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    'Upload the file to the URL using the HTTP 1.2 POST.    
    Dim pResponse As String 
    Try 
      pResponse = pWebClient.UploadString(pDestinationURL, "POST", pTextToSend) 
    Catch ex As Net.WebException 
      If ex.Status = Net.WebExceptionStatus.ConnectFailure Then 
        Return pFault.LogFreeTextFault(158, $"Could not connect to {pDestinationURL}: {ex.Message}", pFunctionParameters, "TRGT-240310-122629", vRequester) 
      Else 
        Return pFault.LogException(ex, pFunctionParameters, "TRGT-240307-164633", vRequester) 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-240307-164643", vRequester)  
    End Try 
 
    If String.IsNullOrWhiteSpace(pResponse) Then 
      Return pFault.LogFreeTextFault(158, $"Received no response from {pDestinationURL}", pFunctionParameters, "TRGT-240310-144820", vRequester) 
    End If 
 
    Try  
      Dim pResponses As String() = pResponse.Split(" "c)  
      If pResponses.Length = 2 Then 
        rHostFaultNo = ccHelper.ToInteger(pResponses(0)) 
        rHostLoggedAlertID = ccHelper.ToLong(pResponses(1))  
      ElseIf pResponses.Length = 1 Then  
        rHostLoggedLoginID = ccHelper.ToLong(pResponses(0))  
        rHostFaultNo = -1  
      End If  
    Catch ex As Exception 
      Return pFault.LogFreeTextFault(158, $"Received an unexpected response from the host: {pResponse}. Caused exception: {ex.Message}{Environment.NewLine}{Tools.LogToTextFile.GetExceptionString(ex)}", pFunctionParameters, "TRGT-240310-145243", vRequester) 
    End Try  
  
    Return pFault.SetOK()  
  End Function 
 
  ''' <summary>  
  ''' This assumes application in ApplicationCredentials mode or None mode, but user identity required.     
  ''' AccessingEntity must be provided is provided if DBController is used by TargCCOrders.WS.    
  ''' The username and email are used to identify the user, and a One Time Password is sent to the user.   
  ''' The user's Language will be used as the UILanguage, unless overridden  
  ''' </summary>  
  ''' <param name="vUserName"></param>  
  ''' <param name="vEmail"></param>  
  ''' <param name="rRequester"></param>  
  ''' <param name="vOverrideUILang"></param>  
  ''' <param name="vAccessingEntity"></param>  
  ''' <returns></returns>  
  Public Shared Function LogInByOTP(ByVal vUserName As String, ByVal vEmail As String, ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vAccessingEntity As csAccessingEntity = Nothing) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    vUserName = vUserName.Trim 
 
    If vAccessingEntity Is Nothing Then 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity={2}", vUserName, "***", "Nothing") 
    Else 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
    End If 
 
    Dim pEntryPoint As String = "ccSecurity_LogInByOTP" 
 
    'Load MyController  
    Try 
      Dim pDummy As String = MyController.ServerName 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.SetAlertMessage("Cannot create connection string" & Environment.NewLine & ex.Message, "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS) 
      Return pFault 
    End Try 
 
    'In case of problems, to help figure it out, uncomment the lines below.  
    'Tools.LogToTextFile.WriteMessage($"DBController ccSecurity LogInByNamePwd", "AssemblyNames")  
    'Try : Tools.LogToTextFile.WriteMessage($"  01. My.Application.Info.AssemblyName: {My.Application.Info.AssemblyName}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  02. ccHelper.DoesAssemblyExist(pTestAssemblyName): {ccHelper.DoesAssemblyExist("Microsoft.VisualStudio.QualityTools.UnitTestFramework")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  03. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  04. ccHelper.DoesAssemblyEndWith('.WS'): {ccHelper.DoesAssemblyEndWith(".WS")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  05. ccHelper.DoesAssemblyEndWith('.WSDev'): {ccHelper.DoesAssemblyEndWith(".WSDev")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  06. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  07. System.Reflection.Assembly.GetEntryAssembly.GetName.Name: {System.Reflection.Assembly.GetEntryAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  08. System.Reflection.Assembly.GetExecutingAssembly().Location: {System.Reflection.Assembly.GetExecutingAssembly().Location}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  09. IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location): {IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  10. Environment.CurrentDirectory: {Environment.CurrentDirectory}", "AssemblyNames") : Catch ex As Exception : End Try  
 
    Dim pInWeb As Boolean = False 
    Dim pInWS As Boolean = False 
    Dim pInComObject As Boolean = False 
    'For Framework  
    If ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) OrElse 
       ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBStdController", StringComparison.OrdinalIgnoreCase) Then 'When used in a desktop application, then My.Application.Info refers to the host assembly   
      Dim pTestAssemblyName As String = "Microsoft.VisualStudio.QualityTools.UnitTestFramework" 
      If ccHelper.DoesAssemblyExist(pTestAssemblyName) = False Then 
        pInWeb = True 
        If System.Reflection.Assembly.GetCallingAssembly.GetName.Name.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WS") OrElse ccHelper.DoesAssemblyEndWith(".WSDev") OrElse ccHelper.DoesAssemblyEndWith(".WSStg") Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WebAPI") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIDev") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIStg") Then 
          pInWS = True 
        ElseIf System.Reflection.Assembly.GetCallingAssembly.GetName.Name.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) Then 
          'the must be a com object  
          pInWeb = False 
          pInWS = False 
          pInComObject = True 
        Else 
          'I assume I'm hosted by a Web App, but not *the* web service  
        End If 
      End If 
    Else 
      'For .Net Core  
      Dim pName As String = System.Reflection.Assembly.GetCallingAssembly.GetName.Name 
      If pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WebAPI", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      ElseIf pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      Else 'Test for Core  
        Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
        If IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
          pInWeb = True 
        ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
          'One of these:    
          'NLog.Web.AspNetCore    
          'Microsoft.AspNetCore.Server.IIS    
          'Microsoft.AspNetCore.WebUtilities    
          pInWeb = True 
        End If 
      End If 
    End If 
 
    Dim pUpdateFault As clsFault 
    rRequester = New clsRequester 
 
    Dim pBlankPassword As String = NETEncryption.clsHash.Hash("", NETEncryption.clsHash.HashName.SHA256) 
 
    If pInWeb = False Then 
      'I load the PCDetails  
      Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
      vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
      If pInComObject = True Then 
        vAccessingEntity.ApplicationName &= " via Com Object" 
      End If 
    Else 
      'expected AccessingEntity   
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    Dim pLoggedLogin As New csLoggedLogin 
 
    LogonInitiateData(vAccessingEntity, pLoggedLogin) 
 
    If vUserName.Trim.Length = 0 Then 
      pLoggedLogin.LoginFaultNumber = 87 'User name not provided  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
    pLoggedLogin.UserName = vUserName 
    pLoggedLogin.HostingAssembly = (New StackFrame(1)).GetMethod().DeclaringType.Namespace() 
    If pInWeb = False Then 
      pLoggedLogin.ClientReportedIP = vAccessingEntity.ClientReportedIP 
      pLoggedLogin.ClientReportedCountry = vAccessingEntity.ClientReportedCtry 
      pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails 
    End If 
 
    rRequester.LoadValuesInLogin(vUserName, 0, 0, "", "", clsEnums.enmUserIdentityType.UD, 0, vOverrideUILang, "", vAccessingEntity.ApplicationName, vAccessingEntity.ApplicationVersion) 
 
    rRequester.SetEntryFunction(pEntryPoint) 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, rRequester) 
    If pFault.isOK = False Then 
      pLoggedLogin.LoginFaultNumber = pFault.Number 
    End If 
 
    If pLoggedLogin.LoginFaultNumber > 0 Then 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'get the appropriate defaults  
    Dim pSystemDefaults As New csSystemDefaultCol 
    pFault = pSystemDefaults.Fill(rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
 
    Dim pSystemDefaultApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS = clsEnums.TranslateEnmApplicationAuthenticationToWS(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_ApplicationAuthenticationToWS).SettingValue) 
    Dim pSystemPasswordExpiryIntervalDays As Integer = ccHelper.ToInteger(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_PasswordExpiryIntervalDays).SettingValue) 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel).SettingValue) 
    Dim pSystemDefaultBlockNonmasterLogin As Boolean = CBool(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin).SettingValue) 
 
    'Check if appropriate method is being used   
    If Not ((pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
           pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
           pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Check if the email is OK  
    Dim pUser = New csUser 
    pFault = pUser.GetByUserName(vUserName, rRequester, False) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    If pUser.ID = 0 Then 
      pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Override text language to user, if UD was sent  
    If rRequester.UILang = clsEnums.enmLanguage.UD Then 
      vOverrideUILang = pUser.Language 
    End If 
    pLoggedLogin.Language = vOverrideUILang 
 
    'Refresh, for logging in case of error  
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pUser.FullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    If pUser.AuthenticationMethod <> clsEnums.enmAuthenticationMethod.OneTimePassword Then 
      pLoggedLogin.LoginFaultNumber = 95 'Password not provided  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, vFreeText:="The user is not defined for 'OneTimePassword'") 
    End If 
 
    pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
 
    rRequester.SetUserEnableSimultaneousLogins(pUser.EnableSimultaneousLogins) 
 
    'reassign to ensure capitals are "pretty"  
    pLoggedLogin.UserName = pUser.UserName 
    pLoggedLogin.UserFullName = pUser.FirstName & " " & pUser.LastName 
 
    'Assign the identity   
    pLoggedLogin.UserIdentityTypeCode = pUser.Type.FastToString() 
    pLoggedLogin.UserIdentityTypeNameCode = CType(pUser.IDinType, Integer) 
 
    'reload for full details   
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'Check if disabled 
    If pUser.IsDisabled = True Then 
      pLoggedLogin.LoginFaultNumber = 81 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Find the user's role 
    Dim pRole As New csRole(pUser.RoleID, clsEnums.enmLoadParent.TextOnly, rRequester, pFault, True) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    Dim pRoleName As String = pRole.Name 
 
    'Now check who does the authentication   
    If (pRoleName = "Master" OrElse pRoleName = "ApplicationMaster") Then 
      pLoggedLogin.LoginFaultNumber = 95 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, vFreeText:="Master & Application Master cannot use OTP") 
    End If 
 
    Dim pIsEmailInvalid As Boolean = False 
    If pUser.Email <> vEmail Then 
      pIsEmailInvalid = True 
    End If 
 
    'Check Password   
    If pIsEmailInvalid = True Then 'Invalid email       
      'vPassword.Equals .... is used for the old format, where the ID is not used as the SALT  
      pLoggedLogin.LoginFaultNumber = 92 'Invalid User Password   
      'Get the StackFrame for later  
      Dim pStackFrame As String = (New StackFrame).GetMethod().Name 
      'Check how many times wrong password since last OK  
      'Get ID of last OK  
      Dim pLoggedLoginsForTest As New csLoggedLoginCol 
      pFault = pLoggedLoginsForTest.FillByUserName(vUserName, rRequester, 4, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      'check if we have 4 already with 92  
      Dim pRefusalCount As Integer = 0 
      For Each l In pLoggedLoginsForTest 
        If l.LoginFaultNumber = -1 Then Exit For 
        If l.LoginFaultNumber = 92 Then pRefusalCount += 1 
      Next 
      If pRefusalCount = 4 Then 
        'disable the user  
        pUser.IsLockedOut = True 
        pFault = pUser.Update(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
        pFault = pUser.UpdateComments(DateTime.Now.ToString("yyyyMMddTHHmm") & ": User temporarily locked out. Invalid password 5 times." & Environment.NewLine & pUser.Comments, rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      End If 
      Return UpdateWithFault(pStackFrame, pLoggedLogin, rRequester) 
    End If 
 
    'Create the roles  
    Dim pRoles As String = "#" 
    pRoles &= pRole.Name & "~" & pRole.ID.ToString().Trim & "#" & pRole.BaseRoleText & "~" & pRole.BaseRoleID.ToString().Trim & "#" 
    pLoggedLogin.Roles = pRoles 
 
    'Now check the computer  
    pLoggedLogin.LoginFaultNumber = -1 
 
    pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
 
    'reload for full details   
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 pUser.PIN(vDecrypt:=True), 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'check application permissions   
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      Dim pApps As String = "#" & pUser.Applications.Replace(ChrW(13), "").Replace(ChrW(10), "#") & "#" 
      Dim pAppName As String = "" 
      If vAccessingEntity.ApplicationName.EndsWith("dev", StringComparison.OrdinalIgnoreCase) OrElse vAccessingEntity.ApplicationName.EndsWith("stg", StringComparison.OrdinalIgnoreCase) Then 
        pAppName = vAccessingEntity.ApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3) 
      Else 
        pAppName = vAccessingEntity.ApplicationName 
      End If 
      If pApps.IndexOf("#" & (pAppName).ToString() & "#", StringComparison.OrdinalIgnoreCase) < 0 Then 
        If pAppName.EndsWith("CC:ChangePassword", StringComparison.OrdinalIgnoreCase) AndAlso rRequester.CallingFunctionWithinApplication = "Initial Login" AndAlso rRequester.EntryFunction = "ccSecurity_LogInByNamePwd" Then 
          'allow it - since it's a change password from the web   
        Else 
          pLoggedLogin.LoginFaultNumber = 99 
          Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
        End If 
      End If 
    End If 
 
    If Math.Abs(DateTime.Now.ToUniversalTime.Subtract(vAccessingEntity.GmtTime).TotalMinutes) > 5 Then 
      pLoggedLogin.LoginFaultNumber = 106 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, $"Server time {DateTime.Now.ToUniversalTime:dd-MMM-yyyy HH:mm} UTC{Environment.NewLine}Client time {vAccessingEntity.GmtTime:dd-MMM-yyyy HH:mm} UTC") 
    End If 
 
    'Now check computer identification  
    If (pUser.RequiresComputerIdentification = True OrElse pUser.RequiresFixedIP = True) Then 
      'Note that user owuld normally change password from another machine...  
      pFault = CheckHardwarePermission((New StackFrame).GetMethod().Name, pUser, vAccessingEntity, pLoggedLogin, rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    End If 
 
    'Check that we are not in maintenance    
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse 
            pRoles.IndexOf("#ApplicationMaster~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      If pSystemDefaultBlockNonmasterLogin = True Then 
        pLoggedLogin.LoginFaultNumber = 109 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    'now check anything else  
    Dim pLoginFaultNumber As Integer = -1 
    RaiseEvent evtAfterLogin(rRequester, pLoginFaultNumber, pFault) 
    If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
    If pLoginFaultNumber <> -1 Then 
      pLoggedLogin.LoginFaultNumber = pLoginFaultNumber 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Now write it to the SysUserStatus table  
    Dim pSysUserStatus As New csUserStatus() 
    pUpdateFault = pSysUserStatus.GetByUserIDAndApplicationName(pUser.ID, pLoggedLogin.ApplicationName, rRequester, False) 
    If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    If pSysUserStatus.ID = 0 Then 
      With pSysUserStatus 
        .UserID = pUser.ID 
        .ApplicationName = pLoggedLogin.ApplicationName 
      End With 
    End If 
    With pSysUserStatus 
      .UserID = pUser.ID 
      .LastLoggedLoginID = pLoggedLogin.ID 
      .LoginTime = pLoggedLogin.TimeLoggedIn 
      .LogoutTime = Nothing 
      .ApplicationName = pLoggedLogin.ApplicationName 
      pUpdateFault = .Update(rRequester, False) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    End With 
 
    'add it to the collection 
    AddUserToCacheSafe(pSysUserStatus) 
    
    'Now check 2 factor authentication  
    If pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.UD Then 
      Return New clsFault(95, $"Authentication Method is not defined for {pUser.UserName}", pFunctionParameters, "TRGT-201010-0938", rRequester) 
      rRequester.RemoveLoginID() 
      Return pFault 
    End If 
 
    'Now send the message 
    'Get the CellOrEmail  
    Dim pCellOrEmail As String = "" 
    If pUser.MessagingMode = clsEnums.enmMessagingMode.SMS Then 
      pCellOrEmail = pUser.PhoneNumber 
    ElseIf pUser.MessagingMode = clsEnums.enmMessagingMode.Email Then 
      pCellOrEmail = pUser.Email 
    End If 
    'send the mfa  
    Dim pMessageMethod As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD 'for future use 
    pFault = csMFA.SetMFA(pUser.ID, pCellOrEmail, "LoginByOTP", "", rRequester, pMessageMethod, vUILang:=pUser.Language) : If Not pFault.isOK Then Return pFault 
 
      'now set the requester loginID to negative   
      rRequester.KillLoginID() 
 
    pFault = pUser.UpdateLastSuccessfulLogin(DateTimeOffset.Now, rRequester) : If Not pFault.isOK Then Return pFault 
 
    'Check ComboListFillManual  
    Dim pTest As New clsComboList() 'if there's an error below, find sample code and instructions at the end of SP 'c__ComboListFillAuto'  
    pFault = pTest.Fill(clsEnums.enmComboListType.ccTestComboListFillManual, rRequester) 
    If Not pFault.isOK Then 
      pFault.AddToUserMessage("Check the ComboListFillManual Stored Procedure") 
      Return pFault 
    End If 
 
    If pInWeb = True Then 
      'get country and details if we don't already have it  
      If String.IsNullOrEmpty(pLoggedLogin.IPAdditionalDetails) Then 
        'Calculate from Originating IP  
        If (String.IsNullOrEmpty(vAccessingEntity.WSReportedCountry) OrElse vAccessingEntity.WSReportedCountry.Equals("UD", StringComparison.OrdinalIgnoreCase)) Then 
          Dim pDetails As String = "" 
 
          If _IPCache.ContainsKey(vAccessingEntity.WSReportedIP) Then  
            pDetails = _IPCache(vAccessingEntity.WSReportedIP) 
            Tools.LogToTextFile.WriteMessage($"LogInByOTP: LoadDetailsForWSReportedIP: Got Cached details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
          Else 
            pFault = vAccessingEntity.LoadDetailsForWSReportedIP(rRequester) : If Not pFault.isOK Then Return pFault 
            Tools.LogToTextFile.WriteMessage($"LogInByOTP: LoadDetailsForWSReportedIP: Loaded details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
            _IPCache(vAccessingEntity.WSReportedIP) = pDetails  
          End If  
 
          If pDetails.IndexOf(",") > 0 Then 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails.Replace(",", ", ") 
          Else 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = "" 
          End If 
          pFault = pLoggedLogin.Update(rRequester) : If Not pFault.isOK Then Return pFault 
        End If 
      End If  
    End If 
 
    rRequester.CallingFunctionWithinApplication = "" 
    rRequester.SetEntryFunction("") 
 
    Return pFault 
    'We are now logged in  
  End Function 
 
  ''' <summary> 
  ''' This assumes application in UserNamePwd or NetworkName mode    
  ''' AccessingEntity is provided if DBController is used by a web application other than WS  
  ''' If UserIdentificationModel is ByDomainUser, the User's Language will be used as the UILanguage, unless overridden.  
  ''' If UserIdentificationModel is ByDomainGroup, the English  will be used as the UILanguage, unless overridden. 
  ''' </summary> 
  ''' <param name="rRequester"></param> 
  ''' <param name="vOverrideUILang"></param> 
  ''' <param name="vNetworkCredentialUserName"></param> 
  ''' <param name="vNetworkCredentialRoles"></param> 
  ''' <param name="vAccessingEntity"></param> 
  ''' <returns></returns> 
  Public Shared Function LogInByNetworkCredentials(ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vNetworkCredentialUserName As String = "", Optional ByVal vNetworkCredentialRoles As String = "", Optional ByVal vAccessingEntity As csAccessingEntity = Nothing) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
    If vAccessingEntity Is Nothing Then 
      pFunctionParameters = "" 
    Else 
      pFunctionParameters = String.Format("AccessingEntity.ApplicationName={0}", vAccessingEntity.ApplicationName) 
    End If 
 
    Dim pEntryPoint As String = "ccSecurity_LogInByNetworkCredentials" 
 
    'Load MyController 
    Try 
      Dim pDummy As String = MyController.ServerName 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.SetAlertMessage("Cannot create connection string" & Environment.NewLine & ex.Message, "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS) 
      'pFault.LogException(5, ex, pFunctionParameters, "TRGT-190401-1647", rRequester) 
      Return pFault 
    End Try 
 
    Dim pUserName As String = "" 
 
    'In case of problems, to help figure it out, uncomment the lines below. 
    'Tools.LogToTextFile.WriteMessage($"DBController ccSecurity LogInByNetworkCredentials", "AssemblyNames") 
    'Try : Tools.LogToTextFile.WriteMessage($"  01. My.Application.Info.AssemblyName: {My.Application.Info.AssemblyName}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  02. ccHelper.DoesAssemblyExist(pTestAssemblyName): {ccHelper.DoesAssemblyExist("Microsoft.VisualStudio.QualityTools.UnitTestFramework")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  03. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  04. ccHelper.DoesAssemblyEndWith('.WS'): {ccHelper.DoesAssemblyEndWith(".WS")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  05. ccHelper.DoesAssemblyEndWith('.WSDev'): {ccHelper.DoesAssemblyEndWith(".WSDev")}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  06. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  07. System.Reflection.Assembly.GetEntryAssembly.GetName.Name: {System.Reflection.Assembly.GetEntryAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  08. System.Reflection.Assembly.GetExecutingAssembly().Location: {System.Reflection.Assembly.GetExecutingAssembly().Location}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  09. IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location): {IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}", "AssemblyNames") : Catch ex As Exception : End Try 
    'Try : Tools.LogToTextFile.WriteMessage($"  10. Environment.CurrentDirectory: {Environment.CurrentDirectory}", "AssemblyNames") : Catch ex As Exception : End Try 
 
    Dim pInWeb As Boolean = False 
    Dim pInWS As Boolean = False 
    Dim pInComObject As Boolean = False 
    'For Framework 
    If ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) OrElse 
       ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBStdController", StringComparison.OrdinalIgnoreCase) Then 'When used in a desktop application, then My.Application.Info refers to the host assembly  
      Dim pTestAssemblyName As String = "Microsoft.VisualStudio.QualityTools.UnitTestFramework" 
      If ccHelper.DoesAssemblyExist(pTestAssemblyName) = False Then 
        pInWeb = True 
        If System.Reflection.Assembly.GetCallingAssembly.GetName.Name.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WS") OrElse ccHelper.DoesAssemblyEndWith(".WSDev") OrElse ccHelper.DoesAssemblyEndWith(".WSStg") Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WebAPI") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIDev") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIStg") Then 
          pInWS = True 
        ElseIf System.Reflection.Assembly.GetCallingAssembly.GetName.Name.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) Then 
          'the must be a com object 
          pInWeb = False 
          pInWS = False 
          pInComObject = True 
        Else 
          'I assume I'm hosted by a Web App, but not *the* web service 
        End If 
      End If 
    Else 
      'For .Net Core 
      If System.Reflection.Assembly.GetCallingAssembly.GetName.Name.Replace("Dev", "").Replace("Stg", "").EndsWith(".WebAPI", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      End If 
    End If 
 
    Dim pUpdateFault As clsFault 
    Dim pUserOrGroup As csUser = Nothing 
    Dim pRoles As String = "" 
    rRequester = New clsRequester 
 
    If pInWeb = False Then 
      Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
      vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      pFunctionParameters = String.Format("AccessingEntity.ApplicationName={0}", vAccessingEntity.ApplicationName) 
      If pInComObject = True Then 
        vAccessingEntity.ApplicationName &= " via Com Object" 
      End If 
      pUserName = vAccessingEntity.EnvironmentUserName 
    Else 
      'expected AccessingEntity  
      If vAccessingEntity Is Nothing Then 
        If pInWS = True Then 
          Return pFault.LogFreeTextFault("If DBController serves TargCCOrders.WS, then it must get the Accessing Entity object.", pFunctionParameters, "TRGT-160129-1538", rRequester) 
        'Else 
        '  vAccessingEntity = New csAccessingEntity() 
        '  If vExternalIP = "" Then 
        '    Return pFault.LogFreeTextFault("If DBController is hosted in a web, then it must be provided the External IP during login.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
        '  End If 
        '  vAccessingEntity.OriginatingIP = vExternalIP 
        '  vAccessingEntity.OriginatingCountry = vExternalIP 
        End If 
      Else 'If vAccessingEntity Is Something  
        'accept the values 
      End If 
    End If 
    
    Dim pLoggedLogin As New csLoggedLogin 
 
    LogonInitiateData(vAccessingEntity, pLoggedLogin) 
 
    pLoggedLogin.Language = vOverrideUILang 
    pLoggedLogin.UserName = pUserName 
    rRequester.LoadValuesInLogin(pUserName, 0, 0, "", "", clsEnums.enmUserIdentityType.UD, 0, vOverrideUILang, "", vAccessingEntity.ApplicationName, vAccessingEntity.ApplicationVersion) 
 
    rRequester.SetEntryFunction(pEntryPoint) 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, rRequester) 
    If pFault.isOK = False Then 
      pLoggedLogin.LoginFaultNumber = pFault.Number 
    End If 
 
    If pLoggedLogin.LoginFaultNumber > 0 Then 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'get the appropriate defaults 
    Dim pSystemDefaults As New csSystemDefaultCol 
    pFault = pSystemDefaults.Fill(rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
 
    Dim pSystemDefaultApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS = clsEnums.TranslateEnmApplicationAuthenticationToWS(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_ApplicationAuthenticationToWS).SettingValue) 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel).SettingValue) 
    Dim pSystemDefaultBlockNonmasterLogin As Boolean = CBool(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin).SettingValue) 
 
    'Check if appropriate method is being used  
    If Not (pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials OrElse 
            pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials) Then 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
    If pInWeb = False Then 
      'can only use SpecificUserCredentials if coming from a web service 
      If pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials Then 
        pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    'Do this if we need the identity 
    If pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByDomainUser Then 
      pUserOrGroup = New csUser 
      pFault = pUserOrGroup.GetByUserName(pUserName, rRequester, False) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
      If pUserOrGroup.ID = 0 Then 
        pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      pLoggedLogin.UserFullName = pUserOrGroup.FirstName & " " & pUserOrGroup.LastName 
      If vOverrideUILang = clsEnums.enmLanguage.UD Then 
        vOverrideUILang = pUserOrGroup.Language 
      End If 
      'Refresh, for logging in case of error 
      rRequester.LoadValuesInLogin(pUserOrGroup.UserName, 
                                 pUserOrGroup.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "", 
                                 pUserOrGroup.Type, 
                                 pUserOrGroup.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
      'no need to check password 
      'Check if disabled 
      If pUserOrGroup.IsDisabled = True Then 
        pLoggedLogin.LoginFaultNumber = 81 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      pLoggedLogin.UserIdentityTypeCode = pUserOrGroup.Type.FastToString() 
      pLoggedLogin.UserIdentityTypeNameCode = CType(pUserOrGroup.IDinType, Integer) 
    ElseIf pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByDomainGroup Then 
      'get list of groups 
      If vOverrideUILang = clsEnums.enmLanguage.UD Then 
        vOverrideUILang = clsEnums.enmLanguage.en 
      End If 
      pUserOrGroup = Nothing 
      Dim pGroups = New csUserCol 
      pFault = pGroups.Fill(rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
      If pGroups.Count = 0 Then 
        pLoggedLogin.LoginFaultNumber = 110 'No user groups found 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      'now check the groups to see if we find one or more 
      Dim pNumFound As Integer = 0 
      For Each pTestGroup As csUser In pGroups 
        If pInWeb = True Then 
          If vNetworkCredentialRoles.IndexOf("#" & pTestGroup.UserName & "#", StringComparison.OrdinalIgnoreCase) >= 0 Then 
            pUserOrGroup = pTestGroup 
            pNumFound += 1 
          End If 
        Else 'Not in Web Service 
          If pTestGroup.UserName = Environment.UserName Then 
            'user names should not be one of the groups 
            'for some reason, when testing IsInRole and giving the present username, it returns true! 
            pLoggedLogin.LoginFaultNumber = 115 'A username in Users (app role) is also a machine username. Ambiguous 
            Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
          End If 
          If Threading.Thread.CurrentPrincipal.IsInRole(pTestGroup.UserName) = True Then 
            pUserOrGroup = pTestGroup 
            pNumFound += 1 
          End If 
        End If 
      Next 
      If pNumFound > 1 Then 
        pLoggedLogin.LoginFaultNumber = 111 'User is assigned to more than 1 valid AD role 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      If pNumFound = 0 Then 
        pLoggedLogin.LoginFaultNumber = 112 'The user does not belong to any valid AD groups 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      pLoggedLogin.UserFullName = pUserName & " " & pUserOrGroup.UserName 
      'Refresh, for logging in case of error 
      rRequester.LoadValuesInLogin(pUserOrGroup.UserName, 
                                 pUserOrGroup.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "", 
                                 pUserOrGroup.Type, 
                                 pUserOrGroup.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
      'no need to check password 
      'Check if disabled 
      If pUserOrGroup.IsDisabled = True Then 
        pLoggedLogin.LoginFaultNumber = 113 'Group disabled 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
      pLoggedLogin.UserIdentityTypeCode = pUserOrGroup.Type.FastToString() 
      pLoggedLogin.UserIdentityTypeNameCode = CType(pUserOrGroup.IDinType, Integer) 
    Else 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
 
    'Get the roles 
    pRoles = "#" 
    'Find the name of the main role 
    Dim pRole As New csRole(pUserOrGroup.RoleID, clsEnums.enmLoadParent.TextOnly, rRequester, pFault, True) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    'pLoggedLogin.LoginFaultNumber = 123  
    pRoles &= pRole.Name & "~" & pRole.ID.ToString().Trim & "#" & pRole.BaseRoleText & "~" & pRole.BaseRoleID.ToString().Trim & "#" 
    pLoggedLogin.Roles = pRoles 
    pLoggedLogin.Roles = pRoles 
 
    'Now check the computer 
    If pUserOrGroup.RequiresComputerIdentification = True Then 
      pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
      pFault = CheckHardwarePermission((New StackFrame).GetMethod().Name, pUserOrGroup, vAccessingEntity, pLoggedLogin, rRequester) 
      If pFault.isOK = False Then Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'check application permissions 
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      Dim pApps As String = "#" & pUserOrGroup.Applications.Replace(ChrW(13), "").Replace(ChrW(10), "#") & "#" 
      Dim pAppName As String = "" 
      If vAccessingEntity.ApplicationName.EndsWith("dev", StringComparison.OrdinalIgnoreCase) OrElse vAccessingEntity.ApplicationName.EndsWith("stg", StringComparison.OrdinalIgnoreCase) Then 
        pAppName = vAccessingEntity.ApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3) 
      Else 
        pAppName = vAccessingEntity.ApplicationName 
      End If 
      If pApps.IndexOf("#" & (pAppName).ToString() & "#", StringComparison.OrdinalIgnoreCase) < 0 Then 
        pLoggedLogin.LoginFaultNumber = 99 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    If Math.Abs(DateTime.Now.ToUniversalTime.Subtract(vAccessingEntity.GmtTime).TotalMinutes) > 5 Then 
      pLoggedLogin.LoginFaultNumber = 106 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, $"Server time {DateTime.Now.ToUniversalTime:dd-MMM-yyyy HH:mm} UTC{Environment.NewLine}Client time {vAccessingEntity.GmtTime:dd-MMM-yyyy HH:mm} UTC") 
    End If 
 
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse pRoles.IndexOf("#ApplicationMaster~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      'Check that we are not in maintenance 
      If pSystemDefaultBlockNonmasterLogin = True Then 
        pLoggedLogin.LoginFaultNumber = 109 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    pLoggedLogin.LoginFaultNumber = -1 
 
    pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
 
    'Refresh 
    rRequester.LoadValuesInLogin(pUserOrGroup.UserName, 
                                 pUserOrGroup.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 pUserOrGroup.PIN(vDecrypt:=True), 
                                 pUserOrGroup.Type, 
                                 pUserOrGroup.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'now check anything else 
    Dim pLoginFaultNumber As Integer = -1 
    RaiseEvent evtAfterLogin(rRequester, pLoginFaultNumber, pFault) 
    If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
    If pLoginFaultNumber <> -1 Then 
      pLoggedLogin.LoginFaultNumber = pLoginFaultNumber 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Now write it to the SysUserStatus table  
    Dim pSysUserStatus As New csUserStatus() 
    pUpdateFault = pSysUserStatus.GetByUserIDAndApplicationName(pUserOrGroup.ID, pLoggedLogin.ApplicationName, rRequester, False) 
    If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    If pSysUserStatus.ID = 0 Then 
      With pSysUserStatus 
        .UserID = pUserOrGroup.ID 
        .ApplicationName = pLoggedLogin.ApplicationName 
      End With 
    End If 
    With pSysUserStatus 
      .UserID = pUserOrGroup.ID 
      .LastLoggedLoginID = pLoggedLogin.ID 
      .LoginTime = pLoggedLogin.TimeLoggedIn 
      .LogoutTime = Nothing 
      .ApplicationName = pLoggedLogin.ApplicationName 
      pUpdateFault = .Update(rRequester, False) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    End With 
    
    'add it to the collection 
    AddUserToCacheSafe(pSysUserStatus) 
    
    pFault = pUserOrGroup.UpdateLastSuccessfulLogin(DateTimeOffset.Now, rRequester) : If Not pFault.isOK Then Return pFault 
 
    'Check ComboListFillManual 
    Dim pTest As New clsComboList() 'if there's an error below, find sample code and instructions at the end of SP 'c__ComboListFillAuto' 
    pFault = pTest.Fill(clsEnums.enmComboListType.ccTestComboListFillManual, rRequester) 
    If Not pFault.isOK Then 
      pFault.AddToUserMessage("Check the ComboListFillManual Stored Procedure") 
      Return pFault 
    End If 
 
    If pInWeb = True Then 
      'get country and details if we don't already have it  
      If String.IsNullOrEmpty(pLoggedLogin.IPAdditionalDetails) Then 
        'Calculate from Originating IP  
        If (String.IsNullOrEmpty(vAccessingEntity.WSReportedCountry) OrElse vAccessingEntity.WSReportedCountry.Equals("UD", StringComparison.OrdinalIgnoreCase)) Then 
          Dim pDetails As String = "" 
 
          If _IPCache.ContainsKey(vAccessingEntity.WSReportedIP) Then  
            pDetails = _IPCache(vAccessingEntity.WSReportedIP) 
            Tools.LogToTextFile.WriteMessage($"LogInByNetworkCredentials: LoadDetailsForWSReportedIP: Got Cached details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
          Else 
            pFault = vAccessingEntity.LoadDetailsForWSReportedIP(rRequester) : If Not pFault.isOK Then Return pFault 
            Tools.LogToTextFile.WriteMessage($"LogInByNetworkCredentials: LoadDetailsForWSReportedIP: Loaded details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
            _IPCache(vAccessingEntity.WSReportedIP) = pDetails  
          End If  
 
          If pDetails.IndexOf(",") > 0 Then 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails.Replace(",", ", ") 
          Else 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = "" 
          End If 
          pFault = pLoggedLogin.Update(rRequester) : If Not pFault.isOK Then Return pFault 
        End If 
      End If  
    End If 
 
    rRequester.CallingFunctionWithinApplication = "" 
    rRequester.SetEntryFunction("") 
    
    Return pFault 
    'We are now logged in 
  End Function 
  Public Shared Function LogInByBiometric(vApplicationIdentifier As String, vKey As String, ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vAccessingEntity As csAccessingEntity = Nothing) As clsFault 
    Dim pFunctionParameters As String = "" 
    Dim pFault As New clsFault 
 
    pFunctionParameters = $"vApplicationIdentifier={vApplicationIdentifier}" 
 
    Dim pEntryPoint As String = "ccSecurity_LogInByBiometric" 
 
    'Load MyController  
    Try 
      Dim pDummy As String = MyController.ServerName 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.SetAlertMessage("Cannot create connection string" & Environment.NewLine & ex.Message, "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS) 
      'pFault.LogException(5, ex, pFunctionParameters, "TRGT-190401-1646", rRequester)  
      Return pFault 
    End Try 
 
    'In case of problems, to help figure it out, uncomment the lines below.  
    'Tools.LogToTextFile.WriteMessage($"DBController ccSecurity LogInByNamePwd", "AssemblyNames")  
    'Try : Tools.LogToTextFile.WriteMessage($"  01. My.Application.Info.AssemblyName: {My.Application.Info.AssemblyName}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  02. ccHelper.DoesAssemblyExist(pTestAssemblyName): {ccHelper.DoesAssemblyExist("Microsoft.VisualStudio.QualityTools.UnitTestFramework")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  03. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  04. ccHelper.DoesAssemblyEndWith('.WS'): {ccHelper.DoesAssemblyEndWith(".WS")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  05. ccHelper.DoesAssemblyEndWith('.WSDev'): {ccHelper.DoesAssemblyEndWith(".WSDev")}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  06. System.Reflection.Assembly.GetCallingAssembly.GetName.Name: {System.Reflection.Assembly.GetCallingAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  07. System.Reflection.Assembly.GetEntryAssembly.GetName.Name: {System.Reflection.Assembly.GetEntryAssembly.GetName.Name}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  08. System.Reflection.Assembly.GetExecutingAssembly().Location: {System.Reflection.Assembly.GetExecutingAssembly().Location}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  09. IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location): {IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}", "AssemblyNames") : Catch ex As Exception : End Try  
    'Try : Tools.LogToTextFile.WriteMessage($"  10. Environment.CurrentDirectory: {Environment.CurrentDirectory}", "AssemblyNames") : Catch ex As Exception : End Try  
 
    Dim pInWeb As Boolean = False 
    Dim pInWS As Boolean = False 
    Dim pInComObject As Boolean = False 
    'For Framework  
    If ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) OrElse 
       ccHelper.GetEntryAssemblyDetails.AssemblyName.EndsWith(".DBStdController", StringComparison.OrdinalIgnoreCase) Then 'When used in a desktop application, then My.Application.Info refers to the host assembly   
      Dim pTestAssemblyName As String = "Microsoft.VisualStudio.QualityTools.UnitTestFramework" 
      If ccHelper.DoesAssemblyExist(pTestAssemblyName) = False Then 
        pInWeb = True 
        If System.Reflection.Assembly.GetCallingAssembly.GetName.Name.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WS") OrElse ccHelper.DoesAssemblyEndWith(".WSDev") OrElse ccHelper.DoesAssemblyEndWith(".WSStg") Then 
          pInWS = True 
        ElseIf ccHelper.DoesAssemblyEndWith(".WebAPI") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIDev") OrElse ccHelper.DoesAssemblyEndWith(".WebAPIStg") Then 
          pInWS = True 
        ElseIf System.Reflection.Assembly.GetCallingAssembly.GetName.Name.EndsWith(".DBController", StringComparison.OrdinalIgnoreCase) Then 
          'the must be a com object  
          pInWeb = False 
          pInWS = False 
          pInComObject = True 
        Else 
          'I assume I'm hosted by a Web App, but not *the* web service  
        End If 
      End If 
    Else 
      'For .Net Core  
      Dim pName As String = System.Reflection.Assembly.GetCallingAssembly.GetName.Name 
      If pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WebAPI", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      ElseIf pName.Replace("Dev", "").Replace("Stg", "").EndsWith(".WS", StringComparison.OrdinalIgnoreCase) Then 
        pInWeb = True 
        pInWS = True 
      Else 'Test for Core  
        Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
        If IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
          pInWeb = True 
        ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
          'One of these:    
          'NLog.Web.AspNetCore    
          'Microsoft.AspNetCore.Server.IIS    
          'Microsoft.AspNetCore.WebUtilities    
          pInWeb = True 
        End If 
      End If 
    End If 
 
    Dim pUpdateFault As clsFault 
    rRequester = New clsRequester 
 
    If pInWeb = False Then 
      'I load the PCDetails  
      Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
      vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      pFunctionParameters = String.Format("vApplicationIdentifier={0},AccessingEntity.ApplicationName={1}", vApplicationIdentifier, vAccessingEntity.ApplicationName) 
      If pInComObject = True Then 
        vAccessingEntity.ApplicationName &= " via Com Object" 
      End If 
    Else 
      'expected AccessingEntity   
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    Dim pLoggedLogin As New csLoggedLogin 
 
    LogonInitiateData(vAccessingEntity, pLoggedLogin) 
 
    If vApplicationIdentifier.Trim.Length = 0 Then 
      pLoggedLogin.LoginFaultNumber = 87 'User name not provided  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
    pLoggedLogin.UserName = "" 
    pLoggedLogin.HostingAssembly = (New StackFrame(1)).GetMethod().DeclaringType.Namespace() 
    If pInWeb = False Then 
      pLoggedLogin.ClientReportedIP = vAccessingEntity.ClientReportedIP 
      pLoggedLogin.ClientReportedCountry = vAccessingEntity.ClientReportedCtry 
      pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails 
    End If 
 
    rRequester.LoadValuesInLogin("", 0, 0, "", "", clsEnums.enmUserIdentityType.UD, 0, vOverrideUILang, "", vAccessingEntity.ApplicationName, vAccessingEntity.ApplicationVersion) 
 
    rRequester.SetEntryFunction(pEntryPoint) 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, rRequester) 
    If pFault.isOK = False Then 
      pLoggedLogin.LoginFaultNumber = pFault.Number 
    End If 
 
    If pLoggedLogin.LoginFaultNumber > 0 Then 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'get the appropriate defaults  
    Dim pSystemDefaults As New csSystemDefaultCol 
    pFault = pSystemDefaults.Fill(rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
 
    Dim pSystemDefaultApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS = clsEnums.TranslateEnmApplicationAuthenticationToWS(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_ApplicationAuthenticationToWS).SettingValue) 
    Dim pSystemPasswordExpiryIntervalDays As Integer = ccHelper.ToInteger(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_PasswordExpiryIntervalDays).SettingValue) 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel).SettingValue) 
    Dim pSystemDefaultBlockNonmasterLogin As Boolean = CBool(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.RealTime_BlockNonmasterLogin).SettingValue) 
 
    'Check if appropriate method is being used   
    If Not ((pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
           pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
           pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Check if the password is OK  
    'Try to get the row 
    Dim pUserLoginKey As New csUserLoginKey() 
    pFault = pUserLoginKey.GetByApplicationNameAndApplicationIdentifier(vAccessingEntity.ApplicationName, vApplicationIdentifier, rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    If pUserLoginKey.IsEmpty Then 
      pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found   
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    Dim pUser = New csUser 
    pFault = pUser.GetByID(pUserLoginKey.UserID, rRequester, False) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    If pUser.ID = 0 Then 
      pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Override text language to user, if UD was sent  
    If rRequester.UILang = clsEnums.enmLanguage.UD Then 
      vOverrideUILang = pUser.Language 
    End If 
    pLoggedLogin.Language = vOverrideUILang 
 
    'Refresh, for logging in case of error  
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pUser.FullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
 
    rRequester.SetUserEnableSimultaneousLogins(pUser.EnableSimultaneousLogins) 
 
    'reassign to ensure capitals are "pretty"  
    pLoggedLogin.UserName = pUser.UserName 
    pLoggedLogin.UserFullName = pUser.FirstName & " " & pUser.LastName 
 
    'Assign the identity   
    pLoggedLogin.UserIdentityTypeCode = pUser.Type.FastToString() 
    pLoggedLogin.UserIdentityTypeNameCode = CType(pUser.IDinType, Integer) 
 
    'reload for full details   
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "", 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'Check if disabled 
    If pUser.IsDisabled = True Then 
      pLoggedLogin.LoginFaultNumber = 81 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Check the key 
    Dim pIncomingHashedPassword = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, vKey) 
    If Not pUserLoginKey.KeyHashed.Equals(pIncomingHashedPassword) Then 
      pLoggedLogin.LoginFaultNumber = 92 'Invalid User Password   
      'Get the StackFrame for later  
      Dim pStackFrame As String = (New StackFrame).GetMethod().Name 
      'Check how many times wrong password since last OK  
      'Get ID of last OK  
      Dim pLoggedLoginsForTest As New csLoggedLoginCol 
      pFault = pLoggedLoginsForTest.FillByUserName(pUser.UserName, rRequester, 4, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      'check if we have 4 already with 92  
      Dim pRefusalCount As Integer = 0 
      For Each l In pLoggedLoginsForTest 
        If l.LoginFaultNumber = -1 Then Exit For 
        If l.LoginFaultNumber = 92 Then pRefusalCount += 1 
      Next 
      If pRefusalCount = 4 Then 
        'disable the user  
        pUser.IsLockedOut = True 
        pFault = pUser.Update(rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
        pFault = pUser.UpdateComments(DateTime.Now.ToString("yyyyMMddTHHmm") & ": User temporarily locked out. Invalid password 5 times." & Environment.NewLine & pUser.Comments, rRequester) : If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
      End If 
      Return UpdateWithFault(pStackFrame, pLoggedLogin, rRequester) 
    End If 
 
    'Find the user's role 
    Dim pRole As New csRole(pUser.RoleID, clsEnums.enmLoadParent.TextOnly, rRequester, pFault, True) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    Dim pRoleName As String = pRole.Name 
 
    'Create the roles  
    Dim pRoles As String = "#" 
    pRoles &= pRole.Name & "~" & pRole.ID.ToString().Trim & "#" & pRole.BaseRoleText & "~" & pRole.BaseRoleID.ToString().Trim & "#" 
    pLoggedLogin.Roles = pRoles 
 
    'Now check the computer  
    pLoggedLogin.LoginFaultNumber = -1 
 
    pUpdateFault = pLoggedLogin.Update(rRequester, True) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
 
    'Now update the UserLoginKey 
    pUserLoginKey.LastAccessTime = DateTime.Now 
    pUserLoginKey.LoggedLoginID = pLoggedLogin.ID 
    pUpdateFault = pUserLoginKey.Update(rRequester, False) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
 
    'reload for full details   
    rRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 pUser.PIN(vDecrypt:=True), 
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 vOverrideUILang, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'check application permissions   
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      Dim pApps As String = "#" & pUser.Applications.Replace(ChrW(13), "").Replace(ChrW(10), "#") & "#" 
      Dim pAppName As String = "" 
      If vAccessingEntity.ApplicationName.EndsWith("dev", StringComparison.OrdinalIgnoreCase) OrElse vAccessingEntity.ApplicationName.EndsWith("stg", StringComparison.OrdinalIgnoreCase) Then 
        pAppName = vAccessingEntity.ApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3) 
      Else 
        pAppName = vAccessingEntity.ApplicationName 
      End If 
      If pApps.IndexOf("#" & (pAppName).ToString() & "#", StringComparison.OrdinalIgnoreCase) < 0 Then 
        pLoggedLogin.LoginFaultNumber = 99 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    If Math.Abs(DateTime.Now.ToUniversalTime.Subtract(vAccessingEntity.GmtTime).TotalMinutes) > 5 Then 
      pLoggedLogin.LoginFaultNumber = 106 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester, $"Server time {DateTime.Now.ToUniversalTime:dd-MMM-yyyy HH:mm} UTC{Environment.NewLine}Client time {vAccessingEntity.GmtTime:dd-MMM-yyyy HH:mm} UTC") 
    End If 
 
    'Now check computer identification  
    If (pUser.RequiresComputerIdentification = True OrElse pUser.RequiresFixedIP = True) Then 
      'Note that user owuld normally change password from another machine...  
      pFault = CheckHardwarePermission((New StackFrame).GetMethod().Name, pUser, vAccessingEntity, pLoggedLogin, rRequester) : If pFault.isOK = False Then rRequester.RemoveLoginID() : Return pFault 
    End If 
 
    'Check that we are not in maintenance    
    If Not (pRoles.IndexOf("#Master~", StringComparison.OrdinalIgnoreCase) >= 0 OrElse 
            pRoles.IndexOf("#ApplicationMaster~", StringComparison.OrdinalIgnoreCase) >= 0) Then 
      If pSystemDefaultBlockNonmasterLogin = True Then 
        pLoggedLogin.LoginFaultNumber = 109 
        Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
      End If 
    End If 
 
    'now check anything else  
    Dim pLoginFaultNumber As Integer = -1 
    RaiseEvent evtAfterLogin(rRequester, pLoginFaultNumber, pFault) 
    If Not pFault.isOK Then rRequester.RemoveLoginID() : Return pFault 
    If pLoginFaultNumber <> -1 Then 
      pLoggedLogin.LoginFaultNumber = pLoginFaultNumber 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, rRequester) 
    End If 
 
    'Now write it to the SysUserStatus table  
    Dim pSysUserStatus As New csUserStatus() 
    pUpdateFault = pSysUserStatus.GetByUserIDAndApplicationName(pUser.ID, pLoggedLogin.ApplicationName, rRequester, False) 
    If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    If pSysUserStatus.ID = 0 Then 
      With pSysUserStatus 
        .UserID = pUser.ID 
        .ApplicationName = pLoggedLogin.ApplicationName 
      End With 
    End If 
    With pSysUserStatus 
      .UserID = pUser.ID 
      .LastLoggedLoginID = pLoggedLogin.ID 
      .LoginTime = pLoggedLogin.TimeLoggedIn 
      .LogoutTime = Nothing 
      .ApplicationName = pLoggedLogin.ApplicationName 
      pUpdateFault = .Update(rRequester, False) : If pUpdateFault.isOK = False Then rRequester.RemoveLoginID() : Return pUpdateFault 
    End With 
 
    'add it to the collection 
    AddUserToCacheSafe(pSysUserStatus) 
    
    pFault = pUser.UpdateLastSuccessfulLogin(DateTimeOffset.Now, rRequester) : If Not pFault.isOK Then Return pFault 
 
    'Check ComboListFillManual  
    Dim pTest As New clsComboList() 'if there's an error below, find sample code and instructions at the end of SP 'c__ComboListFillAuto'  
    pFault = pTest.Fill(clsEnums.enmComboListType.ccTestComboListFillManual, rRequester) 
    If Not pFault.isOK Then 
      pFault.AddToUserMessage("Check the ComboListFillManual Stored Procedure") 
      Return pFault 
    End If 
 
    If pInWeb = True Then 
      'get country and details if we don't already have it  
      If String.IsNullOrEmpty(pLoggedLogin.IPAdditionalDetails) Then 
        'Calculate from Originating IP  
        If (String.IsNullOrEmpty(vAccessingEntity.WSReportedCountry) OrElse vAccessingEntity.WSReportedCountry.Equals("UD", StringComparison.OrdinalIgnoreCase)) Then 
          Dim pDetails As String = "" 
 
          If _IPCache.ContainsKey(vAccessingEntity.WSReportedIP) Then  
            pDetails = _IPCache(vAccessingEntity.WSReportedIP) 
            Tools.LogToTextFile.WriteMessage($"LogInByBiometric: LoadDetailsForWSReportedIP: Got Cached details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
          Else 
            pFault = vAccessingEntity.LoadDetailsForWSReportedIP(rRequester) : If Not pFault.isOK Then Return pFault 
            Tools.LogToTextFile.WriteMessage($"LogInByBiometric: LoadDetailsForWSReportedIP: Loaded details for IP {vAccessingEntity.WSReportedIP}", "IPReport") 
            _IPCache(vAccessingEntity.WSReportedIP) = pDetails  
          End If  
 
          If pDetails.IndexOf(",") > 0 Then 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails.Replace(",", ", ") 
          Else 
            If String.IsNullOrEmpty(pLoggedLogin.ClientReportedCountry) Then pLoggedLogin.OriginatingCountry = vAccessingEntity.ClientReportedCtry 
            pLoggedLogin.IPAdditionalDetails = "" 
          End If 
          pFault = pLoggedLogin.Update(rRequester) : If Not pFault.isOK Then Return pFault 
        End If 
      End If  
    End If 
 
    rRequester.CallingFunctionWithinApplication = "" 
    rRequester.SetEntryFunction("") 
 
    Return pFault 
    'We are now logged in  
  End Function 
 
 
  Private Shared Sub LogonInitiateData(ByVal vAccessingEntity As csAccessingEntity, ByRef rLoggedLogin As csLoggedLogin) 
 
    rLoggedLogin = New csLoggedLogin 
 
    rLoggedLogin.TimeLoggedIn = DateTime.Now 
    rLoggedLogin.ApplicationName = vAccessingEntity.ApplicationName 
    If vAccessingEntity Is Nothing Then 
      'Invalid data received by method 
      rLoggedLogin.LoginFaultNumber = 86 
    End If 
 
    rLoggedLogin.EnvironmentUserName = vAccessingEntity.EnvironmentUserName 
    rLoggedLogin.EnvironmentMachineName = vAccessingEntity.EnvironmentMachineName 
    rLoggedLogin.EnvironmentUserDomainName = vAccessingEntity.EnvironmentUserDomainName 
    rLoggedLogin.DnsGetHostName = vAccessingEntity.DnsGetHostName 
    rLoggedLogin.AddressList = vAccessingEntity.AddressList 
    rLoggedLogin.ComputerMACAddress = vAccessingEntity.ComputerMACAddress 
    rLoggedLogin.SystemDiskVolumeSerialNo = vAccessingEntity.SystemDiskVolumeSerialNo 
    rLoggedLogin.LocalTime = vAccessingEntity.LocalTime 
    rLoggedLogin.GmtTime = vAccessingEntity.GmtTime 
    rLoggedLogin.AccessingComputerDetails = vAccessingEntity.AccessingComputerDetails 
    rLoggedLogin.UICulture = vAccessingEntity.UICulture 
    rLoggedLogin.TotalPhysicalMemoryKb = vAccessingEntity.TotalPhysicalMemory 
    rLoggedLogin.AvailablePhysicalMemoryKb = vAccessingEntity.AvailablePhysicalMemory 
    rLoggedLogin.ApplicationVersion = vAccessingEntity.ApplicationVersion 
    rLoggedLogin.OriginatingIP = vAccessingEntity.WSReportedIP 
    rLoggedLogin.OriginatingCountry = vAccessingEntity.WSReportedCountry 
    rLoggedLogin.ClientReportedIP = vAccessingEntity.ClientReportedIP 
    rLoggedLogin.ClientReportedCountry = vAccessingEntity.ClientReportedCtry 
    rLoggedLogin.IPAdditionalDetails = vAccessingEntity.ClientReportedDetails 
 
  End Sub 
 
  Private Shared Function UpdateWithFault(ByVal vCallingFunction As String, ByRef rLoggedLogin As csLoggedLogin, ByVal rRequester As clsRequester, Optional vFreeText As String = "") As clsFault 
    Dim pUpdateFault As clsFault 
    Dim pFault As New clsFault 
    If rRequester.LoggedLoginID > 0 Then 
      pUpdateFault = rLoggedLogin.Update(rRequester, True) 
    Else 
      pUpdateFault = New clsFault().SetOK() 
    End If 
    If pUpdateFault.isOK Then 
      If rRequester.LoggedLoginID = 0 Then 
        rRequester.LoadValuesInLogin(rRequester.UserName, rRequester.UserID, rLoggedLogin.ID, rRequester.UserFullName, "", rRequester.UserIdentityType, rRequester.UserIdentityInstanceID, rRequester.UILang, rRequester.Roles, rRequester.CallingApplication, rRequester.CallingApplicationVersion) 
      End If 
      Dim pMsg As String = "" 
      If rLoggedLogin.LoginFaultNumber = 91 OrElse rLoggedLogin.LoginFaultNumber = 92 Then 
        pMsg = String.Format("UserName={0}; LoggedLogin.ID={1}; IP={2}", rRequester.UserName, rLoggedLogin.ID, $"{rLoggedLogin.OriginatingIP} {rLoggedLogin.OriginatingCountry}") 
      Else 
        pMsg = String.Format("UserName={0}; LoggedLogin.ID={1}", rRequester.UserName, rLoggedLogin.ID) 
      End If 
      pFault.LogFreeTextFault(rLoggedLogin.LoginFaultNumber, vFreeText, pMsg, "TRGT-HCC-0023", rRequester, vManualFaultingFunction:=vCallingFunction, vAdditionalMessageToUser:=vFreeText) 
      If rRequester.LoggedLoginID > 0 Then rRequester.RemoveLoginID() 
      Return pFault 
    Else 
      Return pUpdateFault  
    End If  
  End Function 
 
  Public Shared Function Check2FactorAuthenticationForLogin(ByVal vCode As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}, LoggedLoginID={1}", vRequester.UserName, vRequester.LoggedLoginID) 
    Dim pFault As New clsFault : pFault.SetOK() 
 
    Dim pEntryPoint As String = "ccSecurity_Check2FactorAuthenticationForLogin" 
    vRequester.SetEntryFunction(pEntryPoint) 
 
    'Get the loggedLogin  
    Dim pLoggedLoginID As Long = vRequester.LoggedLoginID 
 
    If pLoggedLoginID > -9 Then 
      pFault.LogFreeTextFault(143, "pLoggedLoginID > -9", pFunctionParameters, "TRGT-180829-1714", vRequester) 
      vRequester.RemoveLoginID() 
      Return pFault 
    End If 
 
    'Now switch the logged login id  
    vRequester.ReviveLoginID() 
 
    pLoggedLoginID = vRequester.LoggedLoginID 
 
    'Reassign   
    pFunctionParameters = String.Format("User={0}, LoggedLoginID={1}", vRequester.UserName, vRequester.LoggedLoginID) 
 
    'Not needed..... 
    'pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, vRequester) 
    'If pFault.isOK = False Then Return pFault 
 
 
    'Get the logged login  
    Dim pLoggedLogin As New csLoggedLogin 
    pFault = pLoggedLogin.GetByID(vRequester.LoggedLoginID, vRequester, True) : If pFault.isOK = False Then Return pFault 
 
    'Check if valid login  
    If pLoggedLogin.TimeLoggedOut <> Date.MinValue OrElse pLoggedLogin.LoginFaultNumber <> -1 Then 
      pFault.LogFreeTextFault(143, "Invalid Logged Login ID used", pFunctionParameters, "TRGT-180829-1714", vRequester) 
      vRequester.RemoveLoginID() 
      Return pFault 
    End If 
 
    'Get the UserStatus  
    Dim pUserStatus As New csUserStatus 
    pFault = pUserStatus.GetByLastLoggedLoginID(vRequester.LoggedLoginID, vRequester, False) : If pFault.isOK = False Then Return pFault 
 
    If pUserStatus.IsEmpty Then 
      pFault.LogFreeTextFault(143, "pUserStatus.IsEmpty", pFunctionParameters, "TRGT-240504-183840", vRequester) 
      Return Check2FactorAuthenticationForLoginExitAfterFault(pFault, pLoggedLogin, Nothing, vRequester) 
    End If 
 
    'Check that the ID matches   
    If pUserStatus.LastLoggedLoginID <> pLoggedLoginID Then 
      pFault.LogFreeTextFault(143, "pUserStatus.LastLoggedLoginID <> pLoggedLoginID", pFunctionParameters, "TRGT-240504-184001", vRequester) 
      Return Check2FactorAuthenticationForLoginExitAfterFault(pFault, pLoggedLogin, Nothing, vRequester) 
    End If 
 
    'Now check the password   
    Dim pUser As New csUser 
    pFault = pUser.GetByID(vRequester.UserID, vRequester, True) : If pFault.isOK = False Then Return pFault 
 
    'Get the CellOrEmail 
    Dim pCellOrEmail As String = "" 
    If pUser.MessagingMode = clsEnums.enmMessagingMode.SMS Then 
      pCellOrEmail = pUser.PhoneNumber 
    ElseIf pUser.MessagingMode = clsEnums.enmMessagingMode.Email Then 
      pCellOrEmail = pUser.Email 
    End If 
 
    'get the IP 
    Dim pIP As String = pLoggedLogin.OriginatingIP 
    If String.IsNullOrEmpty(pIP) Then pIP = pLoggedLogin.AddressList.Split(","c)(0).Trim() 
    Dim pCountry As String = pLoggedLogin.OriginatingCountry 
    If (String.IsNullOrEmpty(pLoggedLogin.OriginatingCountry) OrElse pLoggedLogin.OriginatingCountry.Equals("UD", StringComparison.OrdinalIgnoreCase)) AndAlso Not pLoggedLogin.ClientReportedCountry.Equals("UD", StringComparison.OrdinalIgnoreCase) Then 
      pCountry = pLoggedLogin.ClientReportedCountry 
    End If 
 
    'send the mfa 
    pFault = csMFA.CheckMFA(pUser.ID, pCellOrEmail, "LoginByNamePwd", vCode, pIP, pCountry, vRequester, "") 
    If Not pFault.isOK Then 
      pFault = Check2FactorAuthenticationForLoginExitAfterFault(pFault, pLoggedLogin, pUserStatus, vRequester) 
      vRequester.SetEntryFunction("") 
      Return pFault 
    End If 
 
    'Now save data if necessary  
    If pUser.AuthenticationMethod = clsEnums.enmAuthenticationMethod.SingleVenue2FA Then 
      pIP = pLoggedLogin.OriginatingIP & "; " & pLoggedLogin.AddressList.Split(","c)(0).Trim() & "; " & DateTime.Now.ToString("yyyyMMddTHHmmss") 
      pFault = pUser.UpdateLoggedInIP(pIP, vRequester) : If Not pFault.isOK() Then Return pFault 
    End If 
 
    vRequester.SetEntryFunction("") 
 
    Return pFault 
  End Function 
 
  Private Shared Function Check2FactorAuthenticationForLoginExitAfterFault(vFault As clsFault, vLoggedLogin As csLoggedLogin, vUserStatus As csUserStatus, vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
    'revive in case of pfault 
    vRequester.ReviveLoginID() 
    pFault = vLoggedLogin.Update(vRequester, False) : If pFault.isOK = False Then Return pFault 
    If vFault.Number <> 144 Then 
      If vUserStatus IsNot Nothing Then 
        vUserStatus.LogoutTime = DateTime.Now 
        pFault = vUserStatus.Update(vRequester, False) : If pFault.isOK = False Then Return pFault 
      End If 
      vRequester.RemoveRoles() 
      vRequester.RemoveLoginID() 
    End If 
    vRequester.KillLoginID() 
    Return vFault 
  End Function 
 
  Friend Shared Function GetUserDetailsFromHost(vUserName As String, vRequester As clsRequester, ByRef rUserText As String) As clsFault 
    Dim pFunctionParameters As String = $"UserName: {vUserName}" 
 
    rUserText = "" 
 
    Dim pFault As New clsFault() 
 
    'Get the host root 
    Dim pSystemDefaultAuthenticationHostRoot As New csSystemDefault() 
    pFault = pSystemDefaultAuthenticationHostRoot.GetByFullSettingName(csSystemDefault.enmFullSettingName.Config_AuthenticationHostRoot, vRequester, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
    Dim pSystemDefaultAuthenticationHostPassword As New csSystemDefault() 
    pFault = pSystemDefaultAuthenticationHostPassword.GetByGroupAndSettingName("Config", "AuthenticationHostPassword", vRequester, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
    Dim pPassword As String = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, pSystemDefaultAuthenticationHostPassword.SettingValue) 
 
    Dim pDestinationURL As String = pSystemDefaultAuthenticationHostRoot.SettingValue 
    If Not pDestinationURL.EndsWith("/") Then pDestinationURL &= "/" 
    pDestinationURL = $"{pDestinationURL}CC/ccAPI.aspx" 
 
    Dim pTextToSend As String = $"{vUserName.Trim()} {pPassword.Trim()}" 
 
    pTextToSend = NETEncryption.clsTripleDES.EncryptData(pTextToSend, "TargCCOrders") 
 
    ' Create a new WebClient instance.     
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("Task", "ExternalAuthenticationGetUserDetails") 
    pWebClient.QueryString.Add("Guest", "TargCCOrders") 
 
    pWebClient.Credentials = vRequester.Credential 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    ' Upload the file to the URL using the HTTP 1.2 POST.     
    Dim pResponse As String 
    Try 
      pResponse = pWebClient.UploadString(pDestinationURL, "POST", pTextToSend) 
    Catch ex As Net.WebException 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-240309-125533", vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-240309-125600", vRequester) 
    End Try 
 
    Dim pHostFaultNo As Integer = 0 
    Dim pHostLoggedAlertID As Long = 0 
 
    Dim pUserText As String = "" 
 
    Try 
      Dim pResponses As String() = pResponse.Split(" "c) 
      If pResponses.Length > 1 Then 
        pHostFaultNo = ccHelper.ToInteger(pResponses(0)) 
        If pResponses.Length = 2 AndAlso ccHelper.IsNumeric(pResponses(1)) Then 
          pHostLoggedAlertID = ccHelper.ToLong(pResponses(1)) 
        Else 
          pUserText = pResponse.Substring(3) 
        End If 
      End If 
    Catch ex As Exception 
      Return pFault.LogFreeTextFault(72, $"Received an unexpected response from the host:  {pResponse}", pFunctionParameters, "TRGT-24039-141832", vRequester) 
    End Try 
 
    If pHostFaultNo = 70 Then 
      Return pFault.LogFreeTextFault(91, $"The user must 1st be created on the host", pFunctionParameters, "TRGT-240309-141940", vRequester, vAdditionalMessageToUser:="The user must 1st be created on the host") 
    End If 
 
    If pHostFaultNo <> -1 Then 
      Return pFault.LogFreeTextFault(pHostFaultNo, $"The LoggedAlertID on the host is {pHostLoggedAlertID}", pFunctionParameters, "TRGT-240309-142225", vRequester, vAdditionalMessageToUser:=$"The LoggedAlertID on the host is {pHostLoggedAlertID}") 
    End If 
 
    rUserText = pUserText 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Shared Function CreateBiometricKeyWithLastOTPForNewUser(vCellOrEmail As String, vOTP As String, vApplicationName As String, vApplicationIdentifier As String, vAccessingIP As String, vAccessingCountry As String, vRequester As clsRequester, ByRef rKey As String) As clsFault  
    Dim pFunctionParameters As String = $"vCellOrEmail: {vCellOrEmail}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    pFault = csMFA.CheckMFA(0, vCellOrEmail, "CreateBiometricKey", vOTP, vAccessingIP, vAccessingCountry, vRequester, "") : If Not pFault.isOK Then Return pFault 
 
    Dim pUser As New csUser(vRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault) : If Not pFault.isOK Then Return pFault 
 
    If vCellOrEmail.Contains("@") Then 
      If Not pUser.Email.Equals(vCellOrEmail, StringComparison.OrdinalIgnoreCase) Then 
        Return pFault.LogFreeTextFault(1, "Email does not match that of requester", pFunctionParameters, "TRGT-251218-100502", vRequester) 
      End If 
    Else 
      If Not pUser.PhoneNumber.Equals(vCellOrEmail, StringComparison.OrdinalIgnoreCase) Then 
        Return pFault.LogFreeTextFault(1, "PhoneNumber does not match that of requester", pFunctionParameters, "TRGT-251218-100509", vRequester) 
      End If 
    End If 
 
    Return CreateBiometricKey(vRequester.UserName, vApplicationName, vApplicationIdentifier, vRequester, rKey) 
 
  End Function 
 
  Public Shared Function CreateBiometricKeyWithLastOTPForExistingUser(vUserName As String, vOTP As String, vApplicationName As String, vApplicationIdentifier As String, vAccessingIP As String, vAccessingCountry As String, vRequester As clsRequester, ByRef rKey As String) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    If vUserName.Equals(vRequester.UserName, StringComparison.OrdinalIgnoreCase) = False Then 
      pFault.LogFreeTextFault(1, "UserName does not match that of requester", pFunctionParameters, "TRGT-251218-100233", vRequester) 
      Return pFault 
    End If 
 
    pFault = csMFA.CheckMFA(vRequester.UserID, "", "CreateBiometricKey", vOTP, vAccessingIP, vAccessingCountry, vRequester, "") : If Not pFault.isOK Then Return pFault 
 
    Return CreateBiometricKey(vUserName, vApplicationName, vApplicationIdentifier, vRequester, rKey) 
 
  End Function 
 
  Public Shared Function CreateBiometricKey(vUserName As String, vApplicationName As String, vApplicationIdentifier As String, vRequester As clsRequester, ByRef rKey As String) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserLoginKeyView, "ccSecurity_CreateBiometricKey", vRequester) : If pFault.isOK = False Then Return pFault 
 
    'Get the user 
    Dim pUser As New csUser() 
    pFault = pUser.GetByUserName(vUserName, vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
 
    Dim pUserLoginKey As New csUserLoginKey() 
    pFault = pUserLoginKey.GetByApplicationNameAndApplicationIdentifier(vApplicationName, vApplicationIdentifier, vRequester) : If pFault.isOK = False Then Return pFault 
 
    If Not pUserLoginKey.IsEmpty Then 
      If pUserLoginKey.UserID <> pUser.ID Then 
        Dim pOtherUser As New csUser() 
        pFault = pOtherUser.GetByID(pUserLoginKey.UserID, vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
        Return pFault.LogFreeTextFault(1, $"LoginKey already exists for another user! {pOtherUser.DefaultDesignation}", pFunctionParameters, "TRGT-250904-223040", vRequester, vAdditionalMessageToUser:="LoginKey already exists") 
      End If 
      pFault = pUserLoginKey.Delete(vRequester) : If Not pFault.isOK Then Return pFault 
    End If  
  
    'Create the key  
    Dim pKey As String = ccHelper.CreateSecretKey() 
 
    'Get the pLoggedLogin for location info 
    Dim pLoggedLogin As New csLoggedLogin(vRequester.LoggedLoginID, vRequester, pFault, True) : If pFault.isOK = False Then Return pFault 
 
    Dim pCountry As String = pLoggedLogin.OriginatingCountry 
    Dim pIP As String = pLoggedLogin.OriginatingIP 
 
    If String.IsNullOrEmpty(pCountry) Then 
      pCountry = pLoggedLogin.ClientReportedCountry 
      pIP = pLoggedLogin.ClientReportedIP 
    End If 
    If pCountry.Length > 10 Then pCountry = pCountry.Substring(0, 10) 
 
    pUserLoginKey = New csUserLoginKey() 
    pUserLoginKey.UserID = pUser.ID  
    pUserLoginKey.ApplicationName = vApplicationName  
    pUserLoginKey.ApplicationIdentifier = vApplicationIdentifier  
    pUserLoginKey.KeyHashed = $"PleaseHash{pKey}" 
    pUserLoginKey.ExternalIPAtCreation = pIP 
    pUserLoginKey.CountryAtCreation = pCountry 
    pUserLoginKey.LastAccessTime = DateTime.Now  
    pUserLoginKey.LoggedLoginID = pLoggedLogin.ID  
    pFault = pUserLoginKey.Update(vRequester, False) : If pFault.isOK = False Then Return pFault  
  
    rKey = pKey 
 
    'Send mail to user  
    Dim pMessage As String = $"A new biometric key has been created for you to use with {vApplicationName}.{Environment.NewLine}If you did not request this, please contact your system administrator immediately!{Environment.NewLine}{Environment.NewLine}IP: {pIP}, Country: {pCountry}" 
    pFault = pUser.SendMessage(pMessage, vRequester, vSubject:="New Biometric Key Created")  
    If Not pFault.isOK Then  
      'the error was logged. Now ignore it  
      pFault.SetOK(vRequester)  
    End If  
  
    Return pFault  
  End Function 
 
  Public Shared Function RemoveAllBiometricKeys(vUserName As String, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}" 
    Dim pFault As New clsFault 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.tbl_c_UserLoginKeyView, "ccSecurity_RemoveAllBiometricKeys", vRequester) : If pFault.isOK = False Then Return pFault 
 
    'Get the user 
    Dim pUser As New csUser() 
    pFault = pUser.GetByUserName(vUserName, vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
 
    pFault = csUserLoginKeyCol.DeleteByUserID(pUser.ID, vRequester) : If pFault.isOK = False Then Return pFault 
 
 
    Dim pLoggedLogin As New csLoggedLogin(vRequester.LoggedLoginID, vRequester, pFault, True) : If pFault.isOK = False Then Return pFault 
 
    'Send mail to user 
    Dim pMessage As String = "" 
    If vUserName = vRequester.UserName Then 
      pMessage = $"All your biometric keys have been deleted at your request.{Environment.NewLine}If you did not request this, please contact your system administrator immediately!{Environment.NewLine}{Environment.NewLine}UserName: {vRequester.UserName}, IP: {If(String.IsNullOrEmpty(pLoggedLogin.OriginatingIP), pLoggedLogin.ClientReportedIP, pLoggedLogin.OriginatingIP)}, Country: {If(String.IsNullOrEmpty(pLoggedLogin.OriginatingCountry), pLoggedLogin.ClientReportedCountry, pLoggedLogin.OriginatingCountry)}" 
    Else 
      pMessage = $"All your biometric keys have been deleted by the administrator.{Environment.NewLine}If you did not request this, please contact your system administrator immediately!{Environment.NewLine}{Environment.NewLine}UserName: {vRequester.UserName}, IP: {If(String.IsNullOrEmpty(pLoggedLogin.OriginatingIP), pLoggedLogin.ClientReportedIP, pLoggedLogin.OriginatingIP)}, Country: {If(String.IsNullOrEmpty(pLoggedLogin.OriginatingCountry), pLoggedLogin.ClientReportedCountry, pLoggedLogin.OriginatingCountry)}" 
    End If 
    pFault = pUser.SendMessage(pMessage, vRequester, vSubject:="New Biometric Key Deleted") 
    If Not pFault.isOK Then 
      'the error was logged. Now ignore it 
      pFault.SetOK(vRequester) 
    End If 
 
    Return pFault 
  End Function 
 
  ' Add this new Shared variable to track if a thread is currently working 
  Private Shared _IsLoading As Boolean = False 
  Private Shared _UserStatusCachePadlock As New Object 
  Private Shared _UserStatusCacheFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
  Private Shared _UserStatusCache As csUserStatusCol 
 
  Private Shared Function LoadUserStatusCache(vRequester As clsRequester, Optional vForceReRead As Boolean = False) As clsFault 
    Dim pFault As clsFault 
    Dim pDoit As Boolean = False 
 
 
    ' If cache is empty, we CANNOT use the non-blocking logic.  We must force this thread to wait or load the data. 
    If _UserStatusCache Is Nothing Then 
      SyncLock _UserStatusCachePadlock 
        ' Double-check optimization (standard singleton pattern) 
        If _UserStatusCache Is Nothing Then 
          Try 
            If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($"UserStatusCache was empty{Environment.NewLine}CallingApplication: {vRequester.CallingApplication}, UserName: {vRequester.UserName}, In CRITICAL STARTUP CHECK", "Caches") 
            ' Force a load right now, blocking other threads so they don't get a NullReferenceException 
            Return RecreateUserStatusCache(vRequester) 
          Catch ex As Exception 
            pFault = New clsFault() 
            Return pFault.LogException(ex, "Failed in CRITICAL STARTUP CHECK", "TRGT-260116-110651", vRequester) 
          End Try 
        End If 
      End SyncLock 
    End If 
 
    ' 1. CHECK + CLAIM (The Guard Lock) 
    SyncLock _UserStatusCachePadlock 
      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($"UserStatusCache Checking:{Environment.NewLine}CallingApplication: {vRequester.CallingApplication}, UserName: {vRequester.UserName}, ForceReRead: {vForceReRead}, TotalSeconds: {DateTimeOffset.Now.Subtract(_UserStatusCacheFilledTime).TotalSeconds:#,##0.0}", "Caches") 
 
      ' We verify the time AND ensure nobody else is already loading it 
      Dim isExpired As Boolean = (_UserStatusCacheFilledTime = DateTimeOffset.MinValue OrElse 
                                    DateTimeOffset.Now.Subtract(_UserStatusCacheFilledTime).TotalSeconds > 60) 
 
      If (isExpired OrElse vForceReRead) AndAlso Not _IsLoading Then 
        pDoit = True 
        _IsLoading = True ' CLAIM the job so other threads skip this block 
      End If 
    End SyncLock 
 
    ' 2. THE WORK (Outside the lock) 
    If pDoit = True Then 
      pFault = RecreateUserStatusCache(vRequester) 
    Else 
      ' 4. STALE DATA FALLBACK 
      ' If pDoit was False, it means either cache is fresh OR someone else is loading it. 
      ' We simply return success and let the app use the existing (slightly stale) cache. 
      pFault = New clsFault() 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  Private Shared Function RecreateUserStatusCache(vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($"    UserStatusCache doing it:{Environment.NewLine}CallingApplication: {vRequester.CallingApplication}, UserName: {vRequester.UserName}{Environment.NewLine}{ccHelper.GetStack()}", "Caches") 
    ' We use Try/Finally to ensure _IsLoading is reset even if the DB crashes 
    Try 
      ' FETCH DATA  
      Dim pFinalCollection As New csUserStatusCol() 
      pFault = pFinalCollection.FillByBoundedLoginTime(DateTime.Now.AddDays(-1).Date, DateTime.MaxValue, vRequester) 
      ' If DB fails, we must return the fault, but code in Finally block will still run 
      If Not pFault.isOK Then Return pFault 
 
 
      ' 3. THE SWAP (The Write Lock) 
      SyncLock _UserStatusCachePadlock 
        _UserStatusCache = pFinalCollection 
        _UserStatusCacheFilledTime = DateTimeOffset.Now 
      End SyncLock 
 
    Catch ex As Exception 
      ' Handle unexpected crashes (NullRef, etc) 
      pFault = New clsFault() 
      Return pFault.LogException(ex, $"LoadUserStatusCache", "TRGT-260115-181746", vRequester) 
    Finally 
      ' CRITICAL: Release the claim so others can try again later if needed 
      SyncLock _UserStatusCachePadlock 
        _IsLoading = False 
      End SyncLock 
    End Try 
    If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($"    UserStatusCache doing it:{Environment.NewLine}CallingApplication: {vRequester.CallingApplication}, UserName: {vRequester.UserName}. Final Cache count {_UserStatusCache.Count}", "Caches") 
 
    Return pFault 
  End Function 
 
  ' Call this method immediately after the user logs in successfully 
  Public Shared Sub AddUserToCacheSafe(ByVal vNewUser As csUserStatus) 
 
    If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($" --> UserStatusCache Adding UserID: {vNewUser.UserID}", "Caches") 
 
    SyncLock _UserStatusCachePadlock 
      'handle situation where the collection is null 
      If _UserStatusCache Is Nothing Then 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage($" -->     Creating new collection in AddUserToCacheSafe for (UserID: {vNewUser.UserID}, ApplicationName{vNewUser.ApplicationName})", "Caches") 
        _UserStatusCache = New csUserStatusCol() 
        _UserStatusCache.Add(vNewUser) 
        Return 
      End If 
 
      ' We create a NEW list and copy the existing items. We do NOT modify the live list (to prevent crashing readers). 
      Dim pNewList As New csUserStatusCol() 
      ' Copy existing users 
      For Each u In _UserStatusCache 
        pNewList.Add(u) 
      Next 
      ' 3. Update an existing user/app, or add the new user 
      Dim alreadyExists As Boolean = False 
      For Each u In pNewList 
        If u.UserID = vNewUser.UserID AndAlso u.ApplicationName = vNewUser.ApplicationName Then 
          u.LastLoggedLoginID = vNewUser.LastLoggedLoginID 
          u.LoginTime = vNewUser.LoginTime 
          u.LogoutTime = vNewUser.LogoutTime 
          alreadyExists = True 
          Exit For 
        End If 
      Next 
 
      If Not alreadyExists Then 
        pNewList.Add(vNewUser) 
      End If 
 
      ' 4. SWAP THE REFERENCE 
      ' This is atomic. Readers currently looking at the old list finish safely. New readers get this new list. 
      _UserStatusCache = pNewList 
 
      ' Optional: Reset the timer so we don't hit DB unnecessarily soon 
      _UserStatusCacheFilledTime = DateTimeOffset.Now 
 
    End SyncLock 
  End Sub 
 
  'Previous code 
  'Private Shared _UserStatusCachePadlock As New Object 
  'Private Shared _UserStatusCacheFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
 
  'Private Shared Function LoadUserStatusCache(ByVal vForceReload As Boolean, ByVal vRequester As clsRequester) As clsFault 
  '  Dim pFault As clsFault 
 
  '  Dim pDoit As Boolean = False 
 
  '  'get a collection of pUserStatuses, and renew every 10 seconds, or with every new login      
  '  SyncLock _UserStatusCachePadlock 
  '    If _UserStatusCacheFilledTime = DateTimeOffset.MinValue Then 
  '      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("UserStatusCache.Fill (virgin) Initial Fill", "Caches") 
  '      _UserStatusCacheFilledTime = DateTimeOffset.Now 
  '      _UserStatusCache = New csUserStatusCol 
  '      pFault = _UserStatusCache.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
  '      pDoit = False 
  '    ElseIf DateTimeOffset.Now.Subtract(_UserStatusCacheFilledTime).TotalSeconds > 60 Then 
  '      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("UserStatusCache.Fill (60s) About to DoIt", "Caches") 
  '      _UserStatusCacheFilledTime = DateTimeOffset.Now 
  '      pDoit = True 
  '    ElseIf vForceReload = True Then 
  '      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("UserStatusCache.Fill (forced) About to DoIt", "Caches") 
  '      _UserStatusCacheFilledTime = DateTimeOffset.Now 
  '      pDoit = True 
  '    Else 
  '      'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    UserStatusCache.Fill No update required", "Caches")  
  '    End If 
  '  End SyncLock 
 
  '  If pDoit = True Then 
  '    If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("UserStatusCache doing it", "Caches") 
  '    Dim pNewCol As New csUserStatusCol 
  '    pFault = pNewCol.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
  '    'now scan  
  '    For Each pOld As csUserStatus In _UserStatusCache 
  '      pOld.Tag = "" 
  '    Next 
  '    For Each pNew As csUserStatus In pNewCol 
  '      Dim pOld As csUserStatus = _UserStatusCache.FindByID(pNew.ID) 
  '      If pOld.IsEmpty Then 
  '        pNew.Tag = "New" 
  '        _UserStatusCache.Add(pNew) 
  '      Else 
  '        If pOld.isEqual(pNew) Then 
  '          pOld.Tag = "Used" 
  '        Else 
  '          _UserStatusCache.Remove(pOld) 
  '          pNew.Tag = "New" 
  '          _UserStatusCache.Add(pNew) 
  '        End If 
  '      End If 
  '    Next 
  '    'Load ID's to delete   
  '    Dim pIDsToDelete As New List(Of Long) 
  '    For Each pOld As csUserStatus In _UserStatusCache 
  '      If pOld.Tag = "" Then pIDsToDelete.Add(pOld.ID) 
  '    Next 
  '    For Each pID As Long In pIDsToDelete 
  '      _UserStatusCache.Remove(_UserStatusCache.FindByID(pID)) 
  '    Next 
  '  Else 
  '    pFault = New clsFault() 
  '    pFault.SetOK() 
  '  End If 
 
  '  Return pFault 
  'End Function 
 
  Public Shared Function LogOut(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}, LoggedLoginID={1}", vRequester.UserName, vRequester.LoggedLoginID) 
    Dim pFault As New clsFault 
 
    Dim pEntryPoint As String = "ccSecurity_LogOut" 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogOut, pEntryPoint, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pLoggedLogin As New csLoggedLogin 
    pFault = pLoggedLogin.GetByID(vRequester.LoggedLoginID, vRequester, True) 
    If pFault.isOK = False Then Return pFault 
 
    pLoggedLogin.TimeLoggedOut = DateTime.Now 
 
    pFault = pLoggedLogin.Update(vRequester, True) 
 
    'get the appropriate default  
    Dim pSystemDefault As New csSystemDefault 
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel, vRequester, True) : If pFault.isOK = False Then Return pFault 
 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefault.SettingValue) 
 
    'write to UserStatus table   
    If vRequester.UserEnableSimultaneousLogins = False Then 
      Dim pUserStatus As New csUserStatus() 
      pFault = pUserStatus.GetByLastLoggedLoginID(pLoggedLogin.ID, vRequester, False) 
      If pFault.isOK Then 
        If pUserStatus.ID = 0 Then 
          pFault.LogFreeTextFault(107, String.Format("Searched for {0}", pLoggedLogin.ID), pFunctionParameters, "TRGT-090419-1041", vRequester) 
        Else 
          pUserStatus.LogoutTime = pLoggedLogin.TimeLoggedOut 
          pFault = pUserStatus.Update(vRequester, False) 
          'Update the collection' 
          AddUserToCacheSafe(pUserStatus) 
        End If 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
  Public Shared Function GetMinimumWSControllerVersion(ByRef rVersionNumber As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}", vRequester.UserName) 
    Dim pFault As New clsFault 
 
    Dim pEntryPoint As String = "ccSecurity_GetMinimumWSControllerVersion" 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogOut, pEntryPoint, vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    Dim pSystemDefault As New csSystemDefault 
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Controller_WSControllerVersion, vRequester, True) 
    If pFault.isOK = False Then Return pFault 
 
    rVersionNumber = pSystemDefault.SettingValue 
 
    Return pFault 
  End Function 
 
  Private Shared Function CheckHardwarePermission(ByVal vFunctionName As String, ByVal vUser As csUser, ByVal vAccessingEntity As csAccessingEntity, ByRef rLoggedLogin As csLoggedLogin, ByVal vRequester As clsRequester) As clsFault  
    Dim pFault As clsFault  
  
    'First check that we have the required information  
    If vAccessingEntity.SystemDiskVolumeSerialNo.Trim.Length = 0 Then  
      rLoggedLogin.LoginFaultNumber = 93  
      Return UpdateWithFault(vFunctionName, rLoggedLogin, vRequester)  
    End If  
    If vAccessingEntity.ApplicationName = "" Then 'Undefined Accessing Application  
      rLoggedLogin.LoginFaultNumber = 94  
      Return UpdateWithFault(vFunctionName, rLoggedLogin, vRequester)  
    End If  
    If vAccessingEntity.ComputerMACAddress = "" Then 'No MAC Address  
      rLoggedLogin.LoginFaultNumber = 84  
      Return UpdateWithFault(vFunctionName, rLoggedLogin, vRequester)  
    End If  
  
    Dim pApplicationName As String = vAccessingEntity.ApplicationName 
    If pApplicationName.EndsWith("Dev", StringComparison.OrdinalIgnoreCase) OrElse pApplicationName.EndsWith("Stg", StringComparison.OrdinalIgnoreCase) Then 
      pApplicationName = pApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3) 
    End If 
 
    Dim pExternalIP = vAccessingEntity.WSReportedIP 
    If String.IsNullOrEmpty(pExternalIP) Then pExternalIP = vAccessingEntity.AddressList.Split(","c)(0).Trim() 
 
    'Get UserPermission based on UserID   
    Dim pUserPermissions As New csUserPermissionCol() 
    pFault = pUserPermissions.FillByUserIDAndApplicationName(vUser.ID, pApplicationName, vRequester) : If pFault.isOK = False Then Return pFault 
    Dim pUserPermission As New csUserPermission 
    'Prepare to create a new row   
    pUserPermission = New csUserPermission 
    With pUserPermission 
      .UserID = vUser.ID 
      .ApplicationName = pApplicationName 
      If vUser.RequiresComputerIdentification Then 
        .ComputerIdentifier = vAccessingEntity.ComputerIdentifier 
        .ComputerName = vAccessingEntity.EnvironmentMachineName 
      Else 
        .ComputerIdentifier = "*" 
        .ComputerName = "*" 
      End If 
      If vUser.RequiresFixedIP Then 
        .ExternalIP = pExternalIP 
      Else 
        .ExternalIP = "*" 
      End If 
      .Comments = "" 
      .HasPermission = False 
    End With 
 
    'Now check possibilities 
    Dim pPossibilities As csUserPermissionCol = pUserPermissions.Clone() 
 
    'RequiresComputerIdentification 
    If vUser.RequiresComputerIdentification Then 
      pPossibilities = pPossibilities.CloneByComputerIdentifier(vAccessingEntity.ComputerIdentifier) 
      If pPossibilities.Count = 0 Then 
        pPossibilities = pUserPermissions.Clone().CloneByComputerIdentifier(vAccessingEntity.SystemDiskVolumeSerialNo) 'temporary, for previous versions 
        For Each l In pPossibilities 
          l.ComputerIdentifier = vAccessingEntity.ComputerIdentifier 
        Next 
      End If 
      pPossibilities = pPossibilities.CloneByComputerName(vAccessingEntity.EnvironmentMachineName) 
      'now check if any are wildcard, and delete them 
      Dim pWCPossibilities As csUserPermissionCol = pUserPermissions.Clone() 
      pWCPossibilities = pWCPossibilities.CloneByComputerIdentifier("*") 
      For i As Integer = 0 To pWCPossibilities.Count - 1 
        pFault = pWCPossibilities(i).Delete(vRequester) : If pFault.isOK = False Then Return pFault 
      Next 
      pWCPossibilities = pUserPermissions.Clone() 
      pWCPossibilities = pWCPossibilities.CloneByComputerName("*") 
      For i As Integer = 0 To pWCPossibilities.Count - 1 
        pFault = pWCPossibilities(i).Delete(vRequester) : If pFault.isOK = False Then Return pFault 
      Next 
    End If 
 
    If vUser.RequiresFixedIP Then 
      pPossibilities = pPossibilities.CloneByExternalIP(pExternalIP) 
      'now check if any are wildcard, and delete them 
      Dim pWCPossibilities As csUserPermissionCol = pUserPermissions.Clone() 
      pWCPossibilities = pWCPossibilities.CloneByExternalIP("*") 
      For i As Integer = 0 To pWCPossibilities.Count - 1 
        pFault = pWCPossibilities(i).Delete(vRequester) : If pFault.isOK = False Then Return pFault 
      Next 
    End If 
 
    'Make wildcard whatever is needed - keep the top one 
    For Each l In pPossibilities 
      If Not vUser.RequiresComputerIdentification Then 
        l.ComputerIdentifier = "*" 
        l.ComputerName = "*" 
      End If 
      If Not vUser.RequiresFixedIP Then 
        l.ExternalIP = "*" 
      End If 
    Next 
 
    'If there Is more than one, then  
    Dim pPossibility As csUserPermission = Nothing 
    If pPossibilities.Count = 1 Then 
      pPossibility = pPossibilities(0) 
    ElseIf pPossibilities.Count > 1 Then 
      pPossibilities.SortByID() 
      pPossibilities.Reverse() 
      pPossibility = pPossibilities(0) 
      For i As Integer = 1 To pPossibilities.Count - 1 
        pFault = pPossibilities(i).Delete(vRequester) : If pFault.isOK = False Then Return pFault 
      Next 
    End If 
 
    If pPossibility Is Nothing Then pPossibility = pUserPermission 
 
    If pPossibility.HasPermission = False Then 
      If vUser.RequiresFixedIP Then 
        rLoggedLogin.LoginFaultNumber = 78 
      ElseIf vUser.RequiresComputerIdentification Then 
        rLoggedLogin.LoginFaultNumber = 82 
      End If 
      With pUserPermission 
        .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "New Entry" & Environment.NewLine & "=====" & Environment.NewLine & .Comments 
      End With 
    End If 
 
 
    'If vUser.UserPermissions.Count > 0 Then  
    '  'find one with the present computer traits   
    '  Dim pPossibilities As csUserPermissionCol  
    '  'MAC Address   
    '  pPossibilities = vUser.UserPermissions.CloneByComputerMACAddress(vAccessingEntity.ComputerMACAddress)  
    '  'now check without the ext (backwards compatible)  
    '  Dim pComputerMACAddress As String = vAccessingEntity.ComputerMACAddress.Trim  
    '  If pComputerMACAddress.IndexOf(":", StringComparison.OrdinalIgnoreCase) > 0 Then  
    '    pComputerMACAddress = pComputerMACAddress.Split(":"c)(1).Trim  
    '  End If  
    '  pPossibilities.AddRange(vUser.UserPermissions.CloneByComputerMACAddress(pComputerMACAddress))  
    '  If pPossibilities.Count = 0 Then  
    '    rLoggedLogin.LoginFaultNumber = 79  
    '    With pUserPermission  
    '      .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "New MAC address " & Environment.NewLine & "=====" & Environment.NewLine & .Comments  
    '    End With  
    '  End If  
    '  If pPossibilities.Count > 0 Then  
    '    'SystemDiskVolumeSerialNo   
    '    pPossibilities = pPossibilities.CloneBySystemDiskVolumeSerialNo(vAccessingEntity.SystemDiskVolumeSerialNo)  
    '    If pPossibilities.Count = 0 Then  
    '      rLoggedLogin.LoginFaultNumber = 77  
    '      With pUserPermission  
    '        .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "New Disk" & Environment.NewLine & "=====" & Environment.NewLine & .Comments  
    '      End With  
    '    End If  
    '  End If  
    '  If pPossibilities.Count > 0 Then  
    '    'ComputerName   
    '    pPossibilities = pPossibilities.CloneByComputerName(vAccessingEntity.EnvironmentMachineName)  
    '    If pPossibilities.Count = 0 Then  
    '      rLoggedLogin.LoginFaultNumber = 76  
    '      With pUserPermission  
    '        .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "Computer Name Changed" & Environment.NewLine & "=====" & Environment.NewLine & .Comments  
    '      End With  
    '    End If  
    '  End If  
    '  If pPossibilities.Count > 0 Then  
    '    'Application Name   
    '    If vAccessingEntity.ApplicationName.EndsWith("Dev", StringComparison.OrdinalIgnoreCase) OrEse vAccessingEntity.ApplicationName.EndsWith("Stg", StringComparison.OrdinalIgnoreCase) Then  
    '      pPossibilities = pPossibilities.CloneByApplicationName(vAccessingEntity.ApplicationName.Substring(0, vAccessingEntity.ApplicationName.Length - 3))  
    '    Else  
    '      pPossibilities = pPossibilities.CloneByApplicationName(vAccessingEntity.ApplicationName)  
    '    End If  
    '    If pPossibilities.Count = 0 Then  
    '      rLoggedLogin.LoginFaultNumber = 83  
    '      With pUserPermission  
    '        .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "Can't find application name" & Environment.NewLine & "=====" & Environment.NewLine & .Comments  
    '      End With  
    '    End If  
    '  End If  
    '  If pPossibilities.Count > 0 Then  
    '    'ExternalIP   
    '    Dim pPossibilitesUntouched As csUserPermissionCol = pPossibilities.CloneByExternalIP(pUserPermission.ExternalIP)  
    '    Dim pPossibilitesWildCard As csUserPermissionCol = pPossibilities.CloneByExternalIP("*")  
    '    pPossibilities = pPossibilities.CloneByExternalIP(vAccessingEntity.ExternalIP)  
    '    'add blanks    
    '    If pPossibilities.Count = 0 AndAlso pPossibilitesWildCard.Count > 0 Then pPossibilities = pPossibilitesWildCard  
    '    If pPossibilities.Count = 0 Then  
    '      If pPossibilitesUntouched.Count = 1 Then  
    '        pUserPermission = pPossibilitesUntouched(0)  
    '        pUserPermission.HasPermission = False  
    '      End If  
    '      rLoggedLogin.LoginFaultNumber = 78  
    '      With pUserPermission  
    '        .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "External IP Not Approved" & Environment.NewLine & "=====" & Environment.NewLine & .Comments  
    '      End With  
    '    End If  
    '  End If  
    '  If pPossibilities.Count > 1 Then  
    '    'Throw New Exception("Should not have more than 1 possibility remaining!!! TRGT-141213-1312")   
    '    Return pFault.LogFreeTextFault("Should not have more than 1 possibility remaining", "", "TRGT-160129-1546", vRequester)  
    '  ElseIf pPossibilities.Count = 1 Then  
    '    pUserPermission = pPossibilities(0)  
    '    If pUserPermission.HasPermission = False Then  
    '      rLoggedLogin.LoginFaultNumber = 82  
    '    End If   
    '  End If   
    'Else   
    '  rLoggedLogin.LoginFaultNumber = 82   
    '  With pUserPermission   
    '    .Comments = DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & Environment.NewLine & "First Access Attempt." & Environment.NewLine & "=====" & Environment.NewLine & .Comments   
    '  End With   
    'End If   
 
    'Record the access    
    pPossibility.LastAccessTime = DateTime.Now 
    pPossibility.LoggedLoginID = rLoggedLogin.ID 
    pFault = pPossibility.Update(vRequester, False) 
    If Not pFault.isOK Then Return pFault 
 
    If rLoggedLogin.LoginFaultNumber <> -1 Then 
      Return UpdateWithFault(vFunctionName, rLoggedLogin, vRequester) 
    End If 
 
    Return pFault  
  End Function  
  
  Public Shared Function ForgotPassword(vUserName As String, vEmail As String, vCellphone As String, vAccessingEntity As csAccessingEntity, ByRef rMessagingMode As clsEnums.enmMessagingMode, Optional vSystem As String = "") As clsFault 
    Dim pFunctionParameters As String 
    Dim pFault As clsFault 
    If vAccessingEntity Is Nothing Then 
      pFunctionParameters = String.Format("vUserName={0},vCellphone={1},AccessingEntity={2}", vUserName, vCellphone, "Nothing") 
    Else 
      pFunctionParameters = String.Format("vUserName={0},vCellphone={1},AccessingEntity.ApplicationName={2}", vUserName, vCellphone, vAccessingEntity.ApplicationName) 
    End If 
 
    Dim pEntryPoint As String = "ccSecurity_ForgotPassword" 
 
    'Load MyController  
    Try 
      Dim pDummy As String = MyController.ServerName 
    Catch ex As Exception 
      pFault = New clsFault 
      pFault.SetAlertMessage("Cannot create connection string" & Environment.NewLine & ex.Message, "Contact Support", clsEnums.enmFaultType.System, clsEnums.enmFaultSeverity.SMS) 
      Return pFault 
    End Try 
 
    Dim pRequester As New clsRequester() 
 
    Dim pLoggedLogin As New csLoggedLogin 
 
    LogonInitiateData(vAccessingEntity, pLoggedLogin) 
 
    If vUserName.Trim.Length = 0 Then 
      pLoggedLogin.LoginFaultNumber = 87 'User name not provided  
    End If 
    pLoggedLogin.UserName = vUserName 
    pLoggedLogin.HostingAssembly = (New StackFrame(1)).GetMethod().DeclaringType.Namespace() 
    pLoggedLogin.OriginatingIP = vAccessingEntity.WSReportedIP 
    pLoggedLogin.OriginatingCountry = vAccessingEntity.WSReportedCountry 
 
    pRequester.LoadValuesInLogin(vUserName, 0, 0, "", "", clsEnums.enmUserIdentityType.UD, 0, clsEnums.enmLanguage.en, "", vAccessingEntity.ApplicationName, vAccessingEntity.ApplicationVersion) 
 
    pRequester.SetEntryFunction(pEntryPoint) 
 
    'iklo add prc 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInByNamePwd, pEntryPoint, pRequester) 
    If pFault.isOK = False Then 
      pLoggedLogin.LoginFaultNumber = pFault.Number 
    End If 
 
    If pLoggedLogin.LoginFaultNumber > 0 Then 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    'get the appropriate defaults  
    Dim pSystemDefaults As New csSystemDefaultCol 
    pFault = pSystemDefaults.Fill(pRequester) : If pFault.isOK = False Then pRequester.RemoveLoginID() : Return pFault 
 
    Dim pSystemDefaultApplicationAuthenticationToWS As clsEnums.enmApplicationAuthenticationToWS = clsEnums.TranslateEnmApplicationAuthenticationToWS(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_ApplicationAuthenticationToWS).SettingValue) 
    Dim pSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel).SettingValue) 
 
    'Check if appropriate method is being used   
    If Not ((pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
           pSystemDefaultApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
           pSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      pLoggedLogin.LoginFaultNumber = 95 'Improper login method used by application  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    'Check if the data received is OK  
    Dim pUser = New csUser 
    pFault = pUser.GetByUserName(vUserName, pRequester, False) : If pFault.isOK = False Then Return pFault 
    If pUser.ID = 0 Then 
      pLoggedLogin.LoginFaultNumber = 91 'UserName Not Found  
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    'check the other details - exit with 92 of no match 
    If Not pUser.Email.Equals(vEmail, StringComparison.OrdinalIgnoreCase) Then 
      pLoggedLogin.LoginFaultNumber = 92 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    If Not vCellphone.Equals("NotNeeded") AndAlso Not pUser.PhoneNumber.Equals(vCellphone, StringComparison.OrdinalIgnoreCase) Then 
      pLoggedLogin.LoginFaultNumber = 92 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    pLoggedLogin.Language = pUser.Language 
 
    'Refresh, for logging in case of error  
    pRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pUser.FullName, 
                                 "",  
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 pUser.Language, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'reassign to ensure capitals are "pretty"  
    pLoggedLogin.UserName = pUser.UserName 
    pLoggedLogin.UserFullName = pUser.FirstName & " " & pUser.LastName 
 
    'Assign the identity   
    pLoggedLogin.UserIdentityTypeCode = pUser.Type.FastToString() 
    pLoggedLogin.UserIdentityTypeNameCode = CType(pUser.IDinType, Integer) 
 
    'reload for full details   
    pRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "",  
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 pUser.Language, 
                                 "", 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'Check if disabled 
    If pUser.IsDisabled = True Then 
      pLoggedLogin.LoginFaultNumber = 81 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
    'Check if locked out - Exempt AllowLoginBy2ndFactorOnly when not sending password 
    If pUser.IsLockedOut = True Then 
      pLoggedLogin.LoginFaultNumber = 119 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    'Get the roles  
    Dim pRoles As String = "#" 
    'Find the name of the main role  
    Dim pRole As New csRole(pUser.RoleID, clsEnums.enmLoadParent.TextOnly, pRequester, pFault, True) : If Not pFault.isOK Then Return pFault 
    'pLoggedLogin.LoginFaultNumber = 123   
    pRoles &= pRole.Name & "~" & pRole.ID.ToString().Trim & "#" & pRole.BaseRoleText & "~" & pRole.BaseRoleID.ToString().Trim & "#" 
    pLoggedLogin.Roles = pRoles 
 
    'Now check the computer  
    pLoggedLogin.LoginFaultNumber = -1 
 
    pFault = pLoggedLogin.Update(pRequester, True) : If Not pFault.isOK Then Return pFault 
 
    'reload for full details   
    pRequester.LoadValuesInLogin(pUser.UserName, 
                                 pUser.ID, 
                                 pLoggedLogin.ID, 
                                 pLoggedLogin.UserFullName, 
                                 "",  
                                 pUser.Type, 
                                 pUser.IDinType, 
                                 pUser.Language, 
                                 pRoles, 
                                 vAccessingEntity.ApplicationName, 
                                 vAccessingEntity.ApplicationVersion) 
 
    'check application permissions   
    If Not vAccessingEntity.ApplicationName.EndsWith("CC:ForgotPassword", StringComparison.OrdinalIgnoreCase) Then 
      pLoggedLogin.LoginFaultNumber = 99 
      Return UpdateWithFault((New StackFrame).GetMethod().Name, pLoggedLogin, pRequester) 
    End If 
 
    'Now Let's do it 
    pRequester.CallingFunctionWithinApplication = "ForgotPassword" 
 
 
    Dim pTicket As String = pRequester.CreateTicket() 
    Dim pTicketEncrypted As String = ccHelper.Cipher(ccHelper.enmEncryptionMethod.AES, pTicket) 
    Dim pTicketBase64 As String = ccHelper.ToBase64String(pTicketEncrypted) 
 
    Dim pSystemBase64 As String = "" 
    If Not String.IsNullOrWhiteSpace(vSystem) Then 
      Dim pSystemEncrypted As String = ccHelper.Cipher(ccHelper.enmEncryptionMethod.AES, vSystem) 
      pSystemBase64 = ccHelper.ToBase64String(pSystemEncrypted) 
    End If 
 
    Dim pUrl As String = $"{MyController.UploadFileURL.Replace("FileUpload.aspx", "ChangePassword.aspx")}?TKT={pTicketBase64}&S={pSystemBase64}" 
    Dim pMessage As String = ccHelper.GetLocalizedSystemText("A request to reset your password has been received.", pRequester) & Environment.NewLine ' "</br>" 
    pMessage &= ccHelper.GetLocalizedSystemText("Please click on the link below to continue the process!", pRequester) & Environment.NewLine ' "</br>" 
    pMessage &= ccHelper.GetLocalizedSystemText("If this was not requested by you, then do NOT click on the link, and contact us immediately!", pRequester) & Environment.NewLine & Environment.NewLine ' "</br>" ' "</br>" 
    pMessage &= ccHelper.GetLocalizedSystemText("Once you click on the link, we will send you a one-time password by email, and your existing password will no longer be valid.", pRequester) & Environment.NewLine & Environment.NewLine ' "</br>" ' "</br>" 
    pFault = pUser.SendMessage($"{pMessage}{Environment.NewLine}{pUrl}", pRequester, vSaveToTable:=False, vSubject:="TargCCOrders Forgot Password") : If Not pFault.isOK Then Return pFault 
 
    rMessagingMode = pUser.MessagingMode 
 
    'pFault = LogOut(rRequester) : If Not pFault.isOK Then Return pFault 
 
    Return pFault 
    'We are now logged in  
  End Function 
  
  ''' <summary>  
  ''' This check that the password meets the requirements as set by 'Security_RequireSecurePasswords' <br/>  
  ''' If checking for an existing user, send the UserID and the LastPasswords hash (from the user object)  
  ''' If checking for a new user, then don't send anything - they are optional  
  ''' </summary>  
  ''' <param name="vUserName"></param>  
  ''' <param name="vPassword"></param>  
  ''' <param name="vRequester"></param>  
  ''' <param name="vUserID"></param>  
  ''' <param name="vLastPasswords"></param>  
  ''' <returns></returns>  
  Public Shared Function CheckNewUserPassword(vUserName As String, vPassword As String, vRequester As clsRequester, vUserID As Long, vLastPasswords As String) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}, vUserID {vUserID}, vPassword {vPassword}, vLastPasswords.Length {vLastPasswords.Length}" 
    Dim pFault As clsFault 
 
    'check the level of require security  
    Dim pSystemDefault As New csSystemDefault 
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_RequireSecurePasswords, vRequester, True) : If Not pFault.isOK Then Return pFault 
 
 
    'Now check that it's not one of the last 4 passwords 
    If Not String.IsNullOrWhiteSpace(vLastPasswords) Then 
      If vUserID = 0 Then 
        Return pFault.LogFreeTextFault("In the last password check, I got a UserID of 0", pFunctionParameters, "TRGT-240912-160111", vRequester) 
      End If 
 
      Dim pPasswordEnc As String = NETEncryption.clsHash.Hash(vPassword, NETEncryption.clsHash.HashName.SHA256) 
      pPasswordEnc = NETEncryption.clsHash.Hash(vUserID.ToString() & pPasswordEnc, NETEncryption.clsHash.HashName.SHA256) 
 
      If vLastPasswords.Contains(pPasswordEnc) = True Then 
        Return pFault.LogFreeTextFault(118, "You cannot update to any of the last 4 passwords", pFunctionParameters, "TRGT-240912-133638", vRequester) 
      End If 
    End If 
 
 
    Dim pSettingValue As Integer = ccHelper.ToInteger(pSystemDefault.SettingValue) 
    If pSettingValue = 0 Then 
      'Password must be 4 characters   
      If vPassword.Trim.Length < 4 Then 
        pFault = New clsFault() 
        Return pFault.LogFreeTextFault(135, "Password must be at least 4 characters", pFunctionParameters, "TRGT-240912-133604", vRequester) 
      End If 
    ElseIf pSettingValue = 1 Then 
      'Password must be 8 characters   
      If vPassword.Trim.Length < 8 Then 
        pFault = New clsFault() 
        Return pFault.LogFreeTextFault(116, "Password must be at least 8 characters", pFunctionParameters, "TRGT-251122-182724", vRequester) 
      End If 
 
      Dim pResponse As String = ccHelper.ValidatePasswordByBestPractice(vPassword, vUserName) 
 
      If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then 
        'get the date the password was assigned 
        Dim pUser As New csUser(vUserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
        Return pFault.LogFreeTextFault(116, pResponse, pFunctionParameters, "TRGT-240912-133647", vRequester) 
      End If 
 
    End If 
 
    Return pFault.SetOK() 
  End Function 
 
  'Approval Codes (2FA?) 
  Private Shared _CancelApproval As New List(Of Long) 'used for ApproveViaWebPage requests 
 
  Public Enum enmApprovalMethod 
    UD 
    ApproveViaWebPage 
    ApproveViaWebLink 
    ApproveSendCodeOnly 
  End Enum 
 
  ''' <summary> 
  ''' <br>This sends a message to the user with a code, and adds it to the database.</br>  
  ''' <br>There are 3 modes for sending approval:</br>  
  ''' <br> - ApproveViaWebPage: the user gets a link that opens a page, where the user types the code received.</br>  
  ''' <br> - ApproveViaWebLink: the user receives a link. Clicking on the link signals approval.</br>  
  ''' <br> - ApproveSendCodeOnly: the user gets a code. He types the code in a screen prepared by the programmer.</br>  
  ''' <br>The vFunctionParams consists of 2 parts delimited by a #.</br>  
  ''' <br>The FunctionMae should be unique in the code.</br>  
  ''' <br>{Function Name}#{TextToShowInLinkAndPage}.</br>  
  ''' <br>For example: vFunctionName = $"SalesRequest-221004-0944#Please approve the sale of your house".</br>  
  ''' </summary> 
  ''' <param name="vFunctionParams"></param> 
  ''' <param name="vApprovalMethod"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function RequireApproval(vFunctionParams As String, vApprovalMethod As enmApprovalMethod, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"FunctionParams: {vFunctionParams}, ApproveViaWebPage: {vApprovalMethod}, UserName: {vRequester.UserName}" 
    Dim pFault As clsFault 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInAnonymously, "ccSecurity_RequireApproval", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If String.IsNullOrEmpty(vFunctionParams) Then 
      Return pFault.LogFreeTextFault("vFunctionName must have a value", pFunctionParameters, "TRGT-221004-1003", vRequester) 
    End If 
    'check the text  
    Dim pTest As String() = vFunctionParams.Split("#"c) 
    If pTest.Length <> 2 Then 
      Return pFault.LogFreeTextFault("vFunctionName must have 2 values delimited by '#'", pFunctionParameters, "TRGT-221004-1003", vRequester) 
    End If 
    If String.IsNullOrEmpty(pTest(0).Trim) OrElse String.IsNullOrEmpty(pTest(1).Trim) Then 
      Return pFault.LogFreeTextFault("vFunctionName must have 2 values delimited by '#'", pFunctionParameters, "TRGT-221004-1003", vRequester) 
    End If 
 
    'create a random number   
    Dim pNewPwd As String = "" 
    If vApprovalMethod = enmApprovalMethod.ApproveSendCodeOnly OrElse vApprovalMethod = enmApprovalMethod.ApproveViaWebPage Then 
      Dim pRnd As Random = New Random() 
      Dim pValue As Integer = pRnd.Next(100, 999999) 
      pNewPwd = pValue.ToString("000000") 
      If vApprovalMethod = enmApprovalMethod.ApproveSendCodeOnly Then 
        pFault = csUser.UpdateApproval(vRequester.UserID, "PleaseHash" & pNewPwd, $"{vFunctionParams}#{vRequester.LoggedLoginID}#2", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
      Else 'If vApprovalMethod = enmApprovalMethod.ApproveViaWebPage Then 
        pFault = csUser.UpdateApproval(vRequester.UserID, "PleaseHash" & pNewPwd, $"{vFunctionParams}#{vRequester.LoggedLoginID}#1", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
      End If 
    ElseIf vApprovalMethod = enmApprovalMethod.ApproveViaWebLink Then 
      pNewPwd = "000000" 
      pFault = csUser.UpdateApproval(vRequester.UserID, "PleaseHash" & pNewPwd, $"{vFunctionParams}#{vRequester.LoggedLoginID}#0", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
    End If 
 
    'Now send the SMS   
    'get the user  
    Dim pUser As New csUser(vRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
 
    Dim pSubject As String = "TargCCOrders" 
 
    Dim pMessage As String 
    If vApprovalMethod = enmApprovalMethod.ApproveViaWebLink OrElse vApprovalMethod = enmApprovalMethod.ApproveViaWebPage Then 
      Dim pLoggedLoginEnc As String = ccHelper.Encrypt(ccHelper.enmEncryptionMethod.TripleDES, vRequester.LoggedLoginID.ToString()) 
      Dim pUserNameEnc As String = ccHelper.Encrypt(ccHelper.enmEncryptionMethod.TripleDES, vRequester.UserName) 
      Dim pFunctionNameEnc As String = ccHelper.Cipher(ccHelper.enmEncryptionMethod.TripleDES, pTest(0)) 
      Dim pUrl As String = $"{MyController.UploadFileURL.Replace("FileUpload.aspx", "Approve.aspx")}?{ccHelper.ToBase64String(pLoggedLoginEnc & "#" & pUserNameEnc & "#" & pFunctionNameEnc)}" 
      If vApprovalMethod = enmApprovalMethod.ApproveViaWebLink Then 
        pMessage = $"{pTest(1)}{Environment.NewLine}Please click on the link below:{Environment.NewLine}{pUrl}" 
      Else 
        pMessage = $"{pTest(1)}{Environment.NewLine}Please click on the link below:{Environment.NewLine}{pUrl}{Environment.NewLine}Enter the following code in the page:{Environment.NewLine}{pNewPwd}" 
      End If 
    Else 
      pMessage = pSubject & ":" & Environment.NewLine & $"{pTest(1)}{Environment.NewLine}Please use the codes below{Environment.NewLine}{pNewPwd}" 
    End If 
 
    pFault = pUser.SendMessage(pMessage, vRequester, vSaveToTable:=False, vSubject:=pSubject) : If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
  
  ''' <summary>  
  ''' <br>This checks the code and approves the function. It is not meant to be used by ApproveViaWebPage</br>  
  ''' </summary>  
  ''' <param name="vAuthorizationCode"></param>  
  ''' <param name="vFunctionName"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function CheckApprovalCode(vAuthorizationCode As String, vFunctionName As String, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"FunctionName: {vFunctionName}, UserName: {vRequester.UserName}" 
    Dim pFault As clsFault 
 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInAnonymously, "ccSecurity_CheckApprovalCode", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    vAuthorizationCode = vAuthorizationCode.Trim 
 
    If String.IsNullOrEmpty(vAuthorizationCode) Then 
      Return pFault.LogFreeTextFault($"The AuthorizationCode should NOT be blank.", pFunctionParameters, "TRGT-221004-1624", vRequester) 
    End If 
 
    Dim pApproveViaWebPage As Boolean = (vFunctionName.Split("#"c).Length = 3) 
    If pApproveViaWebPage Then 
      Return pFault.LogFreeTextFault($"The function should not be used when Approving Via Web Page.", pFunctionParameters, "TRGT-221004-1624", vRequester) 
    End If 
 
    Dim pAuthorizationCode As String = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, vAuthorizationCode) 
 
    'get the user  
    Dim pUser As New csUser(vRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
 
    If pUser.ApprovalFunctionName.Split("#"c).Length = 4 Then 
      If pUser.ApprovalFunctionName.Split("#"c)(3) <> "2" Then 
        Return pFault.LogFreeTextFault($"The function should not be used when Approving Via Web Page or Link.", pFunctionParameters, "TRGT-221004-1624", vRequester) 
      End If 
    End If 
 
    If Not String.IsNullOrEmpty(pUser.ApprovalFunctionName) AndAlso Not vFunctionName.Split("#"c)(0).Equals(pUser.ApprovalFunctionName.Split("#"c)(0), StringComparison.OrdinalIgnoreCase) Then 
      Return pFault.LogFreeTextFault(155, $"vFunctionName.Equals<>pUser.ApprovalFunctionName", vFunctionName, "TRGT-221004-1040", vRequester) 
    End If 
 
    If DateTimeOffset.Now.Subtract(pUser.ApprovalTime).Minutes > 10 Then 
      Return pFault.LogFreeTextFault(155, $" DateTimeOffset.Now.Subtract(pUser.ApprovalTime).Minutes > 10", vFunctionName, "TRGT-221004-1041", vRequester) 
    End If 
 
    'only allow 1 try 
    pFault = csUser.UpdateApproval(vRequester.UserID, "", "", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
 
    If Not pUser.ApprovalCodeHashed.Equals(pAuthorizationCode, StringComparison.OrdinalIgnoreCase) Then 
      pFault.LogFreeTextFault(155, $"pUser.ApprovalCodeHashed.Equals<>pAuthorizationCode", vFunctionName, "TRGT-221004-1043", vRequester) 
    End If 
 
 
    Return pFault 
  End Function 
 
  ''' <summary>  
  ''' <br>This checks the code and approves the function via ApproveViaWebPage.</br>  
  ''' </summary>  
  ''' <param name="vFunctionName"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function CheckApproval(vFunctionName As String, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"FunctionName: {vFunctionName}, UserName: {vRequester.UserName}" 
    Dim pFault As clsFault 
 
    pFault = ccSecurity.GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInAnonymously, "ccSecurity_CheckApproval", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    If _CancelApproval.Contains(vRequester.LoggedLoginID) Then 
      _CancelApproval.Remove(vRequester.LoggedLoginID) 
    End If 
 
    'now check in a loop 
    Dim pApproved As Nullable(Of Boolean) = Nothing 
    Dim pStart As DateTimeOffset = DateTimeOffset.Now 
    Do 
      If _CancelApproval.Contains(vRequester.LoggedLoginID) Then 
        _CancelApproval.Remove(vRequester.LoggedLoginID) 
        pFault = csUser.UpdateApproval(vRequester.UserID, "", "Cancelled", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
        Return pFault.LogFreeTextFault(157, $"", vFunctionName, "TRGT-230412-1434", vRequester) 
      End If 
      Threading.Thread.Sleep(2000) 
      If DateTimeOffset.Now.Subtract(pStart).TotalSeconds > 70 Then Exit Do 
      Dim pUser As New csUser(vRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
      If pUser.ApprovalFunctionName.Split("#"c).Length = 4 Then 
        If pUser.ApprovalFunctionName.Split("#"c)(3) = "2" Then 
          Return pFault.LogFreeTextFault($"This function can only be used when checking via ApproveViaWebPage or Link.", pFunctionParameters, "TRGT-221004-1624", vRequester) 
        End If 
      End If 
      If Not vFunctionName.Split("#"c)(0).Equals(pUser.ApprovalFunctionName.Split("#"c)((0)), StringComparison.OrdinalIgnoreCase) Then 
        pApproved = False 
        Exit Do 
      End If 
      If DateTimeOffset.Now.Subtract(pUser.ApprovalTime).Minutes > 3 Then 
        pApproved = False 
        Exit Do 
      End If 
      If pUser.ApprovalCodeHashed.Equals("Failed", StringComparison.OrdinalIgnoreCase) Then 
        pFault = csUser.UpdateApproval(vRequester.UserID, "", "", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
        pApproved = False 
        Exit Do 
      End If 
      If String.IsNullOrEmpty(pUser.ApprovalCodeHashed) Then 
        pFault = csUser.UpdateApproval(vRequester.UserID, "", "", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
        pApproved = True 
        Exit Do 
      End If 
    Loop 
 
    If pApproved Is Nothing Then 
      pFault = csUser.UpdateApproval(vRequester.UserID, "", "", DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
      Return pFault.LogFreeTextFault(156, $"Approval Code timed out", vFunctionName, "TRGT-221006-1741", vRequester) 
    End If 
    If pApproved = False Then 
      Return pFault.LogFreeTextFault(155, $"Approval Code Failed", vFunctionName, "TRGT-221006-1740", vRequester) 
    End If 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' This cancels the check loop in CheckApproval 
  ''' </summary> 
  ''' <param name="vRequester"></param> 
  Public Shared Sub CancelCheckApproval(vRequester As clsRequester) 
    If Not _CancelApproval.Contains(vRequester.LoggedLoginID) Then 
      _CancelApproval.Add(vRequester.LoggedLoginID) 
    End If 
  End Sub 
 
  ''' <summary>  
  ''' This function is used by the ApproveViaWebPage. It marks the function as approved, so it can be scanned by another thread with 'CheckApproval'.  
  ''' </summary>  
  ''' <param name="vAuthorizationCode"></param>  
  ''' <param name="vFunctionName"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function MarkAsApproved(vAuthorizationCode As String, vFunctionName As String, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"FunctionName: {vFunctionName}, UserName: {vRequester.UserName}" 
    Dim pFault As clsFault 
 
    pFault = GetPermissionForDBControllerFunction(clsEnums.enmProcess.prc_LogInAnonymously, "ccSecurity_MarkAsApproved", vRequester) 
    If pFault.isOK = False Then Return pFault 
 
    vAuthorizationCode = vAuthorizationCode.Trim 
    If String.IsNullOrEmpty(vAuthorizationCode) Then Return pFault 
 
    If _CancelApproval.Contains(vRequester.LoggedLoginID) Then 
      _CancelApproval.Remove(vRequester.LoggedLoginID) 
    End If 
 
 
    'encrypt the password  
    Dim pAuthorizationCode As String = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, vAuthorizationCode) 
 
    'get the user  
    Dim pUser As New csUser(vRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, vRequester, pFault, vMustExist:=True) : If Not pFault.isOK Then Return pFault 
 
    If pUser.ApprovalFunctionName = "Cancelled" Then 
      Return pFault.LogFreeTextFault(157, $"", vFunctionName, "TRGT-230413-0914", vRequester) 
    End If 
 
    'f no code, then it was already answered. Consider it timed out 
    If String.IsNullOrEmpty(pUser.ApprovalCodeHashed) Then 
      Return pFault.LogFreeTextFault(156, $"pUser.ApprovalCodeHashed=''", vFunctionName, "TRGT-221004-0940", vRequester) 
    End If  
  
    If Not vFunctionName.Equals(pUser.ApprovalFunctionName.Split("#"c)(0), StringComparison.OrdinalIgnoreCase) Then 
      Return pFault.LogFreeTextFault(155, $"vFunctionName.Equals<>pUser.ApprovalFunctionName", vFunctionName, "TRGT-221004-0941", vRequester) 
    End If 
 
    If DateTimeOffset.Now.Subtract(pUser.ApprovalTime).Seconds > 110 Then 
      Return pFault.LogFreeTextFault(156, $"DateTimeOffset.Now.Subtract(pUser.ApprovalTime).Seconds > 110", vFunctionName, "TRGT-221004-0942", vRequester) 
    End If 
 
    If Not pUser.ApprovalCodeHashed.Equals(pAuthorizationCode, StringComparison.OrdinalIgnoreCase) Then 
      pFault = csUser.UpdateApproval(vRequester.UserID, "Failed", vFunctionName, DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
      Return pFault.LogFreeTextFault(155, $"pUser.ApprovalCodeHashed.Equals<>pAuthorizationCode", vFunctionName, "TRGT-221004-0942", vRequester) 
    End If 
 
    pFault = csUser.UpdateApproval(vRequester.UserID, "", vFunctionName, DateTimeOffset.Now, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Return pFault 
  End Function 
 
 
  'handle permissions 
  Private Shared _UIProcessCol As csProcessCol 
  Private Shared _UIPermissionCol As csPermissionCol 
  Private Shared _UIPermissionFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
  Private Shared _UIPermissionPadlock As New Object 
  Private Shared _LastDate As Integer = -1 
  Public Shared Function GetPermissionForUI(ByVal vProcess As clsEnums.enmProcess, ByVal vRequester As clsRequester) As Boolean 
    Dim pProcess As String = vProcess.FastToString() 
    Dim pFunctionParameters As String = "fProcessEnum=" & pProcess 
 
    Dim pFault As New clsFault 
 
    'These can always be viewed 
    If pProcess = "tbl_c_ProcessView" OrElse 
        pProcess = "tbl_c_PermissionView" OrElse 
        pProcess = "tbl_c_SystemDefaultView" OrElse 
        pProcess = "tbl_c_LanguageView" OrElse 
        pProcess = "tbl_c_EnumerationView" OrElse 
        pProcess = "tbl_c_LookupView" Then 
      Return True 
    End If 
 
    If vRequester.Roles Is Nothing Then 
      pFault.LogFreeTextFault(97, "Roles is Nothing", pFunctionParameters, "TRGT-100601-1506", vRequester) 
      Return False 
    End If 
 
    'If it's master, then he can do everything  
    If vRequester.IsInRole("Master") = True OrElse vRequester.IsInRole("ApplicationMaster") = True Then 
      Return True 
    End If 
 
    'If I'm not in a web, and not a master, then logout if we passed midnight... 
    If _LastDate = -1 Then 
      _LastDate = DateTime.Now.Day 
    Else 
      If _LastDate <> DateTime.Now.Day Then 
        'logout 
        pFault = LogOut(vRequester) 
        Return False 
      End If 
    End If 
 
    'Fill the Process only if necessary - maybe in the future we can put a time limit to 20 min  
    SyncLock _UIPermissionPadlock 
      If _UIPermissionFilledTime = DateTimeOffset.MinValue Then 
        _UIPermissionFilledTime = DateTimeOffset.Now 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LoadUIPermissions Doing Initial Fill", "Caches") 
        _UIProcessCol = New csProcessCol 
        pFault = _UIProcessCol.Fill(vRequester) 
        If pFault.isOK = False Then Return False 
 
        _UIPermissionCol = New csPermissionCol() 
        pFault = _UIPermissionCol.Fill(vRequester) 
        If pFault.isOK = False Then Return False 
      ElseIf DateTimeOffset.Now.Subtract(_UIPermissionFilledTime).TotalSeconds > 150 Then 'PermissionCacheChange
        _UIPermissionFilledTime = DateTimeOffset.Now 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LoadUIPermissions Doing Fill (5m)", "Caches") 
        _UIProcessCol = New csProcessCol 
        pFault = _UIProcessCol.Fill(vRequester) 
        If pFault.isOK = False Then Return False 
 
        _UIPermissionCol = New csPermissionCol() 
        pFault = _UIPermissionCol.Fill(vRequester) 
        If pFault.isOK = False Then Return False 
      Else 
        'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    LoadUIPermissions No update required", "Caches") 
      End If 
    End SyncLock 
 
    'Get the Process (or Request) ID from the name   
    Dim pProcessID As Long = 0 
    Dim pUIPermissionCol As csPermissionCol = Nothing 
    SyncLock _UIPermissionCol 
      pProcessID = _UIProcessCol.FindByName(pProcess).ID 
      If pProcessID = 0 Then 
        pFault.LogFreeTextFault(205, "Process:" & pProcess, "", "TRGT-090317-1944", vRequester) 
        Return False 
      ElseIf pProcessID = Nothing Then 
        pFault.LogFreeTextFault(205, "Process:" & pProcess, "", "TRGT-090317-1945", vRequester) 
        Return False 
      End If 
      pUIPermissionCol = _UIPermissionCol.CloneByProcessID(pProcessID) 
    End SyncLock 
 
    'Now scan the grid to get permissions   
    Dim pCanDo As Boolean = False 
    For Each pPermission As csPermission In pUIPermissionCol 
      If vRequester.Roles.IndexOf("~" & pPermission.RoleID.ToString().Trim & "#", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        If pPermission.CanDo = True Then 
          pCanDo = True 
        ElseIf pPermission.CanDo = False Then 
          'this overrides 
          pCanDo = False 
          Exit For 
        End If 
      End If 
    Next 
 
    Return pCanDo 
  End Function 
  
  Private Shared _SecurityExemptLock As New Object 
 
  Private Shared Sub LoadSecurityExemptTables() 
    SyncLock _SecurityExemptLock 
      _SecurityExemptViewTables = New List(Of String) 
 
      _SecurityExemptViewTables.Add("c_AlertMessage") 
      _SecurityExemptViewTables.Add("c_Enumeration") 
      _SecurityExemptViewTables.Add("c_Language") 
      _SecurityExemptViewTables.Add("c_LoggedAlert") 
      _SecurityExemptViewTables.Add("c_Lookup") 
      _SecurityExemptViewTables.Add("c_ObjectToTranslate") 
      _SecurityExemptViewTables.Add("c_ObjectTranslation") 
 
      _SecurityExemptUpdateTables = New List(Of String) 
      _SecurityExemptUpdateTables.Add("c_LoggedAlert") 
 
      RaiseEvent evtLoadSecurityExemptions() 
    End SyncLock 
 
  End Sub 
 
  Private Shared _EntryPoints As New Dictionary(Of String, String)  
  ''' <summary>  
  ''' This is used for external assemblies accessing the DBController. 
  ''' This also checks the LoginID and is used Internally by the DBController for extra security.  
  ''' If it's a 1st entry from the outside (of the external assembly), it returns a clone of the requester object. 
  ''' This function requires a security code 
  ''' </summary>  
  ''' <param name="vProcess"></param>  
  ''' <param name="vEntryPoint"></param>  
  ''' <param name="rRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function GetPermissionForDBControllerFunction(vProcess As clsEnums.enmProcess, vEntryPoint As String, vAuthCode As String, ByRef rRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    If vEntryPoint.StartsWith("ext_", StringComparison.OrdinalIgnoreCase) Then 
      Dim pCallingAssembly As String = System.Reflection.Assembly.GetCallingAssembly().GetName.Name 
      Return pFault.LogFreeTextFault(100, $"External Assembly {pCallingAssembly} cannot have an entry point starting with 'ext_'", $"CallingAssembly: {pCallingAssembly}{Environment.NewLine}Process: {vProcess}{Environment.NewLine}EntryPoint: {vEntryPoint}", "TRGT-220131-1657", rRequester) 
    End If 
 
    vEntryPoint = $"ext_{vEntryPoint}" 
 
    'check if we checked 
    If Not _EntryPoints.ContainsKey(vEntryPoint) Then 
      Dim pFullName As String = System.Reflection.Assembly.GetCallingAssembly().FullName 
      If vAuthCode <> ccHelper.Cipher(ccHelper.enmEncryptionMethod.TripleDES, pFullName) Then 
        Dim pCallingAssembly As String = System.Reflection.Assembly.GetCallingAssembly().GetName.Name 
        Return pFault.LogFreeTextFault(100, $"External Assembly {pCallingAssembly} failed credentials", $"CallingAssembly: {pCallingAssembly}{Environment.NewLine}Process: {vProcess}{Environment.NewLine}EntryPoint: {vEntryPoint}", "TRGT-220131-1657", rRequester) 
      End If 
      _EntryPoints.Add(vEntryPoint, vAuthCode) 
    Else 
      If vAuthCode <> _EntryPoints(vEntryPoint) Then 
        Dim pCallingAssembly As String = System.Reflection.Assembly.GetCallingAssembly().GetName.Name 
        Return pFault.LogFreeTextFault(100, $"External Assembly {pCallingAssembly} failed credentials", $"CallingAssembly: {pCallingAssembly}{Environment.NewLine}Process: {vProcess}{Environment.NewLine}EntryPoint: {vEntryPoint}", "TRGT-220131-1657", rRequester) 
      End If 
    End If 
 
    Return GetPermissionForDBControllerFunction(vProcess, vEntryPoint, rRequester)  
 
  End Function 
 
  ''' <summary> 
  ''' This also checks the LoginID and is used Internally by the DBController for extra security. If it's a 1st entry from the outside, it returns a clone of the requester object  
  ''' </summary> 
  ''' <param name="vProcess"></param> 
  ''' <param name="vEntryPoint"></param> 
  ''' <param name="rRequester"></param> 
  ''' <returns></returns> 
  Friend Shared Function GetPermissionForDBControllerFunction(ByVal vProcess As clsEnums.enmProcess, ByVal vEntryPoint As String, ByRef rRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If rRequester Is Nothing Then 
      If _SecurityExemptViewTables Is Nothing Then 
        LoadSecurityExemptTables() 
      End If 
      'get the table 
      Dim pAllow As Boolean = False 
      Dim pTable As String = "" 
      Dim pOperationType As String = "" 
      Dim pProcess As String = vProcess.FastToString() 
      If pProcess.EndsWith("View", StringComparison.OrdinalIgnoreCase) Then 
        pTable = pProcess.Substring(4, pProcess.Length - 8) 
        If _SecurityExemptViewTables.Contains(pTable) Then 
          pAllow = True 
          pOperationType = "View" 
        End If 
      ElseIf pProcess.EndsWith("Update", StringComparison.OrdinalIgnoreCase) Then 
        pTable = pProcess.Substring(4, pProcess.Length - 10) 
        If _SecurityExemptUpdateTables.Contains(pTable) Then 
          pAllow = True 
          pOperationType = "Update" 
        End If 
      End If 
      If pAllow = True Then 
        'Create fake requester 
        rRequester = New clsRequester(pTable, pOperationType, True) 
      Else 
        pFault = New clsFault() 
        Return pFault.LogFreeTextFault(59, "", String.Format("Process={0}, EntryPoint={1}", vProcess, vEntryPoint), "TRGT-190808-1134", Nothing) 
      End If 
    End If 
 
    If rRequester.UserName = "SecurityExempt" AndAlso rRequester.UserFullName = "SecurityExempt No Requester" Then 
      pFault = New clsFault 
      pFault.SetOK() 
    ElseIf String.IsNullOrEmpty(rRequester?.EntryFunction) Then 
      pFault = ccSecurity.GetPermissionForExternal(vProcess, vEntryPoint, rRequester) 
    Else 
      Dim pForceOK As Boolean = False 
      If rRequester.EntryFunction.StartsWith("ccSecurity_LogInBy", StringComparison.OrdinalIgnoreCase) OrElse 
         rRequester.EntryFunction.Equals("ccSecurity_Check2FactorAuthenticationForLogin", StringComparison.OrdinalIgnoreCase) OrElse 
         rRequester.EntryFunction.Equals("ccSecurity_ForgotPassword", StringComparison.OrdinalIgnoreCase) Then 
        pForceOK = True 
      End If 
      pFault = New clsFault 
      If pForceOK = True Then 
        pFault.SetOK() 
      Else 
        pFault = New clsFault 
        If rRequester.LoggedLoginID <= 0 Then 
          'swap it so we can write the error. The LogFreeTextFault will make it negative again 
          rRequester.ReviveLoginID() 
          Return pFault.LogFreeTextFault(90, "", rRequester.ToStringFriend(), "TRGT-160105-1658", rRequester) 
        Else 
          pFault.SetOK() 
        End If 
      End If 
    End If 
 
    Return pFault 
  End Function 
 
  Private Shared _ProcessCol As csProcessCol 
  Private Shared _PermissionCol As csPermissionCol 
  Private Shared _PermissionFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
  Private Shared _PermissionPadlock As New Object 
 
  Private Shared _AllowedLogins As clsComboList = Nothing 
  Private Shared _AllowedLoginsResetTime As DateTimeOffset = DateTimeOffset.MinValue 
  Private Shared _AllowedLoginsPadlock As New Object 
 
  Private Shared Function GetPermissionForExternal(ByVal vProcess As clsEnums.enmProcess, ByVal vEntryPoint As String, ByRef rRequester As clsRequester) As clsFault  
    Dim pFunctionParameters As String = String.Format("Process={0}, EntryPoint={1}", vProcess.FastToString(), vEntryPoint)  
    Dim pFault As New clsFault  
  
    If rRequester Is Nothing Then  
      'Requester should be something - even in login    
      Return pFault.LogFreeTextFault("rRequester is nothing!", pFunctionParameters, "TRGT-160129-1134", rRequester)  
    End If  
  
    'we should immediately create cloned requester, so that we are thread-safe  
    rRequester = rRequester.Clone()  
    rRequester.SetEntryFunction(vEntryPoint) 
 
    'CheckLoginID - Check that we didn't get 0   
    If rRequester.LoggedLoginID = 0 Then 
      'No LoginID received       
      If rRequester.UserName.Equals("SecurityExempt", StringComparison.OrdinalIgnoreCase) AndAlso (New StackFrame(3)).GetMethod().DeclaringType.Name() & "_" & (New StackFrame(3)).GetMethod().Name = "clsFault_CreateLoggedAlert" Then 
        Return pFault.SetOK() 
      ElseIf rRequester.UserName.Equals("SecurityExempt", StringComparison.OrdinalIgnoreCase) AndAlso (New StackFrame(3)).GetMethod().DeclaringType.Name() & "_" & (New StackFrame(3)).GetMethod().Name = "csFunctions_SystemDefaultColFillByGroup" Then 
        'get configs for WSController 
        Return pFault.SetOK() 
      Else 
        Return pFault.LogFreeTextFault(85, String.Format("fAccessingComputer.LoggedLoginID:{0}", rRequester.LoggedLoginID), pFunctionParameters, "TRGT-110410-1227", rRequester) 
      End If 
    End If 
 
    If rRequester.LoggedLoginID < 0 Then 
      'Probably had a previous problem, and now trying again from the outside 
      rRequester.ReviveLoginID() 
    End If 
 
    'Check that we know what called us   
    If String.IsNullOrEmpty(rRequester.CallingFunctionWithinApplication) Then  
      Tools.LogToTextFile.WriteMessage("No CallingFunctionWithinApplication" & Environment.NewLine & "Requester:" & rRequester.ToStringFriend() & Environment.NewLine, "CFWNA") 
      If Debugger.IsAttached Then  
        Return pFault.LogFreeTextFault(142, "", pFunctionParameters, "TRGT-160731-1003", rRequester) 
      End If  
    End If  
  
    'Handle Master as quickly as possible   
    If (rRequester.IsInRole("Master") = True OrElse rRequester.IsInRole("ApplicationMaster") = True) AndAlso rRequester.LoggedLoginID > 0 Then 
      'Do not log master accesses    
      Return pFault.SetOK() 
    End If 
 
    'Don't log if it's file system  
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      Return pFault.SetOK() 
    End If 
 
    'Now check the entry point. It should be something   
    If String.IsNullOrEmpty(vEntryPoint) OrElse vEntryPoint.IndexOf("_") < 0 Then 
      Dim pMessage As String = "" 
      If Not String.IsNullOrEmpty(vEntryPoint) Then pMessage = "vEntryPoint=" & vEntryPoint & " - Missing '_'" 
      Return pFault.LogFreeTextFault(145, "vEntryPoint=" & vEntryPoint, pFunctionParameters, "TRGT-180914-1642", rRequester) 
    End If 
 
    If Debugger.IsAttached Then 
      'do a deep analysis 
      Dim pEntryPoint As String = vEntryPoint 
      If pEntryPoint.Contains(":"c) Then pEntryPoint = pEntryPoint.Split(":"c)(0) 
      Dim pCalculatedEntryPoint As String = "" 
      If vEntryPoint.StartsWith("ext_", StringComparison.OrdinalIgnoreCase) Then 
        pCalculatedEntryPoint = "ext_" & (New StackFrame(3)).GetMethod().DeclaringType.Name & "_" & (New StackFrame(3)).GetMethod().Name 
      Else 
        Dim pFunction As String = (New StackFrame(2)).GetMethod().Name 
        If pFunction = ".ctor" Then pFunction = "New" 
        pCalculatedEntryPoint = (New StackFrame(2)).GetMethod().DeclaringType.Name & "_" & pFunction 
      End If 
      If Not pEntryPoint.Equals(pCalculatedEntryPoint, StringComparison.OrdinalIgnoreCase) Then 
        Return pFault.LogFreeTextFault(145, $"vEntryPoint - received:{vEntryPoint}, calculated:{pCalculatedEntryPoint}", pFunctionParameters, "TRGT-220701-1552", rRequester) 
      End If 
    End If 
  
    Dim pProcess As String = vProcess.FastToString()  
  
    'Fill most pRequest values   
    'don't record mail checks and combolist accesses   
    If Not (vEntryPoint.Equals("csMailCol_FillByMailTypeAndRecipientEmailAndWasSeen", StringComparison.OrdinalIgnoreCase) OrElse vEntryPoint.StartsWith("clsComboList", StringComparison.OrdinalIgnoreCase)) Then   
      Dim pRequest As New csLoggedRequest  
      pRequest.LoggedLoginID = rRequester.LoggedLoginID  
      pRequest.TimeAccessed = DateTime.Now  
      pRequest.UserID = rRequester.UserID  
      pRequest.EntryPoint = vEntryPoint  
      pRequest.CallingFunctionWithinApplication = rRequester.CallingFunctionWithinApplication  
      pRequest.Process = pProcess  
      pRequest.Thread = Threading.Thread.CurrentThread.ManagedThreadId.ToString()  
      
      'now handle lambda function   
      If vEntryPoint.IndexOf("lambda$", StringComparison.OrdinalIgnoreCase) >= 0 Then  
        'it's a lambda function, clear the request (later on we might us it for something)   
        pRequest.EntryPoint = vEntryPoint  
      End If  
      
      'Add it to the list   
      AddRequest(pRequest, rRequester) 
    End If  
 
    Static sSystemDefaultUserIdentificationModel As clsEnums.enmUserIdentificationModel = clsEnums.enmUserIdentificationModel.UD 
    If sSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.UD Then  
      'get the appropriate default   
      Dim pSystemDefault As New csSystemDefault  
      pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_UserIdentificationModel, rRequester, True) : If pFault.isOK = False Then Return pFault  
      sSystemDefaultUserIdentificationModel = clsEnums.TranslateEnmUserIdentificationModel(pSystemDefault.SettingValue)  
    End If  
  
    'to avoid checking in ForgotPassword 
    If rRequester.CallingApplication.EndsWith("CC:ForgotPassword", StringComparison.OrdinalIgnoreCase) Then 
      rRequester.SetUserEnableSimultaneousLogins(True) 
    End If 
 
    'Check that user matches LoginId and is Legal (unless we're allowed multiple    
    If rRequester.UserEnableSimultaneousLogins = False AndAlso 
      (sSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser OrElse 
       sSystemDefaultUserIdentificationModel = clsEnums.enmUserIdentificationModel.ByDomainUser) Then  
      'get a collection of pUserStatuses, and renew every X seconds, or with every new login     
      pFault = LoadUserStatusCache(vRequester:=rRequester) : If pFault.isOK = False Then Return pFault 
      '1st try to find it     
      Dim pUserStatus As csUserStatus = _UserStatusCache.FindByLastLoggedLoginID(rRequester.LoggedLoginID) 
      If pUserStatus.ID = 0 Then 
        'newly logged on and hasn't been added yet  
        pFault = LoadUserStatusCache(vRequester:=rRequester, vForceReRead:=True) : If pFault.isOK = False Then Return pFault 
        pUserStatus = _UserStatusCache.FindByLastLoggedLoginID(rRequester.LoggedLoginID)  
      End If  
  
      If pUserStatus.ID <> 0 AndAlso pUserStatus.UserID = rRequester.UserID Then  
        If pUserStatus.LogoutTime <> Nothing Then ''LoggedLogin already expired     
          Return pFault.LogFreeTextFault(104, String.Format("LoggedLoginID:{0}, UserName:{1}, ExternalCallingApplication:{2} ", rRequester.LoggedLoginID, rRequester.UserName, rRequester.CallingApplication), pFunctionParameters, "TRGT-110410-123252", rRequester) 
        End If 
        If pUserStatus.LoginTime.Date <> DateTime.Now.Date AndAlso DateTime.Now.Subtract(pUserStatus.LoginTime).TotalHours > 2 Then 
          pFault = LogOut(rRequester) 
          Return pFault.LogFreeTextFault(104, $"Expired overnight! LoggedLoginID:{rRequester.LoggedLoginID}, UserName:{rRequester.UserName}, ExternalCallingApplication:{rRequester.CallingApplication}", pFunctionParameters, "TRGT-201114-160200", rRequester) 
        End If 
      ElseIf pUserStatus.ID <> 0 AndAlso pUserStatus.UserID <> rRequester.UserID Then  
        'Attempted to access with an invalid loginID (belonging to another user)   
        Return pFault.LogFreeTextFault(101, String.Format("LoggedLoginID:{0}, UserName:{1}, ExternalCallingApplication:{2}, Expected UserID:{3}, Received UserID:{4} ", rRequester.LoggedLoginID, rRequester.UserName, rRequester.CallingApplication, rRequester.UserID, pUserStatus.UserID), pFunctionParameters, "TRGT-110410-122801", rRequester)  
      ElseIf pUserStatus.ID = 0 Then  
        'Let's get the LastLoggedInID for the user   
        Dim pActualLoginID As Long = 0  
        For Each l In _UserStatusCache  
          If l.UserID = rRequester.UserID AndAlso l.ApplicationName = rRequester.CallingApplication Then  
            pUserStatus = l  
            Exit For  
          End If  
        Next  
        If pUserStatus.ID <> 0 Then  
          If pUserStatus.LastLoggedLoginID = rRequester.LoggedLoginID * 10 Then  
            Return pFault.LogFreeTextFault(134, "LogInID multiple of 10: " & String.Format("LoggedLoginID:{0}, UserName:{1}, ExternalCallingApplication:{2} ", rRequester.LoggedLoginID, rRequester.UserName, rRequester.CallingApplication), pFunctionParameters, "TRGT-110410-122802", rRequester)  
          Else  
            Return pFault.LogFreeTextFault(103, String.Format("UserID:{0}, Expected LoginID:{1}, Found LoginID:{2} ", rRequester.UserID, rRequester.LoggedLoginID, pUserStatus.LastLoggedLoginID), pFunctionParameters, "TRGT-160513-1852", rRequester)  
          End If  
        Else  
          Return pFault.LogFreeTextFault(105, String.Format("UserID:{0}, Expected LoginID:{1}, None Found", rRequester.UserID, rRequester.LoggedLoginID), pFunctionParameters, "TRGT-160513-1853", rRequester)  
        End If  
      End If  
    Else  
      'here enable multiple entries with login-name    
      Dim pAllowedLogin As clsComboListMember = Nothing  
      SyncLock _AllowedLoginsPadlock 
        If _AllowedLoginsResetTime = DateTimeOffset.MinValue Then 
          If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("AllowedLoginsReset Doing Initial Reset", "Caches") 
          _AllowedLoginsResetTime = DateTimeOffset.Now 
          _AllowedLogins = New clsComboList 
        ElseIf DateTimeOffset.Now.Subtract(_AllowedLoginsResetTime).TotalSeconds > 150 Then 'PermissionCacheChange 
          If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("AllowedLoginsReset Doing Reset (5m)", "Caches") 
          _AllowedLoginsResetTime = DateTimeOffset.Now 
          _AllowedLogins = New clsComboList 
        Else 
          'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    AllowedLoginsReset No reset required", "Caches") 
        End If 
        If _AllowedLogins.Count = 0 Then 
          pAllowedLogin = New clsComboListMember(ccHelper.ToLong(0), "") 
        Else 
          pAllowedLogin = _AllowedLogins.FindByKey(rRequester.LoggedLoginID) 
        End If 
        If pAllowedLogin.KeyLong = 0 Then 
          'check it in code    
          Dim pLoggedLogin As New csLoggedLogin 
          pFault = pLoggedLogin.GetByID(rRequester.LoggedLoginID, rRequester, False) : If Not pFault.isOK() Then Return pFault 
          If pLoggedLogin.IsEmpty Then 
            Return pFault.LogFreeTextFault(103, String.Format("UserID:{0}, Expected LoginID:{1} ", rRequester.UserID, rRequester.LoggedLoginID), pFunctionParameters, "TRGT-160730-1155", rRequester) 
          End If 
          If pLoggedLogin.UserName <> rRequester.UserName Then 
            Return pFault.LogFreeTextFault(103, String.Format("UserName:{0}, Expected UserName:{1} ", pLoggedLogin.UserName, rRequester.UserName), pFunctionParameters, "TRGT-160730-1156", rRequester) 
          End If 
          If pLoggedLogin.ApplicationName <> rRequester.CallingApplication Then 
            Return pFault.LogFreeTextFault(103, String.Format("UserName:{0}, Expected UserName:{1} Found Application:{2} ", pLoggedLogin.UserName, rRequester.CallingApplication, pLoggedLogin.ApplicationName), pFunctionParameters, "TRGT-160730-1157", rRequester) 
          End If 
          If pLoggedLogin.TimeLoggedOut <> Nothing Then 
            Return pFault.LogFreeTextFault(104, String.Format("UserName:{0}, Expected UserName:{1} Found Application:{2} ", pLoggedLogin.UserName, rRequester.CallingApplication, pLoggedLogin.ApplicationName), pFunctionParameters, "TRGT-160730-1157", rRequester) 
          End If 
          pAllowedLogin = New clsComboListMember(pLoggedLogin.ID, pLoggedLogin.UserName & "#" & pLoggedLogin.ApplicationName) 
          _AllowedLogins.Add(pAllowedLogin) 
        Else 
          If pAllowedLogin.Text <> rRequester.UserName & "#" & rRequester.CallingApplication Then 
            Return pFault.LogFreeTextFault(103, String.Format("Expected Code:{0} Received Code:{1} ", pAllowedLogin.Text, rRequester.UserName & "#" & rRequester.CallingApplication), pFunctionParameters, "TRGT-160730-1157", rRequester) 
          End If 
        End If 
      End SyncLock 
    End If  
  
    If rRequester.Roles Is Nothing Then  
      Return pFault.LogFreeTextFault(97, "Roles is Nothing", pFunctionParameters, "TRGT-100601-0952", rRequester)  
    End If  
  
    'now check permissions   
  
    'Fill the Process only if necessary - maybe in the future we can put a time limit to 20 min     
    Dim pDoIt As Boolean = False 
 
    SyncLock _PermissionPadlock 
      If _PermissionFilledTime = DateTimeOffset.MinValue Then 
        _PermissionFilledTime = DateTimeOffset.Now 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LoadPermissions Initial Fill", "Caches") 
        _ProcessCol = New csProcessCol 
        pFault = _ProcessCol.Fill(rRequester) : If pFault.isOK = False Then Return pFault 
 
        _PermissionCol = New csPermissionCol() 
        pFault = _PermissionCol.Fill(rRequester) : If pFault.isOK = False Then Return pFault 
      ElseIf DateTimeOffset.Now.Subtract(_PermissionFilledTime).TotalSeconds > 150 Then 'PermissionCacheChange 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LoadPermissions About to do it (20m)", "Caches") 
        _PermissionFilledTime = DateTimeOffset.Now 
        pDoIt = True 
      Else 
        'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    LoadPermissions No update required", "Caches") 
      End If 
    End SyncLock 
 
    If pDoIt = True Then 
      If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LoadPermissions Doing it", "Caches") 
      Dim pProcessColLoc As New csProcessCol 
      pFault = pProcessColLoc.Fill(rRequester) : If pFault.isOK = False Then Return pFault 
 
      Dim pPermissionColLoc As New csPermissionCol() 
      pFault = pPermissionColLoc.Fill(rRequester) : If pFault.isOK = False Then Return pFault 
 
      SyncLock _PermissionPadlock 
        _ProcessCol = pProcessColLoc 
        _PermissionCol = pPermissionColLoc 
      End SyncLock 
    End If 
 
 
    'Get the Process (or Request) ID from the name       
    Dim pProcessID As Long = 0 
    Dim pPermissionCol As csPermissionCol = Nothing 
    SyncLock _PermissionPadlock 
      pProcessID = _ProcessCol.FindByName(pProcess).ID 
      If pProcessID = 0 Then 
        Return pFault.LogFreeTextFault(205, "Process:" & pProcess, pFunctionParameters, "TRGT-090317-1944", rRequester) 
      ElseIf pProcessID = Nothing Then 
        Return pFault.LogFreeTextFault(205, "Process:" & pProcess, pFunctionParameters, "TRGT-090317-1945", rRequester) 
      End If 
      pPermissionCol = _PermissionCol.CloneByProcessID(pProcessID) 
    End SyncLock 
 
    'Now scan the grid to get permissions       
    Dim pCanDo As Boolean = False 
    For Each pPermission As csPermission In pPermissionCol 
      If rRequester.Roles.IndexOf("~" & pPermission.RoleID.ToString().Trim & "#", StringComparison.OrdinalIgnoreCase) >= 0 Then  
        If pPermission.CanDo = True Then  
          pCanDo = True  
        ElseIf pPermission.CanDo = False Then 
          'this overrides 
          pCanDo = False 
          Exit For  
        End If  
      End If  
    Next  
  
    If pCanDo = True Then  
      pFault.SetOK()  
    Else  
      pFault.LogFreeTextFault(96, "Unauthorized! Requested Action: " & pProcess & "", pFunctionParameters, "TRGT-110410-12417", rRequester)  
    End If  
  
    'now check anything else 
    Dim pCancel As Boolean = False 
    RaiseEvent evtAfterGetPermissionForExternal(rRequester, pCancel, pFault) 
    If Not pFault.isOK Then Return pFault 
    If pCancel = True Then Return pFault 
 
    Return pFault  
  End Function  
  
  Private Shared _LogRequestToTable As Nullable(Of Boolean) = Nothing 
 
  Friend Shared Sub AddRequest(ByVal vRequest As csLoggedRequest, ByVal vRequester As clsRequester) 
 
    'Add it to the database  
    If _LogRequestToTable Is Nothing Then 
      Dim pSystemDefault As New csSystemDefault 
      Dim pFault As New clsFault 
      pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Security_LogRequests, vRequester, True) 
      If Not pFault.isOK Then 
        _LogRequestToTable = True 
        Tools.LogToTextFile.WriteMessage("pSystemDefault.GetByFullSettingName Failed (TRGT-180903-1444): " & Environment.NewLine & pFault.StringForMessageBox, "ccSecurity") 
      Else 
        _LogRequestToTable = CBool(ccHelper.ToInteger(pSystemDefault.SettingValue)) 
      End If 
    End If 
 
    If _LogRequestToTable = False Then Exit Sub 
 
    Dim pTask As Task = Task.Run(Sub() 
                                   Dim plocFault As clsFault 
                                   plocFault = vRequest.Update(vRequester, vReload:=False) 
                                   If Not plocFault.isOK Then Tools.LogToTextFile.WriteMessage("pRequest.Update Failed (TRGT-180903-1443): " & Environment.NewLine & plocFault.StringForMessageBox, "ccSecurity") 
                                 End Sub) 
 
  End Sub 
 
  ''' <summary>  
  ''' This is used by tables that are defined as Used For Identity 
  ''' </summary>  
  ''' <param name="vEntity"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Friend Shared Function GetPermissionForExternalIndentityTypeForEntity(ByVal vEntity As cTargCCEntity, ByVal vCallingFunction As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    pFault.SetOK() 
 
    If vEntity.IsEmpty Then Return pFault 
 
    ' 6886948 Block when non-usermanager or administrator, and user is not the requester userid
 
    If vRequester.UserIdentityType <> clsEnums.enmUserIdentityType.Global Then 
      'Check limitations based on IdentityType  
      Dim pCallingClass As String = (New StackFrame(1)).GetMethod().DeclaringType.Name() 
      If pCallingClass.StartsWith("cls", StringComparison.OrdinalIgnoreCase) Then 
        pCallingClass = pCallingClass.Substring(3) 
      ElseIf pCallingClass.StartsWith("cs", StringComparison.OrdinalIgnoreCase) Then 
        pCallingClass = pCallingClass.Substring(2) 
      End If 
 
      Select Case pCallingClass 
        Case "BeehiveBuyerTracking" 
          Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = DirectCast(vEntity, clsBeehiveBuyerTracking) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              If vRequester.UserIdentityInstanceID <> pBeehiveBuyerTracking.CustomerID AndAlso pBeehiveBuyerTracking.CustomerID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pBeehiveBuyerTracking.ID, "", "TRGT-BeehiveBuyerTracking-160116-1402", vRequester) 
              End If 
          End Select 
        Case "Customer" 
          Dim pCustomer As clsCustomer = DirectCast(vEntity, clsCustomer) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Me)
              If vRequester.UserIdentityInstanceID <> pCustomer.ID Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pCustomer.ID, "", "TRGT-Customer-160116-1403", vRequester) 
              End If 
          End Select 
        Case "CustomerDebt" 
          Dim pCustomerDebt As clsCustomerDebt = DirectCast(vEntity, clsCustomerDebt) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              If vRequester.UserIdentityInstanceID <> pCustomerDebt.CustomerID AndAlso pCustomerDebt.CustomerID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pCustomerDebt.ID, "", "TRGT-CustomerDebt-160116-1402", vRequester) 
              End If 
          End Select 
        Case "OrderHeader" 
          Dim pOrderHeader As clsOrderHeader = DirectCast(vEntity, clsOrderHeader) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              If vRequester.UserIdentityInstanceID <> pOrderHeader.CustomerID AndAlso pOrderHeader.CustomerID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pOrderHeader.ID, "", "TRGT-OrderHeader-160116-1402", vRequester) 
              End If 
          End Select 
        Case "JobAlertRecipient" 
          Dim pJobAlertRecipient As csJobAlertRecipient = DirectCast(vEntity, csJobAlertRecipient) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pJobAlertRecipient.UserID AndAlso pJobAlertRecipient.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pJobAlertRecipient.ID, "", "TRGT-JobAlertRecipient-160116-1402", vRequester) 
              End If 
          End Select 
        Case "LoggedAlert" 
          Dim pLoggedAlert As csLoggedAlert = DirectCast(vEntity, csLoggedAlert) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pLoggedAlert.AffectedUserID AndAlso pLoggedAlert.AffectedUserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pLoggedAlert.ID, "", "TRGT-LoggedAlert-160116-1402", vRequester) 
              End If 
          End Select 
        Case "LoggedRequest" 
          Dim pLoggedRequest As csLoggedRequest = DirectCast(vEntity, csLoggedRequest) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pLoggedRequest.UserID AndAlso pLoggedRequest.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pLoggedRequest.ID, "", "TRGT-LoggedRequest-160116-1402", vRequester) 
              End If 
          End Select 
        Case "MFA" 
          Dim pMFA As csMFA = DirectCast(vEntity, csMFA) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pMFA.UserID AndAlso pMFA.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pMFA.ID, "", "TRGT-MFA-160116-1402", vRequester) 
              End If 
          End Select 
        Case "User" 
          Dim pUser As csUser = DirectCast(vEntity, csUser) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Me)
              If vRequester.UserIdentityInstanceID <> pUser.ID Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pUser.ID, "", "TRGT-User-160116-1403", vRequester) 
              End If 
          End Select 
        Case "UserLoginKey" 
          Dim pUserLoginKey As csUserLoginKey = DirectCast(vEntity, csUserLoginKey) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pUserLoginKey.UserID AndAlso pUserLoginKey.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pUserLoginKey.ID, "", "TRGT-UserLoginKey-160116-1402", vRequester) 
              End If 
          End Select 
        Case "UserPermission" 
          Dim pUserPermission As csUserPermission = DirectCast(vEntity, csUserPermission) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pUserPermission.UserID AndAlso pUserPermission.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pUserPermission.ID, "", "TRGT-UserPermission-160116-1402", vRequester) 
              End If 
          End Select 
        Case "UserStatus" 
          Dim pUserStatus As csUserStatus = DirectCast(vEntity, csUserStatus) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              If vRequester.UserIdentityInstanceID <> pUserStatus.UserID AndAlso pUserStatus.UserID > 0 Then 
                pFault.LogFreeTextFault(61, "CallingClass=" & pCallingClass & "; ID=" & pUserStatus.ID, "", "TRGT-UserStatus-160116-1402", vRequester) 
              End If 
          End Select 
      End Select 
    End If 
 
    Return pFault 
  End Function 
  ''' <summary> 
  ''' This is used by tables that are defined as Used For Identity  
  ''' </summary> 
  ''' <param name="vCollection"></param> 
  ''' <param name="rReturnedCollection"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Friend Shared Function GetPermissionForExternalIndentityTypeForCollection(ByVal vCollection As ITargCCCollection, ByRef rReturnedCollection As Generic.List(Of ITargCCEntity), ByVal vCallingFunction As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault 
 
    pFault.SetOK() 
 
    If vRequester.UserIdentityType <> clsEnums.enmUserIdentityType.Global Then 
      'Check limitations based on IdentityType  
      Dim pCallingClass As String = (New StackFrame(1)).GetMethod().DeclaringType.Name() 
      If pCallingClass.StartsWith("cls", StringComparison.OrdinalIgnoreCase) Then 
        pCallingClass = pCallingClass.Substring(3) 
      ElseIf pCallingClass.StartsWith("cs", StringComparison.OrdinalIgnoreCase) Then 
        pCallingClass = pCallingClass.Substring(3) 
      End If 
 
      rReturnedCollection = Nothing 
 
      Select Case pCallingClass 
        Case "BeehiveBuyerTrackingCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              'go through them, and keep only those with an allowed Customer ID 
              For Each lItem In vCollection 
                Dim pBeehiveBuyerTracking As clsBeehiveBuyerTracking = DirectCast(lItem, clsBeehiveBuyerTracking) 
                If pBeehiveBuyerTracking.CustomerID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pBeehiveBuyerTracking) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "CustomerCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Me)
              'go through them, and keep only those with an allowed ID 
              For Each lItem In vCollection 
                Dim pCustomer As clsCustomer = DirectCast(lItem, clsCustomer) 
                If pCustomer.ID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pCustomer) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "CustomerDebtCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              'go through them, and keep only those with an allowed Customer ID 
              For Each lItem In vCollection 
                Dim pCustomerDebt As clsCustomerDebt = DirectCast(lItem, clsCustomerDebt) 
                If pCustomerDebt.CustomerID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pCustomerDebt) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "OrderHeaderCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.Customer ' (Parent)
              'go through them, and keep only those with an allowed Customer ID 
              For Each lItem In vCollection 
                Dim pOrderHeader As clsOrderHeader = DirectCast(lItem, clsOrderHeader) 
                If pOrderHeader.CustomerID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pOrderHeader) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "JobAlertRecipientCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pJobAlertRecipient As csJobAlertRecipient = DirectCast(lItem, csJobAlertRecipient) 
                If pJobAlertRecipient.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pJobAlertRecipient) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "LoggedAlertCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pLoggedAlert As csLoggedAlert = DirectCast(lItem, csLoggedAlert) 
                If pLoggedAlert.AffectedUserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pLoggedAlert) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "LoggedRequestCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pLoggedRequest As csLoggedRequest = DirectCast(lItem, csLoggedRequest) 
                If pLoggedRequest.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pLoggedRequest) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "MFACol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pMFA As csMFA = DirectCast(lItem, csMFA) 
                If pMFA.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pMFA) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "UserCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Me)
              'go through them, and keep only those with an allowed ID 
              For Each lItem In vCollection 
                Dim pUser As csUser = DirectCast(lItem, csUser) 
                If pUser.ID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pUser) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "UserLoginKeyCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pUserLoginKey As csUserLoginKey = DirectCast(lItem, csUserLoginKey) 
                If pUserLoginKey.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pUserLoginKey) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "UserPermissionCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pUserPermission As csUserPermission = DirectCast(lItem, csUserPermission) 
                If pUserPermission.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pUserPermission) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        Case "UserStatusCol" 
          Dim pCollection As New List(Of ITargCCEntity) 
          Select Case vRequester.UserIdentityType 
            Case clsEnums.enmUserIdentityType.c_User ' (Parent)
              'go through them, and keep only those with an allowed User ID 
              For Each lItem In vCollection 
                Dim pUserStatus As csUserStatus = DirectCast(lItem, csUserStatus) 
                If pUserStatus.UserID = vRequester.UserIdentityInstanceID Then 
                  pCollection.Add(pUserStatus) 
                End If 
              Next 
              rReturnedCollection = pCollection 
          End Select 
        End Select 
      End If 
 
    Return pFault 
  End Function 
 
End Class 
 
Public Class csAccessingEntity 
 
  'ReadWrite 
  Private _ApplicationName As String 'WebAppName 
  Private _ApplicationVersion As String 'WebURL 
  Private _ComputerMACAddress As String 
  Private _DnsGetHostName As String 
  Private _AddressList As String 
  Private _AccessingComputerDetails As String 'UserAgentAccessingWeb 
  Private _SystemDiskVolumeSerialNo As String 
  Private _ComputerIdentifier As String 
  Private _TotalPhysicalMemory As Long 
  Private _AvailablePhysicalMemory As Long 
  Private _UICulture As String 
  Private _EnvironmentMachineName As String 
  Private _EnvironmentUserDomainName As String 
  Private _EnvironmentUserName As String 'UserAccessingWeb 
  Private _ClientReportedDetails As String 
  Private _ClientReportedIP As String 
  Private _ClientReportedCtry As String 
  Private _WSReportedIP As String 
  Private _WSReportedCountry As String 
  Private _LocalTime As Date 
  Private _GmtTime As Date 
 
  Public Property ApplicationName() As String 
    Get 
      Return _ApplicationName 
    End Get 
    Set(ByVal value As String) 
      _ApplicationName = value 
    End Set 
  End Property 
  Public Property ApplicationVersion() As String 
    Get 
      Return _ApplicationVersion 
    End Get 
    Set(ByVal value As String) 
      _ApplicationVersion = value 
    End Set 
  End Property 
  Public Property ComputerMACAddress() As String 
    Get 
      Return _ComputerMACAddress 
    End Get 
    Set(ByVal value As String) 
      _ComputerMACAddress = value 
    End Set 
  End Property 
  Public Property DnsGetHostName() As String 
    Get 
      Return _DnsGetHostName 
    End Get 
    Set(ByVal value As String) 
      _DnsGetHostName = value 
    End Set 
  End Property 
  Public Property AddressList() As String 
    Get 
      Return _AddressList 
    End Get 
    Set(ByVal value As String) 
      _AddressList = value 
    End Set 
  End Property 
  Public Property AccessingComputerDetails() As String 
    Get 
      Return _AccessingComputerDetails 
    End Get 
    Set(ByVal value As String) 
      _AccessingComputerDetails = value 
    End Set 
  End Property 
  Public Property SystemDiskVolumeSerialNo() As String 
    Get 
      Return _SystemDiskVolumeSerialNo 
    End Get 
    Set(ByVal value As String) 
      _SystemDiskVolumeSerialNo = value 
    End Set 
  End Property 
  Public Property ComputerIdentifier() As String 
    Get 
      Return _ComputerIdentifier 
    End Get 
    Set(ByVal value As String) 
      _ComputerIdentifier = value 
    End Set 
  End Property 
  Public Property TotalPhysicalMemory() As Long 
    Get 
      Return _TotalPhysicalMemory 
    End Get 
    Set(ByVal value As Long) 
      _TotalPhysicalMemory = value 
    End Set 
  End Property 
  Public Property AvailablePhysicalMemory() As Long 
    Get 
      Return _AvailablePhysicalMemory 
    End Get 
    Set(ByVal value As Long) 
      _AvailablePhysicalMemory = value 
    End Set 
  End Property 
  Public Property UICulture() As String 
    Get 
      Return _UICulture 
    End Get 
    Set(ByVal value As String) 
      _UICulture = value 
    End Set 
  End Property 
  Public Property EnvironmentMachineName() As String 
    Get 
      Return _EnvironmentMachineName 
    End Get 
    Set(ByVal value As String) 
      _EnvironmentMachineName = value 
    End Set 
  End Property 
  Public Property EnvironmentUserDomainName() As String 
    Get 
      Return _EnvironmentUserDomainName 
    End Get 
    Set(ByVal value As String) 
      _EnvironmentUserDomainName = value 
    End Set 
  End Property 
  Public Property EnvironmentUserName() As String 
    Get 
      Return _EnvironmentUserName 
    End Get 
    Set(ByVal value As String) 
      _EnvironmentUserName = value 
    End Set 
  End Property 
  Public Property ClientReportedDetails() As String 
    Get 
      Return _ClientReportedDetails 
    End Get 
    Set(ByVal value As String) 
      _ClientReportedDetails = value 
    End Set 
  End Property 
  Public Property ClientReportedIP() As String 
    Get 
      Return _ClientReportedIP 
    End Get 
    Set(ByVal value As String) 
      _ClientReportedIP = value 
    End Set 
  End Property 
  Public Property ClientReportedCtry() As String 
    Get 
      Return _ClientReportedCtry 
    End Get 
    Set(ByVal value As String) 
      _ClientReportedCtry = value 
    End Set 
  End Property 
  Public Property WSReportedIP() As String 
    Get 
      Return _WSReportedIP 
    End Get 
    Set(ByVal value As String) 
      _WSReportedIP = value 
    End Set 
  End Property 
  Public Property WSReportedCountry() As String 
    Get 
      Return _WSReportedCountry 
    End Get 
    Set(ByVal value As String) 
      _WSReportedCountry = value 
    End Set 
  End Property 
  Public Property LocalTime() As Date 
    Get 
      Return _LocalTime 
    End Get 
    Set(ByVal value As Date) 
      _LocalTime = value 
    End Set 
  End Property 
  Public Property GmtTime() As Date 
    Get 
      Return _GmtTime 
    End Get 
    Set(ByVal value As Date) 
      _GmtTime = value 
    End Set 
  End Property 
 
  ''' <summary> 
  ''' Do not use. For json compatibility only 
  ''' </summary> 
  Public Sub New() 
    
  End Sub 
 
  Public Sub New(vLoadPCDetails As Boolean, vLoadIPAndCountry As Boolean, vRequester As clsRequester, ByRef rFault As clsFault) 
    CreateEmpty() 
    rFault = New clsFault() 
    If vLoadPCDetails Then 
      Try 
        LoadPCDetails() 
      Catch ex As Exception 
        rFault.LogException(ex, "", "TRGT-250910-144625", vRequester) 
        Return 
      End Try 
    End If 
    If vLoadIPAndCountry Then 
      rFault = LoadIPAndCountry(vRequester) 
    Else 
      rFault.SetOK() 
    End If 
  End Sub 
 
  Public Sub New(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
    LoadByteArray(vBytes, vFault, vRequester) 
  End Sub 
 
  'define computer of local 
  Private Sub LoadPCDetails() 
 
    Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
 
    Dim pComputerDetails As Dictionary(Of String, String) = ccHelper.GetComputerDetails() 
 
    If pComputerDetails.ContainsKey("MACAddress") Then 
      Dim pAdapterType As String = "CantGetAdapterType" 
      If pComputerDetails.ContainsKey("AdapterType") Then pAdapterType = pComputerDetails("AdapterType") 
      _ComputerMACAddress = $"{pComputerDetails("MACAddress").Replace(":", "-")}{Environment.NewLine}{pAdapterType}" 
    Else 
      _ComputerMACAddress = $"No network interface" 
    End If 
    _DnsGetHostName = System.Net.Dns.GetHostName 
    If pComputerDetails.ContainsKey("IPAddresses") Then 
      _AddressList = pComputerDetails("IPAddresses") 
    Else 
      _AddressList = "None" 
    End If 
 
    Try 
      Dim pIPGlobalProperties As Net.NetworkInformation.IPGlobalProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties() 
      If String.IsNullOrEmpty(pIPGlobalProperties.DomainName) Then 
        _EnvironmentMachineName = $"{pIPGlobalProperties.HostName}" 
      Else 
        _EnvironmentMachineName = $"{pIPGlobalProperties.HostName}.{pIPGlobalProperties.DomainName}" 
      End If 
    Catch ex As Exception 
      If Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase) Then 
        _EnvironmentMachineName = $"{Environment.MachineName}" 
      Else 
        _EnvironmentMachineName = $"{Environment.MachineName}.{Environment.UserDomainName}" 
      End If 
    End Try 
 
    Dim pBIOSSerialNumber As String = "CantGetBIOSSerialNumber" 
    If pComputerDetails.ContainsKey("BIOSSerialNumber") Then pBIOSSerialNumber = pComputerDetails("BIOSSerialNumber") 
    Dim pProcessorId As String = "CantGetProcessorId" 
    If pComputerDetails.ContainsKey("ProcessorId") Then pProcessorId = pComputerDetails("ProcessorId") 
    Dim pVolumeSerialNumber As String = "CantGetVolumeSerialNumber" 
    If pComputerDetails.ContainsKey("VolumeSerialNumber") Then pVolumeSerialNumber = pComputerDetails("VolumeSerialNumber") 
    Dim pComputerIdentifier As String = pBIOSSerialNumber & "|" & pProcessorId & "|" & pVolumeSerialNumber.Replace(" ", "") 
    _ComputerIdentifier = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, pComputerIdentifier) 
    Dim pComputerIdentifierToDisplay As String = pBIOSSerialNumber & Environment.NewLine & pProcessorId & Environment.NewLine & pVolumeSerialNumber.Replace(" ", "") 
 
 
    Dim pOSName As String = "CantGetOSName" 
    If pComputerDetails.ContainsKey("OSName") Then pOSName = pComputerDetails("OSName") 
    Dim pLastBootUpTime As String = "CantGetLastBootUpTime" 
    If pComputerDetails.ContainsKey("LastBootUpTime") Then pLastBootUpTime = pComputerDetails("LastBootUpTime") 
    Dim pVideoMode As String = "CantGetVideoMode" 
    If pComputerDetails.ContainsKey("VideoMode") Then pVideoMode = pComputerDetails("VideoMode") 
    Dim pManufacturer As String = "CantGetManufacturer" 
    If pComputerDetails.ContainsKey("Manufacturer") Then pManufacturer = pComputerDetails("Manufacturer") 
    Dim pModel As String = "CantGetModel" 
    If pComputerDetails.ContainsKey("Model") Then pModel = pComputerDetails("Model") 
    Dim pProcessor As String = "CantGetProcessor" 
    If pComputerDetails.ContainsKey("Processor") Then pProcessor = pComputerDetails("Processor") 
 
    _AccessingComputerDetails = pOSName & Environment.NewLine & "Up: " & pLastBootUpTime & Environment.NewLine & pVideoMode & Environment.NewLine & pManufacturer & ", " & pModel & Environment.NewLine & pProcessor 
    '_AccessingComputerDetails &= Environment.NewLine & pComputerIdentifierToDisplay 
 
    If String.IsNullOrEmpty(_ApplicationName) Then _ApplicationName = pFileDetails.AssemblyName 
    _ApplicationVersion = pFileDetails.Version 
    _SystemDiskVolumeSerialNo = pVolumeSerialNumber.Split(":"c)(1) 
 
    Dim pTotalPhysicalMemory As String = "0" 
    If pComputerDetails.ContainsKey("TotalPhysicalMemory") Then pTotalPhysicalMemory = pComputerDetails("TotalPhysicalMemory") 
    Dim pFreePhysicalMemory As String = "0" 
    If pComputerDetails.ContainsKey("FreePhysicalMemory") Then pFreePhysicalMemory = pComputerDetails("FreePhysicalMemory") 
 
    _TotalPhysicalMemory = ccHelper.ToLong(pTotalPhysicalMemory) 
    _AvailablePhysicalMemory = ccHelper.ToLong(pFreePhysicalMemory) 
    Try 
      _UICulture = $"{System.Globalization.CultureInfo.CurrentCulture.Name} (UI: {System.Globalization.CultureInfo.CurrentUICulture.Name})" 
    Catch ex As Exception 
      _UICulture = ex.Message 
    End Try 
    _EnvironmentUserDomainName = Environment.UserDomainName 
    _EnvironmentUserName = Environment.UserName 
    _LocalTime = DateTime.Now.ToLocalTime 
    _GmtTime = DateTime.Now.ToUniversalTime 
 
  End Sub 
 
  Private Function LoadIPAndCountry(vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      _ClientReportedCtry = "LoadIPAndCountry was not called" 
      Return New clsFault().SetOK() 
    End If 
 
    Dim pIpifyURL As New csSystemDefault() 
    pFault = pIpifyURL.GetByGroupAndSettingName("Config", "LocationIpifyURL", vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
 
    If String.IsNullOrEmpty(pIpifyURL.SettingValue) Then 
      _ClientReportedCtry = "Ipify was not called" 
      Return pFault 
    End If 
 
    Dim pProxyCheckURL As New csSystemDefault() 
    pFault = pProxyCheckURL.GetByGroupAndSettingName("Config", "LocationProxyCheckURL", vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
    Dim pProxyCheckKey As New csSystemDefault() 
    pFault = pProxyCheckKey.GetByGroupAndSettingName("Config", "LocationProxyCheckKey", vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
 
    Dim pIpiFy As New Tools.IPThreatAndLocator(pIpifyURL.SettingValue) 
 
    Dim pResponse As String = "" 
    pResponse = pIpiFy.GetMyIpOnly() 
    If pResponse <> "OK" Then 
      pFault.LogFreeTextFault(3, $"{pResponse}", "", "TRGT-250831-174731", vRequester) 
      _ClientReportedIP = "0.0.0.0" 
      Return pFault.SetOK() 
    Else 
      _ClientReportedIP = pIpiFy.IP 
    End If 
 
    If String.IsNullOrEmpty(pProxyCheckURL.SettingValue) Then 
      _ClientReportedCtry = "PCNC" 
      Return pFault 
    End If

    Dim pProxyCheck As New Tools.IPThreatAndLocator(pProxyCheckURL.SettingValue, pProxyCheckKey.SettingValue)

    pResponse = pProxyCheck.GetIpInfo(_ClientReportedIP) 
    If pResponse <> "OK" Then 
      pFault.LogFreeTextFault(3, $"{pResponse}", "", "TRGT-250831-174732", vRequester) 
      _ClientReportedCtry = "UD(F)" 
    Else 
      _ClientReportedCtry = pProxyCheck.CountryCode 
      Dim pDetails As New Text.StringBuilder() 
      pDetails.Append("Country: " & pProxyCheck.CountryCode & ",") 
      pDetails.Append("IP: " & pProxyCheck.IP & ",") 
      pDetails.Append("IsVPN: " & pProxyCheck.IsVpn & ",") 
      pDetails.Append("IsTor: " & pProxyCheck.IsTor & ",") 
      pDetails.Append("IpType: " & pProxyCheck.IpType & ",") 
      pDetails.Append("IsCloudProvider: " & pProxyCheck.IsCloudProvider & ",") 
      pDetails.Append("IsProxy: " & pProxyCheck.IsProxy & ",") 
      pDetails.Append("IsAnonymous: " & pProxyCheck.IsAnonymous & ",") 
      pDetails.Append("IsAbuser: " & pProxyCheck.IsAbuser & ",") 
      pDetails.Append("RiskLevel: " & pProxyCheck.RiskLevel & ",") 
      pDetails.Append("CurrencyCode: " & pProxyCheck.CurrencyCode & "") 
      _ClientReportedDetails = pDetails.ToString() 
    End If 
 
    Return pFault 
  End Function 
 
  Friend Function LoadDetailsForWSReportedIP(vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If MyController.DBType = MyController.enmDBType.FileSystem Then 
      _ClientReportedCtry = "LoadDetailsForWSReportedIP was not called" 
      Return New clsFault().SetOK() 
    End If 
 
    Dim pProxyCheckURL As New csSystemDefault() 
    pFault = pProxyCheckURL.GetByGroupAndSettingName("Config", "LocationProxyCheckURL", vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
    Dim pProxyCheckKey As New csSystemDefault() 
    pFault = pProxyCheckKey.GetByGroupAndSettingName("Config", "LocationProxyCheckKey", vRequester, vMustExist:=True) : If pFault.isOK = False Then Return pFault 
 
    Dim pResponse As String = "" 
 
    If String.IsNullOrEmpty(pProxyCheckURL.SettingValue) Then 
      _ClientReportedDetails = "Function was not called" 
      Return pFault 
    End If 
 
    Dim pProxyCheck As New Tools.IPThreatAndLocator(pProxyCheckURL.SettingValue, pProxyCheckKey.SettingValue) 
 
    pResponse = pProxyCheck.GetIpInfo(_WSReportedIP) 
    If pResponse <> "OK" Then 
      pFault.LogFreeTextFault(3, $"{pResponse}", "", "TRGT-250911-232122", vRequester) 
      _ClientReportedDetails = "Failed" 
    Else 
      Dim pDetails As New Text.StringBuilder() 
      pDetails.Append("Country: " & pProxyCheck.CountryCode & ",") 
      pDetails.Append("IP: " & pProxyCheck.IP & ",") 
      pDetails.Append("IsVPN: " & pProxyCheck.IsVpn & ",") 
      pDetails.Append("IsTor: " & pProxyCheck.IsTor & ",") 
      pDetails.Append("IpType: " & pProxyCheck.IpType & ",") 
      pDetails.Append("IsCloudProvider: " & pProxyCheck.IsCloudProvider & ",") 
      pDetails.Append("IsProxy: " & pProxyCheck.IsProxy & ",") 
      pDetails.Append("IsAnonymous: " & pProxyCheck.IsAnonymous & ",") 
      pDetails.Append("IsAbuser: " & pProxyCheck.IsAbuser & ",") 
      pDetails.Append("RiskLevel: " & pProxyCheck.RiskLevel & ",") 
      pDetails.Append("CurrencyCode: " & pProxyCheck.CurrencyCode & "") 
      _ClientReportedDetails = pDetails.ToString() 'pProxyCheck.CountryCode  
    End If 
 
    Return pFault 
  End Function 
 
  Public Function CreateByteArray(ByVal vFault As clsFault, ByVal vRequester As clsRequester) As Byte() 
    Dim pFunctionParameters As String = "" 
 
    vFault.ClearOK() 
    Dim pBytes As Byte() = Nothing 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          Dim pLength As Integer = 0 
          pBinaryWriter.Write(_ApplicationName) 
          pBinaryWriter.Write(_ApplicationVersion) 
          pBinaryWriter.Write(_ComputerMACAddress) 
          pBinaryWriter.Write(_DnsGetHostName) 
          pBinaryWriter.Write(_AddressList) 
          pBinaryWriter.Write(_AccessingComputerDetails) 'UserAgentAccessingWeb  
          pBinaryWriter.Write(_SystemDiskVolumeSerialNo) 
          pBinaryWriter.Write(_ComputerIdentifier) 
          pBinaryWriter.Write(_TotalPhysicalMemory) 
          pBinaryWriter.Write(_AvailablePhysicalMemory) 
          pBinaryWriter.Write(_UICulture) 
          pBinaryWriter.Write(_EnvironmentMachineName) 
          pBinaryWriter.Write(_EnvironmentUserDomainName) 
          pBinaryWriter.Write(_EnvironmentUserName) 'UserAccessingWeb  
          pBinaryWriter.Write(_ClientReportedDetails) 
          pBinaryWriter.Write(_ClientReportedIP) 
          pBinaryWriter.Write(_ClientReportedCtry) 
          pBinaryWriter.Write(_WSReportedIP) 
          pBinaryWriter.Write(_WSReportedCountry) 
          pBinaryWriter.Write(_LocalTime.Ticks) 
          pBinaryWriter.Write(_GmtTime.Ticks) 
          pBinaryWriter.Close() 
        End Using 
        pBytes = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      vFault.LogException(ex, pFunctionParameters, "TRGT-150314-1155", vRequester) 
    End Try 
 
    Return pBytes 
  End Function 
  Private Sub LoadByteArray(ByVal vBytes As Byte(), ByVal vFault As clsFault, ByVal vRequester As clsRequester) 
 
    vFault.ClearOK() 
    Try 
      If vFault Is Nothing Then Throw New Exception("You must initialize the clsFault object before submitting it") 'record it 
      Using pMemoryStream As New System.IO.MemoryStream(vBytes) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pLength As Integer = 0 
          _ApplicationName = pReader.ReadString 'WebAppName  
          _ApplicationVersion = pReader.ReadString 'WebURL  
          _ComputerMACAddress = pReader.ReadString 
          _DnsGetHostName = pReader.ReadString 
          _AddressList = pReader.ReadString 
          _AccessingComputerDetails = pReader.ReadString 'UserAgentAccessingWeb  
          _SystemDiskVolumeSerialNo = pReader.ReadString 
          _ComputerIdentifier = pReader.ReadString 
          _TotalPhysicalMemory = pReader.ReadInt64 
          _AvailablePhysicalMemory = pReader.ReadInt64 
          _UICulture = pReader.ReadString 
          _EnvironmentMachineName = pReader.ReadString 
          _EnvironmentUserDomainName = pReader.ReadString 
          _EnvironmentUserName = pReader.ReadString 'UserAccessingWeb  
          _ClientReportedDetails = pReader.ReadString 
          _ClientReportedIP = pReader.ReadString 
          _ClientReportedCtry = pReader.ReadString 
          _WSReportedIP = pReader.ReadString 
          _WSReportedCountry = pReader.ReadString 
          _LocalTime = New Date(pReader.ReadInt64) 
          _GmtTime = New Date(pReader.ReadInt64) 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
      vFault.SetOK() 
    Catch ex As Exception 
      CreateEmpty() 
      vFault.LogException(ex, "", "TRGT-150314-1156", vRequester) 
    End Try 
  End Sub 
 
  Sub CreateEmpty() 
    _ApplicationName = "" 
    _ApplicationVersion = "" 
    _ComputerMACAddress = "" 
    _DnsGetHostName = "" 
    _AddressList = "" 
    _AccessingComputerDetails = "" 
    _SystemDiskVolumeSerialNo = "" 
    _ComputerIdentifier = "" 
    _TotalPhysicalMemory = 0 
    _AvailablePhysicalMemory = 0 
    _UICulture = "" 
    _EnvironmentMachineName = "" 
    _EnvironmentUserDomainName = "" 
    _EnvironmentUserName = "" 
    _ClientReportedDetails = "" 
    _ClientReportedIP = "" 
    _ClientReportedCtry = "" 
    _WSReportedIP = "" 
    _WSReportedCountry = "" 
    _LocalTime = Nothing 
    _GmtTime = Nothing 
  End Sub 
 
End Class 
