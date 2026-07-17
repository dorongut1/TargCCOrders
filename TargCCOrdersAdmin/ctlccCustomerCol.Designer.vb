<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccCustomerCol
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
    Me.dgvCustomer = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlCustomer = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCustomerCode = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCustomerCode = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCustomerName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPhone = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePhone = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAddress = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAddress = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCity = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCity = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTaxID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTaxID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsCustomerType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colCustomerType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleCustomerType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colPaymentTermsDays = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePaymentTermsDays = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colIsActive = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsActive = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLocation = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAccountantEmail = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleAccountantEmail = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsAccountantMethod = New System.Windows.Forms.BindingSource(Me.components)
    Me.colAccountantMethod = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleAccountantMethod = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colInvoiceName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleInvoiceName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProfitabilityCode = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProfitabilityCode = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCustomerIdentifier = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCustomerIdentifier = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCustomerType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsAccountantMethod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvCustomer
    '
    Me.dgvCustomer.AllowUserToAddRows = False
    Me.dgvCustomer.AllowUserToDeleteRows = False
    Me.dgvCustomer.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvCustomer.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvCustomer.AutoGenerateColumns = False
    Me.dgvCustomer.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvCustomer.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colCustomerCode, Me.colCustomerName, Me.colPhone, Me.colEmail, Me.colAddress, Me.colCity, Me.colTaxID, Me.colCustomerType, Me.colPaymentTermsDays, Me.colNotes, Me.colIsActive, Me.colLocation, Me.colAccountantEmail, Me.colAccountantMethod, Me.colInvoiceName, Me.colProfitabilityCode, Me.colCustomerIdentifier})
    Me.dgvCustomer.DataSource = Me.bsCtlCustomer
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvCustomer.DefaultCellStyle = styleDefaultCell
    Me.dgvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvCustomer.EnableHeadersVisualStyles = False
    Me.dgvCustomer.Location = New System.Drawing.Point(0, 25)
    Me.dgvCustomer.MultiSelect = False 
    Me.dgvCustomer.ContextMenuStrip = Me.cmsGrid 
    Me.dgvCustomer.Name = "dgvCustomer"
    Me.dgvCustomer.RowHeadersVisible = False
    Me.dgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvCustomer.Size = New System.Drawing.Size(712, 347)
    Me.dgvCustomer.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlCustomer
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleCustomerCode, Me.mnuColVisibleCustomerName, Me.mnuColVisiblePhone, Me.mnuColVisibleEmail, Me.mnuColVisibleAddress, Me.mnuColVisibleCity, Me.mnuColVisibleTaxID, Me.mnuColVisibleCustomerType, Me.mnuColVisiblePaymentTermsDays, Me.mnuColVisibleNotes, Me.mnuColVisibleIsActive, Me.mnuColVisibleLocation, Me.mnuColVisibleAccountantEmail, Me.mnuColVisibleAccountantMethod, Me.mnuColVisibleInvoiceName, Me.mnuColVisibleProfitabilityCode, Me.mnuColVisibleCustomerIdentifier, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsCustomerType
    Me.bsCustomerType.DataSource = GetType(clsComboList)
    'bsAccountantMethod
    Me.bsAccountantMethod.DataSource = GetType(clsComboList)
    '
    'bsCtlCustomer
    '
    Me.bsCtlCustomer.DataSource = GetType(clsCustomer)
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
    'colCustomerCode
    '
    Me.colCustomerCode.DataPropertyName = "CustomerCode"
    Me.colCustomerCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCustomerCode.HeaderText = "Customer Code"
    Me.colCustomerCode.Name = "colCustomerCode"
    Me.colCustomerCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCustomerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCustomerCode.Width = 60
    Me.colCustomerCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCustomerCode 
    ' 
    Me.mnuColVisibleCustomerCode.Checked = True 
    Me.mnuColVisibleCustomerCode.CheckOnClick = True 
    Me.mnuColVisibleCustomerCode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCustomerCode.Name = "mnuColVisibleCustomerCode" 
    Me.mnuColVisibleCustomerCode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCustomerCode.Text = "Customer Code" 
    ' 
    'colCustomerName
    '
    Me.colCustomerName.DataPropertyName = "CustomerName"
    Me.colCustomerName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCustomerName.HeaderText = "Customer Name"
    Me.colCustomerName.Name = "colCustomerName"
    Me.colCustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCustomerName.Width = 60
    Me.colCustomerName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCustomerName 
    ' 
    Me.mnuColVisibleCustomerName.Checked = True 
    Me.mnuColVisibleCustomerName.CheckOnClick = True 
    Me.mnuColVisibleCustomerName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCustomerName.Name = "mnuColVisibleCustomerName" 
    Me.mnuColVisibleCustomerName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCustomerName.Text = "Customer Name" 
    ' 
    'colPhone
    '
    Me.colPhone.DataPropertyName = "Phone"
    Me.colPhone.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colPhone.HeaderText = "Phone"
    Me.colPhone.Name = "colPhone"
    Me.colPhone.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPhone.Width = 60
    Me.colPhone.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePhone 
    ' 
    Me.mnuColVisiblePhone.Checked = True 
    Me.mnuColVisiblePhone.CheckOnClick = True 
    Me.mnuColVisiblePhone.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePhone.Name = "mnuColVisiblePhone" 
    Me.mnuColVisiblePhone.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePhone.Text = "Phone" 
    ' 
    'colEmail
    '
    Me.colEmail.DataPropertyName = "Email"
    Me.colEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colEmail.HeaderText = "Email"
    Me.colEmail.Name = "colEmail"
    Me.colEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colEmail.Width = 60
    Me.colEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleEmail 
    ' 
    Me.mnuColVisibleEmail.Checked = True 
    Me.mnuColVisibleEmail.CheckOnClick = True 
    Me.mnuColVisibleEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleEmail.Name = "mnuColVisibleEmail" 
    Me.mnuColVisibleEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleEmail.Text = "Email" 
    ' 
    'colAddress
    '
    Me.colAddress.DataPropertyName = "Address"
    Me.colAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAddress.HeaderText = "Address"
    Me.colAddress.Name = "colAddress"
    Me.colAddress.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAddress.Width = 60
    Me.colAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAddress 
    ' 
    Me.mnuColVisibleAddress.Checked = True 
    Me.mnuColVisibleAddress.CheckOnClick = True 
    Me.mnuColVisibleAddress.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAddress.Name = "mnuColVisibleAddress" 
    Me.mnuColVisibleAddress.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAddress.Text = "Address" 
    ' 
    'colCity
    '
    Me.colCity.DataPropertyName = "City"
    Me.colCity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCity.HeaderText = "City"
    Me.colCity.Name = "colCity"
    Me.colCity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCity.Width = 60
    Me.colCity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCity 
    ' 
    Me.mnuColVisibleCity.Checked = True 
    Me.mnuColVisibleCity.CheckOnClick = True 
    Me.mnuColVisibleCity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCity.Name = "mnuColVisibleCity" 
    Me.mnuColVisibleCity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCity.Text = "City" 
    ' 
    'colTaxID
    '
    Me.colTaxID.DataPropertyName = "TaxID"
    Me.colTaxID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colTaxID.HeaderText = "Tax ID"
    Me.colTaxID.Name = "colTaxID"
    Me.colTaxID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTaxID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTaxID.Width = 60
    Me.colTaxID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTaxID 
    ' 
    Me.mnuColVisibleTaxID.Checked = True 
    Me.mnuColVisibleTaxID.CheckOnClick = True 
    Me.mnuColVisibleTaxID.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTaxID.Name = "mnuColVisibleTaxID" 
    Me.mnuColVisibleTaxID.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTaxID.Text = "Tax ID" 
    ' 
    'colCustomerType
    '
    Me.colCustomerType.DataPropertyName = "CustomerType"
    Me.colCustomerType.DataSource = Me.bsCustomerType
    Me.colCustomerType.DisplayMember = "Text"
    Me.colCustomerType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colCustomerType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colCustomerType.HeaderText = "Customer Type"
    Me.colCustomerType.Name = "colCustomerType"
    Me.colCustomerType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCustomerType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCustomerType.ValueMember = "KeyEnum"
    Me.colCustomerType.Width = 60
    Me.colCustomerType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCustomerType 
    ' 
    Me.mnuColVisibleCustomerType.Checked = True 
    Me.mnuColVisibleCustomerType.CheckOnClick = True 
    Me.mnuColVisibleCustomerType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCustomerType.Name = "mnuColVisibleCustomerType" 
    Me.mnuColVisibleCustomerType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCustomerType.Text = "Customer Type" 
    ' 
    'colPaymentTermsDays
    '
    Me.colPaymentTermsDays.DataPropertyName = "PaymentTermsDays"
    Me.colPaymentTermsDays.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colPaymentTermsDays.HeaderText = "Payment Terms Days"
    Me.colPaymentTermsDays.Name = "colPaymentTermsDays"
    Me.colPaymentTermsDays.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPaymentTermsDays.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPaymentTermsDays.Width = 60
    Me.colPaymentTermsDays.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePaymentTermsDays 
    ' 
    Me.mnuColVisiblePaymentTermsDays.Checked = True 
    Me.mnuColVisiblePaymentTermsDays.CheckOnClick = True 
    Me.mnuColVisiblePaymentTermsDays.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePaymentTermsDays.Name = "mnuColVisiblePaymentTermsDays" 
    Me.mnuColVisiblePaymentTermsDays.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePaymentTermsDays.Text = "Payment Terms Days" 
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
    'colIsActive
    '
    Me.colIsActive.DataPropertyName = "IsActive"
    Me.colIsActive.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsActive.HeaderText = "Is Active"
    Me.colIsActive.Name = "colIsActive"
    Me.colIsActive.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsActive.Width = 60
    Me.colIsActive.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsActive 
    ' 
    Me.mnuColVisibleIsActive.Checked = True 
    Me.mnuColVisibleIsActive.CheckOnClick = True 
    Me.mnuColVisibleIsActive.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsActive.Name = "mnuColVisibleIsActive" 
    Me.mnuColVisibleIsActive.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsActive.Text = "Is Active" 
    ' 
    'colLocation
    '
    Me.colLocation.DataPropertyName = "Location"
    Me.colLocation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLocation.HeaderText = "Location"
    Me.colLocation.Name = "colLocation"
    Me.colLocation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLocation.Width = 60
    Me.colLocation.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLocation 
    ' 
    Me.mnuColVisibleLocation.Checked = True 
    Me.mnuColVisibleLocation.CheckOnClick = True 
    Me.mnuColVisibleLocation.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLocation.Name = "mnuColVisibleLocation" 
    Me.mnuColVisibleLocation.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLocation.Text = "Location" 
    ' 
    'colAccountantEmail
    '
    Me.colAccountantEmail.DataPropertyName = "AccountantEmail"
    Me.colAccountantEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colAccountantEmail.HeaderText = "Accountant Email"
    Me.colAccountantEmail.Name = "colAccountantEmail"
    Me.colAccountantEmail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAccountantEmail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAccountantEmail.Width = 60
    Me.colAccountantEmail.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAccountantEmail 
    ' 
    Me.mnuColVisibleAccountantEmail.Checked = True 
    Me.mnuColVisibleAccountantEmail.CheckOnClick = True 
    Me.mnuColVisibleAccountantEmail.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAccountantEmail.Name = "mnuColVisibleAccountantEmail" 
    Me.mnuColVisibleAccountantEmail.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAccountantEmail.Text = "Accountant Email" 
    ' 
    'colAccountantMethod
    '
    Me.colAccountantMethod.DataPropertyName = "AccountantMethod"
    Me.colAccountantMethod.DataSource = Me.bsAccountantMethod
    Me.colAccountantMethod.DisplayMember = "Text"
    Me.colAccountantMethod.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colAccountantMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colAccountantMethod.HeaderText = "Accountant Method"
    Me.colAccountantMethod.Name = "colAccountantMethod"
    Me.colAccountantMethod.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAccountantMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAccountantMethod.ValueMember = "KeyEnum"
    Me.colAccountantMethod.Width = 60
    Me.colAccountantMethod.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAccountantMethod 
    ' 
    Me.mnuColVisibleAccountantMethod.Checked = True 
    Me.mnuColVisibleAccountantMethod.CheckOnClick = True 
    Me.mnuColVisibleAccountantMethod.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAccountantMethod.Name = "mnuColVisibleAccountantMethod" 
    Me.mnuColVisibleAccountantMethod.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAccountantMethod.Text = "Accountant Method" 
    ' 
    'colInvoiceName
    '
    Me.colInvoiceName.DataPropertyName = "InvoiceName"
    Me.colInvoiceName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colInvoiceName.HeaderText = "Invoice Name"
    Me.colInvoiceName.Name = "colInvoiceName"
    Me.colInvoiceName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colInvoiceName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colInvoiceName.Width = 60
    Me.colInvoiceName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleInvoiceName 
    ' 
    Me.mnuColVisibleInvoiceName.Checked = True 
    Me.mnuColVisibleInvoiceName.CheckOnClick = True 
    Me.mnuColVisibleInvoiceName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleInvoiceName.Name = "mnuColVisibleInvoiceName" 
    Me.mnuColVisibleInvoiceName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleInvoiceName.Text = "Invoice Name" 
    ' 
    'colProfitabilityCode
    '
    Me.colProfitabilityCode.DataPropertyName = "ProfitabilityCode"
    Me.colProfitabilityCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProfitabilityCode.HeaderText = "Profitability Code"
    Me.colProfitabilityCode.Name = "colProfitabilityCode"
    Me.colProfitabilityCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProfitabilityCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProfitabilityCode.Width = 60
    Me.colProfitabilityCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProfitabilityCode 
    ' 
    Me.mnuColVisibleProfitabilityCode.Checked = True 
    Me.mnuColVisibleProfitabilityCode.CheckOnClick = True 
    Me.mnuColVisibleProfitabilityCode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProfitabilityCode.Name = "mnuColVisibleProfitabilityCode" 
    Me.mnuColVisibleProfitabilityCode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProfitabilityCode.Text = "Profitability Code" 
    ' 
    'colCustomerIdentifier
    '
    Me.colCustomerIdentifier.DataPropertyName = "CustomerIdentifier"
    Me.colCustomerIdentifier.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCustomerIdentifier.HeaderText = "Customer Identifier"
    Me.colCustomerIdentifier.Name = "colCustomerIdentifier"
    Me.colCustomerIdentifier.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCustomerIdentifier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCustomerIdentifier.Width = 60
    Me.colCustomerIdentifier.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCustomerIdentifier 
    ' 
    Me.mnuColVisibleCustomerIdentifier.Checked = True 
    Me.mnuColVisibleCustomerIdentifier.CheckOnClick = True 
    Me.mnuColVisibleCustomerIdentifier.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCustomerIdentifier.Name = "mnuColVisibleCustomerIdentifier" 
    Me.mnuColVisibleCustomerIdentifier.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCustomerIdentifier.Text = "Customer Identifier" 
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
    'ctlCustomerCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvCustomer)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccCustomerCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCustomerType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsAccountantMethod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlCustomer, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvCustomer As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlCustomer As System.Windows.Forms.BindingSource
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
  Friend WithEvents colCustomerCode As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCustomerCode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCustomerName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCustomerName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPhone As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePhone As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAddress As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAddress As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCity As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTaxID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTaxID As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsCustomerType As System.Windows.Forms.BindingSource
  Friend WithEvents colCustomerType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleCustomerType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPaymentTermsDays As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePaymentTermsDays As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsActive As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsActive As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLocation As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAccountantEmail As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleAccountantEmail As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsAccountantMethod As System.Windows.Forms.BindingSource
  Friend WithEvents colAccountantMethod As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleAccountantMethod As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colInvoiceName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleInvoiceName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProfitabilityCode As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProfitabilityCode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCustomerIdentifier As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCustomerIdentifier As System.Windows.Forms.ToolStripMenuItem 

End Class
