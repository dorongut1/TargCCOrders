<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlc_LoggedJobCol
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
    Me.dgvLoggedJob = New System.Windows.Forms.DataGridView() 
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
    Me.bsCtlLoggedJob = New System.Windows.Forms.BindingSource(Me.components)
    Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleID = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsJob = New System.Windows.Forms.BindingSource(Me.components)
    Me.colJob = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colJobText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleJob = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colWhenStarted = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleWhenStarted = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colActivatingUser = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleActivatingUser = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colLastRunBy = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLastRunBy = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colExecutionTimeSec = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleExecutionTimeSec = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsRunStatus = New System.Windows.Forms.BindingSource(Me.components)
    Me.colRunStatus = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.mnuColVisibleRunStatus = New System.Windows.Forms.ToolStripMenuItem()  
    Me.colRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleRemarks = New System.Windows.Forms.ToolStripMenuItem() 
    Me.bsLoggedAlert = New System.Windows.Forms.BindingSource(Me.components)
    Me.colLoggedAlert = New System.Windows.Forms.DataGridViewComboBoxColumn() 
    Me.colLoggedAlertText = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleLoggedAlert = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colSuccessCount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleSuccessCount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.colFailureCount = New System.Windows.Forms.DataGridViewTextBoxColumn() 
    Me.mnuColVisibleFailureCount = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsReset = New System.Windows.Forms.ToolStripMenuItem() 
    Me.mnuColsHideMost = New System.Windows.Forms.ToolStripMenuItem()  
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()  
    Me.lblGrid = New System.Windows.Forms.Label()  
    Me.txtSearch = New System.Windows.Forms.TextBox()  
    Me.pnlHeader = New System.Windows.Forms.Panel()  
    CType(Me.dgvLoggedJob, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.BN.SuspendLayout()
    CType(Me.bsJob, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsRunStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsLoggedAlert, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.bsCtlLoggedJob, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'dgvLoggedJob
    '
    Me.dgvLoggedJob.AllowUserToAddRows = False
    Me.dgvLoggedJob.AllowUserToDeleteRows = False
    Me.dgvLoggedJob.AllowUserToOrderColumns = True
    styleAlternatingRowsDefaultCell.BackColor = System.Drawing.Color.White
    Me.dgvLoggedJob.AlternatingRowsDefaultCellStyle = styleAlternatingRowsDefaultCell
    Me.dgvLoggedJob.AutoGenerateColumns = False
    Me.dgvLoggedJob.BackgroundColor = System.Drawing.Color.Snow
    Me.dgvLoggedJob.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    styleColumnHeadersDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleColumnHeadersDefaultCell.BackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleColumnHeadersDefaultCell.ForeColor = System.Drawing.Color.White
    styleColumnHeadersDefaultCell.SelectionBackColor = System.Drawing.Color.Maroon
    styleColumnHeadersDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    styleColumnHeadersDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvLoggedJob.ColumnHeadersDefaultCellStyle = styleColumnHeadersDefaultCell
    Me.dgvLoggedJob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLoggedJob.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colJob, Me.colJobText, Me.colWhenStarted, Me.colActivatingUser, Me.colLastRunBy, Me.colExecutionTimeSec, Me.colRunStatus, Me.colRemarks, Me.colLoggedAlert, Me.colLoggedAlertText, Me.colSuccessCount, Me.colFailureCount})
    Me.dgvLoggedJob.DataSource = Me.bsCtlLoggedJob
    styleDefaultCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    styleDefaultCell.BackColor = System.Drawing.Color.Beige
    styleDefaultCell.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))
    styleDefaultCell.ForeColor = System.Drawing.SystemColors.ControlText
    styleDefaultCell.SelectionBackColor = System.Drawing.SystemColors.Highlight
    styleDefaultCell.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    styleDefaultCell.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvLoggedJob.DefaultCellStyle = styleDefaultCell
    Me.dgvLoggedJob.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvLoggedJob.EnableHeadersVisualStyles = False
    Me.dgvLoggedJob.Location = New System.Drawing.Point(0, 25)
    Me.dgvLoggedJob.MultiSelect = False 
    Me.dgvLoggedJob.ContextMenuStrip = Me.cmsGrid 
    Me.dgvLoggedJob.Name = "dgvLoggedJob"
    Me.dgvLoggedJob.RowHeadersVisible = False
    Me.dgvLoggedJob.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.dgvLoggedJob.Size = New System.Drawing.Size(712, 347)
    Me.dgvLoggedJob.TabIndex = 0
    '
    '
    'BN
    '
    Me.BN.AddNewItem = Nothing
    Me.BN.BackColor = System.Drawing.SystemColors.Control
    Me.BN.BindingSource = Me.bsCtlLoggedJob
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
    Me.btnColumns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColVisibleID, Me.mnuColVisibleJob, Me.mnuColVisibleWhenStarted, Me.mnuColVisibleActivatingUser, Me.mnuColVisibleLastRunBy, Me.mnuColVisibleExecutionTimeSec, Me.mnuColVisibleRunStatus, Me.mnuColVisibleRemarks, Me.mnuColVisibleLoggedAlert, Me.mnuColVisibleSuccessCount, Me.mnuColVisibleFailureCount, Me.mnuColsReset, Me.mnuColsHideMost}) 
    Me.btnColumns.ImageTransparentColor = System.Drawing.Color.Magenta 
    Me.btnColumns.Name = "btnColumns" 
    Me.btnColumns.Size = New System.Drawing.Size(60, 22) 
    Me.btnColumns.Text = "Columns" 
    ' 
    'bsJob
    Me.bsJob.DataSource = GetType(clsComboList)
    'bsRunStatus
    Me.bsRunStatus.DataSource = GetType(clsComboList)
    'bsLoggedAlert
    Me.bsLoggedAlert.DataSource = GetType(clsComboList)
    '
    'bsCtlLoggedJob
    '
    Me.bsCtlLoggedJob.DataSource = GetType(csLoggedJob)
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
    'colJob
    '
    Me.colJob.DataPropertyName = "JobID"
    Me.colJob.DataSource = Me.bsJob
    Me.colJob.DisplayMember = "Text"
    Me.colJob.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colJob.HeaderText = "Job"
    Me.colJob.Name = "colJob"
    Me.colJob.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colJob.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colJob.ValueMember = "KeyLong"
    Me.colJob.Width = 60
    Me.colJob.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colJobText 
    ' 
    Me.colJobText.DataPropertyName = "JobText" 
    Me.colJobText.HeaderText = "Job" 
    Me.colJobText.Name = "colJobText" 
    Me.colJobText.ReadOnly = True 
    Me.colJob.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleJob 
    ' 
    Me.mnuColVisibleJob.Checked = True 
    Me.mnuColVisibleJob.CheckOnClick = True 
    Me.mnuColVisibleJob.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleJob.Name = "mnuColVisibleJob" 
    Me.mnuColVisibleJob.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleJob.Text = "Job" 
    ' 
    'colWhenStarted
    '
    Me.colWhenStarted.DataPropertyName = "WhenStarted"
    Me.colWhenStarted.DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss"
    Me.colWhenStarted.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colWhenStarted.HeaderText = "When Started"
    Me.colWhenStarted.Name = "colWhenStarted"
    Me.colWhenStarted.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colWhenStarted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colWhenStarted.Width = 60
    Me.colWhenStarted.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleWhenStarted 
    ' 
    Me.mnuColVisibleWhenStarted.Checked = True 
    Me.mnuColVisibleWhenStarted.CheckOnClick = True 
    Me.mnuColVisibleWhenStarted.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleWhenStarted.Name = "mnuColVisibleWhenStarted" 
    Me.mnuColVisibleWhenStarted.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleWhenStarted.Text = "When Started" 
    ' 
    'colActivatingUser
    '
    Me.colActivatingUser.DataPropertyName = "ActivatingUser"
    Me.colActivatingUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colActivatingUser.HeaderText = "Activating User"
    Me.colActivatingUser.Name = "colActivatingUser"
    Me.colActivatingUser.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colActivatingUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colActivatingUser.Width = 60
    Me.colActivatingUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleActivatingUser 
    ' 
    Me.mnuColVisibleActivatingUser.Checked = True 
    Me.mnuColVisibleActivatingUser.CheckOnClick = True 
    Me.mnuColVisibleActivatingUser.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleActivatingUser.Name = "mnuColVisibleActivatingUser" 
    Me.mnuColVisibleActivatingUser.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleActivatingUser.Text = "Activating User" 
    ' 
    'colLastRunBy
    '
    Me.colLastRunBy.DataPropertyName = "LastRunBy"
    Me.colLastRunBy.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colLastRunBy.HeaderText = "Last Run By"
    Me.colLastRunBy.Name = "colLastRunBy"
    Me.colLastRunBy.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLastRunBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLastRunBy.Width = 60
    Me.colLastRunBy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLastRunBy 
    ' 
    Me.mnuColVisibleLastRunBy.Checked = True 
    Me.mnuColVisibleLastRunBy.CheckOnClick = True 
    Me.mnuColVisibleLastRunBy.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLastRunBy.Name = "mnuColVisibleLastRunBy" 
    Me.mnuColVisibleLastRunBy.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLastRunBy.Text = "Last Run By" 
    ' 
    'colExecutionTimeSec
    '
    Me.colExecutionTimeSec.DataPropertyName = "ExecutionTimeSec"
    Me.colExecutionTimeSec.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colExecutionTimeSec.HeaderText = "Execution Time Sec"
    Me.colExecutionTimeSec.Name = "colExecutionTimeSec"
    Me.colExecutionTimeSec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colExecutionTimeSec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colExecutionTimeSec.Width = 60
    Me.colExecutionTimeSec.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleExecutionTimeSec 
    ' 
    Me.mnuColVisibleExecutionTimeSec.Checked = True 
    Me.mnuColVisibleExecutionTimeSec.CheckOnClick = True 
    Me.mnuColVisibleExecutionTimeSec.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleExecutionTimeSec.Name = "mnuColVisibleExecutionTimeSec" 
    Me.mnuColVisibleExecutionTimeSec.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleExecutionTimeSec.Text = "Execution Time Sec" 
    ' 
    'colRunStatus
    '
    Me.colRunStatus.DataPropertyName = "RunStatus"
    Me.colRunStatus.DataSource = Me.bsRunStatus
    Me.colRunStatus.DisplayMember = "Text"
    Me.colRunStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colRunStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 
    Me.colRunStatus.HeaderText = "Run Status"
    Me.colRunStatus.Name = "colRunStatus"
    Me.colRunStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRunStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRunStatus.ValueMember = "KeyEnum"
    Me.colRunStatus.Width = 60
    Me.colRunStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRunStatus 
    ' 
    Me.mnuColVisibleRunStatus.Checked = True 
    Me.mnuColVisibleRunStatus.CheckOnClick = True 
    Me.mnuColVisibleRunStatus.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRunStatus.Name = "mnuColVisibleRunStatus" 
    Me.mnuColVisibleRunStatus.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRunStatus.Text = "Run Status" 
    ' 
    'colRemarks
    '
    Me.colRemarks.DataPropertyName = "Remarks"
    Me.colRemarks.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft 
    Me.colRemarks.HeaderText = "Remarks"
    Me.colRemarks.Name = "colRemarks"
    Me.colRemarks.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colRemarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colRemarks.Width = 60
    Me.colRemarks.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleRemarks 
    ' 
    Me.mnuColVisibleRemarks.Checked = True 
    Me.mnuColVisibleRemarks.CheckOnClick = True 
    Me.mnuColVisibleRemarks.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleRemarks.Name = "mnuColVisibleRemarks" 
    Me.mnuColVisibleRemarks.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleRemarks.Text = "Remarks" 
    ' 
    'colLoggedAlert
    '
    Me.colLoggedAlert.DataPropertyName = "LoggedAlertID"
    Me.colLoggedAlert.DataSource = Me.bsLoggedAlert
    Me.colLoggedAlert.DisplayMember = "Text"
    Me.colLoggedAlert.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
    Me.colLoggedAlert.HeaderText = "Logged Alert"
    Me.colLoggedAlert.Name = "colLoggedAlert"
    Me.colLoggedAlert.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colLoggedAlert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colLoggedAlert.ValueMember = "KeyLong"
    Me.colLoggedAlert.Width = 60
    Me.colLoggedAlert.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    ' 
    'colLoggedAlertText 
    ' 
    Me.colLoggedAlertText.DataPropertyName = "LoggedAlertText" 
    Me.colLoggedAlertText.HeaderText = "LoggedAlert" 
    Me.colLoggedAlertText.Name = "colLoggedAlertText" 
    Me.colLoggedAlertText.ReadOnly = True 
    Me.colLoggedAlert.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleLoggedAlert 
    ' 
    Me.mnuColVisibleLoggedAlert.Checked = True 
    Me.mnuColVisibleLoggedAlert.CheckOnClick = True 
    Me.mnuColVisibleLoggedAlert.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleLoggedAlert.Name = "mnuColVisibleLoggedAlert" 
    Me.mnuColVisibleLoggedAlert.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleLoggedAlert.Text = "Logged Alert" 
    ' 
    'colSuccessCount
    '
    Me.colSuccessCount.DataPropertyName = "SuccessCount"
    Me.colSuccessCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colSuccessCount.HeaderText = "Success Count"
    Me.colSuccessCount.Name = "colSuccessCount"
    Me.colSuccessCount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colSuccessCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colSuccessCount.Width = 60
    Me.colSuccessCount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleSuccessCount 
    ' 
    Me.mnuColVisibleSuccessCount.Checked = True 
    Me.mnuColVisibleSuccessCount.CheckOnClick = True 
    Me.mnuColVisibleSuccessCount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleSuccessCount.Name = "mnuColVisibleSuccessCount" 
    Me.mnuColVisibleSuccessCount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleSuccessCount.Text = "Success Count" 
    ' 
    'colFailureCount
    '
    Me.colFailureCount.DataPropertyName = "FailureCount"
    Me.colFailureCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight 
    Me.colFailureCount.HeaderText = "Failure Count"
    Me.colFailureCount.Name = "colFailureCount"
    Me.colFailureCount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    Me.colFailureCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    Me.colFailureCount.Width = 60
    Me.colFailureCount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter 
    'mnuColVisibleFailureCount 
    ' 
    Me.mnuColVisibleFailureCount.Checked = True 
    Me.mnuColVisibleFailureCount.CheckOnClick = True 
    Me.mnuColVisibleFailureCount.CheckState = System.Windows.Forms.CheckState.Checked 
    Me.mnuColVisibleFailureCount.Name = "mnuColVisibleFailureCount" 
    Me.mnuColVisibleFailureCount.Size = New System.Drawing.Size(152, 22) 
    Me.mnuColVisibleFailureCount.Text = "Failure Count" 
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
    'ctlLoggedJobCol
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font 
    Me.Controls.Add(Me.dgvLoggedJob)
    Me.Controls.Add(Me.BN)
    Me.Controls.Add(Me.pnlHeader)
    Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte))   
    Me.Name = "ctlc_LoggedJobCol"
    Me.Size = New System.Drawing.Size(712, 372)
    CType(Me.dgvLoggedJob, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.BN, System.ComponentModel.ISupportInitialize).EndInit()
  ' 
    'tssColumns 
    ' 
    Me.tssColumns.Name = "tssColumns" 
    Me.tssColumns.Size = New System.Drawing.Size(6, 25) 
  ' 
    CType(Me.bsJob, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsRunStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsLoggedAlert, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.bsCtlLoggedJob, System.ComponentModel.ISupportInitialize).EndInit()
    Me.BN.ResumeLayout(False)
    Me.BN.PerformLayout()
    Me.pnlHeader.ResumeLayout(False)
    Me.pnlHeader.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents dgvLoggedJob As System.Windows.Forms.DataGridView
  Friend WithEvents bsCtlLoggedJob As System.Windows.Forms.BindingSource
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
  Friend WithEvents bsJob As System.Windows.Forms.BindingSource
  Friend WithEvents colJob As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colJobText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleJob As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colWhenStarted As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleWhenStarted As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colActivatingUser As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleActivatingUser As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colLastRunBy As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleLastRunBy As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colExecutionTimeSec As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleExecutionTimeSec As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsRunStatus As System.Windows.Forms.BindingSource
  Friend WithEvents colRunStatus As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents mnuColVisibleRunStatus As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleRemarks As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents bsLoggedAlert As System.Windows.Forms.BindingSource
  Friend WithEvents colLoggedAlert As System.Windows.Forms.DataGridViewComboBoxColumn
  Private WithEvents colLoggedAlertText As System.Windows.Forms.DataGridViewTextBoxColumn 
  Friend WithEvents mnuColVisibleLoggedAlert As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colSuccessCount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleSuccessCount As System.Windows.Forms.ToolStripMenuItem 
  Friend WithEvents colFailureCount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents mnuColVisibleFailureCount As System.Windows.Forms.ToolStripMenuItem 

End Class
