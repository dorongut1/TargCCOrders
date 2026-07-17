<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccCustomerDebtCol
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
    Me.dgvCustomerDebt = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlCustomerDebt = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsCustomer = New System.Windows.Forms.BindingSource(Me.components)
    Me.colCustomer = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colCustomerText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCustomer = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsOrderHeader = New System.Windows.Forms.BindingSource(Me.components)
    Me.colOrderHeader = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colOrderHeaderText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderHeader = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDebtAmount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDebtAmount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colPaidAmount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisiblePaidAmount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colRemainingAmount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRemainingAmount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDebtDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDebtDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDueDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsDebtStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colDebtStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleDebtStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNeedsAttention = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleNeedsAttention = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colProductTypes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProductTypes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDeliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDeliveryDate = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvCustomerDebt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsDebtStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlCustomerDebt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvCustomerDebt
    '
    Me.dgvCustomerDebt.AllowUserToAddRows = False
    Me.dgvCustomerDebt.AllowUserToDeleteRows = False
    Me.dgvCustomerDebt.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvCustomerDebt.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvCustomerDebt.AutoGenerateColumns = False
    Me.dgvCustomerDebt.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvCustomerDebt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvCustomerDebt.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvCustomerDebt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvCustomerDebt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colCustomer, Me.colCustomerText, Me.colOrderHeader, Me.colOrderHeaderText, Me.colDebtAmount, Me.colPaidAmount, Me.colRemainingAmount, Me.colDebtDate, Me.colDueDate, Me.colDebtStatus, Me.colNotes, Me.colNeedsAttention, Me.colProductTypes, Me.colDeliveryDate})
    Me.dgvCustomerDebt.DataSource = Me.bsCtlCustomerDebt
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvCustomerDebt.DefaultCellStyle = styleDefaultCell
    Me.dgvCustomerDebt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvCustomerDebt.EnableHeadersVisualStyles = False
    Me.dgvCustomerDebt.Location = New System.Drawing.Point(0, 25)
    Me.dgvCustomerDebt.MultiSelect = False 
    Me.dgvCustomerDebt.ContextMenuStrip = Me.cmsGrid 
    Me.dgvCustomerDebt.Name = "dgvCustomerDebt"
    Me.dgvCustomerDebt.RowHeadersVisible = False
    Me.dgvCustomerDebt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvCustomerDebt.Size = New System.Drawing.Size(712, 347)
    Me.dgvCustomerDebt.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlCustomerDebt
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleCustomer, Me.mnuColVisibleOrderHeader, Me.mnuColVisibleDebtAmount, Me.mnuColVisiblePaidAmount, Me.mnuColVisibleRemainingAmount, Me.mnuColVisibleDebtDate, Me.mnuColVisibleDueDate, Me.mnuColVisibleDebtStatus, Me.mnuColVisibleNotes, Me.mnuColVisibleNeedsAttention, Me.mnuColVisibleProductTypes, Me.mnuColVisibleDeliveryDate, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsCustomer
    Me.bsCustomer.DataSource = GetType(clsComboList)
    'bsOrderHeader
    Me.bsOrderHeader.DataSource = GetType(clsComboList)
    'bsDebtStatus
    Me.bsDebtStatus.DataSource = GetType(clsComboList)
    '
    'bsCtlCustomerDebt
    '
    Me.bsCtlCustomerDebt.DataSource = GetType(clsCustomerDebt)
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
    'colOrderHeader
    '
    Me.colOrderHeader.DataPropertyName = "OrderHeaderID"
    Me.colOrderHeader.DataSource = Me.bsOrderHeader
    Me.colOrderHeader.DisplayMember = "Text"
    Me.colOrderHeader.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colOrderHeader.HeaderText = "Order Header"
    Me.colOrderHeader.Name = "colOrderHeader"
    Me.colOrderHeader.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOrderHeader.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOrderHeader.ValueMember = "KeyLong"
    Me.colOrderHeader.Width = 60
    Me.colOrderHeader.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colOrderHeaderText 
    ' 
    Me.colOrderHeaderText.DataPropertyName = "OrderHeaderText" 
    Me.colOrderHeaderText.HeaderText = "OrderHeader" 
    Me.colOrderHeaderText.Name = "colOrderHeaderText" 
    Me.colOrderHeaderText.ReadOnly = True 
    Me.colOrderHeader.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOrderHeader 
    ' 
    Me.mnuColVisibleOrderHeader.Checked = True 
    Me.mnuColVisibleOrderHeader.CheckOnClick = True 
    Me.mnuColVisibleOrderHeader.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOrderHeader.Name = "mnuColVisibleOrderHeader" 
    Me.mnuColVisibleOrderHeader.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOrderHeader.Text = "Order Header" 
    ' 
    'colDebtAmount
    '
    Me.colDebtAmount.DataPropertyName = "DebtAmount"
    Me.colDebtAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colDebtAmount.HeaderText = "Debt Amount"
    Me.colDebtAmount.Name = "colDebtAmount"
    Me.colDebtAmount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDebtAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDebtAmount.Width = 60
    Me.colDebtAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDebtAmount 
    ' 
    Me.mnuColVisibleDebtAmount.Checked = True 
    Me.mnuColVisibleDebtAmount.CheckOnClick = True 
    Me.mnuColVisibleDebtAmount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDebtAmount.Name = "mnuColVisibleDebtAmount" 
    Me.mnuColVisibleDebtAmount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDebtAmount.Text = "Debt Amount" 
    ' 
    'colPaidAmount
    '
    Me.colPaidAmount.DataPropertyName = "PaidAmount"
    Me.colPaidAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colPaidAmount.HeaderText = "Paid Amount"
    Me.colPaidAmount.Name = "colPaidAmount"
    Me.colPaidAmount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colPaidAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colPaidAmount.Width = 60
    Me.colPaidAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisiblePaidAmount 
    ' 
    Me.mnuColVisiblePaidAmount.Checked = True 
    Me.mnuColVisiblePaidAmount.CheckOnClick = True 
    Me.mnuColVisiblePaidAmount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisiblePaidAmount.Name = "mnuColVisiblePaidAmount" 
    Me.mnuColVisiblePaidAmount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisiblePaidAmount.Text = "Paid Amount" 
    ' 
    'colRemainingAmount
    '
    Me.colRemainingAmount.DataPropertyName = "RemainingAmount"
    Me.colRemainingAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colRemainingAmount.HeaderText = "Remaining Amount"
    Me.colRemainingAmount.Name = "colRemainingAmount"
    Me.colRemainingAmount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRemainingAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRemainingAmount.Width = 60
    Me.colRemainingAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRemainingAmount 
    ' 
    Me.mnuColVisibleRemainingAmount.Checked = True 
    Me.mnuColVisibleRemainingAmount.CheckOnClick = True 
    Me.mnuColVisibleRemainingAmount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRemainingAmount.Name = "mnuColVisibleRemainingAmount" 
    Me.mnuColVisibleRemainingAmount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRemainingAmount.Text = "Remaining Amount" 
    ' 
    'colDebtDate
    '
    Me.colDebtDate.DataPropertyName = "DebtDate"
    Me.colDebtDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colDebtDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDebtDate.HeaderText = "Debt Date"
    Me.colDebtDate.Name = "colDebtDate"
    Me.colDebtDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDebtDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDebtDate.Width = 60
    Me.colDebtDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDebtDate 
    ' 
    Me.mnuColVisibleDebtDate.Checked = True 
    Me.mnuColVisibleDebtDate.CheckOnClick = True 
    Me.mnuColVisibleDebtDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDebtDate.Name = "mnuColVisibleDebtDate" 
    Me.mnuColVisibleDebtDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDebtDate.Text = "Debt Date" 
    ' 
    'colDueDate
    '
    Me.colDueDate.DataPropertyName = "DueDate"
    Me.colDueDate.DefaultCellStyle.Format = "dd-MM-yyyy"
    Me.colDueDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDueDate.HeaderText = "Due Date"
    Me.colDueDate.Name = "colDueDate"
    Me.colDueDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDueDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDueDate.Width = 60
    Me.colDueDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDueDate 
    ' 
    Me.mnuColVisibleDueDate.Checked = True 
    Me.mnuColVisibleDueDate.CheckOnClick = True 
    Me.mnuColVisibleDueDate.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDueDate.Name = "mnuColVisibleDueDate" 
    Me.mnuColVisibleDueDate.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDueDate.Text = "Due Date" 
    ' 
    'colDebtStatus
    '
    Me.colDebtStatus.DataPropertyName = "DebtStatus"
    Me.colDebtStatus.DataSource = Me.bsDebtStatus
    Me.colDebtStatus.DisplayMember = "Text"
    Me.colDebtStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colDebtStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colDebtStatus.HeaderText = "Debt Status"
    Me.colDebtStatus.Name = "colDebtStatus"
    Me.colDebtStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDebtStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDebtStatus.ValueMember = "KeyEnum"
    Me.colDebtStatus.Width = 60
    Me.colDebtStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDebtStatus 
    ' 
    Me.mnuColVisibleDebtStatus.Checked = True 
    Me.mnuColVisibleDebtStatus.CheckOnClick = True 
    Me.mnuColVisibleDebtStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDebtStatus.Name = "mnuColVisibleDebtStatus" 
    Me.mnuColVisibleDebtStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDebtStatus.Text = "Debt Status" 
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
    'colNeedsAttention
    '
    Me.colNeedsAttention.DataPropertyName = "NeedsAttention"
    Me.colNeedsAttention.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colNeedsAttention.HeaderText = "Needs Attention"
    Me.colNeedsAttention.Name = "colNeedsAttention"
    Me.colNeedsAttention.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNeedsAttention.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNeedsAttention.Width = 60
    Me.colNeedsAttention.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNeedsAttention 
    ' 
    Me.mnuColVisibleNeedsAttention.Checked = True 
    Me.mnuColVisibleNeedsAttention.CheckOnClick = True 
    Me.mnuColVisibleNeedsAttention.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNeedsAttention.Name = "mnuColVisibleNeedsAttention" 
    Me.mnuColVisibleNeedsAttention.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNeedsAttention.Text = "Needs Attention" 
    ' 
    'colProductTypes
    '
    Me.colProductTypes.DataPropertyName = "ProductTypes"
    Me.colProductTypes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colProductTypes.HeaderText = "Product Types"
    Me.colProductTypes.Name = "colProductTypes"
    Me.colProductTypes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProductTypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProductTypes.Width = 60
    Me.colProductTypes.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProductTypes 
    ' 
    Me.mnuColVisibleProductTypes.Checked = True 
    Me.mnuColVisibleProductTypes.CheckOnClick = True 
    Me.mnuColVisibleProductTypes.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProductTypes.Name = "mnuColVisibleProductTypes" 
    Me.mnuColVisibleProductTypes.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProductTypes.Text = "Product Types" 
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
    'ctlCustomerDebtCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvCustomerDebt)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccCustomerDebtCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvCustomerDebt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCustomer, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsDebtStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlCustomerDebt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvCustomerDebt As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlCustomerDebt As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsCustomer As System.Windows.Forms.BindingSource
  Friend WithEvents colCustomer As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colCustomerText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleCustomer As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsOrderHeader As System.Windows.Forms.BindingSource
  Friend WithEvents colOrderHeader As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colOrderHeaderText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleOrderHeader As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDebtAmount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDebtAmount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colPaidAmount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisiblePaidAmount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRemainingAmount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRemainingAmount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDebtDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDebtDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDueDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDueDate As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsDebtStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colDebtStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleDebtStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNeedsAttention As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleNeedsAttention As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProductTypes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProductTypes As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDeliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDeliveryDate As System.Windows.Forms.ToolStripMenuItem 

End Class
