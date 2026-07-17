<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_Enumeration
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
    Me.chkIsSystem = New System.Windows.Forms.CheckBox()
    Me.lblIsSystem = New System.Windows.Forms.Label()
    Me.txtEnumType = New System.Windows.Forms.TextBox()
    Me.lblEnumType = New System.Windows.Forms.Label()
    Me.txtEnumValue = New System.Windows.Forms.TextBox()
    Me.lblEnumValue = New System.Windows.Forms.Label()
    Me.txtText = New System.Windows.Forms.TextBox()
    Me.lblText = New System.Windows.Forms.Label()
    Me.txtTextLocalized = New System.Windows.Forms.TextBox()
    Me.lblTextLocalized = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(145, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(425, 25)
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
    'chkIsSystem
    '
    Me.chkIsSystem.AutoSize = True
    Me.chkIsSystem.Location = New System.Drawing.Point(145, 63)
    Me.chkIsSystem.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsSystem.Name = "chkIsSystem"
    Me.chkIsSystem.Size = New System.Drawing.Size(15, 14)
    Me.chkIsSystem.TabIndex = 2
    Me.chkIsSystem.UseVisualStyleBackColor = True
    '
    'lblIsSystem
    '
    Me.lblIsSystem.AutoSize = True
    Me.lblIsSystem.Location = New System.Drawing.Point(42, 58)
    Me.lblIsSystem.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsSystem.Name = "lblIsSystem"
    Me.lblIsSystem.Size = New System.Drawing.Size(18, 13)
    Me.lblIsSystem.TabIndex = 3
    Me.lblIsSystem.Text = "Is System"
    '
    'DtxtEnumType
    '
    Me.txtEnumType.Location = New System.Drawing.Point(145, 97)
    Me.txtEnumType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEnumType.Name = "txtEnumType"
    Me.txtEnumType.Size = New System.Drawing.Size(425, 25)
    Me.txtEnumType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEnumType.TabIndex = 4
    Me.txtEnumType.Text = "txtEnumType"
    '
    'lblEnumType
    '
    Me.lblEnumType.AutoSize = True
    Me.lblEnumType.Location = New System.Drawing.Point(42, 100)
    Me.lblEnumType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEnumType.Name = "lblEnumType"
    Me.lblEnumType.Size = New System.Drawing.Size(18, 13)
    Me.lblEnumType.TabIndex = 5
    Me.lblEnumType.Text = "Enum Type"
    '
    'DtxtEnumValue
    '
    Me.txtEnumValue.Location = New System.Drawing.Point(145, 137)
    Me.txtEnumValue.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEnumValue.Name = "txtEnumValue"
    Me.txtEnumValue.Size = New System.Drawing.Size(425, 25)
    Me.txtEnumValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEnumValue.TabIndex = 6
    Me.txtEnumValue.Text = "txtEnumValue"
    '
    'lblEnumValue
    '
    Me.lblEnumValue.AutoSize = True
    Me.lblEnumValue.Location = New System.Drawing.Point(42, 140)
    Me.lblEnumValue.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEnumValue.Name = "lblEnumValue"
    Me.lblEnumValue.Size = New System.Drawing.Size(18, 13)
    Me.lblEnumValue.TabIndex = 7
    Me.lblEnumValue.Text = "Enum Value"
    '
    'DtxtText
    '
    Me.txtText.Location = New System.Drawing.Point(145, 177)
    Me.txtText.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtText.Name = "txtText"
    Me.txtText.Size = New System.Drawing.Size(425, 25)
    Me.txtText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtText.TabIndex = 8
    Me.txtText.Text = "txtText"
    '
    'lblText
    '
    Me.lblText.AutoSize = True
    Me.lblText.Location = New System.Drawing.Point(42, 180)
    Me.lblText.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblText.Name = "lblText"
    Me.lblText.Size = New System.Drawing.Size(18, 13)
    Me.lblText.TabIndex = 9
    Me.lblText.Text = "Text"
    '
    'EtxtTextLocalized
    '
    Me.txtTextLocalized.Location = New System.Drawing.Point(145, 217)
    Me.txtTextLocalized.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTextLocalized.Name = "txtTextLocalized"
    Me.txtTextLocalized.Size = New System.Drawing.Size(425, 20)
    Me.txtTextLocalized.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTextLocalized.TabIndex = 10
    Me.txtTextLocalized.Text = "txtTextLocalized"
    '
    'lblTextLocalized
    '
    Me.lblTextLocalized.AutoSize = True
    Me.lblTextLocalized.Location = New System.Drawing.Point(42, 220)
    Me.lblTextLocalized.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTextLocalized.Name = "lblTextLocalized"
    Me.lblTextLocalized.Size = New System.Drawing.Size(18, 13)
    Me.lblTextLocalized.TabIndex = 11
    Me.lblTextLocalized.Text = "Text Loc"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 297)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 12
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 297)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 13
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 297)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 14
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 285)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 15
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 285)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 16
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlEnumeration 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.chkIsSystem)
    Me.Controls.Add(Me.lblIsSystem)
    Me.Controls.Add(Me.txtEnumType)
    Me.Controls.Add(Me.lblEnumType)
    Me.Controls.Add(Me.txtEnumValue)
    Me.Controls.Add(Me.lblEnumValue)
    Me.Controls.Add(Me.txtText)
    Me.Controls.Add(Me.lblText)
    Me.Controls.Add(Me.txtTextLocalized)
    Me.Controls.Add(Me.lblTextLocalized)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_Enumeration"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents chkIsSystem As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsSystem As System.Windows.Forms.Label
  Friend WithEvents txtEnumType As System.Windows.Forms.TextBox
  Friend WithEvents lblEnumType As System.Windows.Forms.Label
  Friend WithEvents txtEnumValue As System.Windows.Forms.TextBox
  Friend WithEvents lblEnumValue As System.Windows.Forms.Label
  Friend WithEvents txtText As System.Windows.Forms.TextBox
  Friend WithEvents lblText As System.Windows.Forms.Label
  Friend WithEvents txtTextLocalized As System.Windows.Forms.TextBox
  Friend WithEvents lblTextLocalized As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
