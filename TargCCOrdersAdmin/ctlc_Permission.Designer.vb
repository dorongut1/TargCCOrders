<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_Permission
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
    Me.cboProcess = New IntelliCombo()
    Me.txtProcess = New System.Windows.Forms.TextBox()
    Me.lblProcess = New System.Windows.Forms.Label()
    Me.cboRole = New IntelliCombo()
    Me.txtRole = New System.Windows.Forms.TextBox()
    Me.lblRole = New System.Windows.Forms.Label()
    Me.chkCanDo = New System.Windows.Forms.CheckBox()
    Me.lblCanDo = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(137, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(433, 25)
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
    'cboProcess
    '
    Me.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboProcess.Location = New System.Drawing.Point(130, 51)
    Me.cboProcess.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboProcess.Name = "cboProcess"
    Me.cboProcess.Size = New System.Drawing.Size(383, 21)
    Me.cboProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboProcess.TabIndex = 2
    '
    'AtxtProcess
    '
    Me.txtProcess.Location = New System.Drawing.Point(137, 57)
    Me.txtProcess.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProcess.Name = "txtProcess"
    Me.txtProcess.Size = New System.Drawing.Size(433, 20)
    Me.txtProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProcess.TabIndex = 3
    Me.txtProcess.Text = "txtProcess"
    '
    'lblProcess
    '
    Me.lblProcess.AutoSize = True
    Me.lblProcess.Location = New System.Drawing.Point(42, 60)
    Me.lblProcess.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProcess.Name = "lblProcess"
    Me.lblProcess.Size = New System.Drawing.Size(18, 13)
    Me.lblProcess.TabIndex = 4
    Me.lblProcess.Text = "Process"
    '
    'cboRole
    '
    Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboRole.Location = New System.Drawing.Point(130, 91)
    Me.cboRole.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboRole.Name = "cboRole"
    Me.cboRole.Size = New System.Drawing.Size(383, 21)
    Me.cboRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboRole.TabIndex = 5
    '
    'AtxtRole
    '
    Me.txtRole.Location = New System.Drawing.Point(137, 97)
    Me.txtRole.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRole.Name = "txtRole"
    Me.txtRole.Size = New System.Drawing.Size(433, 20)
    Me.txtRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRole.TabIndex = 6
    Me.txtRole.Text = "txtRole"
    '
    'lblRole
    '
    Me.lblRole.AutoSize = True
    Me.lblRole.Location = New System.Drawing.Point(42, 100)
    Me.lblRole.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRole.Name = "lblRole"
    Me.lblRole.Size = New System.Drawing.Size(18, 13)
    Me.lblRole.TabIndex = 7
    Me.lblRole.Text = "Role"
    '
    'chkCanDo
    '
    Me.chkCanDo.AutoSize = True
    Me.chkCanDo.Location = New System.Drawing.Point(137, 143)
    Me.chkCanDo.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkCanDo.Name = "chkCanDo"
    Me.chkCanDo.Size = New System.Drawing.Size(15, 14)
    Me.chkCanDo.TabIndex = 8
    Me.chkCanDo.UseVisualStyleBackColor = True
    '
    'lblCanDo
    '
    Me.lblCanDo.AutoSize = True
    Me.lblCanDo.Location = New System.Drawing.Point(42, 138)
    Me.lblCanDo.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCanDo.Name = "lblCanDo"
    Me.lblCanDo.Size = New System.Drawing.Size(18, 13)
    Me.lblCanDo.TabIndex = 9
    Me.lblCanDo.Text = "Can Do"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 217)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 10
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 217)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 11
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 217)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 12
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 205)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 13
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 205)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 14
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlPermission 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboProcess)
    Me.Controls.Add(Me.txtProcess)
    Me.Controls.Add(Me.txtProcess)
    Me.Controls.Add(Me.lblProcess)
    Me.Controls.Add(Me.cboRole)
    Me.Controls.Add(Me.txtRole)
    Me.Controls.Add(Me.txtRole)
    Me.Controls.Add(Me.lblRole)
    Me.Controls.Add(Me.chkCanDo)
    Me.Controls.Add(Me.lblCanDo)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_Permission"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboProcess As IntelliCombo
  Friend WithEvents txtProcess As System.Windows.Forms.TextBox
  Friend WithEvents lblProcess As System.Windows.Forms.Label
  Friend WithEvents cboRole As IntelliCombo
  Friend WithEvents txtRole As System.Windows.Forms.TextBox
  Friend WithEvents lblRole As System.Windows.Forms.Label
  Friend WithEvents chkCanDo As System.Windows.Forms.CheckBox
  Friend WithEvents lblCanDo As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
