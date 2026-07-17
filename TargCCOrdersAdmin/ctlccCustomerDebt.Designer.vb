<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccCustomerDebt
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
    Me.cboOrderHeader = New IntelliCombo()
    Me.txtOrderHeader = New System.Windows.Forms.TextBox()
    Me.lblOrderHeader = New System.Windows.Forms.Label()
    Me.txtDebtAmount = New System.Windows.Forms.TextBox()
    Me.lblDebtAmount = New System.Windows.Forms.Label()
    Me.txtPaidAmount = New System.Windows.Forms.TextBox()
    Me.lblPaidAmount = New System.Windows.Forms.Label()
    Me.txtRemainingAmount = New System.Windows.Forms.TextBox()
    Me.lblRemainingAmount = New System.Windows.Forms.Label()
    Me.dtpDebtDate = New System.Windows.Forms.DateTimePicker()
    Me.txtDebtDate = New System.Windows.Forms.TextBox()
    Me.lblDebtDate = New System.Windows.Forms.Label()
    Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
    Me.txtDueDate = New System.Windows.Forms.TextBox()
    Me.lblDueDate = New System.Windows.Forms.Label()
    Me.cboDebtStatus = New System.Windows.Forms.ComboBox()
    Me.txtDebtStatus = New System.Windows.Forms.TextBox()
    Me.lblDebtStatus = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.chkNeedsAttention = New System.Windows.Forms.CheckBox()
    Me.lblNeedsAttention = New System.Windows.Forms.Label()
    Me.txtProductTypes = New System.Windows.Forms.TextBox()
    Me.lblProductTypes = New System.Windows.Forms.Label()
    Me.dtpDeliveryDate = New System.Windows.Forms.DateTimePicker()
    Me.txtDeliveryDate = New System.Windows.Forms.TextBox()
    Me.lblDeliveryDate = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(191, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(379, 25)
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
    Me.cboCustomer.Location = New System.Drawing.Point(184, 51)
    Me.cboCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCustomer.Name = "cboCustomer"
    Me.cboCustomer.Size = New System.Drawing.Size(329, 21)
    Me.cboCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCustomer.TabIndex = 2
    '
    'AtxtCustomer
    '
    Me.txtCustomer.Location = New System.Drawing.Point(191, 57)
    Me.txtCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomer.Name = "txtCustomer"
    Me.txtCustomer.Size = New System.Drawing.Size(379, 20)
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
    'cboOrderHeader
    '
    Me.cboOrderHeader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboOrderHeader.Location = New System.Drawing.Point(184, 91)
    Me.cboOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboOrderHeader.Name = "cboOrderHeader"
    Me.cboOrderHeader.Size = New System.Drawing.Size(329, 21)
    Me.cboOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboOrderHeader.TabIndex = 5
    '
    'AtxtOrderHeader
    '
    Me.txtOrderHeader.Location = New System.Drawing.Point(191, 97)
    Me.txtOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderHeader.Name = "txtOrderHeader"
    Me.txtOrderHeader.Size = New System.Drawing.Size(379, 20)
    Me.txtOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderHeader.TabIndex = 6
    Me.txtOrderHeader.Text = "txtOrderHeader"
    '
    'lblOrderHeader
    '
    Me.lblOrderHeader.AutoSize = True
    Me.lblOrderHeader.Location = New System.Drawing.Point(42, 100)
    Me.lblOrderHeader.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderHeader.Name = "lblOrderHeader"
    Me.lblOrderHeader.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderHeader.TabIndex = 7
    Me.lblOrderHeader.Text = "Order Header"
    '
    'DtxtDebtAmount
    '
    Me.txtDebtAmount.Location = New System.Drawing.Point(191, 137)
    Me.txtDebtAmount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDebtAmount.Name = "txtDebtAmount"
    Me.txtDebtAmount.Size = New System.Drawing.Size(379, 25)
    Me.txtDebtAmount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDebtAmount.TabIndex = 8
    Me.txtDebtAmount.Text = "txtDebtAmount"
    '
    'lblDebtAmount
    '
    Me.lblDebtAmount.AutoSize = True
    Me.lblDebtAmount.Location = New System.Drawing.Point(42, 140)
    Me.lblDebtAmount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDebtAmount.Name = "lblDebtAmount"
    Me.lblDebtAmount.Size = New System.Drawing.Size(18, 13)
    Me.lblDebtAmount.TabIndex = 9
    Me.lblDebtAmount.Text = "Debt Amount"
    '
    'DtxtPaidAmount
    '
    Me.txtPaidAmount.Location = New System.Drawing.Point(191, 177)
    Me.txtPaidAmount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaidAmount.Name = "txtPaidAmount"
    Me.txtPaidAmount.Size = New System.Drawing.Size(379, 25)
    Me.txtPaidAmount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaidAmount.TabIndex = 10
    Me.txtPaidAmount.Text = "txtPaidAmount"
    '
    'lblPaidAmount
    '
    Me.lblPaidAmount.AutoSize = True
    Me.lblPaidAmount.Location = New System.Drawing.Point(42, 180)
    Me.lblPaidAmount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPaidAmount.Name = "lblPaidAmount"
    Me.lblPaidAmount.Size = New System.Drawing.Size(18, 13)
    Me.lblPaidAmount.TabIndex = 11
    Me.lblPaidAmount.Text = "Paid Amount"
    '
    'DtxtRemainingAmount
    '
    Me.txtRemainingAmount.Location = New System.Drawing.Point(191, 217)
    Me.txtRemainingAmount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRemainingAmount.Name = "txtRemainingAmount"
    Me.txtRemainingAmount.Size = New System.Drawing.Size(379, 25)
    Me.txtRemainingAmount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRemainingAmount.TabIndex = 12
    Me.txtRemainingAmount.Text = "txtRemainingAmount"
    '
    'lblRemainingAmount
    '
    Me.lblRemainingAmount.AutoSize = True
    Me.lblRemainingAmount.Location = New System.Drawing.Point(42, 220)
    Me.lblRemainingAmount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRemainingAmount.Name = "lblRemainingAmount"
    Me.lblRemainingAmount.Size = New System.Drawing.Size(18, 13)
    Me.lblRemainingAmount.TabIndex = 13
    Me.lblRemainingAmount.Text = "Remaining Amount"
    '
    'dtpDebtDate
    '
    Me.dtpDebtDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpDebtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpDebtDate.Location = New System.Drawing.Point(184, 251)
    Me.dtpDebtDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpDebtDate.Name = "dtpDebtDate"
    Me.dtpDebtDate.ShowCheckBox = True
    Me.dtpDebtDate.ShowUpDown = True
    Me.dtpDebtDate.Size = New System.Drawing.Size(329, 20)
    Me.dtpDebtDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpDebtDate.TabIndex = 14
    '
    'CtxtDebtDate
    '
    Me.txtDebtDate.Location = New System.Drawing.Point(191, 257)
    Me.txtDebtDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDebtDate.Name = "txtDebtDate"
    Me.txtDebtDate.Size = New System.Drawing.Size(379, 20)
    Me.txtDebtDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDebtDate.TabIndex = 15
    Me.txtDebtDate.Text = "txtDebtDate"
    '
    'lblDebtDate
    '
    Me.lblDebtDate.AutoSize = True
    Me.lblDebtDate.Location = New System.Drawing.Point(42, 260)
    Me.lblDebtDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDebtDate.Name = "lblDebtDate"
    Me.lblDebtDate.Size = New System.Drawing.Size(18, 13)
    Me.lblDebtDate.TabIndex = 16
    Me.lblDebtDate.Text = "Debt Date"
    '
    'dtpDueDate
    '
    Me.dtpDueDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpDueDate.Location = New System.Drawing.Point(184, 291)
    Me.dtpDueDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpDueDate.Name = "dtpDueDate"
    Me.dtpDueDate.ShowCheckBox = True
    Me.dtpDueDate.ShowUpDown = True
    Me.dtpDueDate.Size = New System.Drawing.Size(329, 20)
    Me.dtpDueDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpDueDate.TabIndex = 17
    '
    'CtxtDueDate
    '
    Me.txtDueDate.Location = New System.Drawing.Point(191, 297)
    Me.txtDueDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDueDate.Name = "txtDueDate"
    Me.txtDueDate.Size = New System.Drawing.Size(379, 20)
    Me.txtDueDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDueDate.TabIndex = 18
    Me.txtDueDate.Text = "txtDueDate"
    '
    'lblDueDate
    '
    Me.lblDueDate.AutoSize = True
    Me.lblDueDate.Location = New System.Drawing.Point(42, 300)
    Me.lblDueDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDueDate.Name = "lblDueDate"
    Me.lblDueDate.Size = New System.Drawing.Size(18, 13)
    Me.lblDueDate.TabIndex = 19
    Me.lblDueDate.Text = "Due Date"
    '
    'cboDebtStatus
    '
    Me.cboDebtStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDebtStatus.FormattingEnabled = True
    Me.cboDebtStatus.Location = New System.Drawing.Point(184, 331)
    Me.cboDebtStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDebtStatus.Name = "cboDebtStatus"
    Me.cboDebtStatus.Size = New System.Drawing.Size(329, 21)
    Me.cboDebtStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDebtStatus.TabIndex = 20
    '
    'BtxtDebtStatus
    '
    Me.txtDebtStatus.Location = New System.Drawing.Point(191, 337)
    Me.txtDebtStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDebtStatus.Name = "txtDebtStatus"
    Me.txtDebtStatus.Size = New System.Drawing.Size(379, 20)
    Me.txtDebtStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDebtStatus.TabIndex = 21
    Me.txtDebtStatus.Text = "txtDebtStatus"
    '
    'DtxtDebtStatus
    '
    Me.txtDebtStatus.Location = New System.Drawing.Point(191, 337)
    Me.txtDebtStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDebtStatus.Name = "txtDebtStatus"
    Me.txtDebtStatus.Size = New System.Drawing.Size(379, 25)
    Me.txtDebtStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDebtStatus.TabIndex = 22
    Me.txtDebtStatus.Text = "txtDebtStatus"
    '
    'lblDebtStatus
    '
    Me.lblDebtStatus.AutoSize = True
    Me.lblDebtStatus.Location = New System.Drawing.Point(42, 340)
    Me.lblDebtStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDebtStatus.Name = "lblDebtStatus"
    Me.lblDebtStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblDebtStatus.TabIndex = 23
    Me.lblDebtStatus.Text = "Debt Status"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(191, 377)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(379, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 24
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 375)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 25
    Me.lblNotes.Text = "Notes"
    '
    'chkNeedsAttention
    '
    Me.chkNeedsAttention.AutoSize = True
    Me.chkNeedsAttention.Location = New System.Drawing.Point(191, 503)
    Me.chkNeedsAttention.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkNeedsAttention.Name = "chkNeedsAttention"
    Me.chkNeedsAttention.Size = New System.Drawing.Size(15, 14)
    Me.chkNeedsAttention.TabIndex = 26
    Me.chkNeedsAttention.UseVisualStyleBackColor = True
    '
    'lblNeedsAttention
    '
    Me.lblNeedsAttention.AutoSize = True
    Me.lblNeedsAttention.Location = New System.Drawing.Point(42, 498)
    Me.lblNeedsAttention.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNeedsAttention.Name = "lblNeedsAttention"
    Me.lblNeedsAttention.Size = New System.Drawing.Size(18, 13)
    Me.lblNeedsAttention.TabIndex = 27
    Me.lblNeedsAttention.Text = "Needs Attention"
    '
    'DtxtProductTypes
    '
    Me.txtProductTypes.Location = New System.Drawing.Point(191, 537)
    Me.txtProductTypes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProductTypes.Name = "txtProductTypes"
    Me.txtProductTypes.Size = New System.Drawing.Size(379, 105)
    Me.txtProductTypes.Multiline = True
    Me.txtProductTypes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtProductTypes.WordWrap = False 
    Me.txtProductTypes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProductTypes.TabIndex = 28
    Me.txtProductTypes.Text = "txtProductTypes"
    '
    'lblProductTypes
    '
    Me.lblProductTypes.AutoSize = True
    Me.lblProductTypes.Location = New System.Drawing.Point(42, 535)
    Me.lblProductTypes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProductTypes.Name = "lblProductTypes"
    Me.lblProductTypes.Size = New System.Drawing.Size(18, 13)
    Me.lblProductTypes.TabIndex = 29
    Me.lblProductTypes.Text = "Product Types"
    '
    'dtpDeliveryDate
    '
    Me.dtpDeliveryDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpDeliveryDate.Location = New System.Drawing.Point(184, 651)
    Me.dtpDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpDeliveryDate.Name = "dtpDeliveryDate"
    Me.dtpDeliveryDate.ShowCheckBox = True
    Me.dtpDeliveryDate.ShowUpDown = True
    Me.dtpDeliveryDate.Size = New System.Drawing.Size(329, 20)
    Me.dtpDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpDeliveryDate.TabIndex = 30
    '
    'CtxtDeliveryDate
    '
    Me.txtDeliveryDate.Location = New System.Drawing.Point(191, 657)
    Me.txtDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryDate.Name = "txtDeliveryDate"
    Me.txtDeliveryDate.Size = New System.Drawing.Size(379, 20)
    Me.txtDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryDate.TabIndex = 31
    Me.txtDeliveryDate.Text = "txtDeliveryDate"
    '
    'lblDeliveryDate
    '
    Me.lblDeliveryDate.AutoSize = True
    Me.lblDeliveryDate.Location = New System.Drawing.Point(42, 660)
    Me.lblDeliveryDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryDate.Name = "lblDeliveryDate"
    Me.lblDeliveryDate.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryDate.TabIndex = 32
    Me.lblDeliveryDate.Text = "Delivery Date"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 737)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 33
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 737)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 34
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 737)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 35
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 725)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 36
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 725)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 37
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlCustomerDebt 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.lblCustomer)
    Me.Controls.Add(Me.cboOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.lblOrderHeader)
    Me.Controls.Add(Me.txtDebtAmount)
    Me.Controls.Add(Me.lblDebtAmount)
    Me.Controls.Add(Me.txtPaidAmount)
    Me.Controls.Add(Me.lblPaidAmount)
    Me.Controls.Add(Me.txtRemainingAmount)
    Me.Controls.Add(Me.lblRemainingAmount)
    Me.Controls.Add(Me.dtpDebtDate)
    Me.Controls.Add(Me.txtDebtDate)
    Me.Controls.Add(Me.lblDebtDate)
    Me.Controls.Add(Me.dtpDueDate)
    Me.Controls.Add(Me.txtDueDate)
    Me.Controls.Add(Me.lblDueDate)
    Me.Controls.Add(Me.cboDebtStatus)
    Me.Controls.Add(Me.txtDebtStatus)
    Me.Controls.Add(Me.txtDebtStatus)
    Me.Controls.Add(Me.lblDebtStatus)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.chkNeedsAttention)
    Me.Controls.Add(Me.lblNeedsAttention)
    Me.Controls.Add(Me.txtProductTypes)
    Me.Controls.Add(Me.lblProductTypes)
    Me.Controls.Add(Me.dtpDeliveryDate)
    Me.Controls.Add(Me.txtDeliveryDate)
    Me.Controls.Add(Me.lblDeliveryDate)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccCustomerDebt"
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
  Friend WithEvents cboOrderHeader As IntelliCombo
  Friend WithEvents txtOrderHeader As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderHeader As System.Windows.Forms.Label
  Friend WithEvents txtDebtAmount As System.Windows.Forms.TextBox
  Friend WithEvents lblDebtAmount As System.Windows.Forms.Label
  Friend WithEvents txtPaidAmount As System.Windows.Forms.TextBox
  Friend WithEvents lblPaidAmount As System.Windows.Forms.Label
  Friend WithEvents txtRemainingAmount As System.Windows.Forms.TextBox
  Friend WithEvents lblRemainingAmount As System.Windows.Forms.Label
  Friend WithEvents dtpDebtDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtDebtDate As System.Windows.Forms.TextBox
  Friend WithEvents lblDebtDate As System.Windows.Forms.Label
  Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtDueDate As System.Windows.Forms.TextBox
  Friend WithEvents lblDueDate As System.Windows.Forms.Label
  Friend WithEvents cboDebtStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtDebtStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblDebtStatus As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents chkNeedsAttention As System.Windows.Forms.CheckBox
  Friend WithEvents lblNeedsAttention As System.Windows.Forms.Label
  Friend WithEvents txtProductTypes As System.Windows.Forms.TextBox
  Friend WithEvents lblProductTypes As System.Windows.Forms.Label
  Friend WithEvents dtpDeliveryDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtDeliveryDate As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryDate As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
