<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlccProductPriceCol
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
    Me.dgvProductPrice = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlProductPrice = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsProduct = New System.Windows.Forms.BindingSource(Me.components)
    Me.colProduct = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colProductText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleProduct = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsCustomerType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colCustomerType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleCustomerType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSellingPrice = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colMinQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleMinQuantity = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDiscountPercent = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDiscountPercent = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNotes = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvProductPrice, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCustomerType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlProductPrice, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvProductPrice
    '
    Me.dgvProductPrice.AllowUserToAddRows = False
    Me.dgvProductPrice.AllowUserToDeleteRows = False
    Me.dgvProductPrice.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvProductPrice.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvProductPrice.AutoGenerateColumns = False
    Me.dgvProductPrice.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvProductPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvProductPrice.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvProductPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvProductPrice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colProduct, Me.colProductText, Me.colCustomerType, Me.colSellingPrice, Me.colMinQuantity, Me.colDiscountPercent, Me.colNotes})
    Me.dgvProductPrice.DataSource = Me.bsCtlProductPrice
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvProductPrice.DefaultCellStyle = styleDefaultCell
    Me.dgvProductPrice.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvProductPrice.EnableHeadersVisualStyles = False
    Me.dgvProductPrice.Location = New System.Drawing.Point(0, 25)
    Me.dgvProductPrice.MultiSelect = False 
    Me.dgvProductPrice.ContextMenuStrip = Me.cmsGrid 
    Me.dgvProductPrice.Name = "dgvProductPrice"
    Me.dgvProductPrice.RowHeadersVisible = False
    Me.dgvProductPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvProductPrice.Size = New System.Drawing.Size(712, 347)
    Me.dgvProductPrice.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlProductPrice
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleProduct, Me.mnuColVisibleCustomerType, Me.mnuColVisibleSellingPrice, Me.mnuColVisibleMinQuantity, Me.mnuColVisibleDiscountPercent, Me.mnuColVisibleNotes, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsProduct
    Me.bsProduct.DataSource = GetType(clsComboList)
    'bsCustomerType
    Me.bsCustomerType.DataSource = GetType(clsComboList)
    '
    'bsCtlProductPrice
    '
    Me.bsCtlProductPrice.DataSource = GetType(clsProductPrice)
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
    'colSellingPrice
    '
    Me.colSellingPrice.DataPropertyName = "SellingPrice"
    Me.colSellingPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colSellingPrice.HeaderText = "Selling Price"
    Me.colSellingPrice.Name = "colSellingPrice"
    Me.colSellingPrice.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSellingPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSellingPrice.Width = 60
    Me.colSellingPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSellingPrice 
    ' 
    Me.mnuColVisibleSellingPrice.Checked = True 
    Me.mnuColVisibleSellingPrice.CheckOnClick = True 
    Me.mnuColVisibleSellingPrice.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSellingPrice.Name = "mnuColVisibleSellingPrice" 
    Me.mnuColVisibleSellingPrice.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSellingPrice.Text = "Selling Price" 
    ' 
    'colMinQuantity
    '
    Me.colMinQuantity.DataPropertyName = "MinQuantity"
    Me.colMinQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colMinQuantity.HeaderText = "Min Quantity"
    Me.colMinQuantity.Name = "colMinQuantity"
    Me.colMinQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colMinQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colMinQuantity.Width = 60
    Me.colMinQuantity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleMinQuantity 
    ' 
    Me.mnuColVisibleMinQuantity.Checked = True 
    Me.mnuColVisibleMinQuantity.CheckOnClick = True 
    Me.mnuColVisibleMinQuantity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleMinQuantity.Name = "mnuColVisibleMinQuantity" 
    Me.mnuColVisibleMinQuantity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleMinQuantity.Text = "Min Quantity" 
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
    'ctlProductPriceCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvProductPrice)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlccProductPriceCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvProductPrice, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCustomerType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlProductPrice, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvProductPrice As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlProductPrice As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsProduct As System.Windows.Forms.BindingSource
  Friend WithEvents colProduct As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colProductText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleProduct As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsCustomerType As System.Windows.Forms.BindingSource
  Friend WithEvents colCustomerType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleCustomerType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSellingPrice As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colMinQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleMinQuantity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDiscountPercent As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDiscountPercent As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNotes As System.Windows.Forms.ToolStripMenuItem 

End Class
