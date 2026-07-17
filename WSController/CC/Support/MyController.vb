Public Class MyController 
  
  'Use this to enter the config data. This is great if you obfuscate the code   
  Private Shared ReadOnly _ServerName As New Lazy(Of String)( 
    Function() 
      Dim v = ServerApplicationRoot(APIServerNumber) 
      If v.IndexOf("/", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        Return v.Split("/"c)(0) 
      Else 
        Return v 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ServerName As String 
    Get 
      Return _ServerName.Value 
    End Get 
  End Property 
 
  Public Shared ReadOnly Property IsSQLUserSysAdmin As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
  Public Shared ReadOnly Property IsSQLUserDBOwner As Boolean 
    Get 
      Return False 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _ServerApplication As New Lazy(Of String)( 
    Function() 
      Dim v = ServerApplicationRoot(APIServerNumber) 
      If v.IndexOf("/", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        Return v.Split("/"c)(1) 
      Else 
        Return "Root" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ServerApplication As String 
    Get 
      Return _ServerApplication.Value 
    End Get 
  End Property 
 
  Friend Shared ReadOnly Property DecipherKey() As String 
    Get 
      Return "\zXI-C42Rt" 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _WSPwdEnc As New Lazy(Of String)( 
    Function() 
      Dim v = NETEncryption.clsHash.Hash("W@Hmav/Ro7", NETEncryption.clsHash.HashName.SHA256) 
      Return v 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property WSPwdEnc As String 
    Get 
      Return _WSPwdEnc.Value 
    End Get 
  End Property 
 
  Friend Shared ReadOnly Property WSPwd() As String 
    Get 
      Return "W@Hmav/Ro7" 
    End Get 
  End Property 
 
  Friend Shared ReadOnly Property ApplicationAuthenticationToWS() As clsEnums.enmApplicationAuthenticationToWS 
    Get 
      Return clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials 
    End Get 
  End Property 
  Friend Shared ReadOnly Property UserIdentificationModel() As clsEnums.enmUserIdentificationModel 
    Get 
      Return clsEnums.enmUserIdentificationModel.ByApplicationUser 
    End Get 
  End Property 
  Friend Shared ReadOnly Property ApplicationName() As String 
    Get 
      Return "TargCCOrders" 
    End Get 
  End Property 
  Friend Shared ReadOnly Property ApplicationPwd() As String 
    Get 
      Return "-xJ9Fd\ML," 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SSLCertificateIssuer As New Lazy(Of String())( 
    Function() 
 
      Dim v As String = "" 
      If ServerRequiresSSL = True Then 
        v = GetConfigValueFromAppSetting("TargCCOrders.Issuer") 
        If v IsNot Nothing AndAlso v = "" Then 
          v = "NotOverridden" 
        End If 
      Else 
        v = "None" 
      End If 
      Return v.Split(";"c) 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SSLCertificateIssuer As String() 
    Get 
      Return _SSLCertificateIssuer.Value 
    End Get 
  End Property 
 
 
  Private Shared _AlreadyWarned As List(Of String) 
 
  Private Shared Sub ReportMissingConfig(ByVal vConfigName As String, ByVal vAssuming As String) 
    Dim pMessage As String = $"Missing or invalid configuration for '{vConfigName}'. Assuming {vAssuming}" 
    Tools.LogToTextFile.WriteMessage(pMessage, "ConfigErrors") 
 
    If _AlreadyWarned Is Nothing Then _AlreadyWarned = New List(Of String) 
 
    If _AlreadyWarned.Contains(vConfigName) Then Exit Sub 
    _AlreadyWarned.Add(vConfigName) 
 
    Try 
      Dim pFault As New clsFault 
      Dim pEntryAssembly As String = System.Reflection.Assembly.GetEntryAssembly?.FullName 
      If Not String.IsNullOrEmpty(pEntryAssembly) Then 
        pFault.LogFreeTextFault(4, pMessage, $"EntryAssembly '{pEntryAssembly}'", "TRGT-200111-0938", Nothing) 
      Else 
        Dim pExecutingAssembly As String = System.Reflection.Assembly.GetExecutingAssembly?.FullName 
        pFault.LogFreeTextFault(4, pMessage, $"ExecutingAssembly '{pExecutingAssembly}'", "TRGT-200111-1301", Nothing) 
      End If 
    Catch ex As Exception 
    End Try 
  End Sub 
 
  Private Shared ReadOnly _InTestMode As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.InTestMode") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.InTestMode", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property InTestMode As Boolean 
    Get 
      Return _InTestMode.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _LogDetails As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.LogDetails") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.LogDetails", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property LogDetails As Boolean 
    Get 
      Return _LogDetails.Value 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _UploadedFilesRootFolder As New Lazy(Of String)( 
    Function() 
 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.UploadedFilesRootFolder") 
      If v = "" Then 
        ReportMissingConfig("TargCCOrders.UploadedFilesRootFolder", "C:\Windows\Temp") 
        v = "C:\Windows\Temp" 
      End If 
      v = v.Trim 
      If v.ToLowerInvariant() = "local" Then 
        v = ccHelper.GetEntryAssemblyDetails.BinaryLocation 
        v &= "images\" 
        Try 
          If IO.Directory.Exists(v) = False Then 
            IO.Directory.CreateDirectory(v) 
          End If 
          If IO.File.Exists(v & "update.zip") Then 
            Dim pResponse As String = ccHelper.UnZipUpdateFolder(v) 
            If pResponse <> "OK" Then 
              Throw New Exception(String.Format("Could not extract files from update.zip for {0} ({1}). TRGT-161113-0942", "TargCCOrders.UploadedFilesRootFolder", v)) 
            End If 
          End If 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException($"Couldn't create UploadedFilesRootFolder", ex, "ConfigErrors") 
        End Try 
      Else 
        If v.EndsWith("\", StringComparison.OrdinalIgnoreCase) = False Then v &= "\" 
      End If 
      Try 
        If IO.Directory.Exists(v) = False Then 
          ReportFailedFolder("TargCCOrders.UploadedFilesRootFolder", v) 
        End If 
      Catch ex As Exception 
        Throw New Exception(String.Format("Missing or invalid configuration for {0} ({1}). TRGT-131219-2054", "TargCCOrders.UploadedFilesRootFolder", v)) 
      End Try 
      Return v 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property UploadedFilesRootFolder As String 
    Get 
      Return _UploadedFilesRootFolder.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _LogLocation As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("LogLocation") 
      If v = "" Then 
        v = "C:\Windows\Temp" 
      End If 
      If v.ToLowerInvariant() = "local" Then 
        v = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\TargCCOrders" 
      End If 
      v = v.Trim 
      If v.EndsWith("\", StringComparison.OrdinalIgnoreCase) = False Then v &= "\" 
      Try 
        'if the folder doesn't exist then create it   
        If Not System.IO.Directory.Exists(v) Then 
          System.IO.Directory.CreateDirectory(v) 
        End If 
        'in case we failed   
        If IO.Directory.Exists(v) = False Then 
          v = "C:\Windows\Temp\" 
          ReportFailedFolder("LogLocation", v) 
        End If 
      Catch ex As Exception 
        Throw New Exception(String.Format("Missing or invalid configuration for {0} ({1}). TRGT-131219-2151", "LogLocation", v)) 
      End Try 
      Return v 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property LogLocation As String 
    Get 
      Return _LogLocation.Value 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _ccAPICompressionMode As New Lazy(Of clsEnums.enmccAPICompressionMode)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ccAPICompressionMode") 
      If v = "" Then 
        Throw New Exception(String.Format("Missing or invalid configuration for {0}. TRGT-150314-0922", "TargCCOrders.ccAPICompressionMode")) 
      End If 
      Dim ve As clsEnums.enmccAPICompressionMode = clsEnums.enmccAPICompressionMode.UD 
      If Not String.IsNullOrEmpty(v) Then 
        ve = clsEnums.TranslateEnmccAPICompressionMode(v) 
      End If 
      If ve = clsEnums.enmccAPICompressionMode.UD Then 
        Throw New Exception("TargCCOrders.ccAPICompressionMode must be either 'IIS', 'DeflateTargCC', 'GzipTargCC' or 'None'; TRGT-150314-0924") 
      End If 
      Return ve 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ccAPICompressionMode As clsEnums.enmccAPICompressionMode 
    Get 
      Return _ccAPICompressionMode.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _ServerApplicationRoot As New Lazy(Of String())( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ServerApplicationRoot") 
      If v Is Nothing OrElse String.IsNullOrEmpty(v) Then 
        ReportMissingConfig("TargCCOrders.ServerApplicationRoot", ";") 
        v = ";" 
      End If 
      Return v.Split(";"c) 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ServerApplicationRoot As String() 
    Get 
      Return _ServerApplicationRoot.Value 
    End Get 
  End Property 
 
 
  Private Shared _APIServerIndex As Nullable(Of Integer) 
  Friend Shared ReadOnly Property APIServerNumber As Integer 
    Get 
      If _APIServerIndex Is Nothing Then 
        Dim pServerNoFile As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\ServerNo.ini" 
        If IO.File.Exists(pServerNoFile) = True Then 
          Try 
            _APIServerIndex = ccHelper.ToInteger(IO.File.ReadAllText(pServerNoFile)) 
            If _APIServerIndex > ServerApplicationRoot.Length - 1 Then 
              _APIServerIndex = 0 
            End If 
          Catch ex As Exception 
            _APIServerIndex = 0 
          End Try 
        Else 
          _APIServerIndex = 0 
        End If 
      End If 
      Return _APIServerIndex.Value 
    End Get 
  End Property 
 
  Friend Shared Sub SetNextApiServer() 
    If MyController.ServerApplicationRoot.Length > 1 Then 
      Dim pServerNoFile As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\ServerNo.ini" 
      _APIServerIndex += 1 
      If _APIServerIndex > MyController.ServerApplicationRoot.Length - 1 Then _APIServerIndex = 0 
      IO.File.WriteAllText(pServerNoFile, _APIServerIndex.Value.ToString()) 
    End If 
  End Sub 
 
 
  Private Shared ReadOnly _ServerRequiresSSL As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ServerRequiresSSL") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.ServerRequiresSSL", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ServerRequiresSSL As Boolean 
    Get 
      Return _ServerRequiresSSL.Value 
    End Get 
  End Property 
 
  ''' <summary>  
  ''' This gets a value from appSettings.  
  ''' If the value is encrypted, it decrypts it.  
  ''' If the key isn't found, it throws an error.  
  ''' the decryption is invalid, it returns an empty string.  
  ''' </summary>  
  ''' <param name="vConfigKey"></param>  
  ''' <returns></returns>  
  Public Shared Function GetConfigValueFromAppSetting(ByVal vConfigKey As String) As String 
    Dim pValue As String 
 
    'Use this instead of ConfigurationManager.AppSettings   
    '  it loads at the setting at once, and enables you to query them   
    'ConfigurationManager.AppSettings("KeyName") you get them one at a time    
    Dim pAppSettings As Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings 
    If pAppSettings.Count = 0 Then Throw New Exception("There are no appSettings in the config file!!") 
 
    'check if it's encrypted    
    Dim pConfigKeyEnc As String = "=" & ccHelper.Encrypt(ccHelper.enmEncryptionMethod.TripleDES, vConfigKey) 
    Dim pFoundKeyEnc As Boolean 
    Dim pValueEnc As String 
    If pAppSettings.AllKeys.Contains(pConfigKeyEnc) Then 
      pValueEnc = pAppSettings.Get(pConfigKeyEnc) 
      pFoundKeyEnc = True 
    Else 
      pFoundKeyEnc = False 
      pValueEnc = "" 
    End If 
 
    'now get the unencrypted    
    Dim pFoundKeyNotEnc As Boolean 
    Dim pValueNotEnc As String 
    pFoundKeyNotEnc = False 
    pValueNotEnc = "" 
    If pAppSettings.AllKeys.Contains(vConfigKey) Then 
      pValueNotEnc = pAppSettings.Get(vConfigKey) 
      pFoundKeyNotEnc = True 
    Else 
      'Try and find it - Case insensitive!   
      For Each l In pAppSettings.AllKeys 
        If l.Equals(vConfigKey, StringComparison.OrdinalIgnoreCase) Then 
          pValueNotEnc = pAppSettings.Get(l) 
          pFoundKeyNotEnc = True 
        End If 
      Next 
    End If 
 
    If pFoundKeyEnc = True AndAlso pFoundKeyNotEnc = True Then 
      Dim pMessage As String = String.Format("The same key cannot be found twice, both encrypted And unencrypted. {0} Is invalid! TRGT-170117-1618", vConfigKey) 
      Tools.LogToTextFile.WriteMessage(pMessage, "ConfigErrors") 
      Throw New Exception(pMessage) 
    ElseIf pFoundKeyEnc = False AndAlso pFoundKeyNotEnc = False Then 
      If pFoundKeyEnc = False AndAlso pFoundKeyNotEnc = False Then ' Try the database   
        If vConfigKey.IndexOf(".") > 0 Then vConfigKey = vConfigKey.Split("."c)(1) 
        pValueNotEnc = GetConfigfromDB(vConfigKey) 
        If Not pValueNotEnc.Equals("-999", StringComparison.OrdinalIgnoreCase) Then 
          pFoundKeyNotEnc = True 
        Else 
          pValueNotEnc = "" 
        End If 
      End If 
      If pFoundKeyEnc = False AndAlso pFoundKeyNotEnc = False Then ' still not found!!   
        Dim pMessage As String = String.Format("Missing configuration key {0}. TRGT-170117-1620", vConfigKey) 
        If vConfigKey.Equals("loglocation", StringComparison.OrdinalIgnoreCase) Then 
          'Throw New Exception(pMessage)   
        Else 
          Tools.LogToTextFile.WriteMessage(pMessage, "ConfigErrors") 
        End If 
      End If 
    End If 
 
    If pFoundKeyEnc = True Then 
      If Not String.IsNullOrEmpty(pValueEnc) Then 
        pValue = ccHelper.Decrypt(ccHelper.enmEncryptionMethod.TripleDES, pValueEnc) 
        If pValue = "!! Decryption Error !!" Then 
          Dim pMessage As String = String.Format("The value for configuration key {0} is improperly encrypted! TRGT-170117-1628", vConfigKey) 
          Tools.LogToTextFile.WriteMessage(pMessage, "ConfigErrors") 
          Throw New Exception(pMessage) 
        End If 
      Else 
        pValue = "" 
      End If 
    Else 
      pValue = pValueNotEnc 
    End If 
 
    If pValue Is Nothing Then pValue = "" 
 
    Return pValue 
  End Function 
  Private Shared _Configs As New csSystemDefaultCol 
  Private Shared _ConfigLock As New Object 
  Private Shared _ConfigLastGot As DateTimeOffset = DateTimeOffset.Now.AddDays(-1) 
 
  Private Shared Function GetConfigfromDB(ByVal vConfigSetting As String) As String 
    SyncLock _ConfigLock 
      If DateTimeOffset.Now.Subtract(_ConfigLastGot).TotalMinutes > 10 Then 
        Dim pRequester As New clsRequester("Config", "GetConfigfromDB", True) 
        pRequester.CallingFunctionWithinApplication = "GetConfigfromDB" 
        Dim pFault As clsFault = _Configs.FillByGroup("Config", pRequester) 
        If Not pFault.isOK Then Throw New Exception(pFault.StringForMessageBox) 
        _ConfigLastGot = DateTimeOffset.Now 
      End If 
      Dim pConfig As csSystemDefault = _Configs.FindByGroupAndSettingName("Config", vConfigSetting) 
      If pConfig.IsEmpty Then 
        Return "-999" 
      Else 
        If pConfig.SystemDefaultType = clsEnums.enmSystemDefaultType.Encrypted Then 
          'decrypt   
          Return ccHelper.Decrypt(ccHelper.enmEncryptionMethod.AES, pConfig.SettingValue) 
        ElseIf pConfig.SystemDefaultType = clsEnums.enmSystemDefaultType.Bit Then 
          'Translate   
          If pConfig.SettingValue = "0" Then 
            Return "false" 
          ElseIf pConfig.SettingValue = "1" Then 
            Return "true" 
          Else 
            Return "-999" 
          End If 
        Else 
          Return pConfig.SettingValue 
        End If 
      End If 
    End SyncLock 
  End Function 
  Private Shared Sub ReportFailedFolder(ByVal vConfigName As String, ByVal vFolderName As String) 
    Dim pMessage As String = $"Failed creating folder {vFolderName} Missing for '{vConfigName}'." 
 
    If Not vConfigName.Equals("LogLocation", StringComparison.OrdinalIgnoreCase) Then 
      Tools.LogToTextFile.WriteMessage(pMessage, "ConfigErrors") 
    End If 
 
    If _AlreadyWarned Is Nothing Then _AlreadyWarned = New List(Of String) 
    If _AlreadyWarned.Contains(vConfigName) Then Exit Sub 
    _AlreadyWarned.Add(vConfigName) 
 
    Try 
      Dim pFault As New clsFault 
      Dim pEntryAssembly As String = System.Reflection.Assembly.GetEntryAssembly?.FullName 
      If Not String.IsNullOrEmpty(pEntryAssembly) Then 
        pFault.LogFreeTextFault(4, pMessage, $"EntryAssembly '{pEntryAssembly}'", "TRGT-210408-2035", Nothing) 
      Else 
        Dim pExecutingAssembly As String = System.Reflection.Assembly.GetExecutingAssembly?.FullName 
        pFault.LogFreeTextFault(4, pMessage, $"ExecutingAssembly '{pExecutingAssembly}'", "TRGT-210408-2036", Nothing) 
      End If 
    Catch ex As Exception 
    End Try 
  End Sub 
 
  ''' <summary>    
  ''' This is used to handle a configuration value from applicationSettings.    
  ''' If the value is not encrypted, it simply returns the value.    
  ''' If the value is encrypted, it returns the decrypted value.    
  ''' If the encryption is invalid, then it returns an empty string.    
  ''' Explanations of any problems are in rComment..    
  ''' </summary>    
  ''' <param name="vRawValue"></param>    
  ''' <param name="rComment"></param>    
  ''' <returns></returns>    
  ''' <remarks></remarks>    
  Public Shared Function GetConfigValueFromApplicationSetting(ByVal vRawValue As String, ByRef rComment As String) As String 
    If String.IsNullOrEmpty(MyController.DecipherKey) = True Then 
      rComment = "Missing DecipherKey" 
      Return "" 
    End If 
 
    If vRawValue = "" Then 
      rComment = "No Value Received" 
      Return "" 
    End If 
    If Not vRawValue.StartsWith("=", StringComparison.OrdinalIgnoreCase) Then 
      rComment = "OK;Not Encrypted" 
      Return vRawValue 
    End If 
 
    Dim pStrg As String 
    pStrg = NETEncryption.clsTripleDES.DecryptData(vRawValue.Substring(1), MyController.DecipherKey) 
    If pStrg Is Nothing OrElse pStrg = "" Then 
      rComment = "Decryption Error" 
      Return "" 
    Else 
      rComment = "OK;Encrypted" 
      Return pStrg 
    End If 
  End Function 
 
  Private Shared ReadOnly _IsAuthenticationDoneOnExternalSystem As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.IsAuthenticationDoneOnExternalSystem") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.IsAuthenticationDoneOnExternalSystem", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property IsAuthenticationDoneOnExternalSystem As Boolean 
    Get 
      Return _IsAuthenticationDoneOnExternalSystem.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _CacheOn As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.CacheOn") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.CacheOn", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property CacheOn As Boolean 
    Get 
      Return _CacheOn.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _CacheKeepAliveMin As New Lazy(Of Integer)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.CacheKeepAliveMin") 
      If ccHelper.IsNumeric(v) AndAlso v <> "0" Then 
        Return ccHelper.ToInteger(v) 
      Else 
        ReportMissingConfig("TargCCOrders.CacheKeepAliveMin", "5") 
        Return 5 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property CacheKeepAliveMin As Integer 
    Get 
      Return _CacheKeepAliveMin.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _CacheSingleLanguageOnly As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.CacheSingleLanguageOnly") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.CacheSingleLanguageOnly", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property CacheSingleLanguageOnly As Boolean 
    Get 
      Return _CacheSingleLanguageOnly.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _UsersToShowEnglishAlso As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.UsersToShowEnglishAlso") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.UsersToShowEnglishAlso", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property UsersToShowEnglishAlso As String 
    Get 
      Return _UsersToShowEnglishAlso.Value 
    End Get 
  End Property 
 
End Class 
