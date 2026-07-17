<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_UserStatus
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
    Me.txtLastLoggedLoginID = New System.Windows.Forms.TextBox()
    Me.lblLastLoggedLoginID = New System.Windows.Forms.Label()
    Me.dtpLoginTime = New System.Windows.Forms.DateTimePicker()
    Me.txtLoginTime = New System.Windows.Forms.TextBox()
    Me.lblLoginTime = New System.Windows.Forms.Label()
    Me.dtpLogoutTime = New System.Windows.Forms.DateTimePicker()
    Me.txtLogoutTime = New System.Windows.Forms.TextBox()
    Me.lblLogoutTime = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(196, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(374, 25)
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
    Me.cboUser.Location = New System.Drawing.Point(189, 51)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(324, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 2
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(196, 57)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(374, 20)
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
    Me.txtApplicationName.Location = New System.Drawing.Point(196, 97)
    Me.txtApplicationName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtApplicationName.Name = "txtApplicationName"
    Me.txtApplicationName.Size = New System.Drawing.Size(374, 25)
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
    'DtxtLastLoggedLoginID
    '
    Me.txtLastLoggedLoginID.Location = New System.Drawing.Point(196, 137)
    Me.txtLastLoggedLoginID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastLoggedLoginID.Name = "txtLastLoggedLoginID"
    Me.txtLastLoggedLoginID.Size = New System.Drawing.Size(374, 25)
    Me.txtLastLoggedLoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastLoggedLoginID.TabIndex = 7
    Me.txtLastLoggedLoginID.Text = "txtLastLoggedLoginID"
    '
    'lblLastLoggedLoginID
    '
    Me.lblLastLoggedLoginID.AutoSize = True
    Me.lblLastLoggedLoginID.Location = New System.Drawing.Point(42, 140)
    Me.lblLastLoggedLoginID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastLoggedLoginID.Name = "lblLastLoggedLoginID"
    Me.lblLastLoggedLoginID.Size = New System.Drawing.Size(18, 13)
    Me.lblLastLoggedLoginID.TabIndex = 8
    Me.lblLastLoggedLoginID.Text = "Last Logged Login ID"
    '
    'dtpLoginTime
    '
    Me.dtpLoginTime.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpLoginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpLoginTime.Location = New System.Drawing.Point(189, 171)
    Me.dtpLoginTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpLoginTime.Name = "dtpLoginTime"
    Me.dtpLoginTime.ShowCheckBox = True
    Me.dtpLoginTime.ShowUpDown = True
    Me.dtpLoginTime.Size = New System.Drawing.Size(324, 20)
    Me.dtpLoginTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpLoginTime.TabIndex = 9
    '
    'CtxtLoginTime
    '
    Me.txtLoginTime.Location = New System.Drawing.Point(196, 177)
    Me.txtLoginTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLoginTime.Name = "txtLoginTime"
    Me.txtLoginTime.Size = New System.Drawing.Size(374, 20)
    Me.txtLoginTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoginTime.TabIndex = 10
    Me.txtLoginTime.Text = "txtLoginTime"
    '
    'lblLoginTime
    '
    Me.lblLoginTime.AutoSize = True
    Me.lblLoginTime.Location = New System.Drawing.Point(42, 180)
    Me.lblLoginTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLoginTime.Name = "lblLoginTime"
    Me.lblLoginTime.Size = New System.Drawing.Size(18, 13)
    Me.lblLoginTime.TabIndex = 11
    Me.lblLoginTime.Text = "Login Time"
    '
    'dtpLogoutTime
    '
    Me.dtpLogoutTime.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpLogoutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpLogoutTime.Location = New System.Drawing.Point(189, 211)
    Me.dtpLogoutTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpLogoutTime.Name = "dtpLogoutTime"
    Me.dtpLogoutTime.ShowCheckBox = True
    Me.dtpLogoutTime.ShowUpDown = True
    Me.dtpLogoutTime.Size = New System.Drawing.Size(324, 20)
    Me.dtpLogoutTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpLogoutTime.TabIndex = 12
    '
    'CtxtLogoutTime
    '
    Me.txtLogoutTime.Location = New System.Drawing.Point(196, 217)
    Me.txtLogoutTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLogoutTime.Name = "txtLogoutTime"
    Me.txtLogoutTime.Size = New System.Drawing.Size(374, 20)
    Me.txtLogoutTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLogoutTime.TabIndex = 13
    Me.txtLogoutTime.Text = "txtLogoutTime"
    '
    'lblLogoutTime
    '
    Me.lblLogoutTime.AutoSize = True
    Me.lblLogoutTime.Location = New System.Drawing.Point(42, 220)
    Me.lblLogoutTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLogoutTime.Name = "lblLogoutTime"
    Me.lblLogoutTime.Size = New System.Drawing.Size(18, 13)
    Me.lblLogoutTime.TabIndex = 14
    Me.lblLogoutTime.Text = "Logout Time"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 297)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 15
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 297)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 16
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 297)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 17
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 285)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 18
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 285)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 19
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlUserStatus 
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
    Me.Controls.Add(Me.txtLastLoggedLoginID)
    Me.Controls.Add(Me.lblLastLoggedLoginID)
    Me.Controls.Add(Me.dtpLoginTime)
    Me.Controls.Add(Me.txtLoginTime)
    Me.Controls.Add(Me.lblLoginTime)
    Me.Controls.Add(Me.dtpLogoutTime)
    Me.Controls.Add(Me.txtLogoutTime)
    Me.Controls.Add(Me.lblLogoutTime)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_UserStatus"
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
  Friend WithEvents txtLastLoggedLoginID As System.Windows.Forms.TextBox
  Friend WithEvents lblLastLoggedLoginID As System.Windows.Forms.Label
  Friend WithEvents dtpLoginTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtLoginTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLoginTime As System.Windows.Forms.Label
  Friend WithEvents dtpLogoutTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtLogoutTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLogoutTime As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
