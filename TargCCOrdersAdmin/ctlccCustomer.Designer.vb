<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccCustomer
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
    Me.txtCustomerCode = New System.Windows.Forms.TextBox()
    Me.lblCustomerCode = New System.Windows.Forms.Label()
    Me.txtCustomerName = New System.Windows.Forms.TextBox()
    Me.lblCustomerName = New System.Windows.Forms.Label()
    Me.txtPhone = New System.Windows.Forms.TextBox()
    Me.lblPhone = New System.Windows.Forms.Label()
    Me.txtEmail = New System.Windows.Forms.TextBox()
    Me.lblEmail = New System.Windows.Forms.Label()
    Me.txtAddress = New System.Windows.Forms.TextBox()
    Me.lblAddress = New System.Windows.Forms.Label()
    Me.txtCity = New System.Windows.Forms.TextBox()
    Me.lblCity = New System.Windows.Forms.Label()
    Me.txtTaxID = New System.Windows.Forms.TextBox()
    Me.lblTaxID = New System.Windows.Forms.Label()
    Me.cboCustomerType = New System.Windows.Forms.ComboBox()
    Me.txtCustomerType = New System.Windows.Forms.TextBox()
    Me.lblCustomerType = New System.Windows.Forms.Label()
    Me.txtPaymentTermsDays = New System.Windows.Forms.TextBox()
    Me.lblPaymentTermsDays = New System.Windows.Forms.Label()
    Me.txtNotes = New System.Windows.Forms.TextBox()
    Me.lblNotes = New System.Windows.Forms.Label()
    Me.chkIsActive = New System.Windows.Forms.CheckBox()
    Me.lblIsActive = New System.Windows.Forms.Label()
    Me.txtLocation = New System.Windows.Forms.TextBox()
    Me.lblLocation = New System.Windows.Forms.Label()
    Me.txtAccountantEmail = New System.Windows.Forms.TextBox()
    Me.lblAccountantEmail = New System.Windows.Forms.Label()
    Me.cboAccountantMethod = New System.Windows.Forms.ComboBox()
    Me.txtAccountantMethod = New System.Windows.Forms.TextBox()
    Me.lblAccountantMethod = New System.Windows.Forms.Label()
    Me.txtInvoiceName = New System.Windows.Forms.TextBox()
    Me.lblInvoiceName = New System.Windows.Forms.Label()
    Me.txtProfitabilityCode = New System.Windows.Forms.TextBox()
    Me.lblProfitabilityCode = New System.Windows.Forms.Label()
    Me.txtCustomerIdentifier = New System.Windows.Forms.TextBox()
    Me.lblCustomerIdentifier = New System.Windows.Forms.Label()
    Me.btnEdit = New System.Windows.Forms.Button()
    Me.btnAdd = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnUpdate = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'DtxtID
    '
    Me.txtID.Location = New System.Drawing.Point(197, 17)
    Me.txtID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtID.Name = "txtID"
    Me.txtID.Size = New System.Drawing.Size(373, 25)
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
    'DtxtCustomerCode
    '
    Me.txtCustomerCode.Location = New System.Drawing.Point(197, 57)
    Me.txtCustomerCode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerCode.Name = "txtCustomerCode"
    Me.txtCustomerCode.Size = New System.Drawing.Size(373, 25)
    Me.txtCustomerCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerCode.TabIndex = 2
    Me.txtCustomerCode.Text = "txtCustomerCode"
    '
    'lblCustomerCode
    '
    Me.lblCustomerCode.AutoSize = True
    Me.lblCustomerCode.Location = New System.Drawing.Point(42, 60)
    Me.lblCustomerCode.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerCode.Name = "lblCustomerCode"
    Me.lblCustomerCode.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerCode.TabIndex = 3
    Me.lblCustomerCode.Text = "Customer Code"
    '
    'DtxtCustomerName
    '
    Me.txtCustomerName.Location = New System.Drawing.Point(197, 97)
    Me.txtCustomerName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerName.Name = "txtCustomerName"
    Me.txtCustomerName.Size = New System.Drawing.Size(373, 105)
    Me.txtCustomerName.Multiline = True
    Me.txtCustomerName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtCustomerName.WordWrap = False 
    Me.txtCustomerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerName.TabIndex = 4
    Me.txtCustomerName.Text = "txtCustomerName"
    '
    'lblCustomerName
    '
    Me.lblCustomerName.AutoSize = True
    Me.lblCustomerName.Location = New System.Drawing.Point(42, 95)
    Me.lblCustomerName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerName.Name = "lblCustomerName"
    Me.lblCustomerName.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerName.TabIndex = 5
    Me.lblCustomerName.Text = "Customer Name"
    '
    'DtxtPhone
    '
    Me.txtPhone.Location = New System.Drawing.Point(197, 217)
    Me.txtPhone.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPhone.Name = "txtPhone"
    Me.txtPhone.Size = New System.Drawing.Size(373, 25)
    Me.txtPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPhone.TabIndex = 6
    Me.txtPhone.Text = "txtPhone"
    '
    'lblPhone
    '
    Me.lblPhone.AutoSize = True
    Me.lblPhone.Location = New System.Drawing.Point(42, 220)
    Me.lblPhone.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPhone.Name = "lblPhone"
    Me.lblPhone.Size = New System.Drawing.Size(18, 13)
    Me.lblPhone.TabIndex = 7
    Me.lblPhone.Text = "Phone"
    '
    'DtxtEmail
    '
    Me.txtEmail.Location = New System.Drawing.Point(197, 257)
    Me.txtEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.Size = New System.Drawing.Size(373, 105)
    Me.txtEmail.Multiline = True
    Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtEmail.WordWrap = False 
    Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmail.TabIndex = 8
    Me.txtEmail.Text = "txtEmail"
    '
    'lblEmail
    '
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Location = New System.Drawing.Point(42, 255)
    Me.lblEmail.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(18, 13)
    Me.lblEmail.TabIndex = 9
    Me.lblEmail.Text = "Email"
    '
    'DtxtAddress
    '
    Me.txtAddress.Location = New System.Drawing.Point(197, 377)
    Me.txtAddress.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAddress.Name = "txtAddress"
    Me.txtAddress.Size = New System.Drawing.Size(373, 105)
    Me.txtAddress.Multiline = True
    Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtAddress.WordWrap = False 
    Me.txtAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAddress.TabIndex = 10
    Me.txtAddress.Text = "txtAddress"
    '
    'lblAddress
    '
    Me.lblAddress.AutoSize = True
    Me.lblAddress.Location = New System.Drawing.Point(42, 375)
    Me.lblAddress.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAddress.Name = "lblAddress"
    Me.lblAddress.Size = New System.Drawing.Size(18, 13)
    Me.lblAddress.TabIndex = 11
    Me.lblAddress.Text = "Address"
    '
    'DtxtCity
    '
    Me.txtCity.Location = New System.Drawing.Point(197, 497)
    Me.txtCity.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCity.Name = "txtCity"
    Me.txtCity.Size = New System.Drawing.Size(373, 105)
    Me.txtCity.Multiline = True
    Me.txtCity.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtCity.WordWrap = False 
    Me.txtCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCity.TabIndex = 12
    Me.txtCity.Text = "txtCity"
    '
    'lblCity
    '
    Me.lblCity.AutoSize = True
    Me.lblCity.Location = New System.Drawing.Point(42, 495)
    Me.lblCity.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCity.Name = "lblCity"
    Me.lblCity.Size = New System.Drawing.Size(18, 13)
    Me.lblCity.TabIndex = 13
    Me.lblCity.Text = "City"
    '
    'DtxtTaxID
    '
    Me.txtTaxID.Location = New System.Drawing.Point(197, 617)
    Me.txtTaxID.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtTaxID.Name = "txtTaxID"
    Me.txtTaxID.Size = New System.Drawing.Size(373, 25)
    Me.txtTaxID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTaxID.TabIndex = 14
    Me.txtTaxID.Text = "txtTaxID"
    '
    'lblTaxID
    '
    Me.lblTaxID.AutoSize = True
    Me.lblTaxID.Location = New System.Drawing.Point(42, 620)
    Me.lblTaxID.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblTaxID.Name = "lblTaxID"
    Me.lblTaxID.Size = New System.Drawing.Size(18, 13)
    Me.lblTaxID.TabIndex = 15
    Me.lblTaxID.Text = "Tax ID"
    '
    'cboCustomerType
    '
    Me.cboCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboCustomerType.FormattingEnabled = True
    Me.cboCustomerType.Location = New System.Drawing.Point(190, 651)
    Me.cboCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboCustomerType.Name = "cboCustomerType"
    Me.cboCustomerType.Size = New System.Drawing.Size(323, 21)
    Me.cboCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboCustomerType.TabIndex = 16
    '
    'BtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(197, 657)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(373, 20)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 17
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'DtxtCustomerType
    '
    Me.txtCustomerType.Location = New System.Drawing.Point(197, 657)
    Me.txtCustomerType.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerType.Name = "txtCustomerType"
    Me.txtCustomerType.Size = New System.Drawing.Size(373, 25)
    Me.txtCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerType.TabIndex = 18
    Me.txtCustomerType.Text = "txtCustomerType"
    '
    'lblCustomerType
    '
    Me.lblCustomerType.AutoSize = True
    Me.lblCustomerType.Location = New System.Drawing.Point(42, 660)
    Me.lblCustomerType.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerType.Name = "lblCustomerType"
    Me.lblCustomerType.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerType.TabIndex = 19
    Me.lblCustomerType.Text = "Customer Type"
    '
    'DtxtPaymentTermsDays
    '
    Me.txtPaymentTermsDays.Location = New System.Drawing.Point(197, 697)
    Me.txtPaymentTermsDays.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtPaymentTermsDays.Name = "txtPaymentTermsDays"
    Me.txtPaymentTermsDays.Size = New System.Drawing.Size(373, 25)
    Me.txtPaymentTermsDays.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPaymentTermsDays.TabIndex = 20
    Me.txtPaymentTermsDays.Text = "txtPaymentTermsDays"
    '
    'lblPaymentTermsDays
    '
    Me.lblPaymentTermsDays.AutoSize = True
    Me.lblPaymentTermsDays.Location = New System.Drawing.Point(42, 700)
    Me.lblPaymentTermsDays.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblPaymentTermsDays.Name = "lblPaymentTermsDays"
    Me.lblPaymentTermsDays.Size = New System.Drawing.Size(18, 13)
    Me.lblPaymentTermsDays.TabIndex = 21
    Me.lblPaymentTermsDays.Text = "Payment Terms Days"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(197, 737)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(373, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 22
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 735)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 23
    Me.lblNotes.Text = "Notes"
    '
    'chkIsActive
    '
    Me.chkIsActive.AutoSize = True
    Me.chkIsActive.Location = New System.Drawing.Point(197, 863)
    Me.chkIsActive.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.chkIsActive.Name = "chkIsActive"
    Me.chkIsActive.Size = New System.Drawing.Size(15, 14)
    Me.chkIsActive.TabIndex = 24
    Me.chkIsActive.UseVisualStyleBackColor = True
    '
    'lblIsActive
    '
    Me.lblIsActive.AutoSize = True
    Me.lblIsActive.Location = New System.Drawing.Point(42, 858)
    Me.lblIsActive.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblIsActive.Name = "lblIsActive"
    Me.lblIsActive.Size = New System.Drawing.Size(18, 13)
    Me.lblIsActive.TabIndex = 25
    Me.lblIsActive.Text = "Is Active"
    '
    'DtxtLocation
    '
    Me.txtLocation.Location = New System.Drawing.Point(197, 897)
    Me.txtLocation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLocation.Name = "txtLocation"
    Me.txtLocation.Size = New System.Drawing.Size(373, 105)
    Me.txtLocation.Multiline = True
    Me.txtLocation.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtLocation.WordWrap = False 
    Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocation.TabIndex = 26
    Me.txtLocation.Text = "txtLocation"
    '
    'lblLocation
    '
    Me.lblLocation.AutoSize = True
    Me.lblLocation.Location = New System.Drawing.Point(42, 895)
    Me.lblLocation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLocation.Name = "lblLocation"
    Me.lblLocation.Size = New System.Drawing.Size(18, 13)
    Me.lblLocation.TabIndex = 27
    Me.lblLocation.Text = "Location"
    '
    'DtxtAccountantEmail
    '
    Me.txtAccountantEmail.Location = New System.Drawing.Point(197, 1017)
    Me.txtAccountantEmail.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAccountantEmail.Name = "txtAccountantEmail"
    Me.txtAccountantEmail.Size = New System.Drawing.Size(373, 105)
    Me.txtAccountantEmail.Multiline = True
    Me.txtAccountantEmail.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtAccountantEmail.WordWrap = False 
    Me.txtAccountantEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAccountantEmail.TabIndex = 28
    Me.txtAccountantEmail.Text = "txtAccountantEmail"
    '
    'lblAccountantEmail
    '
    Me.lblAccountantEmail.AutoSize = True
    Me.lblAccountantEmail.Location = New System.Drawing.Point(42, 1015)
    Me.lblAccountantEmail.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAccountantEmail.Name = "lblAccountantEmail"
    Me.lblAccountantEmail.Size = New System.Drawing.Size(18, 13)
    Me.lblAccountantEmail.TabIndex = 29
    Me.lblAccountantEmail.Text = "Accountant Email"
    '
    'cboAccountantMethod
    '
    Me.cboAccountantMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboAccountantMethod.FormattingEnabled = True
    Me.cboAccountantMethod.Location = New System.Drawing.Point(190, 1131)
    Me.cboAccountantMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboAccountantMethod.Name = "cboAccountantMethod"
    Me.cboAccountantMethod.Size = New System.Drawing.Size(323, 21)
    Me.cboAccountantMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboAccountantMethod.TabIndex = 30
    '
    'BtxtAccountantMethod
    '
    Me.txtAccountantMethod.Location = New System.Drawing.Point(197, 1137)
    Me.txtAccountantMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAccountantMethod.Name = "txtAccountantMethod"
    Me.txtAccountantMethod.Size = New System.Drawing.Size(373, 20)
    Me.txtAccountantMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAccountantMethod.TabIndex = 31
    Me.txtAccountantMethod.Text = "txtAccountantMethod"
    '
    'DtxtAccountantMethod
    '
    Me.txtAccountantMethod.Location = New System.Drawing.Point(197, 1137)
    Me.txtAccountantMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtAccountantMethod.Name = "txtAccountantMethod"
    Me.txtAccountantMethod.Size = New System.Drawing.Size(373, 25)
    Me.txtAccountantMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAccountantMethod.TabIndex = 32
    Me.txtAccountantMethod.Text = "txtAccountantMethod"
    '
    'lblAccountantMethod
    '
    Me.lblAccountantMethod.AutoSize = True
    Me.lblAccountantMethod.Location = New System.Drawing.Point(42, 1140)
    Me.lblAccountantMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblAccountantMethod.Name = "lblAccountantMethod"
    Me.lblAccountantMethod.Size = New System.Drawing.Size(18, 13)
    Me.lblAccountantMethod.TabIndex = 33
    Me.lblAccountantMethod.Text = "Accountant Method"
    '
    'DtxtInvoiceName
    '
    Me.txtInvoiceName.Location = New System.Drawing.Point(197, 1177)
    Me.txtInvoiceName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtInvoiceName.Name = "txtInvoiceName"
    Me.txtInvoiceName.Size = New System.Drawing.Size(373, 105)
    Me.txtInvoiceName.Multiline = True
    Me.txtInvoiceName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtInvoiceName.WordWrap = False 
    Me.txtInvoiceName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtInvoiceName.TabIndex = 34
    Me.txtInvoiceName.Text = "txtInvoiceName"
    '
    'lblInvoiceName
    '
    Me.lblInvoiceName.AutoSize = True
    Me.lblInvoiceName.Location = New System.Drawing.Point(42, 1175)
    Me.lblInvoiceName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblInvoiceName.Name = "lblInvoiceName"
    Me.lblInvoiceName.Size = New System.Drawing.Size(18, 13)
    Me.lblInvoiceName.TabIndex = 35
    Me.lblInvoiceName.Text = "Invoice Name"
    '
    'DtxtProfitabilityCode
    '
    Me.txtProfitabilityCode.Location = New System.Drawing.Point(197, 1297)
    Me.txtProfitabilityCode.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProfitabilityCode.Name = "txtProfitabilityCode"
    Me.txtProfitabilityCode.Size = New System.Drawing.Size(373, 25)
    Me.txtProfitabilityCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProfitabilityCode.TabIndex = 36
    Me.txtProfitabilityCode.Text = "txtProfitabilityCode"
    '
    'lblProfitabilityCode
    '
    Me.lblProfitabilityCode.AutoSize = True
    Me.lblProfitabilityCode.Location = New System.Drawing.Point(42, 1300)
    Me.lblProfitabilityCode.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProfitabilityCode.Name = "lblProfitabilityCode"
    Me.lblProfitabilityCode.Size = New System.Drawing.Size(18, 13)
    Me.lblProfitabilityCode.TabIndex = 37
    Me.lblProfitabilityCode.Text = "Profitability Code"
    '
    'DtxtCustomerIdentifier
    '
    Me.txtCustomerIdentifier.Location = New System.Drawing.Point(197, 1337)
    Me.txtCustomerIdentifier.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtCustomerIdentifier.Name = "txtCustomerIdentifier"
    Me.txtCustomerIdentifier.Size = New System.Drawing.Size(373, 25)
    Me.txtCustomerIdentifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtCustomerIdentifier.TabIndex = 38
    Me.txtCustomerIdentifier.Text = "txtCustomerIdentifier"
    '
    'lblCustomerIdentifier
    '
    Me.lblCustomerIdentifier.AutoSize = True
    Me.lblCustomerIdentifier.Location = New System.Drawing.Point(42, 1340)
    Me.lblCustomerIdentifier.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblCustomerIdentifier.Name = "lblCustomerIdentifier"
    Me.lblCustomerIdentifier.Size = New System.Drawing.Size(18, 13)
    Me.lblCustomerIdentifier.TabIndex = 39
    Me.lblCustomerIdentifier.Text = "Customer Identifier"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 1417)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 40
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 1417)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 41
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 1417)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 42
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 1405)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 43
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 1405)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 44
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlCustomer 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.txtCustomerCode)
    Me.Controls.Add(Me.lblCustomerCode)
    Me.Controls.Add(Me.txtCustomerName)
    Me.Controls.Add(Me.lblCustomerName)
    Me.Controls.Add(Me.txtPhone)
    Me.Controls.Add(Me.lblPhone)
    Me.Controls.Add(Me.txtEmail)
    Me.Controls.Add(Me.lblEmail)
    Me.Controls.Add(Me.txtAddress)
    Me.Controls.Add(Me.lblAddress)
    Me.Controls.Add(Me.txtCity)
    Me.Controls.Add(Me.lblCity)
    Me.Controls.Add(Me.txtTaxID)
    Me.Controls.Add(Me.lblTaxID)
    Me.Controls.Add(Me.cboCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.txtCustomerType)
    Me.Controls.Add(Me.lblCustomerType)
    Me.Controls.Add(Me.txtPaymentTermsDays)
    Me.Controls.Add(Me.lblPaymentTermsDays)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.chkIsActive)
    Me.Controls.Add(Me.lblIsActive)
    Me.Controls.Add(Me.txtLocation)
    Me.Controls.Add(Me.lblLocation)
    Me.Controls.Add(Me.txtAccountantEmail)
    Me.Controls.Add(Me.lblAccountantEmail)
    Me.Controls.Add(Me.cboAccountantMethod)
    Me.Controls.Add(Me.txtAccountantMethod)
    Me.Controls.Add(Me.txtAccountantMethod)
    Me.Controls.Add(Me.lblAccountantMethod)
    Me.Controls.Add(Me.txtInvoiceName)
    Me.Controls.Add(Me.lblInvoiceName)
    Me.Controls.Add(Me.txtProfitabilityCode)
    Me.Controls.Add(Me.lblProfitabilityCode)
    Me.Controls.Add(Me.txtCustomerIdentifier)
    Me.Controls.Add(Me.lblCustomerIdentifier)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccCustomer"
    Me.BackColor = System.Drawing.Color.Wheat
    Me.Size = New System.Drawing.Size(650, 622)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtID As System.Windows.Forms.TextBox
  Friend WithEvents lblID As System.Windows.Forms.Label
  Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerCode As System.Windows.Forms.Label
  Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerName As System.Windows.Forms.Label
  Friend WithEvents txtPhone As System.Windows.Forms.TextBox
  Friend WithEvents lblPhone As System.Windows.Forms.Label
  Friend WithEvents txtEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblEmail As System.Windows.Forms.Label
  Friend WithEvents txtAddress As System.Windows.Forms.TextBox
  Friend WithEvents lblAddress As System.Windows.Forms.Label
  Friend WithEvents txtCity As System.Windows.Forms.TextBox
  Friend WithEvents lblCity As System.Windows.Forms.Label
  Friend WithEvents txtTaxID As System.Windows.Forms.TextBox
  Friend WithEvents lblTaxID As System.Windows.Forms.Label
  Friend WithEvents cboCustomerType As System.Windows.Forms.ComboBox
  Friend WithEvents txtCustomerType As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerType As System.Windows.Forms.Label
  Friend WithEvents txtPaymentTermsDays As System.Windows.Forms.TextBox
  Friend WithEvents lblPaymentTermsDays As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents chkIsActive As System.Windows.Forms.CheckBox
  Friend WithEvents lblIsActive As System.Windows.Forms.Label
  Friend WithEvents txtLocation As System.Windows.Forms.TextBox
  Friend WithEvents lblLocation As System.Windows.Forms.Label
  Friend WithEvents txtAccountantEmail As System.Windows.Forms.TextBox
  Friend WithEvents lblAccountantEmail As System.Windows.Forms.Label
  Friend WithEvents cboAccountantMethod As System.Windows.Forms.ComboBox
  Friend WithEvents txtAccountantMethod As System.Windows.Forms.TextBox
  Friend WithEvents lblAccountantMethod As System.Windows.Forms.Label
  Friend WithEvents txtInvoiceName As System.Windows.Forms.TextBox
  Friend WithEvents lblInvoiceName As System.Windows.Forms.Label
  Friend WithEvents txtProfitabilityCode As System.Windows.Forms.TextBox
  Friend WithEvents lblProfitabilityCode As System.Windows.Forms.Label
  Friend WithEvents txtCustomerIdentifier As System.Windows.Forms.TextBox
  Friend WithEvents lblCustomerIdentifier As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
