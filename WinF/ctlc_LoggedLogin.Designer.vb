'Me.BackColor = System.Drawing.XXX --> System.Drawing.Color.Wheat

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_LoggedLogin
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
    Me.txtUserFullName = New System.Windows.Forms.TextBox()
    Me.lblUserFullName = New System.Windows.Forms.Label()
    Me.txtTimeLoggedIn = New System.Windows.Forms.TextBox()
    Me.lblTimeLoggedIn = New System.Windows.Forms.Label()
    Me.txtApplicationName = New System.Windows.Forms.TextBox()
    Me.lblApplicationName = New System.Windows.Forms.Label()
    Me.cboUserIdentityType = New System.Windows.Forms.ComboBox()
    Me.txtUserIdentityType = New System.Windows.Forms.TextBox()
    Me.lblUserIdentityType = New System.Windows.Forms.Label()
    Me.txtUserIdentityTypeName = New System.Windows.Forms.TextBox()
    Me.lblUserIdentityTypeName = New System.Windows.Forms.Label()
    Me.txtRoles = New System.Windows.Forms.TextBox()
    Me.lblRoles = New System.Windows.Forms.Label()
    Me.txtTimeLoggedOut = New System.Windows.Forms.TextBox()
    Me.lblTimeLoggedOut = New System.Windows.Forms.Label()
    Me.txtLoginFaultNumber = New System.Windows.Forms.TextBox()
    Me.lblLoginFaultNumber = New System.Windows.Forms.Label()
    Me.txtEnvironmentUserName = New System.Windows.Forms.TextBox()
    Me.lblEnvironmentUserName = New System.Windows.Forms.Label()
    Me.txtEnvironmentMachineName = New System.Windows.Forms.TextBox()
    Me.lblEnvironmentMachineName = New System.Windows.Forms.Label()
    Me.txtEnvironmentUserDomainName = New System.Windows.Forms.TextBox()
    Me.lblEnvironmentUserDomainName = New System.Windows.Forms.Label()
    Me.txtDnsGetHostName = New System.Windows.Forms.TextBox()
    Me.lblDnsGetHostName = New System.Windows.Forms.Label()
    Me.txtAddressList = New System.Windows.Forms.TextBox()
    Me.lblAddressList = New System.Windows.Forms.Label()
    Me.txtComputerMACAddress = New System.Windows.Forms.TextBox()
    Me.lblComputerMACAddress = New System.Windows.Forms.Label()
    Me.txtSystemDiskVolumeSerialNo = New System.Windows.Forms.TextBox()
    Me.lblSystemDiskVolumeSerialNo = New System.Windows.Forms.Label()
    Me.txtLocalTime = New System.Windows.Forms.TextBox()
    Me.lblLocalTime = New System.Windows.Forms.Label()
    Me.txtGmtTime = New System.Windows.Forms.TextBox()
    Me.lblGmtTime = New System.Windows.Forms.Label()
    Me.txtAccessingComputerDetails = New System.Windows.Forms.TextBox()
    Me.lblAccessingComputerDetails = New System.Windows.Forms.Label()
    Me.txtUICulture = New System.Windows.Forms.TextBox()
    Me.lblUICulture = New System.Windows.Forms.Label()
    Me.txtTotalPhysicalMemoryKb = New System.Windows.Forms.TextBox()
    Me.lblTotalPhysicalMemoryKb = New System.Windows.Forms.Label()
    Me.txtAvailablePhysicalMemoryKb = New System.Windows.Forms.TextBox()
    Me.lblAvailablePhysicalMemoryKb = New System.Windows.Forms.Label()
    Me.txtApplicationVersion = New System.Windows.Forms.TextBox()
    Me.lblApplicationVersion = New System.Windows.Forms.Label()
    Me.txtOriginatingIP = New System.Windows.Forms.TextBox()
    Me.lblOriginatingIP = New System.Windows.Forms.Label()
    Me.cboLanguage = New System.Windows.Forms.ComboBox()
    Me.txtLanguage = New System.Windows.Forms.TextBox()
    Me.lblLanguage = New System.Windows.Forms.Label()
    Me.txtHostingAssembly = New System.Windows.Forms.TextBox()
    Me.lblHostingAssembly = New System.Windows.Forms.Label()
    Me.tbcFault = New System.Windows.Forms.TabControl()
    Me.tbpInfo = New System.Windows.Forms.TabPage()
    Me.tlp1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox7 = New System.Windows.Forms.GroupBox()
    Me.GroupBox9 = New System.Windows.Forms.GroupBox()
    Me.txtClientReportedIP = New System.Windows.Forms.TextBox()
    Me.txtClientReportedCountry = New System.Windows.Forms.TextBox()
    Me.lblClientReportedIP = New System.Windows.Forms.Label()
    Me.lblClientReportedCountry = New System.Windows.Forms.Label()
    Me.GroupBox8 = New System.Windows.Forms.GroupBox()
    Me.txtOriginatingCountry = New System.Windows.Forms.TextBox()
    Me.lblOriginatingCountry = New System.Windows.Forms.Label()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.tbpExtraDetails = New System.Windows.Forms.TabPage()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.GroupBox3 = New System.Windows.Forms.GroupBox()
    Me.Panel4 = New System.Windows.Forms.Panel()
    Me.Panel5 = New System.Windows.Forms.Panel()
    Me.GroupBox6 = New System.Windows.Forms.GroupBox()
    Me.GroupBox5 = New System.Windows.Forms.GroupBox()
    Me.pnlGarbage = New System.Windows.Forms.Panel()
    Me.txtMonthLoggedIn = New System.Windows.Forms.TextBox()
    Me.cboUserIdentityTypeName = New System.Windows.Forms.ComboBox()
    Me.lblMonthLoggedIn = New System.Windows.Forms.Label()
    Me.txtDateLoggedIn = New System.Windows.Forms.TextBox()
    Me.lblDateLoggedIn = New System.Windows.Forms.Label()
    Me.GroupBox10 = New System.Windows.Forms.GroupBox()
    Me.txtIPAdditionalDetails = New System.Windows.Forms.TextBox()
    Me.lblIPAdditionalDetails = New System.Windows.Forms.Label()
    Me.tbcFault.SuspendLayout()
    Me.tbpInfo.SuspendLayout()
    Me.tlp1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox7.SuspendLayout()
    Me.GroupBox9.SuspendLayout()
    Me.GroupBox8.SuspendLayout()
    Me.GroupBox4.SuspendLayout()
    Me.tbpExtraDetails.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.GroupBox3.SuspendLayout()
    Me.Panel4.SuspendLayout()
    Me.Panel5.SuspendLayout()
    Me.GroupBox6.SuspendLayout()
    Me.GroupBox5.SuspendLayout()
    Me.pnlGarbage.SuspendLayout()
    Me.GroupBox10.SuspendLayout()
    Me.SuspendLayout()
    '
    'txtID
    '
    Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtID.Location = New System.Drawing.Point(618, 543)
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
    Me.lblID.Location = New System.Drawing.Point(570, 546)
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
    Me.txtUserName.Location = New System.Drawing.Point(166, 36)
    Me.txtUserName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.Size = New System.Drawing.Size(180, 25)
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
    'txtUserFullName
    '
    Me.txtUserFullName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUserFullName.Location = New System.Drawing.Point(166, 76)
    Me.txtUserFullName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserFullName.Name = "txtUserFullName"
    Me.txtUserFullName.Size = New System.Drawing.Size(180, 25)
    Me.txtUserFullName.TabIndex = 4
    Me.txtUserFullName.Text = "txtUserFullName"
    '
    'lblUserFullName
    '
    Me.lblUserFullName.AutoSize = True
    Me.lblUserFullName.Location = New System.Drawing.Point(13, 79)
    Me.lblUserFullName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblUserFullName.Name = "lblUserFullName"
    Me.lblUserFullName.Size = New System.Drawing.Size(102, 19)
    Me.lblUserFullName.TabIndex = 5
    Me.lblUserFullName.Text = "User Full Name"
    '
    'txtTimeLoggedIn
    '
    Me.txtTimeLoggedIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTimeLoggedIn.Location = New System.Drawing.Point(154, 36)
    Me.txtTimeLoggedIn.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtTimeLoggedIn.Name = "txtTimeLoggedIn"
    Me.txtTimeLoggedIn.Size = New System.Drawing.Size(192, 25)
    Me.txtTimeLoggedIn.TabIndex = 6
    Me.txtTimeLoggedIn.Text = "txtTimeLoggedIn"
    '
    'lblTimeLoggedIn
    '
    Me.lblTimeLoggedIn.AutoSize = True
    Me.lblTimeLoggedIn.Location = New System.Drawing.Point(13, 39)
    Me.lblTimeLoggedIn.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblTimeLoggedIn.Name = "lblTimeLoggedIn"
    Me.lblTimeLoggedIn.Size = New System.Drawing.Size(104, 19)
    Me.lblTimeLoggedIn.TabIndex = 7
    Me.lblTimeLoggedIn.Text = "Time Logged In"
    '
    'txtApplicationName
    '
    Me.txtApplicationName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApplicationName.Location = New System.Drawing.Point(164, 36)
    Me.txtApplicationName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApplicationName.Name = "txtApplicationName"
    Me.txtApplicationName.Size = New System.Drawing.Size(182, 25)
    Me.txtApplicationName.TabIndex = 8
    Me.txtApplicationName.Text = "txtApplicationName"
    '
    'lblApplicationName
    '
    Me.lblApplicationName.AutoSize = True
    Me.lblApplicationName.Location = New System.Drawing.Point(13, 39)
    Me.lblApplicationName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblApplicationName.Name = "lblApplicationName"
    Me.lblApplicationName.Size = New System.Drawing.Size(117, 19)
    Me.lblApplicationName.TabIndex = 9
    Me.lblApplicationName.Text = "Application Name"
    '
    'cboUserIdentityType
    '
    Me.cboUserIdentityType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUserIdentityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUserIdentityType.FormattingEnabled = True
    Me.cboUserIdentityType.Location = New System.Drawing.Point(107, 11)
    Me.cboUserIdentityType.Name = "cboUserIdentityType"
    Me.cboUserIdentityType.Size = New System.Drawing.Size(73, 25)
    Me.cboUserIdentityType.TabIndex = 2
    '
    'txtUserIdentityType
    '
    Me.txtUserIdentityType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUserIdentityType.Location = New System.Drawing.Point(166, 116)
    Me.txtUserIdentityType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserIdentityType.Name = "txtUserIdentityType"
    Me.txtUserIdentityType.Size = New System.Drawing.Size(180, 25)
    Me.txtUserIdentityType.TabIndex = 12
    Me.txtUserIdentityType.Text = "txtUserIdentityType"
    '
    'lblUserIdentityType
    '
    Me.lblUserIdentityType.AutoSize = True
    Me.lblUserIdentityType.Location = New System.Drawing.Point(13, 119)
    Me.lblUserIdentityType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblUserIdentityType.Name = "lblUserIdentityType"
    Me.lblUserIdentityType.Size = New System.Drawing.Size(120, 19)
    Me.lblUserIdentityType.TabIndex = 13
    Me.lblUserIdentityType.Text = "User Identity Type"
    '
    'txtUserIdentityTypeName
    '
    Me.txtUserIdentityTypeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUserIdentityTypeName.Location = New System.Drawing.Point(166, 156)
    Me.txtUserIdentityTypeName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUserIdentityTypeName.Name = "txtUserIdentityTypeName"
    Me.txtUserIdentityTypeName.Size = New System.Drawing.Size(180, 25)
    Me.txtUserIdentityTypeName.TabIndex = 14
    Me.txtUserIdentityTypeName.Text = "txtUserIdentityTypeName"
    '
    'lblUserIdentityTypeName
    '
    Me.lblUserIdentityTypeName.AutoSize = True
    Me.lblUserIdentityTypeName.Location = New System.Drawing.Point(13, 159)
    Me.lblUserIdentityTypeName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblUserIdentityTypeName.Name = "lblUserIdentityTypeName"
    Me.lblUserIdentityTypeName.Size = New System.Drawing.Size(128, 19)
    Me.lblUserIdentityTypeName.TabIndex = 15
    Me.lblUserIdentityTypeName.Text = "Identity Type Name"
    '
    'txtRoles
    '
    Me.txtRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRoles.Location = New System.Drawing.Point(166, 196)
    Me.txtRoles.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtRoles.Multiline = True
    Me.txtRoles.Name = "txtRoles"
    Me.txtRoles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtRoles.Size = New System.Drawing.Size(180, 32)
    Me.txtRoles.TabIndex = 16
    Me.txtRoles.Text = "txtRoles"
    '
    'lblRoles
    '
    Me.lblRoles.AutoSize = True
    Me.lblRoles.Location = New System.Drawing.Point(13, 196)
    Me.lblRoles.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblRoles.Name = "lblRoles"
    Me.lblRoles.Size = New System.Drawing.Size(41, 19)
    Me.lblRoles.TabIndex = 17
    Me.lblRoles.Text = "Roles"
    '
    'txtTimeLoggedOut
    '
    Me.txtTimeLoggedOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTimeLoggedOut.Location = New System.Drawing.Point(154, 76)
    Me.txtTimeLoggedOut.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtTimeLoggedOut.Name = "txtTimeLoggedOut"
    Me.txtTimeLoggedOut.Size = New System.Drawing.Size(192, 25)
    Me.txtTimeLoggedOut.TabIndex = 20
    Me.txtTimeLoggedOut.Text = "txtTimeLoggedOut"
    '
    'lblTimeLoggedOut
    '
    Me.lblTimeLoggedOut.AutoSize = True
    Me.lblTimeLoggedOut.Location = New System.Drawing.Point(13, 79)
    Me.lblTimeLoggedOut.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblTimeLoggedOut.Name = "lblTimeLoggedOut"
    Me.lblTimeLoggedOut.Size = New System.Drawing.Size(116, 19)
    Me.lblTimeLoggedOut.TabIndex = 21
    Me.lblTimeLoggedOut.Text = "Time Logged Out"
    '
    'txtLoginFaultNumber
    '
    Me.txtLoginFaultNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoginFaultNumber.Location = New System.Drawing.Point(154, 156)
    Me.txtLoginFaultNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLoginFaultNumber.Name = "txtLoginFaultNumber"
    Me.txtLoginFaultNumber.Size = New System.Drawing.Size(192, 25)
    Me.txtLoginFaultNumber.TabIndex = 22
    Me.txtLoginFaultNumber.Text = "txtLoginFaultNumber"
    '
    'lblLoginFaultNumber
    '
    Me.lblLoginFaultNumber.AutoSize = True
    Me.lblLoginFaultNumber.Location = New System.Drawing.Point(13, 159)
    Me.lblLoginFaultNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLoginFaultNumber.Name = "lblLoginFaultNumber"
    Me.lblLoginFaultNumber.Size = New System.Drawing.Size(102, 19)
    Me.lblLoginFaultNumber.TabIndex = 23
    Me.lblLoginFaultNumber.Text = "Login Fault No."
    '
    'txtEnvironmentUserName
    '
    Me.txtEnvironmentUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEnvironmentUserName.Location = New System.Drawing.Point(139, 76)
    Me.txtEnvironmentUserName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtEnvironmentUserName.Name = "txtEnvironmentUserName"
    Me.txtEnvironmentUserName.Size = New System.Drawing.Size(207, 25)
    Me.txtEnvironmentUserName.TabIndex = 24
    Me.txtEnvironmentUserName.Text = "txtEnvironmentUserName"
    '
    'lblEnvironmentUserName
    '
    Me.lblEnvironmentUserName.AutoSize = True
    Me.lblEnvironmentUserName.Location = New System.Drawing.Point(7, 79)
    Me.lblEnvironmentUserName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblEnvironmentUserName.Name = "lblEnvironmentUserName"
    Me.lblEnvironmentUserName.Size = New System.Drawing.Size(77, 19)
    Me.lblEnvironmentUserName.TabIndex = 25
    Me.lblEnvironmentUserName.Text = "User Name"
    '
    'txtEnvironmentMachineName
    '
    Me.txtEnvironmentMachineName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEnvironmentMachineName.Location = New System.Drawing.Point(139, 36)
    Me.txtEnvironmentMachineName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtEnvironmentMachineName.Name = "txtEnvironmentMachineName"
    Me.txtEnvironmentMachineName.Size = New System.Drawing.Size(207, 25)
    Me.txtEnvironmentMachineName.TabIndex = 26
    Me.txtEnvironmentMachineName.Text = "txtEnvironmentMachineName"
    '
    'lblEnvironmentMachineName
    '
    Me.lblEnvironmentMachineName.AutoSize = True
    Me.lblEnvironmentMachineName.Location = New System.Drawing.Point(13, 39)
    Me.lblEnvironmentMachineName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblEnvironmentMachineName.Name = "lblEnvironmentMachineName"
    Me.lblEnvironmentMachineName.Size = New System.Drawing.Size(101, 19)
    Me.lblEnvironmentMachineName.TabIndex = 27
    Me.lblEnvironmentMachineName.Text = "Machine Name"
    '
    'txtEnvironmentUserDomainName
    '
    Me.txtEnvironmentUserDomainName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEnvironmentUserDomainName.Location = New System.Drawing.Point(139, 116)
    Me.txtEnvironmentUserDomainName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtEnvironmentUserDomainName.Name = "txtEnvironmentUserDomainName"
    Me.txtEnvironmentUserDomainName.Size = New System.Drawing.Size(207, 25)
    Me.txtEnvironmentUserDomainName.TabIndex = 28
    Me.txtEnvironmentUserDomainName.Text = "txtEnvironmentUserDomainName"
    '
    'lblEnvironmentUserDomainName
    '
    Me.lblEnvironmentUserDomainName.AutoSize = True
    Me.lblEnvironmentUserDomainName.Location = New System.Drawing.Point(7, 119)
    Me.lblEnvironmentUserDomainName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblEnvironmentUserDomainName.Name = "lblEnvironmentUserDomainName"
    Me.lblEnvironmentUserDomainName.Size = New System.Drawing.Size(89, 19)
    Me.lblEnvironmentUserDomainName.TabIndex = 29
    Me.lblEnvironmentUserDomainName.Text = "User Domain"
    '
    'txtDnsGetHostName
    '
    Me.txtDnsGetHostName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDnsGetHostName.Location = New System.Drawing.Point(173, 36)
    Me.txtDnsGetHostName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtDnsGetHostName.Name = "txtDnsGetHostName"
    Me.txtDnsGetHostName.Size = New System.Drawing.Size(176, 25)
    Me.txtDnsGetHostName.TabIndex = 30
    Me.txtDnsGetHostName.Text = "txtDnsGetHostName"
    '
    'lblDnsGetHostName
    '
    Me.lblDnsGetHostName.AutoSize = True
    Me.lblDnsGetHostName.Location = New System.Drawing.Point(16, 39)
    Me.lblDnsGetHostName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblDnsGetHostName.Name = "lblDnsGetHostName"
    Me.lblDnsGetHostName.Size = New System.Drawing.Size(132, 19)
    Me.lblDnsGetHostName.TabIndex = 31
    Me.lblDnsGetHostName.Text = "Dns Get Host Name"
    '
    'txtAddressList
    '
    Me.txtAddressList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAddressList.Location = New System.Drawing.Point(28, 184)
    Me.txtAddressList.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
    Me.txtAddressList.Multiline = True
    Me.txtAddressList.Name = "txtAddressList"
    Me.txtAddressList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtAddressList.Size = New System.Drawing.Size(318, 80)
    Me.txtAddressList.TabIndex = 32
    Me.txtAddressList.Text = "txtAddressList"
    '
    'lblAddressList
    '
    Me.lblAddressList.AutoSize = True
    Me.lblAddressList.Location = New System.Drawing.Point(7, 155)
    Me.lblAddressList.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblAddressList.Name = "lblAddressList"
    Me.lblAddressList.Size = New System.Drawing.Size(83, 19)
    Me.lblAddressList.TabIndex = 33
    Me.lblAddressList.Text = "Address List"
    '
    'txtComputerMACAddress
    '
    Me.txtComputerMACAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtComputerMACAddress.Location = New System.Drawing.Point(31, 182)
    Me.txtComputerMACAddress.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
    Me.txtComputerMACAddress.Multiline = True
    Me.txtComputerMACAddress.Name = "txtComputerMACAddress"
    Me.txtComputerMACAddress.Size = New System.Drawing.Size(318, 47)
    Me.txtComputerMACAddress.TabIndex = 34
    Me.txtComputerMACAddress.Text = "txtComputerMACAddress"
    '
    'lblComputerMACAddress
    '
    Me.lblComputerMACAddress.AutoSize = True
    Me.lblComputerMACAddress.Location = New System.Drawing.Point(16, 153)
    Me.lblComputerMACAddress.Margin = New System.Windows.Forms.Padding(10, 15, 10, 0)
    Me.lblComputerMACAddress.Name = "lblComputerMACAddress"
    Me.lblComputerMACAddress.Size = New System.Drawing.Size(93, 19)
    Me.lblComputerMACAddress.TabIndex = 35
    Me.lblComputerMACAddress.Text = "MAC Address"
    '
    'txtSystemDiskVolumeSerialNo
    '
    Me.txtSystemDiskVolumeSerialNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSystemDiskVolumeSerialNo.Location = New System.Drawing.Point(232, 244)
    Me.txtSystemDiskVolumeSerialNo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtSystemDiskVolumeSerialNo.Name = "txtSystemDiskVolumeSerialNo"
    Me.txtSystemDiskVolumeSerialNo.Size = New System.Drawing.Size(117, 25)
    Me.txtSystemDiskVolumeSerialNo.TabIndex = 36
    Me.txtSystemDiskVolumeSerialNo.Text = "txtSystemDiskVolumeSerialNo"
    '
    'lblSystemDiskVolumeSerialNo
    '
    Me.lblSystemDiskVolumeSerialNo.AutoSize = True
    Me.lblSystemDiskVolumeSerialNo.Location = New System.Drawing.Point(16, 247)
    Me.lblSystemDiskVolumeSerialNo.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblSystemDiskVolumeSerialNo.Name = "lblSystemDiskVolumeSerialNo"
    Me.lblSystemDiskVolumeSerialNo.Size = New System.Drawing.Size(191, 19)
    Me.lblSystemDiskVolumeSerialNo.TabIndex = 37
    Me.lblSystemDiskVolumeSerialNo.Text = "System Disk Volume Serial No"
    '
    'txtLocalTime
    '
    Me.txtLocalTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocalTime.Location = New System.Drawing.Point(114, 284)
    Me.txtLocalTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLocalTime.Name = "txtLocalTime"
    Me.txtLocalTime.Size = New System.Drawing.Size(235, 25)
    Me.txtLocalTime.TabIndex = 38
    Me.txtLocalTime.Text = "txtLocalTime"
    '
    'lblLocalTime
    '
    Me.lblLocalTime.AutoSize = True
    Me.lblLocalTime.Location = New System.Drawing.Point(16, 287)
    Me.lblLocalTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLocalTime.Name = "lblLocalTime"
    Me.lblLocalTime.Size = New System.Drawing.Size(73, 19)
    Me.lblLocalTime.TabIndex = 39
    Me.lblLocalTime.Text = "Local Time"
    '
    'txtGmtTime
    '
    Me.txtGmtTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtGmtTime.Location = New System.Drawing.Point(114, 324)
    Me.txtGmtTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtGmtTime.Name = "txtGmtTime"
    Me.txtGmtTime.Size = New System.Drawing.Size(235, 25)
    Me.txtGmtTime.TabIndex = 40
    Me.txtGmtTime.Text = "txtGmtTime"
    '
    'lblGmtTime
    '
    Me.lblGmtTime.AutoSize = True
    Me.lblGmtTime.Location = New System.Drawing.Point(16, 327)
    Me.lblGmtTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblGmtTime.Name = "lblGmtTime"
    Me.lblGmtTime.Size = New System.Drawing.Size(69, 19)
    Me.lblGmtTime.TabIndex = 41
    Me.lblGmtTime.Text = "Gmt Time"
    '
    'txtAccessingComputerDetails
    '
    Me.txtAccessingComputerDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAccessingComputerDetails.Location = New System.Drawing.Point(28, 59)
    Me.txtAccessingComputerDetails.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
    Me.txtAccessingComputerDetails.Multiline = True
    Me.txtAccessingComputerDetails.Name = "txtAccessingComputerDetails"
    Me.txtAccessingComputerDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtAccessingComputerDetails.Size = New System.Drawing.Size(318, 86)
    Me.txtAccessingComputerDetails.TabIndex = 42
    Me.txtAccessingComputerDetails.Text = "txtAccessingComputerDetails"
    '
    'lblAccessingComputerDetails
    '
    Me.lblAccessingComputerDetails.AutoSize = True
    Me.lblAccessingComputerDetails.Location = New System.Drawing.Point(7, 31)
    Me.lblAccessingComputerDetails.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
    Me.lblAccessingComputerDetails.Name = "lblAccessingComputerDetails"
    Me.lblAccessingComputerDetails.Size = New System.Drawing.Size(179, 19)
    Me.lblAccessingComputerDetails.TabIndex = 43
    Me.lblAccessingComputerDetails.Text = "Accessing Computer Details"
    '
    'txtUICulture
    '
    Me.txtUICulture.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUICulture.Location = New System.Drawing.Point(139, 156)
    Me.txtUICulture.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtUICulture.Name = "txtUICulture"
    Me.txtUICulture.Size = New System.Drawing.Size(207, 25)
    Me.txtUICulture.TabIndex = 44
    Me.txtUICulture.Text = "txtUICulture"
    '
    'lblUICulture
    '
    Me.lblUICulture.AutoSize = True
    Me.lblUICulture.Location = New System.Drawing.Point(7, 159)
    Me.lblUICulture.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblUICulture.Name = "lblUICulture"
    Me.lblUICulture.Size = New System.Drawing.Size(72, 19)
    Me.lblUICulture.TabIndex = 45
    Me.lblUICulture.Text = "UI Culture"
    '
    'txtTotalPhysicalMemoryKb
    '
    Me.txtTotalPhysicalMemoryKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTotalPhysicalMemoryKb.Location = New System.Drawing.Point(207, 76)
    Me.txtTotalPhysicalMemoryKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtTotalPhysicalMemoryKb.Name = "txtTotalPhysicalMemoryKb"
    Me.txtTotalPhysicalMemoryKb.Size = New System.Drawing.Size(142, 25)
    Me.txtTotalPhysicalMemoryKb.TabIndex = 46
    Me.txtTotalPhysicalMemoryKb.Text = "txtTotalPhysicalMemoryKb"
    '
    'lblTotalPhysicalMemoryKb
    '
    Me.lblTotalPhysicalMemoryKb.AutoSize = True
    Me.lblTotalPhysicalMemoryKb.Location = New System.Drawing.Point(16, 79)
    Me.lblTotalPhysicalMemoryKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblTotalPhysicalMemoryKb.Name = "lblTotalPhysicalMemoryKb"
    Me.lblTotalPhysicalMemoryKb.Size = New System.Drawing.Size(166, 19)
    Me.lblTotalPhysicalMemoryKb.TabIndex = 47
    Me.lblTotalPhysicalMemoryKb.Text = "Total Physical Memory Kb"
    '
    'txtAvailablePhysicalMemoryKb
    '
    Me.txtAvailablePhysicalMemoryKb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAvailablePhysicalMemoryKb.Location = New System.Drawing.Point(232, 116)
    Me.txtAvailablePhysicalMemoryKb.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtAvailablePhysicalMemoryKb.Name = "txtAvailablePhysicalMemoryKb"
    Me.txtAvailablePhysicalMemoryKb.Size = New System.Drawing.Size(117, 25)
    Me.txtAvailablePhysicalMemoryKb.TabIndex = 48
    Me.txtAvailablePhysicalMemoryKb.Text = "txtAvailablePhysicalMemoryKb"
    '
    'lblAvailablePhysicalMemoryKb
    '
    Me.lblAvailablePhysicalMemoryKb.AutoSize = True
    Me.lblAvailablePhysicalMemoryKb.Location = New System.Drawing.Point(16, 119)
    Me.lblAvailablePhysicalMemoryKb.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblAvailablePhysicalMemoryKb.Name = "lblAvailablePhysicalMemoryKb"
    Me.lblAvailablePhysicalMemoryKb.Size = New System.Drawing.Size(191, 19)
    Me.lblAvailablePhysicalMemoryKb.TabIndex = 49
    Me.lblAvailablePhysicalMemoryKb.Text = "Available Physical Memory Kb"
    '
    'txtApplicationVersion
    '
    Me.txtApplicationVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApplicationVersion.Location = New System.Drawing.Point(164, 76)
    Me.txtApplicationVersion.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtApplicationVersion.Name = "txtApplicationVersion"
    Me.txtApplicationVersion.Size = New System.Drawing.Size(182, 25)
    Me.txtApplicationVersion.TabIndex = 50
    Me.txtApplicationVersion.Text = "txtApplicationVersion"
    '
    'lblApplicationVersion
    '
    Me.lblApplicationVersion.AutoSize = True
    Me.lblApplicationVersion.Location = New System.Drawing.Point(13, 79)
    Me.lblApplicationVersion.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblApplicationVersion.Name = "lblApplicationVersion"
    Me.lblApplicationVersion.Size = New System.Drawing.Size(126, 19)
    Me.lblApplicationVersion.TabIndex = 51
    Me.lblApplicationVersion.Text = "Application Version"
    '
    'txtOriginatingIP
    '
    Me.txtOriginatingIP.Location = New System.Drawing.Point(39, 26)
    Me.txtOriginatingIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtOriginatingIP.Name = "txtOriginatingIP"
    Me.txtOriginatingIP.Size = New System.Drawing.Size(126, 25)
    Me.txtOriginatingIP.TabIndex = 52
    Me.txtOriginatingIP.Text = "txtOriginatingIP"
    '
    'lblOriginatingIP
    '
    Me.lblOriginatingIP.AutoSize = True
    Me.lblOriginatingIP.Location = New System.Drawing.Point(13, 29)
    Me.lblOriginatingIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblOriginatingIP.Name = "lblOriginatingIP"
    Me.lblOriginatingIP.Size = New System.Drawing.Size(21, 19)
    Me.lblOriginatingIP.TabIndex = 53
    Me.lblOriginatingIP.Text = "IP"
    '
    'cboLanguage
    '
    Me.cboLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLanguage.FormattingEnabled = True
    Me.cboLanguage.Location = New System.Drawing.Point(44, 11)
    Me.cboLanguage.Name = "cboLanguage"
    Me.cboLanguage.Size = New System.Drawing.Size(44, 25)
    Me.cboLanguage.TabIndex = 2
    '
    'txtLanguage
    '
    Me.txtLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLanguage.Location = New System.Drawing.Point(154, 116)
    Me.txtLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLanguage.Name = "txtLanguage"
    Me.txtLanguage.Size = New System.Drawing.Size(192, 25)
    Me.txtLanguage.TabIndex = 56
    Me.txtLanguage.Text = "txtLanguage"
    '
    'lblLanguage
    '
    Me.lblLanguage.AutoSize = True
    Me.lblLanguage.Location = New System.Drawing.Point(13, 119)
    Me.lblLanguage.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLanguage.Name = "lblLanguage"
    Me.lblLanguage.Size = New System.Drawing.Size(69, 19)
    Me.lblLanguage.TabIndex = 57
    Me.lblLanguage.Text = "Language"
    '
    'txtHostingAssembly
    '
    Me.txtHostingAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtHostingAssembly.Location = New System.Drawing.Point(164, 116)
    Me.txtHostingAssembly.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtHostingAssembly.Name = "txtHostingAssembly"
    Me.txtHostingAssembly.Size = New System.Drawing.Size(182, 25)
    Me.txtHostingAssembly.TabIndex = 58
    Me.txtHostingAssembly.Text = "txtHostingAssembly"
    '
    'lblHostingAssembly
    '
    Me.lblHostingAssembly.AutoSize = True
    Me.lblHostingAssembly.Location = New System.Drawing.Point(13, 119)
    Me.lblHostingAssembly.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblHostingAssembly.Name = "lblHostingAssembly"
    Me.lblHostingAssembly.Size = New System.Drawing.Size(119, 19)
    Me.lblHostingAssembly.TabIndex = 59
    Me.lblHostingAssembly.Text = "Hosting Assembly"
    '
    'tbcFault
    '
    Me.tbcFault.Controls.Add(Me.tbpInfo)
    Me.tbcFault.Controls.Add(Me.tbpExtraDetails)
    Me.tbcFault.Dock = System.Windows.Forms.DockStyle.Top
    Me.tbcFault.Location = New System.Drawing.Point(0, 0)
    Me.tbcFault.Name = "tbcFault"
    Me.tbcFault.SelectedIndex = 0
    Me.tbcFault.Size = New System.Drawing.Size(774, 520)
    Me.tbcFault.TabIndex = 97
    '
    'tbpInfo
    '
    Me.tbpInfo.Controls.Add(Me.tlp1)
    Me.tbpInfo.Location = New System.Drawing.Point(4, 26)
    Me.tbpInfo.Name = "tbpInfo"
    Me.tbpInfo.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpInfo.Size = New System.Drawing.Size(766, 490)
    Me.tbpInfo.TabIndex = 0
    Me.tbpInfo.Text = "Info"
    Me.tbpInfo.UseVisualStyleBackColor = True
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
    Me.tlp1.Size = New System.Drawing.Size(760, 484)
    Me.tlp1.TabIndex = 0
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.Color.Wheat
    Me.Panel1.Controls.Add(Me.GroupBox2)
    Me.Panel1.Controls.Add(Me.GroupBox1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(3, 3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel1.Size = New System.Drawing.Size(374, 478)
    Me.Panel1.TabIndex = 2
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.txtTimeLoggedIn)
    Me.GroupBox2.Controls.Add(Me.txtLoginFaultNumber)
    Me.GroupBox2.Controls.Add(Me.lblLoginFaultNumber)
    Me.GroupBox2.Controls.Add(Me.lblTimeLoggedIn)
    Me.GroupBox2.Controls.Add(Me.txtTimeLoggedOut)
    Me.GroupBox2.Controls.Add(Me.lblTimeLoggedOut)
    Me.GroupBox2.Controls.Add(Me.txtLanguage)
    Me.GroupBox2.Controls.Add(Me.lblLanguage)
    Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox2.Location = New System.Drawing.Point(5, 254)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(364, 230)
    Me.GroupBox2.TabIndex = 1
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "The Login"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.txtUserName)
    Me.GroupBox1.Controls.Add(Me.lblUserFullName)
    Me.GroupBox1.Controls.Add(Me.txtUserFullName)
    Me.GroupBox1.Controls.Add(Me.txtUserIdentityType)
    Me.GroupBox1.Controls.Add(Me.lblUserName)
    Me.GroupBox1.Controls.Add(Me.lblUserIdentityType)
    Me.GroupBox1.Controls.Add(Me.lblRoles)
    Me.GroupBox1.Controls.Add(Me.txtUserIdentityTypeName)
    Me.GroupBox1.Controls.Add(Me.txtRoles)
    Me.GroupBox1.Controls.Add(Me.lblUserIdentityTypeName)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(364, 249)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "User"
    '
    'Panel2
    '
    Me.Panel2.BackColor = System.Drawing.Color.Wheat
    Me.Panel2.Controls.Add(Me.GroupBox7)
    Me.Panel2.Controls.Add(Me.GroupBox4)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(383, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel2.Size = New System.Drawing.Size(374, 478)
    Me.Panel2.TabIndex = 3
    '
    'GroupBox7
    '
    Me.GroupBox7.Controls.Add(Me.GroupBox10)
    Me.GroupBox7.Controls.Add(Me.GroupBox9)
    Me.GroupBox7.Controls.Add(Me.GroupBox8)
    Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GroupBox7.Location = New System.Drawing.Point(5, 171)
    Me.GroupBox7.Name = "GroupBox7"
    Me.GroupBox7.Size = New System.Drawing.Size(364, 302)
    Me.GroupBox7.TabIndex = 3
    Me.GroupBox7.TabStop = False
    Me.GroupBox7.Text = "Origin"
    '
    'GroupBox9
    '
    Me.GroupBox9.Controls.Add(Me.txtClientReportedIP)
    Me.GroupBox9.Controls.Add(Me.txtClientReportedCountry)
    Me.GroupBox9.Controls.Add(Me.lblClientReportedIP)
    Me.GroupBox9.Controls.Add(Me.lblClientReportedCountry)
    Me.GroupBox9.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox9.Location = New System.Drawing.Point(3, 83)
    Me.GroupBox9.Name = "GroupBox9"
    Me.GroupBox9.Size = New System.Drawing.Size(358, 61)
    Me.GroupBox9.TabIndex = 57
    Me.GroupBox9.TabStop = False
    Me.GroupBox9.Text = "Reported by Client"
    '
    'txtClientReportedIP
    '
    Me.txtClientReportedIP.Location = New System.Drawing.Point(39, 26)
    Me.txtClientReportedIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtClientReportedIP.Name = "txtClientReportedIP"
    Me.txtClientReportedIP.Size = New System.Drawing.Size(126, 25)
    Me.txtClientReportedIP.TabIndex = 52
    Me.txtClientReportedIP.Text = "txtClientReportedIP"
    '
    'txtClientReportedCountry
    '
    Me.txtClientReportedCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtClientReportedCountry.Location = New System.Drawing.Point(293, 26)
    Me.txtClientReportedCountry.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtClientReportedCountry.Name = "txtClientReportedCountry"
    Me.txtClientReportedCountry.Size = New System.Drawing.Size(47, 25)
    Me.txtClientReportedCountry.TabIndex = 54
    Me.txtClientReportedCountry.Text = "txtClientReportedCountry"
    '
    'lblClientReportedIP
    '
    Me.lblClientReportedIP.AutoSize = True
    Me.lblClientReportedIP.Location = New System.Drawing.Point(13, 29)
    Me.lblClientReportedIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblClientReportedIP.Name = "lblClientReportedIP"
    Me.lblClientReportedIP.Size = New System.Drawing.Size(21, 19)
    Me.lblClientReportedIP.TabIndex = 53
    Me.lblClientReportedIP.Text = "IP"
    '
    'lblClientReportedCountry
    '
    Me.lblClientReportedCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblClientReportedCountry.AutoSize = True
    Me.lblClientReportedCountry.Location = New System.Drawing.Point(216, 29)
    Me.lblClientReportedCountry.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblClientReportedCountry.Name = "lblClientReportedCountry"
    Me.lblClientReportedCountry.Size = New System.Drawing.Size(59, 19)
    Me.lblClientReportedCountry.TabIndex = 55
    Me.lblClientReportedCountry.Text = "Country"
    '
    'GroupBox8
    '
    Me.GroupBox8.Controls.Add(Me.txtOriginatingIP)
    Me.GroupBox8.Controls.Add(Me.txtOriginatingCountry)
    Me.GroupBox8.Controls.Add(Me.lblOriginatingIP)
    Me.GroupBox8.Controls.Add(Me.lblOriginatingCountry)
    Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox8.Location = New System.Drawing.Point(3, 21)
    Me.GroupBox8.Name = "GroupBox8"
    Me.GroupBox8.Size = New System.Drawing.Size(358, 62)
    Me.GroupBox8.TabIndex = 56
    Me.GroupBox8.TabStop = False
    Me.GroupBox8.Text = "Reported by Server"
    '
    'txtOriginatingCountry
    '
    Me.txtOriginatingCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOriginatingCountry.Location = New System.Drawing.Point(293, 26)
    Me.txtOriginatingCountry.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtOriginatingCountry.Name = "txtOriginatingCountry"
    Me.txtOriginatingCountry.Size = New System.Drawing.Size(47, 25)
    Me.txtOriginatingCountry.TabIndex = 54
    Me.txtOriginatingCountry.Text = "txtOriginatingCountry"
    '
    'lblOriginatingCountry
    '
    Me.lblOriginatingCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblOriginatingCountry.AutoSize = True
    Me.lblOriginatingCountry.Location = New System.Drawing.Point(216, 29)
    Me.lblOriginatingCountry.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblOriginatingCountry.Name = "lblOriginatingCountry"
    Me.lblOriginatingCountry.Size = New System.Drawing.Size(59, 19)
    Me.lblOriginatingCountry.TabIndex = 55
    Me.lblOriginatingCountry.Text = "Country"
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.txtApplicationName)
    Me.GroupBox4.Controls.Add(Me.lblApplicationName)
    Me.GroupBox4.Controls.Add(Me.txtApplicationVersion)
    Me.GroupBox4.Controls.Add(Me.lblApplicationVersion)
    Me.GroupBox4.Controls.Add(Me.txtHostingAssembly)
    Me.GroupBox4.Controls.Add(Me.lblHostingAssembly)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(364, 166)
    Me.GroupBox4.TabIndex = 2
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "The Application"
    '
    'tbpExtraDetails
    '
    Me.tbpExtraDetails.Controls.Add(Me.TableLayoutPanel1)
    Me.tbpExtraDetails.Location = New System.Drawing.Point(4, 26)
    Me.tbpExtraDetails.Name = "tbpExtraDetails"
    Me.tbpExtraDetails.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpExtraDetails.Size = New System.Drawing.Size(766, 490)
    Me.tbpExtraDetails.TabIndex = 1
    Me.tbpExtraDetails.Text = "Extra Details"
    Me.tbpExtraDetails.UseVisualStyleBackColor = True
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 1, 0)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 484.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(760, 484)
    Me.TableLayoutPanel1.TabIndex = 1
    '
    'Panel3
    '
    Me.Panel3.BackColor = System.Drawing.Color.Wheat
    Me.Panel3.Controls.Add(Me.GroupBox3)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel3.Location = New System.Drawing.Point(3, 3)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel3.Size = New System.Drawing.Size(374, 478)
    Me.Panel3.TabIndex = 4
    '
    'GroupBox3
    '
    Me.GroupBox3.Controls.Add(Me.lblDnsGetHostName)
    Me.GroupBox3.Controls.Add(Me.txtDnsGetHostName)
    Me.GroupBox3.Controls.Add(Me.txtSystemDiskVolumeSerialNo)
    Me.GroupBox3.Controls.Add(Me.lblSystemDiskVolumeSerialNo)
    Me.GroupBox3.Controls.Add(Me.txtComputerMACAddress)
    Me.GroupBox3.Controls.Add(Me.lblComputerMACAddress)
    Me.GroupBox3.Controls.Add(Me.lblAvailablePhysicalMemoryKb)
    Me.GroupBox3.Controls.Add(Me.lblLocalTime)
    Me.GroupBox3.Controls.Add(Me.txtLocalTime)
    Me.GroupBox3.Controls.Add(Me.lblGmtTime)
    Me.GroupBox3.Controls.Add(Me.txtGmtTime)
    Me.GroupBox3.Controls.Add(Me.txtAvailablePhysicalMemoryKb)
    Me.GroupBox3.Controls.Add(Me.txtTotalPhysicalMemoryKb)
    Me.GroupBox3.Controls.Add(Me.lblTotalPhysicalMemoryKb)
    Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox3.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox3.Name = "GroupBox3"
    Me.GroupBox3.Size = New System.Drawing.Size(364, 367)
    Me.GroupBox3.TabIndex = 2
    Me.GroupBox3.TabStop = False
    Me.GroupBox3.Text = "The Computer"
    '
    'Panel4
    '
    Me.Panel4.Controls.Add(Me.Panel5)
    Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel4.Location = New System.Drawing.Point(383, 3)
    Me.Panel4.Name = "Panel4"
    Me.Panel4.Size = New System.Drawing.Size(374, 478)
    Me.Panel4.TabIndex = 3
    '
    'Panel5
    '
    Me.Panel5.BackColor = System.Drawing.Color.Wheat
    Me.Panel5.Controls.Add(Me.GroupBox6)
    Me.Panel5.Controls.Add(Me.GroupBox5)
    Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel5.Location = New System.Drawing.Point(0, 0)
    Me.Panel5.Name = "Panel5"
    Me.Panel5.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel5.Size = New System.Drawing.Size(374, 478)
    Me.Panel5.TabIndex = 3
    '
    'GroupBox6
    '
    Me.GroupBox6.Controls.Add(Me.txtAccessingComputerDetails)
    Me.GroupBox6.Controls.Add(Me.lblAccessingComputerDetails)
    Me.GroupBox6.Controls.Add(Me.txtAddressList)
    Me.GroupBox6.Controls.Add(Me.lblAddressList)
    Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox6.Location = New System.Drawing.Point(5, 198)
    Me.GroupBox6.Name = "GroupBox6"
    Me.GroupBox6.Size = New System.Drawing.Size(364, 277)
    Me.GroupBox6.TabIndex = 44
    Me.GroupBox6.TabStop = False
    Me.GroupBox6.Text = "Raw Details"
    '
    'GroupBox5
    '
    Me.GroupBox5.Controls.Add(Me.txtEnvironmentUserName)
    Me.GroupBox5.Controls.Add(Me.lblEnvironmentUserName)
    Me.GroupBox5.Controls.Add(Me.lblEnvironmentMachineName)
    Me.GroupBox5.Controls.Add(Me.txtEnvironmentUserDomainName)
    Me.GroupBox5.Controls.Add(Me.lblEnvironmentUserDomainName)
    Me.GroupBox5.Controls.Add(Me.txtEnvironmentMachineName)
    Me.GroupBox5.Controls.Add(Me.txtUICulture)
    Me.GroupBox5.Controls.Add(Me.lblUICulture)
    Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox5.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox5.Name = "GroupBox5"
    Me.GroupBox5.Size = New System.Drawing.Size(364, 193)
    Me.GroupBox5.TabIndex = 3
    Me.GroupBox5.TabStop = False
    Me.GroupBox5.Text = "The Environment"
    '
    'pnlGarbage
    '
    Me.pnlGarbage.Controls.Add(Me.lblIPAdditionalDetails)
    Me.pnlGarbage.Controls.Add(Me.txtMonthLoggedIn)
    Me.pnlGarbage.Controls.Add(Me.cboUserIdentityTypeName)
    Me.pnlGarbage.Controls.Add(Me.lblMonthLoggedIn)
    Me.pnlGarbage.Controls.Add(Me.cboLanguage)
    Me.pnlGarbage.Controls.Add(Me.txtDateLoggedIn)
    Me.pnlGarbage.Controls.Add(Me.lblDateLoggedIn)
    Me.pnlGarbage.Controls.Add(Me.cboUserIdentityType)
    Me.pnlGarbage.Location = New System.Drawing.Point(43, 473)
    Me.pnlGarbage.Name = "pnlGarbage"
    Me.pnlGarbage.Size = New System.Drawing.Size(200, 100)
    Me.pnlGarbage.TabIndex = 98
    Me.pnlGarbage.Visible = False
    '
    'txtMonthLoggedIn
    '
    Me.txtMonthLoggedIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMonthLoggedIn.Location = New System.Drawing.Point(96, 79)
    Me.txtMonthLoggedIn.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtMonthLoggedIn.Name = "txtMonthLoggedIn"
    Me.txtMonthLoggedIn.Size = New System.Drawing.Size(176, 25)
    Me.txtMonthLoggedIn.TabIndex = 101
    Me.txtMonthLoggedIn.Text = "txtMonthLoggedIn"
    '
    'cboUserIdentityTypeName
    '
    Me.cboUserIdentityTypeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUserIdentityTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUserIdentityTypeName.FormattingEnabled = True
    Me.cboUserIdentityTypeName.Location = New System.Drawing.Point(64, 40)
    Me.cboUserIdentityTypeName.Name = "cboUserIdentityTypeName"
    Me.cboUserIdentityTypeName.Size = New System.Drawing.Size(73, 25)
    Me.cboUserIdentityTypeName.TabIndex = 3
    '
    'lblMonthLoggedIn
    '
    Me.lblMonthLoggedIn.AutoSize = True
    Me.lblMonthLoggedIn.Location = New System.Drawing.Point(-6, 82)
    Me.lblMonthLoggedIn.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblMonthLoggedIn.Name = "lblMonthLoggedIn"
    Me.lblMonthLoggedIn.Size = New System.Drawing.Size(109, 19)
    Me.lblMonthLoggedIn.TabIndex = 102
    Me.lblMonthLoggedIn.Text = "MonthLoggedIn"
    '
    'txtDateLoggedIn
    '
    Me.txtDateLoggedIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDateLoggedIn.Location = New System.Drawing.Point(96, 54)
    Me.txtDateLoggedIn.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtDateLoggedIn.Name = "txtDateLoggedIn"
    Me.txtDateLoggedIn.Size = New System.Drawing.Size(176, 25)
    Me.txtDateLoggedIn.TabIndex = 99
    Me.txtDateLoggedIn.Text = "txtDateLoggedIn"
    '
    'lblDateLoggedIn
    '
    Me.lblDateLoggedIn.AutoSize = True
    Me.lblDateLoggedIn.Location = New System.Drawing.Point(-6, 57)
    Me.lblDateLoggedIn.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblDateLoggedIn.Name = "lblDateLoggedIn"
    Me.lblDateLoggedIn.Size = New System.Drawing.Size(96, 19)
    Me.lblDateLoggedIn.TabIndex = 100
    Me.lblDateLoggedIn.Text = "DateLoggedIn"
    '
    'GroupBox10
    '
    Me.GroupBox10.Controls.Add(Me.txtIPAdditionalDetails)
    Me.GroupBox10.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GroupBox10.Location = New System.Drawing.Point(3, 144)
    Me.GroupBox10.Name = "GroupBox10"
    Me.GroupBox10.Size = New System.Drawing.Size(358, 155)
    Me.GroupBox10.TabIndex = 58
    Me.GroupBox10.TabStop = False
    Me.GroupBox10.Text = "Additional Details on IP"
    '
    'txtIPAdditionalDetails
    '
    Me.txtIPAdditionalDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtIPAdditionalDetails.Location = New System.Drawing.Point(14, 36)
    Me.txtIPAdditionalDetails.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtIPAdditionalDetails.Multiline = True
    Me.txtIPAdditionalDetails.Name = "txtIPAdditionalDetails"
    Me.txtIPAdditionalDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtIPAdditionalDetails.Size = New System.Drawing.Size(326, 103)
    Me.txtIPAdditionalDetails.TabIndex = 52
    Me.txtIPAdditionalDetails.Text = "txtIPAdditionalDetails"
    '
    'lblIPAdditionalDetails
    '
    Me.lblIPAdditionalDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblIPAdditionalDetails.AutoSize = True
    Me.lblIPAdditionalDetails.Location = New System.Drawing.Point(340, 546)
    Me.lblIPAdditionalDetails.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblIPAdditionalDetails.Name = "lblIPAdditionalDetails"
    Me.lblIPAdditionalDetails.Size = New System.Drawing.Size(123, 19)
    Me.lblIPAdditionalDetails.TabIndex = 99
    Me.lblIPAdditionalDetails.Text = "IPAdditionalDetails"
    '
    'ctlc_LoggedLogin
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoScroll = True
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Controls.Add(Me.lblIPAdditionalDetails)
    Me.Controls.Add(Me.tbcFault)
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.pnlGarbage)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlc_LoggedLogin"
    Me.Size = New System.Drawing.Size(774, 591)
    Me.tbcFault.ResumeLayout(False)
    Me.tbpInfo.ResumeLayout(False)
    Me.tlp1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.GroupBox7.ResumeLayout(False)
    Me.GroupBox9.ResumeLayout(False)
    Me.GroupBox9.PerformLayout()
    Me.GroupBox8.ResumeLayout(False)
    Me.GroupBox8.PerformLayout()
    Me.GroupBox4.ResumeLayout(False)
    Me.GroupBox4.PerformLayout()
    Me.tbpExtraDetails.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.Panel3.ResumeLayout(False)
    Me.GroupBox3.ResumeLayout(False)
    Me.GroupBox3.PerformLayout()
    Me.Panel4.ResumeLayout(False)
    Me.Panel5.ResumeLayout(False)
    Me.GroupBox6.ResumeLayout(False)
    Me.GroupBox6.PerformLayout()
    Me.GroupBox5.ResumeLayout(False)
    Me.GroupBox5.PerformLayout()
    Me.pnlGarbage.ResumeLayout(False)
    Me.pnlGarbage.PerformLayout()
    Me.GroupBox10.ResumeLayout(False)
    Me.GroupBox10.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents lblUserName As System.Windows.Forms.Label
  Friend WithEvents txtUserFullName As System.Windows.Forms.TextBox
  Friend WithEvents lblUserFullName As System.Windows.Forms.Label
  Friend WithEvents txtTimeLoggedIn As System.Windows.Forms.TextBox
  Friend WithEvents lblTimeLoggedIn As System.Windows.Forms.Label
  Friend WithEvents txtApplicationName As System.Windows.Forms.TextBox
  Friend WithEvents lblApplicationName As System.Windows.Forms.Label
  Friend WithEvents cboUserIdentityType As System.Windows.Forms.ComboBox
  Friend WithEvents txtUserIdentityType As System.Windows.Forms.TextBox
  Friend WithEvents lblUserIdentityType As System.Windows.Forms.Label
  Friend WithEvents txtUserIdentityTypeName As System.Windows.Forms.TextBox
  Friend WithEvents lblUserIdentityTypeName As System.Windows.Forms.Label
  Friend WithEvents txtRoles As System.Windows.Forms.TextBox
  Friend WithEvents lblRoles As System.Windows.Forms.Label
  Friend WithEvents txtTimeLoggedOut As System.Windows.Forms.TextBox
  Friend WithEvents lblTimeLoggedOut As System.Windows.Forms.Label
  Friend WithEvents txtLoginFaultNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblLoginFaultNumber As System.Windows.Forms.Label
  Friend WithEvents txtEnvironmentUserName As System.Windows.Forms.TextBox
  Friend WithEvents lblEnvironmentUserName As System.Windows.Forms.Label
  Friend WithEvents txtEnvironmentMachineName As System.Windows.Forms.TextBox
  Friend WithEvents lblEnvironmentMachineName As System.Windows.Forms.Label
  Friend WithEvents txtEnvironmentUserDomainName As System.Windows.Forms.TextBox
  Friend WithEvents lblEnvironmentUserDomainName As System.Windows.Forms.Label
  Friend WithEvents txtDnsGetHostName As System.Windows.Forms.TextBox
  Friend WithEvents lblDnsGetHostName As System.Windows.Forms.Label
  Friend WithEvents txtAddressList As System.Windows.Forms.TextBox
  Friend WithEvents lblAddressList As System.Windows.Forms.Label
  Friend WithEvents txtComputerMACAddress As System.Windows.Forms.TextBox
  Friend WithEvents lblComputerMACAddress As System.Windows.Forms.Label
  Friend WithEvents txtSystemDiskVolumeSerialNo As System.Windows.Forms.TextBox
  Friend WithEvents lblSystemDiskVolumeSerialNo As System.Windows.Forms.Label
  Friend WithEvents txtLocalTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLocalTime As System.Windows.Forms.Label
  Friend WithEvents txtGmtTime As System.Windows.Forms.TextBox
  Friend WithEvents lblGmtTime As System.Windows.Forms.Label
  Friend WithEvents txtAccessingComputerDetails As System.Windows.Forms.TextBox
  Friend WithEvents lblAccessingComputerDetails As System.Windows.Forms.Label
  Friend WithEvents txtUICulture As System.Windows.Forms.TextBox
  Friend WithEvents lblUICulture As System.Windows.Forms.Label
  Friend WithEvents txtTotalPhysicalMemoryKb As System.Windows.Forms.TextBox
  Friend WithEvents lblTotalPhysicalMemoryKb As System.Windows.Forms.Label
  Friend WithEvents txtAvailablePhysicalMemoryKb As System.Windows.Forms.TextBox
  Friend WithEvents lblAvailablePhysicalMemoryKb As System.Windows.Forms.Label
  Friend WithEvents txtApplicationVersion As System.Windows.Forms.TextBox
  Friend WithEvents lblApplicationVersion As System.Windows.Forms.Label
  Friend WithEvents txtOriginatingIP As System.Windows.Forms.TextBox
  Friend WithEvents lblOriginatingIP As System.Windows.Forms.Label
  Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
  Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
  Friend WithEvents lblLanguage As System.Windows.Forms.Label
  Friend WithEvents txtHostingAssembly As System.Windows.Forms.TextBox
  Friend WithEvents lblHostingAssembly As System.Windows.Forms.Label
  Friend WithEvents tbcFault As TabControl
  Friend WithEvents tbpInfo As TabPage
  Friend WithEvents tlp1 As TableLayoutPanel
  Friend WithEvents Panel1 As Panel
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Panel2 As Panel
  Friend WithEvents tbpExtraDetails As TabPage
  Friend WithEvents GroupBox2 As GroupBox
  Friend WithEvents GroupBox3 As GroupBox
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents Panel4 As Panel
  Friend WithEvents GroupBox5 As GroupBox
  Friend WithEvents pnlGarbage As Panel
  Friend WithEvents cboUserIdentityTypeName As ComboBox
  Friend WithEvents Panel3 As Panel
  Friend WithEvents Panel5 As Panel
  Friend WithEvents GroupBox6 As GroupBox
  Friend WithEvents txtOriginatingCountry As TextBox
  Friend WithEvents lblOriginatingCountry As Label
  Friend WithEvents GroupBox7 As GroupBox
  Friend WithEvents txtDateLoggedIn As TextBox
  Friend WithEvents lblDateLoggedIn As Label
  Friend WithEvents txtMonthLoggedIn As TextBox
  Friend WithEvents lblMonthLoggedIn As Label
  Friend WithEvents GroupBox8 As GroupBox
  Friend WithEvents GroupBox9 As GroupBox
  Friend WithEvents txtClientReportedIP As TextBox
  Friend WithEvents txtClientReportedCountry As TextBox
  Friend WithEvents lblClientReportedIP As Label
  Friend WithEvents lblClientReportedCountry As Label
  Friend WithEvents GroupBox10 As GroupBox
  Friend WithEvents txtIPAdditionalDetails As TextBox
  Friend WithEvents lblIPAdditionalDetails As Label
End Class
