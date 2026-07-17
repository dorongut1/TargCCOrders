<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccDelivery
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
    Me.txtDeliveryAddress = New System.Windows.Forms.TextBox()
    Me.lblDeliveryAddress = New System.Windows.Forms.Label()
    Me.txtContactPhone = New System.Windows.Forms.TextBox()
    Me.lblContactPhone = New System.Windows.Forms.Label()
    Me.txtContactName = New System.Windows.Forms.TextBox()
    Me.lblContactName = New System.Windows.Forms.Label()
    Me.cboDeliveryMethod = New System.Windows.Forms.ComboBox()
    Me.txtDeliveryMethod = New System.Windows.Forms.TextBox()
    Me.lblDeliveryMethod = New System.Windows.Forms.Label()
    Me.dtpOrderedDate = New System.Windows.Forms.DateTimePicker()
    Me.txtOrderedDate = New System.Windows.Forms.TextBox()
    Me.lblOrderedDate = New System.Windows.Forms.Label()
    Me.dtpReceivedDate = New System.Windows.Forms.DateTimePicker()
    Me.txtReceivedDate = New System.Windows.Forms.TextBox()
    Me.lblReceivedDate = New System.Windows.Forms.Label()
    Me.dtpArrivalToHubDate = New System.Windows.Forms.DateTimePicker()
    Me.txtArrivalToHubDate = New System.Windows.Forms.TextBox()
    Me.lblArrivalToHubDate = New System.Windows.Forms.Label()
    Me.dtpArrivalToCustomerDate = New System.Windows.Forms.DateTimePicker()
    Me.txtArrivalToCustomerDate = New System.Windows.Forms.TextBox()
    Me.lblArrivalToCustomerDate = New System.Windows.Forms.Label()
    Me.cboDeliveryStatus = New System.Windows.Forms.ComboBox()
    Me.txtDeliveryStatus = New System.Windows.Forms.TextBox()
    Me.lblDeliveryStatus = New System.Windows.Forms.Label()
    Me.txtLocation = New System.Windows.Forms.TextBox()
    Me.lblLocation = New System.Windows.Forms.Label()
    Me.txtProductsSummary = New System.Windows.Forms.TextBox()
    Me.lblProductsSummary = New System.Windows.Forms.Label()
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
    'DtxtDeliveryAddress
    '
    Me.txtDeliveryAddress.Location = New System.Drawing.Point(219, 97)
    Me.txtDeliveryAddress.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryAddress.Name = "txtDeliveryAddress"
    Me.txtDeliveryAddress.Size = New System.Drawing.Size(351, 105)
    Me.txtDeliveryAddress.Multiline = True
    Me.txtDeliveryAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtDeliveryAddress.WordWrap = False 
    Me.txtDeliveryAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryAddress.TabIndex = 5
    Me.txtDeliveryAddress.Text = "txtDeliveryAddress"
    '
    'lblDeliveryAddress
    '
    Me.lblDeliveryAddress.AutoSize = True
    Me.lblDeliveryAddress.Location = New System.Drawing.Point(42, 95)
    Me.lblDeliveryAddress.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryAddress.Name = "lblDeliveryAddress"
    Me.lblDeliveryAddress.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryAddress.TabIndex = 6
    Me.lblDeliveryAddress.Text = "Delivery Address"
    '
    'DtxtContactPhone
    '
    Me.txtContactPhone.Location = New System.Drawing.Point(219, 217)
    Me.txtContactPhone.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtContactPhone.Name = "txtContactPhone"
    Me.txtContactPhone.Size = New System.Drawing.Size(351, 25)
    Me.txtContactPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtContactPhone.TabIndex = 7
    Me.txtContactPhone.Text = "txtContactPhone"
    '
    'lblContactPhone
    '
    Me.lblContactPhone.AutoSize = True
    Me.lblContactPhone.Location = New System.Drawing.Point(42, 220)
    Me.lblContactPhone.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblContactPhone.Name = "lblContactPhone"
    Me.lblContactPhone.Size = New System.Drawing.Size(18, 13)
    Me.lblContactPhone.TabIndex = 8
    Me.lblContactPhone.Text = "Contact Phone"
    '
    'DtxtContactName
    '
    Me.txtContactName.Location = New System.Drawing.Point(219, 257)
    Me.txtContactName.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtContactName.Name = "txtContactName"
    Me.txtContactName.Size = New System.Drawing.Size(351, 105)
    Me.txtContactName.Multiline = True
    Me.txtContactName.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtContactName.WordWrap = False 
    Me.txtContactName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtContactName.TabIndex = 9
    Me.txtContactName.Text = "txtContactName"
    '
    'lblContactName
    '
    Me.lblContactName.AutoSize = True
    Me.lblContactName.Location = New System.Drawing.Point(42, 255)
    Me.lblContactName.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblContactName.Name = "lblContactName"
    Me.lblContactName.Size = New System.Drawing.Size(18, 13)
    Me.lblContactName.TabIndex = 10
    Me.lblContactName.Text = "Contact Name"
    '
    'cboDeliveryMethod
    '
    Me.cboDeliveryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDeliveryMethod.FormattingEnabled = True
    Me.cboDeliveryMethod.Location = New System.Drawing.Point(212, 371)
    Me.cboDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDeliveryMethod.Name = "cboDeliveryMethod"
    Me.cboDeliveryMethod.Size = New System.Drawing.Size(301, 21)
    Me.cboDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDeliveryMethod.TabIndex = 11
    '
    'BtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(219, 377)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(351, 20)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 12
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'DtxtDeliveryMethod
    '
    Me.txtDeliveryMethod.Location = New System.Drawing.Point(219, 377)
    Me.txtDeliveryMethod.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryMethod.Name = "txtDeliveryMethod"
    Me.txtDeliveryMethod.Size = New System.Drawing.Size(351, 25)
    Me.txtDeliveryMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryMethod.TabIndex = 13
    Me.txtDeliveryMethod.Text = "txtDeliveryMethod"
    '
    'lblDeliveryMethod
    '
    Me.lblDeliveryMethod.AutoSize = True
    Me.lblDeliveryMethod.Location = New System.Drawing.Point(42, 380)
    Me.lblDeliveryMethod.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryMethod.Name = "lblDeliveryMethod"
    Me.lblDeliveryMethod.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryMethod.TabIndex = 14
    Me.lblDeliveryMethod.Text = "Delivery Method"
    '
    'dtpOrderedDate
    '
    Me.dtpOrderedDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpOrderedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpOrderedDate.Location = New System.Drawing.Point(212, 411)
    Me.dtpOrderedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpOrderedDate.Name = "dtpOrderedDate"
    Me.dtpOrderedDate.ShowCheckBox = True
    Me.dtpOrderedDate.ShowUpDown = True
    Me.dtpOrderedDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpOrderedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpOrderedDate.TabIndex = 15
    '
    'CtxtOrderedDate
    '
    Me.txtOrderedDate.Location = New System.Drawing.Point(219, 417)
    Me.txtOrderedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtOrderedDate.Name = "txtOrderedDate"
    Me.txtOrderedDate.Size = New System.Drawing.Size(351, 20)
    Me.txtOrderedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtOrderedDate.TabIndex = 16
    Me.txtOrderedDate.Text = "txtOrderedDate"
    '
    'lblOrderedDate
    '
    Me.lblOrderedDate.AutoSize = True
    Me.lblOrderedDate.Location = New System.Drawing.Point(42, 420)
    Me.lblOrderedDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblOrderedDate.Name = "lblOrderedDate"
    Me.lblOrderedDate.Size = New System.Drawing.Size(18, 13)
    Me.lblOrderedDate.TabIndex = 17
    Me.lblOrderedDate.Text = "Ordered Date"
    '
    'dtpReceivedDate
    '
    Me.dtpReceivedDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpReceivedDate.Location = New System.Drawing.Point(212, 451)
    Me.dtpReceivedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpReceivedDate.Name = "dtpReceivedDate"
    Me.dtpReceivedDate.ShowCheckBox = True
    Me.dtpReceivedDate.ShowUpDown = True
    Me.dtpReceivedDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpReceivedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpReceivedDate.TabIndex = 18
    '
    'CtxtReceivedDate
    '
    Me.txtReceivedDate.Location = New System.Drawing.Point(219, 457)
    Me.txtReceivedDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtReceivedDate.Name = "txtReceivedDate"
    Me.txtReceivedDate.Size = New System.Drawing.Size(351, 20)
    Me.txtReceivedDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtReceivedDate.TabIndex = 19
    Me.txtReceivedDate.Text = "txtReceivedDate"
    '
    'lblReceivedDate
    '
    Me.lblReceivedDate.AutoSize = True
    Me.lblReceivedDate.Location = New System.Drawing.Point(42, 460)
    Me.lblReceivedDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblReceivedDate.Name = "lblReceivedDate"
    Me.lblReceivedDate.Size = New System.Drawing.Size(18, 13)
    Me.lblReceivedDate.TabIndex = 20
    Me.lblReceivedDate.Text = "Received Date"
    '
    'dtpArrivalToHubDate
    '
    Me.dtpArrivalToHubDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpArrivalToHubDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpArrivalToHubDate.Location = New System.Drawing.Point(212, 491)
    Me.dtpArrivalToHubDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpArrivalToHubDate.Name = "dtpArrivalToHubDate"
    Me.dtpArrivalToHubDate.ShowCheckBox = True
    Me.dtpArrivalToHubDate.ShowUpDown = True
    Me.dtpArrivalToHubDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpArrivalToHubDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpArrivalToHubDate.TabIndex = 21
    '
    'CtxtArrivalToHubDate
    '
    Me.txtArrivalToHubDate.Location = New System.Drawing.Point(219, 497)
    Me.txtArrivalToHubDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtArrivalToHubDate.Name = "txtArrivalToHubDate"
    Me.txtArrivalToHubDate.Size = New System.Drawing.Size(351, 20)
    Me.txtArrivalToHubDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtArrivalToHubDate.TabIndex = 22
    Me.txtArrivalToHubDate.Text = "txtArrivalToHubDate"
    '
    'lblArrivalToHubDate
    '
    Me.lblArrivalToHubDate.AutoSize = True
    Me.lblArrivalToHubDate.Location = New System.Drawing.Point(42, 500)
    Me.lblArrivalToHubDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblArrivalToHubDate.Name = "lblArrivalToHubDate"
    Me.lblArrivalToHubDate.Size = New System.Drawing.Size(18, 13)
    Me.lblArrivalToHubDate.TabIndex = 23
    Me.lblArrivalToHubDate.Text = "Arrival To Hub Date"
    '
    'dtpArrivalToCustomerDate
    '
    Me.dtpArrivalToCustomerDate.CustomFormat = "dd-MM-yyyy"
    Me.dtpArrivalToCustomerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpArrivalToCustomerDate.Location = New System.Drawing.Point(212, 531)
    Me.dtpArrivalToCustomerDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.dtpArrivalToCustomerDate.Name = "dtpArrivalToCustomerDate"
    Me.dtpArrivalToCustomerDate.ShowCheckBox = True
    Me.dtpArrivalToCustomerDate.ShowUpDown = True
    Me.dtpArrivalToCustomerDate.Size = New System.Drawing.Size(301, 20)
    Me.dtpArrivalToCustomerDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtpArrivalToCustomerDate.TabIndex = 24
    '
    'CtxtArrivalToCustomerDate
    '
    Me.txtArrivalToCustomerDate.Location = New System.Drawing.Point(219, 537)
    Me.txtArrivalToCustomerDate.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtArrivalToCustomerDate.Name = "txtArrivalToCustomerDate"
    Me.txtArrivalToCustomerDate.Size = New System.Drawing.Size(351, 20)
    Me.txtArrivalToCustomerDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtArrivalToCustomerDate.TabIndex = 25
    Me.txtArrivalToCustomerDate.Text = "txtArrivalToCustomerDate"
    '
    'lblArrivalToCustomerDate
    '
    Me.lblArrivalToCustomerDate.AutoSize = True
    Me.lblArrivalToCustomerDate.Location = New System.Drawing.Point(42, 540)
    Me.lblArrivalToCustomerDate.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblArrivalToCustomerDate.Name = "lblArrivalToCustomerDate"
    Me.lblArrivalToCustomerDate.Size = New System.Drawing.Size(18, 13)
    Me.lblArrivalToCustomerDate.TabIndex = 26
    Me.lblArrivalToCustomerDate.Text = "Arrival To Customer Date"
    '
    'cboDeliveryStatus
    '
    Me.cboDeliveryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDeliveryStatus.FormattingEnabled = True
    Me.cboDeliveryStatus.Location = New System.Drawing.Point(212, 571)
    Me.cboDeliveryStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.cboDeliveryStatus.Name = "cboDeliveryStatus"
    Me.cboDeliveryStatus.Size = New System.Drawing.Size(301, 21)
    Me.cboDeliveryStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cboDeliveryStatus.TabIndex = 27
    '
    'BtxtDeliveryStatus
    '
    Me.txtDeliveryStatus.Location = New System.Drawing.Point(219, 577)
    Me.txtDeliveryStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryStatus.Name = "txtDeliveryStatus"
    Me.txtDeliveryStatus.Size = New System.Drawing.Size(351, 20)
    Me.txtDeliveryStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryStatus.TabIndex = 28
    Me.txtDeliveryStatus.Text = "txtDeliveryStatus"
    '
    'DtxtDeliveryStatus
    '
    Me.txtDeliveryStatus.Location = New System.Drawing.Point(219, 577)
    Me.txtDeliveryStatus.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtDeliveryStatus.Name = "txtDeliveryStatus"
    Me.txtDeliveryStatus.Size = New System.Drawing.Size(351, 25)
    Me.txtDeliveryStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDeliveryStatus.TabIndex = 29
    Me.txtDeliveryStatus.Text = "txtDeliveryStatus"
    '
    'lblDeliveryStatus
    '
    Me.lblDeliveryStatus.AutoSize = True
    Me.lblDeliveryStatus.Location = New System.Drawing.Point(42, 580)
    Me.lblDeliveryStatus.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblDeliveryStatus.Name = "lblDeliveryStatus"
    Me.lblDeliveryStatus.Size = New System.Drawing.Size(18, 13)
    Me.lblDeliveryStatus.TabIndex = 30
    Me.lblDeliveryStatus.Text = "Delivery Status"
    '
    'DtxtLocation
    '
    Me.txtLocation.Location = New System.Drawing.Point(219, 617)
    Me.txtLocation.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtLocation.Name = "txtLocation"
    Me.txtLocation.Size = New System.Drawing.Size(351, 105)
    Me.txtLocation.Multiline = True
    Me.txtLocation.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtLocation.WordWrap = False 
    Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocation.TabIndex = 31
    Me.txtLocation.Text = "txtLocation"
    '
    'lblLocation
    '
    Me.lblLocation.AutoSize = True
    Me.lblLocation.Location = New System.Drawing.Point(42, 615)
    Me.lblLocation.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblLocation.Name = "lblLocation"
    Me.lblLocation.Size = New System.Drawing.Size(18, 13)
    Me.lblLocation.TabIndex = 32
    Me.lblLocation.Text = "Location"
    '
    'DtxtProductsSummary
    '
    Me.txtProductsSummary.Location = New System.Drawing.Point(219, 737)
    Me.txtProductsSummary.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtProductsSummary.Name = "txtProductsSummary"
    Me.txtProductsSummary.Size = New System.Drawing.Size(351, 105)
    Me.txtProductsSummary.Multiline = True
    Me.txtProductsSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtProductsSummary.WordWrap = False 
    Me.txtProductsSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProductsSummary.TabIndex = 33
    Me.txtProductsSummary.Text = "txtProductsSummary"
    '
    'lblProductsSummary
    '
    Me.lblProductsSummary.AutoSize = True
    Me.lblProductsSummary.Location = New System.Drawing.Point(42, 735)
    Me.lblProductsSummary.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblProductsSummary.Name = "lblProductsSummary"
    Me.lblProductsSummary.Size = New System.Drawing.Size(18, 13)
    Me.lblProductsSummary.TabIndex = 34
    Me.lblProductsSummary.Text = "Products Summary"
    '
    'DtxtNotes
    '
    Me.txtNotes.Location = New System.Drawing.Point(219, 857)
    Me.txtNotes.Margin = New System.Windows.Forms.Padding(15, 15, 15, 0) 
    Me.txtNotes.Name = "txtNotes"
    Me.txtNotes.Size = New System.Drawing.Size(351, 105)
    Me.txtNotes.Multiline = True
    Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both 
    Me.txtNotes.WordWrap = False 
    Me.txtNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtNotes.TabIndex = 35
    Me.txtNotes.Text = "txtNotes"
    '
    'lblNotes
    '
    Me.lblNotes.AutoSize = True
    Me.lblNotes.Location = New System.Drawing.Point(42, 855)
    Me.lblNotes.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0) 
    Me.lblNotes.Name = "lblNotes"
    Me.lblNotes.Size = New System.Drawing.Size(18, 13)
    Me.lblNotes.TabIndex = 36
    Me.lblNotes.Text = "Notes"
    '
    'btnDelete
    '
    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnDelete.Location = New System.Drawing.Point(99, 1017)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(75, 26)
    Me.btnDelete.TabIndex = 37
    Me.btnDelete.Text = "Delete"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnEdit
    '
    Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnEdit.Location = New System.Drawing.Point(364, 1017)
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(75, 26)
    Me.btnEdit.TabIndex = 38
    Me.btnEdit.Text = "Edit"
    Me.btnEdit.UseVisualStyleBackColor = True
    '
    'btnAdd
    '
    Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnAdd.Location = New System.Drawing.Point(283, 1017)
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(75, 26)
    Me.btnAdd.TabIndex = 39
    Me.btnAdd.Text = "Add"
    Me.btnAdd.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnCancel.Location = New System.Drawing.Point(283, 1005)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 26)
    Me.btnCancel.TabIndex = 40
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnUpdate
    '
    Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.btnUpdate.Location = New System.Drawing.Point(364, 1005)
    Me.btnUpdate.Name = "btnUpdate"
    Me.btnUpdate.Size = New System.Drawing.Size(75, 26)
    Me.btnUpdate.TabIndex = 41
    Me.btnUpdate.Text = "Update"
    Me.btnUpdate.UseVisualStyleBackColor = True
    '
    'ctlDelivery 
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.AutoScroll = True
    Me.Controls.Add(Me.txtID)
    Me.Controls.Add(Me.lblID)
    Me.Controls.Add(Me.cboOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.txtOrderHeader)
    Me.Controls.Add(Me.lblOrderHeader)
    Me.Controls.Add(Me.txtDeliveryAddress)
    Me.Controls.Add(Me.lblDeliveryAddress)
    Me.Controls.Add(Me.txtContactPhone)
    Me.Controls.Add(Me.lblContactPhone)
    Me.Controls.Add(Me.txtContactName)
    Me.Controls.Add(Me.lblContactName)
    Me.Controls.Add(Me.cboDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.txtDeliveryMethod)
    Me.Controls.Add(Me.lblDeliveryMethod)
    Me.Controls.Add(Me.dtpOrderedDate)
    Me.Controls.Add(Me.txtOrderedDate)
    Me.Controls.Add(Me.lblOrderedDate)
    Me.Controls.Add(Me.dtpReceivedDate)
    Me.Controls.Add(Me.txtReceivedDate)
    Me.Controls.Add(Me.lblReceivedDate)
    Me.Controls.Add(Me.dtpArrivalToHubDate)
    Me.Controls.Add(Me.txtArrivalToHubDate)
    Me.Controls.Add(Me.lblArrivalToHubDate)
    Me.Controls.Add(Me.dtpArrivalToCustomerDate)
    Me.Controls.Add(Me.txtArrivalToCustomerDate)
    Me.Controls.Add(Me.lblArrivalToCustomerDate)
    Me.Controls.Add(Me.cboDeliveryStatus)
    Me.Controls.Add(Me.txtDeliveryStatus)
    Me.Controls.Add(Me.txtDeliveryStatus)
    Me.Controls.Add(Me.lblDeliveryStatus)
    Me.Controls.Add(Me.txtLocation)
    Me.Controls.Add(Me.lblLocation)
    Me.Controls.Add(Me.txtProductsSummary)
    Me.Controls.Add(Me.lblProductsSummary)
    Me.Controls.Add(Me.txtNotes)
    Me.Controls.Add(Me.lblNotes)
    Me.Controls.Add(Me.btnEdit)
    Me.Controls.Add(Me.btnAdd)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnUpdate)
    Me.DoubleBuffered = True 
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccDelivery"
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
  Friend WithEvents txtDeliveryAddress As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryAddress As System.Windows.Forms.Label
  Friend WithEvents txtContactPhone As System.Windows.Forms.TextBox
  Friend WithEvents lblContactPhone As System.Windows.Forms.Label
  Friend WithEvents txtContactName As System.Windows.Forms.TextBox
  Friend WithEvents lblContactName As System.Windows.Forms.Label
  Friend WithEvents cboDeliveryMethod As System.Windows.Forms.ComboBox
  Friend WithEvents txtDeliveryMethod As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryMethod As System.Windows.Forms.Label
  Friend WithEvents dtpOrderedDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtOrderedDate As System.Windows.Forms.TextBox
  Friend WithEvents lblOrderedDate As System.Windows.Forms.Label
  Friend WithEvents dtpReceivedDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtReceivedDate As System.Windows.Forms.TextBox
  Friend WithEvents lblReceivedDate As System.Windows.Forms.Label
  Friend WithEvents dtpArrivalToHubDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtArrivalToHubDate As System.Windows.Forms.TextBox
  Friend WithEvents lblArrivalToHubDate As System.Windows.Forms.Label
  Friend WithEvents dtpArrivalToCustomerDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtArrivalToCustomerDate As System.Windows.Forms.TextBox
  Friend WithEvents lblArrivalToCustomerDate As System.Windows.Forms.Label
  Friend WithEvents cboDeliveryStatus As System.Windows.Forms.ComboBox
  Friend WithEvents txtDeliveryStatus As System.Windows.Forms.TextBox
  Friend WithEvents lblDeliveryStatus As System.Windows.Forms.Label
  Friend WithEvents txtLocation As System.Windows.Forms.TextBox
  Friend WithEvents lblLocation As System.Windows.Forms.Label
  Friend WithEvents txtProductsSummary As System.Windows.Forms.TextBox
  Friend WithEvents lblProductsSummary As System.Windows.Forms.Label
  Friend WithEvents txtNotes As System.Windows.Forms.TextBox
  Friend WithEvents lblNotes As System.Windows.Forms.Label
  Friend WithEvents btnEdit As System.Windows.Forms.Button
  Friend WithEvents btnAdd As System.Windows.Forms.Button
  Friend WithEvents btnDelete As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnUpdate As System.Windows.Forms.Button

End Class
