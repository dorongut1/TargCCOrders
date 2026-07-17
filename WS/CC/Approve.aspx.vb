Public Class Approve
  Inherits System.Web.UI.Page

  Protected Shared prtDir As String
  Protected Shared prtAlignRight As String
  Protected Shared prtAlignLeft As String

  Private Shared _Requester As clsRequester
  Private Shared _FunctionName As String
  Private Shared _Code As String

  Private Event evtChangeTextAndURL(ByRef rLogoURL As String, ByRef rTitleText As String, ByRef rHeaderText As String, vRequester As clsRequester)

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If IsPostBack Then Return

    Dim Encoding As UnicodeEncoding = New UnicodeEncoding(True, False)
    Response.ContentEncoding = Encoding

    Dim pLoggedText As New Text.StringBuilder()

    pLoggedText.AppendLine("Start Request!!")
    pLoggedText.AppendLine("Request('RawUrl') = " & Request.RawUrl)
    pLoggedText.AppendLine("Request.UserHostAddress = " & Request.UserHostAddress)
    Tools.LogToTextFile.WriteMessage(pLoggedText.ToString(), "Approve")

    'try to get the query
    If String.IsNullOrEmpty(Request.Url.Query) OrElse Not Request.Url.Query.Substring(0, 1) = "?" Then
      Response.End()
    End If

    'Requester
    _Requester = Nothing
    Dim pFunction As String = ""
    Try
      Dim pInputBase64 As String = Request.Url.Query.Substring(1)
      Dim bytes As Byte() = Convert.FromBase64String(pInputBase64)
      Dim pInput As String = Text.Encoding.UTF8.GetString(bytes)

      If pInput.IndexOf("#") < 0 Then
        Tools.LogToTextFile.WriteMessage($"Invalid input - no # {pInput}", "Approve")
      End If

      Dim pLoggedLoginIDEnc As String = pInput.Split("#"c)(0)
      Dim pUserNameEnc As String = pInput.Split("#"c)(1)
      _Requester = New clsRequester(pLoggedLoginIDEnc, pUserNameEnc)
      pFunction = ccHelper.Decipher(ccHelper.enmEncryptionMethod.TripleDES, pInput.Split("#"c)(2))
    Catch ex As Exception
      Tools.LogToTextFile.WriteException("Bad ticket", ex, "Approve")
      Response.End()
    End Try
    'In case of previous failure
    Dim pLoggedLoginID As Long = _Requester.LoggedLoginID
    If pLoggedLoginID < 0 Then
      pLoggedLoginID = -pLoggedLoginID - 10
    End If

    'set the CallingFunctionWithinApplication
    _Requester.CallingFunctionWithinApplication = "WS_Approve"

    'Check that the logged login is valid (already checked in clsReqster - this is a double check)
    Dim pFault As clsFault
    Dim pLoggedLogin As New csLoggedLogin
    pFault = pLoggedLogin.GetByID(pLoggedLoginID, _Requester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1647: " & pFault.StringForMessageBox, "Approve")
      Response.End()
    End If
    If pLoggedLogin.UserName <> _Requester.UserName Then
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1649: User names do not match. LoggedLogin.UserName=" & pLoggedLogin.UserFullName & ", Requester.UserName=" & _Requester.UserName & "", "Approve")
      Response.End()
    End If
    If pLoggedLogin.TimeLoggedOut <> Nothing Then
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1650: User is already logged out.", "Approve")
      Response.End()
    End If

    'Now get the user
    Dim pUser As New csUser()
    pFault = pUser.GetByUserName(_Requester.UserName, _Requester, vMustExist:=True)
    If Not pFault.isOK Then
      Tools.LogToTextFile.WriteMessage("TRGT-181209-1647: " & pFault.StringForMessageBox, "Approve")
      Response.End()
    End If

    Dim pTimedOut As Boolean = False
    If String.IsNullOrEmpty(pUser.ApprovalCodeHashed) Then
      'this means it was already answered one way or the other
      pTimedOut = True
    End If

    Dim pIsLink As Boolean = False
    Dim pTask As String = ""

    If Not pTimedOut Then
      'LoggedLogin
      Dim pApprovalFunctionName As String = pUser.ApprovalFunctionName
      Dim pTestLoginID As String = pApprovalFunctionName.Split("#"c)(2)
      If pTestLoginID <> _Requester.LoggedLoginID.ToString Then
        Tools.LogToTextFile.WriteMessage($"TRGT-2005-1016: Logins don't match. In User {pTestLoginID}, In Requester {_Requester.LoggedLoginID} ", "Approve")
        Response.End()
      End If

      'Function
      Dim pTestFunction As String = pApprovalFunctionName.Split("#"c)(0)
      If pTestFunction <> pFunction Then
        Tools.LogToTextFile.WriteMessage($"TRGT-2005-1016: Functions don't match. In User {pTestFunction}, Received in call {pFunction} ", "Approve")
        Response.End()
      End If
      _FunctionName = pFunction

      'Task
      pTask = pApprovalFunctionName.Split("#"c)(1)

      If pApprovalFunctionName.Split("#"c).Length = 4 Then
        If pApprovalFunctionName.Split("#"c)(3) = "0" Then
          pIsLink = True
          _Code = "000000" 'send this s a dummy
        End If
      End If
    End If

    Dim pLogoURL As String = Nothing
    Dim pTitleText As String = Nothing
    Dim pHeaderText As String = Nothing

    'imgLogo.ImageUrl = "../TargCCOrders Logo Small.jpg"
    '$"Hi {_Requester.UserFullName}! Please approve the request for {pTask} that you just made."

    RaiseEvent evtChangeTextAndURL(pLogoURL, pTitleText, pHeaderText, _Requester)

    imgLogo.ImageUrl = If(pLogoURL = Nothing, "../LogoCompany.jpg", pLogoURL)

    'now set direction
    If _Requester.UILang = clsEnums.enmLanguage.he Then
      prtDir = "rtl"
      prtAlignLeft = "right"
      prtAlignRight = "left"
    Else
      prtDir = "ltr"
      prtAlignLeft = "left"
      prtAlignRight = "right"
    End If


    'Check if expired
    If Not pFault.isOK Then
      Response.End()
    End If

    If pIsLink Then
      txtCode.Visible = False 'so it doesn't read the textbox
      btnSend_Click(sender, e)
    ElseIf pTimedOut Then 'OrElse Not pUser.ApprovalFunctionName.Equals(_FunctionName) Then
      lblResponseFailed.Text = "Request already timed out"
      lblApproveRequest.Visible = False
      lblHeader.Visible = False
      lblCodeReceived.Visible = False
      txtCode.Visible = False
      btnSend.Visible = False
    Else
      lblApproveRequest.Text = If(pTitleText = Nothing, ccHelper.GetLocalizedSystemText("Approve Request", _Requester, _Requester.UILang), pTitleText)
      lblHeader.Text = If(pHeaderText = Nothing, String.Format(ccHelper.GetLocalizedSystemText("Hi {0}! Please approve the request for '{1}'", _Requester, _Requester.UILang), _Requester.UserFullName, pTask), pHeaderText)
      lblCodeReceived.Text = ccHelper.GetLocalizedSystemText("Code Received", _Requester, _Requester.UILang)
      btnSend.Text = ccHelper.GetLocalizedSystemText("Send Code", _Requester, _Requester.UILang)
    End If


  End Sub

  Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
    Dim pFault As clsFault
    'send the code
    If txtCode.Visible Then
      _Code = txtCode.Text.Trim
    End If

    lblResponseSucceeded.Text = ""
    lblResponseFailed.Text = ""
    If Not ccHelper.IsNumeric(_Code) Then
      Return
    End If
    If _Code.Length <> 6 Then
      Return
    End If

    btnSend.Visible = False
    lblHeader.Visible = False
    txtCode.Visible = False
    lblCodeReceived.Visible = False

    'now submit it
    pFault = ccSecurity.MarkAsApproved(_Code, _FunctionName, _Requester)
    If Not pFault.isOK Then
      If pFault.Number = 156 Then
        lblResponseFailed.Text = "The request timed out"
      Else
        lblResponseFailed.Text = pFault.ShortStringForUser().Replace(Environment.NewLine, "</br>")
      End If
      Return
    End If

    lblResponseSucceeded.Text = "The request is approved.</br>Please close this window and continue working."

  End Sub

End Class