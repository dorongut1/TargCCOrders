Public Class SMSResponse
  Inherits System.Web.UI.Page

  Private Shared _Padlock As New Object
  Private Shared _LogLocation As String = ""

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim pMessage As String = "Request received at " & Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US"))
    lblResponse.Text = pMessage

    Dim pResponse As String = "---------- Start " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & ControlChars.NewLine
    pResponse &= "UserHostAddress = " & My.Request.UserHostAddress & ControlChars.NewLine
    Dim pHostName As String = ""
    Try
      pHostName = System.Net.Dns.GetHostEntry(My.Request.UserHostAddress).HostName
    Catch ex As Exception
      pHostName = "GetHostEntry: " & ex.Message
    End Try
    pResponse &= "HostName = " & pHostName & ControlChars.NewLine
    pResponse &= "UserHostName = " & My.Request.UserHostName & ControlChars.NewLine
    pResponse &= "UserAgent = " & My.Request.UserAgent & ControlChars.NewLine
    For Each p As String In My.Request.QueryString
      pResponse &= p & ": '" & My.Request.QueryString(p) & "'" & ControlChars.NewLine
    Next
    For Each p As String In My.Request.Form
      pResponse &= p & ": '" & My.Request.Form(p) & "'" & ControlChars.NewLine
    Next
    pResponse &= "---------- End " & ControlChars.NewLine & ControlChars.NewLine

    SyncLock _Padlock
      If String.IsNullOrEmpty(_LogLocation) Then
        _LogLocation = MyController.LogLocation
      End If

      IO.File.AppendAllText(_LogLocation & "Target.SMS" & DateTime.Now.ToString("yyMMdd") & "Callback.txt", pResponse)

    End SyncLock
  End Sub

End Class