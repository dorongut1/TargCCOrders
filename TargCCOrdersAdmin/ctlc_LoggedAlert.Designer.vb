'Me.BackColor = System.Drawing.XXX --> System.Drawing.Color.Wheat

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_LoggedAlert
  Inherits System.Windows.Forms.UserControl


  'UserControl overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
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
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.txtID = New System.Windows.Forms.TextBox()
    Me.lblID = New System.Windows.Forms.Label()
    Me.txtTimeOccurred = New System.Windows.Forms.TextBox()
    Me.lblTimeOccurred = New System.Windows.Forms.Label()
    Me.txtFaultNumber = New System.Windows.Forms.TextBox()
    Me.lblFaultNumber = New System.Windows.Forms.Label()
    Me.txtSystemName = New System.Windows.Forms.TextBox()
    Me.lblSystemName = New System.Windows.Forms.Label()
    Me.txtCallingApplication = New System.Windows.Forms.TextBox()
    Me.lblCallingApplication = New System.Windows.Forms.Label()
    Me.txtCallingApplicationVersion = New System.Windows.Forms.TextBox()
    Me.lblCallingApplicationVersion = New System.Windows.Forms.Label()
    Me.txtCallingFunctionWithinApplication = New System.Windows.Forms.TextBox()
    Me.lblCallingFunctionWithinApplication = New System.Windows.Forms.Label()
    Me.txtFreeText = New System.Windows.Forms.TextBox()
    Me.lblFreeText = New System.Windows.Forms.Label()
    Me.txtFaultingAssembly = New System.Windows.Forms.TextBox()
    Me.lblFaultingAssembly = New System.Windows.Forms.Label()
    Me.txtAssemblyEntryPoint = New System.Windows.Forms.TextBox()
    Me.lblAssemblyEntryPoint = New System.Windows.Forms.Label()
    Me.txtFaultingClass = New System.Windows.Forms.TextBox()
    Me.lblFaultingClass = New System.Windows.Forms.Label()
    Me.txtFaultingFunction = New System.Windows.Forms.TextBox()
    Me.lblFaultingFunction = New System.Windows.Forms.Label()
    Me.txtFaultingFunctionParameters = New System.Windows.Forms.TextBox()
    Me.lblFaultingFunctionParameters = New System.Windows.Forms.Label()
    Me.txtFaultIdent = New System.Windows.Forms.TextBox()
    Me.lblFaultIdent = New System.Windows.Forms.Label()
    Me.txtFaultDescription = New System.Windows.Forms.TextBox()
    Me.lblFaultDescription = New System.Windows.Forms.Label()
    Me.txtMessageSentToUser = New System.Windows.Forms.TextBox()
    Me.lblMessageSentToUser = New System.Windows.Forms.Label()
    Me.txtActionSentToUser = New System.Windows.Forms.TextBox()
    Me.lblActionSentToUser = New System.Windows.Forms.Label()
    Me.cboFaultType = New System.Windows.Forms.ComboBox()
    Me.txtFaultType = New System.Windows.Forms.TextBox()
    Me.lblFaultType = New System.Windows.Forms.Label()
    Me.cboFaultSeverity = New System.Windows.Forms.ComboBox()
    Me.txtFaultSeverity = New System.Windows.Forms.TextBox()
    Me.lblFaultSeverity = New System.Windows.Forms.Label()
        Me.txtLoggedLogin = New System.Windows.Forms.TextBox()
        Me.lblLoggedLogin = New System.Windows.Forms.Label()
        Me.txtThread = New System.Windows.Forms.TextBox()
        Me.lblThread = New System.Windows.Forms.Label()
        Me.tbcFault = New System.Windows.Forms.TabControl()
        Me.tbpInfo = New System.Windows.Forms.TabPage()
        Me.tlp1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.lblUserIdentityType = New System.Windows.Forms.Label()
        Me.txtUserIdentityType = New System.Windows.Forms.TextBox()
        Me.txtUserIdentityTypeName = New System.Windows.Forms.TextBox()
        Me.lblUserIdentityTypeName = New System.Windows.Forms.Label()
        Me.txtAffectedUser = New System.Windows.Forms.TextBox()
        Me.lblAffectedUser = New System.Windows.Forms.Label()
        Me.gpbAdditionalDetails = New System.Windows.Forms.GroupBox()
        Me.txt02 = New System.Windows.Forms.TextBox()
        Me.lbl02 = New System.Windows.Forms.Label()
        Me.txt01 = New System.Windows.Forms.TextBox()
        Me.lbl01 = New System.Windows.Forms.Label()
        Me.tbpExtraDetails = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnJumpTo = New System.Windows.Forms.Button()
        Me.pnlGarbage = New System.Windows.Forms.Panel()
        Me.cboUserIdentityType = New System.Windows.Forms.ComboBox()
        Me.cboUserIdentityTypeName = New System.Windows.Forms.ComboBox()
        Me.txtMonthOccurred = New System.Windows.Forms.TextBox()
        Me.lblMonthOccurred = New System.Windows.Forms.Label()
        Me.txtDateOccurred = New System.Windows.Forms.TextBox()
        Me.lblDateOccurred = New System.Windows.Forms.Label()
        Me.cboAffectedUser = New IntelliCombo()
        Me.cboLoggedLogin = New IntelliCombo()
        Me.tbcFault.SuspendLayout()
        Me.tbpInfo.SuspendLayout()
        Me.tlp1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.gpbAdditionalDetails.SuspendLayout()
        Me.tbpExtraDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.pnlGarbage.SuspendLayout()
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
        'txtTimeOccurred
        '
        Me.txtTimeOccurred.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTimeOccurred.Location = New System.Drawing.Point(155, 46)
        Me.txtTimeOccurred.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtTimeOccurred.Name = "txtTimeOccurred"
        Me.txtTimeOccurred.Size = New System.Drawing.Size(191, 25)
        Me.txtTimeOccurred.TabIndex = 2
        Me.txtTimeOccurred.Text = "txtTimeOccurred"
        '
        'lblTimeOccurred
        '
        Me.lblTimeOccurred.AutoSize = True
        Me.lblTimeOccurred.Location = New System.Drawing.Point(144, 21)
        Me.lblTimeOccurred.Name = "lblTimeOccurred"
        Me.lblTimeOccurred.Size = New System.Drawing.Size(45, 19)
        Me.lblTimeOccurred.TabIndex = 3
        Me.lblTimeOccurred.Text = "When"
        '
        'txtFaultNumber
        '
        Me.txtFaultNumber.Location = New System.Drawing.Point(18, 46)
        Me.txtFaultNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultNumber.Name = "txtFaultNumber"
        Me.txtFaultNumber.Size = New System.Drawing.Size(107, 25)
        Me.txtFaultNumber.TabIndex = 4
        Me.txtFaultNumber.Text = "txtFaultNumber"
        '
        'lblFaultNumber
        '
        Me.lblFaultNumber.AutoSize = True
        Me.lblFaultNumber.Location = New System.Drawing.Point(13, 21)
        Me.lblFaultNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultNumber.Name = "lblFaultNumber"
        Me.lblFaultNumber.Size = New System.Drawing.Size(93, 19)
        Me.lblFaultNumber.TabIndex = 5
        Me.lblFaultNumber.Text = "Fault Number"
        '
        'txtSystemName
        '
        Me.txtSystemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSystemName.Location = New System.Drawing.Point(124, 36)
        Me.txtSystemName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtSystemName.Name = "txtSystemName"
        Me.txtSystemName.Size = New System.Drawing.Size(222, 25)
        Me.txtSystemName.TabIndex = 6
        Me.txtSystemName.Text = "txtSystemName"
        '
        'lblSystemName
        '
        Me.lblSystemName.AutoSize = True
        Me.lblSystemName.Location = New System.Drawing.Point(13, 39)
        Me.lblSystemName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblSystemName.Name = "lblSystemName"
        Me.lblSystemName.Size = New System.Drawing.Size(93, 19)
        Me.lblSystemName.TabIndex = 7
        Me.lblSystemName.Text = "System Name"
        '
        'txtCallingApplication
        '
        Me.txtCallingApplication.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCallingApplication.Location = New System.Drawing.Point(124, 76)
        Me.txtCallingApplication.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtCallingApplication.Name = "txtCallingApplication"
        Me.txtCallingApplication.Size = New System.Drawing.Size(143, 25)
        Me.txtCallingApplication.TabIndex = 8
        Me.txtCallingApplication.Text = "txtCallingApplication"
        '
        'lblCallingApplication
        '
        Me.lblCallingApplication.AutoSize = True
        Me.lblCallingApplication.Location = New System.Drawing.Point(14, 76)
        Me.lblCallingApplication.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblCallingApplication.Name = "lblCallingApplication"
        Me.lblCallingApplication.Size = New System.Drawing.Size(77, 19)
        Me.lblCallingApplication.TabIndex = 9
        Me.lblCallingApplication.Text = "Application"
        '
        'txtCallingApplicationVersion
        '
        Me.txtCallingApplicationVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCallingApplicationVersion.Location = New System.Drawing.Point(279, 76)
        Me.txtCallingApplicationVersion.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtCallingApplicationVersion.Name = "txtCallingApplicationVersion"
        Me.txtCallingApplicationVersion.Size = New System.Drawing.Size(67, 25)
        Me.txtCallingApplicationVersion.TabIndex = 12
        Me.txtCallingApplicationVersion.Text = "1.0.56.32"
        '
        'lblCallingApplicationVersion
        '
        Me.lblCallingApplicationVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCallingApplicationVersion.AutoSize = True
        Me.lblCallingApplicationVersion.Location = New System.Drawing.Point(71, 78)
        Me.lblCallingApplicationVersion.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblCallingApplicationVersion.Name = "lblCallingApplicationVersion"
        Me.lblCallingApplicationVersion.Size = New System.Drawing.Size(29, 19)
        Me.lblCallingApplicationVersion.TabIndex = 13
        Me.lblCallingApplicationVersion.Text = "Ver"
        '
        'txtCallingFunctionWithinApplication
        '
        Me.txtCallingFunctionWithinApplication.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCallingFunctionWithinApplication.Location = New System.Drawing.Point(20, 145)
        Me.txtCallingFunctionWithinApplication.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtCallingFunctionWithinApplication.Name = "txtCallingFunctionWithinApplication"
        Me.txtCallingFunctionWithinApplication.Size = New System.Drawing.Size(326, 25)
        Me.txtCallingFunctionWithinApplication.TabIndex = 14
        Me.txtCallingFunctionWithinApplication.Text = "txtCallingFunctionWithinApplication"
        '
        'lblCallingFunctionWithinApplication
        '
        Me.lblCallingFunctionWithinApplication.AutoSize = True
        Me.lblCallingFunctionWithinApplication.Location = New System.Drawing.Point(14, 117)
        Me.lblCallingFunctionWithinApplication.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblCallingFunctionWithinApplication.Name = "lblCallingFunctionWithinApplication"
        Me.lblCallingFunctionWithinApplication.Size = New System.Drawing.Size(178, 19)
        Me.lblCallingFunctionWithinApplication.TabIndex = 15
        Me.lblCallingFunctionWithinApplication.Text = "Function Within Application"
        '
        'txtFreeText
        '
        Me.txtFreeText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFreeText.Location = New System.Drawing.Point(15, 33)
        Me.txtFreeText.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFreeText.Multiline = True
        Me.txtFreeText.Name = "txtFreeText"
        Me.txtFreeText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFreeText.Size = New System.Drawing.Size(334, 195)
        Me.txtFreeText.TabIndex = 16
        Me.txtFreeText.Text = "txtFreeText"
        Me.txtFreeText.WordWrap = False
        '
        'lblFreeText
        '
        Me.lblFreeText.AutoSize = True
        Me.lblFreeText.Location = New System.Drawing.Point(138, 25)
        Me.lblFreeText.Name = "lblFreeText"
        Me.lblFreeText.Size = New System.Drawing.Size(63, 19)
        Me.lblFreeText.TabIndex = 17
        Me.lblFreeText.Text = "Free Text"
        '
        'txtFaultingAssembly
        '
        Me.txtFaultingAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultingAssembly.Location = New System.Drawing.Point(158, 36)
        Me.txtFaultingAssembly.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultingAssembly.Name = "txtFaultingAssembly"
        Me.txtFaultingAssembly.Size = New System.Drawing.Size(188, 25)
        Me.txtFaultingAssembly.TabIndex = 18
        Me.txtFaultingAssembly.Text = "txtFaultingAssembly"
        '
        'lblFaultingAssembly
        '
        Me.lblFaultingAssembly.AutoSize = True
        Me.lblFaultingAssembly.Location = New System.Drawing.Point(13, 39)
        Me.lblFaultingAssembly.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultingAssembly.Name = "lblFaultingAssembly"
        Me.lblFaultingAssembly.Size = New System.Drawing.Size(120, 19)
        Me.lblFaultingAssembly.TabIndex = 19
        Me.lblFaultingAssembly.Text = "Faulting Assembly"
        '
        'txtAssemblyEntryPoint
        '
        Me.txtAssemblyEntryPoint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAssemblyEntryPoint.Location = New System.Drawing.Point(27, 102)
        Me.txtAssemblyEntryPoint.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
        Me.txtAssemblyEntryPoint.Multiline = True
        Me.txtAssemblyEntryPoint.Name = "txtAssemblyEntryPoint"
        Me.txtAssemblyEntryPoint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAssemblyEntryPoint.Size = New System.Drawing.Size(319, 35)
        Me.txtAssemblyEntryPoint.TabIndex = 20
        Me.txtAssemblyEntryPoint.Text = "txtAssemblyEntryPoint"
        '
        'lblAssemblyEntryPoint
        '
        Me.lblAssemblyEntryPoint.AutoSize = True
        Me.lblAssemblyEntryPoint.Location = New System.Drawing.Point(13, 73)
        Me.lblAssemblyEntryPoint.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblAssemblyEntryPoint.Name = "lblAssemblyEntryPoint"
        Me.lblAssemblyEntryPoint.Size = New System.Drawing.Size(138, 19)
        Me.lblAssemblyEntryPoint.TabIndex = 21
        Me.lblAssemblyEntryPoint.Text = "Assembly Entry Point"
        '
        'txtFaultingClass
        '
        Me.txtFaultingClass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultingClass.Location = New System.Drawing.Point(158, 152)
        Me.txtFaultingClass.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultingClass.Name = "txtFaultingClass"
        Me.txtFaultingClass.Size = New System.Drawing.Size(188, 25)
        Me.txtFaultingClass.TabIndex = 22
        Me.txtFaultingClass.Text = "txtFaultingClass"
        '
        'lblFaultingClass
        '
        Me.lblFaultingClass.AutoSize = True
        Me.lblFaultingClass.Location = New System.Drawing.Point(13, 155)
        Me.lblFaultingClass.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultingClass.Name = "lblFaultingClass"
        Me.lblFaultingClass.Size = New System.Drawing.Size(93, 19)
        Me.lblFaultingClass.TabIndex = 23
        Me.lblFaultingClass.Text = "Faulting Class"
        '
        'txtFaultingFunction
        '
        Me.txtFaultingFunction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultingFunction.Location = New System.Drawing.Point(27, 218)
        Me.txtFaultingFunction.Margin = New System.Windows.Forms.Padding(15, 10, 15, 0)
        Me.txtFaultingFunction.Name = "txtFaultingFunction"
        Me.txtFaultingFunction.Size = New System.Drawing.Size(319, 25)
        Me.txtFaultingFunction.TabIndex = 24
        Me.txtFaultingFunction.Text = "txtFaultingFunction"
        '
        'lblFaultingFunction
        '
        Me.lblFaultingFunction.AutoSize = True
        Me.lblFaultingFunction.Location = New System.Drawing.Point(13, 189)
        Me.lblFaultingFunction.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblFaultingFunction.Name = "lblFaultingFunction"
        Me.lblFaultingFunction.Size = New System.Drawing.Size(115, 19)
        Me.lblFaultingFunction.TabIndex = 25
        Me.lblFaultingFunction.Text = "Faulting Function"
        '
        'txtFaultingFunctionParameters
        '
        Me.txtFaultingFunctionParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFaultingFunctionParameters.Location = New System.Drawing.Point(15, 33)
        Me.txtFaultingFunctionParameters.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultingFunctionParameters.Multiline = True
        Me.txtFaultingFunctionParameters.Name = "txtFaultingFunctionParameters"
        Me.txtFaultingFunctionParameters.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFaultingFunctionParameters.Size = New System.Drawing.Size(334, 282)
        Me.txtFaultingFunctionParameters.TabIndex = 26
        Me.txtFaultingFunctionParameters.Text = "txtFaultingFunctionParameters"
        Me.txtFaultingFunctionParameters.WordWrap = False
        '
        'lblFaultingFunctionParameters
        '
        Me.lblFaultingFunctionParameters.Location = New System.Drawing.Point(113, 10)
        Me.lblFaultingFunctionParameters.Name = "lblFaultingFunctionParameters"
        Me.lblFaultingFunctionParameters.Size = New System.Drawing.Size(63, 79)
        Me.lblFaultingFunctionParameters.TabIndex = 27
        Me.lblFaultingFunctionParameters.Text = "Faulting Function Parameters"
        '
        'txtFaultIdent
        '
        Me.txtFaultIdent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultIdent.Location = New System.Drawing.Point(158, 258)
        Me.txtFaultIdent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultIdent.Name = "txtFaultIdent"
        Me.txtFaultIdent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFaultIdent.Size = New System.Drawing.Size(188, 25)
        Me.txtFaultIdent.TabIndex = 28
        Me.txtFaultIdent.Text = "txtFaultIdent"
        '
        'lblFaultIdent
        '
        Me.lblFaultIdent.AutoSize = True
        Me.lblFaultIdent.Location = New System.Drawing.Point(13, 261)
        Me.lblFaultIdent.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultIdent.Name = "lblFaultIdent"
        Me.lblFaultIdent.Size = New System.Drawing.Size(75, 19)
        Me.lblFaultIdent.TabIndex = 29
        Me.lblFaultIdent.Text = "Fault Ident"
        '
        'txtFaultDescription
        '
        Me.txtFaultDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultDescription.Location = New System.Drawing.Point(155, 86)
        Me.txtFaultDescription.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultDescription.Multiline = True
        Me.txtFaultDescription.Name = "txtFaultDescription"
        Me.txtFaultDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFaultDescription.Size = New System.Drawing.Size(191, 38)
        Me.txtFaultDescription.TabIndex = 30
        Me.txtFaultDescription.Text = "txtFaultDescription"
        '
        'lblFaultDescription
        '
        Me.lblFaultDescription.AutoSize = True
        Me.lblFaultDescription.Location = New System.Drawing.Point(13, 86)
        Me.lblFaultDescription.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultDescription.Name = "lblFaultDescription"
        Me.lblFaultDescription.Size = New System.Drawing.Size(112, 19)
        Me.lblFaultDescription.TabIndex = 31
        Me.lblFaultDescription.Text = "Fault Description"
        '
        'txtMessageSentToUser
        '
        Me.txtMessageSentToUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessageSentToUser.Location = New System.Drawing.Point(155, 139)
        Me.txtMessageSentToUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtMessageSentToUser.Multiline = True
        Me.txtMessageSentToUser.Name = "txtMessageSentToUser"
        Me.txtMessageSentToUser.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessageSentToUser.Size = New System.Drawing.Size(191, 30)
        Me.txtMessageSentToUser.TabIndex = 32
        Me.txtMessageSentToUser.Text = "txtMessageSentToUser"
        '
        'lblMessageSentToUser
        '
        Me.lblMessageSentToUser.AutoSize = True
        Me.lblMessageSentToUser.Location = New System.Drawing.Point(13, 139)
        Me.lblMessageSentToUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblMessageSentToUser.Name = "lblMessageSentToUser"
        Me.lblMessageSentToUser.Size = New System.Drawing.Size(113, 19)
        Me.lblMessageSentToUser.TabIndex = 33
        Me.lblMessageSentToUser.Text = "Message To User"
        '
        'txtActionSentToUser
        '
        Me.txtActionSentToUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtActionSentToUser.Location = New System.Drawing.Point(155, 184)
        Me.txtActionSentToUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtActionSentToUser.Multiline = True
        Me.txtActionSentToUser.Name = "txtActionSentToUser"
        Me.txtActionSentToUser.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtActionSentToUser.Size = New System.Drawing.Size(191, 30)
        Me.txtActionSentToUser.TabIndex = 34
        Me.txtActionSentToUser.Text = "txtActionSentToUser"
        '
        'lblActionSentToUser
        '
        Me.lblActionSentToUser.AutoSize = True
        Me.lblActionSentToUser.Location = New System.Drawing.Point(13, 184)
        Me.lblActionSentToUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblActionSentToUser.Name = "lblActionSentToUser"
        Me.lblActionSentToUser.Size = New System.Drawing.Size(98, 19)
        Me.lblActionSentToUser.TabIndex = 35
        Me.lblActionSentToUser.Text = "Action To User"
        '
        'cboFaultType
        '
        Me.cboFaultType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFaultType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFaultType.FormattingEnabled = True
        Me.cboFaultType.Location = New System.Drawing.Point(84, 13)
        Me.cboFaultType.Name = "cboFaultType"
        Me.cboFaultType.Size = New System.Drawing.Size(48, 25)
        Me.cboFaultType.TabIndex = 2
        '
        'txtFaultType
        '
        Me.txtFaultType.Location = New System.Drawing.Point(65, 229)
        Me.txtFaultType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultType.Name = "txtFaultType"
        Me.txtFaultType.Size = New System.Drawing.Size(72, 25)
        Me.txtFaultType.TabIndex = 38
        Me.txtFaultType.Text = "txtFaultType"
        '
        'lblFaultType
        '
        Me.lblFaultType.AutoSize = True
        Me.lblFaultType.Location = New System.Drawing.Point(13, 232)
        Me.lblFaultType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblFaultType.Name = "lblFaultType"
        Me.lblFaultType.Size = New System.Drawing.Size(37, 19)
        Me.lblFaultType.TabIndex = 39
        Me.lblFaultType.Text = "Type"
        '
        'cboFaultSeverity
        '
        Me.cboFaultSeverity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFaultSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFaultSeverity.FormattingEnabled = True
        Me.cboFaultSeverity.Location = New System.Drawing.Point(84, 39)
        Me.cboFaultSeverity.Name = "cboFaultSeverity"
        Me.cboFaultSeverity.Size = New System.Drawing.Size(48, 25)
        Me.cboFaultSeverity.TabIndex = 2
        '
        'txtFaultSeverity
        '
        Me.txtFaultSeverity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFaultSeverity.Location = New System.Drawing.Point(213, 229)
        Me.txtFaultSeverity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtFaultSeverity.Name = "txtFaultSeverity"
        Me.txtFaultSeverity.Size = New System.Drawing.Size(133, 25)
        Me.txtFaultSeverity.TabIndex = 42
        Me.txtFaultSeverity.Text = "txtFaultSeverity"
        '
        'lblFaultSeverity
        '
        Me.lblFaultSeverity.AutoSize = True
        Me.lblFaultSeverity.Location = New System.Drawing.Point(148, 232)
        Me.lblFaultSeverity.Name = "lblFaultSeverity"
        Me.lblFaultSeverity.Size = New System.Drawing.Size(57, 19)
        Me.lblFaultSeverity.TabIndex = 43
        Me.lblFaultSeverity.Text = "Severity"
        '
        'txtLoggedLogin
        '
        Me.txtLoggedLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLoggedLogin.Location = New System.Drawing.Point(158, 298)
        Me.txtLoggedLogin.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtLoggedLogin.Name = "txtLoggedLogin"
        Me.txtLoggedLogin.Size = New System.Drawing.Size(188, 25)
        Me.txtLoggedLogin.TabIndex = 46
        Me.txtLoggedLogin.Text = "txtLoggedLogin"
        '
        'lblLoggedLogin
        '
        Me.lblLoggedLogin.AutoSize = True
        Me.lblLoggedLogin.Location = New System.Drawing.Point(13, 301)
        Me.lblLoggedLogin.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblLoggedLogin.Name = "lblLoggedLogin"
        Me.lblLoggedLogin.Size = New System.Drawing.Size(93, 19)
        Me.lblLoggedLogin.TabIndex = 47
        Me.lblLoggedLogin.Text = "Logged Login"
        '
        'txtThread
        '
        Me.txtThread.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtThread.Location = New System.Drawing.Point(158, 338)
        Me.txtThread.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtThread.Name = "txtThread"
        Me.txtThread.Size = New System.Drawing.Size(188, 25)
        Me.txtThread.TabIndex = 48
        Me.txtThread.Text = "txtThread"
        '
        'lblThread
        '
        Me.lblThread.AutoSize = True
        Me.lblThread.Location = New System.Drawing.Point(13, 341)
        Me.lblThread.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblThread.Name = "lblThread"
        Me.lblThread.Size = New System.Drawing.Size(51, 19)
        Me.lblThread.TabIndex = 49
        Me.lblThread.Text = "Thread"
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
        Me.tbcFault.TabIndex = 96
        '
        'tbpInfo
        '
        Me.tbpInfo.BackColor = System.Drawing.Color.Wheat
        Me.tbpInfo.Controls.Add(Me.tlp1)
        Me.tbpInfo.Location = New System.Drawing.Point(4, 26)
        Me.tbpInfo.Name = "tbpInfo"
        Me.tbpInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpInfo.Size = New System.Drawing.Size(766, 490)
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
        Me.tlp1.Size = New System.Drawing.Size(760, 484)
        Me.tlp1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel1.Size = New System.Drawing.Size(374, 478)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtCallingApplication)
        Me.GroupBox3.Controls.Add(Me.txtCallingFunctionWithinApplication)
        Me.GroupBox3.Controls.Add(Me.txtCallingApplicationVersion)
        Me.GroupBox3.Controls.Add(Me.lblSystemName)
        Me.GroupBox3.Controls.Add(Me.lblCallingApplication)
        Me.GroupBox3.Controls.Add(Me.lblCallingFunctionWithinApplication)
        Me.GroupBox3.Controls.Add(Me.txtSystemName)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(5, 273)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(364, 197)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "What was affected"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFaultNumber)
        Me.GroupBox1.Controls.Add(Me.lblFaultNumber)
        Me.GroupBox1.Controls.Add(Me.txtFaultDescription)
        Me.GroupBox1.Controls.Add(Me.txtFaultType)
        Me.GroupBox1.Controls.Add(Me.lblFaultType)
        Me.GroupBox1.Controls.Add(Me.lblTimeOccurred)
        Me.GroupBox1.Controls.Add(Me.txtFaultSeverity)
        Me.GroupBox1.Controls.Add(Me.lblFaultDescription)
        Me.GroupBox1.Controls.Add(Me.lblFaultSeverity)
        Me.GroupBox1.Controls.Add(Me.txtMessageSentToUser)
        Me.GroupBox1.Controls.Add(Me.lblActionSentToUser)
        Me.GroupBox1.Controls.Add(Me.txtActionSentToUser)
        Me.GroupBox1.Controls.Add(Me.lblMessageSentToUser)
        Me.GroupBox1.Controls.Add(Me.txtTimeOccurred)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(364, 268)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "About the Fault"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox6)
        Me.Panel2.Controls.Add(Me.gpbAdditionalDetails)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(383, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel2.Size = New System.Drawing.Size(374, 478)
        Me.Panel2.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtFreeText)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(15)
        Me.GroupBox2.Size = New System.Drawing.Size(364, 243)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "What Happenned"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblUserIdentityType)
        Me.GroupBox6.Controls.Add(Me.txtUserIdentityType)
        Me.GroupBox6.Controls.Add(Me.txtUserIdentityTypeName)
        Me.GroupBox6.Controls.Add(Me.lblUserIdentityTypeName)
        Me.GroupBox6.Controls.Add(Me.txtAffectedUser)
        Me.GroupBox6.Controls.Add(Me.lblAffectedUser)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox6.Location = New System.Drawing.Point(5, 248)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(364, 113)
        Me.GroupBox6.TabIndex = 79
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Who was affected"
        '
        'lblUserIdentityType
        '
        Me.lblUserIdentityType.AutoSize = True
        Me.lblUserIdentityType.Location = New System.Drawing.Point(13, 79)
        Me.lblUserIdentityType.Name = "lblUserIdentityType"
        Me.lblUserIdentityType.Size = New System.Drawing.Size(120, 19)
        Me.lblUserIdentityType.TabIndex = 63
        Me.lblUserIdentityType.Text = "User Identity Type"
        '
        'txtUserIdentityType
        '
        Me.txtUserIdentityType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserIdentityType.Location = New System.Drawing.Point(162, 76)
        Me.txtUserIdentityType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtUserIdentityType.Name = "txtUserIdentityType"
        Me.txtUserIdentityType.Size = New System.Drawing.Size(181, 25)
        Me.txtUserIdentityType.TabIndex = 62
        Me.txtUserIdentityType.Text = "txtUserIdentityType"
        '
        'txtUserIdentityTypeName
        '
        Me.txtUserIdentityTypeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserIdentityTypeName.Location = New System.Drawing.Point(162, 76)
        Me.txtUserIdentityTypeName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtUserIdentityTypeName.Name = "txtUserIdentityTypeName"
        Me.txtUserIdentityTypeName.Size = New System.Drawing.Size(181, 25)
        Me.txtUserIdentityTypeName.TabIndex = 64
        Me.txtUserIdentityTypeName.Text = "txtUserIdentityTypeName"
        '
        'lblUserIdentityTypeName
        '
        Me.lblUserIdentityTypeName.AutoSize = True
        Me.lblUserIdentityTypeName.Location = New System.Drawing.Point(13, 79)
        Me.lblUserIdentityTypeName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblUserIdentityTypeName.Name = "lblUserIdentityTypeName"
        Me.lblUserIdentityTypeName.Size = New System.Drawing.Size(160, 19)
        Me.lblUserIdentityTypeName.TabIndex = 65
        Me.lblUserIdentityTypeName.Text = "User Identity Type Name"
        '
        'txtAffectedUser
        '
        Me.txtAffectedUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAffectedUser.Location = New System.Drawing.Point(162, 36)
        Me.txtAffectedUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtAffectedUser.Name = "txtAffectedUser"
        Me.txtAffectedUser.Size = New System.Drawing.Size(181, 25)
        Me.txtAffectedUser.TabIndex = 15
        Me.txtAffectedUser.Text = "txtAffectedUser"
        '
        'lblAffectedUser
        '
        Me.lblAffectedUser.AutoSize = True
        Me.lblAffectedUser.Location = New System.Drawing.Point(13, 39)
        Me.lblAffectedUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblAffectedUser.Name = "lblAffectedUser"
        Me.lblAffectedUser.Size = New System.Drawing.Size(91, 19)
        Me.lblAffectedUser.TabIndex = 16
        Me.lblAffectedUser.Text = "Affected User"
        '
        'gpbAdditionalDetails
        '
        Me.gpbAdditionalDetails.Controls.Add(Me.txt02)
        Me.gpbAdditionalDetails.Controls.Add(Me.lbl02)
        Me.gpbAdditionalDetails.Controls.Add(Me.txt01)
        Me.gpbAdditionalDetails.Controls.Add(Me.lbl01)
        Me.gpbAdditionalDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gpbAdditionalDetails.Location = New System.Drawing.Point(5, 361)
        Me.gpbAdditionalDetails.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.gpbAdditionalDetails.Name = "gpbAdditionalDetails"
        Me.gpbAdditionalDetails.Size = New System.Drawing.Size(364, 112)
        Me.gpbAdditionalDetails.TabIndex = 78
        Me.gpbAdditionalDetails.TabStop = False
        Me.gpbAdditionalDetails.Text = "Additional Details"
        '
        'txt02
        '
        Me.txt02.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt02.Location = New System.Drawing.Point(162, 76)
        Me.txt02.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txt02.Name = "txt02"
        Me.txt02.Size = New System.Drawing.Size(181, 25)
        Me.txt02.TabIndex = 12
        Me.txt02.Text = "txt02"
        '
        'lbl02
        '
        Me.lbl02.AutoSize = True
        Me.lbl02.Location = New System.Drawing.Point(13, 79)
        Me.lbl02.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lbl02.Name = "lbl02"
        Me.lbl02.Size = New System.Drawing.Size(39, 19)
        Me.lbl02.TabIndex = 13
        Me.lbl02.Text = "lbl02"
        '
        'txt01
        '
        Me.txt01.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt01.Location = New System.Drawing.Point(162, 36)
        Me.txt01.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txt01.Name = "txt01"
        Me.txt01.Size = New System.Drawing.Size(181, 25)
        Me.txt01.TabIndex = 12
        Me.txt01.Text = "txt01"
        '
        'lbl01
        '
        Me.lbl01.AutoSize = True
        Me.lbl01.Location = New System.Drawing.Point(13, 39)
        Me.lbl01.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lbl01.Name = "lbl01"
        Me.lbl01.Size = New System.Drawing.Size(39, 19)
        Me.lbl01.TabIndex = 13
        Me.lbl01.Text = "lbl01"
        '
        'tbpExtraDetails
        '
        Me.tbpExtraDetails.BackColor = System.Drawing.Color.Wheat
        Me.tbpExtraDetails.Controls.Add(Me.TableLayoutPanel1)
        Me.tbpExtraDetails.Location = New System.Drawing.Point(4, 26)
        Me.tbpExtraDetails.Name = "tbpExtraDetails"
        Me.tbpExtraDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpExtraDetails.Size = New System.Drawing.Size(766, 490)
        Me.tbpExtraDetails.TabIndex = 1
        Me.tbpExtraDetails.Text = "Extra Details"
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
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 488.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(760, 488)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GroupBox5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel3.Size = New System.Drawing.Size(374, 482)
        Me.Panel3.TabIndex = 2
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtFaultingFunctionParameters)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(15)
        Me.GroupBox5.Size = New System.Drawing.Size(364, 330)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Fault Parameters"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox4)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(383, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel4.Size = New System.Drawing.Size(374, 482)
        Me.Panel4.TabIndex = 3
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtFaultingAssembly)
        Me.GroupBox4.Controls.Add(Me.txtFaultingFunction)
        Me.GroupBox4.Controls.Add(Me.txtFaultingClass)
        Me.GroupBox4.Controls.Add(Me.txtFaultIdent)
        Me.GroupBox4.Controls.Add(Me.lblFaultIdent)
        Me.GroupBox4.Controls.Add(Me.txtAssemblyEntryPoint)
        Me.GroupBox4.Controls.Add(Me.lblFaultingAssembly)
        Me.GroupBox4.Controls.Add(Me.lblFaultingFunction)
        Me.GroupBox4.Controls.Add(Me.lblAssemblyEntryPoint)
        Me.GroupBox4.Controls.Add(Me.lblFaultingClass)
        Me.GroupBox4.Controls.Add(Me.txtLoggedLogin)
        Me.GroupBox4.Controls.Add(Me.lblLoggedLogin)
        Me.GroupBox4.Controls.Add(Me.lblThread)
        Me.GroupBox4.Controls.Add(Me.txtThread)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(364, 387)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Finer Details"
        '
        'btnJumpTo
        '
        Me.btnJumpTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnJumpTo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnJumpTo.Location = New System.Drawing.Point(195, 541)
        Me.btnJumpTo.Name = "btnJumpTo"
        Me.btnJumpTo.Size = New System.Drawing.Size(362, 26)
        Me.btnJumpTo.TabIndex = 77
        Me.btnJumpTo.Text = "Jump to"
        Me.btnJumpTo.UseVisualStyleBackColor = True
        Me.btnJumpTo.Visible = False
        '
        'pnlGarbage
        '
        Me.pnlGarbage.Controls.Add(Me.txtMonthOccurred)
        Me.pnlGarbage.Controls.Add(Me.cboUserIdentityType)
        Me.pnlGarbage.Controls.Add(Me.lblMonthOccurred)
        Me.pnlGarbage.Controls.Add(Me.cboAffectedUser)
        Me.pnlGarbage.Controls.Add(Me.txtDateOccurred)
        Me.pnlGarbage.Controls.Add(Me.lblDateOccurred)
        Me.pnlGarbage.Controls.Add(Me.cboFaultType)
        Me.pnlGarbage.Controls.Add(Me.cboUserIdentityTypeName)
        Me.pnlGarbage.Controls.Add(Me.lblFreeText)
        Me.pnlGarbage.Controls.Add(Me.lblFaultingFunctionParameters)
        Me.pnlGarbage.Controls.Add(Me.lblCallingApplicationVersion)
        Me.pnlGarbage.Controls.Add(Me.cboLoggedLogin)
        Me.pnlGarbage.Controls.Add(Me.cboFaultSeverity)
        Me.pnlGarbage.Location = New System.Drawing.Point(20, 435)
        Me.pnlGarbage.Name = "pnlGarbage"
        Me.pnlGarbage.Size = New System.Drawing.Size(200, 100)
        Me.pnlGarbage.TabIndex = 97
        Me.pnlGarbage.Visible = False
        '
        'cboUserIdentityType
        '
        Me.cboUserIdentityType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboUserIdentityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUserIdentityType.FormattingEnabled = True
        Me.cboUserIdentityType.Location = New System.Drawing.Point(40, 47)
        Me.cboUserIdentityType.Name = "cboUserIdentityType"
        Me.cboUserIdentityType.Size = New System.Drawing.Size(32, 25)
        Me.cboUserIdentityType.TabIndex = 60
        '
        'cboUserIdentityTypeName
        '
        Me.cboUserIdentityTypeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboUserIdentityTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUserIdentityTypeName.FormattingEnabled = True
        Me.cboUserIdentityTypeName.Location = New System.Drawing.Point(40, 73)
        Me.cboUserIdentityTypeName.Name = "cboUserIdentityTypeName"
        Me.cboUserIdentityTypeName.Size = New System.Drawing.Size(32, 25)
        Me.cboUserIdentityTypeName.TabIndex = 61
        '
        'txtMonthOccurred
        '
        Me.txtMonthOccurred.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMonthOccurred.Location = New System.Drawing.Point(60, 107)
        Me.txtMonthOccurred.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtMonthOccurred.Name = "txtMonthOccurred"
        Me.txtMonthOccurred.Size = New System.Drawing.Size(176, 25)
        Me.txtMonthOccurred.TabIndex = 105
        Me.txtMonthOccurred.Text = "txtMonthOccurred"
        '
        'lblMonthOccurred
        '
        Me.lblMonthOccurred.AutoSize = True
        Me.lblMonthOccurred.Location = New System.Drawing.Point(-42, 110)
        Me.lblMonthOccurred.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblMonthOccurred.Name = "lblMonthOccurred"
        Me.lblMonthOccurred.Size = New System.Drawing.Size(107, 19)
        Me.lblMonthOccurred.TabIndex = 106
        Me.lblMonthOccurred.Text = "MonthOccurred"
        '
        'txtDateOccurred
        '
        Me.txtDateOccurred.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDateOccurred.Location = New System.Drawing.Point(60, 82)
        Me.txtDateOccurred.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
        Me.txtDateOccurred.Name = "txtDateOccurred"
        Me.txtDateOccurred.Size = New System.Drawing.Size(176, 25)
        Me.txtDateOccurred.TabIndex = 103
        Me.txtDateOccurred.Text = "txtDateOccurred"
        '
        'lblDateOccurred
        '
        Me.lblDateOccurred.AutoSize = True
        Me.lblDateOccurred.Location = New System.Drawing.Point(-42, 85)
        Me.lblDateOccurred.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblDateOccurred.Name = "lblDateOccurred"
        Me.lblDateOccurred.Size = New System.Drawing.Size(94, 19)
        Me.lblDateOccurred.TabIndex = 104
        Me.lblDateOccurred.Text = "DateOccurred"
        '
        'cboAffectedUser
        '
        Me.cboAffectedUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAffectedUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAffectedUser.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboAffectedUser.Location = New System.Drawing.Point(48, 65)
        Me.cboAffectedUser.Name = "cboAffectedUser"
        Me.cboAffectedUser.Size = New System.Drawing.Size(113, 21)
        Me.cboAffectedUser.TabIndex = 14
        '
        'cboLoggedLogin
        '
        Me.cboLoggedLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboLoggedLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLoggedLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboLoggedLogin.Location = New System.Drawing.Point(84, 65)
        Me.cboLoggedLogin.Name = "cboLoggedLogin"
        Me.cboLoggedLogin.Size = New System.Drawing.Size(48, 21)
        Me.cboLoggedLogin.TabIndex = 2
        '
        'ctlc_LoggedAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Wheat
        Me.Controls.Add(Me.tbcFault)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.btnJumpTo)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.pnlGarbage)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Name = "ctlc_LoggedAlert"
        Me.Size = New System.Drawing.Size(774, 591)
        Me.tbcFault.ResumeLayout(False)
        Me.tbpInfo.ResumeLayout(False)
        Me.tlp1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.gpbAdditionalDetails.ResumeLayout(False)
        Me.gpbAdditionalDetails.PerformLayout()
        Me.tbpExtraDetails.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlGarbage.ResumeLayout(False)
        Me.pnlGarbage.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtTimeOccurred As System.Windows.Forms.TextBox
  Friend WithEvents lblTimeOccurred As System.Windows.Forms.Label
  Friend WithEvents txtFaultNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultNumber As System.Windows.Forms.Label
  Friend WithEvents txtSystemName As System.Windows.Forms.TextBox
  Friend WithEvents lblSystemName As System.Windows.Forms.Label
  Friend WithEvents txtCallingApplication As System.Windows.Forms.TextBox
  Friend WithEvents lblCallingApplication As System.Windows.Forms.Label
  Friend WithEvents txtCallingApplicationVersion As System.Windows.Forms.TextBox
  Friend WithEvents lblCallingApplicationVersion As System.Windows.Forms.Label
  Friend WithEvents txtCallingFunctionWithinApplication As System.Windows.Forms.TextBox
  Friend WithEvents lblCallingFunctionWithinApplication As System.Windows.Forms.Label
  Friend WithEvents txtFreeText As System.Windows.Forms.TextBox
  Friend WithEvents lblFreeText As System.Windows.Forms.Label
  Friend WithEvents txtFaultingAssembly As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultingAssembly As System.Windows.Forms.Label
  Friend WithEvents txtAssemblyEntryPoint As System.Windows.Forms.TextBox
  Friend WithEvents lblAssemblyEntryPoint As System.Windows.Forms.Label
  Friend WithEvents txtFaultingClass As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultingClass As System.Windows.Forms.Label
  Friend WithEvents txtFaultingFunction As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultingFunction As System.Windows.Forms.Label
  Friend WithEvents txtFaultingFunctionParameters As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultingFunctionParameters As System.Windows.Forms.Label
  Friend WithEvents txtFaultIdent As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultIdent As System.Windows.Forms.Label
  Friend WithEvents txtFaultDescription As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultDescription As System.Windows.Forms.Label
  Friend WithEvents txtMessageSentToUser As System.Windows.Forms.TextBox
  Friend WithEvents lblMessageSentToUser As System.Windows.Forms.Label
  Friend WithEvents txtActionSentToUser As System.Windows.Forms.TextBox
  Friend WithEvents lblActionSentToUser As System.Windows.Forms.Label
  Friend WithEvents cboFaultType As System.Windows.Forms.ComboBox
  Friend WithEvents txtFaultType As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultType As System.Windows.Forms.Label
  Friend WithEvents cboFaultSeverity As System.Windows.Forms.ComboBox
  Friend WithEvents txtFaultSeverity As System.Windows.Forms.TextBox
  Friend WithEvents lblFaultSeverity As System.Windows.Forms.Label
  Friend WithEvents cboLoggedLogin As IntelliCombo
  Friend WithEvents txtLoggedLogin As System.Windows.Forms.TextBox
  Friend WithEvents lblLoggedLogin As System.Windows.Forms.Label
  Friend WithEvents txtThread As System.Windows.Forms.TextBox
  Friend WithEvents lblThread As System.Windows.Forms.Label
  Friend WithEvents tbcFault As TabControl
  Friend WithEvents tbpInfo As TabPage
  Friend WithEvents tlp1 As TableLayoutPanel
  Friend WithEvents Panel1 As Panel
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Panel2 As Panel
  Friend WithEvents tbpExtraDetails As TabPage
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents Panel3 As Panel
  Friend WithEvents Panel4 As Panel
  Friend WithEvents GroupBox2 As GroupBox
  Friend WithEvents GroupBox3 As GroupBox
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents pnlGarbage As Panel
  Friend WithEvents btnJumpTo As Button
  Friend WithEvents gpbAdditionalDetails As GroupBox
  Friend WithEvents txt02 As TextBox
  Friend WithEvents lbl02 As Label
  Friend WithEvents txt01 As TextBox
  Friend WithEvents lbl01 As Label
  Friend WithEvents GroupBox5 As GroupBox
  Friend WithEvents GroupBox6 As GroupBox
  Friend WithEvents txtAffectedUser As TextBox
  Friend WithEvents lblAffectedUser As Label
  Friend WithEvents cboAffectedUser As IntelliCombo
  Friend WithEvents lblUserIdentityType As Label
  Friend WithEvents txtUserIdentityTypeName As TextBox
  Friend WithEvents lblUserIdentityTypeName As Label
  Friend WithEvents cboUserIdentityType As ComboBox
  Friend WithEvents cboUserIdentityTypeName As ComboBox
  Friend WithEvents txtUserIdentityType As TextBox
    Friend WithEvents txtMonthOccurred As TextBox
    Friend WithEvents lblMonthOccurred As Label
    Friend WithEvents txtDateOccurred As TextBox
    Friend WithEvents lblDateOccurred As Label
End Class
