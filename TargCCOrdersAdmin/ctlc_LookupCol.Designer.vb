<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LookupCol
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
    Me.dgvLookup = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlLookup = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsParentLookupType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colParentLookupType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleParentLookupType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colParentCode = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleParentCode = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLookupType = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLookupType = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleLookupType = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colCode = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleCode = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleText = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTextLocalized = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTextLocalized = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDescription = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colDescriptionLocalized = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleDescriptionLocalized = New System.Windows.Forms.ToolStripMenuItem()  
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvLookup, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsParentLookupType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLookupType, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlLookup, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvLookup
    '
    Me.dgvLookup.AllowUserToAddRows = False
    Me.dgvLookup.AllowUserToDeleteRows = False
    Me.dgvLookup.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvLookup.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvLookup.AutoGenerateColumns = False
    Me.dgvLookup.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvLookup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLookup.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvLookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLookup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colParentLookupType, Me.colParentCode, Me.colLookupType, Me.colCode, Me.colText, Me.colTextLocalized, Me.colDescription, Me.colDescriptionLocalized})
    Me.dgvLookup.DataSource = Me.bsCtlLookup
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvLookup.DefaultCellStyle = styleDefaultCell
    Me.dgvLookup.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvLookup.EnableHeadersVisualStyles = False
    Me.dgvLookup.Location = New System.Drawing.Point(0, 25)
    Me.dgvLookup.MultiSelect = False 
    Me.dgvLookup.ContextMenuStrip = Me.cmsGrid 
    Me.dgvLookup.Name = "dgvLookup"
    Me.dgvLookup.RowHeadersVisible = False
    Me.dgvLookup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLookup.Size = New System.Drawing.Size(712, 347)
    Me.dgvLookup.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlLookup
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleParentLookupType, Me.mnuColVisibleParentCode, Me.mnuColVisibleLookupType, Me.mnuColVisibleCode, Me.mnuColVisibleText, Me.mnuColVisibleTextLocalized, Me.mnuColVisibleDescription, Me.mnuColVisibleDescriptionLocalized, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsParentLookupType
    Me.bsParentLookupType.DataSource = GetType(clsComboList)
    'bsLookupType
    Me.bsLookupType.DataSource = GetType(clsComboList)
    '
    'bsCtlLookup
    '
    Me.bsCtlLookup.DataSource = GetType(csLookup)
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
    'colParentLookupType
    '
    Me.colParentLookupType.DataPropertyName = "ParentLookupType"
    Me.colParentLookupType.DataSource = Me.bsParentLookupType
    Me.colParentLookupType.DisplayMember = "Text"
    Me.colParentLookupType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colParentLookupType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colParentLookupType.HeaderText = "Parent Lookup Type"
    Me.colParentLookupType.Name = "colParentLookupType"
    Me.colParentLookupType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colParentLookupType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colParentLookupType.ValueMember = "KeyEnum"
    Me.colParentLookupType.Width = 60
    Me.colParentLookupType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleParentLookupType 
    ' 
    Me.mnuColVisibleParentLookupType.Checked = True 
    Me.mnuColVisibleParentLookupType.CheckOnClick = True 
    Me.mnuColVisibleParentLookupType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleParentLookupType.Name = "mnuColVisibleParentLookupType" 
    Me.mnuColVisibleParentLookupType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleParentLookupType.Text = "Parent Lookup Type" 
    ' 
    'colParentCode
    '
    Me.colParentCode.DataPropertyName = "ParentCode"
    Me.colParentCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colParentCode.HeaderText = "Parent Code"
    Me.colParentCode.Name = "colParentCode"
    Me.colParentCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colParentCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colParentCode.Width = 60
    Me.colParentCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleParentCode 
    ' 
    Me.mnuColVisibleParentCode.Checked = True 
    Me.mnuColVisibleParentCode.CheckOnClick = True 
    Me.mnuColVisibleParentCode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleParentCode.Name = "mnuColVisibleParentCode" 
    Me.mnuColVisibleParentCode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleParentCode.Text = "Parent Code" 
    ' 
    'colLookupType
    '
    Me.colLookupType.DataPropertyName = "LookupType"
    Me.colLookupType.DataSource = Me.bsLookupType
    Me.colLookupType.DisplayMember = "Text"
    Me.colLookupType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colLookupType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colLookupType.HeaderText = "Lookup Type"
    Me.colLookupType.Name = "colLookupType"
    Me.colLookupType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLookupType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLookupType.ValueMember = "KeyEnum"
    Me.colLookupType.Width = 60
    Me.colLookupType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLookupType 
    ' 
    Me.mnuColVisibleLookupType.Checked = True 
    Me.mnuColVisibleLookupType.CheckOnClick = True 
    Me.mnuColVisibleLookupType.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLookupType.Name = "mnuColVisibleLookupType" 
    Me.mnuColVisibleLookupType.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLookupType.Text = "Lookup Type" 
    ' 
    'colCode
    '
    Me.colCode.DataPropertyName = "Code"
    Me.colCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colCode.HeaderText = "Code"
    Me.colCode.Name = "colCode"
    Me.colCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colCode.Width = 60
    Me.colCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleCode 
    ' 
    Me.mnuColVisibleCode.Checked = True 
    Me.mnuColVisibleCode.CheckOnClick = True 
    Me.mnuColVisibleCode.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleCode.Name = "mnuColVisibleCode" 
    Me.mnuColVisibleCode.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleCode.Text = "Code" 
    ' 
    'colText
    '
    Me.colText.DataPropertyName = "Text"
    Me.colText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colText.HeaderText = "Text"
    Me.colText.Name = "colText"
    Me.colText.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colText.Width = 60
    Me.colText.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleText 
    ' 
    Me.mnuColVisibleText.Checked = True 
    Me.mnuColVisibleText.CheckOnClick = True 
    Me.mnuColVisibleText.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleText.Name = "mnuColVisibleText" 
    Me.mnuColVisibleText.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleText.Text = "Text" 
    ' 
    'colTextLocalized
    '
    Me.colTextLocalized.DataPropertyName = "TextLocalized"
    Me.colTextLocalized.HeaderText = "TextLocalized"
    Me.colTextLocalized.Name = "colTextLocalized"
    Me.colTextLocalized.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colTextLocalized.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colTextLocalized.Width = 60
    Me.colText.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'mnuColVisibleTextLocalized 
    ' 
    Me.mnuColVisibleTextLocalized.Checked = True 
    Me.mnuColVisibleTextLocalized.CheckOnClick = True 
    Me.mnuColVisibleTextLocalized.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleTextLocalized.Name = "mnuColVisibleTextLocalized" 
    Me.mnuColVisibleText.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleTextLocalized.Text = "TextLocalized" 
    ' 
    'colDescription
    '
    Me.colDescription.DataPropertyName = "Description"
    Me.colDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colDescription.HeaderText = "Description"
    Me.colDescription.Name = "colDescription"
    Me.colDescription.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDescription.Width = 60
    Me.colDescription.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleDescription 
    ' 
    Me.mnuColVisibleDescription.Checked = True 
    Me.mnuColVisibleDescription.CheckOnClick = True 
    Me.mnuColVisibleDescription.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDescription.Name = "mnuColVisibleDescription" 
    Me.mnuColVisibleDescription.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDescription.Text = "Description" 
    ' 
    'colDescriptionLocalized
    '
    Me.colDescriptionLocalized.DataPropertyName = "DescriptionLocalized"
    Me.colDescriptionLocalized.HeaderText = "DescriptionLocalized"
    Me.colDescriptionLocalized.Name = "colDescriptionLocalized"
    Me.colDescriptionLocalized.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colDescriptionLocalized.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colDescriptionLocalized.Width = 60
    Me.colDescription.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'mnuColVisibleDescriptionLocalized 
    ' 
    Me.mnuColVisibleDescriptionLocalized.Checked = True 
    Me.mnuColVisibleDescriptionLocalized.CheckOnClick = True 
    Me.mnuColVisibleDescriptionLocalized.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleDescriptionLocalized.Name = "mnuColVisibleDescriptionLocalized" 
    Me.mnuColVisibleDescription.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleDescriptionLocalized.Text = "DescriptionLocalized" 
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
    'ctlLookupCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvLookup)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LookupCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvLookup, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsParentLookupType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLookupType, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlLookup, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvLookup As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlLookup As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsParentLookupType As System.Windows.Forms.BindingSource
  Friend WithEvents colParentLookupType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleParentLookupType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colParentCode As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleParentCode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLookupType As System.Windows.Forms.BindingSource
  Friend WithEvents colLookupType As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleLookupType As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colCode As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleCode As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colText As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleText As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colTextLocalized As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleTextLocalized As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDescription As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colDescriptionLocalized As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleDescriptionLocalized As System.Windows.Forms.ToolStripMenuItem 

End Class
