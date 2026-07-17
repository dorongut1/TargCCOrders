Public Class ChangePassword
  Inherits System.Web.UI.Page

  Protected Shared prtDir As String
  Protected Shared prtAlignRight As String
  Protected Shared prtAlignLeft As String

  Protected Shared csslocation As String

  Private Shared _FullName As String = ""
  Private Shared _UserName As String = ""
  Private Shared _UI As clsEnums.enmLanguage = clsEnums.enmLanguage.UD

  Private Shared _System As String

  Private Shared _MessagingMode As clsEnums.enmMessagingMode

  Private Event evtChangeTextAndURL(ByRef rLogoURL As String, ByRef rChangePassword As String, ByRef rHi As String, ByRef rPresentPassword As String, ByRef rNewPassword As String, ByRef rRetypePassword As String, ByRef rSendChangePassword As String)

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Session("System") Is Nothing Then
      _System = ""
    Else
      _System = Session("System").ToString()
    End If

    If IsPostBack Then Return

    Dim pRoot As String = Server.MapPath("/")
    If IO.File.Exists(pRoot & "ws.css") Then
      'csslocation = pRoot & "ws.css"
      csslocation = "..\ws.css"
    Else
      csslocation = "ws.css"
    End If

    Dim Encoding As UnicodeEncoding = New UnicodeEncoding(True, False)
    Response.ContentEncoding = Encoding

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Request('D') = " & Request.QueryString("D"))
    pLoggedText.AppendLine("Request('TKT') = " & Request.QueryString("TKT"))
    pLoggedText.AppendLine("Request('S') = " & Request.QueryString("TKT"))
    pLoggedText.Append("Request.UserHostAddress = " & Request.UserHostAddress)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "ChangePassword")

    If Request.QueryString("D") = Nothing AndAlso Request.QueryString("TKT") = Nothing Then Response.End()

    lblResponseFailed.Text = ""
    lblResponseSucceeded.Text = ""


    _FullName = ""
    _UserName = ""
    _UI = clsEnums.enmLanguage.UD
    _MessagingMode = clsEnums.enmMessagingMode.UD
    Dim pFailure As String = ""

    If Request.QueryString("D") IsNot Nothing Then
      Dim pWhenRequested As String = ""
      Try
        pFailure = "Failed Request.QueryString('D')"
        Dim pB64 As String = Request.QueryString("D")
        pFailure = "Failed ccHelper.ToPlainString(pB64)"
        Dim pDEncrypted As String = ccHelper.ToPlainString(pB64)

        pFailure = "Failed ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pDEncrypted)"
        Dim pD As String = ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pDEncrypted)
        Tools.LogToTextFile.WriteMessage(pD, "ChangePassword")

        pFailure = "Failed pD.Split('#'c)"
        Dim pDs As String() = pD.Split("#"c)

        pFailure = "Failed pDs(0)"
        _FullName = pDs(0)
        pFailure = "Failed pDs(1)"
        _UserName = pDs(1)
        pFailure = "Failed pDs(2)"
        _UI = clsEnums.TranslateEnmLanguage(pDs(2))
        pFailure = "Failed pDs(3)"
        pWhenRequested = pDs(3)
        pFailure = ""
      Catch ex As Exception
        Tools.LogToTextFile.WriteException("Bad Incoming Data. " & pFailure, ex, "ChangePassword")
        Response.End()
      End Try
      If Not (pWhenRequested.Equals(DateTime.UtcNow.ToString("yyyyMMddTHHmm")) OrElse
             pWhenRequested.Equals(DateTime.UtcNow.AddMinutes(-1).ToString("yyyyMMddTHHmm"))) Then
        Tools.LogToTextFile.WriteMessage($"Request expired. WhenRequested: {pWhenRequested:yyyyMMddTHHmm}, DateTime.UtcNow: {DateTime.UtcNow:yyyyMMddTHHmm} ", "ChangePassword")
        lblResponseFailed.Text = "Request expired"

        lblResponseSucceeded.Text = ""
        lblInstructions.Visible = False
        lblTitle.Visible = False
        btnSend.Visible = False
        txtPresentPassword.Visible = False
        txtNewPassword.Visible = False
        txtRetypePassword.Visible = False
        lblPresentPassword.Visible = False
        lblNewPassword.Visible = False
        lblRetypePassword.Visible = False

        Return
      End If
    Else
      Try
        pFailure = "Failed Request.QueryString('TKT')"
        Dim pTicketBase64 As String = Request.QueryString("TKT")
        pFailure = "Failed ccHelper.ToPlainString(pTicketBase64)"
        Dim pTicketEncrypted As String = ccHelper.ToPlainString(pTicketBase64)
        pFailure = "Failed ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pTicketEncrypted)"
        Dim pTicket As String = ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pTicketEncrypted)


        Dim pSystemBase64 As String = Request.QueryString("S")
        pFailure = "Failed ccHelper.ToPlainString(pSystemBase64)"
        Dim pSystemEncrypted As String = ccHelper.ToPlainString(pSystemBase64)
        pFailure = "Failed ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pSystemEncrypted)"
        Dim pSystem As String = ccHelper.Decipher(ccHelper.enmEncryptionMethod.AES, pSystemEncrypted)

        If Not (String.IsNullOrEmpty(pSystem)) Then
          Session("System") = pSystem
        Else
          Session("System") = ""
        End If
        _System = pSystem

        pFailure = "Failed clsRequester(pTicket)"
        Dim pRequester As New clsRequester(pTicket)
        pFailure = ""

        'Now reset the password
        Dim pFault As clsFault = Nothing
        Dim pUser As New csUser(pRequester.UserID, clsEnums.enmLoadParent.DoNotLoad, pRequester, pFault, vMustExist:=True)
        If Not pFault.isOK Then
          Tools.LogToTextFile.WriteMessage($"Failed getting user.{Environment.NewLine}    Requester: {pRequester.ToStringFriend()}{Environment.NewLine}    Fault: {pFault.StringForMessageBox}", "ChangePassword")
          lblResponseFailed.Text = "Invalid Ticket"

          lblResponseSucceeded.Text = ""
          lblInstructions.Visible = False
          lblTitle.Visible = False
          btnSend.Visible = False
          txtPresentPassword.Visible = False
          txtNewPassword.Visible = False
          txtRetypePassword.Visible = False
          lblPresentPassword.Visible = False
          lblNewPassword.Visible = False
          lblRetypePassword.Visible = False
          Return
        End If
        pFault = pUser.ChangePassword($"{pUser.ID}AutoCreate", pRequester)
        If Not pFault.isOK Then
          Tools.LogToTextFile.WriteMessage($"Failed creating random password: {pFault.StringForMessageBox}", "ChangePassword")
          Response.End()
        End If

        _FullName = $"{pUser.FirstName} {pUser.LastName}"
        _UserName = pUser.UserName
        _UI = pUser.Language
        _MessagingMode = pUser.MessagingMode

        pFault = ccSecurity.LogOut(pRequester)
      Catch ex As Exception
        Tools.LogToTextFile.WriteException("Bad Incoming Data. " & pFailure, ex, "ChangePassword")
        Response.End()
      End Try
    End If


    If _UI = clsEnums.enmLanguage.he Then
      prtDir = "rtl"
      prtAlignLeft = "right"
      prtAlignRight = "left"
    Else
      prtDir = "ltr"
      prtAlignLeft = "left"
      prtAlignRight = "right"
    End If

    Dim pLogoURL As String = Nothing
    Dim pChangePassword As String = Nothing
    Dim pHi As String = Nothing
    Dim pPresentPassword As String = Nothing
    Dim pNewPassword As String = Nothing
    Dim pRetypePassword As String = Nothing
    Dim pSendChangePassword As String = Nothing

    'imgLogo.ImageUrl = "../TargCCOrders Logo Small.jpg"
    '$"Hi {_Requester.UserFullName}! Please ChangePassword the request for {pTask} that you just made."

    RaiseEvent evtChangeTextAndURL(pLogoURL, pChangePassword, pHi, pPresentPassword, pNewPassword, pRetypePassword, pSendChangePassword)

    lblSystemName.Text = If(_System = Nothing, "TargCCOrders", _System)
    imgLogo.ImageUrl = If(pLogoURL = Nothing, "../LogoCompany.jpg", pLogoURL)
    lblTitle.Text = If(pChangePassword = Nothing, ccHelper.GetLocalizedSystemText("Change Password", Nothing, vLang:=clsEnums.enmLanguage.en), pChangePassword)
    If _MessagingMode = clsEnums.enmMessagingMode.UD Then
      lblInstructions.Text = If(pHi = Nothing, String.Format(ccHelper.GetLocalizedSystemText("Hi {0}!<br/>Change your password below", Nothing, _UI), _FullName), pHi)
    Else
      lblInstructions.Text = If(pHi = Nothing, String.Format(ccHelper.GetLocalizedSystemText($"Hi {_FullName}!<br/><br/>We just sent you a temporary password by {_MessagingMode.FastToString()}.<br/>For your security, please change it below", Nothing, _UI)), pHi)
    End If
    lblPresentPassword.Text = If(pPresentPassword = Nothing, ccHelper.GetLocalizedSystemText("Present Password", Nothing, _UI), pPresentPassword)
    lblNewPassword.Text = If(pNewPassword = Nothing, ccHelper.GetLocalizedSystemText("New Password", Nothing, _UI), pNewPassword)
    lblRetypePassword.Text = If(pRetypePassword = Nothing, ccHelper.GetLocalizedSystemText("Retype Password", Nothing, _UI), pRetypePassword)
    btnSend.Text = If(pSendChangePassword = Nothing, ccHelper.GetLocalizedSystemText("Change Password", Nothing, _UI), pSendChangePassword)

  End Sub

  Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
    Dim pFault As New clsFault

    'send the code
    Dim pPresentPassword As String = txtPresentPassword.Text.Trim()
    Dim pNewPassword As String = txtNewPassword.Text.Trim()
    Dim pRetypePassword As String = txtRetypePassword.Text.Trim()

    lblInstructions.Text = ""
    lblResponseSucceeded.Text = ""
    lblResponseFailed.Text = ""

    If pNewPassword <> pRetypePassword Then
      lblResponseFailed.Text = "The passwords don't match!!"
      Return
    End If

    Dim pRequester As clsRequester = Nothing
    pFault = ccSecurity.LogInByNamePwd(_UserName, pPresentPassword, pRequester, vAccessingEntity:=csFunctions.LoadAccessingEntity(), vNewPassword:=pNewPassword)
    If Not pFault.isOK Then
      lblResponseFailed.Text = pFault.ShortStringForUser().Replace(Environment.NewLine, "</br>")
      Return
    End If

    lblResponseSucceeded.Text = "Your password was successfully changed"

    lblTitle.Visible = False
    btnSend.Visible = False
    txtPresentPassword.Visible = False
    txtNewPassword.Visible = False
    txtRetypePassword.Visible = False
    lblPresentPassword.Visible = False
    lblNewPassword.Visible = False
    lblRetypePassword.Visible = False


  End Sub

End Class