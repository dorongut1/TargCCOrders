<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_ObjectTranslation
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
    Me.cboObjectToTranslate = New IntelliCombo()
    Me.txtObjectToTranslate = New System.Windows.Forms.TextBox()
    Me.lblObjectToTranslate = New System.Windows.Forms.Label()
    Me.txtInstance = New System.Windows.Forms.TextBox()
    Me.lblInstance = New System.Windows.Forms.Label()
    Me.txtDefaultText = New System.Windows.Forms.TextBox()
    Me.lblDefaultText = New System.Windows.Forms.Label()
    Me.cboLanguage = New System.Windows.Forms.ComboBox()
    Me.txtLanguage = New System.Windows.Forms.TextBox()
    Me.lblLanguage = New System.Windows.Forms.Label()
    Me.txtText = New System.Windows.Forms.TextBox()
    Me.lblText = New System.Windows.Forms.Label()
    Me.txtInstanceUniqueText = New System.Windows.Forms.TextBox()
    Me.lblInstanceUniqueText = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(199, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(371, 25)
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
    'cboObjectToTranslate
    '
    Me.cboObjectToTranslate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboObjectToTranslate.Location = New System.Drawing.Point(192, 51)
    Me.cboObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboObjectToTranslate.Name = "cboObjectToTranslate"
    Me.cboObjectToTranslate.Size = New System.Drawing.Size(321, 21)
    Me.cboObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboObjectToTranslate.TabIndex = 2
    '
    'AtxtObjectToTranslate
    '
    Me.txtObjectToTranslate.Location = New System.Drawing.Point(199, 57)
    Me.txtObjectToTranslate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtObjectToTranslate.Name = "txtObjectToTranslate"
    Me.txtObjectToTranslate.Size = New System.Drawing.Size(371, 20)
    Me.txtObjectToTranslate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtObjectToTranslate.TabIndex = 3
    Me.txtObjectToTranslate.Text = "txtObjectToTranslate"
    '
    'lblObjectToTranslate
    '
    Me.lblObjectToTranslate.AutoSize = True
    Me.lblObjectToTranslate.Location = New System.Drawing.Point(42, 60)
    Me.lblObjectToTranslate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblObjectToTranslate.Name = "lblObjectToTranslate"
    Me.lblObjectToTranslate.Size = New System.Drawing.Size(18, 13)
    Me.lblObjectToTranslate.TabIndex = 4
    Me.lblObjectToTranslate.Text = "Object To Translate"
    '
    'DtxtInstance
    '
    Me.txtInstance.Location = New System.Drawing.Point(199, 97)
    Me.txtInstance.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtInstance.Name = "txtInstance"
    Me.txtInstance.Size = New System.Drawing.Size(371, 25)
    Me.txtInstance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtInstance.TabIndex = 5
    Me.txtInstance.Text = "txtInstance"
    '
    'lblInstance
    '
    Me.lblInstance.AutoSize = True
    Me.lblInstance.Location = New System.Drawing.Point(42, 100)
    Me.lblInstance.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblInstance.Name = "lblInstance"
    Me.lblInstance.Size = New System.Drawing.Size(18, 13)
    Me.lblInstance.TabIndex = 6
    Me.lblInstance.Text = "Instance"
    '
    'DtxtDefaultText
    '
    Me.txtDefaultText.Location = New System.Drawing.Point(199, 137)
    Me.txtDefaultText.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDefaultText.Name = "txtDefaultText"
    Me.txtDefaultText.Size = New System.Drawing.Size(371, 105)
    Me.txtDefaultText.Multiline = True
    Me.txtDefaultText.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtDefaultText.WordWrap = False 
    Me.txtDefaultText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDefaultText.TabIndex = 7
    Me.txtDefaultText.Text = "txtDefaultText"
    '
    'lblDefaultText
    '
    Me.lblDefaultText.AutoSize = True
    Me.lblDefaultText.Location = New System.Drawing.Point(42, 135)
    Me.lblDefaultText.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDefaultText.Name = "lblDefaultText"
    Me.lblDefaultText.Size = New System.Drawing.Size(18, 13)
    Me.lblDefaultText.TabIndex = 8
    Me.lblDefaultText.Text = "Default Text"
    '
    'cboLanguage
    '
    Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboLanguage.FormattingEnabled = True
    Me.cboLanguage.Location = New System.Drawing.Point(192, 251)
    Me.cboLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboLanguage.Name = "cboLanguage"
    Me.cboLanguage.Size = New System.Drawing.Size(321, 21)
    Me.cboLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboLanguage.TabIndex = 9
    '
    'BtxtLanguage
    '
    Me.txtLanguage.Location = New System.Drawing.Point(199, 257)
    Me.txtLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLanguage.Name = "txtLanguage"
    Me.txtLanguage.Size = New System.Drawing.Size(371, 20)
    Me.txtLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLanguage.TabIndex = 10
    Me.txtLanguage.Text = "txtLanguage"
    '
    'DtxtLanguage
    '
    Me.txtLanguage.Location = New System.Drawing.Point(199, 257)
    Me.txtLanguage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLanguage.Name = "txtLanguage"
    Me.txtLanguage.Size = New System.Drawing.Size(371, 25)
    Me.txtLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLanguage.TabIndex = 11
    Me.txtLanguage.Text = "txtLanguage"
    '
    'lblLanguage
    '
    Me.lblLanguage.AutoSize = True
    Me.lblLanguage.Location = New System.Drawing.Point(42, 260)
    Me.lblLanguage.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLanguage.Name = "lblLanguage"
    Me.lblLanguage.Size = New System.Drawing.Size(18, 13)
    Me.lblLanguage.TabIndex = 12
    Me.lblLanguage.Text = "Language"
    '
    'DtxtText
    '
    Me.txtText.Location = New System.Drawing.Point(199, 297)
    Me.txtText.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtText.Name = "txtText"
    Me.txtText.Size = New System.Drawing.Size(371, 105)
    Me.txtText.Multiline = True
    Me.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtText.WordWrap = False 
    Me.txtText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtText.TabIndex = 13
    Me.txtText.Text = "txtText"
    '
    'lblText
    '
    Me.lblText.AutoSize = True
    Me.lblText.Location = New System.Drawing.Point(42, 295)
    Me.lblText.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblText.Name = "lblText"
    Me.lblText.Size = New System.Drawing.Size(18, 13)
    Me.lblText.TabIndex = 14
    Me.lblText.Text = "Text"
    '
    'DtxtInstanceUniqueText
    '
    Me.txtInstanceUniqueText.Location = New System.Drawing.Point(199, 417)
    Me.txtInstanceUniqueText.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtInstanceUniqueText.Name = "txtInstanceUniqueText"
    Me.txtInstanceUniqueText.Size = New System.Drawing.Size(371, 105)
    Me.txtInstanceUniqueText.Multiline = True
    Me.txtInstanceUniqueText.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtInstanceUniqueText.WordWrap = False 
    Me.txtInstanceUniqueText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtInstanceUniqueText.TabIndex = 15
    Me.txtInstanceUniqueText.Text = "txtInstanceUniqueText"
    '
    'lblInstanceUniqueText
    '
    Me.lblInstanceUniqueText.AutoSize = True
    Me.lblInstanceUniqueText.Location = New System.Drawing.Point(42, 415)
    Me.lblInstanceUniqueText.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblInstanceUniqueText.Name = "lblInstanceUniqueText"
    Me.lblInstanceUniqueText.Size = New System.Drawing.Size(18, 13)
    Me.lblInstanceUniqueText.TabIndex = 16
    Me.lblInstanceUniqueText.Text = "Instance Unique Text"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 577)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 17
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 577)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 18
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 577)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 19
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 565)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 20
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 565)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 21
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlObjectTranslation 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboObjectToTranslate)
    Me.Controls.Add(Me.txtObjectToTranslate)
    Me.Controls.Add(Me.txtObjectToTranslate)
    Me.Controls.Add(Me.lblObjectToTranslate)
    Me.Controls.Add(Me.txtInstance)
    Me.Controls.Add(Me.lblInstance)
    Me.Controls.Add(Me.txtDefaultText)
    Me.Controls.Add(Me.lblDefaultText)
    Me.Controls.Add(Me.cboLanguage)
    Me.Controls.Add(Me.txtLanguage)
    Me.Controls.Add(Me.txtLanguage)
    Me.Controls.Add(Me.lblLanguage)
    Me.Controls.Add(Me.txtText)
    Me.Controls.Add(Me.lblText)
    Me.Controls.Add(Me.txtInstanceUniqueText)
    Me.Controls.Add(Me.lblInstanceUniqueText)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_ObjectTranslation"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboObjectToTranslate As IntelliCombo
  Friend WithEvents txtObjectToTranslate As System.Windows.Forms.TextBox
  Friend WithEvents lblObjectToTranslate As System.Windows.Forms.Label
  Friend WithEvents txtInstance As System.Windows.Forms.TextBox
  Friend WithEvents lblInstance As System.Windows.Forms.Label
  Friend WithEvents txtDefaultText As System.Windows.Forms.TextBox
  Friend WithEvents lblDefaultText As System.Windows.Forms.Label
  Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
  Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
  Friend WithEvents lblLanguage As System.Windows.Forms.Label
  Friend WithEvents txtText As System.Windows.Forms.TextBox
  Friend WithEvents lblText As System.Windows.Forms.Label
  Friend WithEvents txtInstanceUniqueText As System.Windows.Forms.TextBox
  Friend WithEvents lblInstanceUniqueText As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
