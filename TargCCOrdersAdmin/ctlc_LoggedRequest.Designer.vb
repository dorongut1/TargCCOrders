<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedRequest
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
    Me.cboLoggedLogin = New IntelliCombo()
    Me.txtLoggedLogin = New System.Windows.Forms.TextBox()
    Me.lblLoggedLogin = New System.Windows.Forms.Label()
    Me.txtTimeAccessed = New System.Windows.Forms.TextBox()
    Me.lblTimeAccessed = New System.Windows.Forms.Label()
    Me.cboUser = New IntelliCombo()
    Me.txtUser = New System.Windows.Forms.TextBox()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.txtCallingFunctionWithinApplication = New System.Windows.Forms.TextBox()
    Me.lblCallingFunctionWithinApplication = New System.Windows.Forms.Label()
    Me.txtEntryPoint = New System.Windows.Forms.TextBox()
    Me.lblEntryPoint = New System.Windows.Forms.Label()
    Me.txtProcess = New System.Windows.Forms.TextBox()
    Me.lblProcess = New System.Windows.Forms.Label()
    Me.txtThread = New System.Windows.Forms.TextBox()
    Me.lblThread = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(279, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(291, 25)
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
    'cboLoggedLogin
    '
    Me.cboLoggedLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLoggedLogin.Location = New System.Drawing.Point(272, 51)
    Me.cboLoggedLogin.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboLoggedLogin.Name = "cboLoggedLogin"
    Me.cboLoggedLogin.Size = New System.Drawing.Size(241, 21)
    Me.cboLoggedLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLoggedLogin.TabIndex = 2
    '
    'AtxtLoggedLogin
    '
    Me.txtLoggedLogin.Location = New System.Drawing.Point(279, 57)
    Me.txtLoggedLogin.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLoggedLogin.Name = "txtLoggedLogin"
    Me.txtLoggedLogin.Size = New System.Drawing.Size(291, 20)
    Me.txtLoggedLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoggedLogin.TabIndex = 3
    Me.txtLoggedLogin.Text = "txtLoggedLogin"
    '
    'lblLoggedLogin
    '
    Me.lblLoggedLogin.AutoSize = True
    Me.lblLoggedLogin.Location = New System.Drawing.Point(42, 60)
    Me.lblLoggedLogin.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLoggedLogin.Name = "lblLoggedLogin"
    Me.lblLoggedLogin.Size = New System.Drawing.Size(18, 13)
    Me.lblLoggedLogin.TabIndex = 4
    Me.lblLoggedLogin.Text = "Logged Login"
    '
    'CtxtTimeAccessed
    '
    Me.txtTimeAccessed.Location = New System.Drawing.Point(279, 97)
    Me.txtTimeAccessed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTimeAccessed.Name = "txtTimeAccessed"
    Me.txtTimeAccessed.Size = New System.Drawing.Size(291, 20)
    Me.txtTimeAccessed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTimeAccessed.TabIndex = 5
    Me.txtTimeAccessed.Text = "txtTimeAccessed"
    '
    'lblTimeAccessed
    '
    Me.lblTimeAccessed.AutoSize = True
    Me.lblTimeAccessed.Location = New System.Drawing.Point(42, 100)
    Me.lblTimeAccessed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTimeAccessed.Name = "lblTimeAccessed"
    Me.lblTimeAccessed.Size = New System.Drawing.Size(18, 13)
    Me.lblTimeAccessed.TabIndex = 6
    Me.lblTimeAccessed.Text = "Time Accessed"
    '
    'cboUser
    '
    Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUser.Location = New System.Drawing.Point(272, 131)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(241, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 7
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(279, 137)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(291, 20)
    Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUser.TabIndex = 8
    Me.txtUser.Text = "txtUser"
    '
    'lblUser
    '
    Me.lblUser.AutoSize = True
    Me.lblUser.Location = New System.Drawing.Point(42, 140)
    Me.lblUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(18, 13)
    Me.lblUser.TabIndex = 9
    Me.lblUser.Text = "User"
    '
    'DtxtCallingFunctionWithinApplication
    '
    Me.txtCallingFunctionWithinApplication.Location = New System.Drawing.Point(279, 177)
    Me.txtCallingFunctionWithinApplication.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCallingFunctionWithinApplication.Name = "txtCallingFunctionWithinApplication"
    Me.txtCallingFunctionWithinApplication.Size = New System.Drawing.Size(291, 105)
    Me.txtCallingFunctionWithinApplication.Multiline = True
    Me.txtCallingFunctionWithinApplication.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtCallingFunctionWithinApplication.WordWrap = False 
    Me.txtCallingFunctionWithinApplication.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCallingFunctionWithinApplication.TabIndex = 10
    Me.txtCallingFunctionWithinApplication.Text = "txtCallingFunctionWithinApplication"
    '
    'lblCallingFunctionWithinApplication
    '
    Me.lblCallingFunctionWithinApplication.AutoSize = True
    Me.lblCallingFunctionWithinApplication.Location = New System.Drawing.Point(42, 175)
    Me.lblCallingFunctionWithinApplication.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCallingFunctionWithinApplication.Name = "lblCallingFunctionWithinApplication"
    Me.lblCallingFunctionWithinApplication.Size = New System.Drawing.Size(18, 13)
    Me.lblCallingFunctionWithinApplication.TabIndex = 11
    Me.lblCallingFunctionWithinApplication.Text = "Calling Function Within Application"
    '
    'DtxtEntryPoint
    '
    Me.txtEntryPoint.Location = New System.Drawing.Point(279, 297)
    Me.txtEntryPoint.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEntryPoint.Name = "txtEntryPoint"
    Me.txtEntryPoint.Size = New System.Drawing.Size(291, 105)
    Me.txtEntryPoint.Multiline = True
    Me.txtEntryPoint.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtEntryPoint.WordWrap = False 
    Me.txtEntryPoint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEntryPoint.TabIndex = 12
    Me.txtEntryPoint.Text = "txtEntryPoint"
    '
    'lblEntryPoint
    '
    Me.lblEntryPoint.AutoSize = True
    Me.lblEntryPoint.Location = New System.Drawing.Point(42, 295)
    Me.lblEntryPoint.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEntryPoint.Name = "lblEntryPoint"
    Me.lblEntryPoint.Size = New System.Drawing.Size(18, 13)
    Me.lblEntryPoint.TabIndex = 13
    Me.lblEntryPoint.Text = "Entry Point"
    '
    'DtxtProcess
    '
    Me.txtProcess.Location = New System.Drawing.Point(279, 417)
    Me.txtProcess.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProcess.Name = "txtProcess"
    Me.txtProcess.Size = New System.Drawing.Size(291, 25)
    Me.txtProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProcess.TabIndex = 14
    Me.txtProcess.Text = "txtProcess"
    '
    'lblProcess
    '
    Me.lblProcess.AutoSize = True
    Me.lblProcess.Location = New System.Drawing.Point(42, 420)
    Me.lblProcess.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProcess.Name = "lblProcess"
    Me.lblProcess.Size = New System.Drawing.Size(18, 13)
    Me.lblProcess.TabIndex = 15
    Me.lblProcess.Text = "Process"
    '
    'DtxtThread
    '
    Me.txtThread.Location = New System.Drawing.Point(279, 457)
    Me.txtThread.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtThread.Name = "txtThread"
    Me.txtThread.Size = New System.Drawing.Size(291, 25)
    Me.txtThread.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtThread.TabIndex = 16
    Me.txtThread.Text = "txtThread"
    '
    'lblThread
    '
    Me.lblThread.AutoSize = True
    Me.lblThread.Location = New System.Drawing.Point(42, 460)
    Me.lblThread.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblThread.Name = "lblThread"
    Me.lblThread.Size = New System.Drawing.Size(18, 13)
    Me.lblThread.TabIndex = 17
    Me.lblThread.Text = "Thread"
    '
    'ctlLoggedRequest 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboLoggedLogin)
    Me.Controls.Add(Me.txtLoggedLogin)
    Me.Controls.Add(Me.txtLoggedLogin)
    Me.Controls.Add(Me.lblLoggedLogin)
    Me.Controls.Add(Me.txtTimeAccessed)
    Me.Controls.Add(Me.lblTimeAccessed)
    Me.Controls.Add(Me.cboUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.txtCallingFunctionWithinApplication)
    Me.Controls.Add(Me.lblCallingFunctionWithinApplication)
    Me.Controls.Add(Me.txtEntryPoint)
    Me.Controls.Add(Me.lblEntryPoint)
    Me.Controls.Add(Me.txtProcess)
    Me.Controls.Add(Me.lblProcess)
    Me.Controls.Add(Me.txtThread)
    Me.Controls.Add(Me.lblThread)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedRequest"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboLoggedLogin As IntelliCombo
  Friend WithEvents txtLoggedLogin As System.Windows.Forms.TextBox
  Friend WithEvents lblLoggedLogin As System.Windows.Forms.Label
  Friend WithEvents txtTimeAccessed As System.Windows.Forms.TextBox
  Friend WithEvents lblTimeAccessed As System.Windows.Forms.Label
  Friend WithEvents cboUser As IntelliCombo
  Friend WithEvents txtUser As System.Windows.Forms.TextBox
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents txtCallingFunctionWithinApplication As System.Windows.Forms.TextBox
  Friend WithEvents lblCallingFunctionWithinApplication As System.Windows.Forms.Label
  Friend WithEvents txtEntryPoint As System.Windows.Forms.TextBox
  Friend WithEvents lblEntryPoint As System.Windows.Forms.Label
  Friend WithEvents txtProcess As System.Windows.Forms.TextBox
  Friend WithEvents lblProcess As System.Windows.Forms.Label
  Friend WithEvents txtThread As System.Windows.Forms.TextBox
  Friend WithEvents lblThread As System.Windows.Forms.Label

End Class
