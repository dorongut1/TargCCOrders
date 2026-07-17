<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_ObjectToTranslate
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
    Me.cboObjectType = New System.Windows.Forms.ComboBox()
    Me.txtObjectType = New System.Windows.Forms.TextBox()
    Me.lblObjectType = New System.Windows.Forms.Label()
    Me.txtObject = New System.Windows.Forms.TextBox()
    Me.lblObject = New System.Windows.Forms.Label()
    Me.txtItem = New System.Windows.Forms.TextBox()
    Me.lblItem = New System.Windows.Forms.Label()
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
    'cboObjectType
    '
    Me.cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboObjectType.FormattingEnabled = True
    Me.cboObjectType.Location = New System.Drawing.Point(138, 51)
    Me.cboObjectType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboObjectType.Name = "cboObjectType"
    Me.cboObjectType.Size = New System.Drawing.Size(375, 21)
    Me.cboObjectType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboObjectType.TabIndex = 2
    '
    'BtxtObjectType
    '
    Me.txtObjectType.Location = New System.Drawing.Point(145, 57)
    Me.txtObjectType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtObjectType.Name = "txtObjectType"
    Me.txtObjectType.Size = New System.Drawing.Size(425, 20)
    Me.txtObjectType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtObjectType.TabIndex = 3
    Me.txtObjectType.Text = "txtObjectType"
    '
    'DtxtObjectType
    '
    Me.txtObjectType.Location = New System.Drawing.Point(145, 57)
    Me.txtObjectType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtObjectType.Name = "txtObjectType"
    Me.txtObjectType.Size = New System.Drawing.Size(425, 25)
    Me.txtObjectType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtObjectType.TabIndex = 4
    Me.txtObjectType.Text = "txtObjectType"
    '
    'lblObjectType
    '
    Me.lblObjectType.AutoSize = True
    Me.lblObjectType.Location = New System.Drawing.Point(42, 60)
    Me.lblObjectType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblObjectType.Name = "lblObjectType"
    Me.lblObjectType.Size = New System.Drawing.Size(18, 13)
    Me.lblObjectType.TabIndex = 5
    Me.lblObjectType.Text = "Object Type"
    '
    'DtxtObject
    '
    Me.txtObject.Location = New System.Drawing.Point(145, 97)
    Me.txtObject.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtObject.Name = "txtObject"
    Me.txtObject.Size = New System.Drawing.Size(425, 25)
    Me.txtObject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtObject.TabIndex = 6
    Me.txtObject.Text = "txtObject"
    '
    'lblObject
    '
    Me.lblObject.AutoSize = True
    Me.lblObject.Location = New System.Drawing.Point(42, 100)
    Me.lblObject.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblObject.Name = "lblObject"
    Me.lblObject.Size = New System.Drawing.Size(18, 13)
    Me.lblObject.TabIndex = 7
    Me.lblObject.Text = "Object"
    '
    'DtxtItem
    '
    Me.txtItem.Location = New System.Drawing.Point(145, 137)
    Me.txtItem.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtItem.Name = "txtItem"
    Me.txtItem.Size = New System.Drawing.Size(425, 105)
    Me.txtItem.Multiline = True
    Me.txtItem.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtItem.WordWrap = False 
    Me.txtItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtItem.TabIndex = 8
    Me.txtItem.Text = "txtItem"
    '
    'lblItem
    '
    Me.lblItem.AutoSize = True
    Me.lblItem.Location = New System.Drawing.Point(42, 135)
    Me.lblItem.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblItem.Name = "lblItem"
    Me.lblItem.Size = New System.Drawing.Size(18, 13)
    Me.lblItem.TabIndex = 9
    Me.lblItem.Text = "Item"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 297)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 10
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 297)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 11
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 297)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 12
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 285)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 13
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 285)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 14
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlObjectToTranslate 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboObjectType)
    Me.Controls.Add(Me.txtObjectType)
    Me.Controls.Add(Me.txtObjectType)
    Me.Controls.Add(Me.lblObjectType)
    Me.Controls.Add(Me.txtObject)
    Me.Controls.Add(Me.lblObject)
    Me.Controls.Add(Me.txtItem)
    Me.Controls.Add(Me.lblItem)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_ObjectToTranslate"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboObjectType As System.Windows.Forms.ComboBox
  Friend WithEvents txtObjectType As System.Windows.Forms.TextBox
  Friend WithEvents lblObjectType As System.Windows.Forms.Label
  Friend WithEvents txtObject As System.Windows.Forms.TextBox
  Friend WithEvents lblObject As System.Windows.Forms.Label
  Friend WithEvents txtItem As System.Windows.Forms.TextBox
  Friend WithEvents lblItem As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
