Public Class ForgotPassword
  Inherits System.Web.UI.Page

  Protected Shared prtDir As String
  Protected Shared prtAlignRight As String
  Protected Shared prtAlignLeft As String

  Protected Shared csslocation As String

  Private Shared _FullName As String = ""
  Private Shared _UserName As String = ""
  Private Shared _UI As clsEnums.enmLanguage = clsEnums.enmLanguage.UD

  Private Shared _System As String

  Private Event evtChangeTextForUserDetails(ByRef rLogoURL As String, ByRef rTitle As String, ByRef rInstructions As String, ByRef rUserName As String, ByRef rEmail As String, ByRef rCellphone As String, ByRef rSendPassword As String)


  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If IsPostBack Then Return

    Dim pRoot As String = Server.MapPath("/")
    If IO.File.Exists(pRoot & "ws.css") Then
      'csslocation = pRoot & "ws.css"
      csslocation = "..\ws.css"
    Else
      csslocation = "ws.css"
    End If

    Dim pSystem As String = Request.QueryString("S")
    If String.IsNullOrWhiteSpace(pSystem) Then
      Session("System") = ""
    Else
      Session("System") = pSystem
    End If
    _System = pSystem


    Dim Encoding As UnicodeEncoding = New UnicodeEncoding(True, False)
    Response.ContentEncoding = Encoding

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Request('L') = " & Request.QueryString("L"))
    pLoggedText.AppendLine("Request.UserHostAddress = " & Request.UserHostAddress)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "ChangePassword")

    If Request.QueryString("L") = Nothing Then
      _UI = clsEnums.enmLanguage.en
    Else
      _UI = clsEnums.TranslateEnmLanguage(Request.QueryString("L").ToString())
      If _UI = clsEnums.enmLanguage.UD Then _UI = clsEnums.enmLanguage.en
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

    lblResponseFailed.Visible = False
    lblResponseSucceeded.Visible = False

    ShowUserDetails(True)

  End Sub

  Private Sub ShowUserDetails(vShow As Boolean)

    Dim pLogoURL As String = Nothing
    Dim pTitle As String = Nothing
    Dim pInstructions As String = Nothing
    Dim pUserName As String = Nothing
    Dim pEmail As String = Nothing
    Dim pCellphone As String = Nothing
    Dim pRequestLink As String = Nothing


    If vShow Then
      RaiseEvent evtChangeTextForUserDetails(pLogoURL, pTitle, pInstructions, pUserName, pEmail, pCellphone, pRequestLink)

      lblSystemName.Text = If(_System = Nothing, "TargCCOrders", _System)
      imgLogo.ImageUrl = If(pLogoURL = Nothing, "../LogoCompany.jpg", pLogoURL)
      lblTitle.Text = If(pTitle = Nothing, ccHelper.GetLocalizedSystemText("Forgot Password", Nothing, vLang:=_UI), pTitle)
      lblInstructions.Text = If(pInstructions = Nothing, String.Format(ccHelper.GetLocalizedSystemText("Please enter your details below.</br>We will send you a link where you can create a new password.", Nothing, _UI), _FullName), pInstructions)
      lblUserName.Text = If(pUserName = Nothing, ccHelper.GetLocalizedSystemText("UserName", Nothing, _UI), pUserName)
      lblEmail.Text = If(pEmail = Nothing, ccHelper.GetLocalizedSystemText("Email", Nothing, _UI), pEmail)
      lblCellphone.Text = If(pCellphone = Nothing, ccHelper.GetLocalizedSystemText("Cellphone (numbers only, including area code)", Nothing, _UI), pCellphone)
      btnRequestLink.Text = If(pRequestLink = Nothing, ccHelper.GetLocalizedSystemText("Request Link", Nothing, _UI), pRequestLink)
    End If

    lblTitle.Visible = vShow
    lblUserName.Visible = vShow
    lblEmail.Visible = vShow
    lblCellphone.Visible = vShow
    btnRequestLink.Visible = vShow

    txtUserName.Visible = vShow
    txtEmail.Visible = vShow
    txtCellphone.Visible = vShow

    If My.Settings.RequireCellphoneForPasswordReset = False Then
      lblCellphone.Visible = False
      txtCellphone.Visible = False
    End If

  End Sub


  Protected Sub btnRequestLink_Click(sender As Object, e As EventArgs) Handles btnRequestLink.Click
    lblResponseFailed.Visible = False
    lblResponseSucceeded.Visible = False

    'Get the details
    Dim pUserName As String = txtUserName.Text.Trim()
    Dim pEmail As String = txtEmail.Text.Trim()
    Dim pCellphone As String = txtCellphone.Text.Trim()

    If My.Settings.RequireCellphoneForPasswordReset = False Then pCellphone = "NotNeeded"

    If String.IsNullOrEmpty(pUserName) OrElse
        String.IsNullOrEmpty(pEmail) OrElse
        String.IsNullOrEmpty(pCellphone) Then
      lblResponseFailed.Text = "You must fill in all the details correctly"
      lblResponseFailed.Visible = True
      Return
    End If

    Dim pFault As clsFault
    Dim pMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD
    pFault = ccSecurity.ForgotPassword(pUserName, pEmail, pCellphone, csFunctions.LoadAccessingEntity(), pMessagingMode, vSystem:=_System)
    If Not pFault.isOK Then
      If pFault.Number = 91 Or pFault.Number = 93 Then
        lblResponseFailed.Text = "At least one of the details input is incorrect"
      Else
        lblResponseFailed.Text = pFault.ShortStringForUser().Replace(Environment.NewLine, "</br>")
      End If
      lblResponseFailed.Visible = True
      Return
    End If

    lblResponseSucceeded.Text = $"An activation link has been sent to you by {pMessagingMode.FastToString()}"
    lblInstructions.Visible = False
    lblResponseSucceeded.Visible = True

    ShowUserDetails(False)


  End Sub
End Class