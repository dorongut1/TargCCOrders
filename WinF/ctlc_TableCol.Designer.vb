<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_TableCol
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
    Me.dgvTable = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlTable = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDefaultTextFields = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDefaultTextFields = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colUsedForIdentity = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleUsedForIdentity = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colIsSingleRow = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleIsSingleRow = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colCanAdd = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCanAdd = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCanEdit = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCanEdit = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colCanDelete = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCanDelete = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colAuditAdd = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleAuditAdd = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colAuditEdit = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleAuditEdit = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colAuditDelete = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleAuditDelete = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colTrackRowChangers = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleTrackRowChangers = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colCreateUIMenu = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleCreateUIMenu = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colCreateUICollection = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleCreateUICollection = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colCreateUIEntity = New System.Windows.Forms.DataGridViewCheckBoxColumn() 
    Me.mnuColVisibleCreateUIEntity = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colSortOrder = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSortOrder = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvTable, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCtlTable, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvTable
    '
    Me.dgvTable.AllowUserToAddRows = False
    Me.dgvTable.AllowUserToDeleteRows = False
    Me.dgvTable.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvTable.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvTable.AutoGenerateColumns = False
    Me.dgvTable.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvTable.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colName, Me.colDefaultTextFields, Me.colUsedForIdentity, Me.colIsSingleRow, Me.colCanAdd, Me.colCanEdit, Me.colCanDelete, Me.colAuditAdd, Me.colAuditEdit, Me.colAuditDelete, Me.colTrackRowChangers, Me.colCreateUIMenu, Me.colCreateUICollection, Me.colCreateUIEntity, Me.colSortOrder})
    Me.dgvTable.DataSource = Me.bsCtlTable
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvTable.DefaultCellStyle = styleDefaultCell
    Me.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvTable.EnableHeadersVisualStyles = False
    Me.dgvTable.Location = New System.Drawing.Point(0, 25)
    Me.dgvTable.MultiSelect = False 
    Me.dgvTable.ContextMenuStrip = Me.cmsGrid 
    Me.dgvTable.Name = "dgvTable"
    Me.dgvTable.RowHeadersVisible = False
    Me.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvTable.Size = New System.Drawing.Size(712, 347)
    Me.dgvTable.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlTable
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleName, Me.mnuColVisibleDefaultTextFields, Me.mnuColVisibleUsedForIdentity, Me.mnuColVisibleIsSingleRow, Me.mnuColVisibleCanAdd, Me.mnuColVisibleCanEdit, Me.mnuColVisibleCanDelete, Me.mnuColVisibleAuditAdd, Me.mnuColVisibleAuditEdit, Me.mnuColVisibleAuditDelete, Me.mnuColVisibleTrackRowChangers, Me.mnuColVisibleCreateUIMenu, Me.mnuColVisibleCreateUICollection, Me.mnuColVisibleCreateUIEntity, Me.mnuColVisibleSortOrder, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    '
    'bsCtlTable
    '
    Me.bsCtlTable.DataSource = GetType(csTable)
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
    'colName
    '
    Me.colName.DataPropertyName = "Name"
    Me.colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colName.HeaderText = "Name"
    Me.colName.Name = "colName"
    Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colName.Width = 60
    Me.colName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleName 
    ' 
    Me.mnuColVisibleName.Checked = True 
    Me.mnuColVisibleName.CheckOnClick = True 
    Me.mnuColVisibleName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleName.Name = "mnuColVisibleName" 
    Me.mnuColVisibleName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleName.Text = "Name" 
    ' 
    'colDefaultTextFields
    '
    Me.colDefaultTextFields.DataPropertyName = "DefaultTextFields"
    Me.colDefaultTextFields.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDefaultTextFields.HeaderText = "Default Text Fields"
    Me.colDefaultTextFields.Name = "colDefaultTextFields"
    Me.colDefaultTextFields.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDefaultTextFields.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDefaultTextFields.Width = 60
    Me.colDefaultTextFields.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDefaultTextFields 
    ' 
    Me.mnuColVisibleDefaultTextFields.Checked = True 
    Me.mnuColVisibleDefaultTextFields.CheckOnClick = True 
    Me.mnuColVisibleDefaultTextFields.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDefaultTextFields.Name = "mnuColVisibleDefaultTextFields" 
    Me.mnuColVisibleDefaultTextFields.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDefaultTextFields.Text = "Default Text Fields" 
    ' 
    'colUsedForIdentity
    '
    Me.colUsedForIdentity.DataPropertyName = "UsedForIdentity"
    Me.colUsedForIdentity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colUsedForIdentity.HeaderText = "Used For Identity"
    Me.colUsedForIdentity.Name = "colUsedForIdentity"
    Me.colUsedForIdentity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colUsedForIdentity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colUsedForIdentity.Width = 60
    Me.colUsedForIdentity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleUsedForIdentity 
    ' 
    Me.mnuColVisibleUsedForIdentity.Checked = True 
    Me.mnuColVisibleUsedForIdentity.CheckOnClick = True 
    Me.mnuColVisibleUsedForIdentity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleUsedForIdentity.Name = "mnuColVisibleUsedForIdentity" 
    Me.mnuColVisibleUsedForIdentity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleUsedForIdentity.Text = "Used For Identity" 
    ' 
    'colIsSingleRow
    '
    Me.colIsSingleRow.DataPropertyName = "IsSingleRow"
    Me.colIsSingleRow.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colIsSingleRow.HeaderText = "Is Single Row"
    Me.colIsSingleRow.Name = "colIsSingleRow"
    Me.colIsSingleRow.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colIsSingleRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colIsSingleRow.Width = 60
    Me.colIsSingleRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleIsSingleRow 
    ' 
    Me.mnuColVisibleIsSingleRow.Checked = True 
    Me.mnuColVisibleIsSingleRow.CheckOnClick = True 
    Me.mnuColVisibleIsSingleRow.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleIsSingleRow.Name = "mnuColVisibleIsSingleRow" 
    Me.mnuColVisibleIsSingleRow.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleIsSingleRow.Text = "Is Single Row" 
    ' 
    'colCanAdd
    '
    Me.colCanAdd.DataPropertyName = "CanAdd"
    Me.colCanAdd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCanAdd.HeaderText = "Can Add"
    Me.colCanAdd.Name = "colCanAdd"
    Me.colCanAdd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCanAdd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCanAdd.Width = 60
    Me.colCanAdd.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCanAdd 
    ' 
    Me.mnuColVisibleCanAdd.Checked = True 
    Me.mnuColVisibleCanAdd.CheckOnClick = True 
    Me.mnuColVisibleCanAdd.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCanAdd.Name = "mnuColVisibleCanAdd" 
    Me.mnuColVisibleCanAdd.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCanAdd.Text = "Can Add" 
    ' 
    'colCanEdit
    '
    Me.colCanEdit.DataPropertyName = "CanEdit"
    Me.colCanEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCanEdit.HeaderText = "Can Edit"
    Me.colCanEdit.Name = "colCanEdit"
    Me.colCanEdit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCanEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCanEdit.Width = 60
    Me.colCanEdit.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCanEdit 
    ' 
    Me.mnuColVisibleCanEdit.Checked = True 
    Me.mnuColVisibleCanEdit.CheckOnClick = True 
    Me.mnuColVisibleCanEdit.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCanEdit.Name = "mnuColVisibleCanEdit" 
    Me.mnuColVisibleCanEdit.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCanEdit.Text = "Can Edit" 
    ' 
    'colCanDelete
    '
    Me.colCanDelete.DataPropertyName = "CanDelete"
    Me.colCanDelete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCanDelete.HeaderText = "Can Delete"
    Me.colCanDelete.Name = "colCanDelete"
    Me.colCanDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCanDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCanDelete.Width = 60
    Me.colCanDelete.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCanDelete 
    ' 
    Me.mnuColVisibleCanDelete.Checked = True 
    Me.mnuColVisibleCanDelete.CheckOnClick = True 
    Me.mnuColVisibleCanDelete.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCanDelete.Name = "mnuColVisibleCanDelete" 
    Me.mnuColVisibleCanDelete.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCanDelete.Text = "Can Delete" 
    ' 
    'colAuditAdd
    '
    Me.colAuditAdd.DataPropertyName = "AuditAdd"
    Me.colAuditAdd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colAuditAdd.HeaderText = "Audit Add"
    Me.colAuditAdd.Name = "colAuditAdd"
    Me.colAuditAdd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAuditAdd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAuditAdd.Width = 60
    Me.colAuditAdd.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAuditAdd 
    ' 
    Me.mnuColVisibleAuditAdd.Checked = True 
    Me.mnuColVisibleAuditAdd.CheckOnClick = True 
    Me.mnuColVisibleAuditAdd.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAuditAdd.Name = "mnuColVisibleAuditAdd" 
    Me.mnuColVisibleAuditAdd.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAuditAdd.Text = "Audit Add" 
    ' 
    'colAuditEdit
    '
    Me.colAuditEdit.DataPropertyName = "AuditEdit"
    Me.colAuditEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colAuditEdit.HeaderText = "Audit Edit"
    Me.colAuditEdit.Name = "colAuditEdit"
    Me.colAuditEdit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAuditEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAuditEdit.Width = 60
    Me.colAuditEdit.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAuditEdit 
    ' 
    Me.mnuColVisibleAuditEdit.Checked = True 
    Me.mnuColVisibleAuditEdit.CheckOnClick = True 
    Me.mnuColVisibleAuditEdit.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAuditEdit.Name = "mnuColVisibleAuditEdit" 
    Me.mnuColVisibleAuditEdit.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAuditEdit.Text = "Audit Edit" 
    ' 
    'colAuditDelete
    '
    Me.colAuditDelete.DataPropertyName = "AuditDelete"
    Me.colAuditDelete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colAuditDelete.HeaderText = "Audit Delete"
    Me.colAuditDelete.Name = "colAuditDelete"
    Me.colAuditDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colAuditDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colAuditDelete.Width = 60
    Me.colAuditDelete.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleAuditDelete 
    ' 
    Me.mnuColVisibleAuditDelete.Checked = True 
    Me.mnuColVisibleAuditDelete.CheckOnClick = True 
    Me.mnuColVisibleAuditDelete.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleAuditDelete.Name = "mnuColVisibleAuditDelete" 
    Me.mnuColVisibleAuditDelete.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleAuditDelete.Text = "Audit Delete" 
    ' 
    'colTrackRowChangers
    '
    Me.colTrackRowChangers.DataPropertyName = "TrackRowChangers"
    Me.colTrackRowChangers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colTrackRowChangers.HeaderText = "Track Row Changers"
    Me.colTrackRowChangers.Name = "colTrackRowChangers"
    Me.colTrackRowChangers.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTrackRowChangers.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTrackRowChangers.Width = 60
    Me.colTrackRowChangers.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleTrackRowChangers 
    ' 
    Me.mnuColVisibleTrackRowChangers.Checked = True 
    Me.mnuColVisibleTrackRowChangers.CheckOnClick = True 
    Me.mnuColVisibleTrackRowChangers.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTrackRowChangers.Name = "mnuColVisibleTrackRowChangers" 
    Me.mnuColVisibleTrackRowChangers.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTrackRowChangers.Text = "Track Row Changers" 
    ' 
    'colCreateUIMenu
    '
    Me.colCreateUIMenu.DataPropertyName = "CreateUIMenu"
    Me.colCreateUIMenu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colCreateUIMenu.HeaderText = "Create UI Menu"
    Me.colCreateUIMenu.Name = "colCreateUIMenu"
    Me.colCreateUIMenu.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCreateUIMenu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCreateUIMenu.Width = 60
    Me.colCreateUIMenu.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCreateUIMenu 
    ' 
    Me.mnuColVisibleCreateUIMenu.Checked = True 
    Me.mnuColVisibleCreateUIMenu.CheckOnClick = True 
    Me.mnuColVisibleCreateUIMenu.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCreateUIMenu.Name = "mnuColVisibleCreateUIMenu" 
    Me.mnuColVisibleCreateUIMenu.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCreateUIMenu.Text = "Create UI Menu" 
    ' 
    'colCreateUICollection
    '
    Me.colCreateUICollection.DataPropertyName = "CreateUICollection"
    Me.colCreateUICollection.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colCreateUICollection.HeaderText = "Create UI Collection"
    Me.colCreateUICollection.Name = "colCreateUICollection"
    Me.colCreateUICollection.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCreateUICollection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCreateUICollection.Width = 60
    Me.colCreateUICollection.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCreateUICollection 
    ' 
    Me.mnuColVisibleCreateUICollection.Checked = True 
    Me.mnuColVisibleCreateUICollection.CheckOnClick = True 
    Me.mnuColVisibleCreateUICollection.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCreateUICollection.Name = "mnuColVisibleCreateUICollection" 
    Me.mnuColVisibleCreateUICollection.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCreateUICollection.Text = "Create UI Collection" 
    ' 
    'colCreateUIEntity
    '
    Me.colCreateUIEntity.DataPropertyName = "CreateUIEntity"
    Me.colCreateUIEntity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colCreateUIEntity.HeaderText = "Create UI Entity"
    Me.colCreateUIEntity.Name = "colCreateUIEntity"
    Me.colCreateUIEntity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCreateUIEntity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCreateUIEntity.Width = 60
    Me.colCreateUIEntity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCreateUIEntity 
    ' 
    Me.mnuColVisibleCreateUIEntity.Checked = True 
    Me.mnuColVisibleCreateUIEntity.CheckOnClick = True 
    Me.mnuColVisibleCreateUIEntity.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCreateUIEntity.Name = "mnuColVisibleCreateUIEntity" 
    Me.mnuColVisibleCreateUIEntity.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCreateUIEntity.Text = "Create UI Entity" 
    ' 
    'colSortOrder
    '
    Me.colSortOrder.DataPropertyName = "SortOrder"
    Me.colSortOrder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colSortOrder.HeaderText = "Sort Order"
    Me.colSortOrder.Name = "colSortOrder"
    Me.colSortOrder.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSortOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSortOrder.Width = 60
    Me.colSortOrder.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSortOrder 
    ' 
    Me.mnuColVisibleSortOrder.Checked = True 
    Me.mnuColVisibleSortOrder.CheckOnClick = True 
    Me.mnuColVisibleSortOrder.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSortOrder.Name = "mnuColVisibleSortOrder" 
    Me.mnuColVisibleSortOrder.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSortOrder.Text = "Sort Order" 
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
    'ctlTableCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvTable)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_TableCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvTable, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCtlTable, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvTable As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlTable As System.Windows.Forms.BindingSource
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
  Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDefaultTextFields As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDefaultTextFields As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colUsedForIdentity As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleUsedForIdentity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colIsSingleRow As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleIsSingleRow As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCanAdd As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCanAdd As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCanEdit As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCanEdit As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCanDelete As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCanDelete As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAuditAdd As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleAuditAdd As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAuditEdit As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleAuditEdit As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colAuditDelete As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleAuditDelete As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTrackRowChangers As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleTrackRowChangers As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCreateUIMenu As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleCreateUIMenu As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCreateUICollection As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleCreateUICollection As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCreateUIEntity As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents mnuColVisibleCreateUIEntity As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSortOrder As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSortOrder As System.Windows.Forms.ToolStripMenuItem 

End Class
