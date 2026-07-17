Imports Microsoft.VisualBasic.ApplicationServices

Public Class ctlPnlcsPreferences

  Private _Requester As clsRequester

  Private _UILanguages As clsComboList
  Private _LTLanguages As clsComboList
  Private _MessagingModes As clsComboList

  Private _Lookups As csLookupCol

  Private _User As csUser

  Public Event evtBeforeLoad()
  Public Event evtLoaded()

  Private Sub ctl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub

    Me.Visible = False
  End Sub

  Public Function LoadControl(ByVal vRequester As clsRequester) As clsFault
    Dim pFault As New clsFault

    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()
    Me.Visible = True
    Application.DoEvents()

    _Requester = vRequester

    RaiseEvent evtBeforeLoad()

    tbc.DrawMode = TabDrawMode.OwnerDrawFixed

    _User = New csUser(_Requester.UserID, clsEnums.enmLoadParent.DoNotLoad, _Requester, pFault, True) : If Not pFault.isOK Then Return pFault

    _UILanguages = New clsComboList
    pFault = _UILanguages.FillEnums(clsEnums.enmEnum.Language, _Requester)
    If pFault.isOK = False Then Return pFault
    _UILanguages.Remove(_UILanguages.FindByKey(clsEnums.enmLanguage.UD))
    _UILanguages.SortByText()
    _LTLanguages = _UILanguages.Clone
    _LTLanguages.AddToTop(clsEnums.enmLanguage.UD, "Default")

    cboUILang.ValueMember = "KeyEnum"
    cboUILang.DisplayMember = "Text"
    cboUILang.DataSource = _UILanguages

    cboLTLang.ValueMember = "KeyEnum"
    cboLTLang.DisplayMember = "Text"
    cboLTLang.DataSource = _LTLanguages

    'MessagingModes
    _MessagingModes = New clsComboList
    pFault = _MessagingModes.FillEnums(clsEnums.enmEnum.MessagingMode, _Requester)
    If pFault.isOK = False Then Return pFault
    _MessagingModes.Remove(_MessagingModes.FindByKey(clsEnums.enmLanguage.UD))
    _MessagingModes.SortByText()

    cboMessagingMode.ValueMember = "KeyEnum"
    cboMessagingMode.DisplayMember = "Text"
    cboMessagingMode.DataSource = _MessagingModes


    'Now securityquestion
    pFault = LoadCboSecurityQuestion1() : If Not pFault.isOK Then Return pFault
    pFault = LoadCboSecurityQuestion2() : If Not pFault.isOK Then Return pFault
    pFault = LoadCboSecurityQuestion3() : If Not pFault.isOK Then Return pFault

    _Lookups = New csLookupCol(vIsLocalized:=True)
    pFault = _Lookups.FillByLookupType(clsEnums.enmLookup.SecurityQuestion, _Requester)
    If pFault.isOK = False Then Return pFault

    'Load the user
    pFault = LoadUser()
    If pFault.isOK = False Then Return pFault

    'Load the Database

    cboFontSize.SelectedItem = CStr(MyFont.Size)

    If Not String.IsNullOrEmpty(My.Settings.LoginKey) Then
      btnCreateBiometricKey.Text = "Remove all 'Biometric' Keys"
    End If
    If Not String.IsNullOrEmpty(_Requester.UserPIN) Then
      btnPIN.Text = "Delete PIN"
    End If

    btnViewPIN.Visible = (btnPIN.Text = "Delete PIN")

    tlp.Visible = True
    Application.DoEvents()

    RaiseEvent evtLoaded()

    pFault.SetOK()
    Return pFault
  End Function

  Private Function LoadCboSecurityQuestion1() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion1.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing
    Dim pPrompt As String = ccHelper.GetChoose(_Requester)
    If pTestCol Is Nothing Then
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText()
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion1.Tag = ""
    pFault = LoadCbo(cboSecurityQuestion1, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion1Code <> "" Then cboSecurityQuestion1.SelectedValue = _User.SecurityQuestion1Code

    Return pFault.SetOK()
  End Function
  Private Function LoadCboSecurityQuestion2() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion2.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing
    Dim pPrompt As String = ccHelper.GetChoose(_Requester)
    If pTestCol Is Nothing Then
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText()
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion2.Tag = ""
    pFault = LoadCbo(cboSecurityQuestion2, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion2Code <> "" Then cboSecurityQuestion2.SelectedValue = _User.SecurityQuestion2Code

    Return pFault.SetOK()
  End Function
  Private Function LoadCboSecurityQuestion3() As clsFault
    Dim pFault As clsFault

    Dim pComboList As clsComboList

    'If cboSecurityQuestion3.Items.Count > 0 Then pFault = New clsFault() : Return pFault.SetOK() 'Already loaded 

    'enable using an external list if needed 
    Dim pTestCol As clsComboList = Nothing
    Dim pPrompt As String = ccHelper.GetChoose(_Requester)
    If pTestCol Is Nothing Then
      pComboList = New clsComboList()
      pFault = pComboList.FillLookup(clsEnums.enmLookup.SecurityQuestion, _Requester)
      If pFault.isOK = False Then Return pFault
      pComboList.SortByText()
    Else
      pComboList = pTestCol
    End If

    cboSecurityQuestion3.Tag = ""
    pFault = LoadCbo(cboSecurityQuestion3, pComboList, _Requester)
    If pFault.isOK = False Then Return pFault

    If _User.SecurityQuestion3Code <> "" Then cboSecurityQuestion3.SelectedValue = _User.SecurityQuestion1Code

    Return pFault.SetOK()
  End Function

  Private Sub cboSecurityQuestion1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion1.SelectedIndexChanged
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pCode As String = CType(cboSecurityQuestion1.SelectedValue, String)
    txtSecurityQuestion1.Text = ""
  End Sub
  Private Sub cboSecurityQuestion2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion2.SelectedIndexChanged
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pCode As String = CType(cboSecurityQuestion2.SelectedValue, String)
    txtSecurityQuestion2.Text = ""
  End Sub
  Private Sub cboSecurityQuestion3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecurityQuestion3.SelectedIndexChanged
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pCode As String = CType(cboSecurityQuestion3.SelectedValue, String)
    txtSecurityQuestion3.Text = ""
  End Sub

  'UI
  Private Sub tbc_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbc.DrawItem
    'http://www.daniweb.com/software-development/vbnet/threads/353766/tabcontrol#
    'Firstly we'll define some parameters.
    Dim CurrentTab As TabPage = tbc.TabPages(e.Index)
    Dim ItemRect As Rectangle = tbc.GetTabRect(e.Index)
    'Dim FillBrush As New SolidBrush(Color.Red)
    'Dim TextBrush As New SolidBrush(Color.White)
    Dim FillBrush As New SolidBrush(Me.BackColor)
    Dim TextBrush As New SolidBrush(Color.Black)
    Dim sf As New StringFormat
    sf.Alignment = StringAlignment.Center
    sf.LineAlignment = StringAlignment.Center

    'If we are currently painting the Selected TabItem we'll 
    'change the brush colours and inflate the rectangle.
    If CBool(e.State And DrawItemState.Selected) Then
      'FillBrush.Color = Color.White
      'TextBrush.Color = Color.Red
      FillBrush.Color = CurrentTab.BackColor
      TextBrush.Color = Color.Black
      If e.Index = 0 Then
        ItemRect.Offset(2, 0)
        ItemRect.Inflate(0, 2)
      Else
        ItemRect.Inflate(2, 2)
      End If
    Else

    End If

    'Set up rotation for left and right aligned tabs
    If tbc.Alignment = TabAlignment.Left Or tbc.Alignment = TabAlignment.Right Then
      Dim RotateAngle As Single = 90
      If tbc.Alignment = TabAlignment.Left Then RotateAngle = 270
      Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
      e.Graphics.TranslateTransform(cp.X, cp.Y)
      e.Graphics.RotateTransform(RotateAngle)
      ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
    End If

    'Next we'll paint the TabItem with our Fill Brush
    e.Graphics.FillRectangle(FillBrush, ItemRect)

    'Now draw the text.
    e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

    'Reset any Graphics rotation
    e.Graphics.ResetTransform()

    'Finally, we should Dispose of our brushes.
    FillBrush.Dispose()
    TextBrush.Dispose()

    Dim rt As Rectangle = tbc.GetTabRect(tbc.TabPages.Count - 1)
    Dim rf As RectangleF = New RectangleF(rt.X + rt.Width + 4, rt.Y - 5, tbc.Width - (rt.X + rt.Width) - 4, rt.Height + 5)
    Dim b As Brush = New SolidBrush(Me.BackColor)
    e.Graphics.FillRectangle(b, rf)
    b.Dispose()

  End Sub

  'User
  Private Function LoadUser() As clsFault
    Dim pFault As clsFault = Nothing
    _User = New csUser(_Requester.UserID, clsEnums.enmLoadParent.DoNotLoad, _Requester, pFault, True) : If Not pFault.isOK Then Return pFault

    txtUserName.Text = _User.UserName
    txtLastName.Text = _User.LastName
    txtFirstName.Text = _User.FirstName
    txtType.Text = _User.Type.FastToString()
    txtEmail.Text = _User.Email
    txtPhoneNo.Text = _User.PhoneNumber

    Dim pRole As New csRole(_User.RoleID, clsEnums.enmLoadParent.DoNotLoad, _Requester, pFault, True)
    If pFault.isOK = False Then Return pFault
    txtRoles.Text = pRole.Name

    'Login
    Dim pLastLogins As New csLoggedLoginCol
    pFault = pLastLogins.FillByUserNameAndApplicationName(_User.UserName, My.Application.Info.AssemblyName, _Requester, 2, clsEnums.enmFillDirection.DESC)
    If pFault.isOK = False Then Return pFault

    If pLastLogins.Count < 2 Then
      txtLastLoginThisApp.Text = "Never"
    Else
      txtLastLoginThisApp.Text = pLastLogins(1).TimeLoggedIn.ToString("dd-MMM-yyyy HH:mm")
    End If

    'Now get the last 50 logins
    pLastLogins = New csLoggedLoginCol
    pFault = pLastLogins.FillByUserName(_User.UserName, _Requester, 50, clsEnums.enmFillDirection.DESC)
    If pFault.isOK = False Then Return pFault

    'now create a grid to show
    Dim pDataTable As New DataTableEnhanced
    pDataTable.TableName = "User Logins"

    Dim pColumn As New DataColumEnhanced
    Dim pRow As DataRow

    pColumn = New DataColumEnhanced
    pDataTable.Columns.Add(pColumn)

    pColumn = New DataColumEnhanced
    With pColumn
      .Caption = "App"
      .SetAlignment(DataColumEnhanced.enmAlignment.Left)
    End With
    pDataTable.Columns.Add(pColumn)

    pColumn = New DataColumEnhanced
    With pColumn
      .Caption = "Time"
      .SetAlignment(DataColumEnhanced.enmAlignment.Right)
    End With
    pDataTable.Columns.Add(pColumn)

    For Each p In pLastLogins
      pRow = pDataTable.NewRow
      pRow(0) = p.ID
      pRow(1) = p.ApplicationName
      pRow(2) = p.TimeLoggedIn.ToString("dd-MMM-yyyy HH:mm")
      pDataTable.Rows.Add(pRow)
    Next

    Dim pResponse As String = pDataTable.SetUpTable()
    If pResponse <> "OK" Then
      frmMessageOrInputBox.ShowMsg(pResponse, frmMessageOrInputBox.enmIconType.Warning)
    End If

    dgvLastLogins.DataSource = pDataTable

    For Each pCol As DataColumEnhanced In pDataTable.Columns
      Dim pAlignment As DataGridViewContentAlignment
      If pCol.Alignment = DataColumEnhanced.enmAlignment.Left Then
        pAlignment = DataGridViewContentAlignment.MiddleLeft
      ElseIf pCol.Alignment = DataColumEnhanced.enmAlignment.Right Then
        pAlignment = DataGridViewContentAlignment.MiddleRight
      ElseIf pCol.Alignment = DataColumEnhanced.enmAlignment.Center Then
        pAlignment = DataGridViewContentAlignment.MiddleCenter
      ElseIf pCol.Alignment = DataColumEnhanced.enmAlignment.UD Then
        pAlignment = DataGridViewContentAlignment.NotSet
      End If
      With dgvLastLogins.Columns(pCol.ColumnName)
        If pCol.IsKey Then
          .Visible = False
        Else
          .HeaderText = pCol.Caption
          .DefaultCellStyle.Alignment = pAlignment
          .HeaderCell.Style.Alignment = pAlignment
          .SortMode = DataGridViewColumnSortMode.Automatic
          .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
      End With
    Next

    dgvLastLogins.Refresh()

    'UILang
    txtUILang.Size = cboUILang.Size
    txtUILang.Location = cboUILang.Location
    txtUILang.Text = _UILanguages.FindByKey(_Requester.UILang).Text
    btnUILangCancel.Visible = False

    'LTLang
    txtLTLang.Size = cboLTLang.Size
    txtLTLang.Location = cboLTLang.Location
    txtLTLang.Text = _LTLanguages.FindByKey(clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage)).Text
    btnLTLangCancel.Visible = False

    'MessagingModes
    txtMessagingMode.Size = cboMessagingMode.Size
    txtMessagingMode.Location = cboMessagingMode.Location
    txtMessagingMode.Text = _MessagingModes.FindByKey(_User.MessagingMode).Text
    btnMessagingModeCancel.Visible = False

    If _User.SecurityQuestion1Code <> "" Then cboSecurityQuestion1.SelectedValue = _User.SecurityQuestion1Code
    txtSecurityQuestion1.Text = _User.SecurityQuestion1Response(vDecrypt:=True)
    If _User.SecurityQuestion2Code <> "" Then cboSecurityQuestion2.SelectedValue = _User.SecurityQuestion2Code
    txtSecurityQuestion2.Text = _User.SecurityQuestion2Response(vDecrypt:=True)
    If _User.SecurityQuestion3Code <> "" Then cboSecurityQuestion3.SelectedValue = _User.SecurityQuestion3Code
    txtSecurityQuestion3.Text = _User.SecurityQuestion3Response(vDecrypt:=True)

    cboSecurityQuestion1.Enabled = False
    txtSecurityQuestion1.ReadOnly = True
    cboSecurityQuestion2.Enabled = False
    txtSecurityQuestion2.ReadOnly = True
    cboSecurityQuestion3.Enabled = False
    txtSecurityQuestion3.ReadOnly = True

    btnSecurityQuestion1Cancel.Visible = False
    btnSecurityQuestion2Cancel.Visible = False
    btnSecurityQuestion3Cancel.Visible = False

    btnSecurityQuestionsView.Text = "Hide"
    btnSecurityQuestionsView_Click(Nothing, Nothing)

    Me.Refresh()

    Return pFault
  End Function
  Private Sub btnUserRefresh_Click(sender As Object, e As EventArgs) Handles btnUserRefresh.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    Cursor = Cursors.WaitCursor
    pFault = LoadUser()
    Cursor = Cursors.Default

    If pFault.isOK = False Then ShowFault(pFault, _Requester)

  End Sub
  Private Sub btnPasswordHashedUpdate_Click(sender As Object, e As EventArgs) Handles btnPasswordHashedUpdate.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFunction As String = "btnPasswordHashedUpdate_Click"

    Dim pNewValue As String
    Dim pSucceeded As Boolean
    frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.PasswordTextBox
    frmUpdateField.DialoguePrompt = "Write a new Password "

    Do
      frmUpdateField.DialogueInitialValue = ""
      frmUpdateField.ShowDialog()
      If frmUpdateField.DialogResult = DialogResult.OK Then
        pNewValue = frmUpdateField.DialogueReturnValue.ToString
        Try
          pNewValue = pNewValue.ToString
          pSucceeded = True
        Catch ex As Exception
          pSucceeded = False
        End Try
      Else
        Exit Sub
      End If
    Loop Until pSucceeded = True

    Cursor = Cursors.WaitCursor
    Dim pFault As clsFault

    Try
      pFault = _User.ChangePassword(pNewValue, _Requester)
    Catch ex As Exception
      pFault = New clsFault()
      pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-130208-0857", _Requester)
    End Try
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)

  End Sub

  Private Sub btnCreateBiometricKey_Click(sender As Object, e As EventArgs) Handles btnCreateBiometricKey.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFunction As String = "btnCreateBiometricKey_Click"

    Cursor = Cursors.WaitCursor
    Dim pFault As clsFault = Nothing

    If btnCreateBiometricKey.Text = "Create 'Biometric' Key" Then
      Dim pResponse As String = frmMessageOrInputBox.GetInput($"Please use the OTP from your last login, enter 99 to get a new one.")
      If pResponse = "" Then Cursor = Cursors.Default : Exit Sub

      If pResponse = "99" Then
        'This send sends an OTP to the user to confirm it's them
        Dim pMessagingMode As clsEnums.enmMessagingMode = clsEnums.enmMessagingMode.UD
        pFault = csMFA.SetMFA(_Requester.UserID, "", "CreateBiometricKey", "", _Requester, pMessagingMode)
        If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub
        pResponse = frmMessageOrInputBox.GetInput($"An OTP has been sent to you via {pMessagingMode.FastToString()}. Please enter it below to continue.")
        If pResponse = "" Then Cursor = Cursors.Default : Exit Sub
      End If

      Dim pComputer As New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=True, _Requester, pFault) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub
      Dim pApplicationIdentifier As String = pComputer.EnvironmentUserName & "#" & pComputer.ComputerIdentifier
      pApplicationIdentifier = ccHelper.Encrypt(ccHelper.enmHashType.SHA1, pApplicationIdentifier)
      Dim pKey As String = ""
      pFault = ccSecurity.CreateBiometricKeyWithLastOTPForExistingUser(_Requester.UserName, pResponse, My.Application.Info.AssemblyName, pApplicationIdentifier, pComputer.ClientReportedIP, pComputer.ClientReportedCtry, _Requester, pKey)
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub
      My.Settings.LoginKey = pKey
      My.Settings.Save()
      btnCreateBiometricKey.Text = "Remove all 'Biometric' Keys"
    ElseIf btnCreateBiometricKey.Text = "Remove all 'Biometric' Keys" Then
      pFault = ccSecurity.RemoveAllBiometricKeys(_User.UserName, _Requester)
      If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub
      My.Settings.LoginKey = ""
      My.Settings.Save()
      btnCreateBiometricKey.Text = "Create 'Biometric' Key"
    End If

    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
    'If pFault.isOK = False Then ShowFault(pFault, _Requester)
  End Sub

  Private Sub btnPIN_Click(sender As Object, e As EventArgs) Handles btnPIN.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFunction As String = "btnPIN_Click"

    If btnPIN.Text = "Create PIN" Then
      Dim pNewValue As String
      Dim pSucceeded As Boolean
      frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.SingleLineTextBox
      frmUpdateField.DialoguePrompt = "Create a new PIN"

      Do
        frmUpdateField.DialogueInitialValue = ""
        frmUpdateField.ShowDialog()
        If frmUpdateField.DialogResult = DialogResult.OK Then
          pNewValue = frmUpdateField.DialogueReturnValue.ToString
          Try
            pNewValue = pNewValue.ToString
            pSucceeded = True
          Catch ex As Exception
            pSucceeded = False
          End Try
        Else
          Exit Sub
        End If
      Loop Until pSucceeded = True

      If pNewValue = "" Then Return

      Cursor = Cursors.WaitCursor
      Dim pFault As clsFault

      Try
        pFault = _User.ChangePIN(pNewValue, _Requester)
      Catch ex As Exception
        pFault = New clsFault
        pFault.LogException(60, ex, "Value=" & pNewValue.ToString, "TRGT-250906-1155", _Requester)
      End Try
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      If Not pFault.isOK Then
        Return
      End If
      btnPIN.Text = "Delete PIN"
    Else
      Cursor = Cursors.WaitCursor
      Dim pFault As clsFault

      Try
        pFault = _User.ChangePIN("", _Requester)
      Catch ex As Exception
        pFault = New clsFault
        pFault.LogException(60, ex, "Value=''", "TRGT-250907-152026", _Requester)
      End Try
      Cursor = Cursors.Default
      ShowFault(pFault, _Requester)
      If Not pFault.isOK Then
        Return
      End If
      btnPIN.Text = "Create PIN"
    End If
    btnViewPIN.Visible = (btnPIN.Text = "Delete PIN")
    'If pFault.isOK = False Then ShowFault(pFault, _Requester)
  End Sub

  Private Sub btnUILangChange_Click(sender As Object, e As EventArgs) Handles btnUILangChange.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If btnUILangChange.Text = "Change" Then
      cboUILang.Visible = True
      txtUILang.Visible = False
      cboUILang.SelectedValue = _Requester.UILang
      btnUILangChange.Text = "Update"
      btnUILangCancel.Visible = True
    ElseIf btnUILangChange.Text = "Update" Then
      Dim pNewLang As clsEnums.enmLanguage = CType(cboUILang.SelectedValue, clsEnums.enmLanguage)
      If pNewLang = _Requester.UILang Then
        frmMessageOrInputBox.ShowMsg("The present language is already " & _UILanguages.FindByKey(pNewLang).Text & "!", frmMessageOrInputBox.enmIconType.Exclamation)
        Exit Sub
      End If
      Dim pResponse As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("The application will restart if you change the language. Continue?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes")
      If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then
        btnUILangCancel.PerformClick()
        Exit Sub
      End If
      'Get the language
      Dim pLangauge As New csLanguage
      pFault = pLangauge.GetByCode(pNewLang.ToString, _Requester, True)
      If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub

      My.Settings.Language = pLangauge.Code
      My.Settings.Culture = pLangauge.Culture
      My.Settings.Save()

      'now set the user language
      Dim pUser As New csUser
      pFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
      pUser.Language = pNewLang
      pFault = pUser.Update(_Requester, False) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      RestartMe()

    End If
  End Sub
  Private Sub btnUILangCancel_Click(sender As Object, e As EventArgs) Handles btnUILangCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    txtUILang.Text = _UILanguages.FindByKey(_Requester.UILang).Text
    btnUILangCancel.Visible = False
    btnUILangChange.Text = "Change"
    cboUILang.Visible = False
    txtUILang.Visible = True
  End Sub

  Private Sub btnLTLangChange_Click(sender As Object, e As EventArgs) Handles btnLTLangChange.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name

    If btnLTLangChange.Text = "Change" Then
      cboLTLang.Visible = True
      txtLTLang.Visible = False
      cboLTLang.SelectedValue = clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage)
      btnLTLangChange.Text = "Update"
      btnLTLangCancel.Visible = True
    ElseIf btnLTLangChange.Text = "Update" Then
      Dim pNewLang As clsEnums.enmLanguage = CType(cboLTLang.SelectedValue, clsEnums.enmLanguage)
      If pNewLang = clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage) Then
        frmMessageOrInputBox.ShowMsg("The present language is already " & _LTLanguages.FindByKey(pNewLang).Text & "!", frmMessageOrInputBox.enmIconType.Exclamation)
        Exit Sub
      End If

      My.Settings.LocalizedTextLanguage = pNewLang.FastToString()
      LocalizedTextLanguage = clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage)
      My.Settings.Save()

      cboLTLang.Visible = False
      txtLTLang.Text = cboLTLang.Text
      txtLTLang.Visible = True
      btnLTLangChange.Text = "Change"
      btnLTLangCancel.Visible = False

    End If
  End Sub

  Private Sub btnLTLangCancel_Click(sender As Object, e As EventArgs) Handles btnLTLangCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    txtLTLang.Text = _LTLanguages.FindByKey(clsEnums.TranslateEnmLanguage(My.Settings.LocalizedTextLanguage)).Text
    btnLTLangCancel.Visible = False
    btnLTLangChange.Text = "Change"
    cboLTLang.Visible = False
    txtLTLang.Visible = True
  End Sub


  Private Sub btnMessagingModeChange_Click(sender As Object, e As EventArgs) Handles btnMessagingModeChange.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name

    Dim pUser As New csUser
    Dim pFault As clsFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

    If btnMessagingModeChange.Text = "Change" Then
      cboMessagingMode.Visible = True
      txtMessagingMode.Visible = False

      cboMessagingMode.SelectedValue = pUser.MessagingMode
      btnMessagingModeChange.Text = "Update"
      btnMessagingModeCancel.Visible = True
    ElseIf btnMessagingModeChange.Text = "Update" Then
      Dim pNewMode As clsEnums.enmMessagingMode = CType(cboMessagingMode.SelectedValue, clsEnums.enmMessagingMode)
      If pNewMode = pUser.MessagingMode Then
        frmMessageOrInputBox.ShowMsg("The present messging mode is already " & _MessagingModes.FindByKey(pNewMode).Text & "!", frmMessageOrInputBox.enmIconType.Exclamation)
        Exit Sub
      End If

      pUser.MessagingMode = pNewMode
      _User.MessagingMode = pNewMode
      pFault = pUser.Update(_Requester, False) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      cboMessagingMode.Visible = False
      txtMessagingMode.Text = cboMessagingMode.Text
      txtMessagingMode.Visible = True
      btnMessagingModeChange.Text = "Change"
      btnMessagingModeCancel.Visible = False

    End If
  End Sub

  Private Sub btnMessagingModeCancel_Click(sender As Object, e As EventArgs) Handles btnMessagingModeCancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name

    Dim pUser As New csUser
    Dim pFault As clsFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

    txtMessagingMode.Text = _MessagingModes.FindByKey(pUser.MessagingMode).Text
    btnMessagingModeCancel.Visible = False
    btnMessagingModeChange.Text = "Change"
    cboMessagingMode.Visible = False
    txtMessagingMode.Visible = True
  End Sub




  'Permissions
  Private Sub ResetPermissionsForDefaultRoles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As New clsFault

    If AreYouSure("reset permissions for the default roles (SysAdmin, Administrator, UserManger, User)") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.ResetPermissionsForDefaultRoles(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub

  'Database
  Private Sub btnEjectAllUsers_Click(sender As Object, e As EventArgs)
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Eject All Users") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.EjectAllUsers(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnEjectNonMasterUsersOnly_Click(sender As Object, e As EventArgs)
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Eject Non Master Users Only") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.EjectNonMaster(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnRequestIndexReorganization_Click(sender As Object, e As EventArgs)
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Request Index Reorganization") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.RequestIndexReorganization(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub
  Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs)
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If AreYouSure("Backup Database") = False Then
      Exit Sub
    End If

    Cursor = Cursors.WaitCursor
    pFault = ccDatabaseMaintenance.RequestDatabaseBackup(_Requester)
    Cursor = Cursors.Default
    ShowFault(pFault, _Requester)
  End Sub

  Private Sub btnDefaultFontSize_Click(sender As Object, e As EventArgs) Handles btnDefaultFontSize.Click
    cboFontSize.SelectedItem = "10"
  End Sub

  Private Sub cboFontSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFontSize.SelectedIndexChanged
    Dim pSize As Single = CSng(cboFontSize.SelectedItem)

    lblFontSize.Font = New Font(MyFont.Name, pSize)
  End Sub

  Private Sub btnChangeFontSize_Click(sender As Object, e As EventArgs) Handles btnChangeFontSize.Click

    Dim pResponse As frmMessageOrInputBox.enmButtonReturned = frmMessageOrInputBox.ShowMsg("The application will restart if you change the font size. Continue?", frmMessageOrInputBox.enmIconType.QuestionMark, frmMessageOrInputBox.enmButtons.YesNo, vYesText:="Yes")
    If pResponse <> frmMessageOrInputBox.enmButtonReturned.Yes Then
      btnUILangCancel.PerformClick()
      Exit Sub
    End If

    Dim sPointSize As String = cboFontSize.Text
    Dim pPointSize As Single
    pPointSize = CSng(sPointSize)

    MyFont = New Font(MyFont.Name, pPointSize, FontStyle.Regular)
    My.Settings.FontSize = pPointSize
    My.Settings.Save()

    frmMain.Font = MyFont

    RestartMe()

    'frmMain.PerformAutoScale()

  End Sub

  Private Sub ctlPnlMaintenance_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged

    Dim pSize As Single = CSng(14 * MyFont.Size / 9)
    lblTitle.Font = New Font(MyFont.Name, pSize, FontStyle.Italic) 'FontStyle.Bold Or 

    'Set the font for the BN 
    dgvLastLogins.RowTemplate.Height = ccHelper.ToInteger(23 * MyFont.Size / 9)

  End Sub

  Private Sub grdTableSizes_evtRowDoubleClicked(vTableSize As csTableSize, ByRef rHandled As Boolean)
    rHandled = True
  End Sub

  Private Sub grdIndexFragmentation_evtRowDoubleClicked(vIndexFragmentation As csIndexFragmentation, ByRef rHandled As Boolean)
    rHandled = True
  End Sub

  Private Sub btnViewPIN_Click(sender As Object, e As EventArgs) Handles btnViewPIN.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name

    Dim pFault As clsFault
    Dim pUser As New csUser

    pFault = pUser.GetByID(_Requester.UserID, _Requester, True)
    If pFault.isOK = False Then ShowFault(pFault, _Requester) : Return

    frmMessageOrInputBox.ShowMsg(pUser.PIN(vDecrypt:=True), frmMessageOrInputBox.enmIconType.Information)
  End Sub

  Private Sub btnSecurityQuestion1Change_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion1Change.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If btnSecurityQuestion1Change.Text = "Change" Then
      cboSecurityQuestion1.Enabled = True
      txtSecurityQuestion1.ReadOnly = False
      btnSecurityQuestion1Change.Text = "Update"
      btnSecurityQuestion1Cancel.Visible = True
    ElseIf btnSecurityQuestion1Change.Text = "Update" Then
      Dim pUser As New csUser
      pFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
      pFault = pUser.UpdateSecurityCodeAndQuestion(1, cboSecurityQuestion1.SelectedValue.ToString(), txtSecurityQuestion1.Text, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      pFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      btnSecurityQuestion1Change.Text = "Change"
      btnSecurityQuestion1Cancel.Visible = False
    End If
  End Sub
  Private Sub btnSecurityQuestion1Cancel_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion1Cancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
    btnSecurityQuestion1Cancel.Visible = False
    btnSecurityQuestion1Change.Text = "Change"
  End Sub

  Private Sub btnSecurityQuestion2Change_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion2Change.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If btnSecurityQuestion2Change.Text = "Change" Then
      cboSecurityQuestion2.Enabled = True
      txtSecurityQuestion2.ReadOnly = False
      btnSecurityQuestion2Change.Text = "Update"
      btnSecurityQuestion2Cancel.Visible = True
    ElseIf btnSecurityQuestion2Change.Text = "Update" Then
      Dim pUser As New csUser
      pFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
      pFault = pUser.UpdateSecurityCodeAndQuestion(2, cboSecurityQuestion2.SelectedValue.ToString(), txtSecurityQuestion2.Text, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      pFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      btnSecurityQuestion2Change.Text = "Change"
      btnSecurityQuestion2Cancel.Visible = False
    End If
  End Sub
  Private Sub btnSecurityQuestion2Cancel_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion2Cancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
    btnSecurityQuestion2Cancel.Visible = False
    btnSecurityQuestion2Change.Text = "Change"
  End Sub


  Private Sub btnSecurityQuestion3Change_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion3Change.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault

    If btnSecurityQuestion3Change.Text = "Change" Then
      cboSecurityQuestion3.Enabled = True
      txtSecurityQuestion3.ReadOnly = False
      btnSecurityQuestion3Change.Text = "Update"
      btnSecurityQuestion3Cancel.Visible = True
    ElseIf btnSecurityQuestion3Change.Text = "Update" Then
      Dim pUser As New csUser
      pFault = pUser.GetByID(_Requester.UserID, _Requester, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
      pFault = pUser.UpdateSecurityCodeAndQuestion(3, cboSecurityQuestion3.SelectedValue.ToString(), txtSecurityQuestion3.Text, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      pFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return

      btnSecurityQuestion3Change.Text = "Change"
      btnSecurityQuestion3Cancel.Visible = False
    End If
  End Sub
  Private Sub btnSecurityQuestion3Cancel_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestion3Cancel.Click
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name
    Dim pFault As clsFault = LoadUser() : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return
    btnSecurityQuestion3Cancel.Visible = False
    btnSecurityQuestion3Change.Text = "Change"
  End Sub

  Private Sub btnSecurityQuestionsView_Click(sender As Object, e As EventArgs) Handles btnSecurityQuestionsView.Click
    If btnSecurityQuestionsView.Text = "View" Then
      txtSecurityQuestion1.Show() : btnSecurityQuestion1Change.Show()
      txtSecurityQuestion2.Show() : btnSecurityQuestion2Change.Show()
      txtSecurityQuestion3.Show() : btnSecurityQuestion3Change.Show()
      btnSecurityQuestionsView.Text = "Hide"
    Else
      txtSecurityQuestion1.Hide() : btnSecurityQuestion1Change.Hide()
      txtSecurityQuestion2.Hide() : btnSecurityQuestion2Change.Hide()
      txtSecurityQuestion3.Hide() : btnSecurityQuestion3Change.Hide()
      btnSecurityQuestionsView.Text = "View"
    End If
  End Sub

End Class
