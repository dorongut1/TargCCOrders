<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_UserLoginKey
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
    Me.cboUser = New IntelliCombo()
    Me.txtUser = New System.Windows.Forms.TextBox()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.txtApplicationName = New System.Windows.Forms.TextBox()
    Me.lblApplicationName = New System.Windows.Forms.Label()
    Me.txtApplicationIdentifier = New System.Windows.Forms.TextBox()
    Me.lblApplicationIdentifier = New System.Windows.Forms.Label()
    Me.txtKeyHashed = New System.Windows.Forms.TextBox()
    Me.lblKeyHashed = New System.Windows.Forms.Label()
    Me.txtExternalIPAtCreation = New System.Windows.Forms.TextBox()
    Me.lblExternalIPAtCreation = New System.Windows.Forms.Label()
    Me.txtCountryAtCreation = New System.Windows.Forms.TextBox()
    Me.lblCountryAtCreation = New System.Windows.Forms.Label()
    Me.dtpLastAccessTime = New System.Windows.Forms.DateTimePicker()
    Me.txtLastAccessTime = New System.Windows.Forms.TextBox()
    Me.lblLastAccessTime = New System.Windows.Forms.Label()
    Me.txtLoggedLoginID = New System.Windows.Forms.TextBox()
    Me.lblLoggedLoginID = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(202, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(368, 25)
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
    'cboUser
    '
    Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUser.Location = New System.Drawing.Point(195, 51)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(318, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 2
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(202, 57)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(368, 20)
    Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUser.TabIndex = 3
    Me.txtUser.Text = "txtUser"
    '
    'lblUser
    '
    Me.lblUser.AutoSize = True
    Me.lblUser.Location = New System.Drawing.Point(42, 60)
    Me.lblUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(18, 13)
    Me.lblUser.TabIndex = 4
    Me.lblUser.Text = "User"
    '
    'DtxtApplicationName
    '
    Me.txtApplicationName.Location = New System.Drawing.Point(202, 97)
    Me.txtApplicationName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtApplicationName.Name = "txtApplicationName"
    Me.txtApplicationName.Size = New System.Drawing.Size(368, 25)
    Me.txtApplicationName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApplicationName.TabIndex = 5
    Me.txtApplicationName.Text = "txtApplicationName"
    '
    'lblApplicationName
    '
    Me.lblApplicationName.AutoSize = True
    Me.lblApplicationName.Location = New System.Drawing.Point(42, 100)
    Me.lblApplicationName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblApplicationName.Name = "lblApplicationName"
    Me.lblApplicationName.Size = New System.Drawing.Size(18, 13)
    Me.lblApplicationName.TabIndex = 6
    Me.lblApplicationName.Text = "Application Name"
    '
    'DtxtApplicationIdentifier
    '
    Me.txtApplicationIdentifier.Location = New System.Drawing.Point(202, 137)
    Me.txtApplicationIdentifier.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtApplicationIdentifier.Name = "txtApplicationIdentifier"
    Me.txtApplicationIdentifier.Size = New System.Drawing.Size(368, 105)
    Me.txtApplicationIdentifier.Multiline = True
    Me.txtApplicationIdentifier.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtApplicationIdentifier.WordWrap = False 
    Me.txtApplicationIdentifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtApplicationIdentifier.TabIndex = 7
    Me.txtApplicationIdentifier.Text = "txtApplicationIdentifier"
    '
    'lblApplicationIdentifier
    '
    Me.lblApplicationIdentifier.AutoSize = True
    Me.lblApplicationIdentifier.Location = New System.Drawing.Point(42, 135)
    Me.lblApplicationIdentifier.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblApplicationIdentifier.Name = "lblApplicationIdentifier"
    Me.lblApplicationIdentifier.Size = New System.Drawing.Size(18, 13)
    Me.lblApplicationIdentifier.TabIndex = 8
    Me.lblApplicationIdentifier.Text = "Application Identifier"
    '
    'DtxtKeyHashed
    '
    Me.txtKeyHashed.Location = New System.Drawing.Point(202, 257)
    Me.txtKeyHashed.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtKeyHashed.Name = "txtKeyHashed"
    Me.txtKeyHashed.Size = New System.Drawing.Size(368, 25)
    Me.txtKeyHashed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtKeyHashed.TabIndex = 9
    Me.txtKeyHashed.Text = "txtKeyHashed"
    Me.txtKeyHashed.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtKeyHashed.UseSystemPasswordChar = True
    '
    'lblKeyHashed
    '
    Me.lblKeyHashed.AutoSize = True
    Me.lblKeyHashed.Location = New System.Drawing.Point(42, 260)
    Me.lblKeyHashed.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblKeyHashed.Name = "lblKeyHashed"
    Me.lblKeyHashed.Size = New System.Drawing.Size(18, 13)
    Me.lblKeyHashed.TabIndex = 10
    Me.lblKeyHashed.Text = "Key"
    '
    'DtxtExternalIPAtCreation
    '
    Me.txtExternalIPAtCreation.Location = New System.Drawing.Point(202, 297)
    Me.txtExternalIPAtCreation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtExternalIPAtCreation.Name = "txtExternalIPAtCreation"
    Me.txtExternalIPAtCreation.Size = New System.Drawing.Size(368, 105)
    Me.txtExternalIPAtCreation.Multiline = True
    Me.txtExternalIPAtCreation.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtExternalIPAtCreation.WordWrap = False 
    Me.txtExternalIPAtCreation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtExternalIPAtCreation.TabIndex = 11
    Me.txtExternalIPAtCreation.Text = "txtExternalIPAtCreation"
    '
    'lblExternalIPAtCreation
    '
    Me.lblExternalIPAtCreation.AutoSize = True
    Me.lblExternalIPAtCreation.Location = New System.Drawing.Point(42, 295)
    Me.lblExternalIPAtCreation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblExternalIPAtCreation.Name = "lblExternalIPAtCreation"
    Me.lblExternalIPAtCreation.Size = New System.Drawing.Size(18, 13)
    Me.lblExternalIPAtCreation.TabIndex = 12
    Me.lblExternalIPAtCreation.Text = "External IP At Creation"
    '
    'DtxtCountryAtCreation
    '
    Me.txtCountryAtCreation.Location = New System.Drawing.Point(202, 417)
    Me.txtCountryAtCreation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCountryAtCreation.Name = "txtCountryAtCreation"
    Me.txtCountryAtCreation.Size = New System.Drawing.Size(368, 25)
    Me.txtCountryAtCreation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCountryAtCreation.TabIndex = 13
    Me.txtCountryAtCreation.Text = "txtCountryAtCreation"
    '
    'lblCountryAtCreation
    '
    Me.lblCountryAtCreation.AutoSize = True
    Me.lblCountryAtCreation.Location = New System.Drawing.Point(42, 420)
    Me.lblCountryAtCreation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCountryAtCreation.Name = "lblCountryAtCreation"
    Me.lblCountryAtCreation.Size = New System.Drawing.Size(18, 13)
    Me.lblCountryAtCreation.TabIndex = 14
    Me.lblCountryAtCreation.Text = "Country At Creation"
    '
    'dtpLastAccessTime
    '
    Me.dtpLastAccessTime.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpLastAccessTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpLastAccessTime.Location = New System.Drawing.Point(195, 451)
    Me.dtpLastAccessTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpLastAccessTime.Name = "dtpLastAccessTime"
    Me.dtpLastAccessTime.ShowCheckBox = True
    Me.dtpLastAccessTime.ShowUpDown = True
    Me.dtpLastAccessTime.Size = New System.Drawing.Size(318, 20)
    Me.dtpLastAccessTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpLastAccessTime.TabIndex = 15
    '
    'CtxtLastAccessTime
    '
    Me.txtLastAccessTime.Location = New System.Drawing.Point(202, 457)
    Me.txtLastAccessTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastAccessTime.Name = "txtLastAccessTime"
    Me.txtLastAccessTime.Size = New System.Drawing.Size(368, 20)
    Me.txtLastAccessTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastAccessTime.TabIndex = 16
    Me.txtLastAccessTime.Text = "txtLastAccessTime"
    '
    'lblLastAccessTime
    '
    Me.lblLastAccessTime.AutoSize = True
    Me.lblLastAccessTime.Location = New System.Drawing.Point(42, 460)
    Me.lblLastAccessTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastAccessTime.Name = "lblLastAccessTime"
    Me.lblLastAccessTime.Size = New System.Drawing.Size(18, 13)
    Me.lblLastAccessTime.TabIndex = 17
    Me.lblLastAccessTime.Text = "Last Access Time"
    '
    'DtxtLoggedLoginID
    '
    Me.txtLoggedLoginID.Location = New System.Drawing.Point(202, 497)
    Me.txtLoggedLoginID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLoggedLoginID.Name = "txtLoggedLoginID"
    Me.txtLoggedLoginID.Size = New System.Drawing.Size(368, 25)
    Me.txtLoggedLoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoggedLoginID.TabIndex = 18
    Me.txtLoggedLoginID.Text = "txtLoggedLoginID"
    '
    'lblLoggedLoginID
    '
    Me.lblLoggedLoginID.AutoSize = True
    Me.lblLoggedLoginID.Location = New System.Drawing.Point(42, 500)
    Me.lblLoggedLoginID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLoggedLoginID.Name = "lblLoggedLoginID"
    Me.lblLoggedLoginID.Size = New System.Drawing.Size(18, 13)
    Me.lblLoggedLoginID.TabIndex = 19
    Me.lblLoggedLoginID.Text = "Logged Login ID"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 577)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 20
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 577)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 21
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 565)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 22
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 565)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 23
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlUserLoginKey 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.txtApplicationName)
    Me.Controls.Add(Me.lblApplicationName)
    Me.Controls.Add(Me.txtApplicationIdentifier)
    Me.Controls.Add(Me.lblApplicationIdentifier)
    Me.Controls.Add(Me.txtKeyHashed)
    Me.Controls.Add(Me.lblKeyHashed)
    Me.Controls.Add(Me.txtExternalIPAtCreation)
    Me.Controls.Add(Me.lblExternalIPAtCreation)
    Me.Controls.Add(Me.txtCountryAtCreation)
    Me.Controls.Add(Me.lblCountryAtCreation)
    Me.Controls.Add(Me.dtpLastAccessTime)
    Me.Controls.Add(Me.txtLastAccessTime)
    Me.Controls.Add(Me.lblLastAccessTime)
    Me.Controls.Add(Me.txtLoggedLoginID)
    Me.Controls.Add(Me.lblLoggedLoginID)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_UserLoginKey"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboUser As IntelliCombo
  Friend WithEvents txtUser As System.Windows.Forms.TextBox
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents txtApplicationName As System.Windows.Forms.TextBox
  Friend WithEvents lblApplicationName As System.Windows.Forms.Label
  Friend WithEvents txtApplicationIdentifier As System.Windows.Forms.TextBox
  Friend WithEvents lblApplicationIdentifier As System.Windows.Forms.Label
  Friend WithEvents txtKeyHashed As System.Windows.Forms.TextBox
  Friend WithEvents lblKeyHashed As System.Windows.Forms.Label
  Friend WithEvents txtExternalIPAtCreation As System.Windows.Forms.TextBox
  Friend WithEvents lblExternalIPAtCreation As System.Windows.Forms.Label
  Friend WithEvents txtCountryAtCreation As System.Windows.Forms.TextBox
  Friend WithEvents lblCountryAtCreation As System.Windows.Forms.Label
  Friend WithEvents dtpLastAccessTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtLastAccessTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLastAccessTime As System.Windows.Forms.Label
  Friend WithEvents txtLoggedLoginID As System.Windows.Forms.TextBox
  Friend WithEvents lblLoggedLoginID As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
