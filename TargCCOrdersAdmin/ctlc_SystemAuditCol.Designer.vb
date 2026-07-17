<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_SystemAuditCol
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
    Me.dgvSystemAudit = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlSystemAudit = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colTableName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleTableName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colRowId = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRowId = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOperation = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOperation = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colOccurredAt = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleOccurredAt = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSqlCurrentUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSqlCurrentUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colChangedByUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleChangedByUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colActiveLoginID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleActiveLoginID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSqlSystemUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSqlSystemUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSqlAppName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSqlAppName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSqlHostName = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSqlHostName = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colChanges = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleChanges = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvSystemAudit, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsCtlSystemAudit, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvSystemAudit
    '
    Me.dgvSystemAudit.AllowUserToAddRows = False
    Me.dgvSystemAudit.AllowUserToDeleteRows = False
    Me.dgvSystemAudit.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvSystemAudit.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvSystemAudit.AutoGenerateColumns = False
    Me.dgvSystemAudit.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvSystemAudit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvSystemAudit.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvSystemAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvSystemAudit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colTableName, Me.colRowId, Me.colOperation, Me.colOccurredAt, Me.colSqlCurrentUser, Me.colChangedByUser, Me.colActiveLoginID, Me.colSqlSystemUser, Me.colSqlAppName, Me.colSqlHostName, Me.colChanges})
    Me.dgvSystemAudit.DataSource = Me.bsCtlSystemAudit
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvSystemAudit.DefaultCellStyle = styleDefaultCell
    Me.dgvSystemAudit.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvSystemAudit.EnableHeadersVisualStyles = False
    Me.dgvSystemAudit.Location = New System.Drawing.Point(0, 25)
    Me.dgvSystemAudit.MultiSelect = False 
    Me.dgvSystemAudit.ContextMenuStrip = Me.cmsGrid 
    Me.dgvSystemAudit.Name = "dgvSystemAudit"
    Me.dgvSystemAudit.RowHeadersVisible = False
    Me.dgvSystemAudit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvSystemAudit.Size = New System.Drawing.Size(712, 347)
    Me.dgvSystemAudit.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlSystemAudit
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleTableName, Me.mnuColVisibleRowId, Me.mnuColVisibleOperation, Me.mnuColVisibleOccurredAt, Me.mnuColVisibleSqlCurrentUser, Me.mnuColVisibleChangedByUser, Me.mnuColVisibleActiveLoginID, Me.mnuColVisibleSqlSystemUser, Me.mnuColVisibleSqlAppName, Me.mnuColVisibleSqlHostName, Me.mnuColVisibleChanges, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    '
    'bsCtlSystemAudit
    '
    Me.bsCtlSystemAudit.DataSource = GetType(csSystemAudit)
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
    'colRowId
    '
    Me.colRowId.DataPropertyName = "RowId"
    Me.colRowId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colRowId.HeaderText = "Row Id"
    Me.colRowId.Name = "colRowId"
    Me.colRowId.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRowId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRowId.Width = 60
    Me.colRowId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRowId 
    ' 
    Me.mnuColVisibleRowId.Checked = True 
    Me.mnuColVisibleRowId.CheckOnClick = True 
    Me.mnuColVisibleRowId.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRowId.Name = "mnuColVisibleRowId" 
    Me.mnuColVisibleRowId.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRowId.Text = "Row Id" 
    ' 
    'colOperation
    '
    Me.colOperation.DataPropertyName = "Operation"
    Me.colOperation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colOperation.HeaderText = "Operation"
    Me.colOperation.Name = "colOperation"
    Me.colOperation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOperation.Width = 60
    Me.colOperation.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOperation 
    ' 
    Me.mnuColVisibleOperation.Checked = True 
    Me.mnuColVisibleOperation.CheckOnClick = True 
    Me.mnuColVisibleOperation.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOperation.Name = "mnuColVisibleOperation" 
    Me.mnuColVisibleOperation.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOperation.Text = "Operation" 
    ' 
    'colOccurredAt
    '
    Me.colOccurredAt.DataPropertyName = "OccurredAt"
    Me.colOccurredAt.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colOccurredAt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colOccurredAt.HeaderText = "Occurred At"
    Me.colOccurredAt.Name = "colOccurredAt"
    Me.colOccurredAt.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colOccurredAt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colOccurredAt.Width = 60
    Me.colOccurredAt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleOccurredAt 
    ' 
    Me.mnuColVisibleOccurredAt.Checked = True 
    Me.mnuColVisibleOccurredAt.CheckOnClick = True 
    Me.mnuColVisibleOccurredAt.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleOccurredAt.Name = "mnuColVisibleOccurredAt" 
    Me.mnuColVisibleOccurredAt.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleOccurredAt.Text = "Occurred At" 
    ' 
    'colSqlCurrentUser
    '
    Me.colSqlCurrentUser.DataPropertyName = "SqlCurrentUser"
    Me.colSqlCurrentUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSqlCurrentUser.HeaderText = "Sql Current User"
    Me.colSqlCurrentUser.Name = "colSqlCurrentUser"
    Me.colSqlCurrentUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSqlCurrentUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSqlCurrentUser.Width = 60
    Me.colSqlCurrentUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSqlCurrentUser 
    ' 
    Me.mnuColVisibleSqlCurrentUser.Checked = True 
    Me.mnuColVisibleSqlCurrentUser.CheckOnClick = True 
    Me.mnuColVisibleSqlCurrentUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSqlCurrentUser.Name = "mnuColVisibleSqlCurrentUser" 
    Me.mnuColVisibleSqlCurrentUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSqlCurrentUser.Text = "Sql Current User" 
    ' 
    'colChangedByUser
    '
    Me.colChangedByUser.DataPropertyName = "ChangedByUser"
    Me.colChangedByUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colChangedByUser.HeaderText = "Changed By User"
    Me.colChangedByUser.Name = "colChangedByUser"
    Me.colChangedByUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colChangedByUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colChangedByUser.Width = 60
    Me.colChangedByUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleChangedByUser 
    ' 
    Me.mnuColVisibleChangedByUser.Checked = True 
    Me.mnuColVisibleChangedByUser.CheckOnClick = True 
    Me.mnuColVisibleChangedByUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleChangedByUser.Name = "mnuColVisibleChangedByUser" 
    Me.mnuColVisibleChangedByUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleChangedByUser.Text = "Changed By User" 
    ' 
    'colActiveLoginID
    '
    Me.colActiveLoginID.DataPropertyName = "ActiveLoginID"
    Me.colActiveLoginID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colActiveLoginID.HeaderText = "Active Login ID"
    Me.colActiveLoginID.Name = "colActiveLoginID"
    Me.colActiveLoginID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colActiveLoginID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colActiveLoginID.Width = 60
    Me.colActiveLoginID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleActiveLoginID 
    ' 
    Me.mnuColVisibleActiveLoginID.Checked = True 
    Me.mnuColVisibleActiveLoginID.CheckOnClick = True 
    Me.mnuColVisibleActiveLoginID.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleActiveLoginID.Name = "mnuColVisibleActiveLoginID" 
    Me.mnuColVisibleActiveLoginID.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleActiveLoginID.Text = "Active Login ID" 
    ' 
    'colSqlSystemUser
    '
    Me.colSqlSystemUser.DataPropertyName = "SqlSystemUser"
    Me.colSqlSystemUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSqlSystemUser.HeaderText = "Sql System User"
    Me.colSqlSystemUser.Name = "colSqlSystemUser"
    Me.colSqlSystemUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSqlSystemUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSqlSystemUser.Width = 60
    Me.colSqlSystemUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSqlSystemUser 
    ' 
    Me.mnuColVisibleSqlSystemUser.Checked = True 
    Me.mnuColVisibleSqlSystemUser.CheckOnClick = True 
    Me.mnuColVisibleSqlSystemUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSqlSystemUser.Name = "mnuColVisibleSqlSystemUser" 
    Me.mnuColVisibleSqlSystemUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSqlSystemUser.Text = "Sql System User" 
    ' 
    'colSqlAppName
    '
    Me.colSqlAppName.DataPropertyName = "SqlAppName"
    Me.colSqlAppName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSqlAppName.HeaderText = "Sql App Name"
    Me.colSqlAppName.Name = "colSqlAppName"
    Me.colSqlAppName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSqlAppName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSqlAppName.Width = 60
    Me.colSqlAppName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSqlAppName 
    ' 
    Me.mnuColVisibleSqlAppName.Checked = True 
    Me.mnuColVisibleSqlAppName.CheckOnClick = True 
    Me.mnuColVisibleSqlAppName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSqlAppName.Name = "mnuColVisibleSqlAppName" 
    Me.mnuColVisibleSqlAppName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSqlAppName.Text = "Sql App Name" 
    ' 
    'colSqlHostName
    '
    Me.colSqlHostName.DataPropertyName = "SqlHostName"
    Me.colSqlHostName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colSqlHostName.HeaderText = "Sql Host Name"
    Me.colSqlHostName.Name = "colSqlHostName"
    Me.colSqlHostName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSqlHostName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSqlHostName.Width = 60
    Me.colSqlHostName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSqlHostName 
    ' 
    Me.mnuColVisibleSqlHostName.Checked = True 
    Me.mnuColVisibleSqlHostName.CheckOnClick = True 
    Me.mnuColVisibleSqlHostName.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSqlHostName.Name = "mnuColVisibleSqlHostName" 
    Me.mnuColVisibleSqlHostName.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSqlHostName.Text = "Sql Host Name" 
    ' 
    'colChanges
    '
    Me.colChanges.DataPropertyName = "Changes"
    Me.colChanges.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colChanges.HeaderText = "Changes"
    Me.colChanges.Name = "colChanges"
    Me.colChanges.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colChanges.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colChanges.Width = 60
    Me.colChanges.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleChanges 
    ' 
    Me.mnuColVisibleChanges.Checked = True 
    Me.mnuColVisibleChanges.CheckOnClick = True 
    Me.mnuColVisibleChanges.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleChanges.Name = "mnuColVisibleChanges" 
    Me.mnuColVisibleChanges.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleChanges.Text = "Changes" 
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
    'ctlSystemAuditCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvSystemAudit)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_SystemAuditCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvSystemAudit, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsCtlSystemAudit, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvSystemAudit As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlSystemAudit As System.Windows.Forms.BindingSource
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
  Friend WithEvents colRowId As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRowId As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOperation As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOperation As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colOccurredAt As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleOccurredAt As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSqlCurrentUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSqlCurrentUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colChangedByUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleChangedByUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActiveLoginID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleActiveLoginID As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSqlSystemUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSqlSystemUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSqlAppName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSqlAppName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSqlHostName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSqlHostName As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colChanges As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleChanges As System.Windows.Forms.ToolStripMenuItem 

End Class
