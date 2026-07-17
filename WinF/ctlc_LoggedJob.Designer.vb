<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedJob
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
    Me.cboJob = New IntelliCombo()
    Me.txtJob = New System.Windows.Forms.TextBox()
    Me.lblJob = New System.Windows.Forms.Label()
    Me.txtWhenStarted = New System.Windows.Forms.TextBox()
    Me.lblWhenStarted = New System.Windows.Forms.Label()
    Me.txtActivatingUser = New System.Windows.Forms.TextBox()
    Me.lblActivatingUser = New System.Windows.Forms.Label()
    Me.txtLastRunBy = New System.Windows.Forms.TextBox()
    Me.lblLastRunBy = New System.Windows.Forms.Label()
    Me.txtExecutionTimeSec = New System.Windows.Forms.TextBox()
    Me.lblExecutionTimeSec = New System.Windows.Forms.Label()
    Me.cboRunStatus = New System.Windows.Forms.ComboBox()
    Me.txtRunStatus = New System.Windows.Forms.TextBox()
    Me.lblRunStatus = New System.Windows.Forms.Label()
    Me.txtRemarks = New System.Windows.Forms.TextBox()
    Me.lblRemarks = New System.Windows.Forms.Label()
    Me.cboLoggedAlert = New IntelliCombo()
    Me.txtLoggedAlert = New System.Windows.Forms.TextBox()
    Me.lblLoggedAlert = New System.Windows.Forms.Label()
    Me.txtSuccessCount = New System.Windows.Forms.TextBox()
    Me.lblSuccessCount = New System.Windows.Forms.Label()
    Me.txtFailureCount = New System.Windows.Forms.TextBox()
    Me.lblFailureCount = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(184, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(386, 25)
    Me.txtID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtID.TabIndex = 0
    Me.txtID.Text = "txtID"
    '
    'lblID
    '
    Me.lblID.AutoSize = True
    Me.lblID.Location = New System.Drawing.Point(42, 20)
    Me.lblID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblID.Name = "lblID"
    Me.lblID.Size = New System.Drawing.Size(18, 13)
    Me.lblID.TabIndex = 1
    Me.lblID.Text = "ID"
    '
    'cboJob
    '
    Me.cboJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJob.Location = New System.Drawing.Point(177, 51)
    Me.cboJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboJob.Name = "cboJob"
    Me.cboJob.Size = New System.Drawing.Size(336, 21)
    Me.cboJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJob.TabIndex = 2
    '
    'AtxtJob
    '
    Me.txtJob.Location = New System.Drawing.Point(184, 57)
    Me.txtJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtJob.Name = "txtJob"
    Me.txtJob.Size = New System.Drawing.Size(386, 20)
    Me.txtJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJob.TabIndex = 3
    Me.txtJob.Text = "txtJob"
    '
    'lblJob
    '
    Me.lblJob.AutoSize = True
    Me.lblJob.Location = New System.Drawing.Point(42, 60)
    Me.lblJob.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblJob.Name = "lblJob"
    Me.lblJob.Size = New System.Drawing.Size(18, 13)
    Me.lblJob.TabIndex = 4
    Me.lblJob.Text = "Job"
    '
    'CtxtWhenStarted
    '
    Me.txtWhenStarted.Location = New System.Drawing.Point(184, 97)
    Me.txtWhenStarted.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtWhenStarted.Name = "txtWhenStarted"
    Me.txtWhenStarted.Size = New System.Drawing.Size(386, 20)
    Me.txtWhenStarted.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWhenStarted.TabIndex = 5
    Me.txtWhenStarted.Text = "txtWhenStarted"
    '
    'lblWhenStarted
    '
    Me.lblWhenStarted.AutoSize = True
    Me.lblWhenStarted.Location = New System.Drawing.Point(42, 100)
    Me.lblWhenStarted.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblWhenStarted.Name = "lblWhenStarted"
    Me.lblWhenStarted.Size = New System.Drawing.Size(18, 13)
    Me.lblWhenStarted.TabIndex = 6
    Me.lblWhenStarted.Text = "When Started"
    '
    'DtxtActivatingUser
    '
    Me.txtActivatingUser.Location = New System.Drawing.Point(184, 137)
    Me.txtActivatingUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtActivatingUser.Name = "txtActivatingUser"
    Me.txtActivatingUser.Size = New System.Drawing.Size(386, 25)
    Me.txtActivatingUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtActivatingUser.TabIndex = 7
    Me.txtActivatingUser.Text = "txtActivatingUser"
    '
    'lblActivatingUser
    '
    Me.lblActivatingUser.AutoSize = True
    Me.lblActivatingUser.Location = New System.Drawing.Point(42, 140)
    Me.lblActivatingUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblActivatingUser.Name = "lblActivatingUser"
    Me.lblActivatingUser.Size = New System.Drawing.Size(18, 13)
    Me.lblActivatingUser.TabIndex = 8
    Me.lblActivatingUser.Text = "Activating User"
    '
    'DtxtLastRunBy
    '
    Me.txtLastRunBy.Location = New System.Drawing.Point(184, 177)
    Me.txtLastRunBy.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastRunBy.Name = "txtLastRunBy"
    Me.txtLastRunBy.Size = New System.Drawing.Size(386, 25)
    Me.txtLastRunBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastRunBy.TabIndex = 9
    Me.txtLastRunBy.Text = "txtLastRunBy"
    '
    'lblLastRunBy
    '
    Me.lblLastRunBy.AutoSize = True
    Me.lblLastRunBy.Location = New System.Drawing.Point(42, 180)
    Me.lblLastRunBy.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastRunBy.Name = "lblLastRunBy"
    Me.lblLastRunBy.Size = New System.Drawing.Size(18, 13)
    Me.lblLastRunBy.TabIndex = 10
    Me.lblLastRunBy.Text = "Last Run By"
    '
    'DtxtExecutionTimeSec
    '
    Me.txtExecutionTimeSec.Location = New System.Drawing.Point(184, 217)
    Me.txtExecutionTimeSec.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtExecutionTimeSec.Name = "txtExecutionTimeSec"
    Me.txtExecutionTimeSec.Size = New System.Drawing.Size(386, 25)
    Me.txtExecutionTimeSec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtExecutionTimeSec.TabIndex = 11
    Me.txtExecutionTimeSec.Text = "txtExecutionTimeSec"
    '
    'lblExecutionTimeSec
    '
    Me.lblExecutionTimeSec.AutoSize = True
    Me.lblExecutionTimeSec.Location = New System.Drawing.Point(42, 220)
    Me.lblExecutionTimeSec.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblExecutionTimeSec.Name = "lblExecutionTimeSec"
    Me.lblExecutionTimeSec.Size = New System.Drawing.Size(18, 13)
    Me.lblExecutionTimeSec.TabIndex = 12
    Me.lblExecutionTimeSec.Text = "Execution Time Sec"
    '
    'cboRunStatus
    '
    Me.cboRunStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboRunStatus.FormattingEnabled = True
    Me.cboRunStatus.Location = New System.Drawing.Point(177, 251)
    Me.cboRunStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboRunStatus.Name = "cboRunStatus"
    Me.cboRunStatus.Size = New System.Drawing.Size(336, 21)
    Me.cboRunStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboRunStatus.TabIndex = 13
    '
    'BtxtRunStatus
    '
    Me.txtRunStatus.Location = New System.Drawing.Point(184, 257)
    Me.txtRunStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRunStatus.Name = "txtRunStatus"
    Me.txtRunStatus.Size = New System.Drawing.Size(386, 20)
    Me.txtRunStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRunStatus.TabIndex = 14
    Me.txtRunStatus.Text = "txtRunStatus"
    '
    'DtxtRunStatus
    '
    Me.txtRunStatus.Location = New System.Drawing.Point(184, 257)
    Me.txtRunStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRunStatus.Name = "txtRunStatus"
    Me.txtRunStatus.Size = New System.Drawing.Size(386, 25)
    Me.txtRunStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRunStatus.TabIndex = 15
    Me.txtRunStatus.Text = "txtRunStatus"
    '
    'lblRunStatus
    '
    Me.lblRunStatus.AutoSize = True
    Me.lblRunStatus.Location = New System.Drawing.Point(42, 260)
    Me.lblRunStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRunStatus.Name = "lblRunStatus"
    Me.lblRunStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblRunStatus.TabIndex = 16
    Me.lblRunStatus.Text = "Run Status"
    '
    'DtxtRemarks
    '
    Me.txtRemarks.Location = New System.Drawing.Point(184, 297)
    Me.txtRemarks.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRemarks.Name = "txtRemarks"
    Me.txtRemarks.Size = New System.Drawing.Size(386, 105)
    Me.txtRemarks.Multiline = True
    Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtRemarks.WordWrap = False 
    Me.txtRemarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRemarks.TabIndex = 17
    Me.txtRemarks.Text = "txtRemarks"
    '
    'lblRemarks
    '
    Me.lblRemarks.AutoSize = True
    Me.lblRemarks.Location = New System.Drawing.Point(42, 295)
    Me.lblRemarks.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRemarks.Name = "lblRemarks"
    Me.lblRemarks.Size = New System.Drawing.Size(18, 13)
    Me.lblRemarks.TabIndex = 18
    Me.lblRemarks.Text = "Remarks"
    '
    'cboLoggedAlert
    '
    Me.cboLoggedAlert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLoggedAlert.Location = New System.Drawing.Point(177, 411)
    Me.cboLoggedAlert.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboLoggedAlert.Name = "cboLoggedAlert"
    Me.cboLoggedAlert.Size = New System.Drawing.Size(336, 21)
    Me.cboLoggedAlert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLoggedAlert.TabIndex = 19
    '
    'AtxtLoggedAlert
    '
    Me.txtLoggedAlert.Location = New System.Drawing.Point(184, 417)
    Me.txtLoggedAlert.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLoggedAlert.Name = "txtLoggedAlert"
    Me.txtLoggedAlert.Size = New System.Drawing.Size(386, 20)
    Me.txtLoggedAlert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoggedAlert.TabIndex = 20
    Me.txtLoggedAlert.Text = "txtLoggedAlert"
    '
    'lblLoggedAlert
    '
    Me.lblLoggedAlert.AutoSize = True
    Me.lblLoggedAlert.Location = New System.Drawing.Point(42, 420)
    Me.lblLoggedAlert.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLoggedAlert.Name = "lblLoggedAlert"
    Me.lblLoggedAlert.Size = New System.Drawing.Size(18, 13)
    Me.lblLoggedAlert.TabIndex = 21
    Me.lblLoggedAlert.Text = "Logged Alert"
    '
    'DtxtSuccessCount
    '
    Me.txtSuccessCount.Location = New System.Drawing.Point(184, 457)
    Me.txtSuccessCount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSuccessCount.Name = "txtSuccessCount"
    Me.txtSuccessCount.Size = New System.Drawing.Size(386, 25)
    Me.txtSuccessCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSuccessCount.TabIndex = 22
    Me.txtSuccessCount.Text = "txtSuccessCount"
    '
    'lblSuccessCount
    '
    Me.lblSuccessCount.AutoSize = True
    Me.lblSuccessCount.Location = New System.Drawing.Point(42, 460)
    Me.lblSuccessCount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSuccessCount.Name = "lblSuccessCount"
    Me.lblSuccessCount.Size = New System.Drawing.Size(18, 13)
    Me.lblSuccessCount.TabIndex = 23
    Me.lblSuccessCount.Text = "Success Count"
    '
    'DtxtFailureCount
    '
    Me.txtFailureCount.Location = New System.Drawing.Point(184, 497)
    Me.txtFailureCount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtFailureCount.Name = "txtFailureCount"
    Me.txtFailureCount.Size = New System.Drawing.Size(386, 25)
    Me.txtFailureCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFailureCount.TabIndex = 24
    Me.txtFailureCount.Text = "txtFailureCount"
    '
    'lblFailureCount
    '
    Me.lblFailureCount.AutoSize = True
    Me.lblFailureCount.Location = New System.Drawing.Point(42, 500)
    Me.lblFailureCount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblFailureCount.Name = "lblFailureCount"
    Me.lblFailureCount.Size = New System.Drawing.Size(18, 13)
    Me.lblFailureCount.TabIndex = 25
    Me.lblFailureCount.Text = "Failure Count"
    '
    'ctlLoggedJob 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboJob)
    Me.Controls.Add(Me.txtJob)
    Me.Controls.Add(Me.txtJob)
    Me.Controls.Add(Me.lblJob)
    Me.Controls.Add(Me.txtWhenStarted)
    Me.Controls.Add(Me.lblWhenStarted)
    Me.Controls.Add(Me.txtActivatingUser)
    Me.Controls.Add(Me.lblActivatingUser)
    Me.Controls.Add(Me.txtLastRunBy)
    Me.Controls.Add(Me.lblLastRunBy)
    Me.Controls.Add(Me.txtExecutionTimeSec)
    Me.Controls.Add(Me.lblExecutionTimeSec)
    Me.Controls.Add(Me.cboRunStatus)
    Me.Controls.Add(Me.txtRunStatus)
    Me.Controls.Add(Me.txtRunStatus)
    Me.Controls.Add(Me.lblRunStatus)
    Me.Controls.Add(Me.txtRemarks)
    Me.Controls.Add(Me.lblRemarks)
    Me.Controls.Add(Me.cboLoggedAlert)
    Me.Controls.Add(Me.txtLoggedAlert)
    Me.Controls.Add(Me.txtLoggedAlert)
    Me.Controls.Add(Me.lblLoggedAlert)
    Me.Controls.Add(Me.txtSuccessCount)
    Me.Controls.Add(Me.lblSuccessCount)
    Me.Controls.Add(Me.txtFailureCount)
    Me.Controls.Add(Me.lblFailureCount)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedJob"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboJob As IntelliCombo
  Friend WithEvents txtJob As System.Windows.Forms.TextBox
  Friend WithEvents lblJob As System.Windows.Forms.Label
  Friend WithEvents txtWhenStarted As System.Windows.Forms.TextBox
  Friend WithEvents lblWhenStarted As System.Windows.Forms.Label
  Friend WithEvents txtActivatingUser As System.Windows.Forms.TextBox
  Friend WithEvents lblActivatingUser As System.Windows.Forms.Label
  Friend WithEvents txtLastRunBy As System.Windows.Forms.TextBox
  Friend WithEvents lblLastRunBy As System.Windows.Forms.Label
  Friend WithEvents txtExecutionTimeSec As System.Windows.Forms.TextBox
  Friend WithEvents lblExecutionTimeSec As System.Windows.Forms.Label
  Friend WithEvents cboRunStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtRunStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblRunStatus As System.Windows.Forms.Label
  Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
  Friend WithEvents lblRemarks As System.Windows.Forms.Label
  Friend WithEvents cboLoggedAlert As IntelliCombo
  Friend WithEvents txtLoggedAlert As System.Windows.Forms.TextBox
  Friend WithEvents lblLoggedAlert As System.Windows.Forms.Label
  Friend WithEvents txtSuccessCount As System.Windows.Forms.TextBox
  Friend WithEvents lblSuccessCount As System.Windows.Forms.Label
  Friend WithEvents txtFailureCount As System.Windows.Forms.TextBox
  Friend WithEvents lblFailureCount As System.Windows.Forms.Label

End Class
