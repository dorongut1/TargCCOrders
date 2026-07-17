Public Class frmLoginOTP  
  
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
      Dim pPassword As String = txtEmail.Text.Trim  
  
      If pUserName.Length = 0 OrElse pPassword.Length = 0 Then  
        Exit Sub  
      End If  
  
      Cursor = Cursors.WaitCursor  
      btnOK.Enabled = False  
      btnCancel.Enabled = False  
      Application.DoEvents() 
 
      pFault = ccSecurity.LogInByOTP(pUserName, pPassword, _Requester) 
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
        txtEmail.Text = ""  
        Exit Sub  
      End If  
  
      If _Requester.LoggedLoginID < -9 Then  
        '2 Factor authentication   
        If txtEmail.Top = txtUserName.Top Then  
          lblEmail.Location = txtUserName.Location  
          lblEmail.Top += 4  
        End If  
        txtUserName.Text = ""  
        txtUserName.Visible = False  
        lblUserName.Text = "Enter the number you received"  
        lblEmail.Text = "Number"  
        lblEmail.Left += 20  
        txtEmail.Text = ""  
        txtEmail.UseSystemPasswordChar = False  
        txtEmail.PasswordChar = "#"c  
        txtEmail.Font = New Font(FontFamily.GenericMonospace, 10)  
        btnOK.Enabled = True  
        btnCancel.Enabled = True  
        Cursor = Cursors.Default  
        txtEmail.Focus()  
        Exit Sub  
      End If  
  
    Else 'If txtPassword.Visible = false Then  
      Dim pSMSPassword As String = txtEmail.Text  
      If pSMSPassword.Length = 0 Then  
        Exit Sub  
      End If  
      _Requester.CallingFunctionWithinApplication = "frmLoginOTP:OK_Click"  
      Cursor = Cursors.WaitCursor  
      pFault = ccSecurity.Check2FactorAuthenticationForLogin(pSMSPassword, _Requester)  
      If pFault.isOK = False Then  
        Cursor = Cursors.Default  
        Application.DoEvents()  
        ShowFault(pFault, _Requester)  
        If pFault.Number = 144 Then  
          txtEmail.Text = ""  
          txtEmail.UseSystemPasswordChar = False  
          txtEmail.PasswordChar = "#"c  
          txtEmail.Font = New Font(FontFamily.GenericMonospace, 10)  
          txtEmail.Focus()  
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
 
  Public Sub LoginViaBiometric() 
    Dim pFault As New clsFault 
 
    Dim pComputer As New csAccessingEntity(vLoadPCDetails:=True, vLoadIPAndCountry:=False, _Requester, pFault) : If pFault.isOK = False Then Cursor = Cursors.Default : ShowFault(pFault, _Requester) : Exit Sub 
 
    Dim pApplicationIdentifier As String = pComputer.EnvironmentUserName & "#" & pComputer.ComputerIdentifier 
    pApplicationIdentifier = ccHelper.Encrypt(ccHelper.enmHashType.SHA1, pApplicationIdentifier) 
 
    pFault = ccSecurity.LogInByBiometric(pApplicationIdentifier, My.Settings.LoginKey, _Requester, _UILang) 
    If pFault.isOK = False Then 
      If pFault.Number = 91 OrElse pFault.Number = 92 Then 
        My.Settings.LoginKey = "" 
        My.Settings.Save() 
      End If 
      ShowFault(pFault, _Requester) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End If 
 
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
      If String.IsNullOrEmpty(My.Settings.LoginKey) OrElse My.Settings.LoginKey.Equals("zz", StringComparison.OrdinalIgnoreCase) Then 
        Me.btnOK.Visible = True 
        Me.ShowDialog(fParent) 
      Else 
        LoginViaBiometric() 
        frmAbout.Show() 
        Application.DoEvents() 
        If String.IsNullOrEmpty(_Requester.UserPIN) Then 
          frmMessageOrInputBox.ShowMsg($"Welcome {_Requester.UserFullName}", frmMessageOrInputBox.enmIconType.Information) 
        Else 
          Dim pPin As String = frmMessageOrInputBox.GetInput($"Welcome {_Requester.UserFullName}({_Requester.UserPIN}){Environment.NewLine}Enter your PIN", _Requester.UserPIN.Length) 
          If pPin <> _Requester.UserPIN Then 
            frmMessageOrInputBox.ShowMsg($"Invalid PIN", frmMessageOrInputBox.enmIconType.Exclamation) 
            ccSecurity.LogOut(_Requester) 
            Application.DoEvents() 
            Environment.Exit(0) 
          End If 
        End If 
        Me.Hide() 
      End If 
    Else 
      Throw New Exception("Login screen is required!!") 
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
  
  Private Sub frmLoginOTP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load  
    If Me.DesignMode = True Then Exit Sub  
  
    Me.Text = My.Application.Info.ProductName & " Login"  
    txtUserName.Text = ""  
    txtEmail.Text = ""  
  
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
  
  Private Sub frmLoginOTP_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged  
    If Me.Visible = True Then  
      'If _WSLoader.Loaded = False Then Me.btnOK.Enabled = False  
      Application.DoEvents()  
      txtUserName.Focus()  
    End If  
  End Sub   
  
  Private Sub _WSLoader_evtLoaded() Handles _WSLoader.evtLoaded  
    'Me.btnOK.Enabled = True  
  End Sub  
  
  Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged  
    If txtUserName.Visible Then Return  
  
    Dim pTxt As String = txtEmail.Text  
  
    If Not ccHelper.IsNumeric(pTxt) Then txtEmail.Text = ""  
    If pTxt.Length = 6 Then btnOK.PerformClick()  
  End Sub  
  
End Class  
