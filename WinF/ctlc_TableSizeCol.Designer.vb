<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_TableSizeCol
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
    Me.dgvTableSize = New System.Windows.Forms.DataGridView() 
    Me.BN = New System.Windows.Forms.BindingNavigator(Me.components)
    Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator() 
    Me.btnEdit = New System.Windows.Forms.ToolStripButton() 
    Me.btnImport = New System.Windows.Forms.ToolStripButton() 
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
    Me.bsCtlTableSize = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTableName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTableName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colNumberOfRows = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleNumberOfRows = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colReservedSizeKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleReservedSizeKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDataSizeKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDataSizeKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colIndexSizeKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleIndexSizeKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUnusedSizeKb = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleUnusedSizeKb = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvTableSize, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCtlTableSize, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvTableSize
    '
    Me.dgvTableSize.AllowUserToAddRows = False
    Me.dgvTableSize.AllowUserToDeleteRows = False
    Me.dgvTableSize.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvTableSize.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvTableSize.AutoGenerateColumns = False
    Me.dgvTableSize.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvTableSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvTableSize.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvTableSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvTableSize.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colTableName, Me.colNumberOfRows, Me.colReservedSizeKb, Me.colDataSizeKb, Me.colIndexSizeKb, Me.colUnusedSizeKb})
    Me.dgvTableSize.DataSource = Me.bsCtlTableSize
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvTableSize.DefaultCellStyle = styleDefaultCell
    Me.dgvTableSize.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvTableSize.EnableHeadersVisualStyles = False
    Me.dgvTableSize.Location = New System.Drawing.Point(0, 25)
    Me.dgvTableSize.MultiSelect = False 
    Me.dgvTableSize.ContextMenuStrip = Me.cmsGrid 
    Me.dgvTableSize.Name = "dgvTableSize"
    Me.dgvTableSize.RowHeadersVisible = False
    Me.dgvTableSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvTableSize.Size = New System.Drawing.Size(712, 347)
    Me.dgvTableSize.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlTableSize
    Me.BN.CountItem = Nothing
    Me.BN.DeleteItem = Nothing
    Me.BN.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.BN.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorSeparator, Me.btnEdit, Me.btnImport, Me.btnCeaseEdit, Me.tssEditMode, Me.lblEditMode, Me.tssReports, Me.btnSpreadsheet, Me.lblStatus, Me.btnReport, Me.tssColumns, Me.btnColumns})
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleTableName, Me.mnuColVisibleNumberOfRows, Me.mnuColVisibleReservedSizeKb, Me.mnuColVisibleDataSizeKb, Me.mnuColVisibleIndexSizeKb, Me.mnuColVisibleUnusedSizeKb, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    '
    'bsCtlTableSize
    '
    Me.bsCtlTableSize.DataSource = GetType(csTableSize)
    '
    'colID
    '
    Me.colID.DataPropertyName = "ID"
    Me.colID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colID.HeaderText = "ID"
    Me.colID.Name = "colID"
    Me.colID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colID.ReadOnly = False
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
    'colTableName
    '
    Me.colTableName.DataPropertyName = "TableName"
    Me.colTableName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colTableName.HeaderText = "Table Name"
    Me.colTableName.Name = "colTableName"
    Me.colTableName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTableName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTableName.Width = 60
    Me.colTableName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTableName 
    ' 
    Me.mnuColVisibleTableName.Checked = True 
    Me.mnuColVisibleTableName.CheckOnClick = True 
    Me.mnuColVisibleTableName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTableName.Name = "mnuColVisibleTableName" 
    Me.mnuColVisibleTableName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTableName.Text = "Table Name" 
    ' 
    'colNumberOfRows
    '
    Me.colNumberOfRows.DataPropertyName = "NumberOfRows"
    Me.colNumberOfRows.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colNumberOfRows.HeaderText = "Number Of Rows"
    Me.colNumberOfRows.Name = "colNumberOfRows"
    Me.colNumberOfRows.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colNumberOfRows.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colNumberOfRows.Width = 60
    Me.colNumberOfRows.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleNumberOfRows 
    ' 
    Me.mnuColVisibleNumberOfRows.Checked = True 
    Me.mnuColVisibleNumberOfRows.CheckOnClick = True 
    Me.mnuColVisibleNumberOfRows.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleNumberOfRows.Name = "mnuColVisibleNumberOfRows" 
    Me.mnuColVisibleNumberOfRows.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleNumberOfRows.Text = "Number Of Rows" 
    ' 
    'colReservedSizeKb
    '
    Me.colReservedSizeKb.DataPropertyName = "ReservedSizeKb"
    Me.colReservedSizeKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colReservedSizeKb.HeaderText = "Reserved Size Kb"
    Me.colReservedSizeKb.Name = "colReservedSizeKb"
    Me.colReservedSizeKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colReservedSizeKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colReservedSizeKb.Width = 60
    Me.colReservedSizeKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleReservedSizeKb 
    ' 
    Me.mnuColVisibleReservedSizeKb.Checked = True 
    Me.mnuColVisibleReservedSizeKb.CheckOnClick = True 
    Me.mnuColVisibleReservedSizeKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleReservedSizeKb.Name = "mnuColVisibleReservedSizeKb" 
    Me.mnuColVisibleReservedSizeKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleReservedSizeKb.Text = "Reserved Size Kb" 
    ' 
    'colDataSizeKb
    '
    Me.colDataSizeKb.DataPropertyName = "DataSizeKb"
    Me.colDataSizeKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colDataSizeKb.HeaderText = "Data Size Kb"
    Me.colDataSizeKb.Name = "colDataSizeKb"
    Me.colDataSizeKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDataSizeKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDataSizeKb.Width = 60
    Me.colDataSizeKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDataSizeKb 
    ' 
    Me.mnuColVisibleDataSizeKb.Checked = True 
    Me.mnuColVisibleDataSizeKb.CheckOnClick = True 
    Me.mnuColVisibleDataSizeKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDataSizeKb.Name = "mnuColVisibleDataSizeKb" 
    Me.mnuColVisibleDataSizeKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDataSizeKb.Text = "Data Size Kb" 
    ' 
    'colIndexSizeKb
    '
    Me.colIndexSizeKb.DataPropertyName = "IndexSizeKb"
    Me.colIndexSizeKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIndexSizeKb.HeaderText = "Index Size Kb"
    Me.colIndexSizeKb.Name = "colIndexSizeKb"
    Me.colIndexSizeKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIndexSizeKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIndexSizeKb.Width = 60
    Me.colIndexSizeKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIndexSizeKb 
    ' 
    Me.mnuColVisibleIndexSizeKb.Checked = True 
    Me.mnuColVisibleIndexSizeKb.CheckOnClick = True 
    Me.mnuColVisibleIndexSizeKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIndexSizeKb.Name = "mnuColVisibleIndexSizeKb" 
    Me.mnuColVisibleIndexSizeKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIndexSizeKb.Text = "Index Size Kb" 
    ' 
    'colUnusedSizeKb
    '
    Me.colUnusedSizeKb.DataPropertyName = "UnusedSizeKb"
    Me.colUnusedSizeKb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colUnusedSizeKb.HeaderText = "Unused Size Kb"
    Me.colUnusedSizeKb.Name = "colUnusedSizeKb"
    Me.colUnusedSizeKb.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUnusedSizeKb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUnusedSizeKb.Width = 60
    Me.colUnusedSizeKb.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUnusedSizeKb 
    ' 
    Me.mnuColVisibleUnusedSizeKb.Checked = True 
    Me.mnuColVisibleUnusedSizeKb.CheckOnClick = True 
    Me.mnuColVisibleUnusedSizeKb.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUnusedSizeKb.Name = "mnuColVisibleUnusedSizeKb" 
    Me.mnuColVisibleUnusedSizeKb.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUnusedSizeKb.Text = "Unused Size Kb" 
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
    'ctlTableSizeCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvTableSize)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_TableSizeCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvTableSize, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCtlTableSize, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvTableSize As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlTableSize As System.Windows.Forms.BindingSource
  Friend WithEvents BN As System.Windows.Forms.BindingNavigator
  Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnImport As System.Windows.Forms.ToolStripButton
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
  Friend WithEvents colTableName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTableName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colNumberOfRows As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleNumberOfRows As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colReservedSizeKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleReservedSizeKb As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDataSizeKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDataSizeKb As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIndexSizeKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleIndexSizeKb As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUnusedSizeKb As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleUnusedSizeKb As System.Windows.Forms.ToolStripMenuItem 

End Class
