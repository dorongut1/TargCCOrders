<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccBeehiveBuyerTracking
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
    Me.cboCustomer = New IntelliCombo()
    Me.txtCustomer = New System.Windows.Forms.TextBox()
    Me.lblCustomer = New System.Windows.Forms.Label()
    Me.dtpLastOrderDate = New System.Windows.Forms.DateTimePicker()
    Me.txtLastOrderDate = New System.Windows.Forms.TextBox()
    Me.lblLastOrderDate = New System.Windows.Forms.Label()
    Me.txtBeehiveQuantity = New System.Windows.Forms.TextBox()
    Me.lblBeehiveQuantity = New System.Windows.Forms.Label()
    Me.txtReminderMonth = New System.Windows.Forms.TextBox()
    Me.lblReminderMonth = New System.Windows.Forms.Label()
    Me.chkIsRelevant = New System.Windows.Forms.CheckBox()
    Me.lblIsRelevant = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(178, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(392, 25)
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
    'cboCustomer
    '
    Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCustomer.Location = New System.Drawing.Point(171, 51)
    Me.cboCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCustomer.Name = "cboCustomer"
    Me.cboCustomer.Size = New System.Drawing.Size(342, 21)
    Me.cboCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCustomer.TabIndex = 2
    '
    'AtxtCustomer
    '
    Me.txtCustomer.Location = New System.Drawing.Point(178, 57)
    Me.txtCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomer.Name = "txtCustomer"
    Me.txtCustomer.Size = New System.Drawing.Size(392, 20)
    Me.txtCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomer.TabIndex = 3
    Me.txtCustomer.Text = "txtCustomer"
    '
    'lblCustomer
    '
    Me.lblCustomer.AutoSize = True
    Me.lblCustomer.Location = New System.Drawing.Point(42, 60)
    Me.lblCustomer.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomer.Name = "lblCustomer"
    Me.lblCustomer.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomer.TabIndex = 4
    Me.lblCustomer.Text = "Customer"
    '
    'dtpLastOrderDate
    '
    Me.dtpLastOrderDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpLastOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpLastOrderDate.Location = New System.Drawing.Point(171, 91)
    Me.dtpLastOrderDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpLastOrderDate.Name = "dtpLastOrderDate"
    Me.dtpLastOrderDate.ShowCheckBox = True
    Me.dtpLastOrderDate.ShowUpDown = True
    Me.dtpLastOrderDate.Size = New System.Drawing.Size(342, 20)
    Me.dtpLastOrderDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpLastOrderDate.TabIndex = 5
    '
    'CtxtLastOrderDate
    '
    Me.txtLastOrderDate.Location = New System.Drawing.Point(178, 97)
    Me.txtLastOrderDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLastOrderDate.Name = "txtLastOrderDate"
    Me.txtLastOrderDate.Size = New System.Drawing.Size(392, 20)
    Me.txtLastOrderDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLastOrderDate.TabIndex = 6
    Me.txtLastOrderDate.Text = "txtLastOrderDate"
    '
    'lblLastOrderDate
    '
    Me.lblLastOrderDate.AutoSize = True
    Me.lblLastOrderDate.Location = New System.Drawing.Point(42, 100)
    Me.lblLastOrderDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLastOrderDate.Name = "lblLastOrderDate"
    Me.lblLastOrderDate.Size = New System.Drawing.Size(18, 13)
    Me.lblLastOrderDate.TabIndex = 7
    Me.lblLastOrderDate.Text = "Last Order Date"
    '
    'DtxtBeehiveQuantity
    '
    Me.txtBeehiveQuantity.Location = New System.Drawing.Point(178, 137)
    Me.txtBeehiveQuantity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtBeehiveQuantity.Name = "txtBeehiveQuantity"
    Me.txtBeehiveQuantity.Size = New System.Drawing.Size(392, 25)
    Me.txtBeehiveQuantity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtBeehiveQuantity.TabIndex = 8
    Me.txtBeehiveQuantity.Text = "txtBeehiveQuantity"
    '
    'lblBeehiveQuantity
    '
    Me.lblBeehiveQuantity.AutoSize = True
    Me.lblBeehiveQuantity.Location = New System.Drawing.Point(42, 140)
    Me.lblBeehiveQuantity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblBeehiveQuantity.Name = "lblBeehiveQuantity"
    Me.lblBeehiveQuantity.Size = New System.Drawing.Size(18, 13)
    Me.lblBeehiveQuantity.TabIndex = 9
    Me.lblBeehiveQuantity.Text = "Beehive Quantity"
    '
    'DtxtReminderMonth
    '
    Me.txtReminderMonth.Location = New System.Drawing.Point(178, 177)
    Me.txtReminderMonth.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtReminderMonth.Name = "txtReminderMonth"
    Me.txtReminderMonth.Size = New System.Drawing.Size(392, 25)
    Me.txtReminderMonth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtReminderMonth.TabIndex = 10
    Me.txtReminderMonth.Text = "txtReminderMonth"
    '
    'lblReminderMonth
    '
    Me.lblReminderMonth.AutoSize = True
    Me.lblReminderMonth.Location = New System.Drawing.Point(42, 180)
    Me.lblReminderMonth.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblReminderMonth.Name = "lblReminderMonth"
    Me.lblReminderMonth.Size = New System.Drawing.Size(18, 13)
    Me.lblReminderMonth.TabIndex = 11
    Me.lblReminderMonth.Text = "Reminder Month"
    '
    'chkIsRelevant
    '
    Me.chkIsRelevant.AutoSize = True
    Me.chkIsRelevant.Location = New System.Drawing.Point(178, 223)
    Me.chkIsRelevant.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsRelevant.Name = "chkIsRelevant"
    Me.chkIsRelevant.Size = New System.Drawing.Size(15, 14)
    Me.chkIsRelevant.TabIndex = 12
    Me.chkIsRelevant.UseVisualStyleBackColor = True
    '
    'lblIsRelevant
    '
    Me.lblIsRelevant.AutoSize = True
    Me.lblIsRelevant.Location = New System.Drawing.Point(42, 218)
    Me.lblIsRelevant.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsRelevant.Name = "lblIsRelevant"
    Me.lblIsRelevant.Size = New System.Drawing.Size(18, 13)
    Me.lblIsRelevant.TabIndex = 13
    Me.lblIsRelevant.Text = "Is Relevant"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(178, 257)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(392, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 14
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 255)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 15
    Me.lblNotes.Text = "Notes"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 417)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 16
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 417)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 17
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 417)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 18
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 405)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 19
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 405)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 20
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlBeehiveBuyerTracking 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.lblCustomer)
    Me.Controls.Add(Me.dtpLastOrderDate)
    Me.Controls.Add(Me.txtLastOrderDate)
    Me.Controls.Add(Me.lblLastOrderDate)
    Me.Controls.Add(Me.txtBeehiveQuantity)
    Me.Controls.Add(Me.lblBeehiveQuantity)
    Me.Controls.Add(Me.txtReminderMonth)
    Me.Controls.Add(Me.lblReminderMonth)
    Me.Controls.Add(Me.chkIsRelevant)
    Me.Controls.Add(Me.lblIsRelevant)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccBeehiveBuyerTracking"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboCustomer As IntelliCombo
  Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomer As System.Windows.Forms.Label
  Friend WithEvents dtpLastOrderDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtLastOrderDate As System.Windows.Forms.TextBox
  Friend WithEvents lblLastOrderDate As System.Windows.Forms.Label
  Friend WithEvents txtBeehiveQuantity As System.Windows.Forms.TextBox
  Friend WithEvents lblBeehiveQuantity As System.Windows.Forms.Label
  Friend WithEvents txtReminderMonth As System.Windows.Forms.TextBox
  Friend WithEvents lblReminderMonth As System.Windows.Forms.Label
  Friend WithEvents chkIsRelevant As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsRelevant As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
