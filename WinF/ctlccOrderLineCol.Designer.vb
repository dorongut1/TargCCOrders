<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccOrderLineCol
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
    Me.dgvOrderLine = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlOrderLine = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsOrderHeader = New System.Windows.Forms.BindingSource(Me.components)
    Me.colOrderHeader = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colOrderHeaderText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOrderHeader = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsProduct = New System.Windows.Forms.BindingSource(Me.components)
    Me.colProduct = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colProductText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProduct = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleQuantity = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUnitPrice = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUnitPrice = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDiscountPercent = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDiscountPercent = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUnitCost = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLineNumber = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLineNumber = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLineTotal = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLineTotal = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTotalCost = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colProfit = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProfit = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvOrderLine, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlOrderLine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvOrderLine
    '
    Me.dgvOrderLine.AllowUserToAddRows = False
    Me.dgvOrderLine.AllowUserToDeleteRows = False
    Me.dgvOrderLine.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvOrderLine.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvOrderLine.AutoGenerateColumns = False
    Me.dgvOrderLine.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvOrderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvOrderLine.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvOrderLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvOrderLine.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colOrderHeader, Me.colOrderHeaderText, Me.colProduct, Me.colProductText, Me.colQuantity, Me.colUnitPrice, Me.colDiscountPercent, Me.colUnitCost, Me.colLineNumber, Me.colLineTotal, Me.colTotalCost, Me.colProfit})
    Me.dgvOrderLine.DataSource = Me.bsCtlOrderLine
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvOrderLine.DefaultCellStyle = styleDefaultCell
    Me.dgvOrderLine.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvOrderLine.EnableHeadersVisualStyles = False
    Me.dgvOrderLine.Location = New System.Drawing.Point(0, 25)
    Me.dgvOrderLine.MultiSelect = False 
    Me.dgvOrderLine.ContextMenuStrip = Me.cmsGrid 
    Me.dgvOrderLine.Name = "dgvOrderLine"
    Me.dgvOrderLine.RowHeadersVisible = False
    Me.dgvOrderLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvOrderLine.Size = New System.Drawing.Size(712, 347)
    Me.dgvOrderLine.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlOrderLine
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleOrderHeader, Me.mnuColVisibleProduct, Me.mnuColVisibleQuantity, Me.mnuColVisibleUnitPrice, Me.mnuColVisibleDiscountPercent, Me.mnuColVisibleUnitCost, Me.mnuColVisibleLineNumber, Me.mnuColVisibleLineTotal, Me.mnuColVisibleTotalCost, Me.mnuColVisibleProfit, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsOrderHeader
    Me.bsOrderHeader.DataSource = GetType(clsComboList)
    'bsProduct
    Me.bsProduct.DataSource = GetType(clsComboList)
    '
    'bsCtlOrderLine
    '
    Me.bsCtlOrderLine.DataSource = GetType(clsOrderLine)
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
    'colProduct
    '
    Me.colProduct.DataPropertyName = "ProductID"
    Me.colProduct.DataSource = Me.bsProduct
    Me.colProduct.DisplayMember = "Text"
    Me.colProduct.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colProduct.HeaderText = "Product"
    Me.colProduct.Name = "colProduct"
    Me.colProduct.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProduct.ValueMember = "KeyLong"
    Me.colProduct.Width = 60
    Me.colProduct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colProductText 
    ' 
    Me.colProductText.DataPropertyName = "ProductText" 
    Me.colProductText.HeaderText = "Product" 
    Me.colProductText.Name = "colProductText" 
    Me.colProductText.ReadOnly = True 
    Me.colProduct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProduct 
    ' 
    Me.mnuColVisibleProduct.Checked = True 
    Me.mnuColVisibleProduct.CheckOnClick = True 
    Me.mnuColVisibleProduct.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProduct.Name = "mnuColVisibleProduct" 
    Me.mnuColVisibleProduct.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProduct.Text = "Product" 
    ' 
    'colQuantity
    '
    Me.colQuantity.DataPropertyName = "Quantity"
    Me.colQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colQuantity.HeaderText = "Quantity"
    Me.colQuantity.Name = "colQuantity"
    Me.colQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colQuantity.Width = 60
    Me.colQuantity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleQuantity 
    ' 
    Me.mnuColVisibleQuantity.Checked = True 
    Me.mnuColVisibleQuantity.CheckOnClick = True 
    Me.mnuColVisibleQuantity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleQuantity.Name = "mnuColVisibleQuantity" 
    Me.mnuColVisibleQuantity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleQuantity.Text = "Quantity" 
    ' 
    'colUnitPrice
    '
    Me.colUnitPrice.DataPropertyName = "UnitPrice"
    Me.colUnitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colUnitPrice.HeaderText = "Unit Price"
    Me.colUnitPrice.Name = "colUnitPrice"
    Me.colUnitPrice.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUnitPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUnitPrice.Width = 60
    Me.colUnitPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUnitPrice 
    ' 
    Me.mnuColVisibleUnitPrice.Checked = True 
    Me.mnuColVisibleUnitPrice.CheckOnClick = True 
    Me.mnuColVisibleUnitPrice.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUnitPrice.Name = "mnuColVisibleUnitPrice" 
    Me.mnuColVisibleUnitPrice.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUnitPrice.Text = "Unit Price" 
    ' 
    'colDiscountPercent
    '
    Me.colDiscountPercent.DataPropertyName = "DiscountPercent"
    Me.colDiscountPercent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colDiscountPercent.HeaderText = "Discount Percent"
    Me.colDiscountPercent.Name = "colDiscountPercent"
    Me.colDiscountPercent.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDiscountPercent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDiscountPercent.Width = 60
    Me.colDiscountPercent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDiscountPercent 
    ' 
    Me.mnuColVisibleDiscountPercent.Checked = True 
    Me.mnuColVisibleDiscountPercent.CheckOnClick = True 
    Me.mnuColVisibleDiscountPercent.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDiscountPercent.Name = "mnuColVisibleDiscountPercent" 
    Me.mnuColVisibleDiscountPercent.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDiscountPercent.Text = "Discount Percent" 
    ' 
    'colUnitCost
    '
    Me.colUnitCost.DataPropertyName = "UnitCost"
    Me.colUnitCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colUnitCost.HeaderText = "Unit Cost"
    Me.colUnitCost.Name = "colUnitCost"
    Me.colUnitCost.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUnitCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUnitCost.Width = 60
    Me.colUnitCost.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUnitCost 
    ' 
    Me.mnuColVisibleUnitCost.Checked = True 
    Me.mnuColVisibleUnitCost.CheckOnClick = True 
    Me.mnuColVisibleUnitCost.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUnitCost.Name = "mnuColVisibleUnitCost" 
    Me.mnuColVisibleUnitCost.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUnitCost.Text = "Unit Cost" 
    ' 
    'colLineNumber
    '
    Me.colLineNumber.DataPropertyName = "LineNumber"
    Me.colLineNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colLineNumber.HeaderText = "Line Number"
    Me.colLineNumber.Name = "colLineNumber"
    Me.colLineNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLineNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLineNumber.Width = 60
    Me.colLineNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLineNumber 
    ' 
    Me.mnuColVisibleLineNumber.Checked = True 
    Me.mnuColVisibleLineNumber.CheckOnClick = True 
    Me.mnuColVisibleLineNumber.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLineNumber.Name = "mnuColVisibleLineNumber" 
    Me.mnuColVisibleLineNumber.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLineNumber.Text = "Line Number" 
    ' 
    'colLineTotal
    '
    Me.colLineTotal.DataPropertyName = "LineTotal"
    Me.colLineTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colLineTotal.HeaderText = "Line Total"
    Me.colLineTotal.Name = "colLineTotal"
    Me.colLineTotal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLineTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLineTotal.Width = 60
    Me.colLineTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLineTotal 
    ' 
    Me.mnuColVisibleLineTotal.Checked = True 
    Me.mnuColVisibleLineTotal.CheckOnClick = True 
    Me.mnuColVisibleLineTotal.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLineTotal.Name = "mnuColVisibleLineTotal" 
    Me.mnuColVisibleLineTotal.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLineTotal.Text = "Line Total" 
    ' 
    'colTotalCost
    '
    Me.colTotalCost.DataPropertyName = "TotalCost"
    Me.colTotalCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTotalCost.HeaderText = "Total Cost"
    Me.colTotalCost.Name = "colTotalCost"
    Me.colTotalCost.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTotalCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTotalCost.Width = 60
    Me.colTotalCost.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTotalCost 
    ' 
    Me.mnuColVisibleTotalCost.Checked = True 
    Me.mnuColVisibleTotalCost.CheckOnClick = True 
    Me.mnuColVisibleTotalCost.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTotalCost.Name = "mnuColVisibleTotalCost" 
    Me.mnuColVisibleTotalCost.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTotalCost.Text = "Total Cost" 
    ' 
    'colProfit
    '
    Me.colProfit.DataPropertyName = "Profit"
    Me.colProfit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colProfit.HeaderText = "Profit"
    Me.colProfit.Name = "colProfit"
    Me.colProfit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colProfit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colProfit.Width = 60
    Me.colProfit.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleProfit 
    ' 
    Me.mnuColVisibleProfit.Checked = True 
    Me.mnuColVisibleProfit.CheckOnClick = True 
    Me.mnuColVisibleProfit.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleProfit.Name = "mnuColVisibleProfit" 
    Me.mnuColVisibleProfit.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleProfit.Text = "Profit" 
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
    'ctlOrderLineCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvOrderLine)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccOrderLineCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvOrderLine, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlOrderLine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvOrderLine As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlOrderLine As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsOrderHeader As System.Windows.Forms.BindingSource
  Friend WithEvents colOrderHeader As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colOrderHeaderText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleOrderHeader As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsProduct As System.Windows.Forms.BindingSource
  Friend WithEvents colProduct As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colProductText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleProduct As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleQuantity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUnitPrice As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUnitPrice As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDiscountPercent As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDiscountPercent As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUnitCost As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLineNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLineNumber As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLineTotal As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLineTotal As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTotalCost As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colProfit As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleProfit As System.Windows.Forms.ToolStripMenuItem 

End Class
