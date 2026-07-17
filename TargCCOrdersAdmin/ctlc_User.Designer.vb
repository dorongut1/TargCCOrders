'Look for Drawing.Color.ColourObjectBackground (tabs have them too)

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_User
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
    Me.txtID = New System.Windows.Forms.TextBox()
    Me.lblID = New System.Windows.Forms.Label()
    Me.txtUserName = New System.Windows.Forms.TextBox()
    Me.lblUserName = New System.Windows.Forms.Label()
    Me.txtLastName = New System.Windows.Forms.TextBox()
    Me.lblLastName = New System.Windows.Forms.Label()
    Me.txtFirstName = New System.Windows.Forms.TextBox()
    Me.lblFirstName = New System.Windows.Forms.Label()
    Me.txtFullName = New System.Windows.Forms.TextBox()
    Me.lblFullName = New System.Windows.Forms.Label()
    Me.txtNationalIDNo = New System.Windows.Forms.TextBox()
    Me.lblNationalIDNo = New System.Windows.Forms.Label()
    Me.txtAddress = New System.Windows.Forms.TextBox()
    Me.lblAddress = New System.Windows.Forms.Label()
    Me.txtCity = New System.Windows.Forms.TextBox()
    Me.lblCity = New System.Windows.Forms.Label()
    Me.txtProvinceState = New System.Windows.Forms.TextBox()
    Me.lblProvinceState = New System.Windows.Forms.Label()
    Me.txtPostalCode = New System.Windows.Forms.TextBox()
    Me.lblPostalCode = New System.Windows.Forms.Label()
    Me.txtCountry = New System.Windows.Forms.TextBox()
    Me.lblCountry = New System.Windows.Forms.Label()
    Me.txtPhoneNumber = New System.Windows.Forms.TextBox()
    Me.lblPhoneNumber = New System.Windows.Forms.Label()
    Me.txtEmail = New System.Windows.Forms.TextBox()
    Me.lblEmail = New System.Windows.Forms.Label()
    Me.txtPasswordHashed = New System.Windows.Forms.TextBox()
    Me.btnPasswordHashedUpdate = New System.Windows.Forms.Button()
    Me.txtDatePasswordChanged = New System.Windows.Forms.TextBox()
    Me.lblDatePasswordChanged = New System.Windows.Forms.Label()
    Me.cboType = New System.Windows.Forms.ComboBox()
    Me.txtType = New System.Windows.Forms.TextBox()
    Me.lblType = New System.Windows.Forms.Label()
    Me.txtIDinType = New System.Windows.Forms.TextBox()
    Me.lblIDinType = New System.Windows.Forms.Label()
    Me.chkRequiresComputerIdentification = New System.Windows.Forms.CheckBox()
    Me.lblRequiresComputerIdentification = New System.Windows.Forms.Label()
    Me.chkEnableSimultaneousLogins = New System.Windows.Forms.CheckBox()
    Me.lblEnableSimultaneousLogins = New System.Windows.Forms.Label()
    Me.txtDateActivated = New System.Windows.Forms.TextBox()
    Me.lblDateActivated = New System.Windows.Forms.Label()
    Me.chkIsDisabled = New System.Windows.Forms.CheckBox()
    Me.lblIsDisabled = New System.Windows.Forms.Label()
    Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
    Me.txtExpiryDate = New System.Windows.Forms.TextBox()
    Me.lblExpiryDate = New System.Windows.Forms.Label()
    Me.txtComments = New System.Windows.Forms.TextBox()
    Me.btnCommentsUpdate = New System.Windows.Forms.Button()
    Me.lblComments = New System.Windows.Forms.Label()
    Me.txtApplications = New System.Windows.Forms.TextBox()
    Me.btnApplicationsUpdate = New System.Windows.Forms.Button()
    Me.lblApplications = New System.Windows.Forms.Label()
    Me.txtNextSMSPasswordHashed = New System.Windows.Forms.TextBox()
    Me.btnNextSMSPasswordHashedUpdate = New System.Windows.Forms.Button()
    Me.lblNextSMSPasswordHashed = New System.Windows.Forms.Label()
    Me.lblRole = New System.Windows.Forms.Label()
    Me.txtRole = New System.Windows.Forms.TextBox()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.tbcUser = New System.Windows.Forms.TabControl()
    Me.tbpInfo = New System.Windows.Forms.TabPage()
    Me.tlp1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.GroupBox7 = New System.Windows.Forms.GroupBox()
    Me.txtLastSuccessfulLogin = New System.Windows.Forms.TextBox()
    Me.lblIsLockedOut = New System.Windows.Forms.Label()
    Me.chkIsLockedOut = New System.Windows.Forms.CheckBox()
    Me.lblLastSuccessfulLogin = New System.Windows.Forms.Label()
    Me.GroupBox3 = New System.Windows.Forms.GroupBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.cboMessagingMode = New System.Windows.Forms.ComboBox()
    Me.txtMessagingMode = New System.Windows.Forms.TextBox()
    Me.lblMessagingMode = New System.Windows.Forms.Label()
    Me.cboLanguage = New System.Windows.Forms.ComboBox()
    Me.txtLanguage = New System.Windows.Forms.TextBox()
    Me.lblLanguage = New System.Windows.Forms.Label()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.tbpAccess = New System.Windows.Forms.TabPage()
    Me.gpbUserPermissionColForUser = New System.Windows.Forms.GroupBox()
    Me.tlpSecurity = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.gpbSecurity = New System.Windows.Forms.GroupBox()
    Me.lblRequiresFixedIP = New System.Windows.Forms.Label()
    Me.chkRequiresFixedIP = New System.Windows.Forms.CheckBox()
    Me.Panel4 = New System.Windows.Forms.Panel()
    Me.gpbApplicationsRoles = New System.Windows.Forms.GroupBox()
    Me.cboRole = New IntelliCombo()
    Me.tbpPasswordsCodes = New System.Windows.Forms.TabPage()
    Me.gpbApplicationLoginKeys = New System.Windows.Forms.GroupBox()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.btnDeletePIN = New System.Windows.Forms.Button()
    Me.btnDeleteAllKeys = New System.Windows.Forms.Button()
    Me.MyCtlUserLoginKeyColForUser = New ctlc_UserLoginKeyCol()
    Me.tlpPasswordMFA = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel5 = New System.Windows.Forms.Panel()
    Me.gpb2FactorAuthentication = New System.Windows.Forms.GroupBox()
    Me.lblLoggedInIP = New System.Windows.Forms.Label()
    Me.cboAuthenticationMethod = New System.Windows.Forms.ComboBox()
    Me.lblAuthenticationMethod = New System.Windows.Forms.Label()
    Me.txtLoggedInIP = New System.Windows.Forms.TextBox()
    Me.txtAuthenticationMethod = New System.Windows.Forms.TextBox()
    Me.gpbPassword = New System.Windows.Forms.GroupBox()
    Me.chkPasswordNeverExpires = New System.Windows.Forms.CheckBox()
    Me.lblPasswordNeverExpires = New System.Windows.Forms.Label()
    Me.Panel6 = New System.Windows.Forms.Panel()
    Me.gpbIdentityVerificationQuestions = New System.Windows.Forms.GroupBox()
    Me.btnViewHideSecurityQuestionResponse = New System.Windows.Forms.Button()
    Me.lblSecurityQuestion3 = New System.Windows.Forms.Label()
    Me.lblSecurityQuestion3Response = New System.Windows.Forms.Label()
    Me.txtSecurityQuestion3 = New System.Windows.Forms.TextBox()
    Me.lblSecurityQuestion2Response = New System.Windows.Forms.Label()
    Me.txtSecurityQuestion3Response = New System.Windows.Forms.TextBox()
    Me.lblSecurityQuestion1Response = New System.Windows.Forms.Label()
    Me.txtSecurityQuestion2Response = New System.Windows.Forms.TextBox()
    Me.lblSecurityQuestion2 = New System.Windows.Forms.Label()
    Me.txtSecurityQuestion1Response = New System.Windows.Forms.TextBox()
    Me.txtSecurityQuestion2 = New System.Windows.Forms.TextBox()
    Me.lblSecurityQuestion1 = New System.Windows.Forms.Label()
    Me.txtSecurityQuestion1 = New System.Windows.Forms.TextBox()
    Me.tbpComments = New System.Windows.Forms.TabPage()
    Me.pnlComments = New System.Windows.Forms.Panel()
    Me.lblPasswordHashed = New System.Windows.Forms.Label()
    Me.pnlGarbage = New System.Windows.Forms.Panel()
    Me.btnPINUpdate = New System.Windows.Forms.Button()
    Me.cboSecurityQuestion3 = New System.Windows.Forms.ComboBox()
    Me.lblPIN = New System.Windows.Forms.Label()
    Me.btnSecurityQuestion3ResponseUpdate = New System.Windows.Forms.Button()
    Me.txtPIN = New System.Windows.Forms.TextBox()
    Me.btnLastSuccessfulLoginUpdate = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion2ResponseUpdate = New System.Windows.Forms.Button()
    Me.btnLoggedInIPUpdate = New System.Windows.Forms.Button()
    Me.btnSecurityQuestion1ResponseUpdate = New System.Windows.Forms.Button()
    Me.lblApprovalFunctionName = New System.Windows.Forms.Label()
    Me.txtLastPasswords = New System.Windows.Forms.TextBox()
    Me.lblLastPasswords = New System.Windows.Forms.Label()
    Me.txtApprovalTime = New System.Windows.Forms.TextBox()
    Me.cboSecurityQuestion2 = New System.Windows.Forms.ComboBox()
    Me.txtApprovalFunctionName = New System.Windows.Forms.TextBox()
    Me.cboSecurityQuestion1 = New System.Windows.Forms.ComboBox()
    Me.lblApprovalTime = New System.Windows.Forms.Label()
    Me.dtpApprovalTime = New System.Windows.Forms.DateTimePicker()
    Me.lblApprovalCodeHashed = New System.Windows.Forms.Label()
    Me.txtApprovalCodeHashed = New System.Windows.Forms.TextBox()
    Me.lblTester = New System.Windows.Forms.Label()
    Me.tbcUser.SuspendLayout()
    Me.tbpInfo.SuspendLayout()
    Me.tlp1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.GroupBox7.SuspendLayout()
    Me.GroupBox3.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox4.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.tbpAccess.SuspendLayout()
    Me.tlpSecurity.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.gpbSecurity.SuspendLayout()
    Me.Panel4.SuspendLayout()
    Me.gpbApplicationsRoles.SuspendLayout()
    Me.tbpPasswordsCodes.SuspendLayout()
    Me.gpbApplicationLoginKeys.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.tlpPasswordMFA.SuspendLayout()
    Me.Panel5.SuspendLayout()
    Me.gpb2FactorAuthentication.SuspendLayout()
    Me.gpbPassword.SuspendLayout()
    Me.Panel6.SuspendLayout()
    Me.gpbIdentityVerificationQuestions.SuspendLayout()
    Me.tbpComments.SuspendLayout()
    Me.pnlComments.SuspendLayout()
    Me.pnlGarbage.SuspendLayout()
    Me.SuspendLayout()
    '
    'txtID
    '
    Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtID.Location = New System.Drawing.Point(601, 543)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(90, 25)
    Me.txtID.TabIndex = 0
    Me.txtID.Text = "txtID"
    '
    'lblID
    '
    Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblID.AutoSize = True
    Me.lblID.Location = New System.Drawing.Point(553, 546)
    Me.lblID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblID.Name = "lblID"
    Me.lblID.Size = New System.Drawing.Size(23, 19)
    Me.lblID.TabIndex = 1
    Me.lblID.Text = "ID"
    '
    'txtUserName
    '
    Me.txtUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUserName.Location = New System.Drawing.Point(131, 36)
    Me.txtUserName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.Size = New System.Drawing.Size(209, 25)
    Me.txtUserName.TabIndex = 2
    Me.txtUserName.Text = "txtUserName"
    '
    'lblUserName
    '
    Me.lblUserName.AutoSize = True
    Me.lblUserName.Location = New System.Drawing.Point(13, 39)
    Me.lblUserName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblUserName.Name = "lblUserName"
    Me.lblUserName.Size = New System.Drawing.Size(77, 19)
    Me.lblUserName.TabIndex = 3
    Me.lblUserName.Text = "User Name"
    '
    'txtLastName
    '
    Me.txtLastName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastName.Location = New System.Drawing.Point(131, 76)
    Me.txtLastName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastName.Name = "txtLastName"
    Me.txtLastName.Size = New System.Drawing.Size(209, 25)
    Me.txtLastName.TabIndex = 4
    Me.txtLastName.Text = "txtLastName"
    '
    'lblLastName
    '
    Me.lblLastName.AutoSize = True
    Me.lblLastName.Location = New System.Drawing.Point(13, 79)
    Me.lblLastName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLastName.Name = "lblLastName"
    Me.lblLastName.Size = New System.Drawing.Size(74, 19)
    Me.lblLastName.TabIndex = 5
    Me.lblLastName.Text = "Last Name"
    '
    'txtFirstName
    '
    Me.txtFirstName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFirstName.Location = New System.Drawing.Point(131, 116)
    Me.txtFirstName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtFirstName.Name = "txtFirstName"
    Me.txtFirstName.Size = New System.Drawing.Size(209, 25)
    Me.txtFirstName.TabIndex = 6
    Me.txtFirstName.Text = "txtFirstName"
    '
    'lblFirstName
    '
    Me.lblFirstName.AutoSize = True
    Me.lblFirstName.Location = New System.Drawing.Point(13, 119)
    Me.lblFirstName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblFirstName.Name = "lblFirstName"
    Me.lblFirstName.Size = New System.Drawing.Size(75, 19)
    Me.lblFirstName.TabIndex = 7
    Me.lblFirstName.Text = "First Name"
    '
    'txtFullName
    '
    Me.txtFullName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFullName.Location = New System.Drawing.Point(131, 156)
    Me.txtFullName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtFullName.Name = "txtFullName"
    Me.txtFullName.Size = New System.Drawing.Size(209, 25)
    Me.txtFullName.TabIndex = 8
    Me.txtFullName.Text = "txtFullName"
    '
    'lblFullName
    '
    Me.lblFullName.AutoSize = True
    Me.lblFullName.Location = New System.Drawing.Point(13, 159)
    Me.lblFullName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblFullName.Name = "lblFullName"
    Me.lblFullName.Size = New System.Drawing.Size(70, 19)
    Me.lblFullName.TabIndex = 9
    Me.lblFullName.Text = "Full Name"
    '
    'txtNationalIDNo
    '
    Me.txtNationalIDNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNationalIDNo.Location = New System.Drawing.Point(131, 192)
    Me.txtNationalIDNo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtNationalIDNo.Name = "txtNationalIDNo"
    Me.txtNationalIDNo.Size = New System.Drawing.Size(209, 25)
    Me.txtNationalIDNo.TabIndex = 10
    Me.txtNationalIDNo.Text = "txtNationalIDNo"
    '
    'lblNationalIDNo
    '
    Me.lblNationalIDNo.AutoSize = True
    Me.lblNationalIDNo.Location = New System.Drawing.Point(13, 195)
    Me.lblNationalIDNo.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblNationalIDNo.Name = "lblNationalIDNo"
    Me.lblNationalIDNo.Size = New System.Drawing.Size(100, 19)
    Me.lblNationalIDNo.TabIndex = 11
    Me.lblNationalIDNo.Text = "National ID No"
    '
    'txtAddress
    '
    Me.txtAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAddress.Location = New System.Drawing.Point(134, 36)
    Me.txtAddress.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtAddress.Multiline = True
    Me.txtAddress.Name = "txtAddress"
    Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtAddress.Size = New System.Drawing.Size(207, 54)
    Me.txtAddress.TabIndex = 12
    Me.txtAddress.Text = "txtAddress"
    '
    'lblAddress
    '
    Me.lblAddress.AutoSize = True
    Me.lblAddress.Location = New System.Drawing.Point(13, 39)
    Me.lblAddress.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblAddress.Name = "lblAddress"
    Me.lblAddress.Size = New System.Drawing.Size(58, 19)
    Me.lblAddress.TabIndex = 13
    Me.lblAddress.Text = "Address"
    '
    'txtCity
    '
    Me.txtCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCity.Location = New System.Drawing.Point(134, 105)
    Me.txtCity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtCity.Name = "txtCity"
    Me.txtCity.Size = New System.Drawing.Size(207, 25)
    Me.txtCity.TabIndex = 14
    Me.txtCity.Text = "txtCity"
    '
    'lblCity
    '
    Me.lblCity.AutoSize = True
    Me.lblCity.Location = New System.Drawing.Point(13, 111)
    Me.lblCity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblCity.Name = "lblCity"
    Me.lblCity.Size = New System.Drawing.Size(33, 19)
    Me.lblCity.TabIndex = 15
    Me.lblCity.Text = "City"
    '
    'txtProvinceState
    '
    Me.txtProvinceState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProvinceState.Location = New System.Drawing.Point(134, 145)
    Me.txtProvinceState.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtProvinceState.Name = "txtProvinceState"
    Me.txtProvinceState.Size = New System.Drawing.Size(207, 25)
    Me.txtProvinceState.TabIndex = 16
    Me.txtProvinceState.Text = "txtProvinceState"
    '
    'lblProvinceState
    '
    Me.lblProvinceState.AutoSize = True
    Me.lblProvinceState.Location = New System.Drawing.Point(13, 148)
    Me.lblProvinceState.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblProvinceState.Name = "lblProvinceState"
    Me.lblProvinceState.Size = New System.Drawing.Size(96, 19)
    Me.lblProvinceState.TabIndex = 17
    Me.lblProvinceState.Text = "Province State"
    '
    'txtPostalCode
    '
    Me.txtPostalCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPostalCode.Location = New System.Drawing.Point(134, 185)
    Me.txtPostalCode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPostalCode.Name = "txtPostalCode"
    Me.txtPostalCode.Size = New System.Drawing.Size(103, 25)
    Me.txtPostalCode.TabIndex = 18
    Me.txtPostalCode.Text = "txtPostalCode"
    '
    'lblPostalCode
    '
    Me.lblPostalCode.AutoSize = True
    Me.lblPostalCode.Location = New System.Drawing.Point(13, 188)
    Me.lblPostalCode.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblPostalCode.Name = "lblPostalCode"
    Me.lblPostalCode.Size = New System.Drawing.Size(81, 19)
    Me.lblPostalCode.TabIndex = 19
    Me.lblPostalCode.Text = "Postal Code"
    '
    'txtCountry
    '
    Me.txtCountry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCountry.Location = New System.Drawing.Point(134, 225)
    Me.txtCountry.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtCountry.Name = "txtCountry"
    Me.txtCountry.Size = New System.Drawing.Size(207, 25)
    Me.txtCountry.TabIndex = 20
    Me.txtCountry.Text = "txtCountry"
    '
    'lblCountry
    '
    Me.lblCountry.AutoSize = True
    Me.lblCountry.Location = New System.Drawing.Point(13, 228)
    Me.lblCountry.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblCountry.Name = "lblCountry"
    Me.lblCountry.Size = New System.Drawing.Size(59, 19)
    Me.lblCountry.TabIndex = 21
    Me.lblCountry.Text = "Country"
    '
    'txtPhoneNumber
    '
    Me.txtPhoneNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPhoneNumber.Location = New System.Drawing.Point(153, 36)
    Me.txtPhoneNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPhoneNumber.Name = "txtPhoneNumber"
    Me.txtPhoneNumber.Size = New System.Drawing.Size(188, 25)
    Me.txtPhoneNumber.TabIndex = 22
    Me.txtPhoneNumber.Text = "txtPhoneNumber"
    '
    'lblPhoneNumber
    '
    Me.lblPhoneNumber.AutoSize = True
    Me.lblPhoneNumber.Location = New System.Drawing.Point(13, 39)
    Me.lblPhoneNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblPhoneNumber.Name = "lblPhoneNumber"
    Me.lblPhoneNumber.Size = New System.Drawing.Size(108, 19)
    Me.lblPhoneNumber.TabIndex = 23
    Me.lblPhoneNumber.Text = "Phone Number*"
    '
    'txtEmail
    '
    Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmail.Location = New System.Drawing.Point(153, 76)
    Me.txtEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.Size = New System.Drawing.Size(188, 25)
    Me.txtEmail.TabIndex = 24
    Me.txtEmail.Text = "txtEmail"
    '
    'lblEmail
    '
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Location = New System.Drawing.Point(13, 79)
    Me.lblEmail.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(47, 19)
    Me.lblEmail.TabIndex = 25
    Me.lblEmail.Text = "Email*"
    '
    'txtPasswordHashed
    '
    Me.txtPasswordHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPasswordHashed.Location = New System.Drawing.Point(43, 91)
    Me.txtPasswordHashed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPasswordHashed.Name = "txtPasswordHashed"
    Me.txtPasswordHashed.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPasswordHashed.Size = New System.Drawing.Size(106, 25)
    Me.txtPasswordHashed.TabIndex = 26
    Me.txtPasswordHashed.Text = "txtPasswordHashed"
    Me.txtPasswordHashed.UseSystemPasswordChar = True
    '
    'btnPasswordHashedUpdate
    '
    Me.btnPasswordHashedUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnPasswordHashedUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPasswordHashedUpdate.Location = New System.Drawing.Point(126, 104)
    Me.btnPasswordHashedUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnPasswordHashedUpdate.Name = "btnPasswordHashedUpdate"
    Me.btnPasswordHashedUpdate.Size = New System.Drawing.Size(211, 26)
    Me.btnPasswordHashedUpdate.TabIndex = 27
    Me.btnPasswordHashedUpdate.Text = "Reset"
    Me.btnPasswordHashedUpdate.UseVisualStyleBackColor = True
    '
    'txtDatePasswordChanged
    '
    Me.txtDatePasswordChanged.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDatePasswordChanged.Location = New System.Drawing.Point(135, 24)
    Me.txtDatePasswordChanged.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtDatePasswordChanged.Name = "txtDatePasswordChanged"
    Me.txtDatePasswordChanged.Size = New System.Drawing.Size(202, 25)
    Me.txtDatePasswordChanged.TabIndex = 29
    Me.txtDatePasswordChanged.Text = "txtDatePasswordChanged"
    '
    'lblDatePasswordChanged
    '
    Me.lblDatePasswordChanged.AutoSize = True
    Me.lblDatePasswordChanged.Location = New System.Drawing.Point(13, 27)
    Me.lblDatePasswordChanged.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblDatePasswordChanged.Name = "lblDatePasswordChanged"
    Me.lblDatePasswordChanged.Size = New System.Drawing.Size(97, 19)
    Me.lblDatePasswordChanged.TabIndex = 30
    Me.lblDatePasswordChanged.Text = "Date Changed"
    '
    'cboType
    '
    Me.cboType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboType.FormattingEnabled = True
    Me.cboType.Location = New System.Drawing.Point(131, 33)
    Me.cboType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboType.Name = "cboType"
    Me.cboType.Size = New System.Drawing.Size(177, 25)
    Me.cboType.TabIndex = 2
    '
    'txtType
    '
    Me.txtType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtType.Location = New System.Drawing.Point(131, 36)
    Me.txtType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtType.Name = "txtType"
    Me.txtType.Size = New System.Drawing.Size(209, 25)
    Me.txtType.TabIndex = 33
    Me.txtType.Text = "txtType"
    '
    'lblType
    '
    Me.lblType.AutoSize = True
    Me.lblType.Location = New System.Drawing.Point(13, 39)
    Me.lblType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblType.Name = "lblType"
    Me.lblType.Size = New System.Drawing.Size(37, 19)
    Me.lblType.TabIndex = 34
    Me.lblType.Text = "Type"
    '
    'txtIDinType
    '
    Me.txtIDinType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtIDinType.Location = New System.Drawing.Point(131, 76)
    Me.txtIDinType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtIDinType.Name = "txtIDinType"
    Me.txtIDinType.Size = New System.Drawing.Size(209, 25)
    Me.txtIDinType.TabIndex = 35
    Me.txtIDinType.Text = "txtIDinType"
    '
    'lblIDinType
    '
    Me.lblIDinType.AutoSize = True
    Me.lblIDinType.Location = New System.Drawing.Point(13, 79)
    Me.lblIDinType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblIDinType.Name = "lblIDinType"
    Me.lblIDinType.Size = New System.Drawing.Size(70, 19)
    Me.lblIDinType.TabIndex = 36
    Me.lblIDinType.Text = "I Din Type"
    '
    'chkRequiresComputerIdentification
    '
    Me.chkRequiresComputerIdentification.AutoSize = True
    Me.chkRequiresComputerIdentification.Location = New System.Drawing.Point(254, 71)
    Me.chkRequiresComputerIdentification.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkRequiresComputerIdentification.Name = "chkRequiresComputerIdentification"
    Me.chkRequiresComputerIdentification.Size = New System.Drawing.Size(15, 14)
    Me.chkRequiresComputerIdentification.TabIndex = 37
    Me.chkRequiresComputerIdentification.UseVisualStyleBackColor = True
    '
    'lblRequiresComputerIdentification
    '
    Me.lblRequiresComputerIdentification.AutoSize = True
    Me.lblRequiresComputerIdentification.Location = New System.Drawing.Point(13, 69)
    Me.lblRequiresComputerIdentification.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblRequiresComputerIdentification.Name = "lblRequiresComputerIdentification"
    Me.lblRequiresComputerIdentification.Size = New System.Drawing.Size(204, 19)
    Me.lblRequiresComputerIdentification.TabIndex = 38
    Me.lblRequiresComputerIdentification.Text = "Require Computer Identification"
    '
    'chkEnableSimultaneousLogins
    '
    Me.chkEnableSimultaneousLogins.AutoSize = True
    Me.chkEnableSimultaneousLogins.Location = New System.Drawing.Point(254, 42)
    Me.chkEnableSimultaneousLogins.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkEnableSimultaneousLogins.Name = "chkEnableSimultaneousLogins"
    Me.chkEnableSimultaneousLogins.Size = New System.Drawing.Size(15, 14)
    Me.chkEnableSimultaneousLogins.TabIndex = 39
    Me.chkEnableSimultaneousLogins.UseVisualStyleBackColor = True
    '
    'lblEnableSimultaneousLogins
    '
    Me.lblEnableSimultaneousLogins.AutoSize = True
    Me.lblEnableSimultaneousLogins.Location = New System.Drawing.Point(13, 40)
    Me.lblEnableSimultaneousLogins.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblEnableSimultaneousLogins.Name = "lblEnableSimultaneousLogins"
    Me.lblEnableSimultaneousLogins.Size = New System.Drawing.Size(179, 19)
    Me.lblEnableSimultaneousLogins.TabIndex = 40
    Me.lblEnableSimultaneousLogins.Text = "Enable Simultaneous Logins"
    '
    'txtDateActivated
    '
    Me.txtDateActivated.Location = New System.Drawing.Point(14, 43)
    Me.txtDateActivated.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtDateActivated.Name = "txtDateActivated"
    Me.txtDateActivated.Size = New System.Drawing.Size(159, 25)
    Me.txtDateActivated.TabIndex = 41
    Me.txtDateActivated.Text = "txtDateActivated"
    '
    'lblDateActivated
    '
    Me.lblDateActivated.AutoSize = True
    Me.lblDateActivated.Location = New System.Drawing.Point(10, 21)
    Me.lblDateActivated.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblDateActivated.Name = "lblDateActivated"
    Me.lblDateActivated.Size = New System.Drawing.Size(99, 19)
    Me.lblDateActivated.TabIndex = 42
    Me.lblDateActivated.Text = "Date Activated"
    '
    'chkIsDisabled
    '
    Me.chkIsDisabled.AutoSize = True
    Me.chkIsDisabled.Location = New System.Drawing.Point(78, 82)
    Me.chkIsDisabled.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkIsDisabled.Name = "chkIsDisabled"
    Me.chkIsDisabled.Size = New System.Drawing.Size(15, 14)
    Me.chkIsDisabled.TabIndex = 43
    Me.chkIsDisabled.UseVisualStyleBackColor = True
    '
    'lblIsDisabled
    '
    Me.lblIsDisabled.AutoSize = True
    Me.lblIsDisabled.Location = New System.Drawing.Point(10, 77)
    Me.lblIsDisabled.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblIsDisabled.Name = "lblIsDisabled"
    Me.lblIsDisabled.Size = New System.Drawing.Size(61, 19)
    Me.lblIsDisabled.TabIndex = 44
    Me.lblIsDisabled.Text = "Disabled"
    '
    'dtpExpiryDate
    '
    Me.dtpExpiryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpExpiryDate.CustomFormat = "dd/MM/yyyy HH:mm:ss"
    Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpExpiryDate.Location = New System.Drawing.Point(116, 54)
    Me.dtpExpiryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.dtpExpiryDate.Name = "dtpExpiryDate"
    Me.dtpExpiryDate.ShowCheckBox = True
    Me.dtpExpiryDate.ShowUpDown = True
    Me.dtpExpiryDate.Size = New System.Drawing.Size(143, 25)
    Me.dtpExpiryDate.TabIndex = 45
    '
    'txtExpiryDate
    '
    Me.txtExpiryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtExpiryDate.Location = New System.Drawing.Point(135, 64)
    Me.txtExpiryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtExpiryDate.Name = "txtExpiryDate"
    Me.txtExpiryDate.Size = New System.Drawing.Size(124, 25)
    Me.txtExpiryDate.TabIndex = 46
    Me.txtExpiryDate.Text = "txtExpiryDate"
    '
    'lblExpiryDate
    '
    Me.lblExpiryDate.AutoSize = True
    Me.lblExpiryDate.Location = New System.Drawing.Point(13, 64)
    Me.lblExpiryDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblExpiryDate.Name = "lblExpiryDate"
    Me.lblExpiryDate.Size = New System.Drawing.Size(78, 19)
    Me.lblExpiryDate.TabIndex = 47
    Me.lblExpiryDate.Text = "Expiry Date"
    '
    'txtComments
    '
    Me.txtComments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtComments.Location = New System.Drawing.Point(28, 44)
    Me.txtComments.Margin = New System.Windows.Forms.Padding(15, 0, 15, 0)
    Me.txtComments.Multiline = True
    Me.txtComments.Name = "txtComments"
    Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtComments.Size = New System.Drawing.Size(684, 370)
    Me.txtComments.TabIndex = 48
    Me.txtComments.Text = "txtComments"
    '
    'btnCommentsUpdate
    '
    Me.btnCommentsUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCommentsUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCommentsUpdate.Location = New System.Drawing.Point(637, 439)
    Me.btnCommentsUpdate.Margin = New System.Windows.Forms.Padding(15, 0, 15, 0)
    Me.btnCommentsUpdate.Name = "btnCommentsUpdate"
    Me.btnCommentsUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnCommentsUpdate.TabIndex = 49
    Me.btnCommentsUpdate.Text = "Update"
    Me.btnCommentsUpdate.UseVisualStyleBackColor = True
    '
    'lblComments
    '
    Me.lblComments.AutoSize = True
    Me.lblComments.Location = New System.Drawing.Point(10, 15)
    Me.lblComments.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblComments.Name = "lblComments"
    Me.lblComments.Size = New System.Drawing.Size(76, 19)
    Me.lblComments.TabIndex = 50
    Me.lblComments.Text = "Comments"
    '
    'txtApplications
    '
    Me.txtApplications.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApplications.Location = New System.Drawing.Point(9, 25)
    Me.txtApplications.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApplications.Multiline = True
    Me.txtApplications.Name = "txtApplications"
    Me.txtApplications.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtApplications.Size = New System.Drawing.Size(265, 71)
    Me.txtApplications.TabIndex = 53
    Me.txtApplications.Text = "txtApplications"
    '
    'btnApplicationsUpdate
    '
    Me.btnApplicationsUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnApplicationsUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnApplicationsUpdate.Location = New System.Drawing.Point(291, 25)
    Me.btnApplicationsUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnApplicationsUpdate.Name = "btnApplicationsUpdate"
    Me.btnApplicationsUpdate.Size = New System.Drawing.Size(78, 27)
    Me.btnApplicationsUpdate.TabIndex = 54
    Me.btnApplicationsUpdate.Text = "Update"
    Me.btnApplicationsUpdate.UseVisualStyleBackColor = True
    '
    'lblApplications
    '
    Me.lblApplications.AutoSize = True
    Me.lblApplications.Location = New System.Drawing.Point(30, 38)
    Me.lblApplications.Name = "lblApplications"
    Me.lblApplications.Size = New System.Drawing.Size(83, 19)
    Me.lblApplications.TabIndex = 55
    Me.lblApplications.Text = "Applications"
    '
    'txtNextSMSPasswordHashed
    '
    Me.txtNextSMSPasswordHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNextSMSPasswordHashed.Location = New System.Drawing.Point(73, 57)
    Me.txtNextSMSPasswordHashed.Name = "txtNextSMSPasswordHashed"
    Me.txtNextSMSPasswordHashed.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtNextSMSPasswordHashed.Size = New System.Drawing.Size(206, 25)
    Me.txtNextSMSPasswordHashed.TabIndex = 60
    Me.txtNextSMSPasswordHashed.Text = "txtNextSMSPasswordHashed"
    Me.txtNextSMSPasswordHashed.UseSystemPasswordChar = True
    '
    'btnNextSMSPasswordHashedUpdate
    '
    Me.btnNextSMSPasswordHashedUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnNextSMSPasswordHashedUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnNextSMSPasswordHashedUpdate.Location = New System.Drawing.Point(131, 31)
    Me.btnNextSMSPasswordHashedUpdate.Name = "btnNextSMSPasswordHashedUpdate"
    Me.btnNextSMSPasswordHashedUpdate.Size = New System.Drawing.Size(75, 20)
    Me.btnNextSMSPasswordHashedUpdate.TabIndex = 61
    Me.btnNextSMSPasswordHashedUpdate.Text = "Update"
    Me.btnNextSMSPasswordHashedUpdate.UseVisualStyleBackColor = True
    '
    'lblNextSMSPasswordHashed
    '
    Me.lblNextSMSPasswordHashed.AutoSize = True
    Me.lblNextSMSPasswordHashed.Location = New System.Drawing.Point(-251, 31)
    Me.lblNextSMSPasswordHashed.Name = "lblNextSMSPasswordHashed"
    Me.lblNextSMSPasswordHashed.Size = New System.Drawing.Size(130, 19)
    Me.lblNextSMSPasswordHashed.TabIndex = 62
    Me.lblNextSMSPasswordHashed.Text = "Next SMS Password"
    '
    'lblRole
    '
    Me.lblRole.AutoSize = True
    Me.lblRole.Location = New System.Drawing.Point(13, 114)
    Me.lblRole.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblRole.Name = "lblRole"
    Me.lblRole.Size = New System.Drawing.Size(35, 19)
    Me.lblRole.TabIndex = 55
    Me.lblRole.Text = "Role"
    '
    'txtRole
    '
    Me.txtRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRole.Location = New System.Drawing.Point(93, 111)
    Me.txtRole.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtRole.Name = "txtRole"
    Me.txtRole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtRole.Size = New System.Drawing.Size(276, 25)
    Me.txtRole.TabIndex = 53
    Me.txtRole.Text = "txtUserRole"
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(388, 541)
    Me.btnEdit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 64
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 541)
    Me.btnAdd.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 65
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 529)
    Me.btnCancel.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 66
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(388, 529)
    Me.btnUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 67
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 541)
    Me.btnDelete.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 63
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'tbcUser
    '
    Me.tbcUser.Controls.Add(Me.tbpInfo)
    Me.tbcUser.Controls.Add(Me.tbpAccess)
    Me.tbcUser.Controls.Add(Me.tbpPasswordsCodes)
    Me.tbcUser.Controls.Add(Me.tbpComments)
    Me.tbcUser.Dock = System.Windows.Forms.DockStyle.Top
    Me.tbcUser.Location = New System.Drawing.Point(0, 0)
    Me.tbcUser.Name = "tbcUser"
    Me.tbcUser.SelectedIndex = 0
    Me.tbcUser.Size = New System.Drawing.Size(757, 520)
    Me.tbcUser.TabIndex = 68
    '
    'tbpInfo
    '
    Me.tbpInfo.BackColor = System.Drawing.Color.Wheat
    Me.tbpInfo.Controls.Add(Me.tlp1)
    Me.tbpInfo.Location = New System.Drawing.Point(4, 26)
    Me.tbpInfo.Name = "tbpInfo"
    Me.tbpInfo.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpInfo.Size = New System.Drawing.Size(749, 490)
    Me.tbpInfo.TabIndex = 0
    Me.tbpInfo.Text = "Info"
    '
    'tlp1
    '
    Me.tlp1.ColumnCount = 2
    Me.tlp1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlp1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlp1.Controls.Add(Me.Panel1, 0, 0)
    Me.tlp1.Controls.Add(Me.Panel2, 1, 0)
    Me.tlp1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tlp1.Location = New System.Drawing.Point(3, 3)
    Me.tlp1.Name = "tlp1"
    Me.tlp1.RowCount = 1
    Me.tlp1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.tlp1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 484.0!))
    Me.tlp1.Size = New System.Drawing.Size(743, 484)
    Me.tlp1.TabIndex = 0
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.GroupBox7)
    Me.Panel1.Controls.Add(Me.GroupBox3)
    Me.Panel1.Controls.Add(Me.GroupBox1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(3, 3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel1.Size = New System.Drawing.Size(365, 478)
    Me.Panel1.TabIndex = 2
    '
    'GroupBox7
    '
    Me.GroupBox7.Controls.Add(Me.txtLastSuccessfulLogin)
    Me.GroupBox7.Controls.Add(Me.txtDateActivated)
    Me.GroupBox7.Controls.Add(Me.lblIsLockedOut)
    Me.GroupBox7.Controls.Add(Me.lblIsDisabled)
    Me.GroupBox7.Controls.Add(Me.chkIsLockedOut)
    Me.GroupBox7.Controls.Add(Me.lblLastSuccessfulLogin)
    Me.GroupBox7.Controls.Add(Me.chkIsDisabled)
    Me.GroupBox7.Controls.Add(Me.lblDateActivated)
    Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox7.Location = New System.Drawing.Point(5, 362)
    Me.GroupBox7.Name = "GroupBox7"
    Me.GroupBox7.Size = New System.Drawing.Size(355, 108)
    Me.GroupBox7.TabIndex = 2
    Me.GroupBox7.TabStop = False
    Me.GroupBox7.Text = "Status"
    '
    'txtLastSuccessfulLogin
    '
    Me.txtLastSuccessfulLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastSuccessfulLogin.Location = New System.Drawing.Point(189, 43)
    Me.txtLastSuccessfulLogin.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastSuccessfulLogin.Name = "txtLastSuccessfulLogin"
    Me.txtLastSuccessfulLogin.Size = New System.Drawing.Size(159, 25)
    Me.txtLastSuccessfulLogin.TabIndex = 41
    Me.txtLastSuccessfulLogin.Tag = "dd-MM-yyyy HH:mm"
    Me.txtLastSuccessfulLogin.Text = "txtLastSuccessfulLogin"
    '
    'lblIsLockedOut
    '
    Me.lblIsLockedOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblIsLockedOut.AutoSize = True
    Me.lblIsLockedOut.Location = New System.Drawing.Point(185, 80)
    Me.lblIsLockedOut.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblIsLockedOut.Name = "lblIsLockedOut"
    Me.lblIsLockedOut.Size = New System.Drawing.Size(80, 19)
    Me.lblIsLockedOut.TabIndex = 44
    Me.lblIsLockedOut.Text = "Locked Out"
    '
    'chkIsLockedOut
    '
    Me.chkIsLockedOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.chkIsLockedOut.AutoSize = True
    Me.chkIsLockedOut.Location = New System.Drawing.Point(271, 83)
    Me.chkIsLockedOut.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkIsLockedOut.Name = "chkIsLockedOut"
    Me.chkIsLockedOut.Size = New System.Drawing.Size(15, 14)
    Me.chkIsLockedOut.TabIndex = 43
    Me.chkIsLockedOut.UseVisualStyleBackColor = True
    '
    'lblLastSuccessfulLogin
    '
    Me.lblLastSuccessfulLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblLastSuccessfulLogin.AutoSize = True
    Me.lblLastSuccessfulLogin.Location = New System.Drawing.Point(185, 21)
    Me.lblLastSuccessfulLogin.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLastSuccessfulLogin.Name = "lblLastSuccessfulLogin"
    Me.lblLastSuccessfulLogin.Size = New System.Drawing.Size(137, 19)
    Me.lblLastSuccessfulLogin.TabIndex = 42
    Me.lblLastSuccessfulLogin.Text = "Last Successful Login"
    '
    'GroupBox3
    '
    Me.GroupBox3.Controls.Add(Me.txtType)
    Me.GroupBox3.Controls.Add(Me.lblIDinType)
    Me.GroupBox3.Controls.Add(Me.cboType)
    Me.GroupBox3.Controls.Add(Me.txtIDinType)
    Me.GroupBox3.Controls.Add(Me.lblType)
    Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox3.Location = New System.Drawing.Point(5, 244)
    Me.GroupBox3.Name = "GroupBox3"
    Me.GroupBox3.Size = New System.Drawing.Size(355, 118)
    Me.GroupBox3.TabIndex = 1
    Me.GroupBox3.TabStop = False
    Me.GroupBox3.Text = "Type"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.txtNationalIDNo)
    Me.GroupBox1.Controls.Add(Me.txtUserName)
    Me.GroupBox1.Controls.Add(Me.lblNationalIDNo)
    Me.GroupBox1.Controls.Add(Me.lblUserName)
    Me.GroupBox1.Controls.Add(Me.txtLastName)
    Me.GroupBox1.Controls.Add(Me.lblFirstName)
    Me.GroupBox1.Controls.Add(Me.lblLastName)
    Me.GroupBox1.Controls.Add(Me.lblFullName)
    Me.GroupBox1.Controls.Add(Me.txtFirstName)
    Me.GroupBox1.Controls.Add(Me.txtFullName)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(355, 239)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Identification"
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.GroupBox4)
    Me.Panel2.Controls.Add(Me.GroupBox2)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(374, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel2.Size = New System.Drawing.Size(366, 478)
    Me.Panel2.TabIndex = 3
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.cboMessagingMode)
    Me.GroupBox4.Controls.Add(Me.txtMessagingMode)
    Me.GroupBox4.Controls.Add(Me.lblMessagingMode)
    Me.GroupBox4.Controls.Add(Me.cboLanguage)
    Me.GroupBox4.Controls.Add(Me.txtPhoneNumber)
    Me.GroupBox4.Controls.Add(Me.txtEmail)
    Me.GroupBox4.Controls.Add(Me.lblEmail)
    Me.GroupBox4.Controls.Add(Me.txtLanguage)
    Me.GroupBox4.Controls.Add(Me.lblLanguage)
    Me.GroupBox4.Controls.Add(Me.lblPhoneNumber)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(5, 267)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(356, 207)
    Me.GroupBox4.TabIndex = 3
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "Contact"
    '
    'cboMessagingMode
    '
    Me.cboMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboMessagingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboMessagingMode.FormattingEnabled = True
    Me.cboMessagingMode.Location = New System.Drawing.Point(139, 151)
    Me.cboMessagingMode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboMessagingMode.Name = "cboMessagingMode"
    Me.cboMessagingMode.Size = New System.Drawing.Size(166, 25)
    Me.cboMessagingMode.TabIndex = 70
    '
    'txtMessagingMode
    '
    Me.txtMessagingMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMessagingMode.Location = New System.Drawing.Point(153, 156)
    Me.txtMessagingMode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtMessagingMode.Name = "txtMessagingMode"
    Me.txtMessagingMode.Size = New System.Drawing.Size(188, 25)
    Me.txtMessagingMode.TabIndex = 71
    Me.txtMessagingMode.Text = "txtMessagingMode"
    '
    'lblMessagingMode
    '
    Me.lblMessagingMode.AutoSize = True
    Me.lblMessagingMode.Location = New System.Drawing.Point(13, 159)
    Me.lblMessagingMode.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblMessagingMode.Name = "lblMessagingMode"
    Me.lblMessagingMode.Size = New System.Drawing.Size(115, 19)
    Me.lblMessagingMode.TabIndex = 72
    Me.lblMessagingMode.Text = "Messaging Mode"
    '
    'cboLanguage
    '
    Me.cboLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLanguage.FormattingEnabled = True
    Me.cboLanguage.Location = New System.Drawing.Point(122, 113)
    Me.cboLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboLanguage.Name = "cboLanguage"
    Me.cboLanguage.Size = New System.Drawing.Size(166, 25)
    Me.cboLanguage.TabIndex = 67
    '
    'txtLanguage
    '
    Me.txtLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLanguage.Location = New System.Drawing.Point(153, 116)
    Me.txtLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLanguage.Name = "txtLanguage"
    Me.txtLanguage.Size = New System.Drawing.Size(188, 25)
    Me.txtLanguage.TabIndex = 68
    Me.txtLanguage.Text = "txtLanguage"
    '
    'lblLanguage
    '
    Me.lblLanguage.AutoSize = True
    Me.lblLanguage.Location = New System.Drawing.Point(13, 119)
    Me.lblLanguage.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLanguage.Name = "lblLanguage"
    Me.lblLanguage.Size = New System.Drawing.Size(69, 19)
    Me.lblLanguage.TabIndex = 69
    Me.lblLanguage.Text = "Language"
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.txtPostalCode)
    Me.GroupBox2.Controls.Add(Me.txtAddress)
    Me.GroupBox2.Controls.Add(Me.lblTester)
    Me.GroupBox2.Controls.Add(Me.lblAddress)
    Me.GroupBox2.Controls.Add(Me.lblCountry)
    Me.GroupBox2.Controls.Add(Me.txtCity)
    Me.GroupBox2.Controls.Add(Me.txtCountry)
    Me.GroupBox2.Controls.Add(Me.lblCity)
    Me.GroupBox2.Controls.Add(Me.lblPostalCode)
    Me.GroupBox2.Controls.Add(Me.txtProvinceState)
    Me.GroupBox2.Controls.Add(Me.lblProvinceState)
    Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(356, 262)
    Me.GroupBox2.TabIndex = 1
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Domicile"
    '
    'tbpAccess
    '
    Me.tbpAccess.BackColor = System.Drawing.Color.Wheat
    Me.tbpAccess.Controls.Add(Me.gpbUserPermissionColForUser)
    Me.tbpAccess.Controls.Add(Me.tlpSecurity)
    Me.tbpAccess.Location = New System.Drawing.Point(4, 26)
    Me.tbpAccess.Name = "tbpAccess"
    Me.tbpAccess.Padding = New System.Windows.Forms.Padding(5)
    Me.tbpAccess.Size = New System.Drawing.Size(749, 490)
    Me.tbpAccess.TabIndex = 1
    Me.tbpAccess.Text = "Access"
    '
    'gpbUserPermissionColForUser
    '
    Me.gpbUserPermissionColForUser.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbUserPermissionColForUser.Location = New System.Drawing.Point(5, 176)
    Me.gpbUserPermissionColForUser.Name = "gpbUserPermissionColForUser"
    Me.gpbUserPermissionColForUser.Size = New System.Drawing.Size(739, 309)
    Me.gpbUserPermissionColForUser.TabIndex = 73
    Me.gpbUserPermissionColForUser.TabStop = False
    Me.gpbUserPermissionColForUser.Text = "Computer Permissions"
    '
    'tlpSecurity
    '
    Me.tlpSecurity.ColumnCount = 2
    Me.tlpSecurity.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.98338!))
    Me.tlpSecurity.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.01662!))
    Me.tlpSecurity.Controls.Add(Me.Panel3, 0, 0)
    Me.tlpSecurity.Controls.Add(Me.Panel4, 1, 0)
    Me.tlpSecurity.Dock = System.Windows.Forms.DockStyle.Top
    Me.tlpSecurity.Location = New System.Drawing.Point(5, 5)
    Me.tlpSecurity.Name = "tlpSecurity"
    Me.tlpSecurity.RowCount = 1
    Me.tlpSecurity.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.tlpSecurity.Size = New System.Drawing.Size(739, 171)
    Me.tlpSecurity.TabIndex = 1
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.gpbSecurity)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel3.Location = New System.Drawing.Point(3, 3)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel3.Size = New System.Drawing.Size(333, 165)
    Me.Panel3.TabIndex = 2
    '
    'gpbSecurity
    '
    Me.gpbSecurity.Controls.Add(Me.lblRequiresFixedIP)
    Me.gpbSecurity.Controls.Add(Me.chkRequiresFixedIP)
    Me.gpbSecurity.Controls.Add(Me.lblRequiresComputerIdentification)
    Me.gpbSecurity.Controls.Add(Me.lblEnableSimultaneousLogins)
    Me.gpbSecurity.Controls.Add(Me.chkEnableSimultaneousLogins)
    Me.gpbSecurity.Controls.Add(Me.chkRequiresComputerIdentification)
    Me.gpbSecurity.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbSecurity.Location = New System.Drawing.Point(5, 5)
    Me.gpbSecurity.Name = "gpbSecurity"
    Me.gpbSecurity.Size = New System.Drawing.Size(323, 155)
    Me.gpbSecurity.TabIndex = 0
    Me.gpbSecurity.TabStop = False
    Me.gpbSecurity.Text = "Security"
    '
    'lblRequiresFixedIP
    '
    Me.lblRequiresFixedIP.AutoSize = True
    Me.lblRequiresFixedIP.Location = New System.Drawing.Point(13, 93)
    Me.lblRequiresFixedIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblRequiresFixedIP.Name = "lblRequiresFixedIP"
    Me.lblRequiresFixedIP.Size = New System.Drawing.Size(106, 19)
    Me.lblRequiresFixedIP.TabIndex = 58
    Me.lblRequiresFixedIP.Text = "Require Fixed IP"
    '
    'chkRequiresFixedIP
    '
    Me.chkRequiresFixedIP.AutoSize = True
    Me.chkRequiresFixedIP.Location = New System.Drawing.Point(254, 100)
    Me.chkRequiresFixedIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkRequiresFixedIP.Name = "chkRequiresFixedIP"
    Me.chkRequiresFixedIP.Size = New System.Drawing.Size(15, 14)
    Me.chkRequiresFixedIP.TabIndex = 57
    Me.chkRequiresFixedIP.UseVisualStyleBackColor = True
    '
    'Panel4
    '
    Me.Panel4.Controls.Add(Me.gpbApplicationsRoles)
    Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel4.Location = New System.Drawing.Point(342, 3)
    Me.Panel4.Name = "Panel4"
    Me.Panel4.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel4.Size = New System.Drawing.Size(394, 165)
    Me.Panel4.TabIndex = 3
    '
    'gpbApplicationsRoles
    '
    Me.gpbApplicationsRoles.BackColor = System.Drawing.Color.Wheat
    Me.gpbApplicationsRoles.Controls.Add(Me.cboRole)
    Me.gpbApplicationsRoles.Controls.Add(Me.btnApplicationsUpdate)
    Me.gpbApplicationsRoles.Controls.Add(Me.txtApplications)
    Me.gpbApplicationsRoles.Controls.Add(Me.txtRole)
    Me.gpbApplicationsRoles.Controls.Add(Me.lblRole)
    Me.gpbApplicationsRoles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbApplicationsRoles.Location = New System.Drawing.Point(5, 5)
    Me.gpbApplicationsRoles.Name = "gpbApplicationsRoles"
    Me.gpbApplicationsRoles.Size = New System.Drawing.Size(384, 155)
    Me.gpbApplicationsRoles.TabIndex = 1
    Me.gpbApplicationsRoles.TabStop = False
    Me.gpbApplicationsRoles.Text = "Applications"
    '
    'cboRole
    '
    Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
    Me.cboRole.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.cboRole.Location = New System.Drawing.Point(91, 97)
    Me.cboRole.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboRole.Name = "cboRole"
    Me.cboRole.Size = New System.Drawing.Size(137, 25)
    Me.cboRole.TabIndex = 56
    '
    'tbpPasswordsCodes
    '
    Me.tbpPasswordsCodes.BackColor = System.Drawing.Color.Wheat
    Me.tbpPasswordsCodes.Controls.Add(Me.gpbApplicationLoginKeys)
    Me.tbpPasswordsCodes.Controls.Add(Me.tlpPasswordMFA)
    Me.tbpPasswordsCodes.Location = New System.Drawing.Point(4, 26)
    Me.tbpPasswordsCodes.Name = "tbpPasswordsCodes"
    Me.tbpPasswordsCodes.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpPasswordsCodes.Size = New System.Drawing.Size(749, 490)
    Me.tbpPasswordsCodes.TabIndex = 3
    Me.tbpPasswordsCodes.Text = "Passwords && Codes"
    '
    'gpbApplicationLoginKeys
    '
    Me.gpbApplicationLoginKeys.Controls.Add(Me.TableLayoutPanel1)
    Me.gpbApplicationLoginKeys.Controls.Add(Me.MyCtlUserLoginKeyColForUser)
    Me.gpbApplicationLoginKeys.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbApplicationLoginKeys.Location = New System.Drawing.Point(3, 267)
    Me.gpbApplicationLoginKeys.Name = "gpbApplicationLoginKeys"
    Me.gpbApplicationLoginKeys.Size = New System.Drawing.Size(743, 220)
    Me.gpbApplicationLoginKeys.TabIndex = 75
    Me.gpbApplicationLoginKeys.TabStop = False
    Me.gpbApplicationLoginKeys.Text = "'Biometric' Login Keys"
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.btnDeletePIN, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btnDeleteAllKeys, 0, 0)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 189)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(737, 28)
    Me.TableLayoutPanel1.TabIndex = 66
    '
    'btnDeletePIN
    '
    Me.btnDeletePIN.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnDeletePIN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDeletePIN.Location = New System.Drawing.Point(368, 0)
    Me.btnDeletePIN.Margin = New System.Windows.Forms.Padding(0)
    Me.btnDeletePIN.Name = "btnDeletePIN"
    Me.btnDeletePIN.Size = New System.Drawing.Size(369, 28)
    Me.btnDeletePIN.TabIndex = 66
    Me.btnDeletePIN.Text = "Delete PIN"
    Me.btnDeletePIN.UseVisualStyleBackColor = True
    '
    'btnDeleteAllKeys
    '
    Me.btnDeleteAllKeys.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnDeleteAllKeys.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDeleteAllKeys.Location = New System.Drawing.Point(0, 0)
    Me.btnDeleteAllKeys.Margin = New System.Windows.Forms.Padding(0)
    Me.btnDeleteAllKeys.Name = "btnDeleteAllKeys"
    Me.btnDeleteAllKeys.Size = New System.Drawing.Size(368, 28)
    Me.btnDeleteAllKeys.TabIndex = 65
    Me.btnDeleteAllKeys.Text = "Delete All Keys"
    Me.btnDeleteAllKeys.UseVisualStyleBackColor = True
    '
    'MyCtlUserLoginKeyColForUser
    '
    Me.MyCtlUserLoginKeyColForUser.Dock = System.Windows.Forms.DockStyle.Top
    Me.MyCtlUserLoginKeyColForUser.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.MyCtlUserLoginKeyColForUser.Location = New System.Drawing.Point(3, 21)
    Me.MyCtlUserLoginKeyColForUser.Name = "MyCtlUserLoginKeyColForUser"
    Me.MyCtlUserLoginKeyColForUser.Size = New System.Drawing.Size(737, 168)
    Me.MyCtlUserLoginKeyColForUser.TabIndex = 0
    '
    'tlpPasswordMFA
    '
    Me.tlpPasswordMFA.ColumnCount = 2
    Me.tlpPasswordMFA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlpPasswordMFA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlpPasswordMFA.Controls.Add(Me.Panel5, 0, 0)
    Me.tlpPasswordMFA.Controls.Add(Me.Panel6, 1, 0)
    Me.tlpPasswordMFA.Dock = System.Windows.Forms.DockStyle.Top
    Me.tlpPasswordMFA.Location = New System.Drawing.Point(3, 3)
    Me.tlpPasswordMFA.Name = "tlpPasswordMFA"
    Me.tlpPasswordMFA.RowCount = 1
    Me.tlpPasswordMFA.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.tlpPasswordMFA.Size = New System.Drawing.Size(743, 264)
    Me.tlpPasswordMFA.TabIndex = 74
    '
    'Panel5
    '
    Me.Panel5.Controls.Add(Me.gpb2FactorAuthentication)
    Me.Panel5.Controls.Add(Me.gpbPassword)
    Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel5.Location = New System.Drawing.Point(3, 3)
    Me.Panel5.Name = "Panel5"
    Me.Panel5.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel5.Size = New System.Drawing.Size(365, 258)
    Me.Panel5.TabIndex = 2
    '
    'gpb2FactorAuthentication
    '
    Me.gpb2FactorAuthentication.Controls.Add(Me.lblLoggedInIP)
    Me.gpb2FactorAuthentication.Controls.Add(Me.cboAuthenticationMethod)
    Me.gpb2FactorAuthentication.Controls.Add(Me.lblAuthenticationMethod)
    Me.gpb2FactorAuthentication.Controls.Add(Me.txtLoggedInIP)
    Me.gpb2FactorAuthentication.Controls.Add(Me.txtAuthenticationMethod)
    Me.gpb2FactorAuthentication.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpb2FactorAuthentication.Location = New System.Drawing.Point(5, 149)
    Me.gpb2FactorAuthentication.Name = "gpb2FactorAuthentication"
    Me.gpb2FactorAuthentication.Size = New System.Drawing.Size(355, 104)
    Me.gpb2FactorAuthentication.TabIndex = 2
    Me.gpb2FactorAuthentication.TabStop = False
    Me.gpb2FactorAuthentication.Text = "Multi-Factor Authentication"
    '
    'lblLoggedInIP
    '
    Me.lblLoggedInIP.AutoSize = True
    Me.lblLoggedInIP.Location = New System.Drawing.Point(13, 71)
    Me.lblLoggedInIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLoggedInIP.Name = "lblLoggedInIP"
    Me.lblLoggedInIP.Size = New System.Drawing.Size(82, 19)
    Me.lblLoggedInIP.TabIndex = 23
    Me.lblLoggedInIP.Text = "Loggedin IP"
    '
    'cboAuthenticationMethod
    '
    Me.cboAuthenticationMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboAuthenticationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboAuthenticationMethod.FormattingEnabled = True
    Me.cboAuthenticationMethod.Location = New System.Drawing.Point(190, 10)
    Me.cboAuthenticationMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboAuthenticationMethod.Name = "cboAuthenticationMethod"
    Me.cboAuthenticationMethod.Size = New System.Drawing.Size(59, 25)
    Me.cboAuthenticationMethod.TabIndex = 73
    '
    'lblAuthenticationMethod
    '
    Me.lblAuthenticationMethod.AutoSize = True
    Me.lblAuthenticationMethod.Location = New System.Drawing.Point(13, 31)
    Me.lblAuthenticationMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblAuthenticationMethod.Name = "lblAuthenticationMethod"
    Me.lblAuthenticationMethod.Size = New System.Drawing.Size(152, 19)
    Me.lblAuthenticationMethod.TabIndex = 75
    Me.lblAuthenticationMethod.Text = "Authentication Method"
    '
    'txtLoggedInIP
    '
    Me.txtLoggedInIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoggedInIP.Location = New System.Drawing.Point(120, 68)
    Me.txtLoggedInIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLoggedInIP.Name = "txtLoggedInIP"
    Me.txtLoggedInIP.Size = New System.Drawing.Size(217, 25)
    Me.txtLoggedInIP.TabIndex = 22
    Me.txtLoggedInIP.Text = "txtLoggedInIP"
    '
    'txtAuthenticationMethod
    '
    Me.txtAuthenticationMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAuthenticationMethod.Location = New System.Drawing.Point(190, 28)
    Me.txtAuthenticationMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtAuthenticationMethod.Name = "txtAuthenticationMethod"
    Me.txtAuthenticationMethod.Size = New System.Drawing.Size(147, 25)
    Me.txtAuthenticationMethod.TabIndex = 74
    Me.txtAuthenticationMethod.Text = "txtAuthenticationMethod"
    '
    'gpbPassword
    '
    Me.gpbPassword.Controls.Add(Me.chkPasswordNeverExpires)
    Me.gpbPassword.Controls.Add(Me.btnPasswordHashedUpdate)
    Me.gpbPassword.Controls.Add(Me.txtDatePasswordChanged)
    Me.gpbPassword.Controls.Add(Me.lblPasswordNeverExpires)
    Me.gpbPassword.Controls.Add(Me.lblDatePasswordChanged)
    Me.gpbPassword.Controls.Add(Me.txtExpiryDate)
    Me.gpbPassword.Controls.Add(Me.lblExpiryDate)
    Me.gpbPassword.Controls.Add(Me.dtpExpiryDate)
    Me.gpbPassword.Dock = System.Windows.Forms.DockStyle.Top
    Me.gpbPassword.Location = New System.Drawing.Point(5, 5)
    Me.gpbPassword.Name = "gpbPassword"
    Me.gpbPassword.Size = New System.Drawing.Size(355, 144)
    Me.gpbPassword.TabIndex = 59
    Me.gpbPassword.TabStop = False
    Me.gpbPassword.Text = "Password"
    '
    'chkPasswordNeverExpires
    '
    Me.chkPasswordNeverExpires.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.chkPasswordNeverExpires.AutoSize = True
    Me.chkPasswordNeverExpires.Location = New System.Drawing.Point(322, 65)
    Me.chkPasswordNeverExpires.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkPasswordNeverExpires.Name = "chkPasswordNeverExpires"
    Me.chkPasswordNeverExpires.Size = New System.Drawing.Size(15, 14)
    Me.chkPasswordNeverExpires.TabIndex = 58
    Me.chkPasswordNeverExpires.UseVisualStyleBackColor = True
    '
    'lblPasswordNeverExpires
    '
    Me.lblPasswordNeverExpires.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblPasswordNeverExpires.AutoSize = True
    Me.lblPasswordNeverExpires.Location = New System.Drawing.Point(274, 64)
    Me.lblPasswordNeverExpires.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblPasswordNeverExpires.Name = "lblPasswordNeverExpires"
    Me.lblPasswordNeverExpires.Size = New System.Drawing.Size(45, 19)
    Me.lblPasswordNeverExpires.TabIndex = 38
    Me.lblPasswordNeverExpires.Text = "Never"
    '
    'Panel6
    '
    Me.Panel6.Controls.Add(Me.gpbIdentityVerificationQuestions)
    Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel6.Location = New System.Drawing.Point(374, 3)
    Me.Panel6.Name = "Panel6"
    Me.Panel6.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel6.Size = New System.Drawing.Size(366, 258)
    Me.Panel6.TabIndex = 3
    '
    'gpbIdentityVerificationQuestions
    '
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.btnViewHideSecurityQuestionResponse)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion3)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion3Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion3)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion2Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion3Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion1Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion2Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion2)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion1Response)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion2)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.lblSecurityQuestion1)
    Me.gpbIdentityVerificationQuestions.Controls.Add(Me.txtSecurityQuestion1)
    Me.gpbIdentityVerificationQuestions.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gpbIdentityVerificationQuestions.Location = New System.Drawing.Point(5, 5)
    Me.gpbIdentityVerificationQuestions.Name = "gpbIdentityVerificationQuestions"
    Me.gpbIdentityVerificationQuestions.Size = New System.Drawing.Size(356, 248)
    Me.gpbIdentityVerificationQuestions.TabIndex = 60
    Me.gpbIdentityVerificationQuestions.TabStop = False
    Me.gpbIdentityVerificationQuestions.Text = "Identity Verification Questions"
    '
    'btnViewHideSecurityQuestionResponse
    '
    Me.btnViewHideSecurityQuestionResponse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnViewHideSecurityQuestionResponse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnViewHideSecurityQuestionResponse.Location = New System.Drawing.Point(291, 7)
    Me.btnViewHideSecurityQuestionResponse.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnViewHideSecurityQuestionResponse.Name = "btnViewHideSecurityQuestionResponse"
    Me.btnViewHideSecurityQuestionResponse.Size = New System.Drawing.Size(61, 25)
    Me.btnViewHideSecurityQuestionResponse.TabIndex = 59
    Me.btnViewHideSecurityQuestionResponse.Text = "View"
    Me.btnViewHideSecurityQuestionResponse.UseVisualStyleBackColor = True
    '
    'lblSecurityQuestion3
    '
    Me.lblSecurityQuestion3.AutoSize = True
    Me.lblSecurityQuestion3.Location = New System.Drawing.Point(13, 185)
    Me.lblSecurityQuestion3.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion3.Name = "lblSecurityQuestion3"
    Me.lblSecurityQuestion3.Size = New System.Drawing.Size(77, 19)
    Me.lblSecurityQuestion3.TabIndex = 84
    Me.lblSecurityQuestion3.Text = "Question 3"
    '
    'lblSecurityQuestion3Response
    '
    Me.lblSecurityQuestion3Response.AutoSize = True
    Me.lblSecurityQuestion3Response.Location = New System.Drawing.Point(23, 218)
    Me.lblSecurityQuestion3Response.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion3Response.Name = "lblSecurityQuestion3Response"
    Me.lblSecurityQuestion3Response.Size = New System.Drawing.Size(67, 19)
    Me.lblSecurityQuestion3Response.TabIndex = 81
    Me.lblSecurityQuestion3Response.Text = "Response"
    '
    'txtSecurityQuestion3
    '
    Me.txtSecurityQuestion3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion3.Location = New System.Drawing.Point(115, 182)
    Me.txtSecurityQuestion3.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion3.Name = "txtSecurityQuestion3"
    Me.txtSecurityQuestion3.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion3.TabIndex = 83
    Me.txtSecurityQuestion3.Text = "txtSecurityQuestion3"
    '
    'lblSecurityQuestion2Response
    '
    Me.lblSecurityQuestion2Response.AutoSize = True
    Me.lblSecurityQuestion2Response.Location = New System.Drawing.Point(23, 145)
    Me.lblSecurityQuestion2Response.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion2Response.Name = "lblSecurityQuestion2Response"
    Me.lblSecurityQuestion2Response.Size = New System.Drawing.Size(67, 19)
    Me.lblSecurityQuestion2Response.TabIndex = 81
    Me.lblSecurityQuestion2Response.Text = "Response"
    '
    'txtSecurityQuestion3Response
    '
    Me.txtSecurityQuestion3Response.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion3Response.Location = New System.Drawing.Point(115, 215)
    Me.txtSecurityQuestion3Response.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion3Response.Name = "txtSecurityQuestion3Response"
    Me.txtSecurityQuestion3Response.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion3Response.TabIndex = 80
    Me.txtSecurityQuestion3Response.Text = "txtSecurityQuestion3Response"
    '
    'lblSecurityQuestion1Response
    '
    Me.lblSecurityQuestion1Response.AutoSize = True
    Me.lblSecurityQuestion1Response.Location = New System.Drawing.Point(23, 72)
    Me.lblSecurityQuestion1Response.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion1Response.Name = "lblSecurityQuestion1Response"
    Me.lblSecurityQuestion1Response.Size = New System.Drawing.Size(67, 19)
    Me.lblSecurityQuestion1Response.TabIndex = 81
    Me.lblSecurityQuestion1Response.Text = "Response"
    '
    'txtSecurityQuestion2Response
    '
    Me.txtSecurityQuestion2Response.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion2Response.Location = New System.Drawing.Point(115, 142)
    Me.txtSecurityQuestion2Response.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion2Response.Name = "txtSecurityQuestion2Response"
    Me.txtSecurityQuestion2Response.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion2Response.TabIndex = 80
    Me.txtSecurityQuestion2Response.Text = "txtSecurityQuestion2Response"
    '
    'lblSecurityQuestion2
    '
    Me.lblSecurityQuestion2.AutoSize = True
    Me.lblSecurityQuestion2.Location = New System.Drawing.Point(13, 112)
    Me.lblSecurityQuestion2.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion2.Name = "lblSecurityQuestion2"
    Me.lblSecurityQuestion2.Size = New System.Drawing.Size(77, 19)
    Me.lblSecurityQuestion2.TabIndex = 81
    Me.lblSecurityQuestion2.Text = "Question 2"
    '
    'txtSecurityQuestion1Response
    '
    Me.txtSecurityQuestion1Response.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion1Response.Location = New System.Drawing.Point(115, 69)
    Me.txtSecurityQuestion1Response.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion1Response.Name = "txtSecurityQuestion1Response"
    Me.txtSecurityQuestion1Response.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion1Response.TabIndex = 80
    Me.txtSecurityQuestion1Response.Text = "txtSecurityQuestion1Response"
    '
    'txtSecurityQuestion2
    '
    Me.txtSecurityQuestion2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion2.Location = New System.Drawing.Point(115, 109)
    Me.txtSecurityQuestion2.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion2.Name = "txtSecurityQuestion2"
    Me.txtSecurityQuestion2.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion2.TabIndex = 80
    Me.txtSecurityQuestion2.Text = "txtSecurityQuestion2"
    '
    'lblSecurityQuestion1
    '
    Me.lblSecurityQuestion1.AutoSize = True
    Me.lblSecurityQuestion1.Location = New System.Drawing.Point(13, 39)
    Me.lblSecurityQuestion1.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSecurityQuestion1.Name = "lblSecurityQuestion1"
    Me.lblSecurityQuestion1.Size = New System.Drawing.Size(77, 19)
    Me.lblSecurityQuestion1.TabIndex = 78
    Me.lblSecurityQuestion1.Text = "Question 1"
    '
    'txtSecurityQuestion1
    '
    Me.txtSecurityQuestion1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSecurityQuestion1.Location = New System.Drawing.Point(115, 36)
    Me.txtSecurityQuestion1.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSecurityQuestion1.Name = "txtSecurityQuestion1"
    Me.txtSecurityQuestion1.Size = New System.Drawing.Size(226, 25)
    Me.txtSecurityQuestion1.TabIndex = 77
    Me.txtSecurityQuestion1.Text = "txtSecurityQuestion1"
    '
    'tbpComments
    '
    Me.tbpComments.BackColor = System.Drawing.Color.Wheat
    Me.tbpComments.Controls.Add(Me.pnlComments)
    Me.tbpComments.Location = New System.Drawing.Point(4, 26)
    Me.tbpComments.Name = "tbpComments"
    Me.tbpComments.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpComments.Size = New System.Drawing.Size(749, 490)
    Me.tbpComments.TabIndex = 2
    Me.tbpComments.Text = "Comments"
    '
    'pnlComments
    '
    Me.pnlComments.Controls.Add(Me.lblComments)
    Me.pnlComments.Controls.Add(Me.btnCommentsUpdate)
    Me.pnlComments.Controls.Add(Me.txtComments)
    Me.pnlComments.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlComments.Location = New System.Drawing.Point(3, 3)
    Me.pnlComments.Name = "pnlComments"
    Me.pnlComments.Size = New System.Drawing.Size(743, 484)
    Me.pnlComments.TabIndex = 51
    '
    'lblPasswordHashed
    '
    Me.lblPasswordHashed.AutoSize = True
    Me.lblPasswordHashed.Location = New System.Drawing.Point(174, 94)
    Me.lblPasswordHashed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblPasswordHashed.Name = "lblPasswordHashed"
    Me.lblPasswordHashed.Size = New System.Drawing.Size(67, 19)
    Me.lblPasswordHashed.TabIndex = 28
    Me.lblPasswordHashed.Text = "Password"
    '
    'pnlGarbage
    '
    Me.pnlGarbage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pnlGarbage.Controls.Add(Me.btnPINUpdate)
    Me.pnlGarbage.Controls.Add(Me.cboSecurityQuestion3)
    Me.pnlGarbage.Controls.Add(Me.lblPIN)
    Me.pnlGarbage.Controls.Add(Me.btnSecurityQuestion3ResponseUpdate)
    Me.pnlGarbage.Controls.Add(Me.txtPIN)
    Me.pnlGarbage.Controls.Add(Me.btnLastSuccessfulLoginUpdate)
    Me.pnlGarbage.Controls.Add(Me.btnSecurityQuestion2ResponseUpdate)
    Me.pnlGarbage.Controls.Add(Me.btnLoggedInIPUpdate)
    Me.pnlGarbage.Controls.Add(Me.btnSecurityQuestion1ResponseUpdate)
    Me.pnlGarbage.Controls.Add(Me.lblApprovalFunctionName)
    Me.pnlGarbage.Controls.Add(Me.txtLastPasswords)
    Me.pnlGarbage.Controls.Add(Me.lblLastPasswords)
    Me.pnlGarbage.Controls.Add(Me.txtPasswordHashed)
    Me.pnlGarbage.Controls.Add(Me.txtApprovalTime)
    Me.pnlGarbage.Controls.Add(Me.cboSecurityQuestion2)
    Me.pnlGarbage.Controls.Add(Me.lblApplications)
    Me.pnlGarbage.Controls.Add(Me.txtApprovalFunctionName)
    Me.pnlGarbage.Controls.Add(Me.cboSecurityQuestion1)
    Me.pnlGarbage.Controls.Add(Me.txtNextSMSPasswordHashed)
    Me.pnlGarbage.Controls.Add(Me.lblApprovalTime)
    Me.pnlGarbage.Controls.Add(Me.lblNextSMSPasswordHashed)
    Me.pnlGarbage.Controls.Add(Me.btnNextSMSPasswordHashedUpdate)
    Me.pnlGarbage.Controls.Add(Me.dtpApprovalTime)
    Me.pnlGarbage.Controls.Add(Me.lblPasswordHashed)
    Me.pnlGarbage.Controls.Add(Me.lblApprovalCodeHashed)
    Me.pnlGarbage.Controls.Add(Me.txtApprovalCodeHashed)
    Me.pnlGarbage.Location = New System.Drawing.Point(19, 435)
    Me.pnlGarbage.Name = "pnlGarbage"
    Me.pnlGarbage.Size = New System.Drawing.Size(313, 114)
    Me.pnlGarbage.TabIndex = 69
    Me.pnlGarbage.Visible = False
    '
    'btnPINUpdate
    '
    Me.btnPINUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnPINUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnPINUpdate.Location = New System.Drawing.Point(166, 85)
    Me.btnPINUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnPINUpdate.Name = "btnPINUpdate"
    Me.btnPINUpdate.Size = New System.Drawing.Size(78, 27)
    Me.btnPINUpdate.TabIndex = 86
    Me.btnPINUpdate.Text = "Update"
    Me.btnPINUpdate.UseVisualStyleBackColor = True
    '
    'cboSecurityQuestion3
    '
    Me.cboSecurityQuestion3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboSecurityQuestion3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion3.FormattingEnabled = True
    Me.cboSecurityQuestion3.Location = New System.Drawing.Point(174, 84)
    Me.cboSecurityQuestion3.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboSecurityQuestion3.Name = "cboSecurityQuestion3"
    Me.cboSecurityQuestion3.Size = New System.Drawing.Size(59, 25)
    Me.cboSecurityQuestion3.TabIndex = 82
    '
    'lblPIN
    '
    Me.lblPIN.AutoSize = True
    Me.lblPIN.Location = New System.Drawing.Point(26, 88)
    Me.lblPIN.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblPIN.Name = "lblPIN"
    Me.lblPIN.Size = New System.Drawing.Size(31, 19)
    Me.lblPIN.TabIndex = 83
    Me.lblPIN.Text = "PIN"
    '
    'btnSecurityQuestion3ResponseUpdate
    '
    Me.btnSecurityQuestion3ResponseUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSecurityQuestion3ResponseUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion3ResponseUpdate.Location = New System.Drawing.Point(163, 86)
    Me.btnSecurityQuestion3ResponseUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnSecurityQuestion3ResponseUpdate.Name = "btnSecurityQuestion3ResponseUpdate"
    Me.btnSecurityQuestion3ResponseUpdate.Size = New System.Drawing.Size(78, 27)
    Me.btnSecurityQuestion3ResponseUpdate.TabIndex = 85
    Me.btnSecurityQuestion3ResponseUpdate.Text = "Update"
    Me.btnSecurityQuestion3ResponseUpdate.UseVisualStyleBackColor = True
    '
    'txtPIN
    '
    Me.txtPIN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPIN.Location = New System.Drawing.Point(102, 85)
    Me.txtPIN.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtPIN.Name = "txtPIN"
    Me.txtPIN.Size = New System.Drawing.Size(131, 25)
    Me.txtPIN.TabIndex = 82
    Me.txtPIN.Text = "txtPIN"
    '
    'btnLastSuccessfulLoginUpdate
    '
    Me.btnLastSuccessfulLoginUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnLastSuccessfulLoginUpdate.Location = New System.Drawing.Point(172, 83)
    Me.btnLastSuccessfulLoginUpdate.Name = "btnLastSuccessfulLoginUpdate"
    Me.btnLastSuccessfulLoginUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnLastSuccessfulLoginUpdate.TabIndex = 70
    Me.btnLastSuccessfulLoginUpdate.Text = "Edit"
    Me.btnLastSuccessfulLoginUpdate.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion2ResponseUpdate
    '
    Me.btnSecurityQuestion2ResponseUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSecurityQuestion2ResponseUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion2ResponseUpdate.Location = New System.Drawing.Point(147, 81)
    Me.btnSecurityQuestion2ResponseUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnSecurityQuestion2ResponseUpdate.Name = "btnSecurityQuestion2ResponseUpdate"
    Me.btnSecurityQuestion2ResponseUpdate.Size = New System.Drawing.Size(78, 27)
    Me.btnSecurityQuestion2ResponseUpdate.TabIndex = 85
    Me.btnSecurityQuestion2ResponseUpdate.Text = "Update"
    Me.btnSecurityQuestion2ResponseUpdate.UseVisualStyleBackColor = True
    '
    'btnLoggedInIPUpdate
    '
    Me.btnLoggedInIPUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnLoggedInIPUpdate.Location = New System.Drawing.Point(210, 72)
    Me.btnLoggedInIPUpdate.Name = "btnLoggedInIPUpdate"
    Me.btnLoggedInIPUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnLoggedInIPUpdate.TabIndex = 70
    Me.btnLoggedInIPUpdate.Text = "Edit"
    Me.btnLoggedInIPUpdate.UseVisualStyleBackColor = True
    '
    'btnSecurityQuestion1ResponseUpdate
    '
    Me.btnSecurityQuestion1ResponseUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSecurityQuestion1ResponseUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnSecurityQuestion1ResponseUpdate.Location = New System.Drawing.Point(155, 81)
    Me.btnSecurityQuestion1ResponseUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnSecurityQuestion1ResponseUpdate.Name = "btnSecurityQuestion1ResponseUpdate"
    Me.btnSecurityQuestion1ResponseUpdate.Size = New System.Drawing.Size(78, 27)
    Me.btnSecurityQuestion1ResponseUpdate.TabIndex = 85
    Me.btnSecurityQuestion1ResponseUpdate.Text = "Update"
    Me.btnSecurityQuestion1ResponseUpdate.UseVisualStyleBackColor = True
    '
    'lblApprovalFunctionName
    '
    Me.lblApprovalFunctionName.AutoSize = True
    Me.lblApprovalFunctionName.Location = New System.Drawing.Point(39, 92)
    Me.lblApprovalFunctionName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblApprovalFunctionName.Name = "lblApprovalFunctionName"
    Me.lblApprovalFunctionName.Size = New System.Drawing.Size(153, 19)
    Me.lblApprovalFunctionName.TabIndex = 11
    Me.lblApprovalFunctionName.Text = "ApprovalFunctionName"
    '
    'txtLastPasswords
    '
    Me.txtLastPasswords.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastPasswords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtLastPasswords.Location = New System.Drawing.Point(69, 31)
    Me.txtLastPasswords.Multiline = True
    Me.txtLastPasswords.Name = "txtLastPasswords"
    Me.txtLastPasswords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtLastPasswords.Size = New System.Drawing.Size(172, 35)
    Me.txtLastPasswords.TabIndex = 53
    Me.txtLastPasswords.Text = "txtLastPasswords"
    Me.txtLastPasswords.Visible = False
    '
    'lblLastPasswords
    '
    Me.lblLastPasswords.AutoSize = True
    Me.lblLastPasswords.Location = New System.Drawing.Point(66, 15)
    Me.lblLastPasswords.Name = "lblLastPasswords"
    Me.lblLastPasswords.Size = New System.Drawing.Size(102, 19)
    Me.lblLastPasswords.TabIndex = 54
    Me.lblLastPasswords.Text = "Last Passwords"
    Me.lblLastPasswords.Visible = False
    '
    'txtApprovalTime
    '
    Me.txtApprovalTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApprovalTime.Location = New System.Drawing.Point(132, 1)
    Me.txtApprovalTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApprovalTime.Name = "txtApprovalTime"
    Me.txtApprovalTime.Size = New System.Drawing.Size(109, 25)
    Me.txtApprovalTime.TabIndex = 10
    Me.txtApprovalTime.Text = "txtApprovalTime"
    '
    'cboSecurityQuestion2
    '
    Me.cboSecurityQuestion2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboSecurityQuestion2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion2.FormattingEnabled = True
    Me.cboSecurityQuestion2.Location = New System.Drawing.Point(157, 83)
    Me.cboSecurityQuestion2.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboSecurityQuestion2.Name = "cboSecurityQuestion2"
    Me.cboSecurityQuestion2.Size = New System.Drawing.Size(59, 25)
    Me.cboSecurityQuestion2.TabIndex = 79
    '
    'txtApprovalFunctionName
    '
    Me.txtApprovalFunctionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApprovalFunctionName.Location = New System.Drawing.Point(157, 89)
    Me.txtApprovalFunctionName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApprovalFunctionName.Name = "txtApprovalFunctionName"
    Me.txtApprovalFunctionName.Size = New System.Drawing.Size(117, 25)
    Me.txtApprovalFunctionName.TabIndex = 10
    Me.txtApprovalFunctionName.Text = "txtApprovalFunctionName"
    '
    'cboSecurityQuestion1
    '
    Me.cboSecurityQuestion1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboSecurityQuestion1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSecurityQuestion1.FormattingEnabled = True
    Me.cboSecurityQuestion1.Location = New System.Drawing.Point(147, 83)
    Me.cboSecurityQuestion1.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboSecurityQuestion1.Name = "cboSecurityQuestion1"
    Me.cboSecurityQuestion1.Size = New System.Drawing.Size(59, 25)
    Me.cboSecurityQuestion1.TabIndex = 76
    '
    'lblApprovalTime
    '
    Me.lblApprovalTime.AutoSize = True
    Me.lblApprovalTime.Location = New System.Drawing.Point(36, 21)
    Me.lblApprovalTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblApprovalTime.Name = "lblApprovalTime"
    Me.lblApprovalTime.Size = New System.Drawing.Size(93, 19)
    Me.lblApprovalTime.TabIndex = 47
    Me.lblApprovalTime.Text = "ApprovalTime"
    '
    'dtpApprovalTime
    '
    Me.dtpApprovalTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpApprovalTime.CustomFormat = "dd/MM/yyyy HH:mm:ss"
    Me.dtpApprovalTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpApprovalTime.Location = New System.Drawing.Point(132, 21)
    Me.dtpApprovalTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.dtpApprovalTime.Name = "dtpApprovalTime"
    Me.dtpApprovalTime.ShowCheckBox = True
    Me.dtpApprovalTime.ShowUpDown = True
    Me.dtpApprovalTime.Size = New System.Drawing.Size(144, 25)
    Me.dtpApprovalTime.TabIndex = 45
    '
    'lblApprovalCodeHashed
    '
    Me.lblApprovalCodeHashed.AutoSize = True
    Me.lblApprovalCodeHashed.Location = New System.Drawing.Point(39, 56)
    Me.lblApprovalCodeHashed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblApprovalCodeHashed.Name = "lblApprovalCodeHashed"
    Me.lblApprovalCodeHashed.Size = New System.Drawing.Size(142, 19)
    Me.lblApprovalCodeHashed.TabIndex = 9
    Me.lblApprovalCodeHashed.Text = "ApprovalCodeHashed"
    '
    'txtApprovalCodeHashed
    '
    Me.txtApprovalCodeHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApprovalCodeHashed.Location = New System.Drawing.Point(157, 53)
    Me.txtApprovalCodeHashed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApprovalCodeHashed.Name = "txtApprovalCodeHashed"
    Me.txtApprovalCodeHashed.Size = New System.Drawing.Size(117, 25)
    Me.txtApprovalCodeHashed.TabIndex = 8
    Me.txtApprovalCodeHashed.Text = "txtApprovalCodeHashed"
    '
    'lblTester
    '
    Me.lblTester.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblTester.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Italic)
    Me.lblTester.Location = New System.Drawing.Point(239, 181)
    Me.lblTester.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblTester.Name = "lblTester"
    Me.lblTester.Size = New System.Drawing.Size(105, 32)
    Me.lblTester.TabIndex = 12
    Me.lblTester.Text = "also used for test OTP as tester:xxxxxx"
    Me.lblTester.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'ctlc_User
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoScroll = True
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.Controls.Add(Me.tbcUser)
    Me.Controls.Add(Me.pnlGarbage)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlc_User"
    Me.Size = New System.Drawing.Size(757, 591)
    Me.tbcUser.ResumeLayout(False)
    Me.tbpInfo.ResumeLayout(False)
    Me.tlp1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.GroupBox7.ResumeLayout(False)
    Me.GroupBox7.PerformLayout()
    Me.GroupBox3.ResumeLayout(False)
    Me.GroupBox3.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.GroupBox4.ResumeLayout(False)
    Me.GroupBox4.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.tbpAccess.ResumeLayout(False)
    Me.tlpSecurity.ResumeLayout(False)
    Me.Panel3.ResumeLayout(False)
    Me.gpbSecurity.ResumeLayout(False)
    Me.gpbSecurity.PerformLayout()
    Me.Panel4.ResumeLayout(False)
    Me.gpbApplicationsRoles.ResumeLayout(False)
    Me.gpbApplicationsRoles.PerformLayout()
    Me.tbpPasswordsCodes.ResumeLayout(False)
    Me.gpbApplicationLoginKeys.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.tlpPasswordMFA.ResumeLayout(False)
    Me.Panel5.ResumeLayout(False)
    Me.gpb2FactorAuthentication.ResumeLayout(False)
    Me.gpb2FactorAuthentication.PerformLayout()
    Me.gpbPassword.ResumeLayout(False)
    Me.gpbPassword.PerformLayout()
    Me.Panel6.ResumeLayout(False)
    Me.gpbIdentityVerificationQuestions.ResumeLayout(False)
    Me.gpbIdentityVerificationQuestions.PerformLayout()
    Me.tbpComments.ResumeLayout(False)
    Me.pnlComments.ResumeLayout(False)
    Me.pnlComments.PerformLayout()
    Me.pnlGarbage.ResumeLayout(False)
    Me.pnlGarbage.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents lblUserName As System.Windows.Forms.Label
  Friend WithEvents txtLastName As System.Windows.Forms.TextBox
  Friend WithEvents lblLastName As System.Windows.Forms.Label
  Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
  Friend WithEvents lblFirstName As System.Windows.Forms.Label
  Friend WithEvents txtFullName As System.Windows.Forms.TextBox
  Friend WithEvents lblFullName As System.Windows.Forms.Label
  Friend WithEvents txtNationalIDNo As System.Windows.Forms.TextBox
  Friend WithEvents lblNationalIDNo As System.Windows.Forms.Label
  Friend WithEvents txtAddress As System.Windows.Forms.TextBox
  Friend WithEvents lblAddress As System.Windows.Forms.Label
  Friend WithEvents txtCity As System.Windows.Forms.TextBox
  Friend WithEvents lblCity As System.Windows.Forms.Label
  Friend WithEvents txtProvinceState As System.Windows.Forms.TextBox
  Friend WithEvents lblProvinceState As System.Windows.Forms.Label
  Friend WithEvents txtPostalCode As System.Windows.Forms.TextBox
  Friend WithEvents lblPostalCode As System.Windows.Forms.Label
  Friend WithEvents txtCountry As System.Windows.Forms.TextBox
  Friend WithEvents lblCountry As System.Windows.Forms.Label
  Friend WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblPhoneNumber As System.Windows.Forms.Label
  Friend WithEvents txtEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblEmail As System.Windows.Forms.Label
  Friend WithEvents txtPasswordHashed As System.Windows.Forms.TextBox
  Friend WithEvents btnPasswordHashedUpdate As System.Windows.Forms.Button
  Friend WithEvents txtDatePasswordChanged As System.Windows.Forms.TextBox
  Friend WithEvents lblDatePasswordChanged As System.Windows.Forms.Label
  Friend WithEvents cboType As System.Windows.Forms.ComboBox
  Friend WithEvents txtType As System.Windows.Forms.TextBox
  Friend WithEvents lblType As System.Windows.Forms.Label
  Friend WithEvents txtIDinType As System.Windows.Forms.TextBox
  Friend WithEvents lblIDinType As System.Windows.Forms.Label
  Friend WithEvents chkRequiresComputerIdentification As System.Windows.Forms.CheckBox
  Friend WithEvents lblRequiresComputerIdentification As System.Windows.Forms.Label
  Friend WithEvents chkEnableSimultaneousLogins As System.Windows.Forms.CheckBox
  Friend WithEvents lblEnableSimultaneousLogins As System.Windows.Forms.Label
  Friend WithEvents txtDateActivated As System.Windows.Forms.TextBox
  Friend WithEvents lblDateActivated As System.Windows.Forms.Label
  Friend WithEvents chkIsDisabled As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsDisabled As System.Windows.Forms.Label
  Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtExpiryDate As System.Windows.Forms.TextBox
  Friend WithEvents lblExpiryDate As System.Windows.Forms.Label
  Friend WithEvents txtComments As System.Windows.Forms.TextBox
  Friend WithEvents btnCommentsUpdate As System.Windows.Forms.Button
  Friend WithEvents lblComments As System.Windows.Forms.Label
  Friend WithEvents txtApplications As System.Windows.Forms.TextBox
  Friend WithEvents btnApplicationsUpdate As System.Windows.Forms.Button
  Friend WithEvents lblApplications As System.Windows.Forms.Label
  Friend WithEvents txtNextSMSPasswordHashed As System.Windows.Forms.TextBox
  Friend WithEvents btnNextSMSPasswordHashedUpdate As System.Windows.Forms.Button
  Friend WithEvents lblNextSMSPasswordHashed As System.Windows.Forms.Label
  Friend WithEvents lblRole As System.Windows.Forms.Label
  Friend WithEvents txtRole As System.Windows.Forms.TextBox
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button
  Friend WithEvents tbcUser As TabControl
  Friend WithEvents tbpInfo As TabPage
  Friend WithEvents tlp1 As TableLayoutPanel
  Friend WithEvents GroupBox3 As GroupBox
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents GroupBox2 As GroupBox
  Friend WithEvents tbpAccess As TabPage
  Friend WithEvents pnlGarbage As Panel
  Friend WithEvents txtLastPasswords As TextBox
  Friend WithEvents lblLastPasswords As Label
  Friend WithEvents tlpSecurity As TableLayoutPanel
  Friend WithEvents Panel3 As Panel
  Friend WithEvents gpbSecurity As GroupBox
  Friend WithEvents Panel4 As Panel
  Friend WithEvents gpbApplicationsRoles As GroupBox
  Friend WithEvents GroupBox7 As GroupBox
  Friend WithEvents gpb2FactorAuthentication As GroupBox
  Friend WithEvents tbpComments As TabPage
  Friend WithEvents lblPasswordHashed As Label
  Friend WithEvents gpbUserPermissionColForUser As GroupBox
  Friend WithEvents cboLanguage As ComboBox
  Friend WithEvents txtLanguage As TextBox
  Friend WithEvents lblLanguage As Label
  Friend WithEvents pnlComments As Panel
  Friend WithEvents lblIsLockedOut As Label
  Friend WithEvents chkIsLockedOut As CheckBox
  Friend WithEvents cboRole As IntelliCombo
  Friend WithEvents cboMessagingMode As ComboBox
  Friend WithEvents txtMessagingMode As TextBox
  Friend WithEvents lblMessagingMode As Label
  Friend WithEvents cboAuthenticationMethod As ComboBox
  Friend WithEvents txtAuthenticationMethod As TextBox
  Friend WithEvents lblAuthenticationMethod As Label
  Friend WithEvents txtLoggedInIP As TextBox
  Friend WithEvents lblLoggedInIP As Label
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents btnLoggedInIPUpdate As Button
  Friend WithEvents lblRequiresFixedIP As Label
  Friend WithEvents chkRequiresFixedIP As CheckBox
  Friend WithEvents gpbPassword As GroupBox
  Friend WithEvents dtpApprovalTime As DateTimePicker
  Friend WithEvents lblApprovalTime As Label
  Friend WithEvents txtApprovalCodeHashed As TextBox
  Friend WithEvents lblApprovalCodeHashed As Label
  Friend WithEvents txtApprovalFunctionName As TextBox
  Friend WithEvents lblApprovalFunctionName As Label
  Friend WithEvents txtApprovalTime As TextBox
  Friend WithEvents txtLastSuccessfulLogin As TextBox
  Friend WithEvents lblLastSuccessfulLogin As Label
  Friend WithEvents btnLastSuccessfulLoginUpdate As Button
  Friend WithEvents chkPasswordNeverExpires As CheckBox
  Friend WithEvents lblPasswordNeverExpires As Label
  Friend WithEvents tbpPasswordsCodes As TabPage
  Friend WithEvents gpbApplicationLoginKeys As GroupBox
  Friend WithEvents tlpPasswordMFA As TableLayoutPanel
  Friend WithEvents Panel5 As Panel
  Friend WithEvents Panel6 As Panel
  Friend WithEvents gpbIdentityVerificationQuestions As GroupBox
  Friend WithEvents btnDeleteAllKeys As Button
  Friend WithEvents Panel1 As Panel
  Friend WithEvents Panel2 As Panel
  Friend WithEvents MyCtlUserLoginKeyColForUser As ctlc_UserLoginKeyCol
  Friend WithEvents cboSecurityQuestion1 As ComboBox
  Friend WithEvents lblSecurityQuestion1 As Label
  Friend WithEvents txtSecurityQuestion1 As TextBox
  Friend WithEvents cboSecurityQuestion3 As ComboBox
  Friend WithEvents lblSecurityQuestion3 As Label
  Friend WithEvents txtSecurityQuestion3 As TextBox
  Friend WithEvents cboSecurityQuestion2 As ComboBox
  Friend WithEvents lblSecurityQuestion2 As Label
  Friend WithEvents txtSecurityQuestion2 As TextBox
  Friend WithEvents btnSecurityQuestion1ResponseUpdate As Button
  Friend WithEvents lblSecurityQuestion1Response As Label
  Friend WithEvents txtSecurityQuestion1Response As TextBox
  Friend WithEvents btnSecurityQuestion3ResponseUpdate As Button
  Friend WithEvents btnSecurityQuestion2ResponseUpdate As Button
  Friend WithEvents lblSecurityQuestion3Response As Label
  Friend WithEvents lblSecurityQuestion2Response As Label
  Friend WithEvents txtSecurityQuestion3Response As TextBox
  Friend WithEvents txtSecurityQuestion2Response As TextBox
  Friend WithEvents btnViewHideSecurityQuestionResponse As Button
  Friend WithEvents lblPIN As Label
  Friend WithEvents txtPIN As TextBox
  Friend WithEvents btnPINUpdate As Button
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents btnDeletePIN As Button
  Friend WithEvents lblTester As Label
End Class
