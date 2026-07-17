'ColourObjectBackground
'ColourObjectReadOnlyTextBackground

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlPnlMaintenance
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
    Me.gpbChangeLanguage = New System.Windows.Forms.GroupBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.btnUILangCancel = New System.Windows.Forms.Button()
    Me.btnUILangChange = New System.Windows.Forms.Button()
    Me.txtUILang = New System.Windows.Forms.TextBox()
    Me.cboUILang = New System.Windows.Forms.ComboBox()
    Me.gpbChangePassword = New System.Windows.Forms.GroupBox()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.btnCreateBiometricKey = New System.Windows.Forms.Button()
    Me.btnPasswordHashedUpdate = New System.Windows.Forms.Button()
    Me.btnPIN = New System.Windows.Forms.Button()
    Me.gpbDetails = New System.Windows.Forms.GroupBox()
    Me.lblLastName = New System.Windows.Forms.Label()
    Me.lblRoles = New System.Windows.Forms.Label()
    Me.txtLastName = New System.Windows.Forms.TextBox()
    Me.txtRoles = New System.Windows.Forms.TextBox()
    Me.lblType = New System.Windows.Forms.Label()
    Me.txtFirstName = New System.Windows.Forms.TextBox()
    Me.txtType = New System.Windows.Forms.TextBox()
    Me.lblFirstName = New System.Windows.Forms.Label()
    Me.txtPhoneNo = New System.Windows.Forms.TextBox()
    Me.lblPhoneNo = New System.Windows.Forms.Label()
    Me.txtEmail = New System.Windows.Forms.TextBox()
    Me.lblEmail = New System.Windows.Forms.Label()
    Me.lblUserName = New System.Windows.Forms.Label()
    Me.txtUserName = New System.Windows.Forms.TextBox()
    Me.pnlRight = New System.Windows.Forms.Panel()
    Me.txtPasswordHashed = New System.Windows.Forms.TextBox()
    Me.lblPasswordHashed = New System.Windows.Forms.Label()
    Me.gpbSecurity = New System.Windows.Forms.GroupBox()
    Me.dgvLastLogins = New System.Windows.Forms.DataGridView()
    Me.lblLastLogins = New System.Windows.Forms.Label()
    Me.lblLastLoginThisApp = New System.Windows.Forms.Label()
    Me.txtLastLoginThisApp = New System.Windows.Forms.TextBox()
    Me.btnUserRefresh = New System.Windows.Forms.Button()
    Me.gpbUserInterfaceLanguage = New System.Windows.Forms.GroupBox()
    Me.lblLTLangExplanation = New System.Windows.Forms.Label()
    Me.btnLTLangChange = New System.Windows.Forms.Button()
    Me.btnLTLangCancel = New System.Windows.Forms.Button()
    Me.txtLTLang = New System.Windows.Forms.TextBox()
    Me.cboLTLang = New System.Windows.Forms.ComboBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.btnChangeFontSize = New System.Windows.Forms.Button()
    Me.lblFontSize = New System.Windows.Forms.Label()
    Me.cboFontSize = New System.Windows.Forms.ComboBox()
    Me.btnDefaultFontSize = New System.Windows.Forms.Button()
    Me.tbpDatabase = New System.Windows.Forms.TabPage()
    Me.gpbDatabaseStatus = New System.Windows.Forms.GroupBox()
    Me.txtDatabaseFileSizes = New System.Windows.Forms.TextBox()
    Me.pnlDatabaseStatus = New System.Windows.Forms.Panel()
    Me.btnDatabaseFileSizes = New System.Windows.Forms.Button()
    Me.btnTableSizes = New System.Windows.Forms.Button()
    Me.btnIndexFragmentation = New System.Windows.Forms.Button()
    Me.gbpDBMaintenance = New System.Windows.Forms.GroupBox()
    Me.tblDBMaintenance = New System.Windows.Forms.TableLayoutPanel()
    Me.pnlDBMaintenanceL = New System.Windows.Forms.Panel()
    Me.btnResetPermissionsForDefaultRoles = New System.Windows.Forms.Button()
    Me.btnEjectNonMasterUsersOnly = New System.Windows.Forms.Button()
    Me.btnBackupDatabase = New System.Windows.Forms.Button()
    Me.btnEjectAllUsers = New System.Windows.Forms.Button()
    Me.btnRequestIndexReorganization = New System.Windows.Forms.Button()
    Me.pnlDBMaintenanceR = New System.Windows.Forms.Panel()
    Me.gpbSysAdmin = New System.Windows.Forms.GroupBox()
    Me.btnRunScriptOnServer = New System.Windows.Forms.Button()
    Me.btnEnableCLR = New System.Windows.Forms.Button()
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate = New System.Windows.Forms.Button()
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate = New System.Windows.Forms.Button()
    Me.gpbCreateBinaryFilesOnServer = New System.Windows.Forms.GroupBox()
    Me.rbtnOneFilePerTable = New System.Windows.Forms.RadioButton()
    Me.rbtnOneFileForDatabase = New System.Windows.Forms.RadioButton()
    Me.btnCreateBinaryFilesOnServer = New System.Windows.Forms.Button()
    Me.grdIndexFragmentation = New ctlc_IndexFragmentationCol()
    Me.grdTableSizes = New ctlc_TableSizeCol()
    Me.gpbHeader.SuspendLayout()
    Me.tbc.SuspendLayout()
    Me.tbpActiveUser.SuspendLayout()
    Me.tlp.SuspendLayout()
    Me.pnlLeft.SuspendLayout()
    Me.gpbChangeLanguage.SuspendLayout()
    Me.gpbChangePassword.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.gpbDetails.SuspendLayout()
    Me.pnlRight.SuspendLayout()
    Me.gpbSecurity.SuspendLayout()
    CType(Me.dgvLastLogins, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gpbUserInterfaceLanguage.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.tbpDatabase.SuspendLayout()
    Me.gpbDatabaseStatus.SuspendLayout()
    Me.pnlDatabaseStatus.SuspendLayout()
    Me.gbpDBMaintenance.SuspendLayout()
    Me.tblDBMaintenance.SuspendLayout()
    Me.pnlDBMaintenanceL.SuspendLayout()
    Me.pnlDBMaintenanceR.SuspendLayout()
    Me.gpbSysAdmin.SuspendLayout()
    Me.gpbCreateBinaryFilesOnServer.SuspendLayout()
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
    Me.lblTitle.Size = New System.Drawing.Size(126, 31)
    Me.lblTitle.TabIndex = 0
    Me.lblTitle.Text = "My Settings"
    '
    'tbc
    '
    Me.tbc.Controls.Add(Me.tbpActiveUser)
    Me.tbc.Controls.Add(Me.tbpDatabase)
    Me.tbc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tbc.Location = New System.Drawing.Point(5, 61)
    Me.tbc.Name = "tbc"
    Me.tbc.SelectedIndex = 0
    Me.tbc.Size = New System.Drawing.Size(811, 608)
    Me.tbc.TabIndex = 1
    '
    'tbpActiveUser
    '
    Me.tbpActiveUser.BackColor = System.Drawing.Color.Wheat
    Me.tbpActiveUser.Controls.Add(Me.tlp)
    Me.tbpActiveUser.Location = New System.Drawing.Point(4, 26)
    Me.tbpActiveUser.Name = "tbpActiveUser"
    Me.tbpActiveUser.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpActiveUser.Size = New System.Drawing.Size(803, 578)
    Me.tbpActiveUser.TabIndex = 0
    Me.tbpActiveUser.Text = "Active User"
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
    Me.tlp.Size = New System.Drawing.Size(797, 572)
    Me.tlp.TabIndex = 67
    Me.tlp.Visible = False
    '
    'pnlLeft
    '
    Me.pnlLeft.Controls.Add(Me.gpbChangeLanguage)
    Me.pnlLeft.Controls.Add(Me.gpbChangePassword)
    Me.pnlLeft.Controls.Add(Me.gpbDetails)
    Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlLeft.Location = New System.Drawing.Point(3, 3)
    Me.pnlLeft.Name = "pnlLeft"
    Me.pnlLeft.Padding = New System.Windows.Forms.Padding(5)
    Me.pnlLeft.Size = New System.Drawing.Size(392, 566)
    Me.pnlLeft.TabIndex = 0
    '
    'gpbChangeLanguage
    '
    Me.gpbChangeLanguage.Controls.Add(Me.Label1)
    Me.gpbChangeLanguage.Controls.Add(Me.btnUILangCancel)
    Me.gpbChangeLanguage.Controls.Add(Me.btnUILangChange)
    Me.gpbChangeLanguage.Controls.Add(Me.txtUILang)
    Me.gpbChangeLanguage.Controls.Add(Me.cboUILang)
    Me.gpbChangeLanguage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbChangeLanguage.Location = New System.Drawing.Point(5, 444)
    Me.gpbChangeLanguage.Name = "gpbChangeLanguage"
    Me.gpbChangeLanguage.Size = New System.Drawing.Size(382, 117)
    Me.gpbChangeLanguage.TabIndex = 66
    Me.gpbChangeLanguage.TabStop = False
    Me.gpbChangeLanguage.Text = "Change Your UI Language"
    '
    'Label1
    '
    Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Italic)
    Me.Label1.ForeColor = System.Drawing.Color.Blue
    Me.Label1.Location = New System.Drawing.Point(11, 69)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(361, 43)
    Me.Label1.TabIndex = 61
    Me.Label1.Text = "This changes your preferred language in the system, and also that of the UI."
    '
    'btnUILangCancel
    '
    Me.btnUILangCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUILangCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUILangCancel.Location = New System.Drawing.Point(214, 31)
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
    Me.btnUILangChange.Location = New System.Drawing.Point(295, 31)
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
    Me.txtUILang.Size = New System.Drawing.Size(189, 25)
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
    Me.cboUILang.Size = New System.Drawing.Size(189, 25)
    Me.cboUILang.TabIndex = 0
    '
    'gpbChangePassword
    '
    Me.gpbChangePassword.Controls.Add(Me.TableLayoutPanel1)
    Me.gpbChangePassword.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbChangePassword.Location = New System.Drawing.Point(5, 320)
    Me.gpbChangePassword.Name = "gpbChangePassword"
    Me.gpbChangePassword.Size = New System.Drawing.Size(382, 124)
    Me.gpbChangePassword.TabIndex = 65
    Me.gpbChangePassword.TabStop = False
    Me.gpbChangePassword.Text = "Security"
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.btnCreateBiometricKey, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btnPasswordHashedUpdate, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btnPIN, 1, 1)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 21)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 2
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(376, 100)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'btnCreateBiometricKey
    '
    Me.btnCreateBiometricKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCreateBiometricKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCreateBiometricKey.Location = New System.Drawing.Point(203, 15)
    Me.btnCreateBiometricKey.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnCreateBiometricKey.Name = "btnCreateBiometricKey"
    Me.btnCreateBiometricKey.Size = New System.Drawing.Size(158, 26)
    Me.btnCreateBiometricKey.TabIndex = 64
    Me.btnCreateBiometricKey.Text = "Create 'Biometric' Key"
    Me.btnCreateBiometricKey.UseVisualStyleBackColor = True
    '
    'btnPasswordHashedUpdate
    '
    Me.btnPasswordHashedUpdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnPasswordHashedUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPasswordHashedUpdate.Location = New System.Drawing.Point(15, 15)
    Me.btnPasswordHashedUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnPasswordHashedUpdate.Name = "btnPasswordHashedUpdate"
    Me.btnPasswordHashedUpdate.Size = New System.Drawing.Size(158, 26)
    Me.btnPasswordHashedUpdate.TabIndex = 63
    Me.btnPasswordHashedUpdate.Text = "Change Password"
    Me.btnPasswordHashedUpdate.UseVisualStyleBackColor = True
    '
    'btnPIN
    '
    Me.btnPIN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnPIN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPIN.Location = New System.Drawing.Point(203, 62)
    Me.btnPIN.Margin = New System.Windows.Forms.Padding(15, 12, 15, 0)
    Me.btnPIN.Name = "btnPIN"
    Me.btnPIN.Size = New System.Drawing.Size(158, 26)
    Me.btnPIN.TabIndex = 65
    Me.btnPIN.Text = "Create PIN"
    Me.btnPIN.UseVisualStyleBackColor = True
    '
    'gpbDetails
    '
    Me.gpbDetails.Controls.Add(Me.lblLastName)
    Me.gpbDetails.Controls.Add(Me.lblRoles)
    Me.gpbDetails.Controls.Add(Me.txtLastName)
    Me.gpbDetails.Controls.Add(Me.txtRoles)
    Me.gpbDetails.Controls.Add(Me.lblType)
    Me.gpbDetails.Controls.Add(Me.txtFirstName)
    Me.gpbDetails.Controls.Add(Me.txtType)
    Me.gpbDetails.Controls.Add(Me.lblFirstName)
    Me.gpbDetails.Controls.Add(Me.txtPhoneNo)
    Me.gpbDetails.Controls.Add(Me.lblPhoneNo)
    Me.gpbDetails.Controls.Add(Me.txtEmail)
    Me.gpbDetails.Controls.Add(Me.lblEmail)
    Me.gpbDetails.Controls.Add(Me.lblUserName)
    Me.gpbDetails.Controls.Add(Me.txtUserName)
    Me.gpbDetails.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbDetails.Location = New System.Drawing.Point(5, 5)
    Me.gpbDetails.Name = "gpbDetails"
    Me.gpbDetails.Size = New System.Drawing.Size(382, 315)
    Me.gpbDetails.TabIndex = 64
    Me.gpbDetails.TabStop = False
    Me.gpbDetails.Text = "Details"
    '
    'lblLastName
    '
    Me.lblLastName.AutoSize = True
    Me.lblLastName.Location = New System.Drawing.Point(13, 84)
    Me.lblLastName.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblLastName.Name = "lblLastName"
    Me.lblLastName.Size = New System.Drawing.Size(74, 19)
    Me.lblLastName.TabIndex = 56
    Me.lblLastName.Text = "Last Name"
    '
    'lblRoles
    '
    Me.lblRoles.AutoSize = True
    Me.lblRoles.Location = New System.Drawing.Point(13, 209)
    Me.lblRoles.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblRoles.Name = "lblRoles"
    Me.lblRoles.Size = New System.Drawing.Size(35, 19)
    Me.lblRoles.TabIndex = 54
    Me.lblRoles.Text = "Role"
    '
    'txtLastName
    '
    Me.txtLastName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtLastName.Location = New System.Drawing.Point(165, 76)
    Me.txtLastName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastName.Name = "txtLastName"
    Me.txtLastName.ReadOnly = True
    Me.txtLastName.Size = New System.Drawing.Size(199, 25)
    Me.txtLastName.TabIndex = 55
    Me.txtLastName.Text = "txtLastName"
    '
    'txtRoles
    '
    Me.txtRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRoles.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtRoles.Location = New System.Drawing.Point(165, 196)
    Me.txtRoles.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtRoles.Name = "txtRoles"
    Me.txtRoles.ReadOnly = True
    Me.txtRoles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtRoles.Size = New System.Drawing.Size(199, 25)
    Me.txtRoles.TabIndex = 53
    Me.txtRoles.Text = "txtRoles"
    '
    'lblType
    '
    Me.lblType.AutoSize = True
    Me.lblType.Location = New System.Drawing.Point(13, 169)
    Me.lblType.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblType.Name = "lblType"
    Me.lblType.Size = New System.Drawing.Size(37, 19)
    Me.lblType.TabIndex = 52
    Me.lblType.Text = "Type"
    '
    'txtFirstName
    '
    Me.txtFirstName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFirstName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtFirstName.Location = New System.Drawing.Point(165, 116)
    Me.txtFirstName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtFirstName.Name = "txtFirstName"
    Me.txtFirstName.ReadOnly = True
    Me.txtFirstName.Size = New System.Drawing.Size(199, 25)
    Me.txtFirstName.TabIndex = 57
    Me.txtFirstName.Text = "txtFirstName"
    '
    'txtType
    '
    Me.txtType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtType.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtType.Location = New System.Drawing.Point(165, 156)
    Me.txtType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtType.Name = "txtType"
    Me.txtType.ReadOnly = True
    Me.txtType.Size = New System.Drawing.Size(199, 25)
    Me.txtType.TabIndex = 51
    Me.txtType.Text = "txtType"
    '
    'lblFirstName
    '
    Me.lblFirstName.AutoSize = True
    Me.lblFirstName.Location = New System.Drawing.Point(13, 124)
    Me.lblFirstName.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblFirstName.Name = "lblFirstName"
    Me.lblFirstName.Size = New System.Drawing.Size(75, 19)
    Me.lblFirstName.TabIndex = 58
    Me.lblFirstName.Text = "First Name"
    '
    'txtPhoneNo
    '
    Me.txtPhoneNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPhoneNo.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtPhoneNo.Location = New System.Drawing.Point(168, 276)
    Me.txtPhoneNo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPhoneNo.Name = "txtPhoneNo"
    Me.txtPhoneNo.ReadOnly = True
    Me.txtPhoneNo.Size = New System.Drawing.Size(199, 25)
    Me.txtPhoneNo.TabIndex = 59
    Me.txtPhoneNo.Text = "txtPhoneNo"
    '
    'lblPhoneNo
    '
    Me.lblPhoneNo.AutoSize = True
    Me.lblPhoneNo.Location = New System.Drawing.Point(13, 279)
    Me.lblPhoneNo.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblPhoneNo.Name = "lblPhoneNo"
    Me.lblPhoneNo.Size = New System.Drawing.Size(66, 19)
    Me.lblPhoneNo.TabIndex = 60
    Me.lblPhoneNo.Text = "PhoneNo"
    '
    'txtEmail
    '
    Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmail.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtEmail.Location = New System.Drawing.Point(168, 236)
    Me.txtEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.ReadOnly = True
    Me.txtEmail.Size = New System.Drawing.Size(199, 25)
    Me.txtEmail.TabIndex = 59
    Me.txtEmail.Text = "txtEmail"
    '
    'lblEmail
    '
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Location = New System.Drawing.Point(13, 254)
    Me.lblEmail.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(41, 19)
    Me.lblEmail.TabIndex = 60
    Me.lblEmail.Text = "Email"
    '
    'lblUserName
    '
    Me.lblUserName.AutoSize = True
    Me.lblUserName.Location = New System.Drawing.Point(13, 52)
    Me.lblUserName.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblUserName.Name = "lblUserName"
    Me.lblUserName.Size = New System.Drawing.Size(77, 19)
    Me.lblUserName.TabIndex = 62
    Me.lblUserName.Text = "User Name"
    '
    'txtUserName
    '
    Me.txtUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUserName.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtUserName.Location = New System.Drawing.Point(165, 36)
    Me.txtUserName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.ReadOnly = True
    Me.txtUserName.Size = New System.Drawing.Size(199, 25)
    Me.txtUserName.TabIndex = 61
    Me.txtUserName.Text = "txtUserName"
    '
    'pnlRight
    '
    Me.pnlRight.Controls.Add(Me.txtPasswordHashed)
    Me.pnlRight.Controls.Add(Me.lblPasswordHashed)
    Me.pnlRight.Controls.Add(Me.gpbSecurity)
    Me.pnlRight.Controls.Add(Me.btnUserRefresh)
    Me.pnlRight.Controls.Add(Me.gpbUserInterfaceLanguage)
    Me.pnlRight.Controls.Add(Me.GroupBox1)
    Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlRight.Location = New System.Drawing.Point(401, 3)
    Me.pnlRight.Name = "pnlRight"
    Me.pnlRight.Padding = New System.Windows.Forms.Padding(5)
    Me.pnlRight.Size = New System.Drawing.Size(393, 566)
    Me.pnlRight.TabIndex = 1
    '
    'txtPasswordHashed
    '
    Me.txtPasswordHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPasswordHashed.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtPasswordHashed.Location = New System.Drawing.Point(139, 530)
    Me.txtPasswordHashed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPasswordHashed.Name = "txtPasswordHashed"
    Me.txtPasswordHashed.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPasswordHashed.ReadOnly = True
    Me.txtPasswordHashed.Size = New System.Drawing.Size(53, 25)
    Me.txtPasswordHashed.TabIndex = 49
    Me.txtPasswordHashed.Text = "txtPasswordHashed"
    Me.txtPasswordHashed.UseSystemPasswordChar = True
    '
    'lblPasswordHashed
    '
    Me.lblPasswordHashed.AutoSize = True
    Me.lblPasswordHashed.Location = New System.Drawing.Point(65, 533)
    Me.lblPasswordHashed.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblPasswordHashed.Name = "lblPasswordHashed"
    Me.lblPasswordHashed.Size = New System.Drawing.Size(67, 19)
    Me.lblPasswordHashed.TabIndex = 50
    Me.lblPasswordHashed.Text = "Password"
    '
    'gpbSecurity
    '
    Me.gpbSecurity.Controls.Add(Me.dgvLastLogins)
    Me.gpbSecurity.Controls.Add(Me.lblLastLogins)
    Me.gpbSecurity.Controls.Add(Me.lblLastLoginThisApp)
    Me.gpbSecurity.Controls.Add(Me.txtLastLoginThisApp)
    Me.gpbSecurity.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbSecurity.Location = New System.Drawing.Point(5, 233)
    Me.gpbSecurity.Name = "gpbSecurity"
    Me.gpbSecurity.Size = New System.Drawing.Size(383, 279)
    Me.gpbSecurity.TabIndex = 66
    Me.gpbSecurity.TabStop = False
    Me.gpbSecurity.Text = "Security"
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
    Me.dgvLastLogins.Location = New System.Drawing.Point(18, 97)
    Me.dgvLastLogins.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
    Me.dgvLastLogins.MultiSelect = False
    Me.dgvLastLogins.Name = "dgvLastLogins"
    Me.dgvLastLogins.RowHeadersVisible = False
    Me.dgvLastLogins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLastLogins.Size = New System.Drawing.Size(347, 160)
    Me.dgvLastLogins.TabIndex = 63
    '
    'lblLastLogins
    '
    Me.lblLastLogins.AutoSize = True
    Me.lblLastLogins.Location = New System.Drawing.Point(14, 68)
    Me.lblLastLogins.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblLastLogins.Name = "lblLastLogins"
    Me.lblLastLogins.Size = New System.Drawing.Size(138, 19)
    Me.lblLastLogins.TabIndex = 62
    Me.lblLastLogins.Text = "Last Logins (all Apps)"
    '
    'lblLastLoginThisApp
    '
    Me.lblLastLoginThisApp.AutoSize = True
    Me.lblLastLoginThisApp.Location = New System.Drawing.Point(13, 39)
    Me.lblLastLoginThisApp.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblLastLoginThisApp.Name = "lblLastLoginThisApp"
    Me.lblLastLoginThisApp.Size = New System.Drawing.Size(135, 19)
    Me.lblLastLoginThisApp.TabIndex = 62
    Me.lblLastLoginThisApp.Text = "Last Login (this App)"
    '
    'txtLastLoginThisApp
    '
    Me.txtLastLoginThisApp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastLoginThisApp.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtLastLoginThisApp.Location = New System.Drawing.Point(166, 36)
    Me.txtLastLoginThisApp.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastLoginThisApp.Name = "txtLastLoginThisApp"
    Me.txtLastLoginThisApp.ReadOnly = True
    Me.txtLastLoginThisApp.Size = New System.Drawing.Size(199, 25)
    Me.txtLastLoginThisApp.TabIndex = 61
    Me.txtLastLoginThisApp.Text = "txtLastLoginThisApp"
    '
    'btnUserRefresh
    '
    Me.btnUserRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUserRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUserRefresh.Location = New System.Drawing.Point(307, 530)
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
    Me.gpbUserInterfaceLanguage.Location = New System.Drawing.Point(5, 91)
    Me.gpbUserInterfaceLanguage.Name = "gpbUserInterfaceLanguage"
    Me.gpbUserInterfaceLanguage.Size = New System.Drawing.Size(383, 142)
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
    Me.lblLTLangExplanation.Size = New System.Drawing.Size(361, 66)
    Me.lblLTLangExplanation.TabIndex = 60
    Me.lblLTLangExplanation.Text = "This sets the language for localized text. Use this only when checking or updatin" &
"g text for a localized language other than the user interface language"
    '
    'btnLTLangChange
    '
    Me.btnLTLangChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnLTLangChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnLTLangChange.Location = New System.Drawing.Point(275, 31)
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
    Me.btnLTLangCancel.Location = New System.Drawing.Point(195, 31)
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
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.btnChangeFontSize)
    Me.GroupBox1.Controls.Add(Me.lblFontSize)
    Me.GroupBox1.Controls.Add(Me.cboFontSize)
    Me.GroupBox1.Controls.Add(Me.btnDefaultFontSize)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(383, 86)
    Me.GroupBox1.TabIndex = 67
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Font Size"
    '
    'btnChangeFontSize
    '
    Me.btnChangeFontSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnChangeFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnChangeFontSize.Location = New System.Drawing.Point(300, 19)
    Me.btnChangeFontSize.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnChangeFontSize.Name = "btnChangeFontSize"
    Me.btnChangeFontSize.Size = New System.Drawing.Size(75, 51)
    Me.btnChangeFontSize.TabIndex = 5
    Me.btnChangeFontSize.Text = "Change Font Size"
    Me.btnChangeFontSize.UseVisualStyleBackColor = True
    '
    'lblFontSize
    '
    Me.lblFontSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblFontSize.Location = New System.Drawing.Point(134, 20)
    Me.lblFontSize.Name = "lblFontSize"
    Me.lblFontSize.Size = New System.Drawing.Size(160, 50)
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
    Me.btnDefaultFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDefaultFontSize.Location = New System.Drawing.Point(7, 47)
    Me.btnDefaultFontSize.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnDefaultFontSize.Name = "btnDefaultFontSize"
    Me.btnDefaultFontSize.Size = New System.Drawing.Size(121, 25)
    Me.btnDefaultFontSize.TabIndex = 2
    Me.btnDefaultFontSize.Text = "Default"
    Me.btnDefaultFontSize.UseVisualStyleBackColor = True
    '
    'tbpDatabase
    '
    Me.tbpDatabase.BackColor = System.Drawing.Color.Wheat
    Me.tbpDatabase.Controls.Add(Me.gpbDatabaseStatus)
    Me.tbpDatabase.Controls.Add(Me.gbpDBMaintenance)
    Me.tbpDatabase.Controls.Add(Me.gpbCreateBinaryFilesOnServer)
    Me.tbpDatabase.Location = New System.Drawing.Point(4, 26)
    Me.tbpDatabase.Name = "tbpDatabase"
    Me.tbpDatabase.Padding = New System.Windows.Forms.Padding(5)
    Me.tbpDatabase.Size = New System.Drawing.Size(803, 578)
    Me.tbpDatabase.TabIndex = 1
    Me.tbpDatabase.Text = "Database"
    '
    'gpbDatabaseStatus
    '
    Me.gpbDatabaseStatus.BackColor = System.Drawing.Color.Wheat
    Me.gpbDatabaseStatus.Controls.Add(Me.txtDatabaseFileSizes)
    Me.gpbDatabaseStatus.Controls.Add(Me.grdIndexFragmentation)
    Me.gpbDatabaseStatus.Controls.Add(Me.grdTableSizes)
    Me.gpbDatabaseStatus.Controls.Add(Me.pnlDatabaseStatus)
    Me.gpbDatabaseStatus.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbDatabaseStatus.Location = New System.Drawing.Point(5, 88)
    Me.gpbDatabaseStatus.Name = "gpbDatabaseStatus"
    Me.gpbDatabaseStatus.Size = New System.Drawing.Size(793, 239)
    Me.gpbDatabaseStatus.TabIndex = 3
    Me.gpbDatabaseStatus.TabStop = False
    Me.gpbDatabaseStatus.Text = "Database Status"
    '
    'txtDatabaseFileSizes
    '
    Me.txtDatabaseFileSizes.BackColor = System.Drawing.Color.PapayaWhip
    Me.txtDatabaseFileSizes.Location = New System.Drawing.Point(362, 20)
    Me.txtDatabaseFileSizes.Multiline = True
    Me.txtDatabaseFileSizes.Name = "txtDatabaseFileSizes"
    Me.txtDatabaseFileSizes.ReadOnly = True
    Me.txtDatabaseFileSizes.Size = New System.Drawing.Size(200, 117)
    Me.txtDatabaseFileSizes.TabIndex = 5
    '
    'pnlDatabaseStatus
    '
    Me.pnlDatabaseStatus.Controls.Add(Me.btnDatabaseFileSizes)
    Me.pnlDatabaseStatus.Controls.Add(Me.btnTableSizes)
    Me.pnlDatabaseStatus.Controls.Add(Me.btnIndexFragmentation)
    Me.pnlDatabaseStatus.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnlDatabaseStatus.Location = New System.Drawing.Point(3, 205)
    Me.pnlDatabaseStatus.Name = "pnlDatabaseStatus"
    Me.pnlDatabaseStatus.Size = New System.Drawing.Size(787, 31)
    Me.pnlDatabaseStatus.TabIndex = 3
    '
    'btnDatabaseFileSizes
    '
    Me.btnDatabaseFileSizes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnDatabaseFileSizes.Location = New System.Drawing.Point(265, 5)
    Me.btnDatabaseFileSizes.Name = "btnDatabaseFileSizes"
    Me.btnDatabaseFileSizes.Size = New System.Drawing.Size(125, 27)
    Me.btnDatabaseFileSizes.TabIndex = 2
    Me.btnDatabaseFileSizes.Text = "Database File Sizes"
    Me.btnDatabaseFileSizes.UseVisualStyleBackColor = True
    '
    'btnTableSizes
    '
    Me.btnTableSizes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnTableSizes.Location = New System.Drawing.Point(3, 5)
    Me.btnTableSizes.Name = "btnTableSizes"
    Me.btnTableSizes.Size = New System.Drawing.Size(125, 27)
    Me.btnTableSizes.TabIndex = 0
    Me.btnTableSizes.Text = "Table Sizes"
    Me.btnTableSizes.UseVisualStyleBackColor = True
    '
    'btnIndexFragmentation
    '
    Me.btnIndexFragmentation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnIndexFragmentation.Location = New System.Drawing.Point(134, 5)
    Me.btnIndexFragmentation.Name = "btnIndexFragmentation"
    Me.btnIndexFragmentation.Size = New System.Drawing.Size(125, 27)
    Me.btnIndexFragmentation.TabIndex = 1
    Me.btnIndexFragmentation.Text = "Index Fragmentation"
    Me.btnIndexFragmentation.UseVisualStyleBackColor = True
    '
    'gbpDBMaintenance
    '
    Me.gbpDBMaintenance.Controls.Add(Me.tblDBMaintenance)
    Me.gbpDBMaintenance.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.gbpDBMaintenance.Location = New System.Drawing.Point(5, 327)
    Me.gbpDBMaintenance.Name = "gbpDBMaintenance"
    Me.gbpDBMaintenance.Size = New System.Drawing.Size(793, 250)
    Me.gbpDBMaintenance.TabIndex = 2
    Me.gbpDBMaintenance.TabStop = False
    Me.gbpDBMaintenance.Text = "Database Maintenance"
    '
    'tblDBMaintenance
    '
    Me.tblDBMaintenance.ColumnCount = 2
    Me.tblDBMaintenance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.Controls.Add(Me.pnlDBMaintenanceL, 0, 0)
    Me.tblDBMaintenance.Controls.Add(Me.pnlDBMaintenanceR, 1, 0)
    Me.tblDBMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tblDBMaintenance.Location = New System.Drawing.Point(3, 21)
    Me.tblDBMaintenance.Name = "tblDBMaintenance"
    Me.tblDBMaintenance.RowCount = 1
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 226.0!))
    Me.tblDBMaintenance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 226.0!))
    Me.tblDBMaintenance.Size = New System.Drawing.Size(787, 226)
    Me.tblDBMaintenance.TabIndex = 5
    '
    'pnlDBMaintenanceL
    '
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnResetPermissionsForDefaultRoles)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnEjectNonMasterUsersOnly)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnBackupDatabase)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnEjectAllUsers)
    Me.pnlDBMaintenanceL.Controls.Add(Me.btnRequestIndexReorganization)
    Me.pnlDBMaintenanceL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlDBMaintenanceL.Location = New System.Drawing.Point(3, 3)
    Me.pnlDBMaintenanceL.Name = "pnlDBMaintenanceL"
    Me.pnlDBMaintenanceL.Size = New System.Drawing.Size(387, 220)
    Me.pnlDBMaintenanceL.TabIndex = 0
    '
    'btnResetPermissionsForDefaultRoles
    '
    Me.btnResetPermissionsForDefaultRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnResetPermissionsForDefaultRoles.Location = New System.Drawing.Point(15, 155)
    Me.btnResetPermissionsForDefaultRoles.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnResetPermissionsForDefaultRoles.Name = "btnResetPermissionsForDefaultRoles"
    Me.btnResetPermissionsForDefaultRoles.Size = New System.Drawing.Size(357, 27)
    Me.btnResetPermissionsForDefaultRoles.TabIndex = 9
    Me.btnResetPermissionsForDefaultRoles.Text = "Reset Permissions for Default Roles"
    Me.btnResetPermissionsForDefaultRoles.UseVisualStyleBackColor = True
    '
    'btnEjectNonMasterUsersOnly
    '
    Me.btnEjectNonMasterUsersOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEjectNonMasterUsersOnly.Location = New System.Drawing.Point(15, 50)
    Me.btnEjectNonMasterUsersOnly.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEjectNonMasterUsersOnly.Name = "btnEjectNonMasterUsersOnly"
    Me.btnEjectNonMasterUsersOnly.Size = New System.Drawing.Size(357, 27)
    Me.btnEjectNonMasterUsersOnly.TabIndex = 2
    Me.btnEjectNonMasterUsersOnly.Text = "Eject Non Master Users Only"
    Me.btnEjectNonMasterUsersOnly.UseVisualStyleBackColor = True
    '
    'btnBackupDatabase
    '
    Me.btnBackupDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnBackupDatabase.Location = New System.Drawing.Point(15, 120)
    Me.btnBackupDatabase.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnBackupDatabase.Name = "btnBackupDatabase"
    Me.btnBackupDatabase.Size = New System.Drawing.Size(357, 27)
    Me.btnBackupDatabase.TabIndex = 4
    Me.btnBackupDatabase.Text = "Backup Database"
    Me.btnBackupDatabase.UseVisualStyleBackColor = True
    '
    'btnEjectAllUsers
    '
    Me.btnEjectAllUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEjectAllUsers.Location = New System.Drawing.Point(15, 15)
    Me.btnEjectAllUsers.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEjectAllUsers.Name = "btnEjectAllUsers"
    Me.btnEjectAllUsers.Size = New System.Drawing.Size(357, 27)
    Me.btnEjectAllUsers.TabIndex = 1
    Me.btnEjectAllUsers.Text = "Eject All Users"
    Me.btnEjectAllUsers.UseVisualStyleBackColor = True
    '
    'btnRequestIndexReorganization
    '
    Me.btnRequestIndexReorganization.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRequestIndexReorganization.Location = New System.Drawing.Point(15, 85)
    Me.btnRequestIndexReorganization.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnRequestIndexReorganization.Name = "btnRequestIndexReorganization"
    Me.btnRequestIndexReorganization.Size = New System.Drawing.Size(357, 27)
    Me.btnRequestIndexReorganization.TabIndex = 3
    Me.btnRequestIndexReorganization.Text = "Request Index Reorganization"
    Me.btnRequestIndexReorganization.UseVisualStyleBackColor = True
    '
    'pnlDBMaintenanceR
    '
    Me.pnlDBMaintenanceR.Controls.Add(Me.gpbSysAdmin)
    Me.pnlDBMaintenanceR.Controls.Add(Me.btnTranslationAddAllPossibilitiesToObjectToTranslate)
    Me.pnlDBMaintenanceR.Controls.Add(Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate)
    Me.pnlDBMaintenanceR.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlDBMaintenanceR.Location = New System.Drawing.Point(396, 3)
    Me.pnlDBMaintenanceR.Name = "pnlDBMaintenanceR"
    Me.pnlDBMaintenanceR.Size = New System.Drawing.Size(388, 220)
    Me.pnlDBMaintenanceR.TabIndex = 1
    '
    'gpbSysAdmin
    '
    Me.gpbSysAdmin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gpbSysAdmin.Controls.Add(Me.btnRunScriptOnServer)
    Me.gpbSysAdmin.Controls.Add(Me.btnEnableCLR)
    Me.gpbSysAdmin.Location = New System.Drawing.Point(15, 132)
    Me.gpbSysAdmin.Name = "gpbSysAdmin"
    Me.gpbSysAdmin.Size = New System.Drawing.Size(358, 83)
    Me.gpbSysAdmin.TabIndex = 4
    Me.gpbSysAdmin.TabStop = False
    Me.gpbSysAdmin.Text = "SQL SysAdmin or dbo Only"
    '
    'btnRunScriptOnServer
    '
    Me.btnRunScriptOnServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRunScriptOnServer.Location = New System.Drawing.Point(6, 51)
    Me.btnRunScriptOnServer.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnRunScriptOnServer.Name = "btnRunScriptOnServer"
    Me.btnRunScriptOnServer.Size = New System.Drawing.Size(346, 27)
    Me.btnRunScriptOnServer.TabIndex = 4
    Me.btnRunScriptOnServer.Text = "Run Script On Server"
    Me.btnRunScriptOnServer.UseVisualStyleBackColor = True
    '
    'btnEnableCLR
    '
    Me.btnEnableCLR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEnableCLR.Location = New System.Drawing.Point(6, 22)
    Me.btnEnableCLR.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnEnableCLR.Name = "btnEnableCLR"
    Me.btnEnableCLR.Size = New System.Drawing.Size(346, 27)
    Me.btnEnableCLR.TabIndex = 4
    Me.btnEnableCLR.Text = "Enable CLR"
    Me.btnEnableCLR.UseVisualStyleBackColor = True
    '
    'btnTranslationAddAllPossibilitiesToObjectToTranslate
    '
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Location = New System.Drawing.Point(15, 15)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Name = "btnTranslationAddAllPossibilitiesToObjectToTranslate"
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Size = New System.Drawing.Size(358, 53)
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.TabIndex = 3
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.Text = "Translation - Add All Possibilities To 'ObjectToTranslate'"
    Me.btnTranslationAddAllPossibilitiesToObjectToTranslate.UseVisualStyleBackColor = True
    '
    'btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate
    '
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Location = New System.Drawing.Point(15, 76)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Name = "btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate"
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Size = New System.Drawing.Size(358, 53)
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.TabIndex = 3
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.Text = "Translation - Remove Unused Possibilities From 'ObjectToTranslate'"
    Me.btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate.UseVisualStyleBackColor = True
    '
    'gpbCreateBinaryFilesOnServer
    '
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.rbtnOneFilePerTable)
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.rbtnOneFileForDatabase)
    Me.gpbCreateBinaryFilesOnServer.Controls.Add(Me.btnCreateBinaryFilesOnServer)
    Me.gpbCreateBinaryFilesOnServer.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbCreateBinaryFilesOnServer.Location = New System.Drawing.Point(5, 5)
    Me.gpbCreateBinaryFilesOnServer.Name = "gpbCreateBinaryFilesOnServer"
    Me.gpbCreateBinaryFilesOnServer.Size = New System.Drawing.Size(793, 83)
    Me.gpbCreateBinaryFilesOnServer.TabIndex = 1
    Me.gpbCreateBinaryFilesOnServer.TabStop = False
    Me.gpbCreateBinaryFilesOnServer.Text = "Create Binary Files on Server"
    '
    'rbtnOneFilePerTable
    '
    Me.rbtnOneFilePerTable.AutoSize = True
    Me.rbtnOneFilePerTable.Location = New System.Drawing.Point(18, 55)
    Me.rbtnOneFilePerTable.Margin = New System.Windows.Forms.Padding(15, 8, 15, 0)
    Me.rbtnOneFilePerTable.Name = "rbtnOneFilePerTable"
    Me.rbtnOneFilePerTable.Size = New System.Drawing.Size(134, 23)
    Me.rbtnOneFilePerTable.TabIndex = 2
    Me.rbtnOneFilePerTable.TabStop = True
    Me.rbtnOneFilePerTable.Text = "One File Per Table"
    Me.rbtnOneFilePerTable.UseVisualStyleBackColor = True
    '
    'rbtnOneFileForDatabase
    '
    Me.rbtnOneFileForDatabase.AutoSize = True
    Me.rbtnOneFileForDatabase.Location = New System.Drawing.Point(18, 24)
    Me.rbtnOneFileForDatabase.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.rbtnOneFileForDatabase.Name = "rbtnOneFileForDatabase"
    Me.rbtnOneFileForDatabase.Size = New System.Drawing.Size(154, 23)
    Me.rbtnOneFileForDatabase.TabIndex = 1
    Me.rbtnOneFileForDatabase.TabStop = True
    Me.rbtnOneFileForDatabase.Text = "One file for database"
    Me.rbtnOneFileForDatabase.UseVisualStyleBackColor = True
    '
    'btnCreateBinaryFilesOnServer
    '
    Me.btnCreateBinaryFilesOnServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCreateBinaryFilesOnServer.Location = New System.Drawing.Point(362, 35)
    Me.btnCreateBinaryFilesOnServer.Name = "btnCreateBinaryFilesOnServer"
    Me.btnCreateBinaryFilesOnServer.Size = New System.Drawing.Size(208, 27)
    Me.btnCreateBinaryFilesOnServer.TabIndex = 0
    Me.btnCreateBinaryFilesOnServer.Text = "Create Binary Files on Server"
    Me.btnCreateBinaryFilesOnServer.UseVisualStyleBackColor = True
    '
    'grdIndexFragmentation
    '
    Me.grdIndexFragmentation.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.grdIndexFragmentation.Location = New System.Drawing.Point(282, 19)
    Me.grdIndexFragmentation.Name = "grdIndexFragmentation"
    Me.grdIndexFragmentation.Size = New System.Drawing.Size(155, 95)
    Me.grdIndexFragmentation.TabIndex = 4
    '
    'grdTableSizes
    '
    Me.grdTableSizes.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.grdTableSizes.Location = New System.Drawing.Point(3, 16)
    Me.grdTableSizes.Name = "grdTableSizes"
    Me.grdTableSizes.Size = New System.Drawing.Size(329, 115)
    Me.grdTableSizes.TabIndex = 2
    '
    'ctlPnlMaintenance
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.tbc)
    Me.Controls.Add(Me.gpbHeader)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlPnlMaintenance"
    Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 3)
    Me.Size = New System.Drawing.Size(821, 672)
    Me.gpbHeader.ResumeLayout(False)
    Me.gpbHeader.PerformLayout()
    Me.tbc.ResumeLayout(False)
    Me.tbpActiveUser.ResumeLayout(False)
    Me.tlp.ResumeLayout(False)
    Me.pnlLeft.ResumeLayout(False)
    Me.gpbChangeLanguage.ResumeLayout(False)
    Me.gpbChangeLanguage.PerformLayout()
    Me.gpbChangePassword.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.gpbDetails.ResumeLayout(False)
    Me.gpbDetails.PerformLayout()
    Me.pnlRight.ResumeLayout(False)
    Me.pnlRight.PerformLayout()
    Me.gpbSecurity.ResumeLayout(False)
    Me.gpbSecurity.PerformLayout()
    CType(Me.dgvLastLogins, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gpbUserInterfaceLanguage.ResumeLayout(False)
    Me.gpbUserInterfaceLanguage.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.tbpDatabase.ResumeLayout(False)
    Me.gpbDatabaseStatus.ResumeLayout(False)
    Me.gpbDatabaseStatus.PerformLayout()
    Me.pnlDatabaseStatus.ResumeLayout(False)
    Me.gbpDBMaintenance.ResumeLayout(False)
    Me.tblDBMaintenance.ResumeLayout(False)
    Me.pnlDBMaintenanceL.ResumeLayout(False)
    Me.pnlDBMaintenanceR.ResumeLayout(False)
    Me.gpbSysAdmin.ResumeLayout(False)
    Me.gpbCreateBinaryFilesOnServer.ResumeLayout(False)
    Me.gpbCreateBinaryFilesOnServer.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents gpbHeader As System.Windows.Forms.GroupBox
  Friend WithEvents lblTitle As System.Windows.Forms.Label
  Friend WithEvents tbc As System.Windows.Forms.TabControl
  Friend WithEvents tbpActiveUser As System.Windows.Forms.TabPage
  Friend WithEvents tbpDatabase As System.Windows.Forms.TabPage
  Friend WithEvents txtPasswordHashed As System.Windows.Forms.TextBox
  Friend WithEvents lblPasswordHashed As System.Windows.Forms.Label
  Friend WithEvents txtType As System.Windows.Forms.TextBox
  Friend WithEvents lblType As System.Windows.Forms.Label
  Friend WithEvents txtRoles As System.Windows.Forms.TextBox
  Friend WithEvents lblRoles As System.Windows.Forms.Label
  Friend WithEvents txtLastName As System.Windows.Forms.TextBox
  Friend WithEvents lblLastName As System.Windows.Forms.Label
  Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
  Friend WithEvents lblFirstName As System.Windows.Forms.Label
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
  Friend WithEvents gbpDBMaintenance As System.Windows.Forms.GroupBox
  Friend WithEvents gpbCreateBinaryFilesOnServer As System.Windows.Forms.GroupBox
  Friend WithEvents rbtnOneFilePerTable As System.Windows.Forms.RadioButton
  Friend WithEvents rbtnOneFileForDatabase As System.Windows.Forms.RadioButton
  Friend WithEvents btnCreateBinaryFilesOnServer As System.Windows.Forms.Button
  Friend WithEvents btnEjectAllUsers As System.Windows.Forms.Button
  Friend WithEvents btnEjectNonMasterUsersOnly As System.Windows.Forms.Button
  Friend WithEvents gpbDatabaseStatus As System.Windows.Forms.GroupBox
  Friend WithEvents btnIndexFragmentation As System.Windows.Forms.Button
  Friend WithEvents btnTableSizes As System.Windows.Forms.Button
  Friend WithEvents btnRequestIndexReorganization As System.Windows.Forms.Button
  Friend WithEvents btnResetPermissionsForDefaultRoles As System.Windows.Forms.Button
  Friend WithEvents btnUserRefresh As System.Windows.Forms.Button
  Friend WithEvents gpbUserInterfaceLanguage As System.Windows.Forms.GroupBox
  Friend WithEvents txtUILang As System.Windows.Forms.TextBox
  Friend WithEvents btnUILangChange As System.Windows.Forms.Button
  Friend WithEvents btnUILangCancel As System.Windows.Forms.Button
  Friend WithEvents cboUILang As System.Windows.Forms.ComboBox
  Friend WithEvents btnTranslationRemoveUnusedPossibilitiesFromObjectToTranslate As System.Windows.Forms.Button
  Friend WithEvents btnTranslationAddAllPossibilitiesToObjectToTranslate As System.Windows.Forms.Button
  Friend WithEvents pnlDatabaseStatus As System.Windows.Forms.Panel
  Friend WithEvents grdTableSizes As ctlc_TableSizeCol
  Friend WithEvents grdIndexFragmentation As ctlc_IndexFragmentationCol
  Friend WithEvents btnBackupDatabase As System.Windows.Forms.Button
  Friend WithEvents tblDBMaintenance As TableLayoutPanel
  Friend WithEvents pnlDBMaintenanceL As Panel
  Friend WithEvents pnlDBMaintenanceR As Panel
  Friend WithEvents btnEnableCLR As Button
  Friend WithEvents gpbSysAdmin As GroupBox
  Friend WithEvents btnRunScriptOnServer As Button
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
  Friend WithEvents txtDatabaseFileSizes As TextBox
  Friend WithEvents btnDatabaseFileSizes As Button
  Friend WithEvents gpbChangeLanguage As GroupBox
  Friend WithEvents Label1 As Label
  Friend WithEvents btnCreateBiometricKey As Button
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents btnPIN As Button
End Class
