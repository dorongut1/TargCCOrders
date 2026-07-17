Imports System.Security.Cryptography 
Imports System.Threading 
 
Public Class ccSecurity 
  
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
  ''' The AccessingEntity must be provided, if hosted by a web application  
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
  ''' <returns></returns> 
  Public Shared Function LogInByNamePwd(ByVal vUserName As String, ByVal vPassword As String, ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vSendMessageFor2FA As Boolean = True, Optional ByVal vSendMessageOnPasswordExpiry As Boolean = True, Optional ByVal vAccessingEntity As csAccessingEntity = Nothing, Optional ByVal vNewPassword As String = "") As clsFault 
    Dim pFunctionParameters As String = "" 
    pFunctionParameters = String.Format("UserName={0},Password={1},Calling Assembly={2}", vUserName, "***", System.Reflection.Assembly.GetCallingAssembly().GetName().Name) 
 
    Dim pFault As New clsFault 
 
    rRequester = New clsRequester 
 
    Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
    Dim pIsInWeb As Boolean = False 
    If pFileDetails.BinaryLocation.IndexOf("Temporary ASP.NET Files", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      pIsInWeb = True 
    ElseIf IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
      pIsInWeb = True 
    ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
      'One of these:  
      'NLog.Web.AspNetCore  
      'Microsoft.AspNetCore.Server.IIS  
      'Microsoft.AspNetCore.WebUtilities  
      pIsInWeb = True 
    End If 
 
 
    If vPassword.Length <> 64 Then 
      vPassword = NETEncryption.clsHash.Hash(vPassword, NETEncryption.clsHash.HashName.SHA256) 
    Else 
      'allow the hashed password to pass only if it's hosted by DBController   
      Dim pFound As Boolean = False 
      For Each l In AppDomain.CurrentDomain.GetAssemblies() 
        Dim pName As String = l.GetName.Name 
        pName = pName.Split("."c)(pName.Split("."c).Length - 1) 
        If pName = "DBController" Then 
          pFound = True 
          Exit For 
        End If 
      Next 
      If pFound = False Then 
        'Throw New Exception("Invalid password type for this host")  
        Return pFault.LogFreeTextFault("Invalid password type for this host", pFunctionParameters, "TRGT-160129-1537", rRequester) 
      End If 
    End If 
 
    If pIsInWeb = False Then 
      'I load the PCDetails  
      If vAccessingEntity Is Nothing Then 
        Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
        vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      End If 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
    Else 
      'expected AccessingEntity   
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    'check that we are using the right one   
    If Not ((MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
             MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
             MyController.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      'Improper login method used by application    
      pFault.LogFreeTextFault(95, "", pFunctionParameters, "TRGT-110421-133656", rRequester) 
      Return pFault 
    End If 
 
    'Create Byte Array for AccessingEntity   
    Dim pAccessingEntityBytes As Byte() 
    pAccessingEntityBytes = vAccessingEntity.CreateByteArray(pFault, rRequester) : If Not pFault.isOK Then Return pFault 
 
    Try 
      'Prepare the variables 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pLength As Integer 
          pBinaryWriter.Write(vUserName) 
          pBinaryWriter.Write(vPassword) 
          pBinaryWriter.Write(vOverrideUILang.FastToString()) 
          pBinaryWriter.Write(vSendMessageFor2FA) 
          pBinaryWriter.Write(vSendMessageOnPasswordExpiry) 
          pLength = pAccessingEntityBytes.Length 
          pBinaryWriter.Write(pLength) 
          pBinaryWriter.Write(pAccessingEntityBytes, 0, pLength) 
          pBinaryWriter.Write(vNewPassword) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Use for home-made certificates, issued by a CA on your company server   
      If MyController.ServerRequiresSSL = True Then 
        Dim pIssuer As String = "" 
        Try 
          pIssuer = MyController.SSLCertificateIssuer(MyController.APIServerNumber) 
        Catch ex As Exception 
          pIssuer = "" 
        End Try 
        If String.IsNullOrEmpty(pIssuer) = False AndAlso Not pIssuer.Equals("notoverridden", StringComparison.OrdinalIgnoreCase) Then 
          System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf WebAPI.customCertValidation) 
        End If 
      End If 
 
      'Run the request 
      Dim pFunction As String = "ccSecurityLogInByNamePwd" 
      Dim pParametersToLog = $"UserName: {vUserName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, rRequester) : If Not pFault.isOK Then Return pFault 
 
      'Check that the requester is valid 
      If rRequester.LoggedLoginID = 0 Then 
        Return pFault.LogFreeTextFault(85, "Login Failed", pFunctionParameters, "TRGT-150314-1217", rRequester) 
      End If 
 
      rRequester.CallingFunctionWithinApplication = "Initial Login" 
      pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150314-1216", rRequester) 
    End Try 
 
    pFault = CheckWSControllerVersion(rRequester) : If Not pFault.isOK Then Return pFault 
 
    rRequester.CallingFunctionWithinApplication = "" 
    Return pFault 
    'We are now logged in   
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
    pFunctionParameters = String.Format("UserName={0},Password={1},Calling Assembly={2}", vUserName, vEmail, System.Reflection.Assembly.GetCallingAssembly().GetName().Name) 
 
    Dim pFault As New clsFault 
 
    rRequester = New clsRequester 
 
    Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
    Dim pIsInWeb As Boolean = False 
    If pFileDetails.BinaryLocation.IndexOf("Temporary ASP.NET Files", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      pIsInWeb = True 
    ElseIf IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
      pIsInWeb = True 
    ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
      'One of these:   
      'NLog.Web.AspNetCore   
      'Microsoft.AspNetCore.Server.IIS   
      'Microsoft.AspNetCore.WebUtilities   
      pIsInWeb = True 
    End If 
 
 
    If pIsInWeb = False Then 
      'I load the PCDetails   
      If vAccessingEntity Is Nothing Then 
        Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
        vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      End If 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
    Else 
      'expected AccessingEntity    
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    'check that we are using the right one    
    If Not ((MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
             MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
             MyController.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      'Improper login method used by application     
      pFault.LogFreeTextFault(95, "", pFunctionParameters, "TRGT-110421-133656", rRequester) 
      Return pFault 
    End If 
 
    'Create Byte Array for AccessingEntity    
    Dim pAccessingEntityBytes As Byte() 
    pAccessingEntityBytes = vAccessingEntity.CreateByteArray(pFault, rRequester) : If Not pFault.isOK Then Return pFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pLength As Integer 
          pBinaryWriter.Write(vUserName) 
          pBinaryWriter.Write(vEmail) 
          pBinaryWriter.Write(vOverrideUILang.FastToString()) 
          pLength = pAccessingEntityBytes.Length 
          pBinaryWriter.Write(pLength) 
          pBinaryWriter.Write(pAccessingEntityBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Use for home-made certificates, issued by a CA on your company server    
      If MyController.ServerRequiresSSL = True Then 
        Dim pIssuer As String = "" 
        Try 
          pIssuer = MyController.SSLCertificateIssuer(MyController.APIServerNumber) 
        Catch ex As Exception 
          pIssuer = "" 
        End Try 
        If String.IsNullOrEmpty(pIssuer) = False AndAlso Not pIssuer.Equals("notoverridden", StringComparison.OrdinalIgnoreCase) Then 
          System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf WebAPI.customCertValidation) 
        End If 
      End If 
 
      'Run the request  
      Dim pFunction As String = "ccSecurityLogInByOTP" 
      Dim pParametersToLog = $"UserName: {vUserName};Email: {vEmail};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, rRequester) : If Not pFault.isOK Then Return pFault 
 
      'Check that the requester is valid  
      If rRequester.LoggedLoginID = 0 Then 
        Return pFault.LogFreeTextFault(85, "Login Failed", pFunctionParameters, "TRGT-150314-1217", rRequester) 
      End If 
 
      rRequester.CallingFunctionWithinApplication = "Initial Login" 
      pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150314-1216", rRequester) 
    End Try 
 
    pFault = CheckWSControllerVersion(rRequester) : If Not pFault.isOK Then Return pFault 
 
    rRequester.CallingFunctionWithinApplication = "" 
    Return pFault 
    'We are now logged in    
  End Function 
 
  Public Shared Function LogInByBiometric(vApplicationIdentifier As String, vKey As String, ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vAccessingEntity As csAccessingEntity = Nothing) As clsFault 
    Dim pFunctionParameters As String = "" 
    pFunctionParameters = String.Format("vApplicationIdentifier={0},vKey={1},Calling Assembly={2}", vApplicationIdentifier, "***", System.Reflection.Assembly.GetCallingAssembly().GetName().Name) 
 
    Dim pFault As New clsFault 
 
    rRequester = New clsRequester 
 
    Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
    Dim pIsInWeb As Boolean = False 
    If pFileDetails.BinaryLocation.IndexOf("Temporary ASP.NET Files", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      pIsInWeb = True 
    ElseIf IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
      pIsInWeb = True 
    ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
      'One of these:   
      'NLog.Web.AspNetCore   
      'Microsoft.AspNetCore.Server.IIS   
      'Microsoft.AspNetCore.WebUtilities   
      pIsInWeb = True 
    End If 
 
 
    If pIsInWeb = False Then 
      'I load the PCDetails   
      If vAccessingEntity Is Nothing Then 
        Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
        vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      End If 
      pFunctionParameters = String.Format("vApplicationIdentifier={0},vKey={1},AccessingEntity.ApplicationName={2}", vApplicationIdentifier, "***", vAccessingEntity.ApplicationName) 
    Else 
      'expected AccessingEntity    
      If vAccessingEntity Is Nothing Then 
        Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
      End If 
    End If 
 
    'check that we are using the right one    
    If Not ((MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials OrElse 
             MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.None) AndAlso 
             MyController.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByApplicationUser) Then 
      'Improper login method used by application     
      pFault.LogFreeTextFault(95, "", pFunctionParameters, "TRGT-110421-133656", rRequester) 
      Return pFault 
    End If 
 
    'Create Byte Array for AccessingEntity    
    Dim pAccessingEntityBytes As Byte() 
    pAccessingEntityBytes = vAccessingEntity.CreateByteArray(pFault, rRequester) : If Not pFault.isOK Then Return pFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pLength As Integer 
          pBinaryWriter.Write(vApplicationIdentifier) 
          pBinaryWriter.Write(vKey) 
          pBinaryWriter.Write(vOverrideUILang.FastToString()) 
          pLength = pAccessingEntityBytes.Length 
          pBinaryWriter.Write(pLength) 
          pBinaryWriter.Write(pAccessingEntityBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Use for home-made certificates, issued by a CA on your company server    
      If MyController.ServerRequiresSSL = True Then 
        Dim pIssuer As String = "" 
        Try 
          pIssuer = MyController.SSLCertificateIssuer(MyController.APIServerNumber) 
        Catch ex As Exception 
          pIssuer = "" 
        End Try 
        If String.IsNullOrEmpty(pIssuer) = False AndAlso Not pIssuer.Equals("notoverridden", StringComparison.OrdinalIgnoreCase) Then 
          System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf WebAPI.customCertValidation) 
        End If 
      End If 
 
      'Run the request  
      Dim pFunction As String = "ccSecurityLogInByBiometric" 
      Dim pParametersToLog = $"Parameters: Restricted;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, rRequester) : If Not pFault.isOK Then Return pFault 
 
      'Check that the requester is valid  
      If rRequester.LoggedLoginID = 0 Then 
        Return pFault.LogFreeTextFault(85, "Login Failed", pFunctionParameters, "TRGT-150314-1217", rRequester) 
      End If 
 
      rRequester.CallingFunctionWithinApplication = "Initial Login" 
      pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150314-1216", rRequester) 
    End Try 
 
    pFault = CheckWSControllerVersion(rRequester) : If Not pFault.isOK Then Return pFault 
 
    rRequester.CallingFunctionWithinApplication = "" 
    Return pFault 
    'We are now logged in    
  End Function 
 
  ''' <summary> 
  ''' This assumes application in UserNamePwd or NetworkName mode     
  ''' If used by a web application, you must provide the vParentApp and RemoteIP  
  ''' </summary> 
  ''' <param name="rRequester"></param> 
  ''' <param name="vOverrideUILang"></param> 
  ''' <param name="vUserName"></param> 
  ''' <param name="vPassword"></param> 
  ''' <param name="vAccessingEntity"></param> 
  ''' <param name="vExternalIP"></param> 
  ''' <returns></returns> 
  Public Shared Function LogInByNetworkCredentials(ByRef rRequester As clsRequester, Optional vOverrideUILang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD, Optional ByVal vUserName As String = "", Optional ByVal vPassword As String = "", Optional ByVal vAccessingEntity As csAccessingEntity = Nothing, Optional ByVal vExternalIP As String = "") As clsFault 
    Dim pFunctionParameters As String = String.Format("UserName={0},Calling Assembly={1}", vUserName, System.Reflection.Assembly.GetCallingAssembly().GetName().Name) 
 
    Dim pFault As New clsFault 
    rRequester = New clsRequester 
 
    Dim pFileDetails As ccHelper.FileDetails = ccHelper.GetEntryAssemblyDetails() 
    Dim pIsInWeb As Boolean = False 
    If pFileDetails.BinaryLocation.IndexOf("Temporary ASP.NET Files", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      pIsInWeb = True 
    ElseIf IO.Directory.Exists(pFileDetails.BinaryLocation & "wwwroot") Then 
      pIsInWeb = True 
    ElseIf Debugger.IsAttached AndAlso ccHelper.DoesAssemblyExist("Microsoft.AspNetCore.Server.IIS") Then 
      'One of these:  
      'NLog.Web.AspNetCore  
      'Microsoft.AspNetCore.Server.IIS  
      'Microsoft.AspNetCore.WebUtilities  
      pIsInWeb = True 
    End If 
 
    If pIsInWeb = False Then 
      'I load the PCDetails  
      Dim pTmpRequester As New clsRequester("SystemDefault", "GetAccessingEntityIPsAndCountriesView", False) 
      vAccessingEntity = New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, pTmpRequester, pFault) : If Not pFault.isOK Then Return pFault 
      pFunctionParameters = String.Format("vUserName={0},vPassword={1},AccessingEntity.ApplicationName={2}", vUserName, "***", vAccessingEntity.ApplicationName) 
    Else 
      'expected AccessingEntity   
      Return pFault.LogFreeTextFault("If WSController is hosted in a web application, then it must be provided an AccessingEntity.", pFunctionParameters, "TRGT-201014-1149", rRequester) 
    End If 
 
 
    'check that we are using the right one  
    If Not (MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials OrElse 
        MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials) Then 
      pFault.LogFreeTextFault(95, "", pFunctionParameters, "TRGT-110421-144851", rRequester) 
      Return pFault 
    End If 
 
    'Create Byte Array for AccessingEntity    
    Dim pAccessingEntityBytes As Byte() 
    pAccessingEntityBytes = vAccessingEntity.CreateByteArray(pFault, rRequester) : If Not pFault.isOK Then Return pFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          Dim pLength As Integer 
          pBinaryWriter.Write(vOverrideUILang.FastToString()) 
          pLength = pAccessingEntityBytes.Length 
          pBinaryWriter.Write(pLength) 
          pBinaryWriter.Write(pAccessingEntityBytes, 0, pLength) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Use for home-made certificates, issued by a CA on your company server   
      If MyController.ServerRequiresSSL = True Then 
        Dim pIssuer As String = "" 
        Try 
          pIssuer = MyController.SSLCertificateIssuer(MyController.APIServerNumber) 
        Catch ex As Exception 
          pIssuer = "" 
        End Try 
        If String.IsNullOrEmpty(pIssuer) = False AndAlso Not pIssuer.Equals("notoverridden", StringComparison.OrdinalIgnoreCase) Then 
          System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf WebAPI.customCertValidation) 
        End If 
      End If 
 
      'Run the request  
      Dim pFunction As String = "ccSecurityLogInByNetworkCredentials" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, rRequester) : If Not pFault.isOK Then Return pFault  
 
      'Check that the requester is valid  
      If rRequester.LoggedLoginID = 0 Then 
        Return pFault.LogFreeTextFault(85, "Login Failed", pFunctionParameters, "TRGT-150314-1217", rRequester) 
      End If 
 
      rRequester.CallingFunctionWithinApplication = "Initial Login" 
      pFault = ccHelper.LoadLanguageCache(rRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150314-1216", rRequester) 
    End Try 
 
    pFault = CheckWSControllerVersion(rRequester) : If Not pFault.isOK Then Return pFault 
 
    rRequester.CallingFunctionWithinApplication = "" 
    Return pFault 
    'We are now logged in  
  End Function 
 
  Public Shared Function Check2FactorAuthenticationForLogin(ByVal vCode As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}, LoggedLoginID={1}", vRequester.UserName, vRequester.LoggedLoginID) 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vCode) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "ccSecurityCheck2FactorAuthenticationForLogin" 
      Dim pParametersToLog = $"Parameters: Restricted;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1247", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function CreateBiometricKeyWithLastOTPForNewUser(vCellOrEmail As String, vOTP As String, vApplicationName As String, vApplicationIdentifier As String, vAccessingIP As String, vAccessingCountry As String, vRequester As clsRequester, ByRef rKey As String) As clsFault 
    Dim pFunctionParameters As String = $"vCellOrEmail: {vCellOrEmail}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    rKey = "" 
 
    Try 
      'Prepare the variables      
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request      
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vCellOrEmail) 
          pBinaryWriter.Write(vOTP) 
          pBinaryWriter.Write(vApplicationName) 
          pBinaryWriter.Write(vApplicationIdentifier) 
          pBinaryWriter.Write(vAccessingIP) 
          pBinaryWriter.Write(vAccessingCountry) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request      
      Dim pFunction As String = "ccSecurityCreateBiometricKeyWithLastOTPForNewUser" 
      Dim pParametersToLog = $"CellOrEmail: {vCellOrEmail};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      rKey = System.Text.Encoding.ASCII.GetString(pResponse) 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-250907-183723", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function CreateBiometricKeyWithLastOTPForExistingUser(vUserName As String, vOTP As String, vApplicationName As String, vApplicationIdentifier As String, vAccessingIP As String, vAccessingCountry As String, vRequester As clsRequester, ByRef rKey As String) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    rKey = "" 
 
    Try 
      'Prepare the variables     
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request     
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vUserName) 
          pBinaryWriter.Write(vOTP) 
          pBinaryWriter.Write(vApplicationName) 
          pBinaryWriter.Write(vApplicationIdentifier) 
          pBinaryWriter.Write(vAccessingIP) 
          pBinaryWriter.Write(vAccessingCountry) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request     
      Dim pFunction As String = "ccSecurityCreateBiometricKeyWithLastOTPForExistingUser" 
      Dim pParametersToLog = $"UserName: {vUserName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      rKey = System.Text.Encoding.ASCII.GetString(pResponse) 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-250907-183723", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function CreateBiometricKey(vUserName As String, vApplicationName As String, vApplicationIdentifier As String, vRequester As clsRequester, ByRef rKey As String) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}, vApplicationName: {vApplicationName}" 
    Dim pFault As New clsFault 
 
    rKey = "" 
 
    Try 
      'Prepare the variables    
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request    
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vUserName) 
          pBinaryWriter.Write(vApplicationName) 
          pBinaryWriter.Write(vApplicationIdentifier) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request    
      Dim pFunction As String = "ccSecurityCreateBiometricKey" 
      Dim pParametersToLog = $"UserName: {vUserName};ApplicationName: {vApplicationName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
      rKey = System.Text.Encoding.ASCII.GetString(pResponse) 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-250907-183723", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function RemoveAllBiometricKeys(vUserName As String, vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = $"vUserName: {vUserName}" 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables    
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request    
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vUserName) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request    
      Dim pFunction As String = "ccSecurityRemoveAllBiometricKeys" 
      Dim pParametersToLog = $"UserName: {vUserName};" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1247", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function LogOut(ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}, LoggedLoginID={1}", vRequester.UserName, vRequester.LoggedLoginID) 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 'we need something so it's not empty 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccSecurityLogOut" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1247", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Private Shared Function GetMinimumWSControllerVersion(ByRef rMinimumWSControllerVersion As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("User={0}", vRequester.UserName) 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write("Dummy") 'we need something so it's not empty  
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "ccSecurityGetMinimumWSControllerVersion" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
 
      rMinimumWSControllerVersion = System.Text.Encoding.ASCII.GetString(pResponse) 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150424-1247", vRequester) 
    End Try 
 
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
  
 
  Private Shared Function CheckWSControllerVersion(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
    Dim pRequiredWSVersion As String = "" 
 
    Dim pSystemDefaultMinWSControllerVersion As String = "" 
    pFault = ccSecurity.GetMinimumWSControllerVersion(pSystemDefaultMinWSControllerVersion, vRequester) : If Not pFault.isOK Then Return pFault 
 
 
    'Now get the existing version  
    Dim pThisAssembly As String = System.Reflection.Assembly.GetExecutingAssembly.FullName 
    Dim pAssemblyVersion As String = "" 
 
    Try 
      pAssemblyVersion = pThisAssembly.Split(","c)(1).Split("="c)(1) 
    Catch ex As Exception 
      pAssemblyVersion = pThisAssembly & ":" & ex.Message 
    End Try 
 
    If New Version(pAssemblyVersion) < New Version(pSystemDefaultMinWSControllerVersion) Then 
      pFault = New clsFault 
      Dim pMessage As String = $"Invalid WSController Version. I am {pAssemblyVersion} on {ccHelper.GetComputerName}, the database requires that I be at least {pSystemDefaultMinWSControllerVersion}" 
      pFault.LogFreeTextFault(6, pMessage, $"Ver: {pAssemblyVersion}{Environment.NewLine}AssemblyName: {ccHelper.GetEntryAssemblyDetails.AssemblyName}{Environment.NewLine}BinaryLocation: {ccHelper.GetEntryAssemblyDetails.BinaryLocation}", "TRGT-160430-1356", vRequester) 
    End If 
    Return pFault 
  End Function 
 
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
    Dim pFault As New clsFault 
 
    If vApprovalMethod <> enmApprovalMethod.ApproveSendCodeOnly Then 
      Return pFault.LogFreeTextFault("Only ApproveSendCodeOnly can be used by WS", pFunctionParameters, "TRGT-230520-1527", vRequester) 
    End If 
 
    Try 
      'Prepare the variables     
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request     
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vFunctionParams) 
          pBinaryWriter.Write(vApprovalMethod.ToString()) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request     
      Dim pFunction As String = "ccSecurityRequireApproval" 
      Dim pParametersToLog = $"Parameters: Restricted;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-221003-1713", vRequester) 
    End Try 
 
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
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables    
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create the request    
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vAuthorizationCode) 
          pBinaryWriter.Write(vFunctionName) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request    
      Dim pFunction As String = "ccSecurityCheckApprovalCode" 
      Dim pParametersToLog = $"Parameters: None;" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, pParametersToLog, pResponse, vRequester) : If Not pFault.isOK Then Return pFault  
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-221003-1713", vRequester) 
    End Try 
 
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
