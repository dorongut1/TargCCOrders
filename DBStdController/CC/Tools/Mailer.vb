Option Strict On

Namespace Tools
  ''' <summary>
  ''' Need key in AppSettings for:"EmailFrom","NameFrom","EmailReplyTo","NameReplyTo","SMTPServer","SMTPUserName","SMTPPassword","SMTPPort","SMTPEnableSSL"(True,False)
  ''' If you don't want the computer name used in the last line (with the time) then provide a key for 'ServerNameForMail'
  ''' </summary>
  ''' <remarks></remarks>
  Public Class Mailer

    Private Shared _EmailFrom As String
    Private Shared _NameFrom As String
    Private Shared _EmailReplyTo As String
    Private Shared _NameReplyTo As String
    Private Shared _SMTPServer As String
    Private Shared _SMTPUserName As String
    Private Shared _SMTPPassword As String
    Private Shared _SMTPPort As Integer
    Private Shared _SMTPEnableSSL As Boolean
    Private Shared _ServerNameForMail As String

    Public Enum enmMessageAsHTML
      UD
      LeaveAsText
      TextToHTML
      LeaveAsHTML
    End Enum

    ''' <summary>
    ''' Send mail regarding exception to multiple users. The recipient emails are ';' or NewLine delimited. Need key in AppSettings for:"EmailFrom","NameFrom","EmailReplyTo","NameReplyTo","SMTPServer","SMTPUserName","SMTPPassword","SMTPPort","SMTPEnableSSL".
    ''' If you don't want mail sent, ensure SMTP field is blank in the config file
    ''' </summary>
    ''' <param name="vSubject"></param>
    ''' <param name="vMailToMultiple"></param>
    ''' <param name="fMessageBodyPrefix"></param>
    ''' <param name="vException"></param>
    ''' <param name="vRTL"></param>
    ''' <param name="vEmailFrom"></param>
    ''' <param name="vNameFrom"></param>
    ''' <param name="vEmailReplyTo"></param>
    ''' <param name="vNameReplyTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SendExceptionByMailToMultipleRecipients(ByVal vSubject As String, ByVal vMailToMultiple As String, ByVal fMessageBodyPrefix As String, ByVal vException As Exception, Optional ByVal vMessageAsHTML As enmMessageAsHTML = enmMessageAsHTML.LeaveAsText, Optional ByVal vRTL As Boolean = False, Optional ByVal vEmailFrom As String = "", Optional ByVal vNameFrom As String = "", Optional ByVal vEmailReplyTo As String = "", Optional ByVal vNameReplyTo As String = "") As String
      Return SendMailForReal("", vSubject & ":Exception", vMailToMultiple, "", "", "", "Exception:" & Environment.NewLine & fMessageBodyPrefix & Environment.NewLine & Environment.NewLine & Tools.LogToTextFile.GetExceptionString(vException).Replace("~", Environment.NewLine) & Environment.NewLine & vException.ToString, vMessageAsHTML, vRTL, vEmailFrom, vNameFrom, vEmailReplyTo, vNameReplyTo, "")
    End Function

    ''' <summary>
    ''' Can mail to multiple users. The recipient emails are ';' or NewLine delimited. If sending display name, send it before the email, separated with a comma (John Doe, johnd@ik.com;). Filenames are ';' delimited. If there is no file, then send an empty string. If there is a file, it deletes it after sending. If an attachment is a string, then its filename should be the last filename.
    ''' </summary>
    ''' <param name="vFileName"></param>
    ''' <param name="vSubject"></param>
    ''' <param name="vMailToMultiple"></param>
    ''' <param name="vMessageBody"></param>
    ''' <param name="vMessageAsHTML"></param>
    ''' <param name="vRTL"></param>
    ''' <param name="vEmailFrom"></param>
    ''' <param name="vNameFrom"></param>
    ''' <param name="vEmailReplyTo"></param>
    ''' <param name="vNameReplyTo"></param>
    ''' <param name="vAttachmentString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SendMailToMultipleRecipients(ByVal vFileName As String, ByVal vSubject As String, ByVal vMailToMultiple As String, ByVal vMessageBody As String, Optional ByVal vMessageAsHTML As enmMessageAsHTML = enmMessageAsHTML.LeaveAsText, Optional ByVal vRTL As Boolean = False, Optional ByVal vEmailFrom As String = "", Optional ByVal vNameFrom As String = "", Optional ByVal vEmailReplyTo As String = "", Optional ByVal vNameReplyTo As String = "", Optional ByVal vAttachmentString As String = "") As String
      Return SendMailForReal(vFileName, vSubject, vMailToMultiple, "", "", "", vMessageBody, vMessageAsHTML, vRTL, vEmailFrom, vNameFrom, vEmailReplyTo, vNameReplyTo, vAttachmentString)
    End Function

    ''' <summary>
    ''' Can mail to multiple users. The recipient emails are ';' or NewLine delimited. If sending display name, send it before the email, separated with a comma (John Doe, johnd@ik.com;). Filenames are ';' delimited. If there is no file, then send an empty string. If there is a file, it deletes it after sending. If an attachment is a string, then its filename should be the last filename.
    ''' </summary>
    ''' <param name="vFileName"></param>
    ''' <param name="vSubject"></param>
    ''' <param name="vMailToMultiple"></param>
    ''' <param name="vMailCCMultiple"></param>
    ''' <param name="vMessageBody"></param>
    ''' <param name="vMessageAsHTML"></param>
    ''' <param name="vRTL"></param>
    ''' <param name="vEmailFrom"></param>
    ''' <param name="vNameFrom"></param>
    ''' <param name="vEmailReplyTo"></param>
    ''' <param name="vNameReplyTo"></param>
    ''' <param name="vAttachmentString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function SendMailToMultipleRecipients(ByVal vFileName As String, ByVal vSubject As String, ByVal vMailToMultiple As String, ByVal vMailCCMultiple As String, ByVal vMessageBody As String, Optional ByVal vMessageAsHTML As enmMessageAsHTML = enmMessageAsHTML.LeaveAsText, Optional ByVal vRTL As Boolean = False, Optional ByVal vEmailFrom As String = "", Optional ByVal vNameFrom As String = "", Optional ByVal vEmailReplyTo As String = "", Optional ByVal vNameReplyTo As String = "", Optional ByVal vAttachmentString As String = "") As String
      Return SendMailForReal(vFileName, vSubject, vMailToMultiple, vMailCCMultiple, "", "", vMessageBody, vMessageAsHTML, vRTL, vEmailFrom, vNameFrom, vEmailReplyTo, vNameReplyTo, vAttachmentString)
    End Function

    ''' <summary>
    ''' Sends mail to one user, prettily formatted with name and email. Filenames are ';' delimited. If an attachment is a string, then its filename should be the last filename.
    ''' </summary>
    ''' <param name="vFileName"></param>
    ''' <param name="vSubject"></param>
    ''' <param name="vMailToName"></param>
    ''' <param name="vMailToAddress"></param>
    ''' <param name="vMessageBody"></param>
    ''' <param name="vMessageAsHTML"></param>
    ''' <param name="vRTL"></param>
    ''' <param name="vEmailFrom"></param>
    ''' <param name="vNameFrom"></param>
    ''' <param name="vEmailReplyTo"></param>
    ''' <param name="vNameReplyTo"></param>
    ''' <param name="vAttachmentString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function SendMailToSingleRecipient(ByVal vFileName As String, ByVal vSubject As String, ByVal vMailToName As String, ByVal vMailToAddress As String, ByVal vMessageBody As String, Optional ByVal vMessageAsHTML As enmMessageAsHTML = enmMessageAsHTML.LeaveAsText, Optional ByVal vRTL As Boolean = False, Optional ByVal vEmailFrom As String = "", Optional ByVal vNameFrom As String = "", Optional ByVal vEmailReplyTo As String = "", Optional ByVal vNameReplyTo As String = "", Optional ByVal vAttachmentString As String = "") As String
      Return SendMailForReal(vFileName, vSubject, "", "", vMailToName, vMailToAddress, vMessageBody, vMessageAsHTML, vRTL, vEmailFrom, vNameFrom, vEmailReplyTo, vNameReplyTo, vAttachmentString)
    End Function

    ''' <summary>
    ''' Send only a message to one user, with no attachment
    ''' </summary>
    ''' <param name="vSubject"></param>
    ''' <param name="vMailToName"></param>
    ''' <param name="vMailToAddress"></param>
    ''' <param name="vMessageBody"></param>
    ''' <param name="vMessageAsHTML"></param>
    ''' <param name="vRTL"></param>
    ''' <param name="vEmailFrom"></param>
    ''' <param name="vNameFrom"></param>
    ''' <param name="vEmailReplyTo"></param>
    ''' <param name="vNameReplyTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function SendMessage(ByVal vSubject As String, ByVal vMailToName As String, ByVal vMailToAddress As String, ByVal vMessageBody As String, Optional ByVal vMessageAsHTML As enmMessageAsHTML = enmMessageAsHTML.LeaveAsText, Optional ByVal vRTL As Boolean = False, Optional ByVal vEmailFrom As String = "", Optional ByVal vNameFrom As String = "", Optional ByVal vEmailReplyTo As String = "", Optional ByVal vNameReplyTo As String = "") As String
      Return SendMailForReal("", vSubject, "", "", vMailToName, vMailToAddress, vMessageBody, vMessageAsHTML, vRTL, vEmailFrom, vNameFrom, vEmailReplyTo, vNameReplyTo, "")
    End Function

    Private Shared _MailObj As New Object

    Friend Class MailCounter
      Friend Count As Integer
      Friend LastSent As Date
    End Class

    Private Shared _Mails As New Dictionary(Of String, MailCounter)
    Private Shared _LastHour As Integer = -1


    Private Shared Function SendMailForReal(ByVal vFileName As String,
                                    ByVal vSubject As String,
                                    ByVal vMailToMultiple As String,
                                    ByVal vMailCCMultiple As String,
                                    ByVal vMailToName As String,
                                    ByVal vMailToAddress As String,
                                    ByVal vMessageBody As String,
                                    ByVal vMessageAsHTML As enmMessageAsHTML,
                                    ByVal vRTL As Boolean,
                                    ByVal vEmailFrom As String,
                                    ByVal vNameFrom As String,
                                    ByVal vEmailReplyTo As String,
                                    ByVal vNameReplyTo As String,
                                    ByVal vAttachmentString As String) As String

      SyncLock _MailObj
        Try
          Dim pAuthenticationRequired As Boolean

          If Not String.IsNullOrEmpty(vMailToMultiple) Then
            vMailToMultiple = vMailToMultiple.Replace(Environment.NewLine, ";")
          End If

          Dim pMinutesToHold As Integer = 60

          'clean up if needed
          If DateTime.Now.Hour <> _LastHour Then
            Tools.LogToTextFile.WriteMessage("Started Cache Cleanup. Mails.Count = " & _Mails.Count, "Mailer")
            'remove all those that are over 2 hours old
            Dim pToRemove As New List(Of String)
            For Each l In _Mails
              If DateTime.Now.Subtract(l.Value.LastSent).TotalMinutes > pMinutesToHold * 2 Then
                pToRemove.Add(l.Key)
              End If
            Next
            If pToRemove.Count > 0 Then
              For Each l In pToRemove
                _Mails.Remove(l)
              Next
            End If
            Tools.LogToTextFile.WriteMessage("Finished Cache Cleanup. Mails.Count = " & _Mails.Count, "Mailer")
            _LastHour = DateTime.Now.Hour
          End If

          Dim pMessageToHash As String = ""
          Dim pIndexOfAlertID As Integer = vMessageBody.IndexOf("Alert ID: ")
          If pIndexOfAlertID > 0 Then
            pMessageToHash = vMessageBody.Substring(0, pIndexOfAlertID)
          Else
            pMessageToHash = vMessageBody
          End If
          'Tools.LogToTextFile.WriteMessage("pMessageToHash" & pMessageToHash, "Mailer")

          'check if it exists
          Dim pCountToReport As Integer = 0
          Dim pMailHash As String = ccHelper.Encrypt(ccHelper.enmHashType.SHA256, vSubject & vMailToMultiple & vMailCCMultiple & vMailToAddress & pMessageToHash)
          'Tools.LogToTextFile.WriteMessage("Hash" & pMailHash, "Mailer")
          If _Mails.ContainsKey(pMailHash) Then
            Dim pMailCounter As MailCounter = _Mails(pMailHash)
            If DateTime.Now.Subtract(pMailCounter.LastSent).TotalMinutes < pMinutesToHold Then
              pMailCounter.Count += 1
              Tools.LogToTextFile.WriteMessage("    ** Cache: " & vSubject & " to " & vMailToMultiple & vMailCCMultiple & vMailToAddress & " SAVED (not sent). Count = " & pMailCounter.Count, "Mailer")
              Return "OK"
            Else
              pCountToReport = pMailCounter.Count
              Tools.LogToTextFile.WriteMessage("    ** Cache: " & vSubject & " to " & vMailToMultiple & vMailCCMultiple & vMailToAddress & " sent (time expired) !! Count was " & pMailCounter.Count, "Mailer")
              pMailCounter.Count = 0
              pMailCounter.LastSent = DateTime.Now
            End If
          Else
            Dim pMailCounter As New MailCounter With {.Count = 0, .LastSent = DateTime.Now}
            _Mails.Add(pMailHash, pMailCounter)
            pCountToReport = pMailCounter.Count
            Tools.LogToTextFile.WriteMessage("    ** Cache: " & vSubject & " to " & vMailToMultiple & vMailCCMultiple & vMailToAddress & " added to list and sent !! ", "Mailer")
          End If


          'Check config data
          If String.IsNullOrWhiteSpace(_SMTPServer) Then
            _SMTPServer = MyController.SMTPServer
            If _SMTPServer = "" Then
              Tools.LogToTextFile.WriteMessage("SendMail aborted since no SMTP Server defined. Ignore if in TestMode.", "Mailer")
              Return ("SendMail aborted since no SMTP Server defined. Ignore if in TestMode.")
            End If

            _EmailFrom = MyController.SMTPEmailFrom
            If _EmailFrom = "" Then
              Return ("Require 'EmailFrom' in AppSettings")
            End If
            _NameFrom = MyController.SMTPNameFrom
            If _NameFrom = "" Then
              Return ("Require 'NameFrom' in AppSettings")
            End If

            _EmailReplyTo = MyController.SMTPDefaultEmailReplyTo
            _NameReplyTo = MyController.SMTPDefaultNameReplyTo

            _SMTPUserName = MyController.SMTPUserName
            _SMTPPassword = MyController.SMTPPassword
            Try
              _ServerNameForMail = MyController.ServerNameForMail
              If String.IsNullOrEmpty(_ServerNameForMail) Then
                Try
                  Dim pIPGlobalProperties As Net.NetworkInformation.IPGlobalProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()
                  If String.IsNullOrEmpty(pIPGlobalProperties.DomainName) Then
                    _ServerNameForMail = $"{pIPGlobalProperties.HostName}"
                  Else
                    _ServerNameForMail = $"{pIPGlobalProperties.HostName}.{pIPGlobalProperties.DomainName}"
                  End If
                Catch ex As Exception
                  If Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase) Then
                    _ServerNameForMail = $"{Environment.MachineName}"
                  Else
                    _ServerNameForMail = $"{Environment.MachineName}.{Environment.UserDomainName}"
                  End If
                End Try
              End If
            Catch ex As Exception
              _ServerNameForMail = Environment.MachineName
            End Try
            If _SMTPUserName.Length > 0 Then
              'require that they send if SSL is required
              _SMTPEnableSSL = MyController.SMTPEnableSSL
            End If
            _SMTPPort = MyController.SMTPPort
            If _SMTPPort = 1 Then
              If _SMTPEnableSSL = False Then
                _SMTPPort = 25
              Else
                _SMTPPort = 587
              End If
            End If
          End If

          If _SMTPUserName.Length > 0 Then
            pAuthenticationRequired = True
          Else
            pAuthenticationRequired = False
          End If

          'Start
          Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage
          Dim oAttch As System.Net.Mail.Attachment = Nothing

          If vEmailFrom <> "" Then
            oMsg.From = New System.Net.Mail.MailAddress(vEmailFrom, vNameFrom)
          Else
            oMsg.From = New System.Net.Mail.MailAddress(_EmailFrom, _NameFrom)
          End If

          If vEmailReplyTo <> "" Then
            If Not String.IsNullOrEmpty(vNameReplyTo) Then
              oMsg.ReplyToList.Add(New System.Net.Mail.MailAddress(vEmailReplyTo, vNameReplyTo))
            Else
              oMsg.ReplyToList.Add(vEmailReplyTo)
            End If
          ElseIf _EmailReplyTo <> "" Then
            If Not String.IsNullOrEmpty(_NameReplyTo) Then
              oMsg.ReplyToList.Add(New System.Net.Mail.MailAddress(_EmailReplyTo, _NameReplyTo))
            Else
              oMsg.ReplyToList.Add(_EmailReplyTo)
            End If
          End If

          If vMailToMultiple.Length > 0 Then
            Dim pMailTos As String() = vMailToMultiple.Split(";"c)
            For Each pAddr As String In pMailTos
              If String.IsNullOrEmpty(pAddr.Trim) Then Continue For
              If pAddr.IndexOf(",") < 0 Then
                Try
                  oMsg.To.Add(pAddr)
                Catch ex As Exception
                  Return "Email: " & pAddr & " is not valid"
                End Try
              Else
                Try
                  oMsg.To.Add(New System.Net.Mail.MailAddress(pAddr.Split(","c)(1).Trim, pAddr.Split(","c)(0).Trim))
                Catch ex As Exception
                  Return "Email: " & pAddr & " is not valid"
                End Try
              End If
            Next
            'If we have this, then we can get CCs as well
            If vMailCCMultiple.Length > 0 Then
              Dim pMailCCs As String() = vMailCCMultiple.Replace(Environment.NewLine, ";").Split(";"c)
              For Each pAddr As String In pMailCCs
                If String.IsNullOrEmpty(pAddr.Trim) Then Continue For
                If pAddr.IndexOf(",") < 0 Then
                  Try
                    oMsg.CC.Add(pAddr)
                  Catch ex As Exception
                    Return "Email: " & pAddr & " is not valid"
                  End Try
                Else
                  Try
                    oMsg.CC.Add(New System.Net.Mail.MailAddress(pAddr.Split(","c)(1).Trim, pAddr.Split(","c)(0).Trim))
                  Catch ex As Exception
                    Return "Email: " & pAddr & " is not valid"
                  End Try
                End If
              Next
            End If
          ElseIf vMailToAddress.Length > 0 Then
            Try
              oMsg.To.Add(New System.Net.Mail.MailAddress(vMailToAddress, vMailToName))
            Catch ex As Exception
              Return "Email: " & vMailToAddress & " is not valid. Error: " & ex.Message
            End Try
          Else
            Return "I have no address to send the email to!"
          End If

          oMsg.Subject = vSubject

          'Add ServerName and Time
          'fMessageBody = fMessageBody & Environment.NewLine & Environment.NewLine & pServerNameForMail & " " & DateTime.Now.ToString("yyyyMMddTHHmmss")
          vMessageBody = vMessageBody & Environment.NewLine & Environment.NewLine
          If pCountToReport > 0 Then
            vMessageBody &= $"Note: There were {pCountToReport} identical emails in the last {pMinutesToHold} minutes that the system avoided sending." & Environment.NewLine
          End If
          Dim pSuffix As String = _ServerNameForMail & " " & DateTime.Now.ToUniversalTime.ToString("yyyyMMddTHHmmss")

          Dim pMessage As String = ""
          If vMessageAsHTML = enmMessageAsHTML.TextToHTML Then
            oMsg.IsBodyHtml = True
            If vRTL = True Then
              pMessage = "<p style=""font-family: sans-serif, Arial, sans-serif; font-size: small; direction: rtl;"">"
            Else
              pMessage = "<p style=""font-family: sans-serif, Arial, sans-serif; font-size: small;"">"
            End If
            pMessage &= vMessageBody.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br/>")
            pMessage &= "</p>"
            pMessage &= "<p style=""font-family: sans-serif, Helvetica, sans-serif; font-size: xx-small; direction: ltr;"">"
            pMessage &= pSuffix & "Z (T)"
            pMessage &= "</p>"
          ElseIf vMessageAsHTML = enmMessageAsHTML.LeaveAsHTML Then
            oMsg.IsBodyHtml = True
            pMessage &= vMessageBody
            pMessage &= "<p style=""font-family: sans-serif, Helvetica, sans-serif; font-size: xx-small; direction: ltr;"">"
            pMessage &= pSuffix & "Z (H)"
            pMessage &= "</p>"
          ElseIf vMessageAsHTML = enmMessageAsHTML.LeaveAsText Then
            oMsg.IsBodyHtml = False
            'http://stackoverflow.com/questions/247546/outlook-autocleaning-my-line-breaks-and-screwing-up-my-email-format
            'You can also insert a tab character at the end of the line (just before the CR LF)
            pMessage = vMessageBody.Replace(Environment.NewLine, "   " & Environment.NewLine) & Environment.NewLine & pSuffix & "Z (P)"
          End If

          oMsg.Body = pMessage

          vFileName = vFileName.Trim
          If vFileName <> "" Then
            If vFileName.EndsWith(";") = True Then
              vFileName = vFileName.Substring(0, vFileName.Length - 1)
            End If
            Dim pFiles As String() = vFileName.Split(";"c)
            For i As Integer = 0 To pFiles.Length - 1
              Dim pCreateFile As Boolean = False
              If i = pFiles.Length - 1 Then
                If vAttachmentString <> "" Then
                  pCreateFile = True
                End If
              End If
              If pCreateFile = False Then
                oAttch = New System.Net.Mail.Attachment(pFiles(i), System.Net.Mime.MediaTypeNames.Application.Octet)
                oMsg.Attachments.Add(oAttch)
              Else
                'Dim pBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(vAttachmentString)
                Dim pBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(vAttachmentString) 'Fixed for non-western
                Dim pMs As New System.IO.MemoryStream(pBytes)
                oMsg.Attachments.Add(New System.Net.Mail.Attachment(pMs, pFiles(i), "text/plain"))
              End If
            Next
          End If

          Dim pLogger As New Text.StringBuilder
          pLogger.Append($"Sending {oMsg.Subject} to {oMsg.To.ToString()} cc {oMsg.CC.ToString()} ")
          Tools.LogToTextFile.WriteMessage(pLogger.ToString(), "Mailer")

          Dim mailClient As New System.Net.Mail.SmtpClient()
          'Put your own, or your ISPs, mail server name on this next line
          mailClient.Host = _SMTPServer
          If pAuthenticationRequired = True Then
            'This object stores the authentication values
            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(_SMTPUserName, _SMTPPassword)
            mailClient.UseDefaultCredentials = False
            mailClient.Credentials = basicAuthenticationInfo
            mailClient.Port = _SMTPPort
            mailClient.EnableSsl = _SMTPEnableSSL
          End If

          Try
            mailClient.Send(oMsg)
          Catch e As Exception
            Threading.Thread.Sleep(2000)
            Try
              mailClient.Send(oMsg)
            Catch ex As Exception
              pLogger.AppendLine($"     {ex.Message}")
              Tools.LogToTextFile.WriteMessage(pLogger.ToString(), "Mailer")
              Return ($"SendMail Failed (2 tries). SMTPServer: {_SMTPServer}, SMTPUserName: {_SMTPUserName}, SMTPPasswordLength: {_SMTPPassword.Length}, SMTPEnableSSL: {_SMTPEnableSSL}, SMTPPort: {_SMTPPort}{Environment.NewLine}TRGT-240910-111659{Environment.NewLine}Exception: {ex}")
            End Try
          End Try

          If vFileName <> "" Then
            For Each pAttch As System.Net.Mail.Attachment In oMsg.Attachments
              pAttch.Dispose()
            Next
            oMsg.Attachments.Clear()
            oMsg = Nothing
            Dim pFiles As String() = vFileName.Split(";"c)
            For i As Integer = 0 To pFiles.Length - 1
              Dim pDeleteFile As Boolean = True
              If i = pFiles.Length - 1 Then
                If vAttachmentString <> "" Then
                  pDeleteFile = False
                End If
              End If
              If pDeleteFile = True Then
                Try
                  IO.File.Delete(pFiles(i))
                Catch ex As Exception
                  LogToTextFile.WriteException("Could not delete file:" & pFiles(i), ex, "Mailer")
                End Try
              End If
            Next
          Else
            oMsg = Nothing
          End If
        Catch ex As Exception
          Tools.LogToTextFile.WriteMessage($"     {ex.Message}", "Mailer")
          Return "SendMail had an exception:" & ex.Message
        End Try
      End SyncLock

      Tools.LogToTextFile.WriteMessage($"     OK", "Mailer")
      Return "OK"
    End Function

  End Class
End Namespace