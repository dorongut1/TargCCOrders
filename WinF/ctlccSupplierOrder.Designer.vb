<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccSupplierOrder
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
    Me.cboOrderHeader = New IntelliCombo()
    Me.txtOrderHeader = New System.Windows.Forms.TextBox()
    Me.lblOrderHeader = New System.Windows.Forms.Label()
    Me.txtSupplierEmail = New System.Windows.Forms.TextBox()
    Me.lblSupplierEmail = New System.Windows.Forms.Label()
    Me.txtEmailSubject = New System.Windows.Forms.TextBox()
    Me.lblEmailSubject = New System.Windows.Forms.Label()
    Me.txtEmailBody = New System.Windows.Forms.TextBox()
    Me.lblEmailBody = New System.Windows.Forms.Label()
    Me.cboEmailStatus = New System.Windows.Forms.ComboBox()
    Me.txtEmailStatus = New System.Windows.Forms.TextBox()
    Me.lblEmailStatus = New System.Windows.Forms.Label()
    Me.dtpSentDate = New System.Windows.Forms.DateTimePicker()
    Me.txtSentDate = New System.Windows.Forms.TextBox()
    Me.lblSentDate = New System.Windows.Forms.Label()
    Me.txtTotalCost = New System.Windows.Forms.TextBox()
    Me.lblTotalCost = New System.Windows.Forms.Label()
    Me.cboDeliveryMethod = New System.Windows.Forms.ComboBox()
    Me.txtDeliveryMethod = New System.Windows.Forms.TextBox()
    Me.lblDeliveryMethod = New System.Windows.Forms.Label()
    Me.dtpRequestedDeliveryDate = New System.Windows.Forms.DateTimePicker()
    Me.txtRequestedDeliveryDate = New System.Windows.Forms.TextBox()
    Me.lblRequestedDeliveryDate = New System.Windows.Forms.Label()
    Me.txtRequestedDeliveryDay = New System.Windows.Forms.TextBox()
    Me.lblRequestedDeliveryDay = New System.Windows.Forms.Label()
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
    Me.txtID.Location = New System.Drawing.Point(219, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(351, 25)
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
    'cboOrderHeader
    '
    Me.cboOrderHeader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboOrderHeader.Location = New System.Drawing.Point(212, 51)
    Me.cboOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboOrderHeader.Name = "cboOrderHeader"
    Me.cboOrderHeader.Size = New System.Drawing.Size(301, 21)
    Me.cboOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboOrderHeader.TabIndex = 2
    '
    'AtxtOrderHeader
    '
    Me.txtOrderHeader.Location = New System.Drawing.Point(219, 57)
    Me.txtOrderHeader.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderHeader.Name = "txtOrderHeader"
    Me.txtOrderHeader.Size = New System.Drawing.Size(351, 20)
    Me.txtOrderHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderHeader.TabIndex = 3
    Me.txtOrderHeader.Text = "txtOrderHeader"
    '
    'lblOrderHeader
    '
    Me.lblOrderHeader.AutoSize = True
    Me.lblOrderHeader.Location = New System.Drawing.Point(42, 60)
    Me.lblOrderHeader.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderHeader.Name = "lblOrderHeader"
    Me.lblOrderHeader.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderHeader.TabIndex = 4
    Me.lblOrderHeader.Text = "Order Header"
    '
    'DtxtSupplierEmail
    '
    Me.txtSupplierEmail.Location = New System.Drawing.Point(219, 97)
    Me.txtSupplierEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSupplierEmail.Name = "txtSupplierEmail"
    Me.txtSupplierEmail.Size = New System.Drawing.Size(351, 105)
    Me.txtSupplierEmail.Multiline = True
    Me.txtSupplierEmail.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtSupplierEmail.WordWrap = False 
    Me.txtSupplierEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSupplierEmail.TabIndex = 5
    Me.txtSupplierEmail.Text = "txtSupplierEmail"
    '
    'lblSupplierEmail
    '
    Me.lblSupplierEmail.AutoSize = True
    Me.lblSupplierEmail.Location = New System.Drawing.Point(42, 95)
    Me.lblSupplierEmail.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSupplierEmail.Name = "lblSupplierEmail"
    Me.lblSupplierEmail.Size = New System.Drawing.Size(18, 13)
    Me.lblSupplierEmail.TabIndex = 6
    Me.lblSupplierEmail.Text = "Supplier Email"
    '
    'DtxtEmailSubject
    '
    Me.txtEmailSubject.Location = New System.Drawing.Point(219, 217)
    Me.txtEmailSubject.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEmailSubject.Name = "txtEmailSubject"
    Me.txtEmailSubject.Size = New System.Drawing.Size(351, 105)
    Me.txtEmailSubject.Multiline = True
    Me.txtEmailSubject.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtEmailSubject.WordWrap = False 
    Me.txtEmailSubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmailSubject.TabIndex = 7
    Me.txtEmailSubject.Text = "txtEmailSubject"
    '
    'lblEmailSubject
    '
    Me.lblEmailSubject.AutoSize = True
    Me.lblEmailSubject.Location = New System.Drawing.Point(42, 215)
    Me.lblEmailSubject.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEmailSubject.Name = "lblEmailSubject"
    Me.lblEmailSubject.Size = New System.Drawing.Size(18, 13)
    Me.lblEmailSubject.TabIndex = 8
    Me.lblEmailSubject.Text = "Email Subject"
    '
    'DtxtEmailBody
    '
    Me.txtEmailBody.Location = New System.Drawing.Point(219, 337)
    Me.txtEmailBody.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEmailBody.Name = "txtEmailBody"
    Me.txtEmailBody.Size = New System.Drawing.Size(351, 105)
    Me.txtEmailBody.Multiline = True
    Me.txtEmailBody.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtEmailBody.WordWrap = False 
    Me.txtEmailBody.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmailBody.TabIndex = 9
    Me.txtEmailBody.Text = "txtEmailBody"
    '
    'lblEmailBody
    '
    Me.lblEmailBody.AutoSize = True
    Me.lblEmailBody.Location = New System.Drawing.Point(42, 335)
    Me.lblEmailBody.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEmailBody.Name = "lblEmailBody"
    Me.lblEmailBody.Size = New System.Drawing.Size(18, 13)
    Me.lblEmailBody.TabIndex = 10
    Me.lblEmailBody.Text = "Email Body"
    '
    'cboEmailStatus
    '
    Me.cboEmailStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboEmailStatus.FormattingEnabled = True
    Me.cboEmailStatus.Location = New System.Drawing.Point(212, 451)
    Me.cboEmailStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboEmailStatus.Name = "cboEmailStatus"
    Me.cboEmailStatus.Size = New System.Drawing.Size(301, 21)
    Me.cboEmailStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboEmailStatus.TabIndex = 11
    '
    'BtxtEmailStatus
    '
    Me.txtEmailStatus.Location = New System.Drawing.Point(219, 457)
    Me.txtEmailStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEmailStatus.Name = "txtEmailStatus"
    Me.txtEmailStatus.Size = New System.Drawing.Size(351, 20)
    Me.txtEmailStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmailStatus.TabIndex = 12
    Me.txtEmailStatus.Text = "txtEmailStatus"
    '
    'DtxtEmailStatus
    '
    Me.txtEmailStatus.Location = New System.Drawing.Point(219, 457)
    Me.txtEmailStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEmailStatus.Name = "txtEmailStatus"
    Me.txtEmailStatus.Size = New System.Drawing.Size(351, 25)
    Me.txtEmailStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmailStatus.TabIndex = 13
    Me.txtEmailStatus.Text = "txtEmailStatus"
    '
    'lblEmailStatus
    '
    Me.lblEmailStatus.AutoSize = True
    Me.lblEmailStatus.Location = New System.Drawing.Point(42, 460)
    Me.lblEmailStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEmailStatus.Name = "lblEmailStatus"
    Me.lblEmailStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblEmailStatus.TabIndex = 14
    Me.lblEmailStatus.Text = "Email Status"
    '
    'dtpSentDate
    '
    Me.dtpSentDate.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpSentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpSentDate.Location = New System.Drawing.Point(212, 491)
    Me.dtpSentDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpSentDate.Name = "dtpSentDate"
    Me.dtpSentDate.ShowCheckBox = True
    Me.dtpSentDate.ShowUpDown = True
    Me.dtpSentDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpSentDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpSentDate.TabIndex = 15
    '
    'CtxtSentDate
    '
    Me.txtSentDate.Location = New System.Drawing.Point(219, 497)
    Me.txtSentDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtSentDate.Name = "txtSentDate"
    Me.txtSentDate.Size = New System.Drawing.Size(351, 20)
    Me.txtSentDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSentDate.TabIndex = 16
    Me.txtSentDate.Text = "txtSentDate"
    '
    'lblSentDate
    '
    Me.lblSentDate.AutoSize = True
    Me.lblSentDate.Location = New System.Drawing.Point(42, 500)
    Me.lblSentDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblSentDate.Name = "lblSentDate"
    Me.lblSentDate.Size = New System.Drawing.Size(18, 13)
    Me.lblSentDate.TabIndex = 17
    Me.lblSentDate.Text = "Sent Date"
    '
    'DtxtTotalCost
    '
    Me.txtTotalCost.Location = New System.Drawing.Point(219, 537)
    Me.txtTotalCost.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTotalCost.Name = "txtTotalCost"
    Me.txtTotalCost.Size = New System.Drawing.Size(351, 25)
    Me.txtTotalCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTotalCost.TabIndex = 18
    Me.txtTotalCost.Text = "txtTotalCost"
    '
    'lblTotalCost
    '
    Me.lblTotalCost.AutoSize = True
    Me.lblTotalCost.Location = New System.Drawing.Point(42, 540)
    Me.lblTotalCost.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTotalCost.Name = "lblTotalCost"
    Me.lblTotalCost.Size = New System.Drawing.Size(18, 13)
    Me.lblTotalCost.TabIndex = 19
    Me.lblTotalCost.Text = "Total Cost"
    '
    'cboDeliveryMethod
    '
    Me.cboDeliveryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDeliveryMethod.FormattingEnabled = True
    Me.cboDeliveryMethod.Location = New System.Drawing.Point(212, 571)
    Me.cboDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDeliveryMethod.Name = "cboDeliveryMethod"
    Me.cboDeliveryMethod.Size = New System.Drawing.Size(301, 21)
    Me.cboDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDeliveryMethod.TabIndex = 20
    '
    'BtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(219, 577)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(351, 20)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 21
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'DtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(219, 577)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(351, 25)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 22
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'lblDeliveryMethod
    '
    Me.lblDeliveryMethod.AutoSize = True
    Me.lblDeliveryMethod.Location = New System.Drawing.Point(42, 580)
    Me.lblDeliveryMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryMethod.Name = "lblDeliveryMethod"
    Me.lblDeliveryMethod.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryMethod.TabIndex = 23
    Me.lblDeliveryMethod.Text = "Delivery Method"
    '
    'dtpRequestedDeliveryDate
    '
    Me.dtpRequestedDeliveryDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpRequestedDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpRequestedDeliveryDate.Location = New System.Drawing.Point(212, 611)
    Me.dtpRequestedDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpRequestedDeliveryDate.Name = "dtpRequestedDeliveryDate"
    Me.dtpRequestedDeliveryDate.ShowCheckBox = True
    Me.dtpRequestedDeliveryDate.ShowUpDown = True
    Me.dtpRequestedDeliveryDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpRequestedDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpRequestedDeliveryDate.TabIndex = 24
    '
    'CtxtRequestedDeliveryDate
    '
    Me.txtRequestedDeliveryDate.Location = New System.Drawing.Point(219, 617)
    Me.txtRequestedDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRequestedDeliveryDate.Name = "txtRequestedDeliveryDate"
    Me.txtRequestedDeliveryDate.Size = New System.Drawing.Size(351, 20)
    Me.txtRequestedDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRequestedDeliveryDate.TabIndex = 25
    Me.txtRequestedDeliveryDate.Text = "txtRequestedDeliveryDate"
    '
    'lblRequestedDeliveryDate
    '
    Me.lblRequestedDeliveryDate.AutoSize = True
    Me.lblRequestedDeliveryDate.Location = New System.Drawing.Point(42, 620)
    Me.lblRequestedDeliveryDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRequestedDeliveryDate.Name = "lblRequestedDeliveryDate"
    Me.lblRequestedDeliveryDate.Size = New System.Drawing.Size(18, 13)
    Me.lblRequestedDeliveryDate.TabIndex = 26
    Me.lblRequestedDeliveryDate.Text = "Requested Delivery Date"
    '
    'DtxtRequestedDeliveryDay
    '
    Me.txtRequestedDeliveryDay.Location = New System.Drawing.Point(219, 657)
    Me.txtRequestedDeliveryDay.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtRequestedDeliveryDay.Name = "txtRequestedDeliveryDay"
    Me.txtRequestedDeliveryDay.Size = New System.Drawing.Size(351, 25)
    Me.txtRequestedDeliveryDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRequestedDeliveryDay.TabIndex = 27
    Me.txtRequestedDeliveryDay.Text = "txtRequestedDeliveryDay"
    '
    'lblRequestedDeliveryDay
    '
    Me.lblRequestedDeliveryDay.AutoSize = True
    Me.lblRequestedDeliveryDay.Location = New System.Drawing.Point(42, 660)
    Me.lblRequestedDeliveryDay.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblRequestedDeliveryDay.Name = "lblRequestedDeliveryDay"
    Me.lblRequestedDeliveryDay.Size = New System.Drawing.Size(18, 13)
    Me.lblRequestedDeliveryDay.TabIndex = 28
    Me.lblRequestedDeliveryDay.Text = "Requested Delivery Day"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(219, 697)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(351, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 29
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 695)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 30
    Me.lblNotes.Text = "Notes"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 857)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 31
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 857)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 32
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 857)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 33
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 845)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 34
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 845)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 35
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlSupplierOrder 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.lblOrderHeader)
    Me.Controls.Add(Me.txtSupplierEmail)
    Me.Controls.Add(Me.lblSupplierEmail)
    Me.Controls.Add(Me.txtEmailSubject)
    Me.Controls.Add(Me.lblEmailSubject)
    Me.Controls.Add(Me.txtEmailBody)
    Me.Controls.Add(Me.lblEmailBody)
    Me.Controls.Add(Me.cboEmailStatus)
    Me.Controls.Add(Me.txtEmailStatus)
    Me.Controls.Add(Me.txtEmailStatus)
    Me.Controls.Add(Me.lblEmailStatus)
    Me.Controls.Add(Me.dtpSentDate)
    Me.Controls.Add(Me.txtSentDate)
    Me.Controls.Add(Me.lblSentDate)
    Me.Controls.Add(Me.txtTotalCost)
    Me.Controls.Add(Me.lblTotalCost)
    Me.Controls.Add(Me.cboDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.lblDeliveryMethod)
    Me.Controls.Add(Me.dtpRequestedDeliveryDate)
    Me.Controls.Add(Me.txtRequestedDeliveryDate)
    Me.Controls.Add(Me.lblRequestedDeliveryDate)
    Me.Controls.Add(Me.txtRequestedDeliveryDay)
    Me.Controls.Add(Me.lblRequestedDeliveryDay)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccSupplierOrder"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents cboOrderHeader As IntelliCombo
  Friend WithEvents txtOrderHeader As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderHeader As System.Windows.Forms.Label
  Friend WithEvents txtSupplierEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblSupplierEmail As System.Windows.Forms.Label
  Friend WithEvents txtEmailSubject As System.Windows.Forms.TextBox
  Friend WithEvents lblEmailSubject As System.Windows.Forms.Label
  Friend WithEvents txtEmailBody As System.Windows.Forms.TextBox
  Friend WithEvents lblEmailBody As System.Windows.Forms.Label
  Friend WithEvents cboEmailStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtEmailStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblEmailStatus As System.Windows.Forms.Label
  Friend WithEvents dtpSentDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtSentDate As System.Windows.Forms.TextBox
  Friend WithEvents lblSentDate As System.Windows.Forms.Label
  Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
  Friend WithEvents lblTotalCost As System.Windows.Forms.Label
  Friend WithEvents cboDeliveryMethod As System.Windows.Forms.ComboBox
  Friend WithEvents txtDeliveryMethod As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryMethod As System.Windows.Forms.Label
  Friend WithEvents dtpRequestedDeliveryDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtRequestedDeliveryDate As System.Windows.Forms.TextBox
  Friend WithEvents lblRequestedDeliveryDate As System.Windows.Forms.Label
  Friend WithEvents txtRequestedDeliveryDay As System.Windows.Forms.TextBox
  Friend WithEvents lblRequestedDeliveryDay As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
