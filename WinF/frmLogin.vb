Public Class frmLogin 
 
  Private _Requester As clsRequester 
 
  Private _UILang As clsEnums.enmLanguage 
 
  Private Event evtOKClicked(ByRef rCancelled As Boolean) 
  Private Event evtCancelClicked(ByRef rExitApp As Boolean) 
  Private Event evtFormLoaded() 
  Private Event evtFormResize() 
 
  Private WithEvents _WSLoader As WSLoader 
 
  Friend ReadOnly Property Requester As clsRequester 
    Get 
      Return _Requester 
    End Get 
  End Property 
 
  Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click 
    Dim pFault As clsFault 
 
    If txtUserName.Visible Then 
      Dim pUserName As String = txtUserName.Text.Trim 
      Dim pPassword As String = txtPassword.Text.Trim 
 
      If pUserName.Length = 0 OrElse pPassword.Length = 0 Then 
        Exit Sub 
      End If 
 
      Cursor = Cursors.WaitCursor 
      btnOK.Enabled = False 
      btnCancel.Enabled = False 
      Application.DoEvents() 
 
      If ccSecurity.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByDomainGroup OrElse 
         ccSecurity.UserIdentificationModel = clsEnums.enmUserIdentificationModel.ByDomainUser Then 
        'This can only be run if we are working opposite the web service (ie, ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.SpecificUserCredentials) 
        pFault = ccSecurity.LogInByNetworkCredentials(_Requester, vOverrideUILang:=_UILang, pUserName, pPassword) 
      Else 
        pFault = ccSecurity.LogInByNamePwd(pUserName, pPassword, _Requester, vSendMessageFor2FA:=False, vSendMessageOnPasswordExpiry:=False) 
      End If 
      If pFault.isOK = False AndAlso (pFault.Number = 121 OrElse pFault.Number = 122) Then 'Password expired 
        Cursor = Cursors.Default 
        btnOK.Enabled = True 
        btnCancel.Enabled = True 
        Application.DoEvents() 
        ShowFault(pFault, _Requester) 
 
        'Get New Password 
        Dim pSucceeded As Boolean = False 
        frmUpdateField.StartPosition = FormStartPosition.CenterScreen 
        Dim pPrompt As String = "Enter the new password" 
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.PasswordTextBox 
        frmUpdateField.DialoguePrompt = pPrompt 
        Dim pNewPassword As String = ""  
        Do 
          frmUpdateField.DialogueInitialValue = "" 
          frmUpdateField.ShowDialog() 
          If frmUpdateField.DialogResult = DialogResult.OK Then 
            Try 
              pNewPassword = frmUpdateField.DialogueReturnValue.ToString 
              pSucceeded = True 
            Catch ex As Exception 
              pSucceeded = False 
            End Try 
          Else 
            Application.DoEvents()  
            Environment.Exit(0)  
          End If  
        Loop Until pSucceeded = True  
  
        pSucceeded = False 
        frmUpdateField.StartPosition = FormStartPosition.CenterScreen 
        pPrompt = "Enter the new password again" 
        frmUpdateField.DialogueDataControl = frmUpdateField.enmDataControl.PasswordTextBox 
        frmUpdateField.DialoguePrompt = pPrompt 
        Do 
          frmUpdateField.DialogueInitialValue = "" 
          frmUpdateField.ShowDialog() 
          If frmUpdateField.DialogResult = DialogResult.OK Then 
            Try 
              Dim pStrg As String = frmUpdateField.DialogueReturnValue.ToString 
              If pStrg = pNewPassword Then 
                pSucceeded = True 
              Else 
                frmMessageOrInputBox.ShowMsg("The passwords don't match", frmMessageOrInputBox.enmIconType.Exclamation) 
                pSucceeded = False 
              End If 
            Catch ex As Exception 
              pSucceeded = False 
            End Try 
          Else 
            Application.DoEvents() 
            Environment.Exit(0) 
          End If 
        Loop Until pSucceeded = True 
        pFault = ccSecurity.LogInByNamePwd(pUserName, pPassword, _Requester, vOverrideUILang:=_UILang, vNewPassword:=pNewPassword, vSendMessageFor2FA:=False, vSendMessageOnPasswordExpiry:=False) 
      End If 
      Static sTry As Integer = 0 
      If pFault.isOK = False Then 
        Cursor = Cursors.Default 
        btnOK.Enabled = True 
        btnCancel.Enabled = True 
        Application.DoEvents() 
        ShowFault(pFault, _Requester) 
        sTry += 1 
        If sTry = 3 Then 
          Application.DoEvents() 
          Environment.Exit(0) 
        End If 
        txtPassword.Text = "" 
        Exit Sub 
      End If 
 
      If _Requester.LoggedLoginID < -9 Then 
        '2 Factor authentication  
        If txtPassword.Top = txtUserName.Top Then 
          lblPassword.Location = txtUserName.Location 
          lblPassword.Top += 4 
        End If 
        txtUserName.Text = "" 
        txtUserName.Visible = False 
        lblUserName.Text = "Enter the number you received" 
        lblPassword.Text = "Number" 
        lblPassword.Left += 20 
        txtPassword.Text = "" 
        txtPassword.UseSystemPasswordChar = False 
        txtPassword.PasswordChar = "#"c 
        txtPassword.Font = New Font(FontFamily.GenericMonospace, 10) 
        btnOK.Enabled = True 
        btnCancel.Enabled = True 
        Cursor = Cursors.Default 
        txtPassword.Focus() 
        Exit Sub 
      End If 
 
    Else 'If txtPassword.Visible = false Then 
      Dim pSMSPassword As String = txtPassword.Text 
      If pSMSPassword.Length = 0 Then 
        Exit Sub 
      End If 
      _Requester.CallingFunctionWithinApplication = "FrmLogin:OK_Click" 
      Cursor = Cursors.WaitCursor 
      pFault = ccSecurity.Check2FactorAuthenticationForLogin(pSMSPassword, _Requester) 
      If pFault.isOK = False Then 
        Cursor = Cursors.Default 
        Application.DoEvents() 
        ShowFault(pFault, _Requester) 
        If pFault.Number = 144 Then 
          txtPassword.Text = "" 
          txtPassword.UseSystemPasswordChar = False 
          txtPassword.PasswordChar = "#"c 
          txtPassword.Font = New Font(FontFamily.GenericMonospace, 10) 
          txtPassword.Focus() 
          _Requester.CallingFunctionWithinApplication = "" 
          Return 
        End If 
        Application.DoEvents() 
        Environment.Exit(0) 
      End If 
      _Requester.CallingFunctionWithinApplication = "" 
    End If 
 
    'last chance to cancel from outside source 
    Dim pCancelled As Boolean = False 
    RaiseEvent evtOKClicked(pCancelled) 
 
    If pCancelled = True Then 
      Cursor = Cursors.Default 
      btnOK.Enabled = True 
      btnCancel.Enabled = True 
      Application.DoEvents() 
      Exit Sub 
    End If 
    
    frmAbout.Show()
    Application.DoEvents()
    
    Me.Close() 
  End Sub 
 
  Public Sub LoginWithNoNamePwd() 
    Dim pFault As New clsFault 
 
    pFault = ccSecurity.LogInByNetworkCredentials(_Requester, _UILang) 
    If pFault.isOK = False Then 
      ShowFault(pFault, _Requester) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End If 
 
    Me.Close() 
  End Sub 
 
  Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click 
    Dim pExitApp As Boolean = True 
    RaiseEvent evtCancelClicked(pExitApp) 
    If pExitApp = True Then 
      Application.DoEvents() 
      Environment.Exit(0) 
    End If 
    Me.Close() 
  End Sub 
 
  Public Sub LoadMe(ByRef fParent As Form) 
 
    '1st find the language 
    'set language 
    Dim pLanguage As String = My.Settings.Language 
 
    _UILang = clsEnums.TranslateEnmLanguage(pLanguage) 
 
    RaiseEvent evtFormLoaded() 
    Application.DoEvents() 
 
    _WSLoader = New WSLoader 
    _WSLoader.Load() 
    Application.DoEvents() 
 
    frmAbout.Hide() 
 
    lblVersion.Text = "Version: " & My.Application.Info.Version.ToString 
    LoadLocalizedText() 
 
    If ccSecurity.RequiresLoginScreen = True Then 
      Me.btnOK.Visible = True 
      Me.ShowDialog(fParent) 
    Else 
      LoginWithNoNamePwd() 
    End If 
  End Sub 
 
  Private Sub LoadLocalizedText() 
    If My.Settings.IsLocalized = False Then Exit Sub 
 
    Dim pStrg As String 
 
    'pStrg = ccHelper.GetLocalizedFieldName("Person", "ID", _Requester) 
    'If pStrg <> "" Then lblID.Text = pStrg 
 
    Dim pLang As clsEnums.enmLanguage = clsEnums.TranslateEnmLanguage(My.Settings.Language) 
 
    For Each p As Control In Me.Controls 
      If p.GetType().Name = "Button" Then 
        Dim pCtl As Button = CType(p, Button) 
        pStrg = ccHelper.GetLocalizedUIText("CCText", pCtl.Text, _Requester, pLang) 
        If pStrg <> "" Then pCtl.Text = pStrg 
      ElseIf p.GetType().Name = "Label" Then 
        Dim pCtl As Label = CType(p, Label) 
        pStrg = ccHelper.GetLocalizedUIText("CCText", pCtl.Text, _Requester, pLang) 
        If pStrg <> "" Then pCtl.Text = pStrg 
      End If 
    Next 
 
  End Sub 
 
  Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    If Me.DesignMode = True Then Exit Sub 
 
    Me.Text = My.Application.Info.ProductName & " Login" 
    txtUserName.Text = "" 
    txtPassword.Text = "" 
 
    'put this in evtFormResize and changed as needed 
    'MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    'Me.Font = MyFont 
    'Dim pFactor As Single = My.Settings.FontSize / 10 
    'lblLogo.Font = New Font(lblLogo.Font.FontFamily, lblLogo.Font.Size * pFactor, FontStyle.Bold Or FontStyle.Italic) 
    'lblVersion.Font = New Font(lblVersion.Font.FontFamily, lblVersion.Font.Size * pFactor, FontStyle.Regular) 
    'Me.PerformAutoScale() 
 
    RaiseEvent evtFormResize() 
 
    Me.Left = frmMain.Left + ccHelper.ToInteger((frmMain.Width - Me.Width) / 2) 
    Me.Top = frmMain.Top + ccHelper.ToInteger((frmMain.Height - Me.Height) / 2) 
 
    Me.Visible = False 
  End Sub 
 
  Private Sub frmLogin_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged 
    If Me.Visible = True Then 
      'If _WSLoader.Loaded = False Then Me.btnOK.Enabled = False 
      Application.DoEvents() 
      txtUserName.Focus() 
    End If 
  End Sub  
 
  Private Sub _WSLoader_evtLoaded() Handles _WSLoader.evtLoaded 
    'Me.btnOK.Enabled = True 
  End Sub 
 
  Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged 
    If txtUserName.Visible Then Return 
 
    Dim pTxt As String = txtPassword.Text 
 
    If Not ccHelper.IsNumeric(pTxt) Then txtPassword.Text = "" 
    If pTxt.Length = 6 Then btnOK.PerformClick() 
  End Sub 
 
End Class 
