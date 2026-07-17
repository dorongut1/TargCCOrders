Imports System.Web.SessionState

Public Class Global_asax
  Inherits System.Web.HttpApplication

  Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires when the application is started
    Tools.LogToTextFile.WriteMessage("Web Service Started!!", "WS")
    Application("OnlineNow") = 0
  End Sub

  Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires when the session is started
    Application.Lock()
    Dim pOnlineNow As Long = ccHelper.ToLong(Application("OnlineNow")) + 1
    Application("OnlineNow") = pOnlineNow
    Application.UnLock()

    Dim pUser As String = "     New User #" & My.Request.UserHostAddress & "#" & My.Request.Url.AbsoluteUri & "#" & System.Web.HttpContext.Current.Request.UserAgent & "#"
    Tools.LogToTextFile.WriteMessage("  User Added. Online now =" & pOnlineNow.ToString & pUser, "WS")
  End Sub

  Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires at the beginning of each request
  End Sub

  Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires upon attempting to authenticate the use
  End Sub

  Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires when an error occurs

    'Note that Chrome has a bug where it aggressively looks for "favicon.ico"
    'http://stackoverflow.com/questions/3929322/mysterious-file-does-not-exist-error-in-code-unrelated-to-any-kind-of-file-io

    Dim pFilePath As String = Request.CurrentExecutionFilePath

    Dim pEx As Exception = Server.GetLastError().GetBaseException()

    If pEx.Message.IndexOf("File does not exist") >= 0 Then
      If pFilePath.ToLowerInvariant() = "/favicon.ico" Then
        Server.ClearError()
        Exit Sub
      End If
    End If

    Dim pMessage As String = ""
    pMessage &= "Error Message: " & pEx.Message & Environment.NewLine
    If Not String.IsNullOrEmpty(pEx.InnerException?.Message) Then
      pMessage &= "Inner Exception: " & pEx.InnerException.ToString & Environment.NewLine
    Else
      pMessage &= "Inner Exception: None" & Environment.NewLine
    End If
    pMessage &= "  Inner Error Message: " & pEx?.Message & Environment.NewLine
    pMessage &= "UserHostAddress: " & Request.UserHostAddress & Environment.NewLine
    pMessage &= "CurrentExecutionFilePath:" & pFilePath & Environment.NewLine

    Tools.LogToTextFile.WriteMessage("WebService Failure!" & Environment.NewLine & pMessage & ccHelper.GetStack() & Environment.NewLine & pMessage & "Full Error: " & pEx.ToString, "WS_CC")

    Dim pResponse As String = Tools.Mailer.SendMailToMultipleRecipients("", "WebService Failure!", MyController.ProblemMailTo, pMessage)
    If pResponse <> "OK" Then
      Tools.LogToTextFile.WriteMessage("Tried to send warning mail but couldn't: " & pResponse, "WS_CC")
    End If

    Server.ClearError()
  End Sub

  Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires when the session ends

    Application.Lock()
    Dim pOnlineNow As Long = ccHelper.ToLong(Application("OnlineNow")) - 1
    Application("OnlineNow") = pOnlineNow
    Application.UnLock()
    Tools.LogToTextFile.WriteMessage("  User Left. Online now =" & pOnlineNow.ToString, "WS")
  End Sub

  Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
    ' Fires when the application ends
    Tools.LogToTextFile.WriteMessage("Web Service Stopped!!", "WS")
  End Sub

End Class