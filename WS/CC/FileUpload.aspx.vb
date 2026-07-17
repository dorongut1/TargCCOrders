Public Class FileUpload
  Inherits System.Web.UI.Page
  Protected MyResponse As String


  Private Event evtDoTask(vID As Long, vTask As String, vRequester As clsRequester, ByRef rResponseToWrite As String, ByRef rFault As clsFault)
  Private Event evtDoAutoFileUploadTask(vID As Long, vTable As String, vField As String, vRequester As clsRequester, ByRef rResponseToWrite As String, ByRef rFault As clsFault)
  Private Event evtChangeFolder(vID As Long, vTask As String, vRequester As clsRequester, ByRef rNewRootfolderName As String, ByRef rNewSubfolderName As String, ByRef rFault As clsFault)

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    Dim Encoding As UnicodeEncoding = New UnicodeEncoding(True, False)
    Response.ContentEncoding = Encoding

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Request('ID') = " & Request.QueryString("ID"))
    pLoggedText.AppendLine("Request('Task') = " & Request.QueryString("Task"))
    pLoggedText.AppendLine("Request('FileNameToSaveAs') = " & Request.QueryString("FileNameToSaveAs"))
    pLoggedText.AppendLine("Request('TKT') = " & Request.QueryString("TKT"))
    pLoggedText.AppendLine("Request.UserHostAddress = " & Request.UserHostAddress)
    pLoggedText.Append("Request('WhatSent') = " & Request.QueryString("WhatSent"))
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "FileUpload")

    If Request.QueryString("Task") = Nothing Then
      Exit Sub
    End If

    'Requester
    Dim pRequester As clsRequester = Nothing
    Try
      Dim pTicketBase64 As String = Request.QueryString("TKT")
      Dim bytes As Byte() = Convert.FromBase64String(pTicketBase64)
      Dim pTicket As String = Text.Encoding.UTF8.GetString(bytes)
      'Create the requester
      pRequester = New clsRequester(pTicket)
    Catch ex As Exception
      MyResponse = "Bad ticket"
      Tools.LogToTextFile.WriteException(MyResponse, ex, "FileUpload")
      Exit Sub
    End Try
    'In case of previous failure
    Dim pLoggedLoginID As Long = pRequester.LoggedLoginID
    If pLoggedLoginID < 0 Then
      pLoggedLoginID = -pLoggedLoginID - 10
    End If
    'Check that the logged login is valid
    Dim pFault As New clsFault
    Dim pLoggedLogin As New csLoggedLogin
    pFault = pLoggedLogin.GetByID(pLoggedLoginID, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      MyResponse = ""
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1647: " & pFault.StringForMessageBox, "FileUpload")
      Exit Sub
    End If
    If pLoggedLogin.UserName <> pRequester.UserName Then
      MyResponse = ""
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1649: User names do not match. LoggedLogin.UserName=" & pLoggedLogin.UserFullName & ", Requester.UserName=" & pRequester.UserName & "", "FileUpload")
      Exit Sub
    End If
    If pLoggedLogin.TimeLoggedOut <> Nothing Then
      MyResponse = ""
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1650: User is already logged out.", "FileUpload")
      Exit Sub
    End If

    Dim pFaultParameters As String = String.Format("ID:{0}, Task:{1}", Request.QueryString("ID"), Request.QueryString("Task"))

    'Check file save folder
    Dim pSaveFileToRoot As String = MyController.UploadedFilesRootFolder
    If String.IsNullOrEmpty(pSaveFileToRoot) Then
      MyResponse = "TargCCOrders.UploadedFilesRootFolder root not defined"
      Tools.LogToTextFile.WriteMessage(MyResponse, "FileUpload")
      pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-190212-1828", pRequester)
      Exit Sub
    End If
    If Not IO.Directory.Exists(pSaveFileToRoot) Then
      MyResponse = "TargCCOrders.UploadedFilesRootFolder root '" & pSaveFileToRoot & "' does not exist, or cannot be accessed"
      Tools.LogToTextFile.WriteMessage(MyResponse, "FileUpload")
      pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-190212-1827", pRequester)
      Exit Sub
    End If

    Dim pID As Long
    Try
      pID = ccHelper.ToLong(Request.QueryString("ID"))
    Catch ex As Exception
      MyResponse = "Bad ID"
      Tools.LogToTextFile.WriteException(MyResponse, ex, "FileUpload")
      pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-190212-1826", pRequester)
      Exit Sub
    End Try

    Dim pTask As String = Request.QueryString("Task")
    Dim pFileNameToSaveAs As String = Request.QueryString("FileNameToSaveAs")

    'Check that the task is valid
    Dim pTable As String = ""
    Dim pField As String = ""
    Dim pExtension As String = ""
    If pTask.Equals("AutoFileUpload", StringComparison.OrdinalIgnoreCase) Then
      Dim pWorkingText As String = ""
      Try
        pWorkingText = pFileNameToSaveAs.Split("."c)(0) 'remove extension
        pExtension = pFileNameToSaveAs.Split("."c)(1)

        pTable = pWorkingText.Split("_"c)(0)
        pField = pWorkingText.Split("_"c)(1)
      Catch ex As Exception
        MyResponse = "Bad pFileNameToSaveAs: " & pFileNameToSaveAs
        Tools.LogToTextFile.WriteException(MyResponse, ex, "FileUpload")
        pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-210312-1842", pRequester)
      End Try
      pFileNameToSaveAs = pWorkingText & "_" & pID & "." & pExtension
      If Request.ContentLength = 0 AndAlso pExtension.Equals("del", StringComparison.OrdinalIgnoreCase) Then
        pFileNameToSaveAs = pWorkingText & "_" & pID & ".*"
      End If
    End If

    pSaveFileToRoot = MyController.UploadedFilesRootFolder
    Dim pSubFolderName As String = pTask
    RaiseEvent evtChangeFolder(pID, pTask, pRequester, pSaveFileToRoot, pSubFolderName, pFault) 'Enable overriding the SubFolderName
    If Not pFault.isOK() Then
      MyResponse = pFault.ShortStringForConcatenation
      Tools.LogToTextFile.WriteMessage(MyResponse, "FileUpload")
      Exit Sub
    End If
    'Check if overridden
    If Not pSaveFileToRoot.Equals(MyController.UploadedFilesRootFolder) Then
      If Not pSaveFileToRoot.EndsWith("\") Then pSaveFileToRoot = pSaveFileToRoot & "\"
      If String.IsNullOrEmpty(pSaveFileToRoot) Then
        MyResponse = "Overridden root is not defined"
        Tools.LogToTextFile.WriteMessage(MyResponse, "FileUpload")
        pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-200910-1644", pRequester)
        Exit Sub
      End If
      If Not IO.Directory.Exists(pSaveFileToRoot) Then
        MyResponse = "Overridden root '" & pSaveFileToRoot & "' does not exist, or cannot be accessed"
        Tools.LogToTextFile.WriteMessage(MyResponse, "FileUpload")
        pFault.LogFreeTextFault(1050, MyResponse, pFaultParameters, "TRGT-200910-1645", pRequester)
        Exit Sub
      End If
    End If

    Dim pSaveFileTo As String = pSaveFileToRoot & pSubFolderName & "\"
    If Not IO.Directory.Exists(pSaveFileTo) Then
      'Create it
      Try
        IO.Directory.CreateDirectory(pSaveFileTo)
      Catch ex As Exception
        MyResponse = "Folder " & pSubFolderName & " under TargCCOrders.UploadedFilesRootFolder '" & pSaveFileToRoot & "' could not be created."
        Tools.LogToTextFile.WriteException(MyResponse, ex, "FileUpload")
        pFault.LogException(1050, ex, MyResponse, "TRGT-190212-1825", pRequester)
        Exit Sub
      End Try
    End If
    Dim pSaveFileToArchive As String = pSaveFileTo & "Archive\"
    If Not IO.Directory.Exists(pSaveFileToArchive) Then
      'Create it
      Try
        IO.Directory.CreateDirectory(pSaveFileToArchive)
      Catch ex As Exception
        MyResponse = "Folder " & pSubFolderName & " under TargCCOrders.UploadedFilesRootFolder '" & pSaveFileToRoot & "'. Archive could not be created."
        Tools.LogToTextFile.WriteException(MyResponse, ex, "FileUpload")
        pFault.LogException(1050, ex, MyResponse, "TRGT-210318-0936", pRequester)
        Exit Sub
      End Try
    End If

    Try
      If Not String.IsNullOrEmpty(pFileNameToSaveAs) AndAlso Request.Files IsNot Nothing AndAlso (Request.Files.Count > 0 OrElse Request.ContentLength > 0) Then
        Dim pExt As String = pFileNameToSaveAs.Split("."c)(1)
        If Request.Files.Count > 0 Then
          Dim pRoot As String = pFileNameToSaveAs.Split("."c)(0)
          If IO.File.Exists($"{pSaveFileTo}\{pFileNameToSaveAs}") Then IO.File.Copy($"{pSaveFileTo}\{pFileNameToSaveAs}", $"{pSaveFileTo}\Archive\{pRoot}_R_{IO.File.GetLastWriteTimeUtc($"{pSaveFileTo}\{pFileNameToSaveAs}"):yyyyMMddTHHmmss}Z.{pExt}")
          Request.Files(0).SaveAs($"{pSaveFileTo}\{pFileNameToSaveAs}")
        Else
          Dim pRoot As String = pFileNameToSaveAs.Split("."c)(0)
          If IO.File.Exists($"{pSaveFileTo}\{pFileNameToSaveAs}") Then IO.File.Copy($"{pSaveFileTo}\{pFileNameToSaveAs}", $"{pSaveFileTo}\Archive\{pRoot}_R_{IO.File.GetLastWriteTimeUtc($"{pSaveFileTo}\{pFileNameToSaveAs}"):yyyyMMddTHHmmss}Z.{pExt}")
          Request.SaveAs($"{pSaveFileTo}\{pFileNameToSaveAs}", False)
        End If
      Else
        If pTask.Equals("AutoFileUpload", StringComparison.OrdinalIgnoreCase) AndAlso Request.ContentLength = 0 AndAlso pExtension.Equals("del", StringComparison.OrdinalIgnoreCase) Then
          'Move file to archive
          For Each l In IO.Directory.EnumerateFiles(pSaveFileTo, pFileNameToSaveAs)
            Try
              Dim pExt As String = l.Split("."c)(1)
              Dim pRoot As String = pFileNameToSaveAs.Split("."c)(0)
              IO.File.Move(l, $"{pSaveFileToArchive}{pRoot}_D_{IO.File.GetLastWriteTimeUtc($"{l}"):yyyyMMddTHHmmss}Z.{pExt}")
            Catch ex As Exception
              Tools.LogToTextFile.WriteException($"Couldn't move file {l} to {pSaveFileToArchive}", ex, "FileUpload")
              Try
                IO.File.Delete(l)
              Catch exx As Exception
                Tools.LogToTextFile.WriteException($"Couldn't delete file {l} to {pSaveFileToArchive} either!", exx, "FileUpload")
              End Try
            End Try
          Next
        Else
          Request.SaveAs(pSaveFileTo & "\" & pFileNameToSaveAs, False)
        End If
      End If
    Catch ex As Exception
      pFault.LogException(1050, ex, pFaultParameters, "TRGT-181209-1807", pRequester)
      MyResponse = "LoggedAlertID=" & pFault.LoggedAlertID
      Tools.LogToTextFile.WriteException("Failed: " & MyResponse, ex, "FileUpload")
      Exit Sub
    End Try

    Dim pResponse As String = ""
    RaiseEvent evtDoTask(pID, pTask, pRequester, pResponse, pFault)
    If Not pFault.isOK() Then
      MyResponse = "LoggedAlertID=" & pFault.LoggedAlertID
      Tools.LogToTextFile.WriteMessage("Failed: " & MyResponse, "FileUpload")
      Exit Sub
    End If
    RaiseEvent evtDoAutoFileUploadTask(pID, pTable, pField, pRequester, pResponse, pFault)
    If Not pFault.isOK() Then
      MyResponse = "LoggedAlertID=" & pFault.LoggedAlertID
      Tools.LogToTextFile.WriteMessage("Failed: " & MyResponse, "FileUpload")
      Exit Sub
    End If

    'Update the SP
    If pTask.Equals("AutoFileUpload", StringComparison.OrdinalIgnoreCase) Then
      pFault = ccHelper.MarkDocumentAsUploaded(pTable, pField, pID, pFileNameToSaveAs, pRequester)
      If Not pFault.isOK() Then
        MyResponse = "LoggedAlertID=" & pFault.LoggedAlertID
        Tools.LogToTextFile.WriteMessage("Failed: " & MyResponse, "FileUpload")
        Exit Sub
      End If
    End If

    Tools.LogToTextFile.WriteMessage("Succeeded", "FileUpload")
    If String.IsNullOrEmpty(pResponse) Then
      MyResponse = "OK"
    Else
      MyResponse = pResponse
    End If

  End Sub
End Class