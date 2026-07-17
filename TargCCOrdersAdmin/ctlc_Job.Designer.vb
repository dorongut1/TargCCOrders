'Me.BackColor = System.Drawing.XXX --> System.Drawing.Color.Wheat

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlc_Job
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
    Me.cboJob = New System.Windows.Forms.ComboBox()
    Me.txtJob = New System.Windows.Forms.TextBox()
    Me.lblJob = New System.Windows.Forms.Label()
    Me.cboJobRunner = New System.Windows.Forms.ComboBox()
    Me.txtJobRunner = New System.Windows.Forms.TextBox()
    Me.lblJobRunner = New System.Windows.Forms.Label()
    Me.txtDescription = New System.Windows.Forms.TextBox()
    Me.lblDescription = New System.Windows.Forms.Label()
    Me.txtInstructions = New System.Windows.Forms.TextBox()
    Me.lblInstructions = New System.Windows.Forms.Label()
    Me.cboJobType = New System.Windows.Forms.ComboBox()
    Me.txtJobType = New System.Windows.Forms.TextBox()
    Me.lblJobType = New System.Windows.Forms.Label()
    Me.dtpWhenToRun = New System.Windows.Forms.DateTimePicker()
    Me.txtWhenToRun = New System.Windows.Forms.TextBox()
    Me.lblWhenToRun = New System.Windows.Forms.Label()
    Me.txtCyclicCount = New System.Windows.Forms.TextBox()
    Me.lblCyclicCount = New System.Windows.Forms.Label()
    Me.chkSendNotificationOnSuccess = New System.Windows.Forms.CheckBox()
    Me.lblSendNotificationOnSuccess = New System.Windows.Forms.Label()
    Me.chkSendAlarmOnMissed = New System.Windows.Forms.CheckBox()
    Me.lblSendAlarmOnMissed = New System.Windows.Forms.Label()
    Me.txtTimeOutSec = New System.Windows.Forms.TextBox()
    Me.lblTimeOutSec = New System.Windows.Forms.Label()
    Me.chkActive = New System.Windows.Forms.CheckBox()
    Me.lblActive = New System.Windows.Forms.Label()
    Me.txtActivatingUser = New System.Windows.Forms.TextBox()
    Me.lblActivatingUser = New System.Windows.Forms.Label()
    Me.txtNextRunTime = New System.Windows.Forms.TextBox()
    Me.lblNextRunTime = New System.Windows.Forms.Label()
    Me.txtLastRunTime = New System.Windows.Forms.TextBox()
    Me.lblLastRunTime = New System.Windows.Forms.Label()
    Me.cboJobStatus = New System.Windows.Forms.ComboBox()
    Me.txtJobStatus = New System.Windows.Forms.TextBox()
    Me.lblJobStatus = New System.Windows.Forms.Label()
    Me.chkWarningMailSent = New System.Windows.Forms.CheckBox()
    Me.lblWarningMailSent = New System.Windows.Forms.Label()
    Me.chkIsManaged = New System.Windows.Forms.CheckBox()
    Me.lblIsManaged = New System.Windows.Forms.Label()
    Me.txtLastRunBy = New System.Windows.Forms.TextBox()
    Me.lblLastRunBy = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.btnRunNow = New System.Windows.Forms.Button()
    Me.tbcFault = New System.Windows.Forms.TabControl()
    Me.tbpInfo = New System.Windows.Forms.TabPage()
    Me.tlp1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.tbcFault.SuspendLayout()
    Me.tbpInfo.SuspendLayout()
    Me.tlp1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.GroupBox4.SuspendLayout()
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
    'cboJob
    '
    Me.cboJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJob.FormattingEnabled = True
    Me.cboJob.Location = New System.Drawing.Point(199, 22)
    Me.cboJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboJob.Name = "cboJob"
    Me.cboJob.Size = New System.Drawing.Size(147, 25)
    Me.cboJob.TabIndex = 2
    '
    'txtJob
    '
    Me.txtJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJob.Location = New System.Drawing.Point(116, 36)
    Me.txtJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtJob.Name = "txtJob"
    Me.txtJob.Size = New System.Drawing.Size(233, 25)
    Me.txtJob.TabIndex = 4
    Me.txtJob.Text = "txtJob"
    '
    'lblJob
    '
    Me.lblJob.AutoSize = True
    Me.lblJob.Location = New System.Drawing.Point(13, 39)
    Me.lblJob.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblJob.Name = "lblJob"
    Me.lblJob.Size = New System.Drawing.Size(30, 19)
    Me.lblJob.TabIndex = 5
    Me.lblJob.Text = "Job"
    '
    'cboJobRunner
    '
    Me.cboJobRunner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJobRunner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJobRunner.FormattingEnabled = True
    Me.cboJobRunner.Location = New System.Drawing.Point(186, 65)
    Me.cboJobRunner.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboJobRunner.Name = "cboJobRunner"
    Me.cboJobRunner.Size = New System.Drawing.Size(147, 25)
    Me.cboJobRunner.TabIndex = 2
    '
    'txtJobRunner
    '
    Me.txtJobRunner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJobRunner.Location = New System.Drawing.Point(116, 76)
    Me.txtJobRunner.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtJobRunner.Name = "txtJobRunner"
    Me.txtJobRunner.Size = New System.Drawing.Size(233, 25)
    Me.txtJobRunner.TabIndex = 8
    Me.txtJobRunner.Text = "txtJobRunner"
    '
    'lblJobRunner
    '
    Me.lblJobRunner.AutoSize = True
    Me.lblJobRunner.Location = New System.Drawing.Point(13, 79)
    Me.lblJobRunner.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblJobRunner.Name = "lblJobRunner"
    Me.lblJobRunner.Size = New System.Drawing.Size(78, 19)
    Me.lblJobRunner.TabIndex = 9
    Me.lblJobRunner.Text = "Job Runner"
    '
    'txtDescription
    '
    Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDescription.Location = New System.Drawing.Point(116, 116)
    Me.txtDescription.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtDescription.Multiline = True
    Me.txtDescription.Name = "txtDescription"
    Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtDescription.Size = New System.Drawing.Size(233, 40)
    Me.txtDescription.TabIndex = 10
    Me.txtDescription.Text = "txtDescription"
    '
    'lblDescription
    '
    Me.lblDescription.AutoSize = True
    Me.lblDescription.Location = New System.Drawing.Point(13, 119)
    Me.lblDescription.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblDescription.Name = "lblDescription"
    Me.lblDescription.Size = New System.Drawing.Size(78, 19)
    Me.lblDescription.TabIndex = 11
    Me.lblDescription.Text = "Description"
    '
    'txtInstructions
    '
    Me.txtInstructions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtInstructions.Location = New System.Drawing.Point(116, 171)
    Me.txtInstructions.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtInstructions.Multiline = True
    Me.txtInstructions.Name = "txtInstructions"
    Me.txtInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtInstructions.Size = New System.Drawing.Size(233, 40)
    Me.txtInstructions.TabIndex = 12
    Me.txtInstructions.Text = "txtInstructions"
    '
    'lblInstructions
    '
    Me.lblInstructions.AutoSize = True
    Me.lblInstructions.Location = New System.Drawing.Point(13, 174)
    Me.lblInstructions.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblInstructions.Name = "lblInstructions"
    Me.lblInstructions.Size = New System.Drawing.Size(81, 19)
    Me.lblInstructions.TabIndex = 13
    Me.lblInstructions.Text = "Instructions"
    '
    'cboJobType
    '
    Me.cboJobType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJobType.FormattingEnabled = True
    Me.cboJobType.Location = New System.Drawing.Point(139, 241)
    Me.cboJobType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboJobType.Name = "cboJobType"
    Me.cboJobType.Size = New System.Drawing.Size(111, 25)
    Me.cboJobType.TabIndex = 2
    '
    'txtJobType
    '
    Me.txtJobType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJobType.Location = New System.Drawing.Point(117, 251)
    Me.txtJobType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtJobType.Name = "txtJobType"
    Me.txtJobType.Size = New System.Drawing.Size(133, 25)
    Me.txtJobType.TabIndex = 16
    Me.txtJobType.Text = "txtJobType"
    '
    'lblJobType
    '
    Me.lblJobType.AutoSize = True
    Me.lblJobType.Location = New System.Drawing.Point(13, 254)
    Me.lblJobType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblJobType.Name = "lblJobType"
    Me.lblJobType.Size = New System.Drawing.Size(72, 19)
    Me.lblJobType.TabIndex = 17
    Me.lblJobType.Text = "Frequency"
    '
    'dtpWhenToRun
    '
    Me.dtpWhenToRun.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpWhenToRun.CustomFormat = "dd/MM/yyyy HH:mm:ss"
    Me.dtpWhenToRun.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpWhenToRun.Location = New System.Drawing.Point(139, 281)
    Me.dtpWhenToRun.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.dtpWhenToRun.Name = "dtpWhenToRun"
    Me.dtpWhenToRun.ShowCheckBox = True
    Me.dtpWhenToRun.ShowUpDown = True
    Me.dtpWhenToRun.Size = New System.Drawing.Size(157, 25)
    Me.dtpWhenToRun.TabIndex = 18
    '
    'txtWhenToRun
    '
    Me.txtWhenToRun.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWhenToRun.Location = New System.Drawing.Point(116, 291)
    Me.txtWhenToRun.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtWhenToRun.Name = "txtWhenToRun"
    Me.txtWhenToRun.Size = New System.Drawing.Size(233, 25)
    Me.txtWhenToRun.TabIndex = 19
    Me.txtWhenToRun.Text = "txtWhenToRun"
    '
    'lblWhenToRun
    '
    Me.lblWhenToRun.AutoSize = True
    Me.lblWhenToRun.Location = New System.Drawing.Point(13, 292)
    Me.lblWhenToRun.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblWhenToRun.Name = "lblWhenToRun"
    Me.lblWhenToRun.Size = New System.Drawing.Size(91, 19)
    Me.lblWhenToRun.TabIndex = 20
    Me.lblWhenToRun.Text = "When To Run"
    '
    'txtCyclicCount
    '
    Me.txtCyclicCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCyclicCount.Location = New System.Drawing.Point(298, 251)
    Me.txtCyclicCount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtCyclicCount.Name = "txtCyclicCount"
    Me.txtCyclicCount.Size = New System.Drawing.Size(51, 25)
    Me.txtCyclicCount.TabIndex = 21
    Me.txtCyclicCount.Text = "txtCyclicCount"
    '
    'lblCyclicCount
    '
    Me.lblCyclicCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblCyclicCount.AutoSize = True
    Me.lblCyclicCount.Location = New System.Drawing.Point(256, 254)
    Me.lblCyclicCount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblCyclicCount.Name = "lblCyclicCount"
    Me.lblCyclicCount.Size = New System.Drawing.Size(17, 19)
    Me.lblCyclicCount.TabIndex = 22
    Me.lblCyclicCount.Text = "X"
    '
    'chkSendNotificationOnSuccess
    '
    Me.chkSendNotificationOnSuccess.AutoSize = True
    Me.chkSendNotificationOnSuccess.Location = New System.Drawing.Point(274, 76)
    Me.chkSendNotificationOnSuccess.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkSendNotificationOnSuccess.Name = "chkSendNotificationOnSuccess"
    Me.chkSendNotificationOnSuccess.Size = New System.Drawing.Size(15, 14)
    Me.chkSendNotificationOnSuccess.TabIndex = 27
    Me.chkSendNotificationOnSuccess.UseVisualStyleBackColor = True
    '
    'lblSendNotificationOnSuccess
    '
    Me.lblSendNotificationOnSuccess.AutoSize = True
    Me.lblSendNotificationOnSuccess.Location = New System.Drawing.Point(22, 74)
    Me.lblSendNotificationOnSuccess.Name = "lblSendNotificationOnSuccess"
    Me.lblSendNotificationOnSuccess.Size = New System.Drawing.Size(183, 19)
    Me.lblSendNotificationOnSuccess.TabIndex = 28
    Me.lblSendNotificationOnSuccess.Text = "Send Notification on Success"
    '
    'chkSendAlarmOnMissed
    '
    Me.chkSendAlarmOnMissed.AutoSize = True
    Me.chkSendAlarmOnMissed.Location = New System.Drawing.Point(274, 105)
    Me.chkSendAlarmOnMissed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkSendAlarmOnMissed.Name = "chkSendAlarmOnMissed"
    Me.chkSendAlarmOnMissed.Size = New System.Drawing.Size(15, 14)
    Me.chkSendAlarmOnMissed.TabIndex = 29
    Me.chkSendAlarmOnMissed.UseVisualStyleBackColor = True
    '
    'lblSendAlarmOnMissed
    '
    Me.lblSendAlarmOnMissed.AutoSize = True
    Me.lblSendAlarmOnMissed.Location = New System.Drawing.Point(22, 103)
    Me.lblSendAlarmOnMissed.Name = "lblSendAlarmOnMissed"
    Me.lblSendAlarmOnMissed.Size = New System.Drawing.Size(149, 19)
    Me.lblSendAlarmOnMissed.TabIndex = 30
    Me.lblSendAlarmOnMissed.Text = "Send Alarm On Missed"
    '
    'txtTimeOutSec
    '
    Me.txtTimeOutSec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTimeOutSec.Location = New System.Drawing.Point(201, 36)
    Me.txtTimeOutSec.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtTimeOutSec.Name = "txtTimeOutSec"
    Me.txtTimeOutSec.Size = New System.Drawing.Size(148, 25)
    Me.txtTimeOutSec.TabIndex = 31
    Me.txtTimeOutSec.Text = "txtTimeOutSec"
    '
    'lblTimeOutSec
    '
    Me.lblTimeOutSec.AutoSize = True
    Me.lblTimeOutSec.Location = New System.Drawing.Point(22, 39)
    Me.lblTimeOutSec.Name = "lblTimeOutSec"
    Me.lblTimeOutSec.Size = New System.Drawing.Size(90, 19)
    Me.lblTimeOutSec.TabIndex = 32
    Me.lblTimeOutSec.Text = "Time Out Sec"
    '
    'chkActive
    '
    Me.chkActive.AutoSize = True
    Me.chkActive.Location = New System.Drawing.Point(186, 36)
    Me.chkActive.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkActive.Name = "chkActive"
    Me.chkActive.Size = New System.Drawing.Size(15, 14)
    Me.chkActive.TabIndex = 33
    Me.chkActive.UseVisualStyleBackColor = True
    '
    'lblActive
    '
    Me.lblActive.AutoSize = True
    Me.lblActive.Location = New System.Drawing.Point(22, 34)
    Me.lblActive.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblActive.Name = "lblActive"
    Me.lblActive.Size = New System.Drawing.Size(46, 19)
    Me.lblActive.TabIndex = 34
    Me.lblActive.Text = "Active"
    '
    'txtActivatingUser
    '
    Me.txtActivatingUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtActivatingUser.Location = New System.Drawing.Point(149, 191)
    Me.txtActivatingUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtActivatingUser.Name = "txtActivatingUser"
    Me.txtActivatingUser.Size = New System.Drawing.Size(200, 25)
    Me.txtActivatingUser.TabIndex = 35
    Me.txtActivatingUser.Text = "txtActivatingUser"
    '
    'lblActivatingUser
    '
    Me.lblActivatingUser.AutoSize = True
    Me.lblActivatingUser.Location = New System.Drawing.Point(22, 194)
    Me.lblActivatingUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblActivatingUser.Name = "lblActivatingUser"
    Me.lblActivatingUser.Size = New System.Drawing.Size(102, 19)
    Me.lblActivatingUser.TabIndex = 36
    Me.lblActivatingUser.Text = "Activating User"
    '
    'txtNextRunTime
    '
    Me.txtNextRunTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNextRunTime.Location = New System.Drawing.Point(149, 65)
    Me.txtNextRunTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtNextRunTime.Name = "txtNextRunTime"
    Me.txtNextRunTime.Size = New System.Drawing.Size(200, 25)
    Me.txtNextRunTime.TabIndex = 37
    Me.txtNextRunTime.Text = "txtNextRunTime"
    '
    'lblNextRunTime
    '
    Me.lblNextRunTime.AutoSize = True
    Me.lblNextRunTime.Location = New System.Drawing.Point(22, 68)
    Me.lblNextRunTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblNextRunTime.Name = "lblNextRunTime"
    Me.lblNextRunTime.Size = New System.Drawing.Size(98, 19)
    Me.lblNextRunTime.TabIndex = 38
    Me.lblNextRunTime.Text = "Next Run Time"
    '
    'txtLastRunTime
    '
    Me.txtLastRunTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastRunTime.Location = New System.Drawing.Point(149, 105)
    Me.txtLastRunTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastRunTime.Name = "txtLastRunTime"
    Me.txtLastRunTime.Size = New System.Drawing.Size(200, 25)
    Me.txtLastRunTime.TabIndex = 39
    Me.txtLastRunTime.Text = "txtLastRunTime"
    '
    'lblLastRunTime
    '
    Me.lblLastRunTime.AutoSize = True
    Me.lblLastRunTime.Location = New System.Drawing.Point(22, 108)
    Me.lblLastRunTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLastRunTime.Name = "lblLastRunTime"
    Me.lblLastRunTime.Size = New System.Drawing.Size(95, 19)
    Me.lblLastRunTime.TabIndex = 40
    Me.lblLastRunTime.Text = "Last Run Time"
    '
    'cboJobStatus
    '
    Me.cboJobStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJobStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJobStatus.FormattingEnabled = True
    Me.cboJobStatus.Location = New System.Drawing.Point(186, 131)
    Me.cboJobStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.cboJobStatus.Name = "cboJobStatus"
    Me.cboJobStatus.Size = New System.Drawing.Size(109, 25)
    Me.cboJobStatus.TabIndex = 2
    '
    'txtJobStatus
    '
    Me.txtJobStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJobStatus.Location = New System.Drawing.Point(149, 145)
    Me.txtJobStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtJobStatus.Name = "txtJobStatus"
    Me.txtJobStatus.Size = New System.Drawing.Size(200, 25)
    Me.txtJobStatus.TabIndex = 43
    Me.txtJobStatus.Text = "txtJobStatus"
    '
    'lblJobStatus
    '
    Me.lblJobStatus.AutoSize = True
    Me.lblJobStatus.Location = New System.Drawing.Point(22, 148)
    Me.lblJobStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblJobStatus.Name = "lblJobStatus"
    Me.lblJobStatus.Size = New System.Drawing.Size(72, 19)
    Me.lblJobStatus.TabIndex = 44
    Me.lblJobStatus.Text = "Job Status"
    '
    'chkWarningMailSent
    '
    Me.chkWarningMailSent.AutoSize = True
    Me.chkWarningMailSent.Location = New System.Drawing.Point(186, 271)
    Me.chkWarningMailSent.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkWarningMailSent.Name = "chkWarningMailSent"
    Me.chkWarningMailSent.Size = New System.Drawing.Size(15, 14)
    Me.chkWarningMailSent.TabIndex = 45
    Me.chkWarningMailSent.UseVisualStyleBackColor = True
    '
    'lblWarningMailSent
    '
    Me.lblWarningMailSent.AutoSize = True
    Me.lblWarningMailSent.Location = New System.Drawing.Point(22, 269)
    Me.lblWarningMailSent.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblWarningMailSent.Name = "lblWarningMailSent"
    Me.lblWarningMailSent.Size = New System.Drawing.Size(121, 19)
    Me.lblWarningMailSent.TabIndex = 46
    Me.lblWarningMailSent.Text = "Warning Mail Sent"
    '
    'chkIsManaged
    '
    Me.chkIsManaged.AutoSize = True
    Me.chkIsManaged.Location = New System.Drawing.Point(116, 331)
    Me.chkIsManaged.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.chkIsManaged.Name = "chkIsManaged"
    Me.chkIsManaged.Size = New System.Drawing.Size(15, 14)
    Me.chkIsManaged.TabIndex = 47
    Me.chkIsManaged.UseVisualStyleBackColor = True
    '
    'lblIsManaged
    '
    Me.lblIsManaged.AutoSize = True
    Me.lblIsManaged.Location = New System.Drawing.Point(13, 329)
    Me.lblIsManaged.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblIsManaged.Name = "lblIsManaged"
    Me.lblIsManaged.Size = New System.Drawing.Size(81, 19)
    Me.lblIsManaged.TabIndex = 48
    Me.lblIsManaged.Text = "Is Managed"
    '
    'txtLastRunBy
    '
    Me.txtLastRunBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastRunBy.Location = New System.Drawing.Point(149, 231)
    Me.txtLastRunBy.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.txtLastRunBy.Name = "txtLastRunBy"
    Me.txtLastRunBy.Size = New System.Drawing.Size(200, 25)
    Me.txtLastRunBy.TabIndex = 49
    Me.txtLastRunBy.Text = "txtLastRunBy"
    '
    'lblLastRunBy
    '
    Me.lblLastRunBy.AutoSize = True
    Me.lblLastRunBy.Location = New System.Drawing.Point(22, 234)
    Me.lblLastRunBy.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
    Me.lblLastRunBy.Name = "lblLastRunBy"
    Me.lblLastRunBy.Size = New System.Drawing.Size(81, 19)
    Me.lblLastRunBy.TabIndex = 50
    Me.lblLastRunBy.Text = "Last Run By"
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(365, 541)
    Me.btnEdit.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 52
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(260, 541)
    Me.btnAdd.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 53
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(260, 529)
    Me.btnCancel.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 54
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(365, 529)
    Me.btnUpdate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 55
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
    Me.btnDelete.TabIndex = 51
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnRunNow
    '
    Me.btnRunNow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnRunNow.Location = New System.Drawing.Point(470, 541)
    Me.btnRunNow.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0)
    Me.btnRunNow.Name = "btnRunNow"
    Me.btnRunNow.Size = New System.Drawing.Size(75, 26)
    Me.btnRunNow.TabIndex = 54
    Me.btnRunNow.Text = "Run Now"
    Me.btnRunNow.UseVisualStyleBackColor = True
    '
    'tbcFault
    '
    Me.tbcFault.Controls.Add(Me.tbpInfo)
    Me.tbcFault.Dock = System.Windows.Forms.DockStyle.Top
    Me.tbcFault.Location = New System.Drawing.Point(0, 0)
    Me.tbcFault.Name = "tbcFault"
    Me.tbcFault.SelectedIndex = 0
    Me.tbcFault.Size = New System.Drawing.Size(774, 520)
    Me.tbcFault.TabIndex = 98
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
    Me.tlp1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.tlp1.Size = New System.Drawing.Size(760, 484)
    Me.tlp1.TabIndex = 0
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.GroupBox1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(3, 3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel1.Size = New System.Drawing.Size(374, 478)
    Me.Panel1.TabIndex = 2
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.txtJob)
    Me.GroupBox1.Controls.Add(Me.lblJobRunner)
    Me.GroupBox1.Controls.Add(Me.cboJobType)
    Me.GroupBox1.Controls.Add(Me.txtJobRunner)
    Me.GroupBox1.Controls.Add(Me.dtpWhenToRun)
    Me.GroupBox1.Controls.Add(Me.cboJobRunner)
    Me.GroupBox1.Controls.Add(Me.txtDescription)
    Me.GroupBox1.Controls.Add(Me.txtJobType)
    Me.GroupBox1.Controls.Add(Me.lblJobType)
    Me.GroupBox1.Controls.Add(Me.lblDescription)
    Me.GroupBox1.Controls.Add(Me.chkIsManaged)
    Me.GroupBox1.Controls.Add(Me.lblIsManaged)
    Me.GroupBox1.Controls.Add(Me.cboJob)
    Me.GroupBox1.Controls.Add(Me.txtWhenToRun)
    Me.GroupBox1.Controls.Add(Me.txtInstructions)
    Me.GroupBox1.Controls.Add(Me.lblWhenToRun)
    Me.GroupBox1.Controls.Add(Me.lblJob)
    Me.GroupBox1.Controls.Add(Me.txtCyclicCount)
    Me.GroupBox1.Controls.Add(Me.lblInstructions)
    Me.GroupBox1.Controls.Add(Me.lblCyclicCount)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(364, 376)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Definition"
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.GroupBox2)
    Me.Panel2.Controls.Add(Me.GroupBox4)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(383, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
    Me.Panel2.Size = New System.Drawing.Size(374, 478)
    Me.Panel2.TabIndex = 3
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.txtTimeOutSec)
    Me.GroupBox2.Controls.Add(Me.lblTimeOutSec)
    Me.GroupBox2.Controls.Add(Me.chkSendAlarmOnMissed)
    Me.GroupBox2.Controls.Add(Me.lblSendAlarmOnMissed)
    Me.GroupBox2.Controls.Add(Me.chkSendNotificationOnSuccess)
    Me.GroupBox2.Controls.Add(Me.lblSendNotificationOnSuccess)
    Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox2.Location = New System.Drawing.Point(5, 308)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(364, 162)
    Me.GroupBox2.TabIndex = 3
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Warnings"
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.chkActive)
    Me.GroupBox4.Controls.Add(Me.cboJobStatus)
    Me.GroupBox4.Controls.Add(Me.lblActive)
    Me.GroupBox4.Controls.Add(Me.txtNextRunTime)
    Me.GroupBox4.Controls.Add(Me.lblLastRunTime)
    Me.GroupBox4.Controls.Add(Me.chkWarningMailSent)
    Me.GroupBox4.Controls.Add(Me.lblWarningMailSent)
    Me.GroupBox4.Controls.Add(Me.txtActivatingUser)
    Me.GroupBox4.Controls.Add(Me.lblActivatingUser)
    Me.GroupBox4.Controls.Add(Me.txtLastRunBy)
    Me.GroupBox4.Controls.Add(Me.lblLastRunBy)
    Me.GroupBox4.Controls.Add(Me.txtLastRunTime)
    Me.GroupBox4.Controls.Add(Me.lblNextRunTime)
    Me.GroupBox4.Controls.Add(Me.txtJobStatus)
    Me.GroupBox4.Controls.Add(Me.lblJobStatus)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(5, 5)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(364, 303)
    Me.GroupBox4.TabIndex = 2
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "Status"
    '
    'ctlc_Job
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoScroll = True
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Controls.Add(Me.tbcFault)
    Me.Controls.Add(Me.btnRunNow)
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
    Me.Name = "ctlc_Job"
    Me.Size = New System.Drawing.Size(774, 591)
    Me.tbcFault.ResumeLayout(False)
    Me.tbpInfo.ResumeLayout(False)
    Me.tlp1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.GroupBox4.ResumeLayout(False)
    Me.GroupBox4.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboJob As System.Windows.Forms.ComboBox
  Friend WithEvents txtJob As System.Windows.Forms.TextBox
  Friend WithEvents lblJob As System.Windows.Forms.Label
  Friend WithEvents cboJobRunner As System.Windows.Forms.ComboBox
  Friend WithEvents txtJobRunner As System.Windows.Forms.TextBox
  Friend WithEvents lblJobRunner As System.Windows.Forms.Label
  Friend WithEvents txtDescription As System.Windows.Forms.TextBox
  Friend WithEvents lblDescription As System.Windows.Forms.Label
  Friend WithEvents txtInstructions As System.Windows.Forms.TextBox
  Friend WithEvents lblInstructions As System.Windows.Forms.Label
  Friend WithEvents cboJobType As System.Windows.Forms.ComboBox
  Friend WithEvents txtJobType As System.Windows.Forms.TextBox
  Friend WithEvents lblJobType As System.Windows.Forms.Label
  Friend WithEvents dtpWhenToRun As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWhenToRun As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenToRun As System.Windows.Forms.Label
  Friend WithEvents txtCyclicCount As System.Windows.Forms.TextBox
  Friend WithEvents lblCyclicCount As System.Windows.Forms.Label
  Friend WithEvents chkSendNotificationOnSuccess As System.Windows.Forms.CheckBox
  Friend WithEvents lblSendNotificationOnSuccess As System.Windows.Forms.Label
  Friend WithEvents chkSendAlarmOnMissed As System.Windows.Forms.CheckBox
  Friend WithEvents lblSendAlarmOnMissed As System.Windows.Forms.Label
  Friend WithEvents txtTimeOutSec As System.Windows.Forms.TextBox
  Friend WithEvents lblTimeOutSec As System.Windows.Forms.Label
  Friend WithEvents chkActive As System.Windows.Forms.CheckBox
  Friend WithEvents lblActive As System.Windows.Forms.Label
  Friend WithEvents txtActivatingUser As System.Windows.Forms.TextBox
  Friend WithEvents lblActivatingUser As System.Windows.Forms.Label
  Friend WithEvents txtNextRunTime As System.Windows.Forms.TextBox
  Friend WithEvents lblNextRunTime As System.Windows.Forms.Label
  Friend WithEvents txtLastRunTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLastRunTime As System.Windows.Forms.Label
  Friend WithEvents cboJobStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtJobStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblJobStatus As System.Windows.Forms.Label
  Friend WithEvents chkWarningMailSent As System.Windows.Forms.CheckBox
  Friend WithEvents lblWarningMailSent As System.Windows.Forms.Label
  Friend WithEvents chkIsManaged As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsManaged As System.Windows.Forms.Label
  Friend WithEvents txtLastRunBy As System.Windows.Forms.TextBox
  Friend WithEvents lblLastRunBy As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnRunNow As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button
  Friend WithEvents tbcFault As TabControl
  Friend WithEvents tbpInfo As TabPage
  Friend WithEvents tlp1 As TableLayoutPanel
  Friend WithEvents Panel1 As Panel
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Panel2 As Panel
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents GroupBox2 As GroupBox
End Class
