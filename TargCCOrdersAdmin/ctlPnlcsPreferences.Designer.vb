'ColourObjectBackground
'ColourObjectReadOnlyTextBackground

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlPnlcsPreferences
  Inherits System.Windows.Forms.UserControl


  'UserControl overrides dispose to clean up the component list. 
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer 
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer 
  'It can be modified using the Windows Form Designer.   
  'Do not modify it using the code editor. 
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.gpbHeader = New System.Windows.Forms.GroupBox()
    Me.lblTitle = New System.Windows.Forms.Label()
    Me.tbc = New System.Windows.Forms.TabControl()
    Me.tbpActiveUser = New System.Windows.Forms.TabPage()
    Me.tlp = New System.Windows.Forms.TableLayoutPanel()
    Me.pnlLeft = New System.Windows.Forms.Panel()
    Me.gpbChangePassword = New System.Windows.Forms.GroupBox()
    Me.gpbSecurityQuestions = New System.Windows.Forms.GroupBox()
    Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
    Me.cboSecurityQuestion1 = New System.Windows.Forms.ComboBox()
    Me.txtSecurityQuestion1 = New System.Windows.Forms.TextBox()
    Me.btnSecurityQuestion1Cancel = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion1Change = New System.Windows.Forms.Button()
    Me.txtSecurityQuestion2 = New System.Windows.Forms.TextBox()
    Me.txtSecurityQuestion3 = New System.Windows.Forms.TextBox()
    Me.btnSecurityQuestion2Cancel = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion2Change = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion3Cancel = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion3Change = New System.Windows.Forms.Button()
    Me.cboSecurityQuestion2 = New System.Windows.Forms.ComboBox()
    Me.cboSecurityQuestion3 = New System.Windows.Forms.ComboBox()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.btnViewPIN = New System.Windows.Forms.Button()
    Me.btnCreateBiometricKey = New System.Windows.Forms.Button()
    Me.btnPasswordHashedUpdate = New System.Windows.Forms.Button()
    Me.btnPIN = New System.Windows.Forms.Button()
    Me.gpbDetails = New System.Windows.Forms.GroupBox()
    Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtType = New System.Windows.Forms.TextBox()
    Me.txtLastName = New System.Windows.Forms.TextBox()
    Me.lblName = New System.Windows.Forms.Label()
    Me.txtEmail = New System.Windows.Forms.TextBox()
    Me.txtFirstName = New System.Windows.Forms.TextBox()
    Me.txtUserName = New System.Windows.Forms.TextBox()
    Me.lblUserName = New System.Windows.Forms.Label()
    Me.lblEmail = New System.Windows.Forms.Label()
    Me.lblType = New System.Windows.Forms.Label()
    Me.lblPhoneNo = New System.Windows.Forms.Label()
    Me.txtPhoneNo = New System.Windows.Forms.TextBox()
    Me.lblRoles = New System.Windows.Forms.Label()
    Me.txtRoles = New System.Windows.Forms.TextBox()
    Me.pnlRight = New System.Windows.Forms.Panel()
    Me.gpbSecurity = New System.Windows.Forms.GroupBox()
    Me.dgvLastLogins = New System.Windows.Forms.DataGridView()
    Me.lblLastLogins = New System.Windows.Forms.Label()
    Me.lblLastLoginThisApp = New System.Windows.Forms.Label()
    Me.txtLastLoginThisApp = New System.Windows.Forms.TextBox()
    Me.gpbMessagingMode = New System.Windows.Forms.GroupBox()
    Me.btnMessagingModeChange = New System.Windows.Forms.Button()
    Me.btnMessagingModeCancel = New System.Windows.Forms.Button()
    Me.txtMessagingMode = New System.Windows.Forms.TextBox()
    Me.cboMessagingMode = New System.Windows.Forms.ComboBox()
    Me.btnUserRefresh = New System.Windows.Forms.Button()
    Me.gpbUserInterfaceLanguage = New System.Windows.Forms.GroupBox()
    Me.lblLTLangExplanation = New System.Windows.Forms.Label()
    Me.btnLTLangChange = New System.Windows.Forms.Button()
    Me.btnLTLangCancel = New System.Windows.Forms.Button()
    Me.txtLTLang = New System.Windows.Forms.TextBox()
    Me.cboLTLang = New System.Windows.Forms.ComboBox()
    Me.gpbChangeLanguage = New System.Windows.Forms.GroupBox()
    Me.btnUILangCancel = New System.Windows.Forms.Button()
    Me.btnUILangChange = New System.Windows.Forms.Button()
    Me.txtUILang = New System.Windows.Forms.TextBox()
    Me.cboUILang = New System.Windows.Forms.ComboBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.btnChangeFontSize = New System.Windows.Forms.Button()
    Me.lblFontSize = New System.Windows.Forms.Label()
    Me.cboFontSize = New System.Windows.Forms.ComboBox()
    Me.btnDefaultFontSize = New System.Windows.Forms.Button()
    Me.btnSecurityQuestionsView = New System.Windows.Forms.Button()
    Me.gpbHeader.SuspendLayout()
    Me.tbc.SuspendLayout()
    Me.tbpActiveUser.SuspendLayout()
    Me.tlp.SuspendLayout()
    Me.pnlLeft.SuspendLayout()
    Me.gpbChangePassword.SuspendLayout()
    Me.gpbSecurityQuestions.SuspendLayout()
    Me.TableLayoutPanel3.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.gpbDetails.SuspendLayout()
    Me.TableLayoutPanel2.SuspendLayout()
    Me.pnlRight.SuspendLayout()
    Me.gpbSecurity.SuspendLayout()
    CType(Me.dgvLastLogins, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gpbMessagingMode.SuspendLayout()
    Me.gpbUserInterfaceLanguage.SuspendLayout()
    Me.gpbChangeLanguage.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'gpbHeader
    '
    Me.gpbHeader.Controls.Add(Me.lblTitle)
    Me.gpbHeader.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbHeader.Location = New System.Drawing.Point(5, 5)
    Me.gpbHeader.Name = "gpbHeader"
    Me.gpbHeader.Padding = New System.Windows.Forms.Padding(3, 0, 3, 6)
    Me.gpbHeader.Size = New System.Drawing.Size(811, 56)
    Me.gpbHeader.TabIndex = 0
    Me.gpbHeader.TabStop = False
    '
    'lblTitle
    '
    Me.lblTitle.AutoSize = True
    Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left
    Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Italic)
    Me.lblTitle.Location = New System.Drawing.Point(3, 18)
    Me.lblTitle.Name = "lblTitle"
    Me.lblTitle.Size = New System.Drawing.Size(142, 31)
    Me.lblTitle.TabIndex = 0
    Me.lblTitle.Text = "All About Me"
    '
    'tbc
    '
    Me.tbc.Controls.Add(Me.tbpActiveUser)
    Me.tbc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tbc.Location = New System.Drawing.Point(5, 61)
    Me.tbc.Name = "tbc"
    Me.tbc.SelectedIndex = 0
    Me.tbc.Size = New System.Drawing.Size(811, 552)
    Me.tbc.TabIndex = 1
    '
    'tbpActiveUser
    '
    Me.tbpActiveUser.BackColor = System.Drawing.Color.Wheat
    Me.tbpActiveUser.Controls.Add(Me.tlp)
    Me.tbpActiveUser.Location = New System.Drawing.Point(4, 26)
    Me.tbpActiveUser.Name = "tbpActiveUser"
    Me.tbpActiveUser.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpActiveUser.Size = New System.Drawing.Size(803, 522)
    Me.tbpActiveUser.TabIndex = 0
    Me.tbpActiveUser.Text = "Details"
    '
    'tlp
    '
    Me.tlp.ColumnCount = 2
    Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlp.Controls.Add(Me.pnlLeft, 0, 0)
    Me.tlp.Controls.Add(Me.pnlRight, 1, 0)
    Me.tlp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tlp.Location = New System.Drawing.Point(3, 3)
    Me.tlp.Name = "tlp"
    Me.tlp.RowCount = 1
    Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlp.Size = New System.Drawing.Size(797, 516)
    Me.tlp.TabIndex = 67
    Me.tlp.Visible = False
    '
    'pnlLeft
    '
    Me.pnlLeft.Controls.Add(Me.gpbChangePassword)
    Me.pnlLeft.Controls.Add(Me.gpbDetails)
    Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlLeft.Location = New System.Drawing.Point(3, 3)
    Me.pnlLeft.Name = "pnlLeft"
    Me.pnlLeft.Padding = New System.Windows.Forms.Padding(5)
    Me.pnlLeft.Size = New System.Drawing.Size(392, 510)
    Me.pnlLeft.TabIndex = 0
    '
    'gpbChangePassword
    '
    Me.gpbChangePassword.Controls.Add(Me.gpbSecurityQuestions)
    Me.gpbChangePassword.Controls.Add(Me.TableLayoutPanel1)
    Me.gpbChangePassword.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbChangePassword.Location = New System.Drawing.Point(5, 215)
    Me.gpbChangePassword.Name = "gpbChangePassword"
    Me.gpbChangePassword.Size = New System.Drawing.Size(382, 290)
    Me.gpbChangePassword.TabIndex = 65
    Me.gpbChangePassword.TabStop = False
    Me.gpbChangePassword.Text = "Security"
    '
    'gpbSecurityQuestions
    '
    Me.gpbSecurityQuestions.Controls.Add(Me.btnSecurityQuestionsView)
    Me.gpbSecurityQuestions.Controls.Add(Me.TableLayoutPanel3)
    Me.gpbSecurityQuestions.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbSecurityQuestions.Location = New System.Drawing.Point(3, 91)
    Me.gpbSecurityQuestions.Name = "gpbSecurityQuestions"
    Me.gpbSecurityQuestions.Size = New System.Drawing.Size(376, 196)
    Me.gpbSecurityQuestions.TabIndex = 66
    Me.gpbSecurityQuestions.TabStop = False
    Me.gpbSecurityQuestions.Text = "Security Questions"
    '
    'TableLayoutPanel3
    '
    Me.TableLayoutPanel3.ColumnCount = 3
    Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.TableLayoutPanel3.Controls.Add(Me.cboSecurityQuestion1, 0, 0)
    Me.TableLayoutPanel3.Controls.Add(Me.txtSecurityQuestion1, 1, 0)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion1Cancel, 1, 1)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion1Change, 2, 1)
    Me.TableLayoutPanel3.Controls.Add(Me.txtSecurityQuestion2, 1, 2)
    Me.TableLayoutPanel3.Controls.Add(Me.txtSecurityQuestion3, 1, 4)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion2Cancel, 1, 3)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion2Change, 2, 3)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion3Cancel, 1, 5)
    Me.TableLayoutPanel3.Controls.Add(Me.btnSecurityQuestion3Change, 2, 5)
    Me.TableLayoutPanel3.Controls.Add(Me.cboSecurityQuestion2, 0, 2)
    Me.TableLayoutPanel3.Controls.Add(Me.cboSecurityQuestion3, 0, 4)
    Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 39)
    Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
    Me.TableLayoutPanel3.RowCount = 6
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel3.Size = New System.Drawing.Size(370, 154)
    Me.TableLayoutPanel3.TabIndex = 0
    '
    'cboSecurityQuestion1
    '
    Me.cboSecurityQuestion1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cboSecurityQuestion1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion1.FormattingEnabled = True
    Me.cboSecurityQuestion1.Location = New System.Drawing.Point(5, 0)
    Me.cboSecurityQuestion1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.cboSecurityQuestion1.Name = "cboSecurityQuestion1"
    Me.cboSecurityQuestion1.Size = New System.Drawing.Size(175, 25)
    Me.cboSecurityQuestion1.TabIndex = 69
    '
    'txtSecurityQuestion1
    '
    Me.txtSecurityQuestion1.BackColor = System.Drawing.Color.PapayaWhip
    Me.TableLayoutPanel3.SetColumnSpan(Me.txtSecurityQuestion1, 2)
    Me.txtSecurityQuestion1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtSecurityQuestion1.Location = New System.Drawing.Point(190, 0)
    Me.txtSecurityQuestion1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtSecurityQuestion1.Name = "txtSecurityQuestion1"
    Me.txtSecurityQuestion1.ReadOnly = True
    Me.txtSecurityQuestion1.Size = New System.Drawing.Size(175, 25)
    Me.txtSecurityQuestion1.TabIndex = 53
    Me.txtSecurityQuestion1.Text = "txtSecurityQuestion1"
    '
    'btnSecurityQuestion1Cancel
    '
    Me.btnSecurityQuestion1Cancel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion1Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion1Cancel.Location = New System.Drawing.Point(190, 26)
    Me.btnSecurityQuestion1Cancel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion1Cancel.Name = "btnSecurityQuestion1Cancel"
    Me.btnSecurityQuestion1Cancel.Size = New System.Drawing.Size(82, 24)
    Me.btnSecurityQuestion1Cancel.TabIndex = 2
    Me.btnSecurityQuestion1Cancel.Text = "Cancel"
    Me.btnSecurityQuestion1Cancel.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion1Change
    '
    Me.btnSecurityQuestion1Change.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion1Change.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion1Change.Location = New System.Drawing.Point(282, 26)
    Me.btnSecurityQuestion1Change.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion1Change.Name = "btnSecurityQuestion1Change"
    Me.btnSecurityQuestion1Change.Size = New System.Drawing.Size(83, 24)
    Me.btnSecurityQuestion1Change.TabIndex = 3
    Me.btnSecurityQuestion1Change.Text = "Change"
    Me.btnSecurityQuestion1Change.UseVisualStyleBackColor = True
    '
    'txtSecurityQuestion2
    '
    Me.txtSecurityQuestion2.BackColor = System.Drawing.Color.PapayaWhip
    Me.TableLayoutPanel3.SetColumnSpan(Me.txtSecurityQuestion2, 2)
    Me.txtSecurityQuestion2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtSecurityQuestion2.Location = New System.Drawing.Point(190, 52)
    Me.txtSecurityQuestion2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtSecurityQuestion2.Name = "txtSecurityQuestion2"
    Me.txtSecurityQuestion2.ReadOnly = True
    Me.txtSecurityQuestion2.Size = New System.Drawing.Size(175, 25)
    Me.txtSecurityQuestion2.TabIndex = 54
    Me.txtSecurityQuestion2.Text = "txtSecurityQuestion2"
    '
    'txtSecurityQuestion3
    '
    Me.txtSecurityQuestion3.BackColor = System.Drawing.Color.PapayaWhip
    Me.TableLayoutPanel3.SetColumnSpan(Me.txtSecurityQuestion3, 2)
    Me.txtSecurityQuestion3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtSecurityQuestion3.Location = New System.Drawing.Point(190, 104)
    Me.txtSecurityQuestion3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtSecurityQuestion3.Name = "txtSecurityQuestion3"
    Me.txtSecurityQuestion3.ReadOnly = True
    Me.txtSecurityQuestion3.Size = New System.Drawing.Size(175, 25)
    Me.txtSecurityQuestion3.TabIndex = 55
    Me.txtSecurityQuestion3.Text = "txtSecurityQuestion3"
    '
    'btnSecurityQuestion2Cancel
    '
    Me.btnSecurityQuestion2Cancel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion2Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion2Cancel.Location = New System.Drawing.Point(190, 78)
    Me.btnSecurityQuestion2Cancel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion2Cancel.Name = "btnSecurityQuestion2Cancel"
    Me.btnSecurityQuestion2Cancel.Size = New System.Drawing.Size(82, 24)
    Me.btnSecurityQuestion2Cancel.TabIndex = 58
    Me.btnSecurityQuestion2Cancel.Text = "Cancel"
    Me.btnSecurityQuestion2Cancel.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion2Change
    '
    Me.btnSecurityQuestion2Change.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion2Change.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion2Change.Location = New System.Drawing.Point(282, 78)
    Me.btnSecurityQuestion2Change.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion2Change.Name = "btnSecurityQuestion2Change"
    Me.btnSecurityQuestion2Change.Size = New System.Drawing.Size(83, 24)
    Me.btnSecurityQuestion2Change.TabIndex = 57
    Me.btnSecurityQuestion2Change.Text = "Change"
    Me.btnSecurityQuestion2Change.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion3Cancel
    '
    Me.btnSecurityQuestion3Cancel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion3Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion3Cancel.Location = New System.Drawing.Point(190, 130)
    Me.btnSecurityQuestion3Cancel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion3Cancel.Name = "btnSecurityQuestion3Cancel"
    Me.btnSecurityQuestion3Cancel.Size = New System.Drawing.Size(82, 24)
    Me.btnSecurityQuestion3Cancel.TabIndex = 56
    Me.btnSecurityQuestion3Cancel.Text = "Cancel"
    Me.btnSecurityQuestion3Cancel.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion3Change
    '
    Me.btnSecurityQuestion3Change.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnSecurityQuestion3Change.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion3Change.Location = New System.Drawing.Point(282, 130)
    Me.btnSecurityQuestion3Change.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestion3Change.Name = "btnSecurityQuestion3Change"
    Me.btnSecurityQuestion3Change.Size = New System.Drawing.Size(83, 24)
    Me.btnSecurityQuestion3Change.TabIndex = 59
    Me.btnSecurityQuestion3Change.Text = "Change"
    Me.btnSecurityQuestion3Change.UseVisualStyleBackColor = True
    '
    'cboSecurityQuestion2
    '
    Me.cboSecurityQuestion2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cboSecurityQuestion2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion2.FormattingEnabled = True
    Me.cboSecurityQuestion2.Location = New System.Drawing.Point(5, 52)
    Me.cboSecurityQuestion2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.cboSecurityQuestion2.Name = "cboSecurityQuestion2"
    Me.cboSecurityQuestion2.Size = New System.Drawing.Size(175, 25)
    Me.cboSecurityQuestion2.TabIndex = 68
    '
    'cboSecurityQuestion3
    '
    Me.cboSecurityQuestion3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cboSecurityQuestion3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion3.FormattingEnabled = True
    Me.cboSecurityQuestion3.Location = New System.Drawing.Point(5, 104)
    Me.cboSecurityQuestion3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.cboSecurityQuestion3.Name = "cboSecurityQuestion3"
    Me.cboSecurityQuestion3.Size = New System.Drawing.Size(175, 25)
    Me.cboSecurityQuestion3.TabIndex = 70
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.btnViewPIN, 1, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.btnCreateBiometricKey, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btnPasswordHashedUpdate, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btnPIN, 0, 1)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 21)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 2
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(376, 70)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'btnViewPIN
    '
    Me.btnViewPIN.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnViewPIN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnViewPIN.Location = New System.Drawing.Point(193, 40)
    Me.btnViewPIN.Margin = New System.Windows.Forms.Padding(5)
    Me.btnViewPIN.Name = "btnViewPIN"
    Me.btnViewPIN.Size = New System.Drawing.Size(178, 25)
    Me.btnViewPIN.TabIndex = 66
    Me.btnViewPIN.Text = "View PIN"
    Me.btnViewPIN.UseVisualStyleBackColor = True
    '
    'btnCreateBiometricKey
    '
    Me.btnCreateBiometricKey.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnCreateBiometricKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCreateBiometricKey.Location = New System.Drawing.Point(193, 5)
    Me.btnCreateBiometricKey.Margin = New System.Windows.Forms.Padding(5)
    Me.btnCreateBiometricKey.Name = "btnCreateBiometricKey"
    Me.btnCreateBiometricKey.Size = New System.Drawing.Size(178, 25)
    Me.btnCreateBiometricKey.TabIndex = 64
    Me.btnCreateBiometricKey.Text = "Create 'Biometric' Key"
    Me.btnCreateBiometricKey.UseVisualStyleBackColor = True
    '
    'btnPasswordHashedUpdate
    '
    Me.btnPasswordHashedUpdate.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnPasswordHashedUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPasswordHashedUpdate.Location = New System.Drawing.Point(5, 5)
    Me.btnPasswordHashedUpdate.Margin = New System.Windows.Forms.Padding(5)
    Me.btnPasswordHashedUpdate.Name = "btnPasswordHashedUpdate"
    Me.btnPasswordHashedUpdate.Size = New System.Drawing.Size(178, 25)
    Me.btnPasswordHashedUpdate.TabIndex = 63
    Me.btnPasswordHashedUpdate.Text = "Change Password"
    Me.btnPasswordHashedUpdate.UseVisualStyleBackColor = True
    '
    'btnPIN
    '
    Me.btnPIN.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnPIN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPIN.Location = New System.Drawing.Point(5, 40)
    Me.btnPIN.Margin = New System.Windows.Forms.Padding(5)
    Me.btnPIN.Name = "btnPIN"
    Me.btnPIN.Size = New System.Drawing.Size(178, 25)
    Me.btnPIN.TabIndex = 65
    Me.btnPIN.Text = "Create PIN"
    Me.btnPIN.UseVisualStyleBackColor = True
    '
    'gpbDetails
    '
    Me.gpbDetails.Controls.Add(Me.TableLayoutPanel2)
    Me.gpbDetails.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbDetails.Location = New System.Drawing.Point(5, 5)
    Me.gpbDetails.Name = "gpbDetails"
    Me.gpbDetails.Size = New System.Drawing.Size(382, 210)
    Me.gpbDetails.TabIndex = 64
    Me.gpbDetails.TabStop = False
    Me.gpbDetails.Text = "Details"
    '
    'TableLayoutPanel2
    '
    Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel2.ColumnCount = 3
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
    Me.TableLayoutPanel2.Controls.Add(Me.Label2, 2, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.txtType, 0, 7)
    Me.TableLayoutPanel2.Controls.Add(Me.txtLastName, 2, 1)
    Me.TableLayoutPanel2.Controls.Add(Me.lblName, 1, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.txtEmail, 0, 4)
    Me.TableLayoutPanel2.Controls.Add(Me.txtFirstName, 1, 1)
    Me.TableLayoutPanel2.Controls.Add(Me.txtUserName, 0, 1)
    Me.TableLayoutPanel2.Controls.Add(Me.lblUserName, 0, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.lblEmail, 0, 3)
    Me.TableLayoutPanel2.Controls.Add(Me.lblType, 0, 6)
    Me.TableLayoutPanel2.Controls.Add(Me.lblPhoneNo, 2, 3)
    Me.TableLayoutPanel2.Controls.Add(Me.txtPhoneNo, 2, 4)
    Me.TableLayoutPanel2.Controls.Add(Me.lblRoles, 2, 6)
    Me.TableLayoutPanel2.Controls.Add(Me.txtRoles, 2, 7)
    Me.TableLayoutPanel2.Location = New System.Drawing.Point(6, 24)
    Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
    Me.TableLayoutPanel2.RowCount = 9
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
    Me.TableLayoutPanel2.Size = New System.Drawing.Size(361, 187)
    Me.TableLayoutPanel2.TabIndex = 63
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label2.Location = New System.Drawing.Point(245, 0)
    Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(111, 26)
    Me.Label2.TabIndex = 57
    Me.Label2.Text = "Name (F/L)"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtType
    '
    Me.txtType.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtType.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtType.Location = New System.Drawing.Point(5, 156)
    Me.txtType.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtType.Name = "txtType"
    Me.txtType.ReadOnly = True
    Me.txtType.Size = New System.Drawing.Size(110, 25)
    Me.txtType.TabIndex = 51
    Me.txtType.Text = "txtType"
    '
    'txtLastName
    '
    Me.txtLastName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtLastName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtLastName.Location = New System.Drawing.Point(245, 26)
    Me.txtLastName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtLastName.Name = "txtLastName"
    Me.txtLastName.ReadOnly = True
    Me.txtLastName.Size = New System.Drawing.Size(111, 25)
    Me.txtLastName.TabIndex = 55
    Me.txtLastName.Text = "txtLastName"
    '
    'lblName
    '
    Me.lblName.AutoSize = True
    Me.lblName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblName.Location = New System.Drawing.Point(125, 0)
    Me.lblName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblName.Name = "lblName"
    Me.lblName.Size = New System.Drawing.Size(110, 26)
    Me.lblName.TabIndex = 56
    Me.lblName.Text = "Name (F/L)"
    Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtEmail
    '
    Me.txtEmail.BackColor = System.Drawing.Color.PapayaWhip
    Me.TableLayoutPanel2.SetColumnSpan(Me.txtEmail, 2)
    Me.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtEmail.Location = New System.Drawing.Point(5, 91)
    Me.txtEmail.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.ReadOnly = True
    Me.txtEmail.Size = New System.Drawing.Size(230, 25)
    Me.txtEmail.TabIndex = 59
    Me.txtEmail.Text = "txtEmail"
    '
    'txtFirstName
    '
    Me.txtFirstName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtFirstName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtFirstName.Location = New System.Drawing.Point(125, 26)
    Me.txtFirstName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtFirstName.Name = "txtFirstName"
    Me.txtFirstName.ReadOnly = True
    Me.txtFirstName.Size = New System.Drawing.Size(110, 25)
    Me.txtFirstName.TabIndex = 57
    Me.txtFirstName.Text = "txtFirstName"
    '
    'txtUserName
    '
    Me.txtUserName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtUserName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtUserName.Location = New System.Drawing.Point(5, 26)
    Me.txtUserName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.ReadOnly = True
    Me.txtUserName.Size = New System.Drawing.Size(110, 25)
    Me.txtUserName.TabIndex = 61
    Me.txtUserName.Text = "txtUserName"
    '
    'lblUserName
    '
    Me.lblUserName.AutoSize = True
    Me.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblUserName.Location = New System.Drawing.Point(5, 0)
    Me.lblUserName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblUserName.Name = "lblUserName"
    Me.lblUserName.Size = New System.Drawing.Size(110, 26)
    Me.lblUserName.TabIndex = 62
    Me.lblUserName.Text = "User Name"
    Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblEmail
    '
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblEmail.Location = New System.Drawing.Point(5, 65)
    Me.lblEmail.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(110, 26)
    Me.lblEmail.TabIndex = 60
    Me.lblEmail.Text = "Email"
    Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblType
    '
    Me.lblType.AutoSize = True
    Me.lblType.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblType.Location = New System.Drawing.Point(5, 130)
    Me.lblType.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblType.Name = "lblType"
    Me.lblType.Size = New System.Drawing.Size(110, 26)
    Me.lblType.TabIndex = 52
    Me.lblType.Text = "Type"
    Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblPhoneNo
    '
    Me.lblPhoneNo.AutoSize = True
    Me.lblPhoneNo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblPhoneNo.Location = New System.Drawing.Point(245, 65)
    Me.lblPhoneNo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblPhoneNo.Name = "lblPhoneNo"
    Me.lblPhoneNo.Size = New System.Drawing.Size(111, 26)
    Me.lblPhoneNo.TabIndex = 60
    Me.lblPhoneNo.Text = "PhoneNo"
    Me.lblPhoneNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtPhoneNo
    '
    Me.txtPhoneNo.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtPhoneNo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtPhoneNo.Location = New System.Drawing.Point(245, 91)
    Me.txtPhoneNo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtPhoneNo.Name = "txtPhoneNo"
    Me.txtPhoneNo.ReadOnly = True
    Me.txtPhoneNo.Size = New System.Drawing.Size(111, 25)
    Me.txtPhoneNo.TabIndex = 59
    Me.txtPhoneNo.Text = "txtPhoneNo"
    '
    'lblRoles
    '
    Me.lblRoles.AutoSize = True
    Me.lblRoles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblRoles.Location = New System.Drawing.Point(245, 130)
    Me.lblRoles.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.lblRoles.Name = "lblRoles"
    Me.lblRoles.Size = New System.Drawing.Size(111, 26)
    Me.lblRoles.TabIndex = 54
    Me.lblRoles.Text = "Role"
    Me.lblRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtRoles
    '
    Me.txtRoles.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtRoles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtRoles.Location = New System.Drawing.Point(245, 156)
    Me.txtRoles.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
    Me.txtRoles.Name = "txtRoles"
    Me.txtRoles.ReadOnly = True
    Me.txtRoles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtRoles.Size = New System.Drawing.Size(111, 25)
    Me.txtRoles.TabIndex = 53
    Me.txtRoles.Text = "txtRoles"
    '
    'pnlRight
    '
    Me.pnlRight.Controls.Add(Me.gpbSecurity)
    Me.pnlRight.Controls.Add(Me.gpbMessagingMode)
    Me.pnlRight.Controls.Add(Me.btnUserRefresh)
    Me.pnlRight.Controls.Add(Me.gpbUserInterfaceLanguage)
    Me.pnlRight.Controls.Add(Me.gpbChangeLanguage)
    Me.pnlRight.Controls.Add(Me.GroupBox1)
    Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlRight.Location = New System.Drawing.Point(401, 3)
    Me.pnlRight.Name = "pnlRight"
    Me.pnlRight.Padding = New System.Windows.Forms.Padding(5)
    Me.pnlRight.Size = New System.Drawing.Size(393, 510)
    Me.pnlRight.TabIndex = 1
    '
    'gpbSecurity
    '
    Me.gpbSecurity.Controls.Add(Me.dgvLastLogins)
    Me.gpbSecurity.Controls.Add(Me.lblLastLogins)
    Me.gpbSecurity.Controls.Add(Me.lblLastLoginThisApp)
    Me.gpbSecurity.Controls.Add(Me.txtLastLoginThisApp)
    Me.gpbSecurity.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbSecurity.Location = New System.Drawing.Point(5, 305)
    Me.gpbSecurity.Name = "gpbSecurity"
    Me.gpbSecurity.Size = New System.Drawing.Size(383, 166)
    Me.gpbSecurity.TabIndex = 66
    Me.gpbSecurity.TabStop = False
    Me.gpbSecurity.Text = "Last Logins"
    '
    'dgvLastLogins
    '
    Me.dgvLastLogins.AllowUserToAddRows = False
    Me.dgvLastLogins.AllowUserToDeleteRows = False
    Me.dgvLastLogins.AllowUserToOrderColumns = True
    Me.dgvLastLogins.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvLastLogins.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLastLogins.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
    Me.dgvLastLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLastLogins.EnableHeadersVisualStyles = False
    Me.dgvLastLogins.Location = New System.Drawing.Point(78, 62)
    Me.dgvLastLogins.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
    Me.dgvLastLogins.MultiSelect = False
    Me.dgvLastLogins.Name = "dgvLastLogins"
    Me.dgvLastLogins.RowHeadersVisible = False
    Me.dgvLastLogins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLastLogins.Size = New System.Drawing.Size(287, 92)
    Me.dgvLastLogins.TabIndex = 63
    '
    'lblLastLogins
    '
    Me.lblLastLogins.AutoSize = True
    Me.lblLastLogins.Location = New System.Drawing.Point(14, 61)
    Me.lblLastLogins.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblLastLogins.Name = "lblLastLogins"
    Me.lblLastLogins.Size = New System.Drawing.Size(59, 19)
    Me.lblLastLogins.TabIndex = 62
    Me.lblLastLogins.Text = "All Apps"
    '
    'lblLastLoginThisApp
    '
    Me.lblLastLoginThisApp.AutoSize = True
    Me.lblLastLoginThisApp.Location = New System.Drawing.Point(13, 30)
    Me.lblLastLoginThisApp.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblLastLoginThisApp.Name = "lblLastLoginThisApp"
    Me.lblLastLoginThisApp.Size = New System.Drawing.Size(62, 19)
    Me.lblLastLoginThisApp.TabIndex = 62
    Me.lblLastLoginThisApp.Text = "This App"
    '
    'txtLastLoginThisApp
    '
    Me.txtLastLoginThisApp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastLoginThisApp.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtLastLoginThisApp.Location = New System.Drawing.Point(100, 27)
    Me.txtLastLoginThisApp.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastLoginThisApp.Name = "txtLastLoginThisApp"
    Me.txtLastLoginThisApp.ReadOnly = True
    Me.txtLastLoginThisApp.Size = New System.Drawing.Size(263, 25)
    Me.txtLastLoginThisApp.TabIndex = 61
    Me.txtLastLoginThisApp.Text = "txtLastLoginThisApp"
    '
    'gpbMessagingMode
    '
    Me.gpbMessagingMode.Controls.Add(Me.btnMessagingModeChange)
    Me.gpbMessagingMode.Controls.Add(Me.btnMessagingModeCancel)
    Me.gpbMessagingMode.Controls.Add(Me.txtMessagingMode)
    Me.gpbMessagingMode.Controls.Add(Me.cboMessagingMode)
    Me.gpbMessagingMode.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbMessagingMode.Location = New System.Drawing.Point(5, 245)
    Me.gpbMessagingMode.Name = "gpbMessagingMode"
    Me.gpbMessagingMode.Size = New System.Drawing.Size(383, 60)
    Me.gpbMessagingMode.TabIndex = 68
    Me.gpbMessagingMode.TabStop = False
    Me.gpbMessagingMode.Text = "Messaging Mode"
    '
    'btnMessagingModeChange
    '
    Me.btnMessagingModeChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnMessagingModeChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnMessagingModeChange.Location = New System.Drawing.Point(275, 24)
    Me.btnMessagingModeChange.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnMessagingModeChange.Name = "btnMessagingModeChange"
    Me.btnMessagingModeChange.Size = New System.Drawing.Size(75, 26)
    Me.btnMessagingModeChange.TabIndex = 2
    Me.btnMessagingModeChange.Text = "Change"
    Me.btnMessagingModeChange.UseVisualStyleBackColor = True
    '
    'btnMessagingModeCancel
    '
    Me.btnMessagingModeCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnMessagingModeCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnMessagingModeCancel.Location = New System.Drawing.Point(195, 24)
    Me.btnMessagingModeCancel.Margin = New System.Windows.Forms.Padding(5, 15, 15, 0)
    Me.btnMessagingModeCancel.Name = "btnMessagingModeCancel"
    Me.btnMessagingModeCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnMessagingModeCancel.TabIndex = 1
    Me.btnMessagingModeCancel.Text = "Cancel"
    Me.btnMessagingModeCancel.UseVisualStyleBackColor = True
    '
    'txtMessagingMode
    '
    Me.txtMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMessagingMode.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtMessagingMode.Location = New System.Drawing.Point(10, 26)
    Me.txtMessagingMode.Margin = New System.Windows.Forms.Padding(15, 10, 5, 0)
    Me.txtMessagingMode.Name = "txtMessagingMode"
    Me.txtMessagingMode.ReadOnly = True
    Me.txtMessagingMode.Size = New System.Drawing.Size(175, 25)
    Me.txtMessagingMode.TabIndex = 3
    Me.txtMessagingMode.Text = "txtMessagingMode"
    '
    'cboMessagingMode
    '
    Me.cboMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboMessagingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboMessagingMode.FormattingEnabled = True
    Me.cboMessagingMode.Location = New System.Drawing.Point(10, 32)
    Me.cboMessagingMode.Name = "cboMessagingMode"
    Me.cboMessagingMode.Size = New System.Drawing.Size(175, 25)
    Me.cboMessagingMode.TabIndex = 0
    '
    'btnUserRefresh
    '
    Me.btnUserRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUserRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUserRefresh.Location = New System.Drawing.Point(306, 479)
    Me.btnUserRefresh.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnUserRefresh.Name = "btnUserRefresh"
    Me.btnUserRefresh.Size = New System.Drawing.Size(75, 26)
    Me.btnUserRefresh.TabIndex = 63
    Me.btnUserRefresh.Text = "Refresh"
    Me.btnUserRefresh.UseVisualStyleBackColor = True
    '
    'gpbUserInterfaceLanguage
    '
    Me.gpbUserInterfaceLanguage.Controls.Add(Me.lblLTLangExplanation)
    Me.gpbUserInterfaceLanguage.Controls.Add(Me.btnLTLangChange)
    Me.gpbUserInterfaceLanguage.Controls.Add(Me.btnLTLangCancel)
    Me.gpbUserInterfaceLanguage.Controls.Add(Me.txtLTLang)
    Me.gpbUserInterfaceLanguage.Controls.Add(Me.cboLTLang)
    Me.gpbUserInterfaceLanguage.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbUserInterfaceLanguage.Location = New System.Drawing.Point(5, 135)
    Me.gpbUserInterfaceLanguage.Name = "gpbUserInterfaceLanguage"
    Me.gpbUserInterfaceLanguage.Size = New System.Drawing.Size(383, 110)
    Me.gpbUserInterfaceLanguage.TabIndex = 66
    Me.gpbUserInterfaceLanguage.TabStop = False
    Me.gpbUserInterfaceLanguage.Text = "Localized Text Language"
    '
    'lblLTLangExplanation
    '
    Me.lblLTLangExplanation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblLTLangExplanation.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Italic)
    Me.lblLTLangExplanation.ForeColor = System.Drawing.Color.Blue
    Me.lblLTLangExplanation.Location = New System.Drawing.Point(6, 61)
    Me.lblLTLangExplanation.Name = "lblLTLangExplanation"
    Me.lblLTLangExplanation.Size = New System.Drawing.Size(361, 45)
    Me.lblLTLangExplanation.TabIndex = 60
    Me.lblLTLangExplanation.Text = "This sets the language for localized text. Use this only when checking text for a" &
    " language other than the UI"
    '
    'btnLTLangChange
    '
    Me.btnLTLangChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnLTLangChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnLTLangChange.Location = New System.Drawing.Point(275, 32)
    Me.btnLTLangChange.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnLTLangChange.Name = "btnLTLangChange"
    Me.btnLTLangChange.Size = New System.Drawing.Size(75, 26)
    Me.btnLTLangChange.TabIndex = 2
    Me.btnLTLangChange.Text = "Change"
    Me.btnLTLangChange.UseVisualStyleBackColor = True
    '
    'btnLTLangCancel
    '
    Me.btnLTLangCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnLTLangCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnLTLangCancel.Location = New System.Drawing.Point(193, 32)
    Me.btnLTLangCancel.Margin = New System.Windows.Forms.Padding(5, 15, 15, 0)
    Me.btnLTLangCancel.Name = "btnLTLangCancel"
    Me.btnLTLangCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnLTLangCancel.TabIndex = 1
    Me.btnLTLangCancel.Text = "Cancel"
    Me.btnLTLangCancel.UseVisualStyleBackColor = True
    '
    'txtLTLang
    '
    Me.txtLTLang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLTLang.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtLTLang.Location = New System.Drawing.Point(10, 32)
    Me.txtLTLang.Margin = New System.Windows.Forms.Padding(15, 10, 5, 0)
    Me.txtLTLang.Name = "txtLTLang"
    Me.txtLTLang.ReadOnly = True
    Me.txtLTLang.Size = New System.Drawing.Size(175, 25)
    Me.txtLTLang.TabIndex = 3
    Me.txtLTLang.Text = "txtLTLang"
    '
    'cboLTLang
    '
    Me.cboLTLang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLTLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLTLang.FormattingEnabled = True
    Me.cboLTLang.Location = New System.Drawing.Point(10, 32)
    Me.cboLTLang.Name = "cboLTLang"
    Me.cboLTLang.Size = New System.Drawing.Size(175, 25)
    Me.cboLTLang.TabIndex = 0
    '
    'gpbChangeLanguage
    '
    Me.gpbChangeLanguage.Controls.Add(Me.btnUILangCancel)
    Me.gpbChangeLanguage.Controls.Add(Me.btnUILangChange)
    Me.gpbChangeLanguage.Controls.Add(Me.txtUILang)
    Me.gpbChangeLanguage.Controls.Add(Me.cboUILang)
    Me.gpbChangeLanguage.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbChangeLanguage.Location = New System.Drawing.Point(5, 64)
    Me.gpbChangeLanguage.Name = "gpbChangeLanguage"
    Me.gpbChangeLanguage.Size = New System.Drawing.Size(383, 71)
    Me.gpbChangeLanguage.TabIndex = 66
    Me.gpbChangeLanguage.TabStop = False
    Me.gpbChangeLanguage.Text = "Change Your UI Language"
    '
    'btnUILangCancel
    '
    Me.btnUILangCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUILangCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUILangCancel.Location = New System.Drawing.Point(215, 31)
    Me.btnUILangCancel.Margin = New System.Windows.Forms.Padding(5, 15, 15, 0)
    Me.btnUILangCancel.Name = "btnUILangCancel"
    Me.btnUILangCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnUILangCancel.TabIndex = 1
    Me.btnUILangCancel.Text = "Cancel"
    Me.btnUILangCancel.UseVisualStyleBackColor = True
    '
    'btnUILangChange
    '
    Me.btnUILangChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUILangChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUILangChange.Location = New System.Drawing.Point(296, 31)
    Me.btnUILangChange.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnUILangChange.Name = "btnUILangChange"
    Me.btnUILangChange.Size = New System.Drawing.Size(75, 26)
    Me.btnUILangChange.TabIndex = 2
    Me.btnUILangChange.Text = "Change"
    Me.btnUILangChange.UseVisualStyleBackColor = True
    '
    'txtUILang
    '
    Me.txtUILang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUILang.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtUILang.Location = New System.Drawing.Point(15, 31)
    Me.txtUILang.Margin = New System.Windows.Forms.Padding(15, 10, 5, 0)
    Me.txtUILang.Name = "txtUILang"
    Me.txtUILang.ReadOnly = True
    Me.txtUILang.Size = New System.Drawing.Size(190, 25)
    Me.txtUILang.TabIndex = 3
    Me.txtUILang.Text = "txtUILang"
    '
    'cboUILang
    '
    Me.cboUILang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUILang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUILang.FormattingEnabled = True
    Me.cboUILang.Location = New System.Drawing.Point(15, 31)
    Me.cboUILang.Name = "cboUILang"
    Me.cboUILang.Size = New System.Drawing.Size(190, 25)
    Me.cboUILang.TabIndex = 0
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.btnChangeFontSize)
    Me.GroupBox1.Controls.Add(Me.lblFontSize)
    Me.GroupBox1.Controls.Add(Me.cboFontSize)
    Me.GroupBox1.Controls.Add(Me.btnDefaultFontSize)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(383, 59)
    Me.GroupBox1.TabIndex = 67
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Font Size"
    '
    'btnChangeFontSize
    '
    Me.btnChangeFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnChangeFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnChangeFontSize.Location = New System.Drawing.Point(300, 20)
    Me.btnChangeFontSize.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnChangeFontSize.Name = "btnChangeFontSize"
    Me.btnChangeFontSize.Size = New System.Drawing.Size(75, 25)
    Me.btnChangeFontSize.TabIndex = 5
    Me.btnChangeFontSize.Text = "Change"
    Me.btnChangeFontSize.UseVisualStyleBackColor = True
    '
    'lblFontSize
    '
    Me.lblFontSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblFontSize.Location = New System.Drawing.Point(134, 17)
    Me.lblFontSize.Name = "lblFontSize"
    Me.lblFontSize.Size = New System.Drawing.Size(82, 30)
    Me.lblFontSize.TabIndex = 4
    Me.lblFontSize.Text = "fs"
    Me.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cboFontSize
    '
    Me.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboFontSize.FormattingEnabled = True
    Me.cboFontSize.Items.AddRange(New Object() {"6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24"})
    Me.cboFontSize.Location = New System.Drawing.Point(7, 20)
    Me.cboFontSize.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboFontSize.Name = "cboFontSize"
    Me.cboFontSize.Size = New System.Drawing.Size(121, 25)
    Me.cboFontSize.TabIndex = 3
    '
    'btnDefaultFontSize
    '
    Me.btnDefaultFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnDefaultFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDefaultFontSize.Location = New System.Drawing.Point(218, 20)
    Me.btnDefaultFontSize.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnDefaultFontSize.Name = "btnDefaultFontSize"
    Me.btnDefaultFontSize.Size = New System.Drawing.Size(83, 25)
    Me.btnDefaultFontSize.TabIndex = 2
    Me.btnDefaultFontSize.Text = "Default"
    Me.btnDefaultFontSize.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestionsView
    '
    Me.btnSecurityQuestionsView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSecurityQuestionsView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestionsView.Location = New System.Drawing.Point(289, 13)
    Me.btnSecurityQuestionsView.Margin = New System.Windows.Forms.Padding(5, 0, 5, 2)
    Me.btnSecurityQuestionsView.Name = "btnSecurityQuestionsView"
    Me.btnSecurityQuestionsView.Size = New System.Drawing.Size(75, 24)
    Me.btnSecurityQuestionsView.TabIndex = 2
    Me.btnSecurityQuestionsView.Text = "View"
    Me.btnSecurityQuestionsView.UseVisualStyleBackColor = True
    '
    'ctlPnlcsPreferences
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.tbc)
    Me.Controls.Add(Me.gpbHeader)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlPnlcsPreferences"
    Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 3)
    Me.Size = New System.Drawing.Size(821, 616)
    Me.gpbHeader.ResumeLayout(False)
    Me.gpbHeader.PerformLayout()
    Me.tbc.ResumeLayout(False)
    Me.tbpActiveUser.ResumeLayout(False)
    Me.tlp.ResumeLayout(False)
    Me.pnlLeft.ResumeLayout(False)
    Me.gpbChangePassword.ResumeLayout(False)
    Me.gpbSecurityQuestions.ResumeLayout(False)
    Me.TableLayoutPanel3.ResumeLayout(False)
    Me.TableLayoutPanel3.PerformLayout()
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.gpbDetails.ResumeLayout(False)
    Me.TableLayoutPanel2.ResumeLayout(False)
    Me.TableLayoutPanel2.PerformLayout()
    Me.pnlRight.ResumeLayout(False)
    Me.gpbSecurity.ResumeLayout(False)
    Me.gpbSecurity.PerformLayout()
    CType(Me.dgvLastLogins, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gpbMessagingMode.ResumeLayout(False)
    Me.gpbMessagingMode.PerformLayout()
    Me.gpbUserInterfaceLanguage.ResumeLayout(False)
    Me.gpbUserInterfaceLanguage.PerformLayout()
    Me.gpbChangeLanguage.ResumeLayout(False)
    Me.gpbChangeLanguage.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents gpbHeader As System.Windows.Forms.GroupBox
  Friend WithEvents lblTitle As System.Windows.Forms.Label
  Friend WithEvents tbc As System.Windows.Forms.TabControl
  Friend WithEvents tbpActiveUser As System.Windows.Forms.TabPage
  Friend WithEvents txtType As System.Windows.Forms.TextBox
  Friend WithEvents lblType As System.Windows.Forms.Label
  Friend WithEvents txtRoles As System.Windows.Forms.TextBox
  Friend WithEvents lblRoles As System.Windows.Forms.Label
  Friend WithEvents txtLastName As System.Windows.Forms.TextBox
  Friend WithEvents lblName As System.Windows.Forms.Label
  Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
  Friend WithEvents txtEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblEmail As System.Windows.Forms.Label
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents lblUserName As System.Windows.Forms.Label
  Friend WithEvents btnPasswordHashedUpdate As System.Windows.Forms.Button
  Friend WithEvents tlp As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents pnlLeft As System.Windows.Forms.Panel
  Friend WithEvents gpbChangePassword As System.Windows.Forms.GroupBox
  Friend WithEvents gpbDetails As System.Windows.Forms.GroupBox
  Friend WithEvents pnlRight As System.Windows.Forms.Panel
  Friend WithEvents gpbSecurity As System.Windows.Forms.GroupBox
  Friend WithEvents dgvLastLogins As System.Windows.Forms.DataGridView
  Friend WithEvents lblLastLogins As System.Windows.Forms.Label
  Friend WithEvents lblLastLoginThisApp As System.Windows.Forms.Label
  Friend WithEvents txtLastLoginThisApp As System.Windows.Forms.TextBox
  Friend WithEvents btnUserRefresh As System.Windows.Forms.Button
  Friend WithEvents gpbUserInterfaceLanguage As System.Windows.Forms.GroupBox
  Friend WithEvents txtUILang As System.Windows.Forms.TextBox
  Friend WithEvents btnUILangChange As System.Windows.Forms.Button
  Friend WithEvents btnUILangCancel As System.Windows.Forms.Button
  Friend WithEvents cboUILang As System.Windows.Forms.ComboBox
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents btnChangeFontSize As Button
  Friend WithEvents lblFontSize As Label
  Friend WithEvents cboFontSize As ComboBox
  Friend WithEvents btnDefaultFontSize As Button
  Friend WithEvents txtPhoneNo As TextBox
  Friend WithEvents lblPhoneNo As Label
  Friend WithEvents btnLTLangChange As Button
  Friend WithEvents btnLTLangCancel As Button
  Friend WithEvents lblLTLangExplanation As Label
  Friend WithEvents txtLTLang As TextBox
  Friend WithEvents cboLTLang As ComboBox
  Friend WithEvents gpbChangeLanguage As GroupBox
  Friend WithEvents btnCreateBiometricKey As Button
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents btnPIN As Button
  Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
  Friend WithEvents Label2 As Label
  Friend WithEvents gpbMessagingMode As GroupBox
  Friend WithEvents btnMessagingModeChange As Button
  Friend WithEvents btnMessagingModeCancel As Button
  Friend WithEvents txtMessagingMode As TextBox
  Friend WithEvents cboMessagingMode As ComboBox
  Friend WithEvents btnViewPIN As Button
  Friend WithEvents gpbSecurityQuestions As GroupBox
  Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
  Friend WithEvents btnSecurityQuestion1Cancel As Button
  Friend WithEvents btnSecurityQuestion1Change As Button
  Friend WithEvents btnSecurityQuestion2Change As Button
  Friend WithEvents btnSecurityQuestion3Cancel As Button
  Friend WithEvents txtSecurityQuestion1 As TextBox
  Friend WithEvents txtSecurityQuestion2 As TextBox
  Friend WithEvents txtSecurityQuestion3 As TextBox
  Friend WithEvents btnSecurityQuestion2Cancel As Button
  Friend WithEvents btnSecurityQuestion3Change As Button
  Friend WithEvents cboSecurityQuestion1 As ComboBox
  Friend WithEvents cboSecurityQuestion2 As ComboBox
  Friend WithEvents cboSecurityQuestion3 As ComboBox
  Friend WithEvents btnSecurityQuestionsView As Button
End Class
