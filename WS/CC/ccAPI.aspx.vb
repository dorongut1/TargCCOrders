Public Class ccAPI
    Inherits System.Web.UI.Page

  Shared _WSVersion As String = ""

  Public prResponse As String = ""

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim pFunctionParameters As String = "IP:" & My.Request.UserHostAddress
    Dim pFault As New clsFault

    If My.Request.InputStream.Length = 0 Then
      Response.Write("Page up " & DateTime.Now.ToString("HHmmssffff"))
      Response.Flush()
      Exit Sub
    End If

    Dim pExternalTask As String = Request.QueryString("Task")
    If pExternalTask = "ExternalAuthentication" Then
      DoAuthenticationForGuest()
      Response.Flush()
      Exit Sub
    ElseIf pExternalTask = "ExternalAuthenticationGetUserDetails" Then
      GetUserDetailsForGuest()
      Response.Flush()
      Exit Sub
    End If

    'Start the real work here
    Dim pTask As String
    Dim pRequest As Byte()
    Dim pTicket As String
    Dim pVersion As String
    Dim pWSPwd As String

    Dim pRequester As clsRequester = Nothing 'only valid for login, or until login

    pFunctionParameters = "Load InputStream"
    Try
      Dim pRequestStream As IO.Stream = My.Request.InputStream
      Using pMemoryStream As New IO.MemoryStream
        pRequestStream.CopyTo(pMemoryStream)
        pMemoryStream.Position = 0
        'Now read the values
        Using pBinaryReader As New IO.BinaryReader(pMemoryStream, Text.Encoding.UTF8)
          pTask = pBinaryReader.ReadString()
          pRequest = pBinaryReader.ReadBytes(pBinaryReader.ReadInt32)
          pTicket = pBinaryReader.ReadString()
          pVersion = pBinaryReader.ReadString()
          pWSPwd = pBinaryReader.ReadString()
          pBinaryReader.Close()
        End Using
        pMemoryStream.Close()
      End Using
    Catch ex As Exception
      Tools.LogToTextFile.WriteException(pFunctionParameters, ex, "ccAPI")
      pFault.LogException(71, ex, pFunctionParameters, "TRGT-150316-0916", pRequester)
      Response.Flush()
      Exit Sub
    End Try

    pFunctionParameters = "Checking WSPwd"
    If pWSPwd <> MyConfig.WSPwd Then
      Tools.LogToTextFile.WriteMessage(pFunctionParameters & " - Failed", "ccAPI")
      pFault.LogFreeTextFault(72, "Failed", pFunctionParameters, "TRGT-150316-0917", pRequester)
      Response.Flush()
      Exit Sub
    End If

    pFunctionParameters = "Checking Task"
    If pTask = "" Then
      Tools.LogToTextFile.WriteMessage(pFunctionParameters & " - None received", "ccAPI")
      pFault.LogFreeTextFault(72, "Task Not received", pFunctionParameters, "TRGT-150316-0917", pRequester)
      Response.Flush()
      Exit Sub
    End If

    pFunctionParameters = "Create Requester for " & pTask
    'Translate vTicket
    pRequester = Nothing 'only valid for login
    If pTicket <> "" Then
      Try
        pRequester = New clsRequester(pTicket)
      Catch ex As Exception
        Tools.LogToTextFile.WriteException(pFunctionParameters, ex, "ccAPI")
        pFault.LogException(72, ex, pFunctionParameters, "TRGT-150316-0922", pRequester)
        Response.Flush()
        Exit Sub
      End Try
    Else
      'If pTask <> "Login" Then
      '  Tools.LogToTextFile.WriteMessage(pFunctionParameters & " - None received", "ccAPI")
      '  pFault.LogFreeTextFault(72, "None received", pFunctionParameters, "TRGT-150316-0921", pRequester)
      '  Response.Flush()
      '  Exit Sub
      'End If
    End If

    Dim pDoLogin As Boolean = False
    If pTask.StartsWith("ccSecurityLogInBy", StringComparison.OrdinalIgnoreCase) OrElse
        pTask = "ccSecurityCheck2FactorAuthenticationForLogin" OrElse
        pTask = "ccSecurityLogOut" Then
      pDoLogin = True
    End If

    pFunctionParameters = "Executing function for " & pTask
    Dim pResponseBytes As Byte() = Nothing
    Try
      If pDoLogin = True Then
        pFault = csFunctions.DoLogin(pTask, pRequest, pRequester)
        If Not pFault.isOK Then
          If pRequester Is Nothing Then
            pRequester = New clsRequester(pTicket)
          End If
        End If
      Else
        pFault = csFunctions.DoFunction(pTask, pRequest, pResponseBytes, pRequester)
        'Since from here we go back to the outside, set the LoginID back to positive
        '  Negative LoginIDs are used to force a thread to exit after reaching an error. 
        '  It makes no sense to expose it on the other side of the web service,
        'Unfortunately, the idea failed, because it's needed for 2 factor authentication
        'If pRequester.LoggedLoginID < 0 Then pRequester.ReviveLoginID()
      End If
    Catch ex As Exception
      Tools.LogToTextFile.WriteException(pFunctionParameters, ex, "ccAPI")
      pFault.LogException(72, ex, pFunctionParameters, "TRGT-150316-0923", pRequester)
      'Response.Flush()
      'Exit Sub
    End Try

    pFunctionParameters = "Converting Response for " & pTask
    Dim pccAPIBytes As Byte() = Nothing
    Try
      'Convert to Byte array of Fault, requester and response
      Using pMemoryStream As New System.IO.MemoryStream()
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream)
          'pFault
          Dim pLength As Integer = 0
          Dim pByte As Byte() = Nothing
          Dim pFaultInAPI As New clsFault
          pByte = pFault.CreateByteArray(pFaultInAPI, pRequester)
          If Not pFaultInAPI.isOK Then
            Tools.LogToTextFile.WriteMessage(pFunctionParameters & " - pFaultInAPI Not OK: " & pFaultInAPI.StringForMessageBox, "ccAPI")
            Response.Flush()
            Exit Sub
          End If
          pLength = pByte.Length
          pBinaryWriter.Write(pLength)
          pBinaryWriter.Write(pByte, 0, pLength)
          'Requester ticket
          If pRequester IsNot Nothing Then
            pTicket = pRequester.CreateTicket()
          Else
            pTicket = ""
          End If
          pBinaryWriter.Write(pTicket)
          'The Response
          If pDoLogin = True Then
            pLength = 0
            pBinaryWriter.Write(pLength)
          Else
            If pResponseBytes IsNot Nothing Then
              pLength = pResponseBytes.Length
            Else
              pLength = 0
            End If
            pBinaryWriter.Write(pLength)
            If pLength > 0 Then pBinaryWriter.Write(pResponseBytes, 0, pLength)
          End If
          pBinaryWriter.Close()
        End Using

        pccAPIBytes = pMemoryStream.ToArray()

        pMemoryStream.Close()
      End Using
    Catch ex As Exception
      Tools.LogToTextFile.WriteException(pFunctionParameters, ex, "ccAPI")
      pFault.LogException(71, ex, pFunctionParameters, "TRGT-150310-1513", pRequester)
      Response.Flush()
      Exit Sub
    End Try

    Dim pccAPIBytesOut As Byte() = Nothing
    pFunctionParameters = "Compressing Response for " & pTask
    Try
      If pccAPIBytes IsNot Nothing AndAlso pccAPIBytes.Length > 0 Then
        If MyConfig.ccAPICompressionMode = MyConfig.enmccAPICompressionMode.DeflateTargCC Then
          pccAPIBytesOut = ccHelper.CompressDeflate(pccAPIBytes)
        ElseIf MyConfig.ccAPICompressionMode = MyConfig.enmccAPICompressionMode.GzipTargCC Then
          pccAPIBytesOut = ccHelper.CompressGZip(pccAPIBytes)
        ElseIf MyConfig.ccAPICompressionMode = MyConfig.enmccAPICompressionMode.IIS Then
          'Do Nothing - handled  by IIS
          pccAPIBytesOut = pccAPIBytes
        ElseIf MyConfig.ccAPICompressionMode = MyConfig.enmccAPICompressionMode.None Then
          'Do nothing
          pccAPIBytesOut = pccAPIBytes
        End If
      End If
    Catch ex As Exception
      Tools.LogToTextFile.WriteException(pFunctionParameters, ex, "ccAPI")
      pFault.LogException(71, ex, pFunctionParameters, "TRGT-150310-1514", pRequester)
      Response.Flush()
      Exit Sub
    End Try

    Response.OutputStream.Write(pccAPIBytesOut, 0, pccAPIBytesOut.Length)
    Response.Flush()
  End Sub

  Private Sub DoAuthenticationForGuest()
    Dim pFault As clsFault = Nothing

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Task = " & Request.QueryString("Task"))
    pLoggedText.AppendLine("Guest = " & Request.QueryString("Guest"))

    Dim pIP As String = ""
    Dim pCountry As String = ""
    csFunctions.GetRealExternalCountryAndIP(pCountry, pIP)
    pLoggedText.AppendLine("IP = " & pIP)
    pLoggedText.AppendLine("Country = " & pCountry)

    Dim pFunctionParameters As String = pLoggedText.ToString()

    pLoggedText.Append("InputStreamLength = " & Request.InputStream?.Length)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "DoAuthenticationForGuest")

    Dim pGuest As String = Request.QueryString("Guest")
    If String.IsNullOrEmpty(pGuest) Then Return
    If Request.InputStream?.Length = 0 Then Return

    'Get the string
    Dim pInStream As IO.Stream = Request.InputStream
    Dim pLen As Integer = ccHelper.ToInteger(pInStream.Length)
    Dim pByteArray(pLen - 1) As Byte
    Dim iRead As Integer = pInStream.Read(pByteArray, 0, pLen)

    Dim pStringReceived As String = ""
    If pLen > 0 Then pStringReceived = System.Text.Encoding.UTF8.GetString(pByteArray)

    If String.IsNullOrEmpty(pStringReceived) Then
      Tools.LogToTextFile.WriteMessage($"    StringReceived is blank", "DoAuthenticationForGuest")
      Return
    End If

    'Decrypt
    pStringReceived = NETEncryption.clsTripleDES.DecryptData(pStringReceived, pGuest)
    If pStringReceived Is Nothing Then
      Tools.LogToTextFile.WriteMessage($"    String decryption failed", "DoAuthenticationForGuest")
      Return
    End If


    Dim pName As String
    Dim pPassword As String
    Dim pNewPassword As String = ""
    Dim pStringsReceived As String() = pStringReceived.Split(" "c)
    If Not (pStringsReceived.Length = 2 OrElse pStringsReceived.Length = 3) Then
      Tools.LogToTextFile.WriteMessage($"    Invalid string received {pStringReceived}", "DoAuthenticationForGuest")
      Return
    End If
    Try
      pName = pStringsReceived(0)
      pPassword = pStringsReceived(1)
      If pStringsReceived.Length = 3 Then pNewPassword = pStringsReceived(2)
    Catch ex As Exception
      Tools.LogToTextFile.WriteMessage($"    Invalid string received {pStringReceived} - Could not interpret. Exception: {ex.Message}", "DoAuthenticationForGuest")
      Return
    End Try

    Dim pRequester As clsRequester = Nothing

    Dim pAccessingEntity As New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=False, vRequester:=pRequester, rFault:=pFault)
    pAccessingEntity.WSReportedIP = pIP
    pAccessingEntity.WSReportedCountry = pCountry

    pFault = ccSecurity.LogInByNamePwd(pName, pPassword, pRequester, vNewPassword:=pNewPassword, vAccessingEntity:=pAccessingEntity, vGuestSystem:=pGuest)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage($"    Login failed", "DoAuthenticationForGuest")
      Response.Write($"{pFault.Number} {pFault.LoggedAlertID}")
      Return
    End If

    pRequester.CallingFunctionWithinApplication = "ccAPI:DoAuthenticationForGuest"

    'Check that the guest is allowed
    Dim pSystemDefault As New csSystemDefault()
    pFault = pSystemDefault.GetByGroupAndSettingName("AllowedGuest", pGuest, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage($"    Guest {pGuest} not found", "DoAuthenticationForGuest")
      ccSecurity.LogOut(pRequester)
      Return
    End If
    Dim pExpectedIP As String = pSystemDefault.SettingValue
    If Not pExpectedIP.Contains(pIP) Then
      pFault.LogFreeTextFault(78, $"         IP for Guest {pGuest} invalid . Expected {pExpectedIP.Replace(Environment.NewLine, ";")}, got {pIP}.", pFunctionParameters, "TRGT-250430-105006", pRequester)
      Tools.LogToTextFile.WriteMessage($"    IP for Guest {pGuest} invalid . Expected {pExpectedIP.Replace(Environment.NewLine, ";")}, got {pIP}. ", "DoAuthenticationForGuest")
      ccSecurity.LogOut(pRequester)
      Return
    End If

    Response.Write(pRequester.LoggedLoginID)

    ccSecurity.LogOut(pRequester)

  End Sub

  Private Sub GetUserDetailsForGuest()
    Dim pFault As clsFault = Nothing

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Task = " & Request.QueryString("Task"))
    pLoggedText.AppendLine("Guest = " & Request.QueryString("Guest"))

    Dim pIP As String = ""
    Dim pCountry As String = ""
    csFunctions.GetRealExternalCountryAndIP(pCountry, pIP)
    pLoggedText.AppendLine("IP = " & pIP)
    pLoggedText.AppendLine("Country = " & pCountry)

    Dim pFunctionParameters As String = pLoggedText.ToString()

    pLoggedText.Append("InputStreamLength = " & Request.InputStream?.Length)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "GetUserDetailsForGuest")

    Dim pGuest As String = Request.QueryString("Guest")
    If String.IsNullOrEmpty(pGuest) Then Return
    If Request.InputStream?.Length = 0 Then Return

    'Get the string
    Dim pInStream As IO.Stream = Request.InputStream
    Dim pLen As Integer = ccHelper.ToInteger(pInStream.Length)
    Dim pByteArray(pLen - 1) As Byte
    Dim iRead As Integer = pInStream.Read(pByteArray, 0, pLen)

    Dim pStringReceived As String = ""
    If pLen > 0 Then pStringReceived = System.Text.Encoding.UTF8.GetString(pByteArray)

    If String.IsNullOrEmpty(pStringReceived) Then
      Tools.LogToTextFile.WriteMessage($"    StringReceived is blank", "GetUserDetailsForGuest")
      Return
    End If

    'Decrypt
    pStringReceived = NETEncryption.clsTripleDES.DecryptData(pStringReceived, pGuest)
    If pStringReceived Is Nothing Then
      Tools.LogToTextFile.WriteMessage($"    String decryption failed", "GetUserDetailsForGuest")
      Return
    End If

    Dim pUserNameForDetails As String
    Dim pPasswordForLogin As String
    Dim pStringsReceived As String() = pStringReceived.Split(" "c)
    If Not (pStringsReceived.Length = 2) Then
      Tools.LogToTextFile.WriteMessage($"    Invalid string received {pStringReceived}", "GetUserDetailsForGuest")
      Return
    End If
    Try
      pUserNameForDetails = pStringsReceived(0)
      pPasswordForLogin = pStringsReceived(1)
    Catch ex As Exception
      Tools.LogToTextFile.WriteMessage($"    Invalid string received {pStringReceived} - Could not interpret. Exception: {ex.Message}", "GetUserDetailsForGuest")
      Return
    End Try

    Dim pRequester As clsRequester = Nothing

    Dim pAccessingEntity As New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=False, vRequester:=pRequester, rFault:=pFault)
    pAccessingEntity.WSReportedIP = pIP
    pAccessingEntity.WSReportedCountry = pCountry

    pFault = ccSecurity.LogInByNamePwd(pGuest, pPasswordForLogin, pRequester, vAccessingEntity:=pAccessingEntity, vGuestSystem:=pGuest)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage($"    Login failed", "GetUserDetailsForGuest")
      Response.Write($"{pFault.Number} {pFault.LoggedAlertID}")
      Return
    End If

    pRequester.CallingFunctionWithinApplication = "ccAPI:GetUserDetailsForGuest"

    'Check that the guest is allowed
    Dim pSystemDefault As New csSystemDefault()
    pFault = pSystemDefault.GetByGroupAndSettingName("AllowedGuest", pGuest, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage($"    Guest {pGuest} not found", "GetUserDetailsForGuest")
      ccSecurity.LogOut(pRequester)
      Return
    End If
    Dim pExpectedIP As String = pSystemDefault.SettingValue
    If Not pExpectedIP.Contains(pIP) Then
      pFault.LogFreeTextFault(78, $"         IP for Guest {pGuest} invalid . Expected {pExpectedIP.Replace(Environment.NewLine, ";")}, got {pIP}.", pFunctionParameters, "TRGT-250430-1359", pRequester)
      Tools.LogToTextFile.WriteMessage($"    IP for Guest {pGuest} invalid . Expected {pExpectedIP.Replace(Environment.NewLine, ";")}, got {pIP}., ", "GetUserDetailsForGuest")
      ccSecurity.LogOut(pRequester)
      Return
    End If

    'Mow get the user
    Dim pUser As New csUser()
    pFault = pUser.GetByUserName(pUserNameForDetails, pRequester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage($"    GetByUserName failed for {pUserNameForDetails}", "GetUserDetailsForGuest")
      Response.Write($"{pFault.Number} {pFault.LoggedAlertID}")
      ccSecurity.LogOut(pRequester)
      Return
    End If

    'now add the user rights
    Dim pApps As String = "#" & pUser.Applications.Replace(ChrW(13), "").Replace(ChrW(10), "#") & "#"
    Tools.LogToTextFile.WriteMessage($"User: {pUserNameForDetails}, Guest: {pGuest}, pApps: {pApps}", "GetUserDetailsForGuest")
    If pApps.IndexOf($"#{pGuest}#") <= 0 Then
      Dim pNewApps As String = pUser.Applications
      If String.IsNullOrWhiteSpace(pNewApps) OrElse pApps.EndsWith("##") Then
        pNewApps &= pGuest
      Else
        pNewApps &= Environment.NewLine & pGuest
      End If
      Tools.LogToTextFile.WriteMessage($"User: {pUserNameForDetails}, pNewApps: {pNewApps}", "GetUserDetailsForGuest")
      pUser.UpdateApplications(pNewApps, pRequester)
      If Not pFault.isOK Then
        Tools.LogToTextFile.WriteMessage($"    UpdateApplications failed", "GetUserDetailsForGuest")
        Response.Write($"{pFault.Number} {pFault.LoggedAlertID}")
        ccSecurity.LogOut(pRequester)
        Return
      End If
    End If

    Dim pResponseToReturn As New Text.StringBuilder()
    pResponseToReturn.Append($"{pUser.LastName}~")
    pResponseToReturn.Append($"{pUser.FirstName}~")
    pResponseToReturn.Append($"{pUser.NationalIDNo}~")
    pResponseToReturn.Append($"{pUser.Address}~")
    pResponseToReturn.Append($"{pUser.City}~")
    pResponseToReturn.Append($"{pUser.ProvinceState}~")
    pResponseToReturn.Append($"{pUser.PostalCode}~")
    pResponseToReturn.Append($"{pUser.Country}~")
    pResponseToReturn.Append($"{pUser.PhoneNumber}~")
    pResponseToReturn.Append($"{pUser.Email}~")
    pResponseToReturn.Append($"{pUser.Language.FastToString()}~")
    pResponseToReturn.Append($"{pUser.MessagingMode.FastToString()}~")
    pResponseToReturn.Append($"{pUser.DatePasswordChanged:yyyyMMddTHHmmss}~")
    pResponseToReturn.Append($"{pUser.ExpiryDate:yyyyMMdd}")


    Response.Write($"{-1} {pResponseToReturn}")

    ccSecurity.LogOut(pRequester)

  End Sub

End Class