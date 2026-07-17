<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_UserPermission
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
    Me.txtComputerIdentifier = New System.Windows.Forms.TextBox()
    Me.lblComputerIdentifier = New System.Windows.Forms.Label()
    Me.txtComputerName = New System.Windows.Forms.TextBox()
    Me.lblComputerName = New System.Windows.Forms.Label()
    Me.txtExternalIP = New System.Windows.Forms.TextBox()
    Me.lblExternalIP = New System.Windows.Forms.Label()
    Me.chkHasPermission = New System.Windows.Forms.CheckBox()
    Me.lblHasPermission = New System.Windows.Forms.Label()
    Me.txtComments = New System.Windows.Forms.TextBox()
    Me.lblComments = New System.Windows.Forms.Label()
    Me.dtpLastAccessTime = New System.Windows.Forms.DateTimePicker()
    Me.txtLastAccessTime = New System.Windows.Forms.TextBox()
    Me.lblLastAccessTime = New System.Windows.Forms.Label()
    Me.txtLoggedLoginID = New System.Windows.Forms.TextBox()
    Me.lblLoggedLoginID = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(193, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(377, 25)
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
    Me.cboUser.Location = New System.Drawing.Point(186, 51)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(327, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 2
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(193, 57)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(377, 20)
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
    Me.txtApplicationName.Location = New System.Drawing.Point(193, 97)
    Me.txtApplicationName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtApplicationName.Name = "txtApplicationName"
    Me.txtApplicationName.Size = New System.Drawing.Size(377, 25)
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
    'DtxtComputerIdentifier
    '
    Me.txtComputerIdentifier.Location = New System.Drawing.Point(193, 137)
    Me.txtComputerIdentifier.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtComputerIdentifier.Name = "txtComputerIdentifier"
    Me.txtComputerIdentifier.Size = New System.Drawing.Size(377, 105)
    Me.txtComputerIdentifier.Multiline = True
    Me.txtComputerIdentifier.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtComputerIdentifier.WordWrap = False 
    Me.txtComputerIdentifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtComputerIdentifier.TabIndex = 7
    Me.txtComputerIdentifier.Text = "txtComputerIdentifier"
    '
    'lblComputerIdentifier
    '
    Me.lblComputerIdentifier.AutoSize = True
    Me.lblComputerIdentifier.Location = New System.Drawing.Point(42, 135)
    Me.lblComputerIdentifier.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblComputerIdentifier.Name = "lblComputerIdentifier"
    Me.lblComputerIdentifier.Size = New System.Drawing.Size(18, 13)
    Me.lblComputerIdentifier.TabIndex = 8
    Me.lblComputerIdentifier.Text = "Computer Identifier"
    '
    'DtxtComputerName
    '
    Me.txtComputerName.Location = New System.Drawing.Point(193, 257)
    Me.txtComputerName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtComputerName.Name = "txtComputerName"
    Me.txtComputerName.Size = New System.Drawing.Size(377, 25)
    Me.txtComputerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtComputerName.TabIndex = 9
    Me.txtComputerName.Text = "txtComputerName"
    '
    'lblComputerName
    '
    Me.lblComputerName.AutoSize = True
    Me.lblComputerName.Location = New System.Drawing.Point(42, 260)
    Me.lblComputerName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblComputerName.Name = "lblComputerName"
    Me.lblComputerName.Size = New System.Drawing.Size(18, 13)
    Me.lblComputerName.TabIndex = 10
    Me.lblComputerName.Text = "Computer Name"
    '
    'DtxtExternalIP
    '
    Me.txtExternalIP.Location = New System.Drawing.Point(193, 297)
    Me.txtExternalIP.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtExternalIP.Name = "txtExternalIP"
    Me.txtExternalIP.Size = New System.Drawing.Size(377, 105)
    Me.txtExternalIP.Multiline = True
    Me.txtExternalIP.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtExternalIP.WordWrap = False 
    Me.txtExternalIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtExternalIP.TabIndex = 11
    Me.txtExternalIP.Text = "txtExternalIP"
    '
    'lblExternalIP
    '
    Me.lblExternalIP.AutoSize = True
    Me.lblExternalIP.Location = New System.Drawing.Point(42, 295)
    Me.lblExternalIP.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblExternalIP.Name = "lblExternalIP"
    Me.lblExternalIP.Size = New System.Drawing.Size(18, 13)
    Me.lblExternalIP.TabIndex = 12
    Me.lblExternalIP.Text = "External IP"
    '
    'chkHasPermission
    '
    Me.chkHasPermission.AutoSize = True
    Me.chkHasPermission.Location = New System.Drawing.Point(193, 423)
    Me.chkHasPermission.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkHasPermission.Name = "chkHasPermission"
    Me.chkHasPermission.Size = New System.Drawing.Size(15, 14)
    Me.chkHasPermission.TabIndex = 13
    Me.chkHasPermission.UseVisualStyleBackColor = True
    '
    'lblHasPermission
    '
    Me.lblHasPermission.AutoSize = True
    Me.lblHasPermission.Location = New System.Drawing.Point(42, 418)
    Me.lblHasPermission.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblHasPermission.Name = "lblHasPermission"
    Me.lblHasPermission.Size = New System.Drawing.Size(18, 13)
    Me.lblHasPermission.TabIndex = 14
    Me.lblHasPermission.Text = "Has Permission"
    '
    'DtxtComments
    '
    Me.txtComments.Location = New System.Drawing.Point(193, 457)
    Me.txtComments.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtComments.Name = "txtComments"
    Me.txtComments.Size = New System.Drawing.Size(377, 105)
    Me.txtComments.Multiline = True
    Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtComments.WordWrap = False 
    Me.txtComments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtComments.TabIndex = 15
    Me.txtComments.Text = "txtComments"
    '
    'lblComments
    '
    Me.lblComments.AutoSize = True
    Me.lblComments.Location = New System.Drawing.Point(42, 455)
    Me.lblComments.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblComments.Name = "lblComments"
    Me.lblComments.Size = New System.Drawing.Size(18, 13)
    Me.lblComments.TabIndex = 16
    Me.lblComments.Text = "Comments"
    '
    'dtpLastAccessTime
    '
    Me.dtpLastAccessTime.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpLastAccessTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpLastAccessTime.Location = New System.Drawing.Point(186, 571)
    Me.dtpLastAccessTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpLastAccessTime.Name = "dtpLastAccessTime"
    Me.dtpLastAccessTime.ShowCheckBox = True
    Me.dtpLastAccessTime.ShowUpDown = True
    Me.dtpLastAccessTime.Size = New System.Drawing.Size(327, 20)
    Me.dtpLastAccessTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpLastAccessTime.TabIndex = 17
    '
    'CtxtLastAccessTime
    '
    Me.txtLastAccessTime.Location = New System.Drawing.Point(193, 577)
    Me.txtLastAccessTime.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastAccessTime.Name = "txtLastAccessTime"
    Me.txtLastAccessTime.Size = New System.Drawing.Size(377, 20)
    Me.txtLastAccessTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastAccessTime.TabIndex = 18
    Me.txtLastAccessTime.Text = "txtLastAccessTime"
    '
    'lblLastAccessTime
    '
    Me.lblLastAccessTime.AutoSize = True
    Me.lblLastAccessTime.Location = New System.Drawing.Point(42, 580)
    Me.lblLastAccessTime.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastAccessTime.Name = "lblLastAccessTime"
    Me.lblLastAccessTime.Size = New System.Drawing.Size(18, 13)
    Me.lblLastAccessTime.TabIndex = 19
    Me.lblLastAccessTime.Text = "Last Access Time"
    '
    'DtxtLoggedLoginID
    '
    Me.txtLoggedLoginID.Location = New System.Drawing.Point(193, 617)
    Me.txtLoggedLoginID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLoggedLoginID.Name = "txtLoggedLoginID"
    Me.txtLoggedLoginID.Size = New System.Drawing.Size(377, 25)
    Me.txtLoggedLoginID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLoggedLoginID.TabIndex = 20
    Me.txtLoggedLoginID.Text = "txtLoggedLoginID"
    '
    'lblLoggedLoginID
    '
    Me.lblLoggedLoginID.AutoSize = True
    Me.lblLoggedLoginID.Location = New System.Drawing.Point(42, 620)
    Me.lblLoggedLoginID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLoggedLoginID.Name = "lblLoggedLoginID"
    Me.lblLoggedLoginID.Size = New System.Drawing.Size(18, 13)
    Me.lblLoggedLoginID.TabIndex = 21
    Me.lblLoggedLoginID.Text = "Logged Login ID"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 697)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 22
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 697)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 23
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 697)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 24
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 685)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 25
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 685)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 26
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlUserPermission 
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
    Me.Controls.Add(Me.txtComputerIdentifier)
    Me.Controls.Add(Me.lblComputerIdentifier)
    Me.Controls.Add(Me.txtComputerName)
    Me.Controls.Add(Me.lblComputerName)
    Me.Controls.Add(Me.txtExternalIP)
    Me.Controls.Add(Me.lblExternalIP)
    Me.Controls.Add(Me.chkHasPermission)
    Me.Controls.Add(Me.lblHasPermission)
    Me.Controls.Add(Me.txtComments)
    Me.Controls.Add(Me.lblComments)
    Me.Controls.Add(Me.dtpLastAccessTime)
    Me.Controls.Add(Me.txtLastAccessTime)
    Me.Controls.Add(Me.lblLastAccessTime)
    Me.Controls.Add(Me.txtLoggedLoginID)
    Me.Controls.Add(Me.lblLoggedLoginID)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_UserPermission"
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
  Friend WithEvents txtComputerIdentifier As System.Windows.Forms.TextBox
  Friend WithEvents lblComputerIdentifier As System.Windows.Forms.Label
  Friend WithEvents txtComputerName As System.Windows.Forms.TextBox
  Friend WithEvents lblComputerName As System.Windows.Forms.Label
  Friend WithEvents txtExternalIP As System.Windows.Forms.TextBox
  Friend WithEvents lblExternalIP As System.Windows.Forms.Label
  Friend WithEvents chkHasPermission As System.Windows.Forms.CheckBox
  Friend WithEvents lblHasPermission As System.Windows.Forms.Label
  Friend WithEvents txtComments As System.Windows.Forms.TextBox
  Friend WithEvents lblComments As System.Windows.Forms.Label
  Friend WithEvents dtpLastAccessTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtLastAccessTime As System.Windows.Forms.TextBox
  Friend WithEvents lblLastAccessTime As System.Windows.Forms.Label
  Friend WithEvents txtLoggedLoginID As System.Windows.Forms.TextBox
  Friend WithEvents lblLoggedLoginID As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
