<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_JobAlertRecipient
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
    Me.cboUser = New IntelliCombo()
    Me.txtUser = New System.Windows.Forms.TextBox()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.cboJobAlertType = New System.Windows.Forms.ComboBox()
    Me.txtJobAlertType = New System.Windows.Forms.TextBox()
    Me.lblJobAlertType = New System.Windows.Forms.Label()
    Me.txtOverrideName = New System.Windows.Forms.TextBox()
    Me.lblOverrideName = New System.Windows.Forms.Label()
    Me.txtOverrideEmailOrPhone = New System.Windows.Forms.TextBox()
    Me.lblOverrideEmailOrPhone = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(217, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(353, 25)
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
    Me.cboJob.Location = New System.Drawing.Point(210, 51)
    Me.cboJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboJob.Name = "cboJob"
    Me.cboJob.Size = New System.Drawing.Size(303, 21)
    Me.cboJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJob.TabIndex = 2
    '
    'AtxtJob
    '
    Me.txtJob.Location = New System.Drawing.Point(217, 57)
    Me.txtJob.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtJob.Name = "txtJob"
    Me.txtJob.Size = New System.Drawing.Size(353, 20)
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
    'cboUser
    '
    Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboUser.Location = New System.Drawing.Point(210, 91)
    Me.cboUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboUser.Name = "cboUser"
    Me.cboUser.Size = New System.Drawing.Size(303, 21)
    Me.cboUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboUser.TabIndex = 5
    '
    'AtxtUser
    '
    Me.txtUser.Location = New System.Drawing.Point(217, 97)
    Me.txtUser.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtUser.Name = "txtUser"
    Me.txtUser.Size = New System.Drawing.Size(353, 20)
    Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUser.TabIndex = 6
    Me.txtUser.Text = "txtUser"
    '
    'lblUser
    '
    Me.lblUser.AutoSize = True
    Me.lblUser.Location = New System.Drawing.Point(42, 100)
    Me.lblUser.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(18, 13)
    Me.lblUser.TabIndex = 7
    Me.lblUser.Text = "User"
    '
    'cboJobAlertType
    '
    Me.cboJobAlertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboJobAlertType.FormattingEnabled = True
    Me.cboJobAlertType.Location = New System.Drawing.Point(210, 131)
    Me.cboJobAlertType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboJobAlertType.Name = "cboJobAlertType"
    Me.cboJobAlertType.Size = New System.Drawing.Size(303, 21)
    Me.cboJobAlertType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboJobAlertType.TabIndex = 8
    '
    'BtxtJobAlertType
    '
    Me.txtJobAlertType.Location = New System.Drawing.Point(217, 137)
    Me.txtJobAlertType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtJobAlertType.Name = "txtJobAlertType"
    Me.txtJobAlertType.Size = New System.Drawing.Size(353, 20)
    Me.txtJobAlertType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJobAlertType.TabIndex = 9
    Me.txtJobAlertType.Text = "txtJobAlertType"
    '
    'DtxtJobAlertType
    '
    Me.txtJobAlertType.Location = New System.Drawing.Point(217, 137)
    Me.txtJobAlertType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtJobAlertType.Name = "txtJobAlertType"
    Me.txtJobAlertType.Size = New System.Drawing.Size(353, 25)
    Me.txtJobAlertType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtJobAlertType.TabIndex = 10
    Me.txtJobAlertType.Text = "txtJobAlertType"
    '
    'lblJobAlertType
    '
    Me.lblJobAlertType.AutoSize = True
    Me.lblJobAlertType.Location = New System.Drawing.Point(42, 140)
    Me.lblJobAlertType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblJobAlertType.Name = "lblJobAlertType"
    Me.lblJobAlertType.Size = New System.Drawing.Size(18, 13)
    Me.lblJobAlertType.TabIndex = 11
    Me.lblJobAlertType.Text = "Job Alert Type"
    '
    'DtxtOverrideName
    '
    Me.txtOverrideName.Location = New System.Drawing.Point(217, 177)
    Me.txtOverrideName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOverrideName.Name = "txtOverrideName"
    Me.txtOverrideName.Size = New System.Drawing.Size(353, 25)
    Me.txtOverrideName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOverrideName.TabIndex = 12
    Me.txtOverrideName.Text = "txtOverrideName"
    '
    'lblOverrideName
    '
    Me.lblOverrideName.AutoSize = True
    Me.lblOverrideName.Location = New System.Drawing.Point(42, 180)
    Me.lblOverrideName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOverrideName.Name = "lblOverrideName"
    Me.lblOverrideName.Size = New System.Drawing.Size(18, 13)
    Me.lblOverrideName.TabIndex = 13
    Me.lblOverrideName.Text = "Override Name"
    '
    'DtxtOverrideEmailOrPhone
    '
    Me.txtOverrideEmailOrPhone.Location = New System.Drawing.Point(217, 217)
    Me.txtOverrideEmailOrPhone.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOverrideEmailOrPhone.Name = "txtOverrideEmailOrPhone"
    Me.txtOverrideEmailOrPhone.Size = New System.Drawing.Size(353, 25)
    Me.txtOverrideEmailOrPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOverrideEmailOrPhone.TabIndex = 14
    Me.txtOverrideEmailOrPhone.Text = "txtOverrideEmailOrPhone"
    '
    'lblOverrideEmailOrPhone
    '
    Me.lblOverrideEmailOrPhone.AutoSize = True
    Me.lblOverrideEmailOrPhone.Location = New System.Drawing.Point(42, 220)
    Me.lblOverrideEmailOrPhone.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOverrideEmailOrPhone.Name = "lblOverrideEmailOrPhone"
    Me.lblOverrideEmailOrPhone.Size = New System.Drawing.Size(18, 13)
    Me.lblOverrideEmailOrPhone.TabIndex = 15
    Me.lblOverrideEmailOrPhone.Text = "Override Email Or Phone"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 297)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 16
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 297)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 17
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 297)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 18
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 285)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 19
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 285)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 20
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlJobAlertRecipient 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboJob)
    Me.Controls.Add(Me.txtJob)
    Me.Controls.Add(Me.txtJob)
    Me.Controls.Add(Me.lblJob)
    Me.Controls.Add(Me.cboUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.txtUser)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.cboJobAlertType)
    Me.Controls.Add(Me.txtJobAlertType)
    Me.Controls.Add(Me.txtJobAlertType)
    Me.Controls.Add(Me.lblJobAlertType)
    Me.Controls.Add(Me.txtOverrideName)
    Me.Controls.Add(Me.lblOverrideName)
    Me.Controls.Add(Me.txtOverrideEmailOrPhone)
    Me.Controls.Add(Me.lblOverrideEmailOrPhone)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_JobAlertRecipient"
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
  Friend WithEvents cboUser As IntelliCombo
  Friend WithEvents txtUser As System.Windows.Forms.TextBox
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents cboJobAlertType As System.Windows.Forms.ComboBox
  Friend WithEvents txtJobAlertType As System.Windows.Forms.TextBox
  Friend WithEvents lblJobAlertType As System.Windows.Forms.Label
  Friend WithEvents txtOverrideName As System.Windows.Forms.TextBox
  Friend WithEvents lblOverrideName As System.Windows.Forms.Label
  Friend WithEvents txtOverrideEmailOrPhone As System.Windows.Forms.TextBox
  Friend WithEvents lblOverrideEmailOrPhone As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
