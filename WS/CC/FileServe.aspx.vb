Public Class FileServe
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    Dim Encoding As UnicodeEncoding = New UnicodeEncoding(True, False)
    Response.ContentEncoding = Encoding

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Request('FileName') = " & Request.QueryString("FileName"))
    pLoggedText.AppendLine("Request('Task') = " & Request.QueryString("Task"))
    pLoggedText.AppendLine("Request('TKT') = " & Request.QueryString("TKT"))
    pLoggedText.Append("Request.UserHostAddress = " & Request.UserHostAddress)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "FileServe")

    Dim pFunctionParameters As String = $"FileName: {Request.QueryString("FileName").NullToEmptyOrTrimmed}, Task: {Request.QueryString("Task").NullToEmptyOrTrimmed}, UserHostAddress: {Request.UserHostAddress}"

    If Request.QueryString("Task") = Nothing Then
      Exit Sub
    End If

    'Requester
    Dim pRequester As clsRequester
    Try
      Dim pTicketBase64 As String = Request.QueryString("TKT")
      Dim bytes As Byte() = Convert.FromBase64String(pTicketBase64)
      Dim pTicket As String = Text.Encoding.UTF8.GetString(bytes)
      'Create the requester
      pRequester = New clsRequester(pTicket)
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("Bad Ticket", ex, "FileServe")
      Exit Sub
    End Try
    'In case of previous failure
    Dim pLoggedLoginID As Long = pRequester.LoggedLoginID
    If pLoggedLoginID < 0 Then
      pRequester.ReviveLoginID()
      Tools.LogToTextFile.WriteMessage($"TRGT-240605-193521: I got a negative login {pLoggedLoginID} for {pRequester.UserName} (previous failure?). After ReviveLoginID its {pRequester.LoggedLoginID}  ", "FileServe")
      pLoggedLoginID = pRequester.LoggedLoginID
    End If
    'Check that the logged login is valid
    Dim pFault As New clsFault
    Dim pLoggedLogin As New csLoggedLogin
    pFault = pLoggedLogin.GetByID(pLoggedLoginID, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-123306: Problem loading loggedlogin - " & pFault.StringForMessageBox, "FileServe")
      Exit Sub
    End If
    If pLoggedLogin.UserName <> pRequester.UserName Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-123320: User names do not match. LoggedLogin.UserName=" & pLoggedLogin.UserFullName & ", Requester.UserName=" & pRequester.UserName & "", "FileServe")
      Exit Sub
    End If
    If pLoggedLogin.TimeLoggedOut <> Nothing Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-123340: User is already logged out.", "FileServe")
      Exit Sub
    End If
    If pLoggedLogin.TimeLoggedIn.Date <> Now.Date Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-153358: User did not log in today.", "FileServe")
      Exit Sub
    End If
    'get the user
    Dim pUser As New csUser(pRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, pRequester, pFault, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-153554: Problem loading user - " & pFault.StringForMessageBox, "FileServe")
      Exit Sub
    End If
    'if not allowed multiple logins then check that that login matches LastloggedinID
    If pUser.EnableSimultaneousLogins = False Then
      Dim pUserStatus As New csUserStatus()
      pFault = pUserStatus.GetByUserIDAndApplicationName(pRequester.UserID, pRequester.CallingApplication, pRequester, vMustExist:=True)
      If Not pFault.isOK Then
        Tools.LogToTextFile.WriteMessage("TRGT-240220-153848: Problem loading UserStatus - " & pFault.StringForMessageBox, "FileServe")
        Exit Sub
      End If
      If pRequester.LoggedLoginID <> pUserStatus.LastLoggedLoginID Then
        Tools.LogToTextFile.WriteMessage($"TRGT-240220-153848: I didn't get the expected loginID - I got {pRequester.LoggedLoginID} when I expected {pUserStatus.LastLoggedLoginID} ", "FileServe")
        Exit Sub
      End If
    End If

    Dim pTask As String = Request.QueryString("Task")

    'Get the root
    Dim pSystemDefault As New csSystemDefault()
    pFault = pSystemDefault.GetByFullSettingName(csSystemDefault.enmFullSettingName.Config_UploadedFilesRootFolder, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage("TRGT-240220-124001: " & pFault.StringForMessageBox, "FileServe")
      Exit Sub
    End If
    Dim pRootFolder As String = pSystemDefault.SettingValue : If Not pRootFolder.EndsWith("\") Then pRootFolder = pRootFolder & "\"

    Dim pFileName As String = Request.QueryString("FileName")


    Dim pFullFileName = $"{pRootFolder}{pTask}\{pFileName}"

    If Not IO.File.Exists(pFullFileName) Then
      pFault.LogFreeTextFault(65, $"The file {pFullFileName} was not found", pFunctionParameters, "TRGT-240220140158", pRequester)
      Tools.LogToTextFile.WriteMessage($"TRGT-240220-124540: Could not find file {pFullFileName}", "FileServe")
      Exit Sub
    End If

    ' Serve the file to the client
    Response.ContentType = "application/octet-stream"
    'Response.AppendHeader("Content-Disposition", "attachment; filename=TheFile.jpg") //use is you want to save the file
    Response.AppendHeader("Content-Disposition", "inline")
    Response.TransmitFile(pFullFileName)
    Response.End()

  End Sub

End Class