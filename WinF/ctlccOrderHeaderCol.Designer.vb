<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccOrderHeaderCol
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
    Me.components = New System.ComponentModel.Container
    Dim styleAlternatingRowsDefaultCell As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle() 
    Dim styleColumnHeadersDefaultCell As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle() 
    Dim styleDefaultCell As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle() 
    Me.dgvOrderHeader = New System.Windows.Forms.DataGridView() 
    Me.BN = New System.Windows.Forms.BindingNavigator(Me.components)
    Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator() 
    Me.btnEdit = New System.Windows.Forms.ToolStripButton() 
    Me.btnImport = New System.Windows.Forms.ToolStripButton() 
    Me.btnAdd = New System.Windows.Forms.ToolStripButton() 
    Me.btnDelete = New System.Windows.Forms.ToolStripButton() 
    Me.btnCeaseEdit = New System.Windows.Forms.ToolStripButton() 
    Me.tssEditMode = New System.Windows.Forms.ToolStripSeparator() 
    Me.tssColumns = New System.Windows.Forms.ToolStripSeparator() 
    Me.lblEditMode = New System.Windows.Forms.ToolStripLabel() 
    Me.tssReports = New System.Windows.Forms.ToolStripSeparator()  
    Me.btnSpreadsheet = New System.Windows.Forms.ToolStripButton()  
    Me.lblStatus = New System.Windows.Forms.ToolStripLabel() 
    Me.btnReport = New System.Windows.Forms.ToolStripButton()  
    Me.btnColumns = New System.Windows.Forms.ToolStripDropDownButton()  
    Me.cmsGrid = New System.Windows.Forms.ContextMenuStrip(Me.components) 
    Me.tsmiOpenDetail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiOpenInTab = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiCopyID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiCopyRow = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiCopyRowHeaders = New System.Windows.Forms.ToolStripMenuItem() 
    Me.tsmiCopyExcel = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsCtlOrderHeader = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOrderNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsCustomer = New System.Windows.Forms.BindingSource(Me.components)
    Me.colCustomer = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colCustomerText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCustomer = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOrderDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTotalAmount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colVATAmount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleVATAmount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTotalWithVAT = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTotalWithVAT = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsPaymentMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colPaymentMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisiblePaymentMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsPaymentStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colPaymentStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisiblePaymentStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colPaymentDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePaymentDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colInvoiceNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleInvoiceNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDeliveryMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDeliveryMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDeliveryMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colDeliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDeliveryDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDeliveryDay = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDeliveryDay = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDeliveryDay = New System.Windows.Forms.ToolStripMenuItem()  
    Me.bsOrderStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colOrderStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleOrderStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNotes2 = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes2 = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOrderMonth = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderMonth = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colQuarter = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleQuarter = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsPaymentMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsPaymentStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDeliveryDay, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsOrderStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvOrderHeader
    '
    Me.dgvOrderHeader.AllowUserToAddRows = False
    Me.dgvOrderHeader.AllowUserToDeleteRows = False
    Me.dgvOrderHeader.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvOrderHeader.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvOrderHeader.AutoGenerateColumns = False
    Me.dgvOrderHeader.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvOrderHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvOrderHeader.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvOrderHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvOrderHeader.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colOrderNumber, Me.colCustomer, Me.colCustomerText, Me.colOrderDate, Me.colTotalAmount, Me.colVATAmount, Me.colTotalWithVAT, Me.colPaymentMethod, Me.colPaymentStatus, Me.colPaymentDate, Me.colInvoiceNumber, Me.colDeliveryMethod, Me.colDeliveryDate, Me.colDeliveryDay, Me.colOrderStatus, Me.colNotes, Me.colNotes2, Me.colOrderMonth, Me.colQuarter})
    Me.dgvOrderHeader.DataSource = Me.bsCtlOrderHeader
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvOrderHeader.DefaultCellStyle = styleDefaultCell
    Me.dgvOrderHeader.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvOrderHeader.EnableHeadersVisualStyles = False
    Me.dgvOrderHeader.Location = New System.Drawing.Point(0, 25)
    Me.dgvOrderHeader.MultiSelect = False 
    Me.dgvOrderHeader.ContextMenuStrip = Me.cmsGrid 
    Me.dgvOrderHeader.Name = "dgvOrderHeader"
    Me.dgvOrderHeader.RowHeadersVisible = False
    Me.dgvOrderHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvOrderHeader.Size = New System.Drawing.Size(712, 347)
    Me.dgvOrderHeader.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlOrderHeader
    Me.BN.CountItem = Nothing
    Me.BN.DeleteItem = Nothing
    Me.BN.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.BN.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorSeparator, Me.btnEdit, Me.btnAdd, Me.btnDelete, Me.btnImport, Me.btnCeaseEdit, Me.tssEditMode, Me.lblEditMode, Me.tssReports, Me.btnSpreadsheet, Me.lblStatus, Me.btnReport, Me.tssColumns, Me.btnColumns})
    Me.BN.Location = New System.Drawing.Point(0, 0)
    Me.BN.MoveFirstItem = Nothing
    Me.BN.MoveLastItem = Nothing
    Me.BN.MoveNextItem = Nothing
    Me.BN.MovePreviousItem = Nothing
    Me.BN.Name = "BN"
    Me.BN.PositionItem = Nothing
    Me.BN.Size = New System.Drawing.Size(712, 25)
    Me.BN.TabIndex = 1
    Me.BN.Text = "BN"
    '
    'BindingNavigatorSeparator
    '
    Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
    Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
    '
    'btnEdit
    '
    Me.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnEdit.Name = "btnEdit"
    Me.btnEdit.Size = New System.Drawing.Size(29, 22)
    Me.btnEdit.Text = "&Edit"
    Me.btnEdit.ToolTipText = "Enter edit mode"
    '
    'btnImport
    '
    Me.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnImport.Name = "btnImport"
    Me.btnImport.Size = New System.Drawing.Size(29, 22)
    Me.btnImport.Text = "&Import"
    Me.btnImport.ToolTipText = "Import CSV File"
    '
    'btnAdd
    '
    Me.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnAdd.Name = "btnAdd"
    Me.btnAdd.Size = New System.Drawing.Size(30, 22)
    Me.btnAdd.Text = "&Add"
    Me.btnAdd.ToolTipText = "Add new row"
    '
    'btnDelete
    '
    Me.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(42, 22)
    Me.btnDelete.Text = "&Delete"
    Me.btnDelete.ToolTipText = "Delete current row"
    '
    'btnCeaseEdit
    '
    Me.btnCeaseEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.btnCeaseEdit.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnCeaseEdit.Name = "btnCeaseEdit"
    Me.btnCeaseEdit.Size = New System.Drawing.Size(62, 22)
    Me.btnCeaseEdit.Text = "&Cease Edit"
    Me.btnCeaseEdit.ToolTipText = "Exit edit mode"
    '
    'tssEditMode
    '
    Me.tssEditMode.Name = "tssEditMode"
    Me.tssEditMode.Size = New System.Drawing.Size(6, 25)
    '
    'lblEditMode
    '
    Me.lblEditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.lblEditMode.Font = New System.Drawing.Font("Segoe UI", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
    Me.lblEditMode.ForeColor = System.Drawing.Color.Red
    Me.lblEditMode.Name = "lblEditMode"
    Me.lblEditMode.Size = New System.Drawing.Size(62, 22)
    Me.lblEditMode.Text = "Edit Mode"
    '
    'tssReports 
    ' 
    Me.tssReports.Name = "tssReports" 
    Me.tssReports.Size = New System.Drawing.Size(6, 25) 
    ' 
    'btnSpreadsheet 
    ' 
    Me.btnSpreadsheet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text 
    Me.btnSpreadsheet.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnSpreadsheet.Name = "btnSpreadsheet" 
    Me.btnSpreadsheet.Size = New System.Drawing.Size(72, 22) 
    Me.btnSpreadsheet.Text = "Spreadsheet" 
    ' 
    'lblStatus 
    ' 
    Me.lblStatus.Name = "lblStatus" 
    Me.lblStatus.ForeColor = Color.Red 
    Me.lblStatus.Size = New System.Drawing.Size(52, 22) 
    Me.lblStatus.Text = "lblStatus" 
    ' 
    'btnReport 
    ' 
    Me.btnReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text 
    Me.btnReport.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnReport.Name = "btnReport" 
    Me.btnReport.Size = New System.Drawing.Size(44, 22) 
    Me.btnReport.Text = "Report" 
    ' 
    'btnColumns 
    ' 
    Me.btnColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text 
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleOrderNumber, Me.mnuColVisibleCustomer, Me.mnuColVisibleOrderDate, Me.mnuColVisibleTotalAmount, Me.mnuColVisibleVATAmount, Me.mnuColVisibleTotalWithVAT, Me.mnuColVisiblePaymentMethod, Me.mnuColVisiblePaymentStatus, Me.mnuColVisiblePaymentDate, Me.mnuColVisibleInvoiceNumber, Me.mnuColVisibleDeliveryMethod, Me.mnuColVisibleDeliveryDate, Me.mnuColVisibleDeliveryDay, Me.mnuColVisibleOrderStatus, Me.mnuColVisibleNotes, Me.mnuColVisibleNotes2, Me.mnuColVisibleOrderMonth, Me.mnuColVisibleQuarter, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsCustomer
    Me.bsCustomer.DataSource = GetType(clsComboList)
    'bsPaymentMethod
    Me.bsPaymentMethod.DataSource = GetType(clsComboList)
    'bsPaymentStatus
    Me.bsPaymentStatus.DataSource = GetType(clsComboList)
    'bsDeliveryMethod
    Me.bsDeliveryMethod.DataSource = GetType(clsComboList)
    'bsDeliveryDay
    Me.bsDeliveryDay.DataSource = GetType(clsComboList)
    'bsOrderStatus
    Me.bsOrderStatus.DataSource = GetType(clsComboList)
    '
    'bsCtlOrderHeader
    '
    Me.bsCtlOrderHeader.DataSource = GetType(clsOrderHeader)
    '
    'colID
    '
    Me.colID.DataPropertyName = "ID"
    Me.colID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colID.HeaderText = "ID"
    Me.colID.Name = "colID"
    Me.colID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colID.ReadOnly = True
    Me.colID.Width = 60
    Me.colID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleID 
    ' 
    Me.mnuColVisibleID.Checked = True 
    Me.mnuColVisibleID.CheckOnClick = True 
    Me.mnuColVisibleID.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleID.Name = "mnuColVisibleID" 
    Me.mnuColVisibleID.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleID.Text = "ID" 
    ' 
    'colOrderNumber
    '
    Me.colOrderNumber.DataPropertyName = "OrderNumber"
    Me.colOrderNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colOrderNumber.HeaderText = "Order Number"
    Me.colOrderNumber.Name = "colOrderNumber"
    Me.colOrderNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderNumber.Width = 60
    Me.colOrderNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderNumber 
    ' 
    Me.mnuColVisibleOrderNumber.Checked = True 
    Me.mnuColVisibleOrderNumber.CheckOnClick = True 
    Me.mnuColVisibleOrderNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderNumber.Name = "mnuColVisibleOrderNumber" 
    Me.mnuColVisibleOrderNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderNumber.Text = "Order Number" 
    ' 
    'colCustomer
    '
    Me.colCustomer.DataPropertyName = "CustomerID"
    Me.colCustomer.DataSource = Me.bsCustomer
    Me.colCustomer.DisplayMember = "Text"
    Me.colCustomer.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colCustomer.HeaderText = "Customer"
    Me.colCustomer.Name = "colCustomer"
    Me.colCustomer.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCustomer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCustomer.ValueMember = "KeyLong"
    Me.colCustomer.Width = 60
    Me.colCustomer.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colCustomerText 
    ' 
    Me.colCustomerText.DataPropertyName = "CustomerText" 
    Me.colCustomerText.HeaderText = "Customer" 
    Me.colCustomerText.Name = "colCustomerText" 
    Me.colCustomerText.ReadOnly = True 
    Me.colCustomer.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCustomer 
    ' 
    Me.mnuColVisibleCustomer.Checked = True 
    Me.mnuColVisibleCustomer.CheckOnClick = True 
    Me.mnuColVisibleCustomer.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCustomer.Name = "mnuColVisibleCustomer" 
    Me.mnuColVisibleCustomer.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCustomer.Text = "Customer" 
    ' 
    'colOrderDate
    '
    Me.colOrderDate.DataPropertyName = "OrderDate"
    Me.colOrderDate.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colOrderDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colOrderDate.HeaderText = "Order Date"
    Me.colOrderDate.Name = "colOrderDate"
    Me.colOrderDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderDate.Width = 60
    Me.colOrderDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderDate 
    ' 
    Me.mnuColVisibleOrderDate.Checked = True 
    Me.mnuColVisibleOrderDate.CheckOnClick = True 
    Me.mnuColVisibleOrderDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderDate.Name = "mnuColVisibleOrderDate" 
    Me.mnuColVisibleOrderDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderDate.Text = "Order Date" 
    ' 
    'colTotalAmount
    '
    Me.colTotalAmount.DataPropertyName = "TotalAmount"
    Me.colTotalAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTotalAmount.HeaderText = "Total Amount"
    Me.colTotalAmount.Name = "colTotalAmount"
    Me.colTotalAmount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTotalAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTotalAmount.Width = 60
    Me.colTotalAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTotalAmount 
    ' 
    Me.mnuColVisibleTotalAmount.Checked = True 
    Me.mnuColVisibleTotalAmount.CheckOnClick = True 
    Me.mnuColVisibleTotalAmount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTotalAmount.Name = "mnuColVisibleTotalAmount" 
    Me.mnuColVisibleTotalAmount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTotalAmount.Text = "Total Amount" 
    ' 
    'colVATAmount
    '
    Me.colVATAmount.DataPropertyName = "VATAmount"
    Me.colVATAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colVATAmount.HeaderText = "VAT Amount"
    Me.colVATAmount.Name = "colVATAmount"
    Me.colVATAmount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colVATAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colVATAmount.Width = 60
    Me.colVATAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleVATAmount 
    ' 
    Me.mnuColVisibleVATAmount.Checked = True 
    Me.mnuColVisibleVATAmount.CheckOnClick = True 
    Me.mnuColVisibleVATAmount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleVATAmount.Name = "mnuColVisibleVATAmount" 
    Me.mnuColVisibleVATAmount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleVATAmount.Text = "VAT Amount" 
    ' 
    'colTotalWithVAT
    '
    Me.colTotalWithVAT.DataPropertyName = "TotalWithVAT"
    Me.colTotalWithVAT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTotalWithVAT.HeaderText = "Total With VAT"
    Me.colTotalWithVAT.Name = "colTotalWithVAT"
    Me.colTotalWithVAT.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTotalWithVAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTotalWithVAT.Width = 60
    Me.colTotalWithVAT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTotalWithVAT 
    ' 
    Me.mnuColVisibleTotalWithVAT.Checked = True 
    Me.mnuColVisibleTotalWithVAT.CheckOnClick = True 
    Me.mnuColVisibleTotalWithVAT.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTotalWithVAT.Name = "mnuColVisibleTotalWithVAT" 
    Me.mnuColVisibleTotalWithVAT.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTotalWithVAT.Text = "Total With VAT" 
    ' 
    'colPaymentMethod
    '
    Me.colPaymentMethod.DataPropertyName = "PaymentMethod"
    Me.colPaymentMethod.DataSource = Me.bsPaymentMethod
    Me.colPaymentMethod.DisplayMember = "Text"
    Me.colPaymentMethod.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colPaymentMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colPaymentMethod.HeaderText = "Payment Method"
    Me.colPaymentMethod.Name = "colPaymentMethod"
    Me.colPaymentMethod.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPaymentMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPaymentMethod.ValueMember = "KeyEnum"
    Me.colPaymentMethod.Width = 60
    Me.colPaymentMethod.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePaymentMethod 
    ' 
    Me.mnuColVisiblePaymentMethod.Checked = True 
    Me.mnuColVisiblePaymentMethod.CheckOnClick = True 
    Me.mnuColVisiblePaymentMethod.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePaymentMethod.Name = "mnuColVisiblePaymentMethod" 
    Me.mnuColVisiblePaymentMethod.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePaymentMethod.Text = "Payment Method" 
    ' 
    'colPaymentStatus
    '
    Me.colPaymentStatus.DataPropertyName = "PaymentStatus"
    Me.colPaymentStatus.DataSource = Me.bsPaymentStatus
    Me.colPaymentStatus.DisplayMember = "Text"
    Me.colPaymentStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colPaymentStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colPaymentStatus.HeaderText = "Payment Status"
    Me.colPaymentStatus.Name = "colPaymentStatus"
    Me.colPaymentStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPaymentStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPaymentStatus.ValueMember = "KeyEnum"
    Me.colPaymentStatus.Width = 60
    Me.colPaymentStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePaymentStatus 
    ' 
    Me.mnuColVisiblePaymentStatus.Checked = True 
    Me.mnuColVisiblePaymentStatus.CheckOnClick = True 
    Me.mnuColVisiblePaymentStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePaymentStatus.Name = "mnuColVisiblePaymentStatus" 
    Me.mnuColVisiblePaymentStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePaymentStatus.Text = "Payment Status" 
    ' 
    'colPaymentDate
    '
    Me.colPaymentDate.DataPropertyName = "PaymentDate"
    Me.colPaymentDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colPaymentDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colPaymentDate.HeaderText = "Payment Date"
    Me.colPaymentDate.Name = "colPaymentDate"
    Me.colPaymentDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPaymentDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPaymentDate.Width = 60
    Me.colPaymentDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePaymentDate 
    ' 
    Me.mnuColVisiblePaymentDate.Checked = True 
    Me.mnuColVisiblePaymentDate.CheckOnClick = True 
    Me.mnuColVisiblePaymentDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePaymentDate.Name = "mnuColVisiblePaymentDate" 
    Me.mnuColVisiblePaymentDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePaymentDate.Text = "Payment Date" 
    ' 
    'colInvoiceNumber
    '
    Me.colInvoiceNumber.DataPropertyName = "InvoiceNumber"
    Me.colInvoiceNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colInvoiceNumber.HeaderText = "Invoice Number"
    Me.colInvoiceNumber.Name = "colInvoiceNumber"
    Me.colInvoiceNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colInvoiceNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colInvoiceNumber.Width = 60
    Me.colInvoiceNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleInvoiceNumber 
    ' 
    Me.mnuColVisibleInvoiceNumber.Checked = True 
    Me.mnuColVisibleInvoiceNumber.CheckOnClick = True 
    Me.mnuColVisibleInvoiceNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleInvoiceNumber.Name = "mnuColVisibleInvoiceNumber" 
    Me.mnuColVisibleInvoiceNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleInvoiceNumber.Text = "Invoice Number" 
    ' 
    'colDeliveryMethod
    '
    Me.colDeliveryMethod.DataPropertyName = "DeliveryMethod"
    Me.colDeliveryMethod.DataSource = Me.bsDeliveryMethod
    Me.colDeliveryMethod.DisplayMember = "Text"
    Me.colDeliveryMethod.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colDeliveryMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDeliveryMethod.HeaderText = "Delivery Method"
    Me.colDeliveryMethod.Name = "colDeliveryMethod"
    Me.colDeliveryMethod.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryMethod.ValueMember = "KeyEnum"
    Me.colDeliveryMethod.Width = 60
    Me.colDeliveryMethod.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryMethod 
    ' 
    Me.mnuColVisibleDeliveryMethod.Checked = True 
    Me.mnuColVisibleDeliveryMethod.CheckOnClick = True 
    Me.mnuColVisibleDeliveryMethod.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryMethod.Name = "mnuColVisibleDeliveryMethod" 
    Me.mnuColVisibleDeliveryMethod.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryMethod.Text = "Delivery Method" 
    ' 
    'colDeliveryDate
    '
    Me.colDeliveryDate.DataPropertyName = "DeliveryDate"
    Me.colDeliveryDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colDeliveryDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDeliveryDate.HeaderText = "Delivery Date"
    Me.colDeliveryDate.Name = "colDeliveryDate"
    Me.colDeliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryDate.Width = 60
    Me.colDeliveryDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryDate 
    ' 
    Me.mnuColVisibleDeliveryDate.Checked = True 
    Me.mnuColVisibleDeliveryDate.CheckOnClick = True 
    Me.mnuColVisibleDeliveryDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryDate.Name = "mnuColVisibleDeliveryDate" 
    Me.mnuColVisibleDeliveryDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryDate.Text = "Delivery Date" 
    ' 
    'colDeliveryDay
    '
    Me.colDeliveryDay.DataPropertyName = "DeliveryDay"
    Me.colDeliveryDay.DataSource = Me.bsDeliveryDay
    Me.colDeliveryDay.DisplayMember = "Text"
    Me.colDeliveryDay.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colDeliveryDay.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDeliveryDay.HeaderText = "Delivery Day"
    Me.colDeliveryDay.Name = "colDeliveryDay"
    Me.colDeliveryDay.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDeliveryDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDeliveryDay.ValueMember = "KeyEnum"
    Me.colDeliveryDay.Width = 60
    Me.colDeliveryDay.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDeliveryDay 
    ' 
    Me.mnuColVisibleDeliveryDay.Checked = True 
    Me.mnuColVisibleDeliveryDay.CheckOnClick = True 
    Me.mnuColVisibleDeliveryDay.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDeliveryDay.Name = "mnuColVisibleDeliveryDay" 
    Me.mnuColVisibleDeliveryDay.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDeliveryDay.Text = "Delivery Day" 
    ' 
    'colOrderStatus
    '
    Me.colOrderStatus.DataPropertyName = "OrderStatus"
    Me.colOrderStatus.DataSource = Me.bsOrderStatus
    Me.colOrderStatus.DisplayMember = "Text"
    Me.colOrderStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colOrderStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colOrderStatus.HeaderText = "Order Status"
    Me.colOrderStatus.Name = "colOrderStatus"
    Me.colOrderStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderStatus.ValueMember = "KeyEnum"
    Me.colOrderStatus.Width = 60
    Me.colOrderStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderStatus 
    ' 
    Me.mnuColVisibleOrderStatus.Checked = True 
    Me.mnuColVisibleOrderStatus.CheckOnClick = True 
    Me.mnuColVisibleOrderStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderStatus.Name = "mnuColVisibleOrderStatus" 
    Me.mnuColVisibleOrderStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderStatus.Text = "Order Status" 
    ' 
    'colNotes
    '
    Me.colNotes.DataPropertyName = "Notes"
    Me.colNotes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colNotes.HeaderText = "Notes"
    Me.colNotes.Name = "colNotes"
    Me.colNotes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNotes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNotes.Width = 60
    Me.colNotes.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNotes 
    ' 
    Me.mnuColVisibleNotes.Checked = True 
    Me.mnuColVisibleNotes.CheckOnClick = True 
    Me.mnuColVisibleNotes.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNotes.Name = "mnuColVisibleNotes" 
    Me.mnuColVisibleNotes.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNotes.Text = "Notes" 
    ' 
    'colNotes2
    '
    Me.colNotes2.DataPropertyName = "Notes2"
    Me.colNotes2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colNotes2.HeaderText = "Notes 2"
    Me.colNotes2.Name = "colNotes2"
    Me.colNotes2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNotes2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNotes2.Width = 60
    Me.colNotes2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNotes2 
    ' 
    Me.mnuColVisibleNotes2.Checked = True 
    Me.mnuColVisibleNotes2.CheckOnClick = True 
    Me.mnuColVisibleNotes2.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNotes2.Name = "mnuColVisibleNotes2" 
    Me.mnuColVisibleNotes2.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNotes2.Text = "Notes 2" 
    ' 
    'colOrderMonth
    '
    Me.colOrderMonth.DataPropertyName = "OrderMonth"
    Me.colOrderMonth.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colOrderMonth.HeaderText = "Order Month"
    Me.colOrderMonth.Name = "colOrderMonth"
    Me.colOrderMonth.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderMonth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderMonth.Width = 60
    Me.colOrderMonth.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderMonth 
    ' 
    Me.mnuColVisibleOrderMonth.Checked = True 
    Me.mnuColVisibleOrderMonth.CheckOnClick = True 
    Me.mnuColVisibleOrderMonth.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderMonth.Name = "mnuColVisibleOrderMonth" 
    Me.mnuColVisibleOrderMonth.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderMonth.Text = "Order Month" 
    ' 
    'colQuarter
    '
    Me.colQuarter.DataPropertyName = "Quarter"
    Me.colQuarter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colQuarter.HeaderText = "Quarter"
    Me.colQuarter.Name = "colQuarter"
    Me.colQuarter.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colQuarter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colQuarter.Width = 60
    Me.colQuarter.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleQuarter 
    ' 
    Me.mnuColVisibleQuarter.Checked = True 
    Me.mnuColVisibleQuarter.CheckOnClick = True 
    Me.mnuColVisibleQuarter.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleQuarter.Name = "mnuColVisibleQuarter" 
    Me.mnuColVisibleQuarter.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleQuarter.Text = "Quarter" 
    ' 
    '
    'mnuColsReset 
    ' 
    Me.mnuColsReset.Name = "mnuColsReset" 
    Me.mnuColsReset.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColsReset.Text = "Reset" 
    ' 
    'mnuColsHideMost 
    ' 
    Me.mnuColsHideMost.Name = "mnuColsHideMost" 
    Me.mnuColsHideMost.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColsHideMost.Text = "HideMost" 
    ' 
    'chkAutoRefresh 
    ' 
    Me.chkAutoRefresh.AutoSize = False 
    Me.chkAutoRefresh.BackColor = System.Drawing.SystemColors.Control 
    Me.chkAutoRefresh.Dock = System.Windows.Forms.DockStyle.Right 
    Me.chkAutoRefresh.Location = New System.Drawing.Point(589, 10) 
    Me.chkAutoRefresh.Name = "chkAutoRefresh" 
    Me.chkAutoRefresh.Padding = New System.Windows.Forms.Padding(10, 0, 0, 8) 
    Me.chkAutoRefresh.Size = New System.Drawing.Size(123, 35) 
    Me.chkAutoRefresh.TabIndex = 2 
    Me.chkAutoRefresh.Text = "Auto-Refresh" 
    Me.chkAutoRefresh.UseVisualStyleBackColor = False 
    Me.chkAutoRefresh.Visible = False 
    ' 
    'lblGrid 
    ' 
    Me.lblGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles) 
    Me.lblGrid.AutoSize = True 
    Me.lblGrid.Location = New System.Drawing.Point(0, 0) 
    Me.lblGrid.Margin = New System.Windows.Forms.Padding(3) 
    Me.lblGrid.Name = "lblGrid" 
    Me.lblGrid.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3) 
    Me.lblGrid.Size = New System.Drawing.Size(55, 32) 
    Me.lblGrid.TabIndex = 0 
    Me.lblGrid.Text = "Test Text." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2nd row" 
    Me.lblGrid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft 
    ' 
    'txtSearch 
    ' 
    Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Right 
    Me.txtSearch.Location = New System.Drawing.Point(389, 10) 
    Me.txtSearch.Name = "txtSearch" 
    Me.txtSearch.Size = New System.Drawing.Size(200, 25) 
    Me.txtSearch.TabIndex = 4 
    Me.txtSearch.ForeColor = System.Drawing.Color.Gray 
    Me.txtSearch.Text = "Search..." 
    ' 
    'cmsGrid - context menu for grid rows 
    ' 
    Me.cmsGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiOpenDetail, Me.tsmiOpenInTab, New System.Windows.Forms.ToolStripSeparator(), Me.tsmiCopyID, Me.tsmiCopyRow, Me.tsmiCopyRowHeaders, Me.tsmiCopyExcel}) 
    Me.cmsGrid.Name = "cmsGrid" 
    Me.cmsGrid.Size = New System.Drawing.Size(220, 170) 
    Me.tsmiOpenDetail.Name = "tsmiOpenDetail" 
    Me.tsmiOpenDetail.Text = "Open in New Window" 
    Me.tsmiOpenDetail.Font = New System.Drawing.Font(Me.tsmiOpenDetail.Font, System.Drawing.FontStyle.Bold) 
    Me.tsmiOpenInTab.Name = "tsmiOpenInTab" 
    Me.tsmiOpenInTab.Text = "Open in New Tab" 
    Me.tsmiCopyID.Name = "tsmiCopyID" 
    Me.tsmiCopyID.Text = "Copy ID" 
    Me.tsmiCopyRow.Name = "tsmiCopyRow" 
    Me.tsmiCopyRow.Text = "Copy Row" 
    Me.tsmiCopyRowHeaders.Name = "tsmiCopyRowHeaders" 
    Me.tsmiCopyRowHeaders.Text = "Copy Row with Headers" 
    Me.tsmiCopyExcel.Name = "tsmiCopyExcel" 
    Me.tsmiCopyExcel.Text = "Copy for Excel" 
    ' 
    'pnlHeader 
    ' 
    Me.pnlHeader.AutoSize = False 
    Me.pnlHeader.Controls.Add(Me.txtSearch) 
    Me.pnlHeader.Controls.Add(Me.chkAutoRefresh) 
    Me.pnlHeader.Controls.Add(Me.lblGrid) 
    Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top 
    Me.pnlHeader.Location = New System.Drawing.Point(0, 0) 
    Me.pnlHeader.Name = "pnlHeader" 
    Me.pnlHeader.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0) 
    Me.pnlHeader.Size = New System.Drawing.Size(712, 45) 
    Me.pnlHeader.TabIndex = 3 
    ' 
    'ctlOrderHeaderCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvOrderHeader)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccOrderHeaderCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCustomer, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsPaymentMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsPaymentStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDeliveryMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDeliveryDay, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsOrderStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvOrderHeader As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlOrderHeader As System.Windows.Forms.BindingSource
  Friend WithEvents BN As System.Windows.Forms.BindingNavigator
  Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnImport As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnAdd As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnCeaseEdit As System.Windows.Forms.ToolStripButton
  Friend WithEvents tssEditMode As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents lblEditMode As System.Windows.Forms.ToolStripLabel
  Friend WithEvents tssReports As System.Windows.Forms.ToolStripSeparator 
  Friend WithEvents btnSpreadsheet As System.Windows.Forms.ToolStripButton 
  Friend WithEvents lblStatus As ToolStripLabel 
  Friend WithEvents btnReport As System.Windows.Forms.ToolStripButton 
  Friend WithEvents btnColumns As System.Windows.Forms.ToolStripDropDownButton 
  Friend WithEvents tssColumns As System.Windows.Forms.ToolStripSeparator 
  Friend WithEvents mnuColsReset As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents mnuColsHideMost As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents chkAutoRefresh As CheckBox 
  Friend WithEvents lblGrid As Label 
  Friend WithEvents txtSearch As TextBox 
  Friend WithEvents pnlHeader As Panel 
  Friend WithEvents cmsGrid As System.Windows.Forms.ContextMenuStrip 
  Friend WithEvents tsmiOpenDetail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiOpenInTab As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiCopyID As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiCopyRow As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiCopyRowHeaders As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents tsmiCopyExcel As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleID As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOrderNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOrderNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsCustomer As System.Windows.Forms.BindingSource
  Friend WithEvents colCustomer As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colCustomerText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleCustomer As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOrderDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOrderDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTotalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTotalAmount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colVATAmount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleVATAmount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTotalWithVAT As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTotalWithVAT As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsPaymentMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colPaymentMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisiblePaymentMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsPaymentStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colPaymentStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisiblePaymentStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPaymentDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePaymentDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colInvoiceNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleInvoiceNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDeliveryMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colDeliveryMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDeliveryMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDeliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDeliveryDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDeliveryDay As System.Windows.Forms.BindingSource
  Friend WithEvents colDeliveryDay As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDeliveryDay As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsOrderStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colOrderStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleOrderStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes2 As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOrderMonth As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOrderMonth As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colQuarter As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleQuarter As System.Windows.Forms.ToolStripMenuItem 

End Class
