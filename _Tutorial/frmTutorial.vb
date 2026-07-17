Public Class frmTutorial

  Private _Requester As clsRequester
  Private _Ticket As String

  Private Sub frmTutorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    txtResults.Text = ""

    txtUserName.Text = My.Settings.UserName

    txtPassword.Text = ""

    SetButtonsForController()

  End Sub

  Private Sub SetButtonsForController()

    Dim ctrl As Control = Me.GetNextControl(Me, True)
    Do Until ctrl Is Nothing
      If ctrl.Name.StartsWith("btn") Then
        If ctrl.Name.IndexOf("login", StringComparison.OrdinalIgnoreCase) < 0 Then
          If ctrl.Name.StartsWith("btnws", StringComparison.OrdinalIgnoreCase) Then
            ctrl.Enabled = Not (_Ticket = "")
          ElseIf ctrl.Name.StartsWith("btn", StringComparison.OrdinalIgnoreCase) Then
            ctrl.Enabled = Not (_Requester Is Nothing)
          End If
        End If
      End If
      ctrl = Me.GetNextControl(ctrl, True)
    Loop
  End Sub


  Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLoginNamePwd.Click, btnWSLogin.Click, btnLoginNetwork.Click
    'make sure you add ExchangeNet.Tutorial user's allowed applications

    Cursor = Cursors.WaitCursor

    Dim pS As New Stopwatch()
    pS.Start()

    txtResults.Text = ""
    Application.DoEvents()

    Dim pUserName As String = txtUserName.Text
    Dim pPassword As String = txtPassword.Text

    My.Settings.UserName = pUserName
    My.Settings.Save()

    Dim pFault As clsFault
    _Requester = Nothing
    _Ticket = ""

    If CType(sender, Button) Is btnLoginNamePwd Then
      pFault = ccSecurity.LogInByNamePwd(pUserName, pPassword, _Requester, vOverrideUILang:=clsEnums.enmLanguage.he)
      'Always check for a fault. Pur it after the function for easier readability
      If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return

      pS.Stop()
      txtResults.Text = "Logged In! " & Environment.NewLine &
                      _Requester.UserFullName & Environment.NewLine &
                      $"LoginID: {_Requester.LoggedLoginID}" & Environment.NewLine &
                      $"Language: {_Requester.UILang.FastToString()}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    ElseIf CType(sender, Button) Is btnLoginNetwork Then
      pFault = ccSecurity.LogInByNetworkCredentials(_Requester, clsEnums.enmLanguage.en)
      'Always check for a fault. Pur it after the function for easier readability
      If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return

      pS.Stop()
      txtResults.Text = "Logged In! " & Environment.NewLine &
                      _Requester.UserFullName & Environment.NewLine &
                      $"LoginID: {_Requester.LoggedLoginID}" & Environment.NewLine &
                      $"Language: {_Requester.UILang.FastToString()}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Else 'If CType(sender, Button) Is btnLogin Then
      Dim pSFault As SR.Fault
      Dim pSr As New SR.TutorialSoapClient()
      'pWr.EnableDecompression = True
      'pWr.PreAuthenticate = True
      'pWr.Credentials = vRequester.Credential

      pSFault = pSr.CreateTicket(pUserName, pPassword, _Ticket)
      If pSFault.ErrorTypeNumber <> -1 Then
        Cursor = Cursors.Default
        MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
      End If

      pS.Stop()
      txtResults.Text = "Logged In! " & Environment.NewLine &
                      $"Ticket: {_Ticket}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"
    End If

    Cursor = Cursors.Default

    SetButtonsForController()

  End Sub

  Private Sub btnIntrinsicGetUserFromLastLoggedAlert_Click(sender As Object, e As EventArgs) Handles btnIntrinsicGetUserFromLastLoggedAlert.Click
    _Requester.CallingFunctionWithinApplication = "btnIntrinsicGetUserFromLastLoggedAlert_Click"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pLoggedAlerts As New csLoggedAlertCol()
    Dim pFault As clsFault = pLoggedAlerts.Fill(_Requester, 1, clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return

    If pLoggedAlerts.Count = 0 Then
      If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox("There are no logged alerts", MsgBoxStyle.Exclamation) : Return
    End If

    Dim pLastLoggedAlert As csLoggedAlert = pLoggedAlerts(0)

    Dim pUser As New csUser(pLastLoggedAlert.AffectedUserID, False, _Requester, pFault, vMustExist:=True) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()
    txtResults.Text = $"Got user from LoggedAlertID: {pLastLoggedAlert.ID}" & Environment.NewLine &
                      $"User: {pUser.ToString().Replace("‡", Environment.NewLine & "    ")}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default

  End Sub

  Private Sub btnWSIntrinsicGetUserFromLastLoggedAlert_Click(sender As Object, e As EventArgs) Handles btnWSIntrinsicGetUserFromLastLoggedAlert.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    MsgBox("Intrinsic functions are not exposed via the Web Service. If you need this data, create a specific function and expose it to the web service.")


    Cursor = Cursors.Default

  End Sub

  Private Sub btnCTutorialGetAlertListsFromFunction_Click(sender As Object, e As EventArgs) Handles btnCTutorialGetAlertListsFromFunction.Click
    _Requester.CallingFunctionWithinApplication = "btnCTutorialGetAlertListsFromFunction"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pError41List As csLoggedAlertCol = Nothing
    Dim pError50List As csLoggedAlertCol = Nothing
    Dim pMonthOut As Date = Nothing
    Dim pNum41 As Integer = 0
    Dim pNum50 As Integer = 0

    'Dim pFault As clsFault = TutorialController.GetAlertListsFromFunction(DateTime.Now, _Requester, pError41List, pError50List, pMonthOut, pNum41, pNum50)
    Dim pFault As clsFault = TutorialController.GetAlertListsFromFunction(New Date(2020, 1, 15), _Requester, pError41List, pError50List, pMonthOut, pNum41, pNum50) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()
    txtResults.Text = $"Month: {pMonthOut:dd-MMM-yyyy}" & Environment.NewLine &
                      $"Num41: {pNum41}" & Environment.NewLine &
                      $"  {pError41List.ToString()}" & Environment.NewLine &
                      $"Num50: {pNum50}" & Environment.NewLine &
                      $"  {pError50List.ToString()}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default

  End Sub
  Private Sub btnWSTutorialGetAlertListsFromFunction_Click(sender As Object, e As EventArgs) Handles btnWSTutorialGetAlertListsFromFunction.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()
    Dim pS As New Stopwatch()
    pS.Start()

    Dim pError41List As SR.csLoggedAlert() = Nothing
    Dim pError50List As SR.csLoggedAlert() = Nothing
    Dim pMonthOut As Date = Nothing
    Dim pNum41 As Integer = 0
    Dim pNum50 As Integer = 0

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.Tutorial_GetAlertListsFromFunction(New Date(2020, 1, 15), _Ticket, pError41List, pError50List, pMonthOut, pNum41, pNum50)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()
    Dim pMessage As New Text.StringBuilder()
    pMessage.AppendLine($"Month: {pMonthOut:dd-MMM-yyyy}")
    pMessage.AppendLine($"Num41: {pNum41}")
    For Each l In pError41List
      pMessage.AppendLine($"  FaultNumber: {l.FaultNumber}")
      pMessage.AppendLine($"  ID: {l.ID}")
    Next
    pMessage.AppendLine($"Num50: {pNum50}")
    For Each l In pError50List
      pMessage.AppendLine($"  FaultNumber: {l.FaultNumber}")
      pMessage.AppendLine($"  ID: {l.ID}")
    Next
    pMessage.AppendLine($"Time: {pS.Elapsed.TotalMilliseconds}")

    txtResults.Text = pMessage.ToString


    'txtResults.Text = $"Month: {pMonthOut:dd-MMM-yyyy}" & Environment.NewLine &
    '                  $"Num41: {pNum41}" & Environment.NewLine &
    '                  $"  {pError41List}" & Environment.NewLine &
    '                  $"Num50: {pNum50}" & Environment.NewLine &
    '                  $"  {pError50List}" & Environment.NewLine &
    '                  $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default

  End Sub

  Private Sub btnTutorialCreateReportPayment_Click(sender As Object, e As EventArgs) Handles btnTutorialCreateReportPayment.Click
    _Requester.CallingFunctionWithinApplication = "btnTutorialCreateReportPayment"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pFault As clsFault = TutorialController.CreateReportPayment(1000, New Date(2020, 9, 15), _Requester) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()
    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnWSTutorialCreateReportPaymentClick(sender As Object, e As EventArgs) Handles btnWSTutorialCreateReportPayment.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.Tutorial_CreateReportPayment(1000, New Date(2020, 9, 15), _Ticket)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()
    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnTutorialCreateReportPaymentUsingCcHelper_Click(sender As Object, e As EventArgs) Handles btnTutorialCreateReportPaymentUsingCcHelper.Click
    _Requester.CallingFunctionWithinApplication = "btnTutorialCreateReportPaymentUsingCcHelper"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pResponse As String = ""
    Dim pFault As clsFault = TutorialController.CreateReportPaymentUsingCcHelper(1000, New Date(2020, 9, 15), _Requester, pResponse) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()
    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Response: {pResponse}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnWSTutorialCreateReportPaymentUsingCcHelper_Click(sender As Object, e As EventArgs) Handles btnWSTutorialCreateReportPaymentUsingCcHelper.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pResponse As String = ""

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.Tutorial_CreateReportPaymentUsingCcHelper(1000, New Date(2020, 9, 15), _Ticket, pResponse)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()
    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Response: {pResponse}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnTutorialGetYesterdaysLoggedinUsers_Click(sender As Object, e As EventArgs) Handles btnTutorialGetYesterdaysLoggedinUsers.Click
    _Requester.CallingFunctionWithinApplication = "btnTutorialGetYesterdaysLoggedinUsers"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pYesterdaysDate As Date = Nothing
    Dim pLoggedInUsers As clsComboList = Nothing

    Dim pFault As clsFault = TutorialController.GetYesterdaysLoggedinUsers(_Requester, pYesterdaysDate, pLoggedInUsers) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()

    txtResults.Text = $"Done" & Environment.NewLine &
                      $"YesterdaysDate: {pYesterdaysDate:dd-MMM-yyyy}" & Environment.NewLine &
                      $"LoggedInUsers: {pLoggedInUsers}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub
  Private Sub btnWSTutorialGetYesterdaysLoggedinUsers_Click(sender As Object, e As EventArgs) Handles btnWSTutorialGetYesterdaysLoggedinUsers.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pYesterdaysDate As Date = Nothing
    Dim pLoggedInUsers As SR.clsLongAndText() = Nothing

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.Tutorial_GetYesterdaysLoggedinUsers(_Ticket, pYesterdaysDate, pLoggedInUsers)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()

    Dim pMessage As New Text.StringBuilder()
    pMessage.AppendLine($"Done")
    pMessage.AppendLine($"YesterdaysDate: {pYesterdaysDate:dd-MMM-yyyy}")
    pMessage.AppendLine($"LoggedInUsers: {pLoggedInUsers.Count}")
    For Each l In pLoggedInUsers
      pMessage.AppendLine($"  KeyLong: {l.Long}, Text: {l.Text} ")
    Next
    pMessage.AppendLine($"Time: {pS.Elapsed.TotalMilliseconds}")

    txtResults.Text = pMessage.ToString

    Cursor = Cursors.Default
  End Sub

  Private Sub btnTutorialCreateDummyMail_Click(sender As Object, e As EventArgs) Handles btnTutorialCreateDummyMail.Click
    _Requester.CallingFunctionWithinApplication = "btnTutorialCreateDummyMail"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pMail As csMail = Nothing

    Dim pFault As clsFault = TutorialController.CreateDummyMail(_Requester, pMail) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return


    pS.Stop()

    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Mail: {pMail}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub
  Private Sub btnWSTutorialCreateDummyMail_Click(sender As Object, e As EventArgs) Handles btnWSTutorialCreateDummyMail.Click

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pMail As SR.csMail = Nothing

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.Tutorial_CreateDummyMail(_Ticket, pMail)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()

    Dim pMessage As New Text.StringBuilder()
    pMessage.AppendLine($"Done")
    pMessage.AppendLine($"Mail: {pMail.Subject} ")
    pMessage.AppendLine($"    Body: {pMail.Body} ")
    pMessage.AppendLine($"    ID: {pMail.ID} ")
    pMessage.AppendLine($"    MailType: {pMail.MessagingMode.ToString()} ")
    pMessage.AppendLine($"    RecipientEmail: {pMail.RecipientEmail} ")
    pMessage.AppendLine($"    WhenSent: {pMail.WhenSent:dd-MMM-yyyy HH:mm:ss} ")
    pMessage.AppendLine($"Time: {pS.Elapsed.TotalMilliseconds}")

    txtResults.Text = pMessage.ToString

    Cursor = Cursors.Default
  End Sub

  Private Sub btnTutorialcsLoggedAlertCol_FillByBoundedFaultNumber_Click(sender As Object, e As EventArgs) Handles btnTutorialcsLoggedAlertCol_FillByBoundedFaultNumber.Click
    _Requester.CallingFunctionWithinApplication = "btnTutorialcsLoggedAlertCol_FillByBoundedFaultNumber"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pLoggedAlerts As csLoggedAlertCol = Nothing
    Dim pCount As Integer = 0

    Dim pFault As clsFault = TutorialController.csLoggedAlertCol_FillByBoundedFaultNumber(41, 50, _Requester, pLoggedAlerts, pCount) : If Not pFault.isOK Then Cursor = Cursors.Default : MsgBox(pFault.StringForMessageBox, MsgBoxStyle.Exclamation) : Return

    pS.Stop()

    txtResults.Text = $"Done" & Environment.NewLine &
                      $"NumAlerts: {pCount}" & Environment.NewLine &
                      $"Alerts: {pLoggedAlerts}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnIntrinsicIntrinsicGetEntityInNonRequesterLanguage_Click(sender As Object, e As EventArgs) Handles btnIntrinsicIntrinsicGetEntityInNonRequesterLanguage.Click
    _Requester.CallingFunctionWithinApplication = "btnIntrinsicIntrinsicGetEntityInNonRequesterLanguage"

    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    'Get an alert
    Dim pAlerts As New csAlertMessageCol(vIsLocalized:=True)
    pAlerts.OverrideDefaultLanguage(clsEnums.enmLanguage.he)
    Dim pFault As clsFault = pAlerts.Fill(_Requester)
    Dim pAlert As csAlertMessage = pAlerts.FindByID(41)

    'Get a lookup
    Dim pstrg As String = ccHelper.GetLocalizedLookup(clsEnums.enmLookup.UD, "", clsEnums.enmLookup.Generic, -3, _Requester, vLang:=clsEnums.enmLanguage.he)

    'Get an enum
    Dim pstrgEnum As String = ccHelper.GetLocalizedEnum(clsEnums.enmEnum.JobType, "OneOff", _Requester, vLang:=clsEnums.enmLanguage.he)

    pS.Stop()

    txtResults.Text = $"Done" & Environment.NewLine &
                      $"Description: {pAlert.Description}" & Environment.NewLine &
                      $"Message: {pAlert.Message}" & Environment.NewLine &
                      $"MessageLocalized: {pAlert.MessageLocalized}" & Environment.NewLine &
                      $"Action: {pAlert.Action}" & Environment.NewLine &
                      $"ActionLocalized: {pAlert.ActionLocalized}" & Environment.NewLine &
                      $"Choose: {pstrg}" & Environment.NewLine &
                      $"OneOff: {pstrgEnum}" & Environment.NewLine &
                      $"Time: {pS.Elapsed.TotalMilliseconds}"

    Cursor = Cursors.Default
  End Sub

  Private Sub btnWSTutorialcsLoggedAlertCol_FillByBoundedFaultNumber_Click(sender As Object, e As EventArgs) Handles btnWSTutorialcsLoggedAlertCol_FillByBoundedFaultNumber.Click
    Cursor = Cursors.WaitCursor

    txtResults.Text = ""
    Application.DoEvents()

    Dim pS As New Stopwatch()
    pS.Start()

    Dim pLoggedAlerts As SR.csLoggedAlert() = Nothing
    Dim pCount As Integer = 0

    Dim pSFault As SR.Fault
    Dim pSr As New SR.TutorialSoapClient()
    pSFault = pSr.csLoggedAlertCol_FillByBoundedFaultNumber(41, 50, _Ticket, pLoggedAlerts, pCount)
    If pSFault.ErrorTypeNumber <> -1 Then
      Cursor = Cursors.Default
      MsgBox($"ErrorType: {pSFault.ErrorTypeNumber}{Environment.NewLine}Description: {pSFault.ErrorDescription}{Environment.NewLine}ActionToTake: {pSFault.ErrorActionToTake}{Environment.NewLine}ErrorNumber: {pSFault.LoggedErrorNumber}", MsgBoxStyle.Exclamation) : Return
    End If

    pS.Stop()

    Dim pMessage As New Text.StringBuilder()
    pMessage.AppendLine($"Done")
    pMessage.AppendLine($"Count: {pLoggedAlerts.Count} ")
    For Each l In pLoggedAlerts
      pMessage.AppendLine($"    ID: {l.ID}, FaultNumberFaultNumber: {l.FaultNumber}, When: {l.TimeOccurred} ")
    Next
    pMessage.AppendLine($"Time: {pS.Elapsed.TotalMilliseconds}")

    txtResults.Text = pMessage.ToString

    Cursor = Cursors.Default
  End Sub

End Class
