Public Class MyController 
  
  'See documentation for use of this property 
  Private Shared _ConfigForNoAppConfig As Dictionary(Of String, String) 
  
  Private Shared _DbName As String  
  Private Shared _DbServer As String  
 
  Public Shared Event evtAddUsersForLocalDB(ByRef rFault As clsFault, ByVal vRequester As clsRequester)  
 
  Public Shared ReadOnly Property ServerName() As String 
    Get 
      If String.IsNullOrEmpty(_DbServer) = True Then 
        Try 
          CreateDBConnString() 
          'now make sure we initialized what we need 
          Initalize() 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException("Problem creating DBConn", ex, "CC") 
          Throw ex 
        End Try 
      End If 
      Return _DbServer 
    End Get 
  End Property 
  Friend Shared ReadOnly Property DBName() As String 
    Get 
      If String.IsNullOrEmpty(_DbName) = True Then 
        Try 
          CreateDBConnString() 
          'now make sure we initialized what we need 
          Initalize() 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException("Problem creating DBConn", ex, "CC") 
          Throw ex 
        End Try 
      End If 
      Return _DbName 
    End Get 
  End Property 
  
  Public Shared ReadOnly Property ServerApplication() As String 
    Get 
      If String.IsNullOrEmpty(_DbName) = True Then 
        Try 
          CreateDBConnString() 
          'now make sure we initialized what we need 
          Initalize() 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException("Problem creating DBConn", ex, "CC") 
          Throw ex 
        End Try 
      End If 
      Return _DbName 
    End Get 
  End Property 
 
  Private Shared _DbConn As String 
  Friend Shared ReadOnly Property DBConn() As String 
    Get 
      If String.IsNullOrEmpty(_DbConn) = True Then 
        Try 
          CreateDBConnString() 
          'now make sure we initialized what we need 
          Initalize() 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException("Problem creating DBConn", ex, "CC") 
          Throw ex 
        End Try 
      End If 
      Return _DbConn 
    End Get 
  End Property 
 
  Private Shared _IsSQLUserSysAdmin As Boolean 
  Private Shared _IsSQLUserDBOwner As Boolean 
 
  Public Shared ReadOnly Property IsSQLUserSysAdmin As Boolean 
    Get 
      Return _IsSQLUserSysAdmin 
    End Get 
  End Property 
  Public Shared ReadOnly Property IsSQLUserDBOwner As Boolean 
    Get 
      Return _IsSQLUserDBOwner 
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
        pFault.LogFreeTextFault(4, pMessage, $"EntryAssembly '{pEntryAssembly}'{pEntryAssembly}'{Environment.NewLine}{ccHelper.GetStack()}", "TRGT-210408-2035", Nothing) 
      Else  
        Dim pExecutingAssembly As String = System.Reflection.Assembly.GetExecutingAssembly?.FullName 
        pFault.LogFreeTextFault(4, pMessage, $"ExecutingAssembly '{pExecutingAssembly}'{Environment.NewLine}{ccHelper.GetStack()}", "TRGT-210408-2036", Nothing) 
      End If 
    Catch ex As Exception 
    End Try 
  End Sub 
 
  Private Shared Sub Initalize() 
    'now make sure we initialized what we need 
    Dim pLogLocation As String = LogLocation 
    Dim pProblemMailTo As String = ProblemMailTo 
    Dim pLogDetails As Boolean = LogDetails 
    If pLogLocation.Equals("C:\Windows\Temp\", StringComparison.OrdinalIgnoreCase) Then 
      ReportMissingConfig("LogLocation", "C:\Windows\Temp") 
    End If 
  End Sub 
 
  Private Shared ReadOnly _IsForgiving As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.IsForgiving") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.IsForgiving", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property IsForgiving As Boolean 
    Get 
      Return _IsForgiving.Value 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _GarbageCreation As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.GarbageCreation") 
      Return String.Equals(v, "true", StringComparison.OrdinalIgnoreCase) 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  ''' <summary>  
  ''' Use when you have to override safety features, in order to obfuscate the database  
  ''' Make sure it's not in the config file in production (it defaults to False)  
  ''' </summary>  
  ''' <returns></returns>  
  Public Shared ReadOnly Property GarbageCreation As Boolean 
    Get 
      Return _GarbageCreation.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _IsAuthenticationDoneOnExternalSystem As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.IsAuthenticationDoneOnExternalSystem") 
      Return String.Equals(v, "true", StringComparison.OrdinalIgnoreCase) 
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
 
  Private Shared ReadOnly _SMTPServer As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPServer") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPServer As String 
    Get 
      Return _SMTPServer.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _ProblemMailTo As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ProblemMailTo") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.ProblemMailTo", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ProblemMailTo As String 
    Get 
      Return _ProblemMailTo.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPNameFrom As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPNameFrom") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.SMTPNameFrom", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPNameFrom As String 
    Get 
      Return _SMTPNameFrom.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPEmailFrom As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPEmailFrom") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.SMTPEmailFrom", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPEmailFrom As String 
    Get 
      Return _SMTPEmailFrom.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPDefaultNameReplyTo As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPDefaultNameReplyTo") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPDefaultNameReplyTo As String 
    Get 
      Return _SMTPDefaultNameReplyTo.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPDefaultEmailReplyTo As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPDefaultEmailReplyTo") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPDefaultEmailReplyTo As String 
    Get 
      Return _SMTPDefaultEmailReplyTo.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPEnableSSL As New Lazy(Of Boolean)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPEnableSSL") 
      If v.ToLowerInvariant() = "true" Then 
        Return True 
      ElseIf v.ToLowerInvariant() = "false" Then 
        Return False 
      Else 
        ReportMissingConfig("TargCCOrders.SMTPEnableSSL", "true") 
        Return True 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPEnableSSL As Boolean 
    Get 
      Return _SMTPEnableSSL.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPUserName As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPUserName") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPUserName As String 
    Get 
      Return _SMTPUserName.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPPassword As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPPassword") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPPassword As String 
    Get 
      Return _SMTPPassword.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMTPPort As New Lazy(Of Integer)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMTPPort") 
      If ccHelper.IsNumeric(v) AndAlso v <> "0" Then 
        Return ccHelper.ToInteger(v) 
      Else 
        Return 1 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMTPPort As Integer 
    Get 
      Return _SMTPPort.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _ServerNameForMail As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ServerNameForMail") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ServerNameForMail As String 
    Get 
      Return _ServerNameForMail.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _ccAPICompressionMode As New Lazy(Of clsEnums.enmccAPICompressionMode)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.ccAPICompressionMode") 
      Dim vEnum = clsEnums.TranslateEnmccAPICompressionMode(v) 
      If vEnum <> clsEnums.enmccAPICompressionMode.UD Then 
        Return vEnum 
      Else 
        ReportMissingConfig("TargCCOrders.ccAPICompressionMode", "MustDefine") 
        Return clsEnums.enmccAPICompressionMode.UD 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property ccAPICompressionMode As clsEnums.enmccAPICompressionMode 
    Get 
      Return _ccAPICompressionMode.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _DownloadFileURL As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.DownloadFileURL") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property DownloadFileURL As String 
    Get 
      Return _DownloadFileURL.Value 
    End Get 
  End Property 
 
 
  Private Shared ReadOnly _UploadFileURL As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.UploadFileURL") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property UploadFileURL As String 
    Get 
      Return _UploadFileURL.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _UploadFileUserName As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.UploadFileUserName") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.UploadFileUserName", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property UploadFileUserName As String 
    Get 
      Return _UploadFileUserName.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _UploadFilePwd As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.UploadFilePwd") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property UploadFilePwd As String 
    Get 
      Return _UploadFilePwd.Value 
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
 
  Private Shared ReadOnly _XMLDataLocation As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.XMLDataLocation") 
      If v = "" Then 
        ReportMissingConfig("TargCCOrders.XMLDataLocation", "C:\Windows\Temp") 
        v = "C:\Windows\Temp" 
      End If 
      v = v.Trim 
      If v.ToLowerInvariant() = "local" Then 
        v = ccHelper.GetEntryAssemblyDetails.BinaryLocation 
        v &= "data\" 
        Try 
          If IO.Directory.Exists(v) = False Then 
            IO.Directory.CreateDirectory(v) 
          End If 
        Catch ex As Exception 
          Tools.LogToTextFile.WriteException($"Couldn't create XMLDataLocation", ex, "ConfigErrors") 
        End Try 
      Else 
        If v.EndsWith("\", StringComparison.OrdinalIgnoreCase) = False Then v &= "\" 
      End If 
      Try 
        If IO.Directory.Exists(v) = False Then 
          ReportFailedFolder("TargCCOrders.XMLDataLocation", v) 
        End If 
      Catch ex As Exception 
        Throw New Exception(String.Format("Missing or invalid configuration for {0} ({1}). TRGT-131219-2119", "TargCCOrders.XMLDataLocation", v)) 
      End Try 
      Return v 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property XMLDataLocation As String 
    Get 
      Return _XMLDataLocation.Value 
    End Get 
  End Property 
 
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
      If Not v.EndsWith("\", StringComparison.OrdinalIgnoreCase) Then v &= "\" 
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
 
  Private Shared ReadOnly _SMSUrl As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMSUrl") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMSUrl As String 
    Get 
      Return _SMSUrl.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMSUserName As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMSUserName") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMSUserName As String 
    Get 
      Return _SMSUserName.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMSPassword As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMSPassword") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.SMSPassword", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMSPassword As String 
    Get 
      Return _SMSPassword.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMSSentFrom As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMSSentFrom") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.SMSSentFrom", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMSSentFrom As String 
    Get 
      Return _SMSSentFrom.Value 
    End Get 
  End Property 
 
  Private Shared ReadOnly _SMSAppHash As New Lazy(Of String)( 
    Function() 
      Dim v = GetConfigValueFromAppSetting("TargCCOrders.SMSAppHash") 
      If Not String.IsNullOrEmpty(v) Then 
        Return v 
      Else 
        ReportMissingConfig("TargCCOrders.SMSAppHash", "None") 
        Return "" 
      End If 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property SMSAppHash As String 
    Get 
      Return _SMSAppHash.Value 
    End Get 
  End Property 
 
 
  Friend Shared ReadOnly Property DecipherKey() As String 
    Get 
      Return "\zXI-C42Rt" 
    End Get 
  End Property 
 
  Private Shared ReadOnly _WSPwdEnc As New Lazy(Of String)( 
    Function() 
      Dim v As String = "W@Hmav/Ro7" 
      Return NETEncryption.clsHash.Hash(v, NETEncryption.clsHash.HashName.SHA256) 
    End Function, 
    Threading.LazyThreadSafetyMode.ExecutionAndPublication 
  ) 
 
  Public Shared ReadOnly Property WSPwdEnc As String 
    Get 
      Return _WSPwdEnc.Value 
    End Get 
  End Property 
 
  Friend Enum enmDBType 
    UD 
    [SQL] 
    [FileSystem] 
  End Enum 
 
  Private Shared _DBType As enmDBType 
  Friend Shared ReadOnly Property DBType As enmDBType 
    Get 
      Return _DBType 
    End Get 
  End Property 
 
  Private Shared Function CreateDBConnString() As String 
    Dim pDBConnData As String = Nothing 
 
    'Controller  
    pDBConnData = GetConfigValueFromAppSetting("TargCCOrders.Controller") 
    If String.IsNullOrEmpty(pDBConnData) = True Then 
      Throw New Exception("Missing TargCCOrders.Controller") 
    End If 
 
    'check if DAL is to be logged 
    If pDBConnData.StartsWith("l:", StringComparison.OrdinalIgnoreCase) Then 'Superseded by LogDetails 
      pDBConnData = pDBConnData.Substring(2) 
    End If 
 
    'FileSystem Database 
    If pDBConnData.IndexOf(";", StringComparison.OrdinalIgnoreCase) > 0 AndAlso Not pDBConnData.StartsWith("LocalDB", StringComparison.OrdinalIgnoreCase) Then 
      _DBType = enmDBType.FileSystem 
      _DbConn = pDBConnData.Split(";"c)(0) 
      If _DbConn.Equals("xml", StringComparison.OrdinalIgnoreCase) Then 
        '_DbConn = "XML" 
        Throw New Exception("Controller is not valid. XML has been superseded with binary") 
      ElseIf _DbConn.Equals("binary", StringComparison.OrdinalIgnoreCase) Then 
        _DbConn = "BINARY" 
      Else 
        Throw New Exception("Controller is not valid. Expected XML or Binary") 
      End If 
 
      pDBConnData = pDBConnData.Split(";"c)(1) 
      'pDBConnData should start with XML or Binary  
      Dim pResponse As String = "" 
   
      Dim pFileLocation As String = "" 
      pFileLocation = XMLDataLocation 
   
      _DBCache = New ccDatabaseMaintenance.clsDatabase 
      _DbServer = "Filesystem (" & pFileLocation & ")" 
      If pDBConnData.EndsWith("readonly", StringComparison.OrdinalIgnoreCase) Then 
        _DBCache.ReadOnly = True 
        pDBConnData = pDBConnData.Substring(0, pDBConnData.Length - 8) 
      Else 
        _DBCache.ReadOnly = False 
      End If 
      _DbName = pDBConnData.ToLowerInvariant().Replace("filesystem", "") 
      If _DbName = "database" Then 
        pResponse = _DBCache.LoadDatabaseFromBinary() 
        If pResponse <> "OK" Then 
          Throw New Exception("Problem loading local DB: " & pResponse) 
        End If 
      ElseIf _DbName = "tables" Then 
        'it will load as needed 
      Else 
        Throw New Exception("Invalid DBName - Must be Tables or Database") 
      End If 
 
      'unused for filesystem type 
      _IsSQLUserSysAdmin = False 
      _IsSQLUserDBOwner = False 
 
      Return _DbConn 
    End If  
    
    'If we're still here, then it's a SQL Server Database
    
    _DBCache = Nothing 
 
    _DBType = enmDBType.SQL 
 
    Dim pServerName As String = "" 
    Dim pDBName As String = "" 
    Dim pInstanceName As String = "" 
    Dim pSQLUserName As String = "" 
    Dim pSQLPassword As String = "" 
    Dim pMaxPoolSize As String = "" 
 
    Try 
      pServerName = pDBConnData.Split("~"c)(0).Trim 
      pDBName = pDBConnData.Split("~"c)(1).Trim 
      If pServerName = "" Then Throw New Exception("Invalid TargCCOrders.Controller: Missing Server Name") 
      If pDBName = "" Then Throw New Exception("Invalid TargCCOrders.Controller: Missing Database Name") 
      _DbName = pDBName 
    Catch ex As Exception 
      Throw New Exception("Invalid TargCCOrders.Controller") 
    End Try 
   
    Dim pDataLocation As String = "" 
    If pServerName.StartsWith("localdb", StringComparison.OrdinalIgnoreCase) Then 
      pDataLocation = pServerName.Split(";"c)(1) 
 
      pServerName = pServerName.Split(";"c)(0) 
      If pServerName.IndexOf("\") > -1 Then 
        pInstanceName = pServerName.Split("\"c)(1) 
        pServerName = pServerName.Split("\"c)(0) 
      End If 
      If Not pServerName.Equals("localdb", StringComparison.OrdinalIgnoreCase) Then 
        Throw New Exception("I should have gotten the DBLocation right after the LocalDB, in the format ""LocalDB[\InstanceName];C:\DBlocation"" - TRGT-170201-0938 ") 
      End If 
      If String.IsNullOrEmpty(pInstanceName) Then 
        pServerName = "(localdb)\mssqllocaldb" 
      Else 
        pServerName = "(localdb)\.\" & pInstanceName  
      End If 
 
      'Get the location  
      If String.IsNullOrEmpty(pDataLocation) Then 
        Throw New Exception("I should have gotten the DBLocation right after the LocalDB, in the format ""LocalDB[\InstanceName];C:\DBlocation"" - TRGT-170201-0937 ") 
      End If 
      'Check that the folder exists 
      If Not (pDataLocation.EndsWith("\", StringComparison.OrdinalIgnoreCase)) Then pDataLocation &= "\" 
      If IO.Directory.Exists(pDataLocation) = False Then 
        Throw New Exception("Invalid DBLocation. " & pDataLocation & " does not exist!") 
      End If 
      'Check that the file exists 
      If IO.File.Exists(pDataLocation & _DbName & ".mdf") = False Then 
        Throw New Exception("Database file does not exist at " & pDataLocation & "!") 
      End If 
      pDBName = pDBName & ";AttachDbFilename=" & pDataLocation & _DbName & ".mdf" 
    End If 
    _DbServer = pServerName 
   
    If pDBConnData.Split("~"c).Length >= 3 Then 
      pMaxPoolSize = pDBConnData.Split("~"c)(2).Trim 
      If String.IsNullOrEmpty(pMaxPoolSize) Then 
        pMaxPoolSize = "100" 'this is the default value anyway 
      Else 
        If Not (ccHelper.IsNumeric(pMaxPoolSize)) Then 
          pMaxPoolSize = "" 
        End If 
      End If 
    End If 
    If pDBConnData.Split("~"c).Length >= 4 Then 
      'SQL Server Authentication   
      Try 
        If String.IsNullOrEmpty(pMaxPoolSize) Then 
          pSQLUserName = pDBConnData.Split("~"c)(2).Trim 
          pSQLPassword = pDBConnData.Split("~"c)(3).Trim 
        Else 
          pSQLUserName = pDBConnData.Split("~"c)(3).Trim 
          If Not String.IsNullOrEmpty(pSQLUserName) Then 
            pSQLPassword = pDBConnData.Split("~"c)(4).Trim 
          End If 
        End If 
        If pSQLUserName = "" Then pSQLUserName = "" 
        If pSQLPassword = "" Then pSQLPassword = "" 
      Catch ex As Exception 
        Throw New Exception("Invalid TargCCOrders.Controller") 
      End Try 
      If pSQLPassword.IndexOf(";") >= 0 Then 
        Throw New Exception("The SQL Server password cannot include a ';'") 
      End If 
      'https://www.connectionstrings.com/all-sql-server-connection-string-keywords/  
      _DbConn = "Data Source=" & pServerName & ";Initial Catalog=" & pDBName & ";Persist Security Info=False;User ID=" & pSQLUserName & ";Password=" & pSQLPassword & "" 
    Else 
      'Trusted Connection  
      _DbConn = "Data Source=" & pServerName & ";Initial Catalog=" & pDBName & ";Integrated Security=SSPI" 
    End If 
    Dim pAppName As String = "" 
    Try 
      pAppName = ccHelper.GetEntryAssemblyDetails.AssemblyName 
    Catch ex As Exception 
    End Try 
    If pAppName = "" Then 
      Try 
        'check for server name here  
        pAppName = System.AppDomain.CurrentDomain.BaseDirectory.Split("/"c)(System.AppDomain.CurrentDomain.BaseDirectory.Split("/"c).Length - 2) 
      Catch ex As Exception 
      End Try 
    End If 
    If pAppName = "" Then 
      pAppName = "DBController Unknown App" 
    End If 
    _DbConn &= ";Application Name=" & pAppName 
   
    If String.IsNullOrEmpty(pMaxPoolSize) Then 
      'it will stay at the default of 100 
    ElseIf ccHelper.IsNumeric(pMaxPoolSize) Then 
      _DbConn &= ";Max Pool Size=" & ccHelper.ToInteger(pMaxPoolSize) 
    End If 
 
    Dim pIsForgiving As Boolean = IsForgiving 
   
    'Check version 
    Dim pSystemDefault As New csSystemDefault 
    Dim pRequester As New clsRequester("DBConnString", "Create", True) 
   
    Dim pFault As clsFault 
    Try 
      pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Controller_DBControllerVersion, pRequester, True) 
    Catch ex As Exception 
      Throw New Exception("Problem accessing the database. Check the text logs") 
    End Try 
    If Not pFault.isOK Then 
      Throw New Exception("Problem accessing the database. Check the text logs") 
    End If 
    Dim pLocalDBConnStringTemp = "" 
    Dim pOriginalDBConn = _DbConn 
    If Not pFault.isOK Then 
      If pServerName.ToLowerInvariant.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase) AndAlso Not String.IsNullOrEmpty(pSQLUserName) Then 
        'access the database with integrated security 
        If pFault.Description.IndexOf("Num: 18456") > -1 Then ' Login failed for user .... 
          pLocalDBConnStringTemp = "Data Source=" & pServerName & ";Initial Catalog=" & pDBName & ";Integrated Security=SSPI" 
          _DbConn = pLocalDBConnStringTemp 
          pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Controller_DBControllerVersion, pRequester, True) 
          If Not pFault.isOK Then 
            _DbConn = Nothing 
            Throw New Exception("Problem accessing the database. Check the text logs") 
          End If 
        Else 
          'create the database 
          Dim pMessage As String = "" 
          pMessage &= "You must 1st create the shared database server" & Environment.NewLine & Environment.NewLine 
          pMessage &= "1. Open an Command window with Administrator rights" & Environment.NewLine 
          pMessage &= "2. Run: SqlLocalDB.exe create " & pInstanceName & " 12.0 -s" & Environment.NewLine 
          pMessage &= "3. Run: SqlLocalDB.exe share " & pInstanceName & " " & pInstanceName & "" & Environment.NewLine 
          pMessage &= "4. Run: SqlLocalDB.exe p .\" & pInstanceName & "" & Environment.NewLine 
          pMessage &= "5. Run: SqlLocalDB.exe s .\" & pInstanceName & "" & Environment.NewLine & Environment.NewLine 
          pMessage &= "Here they are below. Simply paste them into the Command window." & Environment.NewLine 
          pMessage &= "Once that's done, run me again" & Environment.NewLine & Environment.NewLine & Environment.NewLine 
          pMessage &= "SqlLocalDB.exe create " & pInstanceName & " 12.0 -s" & Environment.NewLine 
          pMessage &= "SqlLocalDB.exe share " & pInstanceName & " " & pInstanceName & "" & Environment.NewLine 
          pMessage &= "SqlLocalDB.exe p .\" & pInstanceName & "" & Environment.NewLine 
          pMessage &= "SqlLocalDB.exe s .\" & pInstanceName & "" & Environment.NewLine 
          Throw New Exception(pMessage) 
        End If 
      Else 
        _DbConn = Nothing 
        Throw New Exception("Problem accessing the database. Check the text logs") 
      End If 
    End If 
   
    'Now get the existing version 
    Dim pThisAssembly As String = System.Reflection.Assembly.GetExecutingAssembly.FullName 
    Dim pAssemblyVersion As String = "" 
   
    Try 
      pAssemblyVersion = pThisAssembly.Split(","c)(1).Split("="c)(1) 
    Catch ex As Exception 
      pAssemblyVersion = pThisAssembly & ":" & ex.Message 
    End Try 
   
    If New Version(pAssemblyVersion) < New Version(pSystemDefault.SettingValue) Then 
      pFault = New clsFault 
      Dim pMessage As String = $"Invalid DBController Version. I am {pAssemblyVersion} on {ccHelper.GetComputerName()}, the database requires that I be at least {pSystemDefault.SettingValue}" 
      pFault.LogFreeTextFault(6, pMessage, $"Ver: {pAssemblyVersion}{Environment.NewLine}AssemblyName: {ccHelper.GetEntryAssemblyDetails.AssemblyName}{Environment.NewLine}BinaryLocation: {ccHelper.GetEntryAssemblyDetails.BinaryLocation}", "TRGT-160217-1608", pRequester) 
      _DbConn = Nothing 
      Throw New Exception(pMessage) 
    End If 
   
    'Now check the SQLUser User Rights 
    _IsSQLUserDBOwner = False : _IsSQLUserSysAdmin = False 
    Dim pIsCLREnabled As Boolean = False 
    pFault = ccDatabaseMaintenance.GetActiveSQLUserRights(_IsSQLUserSysAdmin, _IsSQLUserDBOwner, pIsCLREnabled, pRequester) 
    If Not pFault.isOK() Then 
      _DbConn = Nothing 
      Throw New Exception("Failed getting GetActiveSQLUserRights") 
    End If 
    If pIsCLREnabled = False Then 
      If pServerName.ToLowerInvariant.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase) Then 
        pFault = ccDatabaseMaintenance.EnableCLR(pRequester) 
        If Not pFault.isOK() Then 
          _DbConn = Nothing 
          Throw New Exception("Failed Enabling CLR for LocalDB") 
        End If 
      Else 
        _DbConn = Nothing 
        Throw New Exception("This application requires CLR to be enabled. Please contact your DBA.") 
      End If 
    End If 
    If pServerName.ToLowerInvariant.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase) AndAlso Not String.IsNullOrEmpty(pLocalDBConnStringTemp) Then 
      pFault = Nothing 
      RaiseEvent evtAddUsersForLocalDB(pFault, pRequester) 
      'Put this in the Support partial class id you want to use SQL Logins 
      'Private Shared Sub MyController_evtAddUsersForLocalDB(ByRef rFault As clsFault, vRequester As clsRequester) Handles Me.evtAddUsersForLocalDB  
      '  'Add required users to LocalDB  
      '  rFault = ccDatabaseMaintenance.CreateSQLUser("UserName", "2w3e4ree3", "0x6&125454262AC362ABC", vRequester)  
      'End Sub  
      If Not pFault Is Nothing Then 
        If Not pFault.isOK Then 
          _DbConn = Nothing 
          Throw New Exception("Failed creating user for LocalDB") 
        Else 
          Throw New Exception("New logins created. You must now run me again") 
        End If 
      End If 
      _DbConn = pOriginalDBConn 
    End If 
 
    Return _DbConn 
    
  End Function 
 
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
      If _ConfigForNoAppConfig IsNot Nothing Then 
        If _ConfigForNoAppConfig.ContainsKey(vConfigKey) Then 
          pValueNotEnc = _ConfigForNoAppConfig.Item(vConfigKey) 'it's never encrypted   
          pFoundKeyNotEnc = True 
        End If 
      End If 
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
        If vConfigKey.Equals("LogLocation", StringComparison.OrdinalIgnoreCase) Then 
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
        Dim pFault As clsFault = _Configs.FillByGroup("Config", pRequester) 
        If Not pFault.isOK Then 
          If pFault.Number = 49 Then 
            Return "" 
          Else 
            Throw New Exception(pFault.StringForMessageBox) 
          End If 
        End If 
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
 
  Private Shared _DBCache As ccDatabaseMaintenance.clsDatabase 
  Friend Shared ReadOnly Property DBCache As ccDatabaseMaintenance.clsDatabase 
    Get 
      Return _DBCache 
    End Get 
  End Property 
 
End Class 
