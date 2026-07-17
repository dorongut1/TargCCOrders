Imports System.Threading 
Imports System.Globalization 
Imports System.Configuration 
 
Namespace My 
 
  ' The following events are available for MyApplication: 
  '  
  ' Startup: Raised when the application starts, before the startup form is created. 
  ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally. 
  ' UnhandledException: Raised if the application encounters an unhandled exception. 
  ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.  
  ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected. 
  Partial Friend Class MyApplication 
 
    Private Shared _Requester As clsRequester 
 
    Private Shared _WillRestartOnShutdown As Boolean 
 
    Public Shared Sub SetRequester(ByVal vRequester As clsRequester) 
      _Requester = vRequester 
    End Sub 
 
    Public Shared Sub ExecuteRestart() 
      _WillRestartOnShutdown = True 
      Application.DoEvents() 
      System.Windows.Forms.Application.Exit() 
    End Sub 
 
    Private Sub MyApplication_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged 
      If ccHelper.GetControllerName() <> "WSController" Then Exit Sub 
      If e.IsNetworkAvailable = False Then 
        If _Requester.UILang = clsEnums.enmLanguage.he Then 
          frmMessageOrInputBox.ShowMsg("התנתקת מהרשת!" & Environment.NewLine & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), frmMessageOrInputBox.enmIconType.CriticalError) 
        ElseIf _Requester.UILang = clsEnums.enmLanguage.ru Then 
          frmMessageOrInputBox.ShowMsg("Вы отключены от сети!", frmMessageOrInputBox.enmIconType.CriticalError) 
        Else 
          frmMessageOrInputBox.ShowMsg("You are disconnected from the network!" & Environment.NewLine & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), frmMessageOrInputBox.enmIconType.CriticalError) 
        End If 
      End If 
      If e.IsNetworkAvailable = True Then 
        If _Requester.UILang = clsEnums.enmLanguage.he Then 
          frmMessageOrInputBox.ShowMsg("התחברת מחדש לרשת!" & Environment.NewLine & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), frmMessageOrInputBox.enmIconType.CriticalError) 
        ElseIf _Requester.UILang = clsEnums.enmLanguage.ru Then 
          frmMessageOrInputBox.ShowMsg("Теперь вы подключены к сети!", frmMessageOrInputBox.enmIconType.CriticalError) 
        Else 
          frmMessageOrInputBox.ShowMsg("You are now connected to the network!" & Environment.NewLine & DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), frmMessageOrInputBox.enmIconType.CriticalError) 
        End If 
      End If 
    End Sub 
 
    Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown 
      Dim pFault As clsFault 
 
      pFault = ccSecurity.LogOut(_Requester) 
      If pFault.isOK = False Then 
        If Not (pFault.Number = 104 Or pFault.Number = 103 Or pFault.Number = 105) Then 
          ShowFault(pFault, _Requester) 
        End If 
      End If 
 
      'Do the restart here, so that we are sure all the other threads have closed down.  
      'Otherwise, it may shut down but not restart (especially in single instance apps)  
      If _WillRestartOnShutdown = True Then System.Windows.Forms.Application.Restart() 
    End Sub 
 
    Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance 
      Dim i As MsgBoxResult = MsgBox("There is already an instance running. Would you like to kill it?" & Environment.NewLine & "Click ‘Yes’ to kill it or ‘No’ to use the existing instance.", MsgBoxStyle.YesNo Or MsgBoxStyle.Question Or MsgBoxStyle.SystemModal) 
      If i = MsgBoxResult.No Then Exit Sub 
 
      'Note, this only works if you set 'Make single instance application' in Project Properties, Application screen 
      'If I use frmMessageOrInputBox, it shows up on the wrong instance!!  
      MsgBox("Please start me again", MsgBoxStyle.Information) 
 
      Dim p As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName(My.Application.Info.AssemblyName) 
 
      For l As Integer = 0 To p.Length - 1 
        Dim pS As System.Diagnostics.Process = p(l) 
        If pS.ProcessName.ToLowerInvariant() = My.Application.Info.AssemblyName.ToLowerInvariant() Then 
          pS.Kill() 
        End If 
      Next 
    End Sub 
 
    Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup 
 
      'Check Settings file for corruption 
      Try 
        Settings.Reload() 
        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal) 
      Catch ex As System.Configuration.ConfigurationErrorsException 
        Dim pMessage As String = $"{My.Application.Info.ProductName} has detected that your user settings file has become corrupt. " & 
                                    $"This may be due to a crash or improper exiting of the program.{Environment.NewLine}" & 
                                    $"{My.Application.Info.ProductName} must reset your user settings in order to continue." 
        MsgBox(pMessage, MsgBoxStyle.Critical) 
        Dim Filename As String = ex.Filename 
        System.IO.File.Delete(Filename) 
        Settings.Reload() 
      End Try 
 
      Dim pLanguage As String 
      Dim pCulture As String 
      If My.Settings.IsLocalized = True Then 
        pLanguage = My.Settings.Language 
        pCulture = My.Settings.Culture 
 
        If pLanguage = "" Then pLanguage = "en" : My.Settings.Language = pLanguage 
        If pCulture = "" Then pCulture = "en-US" : My.Settings.Culture = pCulture 
 
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(pCulture, True) 
        Thread.CurrentThread.CurrentCulture = New CultureInfo(pCulture, True) 
      Else 
        pLanguage = "en" 
        pCulture = "en-US" 
        My.Settings.Language = pLanguage 
        My.Settings.Culture = pCulture 
        My.Settings.Save() 
      End If 
 
      LocalizedTextLanguage = clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage) 
 
      If My.Settings.IsLocalized = True AndAlso My.Settings.Language = "he" Then 
        frmMain.RightToLeft = RightToLeft.Yes 
        frmMain.RightToLeftLayout = True 
        frmLogin.RightToLeft = RightToLeft.Yes 
        frmLogin.RightToLeftLayout = True 
        frmLoginOTP.RightToLeft = RightToLeft.Yes 
        frmLoginOTP.RightToLeftLayout = True 
        frmPopup.RightToLeft = RightToLeft.Yes 
        frmPopup.RightToLeftLayout = True 
        frmMessageOrInputBox.RightToLeft = RightToLeft.Yes 
        frmMessageOrInputBox.RightToLeftLayout = True 
        frmUpdateField.RightToLeft = RightToLeft.Yes 
        frmUpdateField.RightToLeftLayout = True 
        frmAbout.RightToLeft = RightToLeft.Yes 
        frmAbout.RightToLeftLayout = True 
      End If 
 
      _WillRestartOnShutdown = False 
 
      'frmSplash = New frmSplash  
      'frmSplash.StartPosition = FormStartPosition.CenterScreen  
      'frmSplash.Show()  
      'Application.DoEvents()  
 
      If My.Computer.Network.IsAvailable = False AndAlso ccHelper.GetControllerName() = "WSController" Then 
        Dim pStrg As String = "" 
        If pLanguage = "he" Then 
          pStrg &= "אתה מנותק מהרשת!" & Environment.NewLine 
        ElseIf pLanguage = "ru" Then 
          pStrg &= "Вы отключены от сети!" & Environment.NewLine 
        Else 
          pStrg &= "You are disconnected from the network!" & Environment.NewLine 
        End If 
        frmMessageOrInputBox.ShowMsg(pStrg, frmMessageOrInputBox.enmIconType.CriticalError) 
        End 
      End If 
 
      frmAbout.Show() 
      Application.DoEvents() 
 
    End Sub 
 
    Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException 
      Dim pFault As New clsFault 
 
      If _Requester IsNot Nothing Then 
        If String.IsNullOrEmpty(_Requester.CallingFunctionWithinApplication) Then 
          _Requester.CallingFunctionWithinApplication = "MyApplication_UnhandledException(none before)" 
        End If 
      End If 
 
      Try 
        pFault.LogException(e.Exception, e.ToString, "TRGT-090202-1049", _Requester) 
        ShowFault(pFault, _Requester) 
        e.ExitApplication = False 
      Catch ex As Exception 
        'Invalid DBController Version. See Logged Alert  
        If ex.Message.IndexOf("See Logged Alert") >= 0 Then 'not a surprise  
          frmMessageOrInputBox.ShowMsg("Application.UnhandledException: TRGT-090722-1717" & Environment.NewLine & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.CriticalError) 
        Else 
          frmMessageOrInputBox.ShowMsg("Application.UnhandledException: TRGT-090722-1716" & Environment.NewLine & GetExceptionText(e.Exception).Replace("~", vbNewLine) & Environment.NewLine & Environment.NewLine & "This Fault could not be sent to the controller." & Environment.NewLine & "Please contact Customer Service" & vbNewLine & Environment.NewLine & "Further Details:" & Environment.NewLine & e.Exception.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
        End If 
        Application.DoEvents() 
        Environment.Exit(0) 
      End Try 
 
    End Sub 
 
 
    Private Function GetExceptionText(ByVal vEx As Exception) As String 
      Dim pEx As Exception = vEx 
      Dim pString As String 
 
      pString = "Exception!!! Type:" & vEx.GetType.ToString & "~" 
      pString &= " Details:" & "~" 
 
      Dim iCntr As Integer = 1 
      pString &= "  " & iCntr & ". " & pEx.Message & "~" 
      'now do inner exceptions  
      Do Until pEx.InnerException Is Nothing 
        iCntr += 1 
        pEx = pEx.InnerException 
        pString &= "  " & iCntr & ". " & pEx.Message & "~" 
      Loop 
 
      Return pString 
    End Function 
  End Class 
 
End Namespace 
