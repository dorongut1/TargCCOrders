<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_AlertMessage
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
    Me.txtNumber = New System.Windows.Forms.TextBox()
    Me.lblNumber = New System.Windows.Forms.Label()
    Me.txtDescription = New System.Windows.Forms.TextBox()
    Me.lblDescription = New System.Windows.Forms.Label()
    Me.cboType = New System.Windows.Forms.ComboBox()
    Me.txtType = New System.Windows.Forms.TextBox()
    Me.lblType = New System.Windows.Forms.Label()
    Me.cboSeverity = New System.Windows.Forms.ComboBox()
    Me.txtSeverity = New System.Windows.Forms.TextBox()
    Me.lblSeverity = New System.Windows.Forms.Label()
    Me.txtMessage = New System.Windows.Forms.TextBox()
    Me.lblMessage = New System.Windows.Forms.Label()
    Me.txtMessageLocalized = New System.Windows.Forms.TextBox()
    Me.lblMessageLocalized = New System.Windows.Forms.Label()
    Me.txtAction = New System.Windows.Forms.TextBox()
    Me.lblAction = New System.Windows.Forms.Label()
    Me.txtActionLocalized = New System.Windows.Forms.TextBox()
    Me.lblActionLocalized = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(146, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(424, 25)
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
    'DtxtNumber
    '
    Me.txtNumber.Location = New System.Drawing.Point(146, 57)
    Me.txtNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNumber.Name = "txtNumber"
    Me.txtNumber.Size = New System.Drawing.Size(424, 25)
    Me.txtNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNumber.TabIndex = 2
    Me.txtNumber.Text = "txtNumber"
    '
    'lblNumber
    '
    Me.lblNumber.AutoSize = True
    Me.lblNumber.Location = New System.Drawing.Point(42, 60)
    Me.lblNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNumber.Name = "lblNumber"
    Me.lblNumber.Size = New System.Drawing.Size(18, 13)
    Me.lblNumber.TabIndex = 3
    Me.lblNumber.Text = "Number"
    '
    'DtxtDescription
    '
    Me.txtDescription.Location = New System.Drawing.Point(146, 97)
    Me.txtDescription.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDescription.Name = "txtDescription"
    Me.txtDescription.Size = New System.Drawing.Size(424, 105)
    Me.txtDescription.Multiline = True
    Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtDescription.WordWrap = False 
    Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDescription.TabIndex = 4
    Me.txtDescription.Text = "txtDescription"
    '
    'lblDescription
    '
    Me.lblDescription.AutoSize = True
    Me.lblDescription.Location = New System.Drawing.Point(42, 95)
    Me.lblDescription.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDescription.Name = "lblDescription"
    Me.lblDescription.Size = New System.Drawing.Size(18, 13)
    Me.lblDescription.TabIndex = 5
    Me.lblDescription.Text = "Description"
    '
    'cboType
    '
    Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboType.FormattingEnabled = True
    Me.cboType.Location = New System.Drawing.Point(139, 211)
    Me.cboType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboType.Name = "cboType"
    Me.cboType.Size = New System.Drawing.Size(374, 21)
    Me.cboType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboType.TabIndex = 6
    '
    'BtxtType
    '
    Me.txtType.Location = New System.Drawing.Point(146, 217)
    Me.txtType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtType.Name = "txtType"
    Me.txtType.Size = New System.Drawing.Size(424, 20)
    Me.txtType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtType.TabIndex = 7
    Me.txtType.Text = "txtType"
    '
    'DtxtType
    '
    Me.txtType.Location = New System.Drawing.Point(146, 217)
    Me.txtType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtType.Name = "txtType"
    Me.txtType.Size = New System.Drawing.Size(424, 25)
    Me.txtType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtType.TabIndex = 8
    Me.txtType.Text = "txtType"
    '
    'lblType
    '
    Me.lblType.AutoSize = True
    Me.lblType.Location = New System.Drawing.Point(42, 220)
    Me.lblType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblType.Name = "lblType"
    Me.lblType.Size = New System.Drawing.Size(18, 13)
    Me.lblType.TabIndex = 9
    Me.lblType.Text = "Type"
    '
    'cboSeverity
    '
    Me.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboSeverity.FormattingEnabled = True
    Me.cboSeverity.Location = New System.Drawing.Point(139, 251)
    Me.cboSeverity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboSeverity.Name = "cboSeverity"
    Me.cboSeverity.Size = New System.Drawing.Size(374, 21)
    Me.cboSeverity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboSeverity.TabIndex = 10
    '
    'BtxtSeverity
    '
    Me.txtSeverity.Location = New System.Drawing.Point(146, 257)
    Me.txtSeverity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSeverity.Name = "txtSeverity"
    Me.txtSeverity.Size = New System.Drawing.Size(424, 20)
    Me.txtSeverity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeverity.TabIndex = 11
    Me.txtSeverity.Text = "txtSeverity"
    '
    'DtxtSeverity
    '
    Me.txtSeverity.Location = New System.Drawing.Point(146, 257)
    Me.txtSeverity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSeverity.Name = "txtSeverity"
    Me.txtSeverity.Size = New System.Drawing.Size(424, 25)
    Me.txtSeverity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeverity.TabIndex = 12
    Me.txtSeverity.Text = "txtSeverity"
    '
    'lblSeverity
    '
    Me.lblSeverity.AutoSize = True
    Me.lblSeverity.Location = New System.Drawing.Point(42, 260)
    Me.lblSeverity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSeverity.Name = "lblSeverity"
    Me.lblSeverity.Size = New System.Drawing.Size(18, 13)
    Me.lblSeverity.TabIndex = 13
    Me.lblSeverity.Text = "Severity"
    '
    'DtxtMessage
    '
    Me.txtMessage.Location = New System.Drawing.Point(146, 297)
    Me.txtMessage.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtMessage.Name = "txtMessage"
    Me.txtMessage.Size = New System.Drawing.Size(424, 105)
    Me.txtMessage.Multiline = True
    Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtMessage.WordWrap = False 
    Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMessage.TabIndex = 14
    Me.txtMessage.Text = "txtMessage"
    '
    'lblMessage
    '
    Me.lblMessage.AutoSize = True
    Me.lblMessage.Location = New System.Drawing.Point(42, 295)
    Me.lblMessage.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblMessage.Name = "lblMessage"
    Me.lblMessage.Size = New System.Drawing.Size(18, 13)
    Me.lblMessage.TabIndex = 15
    Me.lblMessage.Text = "Message"
    '
    'EtxtMessageLocalized
    '
    Me.txtMessageLocalized.Location = New System.Drawing.Point(146, 417)
    Me.txtMessageLocalized.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtMessageLocalized.Name = "txtMessageLocalized"
    Me.txtMessageLocalized.Size = New System.Drawing.Size(424, 105)
    Me.txtMessageLocalized.Multiline = True
    Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtMessage.WordWrap = False 
    Me.txtMessageLocalized.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtMessageLocalized.TabIndex = 16
    Me.txtMessageLocalized.Text = "txtMessageLocalized"
    '
    'lblMessageLocalized
    '
    Me.lblMessageLocalized.AutoSize = True
    Me.lblMessageLocalized.Location = New System.Drawing.Point(42, 440)
    Me.lblMessageLocalized.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblMessageLocalized.Name = "lblMessageLocalized"
    Me.lblMessageLocalized.Size = New System.Drawing.Size(18, 13)
    Me.lblMessageLocalized.TabIndex = 17
    Me.lblMessageLocalized.Text = "Message Loc"
    '
    'DtxtAction
    '
    Me.txtAction.Location = New System.Drawing.Point(146, 537)
    Me.txtAction.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAction.Name = "txtAction"
    Me.txtAction.Size = New System.Drawing.Size(424, 105)
    Me.txtAction.Multiline = True
    Me.txtAction.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtAction.WordWrap = False 
    Me.txtAction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAction.TabIndex = 18
    Me.txtAction.Text = "txtAction"
    '
    'lblAction
    '
    Me.lblAction.AutoSize = True
    Me.lblAction.Location = New System.Drawing.Point(42, 535)
    Me.lblAction.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAction.Name = "lblAction"
    Me.lblAction.Size = New System.Drawing.Size(18, 13)
    Me.lblAction.TabIndex = 19
    Me.lblAction.Text = "Action"
    '
    'EtxtActionLocalized
    '
    Me.txtActionLocalized.Location = New System.Drawing.Point(146, 657)
    Me.txtActionLocalized.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtActionLocalized.Name = "txtActionLocalized"
    Me.txtActionLocalized.Size = New System.Drawing.Size(424, 105)
    Me.txtActionLocalized.Multiline = True
    Me.txtAction.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtAction.WordWrap = False 
    Me.txtActionLocalized.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtActionLocalized.TabIndex = 20
    Me.txtActionLocalized.Text = "txtActionLocalized"
    '
    'lblActionLocalized
    '
    Me.lblActionLocalized.AutoSize = True
    Me.lblActionLocalized.Location = New System.Drawing.Point(42, 680)
    Me.lblActionLocalized.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblActionLocalized.Name = "lblActionLocalized"
    Me.lblActionLocalized.Size = New System.Drawing.Size(18, 13)
    Me.lblActionLocalized.TabIndex = 21
    Me.lblActionLocalized.Text = "Action Loc"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 817)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 22
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 817)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 23
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 817)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 24
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 805)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 25
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 805)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 26
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlAlertMessage 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtNumber)
    Me.Controls.Add(Me.lblNumber)
    Me.Controls.Add(Me.txtDescription)
    Me.Controls.Add(Me.lblDescription)
    Me.Controls.Add(Me.cboType)
    Me.Controls.Add(Me.txtType)
    Me.Controls.Add(Me.txtType)
    Me.Controls.Add(Me.lblType)
    Me.Controls.Add(Me.cboSeverity)
    Me.Controls.Add(Me.txtSeverity)
    Me.Controls.Add(Me.txtSeverity)
    Me.Controls.Add(Me.lblSeverity)
    Me.Controls.Add(Me.txtMessage)
    Me.Controls.Add(Me.lblMessage)
    Me.Controls.Add(Me.txtMessageLocalized)
    Me.Controls.Add(Me.lblMessageLocalized)
    Me.Controls.Add(Me.txtAction)
    Me.Controls.Add(Me.lblAction)
    Me.Controls.Add(Me.txtActionLocalized)
    Me.Controls.Add(Me.lblActionLocalized)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_AlertMessage"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblNumber As System.Windows.Forms.Label
  Friend WithEvents txtDescription As System.Windows.Forms.TextBox
  Friend WithEvents lblDescription As System.Windows.Forms.Label
  Friend WithEvents cboType As System.Windows.Forms.ComboBox
  Friend WithEvents txtType As System.Windows.Forms.TextBox
  Friend WithEvents lblType As System.Windows.Forms.Label
  Friend WithEvents cboSeverity As System.Windows.Forms.ComboBox
  Friend WithEvents txtSeverity As System.Windows.Forms.TextBox
  Friend WithEvents lblSeverity As System.Windows.Forms.Label
  Friend WithEvents txtMessage As System.Windows.Forms.TextBox
  Friend WithEvents lblMessage As System.Windows.Forms.Label
  Friend WithEvents txtMessageLocalized As System.Windows.Forms.TextBox
  Friend WithEvents lblMessageLocalized As System.Windows.Forms.Label
  Friend WithEvents txtAction As System.Windows.Forms.TextBox
  Friend WithEvents lblAction As System.Windows.Forms.Label
  Friend WithEvents txtActionLocalized As System.Windows.Forms.TextBox
  Friend WithEvents lblActionLocalized As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
