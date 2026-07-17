<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccOrderHeader
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
    Me.txtOrderNumber = New System.Windows.Forms.TextBox()
    Me.lblOrderNumber = New System.Windows.Forms.Label()
    Me.cboCustomer = New IntelliCombo()
    Me.txtCustomer = New System.Windows.Forms.TextBox()
    Me.lblCustomer = New System.Windows.Forms.Label()
    Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
    Me.txtOrderDate = New System.Windows.Forms.TextBox()
    Me.lblOrderDate = New System.Windows.Forms.Label()
    Me.txtTotalAmount = New System.Windows.Forms.TextBox()
    Me.lblTotalAmount = New System.Windows.Forms.Label()
    Me.txtVATAmount = New System.Windows.Forms.TextBox()
    Me.lblVATAmount = New System.Windows.Forms.Label()
    Me.txtTotalWithVAT = New System.Windows.Forms.TextBox()
    Me.lblTotalWithVAT = New System.Windows.Forms.Label()
    Me.cboPaymentMethod = New System.Windows.Forms.ComboBox()
    Me.txtPaymentMethod = New System.Windows.Forms.TextBox()
    Me.lblPaymentMethod = New System.Windows.Forms.Label()
    Me.cboPaymentStatus = New System.Windows.Forms.ComboBox()
    Me.txtPaymentStatus = New System.Windows.Forms.TextBox()
    Me.lblPaymentStatus = New System.Windows.Forms.Label()
    Me.dtpPaymentDate = New System.Windows.Forms.DateTimePicker()
    Me.txtPaymentDate = New System.Windows.Forms.TextBox()
    Me.lblPaymentDate = New System.Windows.Forms.Label()
    Me.txtInvoiceNumber = New System.Windows.Forms.TextBox()
    Me.lblInvoiceNumber = New System.Windows.Forms.Label()
    Me.cboDeliveryMethod = New System.Windows.Forms.ComboBox()
    Me.txtDeliveryMethod = New System.Windows.Forms.TextBox()
    Me.lblDeliveryMethod = New System.Windows.Forms.Label()
    Me.dtpDeliveryDate = New System.Windows.Forms.DateTimePicker()
    Me.txtDeliveryDate = New System.Windows.Forms.TextBox()
    Me.lblDeliveryDate = New System.Windows.Forms.Label()
    Me.cboDeliveryDay = New System.Windows.Forms.ComboBox()
    Me.txtDeliveryDay = New System.Windows.Forms.TextBox()
    Me.lblDeliveryDay = New System.Windows.Forms.Label()
    Me.cboOrderStatus = New System.Windows.Forms.ComboBox()
    Me.txtOrderStatus = New System.Windows.Forms.TextBox()
    Me.lblOrderStatus = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.txtNotes2 = New System.Windows.Forms.TextBox()
    Me.lblNotes2 = New System.Windows.Forms.Label()
    Me.txtOrderMonth = New System.Windows.Forms.TextBox()
    Me.lblOrderMonth = New System.Windows.Forms.Label()
    Me.txtQuarter = New System.Windows.Forms.TextBox()
    Me.lblQuarter = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(180, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(390, 25)
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
    'DtxtOrderNumber
    '
    Me.txtOrderNumber.Location = New System.Drawing.Point(180, 57)
    Me.txtOrderNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderNumber.Name = "txtOrderNumber"
    Me.txtOrderNumber.Size = New System.Drawing.Size(390, 25)
    Me.txtOrderNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderNumber.TabIndex = 2
    Me.txtOrderNumber.Text = "txtOrderNumber"
    '
    'lblOrderNumber
    '
    Me.lblOrderNumber.AutoSize = True
    Me.lblOrderNumber.Location = New System.Drawing.Point(42, 60)
    Me.lblOrderNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderNumber.Name = "lblOrderNumber"
    Me.lblOrderNumber.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderNumber.TabIndex = 3
    Me.lblOrderNumber.Text = "Order Number"
    '
    'cboCustomer
    '
    Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCustomer.Location = New System.Drawing.Point(173, 91)
    Me.cboCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCustomer.Name = "cboCustomer"
    Me.cboCustomer.Size = New System.Drawing.Size(340, 21)
    Me.cboCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCustomer.TabIndex = 4
    '
    'AtxtCustomer
    '
    Me.txtCustomer.Location = New System.Drawing.Point(180, 97)
    Me.txtCustomer.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomer.Name = "txtCustomer"
    Me.txtCustomer.Size = New System.Drawing.Size(390, 20)
    Me.txtCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomer.TabIndex = 5
    Me.txtCustomer.Text = "txtCustomer"
    '
    'lblCustomer
    '
    Me.lblCustomer.AutoSize = True
    Me.lblCustomer.Location = New System.Drawing.Point(42, 100)
    Me.lblCustomer.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomer.Name = "lblCustomer"
    Me.lblCustomer.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomer.TabIndex = 6
    Me.lblCustomer.Text = "Customer"
    '
    'dtpOrderDate
    '
    Me.dtpOrderDate.CustomFormat = "dd-MM-yyyy HH:mm:ss"
    Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpOrderDate.Location = New System.Drawing.Point(173, 131)
    Me.dtpOrderDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpOrderDate.Name = "dtpOrderDate"
    Me.dtpOrderDate.ShowCheckBox = True
    Me.dtpOrderDate.ShowUpDown = True
    Me.dtpOrderDate.Size = New System.Drawing.Size(340, 20)
    Me.dtpOrderDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpOrderDate.TabIndex = 7
    '
    'CtxtOrderDate
    '
    Me.txtOrderDate.Location = New System.Drawing.Point(180, 137)
    Me.txtOrderDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderDate.Name = "txtOrderDate"
    Me.txtOrderDate.Size = New System.Drawing.Size(390, 20)
    Me.txtOrderDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderDate.TabIndex = 8
    Me.txtOrderDate.Text = "txtOrderDate"
    '
    'lblOrderDate
    '
    Me.lblOrderDate.AutoSize = True
    Me.lblOrderDate.Location = New System.Drawing.Point(42, 140)
    Me.lblOrderDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderDate.Name = "lblOrderDate"
    Me.lblOrderDate.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderDate.TabIndex = 9
    Me.lblOrderDate.Text = "Order Date"
    '
    'DtxtTotalAmount
    '
    Me.txtTotalAmount.Location = New System.Drawing.Point(180, 177)
    Me.txtTotalAmount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTotalAmount.Name = "txtTotalAmount"
    Me.txtTotalAmount.Size = New System.Drawing.Size(390, 25)
    Me.txtTotalAmount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTotalAmount.TabIndex = 10
    Me.txtTotalAmount.Text = "txtTotalAmount"
    '
    'lblTotalAmount
    '
    Me.lblTotalAmount.AutoSize = True
    Me.lblTotalAmount.Location = New System.Drawing.Point(42, 180)
    Me.lblTotalAmount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTotalAmount.Name = "lblTotalAmount"
    Me.lblTotalAmount.Size = New System.Drawing.Size(18, 13)
    Me.lblTotalAmount.TabIndex = 11
    Me.lblTotalAmount.Text = "Total Amount"
    '
    'DtxtVATAmount
    '
    Me.txtVATAmount.Location = New System.Drawing.Point(180, 217)
    Me.txtVATAmount.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtVATAmount.Name = "txtVATAmount"
    Me.txtVATAmount.Size = New System.Drawing.Size(390, 25)
    Me.txtVATAmount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtVATAmount.TabIndex = 12
    Me.txtVATAmount.Text = "txtVATAmount"
    '
    'lblVATAmount
    '
    Me.lblVATAmount.AutoSize = True
    Me.lblVATAmount.Location = New System.Drawing.Point(42, 220)
    Me.lblVATAmount.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblVATAmount.Name = "lblVATAmount"
    Me.lblVATAmount.Size = New System.Drawing.Size(18, 13)
    Me.lblVATAmount.TabIndex = 13
    Me.lblVATAmount.Text = "VAT Amount"
    '
    'DtxtTotalWithVAT
    '
    Me.txtTotalWithVAT.Location = New System.Drawing.Point(180, 257)
    Me.txtTotalWithVAT.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTotalWithVAT.Name = "txtTotalWithVAT"
    Me.txtTotalWithVAT.Size = New System.Drawing.Size(390, 25)
    Me.txtTotalWithVAT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTotalWithVAT.TabIndex = 14
    Me.txtTotalWithVAT.Text = "txtTotalWithVAT"
    '
    'lblTotalWithVAT
    '
    Me.lblTotalWithVAT.AutoSize = True
    Me.lblTotalWithVAT.Location = New System.Drawing.Point(42, 260)
    Me.lblTotalWithVAT.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTotalWithVAT.Name = "lblTotalWithVAT"
    Me.lblTotalWithVAT.Size = New System.Drawing.Size(18, 13)
    Me.lblTotalWithVAT.TabIndex = 15
    Me.lblTotalWithVAT.Text = "Total With VAT"
    '
    'cboPaymentMethod
    '
    Me.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboPaymentMethod.FormattingEnabled = True
    Me.cboPaymentMethod.Location = New System.Drawing.Point(173, 291)
    Me.cboPaymentMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboPaymentMethod.Name = "cboPaymentMethod"
    Me.cboPaymentMethod.Size = New System.Drawing.Size(340, 21)
    Me.cboPaymentMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboPaymentMethod.TabIndex = 16
    '
    'BtxtPaymentMethod
    '
    Me.txtPaymentMethod.Location = New System.Drawing.Point(180, 297)
    Me.txtPaymentMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentMethod.Name = "txtPaymentMethod"
    Me.txtPaymentMethod.Size = New System.Drawing.Size(390, 20)
    Me.txtPaymentMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentMethod.TabIndex = 17
    Me.txtPaymentMethod.Text = "txtPaymentMethod"
    '
    'DtxtPaymentMethod
    '
    Me.txtPaymentMethod.Location = New System.Drawing.Point(180, 297)
    Me.txtPaymentMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentMethod.Name = "txtPaymentMethod"
    Me.txtPaymentMethod.Size = New System.Drawing.Size(390, 25)
    Me.txtPaymentMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentMethod.TabIndex = 18
    Me.txtPaymentMethod.Text = "txtPaymentMethod"
    '
    'lblPaymentMethod
    '
    Me.lblPaymentMethod.AutoSize = True
    Me.lblPaymentMethod.Location = New System.Drawing.Point(42, 300)
    Me.lblPaymentMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPaymentMethod.Name = "lblPaymentMethod"
    Me.lblPaymentMethod.Size = New System.Drawing.Size(18, 13)
    Me.lblPaymentMethod.TabIndex = 19
    Me.lblPaymentMethod.Text = "Payment Method"
    '
    'cboPaymentStatus
    '
    Me.cboPaymentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboPaymentStatus.FormattingEnabled = True
    Me.cboPaymentStatus.Location = New System.Drawing.Point(173, 331)
    Me.cboPaymentStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboPaymentStatus.Name = "cboPaymentStatus"
    Me.cboPaymentStatus.Size = New System.Drawing.Size(340, 21)
    Me.cboPaymentStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboPaymentStatus.TabIndex = 20
    '
    'BtxtPaymentStatus
    '
    Me.txtPaymentStatus.Location = New System.Drawing.Point(180, 337)
    Me.txtPaymentStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentStatus.Name = "txtPaymentStatus"
    Me.txtPaymentStatus.Size = New System.Drawing.Size(390, 20)
    Me.txtPaymentStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentStatus.TabIndex = 21
    Me.txtPaymentStatus.Text = "txtPaymentStatus"
    '
    'DtxtPaymentStatus
    '
    Me.txtPaymentStatus.Location = New System.Drawing.Point(180, 337)
    Me.txtPaymentStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentStatus.Name = "txtPaymentStatus"
    Me.txtPaymentStatus.Size = New System.Drawing.Size(390, 25)
    Me.txtPaymentStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentStatus.TabIndex = 22
    Me.txtPaymentStatus.Text = "txtPaymentStatus"
    '
    'lblPaymentStatus
    '
    Me.lblPaymentStatus.AutoSize = True
    Me.lblPaymentStatus.Location = New System.Drawing.Point(42, 340)
    Me.lblPaymentStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPaymentStatus.Name = "lblPaymentStatus"
    Me.lblPaymentStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblPaymentStatus.TabIndex = 23
    Me.lblPaymentStatus.Text = "Payment Status"
    '
    'dtpPaymentDate
    '
    Me.dtpPaymentDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpPaymentDate.Location = New System.Drawing.Point(173, 371)
    Me.dtpPaymentDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpPaymentDate.Name = "dtpPaymentDate"
    Me.dtpPaymentDate.ShowCheckBox = True
    Me.dtpPaymentDate.ShowUpDown = True
    Me.dtpPaymentDate.Size = New System.Drawing.Size(340, 20)
    Me.dtpPaymentDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpPaymentDate.TabIndex = 24
    '
    'CtxtPaymentDate
    '
    Me.txtPaymentDate.Location = New System.Drawing.Point(180, 377)
    Me.txtPaymentDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentDate.Name = "txtPaymentDate"
    Me.txtPaymentDate.Size = New System.Drawing.Size(390, 20)
    Me.txtPaymentDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentDate.TabIndex = 25
    Me.txtPaymentDate.Text = "txtPaymentDate"
    '
    'lblPaymentDate
    '
    Me.lblPaymentDate.AutoSize = True
    Me.lblPaymentDate.Location = New System.Drawing.Point(42, 380)
    Me.lblPaymentDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPaymentDate.Name = "lblPaymentDate"
    Me.lblPaymentDate.Size = New System.Drawing.Size(18, 13)
    Me.lblPaymentDate.TabIndex = 26
    Me.lblPaymentDate.Text = "Payment Date"
    '
    'DtxtInvoiceNumber
    '
    Me.txtInvoiceNumber.Location = New System.Drawing.Point(180, 417)
    Me.txtInvoiceNumber.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtInvoiceNumber.Name = "txtInvoiceNumber"
    Me.txtInvoiceNumber.Size = New System.Drawing.Size(390, 25)
    Me.txtInvoiceNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtInvoiceNumber.TabIndex = 27
    Me.txtInvoiceNumber.Text = "txtInvoiceNumber"
    '
    'lblInvoiceNumber
    '
    Me.lblInvoiceNumber.AutoSize = True
    Me.lblInvoiceNumber.Location = New System.Drawing.Point(42, 420)
    Me.lblInvoiceNumber.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
    Me.lblInvoiceNumber.Size = New System.Drawing.Size(18, 13)
    Me.lblInvoiceNumber.TabIndex = 28
    Me.lblInvoiceNumber.Text = "Invoice Number"
    '
    'cboDeliveryMethod
    '
    Me.cboDeliveryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDeliveryMethod.FormattingEnabled = True
    Me.cboDeliveryMethod.Location = New System.Drawing.Point(173, 451)
    Me.cboDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDeliveryMethod.Name = "cboDeliveryMethod"
    Me.cboDeliveryMethod.Size = New System.Drawing.Size(340, 21)
    Me.cboDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDeliveryMethod.TabIndex = 29
    '
    'BtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(180, 457)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(390, 20)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 30
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'DtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(180, 457)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(390, 25)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 31
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'lblDeliveryMethod
    '
    Me.lblDeliveryMethod.AutoSize = True
    Me.lblDeliveryMethod.Location = New System.Drawing.Point(42, 460)
    Me.lblDeliveryMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryMethod.Name = "lblDeliveryMethod"
    Me.lblDeliveryMethod.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryMethod.TabIndex = 32
    Me.lblDeliveryMethod.Text = "Delivery Method"
    '
    'dtpDeliveryDate
    '
    Me.dtpDeliveryDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpDeliveryDate.Location = New System.Drawing.Point(173, 491)
    Me.dtpDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpDeliveryDate.Name = "dtpDeliveryDate"
    Me.dtpDeliveryDate.ShowCheckBox = True
    Me.dtpDeliveryDate.ShowUpDown = True
    Me.dtpDeliveryDate.Size = New System.Drawing.Size(340, 20)
    Me.dtpDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpDeliveryDate.TabIndex = 33
    '
    'CtxtDeliveryDate
    '
    Me.txtDeliveryDate.Location = New System.Drawing.Point(180, 497)
    Me.txtDeliveryDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryDate.Name = "txtDeliveryDate"
    Me.txtDeliveryDate.Size = New System.Drawing.Size(390, 20)
    Me.txtDeliveryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryDate.TabIndex = 34
    Me.txtDeliveryDate.Text = "txtDeliveryDate"
    '
    'lblDeliveryDate
    '
    Me.lblDeliveryDate.AutoSize = True
    Me.lblDeliveryDate.Location = New System.Drawing.Point(42, 500)
    Me.lblDeliveryDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryDate.Name = "lblDeliveryDate"
    Me.lblDeliveryDate.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryDate.TabIndex = 35
    Me.lblDeliveryDate.Text = "Delivery Date"
    '
    'cboDeliveryDay
    '
    Me.cboDeliveryDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDeliveryDay.FormattingEnabled = True
    Me.cboDeliveryDay.Location = New System.Drawing.Point(173, 531)
    Me.cboDeliveryDay.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDeliveryDay.Name = "cboDeliveryDay"
    Me.cboDeliveryDay.Size = New System.Drawing.Size(340, 21)
    Me.cboDeliveryDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDeliveryDay.TabIndex = 36
    '
    'BtxtDeliveryDay
    '
    Me.txtDeliveryDay.Location = New System.Drawing.Point(180, 537)
    Me.txtDeliveryDay.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryDay.Name = "txtDeliveryDay"
    Me.txtDeliveryDay.Size = New System.Drawing.Size(390, 20)
    Me.txtDeliveryDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryDay.TabIndex = 37
    Me.txtDeliveryDay.Text = "txtDeliveryDay"
    '
    'DtxtDeliveryDay
    '
    Me.txtDeliveryDay.Location = New System.Drawing.Point(180, 537)
    Me.txtDeliveryDay.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryDay.Name = "txtDeliveryDay"
    Me.txtDeliveryDay.Size = New System.Drawing.Size(390, 25)
    Me.txtDeliveryDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryDay.TabIndex = 38
    Me.txtDeliveryDay.Text = "txtDeliveryDay"
    '
    'lblDeliveryDay
    '
    Me.lblDeliveryDay.AutoSize = True
    Me.lblDeliveryDay.Location = New System.Drawing.Point(42, 540)
    Me.lblDeliveryDay.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryDay.Name = "lblDeliveryDay"
    Me.lblDeliveryDay.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryDay.TabIndex = 39
    Me.lblDeliveryDay.Text = "Delivery Day"
    '
    'cboOrderStatus
    '
    Me.cboOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboOrderStatus.FormattingEnabled = True
    Me.cboOrderStatus.Location = New System.Drawing.Point(173, 571)
    Me.cboOrderStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboOrderStatus.Name = "cboOrderStatus"
    Me.cboOrderStatus.Size = New System.Drawing.Size(340, 21)
    Me.cboOrderStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboOrderStatus.TabIndex = 40
    '
    'BtxtOrderStatus
    '
    Me.txtOrderStatus.Location = New System.Drawing.Point(180, 577)
    Me.txtOrderStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderStatus.Name = "txtOrderStatus"
    Me.txtOrderStatus.Size = New System.Drawing.Size(390, 20)
    Me.txtOrderStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderStatus.TabIndex = 41
    Me.txtOrderStatus.Text = "txtOrderStatus"
    '
    'DtxtOrderStatus
    '
    Me.txtOrderStatus.Location = New System.Drawing.Point(180, 577)
    Me.txtOrderStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderStatus.Name = "txtOrderStatus"
    Me.txtOrderStatus.Size = New System.Drawing.Size(390, 25)
    Me.txtOrderStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderStatus.TabIndex = 42
    Me.txtOrderStatus.Text = "txtOrderStatus"
    '
    'lblOrderStatus
    '
    Me.lblOrderStatus.AutoSize = True
    Me.lblOrderStatus.Location = New System.Drawing.Point(42, 580)
    Me.lblOrderStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderStatus.Name = "lblOrderStatus"
    Me.lblOrderStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderStatus.TabIndex = 43
    Me.lblOrderStatus.Text = "Order Status"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(180, 617)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(390, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 44
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 615)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 45
    Me.lblNotes.Text = "Notes"
    '
    'DtxtNotes2
    '
    Me.txtNotes2.Location = New System.Drawing.Point(180, 737)
    Me.txtNotes2.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes2.Name = "txtNotes2"
    Me.txtNotes2.Size = New System.Drawing.Size(390, 105)
    Me.txtNotes2.Multiline = True
    Me.txtNotes2.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes2.WordWrap = False 
    Me.txtNotes2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes2.TabIndex = 46
    Me.txtNotes2.Text = "txtNotes2"
    '
    'lblNotes2
    '
    Me.lblNotes2.AutoSize = True
    Me.lblNotes2.Location = New System.Drawing.Point(42, 735)
    Me.lblNotes2.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes2.Name = "lblNotes2"
    Me.lblNotes2.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes2.TabIndex = 47
    Me.lblNotes2.Text = "Notes 2"
    '
    'DtxtOrderMonth
    '
    Me.txtOrderMonth.Location = New System.Drawing.Point(180, 857)
    Me.txtOrderMonth.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderMonth.Name = "txtOrderMonth"
    Me.txtOrderMonth.Size = New System.Drawing.Size(390, 25)
    Me.txtOrderMonth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderMonth.TabIndex = 48
    Me.txtOrderMonth.Text = "txtOrderMonth"
    '
    'lblOrderMonth
    '
    Me.lblOrderMonth.AutoSize = True
    Me.lblOrderMonth.Location = New System.Drawing.Point(42, 860)
    Me.lblOrderMonth.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderMonth.Name = "lblOrderMonth"
    Me.lblOrderMonth.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderMonth.TabIndex = 49
    Me.lblOrderMonth.Text = "Order Month"
    '
    'DtxtQuarter
    '
    Me.txtQuarter.Location = New System.Drawing.Point(180, 897)
    Me.txtQuarter.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtQuarter.Name = "txtQuarter"
    Me.txtQuarter.Size = New System.Drawing.Size(390, 25)
    Me.txtQuarter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtQuarter.TabIndex = 50
    Me.txtQuarter.Text = "txtQuarter"
    '
    'lblQuarter
    '
    Me.lblQuarter.AutoSize = True
    Me.lblQuarter.Location = New System.Drawing.Point(42, 900)
    Me.lblQuarter.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblQuarter.Name = "lblQuarter"
    Me.lblQuarter.Size = New System.Drawing.Size(18, 13)
    Me.lblQuarter.TabIndex = 51
    Me.lblQuarter.Text = "Quarter"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 977)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 52
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 977)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 53
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 977)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 54
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 965)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 55
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 965)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 56
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlOrderHeader 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtOrderNumber)
    Me.Controls.Add(Me.lblOrderNumber)
    Me.Controls.Add(Me.cboCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.txtCustomer)
    Me.Controls.Add(Me.lblCustomer)
    Me.Controls.Add(Me.dtpOrderDate)
    Me.Controls.Add(Me.txtOrderDate)
    Me.Controls.Add(Me.lblOrderDate)
    Me.Controls.Add(Me.txtTotalAmount)
    Me.Controls.Add(Me.lblTotalAmount)
    Me.Controls.Add(Me.txtVATAmount)
    Me.Controls.Add(Me.lblVATAmount)
    Me.Controls.Add(Me.txtTotalWithVAT)
    Me.Controls.Add(Me.lblTotalWithVAT)
    Me.Controls.Add(Me.cboPaymentMethod)
    Me.Controls.Add(Me.txtPaymentMethod)
    Me.Controls.Add(Me.txtPaymentMethod)
    Me.Controls.Add(Me.lblPaymentMethod)
    Me.Controls.Add(Me.cboPaymentStatus)
    Me.Controls.Add(Me.txtPaymentStatus)
    Me.Controls.Add(Me.txtPaymentStatus)
    Me.Controls.Add(Me.lblPaymentStatus)
    Me.Controls.Add(Me.dtpPaymentDate)
    Me.Controls.Add(Me.txtPaymentDate)
    Me.Controls.Add(Me.lblPaymentDate)
    Me.Controls.Add(Me.txtInvoiceNumber)
    Me.Controls.Add(Me.lblInvoiceNumber)
    Me.Controls.Add(Me.cboDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.lblDeliveryMethod)
    Me.Controls.Add(Me.dtpDeliveryDate)
    Me.Controls.Add(Me.txtDeliveryDate)
    Me.Controls.Add(Me.lblDeliveryDate)
    Me.Controls.Add(Me.cboDeliveryDay)
    Me.Controls.Add(Me.txtDeliveryDay)
    Me.Controls.Add(Me.txtDeliveryDay)
    Me.Controls.Add(Me.lblDeliveryDay)
    Me.Controls.Add(Me.cboOrderStatus)
    Me.Controls.Add(Me.txtOrderStatus)
    Me.Controls.Add(Me.txtOrderStatus)
    Me.Controls.Add(Me.lblOrderStatus)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.txtNotes2)
    Me.Controls.Add(Me.lblNotes2)
    Me.Controls.Add(Me.txtOrderMonth)
    Me.Controls.Add(Me.lblOrderMonth)
    Me.Controls.Add(Me.txtQuarter)
    Me.Controls.Add(Me.lblQuarter)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccOrderHeader"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtOrderNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderNumber As System.Windows.Forms.Label
  Friend WithEvents cboCustomer As IntelliCombo
  Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomer As System.Windows.Forms.Label
  Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtOrderDate As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderDate As System.Windows.Forms.Label
  Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
  Friend WithEvents lblTotalAmount As System.Windows.Forms.Label
  Friend WithEvents txtVATAmount As System.Windows.Forms.TextBox
  Friend WithEvents lblVATAmount As System.Windows.Forms.Label
  Friend WithEvents txtTotalWithVAT As System.Windows.Forms.TextBox
  Friend WithEvents lblTotalWithVAT As System.Windows.Forms.Label
  Friend WithEvents cboPaymentMethod As System.Windows.Forms.ComboBox
  Friend WithEvents txtPaymentMethod As System.Windows.Forms.TextBox
  Friend WithEvents lblPaymentMethod As System.Windows.Forms.Label
  Friend WithEvents cboPaymentStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtPaymentStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblPaymentStatus As System.Windows.Forms.Label
  Friend WithEvents dtpPaymentDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtPaymentDate As System.Windows.Forms.TextBox
  Friend WithEvents lblPaymentDate As System.Windows.Forms.Label
  Friend WithEvents txtInvoiceNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblInvoiceNumber As System.Windows.Forms.Label
  Friend WithEvents cboDeliveryMethod As System.Windows.Forms.ComboBox
  Friend WithEvents txtDeliveryMethod As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryMethod As System.Windows.Forms.Label
  Friend WithEvents dtpDeliveryDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtDeliveryDate As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryDate As System.Windows.Forms.Label
  Friend WithEvents cboDeliveryDay As System.Windows.Forms.ComboBox
  Friend WithEvents txtDeliveryDay As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryDay As System.Windows.Forms.Label
  Friend WithEvents cboOrderStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtOrderStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderStatus As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents txtNotes2 As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes2 As System.Windows.Forms.Label
  Friend WithEvents txtOrderMonth As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderMonth As System.Windows.Forms.Label
  Friend WithEvents txtQuarter As System.Windows.Forms.TextBox
  Friend WithEvents lblQuarter As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
